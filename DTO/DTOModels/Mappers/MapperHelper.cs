using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
namespace DTOModels.Mappers
{
    public class MapperHelper<DTO, ENT> where DTO : class where ENT : class
    {
        public DTO entityToDto(ENT source)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ENT, DTO>();
            });
            IMapper IMapper = config.CreateMapper();
            return IMapper.Map<ENT, DTO>(source);
        }

        public ENT dtoToEntity(DTO source)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DTO, ENT>();
            });
            IMapper IMapper = config.CreateMapper();
            return IMapper.Map<DTO, ENT>(source);
        }

        public List<DTO> listaEntityToDto(List<ENT> sources)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ENT, DTO>();
            });
            IMapper IMapper = config.CreateMapper();
            List<DTO> dests = new List<DTO>();
            sources.ForEach(x => {
                DTO aux = IMapper.Map<ENT, DTO>(x);
                dests.Add(aux);
            });
            return dests;
        }
        public List<ENT> listaDtoToEntity(List<DTO> sources)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DTO, ENT>();
            });
            IMapper IMapper = config.CreateMapper();
            List<ENT> dests = new List<ENT>();
            sources.ForEach(x => {
                ENT aux = IMapper.Map<DTO, ENT>(x);
                dests.Add(aux);
            });
            return dests;
        }
    }
}
