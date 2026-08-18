using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000BE RID: 190
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingFromStringReq : SettingsBaseMessageReq
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000256E File Offset: 0x0000076E
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00002576 File Offset: 0x00000776
		[DataMember]
		public Setting Setting { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00002587 File Offset: 0x00000787
		[DataMember]
		public string StringValue { get; set; }
	}
}
