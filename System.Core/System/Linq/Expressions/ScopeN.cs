using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200021F RID: 543
	internal class ScopeN : ScopeExpression
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x00043C58 File Offset: 0x00041E58
		internal ScopeN(IList<ParameterExpression> variables, IList<Expression> body) : base(variables)
		{
			this._body = body;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00043C68 File Offset: 0x00041E68
		internal override Expression GetExpression(int index)
		{
			return this._body[index];
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00043C76 File Offset: 0x00041E76
		internal override int ExpressionCount
		{
			get
			{
				return this._body.Count;
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00043C83 File Offset: 0x00041E83
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return Expression.ReturnReadOnly<Expression>(ref this._body);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00043C90 File Offset: 0x00041E90
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new ScopeN(base.ReuseOrValidateVariables(variables), args);
		}

		// Token: 0x04000973 RID: 2419
		private IList<Expression> _body;
	}
}
