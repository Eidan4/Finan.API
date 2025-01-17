using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finan.API.Application.DTOs.Students;
using Finan.API.Domain.Estudents;

namespace Finan.API.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDto, StudentsEntity>().ReverseMap();

        }
    }
}
