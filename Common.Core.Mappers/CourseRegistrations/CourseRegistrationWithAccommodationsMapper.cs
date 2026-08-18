using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.Mappers.Accommodations;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x02000160 RID: 352
	public static class CourseRegistrationWithAccommodationsMapper
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x0001BF1C File Offset: 0x0001A11C
		static CourseRegistrationWithAccommodationsMapper()
		{
			AccommodationDataMapper.CreateMap();
			CourseRegistrationMapper.CreateMap();
			Mapper.CreateMap<CourseRegistrationWithAccommodationsDTO, CourseRegistrationWithAccommodations>().ForMember((CourseRegistrationWithAccommodations pb) => pb.CourseOrTemplateAccommodations, delegate(IMemberConfigurationExpression<CourseRegistrationWithAccommodationsDTO> m)
			{
				m.MapFrom<List<AccommodationData>>((CourseRegistrationWithAccommodationsDTO pbdto) => (pbdto.CourseOrTemplateAccommodations == null) ? null : (from g in pbdto.CourseOrTemplateAccommodations
				select g.ToDomainObject()).ToList<AccommodationData>());
			}).ForMember((CourseRegistrationWithAccommodations pb) => pb.CourseReg, delegate(IMemberConfigurationExpression<CourseRegistrationWithAccommodationsDTO> m)
			{
				m.MapFrom<CourseRegistration>((CourseRegistrationWithAccommodationsDTO pbdto) => (pbdto.CourseReg == null) ? null : pbdto.CourseReg.ToDomainObject());
			});
			Mapper.CreateMap<CourseRegistrationWithAccommodations, CourseRegistrationWithAccommodationsDTO>().ForMember((CourseRegistrationWithAccommodationsDTO pb) => pb.CourseOrTemplateAccommodations, delegate(IMemberConfigurationExpression<CourseRegistrationWithAccommodations> m)
			{
				m.MapFrom<List<AccommodationDataDTO>>((CourseRegistrationWithAccommodations pbdto) => (pbdto.CourseOrTemplateAccommodations == null) ? null : (from g in pbdto.CourseOrTemplateAccommodations
				select g.ToDTO()).ToList<AccommodationDataDTO>());
			}).ForMember((CourseRegistrationWithAccommodationsDTO pb) => pb.CourseReg, delegate(IMemberConfigurationExpression<CourseRegistrationWithAccommodations> m)
			{
				m.MapFrom<CourseRegistrationDTO>((CourseRegistrationWithAccommodations pbdto) => (pbdto.CourseReg == null) ? null : pbdto.CourseReg.ToDTO());
			});
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001C07C File Offset: 0x0001A27C
		public static CourseRegistrationWithAccommodations ToDomainObject(this CourseRegistrationWithAccommodationsDTO dto)
		{
			return Mapper.Map<CourseRegistrationWithAccommodationsDTO, CourseRegistrationWithAccommodations>(dto);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001C094 File Offset: 0x0001A294
		public static CourseRegistrationWithAccommodationsDTO ToDTO(this CourseRegistrationWithAccommodations item)
		{
			return Mapper.Map<CourseRegistrationWithAccommodations, CourseRegistrationWithAccommodationsDTO>(item);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001C0AC File Offset: 0x0001A2AC
		public static IList<CourseRegistrationWithAccommodations> ToDomainObject(this IList<CourseRegistrationWithAccommodationsDTO> list)
		{
			IList<CourseRegistrationWithAccommodations> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<CourseRegistrationWithAccommodations>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001C0F0 File Offset: 0x0001A2F0
		public static IList<CourseRegistrationWithAccommodationsDTO> ToDTO(this IList<CourseRegistrationWithAccommodations> list)
		{
			IList<CourseRegistrationWithAccommodationsDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<CourseRegistrationWithAccommodationsDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
