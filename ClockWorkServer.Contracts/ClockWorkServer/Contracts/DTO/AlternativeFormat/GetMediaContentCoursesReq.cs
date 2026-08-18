using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B84 RID: 2948
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentCoursesReq : BaseMessageReq
	{
		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x0001E840 File Offset: 0x0001CA40
		// (set) Token: 0x06003E43 RID: 15939 RVA: 0x0001E848 File Offset: 0x0001CA48
		[DataMember]
		public Guid MediaContentId { get; set; }
	}
}
