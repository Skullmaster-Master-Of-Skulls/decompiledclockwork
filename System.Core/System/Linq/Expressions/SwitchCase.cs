using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000268 RID: 616
	[DebuggerTypeProxy(typeof(Expression.SwitchCaseProxy))]
	[__DynamicallyInvokable]
	public sealed class SwitchCase
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x000493C6 File Offset: 0x000475C6
		internal SwitchCase(Expression body, ReadOnlyCollection<Expression> testValues)
		{
			this._body = body;
			this._testValues = testValues;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x000493DC File Offset: 0x000475DC
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> TestValues
		{
			[__DynamicallyInvokable]
			get
			{
				return this._testValues;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x000493E4 File Offset: 0x000475E4
		[__DynamicallyInvokable]
		public Expression Body
		{
			[__DynamicallyInvokable]
			get
			{
				return this._body;
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000493EC File Offset: 0x000475EC
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return ExpressionStringBuilder.SwitchCaseToString(this);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000493F4 File Offset: 0x000475F4
		[__DynamicallyInvokable]
		public SwitchCase Update(IEnumerable<Expression> testValues, Expression body)
		{
			if (testValues == this.TestValues && body == this.Body)
			{
				return this;
			}
			return Expression.SwitchCase(body, testValues);
		}

		// Token: 0x04000A4E RID: 2638
		private readonly ReadOnlyCollection<Expression> _testValues;

		// Token: 0x04000A4F RID: 2639
		private readonly Expression _body;
	}
}
