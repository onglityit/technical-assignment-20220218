using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Darren.Base.Model
{
    public class TransactionResultModel
    {
        public string Id { get; set; }
        public string Payment { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }
    }
}
