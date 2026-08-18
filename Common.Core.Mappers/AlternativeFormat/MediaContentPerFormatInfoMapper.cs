using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000215 RID: 533
	public static class MediaContentPerFormatInfoMapper
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x00026D14 File Offset: 0x00024F14
		static MediaContentPerFormatInfoMapper()
		{
			Mapper.CreateMap<MediaContentPerFormatInfo, MediaContentPerFormatInfoDTO>();
			Mapper.CreateMap<MediaContentPerFormatInfoDTO, MediaContentPerFormatInfo>().ForMember((MediaContentPerFormatInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaContentPerFormatInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00026D90 File Offset: 0x00024F90
		public static MediaContentPerFormatInfo ToDomainObject(this MediaContentPerFormatInfoDTO dto)
		{
			return Mapper.Map<MediaContentPerFormatInfoDTO, MediaContentPerFormatInfo>(dto);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00026DA8 File Offset: 0x00024FA8
		public static MediaContentPerFormatInfoDTO ToDTO(this MediaContentPerFormatInfo bo)
		{
			return Mapper.Map<MediaContentPerFormatInfo, MediaContentPerFormatInfoDTO>(bo);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00026DC0 File Offset: 0x00024FC0
		public static IList<MediaContentPerFormatInfo> ToDomainObject(this IList<MediaContentPerFormatInfoDTO> list)
		{
			IList<MediaContentPerFormatInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaContentPerFormatInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00026E04 File Offset: 0x00025004
		public static IList<MediaContentPerFormatInfoDTO> ToDTO(this IList<MediaContentPerFormatInfo> list)
		{
			IList<MediaContentPerFormatInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaContentPerFormatInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
