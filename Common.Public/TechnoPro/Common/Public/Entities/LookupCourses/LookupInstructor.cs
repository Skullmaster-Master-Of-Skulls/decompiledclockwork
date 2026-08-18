using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002ED RID: 749
	[Serializable]
	public class LookupInstructor : BusinessBase<int>
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0001BE08 File Offset: 0x0001A008
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int InstructorId
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

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0001BE20 File Offset: 0x0001A020
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x0001BE28 File Offset: 0x0001A028
		public string Name { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0001BE31 File Offset: 0x0001A031
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x0001BE39 File Offset: 0x0001A039
		public string Username { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0001BE42 File Offset: 0x0001A042
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x0001BE4A File Offset: 0x0001A04A
		public string Email { get; set; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0001BE53 File Offset: 0x0001A053
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x0001BE5B File Offset: 0x0001A05B
		public string Phone { get; set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0001BE64 File Offset: 0x0001A064
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0001BE6C File Offset: 0x0001A06C
		public string EmployeeId { get; set; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0001BE75 File Offset: 0x0001A075
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0001BE7D File Offset: 0x0001A07D
		public string ExternalId { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0001BE86 File Offset: 0x0001A086
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x0001BE8E File Offset: 0x0001A08E
		public bool IsPrimary { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0001BE97 File Offset: 0x0001A097
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x0001BE9F File Offset: 0x0001A09F
		public int Percentage { get; set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0001BEB9 File Offset: 0x0001A0B9
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x0001BEC1 File Offset: 0x0001A0C1
		public bool IsExemptAssignmentFromDataSync { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0001BECA File Offset: 0x0001A0CA
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0001BED2 File Offset: 0x0001A0D2
		public LookupInstructorCourseInfo CourseSpecificInfo { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0001BEDB File Offset: 0x0001A0DB
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0001BEE3 File Offset: 0x0001A0E3
		public ePermissionForCourse PermissionLevel { get; set; }
	}
}
