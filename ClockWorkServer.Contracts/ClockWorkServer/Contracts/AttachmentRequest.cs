using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000AA RID: 170
	[DataContract(Namespace = "http://tpro.ca")]
	public class AttachmentRequest
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00002136 File Offset: 0x00000336
		[DataMember]
		public int Id { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000213F File Offset: 0x0000033F
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00002147 File Offset: 0x00000347
		[DataMember]
		public string Extension { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00002158 File Offset: 0x00000358
		[DataMember]
		public string Filename { get; set; }
	}
}
