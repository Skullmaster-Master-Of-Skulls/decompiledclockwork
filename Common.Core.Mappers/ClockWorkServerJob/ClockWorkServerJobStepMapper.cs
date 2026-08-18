using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerJob
{
	// Token: 0x0200016A RID: 362
	public static class ClockWorkServerJobStepMapper
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0001C914 File Offset: 0x0001AB14
		static ClockWorkServerJobStepMapper()
		{
			Mapper.CreateMap<ClockWorkServerJobStep, ClockWorkServerJobStepDTO>();
			Mapper.CreateMap<ClockWorkServerJobStepDTO, ClockWorkServerJobStep>().ForMember((ClockWorkServerJobStep pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClockWorkServerJobStepDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001C990 File Offset: 0x0001AB90
		public static ClockWorkServerJobStep ToDomainObject(this ClockWorkServerJobStepDTO dto)
		{
			return Mapper.Map<ClockWorkServerJobStepDTO, ClockWorkServerJobStep>(dto);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		public static ClockWorkServerJobStepDTO ToDTO(this ClockWorkServerJobStep bo)
		{
			return Mapper.Map<ClockWorkServerJobStep, ClockWorkServerJobStepDTO>(bo);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		public static IList<ClockWorkServerJobStep> ToDomainObject(this IList<ClockWorkServerJobStepDTO> list)
		{
			IList<ClockWorkServerJobStep> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<ClockWorkServerJobStep>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001CA04 File Offset: 0x0001AC04
		public static IList<ClockWorkServerJobStepDTO> ToDTO(this IList<ClockWorkServerJobStep> list)
		{
			IList<ClockWorkServerJobStepDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<ClockWorkServerJobStepDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
