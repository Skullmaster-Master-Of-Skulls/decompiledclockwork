using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x02000018 RID: 24
	public static class UserOrGroupJustPermissionSetMapper
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000045D8 File Offset: 0x000027D8
		static UserOrGroupJustPermissionSetMapper()
		{
			UserOrGroupJustPermissionMapper.CreateMap();
			Mapper.CreateMap<UserOrGroupJustPermissionSet, UserOrGroupJustPermissionSetDTO>().ForMember((UserOrGroupJustPermissionSetDTO pb) => pb.GeneralPermissions, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSet> m)
			{
				m.MapFrom<List<UserOrGroupJustPermissionDTO>>((UserOrGroupJustPermissionSet pbdto) => (pbdto.GeneralPermissions == null) ? null : (from g in pbdto.GeneralPermissions
				select g.ToDTO()).ToList<UserOrGroupJustPermissionDTO>());
			}).ForMember((UserOrGroupJustPermissionSetDTO pb) => pb.ScreenNumsAllowedCreateScreen, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSet> m)
			{
				m.MapFrom<List<int>>((UserOrGroupJustPermissionSet pbdto) => (pbdto.ScreenNumsAllowedCreateScreen == null) ? null : pbdto.ScreenNumsAllowedCreateScreen.ToList<int>());
			}).ForMember((UserOrGroupJustPermissionSetDTO pb) => pb.ScreenNumsAllowedModifyScreen, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSet> m)
			{
				m.MapFrom<List<int>>((UserOrGroupJustPermissionSet pbdto) => (pbdto.ScreenNumsAllowedModifyScreen == null) ? null : pbdto.ScreenNumsAllowedModifyScreen.ToList<int>());
			}).ForMember((UserOrGroupJustPermissionSetDTO pb) => pb.ScreenNumsAllowedViewScreen, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSet> m)
			{
				m.MapFrom<List<int>>((UserOrGroupJustPermissionSet pbdto) => (pbdto.ScreenNumsAllowedViewScreen == null) ? null : pbdto.ScreenNumsAllowedViewScreen.ToList<int>());
			});
			Mapper.CreateMap<UserOrGroupJustPermissionSetDTO, UserOrGroupJustPermissionSet>().ForMember((UserOrGroupJustPermissionSet pb) => pb.GeneralPermissions, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSetDTO> m)
			{
				m.MapFrom<List<UserOrGroupJustPermission>>((UserOrGroupJustPermissionSetDTO pbdto) => (pbdto.GeneralPermissions == null) ? null : (from g in pbdto.GeneralPermissions
				select g.ToDomainObject()).ToList<UserOrGroupJustPermission>());
			}).ForMember((UserOrGroupJustPermissionSet pb) => pb.ScreenNumsAllowedCreateScreen, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSetDTO> m)
			{
				m.MapFrom<List<int>>((UserOrGroupJustPermissionSetDTO pbdto) => (pbdto.ScreenNumsAllowedCreateScreen == null) ? null : pbdto.ScreenNumsAllowedCreateScreen.ToList<int>());
			}).ForMember((UserOrGroupJustPermissionSet pb) => pb.ScreenNumsAllowedModifyScreen, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSetDTO> m)
			{
				m.MapFrom<List<int>>((UserOrGroupJustPermissionSetDTO pbdto) => (pbdto.ScreenNumsAllowedModifyScreen == null) ? null : pbdto.ScreenNumsAllowedModifyScreen.ToList<int>());
			}).ForMember((UserOrGroupJustPermissionSet pb) => pb.ScreenNumsAllowedViewScreen, delegate(IMemberConfigurationExpression<UserOrGroupJustPermissionSetDTO> m)
			{
				m.MapFrom<List<int>>((UserOrGroupJustPermissionSetDTO pbdto) => (pbdto.ScreenNumsAllowedViewScreen == null) ? null : pbdto.ScreenNumsAllowedViewScreen.ToList<int>());
			});
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004868 File Offset: 0x00002A68
		public static UserOrGroupJustPermissionSet ToDomainObject(this UserOrGroupJustPermissionSetDTO dto)
		{
			return Mapper.Map<UserOrGroupJustPermissionSetDTO, UserOrGroupJustPermissionSet>(dto);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004880 File Offset: 0x00002A80
		public static UserOrGroupJustPermissionSetDTO ToDTO(this UserOrGroupJustPermissionSet item)
		{
			return Mapper.Map<UserOrGroupJustPermissionSet, UserOrGroupJustPermissionSetDTO>(item);
		}
	}
}
