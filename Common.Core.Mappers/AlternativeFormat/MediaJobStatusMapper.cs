using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200021D RID: 541
	public static class MediaJobStatusMapper
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x00029334 File Offset: 0x00027534
		static MediaJobStatusMapper()
		{
			Mapper.CreateMap<MediaJobStatus, MediaJobStatusDTO>();
			Mapper.CreateMap<MediaJobStatusDTO, MediaJobStatus>().ForMember((MediaJobStatus bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaJobStatusDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000293B0 File Offset: 0x000275B0
		public static MediaJobStatus ToDomainObject(this MediaJobStatusDTO mediaJobStatusDTO)
		{
			return Mapper.Map<MediaJobStatusDTO, MediaJobStatus>(mediaJobStatusDTO);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000293C8 File Offset: 0x000275C8
		public static IList<MediaJobStatus> ToDomainObject(this IList<MediaJobStatusDTO> list)
		{
			IList<MediaJobStatus> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaJobStatus>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002940C File Offset: 0x0002760C
		public static MediaJobStatusDTO ToDTO(this MediaJobStatus mediaJobStatus)
		{
			return Mapper.Map<MediaJobStatus, MediaJobStatusDTO>(mediaJobStatus);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00029424 File Offset: 0x00027624
		public static IList<MediaJobStatusDTO> ToDTO(this IList<MediaJobStatus> list)
		{
			IList<MediaJobStatusDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaJobStatusDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
