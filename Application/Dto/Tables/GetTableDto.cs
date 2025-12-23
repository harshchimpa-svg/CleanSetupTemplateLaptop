using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Tables
{
    public class GetTableDto : BaseDto, IMapFrom<Table>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
    }
}
