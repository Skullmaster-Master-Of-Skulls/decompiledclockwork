using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200031B RID: 795
	internal class Simplifier<T_Identifier> : BasicVisitor<T_Identifier>
	{
		// Token: 0x06001B71 RID: 7025 RVA: 0x0008798D File Offset: 0x00085B8D
		protected Simplifier()
		{
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00087998 File Offset: 0x00085B98
		internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
		{
			BoolExpr<T_Identifier> boolExpr = expression.Child.Accept<BoolExpr<T_Identifier>>(this);
			switch (boolExpr.ExprType)
			{
			case ExprType.Not:
				return ((NotExpr<T_Identifier>)boolExpr).Child;
			case ExprType.True:
				return FalseExpr<T_Identifier>.Value;
			case ExprType.False:
				return TrueExpr<T_Identifier>.Value;
			}
			return base.VisitNot(expression);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000879F6 File Offset: 0x00085BF6
		internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.SimplifyTree(expression);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000879FF File Offset: 0x00085BFF
		internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.SimplifyTree(expression);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00087A08 File Offset: 0x00085C08
		private BoolExpr<T_Identifier> SimplifyTree(TreeExpr<T_Identifier> tree)
		{
			bool flag = ExprType.And == tree.ExprType;
			List<BoolExpr<T_Identifier>> list = new List<BoolExpr<T_Identifier>>(tree.Children.Count);
			foreach (BoolExpr<T_Identifier> boolExpr in tree.Children)
			{
				BoolExpr<T_Identifier> boolExpr2 = boolExpr.Accept<BoolExpr<T_Identifier>>(this);
				if (boolExpr2.ExprType == tree.ExprType)
				{
					list.AddRange(((TreeExpr<T_Identifier>)boolExpr2).Children);
				}
				else
				{
					list.Add(boolExpr2);
				}
			}
			Dictionary<BoolExpr<T_Identifier>, bool> dictionary = new Dictionary<BoolExpr<T_Identifier>, bool>(tree.Children.Count);
			List<BoolExpr<T_Identifier>> list2 = new List<BoolExpr<T_Identifier>>(tree.Children.Count);
			foreach (BoolExpr<T_Identifier> boolExpr3 in list)
			{
				switch (boolExpr3.ExprType)
				{
				case ExprType.Not:
					dictionary[((NotExpr<T_Identifier>)boolExpr3).Child] = true;
					continue;
				case ExprType.True:
					if (!flag)
					{
						return TrueExpr<T_Identifier>.Value;
					}
					continue;
				case ExprType.False:
					if (flag)
					{
						return FalseExpr<T_Identifier>.Value;
					}
					continue;
				}
				list2.Add(boolExpr3);
			}
			List<BoolExpr<T_Identifier>> list3 = new List<BoolExpr<T_Identifier>>();
			foreach (BoolExpr<T_Identifier> boolExpr4 in list2)
			{
				if (dictionary.ContainsKey(boolExpr4))
				{
					if (flag)
					{
						return FalseExpr<T_Identifier>.Value;
					}
					return TrueExpr<T_Identifier>.Value;
				}
				else
				{
					list3.Add(boolExpr4);
				}
			}
			foreach (BoolExpr<T_Identifier> boolExpr5 in dictionary.Keys)
			{
				list3.Add(boolExpr5.MakeNegated());
			}
			if (list3.Count == 0)
			{
				if (flag)
				{
					return TrueExpr<T_Identifier>.Value;
				}
				return FalseExpr<T_Identifier>.Value;
			}
			else
			{
				if (1 == list3.Count)
				{
					return list3[0];
				}
				TreeExpr<T_Identifier> result;
				if (flag)
				{
					result = new AndExpr<T_Identifier>(list3);
				}
				else
				{
					result = new OrExpr<T_Identifier>(list3);
				}
				return result;
			}
			BoolExpr<T_Identifier> result2;
			return result2;
		}

		// Token: 0x040009A3 RID: 2467
		internal static readonly Simplifier<T_Identifier> Instance = new Simplifier<T_Identifier>();
	}
}
