using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000219 RID: 537
	internal sealed class Block3 : BlockExpression
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x000439CA File Offset: 0x00041BCA
		internal Block3(Expression arg0, Expression arg1, Expression arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000439E7 File Offset: 0x00041BE7
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
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00043A1C File Offset: 0x00041C1C
		internal override int ExpressionCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00043A1F File Offset: 0x00041C1F
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00043A2D File Offset: 0x00041C2D
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block3(args[0], args[1], args[2]);
		}

		// Token: 0x04000964 RID: 2404
		private object _arg0;

		// Token: 0x04000965 RID: 2405
		private readonly Expression _arg1;

		// Token: 0x04000966 RID: 2406
		private readonly Expression _arg2;
	}
}
