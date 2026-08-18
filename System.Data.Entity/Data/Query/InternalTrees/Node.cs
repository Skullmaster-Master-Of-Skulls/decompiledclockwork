using System;
using System.Collections.Generic;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B8 RID: 184
	internal class Node
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		internal Node(int nodeId, Op op, List<Node> children)
		{
			this.m_id = nodeId;
			this.m_op = op;
			this.m_children = children;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003ACBD File Offset: 0x00038EBD
		internal Node(Op op, params Node[] children) : this(-1, op, new List<Node>(children))
		{
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0003ACCD File Offset: 0x00038ECD
		internal List<Node> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0003ACD5 File Offset: 0x00038ED5
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0003ACDD File Offset: 0x00038EDD
		internal Op Op
		{
			get
			{
				return this.m_op;
			}
			set
			{
				this.m_op = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0003ACE6 File Offset: 0x00038EE6
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0003ACF4 File Offset: 0x00038EF4
		internal Node Child0
		{
			get
			{
				return this.m_children[0];
			}
			set
			{
				this.m_children[0] = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0003AD03 File Offset: 0x00038F03
		internal bool HasChild0
		{
			get
			{
				return this.m_children.Count > 0;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0003AD13 File Offset: 0x00038F13
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0003AD21 File Offset: 0x00038F21
		internal Node Child1
		{
			get
			{
				return this.m_children[1];
			}
			set
			{
				this.m_children[1] = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0003AD30 File Offset: 0x00038F30
		internal bool HasChild1
		{
			get
			{
				return this.m_children.Count > 1;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0003AD40 File Offset: 0x00038F40
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0003AD4E File Offset: 0x00038F4E
		internal Node Child2
		{
			get
			{
				return this.m_children[2];
			}
			set
			{
				this.m_children[2] = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0003AD5D File Offset: 0x00038F5D
		internal Node Child3
		{
			get
			{
				return this.m_children[3];
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0003AD6B File Offset: 0x00038F6B
		internal bool HasChild2
		{
			get
			{
				return this.m_children.Count > 2;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0003AD7B File Offset: 0x00038F7B
		internal bool HasChild3
		{
			get
			{
				return this.m_children.Count > 3;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003AD8C File Offset: 0x00038F8C
		internal bool IsEquivalent(Node other)
		{
			if (this.Children.Count != other.Children.Count)
			{
				return false;
			}
			bool? flag = new bool?(this.Op.IsEquivalent(other.Op));
			bool? flag2 = flag;
			bool flag3 = true;
			if (!(flag2.GetValueOrDefault() == flag3 & flag2 != null))
			{
				return false;
			}
			for (int i = 0; i < this.Children.Count; i++)
			{
				if (!this.Children[i].IsEquivalent(other.Children[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0003AE1D File Offset: 0x0003901D
		internal bool IsNodeInfoInitialized
		{
			get
			{
				return this.m_nodeInfo != null;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0003AE28 File Offset: 0x00039028
		internal NodeInfo GetNodeInfo(Command command)
		{
			if (this.m_nodeInfo == null)
			{
				this.InitializeNodeInfo(command);
			}
			return this.m_nodeInfo;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003AE40 File Offset: 0x00039040
		internal ExtendedNodeInfo GetExtendedNodeInfo(Command command)
		{
			if (this.m_nodeInfo == null)
			{
				this.InitializeNodeInfo(command);
			}
			return this.m_nodeInfo as ExtendedNodeInfo;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003AE69 File Offset: 0x00039069
		private void InitializeNodeInfo(Command command)
		{
			if (this.Op.IsRelOp || this.Op.IsPhysicalOp)
			{
				this.m_nodeInfo = new ExtendedNodeInfo(command);
			}
			else
			{
				this.m_nodeInfo = new NodeInfo(command);
			}
			command.RecomputeNodeInfo(this);
		}

		// Token: 0x040008FB RID: 2299
		private int m_id;

		// Token: 0x040008FC RID: 2300
		private List<Node> m_children;

		// Token: 0x040008FD RID: 2301
		private Op m_op;

		// Token: 0x040008FE RID: 2302
		private NodeInfo m_nodeInfo;
	}
}
