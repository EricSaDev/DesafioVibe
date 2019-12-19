using LoginDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginDemo.Controllers
{
    public class BaseController : Controller
    {
        public User DadosUsuarioLogado
        {
            get
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
                    }
                }
                return objLoggedInUser;
            }
        }
    }
}
