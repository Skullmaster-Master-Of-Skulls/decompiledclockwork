using System;

namespace Antlr.Runtime
{
	// Token: 0x02000011 RID: 17
	public interface ITokenStreamInformation
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A5 RID: 165
		IToken LastToken { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166
		IToken LastRealToken { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A7 RID: 167
		int MaxLookBehind { get; }
	}
}
