using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200021B RID: 539
	public static class CompletedMediaJobMapper
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00028EF4 File Offset: 0x000270F4
		static CompletedMediaJobMapper()
		{
			MediaJobMapper.CreateMap();
			Mapper.CreateMap<MediaJob, CompletedMediaJob>().ForMember((CompletedMediaJob bo) => bo.CompletedBy, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CompletedMediaJob bo) => bo.CompletedNotes, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CompletedMediaJob bo) => (object)bo.CompletedOn, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CompletedMediaJob bo) => (object)bo.IsCancelled, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			}).ForMember((CompletedMediaJob bo) => (object)bo.IsCompleted, delegate(IMemberConfigurationExpression<MediaJob> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CompletedMediaJob, CompletedMediaJobDTO>();
			Mapper.CreateMap<CompletedMediaJobDTO, CompletedMediaJob>().ForMember((CompletedMediaJob bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<CompletedMediaJobDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00029130 File Offset: 0x00027330
		public static CompletedMediaJob ToDomainObject(this CompletedMediaJobDTO mediaJobDTO)
		{
			return Mapper.Map<CompletedMediaJobDTO, CompletedMediaJob>(mediaJobDTO);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00029148 File Offset: 0x00027348
		public static IList<CompletedMediaJob> ToDomainObject(this IList<CompletedMediaJobDTO> list)
		{
			IList<CompletedMediaJob> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<CompletedMediaJob>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002918C File Offset: 0x0002738C
		public static CompletedMediaJobDTO ToDTO(this CompletedMediaJob mediaJob)
		{
			return Mapper.Map<CompletedMediaJob, CompletedMediaJobDTO>(mediaJob);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000291A4 File Offset: 0x000273A4
		public static IList<CompletedMediaJobDTO> ToDTO(this IList<CompletedMediaJob> list)
		{
			IList<CompletedMediaJobDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<CompletedMediaJobDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000291E8 File Offset: 0x000273E8
		public static CompletedMediaJob CopyToCompletedMediaJob(this MediaJob mediaJob)
		{
			return Mapper.Map<MediaJob, CompletedMediaJob>(mediaJob);
		}
	}
}
