using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000145 RID: 325
	[DataContract(Namespace = "http://tpro.ca")]
	public class OldUserSettingReportForUserDTO
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x000039A8 File Offset: 0x00001BA8
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x000039B0 File Offset: 0x00001BB0
		[DataMember]
		public eSettingCode SettingCode { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000039B9 File Offset: 0x00001BB9
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x000039C1 File Offset: 0x00001BC1
		[DataMember]
		public IList<OldUserSettingReportForUserItemDTO> Items { get; set; }
	}
}
