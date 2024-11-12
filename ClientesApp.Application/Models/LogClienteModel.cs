using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Models
{
    /// <summary>
    /// Modelo de dados para gravação dos logs de cliente
    /// </summary>
    public class LogClienteModel
    {
        public Guid? Id { get; set; }
        public TipoOperacao? TipoOperacao { get; set; }
        public DateTime? DataOperacao { get; set; }
        public Guid? ClienteId { get; set; }
        public string? DadosCliente { get; set; }
    }

    public enum TipoOperacao
    {
        Add = 1,
        Update = 2,
        Delete = 3
    }
}
