using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Inputs
{
    public class InsertOrderInput : BaseInput
    {
        public PartnerType PartnerId { get; set; }

        [MaxLength(25)]
        [Required]
        public string TypeOfOrder { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [MaxLength(250)]
        [Required]
        public string CompanyName { get; set; }

        [MaxLength(50)]
        [Required]
        public string SubmittedBy { get; set; }

        [MaxLength(int.MaxValue)]
        public string AdditionalOrderInfo { get; set; }

        public ICollection<InsertCampaignProduct> PaidProducts { get; set; }

        public ICollection<InsertWebSiteProduct> WebSites { get; set; }
    }


    public abstract class InsertProductInput : BaseInput
    {

        public ProductType ProductType { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Notes { get; set; }
    }


    public class InsertCampaignProduct : InsertProductInput
    {
        public InsertCampaignProduct()
        {
            ProductType = ProductType.PaidProduct;
        }
        [MaxLength(250)]
        [Required]
        public string CampaignName { get; set; }

        [MaxLength(250)]
        [Required]
        public string CampaignAddressLine1 { get; set; }

        [MaxLength(250)]
        [Required]
        public string CampaignPostCode { get; set; }

        [Range(0, 100)]
        [Required]
        public decimal Radius { get; set; }

        [MaxLength(250)]
        [Required]
        public string LeadPhoneNumber { get; set; }

        [MaxLength(250)]
        [Required]
        public string SMSPhoneNumber { get; set; }

        [MaxLength(250)]
        [Required]
        public string UniqueSellingPoint1 { get; set; }

        [MaxLength(250)]
        [Required]
        public string UniqueSellingPoint2 { get; set; }

        [MaxLength(250)]
        [Required]
        public string UniqueSellingPoint3 { get; set; }

        [MaxLength(250)]
        [Required]
        public string DestinationURL { get; set; }

        [Range(0, 10000)]
        [Required(ErrorMessage = "Offer field required!")]
        public decimal Offer { get; set; }
    }

    public class InsertWebSiteProduct : InsertProductInput
    {
        public InsertWebSiteProduct()
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
