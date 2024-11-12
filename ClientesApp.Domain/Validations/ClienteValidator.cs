using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Validations
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        private Guid? _currentClienteId;

        public ClienteValidator(IClienteRepository clienteRepository)
        {
           _clienteRepository = clienteRepository;
            ConfigureRules();
        }

        //método para receber o ID do cliente
        public void SetCurrentClienteId(Guid clienteId)
        {
            _currentClienteId = clienteId;
        }

        private void ConfigureRules()
        {
            RuleFor(c => c.Id).NotEmpty()
                .WithMessage("O Id é obrigatório")
                .Must(id => id != Guid.Empty)
                .WithMessage("O Id não pode ser igual ao valor padrão.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(8, 150)
                .WithMessage("O nome deve ter de 8 a 150 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress()
                .WithMessage("O email deve ter um endereço de email válido.")
                .MustAsync(BeUniqueEmail).WithMessage("O email já está em uso.");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve ter 11 dígitos.")
                .MustAsync(BeUniqueCpf).WithMessage("O cpf já está em uso.");
        }

        /*
        Ajustes:
        Modificar a classe ClienteValidator da camada de domínio para verificar se um email ou cpf 
        já está cadastrado e se a ação for uma edição verificar se os mesmos já estão cadastrados 
        para um cliente diferente do que está sendo atualizado.
        */

        /// <summary>
        /// CancellationToken - permite cancelar em qualquer momento essa thred
        /// </summary>
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _clienteRepository.VerifyExistsAsync
                (c => c.Email.Equals(email) && c.Id != _currentClienteId);
        }

        private async Task<bool> BeUniqueCpf(string cpf, CancellationToken cancellationToken)
        {
            return !await _clienteRepository.VerifyExistsAsync
                (c => c.Cpf.Equals(cpf) && c.Id != _currentClienteId);
        }


    }
}
