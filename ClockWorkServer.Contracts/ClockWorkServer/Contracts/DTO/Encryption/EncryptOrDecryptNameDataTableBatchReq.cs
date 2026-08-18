using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x0200060B RID: 1547
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncryptOrDecryptNameDataTableBatchReq : BaseMessageReq
	{
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0000E560 File Offset: 0x0000C760
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x0000E568 File Offset: 0x0000C768
		[DataMember]
		public bool Encrypt { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x0000E571 File Offset: 0x0000C771
		// (set) Token: 0x06001F8D RID: 8077 RVA: 0x0000E579 File Offset: 0x0000C779
		[DataMember]
		public DataTable Table { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0000E582 File Offset: 0x0000C782
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x0000E58A File Offset: 0x0000C78A
		[DataMember]
		public string[] ColsToEncryptOrDecrypt { get; set; }
	}
}
