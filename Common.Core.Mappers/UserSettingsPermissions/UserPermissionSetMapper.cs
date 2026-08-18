using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x0200001C RID: 28
	public static class UserPermissionSetMapper
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004CA8 File Offset: 0x00002EA8
		static UserPermissionSetMapper()
		{
			UserPermissionMapper.CreateMap();
			Mapper.CreateMap<UserPermissionSet, UserPermissionSetDTO>().ForMember((UserPermissionSetDTO pb) => pb.GroupPermissions, delegate(IMemberConfigurationExpression<UserPermissionSet> m)
			{
				m.MapFrom<List<UserPermissionDTO>>((UserPermissionSet pbdto) => (pbdto.GroupPermissions == null) ? null : (from g in pbdto.GroupPermissions
				select g.ToDTO()).ToList<UserPermissionDTO>());
			}).ForMember((UserPermissionSetDTO pb) => pb.PersonPermissions, delegate(IMemberConfigurationExpression<UserPermissionSet> m)
			{
				m.MapFrom<List<UserPermissionDTO>>((UserPermissionSet pbdto) => (pbdto.PersonPermissions == null) ? null : (from g in pbdto.PersonPermissions
				select g.ToDTO()).ToList<UserPermissionDTO>());
			});
			Mapper.CreateMap<UserPermissionSetDTO, UserPermissionSet>().ForMember((UserPermissionSet pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<UserPermissionSetDTO> m)
			{
				m.Ignore();
			}).ForMember((UserPermissionSet pb) => pb.GroupPermissions, delegate(IMemberConfigurationExpression<UserPermissionSetDTO> m)
			{
				m.MapFrom<List<UserPermission>>((UserPermissionSetDTO pbdto) => (pbdto.GroupPermissions == null) ? null : (from g in pbdto.GroupPermissions
				select g.ToDomainObject()).ToList<UserPermission>());
			}).ForMember((UserPermissionSet pb) => pb.PersonPermissions, delegate(IMemberConfigurationExpression<UserPermissionSetDTO> m)
			{
				m.MapFrom<List<UserPermission>>((UserPermissionSetDTO pbdto) => (pbdto.PersonPermissions == null) ? null : (from g in pbdto.PersonPermissions
				select g.ToDomainObject()).ToList<UserPermission>());
			});
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004E64 File Offset: 0x00003064
		public static UserPermissionSet ToDomainObject(this UserPermissionSetDTO dto)
		{
			return Mapper.Map<UserPermissionSetDTO, UserPermissionSet>(dto);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004E7C File Offset: 0x0000307C
		public static UserPermissionSetDTO ToDTO(this UserPermissionSet item)
		{
			return Mapper.Map<UserPermissionSet, UserPermissionSetDTO>(item);
		}
	}
}
