using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x02000017 RID: 23
	public static class UserOrGroupJustPermissionMapper
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004598 File Offset: 0x00002798
		static UserOrGroupJustPermissionMapper()
		{
			Mapper.CreateMap<UserOrGroupJustPermission, UserOrGroupJustPermissionDTO>();
			Mapper.CreateMap<UserOrGroupJustPermissionDTO, UserOrGroupJustPermission>();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000045A8 File Offset: 0x000027A8
		public static UserOrGroupJustPermission ToDomainObject(this UserOrGroupJustPermissionDTO dto)
		{
			return Mapper.Map<UserOrGroupJustPermissionDTO, UserOrGroupJustPermission>(dto);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000045C0 File Offset: 0x000027C0
		public static UserOrGroupJustPermissionDTO ToDTO(this UserOrGroupJustPermission item)
		{
			return Mapper.Map<UserOrGroupJustPermission, UserOrGroupJustPermissionDTO>(item);
		}
	}
}
