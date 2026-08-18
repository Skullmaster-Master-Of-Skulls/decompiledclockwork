using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000222 RID: 546
	[DebuggerTypeProxy(typeof(Expression.CatchBlockProxy))]
	[__DynamicallyInvokable]
	public sealed class CatchBlock
	{
		// Token: 0x060013F5 RID: 5109 RVA: 0x00043DEA File Offset: 0x00041FEA
		internal CatchBlock(Type test, ParameterExpression variable, Expression body, Expression filter)
		{
			this._test = test;
			this._var = variable;
			this._body = body;
			this._filter = filter;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00043E0F File Offset: 0x0004200F
		[__DynamicallyInvokable]
		public ParameterExpression Variable
		{
			[__DynamicallyInvokable]
			get
			{
				return this._var;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00043E17 File Offset: 0x00042017
		[__DynamicallyInvokable]
		public Type Test
		{
			[__DynamicallyInvokable]
			get
			{
				return this._test;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00043E1F File Offset: 0x0004201F
		[__DynamicallyInvokable]
		public Expression Body
		{
			[__DynamicallyInvokable]
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00043E27 File Offset: 0x00042027
		[__DynamicallyInvokable]
		public Expression Filter
		{
			[__DynamicallyInvokable]
			get
			{
				return this._filter;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00043E2F File Offset: 0x0004202F
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return ExpressionStringBuilder.CatchBlockToString(this);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00043E37 File Offset: 0x00042037
		[__DynamicallyInvokable]
		public CatchBlock Update(ParameterExpression variable, Expression filter, Expression body)
		{
			if (variable == this.Variable && filter == this.Filter && body == this.Body)
			{
				return this;
			}
			return Expression.MakeCatchBlock(this.Test, variable, body, filter);
		}

		// Token: 0x04000977 RID: 2423
		private readonly Type _test;

		// Token: 0x04000978 RID: 2424
		private readonly ParameterExpression _var;

		// Token: 0x04000979 RID: 2425
		private readonly Expression _body;

		// Token: 0x0400097A RID: 2426
		private readonly Expression _filter;
	}
}
