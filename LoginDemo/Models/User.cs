using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    public class User
    {
        [Key]
        public string CPF { get; set; }

        public string NOME { get; set; }
        public DateTime NASCIMENTO { get; set; }
        public string SENHA { get; set; }
        public string PERFIL { get; set; }
        public string APIACCESSTOKEN { get; set; }
        public string LOCALACCESSTOKEN { get; set; }
        public DateTime VALIDADELOGIN { get; set; } 
    }
}
