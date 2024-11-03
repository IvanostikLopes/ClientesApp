using Bogus;
using ClientesApp.Domain.Entities;
using ClientesApp.Infra.Data.SqlServer.Contexts;
using ClientesApp.Infra.Data.SqlServer.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ClientesApp.Infra.Data.SqlServer.Tests
{
    public class ClienteRepositoryTest
    {
        private readonly Faker<Cliente> _fakerCliente;
        private readonly DataContext _dataContext;
        private readonly ClienteRepository _clienteRepository;

        public ClienteRepositoryTest()
        {
            //configurando o Bogus para construir um cliente
            _fakerCliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"));


            //configurar o DataContext para utilizar banco de memória no EF
            // o nome do banco pode ser qualquer um (posso criar o nome sem problema)
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ClientesAppTestsDB")
                .Options;

            //criando o mock da classe DataContext
            _dataContext = new DataContext(options);

            //instanciando a classe de repositório
            _clienteRepository = new ClienteRepository(_dataContext);
        }

        // [Fact(Skip = "Não implementado.", DisplayName = "Adicionar cliente com sucesso no repositório.")]

        // Teste de verdade
        [Fact(DisplayName = "Adicionar cliente com sucesso no repositório.")]
        public async Task AddAsync_ShouldAddCliente()
        {
            //gerando os dados do cliente
            var cliente = _fakerCliente.Generate();

            //gravando o cliente no banco de dados
            await _clienteRepository.AddAsync(cliente);

            //verificar se o cliente foi cadastrado (asserções de teste)

            //buscando o cliente no banco de dados através do ID
            var clienteCadastrado = await _clienteRepository.GetByIdAsync(cliente.Id);

            //verificar se o cliente foi cadastrado (asserções de teste)
            if (clienteCadastrado == null)
                Assert.True(false, "O cliente não foi cadastrado corretamente.");

            clienteCadastrado?.Nome.Should().BeSameAs(cliente.Nome);
            clienteCadastrado?.Email.Should().BeSameAs(cliente.Email);
            clienteCadastrado?.Cpf.Should().BeSameAs(cliente.Cpf);

        }

        [Fact(DisplayName = "Atualizar cliente com sucesso no repositório.")]
        public async Task UpdateAsync_ShouldUpdateCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);

            cliente.Nome = "Novo Nome";
            cliente.Email = "novoemail@example.com";

            await _clienteRepository.UpdateAsync(cliente);

            var clienteAtualizado = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteAtualizado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteAtualizado?.Nome.Should().Be("Novo Nome");
            clienteAtualizado?.Email.Should().Be("novoemail@example.com");
        }

        [Fact(DisplayName = "Excluir cliente com sucesso no repositório.")]
        public async Task DeleteAsync_ShouldRemoveCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.DeleteAsync(cliente);

            var clienteRemovido = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteRemovido != null)
                Assert.True(false, "Cliente não excluído.");
        }

        [Fact(DisplayName = "Obter cliente pelo ID com sucesso no repositório.")]
        public async Task GetByIdAsync_ShouldReturnCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);

            var clienteEncontrado = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteEncontrado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteEncontrado?.Id.Should().Be(cliente.Id);
        }

        [Fact(DisplayName = "Obter vários clientes com base em uma condição.")]
        public async Task GetManyAsync_ShouldReturnClientes()
        {
            var clientes = _fakerCliente.Generate(5);
            foreach (var cliente in clientes)
            {
                await _clienteRepository.AddAsync(cliente);
            }

            var clientesEncontrados = await _clienteRepository.GetManyAsync(c => c.Nome != null);

            if (clientesEncontrados == null)
                Assert.True(false, "Cliente não encontrado.");

            clientesEncontrados.Count.Should().BeGreaterOrEqualTo(5);
        }

        [Fact(DisplayName = "Obter cliente único com base em uma condição.")]
        public async Task GetOneAsync_ShouldReturnSingleCliente()
        {
            var clientes = _fakerCliente.Generate(3);
            foreach (var cliente in clientes)
            {
                await _clienteRepository.AddAsync(cliente);
            }

            var clienteEmail = clientes[0].Email;
            var clienteEncontrado = await _clienteRepository.GetOneAsync(c => c.Email == clienteEmail);

            if (clienteEncontrado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteEncontrado?.Email.Should().Be(clienteEmail);
        }
    }
}
