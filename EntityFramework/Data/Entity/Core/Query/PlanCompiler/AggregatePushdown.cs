using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200064D RID: 1613
	internal class AggregatePushdown
	{
		// Token: 0x06003F21 RID: 16161 RVA: 0x00120B80 File Offset: 0x0011ED80
		private AggregatePushdown(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00120B90 File Offset: 0x0011ED90
		internal static void Process(PlanCompiler planCompilerState)
		{
			AggregatePushdown aggregatePushdown = new AggregatePushdown(planCompilerState.Command);
			aggregatePushdown.Process();
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00120BB0 File Offset: 0x0011EDB0
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

		// Token: 0x06003F24 RID: 16164 RVA: 0x00120C50 File Offset: 0x0011EE50
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GroupByInto")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003F25 RID: 16165 RVA: 0x00120DDC File Offset: 0x0011EFDC
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

		// Token: 0x06003F26 RID: 16166 RVA: 0x00120E54 File Offset: 0x0011F054
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

		// Token: 0x06003F27 RID: 16167 RVA: 0x00120EE0 File Offset: 0x0011F0E0
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

		// Token: 0x04001793 RID: 6035
		private readonly Command m_command;

		// Token: 0x04001794 RID: 6036
		private TryGetValue m_tryGetParent;
	}
}
