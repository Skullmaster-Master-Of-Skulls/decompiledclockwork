using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A4 RID: 2468
	[DataContract(Namespace = "http://tpro.ca")]
	public class FinalExamsViewLightBookingDTO
	{
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x0001849B File Offset: 0x0001669B
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000184A3 File Offset: 0x000166A3
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000184AC File Offset: 0x000166AC
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000184B4 File Offset: 0x000166B4
		[DataMember]
		public bool IsCancelled { get; set; }

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000184BD File Offset: 0x000166BD
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000184C5 File Offset: 0x000166C5
		[DataMember]
		public bool IsTentative { get; set; }

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000184CE File Offset: 0x000166CE
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000184D6 File Offset: 0x000166D6
		[DataMember]
		public bool IsNoShow { get; set; }

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x000184DF File Offset: 0x000166DF
		// (set) Token: 0x0600320C RID: 12812 RVA: 0x000184E7 File Offset: 0x000166E7
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000184F0 File Offset: 0x000166F0
		// (set) Token: 0x0600320E RID: 12814 RVA: 0x000184F8 File Offset: 0x000166F8
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x00018501 File Offset: 0x00016701
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x00018509 File Offset: 0x00016709
		[DataMember]
		public virtual string CourseTitle { get; set; }

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x00018512 File Offset: 0x00016712
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x0001851A File Offset: 0x0001671A
		[DataMember]
		public DateTime DateBooked { get; set; }

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x00018523 File Offset: 0x00016723
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x0001852B File Offset: 0x0001672B
		[DataMember]
		public DateTime StudentReportedClassTestStartDateTime { get; set; }

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x00018534 File Offset: 0x00016734
		// (set) Token: 0x06003216 RID: 12822 RVA: 0x0001853C File Offset: 0x0001673C
		[DataMember]
		public DateTime StudentReportedClassTestEndDateTime { get; set; }
	}
}
