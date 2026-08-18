using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003F6 RID: 1014
	internal class Graph<TVertex>
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x000B2384 File Offset: 0x000B0584
		internal Graph(IEqualityComparer<TVertex> comparer)
		{
			this.m_comparer = comparer;
			this.m_successorMap = new Dictionary<TVertex, HashSet<TVertex>>(comparer);
			this.m_predecessorCounts = new Dictionary<TVertex, int>(comparer);
			this.m_vertices = new HashSet<TVertex>(comparer);
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000B23B7 File Offset: 0x000B05B7
		internal IEnumerable<TVertex> Vertices
		{
			get
			{
				return this.m_vertices;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000B25F0 File Offset: 0x000B07F0
		internal IEnumerable<KeyValuePair<TVertex, TVertex>> Edges
		{
			get
			{
				foreach (KeyValuePair<TVertex, HashSet<TVertex>> successors in this.m_successorMap)
				{
					KeyValuePair<TVertex, HashSet<TVertex>> keyValuePair = successors;
					foreach (TVertex vertex in keyValuePair.Value)
					{
						KeyValuePair<TVertex, HashSet<TVertex>> keyValuePair2 = successors;
						yield return new KeyValuePair<TVertex, TVertex>(keyValuePair2.Key, vertex);
					}
				}
				yield break;
			}
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000B260D File Offset: 0x000B080D
		internal void AddVertex(TVertex vertex)
		{
			this.m_vertices.Add(vertex);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000B261C File Offset: 0x000B081C
		internal void AddEdge(TVertex from, TVertex to)
		{
			if (this.m_vertices.Contains(from) && this.m_vertices.Contains(to))
			{
				HashSet<TVertex> hashSet;
				if (!this.m_successorMap.TryGetValue(from, out hashSet))
				{
					hashSet = new HashSet<TVertex>(this.m_comparer);
					this.m_successorMap.Add(from, hashSet);
				}
				if (hashSet.Add(to))
				{
					int num;
					if (!this.m_predecessorCounts.TryGetValue(to, out num))
					{
						num = 1;
					}
					else
					{
						num++;
					}
					this.m_predecessorCounts[to] = num;
				}
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000B269C File Offset: 0x000B089C
		internal bool TryTopologicalSort(out IEnumerable<TVertex> orderedVertices, out IEnumerable<TVertex> remainder)
		{
			SortedSet<TVertex> sortedSet = new SortedSet<TVertex>(Comparer<TVertex>.Default);
			foreach (TVertex tvertex in this.m_vertices)
			{
				int num;
				if (!this.m_predecessorCounts.TryGetValue(tvertex, out num) || num == 0)
				{
					sortedSet.Add(tvertex);
				}
			}
			TVertex[] array = new TVertex[this.m_vertices.Count];
			int count = 0;
			while (0 < sortedSet.Count)
			{
				TVertex min = sortedSet.Min;
				sortedSet.Remove(min);
				HashSet<TVertex> hashSet;
				if (this.m_successorMap.TryGetValue(min, out hashSet))
				{
					foreach (TVertex tvertex2 in hashSet)
					{
						int num2 = this.m_predecessorCounts[tvertex2] - 1;
						this.m_predecessorCounts[tvertex2] = num2;
						if (num2 == 0)
						{
							sortedSet.Add(tvertex2);
						}
					}
					this.m_successorMap.Remove(min);
				}
				array[count++] = min;
				this.m_vertices.Remove(min);
			}
			if (this.m_vertices.Count == 0)
			{
				orderedVertices = array;
				remainder = Enumerable.Empty<TVertex>();
				return true;
			}
			orderedVertices = array.Take(count);
			remainder = this.m_vertices;
			return false;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000B2814 File Offset: 0x000B0A14
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<TVertex, HashSet<TVertex>> keyValuePair in this.m_successorMap)
			{
				bool flag = true;
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}] --> ", new object[]
				{
					keyValuePair.Key
				});
				foreach (TVertex tvertex in keyValuePair.Value)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}]", new object[]
					{
						tvertex
					});
				}
				stringBuilder.Append("; ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000DE9 RID: 3561
		private readonly Dictionary<TVertex, HashSet<TVertex>> m_successorMap;

		// Token: 0x04000DEA RID: 3562
		private readonly Dictionary<TVertex, int> m_predecessorCounts;

		// Token: 0x04000DEB RID: 3563
		private readonly HashSet<TVertex> m_vertices;

		// Token: 0x04000DEC RID: 3564
		private readonly IEqualityComparer<TVertex> m_comparer;
	}
}
