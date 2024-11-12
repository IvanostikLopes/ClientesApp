using ClientesApp.Application.Models;


namespace ClientesApp.Application.Interfaces.Logs
{
    public interface ILogClienteDataStore
    {
        Task AddAsync(LogClienteModel model);
    }
}
