using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x0200001A RID: 26
	public static class UserPermissionIsAllowedSetMapper
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00004944 File Offset: 0x00002B44
		static UserPermissionIsAllowedSetMapper()
		{
			UserPermissionIsAllowedMapper.CreateMap();
			Mapper.CreateMap<UserPermissionIsAllowedSet, UserPermissionIsAllowedSetDTO>().ForMember((UserPermissionIsAllowedSetDTO pb) => pb.GeneralPermissionsAllowed, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSet> m)
			{
				m.MapFrom<List<UserPermissionIsAllowedDTO>>((UserPermissionIsAllowedSet pbdto) => (pbdto.GeneralPermissionsAllowed == null) ? null : (from g in pbdto.GeneralPermissionsAllowed
				select g.ToDTO()).ToList<UserPermissionIsAllowedDTO>());
			}).ForMember((UserPermissionIsAllowedSetDTO pb) => pb.ScreenNumsAllowedCreateScreen, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSet> m)
			{
				m.MapFrom<List<int>>((UserPermissionIsAllowedSet pbdto) => (pbdto.ScreenNumsAllowedCreateScreen == null) ? null : pbdto.ScreenNumsAllowedCreateScreen.ToList<int>());
			}).ForMember((UserPermissionIsAllowedSetDTO pb) => pb.ScreenNumsAllowedModifyScreen, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSet> m)
			{
				m.MapFrom<List<int>>((UserPermissionIsAllowedSet pbdto) => (pbdto.ScreenNumsAllowedModifyScreen == null) ? null : pbdto.ScreenNumsAllowedModifyScreen.ToList<int>());
			}).ForMember((UserPermissionIsAllowedSetDTO pb) => pb.ScreenNumsAllowedViewScreen, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSet> m)
			{
				m.MapFrom<List<int>>((UserPermissionIsAllowedSet pbdto) => (pbdto.ScreenNumsAllowedViewScreen == null) ? null : pbdto.ScreenNumsAllowedViewScreen.ToList<int>());
			});
			Mapper.CreateMap<UserPermissionIsAllowedSetDTO, UserPermissionIsAllowedSet>().ForMember((UserPermissionIsAllowedSet pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSetDTO> m)
			{
				m.Ignore();
			}).ForMember((UserPermissionIsAllowedSet pb) => pb.GeneralPermissionsAllowed, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSetDTO> m)
			{
				m.MapFrom<List<UserPermissionIsAllowed>>((UserPermissionIsAllowedSetDTO pbdto) => (pbdto.GeneralPermissionsAllowed == null) ? null : (from g in pbdto.GeneralPermissionsAllowed
				select g.ToDomainObject()).ToList<UserPermissionIsAllowed>());
			}).ForMember((UserPermissionIsAllowedSet pb) => pb.ScreenNumsAllowedCreateScreen, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSetDTO> m)
			{
				m.MapFrom<List<int>>((UserPermissionIsAllowedSetDTO pbdto) => (pbdto.ScreenNumsAllowedCreateScreen == null) ? null : pbdto.ScreenNumsAllowedCreateScreen.ToList<int>());
			}).ForMember((UserPermissionIsAllowedSet pb) => pb.ScreenNumsAllowedModifyScreen, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSetDTO> m)
			{
				m.MapFrom<List<int>>((UserPermissionIsAllowedSetDTO pbdto) => (pbdto.ScreenNumsAllowedModifyScreen == null) ? null : pbdto.ScreenNumsAllowedModifyScreen.ToList<int>());
			}).ForMember((UserPermissionIsAllowedSet pb) => pb.ScreenNumsAllowedViewScreen, delegate(IMemberConfigurationExpression<UserPermissionIsAllowedSetDTO> m)
			{
				m.MapFrom<List<int>>((UserPermissionIsAllowedSetDTO pbdto) => (pbdto.ScreenNumsAllowedViewScreen == null) ? null : pbdto.ScreenNumsAllowedViewScreen.ToList<int>());
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004C38 File Offset: 0x00002E38
		public static UserPermissionIsAllowedSet ToDomainObject(this UserPermissionIsAllowedSetDTO dto)
		{
			return Mapper.Map<UserPermissionIsAllowedSetDTO, UserPermissionIsAllowedSet>(dto);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004C50 File Offset: 0x00002E50
		public static UserPermissionIsAllowedSetDTO ToDTO(this UserPermissionIsAllowedSet item)
		{
			return Mapper.Map<UserPermissionIsAllowedSet, UserPermissionIsAllowedSetDTO>(item);
		}
	}
}
