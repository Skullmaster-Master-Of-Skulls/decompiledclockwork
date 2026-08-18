using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x02000185 RID: 389
	public class StudentFileStatusTypeAttribute : Attribute
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public StudentFileStatusTypeAttribute()
		{
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00012EB5 File Offset: 0x000110B5
		public StudentFileStatusTypeAttribute(string postFix)
		{
			this.PostFix = postFix;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00012EC7 File Offset: 0x000110C7
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00012ECF File Offset: 0x000110CF
		public string PostFix { get; set; }
	}
}
