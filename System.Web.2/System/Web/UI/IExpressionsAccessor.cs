using System;

namespace System.Web.UI
{
	// Token: 0x020002A4 RID: 676
	public interface IExpressionsAccessor
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001F94 RID: 8084
		bool HasExpressions { get; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001F95 RID: 8085
		ExpressionBindingCollection Expressions { get; }
	}
}
