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

        public void AddAuction(Auction auction)
        {
            db.AddToAuctions(auction);
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
