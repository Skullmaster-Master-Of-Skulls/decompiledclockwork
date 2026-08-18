using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C0 RID: 192
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveSettingReq : SettingsBaseMessageReq
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000025A1 File Offset: 0x000007A1
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x000025A9 File Offset: 0x000007A9
		[DataMember]
		public AppSettingDTO Setting { get; set; }
	}
}
