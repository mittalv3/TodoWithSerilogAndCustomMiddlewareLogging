using AutoMapper;

namespace Todo.API.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile() 
        {
            CreateMap<Entities.ToDo,Models.ToDoDto>();
            CreateMap<Models.ToDoCreationDto, Entities.ToDo>();

            CreateMap<Models.ToDoForUpdateDto, Entities.ToDo>();
        }
    }
}
