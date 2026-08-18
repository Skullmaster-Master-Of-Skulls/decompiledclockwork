using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000AF RID: 175
	public class LabelInfo
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00036C12 File Offset: 0x00034E12
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x00036C1A File Offset: 0x00034E1A
		public int RefCount { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00036C23 File Offset: 0x00034E23
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00036C2B File Offset: 0x00034E2B
		public int NestLevel { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00036C34 File Offset: 0x00034E34
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00036C3C File Offset: 0x00034E3C
		public string MinLabel { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00036C45 File Offset: 0x00034E45
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x00036C4D File Offset: 0x00034E4D
		public bool HasIssues { get; set; }
	}
}
