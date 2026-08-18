using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C12 RID: 3090
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePublisherReq : BaseMessageReq
	{
		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x0001FD40 File Offset: 0x0001DF40
		// (set) Token: 0x060040F1 RID: 16625 RVA: 0x0001FD48 File Offset: 0x0001DF48
		[DataMember]
		public MediaPublisherDTO MediaPublisher { get; set; }
	}
}
