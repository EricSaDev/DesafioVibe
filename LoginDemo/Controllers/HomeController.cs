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

namespace LoginDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            User objLoggedInUser = new User();

            if (User.Identity.IsAuthenticated)
            {
                var claimsIndentity = HttpContext.User.Identity as ClaimsIdentity;
                var userClaims = claimsIndentity.Claims;

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    foreach (var claim in userClaims)
                    {
                        var cType = claim.Type;
                        var cValue = claim.Value;
                        switch (cType)
                        {
                            case "CPF":
                                objLoggedInUser.CPF = cValue;
                                break;
                            case "NOME":
                                objLoggedInUser.NOME = cValue;
                                break;
                            case "NASCIMENTO":
                                objLoggedInUser.NASCIMENTO = DateTime.Parse(cValue);
                                break;
                            case "APIACCESSTOKEN":
                                objLoggedInUser.APIACCESSTOKEN = cValue;
                                break;
                            case "PERFIL":
                                objLoggedInUser.PERFIL = cValue;
                                break;
                        }
                    }
                    ViewBag.UserRole = GetRole();
                }
            }
            return View("Index", objLoggedInUser);
        }

        public IActionResult LoginUser(User user)
        {
            if (string.IsNullOrEmpty(user.CPF) || string.IsNullOrEmpty(user.SENHA))
            {
                ViewBag.Message = "Digite o usuário e a senha.";
                return View("Index");
            }

            TokenProvider _tokenProvider = new TokenProvider();
            var userToken = _tokenProvider.LoginUser(user);

            tokenAPI tokenAPI = _tokenProvider.tokenAPI;

            if (userToken != null)
            {
                HttpContext.Session.SetString("JWToken", userToken);
                HttpContext.Session.SetString("APIToken", tokenAPI.chave);
            }
            return Redirect("~/Dashboard/Cliente");
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
            return Redirect("~/Home/Index");
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
