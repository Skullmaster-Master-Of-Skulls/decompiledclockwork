using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ActionPlan;
using TechnoPro.Common.Public.Entities.ActionPlan;

namespace TechnoPro.Common.Core.Mappers.ActionPlan
{
	// Token: 0x0200022D RID: 557
	public static class CompletionStatusMapper
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0002B4E4 File Offset: 0x000296E4
		static CompletionStatusMapper()
		{
			Mapper.CreateMap<ActionTaskCompletionStatusDTO, ActionTaskCompletionStatus>().ForMember((ActionTaskCompletionStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ActionTaskCompletionStatusDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ActionTaskCompletionStatus, ActionTaskCompletionStatusDTO>();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002B560 File Offset: 0x00029760
		public static ActionTaskCompletionStatus ToDomainObject(this ActionTaskCompletionStatusDTO completionStatusDTO)
		{
			return Mapper.Map<ActionTaskCompletionStatusDTO, ActionTaskCompletionStatus>(completionStatusDTO);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002B578 File Offset: 0x00029778
		public static ActionTaskCompletionStatusDTO ToDTO(this ActionTaskCompletionStatus actionTask)
		{
			return Mapper.Map<ActionTaskCompletionStatus, ActionTaskCompletionStatusDTO>(actionTask);
		}
	}
}
