using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000656 RID: 1622
	internal sealed class AugmentedJoinNode : AugmentedNode
	{
		// Token: 0x06003F6D RID: 16237 RVA: 0x001227BC File Offset: 0x001209BC
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

		// Token: 0x06003F6E RID: 16238 RVA: 0x001227FF File Offset: 0x001209FF
		internal AugmentedJoinNode(int id, Node node, List<AugmentedNode> children) : base(id, node, children)
		{
			this.m_leftVars = new List<ColumnVar>();
			this.m_rightVars = new List<ColumnVar>();
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x00122820 File Offset: 0x00120A20
		internal Node OtherPredicate
		{
			get
			{
				return this.m_otherPredicate;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x00122828 File Offset: 0x00120A28
		internal List<ColumnVar> LeftVars
		{
			get
			{
				return this.m_leftVars;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x00122830 File Offset: 0x00120A30
		internal List<ColumnVar> RightVars
		{
			get
			{
				return this.m_rightVars;
			}
		}

		// Token: 0x040017AA RID: 6058
		private readonly List<ColumnVar> m_leftVars;

		// Token: 0x040017AB RID: 6059
		private readonly List<ColumnVar> m_rightVars;

		// Token: 0x040017AC RID: 6060
		private readonly Node m_otherPredicate;
	}
}
