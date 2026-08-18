using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000134 RID: 308
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUserSettingReportForUserSetResp
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0000357A File Offset: 0x0000177A
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00003582 File Offset: 0x00001782
		[DataMember]
		public OldUserSettingReportForUserSetDTO ReportSet { get; set; }
	}
}
