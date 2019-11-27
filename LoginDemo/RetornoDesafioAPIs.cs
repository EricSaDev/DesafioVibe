using LoginDemo.CustomAttributes;
using LoginDemo.Models;
using LoginDemo.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo
{
    public class RetornoDesafioAPIs
    {
        private static string apiRoot = "https://vibeselecao.azurewebsites.net";
        private static string apiAutenticacao = "/api/Autenticacao";
        private static string apiCliente = "/api/Cliente";
        private static string apiUsuario = "/api/Usuario";

        HttpClient client = new HttpClient();

        public object JsonNamingPolicy { get; private set; }

        public tokenAPI getLogin(User userLogin)
        {
            string senhaMD5 = MD5.MD5Hash(userLogin.SENHA);
            string cpfFormat = userLogin.CPF.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage respToken = client.PostAsync(apiRoot + apiAutenticacao, 
                new StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        cpf = cpfFormat,
                        senha = senhaMD5
                    }), 
                    Encoding.UTF8, "application/json")).Result;

            string conteudo = respToken.Content.ReadAsStringAsync().Result;

            if (respToken.StatusCode == HttpStatusCode.OK)
            {
                tokenAPI token = JsonConvert.DeserializeObject<tokenAPI>(conteudo);

                if (!string.IsNullOrEmpty(token.chave))
                {
                    return token;
                }
            }
            return null;
        }

        public User getUsuario(User user, tokenAPI tokenAPI)
        {
            string senhaMD5 = MD5.MD5Hash(user.SENHA);
            string cpfFormat = user.CPF.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAPI.chave);

            HttpResponseMessage response = client.GetAsync(apiRoot + apiUsuario + "/" + cpfFormat).Result;

            string conteudo = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                User respUser = JsonConvert.DeserializeObject<User>(conteudo);
                respUser.NASCIMENTO = respUser.NASCIMENTO;
                respUser.SENHA = senhaMD5;
                respUser.APIACCESSTOKEN = tokenAPI.chave;
                respUser.PERFIL = Roles.ADMIN.ToString();

                return respUser;
            }
            return null;
        }

        public string addUsuario(User user)
        {
            string senhaMD5 = MD5.MD5Hash(user.SENHA);
            string cpfFormat = user.CPF.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage respToken = client.PostAsync(apiRoot + apiUsuario, new 
                StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        cpf = cpfFormat,
                        nome = user.NOME,
                        nascimento = user.NASCIMENTO,
                        senha = senhaMD5
                    }), 
                    Encoding.UTF8, "application/json")).Result;

            string conteudo = respToken.Content.ReadAsStringAsync().Result;

            if (respToken.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }

            Msg msg = JsonConvert.DeserializeObject<Msg>(conteudo);
            return msg.mensagem;
        }

        public List<Cliente> getClientes(String tokenAPI)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAPI);

            HttpResponseMessage response = client.GetAsync(apiRoot + apiCliente).Result;

            string conteudo = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<Cliente> listCliente = JsonConvert.DeserializeObject<List<Cliente>>(conteudo);
                return listCliente;
            }
            return null;
        }

        public ClienteDetalhe getClienteDetalhe(String tokenAPI, string ID)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAPI);

            HttpResponseMessage response = client.GetAsync(apiRoot + apiCliente + "/" + ID).Result;

            string conteudo = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ClienteDetalhe clienteDetalhe = JsonConvert.DeserializeObject<ClienteDetalhe>(conteudo);

                return clienteDetalhe;
            }
            return null;
        }

    }
}
