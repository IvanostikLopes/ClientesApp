using ClientesApp.Application.Command;
using ClientesApp.Application.Interfaces.Logs;
using MediatR;


namespace ClientesApp.Application.Handlers
{
    // ClienteRequestHandler será o ouvinte que irá processar
    // de todos os commando que eu executar para cliente
    // IRequestHandler<ClienteCommand> ele escuta toda notificação
    // que lança um Command.
    // Toda vez o que o Mediator disparar um Send, cairá nessa classe
    public class ClienteRequestHandler : IRequestHandler<ClienteCommand>
    {
        private readonly ILogClienteDataStore _logClienteDataStore;

        public ClienteRequestHandler(ILogClienteDataStore logClienteDataStore)
        {
            _logClienteDataStore = logClienteDataStore;
        }

        public async Task Handle(ClienteCommand request, CancellationToken cancellationToken)
        {
            await _logClienteDataStore.AddAsync(request.LogCliente);
        }
    }
}
