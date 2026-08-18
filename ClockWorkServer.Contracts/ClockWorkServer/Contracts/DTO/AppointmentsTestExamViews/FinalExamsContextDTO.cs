using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A2 RID: 2466
	[DataContract(Namespace = "http://tpro.ca")]
	public class FinalExamsContextDTO
	{
		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x0001839C File Offset: 0x0001659C
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x000183A4 File Offset: 0x000165A4
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000183AD File Offset: 0x000165AD
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x000183B5 File Offset: 0x000165B5
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
