using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200021E RID: 542
	internal sealed class Scope1 : ScopeExpression
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x00043C10 File Offset: 0x00041E10
		internal Scope1(IList<ParameterExpression> variables, Expression body) : base(variables)
		{
			this._body = body;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00043C20 File Offset: 0x00041E20
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return Expression.ReturnObject<Expression>(this._body);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00043C36 File Offset: 0x00041E36
		internal override int ExpressionCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00043C39 File Offset: 0x00041E39
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._body);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00043C47 File Offset: 0x00041E47
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Scope1(base.ReuseOrValidateVariables(variables), args[0]);
		}

		// Token: 0x04000972 RID: 2418
		private object _body;
	}
}
