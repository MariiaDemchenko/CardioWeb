using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using CardiologicalResearch.Models;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace CardiologicalResearch.Controllers
{
    public class AuthController : ApiController
    {

        private readonly IRepository  _repository;

        public AuthController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/v1/auth
        public string Get()
        {
            AuthenticationHeaderValue authorization = ActionContext.Request.Headers.Authorization;
            if (authorization == null)
            {
                // No authentication was attempted (for this authentication method).
                // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
                return string.Empty;
            }

            if (authorization.Scheme != "Basic")
            {
                // No authentication was attempted (for this authentication method).
                // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
                return string.Empty;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                return string.Empty;
            }
            var bytes = Convert.FromBase64String(authorization.Parameter);
            var str = Encoding.UTF8.GetString(bytes);
            var parameters = str.Split(':');
            if (parameters.Length != 2 || string.IsNullOrEmpty(parameters[0]) || string.IsNullOrEmpty(parameters[1]))
            {
                return JsonConvert.SerializeObject(new { error = new { code = -32700, message = "Parse error" } });
            }

            return $"{Translit(parameters[0])}_HashKey";
        }

        public async Task<string> Post()
        {
            var content = await Request.Content.ReadAsStringAsync();
            var methodRequest = JsonConvert.DeserializeObject<MethodRequest<LoginParams>>(content);

            if (methodRequest?.Params == null)
            {
                return JsonConvert.SerializeObject(new { error = new { code = -32700, message = "Parse error" } });
            }

            var bytes = Encoding.UTF8.GetBytes($"{methodRequest.Params.UserName}:{methodRequest.Params.Password}");

             ActionContext.Request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));


            var user = _repository.GetUserByName(methodRequest.Params.UserName);


            if (user == null)
            {
                return JsonConvert.SerializeObject(new { error = new { code = -32700, message = "Wrong user or password" } });
            }

            return JsonConvert.SerializeObject(new { result = new { id = user.UserId, hashKey = Get() } });
        }

        private static string Translit(string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", string.Empty, "Y", string.Empty, "E", "Yu", "Ya" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", string.Empty, "y", string.Empty, "e", "yu", "ya" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }
    }
}
