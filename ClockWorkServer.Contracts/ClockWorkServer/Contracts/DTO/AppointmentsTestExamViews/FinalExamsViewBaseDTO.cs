using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A3 RID: 2467
	[DataContract(Namespace = "http://tpro.ca")]
	public class FinalExamsViewBaseDTO
	{
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000183BE File Offset: 0x000165BE
		// (set) Token: 0x060031E9 RID: 12777 RVA: 0x000183C6 File Offset: 0x000165C6
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000183CF File Offset: 0x000165CF
		// (set) Token: 0x060031EB RID: 12779 RVA: 0x000183D7 File Offset: 0x000165D7
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000183E0 File Offset: 0x000165E0
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x000183E8 File Offset: 0x000165E8
		[DataMember]
		public DateTime ExamStartDateTime { get; set; }

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000183F1 File Offset: 0x000165F1
		// (set) Token: 0x060031EF RID: 12783 RVA: 0x000183F9 File Offset: 0x000165F9
		[DataMember]
		public DateTime ExamEndDateTime { get; set; }

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x00018402 File Offset: 0x00016602
		// (set) Token: 0x060031F1 RID: 12785 RVA: 0x0001840A File Offset: 0x0001660A
		[DataMember]
		public virtual string CourseTitle { get; set; }

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x00018413 File Offset: 0x00016613
		// (set) Token: 0x060031F3 RID: 12787 RVA: 0x0001841B File Offset: 0x0001661B
		[DataMember]
		public bool HasTestCopy { get; set; }

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x00018424 File Offset: 0x00016624
		// (set) Token: 0x060031F5 RID: 12789 RVA: 0x0001842C File Offset: 0x0001662C
		[DataMember]
		public string TestCopyNote { get; set; }

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x00018435 File Offset: 0x00016635
		// (set) Token: 0x060031F7 RID: 12791 RVA: 0x0001843D File Offset: 0x0001663D
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x00018446 File Offset: 0x00016646
		// (set) Token: 0x060031F9 RID: 12793 RVA: 0x0001844E File Offset: 0x0001664E
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x00018457 File Offset: 0x00016657
		// (set) Token: 0x060031FB RID: 12795 RVA: 0x0001845F File Offset: 0x0001665F
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x00018468 File Offset: 0x00016668
		// (set) Token: 0x060031FD RID: 12797 RVA: 0x00018470 File Offset: 0x00016670
		[DataMember]
		public string InstructorContactedNote { get; set; }

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x00018479 File Offset: 0x00016679
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x00018481 File Offset: 0x00016681
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x0001848A File Offset: 0x0001668A
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x00018492 File Offset: 0x00016692
		[DataMember]
		public string TestPickedUpNote { get; set; }
	}
}
