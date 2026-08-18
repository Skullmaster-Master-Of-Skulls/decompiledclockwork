using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C14 RID: 3092
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePublisherReq : BaseMessageReq
	{
		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x0001FD62 File Offset: 0x0001DF62
		// (set) Token: 0x060040F7 RID: 16631 RVA: 0x0001FD6A File Offset: 0x0001DF6A
		[DataMember]
		public MediaPublisherDTO MediaPublisher { get; set; }
	}
}
