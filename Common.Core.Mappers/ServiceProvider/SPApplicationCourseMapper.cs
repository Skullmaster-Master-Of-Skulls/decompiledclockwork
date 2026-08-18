using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x0200006A RID: 106
	public static class SPApplicationCourseMapper
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000B118 File Offset: 0x00009318
		static SPApplicationCourseMapper()
		{
			SPApplicationMapper.CreateMap();
			SPProviderCourseRegistrationMapper.CreateMap();
			Mapper.CreateMap<SPApplicationCourse, SPApplicationCourseDTO>();
			Mapper.CreateMap<SPApplicationCourseDTO, SPApplicationCourse>().ForMember((SPApplicationCourse pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPApplicationCourseDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public static SPApplicationCourse ToDomainObject(this SPApplicationCourseDTO dto)
		{
			return Mapper.Map<SPApplicationCourseDTO, SPApplicationCourse>(dto);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public static SPApplicationCourseDTO ToDTO(this SPApplicationCourse item)
		{
			return Mapper.Map<SPApplicationCourse, SPApplicationCourseDTO>(item);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public static IList<SPApplicationCourse> ToDomainObject(this IList<SPApplicationCourseDTO> list)
		{
			IList<SPApplicationCourse> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPApplicationCourse>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B214 File Offset: 0x00009414
		public static IList<SPApplicationCourseDTO> ToDTO(this IList<SPApplicationCourse> list)
		{
			IList<SPApplicationCourseDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPApplicationCourseDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
