using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.Core.Mappers.AlertTrigger
{
	// Token: 0x0200022A RID: 554
	public static class AlertTriggerForUserSetMapper
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x0002B254 File Offset: 0x00029454
		static AlertTriggerForUserSetMapper()
		{
			AlertTriggerForUserGroupMapper.CreateMap();
			Mapper.CreateMap<AlertTriggerForUserSetDTO, AlertTriggerForUserSet>().ForMember((AlertTriggerForUserSet pb) => pb.AlertTriggerGroups, delegate(IMemberConfigurationExpression<AlertTriggerForUserSetDTO> m)
			{
				m.MapFrom<List<AlertTriggerForUserGroup>>((AlertTriggerForUserSetDTO pbdto) => (pbdto.AlertTriggerGroups == null) ? null : (from g in pbdto.AlertTriggerGroups
				select g.ToDomainObject()).ToList<AlertTriggerForUserGroup>());
			});
			Mapper.CreateMap<AlertTriggerForUserSet, AlertTriggerForUserSetDTO>().ForMember((AlertTriggerForUserSetDTO pb) => pb.AlertTriggerGroups, delegate(IMemberConfigurationExpression<AlertTriggerForUserSet> m)
			{
				m.MapFrom<List<AlertTriggerForUserGroupDTO>>((AlertTriggerForUserSet pbdto) => (pbdto.AlertTriggerGroups == null) ? null : (from g in pbdto.AlertTriggerGroups
				select g.ToDTO()).ToList<AlertTriggerForUserGroupDTO>());
			});
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002B310 File Offset: 0x00029510
		public static AlertTriggerForUserSet ToDomainObject(this AlertTriggerForUserSetDTO dto)
		{
			return Mapper.Map<AlertTriggerForUserSetDTO, AlertTriggerForUserSet>(dto);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002B328 File Offset: 0x00029528
		public static AlertTriggerForUserSetDTO ToDTO(this AlertTriggerForUserSet item)
		{
			return Mapper.Map<AlertTriggerForUserSet, AlertTriggerForUserSetDTO>(item);
		}
	}
}
