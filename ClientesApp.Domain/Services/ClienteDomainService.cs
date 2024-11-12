using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Exceptions;
using ClientesApp.Domain.Interfaces.Repositories;
using ClientesApp.Domain.Interfaces.Services;
using ClientesApp.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValidationException = FluentValidation.ValidationException;

namespace ClientesApp.Domain.Services
{
    public class ClienteDomainService : IClienteDomainService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validator;

        public ClienteDomainService(IClienteRepository clienteRepository, IValidator<Cliente> validator)
        {
            _clienteRepository = clienteRepository;
            _validator = validator;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            var validationResult = await _validator.ValidateAsync(cliente);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            await _clienteRepository.AddAsync(cliente);

            return cliente;

        }
        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {

            if (!await _clienteRepository.VerifyExistsAsync(c => c.Id == cliente.Id))
                throw new ClienteNotFoundException(cliente.Id);

            // se for verdade, já faço um cast para a variável validator
            // a variável validator já uma referência da _validator também
            if (_validator is ClienteValidator validator)
                validator.SetCurrentClienteId(cliente.Id);

            var validationResult = await _validator.ValidateAsync(cliente);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _clienteRepository.UpdateAsync(cliente);
            return cliente;
        }

        public async Task<Cliente> DeleteAsync(Guid id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente == null)
                throw new ClienteNotFoundException(id);

            await _clienteRepository.DeleteAsync(cliente);
            return cliente;

        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task<List<Cliente>> GetManyAsync(string nome)
        {
            return await _clienteRepository.GetManyAsync(c => c.Nome.Contains(nome));
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }
        
    }
}
