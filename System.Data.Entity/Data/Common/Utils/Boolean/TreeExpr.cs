using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003AE RID: 942
	internal abstract class TreeExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x060033B9 RID: 13241 RVA: 0x000C8A3E File Offset: 0x000C6C3E
		protected TreeExpr(IEnumerable<BoolExpr<T_Identifier>> children)
		{
			this._children = new Set<BoolExpr<T_Identifier>>(children);
			this._children.MakeReadOnly();
			this._hashCode = this._children.GetElementsHashCode();
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000C8A6F File Offset: 0x000C6C6F
		internal Set<BoolExpr<T_Identifier>> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000C8A77 File Offset: 0x000C6C77
		public override bool Equals(object obj)
		{
			return base.Equals(obj as BoolExpr<T_Identifier>);
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000C8A85 File Offset: 0x000C6C85
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000C8A8D File Offset: 0x000C6C8D
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}({1})", new object[]
			{
				this.ExprType,
				this._children
			});
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000C8AB6 File Offset: 0x000C6CB6
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return ((TreeExpr<T_Identifier>)other).Children.SetEquals(this.Children);
		}

		// Token: 0x04001698 RID: 5784
		private readonly Set<BoolExpr<T_Identifier>> _children;

		// Token: 0x04001699 RID: 5785
		private readonly int _hashCode;
	}
}
