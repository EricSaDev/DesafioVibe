using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    public class User
    {
        public string CPF { get; set; }
        public string NOME { get; set; }
        public DateTime NASCIMENTO { get; set; }
        public string SENHA { get; set; }
        public string PERFIL { get; set; }
        public string APIACCESSTOKEN { get; set; }
    }
}
