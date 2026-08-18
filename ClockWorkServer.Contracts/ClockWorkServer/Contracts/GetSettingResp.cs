using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000BD RID: 189
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingResp
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000255D File Offset: 0x0000075D
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00002565 File Offset: 0x00000765
		[DataMember]
		public AppSettingDTO Setting { get; set; }
	}
}
