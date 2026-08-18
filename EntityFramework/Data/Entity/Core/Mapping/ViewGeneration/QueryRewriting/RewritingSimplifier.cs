using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000440 RID: 1088
	internal class RewritingSimplifier<T_Tile> where T_Tile : class
	{
		// Token: 0x06002819 RID: 10265 RVA: 0x000C3944 File Offset: 0x000C1B44
		private RewritingSimplifier(T_Tile originalRewriting, T_Tile toAvoid, Dictionary<T_Tile, TileOpKind> usedViews, RewritingProcessor<T_Tile> qp)
		{
			this.m_originalRewriting = originalRewriting;
			this.m_toAvoid = toAvoid;
			this.m_qp = qp;
			this.m_usedViews = usedViews;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000C3974 File Offset: 0x000C1B74
		private RewritingSimplifier(T_Tile rewriting, T_Tile toFill, T_Tile toAvoid, RewritingProcessor<T_Tile> qp)
		{
			this.m_originalRewriting = toFill;
			this.m_toAvoid = toAvoid;
			this.m_qp = qp;
			this.m_usedViews = new Dictionary<T_Tile, TileOpKind>();
			this.GatherUnionedSubqueriesInUsedViews(rewriting);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000C39B0 File Offset: 0x000C1BB0
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

		// Token: 0x0600281C RID: 10268 RVA: 0x000C39E0 File Offset: 0x000C1BE0
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

		// Token: 0x0600281D RID: 10269 RVA: 0x000C3A10 File Offset: 0x000C1C10
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

		// Token: 0x0600281E RID: 10270 RVA: 0x000C3A68 File Offset: 0x000C1C68
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

		// Token: 0x0600281F RID: 10271 RVA: 0x000C3A94 File Offset: 0x000C1C94
		private bool SimplifyRewritingOnce(out T_Tile simplifiedRewriting)
		{
			HashSet<T_Tile> hashSet = new HashSet<T_Tile>(this.m_usedViews.Keys);
			foreach (T_Tile t_Tile in this.m_usedViews.Keys)
			{
				switch (this.m_usedViews[t_Tile])
				{
				case TileOpKind.Union:
				case TileOpKind.Join:
					hashSet.Remove(t_Tile);
					if (this.SimplifyRewritingOnce(t_Tile, hashSet, out simplifiedRewriting))
					{
						return true;
					}
					hashSet.Add(t_Tile);
					break;
				}
			}
			simplifiedRewriting = default(T_Tile);
			return false;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000C3B40 File Offset: 0x000C1D40
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
			TileOpKind tileOpKind = this.m_usedViews[key];
			bool flag;
			if (tileOpKind == TileOpKind.Union)
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

		// Token: 0x06002821 RID: 10273 RVA: 0x000C3C9C File Offset: 0x000C1E9C
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

		// Token: 0x04000F21 RID: 3873
		private readonly T_Tile m_originalRewriting;

		// Token: 0x04000F22 RID: 3874
		private readonly T_Tile m_toAvoid;

		// Token: 0x04000F23 RID: 3875
		private readonly RewritingProcessor<T_Tile> m_qp;

		// Token: 0x04000F24 RID: 3876
		private readonly Dictionary<T_Tile, TileOpKind> m_usedViews = new Dictionary<T_Tile, TileOpKind>();
	}
}
