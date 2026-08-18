using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.Core.Mappers.AlertTrigger
{
	// Token: 0x0200022B RID: 555
	public static class AlertTriggerForUserGroupMapper
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0002B340 File Offset: 0x00029540
		static AlertTriggerForUserGroupMapper()
		{
			AlertTriggerForUserMapper.CreateMap();
			Mapper.CreateMap<AlertTriggerForUserGroupDTO, AlertTriggerForUserGroup>().ForMember((AlertTriggerForUserGroup pb) => pb.Triggers, delegate(IMemberConfigurationExpression<AlertTriggerForUserGroupDTO> m)
			{
				m.MapFrom<List<AlertTriggerForUser>>((AlertTriggerForUserGroupDTO pbdto) => (pbdto.Triggers == null) ? null : (from g in pbdto.Triggers
				select g.ToDomainObject()).ToList<AlertTriggerForUser>());
			});
			Mapper.CreateMap<AlertTriggerForUserGroup, AlertTriggerForUserGroupDTO>().ForMember((AlertTriggerForUserGroupDTO pb) => pb.Triggers, delegate(IMemberConfigurationExpression<AlertTriggerForUserGroup> m)
			{
				m.MapFrom<List<AlertTriggerForUserDTO>>((AlertTriggerForUserGroup pbdto) => (pbdto.Triggers == null) ? null : (from g in pbdto.Triggers
				select g.ToDTO()).ToList<AlertTriggerForUserDTO>());
			});
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002B3FC File Offset: 0x000295FC
		public static AlertTriggerForUserGroup ToDomainObject(this AlertTriggerForUserGroupDTO dto)
		{
			return Mapper.Map<AlertTriggerForUserGroupDTO, AlertTriggerForUserGroup>(dto);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002B414 File Offset: 0x00029614
		public static AlertTriggerForUserGroupDTO ToDTO(this AlertTriggerForUserGroup item)
		{
			return Mapper.Map<AlertTriggerForUserGroup, AlertTriggerForUserGroupDTO>(item);
		}
	}
}
