using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions
{
	// Token: 0x02000016 RID: 22
	public static class OldUserSettingMapper
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003F38 File Offset: 0x00002138
		static OldUserSettingMapper()
		{
			Mapper.CreateMap<OldUserSetting, OldUserSettingDTO>().ForMember((OldUserSettingDTO pb) => pb.StringVal, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<string>((OldUserSetting pbdto) => pbdto.StringVal);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.IntVal, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<int>((OldUserSetting pbdto) => pbdto.IntVal);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.SettingIdOrSettingGroupId, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<int>((OldUserSetting pbdto) => pbdto.SettingIdOrSettingGroupId);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.SettingCode, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<eSettingCode>((OldUserSetting pbdto) => pbdto.SettingCode);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.ModificationStatus, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<eDataItemModificationStatus>((OldUserSetting pbdto) => pbdto.ModificationStatus);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.PersonOrGroupId, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<int>((OldUserSetting pbdto) => pbdto.PersonOrGroupId);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.SettingType, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<eOldUserSettingType>((OldUserSetting pbdto) => pbdto.SettingType);
			}).ForMember((OldUserSettingDTO pb) => (object)pb.OrderNum, delegate(IMemberConfigurationExpression<OldUserSetting> m)
			{
				m.MapFrom<int>((OldUserSetting pbdto) => pbdto.OrderNum);
			});
			Mapper.CreateMap<OldUserSettingDTO, OldUserSetting>().ForMember((OldUserSetting f) => (object)f.Id, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.Ignore();
			}).ForMember((OldUserSetting pb) => pb.StringVal, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<string>((OldUserSettingDTO pbdto) => pbdto.StringVal);
			}).ForMember((OldUserSetting pb) => (object)pb.IntVal, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<int>((OldUserSettingDTO pbdto) => pbdto.IntVal);
			}).ForMember((OldUserSetting pb) => (object)pb.SettingIdOrSettingGroupId, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<int>((OldUserSettingDTO pbdto) => pbdto.SettingIdOrSettingGroupId);
			}).ForMember((OldUserSetting pb) => (object)pb.SettingCode, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<eSettingCode>((OldUserSettingDTO pbdto) => pbdto.SettingCode);
			}).ForMember((OldUserSetting pb) => (object)pb.ModificationStatus, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<eDataItemModificationStatus>((OldUserSettingDTO pbdto) => pbdto.ModificationStatus);
			}).ForMember((OldUserSetting pb) => (object)pb.PersonOrGroupId, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<int>((OldUserSettingDTO pbdto) => pbdto.PersonOrGroupId);
			}).ForMember((OldUserSetting pb) => (object)pb.SettingType, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<eOldUserSettingType>((OldUserSettingDTO pbdto) => pbdto.SettingType);
			}).ForMember((OldUserSetting pb) => (object)pb.OrderNum, delegate(IMemberConfigurationExpression<OldUserSettingDTO> m)
			{
				m.MapFrom<int>((OldUserSettingDTO pbdto) => pbdto.OrderNum);
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004568 File Offset: 0x00002768
		public static OldUserSetting ToDomainObject(this OldUserSettingDTO dto)
		{
			return Mapper.Map<OldUserSettingDTO, OldUserSetting>(dto);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004580 File Offset: 0x00002780
		public static OldUserSettingDTO ToDTO(this OldUserSetting item)
		{
			return Mapper.Map<OldUserSetting, OldUserSettingDTO>(item);
		}
	}
}
