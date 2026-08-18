using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200043F RID: 1087
	internal class RewritingProcessor<T_Tile> : TileProcessor<T_Tile> where T_Tile : class
	{
		// Token: 0x06002800 RID: 10240 RVA: 0x000C2DAE File Offset: 0x000C0FAE
		public RewritingProcessor(TileProcessor<T_Tile> tileProcessor)
		{
			this.m_tileProcessor = tileProcessor;
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x000C2DBD File Offset: 0x000C0FBD
		internal TileProcessor<T_Tile> TileProcessor
		{
			get
			{
				return this.m_tileProcessor;
			}
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000C2DC5 File Offset: 0x000C0FC5
		public void GetStatistics(out int numSATChecks, out int numIntersection, out int numUnion, out int numDifference, out int numErrors)
		{
			numSATChecks = this.m_numSATChecks;
			numIntersection = this.m_numIntersection;
			numUnion = this.m_numUnion;
			numDifference = this.m_numDifference;
			numErrors = this.m_numErrors;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000C2DF1 File Offset: 0x000C0FF1
		internal override T_Tile GetArg1(T_Tile tile)
		{
			return this.m_tileProcessor.GetArg1(tile);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000C2DFF File Offset: 0x000C0FFF
		internal override T_Tile GetArg2(T_Tile tile)
		{
			return this.m_tileProcessor.GetArg2(tile);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000C2E0D File Offset: 0x000C100D
		internal override TileOpKind GetOpKind(T_Tile tile)
		{
			return this.m_tileProcessor.GetOpKind(tile);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000C2E1B File Offset: 0x000C101B
		internal override bool IsEmpty(T_Tile a)
		{
			this.m_numSATChecks++;
			return this.m_tileProcessor.IsEmpty(a);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000C2E37 File Offset: 0x000C1037
		public bool IsDisjointFrom(T_Tile a, T_Tile b)
		{
			return this.m_tileProcessor.IsEmpty(this.Join(a, b));
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000C2E4C File Offset: 0x000C104C
		internal bool IsContainedIn(T_Tile a, T_Tile b)
		{
			T_Tile tile = this.AntiSemiJoin(a, b);
			return this.IsEmpty(tile);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000C2E6C File Offset: 0x000C106C
		internal bool IsEquivalentTo(T_Tile a, T_Tile b)
		{
			bool flag = this.IsContainedIn(a, b);
			bool flag2 = this.IsContainedIn(b, a);
			return flag && flag2;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000C2E91 File Offset: 0x000C1091
		internal override T_Tile Union(T_Tile a, T_Tile b)
		{
			this.m_numUnion++;
			return this.m_tileProcessor.Union(a, b);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000C2EAE File Offset: 0x000C10AE
		internal override T_Tile Join(T_Tile a, T_Tile b)
		{
			if (a == null)
			{
				return b;
			}
			this.m_numIntersection++;
			return this.m_tileProcessor.Join(a, b);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000C2ED5 File Offset: 0x000C10D5
		internal override T_Tile AntiSemiJoin(T_Tile a, T_Tile b)
		{
			this.m_numDifference++;
			return this.m_tileProcessor.AntiSemiJoin(a, b);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000C2EF2 File Offset: 0x000C10F2
		public void AddError()
		{
			this.m_numErrors++;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000C2F04 File Offset: 0x000C1104
		public int CountOperators(T_Tile query)
		{
			int num = 0;
			if (query != null && this.GetOpKind(query) != TileOpKind.Named)
			{
				num++;
				num += this.CountOperators(this.GetArg1(query));
				num += this.CountOperators(this.GetArg2(query));
			}
			return num;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000C2F4C File Offset: 0x000C114C
		public int CountViews(T_Tile query)
		{
			HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
			this.GatherViews(query, hashSet);
			return hashSet.Count;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000C2F6D File Offset: 0x000C116D
		public void GatherViews(T_Tile rewriting, HashSet<T_Tile> views)
		{
			if (rewriting != null)
			{
				if (this.GetOpKind(rewriting) == TileOpKind.Named)
				{
					views.Add(rewriting);
					return;
				}
				this.GatherViews(this.GetArg1(rewriting), views);
				this.GatherViews(this.GetArg2(rewriting), views);
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000C3160 File Offset: 0x000C1360
		public static IEnumerable<T> AllButOne<T>(IEnumerable<T> list, int toSkipPosition)
		{
			int valuePosition = 0;
			foreach (T value in list)
			{
				int num;
				valuePosition = (num = valuePosition) + 1;
				if (num != toSkipPosition)
				{
					yield return value;
				}
			}
			yield break;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000C333C File Offset: 0x000C153C
		public static IEnumerable<T> Concat<T>(T value, IEnumerable<T> rest)
		{
			yield return value;
			foreach (T restValue in rest)
			{
				yield return restValue;
			}
			yield break;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000C35F0 File Offset: 0x000C17F0
		public static IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> list)
		{
			IEnumerable<T> rest = null;
			int valuePosition = 0;
			foreach (T value in list)
			{
				int toSkipPosition;
				valuePosition = (toSkipPosition = valuePosition) + 1;
				rest = RewritingProcessor<T_Tile>.AllButOne<T>(list, toSkipPosition);
				foreach (IEnumerable<T> restPermutation in RewritingProcessor<T_Tile>.Permute<T>(rest))
				{
					yield return RewritingProcessor<T_Tile>.Concat<T>(value, restPermutation);
				}
			}
			if (rest == null)
			{
				yield return list;
			}
			yield break;
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000C3610 File Offset: 0x000C1810
		public static List<T> RandomPermutation<T>(IEnumerable<T> input)
		{
			List<T> list = new List<T>(input);
			for (int i = 0; i < list.Count; i++)
			{
				int index = RewritingProcessor<T_Tile>.rnd.Next(list.Count);
				T value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
			return list;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000C3830 File Offset: 0x000C1A30
		public static IEnumerable<T> Reverse<T>(IEnumerable<T> input, HashSet<T> filter)
		{
			List<T> output = new List<T>(input);
			output.Reverse();
			foreach (T t in output)
			{
				if (filter.Contains(t))
				{
					yield return t;
				}
			}
			yield break;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000C3854 File Offset: 0x000C1A54
		public bool RewriteQuery(T_Tile toFill, T_Tile toAvoid, IEnumerable<T_Tile> views, out T_Tile rewriting)
		{
			if (this.RewriteQueryOnce(toFill, toAvoid, views, out rewriting))
			{
				HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
				this.GatherViews(rewriting, hashSet);
				int num = this.CountOperators(rewriting);
				int num2 = 0;
				int num3 = Math.Min(0, Math.Max(0, (int)((double)hashSet.Count * 0.0)));
				while (num2++ < num3)
				{
					IEnumerable<T_Tile> views2;
					if (num2 == 1)
					{
						views2 = RewritingProcessor<T_Tile>.Reverse<T_Tile>(views, hashSet);
					}
					else
					{
						views2 = RewritingProcessor<T_Tile>.RandomPermutation<T_Tile>(hashSet);
					}
					T_Tile t_Tile;
					this.RewriteQueryOnce(toFill, toAvoid, views2, out t_Tile);
					int num4 = this.CountOperators(t_Tile);
					if (num4 < num)
					{
						num = num4;
						rewriting = t_Tile;
					}
					HashSet<T_Tile> hashSet2 = new HashSet<T_Tile>();
					this.GatherViews(t_Tile, hashSet2);
					hashSet = hashSet2;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000C3914 File Offset: 0x000C1B14
		public bool RewriteQueryOnce(T_Tile toFill, T_Tile toAvoid, IEnumerable<T_Tile> views, out T_Tile rewriting)
		{
			List<T_Tile> views2 = new List<T_Tile>(views);
			return RewritingPass<T_Tile>.RewriteQuery(toFill, toAvoid, out rewriting, views2, this);
		}

		// Token: 0x04000F17 RID: 3863
		public const double PermuteFraction = 0.0;

		// Token: 0x04000F18 RID: 3864
		public const int MinPermutations = 0;

		// Token: 0x04000F19 RID: 3865
		public const int MaxPermutations = 0;

		// Token: 0x04000F1A RID: 3866
		private int m_numSATChecks;

		// Token: 0x04000F1B RID: 3867
		private int m_numIntersection;

		// Token: 0x04000F1C RID: 3868
		private int m_numDifference;

		// Token: 0x04000F1D RID: 3869
		private int m_numUnion;

		// Token: 0x04000F1E RID: 3870
		private int m_numErrors;

		// Token: 0x04000F1F RID: 3871
		private readonly TileProcessor<T_Tile> m_tileProcessor;

		// Token: 0x04000F20 RID: 3872
		private static Random rnd = new Random(1507);
	}
}
