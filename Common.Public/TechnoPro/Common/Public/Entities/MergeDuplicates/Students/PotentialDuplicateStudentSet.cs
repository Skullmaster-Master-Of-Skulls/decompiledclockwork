using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Students
{
	// Token: 0x02000292 RID: 658
	public class PotentialDuplicateStudentSet
	{
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00019B23 File Offset: 0x00017D23
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x00019B2B File Offset: 0x00017D2B
		public PersonBase Student1 { get; set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00019B34 File Offset: 0x00017D34
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x00019B3C File Offset: 0x00017D3C
		public PersonBase Student2 { get; set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00019B45 File Offset: 0x00017D45
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x00019B4D File Offset: 0x00017D4D
		public int EditDistance { get; set; }
	}
}
