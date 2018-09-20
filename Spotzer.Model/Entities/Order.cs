using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Entities
{
    public enum PartnerType
    {
        PartnerA = 1,
        PartnerB,
        PartnerC,
        PartnerD
    }
    [Table("Order")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PartnerType PartnerId { get; set; }

        [MaxLength(25)]
        public string TypeOfOrder { get; set; }

        public int CompanyId { get; set; }

        [MaxLength(250)]
        public string CompanyName { get; set; }

        [MaxLength(50)]
        public string SubmittedBy { get; set; }

        [MaxLength(int.MaxValue)]
        public string AdditionalOrderInfo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
