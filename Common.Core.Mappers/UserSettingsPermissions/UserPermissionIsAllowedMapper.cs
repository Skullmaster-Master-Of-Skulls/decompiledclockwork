using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x02000019 RID: 25
	public static class UserPermissionIsAllowedMapper
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004898 File Offset: 0x00002A98
		static UserPermissionIsAllowedMapper()
		{
			Mapper.CreateMap<UserPermissionIsAllowed, UserPermissionIsAllowedDTO>();
			Mapper.CreateMap<UserPermissionIsAllowedDTO, UserPermissionIsAllowed>().ForMember((UserPermissionIsAllowed pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004914 File Offset: 0x00002B14
		public static UserPermissionIsAllowed ToDomainObject(this UserPermissionIsAllowedDTO dto)
		{
			return Mapper.Map<UserPermissionIsAllowedDTO, UserPermissionIsAllowed>(dto);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000492C File Offset: 0x00002B2C
		public static UserPermissionIsAllowedDTO ToDTO(this UserPermissionIsAllowed item)
		{
			return Mapper.Map<UserPermissionIsAllowed, UserPermissionIsAllowedDTO>(item);
		}
	}
}
