using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000655 RID: 1621
	internal class AugmentedNode
	{
		// Token: 0x06003F66 RID: 16230 RVA: 0x001226F7 File Offset: 0x001208F7
		internal AugmentedNode(int id, Node node) : this(id, node, new List<AugmentedNode>())
		{
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x00122708 File Offset: 0x00120908
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x00122794 File Offset: 0x00120994
		internal int Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x0012279C File Offset: 0x0012099C
		internal Node Node
		{
			get
			{
				return this.m_node;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x001227A4 File Offset: 0x001209A4
		internal AugmentedNode Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x001227AC File Offset: 0x001209AC
		internal List<AugmentedNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x001227B4 File Offset: 0x001209B4
		internal List<JoinEdge> JoinEdges
		{
			get
			{
				return this.m_joinEdges;
			}
		}

		// Token: 0x040017A5 RID: 6053
		private readonly int m_id;

		// Token: 0x040017A6 RID: 6054
		private readonly Node m_node;

		// Token: 0x040017A7 RID: 6055
		protected AugmentedNode m_parent;

		// Token: 0x040017A8 RID: 6056
		private readonly List<AugmentedNode> m_children;

		// Token: 0x040017A9 RID: 6057
		private readonly List<JoinEdge> m_joinEdges = new List<JoinEdge>();
	}
}
