using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x020006FB RID: 1787
	internal class OlapConstantExpression : OlapExpression
	{
		// Token: 0x06003F89 RID: 16265 RVA: 0x000C963C File Offset: 0x000C783C
		internal OlapConstantExpression(object value)
		{
			this.value = value;
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000C964B File Offset: 0x000C784B
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000C9653 File Offset: 0x000C7853
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.Constant;
			}
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000C9656 File Offset: 0x000C7856
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitConstant(this);
		}

		// Token: 0x040010CC RID: 4300
		private readonly object value;
	}
}
