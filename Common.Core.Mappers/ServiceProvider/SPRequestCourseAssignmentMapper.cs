using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000071 RID: 113
	public static class SPRequestCourseAssignmentMapper
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		static SPRequestCourseAssignmentMapper()
		{
			SPProviderMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<SPRequestCourseAssignment, SPRequestCourseAssignmentDTO>();
			Mapper.CreateMap<SPRequestCourseAssignmentDTO, SPRequestCourseAssignment>().ForMember((SPRequestCourseAssignment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestCourseAssignmentDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000BA5C File Offset: 0x00009C5C
		public static SPRequestCourseAssignment ToDomainObject(this SPRequestCourseAssignmentDTO dto)
		{
			return Mapper.Map<SPRequestCourseAssignmentDTO, SPRequestCourseAssignment>(dto);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000BA74 File Offset: 0x00009C74
		public static SPRequestCourseAssignmentDTO ToDTO(this SPRequestCourseAssignment item)
		{
			return Mapper.Map<SPRequestCourseAssignment, SPRequestCourseAssignmentDTO>(item);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000BA8C File Offset: 0x00009C8C
		public static IList<SPRequestCourseAssignment> ToDomainObject(this IList<SPRequestCourseAssignmentDTO> list)
		{
			IList<SPRequestCourseAssignment> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestCourseAssignment>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public static IList<SPRequestCourseAssignmentDTO> ToDTO(this IList<SPRequestCourseAssignment> list)
		{
			IList<SPRequestCourseAssignmentDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestCourseAssignmentDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
