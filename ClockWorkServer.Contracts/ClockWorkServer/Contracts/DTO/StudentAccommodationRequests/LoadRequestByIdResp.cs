using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000251 RID: 593
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestByIdResp
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00006247 File Offset: 0x00004447
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0000624F File Offset: 0x0000444F
		[DataMember]
		public StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest { get; set; }
	}
}
