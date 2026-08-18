using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000133 RID: 307
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUserSettingReportForUserSetReq : BaseMessageReq
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00003569 File Offset: 0x00001769
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00003571 File Offset: 0x00001771
		[DataMember]
		public int PersonId { get; set; }
	}
}
