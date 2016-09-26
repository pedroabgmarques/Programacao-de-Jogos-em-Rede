using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Common
{
    public struct Mensagem
    {
        [JsonProperty("Message")]
        public string Message { get; set; } 
    }
}
