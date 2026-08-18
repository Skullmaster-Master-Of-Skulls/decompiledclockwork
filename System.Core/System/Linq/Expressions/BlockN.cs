using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200021C RID: 540
	internal class BlockN : BlockExpression
	{
		// Token: 0x060013CE RID: 5070 RVA: 0x00043B71 File Offset: 0x00041D71
		internal BlockN(IList<Expression> expressions)
		{
			this._expressions = expressions;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00043B80 File Offset: 0x00041D80
		internal override Expression GetExpression(int index)
		{
			return this._expressions[index];
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00043B8E File Offset: 0x00041D8E
		internal override int ExpressionCount
		{
			get
			{
				return this._expressions.Count;
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00043B9B File Offset: 0x00041D9B
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return Expression.ReturnReadOnly<Expression>(ref this._expressions);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00043BA8 File Offset: 0x00041DA8
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new BlockN(args);
		}

		// Token: 0x04000970 RID: 2416
		private IList<Expression> _expressions;
	}
}
