using ClientesApp.Application.Models;
using ClientesApp.Infra.Data.MongoDB.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.MongoDB.Contexts
{
    public class MongoDBContext
    {
        private readonly MongoDBSettings _mongoDBSettings;

        // Não precisa passar no construtor, ele fará a conexão com o MongoDB
        // porque sou eu que darei new nele e sendo assim não precisa de 
        // injeção de dependência
        private IMongoDatabase _mongoDatabase;

        public MongoDBContext(MongoDBSettings mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings;


            #region Conexão com o banco de dados

            var settings = MongoClientSettings.FromUrl(new MongoUrl(_mongoDBSettings.Url));
            var client = new MongoClient(settings);

            _mongoDatabase = client.GetDatabase(_mongoDBSettings.Database);

            #endregion

        }


        public IMongoCollection<LogClienteModel> LogClientes
            // Esse lambda suprime um GET (é um return)
            // LogClientes nome da minha collection
            // Posso consultar, gravar, alterar e excluir
            // A diferença entre o MongoDB e o Entity, é que não precisa
            // fazer um mapeamento muito detalhado não
            // é só informar que essa classe LogClienteModel representa
            // minha collection no banco de dados
            => _mongoDatabase.GetCollection<LogClienteModel>("LogClientes");

    }
}
