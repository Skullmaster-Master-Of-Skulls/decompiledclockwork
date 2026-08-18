using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Storages
{
	// Token: 0x0200025F RID: 607
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptAndVerifyResp
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00006897 File Offset: 0x00004A97
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0000689F File Offset: 0x00004A9F
		[DataMember]
		public byte[] DecryptedFile { get; set; }
	}
}
