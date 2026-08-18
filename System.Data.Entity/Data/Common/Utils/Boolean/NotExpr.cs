using System;
using System.Globalization;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B1 RID: 945
	internal sealed class NotExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x060033C7 RID: 13255 RVA: 0x000C8B04 File Offset: 0x000C6D04
		internal NotExpr(BoolExpr<T_Identifier> child) : base(new BoolExpr<T_Identifier>[]
		{
			child
		})
		{
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x00017938 File Offset: 0x00015B38
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Not;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000C8B16 File Offset: 0x000C6D16
		internal BoolExpr<T_Identifier> Child
		{
			get
			{
				return base.Children.First<BoolExpr<T_Identifier>>();
			}
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000C8B23 File Offset: 0x000C6D23
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitNot(this);
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000C8B2C File Offset: 0x000C6D2C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "!{0}", new object[]
			{
				this.Child
			});
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000C8B4C File Offset: 0x000C6D4C
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return this.Child;
		}
	}
}
