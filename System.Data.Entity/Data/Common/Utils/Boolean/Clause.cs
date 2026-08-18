using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B3 RID: 947
	internal abstract class Clause<T_Identifier> : NormalFormNode<T_Identifier>
	{
		// Token: 0x060033CD RID: 13261 RVA: 0x000C8B54 File Offset: 0x000C6D54
		protected Clause(Set<Literal<T_Identifier>> literals, ExprType treeType) : base(Clause<T_Identifier>.ConvertLiteralsToExpr(literals, treeType))
		{
			this._literals = literals.AsReadOnly();
			this._hashCode = this._literals.GetElementsHashCode();
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000C8B80 File Offset: 0x000C6D80
		internal Set<Literal<T_Identifier>> Literals
		{
			get
			{
				return this._literals;
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000C8B88 File Offset: 0x000C6D88
		private static BoolExpr<T_Identifier> ConvertLiteralsToExpr(Set<Literal<T_Identifier>> literals, ExprType treeType)
		{
			bool flag = treeType == ExprType.And;
			IEnumerable<BoolExpr<T_Identifier>> children = literals.Select(new Func<Literal<T_Identifier>, BoolExpr<T_Identifier>>(Clause<T_Identifier>.ConvertLiteralToExpression));
			if (flag)
			{
				return new AndExpr<T_Identifier>(children);
			}
			return new OrExpr<T_Identifier>(children);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000C8BBD File Offset: 0x000C6DBD
		private static BoolExpr<T_Identifier> ConvertLiteralToExpression(Literal<T_Identifier> literal)
		{
			return literal.Expr;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000C8BC8 File Offset: 0x000C6DC8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Clause{");
			stringBuilder.Append(this._literals);
			return stringBuilder.Append("}").ToString();
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000C8C04 File Offset: 0x000C6E04
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000A1177 File Offset: 0x0009F377
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x040016A1 RID: 5793
		private readonly Set<Literal<T_Identifier>> _literals;

		// Token: 0x040016A2 RID: 5794
		private readonly int _hashCode;
	}
}
