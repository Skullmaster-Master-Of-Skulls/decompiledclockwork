using System;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200031A RID: 794
	internal sealed class NotExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x06001B6B RID: 7019 RVA: 0x00087920 File Offset: 0x00085B20
		internal NotExpr(BoolExpr<T_Identifier> child) : base(new BoolExpr<T_Identifier>[]
		{
			child
		})
		{
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0008793F File Offset: 0x00085B3F
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Not;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00087942 File Offset: 0x00085B42
		internal BoolExpr<T_Identifier> Child
		{
			get
			{
				return base.Children.First<BoolExpr<T_Identifier>>();
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0008794F File Offset: 0x00085B4F
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitNot(this);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00087958 File Offset: 0x00085B58
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "!{0}", new object[]
			{
				this.Child
			});
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00087985 File Offset: 0x00085B85
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return this.Child;
		}
	}
}
