using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    public class Cliente
    {
        public string ID { get; set; }
        public string CPF { get; set; }
        public string NOME { get; set; }
        public bool ESPECIAL { get; set; }
    }
}
