using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroShopping.WebUI.Models
{
    public class AuctionModel
    {
        public int AuctionId { get; set; }
        public int AuctionCategoryId { get; set; }
        public bool IsFinished { get; set; }
        public string LatestBidder { get; set; }
        public string Thumbnail { get; set; }

        public string ProductName { get; set; }
        [Display(Name = "Numero de Serie:")]
        [Required(ErrorMessage = "Debe escribir el numero de serie.")]
        public string SerialNumber { get; set; }

        [Display(Name = "Remate esta corriendo ahora?")]
        public bool IsActive { get; set; }

        [Display(Name = "Tiempo Inicio:")]
        [Required(ErrorMessage = "Debe introducir el tiempo de inicio.")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Tiempo Fin:")]
        public DateTime EndTime { get; set; }
        
        public DateTime LastBidTime { get; set; }

        public bool AvailableForBuyNow { get; set; }
        public decimal BuyNowCost { get; set; }

        [Display(Name = "Cost de Primer Lance:")]
        [Required(ErrorMessage = "Debe escribir el cost de Lance.")]
        public decimal LanceCost { get; set; }

        public decimal ClosingLanceCost { get; set; }
        public int WonByUser { get; set; }
        public int LancesSpentByWinner { get; set; }
        
        [Required(ErrorMessage = "Debe escribir el cost regular del articulo.")]
        [Display(Name = "Costo Regular de Articulo")]
        public decimal RegularCost { get; set; }

        public int PercentageSaving { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Producto:")]
        public List<SelectListItem> Products { get; set; }
        
    }
}