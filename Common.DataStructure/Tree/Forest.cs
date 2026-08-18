using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x0200000C RID: 12
	[DataContract(Namespace = "http://tpro.ca")]
	public class Forest<T> : ICloneable<Forest<T>>, ICloneable
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000026A3 File Offset: 0x000008A3
		public Forest()
		{
			this.Nodes = new TreeNodeCollection<T>();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026B6 File Offset: 0x000008B6
		public Forest(Forest<T> forest)
		{
			this.Nodes = new TreeNodeCollection<T>();
			this.CopyNodesShallow(forest.Nodes, this, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026D8 File Offset: 0x000008D8
		private void CopyNodesShallow(TreeNodeCollection<T> nodesSource, Forest<T> forestDest, TreeNode<T> parentNodeDest)
		{
			foreach (TreeNode<T> treeNode in nodesSource)
			{
				TreeNode<T> parentNodeDest2 = forestDest.AppendNode(parentNodeDest, treeNode.Value);
				if (treeNode.Nodes.Count > 0)
				{
					this.CopyNodesShallow(treeNode.Nodes, forestDest, parentNodeDest2);
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002744 File Offset: 0x00000944
		public string ConvertToFormattedText()
		{
			if (this.Nodes.Count < 1)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			this.BuildFormattedText(this.Nodes, ref stringBuilder, "");
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002784 File Offset: 0x00000984
		private void BuildFormattedText(TreeNodeCollection<T> nodes, ref StringBuilder sb, string currentIndent)
		{
			foreach (TreeNode<T> treeNode in nodes)
			{
				string text;
				if (treeNode.Value != null)
				{
					T value = treeNode.Value;
					text = value.ToString();
				}
				else
				{
					text = "NULL";
				}
				string arg = text;
				sb.AppendFormat("{0}{1}{2}", currentIndent, arg, Environment.NewLine);
				if (treeNode.Nodes.Count > 0)
				{
					this.BuildFormattedText(treeNode.Nodes, ref sb, currentIndent + "     ");
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002828 File Offset: 0x00000A28
		public Forest<T> Where(Func<TreeNode<T>, bool> Predicate)
		{
			Forest<T> forest = new Forest<T>();
			this.Where(Predicate, this.Nodes, forest, null);
			return forest;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000284C File Offset: 0x00000A4C
		private void Where(Func<TreeNode<T>, bool> predicate, TreeNodeCollection<T> nodesSource, Forest<T> forestDest, TreeNode<T> parentNodeDest)
		{
			foreach (TreeNode<T> treeNode in nodesSource)
			{
				if (predicate(treeNode))
				{
					TreeNode<T> parentNodeDest2 = forestDest.AppendNode(parentNodeDest, treeNode.Value);
					if (treeNode.Nodes.Count > 0)
					{
						this.Where(predicate, treeNode.Nodes, forestDest, parentNodeDest2);
					}
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000028C4 File Offset: 0x00000AC4
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000028CC File Offset: 0x00000ACC
		[DataMember]
		public TreeNodeCollection<T> Nodes { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028D8 File Offset: 0x00000AD8
		public IList<TreeNode<T>> AllNodesList
		{
			get
			{
				List<TreeNode<T>> result = new List<TreeNode<T>>();
				this.ExtractNodes(this.Nodes, ref result);
				return result;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028FC File Offset: 0x00000AFC
		public Forest<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			Forest<TOutput> forest = new Forest<TOutput>();
			this.ConvertAll<TOutput>(converter, this.Nodes, forest, null);
			return forest;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002920 File Offset: 0x00000B20
		private void ConvertAll<TOutput>(Converter<T, TOutput> converter, TreeNodeCollection<T> nodesSource, Forest<TOutput> forestDest, TreeNode<TOutput> parentNodeDest)
		{
			foreach (TreeNode<T> treeNode in nodesSource)
			{
				TreeNode<TOutput> parentNodeDest2 = forestDest.AppendNode(parentNodeDest, converter(treeNode.Value));
				if (treeNode.Nodes.Count > 0)
				{
					this.ConvertAll<TOutput>(converter, treeNode.Nodes, forestDest, parentNodeDest2);
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002994 File Offset: 0x00000B94
		private void ExtractNodes(TreeNodeCollection<T> nodes, ref List<TreeNode<T>> nodesList)
		{
			foreach (TreeNode<T> treeNode in nodes)
			{
				nodesList.Add(treeNode);
				if (treeNode.Nodes.Count > 0)
				{
					this.ExtractNodes(treeNode.Nodes, ref nodesList);
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029F8 File Offset: 0x00000BF8
		public TreeNode<T> AppendNode(TreeNode<T> parentNode, T Value)
		{
			if (parentNode == null)
			{
				TreeNode<T> treeNode = new TreeNode<T>(Value);
				this.Nodes.AddNode(treeNode);
				return treeNode;
			}
			return parentNode.AppendNode(Value, parentNode);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A25 File Offset: 0x00000C25
		public TreeNode<T> Find(Predicate<T> match)
		{
			return this.Find(this.Nodes, match);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A34 File Offset: 0x00000C34
		public List<TreeNode<T>> FindAll(Predicate<T> match)
		{
			List<TreeNode<T>> result = new List<TreeNode<T>>();
			this.FindAll(ref result, this.Nodes, match);
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A58 File Offset: 0x00000C58
		private TreeNode<T> Find(TreeNodeCollection<T> nodes, Predicate<T> match)
		{
			foreach (TreeNode<T> treeNode in nodes)
			{
				if (match(treeNode.Value))
				{
					return treeNode;
				}
				if (treeNode.Nodes.Count > 0)
				{
					TreeNode<T> treeNode2 = this.Find(treeNode.Nodes, match);
					if (treeNode2 != null)
					{
						return treeNode2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private void FindAll(ref List<TreeNode<T>> results, TreeNodeCollection<T> nodes, Predicate<T> match)
		{
			foreach (TreeNode<T> treeNode in nodes)
			{
				if (match(treeNode.Value))
				{
					results.Add(treeNode);
				}
				if (treeNode.Nodes.Count > 0)
				{
					this.FindAll(ref results, treeNode.Nodes, match);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B48 File Offset: 0x00000D48
		public Forest<T> Clone()
		{
			return new Forest<T>(this);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B50 File Offset: 0x00000D50
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B58 File Offset: 0x00000D58
		public static C ConvertForestToCollection<I, G, C, R>(Forest<R> forest) where I : class, ForestNodeItem where G : class, ForestNodeGroup where C : class, IForestCollection<I, G> where R : class, ForestNodeItemOrGroup<I, G>
		{
			C c = (C)((object)Activator.CreateInstance(typeof(C)));
			IList<TreeNode<R>> allNodesList = forest.AllNodesList;
			List<TreeNode<R>> list = (from g in allNodesList
			where g.Value.Group != null
			select g).ToList<TreeNode<R>>();
			List<TreeNode<R>> list2 = (from g in allNodesList
			where g.Value.Item != null
			select g).ToList<TreeNode<R>>();
			c.Items = list2.ConvertAll<I>((TreeNode<R> g) => g.Value.Item);
			c.Groups = list.ConvertAll<G>((TreeNode<R> g) => g.Value.Group);
			return c;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C34 File Offset: 0x00000E34
		public static Forest<R> ConvertCollectionToForest<I, G, C, R>(C Collection) where I : class, ForestNodeItem where G : class, ForestNodeGroup where C : class, IForestCollection<I, G> where R : class, ForestNodeItemOrGroup<I, G>
		{
			IEnumerable<I> enumerable = from g in Collection.Items
			where g.Id < 1 || Collection.Groups.FirstOrDefault((G h) => h.Id == g.Id) == null
			select g;
			List<Forest<T>.Node> groupNodes = Forest<T>.MakeTreeFromFlatList<G>(Collection.Groups);
			Forest<R> forest = (Forest<R>)Activator.CreateInstance(typeof(Forest<R>));
			Forest<T>.AddGroupsAndReportsToForest<I, G, C, R>(groupNodes, null, ref forest, Collection);
			Type typeFromHandle = typeof(R);
			foreach (I item in enumerable)
			{
				R r = (R)((object)Activator.CreateInstance(typeFromHandle));
				r.Item = item;
				forest.AppendNode(null, r);
			}
			return forest;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D0C File Offset: 0x00000F0C
		private static void AddGroupsAndReportsToForest<I, G, C, R>(List<Forest<T>.Node> groupNodes, TreeNode<R> currentParentNode, ref Forest<R> forest, C collection) where I : class, ForestNodeItem where G : class, ForestNodeGroup where C : class, IForestCollection<I, G> where R : class, ForestNodeItemOrGroup<I, G>
		{
			Type typeFromHandle = typeof(R);
			using (List<Forest<T>.Node>.Enumerator enumerator = groupNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Forest<T>.Node groupNode = enumerator.Current;
					G group = collection.Groups.FirstOrDefault((G g) => g.ParentId == groupNode.Id);
					R r3 = (R)((object)Activator.CreateInstance(typeFromHandle));
					r3.Group = group;
					TreeNode<R> treeNode = forest.AppendNode(currentParentNode, r3);
					if (groupNode.Children.Count > 0)
					{
						Forest<T>.AddGroupsAndReportsToForest<I, G, C, R>(groupNode.Children, treeNode, ref forest, collection);
					}
					foreach (I item in (from r in collection.Items
					where r.ParentId == groupNode.Id
					select r).ToList<I>())
					{
						R r2 = (R)((object)Activator.CreateInstance(typeFromHandle));
						r2.Item = item;
						forest.AppendNode(treeNode, r2);
					}
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E70 File Offset: 0x00001070
		private static List<Forest<T>.Node> MakeTreeFromFlatList<G>(IEnumerable<G> groups) where G : class, ForestNodeGroup
		{
			List<Forest<T>.Node> list = new List<Forest<T>.Node>();
			foreach (G g in groups)
			{
				list.Add(new Forest<T>.Node(g.Id, g.ParentId));
			}
			return Forest<T>.MakeTreeFromFlatList(list);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EE0 File Offset: 0x000010E0
		private static List<Forest<T>.Node> MakeTreeFromFlatList(IEnumerable<Forest<T>.Node> flatList)
		{
			Dictionary<int, Forest<T>.Node> dictionary = flatList.ToDictionary((Forest<T>.Node n) => n.Id, (Forest<T>.Node n) => n);
			List<Forest<T>.Node> list = new List<Forest<T>.Node>();
			foreach (Forest<T>.Node node in flatList)
			{
				if (node.ParentId != null && dictionary.ContainsKey(node.ParentId.Value))
				{
					Forest<T>.Node node2 = dictionary[node.ParentId.Value];
					node.Parent = node2;
					node2.Children.Add(node);
				}
				else
				{
					list.Add(node);
				}
			}
			return list;
		}

		// Token: 0x0200001E RID: 30
		internal class Node
		{
			// Token: 0x060000DD RID: 221 RVA: 0x000055F6 File Offset: 0x000037F6
			public Node()
			{
				this.Children = new List<Forest<T>.Node>();
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00005609 File Offset: 0x00003809
			public Node(int id, int parentId)
			{
				this.Id = id;
				if (parentId > 0)
				{
					this.ParentId = new int?(parentId);
				}
				this.Children = new List<Forest<T>.Node>();
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000DF RID: 223 RVA: 0x00005633 File Offset: 0x00003833
			// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000563B File Offset: 0x0000383B
			public int Id { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005644 File Offset: 0x00003844
			// (set) Token: 0x060000E2 RID: 226 RVA: 0x0000564C File Offset: 0x0000384C
			public int? ParentId { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005655 File Offset: 0x00003855
			// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000565D File Offset: 0x0000385D
			public List<Forest<T>.Node> Children { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005666 File Offset: 0x00003866
			// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000566E File Offset: 0x0000386E
			public Forest<T>.Node Parent { get; set; }
		}
	}
}
