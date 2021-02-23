using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace trifenix.versions.model
{

    /// <summary>
    /// Clase estática de hash para validar el modelo.
    /// candidata a ser un método global.
    /// </summary>
    public static class Hash
    {

        /// <summary>
        /// Convierte cualquier objeto a json y luego a hash.
        /// </summary>
        /// <param name="element">element a hashear</param>
        /// <returns>hash del elemento</returns>
        public static string Serialize(object element) {
            var jsonString =  JsonConvert.SerializeObject(element, new JavaScriptDateTimeConverter());
            return ComputeSha256Hash(jsonString);
        }

        /// <summary>
        /// Crea un hash a partir de un string
        /// </summary>
        /// <param name="rawData">string que debe ser hasheado</param>
        /// <returns>hash</returns>
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
