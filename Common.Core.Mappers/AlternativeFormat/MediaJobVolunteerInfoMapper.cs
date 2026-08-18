using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200021E RID: 542
	public static class MediaJobVolunteerInfoMapper
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x00029468 File Offset: 0x00027668
		static MediaJobVolunteerInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			AlternateFormatVolunteerMapper.CreateMap();
			Mapper.CreateMap<MediaJobVolunteerInfo, MediaJobVolunteerInfoDTO>();
			Mapper.CreateMap<MediaJobVolunteerInfoDTO, MediaJobVolunteerInfo>().ForMember((MediaJobVolunteerInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaJobVolunteerInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000294F0 File Offset: 0x000276F0
		public static MediaJobVolunteerInfo ToDomainObject(this MediaJobVolunteerInfoDTO mediaJobVolunteerInfoDTO)
		{
			return Mapper.Map<MediaJobVolunteerInfoDTO, MediaJobVolunteerInfo>(mediaJobVolunteerInfoDTO);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00029508 File Offset: 0x00027708
		public static IList<MediaJobVolunteerInfo> ToDomainObject(this IList<MediaJobVolunteerInfoDTO> list)
		{
			IList<MediaJobVolunteerInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaJobVolunteerInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002954C File Offset: 0x0002774C
		public static MediaJobVolunteerInfoDTO ToDTO(this MediaJobVolunteerInfo mediaJobRunningNote)
		{
			return Mapper.Map<MediaJobVolunteerInfo, MediaJobVolunteerInfoDTO>(mediaJobRunningNote);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00029564 File Offset: 0x00027764
		public static IList<MediaJobVolunteerInfoDTO> ToDTO(this IList<MediaJobVolunteerInfo> list)
		{
			IList<MediaJobVolunteerInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaJobVolunteerInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
