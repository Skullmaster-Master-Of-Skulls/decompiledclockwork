using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C17 RID: 3095
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeletePublisherResp
	{
		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x0001FD95 File Offset: 0x0001DF95
		// (set) Token: 0x06004100 RID: 16640 RVA: 0x0001FD9D File Offset: 0x0001DF9D
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}
