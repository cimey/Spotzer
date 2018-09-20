using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Inputs
{
    public class GetOrderInput : BaseInput
    {
        public int? Id { get; set; }

        public PartnerType PartnerType { get; set; }
    }
}
