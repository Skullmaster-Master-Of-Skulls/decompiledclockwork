using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000050 RID: 80
	internal class AugmentedNode
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		internal AugmentedNode(int id, Node node) : this(id, node, new List<AugmentedNode>())
		{
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001F200 File Offset: 0x0001D400
		internal AugmentedNode(int id, Node node, List<AugmentedNode> children)
		{
			this.m_id = id;
			this.m_node = node;
			this.m_children = children;
			PlanCompiler.Assert(children != null, "null children (gasp!)");
			foreach (AugmentedNode augmentedNode in this.m_children)
			{
				augmentedNode.m_parent = this;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001F288 File Offset: 0x0001D488
		internal int Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001F290 File Offset: 0x0001D490
		internal Node Node
		{
			get
			{
				return this.m_node;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001F298 File Offset: 0x0001D498
		internal AugmentedNode Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001F2A0 File Offset: 0x0001D4A0
		internal List<AugmentedNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		internal List<JoinEdge> JoinEdges
		{
			get
			{
				return this.m_joinEdges;
			}
		}

		// Token: 0x04000798 RID: 1944
		private int m_id;

		// Token: 0x04000799 RID: 1945
		private Node m_node;

		// Token: 0x0400079A RID: 1946
		protected AugmentedNode m_parent;

		// Token: 0x0400079B RID: 1947
		private List<AugmentedNode> m_children;

		// Token: 0x0400079C RID: 1948
		private readonly List<JoinEdge> m_joinEdges = new List<JoinEdge>();
	}
}
