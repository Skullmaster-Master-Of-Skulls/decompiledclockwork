using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x0200006C RID: 108
	public static class SPProviderCourseRegistrationMapper
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000B3AC File Offset: 0x000095AC
		static SPProviderCourseRegistrationMapper()
		{
			SPProviderMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			CourseRegistrationStatusMapper.CreateMap();
			Mapper.CreateMap<SPProviderCourseRegistration, SPProviderCourseRegistrationDTO>();
			Mapper.CreateMap<SPProviderCourseRegistrationDTO, SPProviderCourseRegistration>().ForMember((SPProviderCourseRegistration pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPProviderCourseRegistrationDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000B43C File Offset: 0x0000963C
		public static SPProviderCourseRegistration ToDomainObject(this SPProviderCourseRegistrationDTO dto)
		{
			return Mapper.Map<SPProviderCourseRegistrationDTO, SPProviderCourseRegistration>(dto);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B454 File Offset: 0x00009654
		public static SPProviderCourseRegistrationDTO ToDTO(this SPProviderCourseRegistration item)
		{
			return Mapper.Map<SPProviderCourseRegistration, SPProviderCourseRegistrationDTO>(item);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B46C File Offset: 0x0000966C
		public static IList<SPProviderCourseRegistration> ToDomainObject(this IList<SPProviderCourseRegistrationDTO> list)
		{
			IList<SPProviderCourseRegistration> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPProviderCourseRegistration>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static IList<SPProviderCourseRegistrationDTO> ToDTO(this IList<SPProviderCourseRegistration> list)
		{
			IList<SPProviderCourseRegistrationDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPProviderCourseRegistrationDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
