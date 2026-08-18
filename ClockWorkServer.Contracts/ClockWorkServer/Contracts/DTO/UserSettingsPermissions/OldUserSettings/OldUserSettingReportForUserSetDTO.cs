using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000147 RID: 327
	[DataContract(Namespace = "http://tpro.ca")]
	public class OldUserSettingReportForUserSetDTO
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00003A0E File Offset: 0x00001C0E
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00003A16 File Offset: 0x00001C16
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00003A27 File Offset: 0x00001C27
		[DataMember]
		public IList<OldUserSettingReportForUserDTO> SettingsWithReports { get; set; }
	}
}
