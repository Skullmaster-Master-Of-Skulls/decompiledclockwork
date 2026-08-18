using System;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000333 RID: 819
	internal sealed class ParserOptions
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000BC360 File Offset: 0x000BA560
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

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000BC375 File Offset: 0x000BA575
		internal bool NameComparisonCaseInsensitive
		{
			get
			{
				return this.ParserCompilationMode != ParserOptions.CompilationMode.RestrictedViewGenerationMode;
			}
		}

		// Token: 0x0400153F RID: 5439
		internal ParserOptions.CompilationMode ParserCompilationMode;

		// Token: 0x0200064D RID: 1613
		internal enum CompilationMode
		{
			// Token: 0x04001EEC RID: 7916
			NormalMode,
			// Token: 0x04001EED RID: 7917
			RestrictedViewGenerationMode,
			// Token: 0x04001EEE RID: 7918
			UserViewGenerationMode
		}
	}
}
