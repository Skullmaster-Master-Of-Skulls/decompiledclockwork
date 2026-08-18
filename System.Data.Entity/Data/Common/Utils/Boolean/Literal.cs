using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B7 RID: 951
	internal sealed class Literal<T_Identifier> : NormalFormNode<T_Identifier>, IEquatable<Literal<T_Identifier>>
	{
		// Token: 0x060033DE RID: 13278 RVA: 0x000C8DB8 File Offset: 0x000C6FB8
		internal Literal(TermExpr<T_Identifier> term, bool isTermPositive) : base(isTermPositive ? term : new NotExpr<T_Identifier>(term))
		{
			this._term = term;
			this._isTermPositive = isTermPositive;
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000C8DDA File Offset: 0x000C6FDA
		internal TermExpr<T_Identifier> Term
		{
			get
			{
				return this._term;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000C8DE2 File Offset: 0x000C6FE2
		internal bool IsTermPositive
		{
			get
			{
				return this._isTermPositive;
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000C8DEA File Offset: 0x000C6FEA
		internal Literal<T_Identifier> MakeNegated()
		{
			return IdentifierService<T_Identifier>.Instance.NegateLiteral(this);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000C8DF7 File Offset: 0x000C6FF7
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}{1}", new object[]
			{
				this._isTermPositive ? string.Empty : "!",
				this._term
			});
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000C8E29 File Offset: 0x000C7029
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Literal<T_Identifier>);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000C8E37 File Offset: 0x000C7037
		public bool Equals(Literal<T_Identifier> other)
		{
			return other != null && other._isTermPositive == this._isTermPositive && other._term.Equals(this._term);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000C8E5D File Offset: 0x000C705D
		public override int GetHashCode()
		{
			return this._term.GetHashCode();
		}

		// Token: 0x040016A6 RID: 5798
		private readonly TermExpr<T_Identifier> _term;

		// Token: 0x040016A7 RID: 5799
		private readonly bool _isTermPositive;
	}
}
