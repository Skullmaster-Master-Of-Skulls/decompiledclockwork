using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000301 RID: 769
	internal abstract class Clause<T_Identifier> : NormalFormNode<T_Identifier>
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x000868F9 File Offset: 0x00084AF9
		protected Clause(Set<Literal<T_Identifier>> literals, ExprType treeType) : base(Clause<T_Identifier>.ConvertLiteralsToExpr(literals, treeType))
		{
			this._literals = literals.AsReadOnly();
			this._hashCode = this._literals.GetElementsHashCode();
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00086925 File Offset: 0x00084B25
		internal Set<Literal<T_Identifier>> Literals
		{
			get
			{
				return this._literals;
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00086930 File Offset: 0x00084B30
		private static BoolExpr<T_Identifier> ConvertLiteralsToExpr(Set<Literal<T_Identifier>> literals, ExprType treeType)
		{
			bool flag = ExprType.And == treeType;
			IEnumerable<BoolExpr<T_Identifier>> children = literals.Select(new Func<Literal<T_Identifier>, BoolExpr<T_Identifier>>(Clause<T_Identifier>.ConvertLiteralToExpression));
			if (flag)
			{
				return new AndExpr<T_Identifier>(children);
			}
			return new OrExpr<T_Identifier>(children);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00086965 File Offset: 0x00084B65
		private static BoolExpr<T_Identifier> ConvertLiteralToExpression(Literal<T_Identifier> literal)
		{
			return literal.Expr;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00086970 File Offset: 0x00084B70
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Clause{");
			stringBuilder.Append(this._literals);
			return stringBuilder.Append("}").ToString();
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000869AC File Offset: 0x00084BAC
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000869B4 File Offset: 0x00084BB4
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x04000978 RID: 2424
		private readonly Set<Literal<T_Identifier>> _literals;

		// Token: 0x04000979 RID: 2425
		private readonly int _hashCode;
	}
}
