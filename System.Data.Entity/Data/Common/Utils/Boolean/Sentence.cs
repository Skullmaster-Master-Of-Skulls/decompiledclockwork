using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003BA RID: 954
	internal abstract class Sentence<T_Identifier, T_Clause> : NormalFormNode<T_Identifier> where T_Clause : Clause<T_Identifier>, IEquatable<T_Clause>
	{
		// Token: 0x060033EA RID: 13290 RVA: 0x000C8EA0 File Offset: 0x000C70A0
		protected Sentence(Set<T_Clause> clauses, ExprType treeType) : base(Sentence<T_Identifier, T_Clause>.ConvertClausesToExpr(clauses, treeType))
		{
			this._clauses = clauses.AsReadOnly();
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000C8EBC File Offset: 0x000C70BC
		private static BoolExpr<T_Identifier> ConvertClausesToExpr(Set<T_Clause> clauses, ExprType treeType)
		{
			bool flag = treeType == ExprType.And;
			IEnumerable<BoolExpr<T_Identifier>> children = clauses.Select(new Func<T_Clause, BoolExpr<T_Identifier>>(NormalFormNode<T_Identifier>.ExprSelector<T_Clause>));
			if (flag)
			{
				return new AndExpr<T_Identifier>(children);
			}
			return new OrExpr<T_Identifier>(children);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000C8EF4 File Offset: 0x000C70F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Sentence{");
			stringBuilder.Append(this._clauses);
			return stringBuilder.Append("}").ToString();
		}

		// Token: 0x040016A9 RID: 5801
		private readonly Set<T_Clause> _clauses;
	}
}
