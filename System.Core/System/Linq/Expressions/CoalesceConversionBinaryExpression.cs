using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000212 RID: 530
	internal sealed class CoalesceConversionBinaryExpression : BinaryExpression
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x0003C7DF File Offset: 0x0003A9DF
		internal CoalesceConversionBinaryExpression(Expression left, Expression right, LambdaExpression conversion) : base(left, right)
		{
			this._conversion = conversion;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0003C7F8 File Offset: 0x0003A9F8
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Coalesce;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0003C7FB File Offset: 0x0003A9FB
		public sealed override Type Type
		{
			get
			{
				return base.Right.Type;
			}
		}

		// Token: 0x0400095A RID: 2394
		private readonly LambdaExpression _conversion;
	}
}
