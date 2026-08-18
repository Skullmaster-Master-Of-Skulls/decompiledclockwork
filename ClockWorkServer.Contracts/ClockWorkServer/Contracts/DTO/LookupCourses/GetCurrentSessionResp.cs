using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C3 RID: 1987
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCurrentSessionResp
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x0001347C File Offset: 0x0001167C
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x00013484 File Offset: 0x00011684
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
