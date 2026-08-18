using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C15 RID: 3093
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePublisherResp
	{
		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x0001FD73 File Offset: 0x0001DF73
		// (set) Token: 0x060040FA RID: 16634 RVA: 0x0001FD7B File Offset: 0x0001DF7B
		[DataMember]
		public bool WasUpdated { get; set; }
	}
}
