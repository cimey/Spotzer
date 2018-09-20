using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Enums
{
    public class OrderConstants
    {
        public const string PartnerAMissingWebsite = "Partner A's order should include at least one website product!";
        public const string PartnerAIncludePaidProduct = "Partner A can not sell paid products!";
        public const string PartnerDMissingPaidProduct = "Partner D's order should include at least one paid product!";
        public const string PartnerDIncludeWebsite = "Partner D can not sell websites!";
        public const string PartnerBandDCantHaveAdditionalInfo = "Partner B and D can not have additional info!";
        public const string PartnerAandCHaveAdditionalInfo = "Partner A and C have to include additional info!";
    }
}
