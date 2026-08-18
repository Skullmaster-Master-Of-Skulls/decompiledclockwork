using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200008F RID: 143
	internal class AggregatePushdown
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00034531 File Offset: 0x00032731
		private AggregatePushdown(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00034540 File Offset: 0x00032740
		internal static void Process(PlanCompiler planCompilerState)
		{
			AggregatePushdown aggregatePushdown = new AggregatePushdown(planCompilerState.Command);
			aggregatePushdown.Process();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00034560 File Offset: 0x00032760
		private void Process()
		{
			IEnumerable<GroupAggregateVarInfo> enumerable = GroupAggregateRefComputingVisitor.Process(this.m_command, out this.m_tryGetParent);
			foreach (GroupAggregateVarInfo groupAggregateVarInfo in enumerable)
			{
				if (groupAggregateVarInfo.HasCandidateAggregateNodes)
				{
					foreach (KeyValuePair<Node, Node> candidate in groupAggregateVarInfo.CandidateAggregateNodes)
					{
						this.TryProcessCandidate(candidate, groupAggregateVarInfo);
					}
				}
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00034600 File Offset: 0x00032800
		private void TryProcessCandidate(KeyValuePair<Node, Node> candidate, GroupAggregateVarInfo groupAggregateVarInfo)
		{
			Node definingGroupNode = groupAggregateVarInfo.DefiningGroupNode;
			IList<Node> list;
			IList<Node> list2;
			this.FindPathsToLeastCommonAncestor(candidate.Key, definingGroupNode, out list, out list2);
			if (!AggregatePushdown.AreAllNodesSupportedForPropagation(list2))
			{
				return;
			}
			GroupByIntoOp groupByIntoOp = (GroupByIntoOp)definingGroupNode.Op;
			PlanCompiler.Assert(groupByIntoOp.Inputs.Count == 1, "There should be one input var to GroupByInto at this stage");
			Var first = groupByIntoOp.Inputs.First;
			FunctionOp functionOp = (FunctionOp)candidate.Key.Op;
			Node node = OpCopier.Copy(this.m_command, candidate.Value);
			Dictionary<Var, Var> dictionary = new Dictionary<Var, Var>(1);
			dictionary.Add(groupAggregateVarInfo.GroupAggregateVar, first);
			VarRemapper varRemapper = new VarRemapper(this.m_command, dictionary);
			varRemapper.RemapSubtree(node);
			Node definingExpr = this.m_command.CreateNode(this.m_command.CreateAggregateOp(functionOp.Function, false), node);
			Var v;
			Node item = this.m_command.CreateVarDefNode(definingExpr, out v);
			definingGroupNode.Child2.Children.Add(item);
			GroupByIntoOp groupByIntoOp2 = (GroupByIntoOp)definingGroupNode.Op;
			groupByIntoOp2.Outputs.Set(v);
			for (int i = 0; i < list2.Count; i++)
			{
				Node node2 = list2[i];
				if (node2.Op.OpType == OpType.Project)
				{
					ProjectOp projectOp = (ProjectOp)node2.Op;
					projectOp.Outputs.Set(v);
				}
			}
			candidate.Key.Op = this.m_command.CreateVarRefOp(v);
			candidate.Key.Children.Clear();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0003478C File Offset: 0x0003298C
		private static bool AreAllNodesSupportedForPropagation(IList<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				if (node.Op.OpType != OpType.Project && node.Op.OpType != OpType.Filter && node.Op.OpType != OpType.ConstrainedSort)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00034804 File Offset: 0x00032A04
		private void FindPathsToLeastCommonAncestor(Node node1, Node node2, out IList<Node> ancestors1, out IList<Node> ancestors2)
		{
			ancestors1 = this.FindAncestors(node1);
			ancestors2 = this.FindAncestors(node2);
			int num = ancestors1.Count - 1;
			int num2 = ancestors2.Count - 1;
			while (ancestors1[num] == ancestors2[num2])
			{
				num--;
				num2--;
			}
			for (int i = ancestors1.Count - 1; i > num; i--)
			{
				ancestors1.RemoveAt(i);
			}
			for (int j = ancestors2.Count - 1; j > num2; j--)
			{
				ancestors2.RemoveAt(j);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00034890 File Offset: 0x00032A90
		private IList<Node> FindAncestors(Node node)
		{
			List<Node> list = new List<Node>();
			Node key = node;
			Node node2;
			while (this.m_tryGetParent(key, out node2))
			{
				list.Add(node2);
				key = node2;
			}
			return list;
		}

		// Token: 0x0400089F RID: 2207
		private readonly Command m_command;

		// Token: 0x040008A0 RID: 2208
		private TryGetValue m_tryGetParent;
	}
}
