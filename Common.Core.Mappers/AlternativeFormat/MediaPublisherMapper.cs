using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000221 RID: 545
	public static class MediaPublisherMapper
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x000298FC File Offset: 0x00027AFC
		static MediaPublisherMapper()
		{
			Mapper.CreateMap<MediaPublisher, MediaPublisherDTO>();
			Mapper.CreateMap<MediaPublisherDTO, MediaPublisher>().ForMember((MediaPublisher bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaPublisherDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00029978 File Offset: 0x00027B78
		public static MediaPublisher ToDomainObject(this MediaPublisherDTO mediaPublisherDTO)
		{
			return Mapper.Map<MediaPublisherDTO, MediaPublisher>(mediaPublisherDTO);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00029990 File Offset: 0x00027B90
		public static IList<MediaPublisher> ToDomainObject(this IList<MediaPublisherDTO> list)
		{
			IList<MediaPublisher> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaPublisher>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000299D4 File Offset: 0x00027BD4
		public static MediaPublisherDTO ToDTO(this MediaPublisher mediaPublisher)
		{
			return Mapper.Map<MediaPublisher, MediaPublisherDTO>(mediaPublisher);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000299EC File Offset: 0x00027BEC
		public static IList<MediaPublisherDTO> ToDTO(this IList<MediaPublisher> list)
		{
			IList<MediaPublisherDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaPublisherDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
