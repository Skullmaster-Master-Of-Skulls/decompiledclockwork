using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ActionPlan;
using TechnoPro.Common.Public.Entities.ActionPlan;

namespace TechnoPro.Common.Core.Mappers.ActionPlan
{
	// Token: 0x0200022C RID: 556
	public static class ActionTaskMapper
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0002B42C File Offset: 0x0002962C
		static ActionTaskMapper()
		{
			CompletionStatusMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ActionTaskDTO, ActionTask>().ForMember((ActionTask pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ActionTaskDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ActionTask, ActionTaskDTO>();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002B4B4 File Offset: 0x000296B4
		public static ActionTask ToDomainObject(this ActionTaskDTO actionTaskDTO)
		{
			return Mapper.Map<ActionTaskDTO, ActionTask>(actionTaskDTO);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002B4CC File Offset: 0x000296CC
		public static ActionTaskDTO ToDTO(this ActionTask actionTask)
		{
			return Mapper.Map<ActionTask, ActionTaskDTO>(actionTask);
		}
	}
}
