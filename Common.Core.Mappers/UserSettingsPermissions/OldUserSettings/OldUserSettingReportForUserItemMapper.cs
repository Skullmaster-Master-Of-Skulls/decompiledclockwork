using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200001D RID: 29
	public static class OldUserSettingReportForUserItemMapper
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004E94 File Offset: 0x00003094
		static OldUserSettingReportForUserItemMapper()
		{
			Mapper.CreateMap<OldUserSettingReportForUserItem, OldUserSettingReportForUserItemDTO>().ForMember((OldUserSettingReportForUserItemDTO pb) => pb.StringVal, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItem> m)
			{
				m.MapFrom<string>((OldUserSettingReportForUserItem pbdto) => pbdto.StringVal);
			}).ForMember((OldUserSettingReportForUserItemDTO pb) => (object)pb.IntVal, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItem> m)
			{
				m.MapFrom<int>((OldUserSettingReportForUserItem pbdto) => pbdto.IntVal);
			}).ForMember((OldUserSettingReportForUserItemDTO pb) => (object)pb.PersonOrGroupId, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItem> m)
			{
				m.MapFrom<int>((OldUserSettingReportForUserItem pbdto) => pbdto.PersonOrGroupId);
			}).ForMember((OldUserSettingReportForUserItemDTO pb) => (object)pb.SettingType, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItem> m)
			{
				m.MapFrom<eOldUserSettingType>((OldUserSettingReportForUserItem pbdto) => pbdto.SettingType);
			});
			Mapper.CreateMap<OldUserSettingReportForUserItemDTO, OldUserSettingReportForUserItem>().ForMember((OldUserSettingReportForUserItem pb) => pb.StringVal, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItemDTO> m)
			{
				m.MapFrom<string>((OldUserSettingReportForUserItemDTO pbdto) => pbdto.StringVal);
			}).ForMember((OldUserSettingReportForUserItem pb) => (object)pb.IntVal, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItemDTO> m)
			{
				m.MapFrom<int>((OldUserSettingReportForUserItemDTO pbdto) => pbdto.IntVal);
			}).ForMember((OldUserSettingReportForUserItem pb) => (object)pb.PersonOrGroupId, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItemDTO> m)
			{
				m.MapFrom<int>((OldUserSettingReportForUserItemDTO pbdto) => pbdto.PersonOrGroupId);
			}).ForMember((OldUserSettingReportForUserItem pb) => (object)pb.SettingType, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserItemDTO> m)
			{
				m.MapFrom<eOldUserSettingType>((OldUserSettingReportForUserItemDTO pbdto) => pbdto.SettingType);
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005178 File Offset: 0x00003378
		public static OldUserSettingReportForUserItem ToDomainObject(this OldUserSettingReportForUserItemDTO dto)
		{
			return Mapper.Map<OldUserSettingReportForUserItemDTO, OldUserSettingReportForUserItem>(dto);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005190 File Offset: 0x00003390
		public static OldUserSettingReportForUserItemDTO ToDTO(this OldUserSettingReportForUserItem item)
		{
			return Mapper.Map<OldUserSettingReportForUserItem, OldUserSettingReportForUserItemDTO>(item);
		}
	}
}
