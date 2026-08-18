using System;

namespace WebGrease
{
	// Token: 0x020000E8 RID: 232
	public class CacheVaryByFile
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x00046537 File Offset: 0x00044737
		private CacheVaryByFile()
		{
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0004653F File Offset: 0x0004473F
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00046547 File Offset: 0x00044747
		public string Hash { get; private set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00046550 File Offset: 0x00044750
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00046558 File Offset: 0x00044758
		public string Path { get; private set; }

		// Token: 0x06000F1E RID: 3870 RVA: 0x00046564 File Offset: 0x00044764
		public static CacheVaryByFile FromFile(IWebGreaseContext context, ContentItem contentItem)
		{
			return new CacheVaryByFile
			{
				Path = contentItem.RelativeContentPath,
				Hash = contentItem.GetContentHash(context)
			};
		}
	}
}
