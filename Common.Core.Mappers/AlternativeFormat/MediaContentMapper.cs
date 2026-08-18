using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000213 RID: 531
	public static class MediaContentMapper
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x00026C00 File Offset: 0x00024E00
		static MediaContentMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			BasicMediaContentMapper.CreateMap();
			Mapper.CreateMap<MediaContent, MediaContentDTO>();
			Mapper.CreateMap<MediaContentDTO, MediaContent>();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00026C1C File Offset: 0x00024E1C
		public static MediaContent ToDomainObject(this MediaContentDTO mediaContentDTO)
		{
			return Mapper.Map<MediaContentDTO, MediaContent>(mediaContentDTO);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00026C34 File Offset: 0x00024E34
		public static IList<MediaContent> ToDomainObject(this IList<MediaContentDTO> list)
		{
			IList<MediaContent> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaContent>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00026C78 File Offset: 0x00024E78
		public static MediaContentDTO ToDTO(this MediaContent mediaContent)
		{
			return Mapper.Map<MediaContent, MediaContentDTO>(mediaContent);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00026C90 File Offset: 0x00024E90
		public static IList<MediaContentDTO> ToDTO(this IList<MediaContent> list)
		{
			IList<MediaContentDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaContentDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
