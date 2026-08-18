using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000210 RID: 528
	internal sealed class LogicalBinaryExpression : BinaryExpression
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x0003C79F File Offset: 0x0003A99F
		internal LogicalBinaryExpression(ExpressionType nodeType, Expression left, Expression right) : base(left, right)
		{
			this._nodeType = nodeType;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0003C7B0 File Offset: 0x0003A9B0
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0003C7BC File Offset: 0x0003A9BC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return this._nodeType;
			}
		}

		// Token: 0x04000959 RID: 2393
		private readonly ExpressionType _nodeType;
	}
}
