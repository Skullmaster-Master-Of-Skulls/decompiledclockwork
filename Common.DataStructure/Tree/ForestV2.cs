using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x0200000D RID: 13
	[DataContract(Namespace = "http://tpro.ca")]
	public class ForestV2<T>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002FCC File Offset: 0x000011CC
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002FD4 File Offset: 0x000011D4
		[DataMember]
		public IList<TreeNodeV2<T>> Nodes { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x00002FDD File Offset: 0x000011DD
		public ForestV2()
		{
			this.Nodes = new List<TreeNodeV2<T>>();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FF0 File Offset: 0x000011F0
		public TreeNodeV2<T> Find(Predicate<T> match)
		{
			return this.Find(this.Nodes, match);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003000 File Offset: 0x00001200
		public List<TreeNodeV2<T>> FindAll(Predicate<T> match)
		{
			List<TreeNodeV2<T>> result = new List<TreeNodeV2<T>>();
			this.FindAll(ref result, this.Nodes, match);
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003024 File Offset: 0x00001224
		public IList<TreeNodeV2<T>> DepthFirstTraversal()
		{
			List<TreeNodeV2<T>> list = new List<TreeNodeV2<T>>();
			foreach (TreeNodeV2<T> node in this.Nodes)
			{
				list.AddRange(this.DepthFirstTraversal(node));
			}
			return list;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003080 File Offset: 0x00001280
		private IList<TreeNodeV2<T>> DepthFirstTraversal(TreeNodeV2<T> node)
		{
			List<TreeNodeV2<T>> list = new List<TreeNodeV2<T>>
			{
				node
			};
			foreach (TreeNodeV2<T> node2 in node.Nodes)
			{
				list.AddRange(this.DepthFirstTraversal(node2));
			}
			return list;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030E4 File Offset: 0x000012E4
		private TreeNodeV2<T> Find(IList<TreeNodeV2<T>> nodes, Predicate<T> match)
		{
			foreach (TreeNodeV2<T> treeNodeV in nodes)
			{
				if (match(treeNodeV.Value))
				{
					return treeNodeV;
				}
				if (treeNodeV.Nodes.Count > 0)
				{
					TreeNodeV2<T> treeNodeV2 = this.Find(treeNodeV.Nodes, match);
					if (treeNodeV2 != null)
					{
						return treeNodeV2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003160 File Offset: 0x00001360
		private void FindAll(ref List<TreeNodeV2<T>> results, IList<TreeNodeV2<T>> nodes, Predicate<T> match)
		{
			foreach (TreeNodeV2<T> treeNodeV in nodes)
			{
				if (match(treeNodeV.Value))
				{
					results.Add(treeNodeV);
				}
				if (treeNodeV.Nodes.Count > 0)
				{
					this.FindAll(ref results, treeNodeV.Nodes, match);
				}
			}
		}
	}
}
