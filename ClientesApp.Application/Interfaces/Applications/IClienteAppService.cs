using ClientesApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Interfaces.Applications
{
    public interface IClienteAppService : IDisposable
    {
        Task<ClienteResponseDto> AddAsync(ClienteRequestDto request);
        Task<ClienteResponseDto> UpdateAsync(Guid id, ClienteRequestDto request);
        Task<ClienteResponseDto> DeleteAsync(Guid id);
        Task<List<ClienteResponseDto>> GetManyAsync(string nome);
        Task<ClienteResponseDto?> GetByIdAsync(Guid id);
    }

}
