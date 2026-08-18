using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A8 RID: 168
	[DataContract(Namespace = "http://tpro.ca")]
	public class AttachmentFile
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00002059 File Offset: 0x00000259
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00002061 File Offset: 0x00000261
		[DataMember]
		public byte[] BinaryData { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000206A File Offset: 0x0000026A
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00002072 File Offset: 0x00000272
		[DataMember]
		public AttachmentInfo Info { get; set; }
	}
}
