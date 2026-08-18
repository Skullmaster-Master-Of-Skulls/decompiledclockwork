using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000219 RID: 537
	public static class MediaJobMapper
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00028A04 File Offset: 0x00026C04
		static MediaJobMapper()
		{
			PersonBaseMapper.CreateMap();
			MediaContentMapper.CreateMap();
			CampusMapper.CreateMap();
			Mapper.CreateMap<MediaJob, MediaJobDTO>().ForMember((MediaJobDTO dto) => dto.Campus, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.MapFrom<SchoolCampusDTO>((MediaJob bo) => bo.Campus.ToDTO());
			});
			Mapper.CreateMap<MediaJobDTO, MediaJob>().ForMember((MediaJob bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaJobDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaJob bo) => bo.Campus, delegate(IMemberConfigurationExpression<MediaJobDTO> m)
			{
				m.MapFrom<SchoolCampus>((MediaJobDTO dto) => dto.Campus.ToDomainObject());
			});
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00028B30 File Offset: 0x00026D30
		public static MediaJob ToDomainObject(this MediaJobDTO mediaJobDTO)
		{
			return Mapper.Map<MediaJobDTO, MediaJob>(mediaJobDTO);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00028B48 File Offset: 0x00026D48
		public static IList<MediaJob> ToDomainObject(this IList<MediaJobDTO> list)
		{
			IList<MediaJob> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaJob>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00028B8C File Offset: 0x00026D8C
		public static MediaJobDTO ToDTO(this MediaJob mediaJob)
		{
			return Mapper.Map<MediaJob, MediaJobDTO>(mediaJob);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00028BA4 File Offset: 0x00026DA4
		public static IList<MediaJobDTO> ToDTO(this IList<MediaJob> list)
		{
			IList<MediaJobDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaJobDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
