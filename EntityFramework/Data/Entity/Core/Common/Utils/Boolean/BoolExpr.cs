using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001E0 RID: 480
	internal abstract class BoolExpr<T_Identifier> : IEquatable<BoolExpr<T_Identifier>>
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060010EF RID: 4335
		internal abstract ExprType ExprType { get; }

		// Token: 0x060010F0 RID: 4336
		internal abstract T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor);

		// Token: 0x060010F1 RID: 4337 RVA: 0x000483F4 File Offset: 0x000465F4
		internal BoolExpr<T_Identifier> Simplify()
		{
			return IdentifierService<T_Identifier>.Instance.LocalSimplify(this);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00048404 File Offset: 0x00046604
		internal BoolExpr<T_Identifier> ExpensiveSimplify(out Converter<T_Identifier> converter)
		{
			ConversionContext<T_Identifier> context = IdentifierService<T_Identifier>.Instance.CreateConversionContext();
			converter = new Converter<T_Identifier>(this, context);
			if (converter.Vertex.IsOne())
			{
				return TrueExpr<T_Identifier>.Value;
			}
			if (converter.Vertex.IsZero())
			{
				return FalseExpr<T_Identifier>.Value;
			}
			return BoolExpr<T_Identifier>.ChooseCandidate(new BoolExpr<T_Identifier>[]
			{
				this,
				converter.Cnf.Expr,
				converter.Dnf.Expr
			});
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0004847C File Offset: 0x0004667C
		private static BoolExpr<T_Identifier> ChooseCandidate(params BoolExpr<T_Identifier>[] candidates)
		{
			int num = 0;
			int num2 = 0;
			BoolExpr<T_Identifier> boolExpr = null;
			foreach (BoolExpr<T_Identifier> boolExpr2 in candidates)
			{
				BoolExpr<T_Identifier> boolExpr3 = boolExpr2.Simplify();
				int num3 = boolExpr3.GetTerms().Distinct<TermExpr<T_Identifier>>().Count<TermExpr<T_Identifier>>();
				int num4 = boolExpr3.CountTerms();
				if (boolExpr == null || num3 < num || (num3 == num && num4 < num2))
				{
					boolExpr = boolExpr3;
					num = num3;
					num2 = num4;
				}
			}
			return boolExpr;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000484EB File Offset: 0x000466EB
		internal List<TermExpr<T_Identifier>> GetTerms()
		{
			return LeafVisitor<T_Identifier>.GetTerms(this);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000484F3 File Offset: 0x000466F3
		internal int CountTerms()
		{
			return TermCounter<T_Identifier>.CountTerms(this);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000484FB File Offset: 0x000466FB
		public static implicit operator BoolExpr<T_Identifier>(T_Identifier value)
		{
			return new TermExpr<T_Identifier>(value);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00048503 File Offset: 0x00046703
		internal virtual BoolExpr<T_Identifier> MakeNegated()
		{
			return new NotExpr<T_Identifier>(this);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004850B File Offset: 0x0004670B
		public override string ToString()
		{
			return this.ExprType.ToString();
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004851D File Offset: 0x0004671D
		public bool Equals(BoolExpr<T_Identifier> other)
		{
			return other != null && this.ExprType == other.ExprType && this.EquivalentTypeEquals(other);
		}

		// Token: 0x060010FA RID: 4346
		protected abstract bool EquivalentTypeEquals(BoolExpr<T_Identifier> other);
	}
}
