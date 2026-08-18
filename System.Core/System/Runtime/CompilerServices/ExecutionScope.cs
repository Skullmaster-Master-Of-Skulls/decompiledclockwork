using System;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000138 RID: 312
	[Obsolete("do not use this type", true)]
	public class ExecutionScope
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x00024947 File Offset: 0x00022B47
		internal ExecutionScope()
		{
			this.Parent = null;
			this.Globals = null;
			this.Locals = null;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00024964 File Offset: 0x00022B64
		public object[] CreateHoistedLocals()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002496B File Offset: 0x00022B6B
		public Delegate CreateDelegate(int indexLambda, object[] locals)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00024972 File Offset: 0x00022B72
		public Expression IsolateExpression(Expression expression, object[] locals)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400075E RID: 1886
		public ExecutionScope Parent;

		// Token: 0x0400075F RID: 1887
		public object[] Globals;

		// Token: 0x04000760 RID: 1888
		public object[] Locals;
	}
}
