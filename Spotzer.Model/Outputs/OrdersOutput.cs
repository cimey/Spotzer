using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Outputs
{
    public class OrderOutput
    {
        public int Id { get; set; }

        public PartnerType PartnerId { get; set; }

        public string TypeOfOrder { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string SubmittedBy { get; set; }

        public string AdditionalOrderInfo { get; set; }

        public ICollection<ProductOutput> Lines { get; set; }
    }


    public abstract class ProductOutput 
    {
        public int Id { get; set; }

        public ProductType ProductType { get; set; }

        public string Category { get; set; }

        public string Notes { get; set; }
    }


    public class CampaignOutput : ProductOutput
    {
        public string CampaignName { get; set; }

        public string CampaignAddressLine1 { get; set; }

        public string CampaignPostCode { get; set; }

        public decimal Radius { get; set; }

        public string LeadPhoneNumber { get; set; }

        public string SMSPhoneNumber { get; set; }

        public string UniqueSellingPoint1 { get; set; }

        public string UniqueSellingPoint2 { get; set; }

        public string UniqueSellingPoint3 { get; set; }

        public string DestinationURL { get; set; }

        public decimal Offer { get; set; }
    }

    public class WebSiteOutput : ProductOutput
    {
        public int TemplateId { get; set; }

        public string WebsiteBusinessName { get; set; }

        public string WebsiteAddressLine1 { get; set; }

        public string WebsiteAddressLine2 { get; set; }

        public string WebsiteCity { get; set; }

        public string WebsiteState { get; set; }

        public string WebsitePostCode { get; set; }

        public string WebsitePhone { get; set; }

        public string WebsiteEmail { get; set; }

        public string WebsiteMobile { get; set; }
    }
}
