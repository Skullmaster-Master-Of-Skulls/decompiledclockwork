using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B72 RID: 2930
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByPublisherReq : BaseMessageReq
	{
		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x0001E71F File Offset: 0x0001C91F
		// (set) Token: 0x06003E0F RID: 15887 RVA: 0x0001E727 File Offset: 0x0001C927
		[DataMember]
		public int PublisherID { get; set; }
	}
}
