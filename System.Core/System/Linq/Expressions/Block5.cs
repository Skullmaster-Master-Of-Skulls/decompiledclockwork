using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200021B RID: 539
	internal sealed class Block5 : BlockExpression
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x00043AC6 File Offset: 0x00041CC6
		internal Block5(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00043AF4 File Offset: 0x00041CF4
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
			case 4:
				return this._arg4;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00043B4A File Offset: 0x00041D4A
		internal override int ExpressionCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00043B4D File Offset: 0x00041D4D
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00043B5B File Offset: 0x00041D5B
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block5(args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x0400096B RID: 2411
		private object _arg0;

		// Token: 0x0400096C RID: 2412
		private readonly Expression _arg1;

		// Token: 0x0400096D RID: 2413
		private readonly Expression _arg2;

		// Token: 0x0400096E RID: 2414
		private readonly Expression _arg3;

		// Token: 0x0400096F RID: 2415
		private readonly Expression _arg4;
	}
}
