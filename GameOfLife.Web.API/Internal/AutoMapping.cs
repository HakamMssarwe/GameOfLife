using AutoMapper;
using GameOfLife.Infrastructure.Entities.DB;
using GameOfLife.Infrastructure.Entities.DTOs;

namespace GameOfLife.Web.API.Internal
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<BoardDTO, Board>();
        }
    }
}
