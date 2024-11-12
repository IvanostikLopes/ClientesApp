using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Dtos
{
    /// <summary>
    /// DTO para a requisições (entrada de dados) de cliente na aplicação
    /// </summary>
    public class ClienteRequestDto
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
    }

}
