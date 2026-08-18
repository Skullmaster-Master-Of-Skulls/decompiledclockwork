using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004E2 RID: 1250
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReverseEncryptionOnDataResp
	{
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x0000C3CF File Offset: 0x0000A5CF
		[DataMember]
		public int NumItemsAffected { get; set; }
	}
}
