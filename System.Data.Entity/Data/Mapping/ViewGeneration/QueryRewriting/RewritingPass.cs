using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200028D RID: 653
	internal class RewritingPass<T_Tile> where T_Tile : class
	{
		// Token: 0x06002728 RID: 10024 RVA: 0x00098B86 File Offset: 0x00096D86
		public RewritingPass(T_Tile toFill, T_Tile toAvoid, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			this.m_toFill = toFill;
			this.m_toAvoid = toAvoid;
			this.m_views = views;
			this.m_qp = qp;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x00098BB8 File Offset: 0x00096DB8
		public static bool RewriteQuery(T_Tile toFill, T_Tile toAvoid, out T_Tile rewriting, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			RewritingPass<T_Tile> rewritingPass = new RewritingPass<T_Tile>(toFill, toAvoid, views, qp);
			if (rewritingPass.RewriteQuery(out rewriting))
			{
				RewritingSimplifier<T_Tile>.TrySimplifyUnionRewriting(ref rewriting, toFill, toAvoid, qp);
				return true;
			}
			return false;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00098BE8 File Offset: 0x00096DE8
		private static bool RewriteQueryInternal(T_Tile toFill, T_Tile toAvoid, out T_Tile rewriting, List<T_Tile> views, HashSet<T_Tile> recentlyUsedViews, RewritingProcessor<T_Tile> qp)
		{
			if (qp.REORDER_VIEWS && recentlyUsedViews.Count > 0)
			{
				List<T_Tile> list = new List<T_Tile>();
				foreach (T_Tile item in views)
				{
					if (!recentlyUsedViews.Contains(item))
					{
						list.Add(item);
					}
				}
				list.AddRange(recentlyUsedViews);
				views = list;
			}
			RewritingPass<T_Tile> rewritingPass = new RewritingPass<T_Tile>(toFill, toAvoid, views, qp);
			return rewritingPass.RewriteQuery(out rewriting);
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x00098C78 File Offset: 0x00096E78
		private bool RewriteQuery(out T_Tile rewriting)
		{
			rewriting = this.m_toFill;
			T_Tile t_Tile;
			if (!this.FindRewritingByIncludedAndDisjoint(out t_Tile) && !this.FindContributingView(out t_Tile))
			{
				return false;
			}
			bool flag = !this.m_qp.IsDisjointFrom(t_Tile, this.m_toAvoid);
			if (flag)
			{
				foreach (T_Tile view in this.AvailableViews)
				{
					if (this.TryJoin(view, ref t_Tile))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				foreach (T_Tile view2 in this.AvailableViews)
				{
					if (this.TryAntiSemiJoin(view2, ref t_Tile))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				return false;
			}
			RewritingSimplifier<T_Tile>.TrySimplifyJoinRewriting(ref t_Tile, this.m_toAvoid, this.m_usedViews, this.m_qp);
			T_Tile t_Tile2 = this.m_qp.AntiSemiJoin(this.m_toFill, t_Tile);
			if (!this.m_qp.IsEmpty(t_Tile2))
			{
				T_Tile t_Tile3;
				if (!RewritingPass<T_Tile>.RewriteQueryInternal(t_Tile2, this.m_toAvoid, out t_Tile3, this.m_views, new HashSet<T_Tile>(this.m_usedViews.Keys), this.m_qp))
				{
					rewriting = t_Tile3;
					return false;
				}
				if (this.m_qp.IsContainedIn(t_Tile, t_Tile3))
				{
					t_Tile = t_Tile3;
				}
				else
				{
					t_Tile = this.m_qp.Union(t_Tile, t_Tile3);
				}
			}
			rewriting = t_Tile;
			return true;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x00098DFC File Offset: 0x00096FFC
		private bool TryJoin(T_Tile view, ref T_Tile rewriting)
		{
			T_Tile t_Tile = this.m_qp.Join(rewriting, view);
			if (!this.m_qp.IsEmpty(t_Tile))
			{
				this.m_usedViews[view] = TileOpKind.Join;
				rewriting = t_Tile;
				return this.m_qp.IsDisjointFrom(rewriting, this.m_toAvoid);
			}
			return false;
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x00098E58 File Offset: 0x00097058
		private bool TryAntiSemiJoin(T_Tile view, ref T_Tile rewriting)
		{
			T_Tile t_Tile = this.m_qp.AntiSemiJoin(rewriting, view);
			if (!this.m_qp.IsEmpty(t_Tile))
			{
				this.m_usedViews[view] = TileOpKind.AntiSemiJoin;
				rewriting = t_Tile;
				return this.m_qp.IsDisjointFrom(rewriting, this.m_toAvoid);
			}
			return false;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00098EB4 File Offset: 0x000970B4
		private bool FindRewritingByIncludedAndDisjoint(out T_Tile rewritingSoFar)
		{
			rewritingSoFar = default(T_Tile);
			foreach (T_Tile t_Tile in this.AvailableViews)
			{
				if (this.m_qp.IsContainedIn(this.m_toFill, t_Tile))
				{
					if (rewritingSoFar == null)
					{
						rewritingSoFar = t_Tile;
						this.m_usedViews[t_Tile] = TileOpKind.Join;
					}
					else
					{
						T_Tile t_Tile2 = this.m_qp.Join(rewritingSoFar, t_Tile);
						if (this.m_qp.IsContainedIn(rewritingSoFar, t_Tile2))
						{
							continue;
						}
						rewritingSoFar = t_Tile2;
						this.m_usedViews[t_Tile] = TileOpKind.Join;
					}
					if (this.m_qp.IsContainedIn(rewritingSoFar, this.m_toFill))
					{
						return true;
					}
				}
			}
			if (rewritingSoFar != null)
			{
				foreach (T_Tile t_Tile3 in this.AvailableViews)
				{
					if (this.m_qp.IsDisjointFrom(this.m_toFill, t_Tile3) && !this.m_qp.IsDisjointFrom(rewritingSoFar, t_Tile3))
					{
						rewritingSoFar = this.m_qp.AntiSemiJoin(rewritingSoFar, t_Tile3);
						this.m_usedViews[t_Tile3] = TileOpKind.AntiSemiJoin;
						if (this.m_qp.IsContainedIn(rewritingSoFar, this.m_toFill))
						{
							return true;
						}
					}
				}
			}
			return rewritingSoFar != null;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00099064 File Offset: 0x00097264
		private bool FindContributingView(out T_Tile rewriting)
		{
			foreach (T_Tile t_Tile in this.AvailableViews)
			{
				if (!this.m_qp.IsDisjointFrom(t_Tile, this.m_toFill))
				{
					rewriting = t_Tile;
					this.m_usedViews[t_Tile] = TileOpKind.Join;
					return true;
				}
			}
			rewriting = default(T_Tile);
			return false;
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000990E0 File Offset: 0x000972E0
		private IEnumerable<T_Tile> AvailableViews
		{
			get
			{
				return from view in this.m_views
				where !this.m_usedViews.ContainsKey(view)
				select view;
			}
		}

		// Token: 0x04001201 RID: 4609
		private readonly T_Tile m_toFill;

		// Token: 0x04001202 RID: 4610
		private readonly T_Tile m_toAvoid;

		// Token: 0x04001203 RID: 4611
		private readonly List<T_Tile> m_views;

		// Token: 0x04001204 RID: 4612
		private readonly RewritingProcessor<T_Tile> m_qp;

		// Token: 0x04001205 RID: 4613
		private readonly Dictionary<T_Tile, TileOpKind> m_usedViews = new Dictionary<T_Tile, TileOpKind>();
	}
}
