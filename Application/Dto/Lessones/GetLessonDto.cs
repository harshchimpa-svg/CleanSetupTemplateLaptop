using Application.Common.Mappings.Commons;
using Application.Dto.Classes;
using Application.Dto.CommonDtos;
using Application.Dto.Subjectes;
using Domain.Entities.Lessones;

namespace Application.Dto.Lessones;

public class GetLessonDto : BaseDto, IMapFrom<Lesson>
{
    public string Name { get; set; }
    public string SubjectId { get; set; }
    public GetSubjectDto Subject { get; set; }
    public GetClassDto Class { get; set; }

}
