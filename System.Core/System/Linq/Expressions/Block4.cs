using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200021A RID: 538
	internal sealed class Block4 : BlockExpression
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x00043A3D File Offset: 0x00041C3D
		internal Block4(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00043A62 File Offset: 0x00041C62
		internal override Expression GetExpression(int index)
		{
			switch (index)
			{
			case 0:
				return Expression.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			case 3:
				return this._arg3;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00043AA2 File Offset: 0x00041CA2
		internal override int ExpressionCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00043AA5 File Offset: 0x00041CA5
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00043AB3 File Offset: 0x00041CB3
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block4(args[0], args[1], args[2], args[3]);
		}

		// Token: 0x04000967 RID: 2407
		private object _arg0;

		// Token: 0x04000968 RID: 2408
		private readonly Expression _arg1;

		// Token: 0x04000969 RID: 2409
		private readonly Expression _arg2;

		// Token: 0x0400096A RID: 2410
		private readonly Expression _arg3;
	}
}
