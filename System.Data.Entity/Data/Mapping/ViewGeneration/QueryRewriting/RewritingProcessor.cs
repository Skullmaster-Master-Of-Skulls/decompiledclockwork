using System;
using System.Collections.Generic;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200028F RID: 655
	internal class RewritingProcessor<T_Tile> : TileProcessor<T_Tile> where T_Tile : class
	{
		// Token: 0x0600273A RID: 10042 RVA: 0x0009910A File Offset: 0x0009730A
		public RewritingProcessor(TileProcessor<T_Tile> tileProcessor)
		{
			this.m_tileProcessor = tileProcessor;
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x00099119 File Offset: 0x00097319
		internal TileProcessor<T_Tile> TileProcessor
		{
			get
			{
				return this.m_tileProcessor;
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x00099121 File Offset: 0x00097321
		public void GetStatistics(out int numSATChecks, out int numIntersection, out int numUnion, out int numDifference, out int numErrors)
		{
			numSATChecks = this.m_numSATChecks;
			numIntersection = this.m_numIntersection;
			numUnion = this.m_numUnion;
			numDifference = this.m_numDifference;
			numErrors = this.m_numErrors;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x00099150 File Offset: 0x00097350
		public void PrintStatistics()
		{
			Console.WriteLine("{0} containment checks, {4} set operations ({1} intersections + {2} unions + {3} differences)", new object[]
			{
				this.m_numSATChecks,
				this.m_numIntersection,
				this.m_numUnion,
				this.m_numDifference,
				this.m_numIntersection + this.m_numUnion + this.m_numDifference
			});
			Console.WriteLine("{0} errors", this.m_numErrors);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000991D6 File Offset: 0x000973D6
		internal override T_Tile GetArg1(T_Tile tile)
		{
			return this.m_tileProcessor.GetArg1(tile);
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000991E4 File Offset: 0x000973E4
		internal override T_Tile GetArg2(T_Tile tile)
		{
			return this.m_tileProcessor.GetArg2(tile);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000991F2 File Offset: 0x000973F2
		internal override TileOpKind GetOpKind(T_Tile tile)
		{
			return this.m_tileProcessor.GetOpKind(tile);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x00099200 File Offset: 0x00097400
		internal override bool IsEmpty(T_Tile a)
		{
			this.m_numSATChecks++;
			return this.m_tileProcessor.IsEmpty(a);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0009921C File Offset: 0x0009741C
		public bool IsDisjointFrom(T_Tile a, T_Tile b)
		{
			return this.m_tileProcessor.IsEmpty(this.Join(a, b));
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x00099234 File Offset: 0x00097434
		internal bool IsContainedIn(T_Tile a, T_Tile b)
		{
			T_Tile tile = this.AntiSemiJoin(a, b);
			return this.IsEmpty(tile);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x00099254 File Offset: 0x00097454
		internal bool IsEquivalentTo(T_Tile a, T_Tile b)
		{
			bool flag = this.IsContainedIn(a, b);
			bool flag2 = this.IsContainedIn(b, a);
			return flag && flag2;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00099276 File Offset: 0x00097476
		internal override T_Tile Union(T_Tile a, T_Tile b)
		{
			this.m_numUnion++;
			return this.m_tileProcessor.Union(a, b);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x00099293 File Offset: 0x00097493
		internal override T_Tile Join(T_Tile a, T_Tile b)
		{
			if (a == null)
			{
				return b;
			}
			this.m_numIntersection++;
			return this.m_tileProcessor.Join(a, b);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000992BA File Offset: 0x000974BA
		internal override T_Tile AntiSemiJoin(T_Tile a, T_Tile b)
		{
			this.m_numDifference++;
			return this.m_tileProcessor.AntiSemiJoin(a, b);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000992D7 File Offset: 0x000974D7
		public void AddError()
		{
			this.m_numErrors++;
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000992E8 File Offset: 0x000974E8
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

		// Token: 0x0600274A RID: 10058 RVA: 0x00099330 File Offset: 0x00097530
		public int CountViews(T_Tile query)
		{
			HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
			this.GatherViews(query, hashSet);
			return hashSet.Count;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x00099351 File Offset: 0x00097551
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

		// Token: 0x0600274C RID: 10060 RVA: 0x0009938A File Offset: 0x0009758A
		public static IEnumerable<T> AllButOne<T>(IEnumerable<T> list, int toSkipPosition)
		{
			int valuePosition = 0;
			foreach (T t in list)
			{
				int num = valuePosition;
				valuePosition = num + 1;
				if (num != toSkipPosition)
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000993A1 File Offset: 0x000975A1
		public static IEnumerable<T> Concat<T>(T value, IEnumerable<T> rest)
		{
			yield return value;
			foreach (T t in rest)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000993B8 File Offset: 0x000975B8
		public static IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> list)
		{
			IEnumerable<T> rest = null;
			int valuePosition = 0;
			foreach (T value in list)
			{
				int num = valuePosition;
				valuePosition = num + 1;
				rest = RewritingProcessor<T_Tile>.AllButOne<T>(list, num);
				foreach (IEnumerable<T> rest2 in RewritingProcessor<T_Tile>.Permute<T>(rest))
				{
					yield return RewritingProcessor<T_Tile>.Concat<T>(value, rest2);
				}
				IEnumerator<IEnumerable<T>> enumerator2 = null;
				value = default(T);
			}
			IEnumerator<T> enumerator = null;
			if (rest == null)
			{
				yield return list;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000993C8 File Offset: 0x000975C8
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

		// Token: 0x06002750 RID: 10064 RVA: 0x0009941D File Offset: 0x0009761D
		public static IEnumerable<T> Reverse<T>(IEnumerable<T> input, HashSet<T> filter)
		{
			List<T> list = new List<T>(input);
			list.Reverse();
			foreach (T t in list)
			{
				if (filter.Contains(t))
				{
					yield return t;
				}
			}
			List<T>.Enumerator enumerator = default(List<T>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x00099434 File Offset: 0x00097634
		public bool RewriteQuery(T_Tile toFill, T_Tile toAvoid, IEnumerable<T_Tile> views, out T_Tile rewriting)
		{
			if (this.RewriteQueryOnce(toFill, toAvoid, views, out rewriting))
			{
				HashSet<T_Tile> hashSet = new HashSet<T_Tile>();
				this.GatherViews(rewriting, hashSet);
				int num = this.CountOperators(rewriting);
				int num2 = 0;
				int num3 = Math.Min(this.MAX_PERMUTATIONS, Math.Max(this.MIN_PERMUTATIONS, (int)((double)hashSet.Count * this.PERMUTE_FRACTION)));
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
					bool flag = this.RewriteQueryOnce(toFill, toAvoid, views2, out t_Tile);
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

		// Token: 0x06002752 RID: 10066 RVA: 0x000994FC File Offset: 0x000976FC
		public bool RewriteQueryOnce(T_Tile toFill, T_Tile toAvoid, IEnumerable<T_Tile> views, out T_Tile rewriting)
		{
			List<T_Tile> views2 = new List<T_Tile>(views);
			return RewritingPass<T_Tile>.RewriteQuery(toFill, toAvoid, out rewriting, views2, this);
		}

		// Token: 0x04001206 RID: 4614
		public double PERMUTE_FRACTION;

		// Token: 0x04001207 RID: 4615
		public int MIN_PERMUTATIONS;

		// Token: 0x04001208 RID: 4616
		public int MAX_PERMUTATIONS;

		// Token: 0x04001209 RID: 4617
		public bool REORDER_VIEWS;

		// Token: 0x0400120A RID: 4618
		private int m_numSATChecks;

		// Token: 0x0400120B RID: 4619
		private int m_numIntersection;

		// Token: 0x0400120C RID: 4620
		private int m_numDifference;

		// Token: 0x0400120D RID: 4621
		private int m_numUnion;

		// Token: 0x0400120E RID: 4622
		private int m_numErrors;

		// Token: 0x0400120F RID: 4623
		private TileProcessor<T_Tile> m_tileProcessor;

		// Token: 0x04001210 RID: 4624
		private static Random rnd = new Random(1507);
	}
}
