using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C16 RID: 3094
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeletePublisherReq : BaseMessageReq
	{
		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x0001FD84 File Offset: 0x0001DF84
		// (set) Token: 0x060040FD RID: 16637 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		[DataMember]
		public int MediaPublisherId { get; set; }
	}
}
