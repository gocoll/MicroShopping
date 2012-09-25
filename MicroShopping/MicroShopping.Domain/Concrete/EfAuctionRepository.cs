using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfAuctionRepository : IAuctionRepository, IDisposable
    {
        private readonly MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<Auction> FindAllAuctions()
        {
            return db.Auctions;
        }

        public Auction FindAuctionById(int id)
        {
            return db.Auctions.SingleOrDefault(x => x.AuctionId == id);
        }

        public UserAuctionLance FindLastBidderForAuction(int auctionId)
        {
            return db.UserAuctionLances.Where(x => x.AuctionId == auctionId).OrderByDescending(x => x.DateTimeOfLance).First();
        }

        public int FindSavingsPercentageForWinner(int auctionId)
        {
            var auction = db.Auctions.SingleOrDefault(x => x.AuctionId == auctionId);
            var bids = auction.UserAuctionLances.Where(x => x.UserId == auction.WonByUser).Count();


            var valor = bids + (decimal)auction.LanceCost;
            var percentage = 100 - ((valor / auction.RegularCost) * 100);

            if (percentage > 0)
                return Convert.ToInt32(percentage);
            else
                return 0;
        }

        public int FindBidCountForWinner(int auctionId)
        {
            var auction = db.Auctions.SingleOrDefault(x => x.AuctionId == auctionId);
            return auction.UserAuctionLances.Where(x => x.UserId == auction.WonByUser).Count();
        }

        public void AddAuction(Auction auction)
        {
            db.AddToAuctions(auction);
        }

        public void AddAuctionBid(UserAuctionLance bid)
        {
            db.AddToUserAuctionLances(bid);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
