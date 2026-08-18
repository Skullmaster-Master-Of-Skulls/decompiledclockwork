using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200043E RID: 1086
	internal class RewritingPass<T_Tile> where T_Tile : class
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x000C28A1 File Offset: 0x000C0AA1
		public RewritingPass(T_Tile toFill, T_Tile toAvoid, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			this.m_toFill = toFill;
			this.m_toAvoid = toAvoid;
			this.m_views = views;
			this.m_qp = qp;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000C28D4 File Offset: 0x000C0AD4
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

		// Token: 0x060027F8 RID: 10232 RVA: 0x000C2904 File Offset: 0x000C0B04
		private static bool RewriteQueryInternal(T_Tile toFill, T_Tile toAvoid, out T_Tile rewriting, List<T_Tile> views, RewritingProcessor<T_Tile> qp)
		{
			RewritingPass<T_Tile> rewritingPass = new RewritingPass<T_Tile>(toFill, toAvoid, views, qp);
			return rewritingPass.RewriteQuery(out rewriting);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000C2924 File Offset: 0x000C0B24
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
				if (!RewritingPass<T_Tile>.RewriteQueryInternal(t_Tile2, this.m_toAvoid, out t_Tile3, this.m_views, this.m_qp))
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

		// Token: 0x060027FA RID: 10234 RVA: 0x000C2A9C File Offset: 0x000C0C9C
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

		// Token: 0x060027FB RID: 10235 RVA: 0x000C2AF8 File Offset: 0x000C0CF8
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

		// Token: 0x060027FC RID: 10236 RVA: 0x000C2B54 File Offset: 0x000C0D54
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

		// Token: 0x060027FD RID: 10237 RVA: 0x000C2D08 File Offset: 0x000C0F08
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x000C2D95 File Offset: 0x000C0F95
		private IEnumerable<T_Tile> AvailableViews
		{
			get
			{
				return from view in this.m_views
				where !this.m_usedViews.ContainsKey(view)
				select view;
			}
		}

		// Token: 0x04000F12 RID: 3858
		private readonly T_Tile m_toFill;

		// Token: 0x04000F13 RID: 3859
		private readonly T_Tile m_toAvoid;

		// Token: 0x04000F14 RID: 3860
		private readonly List<T_Tile> m_views;

		// Token: 0x04000F15 RID: 3861
		private readonly RewritingProcessor<T_Tile> m_qp;

		// Token: 0x04000F16 RID: 3862
		private readonly Dictionary<T_Tile, TileOpKind> m_usedViews = new Dictionary<T_Tile, TileOpKind>();
	}
}
