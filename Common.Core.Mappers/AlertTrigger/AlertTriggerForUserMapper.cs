using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.Core.Mappers.AlertTrigger
{
	// Token: 0x02000229 RID: 553
	public static class AlertTriggerForUserMapper
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x0002B215 File Offset: 0x00029415
		static AlertTriggerForUserMapper()
		{
			Mapper.CreateMap<AlertTriggerForUserDTO, AlertTriggerForUser>();
			Mapper.CreateMap<AlertTriggerForUser, AlertTriggerForUserDTO>();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002B224 File Offset: 0x00029424
		public static AlertTriggerForUser ToDomainObject(this AlertTriggerForUserDTO dto)
		{
			return Mapper.Map<AlertTriggerForUserDTO, AlertTriggerForUser>(dto);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002B23C File Offset: 0x0002943C
		public static AlertTriggerForUserDTO ToDTO(this AlertTriggerForUser item)
		{
			return Mapper.Map<AlertTriggerForUser, AlertTriggerForUserDTO>(item);
		}
	}
}
