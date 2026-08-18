using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001DF RID: 479
	internal class KnowledgeBase<T_Identifier>
	{
		// Token: 0x060010E8 RID: 4328 RVA: 0x00048283 File Offset: 0x00046483
		internal KnowledgeBase()
		{
			this._facts = new List<BoolExpr<T_Identifier>>();
			this._knowledge = Vertex.One;
			this._context = IdentifierService<T_Identifier>.Instance.CreateConversionContext();
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x000482B1 File Offset: 0x000464B1
		protected IEnumerable<BoolExpr<T_Identifier>> Facts
		{
			get
			{
				return this._facts;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000482BC File Offset: 0x000464BC
		internal void AddKnowledgeBase(KnowledgeBase<T_Identifier> kb)
		{
			foreach (BoolExpr<T_Identifier> fact in kb._facts)
			{
				this.AddFact(fact);
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00048310 File Offset: 0x00046510
		internal virtual void AddFact(BoolExpr<T_Identifier> fact)
		{
			this._facts.Add(fact);
			Converter<T_Identifier> converter = new Converter<T_Identifier>(fact, this._context);
			Vertex vertex = converter.Vertex;
			this._knowledge = this._context.Solver.And(this._knowledge, vertex);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004835A File Offset: 0x0004655A
		internal void AddImplication(BoolExpr<T_Identifier> condition, BoolExpr<T_Identifier> implies)
		{
			this.AddFact(new KnowledgeBase<T_Identifier>.Implication(condition, implies));
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00048369 File Offset: 0x00046569
		internal void AddEquivalence(BoolExpr<T_Identifier> left, BoolExpr<T_Identifier> right)
		{
			this.AddFact(new KnowledgeBase<T_Identifier>.Equivalence(left, right));
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00048378 File Offset: 0x00046578
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

		// Token: 0x04000511 RID: 1297
		private readonly List<BoolExpr<T_Identifier>> _facts;

		// Token: 0x04000512 RID: 1298
		private Vertex _knowledge;

		// Token: 0x04000513 RID: 1299
		private readonly ConversionContext<T_Identifier> _context;

		// Token: 0x020001E3 RID: 483
		protected class Implication : OrExpr<T_Identifier>
		{
			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06001106 RID: 4358 RVA: 0x00048601 File Offset: 0x00046801
			internal BoolExpr<T_Identifier> Condition
			{
				get
				{
					return this._condition;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06001107 RID: 4359 RVA: 0x00048609 File Offset: 0x00046809
			internal BoolExpr<T_Identifier> Implies
			{
				get
				{
					return this._implies;
				}
			}

			// Token: 0x06001108 RID: 4360 RVA: 0x00048614 File Offset: 0x00046814
			internal Implication(BoolExpr<T_Identifier> condition, BoolExpr<T_Identifier> implies) : base(new BoolExpr<T_Identifier>[]
			{
				condition.MakeNegated(),
				implies
			})
			{
				this._condition = condition;
				this._implies = implies;
			}

			// Token: 0x06001109 RID: 4361 RVA: 0x0004864C File Offset: 0x0004684C
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0} --> {1}", new object[]
				{
					this._condition,
					this._implies
				});
			}

			// Token: 0x04000516 RID: 1302
			private readonly BoolExpr<T_Identifier> _condition;

			// Token: 0x04000517 RID: 1303
			private readonly BoolExpr<T_Identifier> _implies;
		}

		// Token: 0x020001E5 RID: 485
		protected class Equivalence : AndExpr<T_Identifier>
		{
			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x0600110E RID: 4366 RVA: 0x000486A0 File Offset: 0x000468A0
			internal BoolExpr<T_Identifier> Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600110F RID: 4367 RVA: 0x000486A8 File Offset: 0x000468A8
			internal BoolExpr<T_Identifier> Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x06001110 RID: 4368 RVA: 0x000486B0 File Offset: 0x000468B0
			internal Equivalence(BoolExpr<T_Identifier> left, BoolExpr<T_Identifier> right) : base(new BoolExpr<T_Identifier>[]
			{
				new KnowledgeBase<T_Identifier>.Implication(left, right),
				new KnowledgeBase<T_Identifier>.Implication(right, left)
			})
			{
				this._left = left;
				this._right = right;
			}

			// Token: 0x06001111 RID: 4369 RVA: 0x000486F0 File Offset: 0x000468F0
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0} <--> {1}", new object[]
				{
					this._left,
					this._right
				});
			}

			// Token: 0x04000518 RID: 1304
			private readonly BoolExpr<T_Identifier> _left;

			// Token: 0x04000519 RID: 1305
			private readonly BoolExpr<T_Identifier> _right;
		}
	}
}
