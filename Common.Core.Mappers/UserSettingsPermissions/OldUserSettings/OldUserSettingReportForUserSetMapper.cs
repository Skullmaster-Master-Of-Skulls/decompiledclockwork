using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200001F RID: 31
	public static class OldUserSettingReportForUserSetMapper
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00005294 File Offset: 0x00003494
		static OldUserSettingReportForUserSetMapper()
		{
			OldUserSettingReportForUserMapper.CreateMap();
			Mapper.CreateMap<OldUserSettingReportForUserSet, OldUserSettingReportForUserSetDTO>().ForMember((OldUserSettingReportForUserSetDTO pb) => pb.SettingsWithReports, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserSet> m)
			{
				m.MapFrom<List<OldUserSettingReportForUserDTO>>((OldUserSettingReportForUserSet pbdto) => (pbdto.SettingsWithReports == null) ? null : (from g in pbdto.SettingsWithReports
				select g.ToDTO()).ToList<OldUserSettingReportForUserDTO>());
			});
			Mapper.CreateMap<OldUserSettingReportForUserSetDTO, OldUserSettingReportForUserSet>().ForMember((OldUserSettingReportForUserSet pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserSetDTO> m)
			{
				m.Ignore();
			}).ForMember((OldUserSettingReportForUserSet pb) => pb.SettingsWithReports, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserSetDTO> m)
			{
				m.MapFrom<List<OldUserSettingReportForUser>>((OldUserSettingReportForUserSetDTO pbdto) => (pbdto.SettingsWithReports == null) ? null : (from g in pbdto.SettingsWithReports
				select g.ToDomainObject()).ToList<OldUserSettingReportForUser>());
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000053B4 File Offset: 0x000035B4
		public static OldUserSettingReportForUserSet ToDomainObject(this OldUserSettingReportForUserSetDTO dto)
		{
			return Mapper.Map<OldUserSettingReportForUserSetDTO, OldUserSettingReportForUserSet>(dto);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000053CC File Offset: 0x000035CC
		public static OldUserSettingReportForUserSetDTO ToDTO(this OldUserSettingReportForUserSet item)
		{
			return Mapper.Map<OldUserSettingReportForUserSet, OldUserSettingReportForUserSetDTO>(item);
		}
	}
}
