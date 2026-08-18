using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003AD RID: 941
	internal sealed class TermExpr<T_Identifier> : BoolExpr<T_Identifier>, IEquatable<TermExpr<T_Identifier>>
	{
		// Token: 0x060033AE RID: 13230 RVA: 0x000C894E File Offset: 0x000C6B4E
		internal TermExpr(IEqualityComparer<T_Identifier> comparer, T_Identifier identifier)
		{
			this._identifier = identifier;
			if (comparer == null)
			{
				this._comparer = EqualityComparer<T_Identifier>.Default;
				return;
			}
			this._comparer = comparer;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000C8973 File Offset: 0x000C6B73
		internal TermExpr(T_Identifier identifier) : this(null, identifier)
		{
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000C897D File Offset: 0x000C6B7D
		internal T_Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Term;
			}
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000C8985 File Offset: 0x000C6B85
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TermExpr<T_Identifier>);
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000C8993 File Offset: 0x000C6B93
		public bool Equals(TermExpr<T_Identifier> other)
		{
			return this._comparer.Equals(this._identifier, other._identifier);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000C89AC File Offset: 0x000C6BAC
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this._comparer.Equals(this._identifier, ((TermExpr<T_Identifier>)other)._identifier);
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000C89CA File Offset: 0x000C6BCA
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode(this._identifier);
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000C89DD File Offset: 0x000C6BDD
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}", new object[]
			{
				this._identifier
			});
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000C89FD File Offset: 0x000C6BFD
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitTerm(this);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000C8A08 File Offset: 0x000C6C08
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			Literal<T_Identifier> literal = new Literal<T_Identifier>(this, true);
			Literal<T_Identifier> literal2 = literal.MakeNegated();
			if (literal2.IsTermPositive)
			{
				return literal2.Term;
			}
			return new NotExpr<T_Identifier>(literal2.Term);
		}

		// Token: 0x04001696 RID: 5782
		private readonly T_Identifier _identifier;

		// Token: 0x04001697 RID: 5783
		private readonly IEqualityComparer<T_Identifier> _comparer;
	}
}
