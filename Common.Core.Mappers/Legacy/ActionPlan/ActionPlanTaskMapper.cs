using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;

namespace TechnoPro.Common.Core.Mappers.Legacy.ActionPlan
{
	// Token: 0x020000EE RID: 238
	public static class ActionPlanTaskMapper
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00012E38 File Offset: 0x00011038
		static ActionPlanTaskMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ActionPlanTaskDTO, ActionPlanTask>().ForMember((ActionPlanTask pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ActionPlanTaskDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ActionPlanTask, ActionPlanTaskDTO>();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012EBC File Offset: 0x000110BC
		public static ActionPlanTask ToDomainObject(this ActionPlanTaskDTO dynamicDataDTO)
		{
			return Mapper.Map<ActionPlanTaskDTO, ActionPlanTask>(dynamicDataDTO);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00012ED4 File Offset: 0x000110D4
		public static ActionPlanTaskDTO ToDTO(this ActionPlanTask dynamicData)
		{
			return Mapper.Map<ActionPlanTask, ActionPlanTaskDTO>(dynamicData);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00012EEC File Offset: 0x000110EC
		public static IList<ActionPlanTask> ToDomainObject(this IList<ActionPlanTaskDTO> daos)
		{
			IList<ActionPlanTask> result;
			if (daos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in daos
				select g.ToDomainObject()).ToList<ActionPlanTask>();
			}
			return result;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00012F30 File Offset: 0x00011130
		public static IList<ActionPlanTaskDTO> ToDTO(this IList<ActionPlanTask> entities)
		{
			IList<ActionPlanTaskDTO> result;
			if (entities == null)
			{
				result = null;
			}
			else
			{
				result = (from g in entities
				select g.ToDTO()).ToList<ActionPlanTaskDTO>();
			}
			return result;
		}
	}
}
