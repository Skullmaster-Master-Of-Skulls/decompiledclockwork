using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000218 RID: 536
	internal sealed class Block2 : BlockExpression
	{
		// Token: 0x060013BA RID: 5050 RVA: 0x00043973 File Offset: 0x00041B73
		internal Block2(Expression arg0, Expression arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00043989 File Offset: 0x00041B89
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return Expression.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw new InvalidOperationException();
			}
			return this._arg1;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000439AC File Offset: 0x00041BAC
		internal override int ExpressionCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000439AF File Offset: 0x00041BAF
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000439BD File Offset: 0x00041BBD
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block2(args[0], args[1]);
		}

		// Token: 0x04000962 RID: 2402
		private object _arg0;

		// Token: 0x04000963 RID: 2403
		private readonly Expression _arg1;
	}
}
