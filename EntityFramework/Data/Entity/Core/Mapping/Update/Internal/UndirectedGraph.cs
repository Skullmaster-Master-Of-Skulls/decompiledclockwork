using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Text;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x02000411 RID: 1041
	internal class UndirectedGraph<TVertex> : InternalBase
	{
		// Token: 0x0600264F RID: 9807 RVA: 0x000B622E File Offset: 0x000B442E
		internal UndirectedGraph(IEqualityComparer<TVertex> comparer)
		{
			this.m_graph = new Graph<TVertex>(comparer);
			this.m_comparer = comparer;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000B6249 File Offset: 0x000B4449
		internal IEnumerable<TVertex> Vertices
		{
			get
			{
				return this.m_graph.Vertices;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000B6256 File Offset: 0x000B4456
		internal IEnumerable<KeyValuePair<TVertex, TVertex>> Edges
		{
			get
			{
				return this.m_graph.Edges;
			}
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000B6263 File Offset: 0x000B4463
		internal void AddVertex(TVertex vertex)
		{
			this.m_graph.AddVertex(vertex);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000B6271 File Offset: 0x000B4471
		internal void AddEdge(TVertex first, TVertex second)
		{
			this.m_graph.AddEdge(first, second);
			this.m_graph.AddEdge(second, first);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000B6290 File Offset: 0x000B4490
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

		// Token: 0x06002655 RID: 9813 RVA: 0x000B6460 File Offset: 0x000B4660
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_graph);
		}

		// Token: 0x04000E55 RID: 3669
		private readonly Graph<TVertex> m_graph;

		// Token: 0x04000E56 RID: 3670
		private readonly IEqualityComparer<TVertex> m_comparer;

		// Token: 0x02000412 RID: 1042
		private class ComponentNum
		{
			// Token: 0x06002656 RID: 9814 RVA: 0x000B646F File Offset: 0x000B466F
			internal ComponentNum(int compNum)
			{
				this.componentNum = compNum;
			}

			// Token: 0x06002657 RID: 9815 RVA: 0x000B6480 File Offset: 0x000B4680
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0}", new object[]
				{
					this.componentNum
				});
			}

			// Token: 0x04000E57 RID: 3671
			internal int componentNum;
		}
	}
}
