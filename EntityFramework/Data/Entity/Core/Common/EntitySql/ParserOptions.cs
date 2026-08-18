using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000265 RID: 613
	internal sealed class ParserOptions
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x000630F8 File Offset: 0x000612F8
		internal StringComparer NameComparer
		{
			get
			{
				if (!this.NameComparisonCaseInsensitive)
				{
					return StringComparer.Ordinal;
				}
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0006310D File Offset: 0x0006130D
		internal bool NameComparisonCaseInsensitive
		{
			get
			{
				return this.ParserCompilationMode != ParserOptions.CompilationMode.RestrictedViewGenerationMode;
			}
		}

		// Token: 0x04000748 RID: 1864
		internal ParserOptions.CompilationMode ParserCompilationMode;

		// Token: 0x02000266 RID: 614
		internal enum CompilationMode
		{
			// Token: 0x0400074A RID: 1866
			NormalMode,
			// Token: 0x0400074B RID: 1867
			RestrictedViewGenerationMode,
			// Token: 0x0400074C RID: 1868
			UserViewGenerationMode
		}
	}
}
