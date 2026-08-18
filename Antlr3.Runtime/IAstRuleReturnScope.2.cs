using System;

namespace Antlr.Runtime
{
	// Token: 0x0200000C RID: 12
	public interface IAstRuleReturnScope<TAstLabel> : IAstRuleReturnScope, IRuleReturnScope
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66
		TAstLabel Tree { get; }
	}
}
