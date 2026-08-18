using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000BF RID: 191
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingFromStringResp
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00002590 File Offset: 0x00000790
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00002598 File Offset: 0x00000798
		[DataMember]
		public AppSettingDTO Setting { get; set; }
	}
}
