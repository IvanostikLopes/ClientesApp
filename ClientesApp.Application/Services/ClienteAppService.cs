using AutoMapper;
using ClientesApp.Application.Command;
using ClientesApp.Application.Dtos;
using ClientesApp.Application.Interfaces.Applications;
using ClientesApp.Application.Models;
using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Services;
using MediatR;
using Newtonsoft.Json;


namespace ClientesApp.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteDomainService _clienteDomainService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ClienteAppService(IClienteDomainService clienteDomainService, IMapper mapper, IMediator mediator)
        {
            _clienteDomainService = clienteDomainService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ClienteResponseDto> AddAsync(ClienteRequestDto request)
        {
            // Copia os dados de request para Cliente
            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = Guid.NewGuid();

            // O domínio grava e devolve a resposta
            var result = await _clienteDomainService.AddAsync(cliente);


            #region Executar o COMMNAND (CQRS)

            // Aqui eu disparo um command e alguém vai ouvir esse command
            // e jogar lá no MongoDB
            await _mediator.Send(new ClienteCommand
            {
                LogCliente = new LogClienteModel
                {
                    Id = Guid.NewGuid(),
                    DataOperacao = DateTime.Now,
                    TipoOperacao = TipoOperacao.Add,
                    ClienteId = cliente.Id,
                    DadosCliente = JsonConvert.SerializeObject(cliente)
                }
            });

            #endregion



            // Devolve os dados que foram inseridos
            return _mapper.Map<ClienteResponseDto>(result);
        }

        public async Task<ClienteResponseDto> UpdateAsync(Guid id, ClienteRequestDto request)
        {
            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = id;

            var result = await _clienteDomainService.UpdateAsync(cliente);

            #region Executar o COMMNAND (CQRS)

            await _mediator.Send(new ClienteCommand
            {
                LogCliente = new LogClienteModel
                {
                    Id = Guid.NewGuid(),
                    DataOperacao = DateTime.Now,
                    TipoOperacao = TipoOperacao.Update,
                    ClienteId = cliente.Id,
                    DadosCliente = JsonConvert.SerializeObject(cliente)
                }
            });
            #endregion

            return _mapper.Map<ClienteResponseDto>(result);
        }

        public async Task<ClienteResponseDto> DeleteAsync(Guid id)
        {
            var result = await _clienteDomainService.DeleteAsync(id);

            #region Executar o COMMNAND (CQRS)

            await _mediator.Send(new ClienteCommand
            {
                LogCliente = new LogClienteModel
                {
                    Id = Guid.NewGuid(),
                    DataOperacao = DateTime.Now,
                    TipoOperacao = TipoOperacao.Delete,
                    ClienteId = result.Id,
                    DadosCliente = JsonConvert.SerializeObject(result)
                }
            });

            #endregion

            return _mapper.Map<ClienteResponseDto>(result);
        }

        public async Task<List<ClienteResponseDto>> GetManyAsync(string nome)
        {
            var result = await _clienteDomainService.GetManyAsync(nome);

            // Copia o result que uma lista de clientes para
            // para uma lista do objeto ClienteResponseDto
            return _mapper.Map<List<ClienteResponseDto>>(result);
        }

        public async Task<ClienteResponseDto?> GetByIdAsync(Guid id)
        {
            var result = await _clienteDomainService.GetByIdAsync(id);
            return _mapper.Map<ClienteResponseDto>(result);

        }

        public void Dispose()
        {
            _clienteDomainService.Dispose();
        }

    }
}
