using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IAuctionRepository
    {
        IQueryable<Auction> FindAllAuctions();
        Auction FindAuctionById(int id);
        UserAuctionLance FindLastBidderForAuction(int auctionId);
        int FindSavingsPercentageForWinner(int auctionId);
        int FindBidCountForWinner(int auctionId);
        void AddAuction(Auction auction);
        void AddAuctionBid(UserAuctionLance bid);
        void SaveChanges();
    }
}
