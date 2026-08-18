using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x020006FA RID: 1786
	internal class OlapBinaryExpression : OlapExpression
	{
		// Token: 0x06003F83 RID: 16259 RVA: 0x000C95DF File Offset: 0x000C77DF
		internal OlapBinaryExpression(OlapExpression left, OlapExpression right, OlapExpressionOperator expressionOperator)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			this.leftField = left;
			this.rightField = right;
			this.expressionOperator = expressionOperator;
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000C9618 File Offset: 0x000C7818
		public OlapExpression Left
		{
			get
			{
				return this.leftField;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x000C9620 File Offset: 0x000C7820
		public OlapExpression Right
		{
			get
			{
				return this.rightField;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000C9628 File Offset: 0x000C7828
		public OlapExpressionOperator Operator
		{
			get
			{
				return this.expressionOperator;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000C9630 File Offset: 0x000C7830
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.Binary;
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000C9633 File Offset: 0x000C7833
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitBinary(this);
		}

		// Token: 0x040010C9 RID: 4297
		private readonly OlapExpression leftField;

		// Token: 0x040010CA RID: 4298
		private readonly OlapExpression rightField;

		// Token: 0x040010CB RID: 4299
		private readonly OlapExpressionOperator expressionOperator;
	}
}
