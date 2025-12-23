using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Subjectes;
using Domain.Entities.Classes;

namespace Application.Dto.Classes;

public class GetClassDto : BaseDto, IMapFrom<Class>
{
    public string Name { get; set; }
    public List<GetSubjectDto> Subject { get; set; }


}
