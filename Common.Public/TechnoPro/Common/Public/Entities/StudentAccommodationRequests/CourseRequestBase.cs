using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x0200019A RID: 410
	public class CourseRequestBase : BusinessBase<int>
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00013884 File Offset: 0x00011A84
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CoursesId
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0001389C File Offset: 0x00011A9C
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x000138A4 File Offset: 0x00011AA4
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000138AD File Offset: 0x00011AAD
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x000138B5 File Offset: 0x00011AB5
		public eStudentCourseAccommodationRequestStatus Status { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000138BE File Offset: 0x00011ABE
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x000138C6 File Offset: 0x00011AC6
		public DateTime? DateRequested { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000138CF File Offset: 0x00011ACF
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x000138D7 File Offset: 0x00011AD7
		public PersonBase WhoEntered { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000138E0 File Offset: 0x00011AE0
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x000138E8 File Offset: 0x00011AE8
		public DateTime DateEntered { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000138F1 File Offset: 0x00011AF1
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x000138F9 File Offset: 0x00011AF9
		public string Note1 { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00013902 File Offset: 0x00011B02
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0001390A File Offset: 0x00011B0A
		public string Note2 { get; set; }
	}
}
