using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x02000012 RID: 18
	[DataContract(Namespace = "http://tpro.ca")]
	public class Tree<T>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000031D4 File Offset: 0x000013D4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000031DC File Offset: 0x000013DC
		[DataMember]
		public TreeNode<T> Root { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x000031E8 File Offset: 0x000013E8
		public TreeNode<T> AppendNode(TreeNode<T> parentNode, T Value)
		{
			if (parentNode == null)
			{
				TreeNode<T> treeNode = new TreeNode<T>(Value);
				this.Root.Nodes.AddNode(treeNode);
				return treeNode;
			}
			return parentNode.AppendNode(Value, parentNode);
		}
	}
}
