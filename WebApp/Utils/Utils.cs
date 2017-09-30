using Entidades.Administracion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Convierte un archivo de imagen a un arreglo de bytes.
        /// </summary>
        /// <param name="image">Archivo de imagen</param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;

            var reader = new BinaryReader(image.InputStream);

            imageBytes = reader.ReadBytes((int)image.ContentLength);

            return imageBytes;
        }

        public static byte[] ConvertBase64ToBytes(string code)
        {
            var imagen = code.Replace("data:image/png;base64,", string.Empty);
            return Convert.FromBase64String(imagen);
        }

        public static bool esCedulaValida(string cedula)
        {
            const int NUMERO_DE_PROVINCIAS = 24;
            //verifica que tenga 10 dígitos y que contenga solo valores numéricos
            if (!(cedula.Length == 10))
            {
                return false;
            }

            //verifica que los dos primeros dígitos correspondan a un valor entre 1 y NUMERO_DE_PROVINCIAS
            var prov = int.Parse(cedula.Substring(0, 2));

            if (!((prov > 0) && (prov <= NUMERO_DE_PROVINCIAS)))
            {
                return false;
            }

            //verifica que el último dígito de la cédula sea válido
            var d = new int[10];

            //Asignamos el string a un array
            for (var i = 0; i < d.Length; i++)
            {
                d[i] = int.Parse(cedula[i] + "");
            }

            var imp = 0;
            var par = 0;

            //sumamos los duplos de posición impar
            for (var i = 0; i < d.Length; i += 2)
            {
                d[i] = ((d[i] * 2) > 9) ? ((d[i] * 2) - 9) : (d[i] * 2);
                imp += d[i];
            }

            //sumamos los digitos de posición par
            for (var i = 1; i < (d.Length - 1); i += 2)
            {
                par += d[i];
            }

            //Sumamos los dos resultados
            var suma = imp + par;

            //Restamos de la decena superior
            var d10 = int.Parse((suma + 10).ToString().Substring(0, 1) + "0") - suma;

            //Si es diez el décimo dígito es cero        
            d10 = (d10 == 10) ? 0 : d10;

            //si el décimo dígito calculado es igual al digitado la cédula es correcta
            return d10 == d[9];
        }

        public static async Task<string> getFaceID(byte[] foto)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            string clave = ConfigurationManager.AppSettings["ClaveApiFacial"].ToString();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", clave);

            // Request parameters
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            var uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(foto))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody.Split(',')[0].Split(':')[1].Trim('"', '{', '}');
            }
        }

        public static async Task<bool> CompareFaces(string faceID1, string faceID2)
        {
            var client = new HttpClient();
            string clave = ConfigurationManager.AppSettings["ClaveApiFacial"].ToString();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", clave);

            var uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/verify";

            HttpResponseMessage response;

            var y = ("{\"faceId1\":\"" + faceID1 + "\",\"faceId2\":\"" + faceID2 + "\"}");

            using (var content = new StringContent(y))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                var esigual = responseBody.Split(',')[0].Split(':')[1].Trim('"', '{', '}');
                if(esigual == "true")
                {
                    var ponderacion = double.Parse(responseBody.Split(',')[1].Split(':')[1].Trim('"', '{', '}'))/100000;
                    if (ponderacion > 0.6)
                        return true;
                }
                return false;
            }
        }

        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static string GetClaim(string claim)
        {
            var claimsIdentity = ClaimsPrincipal.Current.Identities.FirstOrDefault();
            var aux = claimsIdentity.Claims.FirstOrDefault(c => c.Type == claim);
            return aux.Value;
        }
    }
}