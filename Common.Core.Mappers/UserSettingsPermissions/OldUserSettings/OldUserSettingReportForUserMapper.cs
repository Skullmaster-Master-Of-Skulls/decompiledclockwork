using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Mappers.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200001E RID: 30
	public static class OldUserSettingReportForUserMapper
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000051A8 File Offset: 0x000033A8
		static OldUserSettingReportForUserMapper()
		{
			OldUserSettingReportForUserItemMapper.CreateMap();
			Mapper.CreateMap<OldUserSettingReportForUser, OldUserSettingReportForUserDTO>().ForMember((OldUserSettingReportForUserDTO pb) => pb.Items, delegate(IMemberConfigurationExpression<OldUserSettingReportForUser> m)
			{
				m.MapFrom<List<OldUserSettingReportForUserItemDTO>>((OldUserSettingReportForUser pbdto) => (pbdto.Items == null) ? null : (from g in pbdto.Items
				select g.ToDTO()).ToList<OldUserSettingReportForUserItemDTO>());
			});
			Mapper.CreateMap<OldUserSettingReportForUserDTO, OldUserSettingReportForUser>().ForMember((OldUserSettingReportForUser pb) => pb.Items, delegate(IMemberConfigurationExpression<OldUserSettingReportForUserDTO> m)
			{
				m.MapFrom<List<OldUserSettingReportForUserItem>>((OldUserSettingReportForUserDTO pbdto) => (pbdto.Items == null) ? null : (from g in pbdto.Items
				select g.ToDomainObject()).ToList<OldUserSettingReportForUserItem>());
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005264 File Offset: 0x00003464
		public static OldUserSettingReportForUser ToDomainObject(this OldUserSettingReportForUserDTO dto)
		{
			return Mapper.Map<OldUserSettingReportForUserDTO, OldUserSettingReportForUser>(dto);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000527C File Offset: 0x0000347C
		public static OldUserSettingReportForUserDTO ToDTO(this OldUserSettingReportForUser item)
		{
			return Mapper.Map<OldUserSettingReportForUser, OldUserSettingReportForUserDTO>(item);
		}
	}
}
