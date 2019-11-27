using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    public class ClienteDetalhe
    {
        public string urlImagem { get; set; }
        public string empresa { get; set; }
        public ClienteEndereco endereco { get; set; }
    }
}
