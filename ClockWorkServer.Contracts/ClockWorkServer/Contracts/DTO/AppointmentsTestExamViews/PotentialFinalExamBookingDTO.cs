using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009AA RID: 2474
	[DataContract(Namespace = "http://tpro.ca")]
	public class PotentialFinalExamBookingDTO
	{
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000185E7 File Offset: 0x000167E7
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x000185EF File Offset: 0x000167EF
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000185F8 File Offset: 0x000167F8
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x00018600 File Offset: 0x00016800
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x00018609 File Offset: 0x00016809
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x00018611 File Offset: 0x00016811
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x0001861A File Offset: 0x0001681A
		// (set) Token: 0x06003236 RID: 12854 RVA: 0x00018622 File Offset: 0x00016822
		[DataMember]
		public DateTime ExamStartDateTime { get; set; }

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x0001862B File Offset: 0x0001682B
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x00018633 File Offset: 0x00016833
		[DataMember]
		public DateTime ExamEndDateTime { get; set; }
	}
}
