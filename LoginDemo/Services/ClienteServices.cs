using LoginDemo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Services
{
    public class ClienteServices
    {
        public static List<Cliente> getClientes(string APIToken)
        {
            RetornoDesafioAPIs retornoDesafioAPIs = new RetornoDesafioAPIs();

            List<Cliente> listCliente = retornoDesafioAPIs.getClientes(APIToken);
            return listCliente;
        }

        public static ClienteDetalhe getClienteDetalhe(string APIToken, string ID)
        {
            RetornoDesafioAPIs retornoDesafioAPIs = new RetornoDesafioAPIs();

            ClienteDetalhe clienteDetalhe = retornoDesafioAPIs.getClienteDetalhe(APIToken, ID);
            return clienteDetalhe;
        }
    }
}
