using System;
using System.Linq;
using System.Threading.Tasks;
using LoginDemo.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using LoginDemo.Models;
using LoginDemo.Services;
using LoginDemo.Repositorio;

namespace LoginDemo.Controllers
{
    [UnAuthorized]
    public class ClienteController : BaseController
    {
        ClienteDetalhe clienteDetalhe = new ClienteDetalhe();
        private PersisteContext persisteContext;

        public ClienteController(PersisteContext pc)
        {
            persisteContext = pc;
        }

        [Authorize(Roles.ADMIN)]
        public IActionResult AdminPage()
        {
            ViewBag.UserRole = GetRole();
            ViewBag.Message = "Permission controlled through Authorize Attribute";
            return View("AdminPage");
        }

        public async Task<IActionResult> Index(string currentFilter, string searchString, bool? especial, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var listCliente = persisteContext.Cliente
                .Where(entry => entry.USERCPF.Equals(DadosUsuarioLogado.CPF));

            if (!String.IsNullOrEmpty(searchString))
            {
                listCliente = listCliente.Where(entry => entry.NOME.Contains(searchString));
            }

            if (especial != null)
            {
                listCliente = listCliente.Where(entry => entry.ESPECIAL == especial);
                if (especial == true)
                {
                    ViewData["optTrue"] = "selected";
                }
                else
                {
                    @ViewData["optFalse"] = "selected";
                }
            }

            int pageSize = 10;
            ViewBag.UserRole = GetRole();
            return View(await PaginatedList<Cliente>.CreateAsync(listCliente.OrderBy(entry => entry.NOME), pageNumber ?? 1, pageSize));
        }

        public IActionResult ClienteDetalhe(string id)
        {
            Cliente cliente = persisteContext.Cliente.FirstOrDefault(entry => entry.ID.Equals(id));

            @ViewData["cpf"] = cliente.CPF;
            @ViewData["nome"] = cliente.NOME;
            @ViewData["especial"] = cliente.ESPECIAL;

            clienteDetalhe = ClienteServices.getClienteDetalhe(DadosUsuarioLogado.APIACCESSTOKEN, id);

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