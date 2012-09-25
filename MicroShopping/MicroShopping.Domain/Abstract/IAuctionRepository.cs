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
        void AddAuction(Auction auction);
        void AddAuctionBid(UserAuctionLance bid);
        void SaveChanges();
    }
}
