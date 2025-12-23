using Application.Common.Mappings.Commons;
using Application.Dto.Classes;
using Application.Dto.CommonDtos;
using Application.Dto.Lessones;
using Domain.Subjects;

namespace Application.Dto.Subjectes;

public class GetSubjectDto : BaseDto, IMapFrom<Subject>
{
    public string Name { get; set; }
    public string ClassId { get; set; }
    public GetClassDto Class { get; set; } = new();
    public List<GetLessonDto> Lesson { get; set; }


}
