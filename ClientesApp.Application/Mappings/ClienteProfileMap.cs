using AutoMapper;
using ClientesApp.Application.Dtos;
using ClientesApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Mappings
{
    public class ClienteProfileMap : Profile
    {
        public ClienteProfileMap()
        {
            CreateMap<ClienteRequestDto, Cliente>();
            CreateMap<Cliente, ClienteResponseDto>();
        }

    }
}
