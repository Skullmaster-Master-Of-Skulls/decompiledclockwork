using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x02000130 RID: 304
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUserPersonalSettingValueResp
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00003503 File Offset: 0x00001703
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0000350B File Offset: 0x0000170B
		[DataMember]
		public OldUserSettingDTO SettingValue { get; set; }
	}
}
