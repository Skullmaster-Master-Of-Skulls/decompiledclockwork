using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000054 RID: 84
	internal class JoinEdge
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		private JoinEdge(AugmentedTableNode left, AugmentedTableNode right, AugmentedJoinNode joinNode, JoinKind joinKind, List<ColumnVar> leftVars, List<ColumnVar> rightVars)
		{
			this.m_left = left;
			this.m_right = right;
			this.m_joinKind = joinKind;
			this.m_joinNode = joinNode;
			this.m_leftVars = leftVars;
			this.m_rightVars = rightVars;
			PlanCompiler.Assert(this.m_leftVars.Count == this.m_rightVars.Count, "Count mismatch: " + this.m_leftVars.Count.ToString() + "," + this.m_rightVars.Count.ToString());
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001F462 File Offset: 0x0001D662
		internal AugmentedTableNode Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001F46A File Offset: 0x0001D66A
		internal AugmentedTableNode Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001F472 File Offset: 0x0001D672
		internal AugmentedJoinNode JoinNode
		{
			get
			{
				return this.m_joinNode;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001F47A File Offset: 0x0001D67A
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001F482 File Offset: 0x0001D682
		internal JoinKind JoinKind
		{
			get
			{
				return this.m_joinKind;
			}
			set
			{
				this.m_joinKind = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001F48B File Offset: 0x0001D68B
		internal List<ColumnVar> LeftVars
		{
			get
			{
				return this.m_leftVars;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001F493 File Offset: 0x0001D693
		internal List<ColumnVar> RightVars
		{
			get
			{
				return this.m_rightVars;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001F49B File Offset: 0x0001D69B
		internal bool IsEliminated
		{
			get
			{
				return this.Left.IsEliminated || this.Right.IsEliminated;
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		internal static JoinEdge CreateJoinEdge(AugmentedTableNode left, AugmentedTableNode right, AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			List<ColumnVar> list = new List<ColumnVar>();
			List<ColumnVar> list2 = new List<ColumnVar>();
			list.Add(leftVar);
			list2.Add(rightVar);
			OpType opType = joinNode.Node.Op.OpType;
			PlanCompiler.Assert(opType == OpType.LeftOuterJoin || opType == OpType.InnerJoin, "Unexpected join type for join edge: " + opType.ToString());
			JoinKind joinKind = (opType == OpType.LeftOuterJoin) ? JoinKind.LeftOuter : JoinKind.Inner;
			return new JoinEdge(left, right, joinNode, joinKind, list, list2);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001F534 File Offset: 0x0001D734
		internal static JoinEdge CreateTransitiveJoinEdge(AugmentedTableNode left, AugmentedTableNode right, JoinKind joinKind, List<ColumnVar> leftVars, List<ColumnVar> rightVars)
		{
			return new JoinEdge(left, right, null, joinKind, leftVars, rightVars);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001F54F File Offset: 0x0001D74F
		internal bool AddCondition(AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			if (joinNode != this.m_joinNode)
			{
				return false;
			}
			this.m_leftVars.Add(leftVar);
			this.m_rightVars.Add(rightVar);
			return true;
		}

		// Token: 0x040007A8 RID: 1960
		private AugmentedTableNode m_left;

		// Token: 0x040007A9 RID: 1961
		private AugmentedTableNode m_right;

		// Token: 0x040007AA RID: 1962
		private AugmentedJoinNode m_joinNode;

		// Token: 0x040007AB RID: 1963
		private JoinKind m_joinKind;

		// Token: 0x040007AC RID: 1964
		private List<ColumnVar> m_leftVars;

		// Token: 0x040007AD RID: 1965
		private List<ColumnVar> m_rightVars;
	}
}
