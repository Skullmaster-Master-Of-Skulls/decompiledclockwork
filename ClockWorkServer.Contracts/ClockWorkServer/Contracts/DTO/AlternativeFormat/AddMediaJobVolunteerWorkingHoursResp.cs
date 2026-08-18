using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C0C RID: 3084
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaJobVolunteerWorkingHoursResp
	{
		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x0001FC74 File Offset: 0x0001DE74
		// (set) Token: 0x060040D3 RID: 16595 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		[DataMember]
		public int MediaJobVolunteerWorkingHoursInfoId { get; set; }
	}
}
