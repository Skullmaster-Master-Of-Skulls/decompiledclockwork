using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000220 RID: 544
	public static class AlternateFormatVolunteerMapper
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x000296E4 File Offset: 0x000278E4
		static AlternateFormatVolunteerMapper()
		{
			PersonBaseMapper.CreateMap();
			StaffCommonInfoMapper.CreateMap();
			Mapper.CreateMap<AlternateFormatVolunteerDTO, AlternateFormatVolunteer>().ForMember((AlternateFormatVolunteer pb) => pb.Staff, delegate(IMemberConfigurationExpression<AlternateFormatVolunteerDTO> m)
			{
				m.MapFrom<PersonBase>((AlternateFormatVolunteerDTO pbdto) => (pbdto.Staff == null) ? null : pbdto.Staff.ToDomainObject());
			}).ForMember((AlternateFormatVolunteer pb) => pb.StaffCommonInfo, delegate(IMemberConfigurationExpression<AlternateFormatVolunteerDTO> m)
			{
				m.MapFrom<StaffCommonInfo>((AlternateFormatVolunteerDTO pbdto) => (pbdto.StaffCommonInfo == null) ? null : pbdto.StaffCommonInfo.ToDomainObject());
			});
			Mapper.CreateMap<AlternateFormatVolunteer, AlternateFormatVolunteerDTO>().ForMember((AlternateFormatVolunteerDTO pb) => pb.Staff, delegate(IMemberConfigurationExpression<AlternateFormatVolunteer> m)
			{
				m.MapFrom<PersonBaseDTO>((AlternateFormatVolunteer pbdto) => (pbdto.Staff == null) ? null : pbdto.Staff.ToDTO());
			}).ForMember((AlternateFormatVolunteerDTO pb) => pb.StaffCommonInfo, delegate(IMemberConfigurationExpression<AlternateFormatVolunteer> m)
			{
				m.MapFrom<StaffCommonInfoDTO>((AlternateFormatVolunteer pbdto) => (pbdto.StaffCommonInfo == null) ? null : pbdto.StaffCommonInfo.ToDTO());
			});
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00029844 File Offset: 0x00027A44
		public static AlternateFormatVolunteer ToDomainObject(this AlternateFormatVolunteerDTO dto)
		{
			return Mapper.Map<AlternateFormatVolunteerDTO, AlternateFormatVolunteer>(dto);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002985C File Offset: 0x00027A5C
		public static AlternateFormatVolunteerDTO ToDTO(this AlternateFormatVolunteer item)
		{
			return Mapper.Map<AlternateFormatVolunteer, AlternateFormatVolunteerDTO>(item);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00029874 File Offset: 0x00027A74
		public static IList<AlternateFormatVolunteer> ToDomainObject(this IList<AlternateFormatVolunteerDTO> list)
		{
			IList<AlternateFormatVolunteer> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AlternateFormatVolunteer>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000298B8 File Offset: 0x00027AB8
		public static IList<AlternateFormatVolunteerDTO> ToDTO(this IList<AlternateFormatVolunteer> list)
		{
			IList<AlternateFormatVolunteerDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AlternateFormatVolunteerDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
