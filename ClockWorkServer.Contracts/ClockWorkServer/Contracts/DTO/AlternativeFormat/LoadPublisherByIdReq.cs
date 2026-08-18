using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C18 RID: 3096
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPublisherByIdReq : BaseMessageReq
	{
		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x0001FDA6 File Offset: 0x0001DFA6
		// (set) Token: 0x06004103 RID: 16643 RVA: 0x0001FDAE File Offset: 0x0001DFAE
		[DataMember]
		public int MediaPublisherId { get; set; }
	}
}
