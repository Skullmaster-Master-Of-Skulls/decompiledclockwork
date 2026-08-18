using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Text;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002CF RID: 719
	internal class UndirectedGraph<TVertex> : InternalBase
	{
		// Token: 0x06002A50 RID: 10832 RVA: 0x000A629A File Offset: 0x000A449A
		internal UndirectedGraph(IEqualityComparer<TVertex> comparer)
		{
			this.m_graph = new Graph<TVertex>(comparer);
			this.m_comparer = comparer;
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000A62B5 File Offset: 0x000A44B5
		internal IEnumerable<TVertex> Vertices
		{
			get
			{
				return this.m_graph.Vertices;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000A62C2 File Offset: 0x000A44C2
		internal IEnumerable<KeyValuePair<TVertex, TVertex>> Edges
		{
			get
			{
				return this.m_graph.Edges;
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000A62CF File Offset: 0x000A44CF
		internal void AddVertex(TVertex vertex)
		{
			this.m_graph.AddVertex(vertex);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000A62DD File Offset: 0x000A44DD
		internal void AddEdge(TVertex first, TVertex second)
		{
			this.m_graph.AddEdge(first, second);
			this.m_graph.AddEdge(second, first);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000A62FC File Offset: 0x000A44FC
		internal KeyToListMap<int, TVertex> GenerateConnectedComponents()
		{
			int num = 0;
			Dictionary<TVertex, UndirectedGraph<TVertex>.ComponentNum> dictionary = new Dictionary<TVertex, UndirectedGraph<TVertex>.ComponentNum>(this.m_comparer);
			foreach (TVertex key in this.Vertices)
			{
				dictionary.Add(key, new UndirectedGraph<TVertex>.ComponentNum(num));
				num++;
			}
			foreach (KeyValuePair<TVertex, TVertex> keyValuePair in this.Edges)
			{
				if (dictionary[keyValuePair.Key].componentNum != dictionary[keyValuePair.Value].componentNum)
				{
					int componentNum = dictionary[keyValuePair.Value].componentNum;
					int componentNum2 = dictionary[keyValuePair.Key].componentNum;
					dictionary[keyValuePair.Value].componentNum = componentNum2;
					foreach (TVertex key2 in dictionary.Keys)
					{
						if (dictionary[key2].componentNum == componentNum)
						{
							dictionary[key2].componentNum = componentNum2;
						}
					}
				}
			}
			KeyToListMap<int, TVertex> keyToListMap = new KeyToListMap<int, TVertex>(EqualityComparer<int>.Default);
			foreach (TVertex tvertex in this.Vertices)
			{
				int componentNum3 = dictionary[tvertex].componentNum;
				keyToListMap.Add(componentNum3, tvertex);
			}
			return keyToListMap;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000A64C8 File Offset: 0x000A46C8
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_graph.ToString());
		}

		// Token: 0x040012DC RID: 4828
		private Graph<TVertex> m_graph;

		// Token: 0x040012DD RID: 4829
		private IEqualityComparer<TVertex> m_comparer;

		// Token: 0x0200062A RID: 1578
		private class ComponentNum
		{
			// Token: 0x06004339 RID: 17209 RVA: 0x000F4E2B File Offset: 0x000F302B
			internal ComponentNum(int compNum)
			{
				this.componentNum = compNum;
			}

			// Token: 0x0600433A RID: 17210 RVA: 0x000F4E3A File Offset: 0x000F303A
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0}", new object[]
				{
					this.componentNum
				});
			}

			// Token: 0x04001E83 RID: 7811
			internal int componentNum;
		}
	}
}
