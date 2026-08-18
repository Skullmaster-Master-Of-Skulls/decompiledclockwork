using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001E1 RID: 481
	internal abstract class TreeExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x00048541 File Offset: 0x00046741
		protected TreeExpr(IEnumerable<BoolExpr<T_Identifier>> children)
		{
			this._children = new Set<BoolExpr<T_Identifier>>(children);
			this._children.MakeReadOnly();
			this._hashCode = this._children.GetElementsHashCode();
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00048572 File Offset: 0x00046772
		internal Set<BoolExpr<T_Identifier>> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0004857A File Offset: 0x0004677A
		public override bool Equals(object obj)
		{
			return base.Equals(obj as BoolExpr<T_Identifier>);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00048588 File Offset: 0x00046788
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00048590 File Offset: 0x00046790
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}({1})", new object[]
			{
				this.ExprType,
				this._children
			});
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000485C6 File Offset: 0x000467C6
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return ((TreeExpr<T_Identifier>)other).Children.SetEquals(this.Children);
		}

		// Token: 0x04000514 RID: 1300
		private readonly Set<BoolExpr<T_Identifier>> _children;

		// Token: 0x04000515 RID: 1301
		private readonly int _hashCode;
	}
}
