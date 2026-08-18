using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003BD RID: 957
	internal class Simplifier<T_Identifier> : BasicVisitor<T_Identifier>
	{
		// Token: 0x060033EF RID: 13295 RVA: 0x000C8F44 File Offset: 0x000C7144
		protected Simplifier()
		{
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000C8F4C File Offset: 0x000C714C
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

		// Token: 0x060033F1 RID: 13297 RVA: 0x000C8FAA File Offset: 0x000C71AA
		internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.SimplifyTree(expression);
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000C8FAA File Offset: 0x000C71AA
		internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.SimplifyTree(expression);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000C8FB4 File Offset: 0x000C71B4
		private BoolExpr<T_Identifier> SimplifyTree(TreeExpr<T_Identifier> tree)
		{
			bool flag = tree.ExprType == ExprType.And;
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

		// Token: 0x040016AA RID: 5802
		internal static readonly Simplifier<T_Identifier> Instance = new Simplifier<T_Identifier>();
	}
}
