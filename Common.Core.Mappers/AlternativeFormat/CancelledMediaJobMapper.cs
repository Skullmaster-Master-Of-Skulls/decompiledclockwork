using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200021A RID: 538
	public static class CancelledMediaJobMapper
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x00028BE8 File Offset: 0x00026DE8
		static CancelledMediaJobMapper()
		{
			MediaJobMapper.CreateMap();
			Mapper.CreateMap<MediaJob, CancelledMediaJob>().ForMember((CancelledMediaJob bo) => bo.CancelledBy, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CancelledMediaJob bo) => bo.CancellationReason, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CancelledMediaJob bo) => (object)bo.CancelledOn, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CancelledMediaJob bo) => (object)bo.IsCancelled, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CancelledMediaJob bo) => (object)bo.IsCompleted, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CancelledMediaJob, CancelledMediaJobDTO>();
			Mapper.CreateMap<CancelledMediaJobDTO, CancelledMediaJob>().ForMember((CancelledMediaJob bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<CancelledMediaJobDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00028E24 File Offset: 0x00027024
		public static CancelledMediaJob ToDomainObject(this CancelledMediaJobDTO mediaJobDTO)
		{
			return Mapper.Map<CancelledMediaJobDTO, CancelledMediaJob>(mediaJobDTO);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00028E3C File Offset: 0x0002703C
		public static IList<CancelledMediaJob> ToDomainObject(this IList<CancelledMediaJobDTO> list)
		{
			IList<CancelledMediaJob> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<CancelledMediaJob>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00028E80 File Offset: 0x00027080
		public static CancelledMediaJobDTO ToDTO(this CancelledMediaJob mediaJob)
		{
			return Mapper.Map<CancelledMediaJob, CancelledMediaJobDTO>(mediaJob);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00028E98 File Offset: 0x00027098
		public static IList<CancelledMediaJobDTO> ToDTO(this IList<CancelledMediaJob> list)
		{
			IList<CancelledMediaJobDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<CancelledMediaJobDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00028EDC File Offset: 0x000270DC
		public static CancelledMediaJob CopyToCancelledMediaJob(this MediaJob mediaJob)
		{
			return Mapper.Map<MediaJob, CancelledMediaJob>(mediaJob);
		}
	}
}
