using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x02000013 RID: 19
	[DataContract(Namespace = "http://tpro.ca")]
	public class TreeNode<T>
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000321A File Offset: 0x0000141A
		public TreeNode()
		{
			this.Nodes = new TreeNodeCollection<T>();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000322D File Offset: 0x0000142D
		public TreeNode(T Value)
		{
			this.Value = Value;
			this.Nodes = new TreeNodeCollection<T>();
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003247 File Offset: 0x00001447
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000324F File Offset: 0x0000144F
		[DataMember]
		public T Value { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003258 File Offset: 0x00001458
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003260 File Offset: 0x00001460
		[DataMember]
		public TreeNodeCollection<T> Nodes { get; set; }

		// Token: 0x06000079 RID: 121 RVA: 0x0000326C File Offset: 0x0000146C
		public TreeNode<T> AppendNode(T value, TreeNode<T> parent)
		{
			TreeNode<T> treeNode = new TreeNode<T>(value);
			this.Nodes.AddNode(treeNode);
			return treeNode;
		}
	}
}
