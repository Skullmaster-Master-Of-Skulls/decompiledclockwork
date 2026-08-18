using System;

namespace Antlr.Runtime
{
	// Token: 0x02000022 RID: 34
	public interface ITokenSource
	{
		// Token: 0x06000184 RID: 388
		IToken NextToken();

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000185 RID: 389
		string SourceName { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000186 RID: 390
		string[] TokenNames { get; }
	}
}
