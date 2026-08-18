using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000313 RID: 787
	internal sealed class Literal<T_Identifier> : NormalFormNode<T_Identifier>, IEquatable<Literal<T_Identifier>>
	{
		// Token: 0x06001B4D RID: 6989 RVA: 0x0008772C File Offset: 0x0008592C
		internal Literal(TermExpr<T_Identifier> term, bool isTermPositive) : base(isTermPositive ? term : new NotExpr<T_Identifier>(term))
		{
			this._term = term;
			this._isTermPositive = isTermPositive;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0008774E File Offset: 0x0008594E
		internal TermExpr<T_Identifier> Term
		{
			get
			{
				return this._term;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00087756 File Offset: 0x00085956
		internal bool IsTermPositive
		{
			get
			{
				return this._isTermPositive;
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0008775E File Offset: 0x0008595E
		internal Literal<T_Identifier> MakeNegated()
		{
			return IdentifierService<T_Identifier>.Instance.NegateLiteral(this);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0008776C File Offset: 0x0008596C
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}{1}", new object[]
			{
				this._isTermPositive ? string.Empty : "!",
				this._term
			});
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000877AB File Offset: 0x000859AB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Literal<T_Identifier>);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000877B9 File Offset: 0x000859B9
		public bool Equals(Literal<T_Identifier> other)
		{
			return other != null && other._isTermPositive == this._isTermPositive && other._term.Equals(this._term);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000877DF File Offset: 0x000859DF
		public override int GetHashCode()
		{
			return this._term.GetHashCode();
		}

		// Token: 0x0400099B RID: 2459
		private readonly TermExpr<T_Identifier> _term;

		// Token: 0x0400099C RID: 2460
		private readonly bool _isTermPositive;
	}
}
