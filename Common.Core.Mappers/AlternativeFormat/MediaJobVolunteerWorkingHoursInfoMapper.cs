using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200021F RID: 543
	public static class MediaJobVolunteerWorkingHoursInfoMapper
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x000295A8 File Offset: 0x000277A8
		static MediaJobVolunteerWorkingHoursInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<MediaJobVolunteerWorkingHoursInfo, MediaJobVolunteerWorkingHoursInfoDTO>();
			Mapper.CreateMap<MediaJobVolunteerWorkingHoursInfoDTO, MediaJobVolunteerWorkingHoursInfo>().ForMember((MediaJobVolunteerWorkingHoursInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaJobVolunteerWorkingHoursInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002962C File Offset: 0x0002782C
		public static MediaJobVolunteerWorkingHoursInfo ToDomainObject(this MediaJobVolunteerWorkingHoursInfoDTO mediaJobVolunteerWorkingHoursInfoDTO)
		{
			return Mapper.Map<MediaJobVolunteerWorkingHoursInfoDTO, MediaJobVolunteerWorkingHoursInfo>(mediaJobVolunteerWorkingHoursInfoDTO);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00029644 File Offset: 0x00027844
		public static IList<MediaJobVolunteerWorkingHoursInfo> ToDomainObject(this IList<MediaJobVolunteerWorkingHoursInfoDTO> list)
		{
			IList<MediaJobVolunteerWorkingHoursInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaJobVolunteerWorkingHoursInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00029688 File Offset: 0x00027888
		public static MediaJobVolunteerWorkingHoursInfoDTO ToDTO(this MediaJobVolunteerWorkingHoursInfo mediaJobVolunteerWorkingHoursInfo)
		{
			return Mapper.Map<MediaJobVolunteerWorkingHoursInfo, MediaJobVolunteerWorkingHoursInfoDTO>(mediaJobVolunteerWorkingHoursInfo);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000296A0 File Offset: 0x000278A0
		public static IList<MediaJobVolunteerWorkingHoursInfoDTO> ToDTO(this IList<MediaJobVolunteerWorkingHoursInfo> list)
		{
			IList<MediaJobVolunteerWorkingHoursInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaJobVolunteerWorkingHoursInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
