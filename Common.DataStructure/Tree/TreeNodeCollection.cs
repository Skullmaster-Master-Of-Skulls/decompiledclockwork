using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x02000015 RID: 21
	[DataContract(Namespace = "http://tpro.ca")]
	public class TreeNodeCollection<T> : IEnumerable<TreeNode<T>>, IEnumerable
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000032DC File Offset: 0x000014DC
		public TreeNodeCollection()
		{
			this.Nodes = new List<TreeNode<T>>();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000032EF File Offset: 0x000014EF
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000032F7 File Offset: 0x000014F7
		[DataMember]
		public IList<TreeNode<T>> Nodes { get; set; }

		// Token: 0x1700001F RID: 31
		public TreeNode<T> this[int index]
		{
			get
			{
				return this.Nodes[index];
			}
			set
			{
				this.Nodes[index] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000331D File Offset: 0x0000151D
		public int Count
		{
			get
			{
				return this.Nodes.Count;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000332A File Offset: 0x0000152A
		public IEnumerator<TreeNode<T>> GetEnumerator()
		{
			return this.Nodes.GetEnumerator();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000332A File Offset: 0x0000152A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Nodes.GetEnumerator();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003337 File Offset: 0x00001537
		public void AddNode(TreeNode<T> node)
		{
			this.Nodes.Add(node);
		}
	}
}
