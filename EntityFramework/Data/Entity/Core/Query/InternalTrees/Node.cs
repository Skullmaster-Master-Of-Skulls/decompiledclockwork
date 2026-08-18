using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200063C RID: 1596
	internal class Node
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x0011F8D0 File Offset: 0x0011DAD0
		internal Node(int nodeId, Op op, List<Node> children)
		{
			this.m_id = nodeId;
			this.Op = op;
			this.m_children = children;
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0011F8ED File Offset: 0x0011DAED
		internal Node(Op op, params Node[] children) : this(-1, op, new List<Node>(children))
		{
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x0011F8FD File Offset: 0x0011DAFD
		internal List<Node> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x0011F905 File Offset: 0x0011DB05
		// (set) Token: 0x06003EB2 RID: 16050 RVA: 0x0011F90D File Offset: 0x0011DB0D
		internal Op Op { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003EB3 RID: 16051 RVA: 0x0011F916 File Offset: 0x0011DB16
		// (set) Token: 0x06003EB4 RID: 16052 RVA: 0x0011F924 File Offset: 0x0011DB24
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

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003EB5 RID: 16053 RVA: 0x0011F933 File Offset: 0x0011DB33
		internal bool HasChild0
		{
			get
			{
				return this.m_children.Count > 0;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x0011F943 File Offset: 0x0011DB43
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x0011F951 File Offset: 0x0011DB51
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

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x0011F960 File Offset: 0x0011DB60
		internal bool HasChild1
		{
			get
			{
				return this.m_children.Count > 1;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x0011F970 File Offset: 0x0011DB70
		// (set) Token: 0x06003EBA RID: 16058 RVA: 0x0011F97E File Offset: 0x0011DB7E
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

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x0011F98D File Offset: 0x0011DB8D
		internal Node Child3
		{
			get
			{
				return this.m_children[3];
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x0011F99B File Offset: 0x0011DB9B
		internal bool HasChild2
		{
			get
			{
				return this.m_children.Count > 2;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x0011F9AB File Offset: 0x0011DBAB
		internal bool HasChild3
		{
			get
			{
				return this.m_children.Count > 3;
			}
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x0011F9BC File Offset: 0x0011DBBC
		internal bool IsEquivalent(Node other)
		{
			if (this.Children.Count != other.Children.Count)
			{
				return false;
			}
			bool? flag = new bool?(this.Op.IsEquivalent(other.Op));
			if (flag != true)
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

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x0011FA4F File Offset: 0x0011DC4F
		internal bool IsNodeInfoInitialized
		{
			get
			{
				return this.m_nodeInfo != null;
			}
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x0011FA5D File Offset: 0x0011DC5D
		internal NodeInfo GetNodeInfo(Command command)
		{
			if (this.m_nodeInfo == null)
			{
				this.InitializeNodeInfo(command);
			}
			return this.m_nodeInfo;
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0011FA74 File Offset: 0x0011DC74
		internal ExtendedNodeInfo GetExtendedNodeInfo(Command command)
		{
			if (this.m_nodeInfo == null)
			{
				this.InitializeNodeInfo(command);
			}
			return this.m_nodeInfo as ExtendedNodeInfo;
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0011FA9D File Offset: 0x0011DC9D
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

		// Token: 0x04001776 RID: 6006
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
		private readonly int m_id;

		// Token: 0x04001777 RID: 6007
		private readonly List<Node> m_children;

		// Token: 0x04001778 RID: 6008
		private NodeInfo m_nodeInfo;
	}
}
