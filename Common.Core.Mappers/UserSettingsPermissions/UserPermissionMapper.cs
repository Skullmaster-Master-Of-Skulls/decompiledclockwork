using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x0200001B RID: 27
	public static class UserPermissionMapper
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004C68 File Offset: 0x00002E68
		static UserPermissionMapper()
		{
			Mapper.CreateMap<UserPermission, UserPermissionDTO>();
			Mapper.CreateMap<UserPermissionDTO, UserPermission>();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004C78 File Offset: 0x00002E78
		public static UserPermission ToDomainObject(this UserPermissionDTO dto)
		{
			return Mapper.Map<UserPermissionDTO, UserPermission>(dto);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004C90 File Offset: 0x00002E90
		public static UserPermissionDTO ToDTO(this UserPermission item)
		{
			return Mapper.Map<UserPermission, UserPermissionDTO>(item);
		}
	}
}
