using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C7 RID: 711
	internal class Graph<TVertex>
	{
		// Token: 0x060029EF RID: 10735 RVA: 0x000A3FBD File Offset: 0x000A21BD
		internal Graph(IEqualityComparer<TVertex> comparer)
		{
			EntityUtil.CheckArgumentNull<IEqualityComparer<TVertex>>(comparer, "comparer");
			this.m_comparer = comparer;
			this.m_successorMap = new Dictionary<TVertex, HashSet<TVertex>>(comparer);
			this.m_predecessorCounts = new Dictionary<TVertex, int>(comparer);
			this.m_vertices = new HashSet<TVertex>(comparer);
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000A3FFC File Offset: 0x000A21FC
		internal IEnumerable<TVertex> Vertices
		{
			get
			{
				return this.m_vertices;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000A4004 File Offset: 0x000A2204
		internal IEnumerable<KeyValuePair<TVertex, TVertex>> Edges
		{
			get
			{
				foreach (KeyValuePair<TVertex, HashSet<TVertex>> successors in this.m_successorMap)
				{
					foreach (TVertex value in successors.Value)
					{
						yield return new KeyValuePair<TVertex, TVertex>(successors.Key, value);
					}
					HashSet<TVertex>.Enumerator enumerator2 = default(HashSet<TVertex>.Enumerator);
					successors = default(KeyValuePair<TVertex, HashSet<TVertex>>);
				}
				Dictionary<TVertex, HashSet<TVertex>>.Enumerator enumerator = default(Dictionary<TVertex, HashSet<TVertex>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000A4021 File Offset: 0x000A2221
		internal void AddVertex(TVertex vertex)
		{
			this.m_vertices.Add(vertex);
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000A4030 File Offset: 0x000A2230
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

		// Token: 0x060029F4 RID: 10740 RVA: 0x000A40B0 File Offset: 0x000A22B0
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

		// Token: 0x060029F5 RID: 10741 RVA: 0x000A4228 File Offset: 0x000A2428
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

		// Token: 0x040012B2 RID: 4786
		private readonly Dictionary<TVertex, HashSet<TVertex>> m_successorMap;

		// Token: 0x040012B3 RID: 4787
		private readonly Dictionary<TVertex, int> m_predecessorCounts;

		// Token: 0x040012B4 RID: 4788
		private readonly HashSet<TVertex> m_vertices;

		// Token: 0x040012B5 RID: 4789
		private readonly IEqualityComparer<TVertex> m_comparer;
	}
}
