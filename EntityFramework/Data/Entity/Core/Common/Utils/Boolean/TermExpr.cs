using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200031F RID: 799
	internal sealed class TermExpr<T_Identifier> : BoolExpr<T_Identifier>, IEquatable<TermExpr<T_Identifier>>
	{
		// Token: 0x06001B91 RID: 7057 RVA: 0x00088117 File Offset: 0x00086317
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

		// Token: 0x06001B92 RID: 7058 RVA: 0x0008813C File Offset: 0x0008633C
		internal TermExpr(T_Identifier identifier) : this(null, identifier)
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00088146 File Offset: 0x00086346
		internal T_Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0008814E File Offset: 0x0008634E
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Term;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00088151 File Offset: 0x00086351
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TermExpr<T_Identifier>);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0008815F File Offset: 0x0008635F
		public bool Equals(TermExpr<T_Identifier> other)
		{
			return this._comparer.Equals(this._identifier, other._identifier);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00088178 File Offset: 0x00086378
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this._comparer.Equals(this._identifier, ((TermExpr<T_Identifier>)other)._identifier);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00088196 File Offset: 0x00086396
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode(this._identifier);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000881AC File Offset: 0x000863AC
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}", new object[]
			{
				this._identifier
			});
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000881D9 File Offset: 0x000863D9
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitTerm(this);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000881E4 File Offset: 0x000863E4
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

		// Token: 0x040009AE RID: 2478
		private readonly T_Identifier _identifier;

		// Token: 0x040009AF RID: 2479
		private readonly IEqualityComparer<T_Identifier> _comparer;
	}
}
