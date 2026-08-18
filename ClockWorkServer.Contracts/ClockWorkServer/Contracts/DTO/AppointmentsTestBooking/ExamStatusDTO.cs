using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B1 RID: 2481
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExamStatusDTO
	{
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x000188CC File Offset: 0x00016ACC
		// (set) Token: 0x0600328B RID: 12939 RVA: 0x000188D4 File Offset: 0x00016AD4
		[DataMember]
		public int ExamStatusLookupId { get; set; }

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000188DD File Offset: 0x00016ADD
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x000188E5 File Offset: 0x00016AE5
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000188EE File Offset: 0x00016AEE
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x000188F6 File Offset: 0x00016AF6
		[DataMember]
		public int ColourArgB { get; set; }
	}
}
