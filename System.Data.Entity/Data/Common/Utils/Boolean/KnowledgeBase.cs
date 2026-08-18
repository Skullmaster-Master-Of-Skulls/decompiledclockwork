using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B6 RID: 950
	internal class KnowledgeBase<T_Identifier>
	{
		// Token: 0x060033D8 RID: 13272 RVA: 0x000C8C50 File Offset: 0x000C6E50
		internal KnowledgeBase()
		{
			this._facts = new List<BoolExpr<T_Identifier>>();
			this._knowledge = Vertex.One;
			this._context = IdentifierService<T_Identifier>.Instance.CreateConversionContext();
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000C8C80 File Offset: 0x000C6E80
		internal void AddKnowledgeBase(KnowledgeBase<T_Identifier> kb)
		{
			foreach (BoolExpr<T_Identifier> fact in kb._facts)
			{
				this.AddFact(fact);
			}
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000C8CD4 File Offset: 0x000C6ED4
		internal virtual void AddFact(BoolExpr<T_Identifier> fact)
		{
			this._facts.Add(fact);
			Converter<T_Identifier> converter = new Converter<T_Identifier>(fact, this._context);
			Vertex vertex = converter.Vertex;
			this._knowledge = this._context.Solver.And(this._knowledge, vertex);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000C8D1E File Offset: 0x000C6F1E
		internal void AddImplication(BoolExpr<T_Identifier> condition, BoolExpr<T_Identifier> implies)
		{
			this.AddFact(new KnowledgeBase<T_Identifier>.Implication(condition, implies));
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000C8D2D File Offset: 0x000C6F2D
		internal void AddEquivalence(BoolExpr<T_Identifier> left, BoolExpr<T_Identifier> right)
		{
			this.AddFact(new KnowledgeBase<T_Identifier>.Equivalence(left, right));
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000C8D3C File Offset: 0x000C6F3C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Facts:");
			foreach (BoolExpr<T_Identifier> boolExpr in this._facts)
			{
				stringBuilder.Append("\t").AppendLine(boolExpr.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040016A3 RID: 5795
		private readonly List<BoolExpr<T_Identifier>> _facts;

		// Token: 0x040016A4 RID: 5796
		private Vertex _knowledge;

		// Token: 0x040016A5 RID: 5797
		private readonly ConversionContext<T_Identifier> _context;

		// Token: 0x02000688 RID: 1672
		private class Implication : OrExpr<T_Identifier>
		{
			// Token: 0x06004507 RID: 17671 RVA: 0x000F8DD8 File Offset: 0x000F6FD8
			internal Implication(BoolExpr<T_Identifier> condition, BoolExpr<T_Identifier> implies) : base(new BoolExpr<T_Identifier>[]
			{
				condition.MakeNegated(),
				implies
			})
			{
				this._condition = condition;
				this._implies = implies;
			}

			// Token: 0x06004508 RID: 17672 RVA: 0x000F8E01 File Offset: 0x000F7001
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0} --> {1}", new object[]
				{
					this._condition,
					this._implies
				});
			}

			// Token: 0x04001FD4 RID: 8148
			private BoolExpr<T_Identifier> _condition;

			// Token: 0x04001FD5 RID: 8149
			private BoolExpr<T_Identifier> _implies;
		}

		// Token: 0x02000689 RID: 1673
		private class Equivalence : AndExpr<T_Identifier>
		{
			// Token: 0x06004509 RID: 17673 RVA: 0x000F8E25 File Offset: 0x000F7025
			internal Equivalence(BoolExpr<T_Identifier> left, BoolExpr<T_Identifier> right) : base(new BoolExpr<T_Identifier>[]
			{
				new KnowledgeBase<T_Identifier>.Implication(left, right),
				new KnowledgeBase<T_Identifier>.Implication(right, left)
			})
			{
				this._left = left;
				this._right = right;
			}

			// Token: 0x0600450A RID: 17674 RVA: 0x000F8E55 File Offset: 0x000F7055
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0} <--> {1}", new object[]
				{
					this._left,
					this._right
				});
			}

			// Token: 0x04001FD6 RID: 8150
			private BoolExpr<T_Identifier> _left;

			// Token: 0x04001FD7 RID: 8151
			private BoolExpr<T_Identifier> _right;
		}
	}
}
