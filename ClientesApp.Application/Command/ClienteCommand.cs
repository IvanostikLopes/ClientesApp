using ClientesApp.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Command
{

    // IRequest essa interface não me obriga a implementar nada
    public class ClienteCommand : IRequest
    {
        /// <summary>
        /// Registro do log de operação com o cliente (Add, Update, Delete)
        /// O Command me ajuda a interceptar isso
        /// </summary>
        public LogClienteModel? LogCliente { get; set; }

    }
}
