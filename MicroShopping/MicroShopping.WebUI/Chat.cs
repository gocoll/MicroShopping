using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using MicroShopping.Domain;
using MicroShopping.Domain.Concrete;
using SignalR.Hubs;

namespace MicroShopping.WebUI
{
    public class Chat : Hub
    {
        EfAuctionRepository auctionRepository = new EfAuctionRepository();
        EfUserRepository userRepository = new EfUserRepository();

        public void Receive(string bid)
        {
            int auctionId;
            bool validRequest = int.TryParse(bid, out auctionId);

            if (Context.User.Identity.IsAuthenticated && validRequest)
            {
                var auction = auctionRepository.FindAuctionById(auctionId);

                if (auction != null && auction.WonByUser == null)
                {
                    var user = userRepository.FindUserByEmail(Context.User.Identity.Name);
                    user.LanceCreditBalance--;
                    user.LancesSpent++;
                    userRepository.SaveChanges();

                    auction.LanceCost += (decimal)0.01;
                    auction.LastBidTime = DateTime.Now;
                    auction.ClosingLanceCount++;

                    //Save the transaction of the bid nto the table UserAuctionLance.
                    UserAuctionLance lanceRecord = new UserAuctionLance()
                    {
                        UserId = user.UserId,
                        AuctionId = auction.AuctionId,
                        DateTimeOfLance = DateTime.Now,
                        LanceCost = auction.LanceCost,
                        UserName = user.Nickname
                    };
                    auctionRepository.AddAuctionBid(lanceRecord);
                    auctionRepository.SaveChanges();

                    BidUpdateMessage message = new BidUpdateMessage();
                    message.AuctionId = auction.AuctionId;
                    message.LanceCost = String.Format("{0:0.00}", (decimal)auction.LanceCost);
                    message.LatestBidder = user.Nickname;

                    var result = Json.Encode(message);
                    Clients.updateAuction(result);
                }
            }
            else
            {
                Caller.ReloadPage();
            }
        }

        public class BidUpdateMessage
        {
            public int AuctionId { get; set; }
            public string LanceCost { get; set; }
            public string LatestBidder { get; set; }
        }
    }
}