using AutoMapper;
using Contracts.Common;
using Contracts.Schedules;
using Domain.Model.Entity;

namespace Application.Common.Mapping.Profiles;

public class CommonProfile : Profile
{
    public CommonProfile()
    {

        CreateMap<DayScheduleDTO, DayScheduleEntity>()
            .ForMember(
            c => c.GroupName,
            x => x.MapFrom(c => c.Group))
            .ForMember(
            c => c.Date,
            x => x.MapFrom(c => c.Date));

        CreateMap<DayScheduleDTO, List<LessonEntity>>()
            .ConvertUsing((src, dest, context) =>
                context.Mapper.Map<List<LessonEntity>>(src.Lessons));
    

        CreateMap<Lesson, LessonEntity>();
        CreateMap<LessonEntity, Lesson>();
    }
}
