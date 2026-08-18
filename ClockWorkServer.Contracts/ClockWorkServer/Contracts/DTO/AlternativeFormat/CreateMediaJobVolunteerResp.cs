using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C00 RID: 3072
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaJobVolunteerResp
	{
		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x0001FB64 File Offset: 0x0001DD64
		// (set) Token: 0x060040A7 RID: 16551 RVA: 0x0001FB6C File Offset: 0x0001DD6C
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
