using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000303 RID: 771
	internal abstract class Sentence<T_Identifier, T_Clause> : NormalFormNode<T_Identifier> where T_Clause : Clause<T_Identifier>, IEquatable<T_Clause>
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x000869DF File Offset: 0x00084BDF
		protected Sentence(Set<T_Clause> clauses, ExprType treeType) : base(Sentence<T_Identifier, T_Clause>.ConvertClausesToExpr(clauses, treeType))
		{
			this._clauses = clauses.AsReadOnly();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000869FC File Offset: 0x00084BFC
		private static BoolExpr<T_Identifier> ConvertClausesToExpr(Set<T_Clause> clauses, ExprType treeType)
		{
			bool flag = ExprType.And == treeType;
			IEnumerable<BoolExpr<T_Identifier>> children = clauses.Select(new Func<T_Clause, BoolExpr<T_Identifier>>(NormalFormNode<T_Identifier>.ExprSelector<T_Clause>));
			if (flag)
			{
				return new AndExpr<T_Identifier>(children);
			}
			return new OrExpr<T_Identifier>(children);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00086A34 File Offset: 0x00084C34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Sentence{");
			stringBuilder.Append(this._clauses);
			return stringBuilder.Append("}").ToString();
		}

		// Token: 0x0400097A RID: 2426
		private readonly Set<T_Clause> _clauses;
	}
}
