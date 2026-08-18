using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x0200019F RID: 415
	public class StudentCourseAccommodationRequest : BusinessBase<int>
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00013A0C File Offset: 0x00011C0C
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int StudentCourseAccommodationRequestId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00013A24 File Offset: 0x00011C24
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00013A2C File Offset: 0x00011C2C
		public int LuCourseId { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00013A35 File Offset: 0x00011C35
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00013A3D File Offset: 0x00011C3D
		public PersonBase Student { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00013A46 File Offset: 0x00011C46
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x00013A4E File Offset: 0x00011C4E
		public LookupCourseBaseWithPrimaryInstructor CourseBase { get; set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00013A57 File Offset: 0x00011C57
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00013A5F File Offset: 0x00011C5F
		public eStudentCourseAccommodationRequestStatus Status { get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00013A68 File Offset: 0x00011C68
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00013A70 File Offset: 0x00011C70
		public DateTime? DateRequested { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00013A79 File Offset: 0x00011C79
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00013A81 File Offset: 0x00011C81
		public bool AccommodationChangesRequested { get; set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00013A8A File Offset: 0x00011C8A
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00013A92 File Offset: 0x00011C92
		public bool AdditionalAccommodationsRequested { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00013A9B File Offset: 0x00011C9B
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x00013AA3 File Offset: 0x00011CA3
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00013AAC File Offset: 0x00011CAC
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00013AB4 File Offset: 0x00011CB4
		public DateTime DateEntered { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00013ABD File Offset: 0x00011CBD
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00013AC5 File Offset: 0x00011CC5
		public string Note1 { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00013ACE File Offset: 0x00011CCE
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x00013AD6 File Offset: 0x00011CD6
		public string Note2 { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00013ADF File Offset: 0x00011CDF
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00013AE7 File Offset: 0x00011CE7
		public IList<StudentCourseAccommodationModificationRequestItem> AccommodationModificationRequests { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00013AF0 File Offset: 0x00011CF0
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public PersonBase AssignedAdvisor { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00013B01 File Offset: 0x00011D01
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x00013B09 File Offset: 0x00011D09
		public DateTime? DateApproved { get; set; }
	}
}
