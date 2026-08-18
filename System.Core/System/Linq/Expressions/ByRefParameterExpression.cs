using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000264 RID: 612
	internal sealed class ByRefParameterExpression : TypedParameterExpression
	{
		// Token: 0x0600160C RID: 5644 RVA: 0x00049349 File Offset: 0x00047549
		internal ByRefParameterExpression(Type type, string name) : base(type, name)
		{
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00049353 File Offset: 0x00047553
		internal override bool GetIsByRef()
		{
			return true;
		}
	}
}
