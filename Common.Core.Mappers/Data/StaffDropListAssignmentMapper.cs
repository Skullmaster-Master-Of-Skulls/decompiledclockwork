using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.Data;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Data
{
	// Token: 0x02000137 RID: 311
	public static class StaffDropListAssignmentMapper
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00019928 File Offset: 0x00017B28
		static StaffDropListAssignmentMapper()
		{
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<StaffDropListAssignment, StaffDropListAssignmentDTO>().ForMember((StaffDropListAssignmentDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<StaffDropListAssignment> m)
			{
				m.MapFrom<BasicPersonDTO>((StaffDropListAssignment pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			});
			Mapper.CreateMap<StaffDropListAssignmentDTO, StaffDropListAssignment>().ForMember((StaffDropListAssignment pb) => pb.Student, delegate(IMemberConfigurationExpression<StaffDropListAssignmentDTO> m)
			{
				m.MapFrom<BasicPerson>((StaffDropListAssignmentDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			});
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000199E4 File Offset: 0x00017BE4
		public static StaffDropListAssignment ToDomainObject(this StaffDropListAssignmentDTO groupDTO)
		{
			return Mapper.Map<StaffDropListAssignmentDTO, StaffDropListAssignment>(groupDTO);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000199FC File Offset: 0x00017BFC
		public static StaffDropListAssignmentDTO ToDTO(this StaffDropListAssignment group)
		{
			return Mapper.Map<StaffDropListAssignment, StaffDropListAssignmentDTO>(group);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00019A14 File Offset: 0x00017C14
		public static IList<StaffDropListAssignment> ToDomainObject(this IList<StaffDropListAssignmentDTO> dtos)
		{
			IList<StaffDropListAssignment> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<StaffDropListAssignment>();
			}
			return result;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00019A58 File Offset: 0x00017C58
		public static IList<StaffDropListAssignmentDTO> ToDTO(this IList<StaffDropListAssignment> items)
		{
			IList<StaffDropListAssignmentDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<StaffDropListAssignmentDTO>();
			}
			return result;
		}
	}
}
