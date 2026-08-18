using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000072 RID: 114
	public static class SPRequestCourseMapper
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000BB14 File Offset: 0x00009D14
		static SPRequestCourseMapper()
		{
			SPRequestMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			SPRequestStatusTypeMapper.CreateMap();
			SPRequestAssignmentStatusTypeMapper.CreateMap();
			SPUrgencyLevelTypeMapper.CreateMap();
			SPRequestCourseAssignmentMapper.CreateMap();
			Mapper.CreateMap<SPRequestCourse, SPRequestCourseDTO>();
			Mapper.CreateMap<SPRequestCourseDTO, SPRequestCourse>().ForMember((SPRequestCourse pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestCourseDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public static SPRequestCourse ToDomainObject(this SPRequestCourseDTO dto)
		{
			return Mapper.Map<SPRequestCourseDTO, SPRequestCourse>(dto);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public static SPRequestCourseDTO ToDTO(this SPRequestCourse item)
		{
			return Mapper.Map<SPRequestCourse, SPRequestCourseDTO>(item);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public static IList<SPRequestCourse> ToDomainObject(this IList<SPRequestCourseDTO> list)
		{
			IList<SPRequestCourse> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestCourse>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000BC28 File Offset: 0x00009E28
		public static IList<SPRequestCourseDTO> ToDTO(this IList<SPRequestCourse> list)
		{
			IList<SPRequestCourseDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestCourseDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
