using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginDemo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using LoginDemo.CustomAttributes;
using LoginDemo.Repositorio;
using Microsoft.EntityFrameworkCore;
using LoginDemo.Utils;
using LoginDemo.Services;

namespace LoginDemo.Controllers
{
    public class HomeController : BaseController
    {
        private PersisteContext persisteContext;

        public object EntityFunctions { get; private set; }

        public HomeController(PersisteContext pc)
        {
            persisteContext = pc;
        }

        public IActionResult Index()
        {            
            ViewBag.UserRole = GetRole();
            return View("Index", DadosUsuarioLogado);
        }

        public IActionResult LoginUser(User user)
        {
            if (string.IsNullOrEmpty(user.CPF) || string.IsNullOrEmpty(user.SENHA))
            {
                ViewBag.Message = "Digite o usuário e a senha.";
                return View("Index");
            }
            //======================================================================
            tokenAPI tokenAPI = new tokenAPI();
            RetornoDesafioAPIs retornoDesafioAPIs = new RetornoDesafioAPIs();

            tokenAPI = retornoDesafioAPIs.getLogin(user);

            if (tokenAPI == null)
            {
                var userLogin = persisteContext.User.AsNoTracking().FirstOrDefault(
                        entry => entry.CPF.Equals(user.CPF) &&
                        entry.SENHA.Equals(MD5.MD5Hash(user.SENHA)) &&
                        entry.VALIDADELOGIN <= DateTime.Now
                );
                if (userLogin == null)
                {
                    ViewBag.Message = "Acesso não permitido.";
                    return View("Index");
                }
                else
                {
                    user = userLogin;
                }
            }
            
            User usuario = retornoDesafioAPIs.getUsuario(user, tokenAPI);

            if (usuario == null)
                return null;
            //======================================================================
            TokenProvider _tokenProvider = new TokenProvider();
            User userLogged = _tokenProvider.LoginUser(usuario);

            if (userLogged.LOCALACCESSTOKEN != null)
            {
                HttpContext.Session.SetString("JWToken", userLogged.LOCALACCESSTOKEN);
                HttpContext.Session.SetString("APIToken", tokenAPI.chave);
            }
            //======================================================================
            userLogged.VALIDADELOGIN = new DateTimeOffset(DateTime.Now.AddDays(7)).DateTime;

            var userModel = persisteContext.User.AsNoTracking().FirstOrDefault(entry => entry.CPF.Equals(userLogged.CPF));
            if (userModel != null){
                persisteContext.Entry(userLogged).State = EntityState.Modified;
            }
            else{
                persisteContext.User.Add(userLogged);

                var clienteModel = persisteContext.Cliente.FirstOrDefault(entry => entry.USERCPF.Equals(userLogged.CPF));
                if (clienteModel == null)
                {
                    List<Cliente> resListCliente = ClienteServices.getClientes(userLogged.APIACCESSTOKEN);
                    foreach (Cliente cliente in resListCliente)
                    {
                        cliente.USERCPF = userLogged.CPF;
                        persisteContext.Cliente.Add(cliente);
                    }
                }
            }

            persisteContext.SaveChanges();
            //======================================================================
            return Redirect("~/Cliente");
            //======================================================================
        }

        public IActionResult Cadastro(User user)
        {
            return View("Cadastro");
        }

        public IActionResult Cadastrar(User user)
        {
            if (string.IsNullOrEmpty(user.CPF) || string.IsNullOrEmpty(user.NOME) || string.IsNullOrEmpty(user.NASCIMENTO.ToString()) || string.IsNullOrEmpty(user.SENHA))
            {
                ViewBag.Message = "Digite todos os dados para cadastro.";
                return View("Cadastro");
            }

            RetornoDesafioAPIs retornoDesafioAPIs = new RetornoDesafioAPIs();
            string retorno = retornoDesafioAPIs.addUsuario(user);
            if (retorno != "OK")
            {
                ViewBag.Message = retorno;
                return View("Cadastro");
            }
            ViewBag.Message = "Usuário cadastrado com sucesso. Retorne ao login para acessar o sistema.";
            return View("Cadastro");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home");
        }

        public JsonResult EndSession()
        {
            HttpContext.Session.Clear();
            return Json(new {result = "success"});
        }
        private string GetRole()
        {
            if (this.HavePermission(Roles.ADMIN))
                return " - ADMIN";
            return null;
        }
    }
}
