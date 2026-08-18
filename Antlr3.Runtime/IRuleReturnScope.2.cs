using System;

namespace Antlr.Runtime
{
	// Token: 0x02000009 RID: 9
	public interface IRuleReturnScope<TLabel> : IRuleReturnScope
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56
		TLabel Start { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57
		TLabel Stop { get; }
	}
}
