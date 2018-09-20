using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Entities
{

    public enum ProductType
    {
        WebSite = 1,
        PaidProduct
    }

    //public class Deneme
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    [Table("Product")]
    public abstract class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ProductType ProductType { get; set; }

        public string Category { get; set; }

        public string Notes { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }


    public class PaidSearch : Product
    {
        public PaidSearch()
        {
            ProductType = ProductType.PaidProduct;
        }

        [MaxLength(250)]
        public string CampaignName { get; set; }

        [MaxLength(250)]
        public string CampaignAddressLine1 { get; set; }

        [MaxLength(250)]
        public string CampaignPostCode { get; set; }

        [Range(0, 100)]
        public decimal Radius { get; set; }

        [MaxLength(250)]
        public string LeadPhoneNumber { get; set; }

        [MaxLength(250)]
        public string SMSPhoneNumber { get; set; }

        [MaxLength(250)]
        public string UniqueSellingPoint1 { get; set; }

        [MaxLength(250)]
        public string UniqueSellingPoint2 { get; set; }

        [MaxLength(250)]
        public string UniqueSellingPoint3 { get; set; }

        [MaxLength(250)]
        public string DestinationURL { get; set; }

        [Range(0, 10000)]
        public decimal Offer { get; set; }
    }

    public class WebSite : Product
    {

        public WebSite()
        {
            ProductType = ProductType.WebSite;
        }

        public int TemplateId { get; set; }

        [MaxLength(250)]
        public string WebsiteBusinessName { get; set; }

        [MaxLength(250)]
        public string WebsiteAddressLine1 { get; set; }

        [MaxLength(250)]
        public string WebsiteAddressLine2 { get; set; }

        [MaxLength(250)]
        public string WebsiteCity { get; set; }

        [MaxLength(250)]
        public string WebsiteState { get; set; }

        [MaxLength(250)]
        public string WebsitePostCode { get; set; }

        [MaxLength(250)]
        public string WebsitePhone { get; set; }

        [MaxLength(250)]
        public string WebsiteEmail { get; set; }

        [MaxLength(250)]
        public string WebsiteMobile { get; set; }

    }
}
