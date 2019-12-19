using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    public class Cliente
    {
        [Key]
        public string ID { get; set; }

        public string CPF { get; set; }
        public string NOME { get; set; }
        public bool ESPECIAL { get; set; }
        
        public string USERCPF { get; set; }

        [ForeignKey("USERCPF")]
        public User USER { get; set; }
    }
}
