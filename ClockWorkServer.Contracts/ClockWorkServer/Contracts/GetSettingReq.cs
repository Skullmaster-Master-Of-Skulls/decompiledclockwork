using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000BC RID: 188
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingReq : SettingsBaseMessageReq
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000254C File Offset: 0x0000074C
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00002554 File Offset: 0x00000754
		[DataMember]
		public Setting Setting { get; set; }
	}
}
