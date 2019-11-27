using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginDemo.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginDemo.Models;
using LoginDemo.Services;

namespace LoginDemo.Controllers
{
    [UnAuthorized]
    public class DashboardController : Controller
    {
        [Authorize(Roles.ADMIN)]
        public IActionResult AdminPage()
        {
            ViewBag.UserRole = GetRole();
            ViewBag.Message = "Permission controlled through Authorize Attribute";
            return View("AdminPage");
        }

        public IActionResult Cliente()        {

            RetornoDesafioAPIs retornoDesafioAPIs = new RetornoDesafioAPIs();
            string APIToken = HttpContext.Session.GetString("APIToken");

            List<Cliente> listCliente = ClienteServices.getClientes(APIToken);

            listCliente = listCliente.OrderBy(s => s.NOME).ToList();

            ViewBag.UserRole = GetRole();
            return View(listCliente);
        }

        public IActionResult ClienteDetalhe(string id)
        {
            RetornoDesafioAPIs retornoDesafioAPIs = new RetornoDesafioAPIs();
            string APIToken = HttpContext.Session.GetString("APIToken");

            ClienteDetalhe clienteDetalhe = ClienteServices.getClienteDetalhe(APIToken, id);

            ViewBag.UserRole = GetRole();
            return View(clienteDetalhe);
        }

        public IActionResult NoPermission()
        {
            ViewBag.UserRole = GetRole();
            return View("NoPermission");
        }

        private string GetRole()
        {
            if (this.HavePermission(Roles.ADMIN))
                return " - ADMIN";
            return null;
        }
    }
}