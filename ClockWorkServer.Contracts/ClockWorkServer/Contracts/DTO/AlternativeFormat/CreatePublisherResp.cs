using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C13 RID: 3091
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePublisherResp
	{
		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x0001FD51 File Offset: 0x0001DF51
		// (set) Token: 0x060040F4 RID: 16628 RVA: 0x0001FD59 File Offset: 0x0001DF59
		[DataMember]
		public int MediaPublisherId { get; set; }
	}
}
