using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000216 RID: 534
	public static class MediaContentPerFormatStatusInfoMapper
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x00026E48 File Offset: 0x00025048
		static MediaContentPerFormatStatusInfoMapper()
		{
			Mapper.CreateMap<MediaContentPerFormatStatusInfo, MediaContentPerFormatStatusInfoDTO>();
			Mapper.CreateMap<MediaContentPerFormatStatusInfoDTO, MediaContentPerFormatStatusInfo>().ForMember((MediaContentPerFormatStatusInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaContentPerFormatStatusInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00026EC4 File Offset: 0x000250C4
		public static MediaContentPerFormatStatusInfo ToDomainObject(this MediaContentPerFormatStatusInfoDTO dto)
		{
			return Mapper.Map<MediaContentPerFormatStatusInfoDTO, MediaContentPerFormatStatusInfo>(dto);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00026EDC File Offset: 0x000250DC
		public static MediaContentPerFormatStatusInfoDTO ToDTO(this MediaContentPerFormatStatusInfo bo)
		{
			return Mapper.Map<MediaContentPerFormatStatusInfo, MediaContentPerFormatStatusInfoDTO>(bo);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00026EF4 File Offset: 0x000250F4
		public static IList<MediaContentPerFormatStatusInfo> ToDomainObject(this IList<MediaContentPerFormatStatusInfoDTO> list)
		{
			IList<MediaContentPerFormatStatusInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaContentPerFormatStatusInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00026F38 File Offset: 0x00025138
		public static IList<MediaContentPerFormatStatusInfoDTO> ToDTO(this IList<MediaContentPerFormatStatusInfo> list)
		{
			IList<MediaContentPerFormatStatusInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaContentPerFormatStatusInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
