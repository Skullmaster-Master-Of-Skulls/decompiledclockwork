using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000052 RID: 82
	internal sealed class AugmentedJoinNode : AugmentedNode
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0001F35F File Offset: 0x0001D55F
		internal AugmentedJoinNode(int id, Node node, AugmentedNode leftChild, AugmentedNode rightChild, List<ColumnVar> leftVars, List<ColumnVar> rightVars, Node otherPredicate) : this(id, node, new List<AugmentedNode>(new AugmentedNode[]
		{
			leftChild,
			rightChild
		}))
		{
			this.m_otherPredicate = otherPredicate;
			this.m_rightVars = rightVars;
			this.m_leftVars = leftVars;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001F395 File Offset: 0x0001D595
		internal AugmentedJoinNode(int id, Node node, List<AugmentedNode> children) : base(id, node, children)
		{
			this.m_leftVars = new List<ColumnVar>();
			this.m_rightVars = new List<ColumnVar>();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001F3B6 File Offset: 0x0001D5B6
		internal Node OtherPredicate
		{
			get
			{
				return this.m_otherPredicate;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001F3BE File Offset: 0x0001D5BE
		internal List<ColumnVar> LeftVars
		{
			get
			{
				return this.m_leftVars;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001F3C6 File Offset: 0x0001D5C6
		internal List<ColumnVar> RightVars
		{
			get
			{
				return this.m_rightVars;
			}
		}

		// Token: 0x040007A2 RID: 1954
		private List<ColumnVar> m_leftVars;

		// Token: 0x040007A3 RID: 1955
		private List<ColumnVar> m_rightVars;

		// Token: 0x040007A4 RID: 1956
		private Node m_otherPredicate;
	}
}
