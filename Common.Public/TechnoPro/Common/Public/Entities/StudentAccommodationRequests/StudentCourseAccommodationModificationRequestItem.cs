using System;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x0200019E RID: 414
	public class StudentCourseAccommodationModificationRequestItem : BusinessBase<int>
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0001397C File Offset: 0x00011B7C
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int StudentCourseAccommodationModificationRequestItemId
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

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00013994 File Offset: 0x00011B94
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0001399C File Offset: 0x00011B9C
		public DynamicData RequestedAccommodationData { get; set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000139A5 File Offset: 0x00011BA5
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x000139AD File Offset: 0x00011BAD
		public eStudentCourseAccommodationModificationType ModificationType { get; set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000139B6 File Offset: 0x00011BB6
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x000139BE File Offset: 0x00011BBE
		public string Note1 { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000139C7 File Offset: 0x00011BC7
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x000139CF File Offset: 0x00011BCF
		public string Note2 { get; set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000139D8 File Offset: 0x00011BD8
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x000139E0 File Offset: 0x00011BE0
		public PersonBase WhoEntered { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000139E9 File Offset: 0x00011BE9
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x000139F1 File Offset: 0x00011BF1
		public DateTime DateEntered { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000139FA File Offset: 0x00011BFA
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00013A02 File Offset: 0x00011C02
		public eStudentCourseAccommodationRequestStatus Status { get; set; }
	}
}
