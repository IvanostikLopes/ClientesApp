using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.MongoDB.Settings
{
    public class MongoDBSettings
    {
        /// <summary>
        /// Classe para capturar as configurações do /appsettings
        /// Colocar os nomes dos atributos exatamente iguais como estão 
        /// no appsettings.Development.json e appsettings.Product.json
        /// </summary>

        public string? Url { get; set; }
        public string? Database { get; set; }

    }
}
