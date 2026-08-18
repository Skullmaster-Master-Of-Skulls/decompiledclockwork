using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200067D RID: 1661
	internal class JoinEdge
	{
		// Token: 0x060040F7 RID: 16631 RVA: 0x0012BE4C File Offset: 0x0012A04C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private JoinEdge(AugmentedTableNode left, AugmentedTableNode right, AugmentedJoinNode joinNode, JoinKind joinKind, List<ColumnVar> leftVars, List<ColumnVar> rightVars)
		{
			this.m_left = left;
			this.m_right = right;
			this.JoinKind = joinKind;
			this.m_joinNode = joinNode;
			this.m_leftVars = leftVars;
			this.m_rightVars = rightVars;
			PlanCompiler.Assert(this.m_leftVars.Count == this.m_rightVars.Count, string.Concat(new object[]
			{
				"Count mismatch: ",
				this.m_leftVars.Count,
				",",
				this.m_rightVars.Count
			}));
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x0012BEEC File Offset: 0x0012A0EC
		internal AugmentedTableNode Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x0012BEF4 File Offset: 0x0012A0F4
		internal AugmentedTableNode Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x0012BEFC File Offset: 0x0012A0FC
		internal AugmentedJoinNode JoinNode
		{
			get
			{
				return this.m_joinNode;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x0012BF04 File Offset: 0x0012A104
		// (set) Token: 0x060040FC RID: 16636 RVA: 0x0012BF0C File Offset: 0x0012A10C
		internal JoinKind JoinKind { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x0012BF15 File Offset: 0x0012A115
		internal List<ColumnVar> LeftVars
		{
			get
			{
				return this.m_leftVars;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x0012BF1D File Offset: 0x0012A11D
		internal List<ColumnVar> RightVars
		{
			get
			{
				return this.m_rightVars;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x0012BF25 File Offset: 0x0012A125
		internal bool IsEliminated
		{
			get
			{
				return this.Left.IsEliminated || this.Right.IsEliminated;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x0012BF44 File Offset: 0x0012A144
		internal bool RestrictedElimination
		{
			get
			{
				return this.m_joinNode != null && (this.m_joinNode.OtherPredicate != null || this.m_left.LastVisibleId < this.m_joinNode.Id || this.m_right.LastVisibleId < this.m_joinNode.Id);
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0012BF9C File Offset: 0x0012A19C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal static JoinEdge CreateJoinEdge(AugmentedTableNode left, AugmentedTableNode right, AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			List<ColumnVar> list = new List<ColumnVar>();
			List<ColumnVar> list2 = new List<ColumnVar>();
			list.Add(leftVar);
			list2.Add(rightVar);
			OpType opType = joinNode.Node.Op.OpType;
			PlanCompiler.Assert(opType == OpType.LeftOuterJoin || opType == OpType.InnerJoin, "Unexpected join type for join edge: " + opType);
			JoinKind joinKind = (opType == OpType.LeftOuterJoin) ? JoinKind.LeftOuter : JoinKind.Inner;
			return new JoinEdge(left, right, joinNode, joinKind, list, list2);
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x0012C010 File Offset: 0x0012A210
		internal static JoinEdge CreateTransitiveJoinEdge(AugmentedTableNode left, AugmentedTableNode right, JoinKind joinKind, List<ColumnVar> leftVars, List<ColumnVar> rightVars)
		{
			return new JoinEdge(left, right, null, joinKind, leftVars, rightVars);
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x0012C02B File Offset: 0x0012A22B
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

		// Token: 0x04001829 RID: 6185
		private readonly AugmentedTableNode m_left;

		// Token: 0x0400182A RID: 6186
		private readonly AugmentedTableNode m_right;

		// Token: 0x0400182B RID: 6187
		private readonly AugmentedJoinNode m_joinNode;

		// Token: 0x0400182C RID: 6188
		private readonly List<ColumnVar> m_leftVars;

		// Token: 0x0400182D RID: 6189
		private readonly List<ColumnVar> m_rightVars;
	}
}
