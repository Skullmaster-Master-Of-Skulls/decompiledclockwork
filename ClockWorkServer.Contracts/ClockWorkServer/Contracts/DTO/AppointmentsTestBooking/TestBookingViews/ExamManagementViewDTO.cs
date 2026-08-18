using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x02000A44 RID: 2628
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExamManagementViewDTO
	{
		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x0001A3FD File Offset: 0x000185FD
		// (set) Token: 0x06003630 RID: 13872 RVA: 0x0001A405 File Offset: 0x00018605
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x0001A40E File Offset: 0x0001860E
		// (set) Token: 0x06003632 RID: 13874 RVA: 0x0001A416 File Offset: 0x00018616
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x0001A41F File Offset: 0x0001861F
		// (set) Token: 0x06003634 RID: 13876 RVA: 0x0001A427 File Offset: 0x00018627
		[DataMember]
		public eExamManagementViewGroup Group { get; set; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x0001A430 File Offset: 0x00018630
		// (set) Token: 0x06003636 RID: 13878 RVA: 0x0001A438 File Offset: 0x00018638
		[DataMember]
		public eExamManagementViewType ViewType { get; set; }

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x0001A441 File Offset: 0x00018641
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x0001A449 File Offset: 0x00018649
		[DataMember]
		public eExamManagementQueryType QueryType { get; set; }

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x0001A452 File Offset: 0x00018652
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x0001A45A File Offset: 0x0001865A
		[DataMember]
		public int? StartDaysFromToday { get; set; }

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x0001A463 File Offset: 0x00018663
		// (set) Token: 0x0600363C RID: 13884 RVA: 0x0001A46B File Offset: 0x0001866B
		[DataMember]
		public int? EndNumDays { get; set; }

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x0001A474 File Offset: 0x00018674
		// (set) Token: 0x0600363E RID: 13886 RVA: 0x0001A47C File Offset: 0x0001867C
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x0001A485 File Offset: 0x00018685
		// (set) Token: 0x06003640 RID: 13888 RVA: 0x0001A48D File Offset: 0x0001868D
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x0001A496 File Offset: 0x00018696
		// (set) Token: 0x06003642 RID: 13890 RVA: 0x0001A49E File Offset: 0x0001869E
		[DataMember]
		public int ReportId { get; set; }
	}
}
