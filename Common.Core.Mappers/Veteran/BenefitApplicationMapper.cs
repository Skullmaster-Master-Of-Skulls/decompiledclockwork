using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.Core.Mappers.Academic;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.Core.Mappers.Veteran
{
	// Token: 0x02000013 RID: 19
	public static class BenefitApplicationMapper
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00003B48 File Offset: 0x00001D48
		static BenefitApplicationMapper()
		{
			SemesterMapper.CreateMap();
			VeteranChapterMapper.CreateMap();
			Mapper.CreateMap<BenefitApplicationDTO, BenefitApplication>().ForMember((BenefitApplication pb) => pb.Semester, delegate(IMemberConfigurationExpression<BenefitApplicationDTO> m)
			{
				m.MapFrom<Semester>((BenefitApplicationDTO pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDomainObject());
			}).ForMember((BenefitApplication pb) => pb.Chapter, delegate(IMemberConfigurationExpression<BenefitApplicationDTO> m)
			{
				m.MapFrom<BenefitApplication>((BenefitApplicationDTO pbdto) => (pbdto.Chapter == null) ? null : pbdto.ToDomainObject());
			});
			Mapper.CreateMap<BenefitApplication, BenefitApplicationDTO>().ForMember((BenefitApplicationDTO pb) => pb.Semester, delegate(IMemberConfigurationExpression<BenefitApplication> m)
			{
				m.MapFrom<SemesterDTO>((BenefitApplication pbdto) => (pbdto.Semester == null) ? null : pbdto.Semester.ToDTO());
			}).ForMember((BenefitApplicationDTO pb) => pb.Chapter, delegate(IMemberConfigurationExpression<BenefitApplication> m)
			{
				m.MapFrom<VeteranChapterDTO>((BenefitApplication pbdto) => (pbdto.Chapter == null) ? null : pbdto.Chapter.ToDTO());
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public static BenefitApplication ToDomainObject(this BenefitApplicationDTO dto)
		{
			return Mapper.Map<BenefitApplicationDTO, BenefitApplication>(dto);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public static BenefitApplicationDTO ToDTO(this BenefitApplication item)
		{
			return Mapper.Map<BenefitApplication, BenefitApplicationDTO>(item);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public static IList<BenefitApplication> ToDomainObject(this IList<BenefitApplicationDTO> dtos)
		{
			IList<BenefitApplication> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from dto in dtos
				select Mapper.Map<BenefitApplicationDTO, BenefitApplication>(dto)).ToList<BenefitApplication>();
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003D1C File Offset: 0x00001F1C
		public static IList<BenefitApplicationDTO> ToDTO(this IList<BenefitApplication> items)
		{
			IList<BenefitApplicationDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from item in items
				select Mapper.Map<BenefitApplication, BenefitApplicationDTO>(item)).ToList<BenefitApplicationDTO>();
			}
			return result;
		}
	}
}
