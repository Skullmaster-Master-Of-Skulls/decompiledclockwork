using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003AA RID: 938
	internal abstract class BoolExpr<T_Identifier> : IEquatable<BoolExpr<T_Identifier>>
	{
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06003393 RID: 13203
		internal abstract ExprType ExprType { get; }

		// Token: 0x06003394 RID: 13204
		internal abstract T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor);

		// Token: 0x06003395 RID: 13205 RVA: 0x000C87A0 File Offset: 0x000C69A0
		internal BoolExpr<T_Identifier> Simplify()
		{
			return IdentifierService<T_Identifier>.Instance.LocalSimplify(this);
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000C87B0 File Offset: 0x000C69B0
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

		// Token: 0x06003397 RID: 13207 RVA: 0x000C8828 File Offset: 0x000C6A28
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

		// Token: 0x06003398 RID: 13208 RVA: 0x000C8896 File Offset: 0x000C6A96
		internal List<TermExpr<T_Identifier>> GetTerms()
		{
			return LeafVisitor<T_Identifier>.GetTerms(this);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000C889E File Offset: 0x000C6A9E
		internal int CountTerms()
		{
			return TermCounter<T_Identifier>.CountTerms(this);
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000C88A6 File Offset: 0x000C6AA6
		public static implicit operator BoolExpr<T_Identifier>(T_Identifier value)
		{
			return new TermExpr<T_Identifier>(value);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000C88AE File Offset: 0x000C6AAE
		internal virtual BoolExpr<T_Identifier> MakeNegated()
		{
			return new NotExpr<T_Identifier>(this);
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000C88B8 File Offset: 0x000C6AB8
		public override string ToString()
		{
			return this.ExprType.ToString();
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000C88D9 File Offset: 0x000C6AD9
		public bool Equals(BoolExpr<T_Identifier> other)
		{
			return other != null && this.ExprType == other.ExprType && this.EquivalentTypeEquals(other);
		}

		// Token: 0x0600339E RID: 13214
		protected abstract bool EquivalentTypeEquals(BoolExpr<T_Identifier> other);
	}
}
