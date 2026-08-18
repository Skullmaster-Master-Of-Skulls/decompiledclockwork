using System;

namespace Antlr.Runtime
{
	// Token: 0x02000010 RID: 16
	public interface ITokenStream : IIntStream
	{
		// Token: 0x0600009F RID: 159
		IToken LT(int k);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A0 RID: 160
		int Range { get; }

		// Token: 0x060000A1 RID: 161
		IToken Get(int i);

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A2 RID: 162
		ITokenSource TokenSource { get; }

		// Token: 0x060000A3 RID: 163
		string ToString(int start, int stop);

		// Token: 0x060000A4 RID: 164
		string ToString(IToken start, IToken stop);
	}
}
