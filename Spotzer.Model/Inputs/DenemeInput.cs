using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Model.Inputs
{
    public class DenemeInput : BaseInput
    {
        [MaxLength(25,ErrorMessage ="Length should be maximum 250")]
        public string Name { get; set; }
    }
}
