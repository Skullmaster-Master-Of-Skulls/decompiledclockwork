using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000290 RID: 656
	internal class RewritingSimplifier<T_Tile> where T_Tile : class
	{
		// Token: 0x06002754 RID: 10068 RVA: 0x0009952C File Offset: 0x0009772C
		private RewritingSimplifier(T_Tile originalRewriting, T_Tile toAvoid, Dictionary<T_Tile, TileOpKind> usedViews, RewritingProcessor<T_Tile> qp)
		{
			this.m_originalRewriting = originalRewriting;
			this.m_toAvoid = toAvoid;
			this.m_qp = qp;
			this.m_usedViews = usedViews;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0009955C File Offset: 0x0009775C
		private RewritingSimplifier(T_Tile rewriting, T_Tile toFill, T_Tile toAvoid, RewritingProcessor<T_Tile> qp)
		{
			this.m_originalRewriting = toFill;
			this.m_toAvoid = toAvoid;
			this.m_qp = qp;
			this.m_usedViews = new Dictionary<T_Tile, TileOpKind>();
			this.GatherUnionedSubqueriesInUsedViews(rewriting);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00099598 File Offset: 0x00097798
		internal static bool TrySimplifyUnionRewriting(ref T_Tile rewriting, T_Tile toFill, T_Tile toAvoid, RewritingProcessor<T_Tile> qp)
		{
			RewritingSimplifier<T_Tile> rewritingSimplifier = new RewritingSimplifier<T_Tile>(rewriting, toFill, toAvoid, qp);
			T_Tile t_Tile;
			if (rewritingSimplifier.SimplifyRewriting(out t_Tile))
			{
				rewriting = t_Tile;
				return true;
			}
			return false;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000995C8 File Offset: 0x000977C8
		internal static bool TrySimplifyJoinRewriting(ref T_Tile rewriting, T_Tile toAvoid, Dictionary<T_Tile, TileOpKind> usedViews, RewritingProcessor<T_Tile> qp)
		{
			RewritingSimplifier<T_Tile> rewritingSimplifier = new RewritingSimplifier<T_Tile>(rewriting, toAvoid, usedViews, qp);
			T_Tile t_Tile;
			if (rewritingSimplifier.SimplifyRewriting(out t_Tile))
			{
				rewriting = t_Tile;
				return true;
			}
			return false;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000995F8 File Offset: 0x000977F8
		private void GatherUnionedSubqueriesInUsedViews(T_Tile query)
		{
			if (query != null)
			{
				if (this.m_qp.GetOpKind(query) != TileOpKind.Union)
				{
					this.m_usedViews[query] = TileOpKind.Union;
					return;
				}
				this.GatherUnionedSubqueriesInUsedViews(this.m_qp.GetArg1(query));
				this.GatherUnionedSubqueriesInUsedViews(this.m_qp.GetArg2(query));
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x00099650 File Offset: 0x00097850
		private bool SimplifyRewriting(out T_Tile simplifiedRewriting)
		{
			bool result = false;
			simplifiedRewriting = default(T_Tile);
			T_Tile t_Tile;
			while (this.SimplifyRewritingOnce(out t_Tile))
			{
				result = true;
				simplifiedRewriting = t_Tile;
			}
			return result;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0009967C File Offset: 0x0009787C
		private bool SimplifyRewritingOnce(out T_Tile simplifiedRewriting)
		{
			HashSet<T_Tile> hashSet = new HashSet<T_Tile>(this.m_usedViews.Keys);
			foreach (T_Tile t_Tile in this.m_usedViews.Keys)
			{
				TileOpKind tileOpKind = this.m_usedViews[t_Tile];
				if (tileOpKind <= TileOpKind.Join)
				{
					hashSet.Remove(t_Tile);
					if (this.SimplifyRewritingOnce(t_Tile, hashSet, out simplifiedRewriting))
					{
						return true;
					}
					hashSet.Add(t_Tile);
				}
			}
			simplifiedRewriting = default(T_Tile);
			return false;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0009971C File Offset: 0x0009791C
		private bool SimplifyRewritingOnce(T_Tile newRewriting, HashSet<T_Tile> remainingViews, out T_Tile simplifiedRewriting)
		{
			simplifiedRewriting = default(T_Tile);
			if (remainingViews.Count == 0)
			{
				return false;
			}
			if (remainingViews.Count != 1)
			{
				int num = remainingViews.Count / 2;
				int num2 = 0;
				T_Tile t_Tile = newRewriting;
				T_Tile t_Tile2 = newRewriting;
				HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
				HashSet<T_Tile> hashSet2 = new HashSet<T_Tile>();
				foreach (T_Tile t_Tile3 in remainingViews)
				{
					TileOpKind viewKind = this.m_usedViews[t_Tile3];
					if (num2++ < num)
					{
						hashSet.Add(t_Tile3);
						t_Tile = this.GetRewritingHalf(t_Tile, t_Tile3, viewKind);
					}
					else
					{
						hashSet2.Add(t_Tile3);
						t_Tile2 = this.GetRewritingHalf(t_Tile2, t_Tile3, viewKind);
					}
				}
				return this.SimplifyRewritingOnce(t_Tile, hashSet2, out simplifiedRewriting) || this.SimplifyRewritingOnce(t_Tile2, hashSet, out simplifiedRewriting);
			}
			T_Tile key = remainingViews.First<T_Tile>();
			bool flag;
			if (this.m_usedViews[key] == TileOpKind.Union)
			{
				flag = this.m_qp.IsContainedIn(this.m_originalRewriting, newRewriting);
			}
			else
			{
				flag = (this.m_qp.IsContainedIn(this.m_originalRewriting, newRewriting) && this.m_qp.IsDisjointFrom(this.m_toAvoid, newRewriting));
			}
			if (flag)
			{
				simplifiedRewriting = newRewriting;
				this.m_usedViews.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00099878 File Offset: 0x00097A78
		private T_Tile GetRewritingHalf(T_Tile halfRewriting, T_Tile remainingView, TileOpKind viewKind)
		{
			switch (viewKind)
			{
			case TileOpKind.Union:
				halfRewriting = this.m_qp.Union(halfRewriting, remainingView);
				break;
			case TileOpKind.Join:
				halfRewriting = this.m_qp.Join(halfRewriting, remainingView);
				break;
			case TileOpKind.AntiSemiJoin:
				halfRewriting = this.m_qp.AntiSemiJoin(halfRewriting, remainingView);
				break;
			}
			return halfRewriting;
		}

		// Token: 0x04001211 RID: 4625
		private readonly T_Tile m_originalRewriting;

		// Token: 0x04001212 RID: 4626
		private readonly T_Tile m_toAvoid;

		// Token: 0x04001213 RID: 4627
		private readonly RewritingProcessor<T_Tile> m_qp;

		// Token: 0x04001214 RID: 4628
		private readonly Dictionary<T_Tile, TileOpKind> m_usedViews = new Dictionary<T_Tile, TileOpKind>();
	}
}
