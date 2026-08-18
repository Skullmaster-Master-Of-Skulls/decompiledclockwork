using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000A48 RID: 2632
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnbookedStudentsSmallDTO
	{
		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x0001AE0F File Offset: 0x0001900F
		// (set) Token: 0x06003740 RID: 14144 RVA: 0x0001AE17 File Offset: 0x00019017
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x0001AE20 File Offset: 0x00019020
		// (set) Token: 0x06003742 RID: 14146 RVA: 0x0001AE28 File Offset: 0x00019028
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x0001AE31 File Offset: 0x00019031
		// (set) Token: 0x06003744 RID: 14148 RVA: 0x0001AE39 File Offset: 0x00019039
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x0001AE42 File Offset: 0x00019042
		// (set) Token: 0x06003746 RID: 14150 RVA: 0x0001AE4A File Offset: 0x0001904A
		[DataMember]
		public string ValText { get; set; }

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x0001AE53 File Offset: 0x00019053
		// (set) Token: 0x06003748 RID: 14152 RVA: 0x0001AE5B File Offset: 0x0001905B
		[DataMember]
		public int TestDuration { get; set; }

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x0001AE64 File Offset: 0x00019064
		// (set) Token: 0x0600374A RID: 14154 RVA: 0x0001AE6C File Offset: 0x0001906C
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x0001AE75 File Offset: 0x00019075
		// (set) Token: 0x0600374C RID: 14156 RVA: 0x0001AE7D File Offset: 0x0001907D
		[DataMember]
		public DateTime DateOfTest { get; set; }

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x0001AE86 File Offset: 0x00019086
		// (set) Token: 0x0600374E RID: 14158 RVA: 0x0001AE8E File Offset: 0x0001908E
		[DataMember]
		public DateTime TestStartTime { get; set; }

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x0001AE97 File Offset: 0x00019097
		// (set) Token: 0x06003750 RID: 14160 RVA: 0x0001AE9F File Offset: 0x0001909F
		[DataMember]
		public DateTime TestEndTime { get; set; }

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x0001AEA8 File Offset: 0x000190A8
		// (set) Token: 0x06003752 RID: 14162 RVA: 0x0001AEB0 File Offset: 0x000190B0
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x0001AEB9 File Offset: 0x000190B9
		// (set) Token: 0x06003754 RID: 14164 RVA: 0x0001AEC1 File Offset: 0x000190C1
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x0001AECA File Offset: 0x000190CA
		// (set) Token: 0x06003756 RID: 14166 RVA: 0x0001AED2 File Offset: 0x000190D2
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x0001AEDB File Offset: 0x000190DB
		// (set) Token: 0x06003758 RID: 14168 RVA: 0x0001AEE3 File Offset: 0x000190E3
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x0001AEEC File Offset: 0x000190EC
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x0001AEF4 File Offset: 0x000190F4
		[DataMember]
		public string StudentEmail { get; set; }
	}
}
