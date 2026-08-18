using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200008D RID: 141
	internal class GroupAggregateRefComputingVisitor : BasicOpVisitor
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x000341CC File Offset: 0x000323CC
		internal static IEnumerable<GroupAggregateVarInfo> Process(Command itree, out TryGetValue tryGetParent)
		{
			GroupAggregateRefComputingVisitor groupAggregateRefComputingVisitor = new GroupAggregateRefComputingVisitor(itree);
			groupAggregateRefComputingVisitor.VisitNode(itree.Root);
			tryGetParent = new TryGetValue(groupAggregateRefComputingVisitor._childToParent.TryGetValue);
			return groupAggregateRefComputingVisitor._groupAggregateVarInfoManager.GroupAggregateVarInfos;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0003420B File Offset: 0x0003240B
		private GroupAggregateRefComputingVisitor(Command itree)
		{
			this._command = itree;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00034230 File Offset: 0x00032430
		public override void Visit(VarDefOp op, Node n)
		{
			this.VisitDefault(n);
			Node child = n.Child0;
			Op op2 = child.Op;
			GroupAggregateVarInfo groupAggregateVarInfo;
			Node computationTemplate;
			bool isUnnested;
			if (GroupAggregateVarComputationTranslator.TryTranslateOverGroupAggregateVar(child, true, this._command, this._groupAggregateVarInfoManager, out groupAggregateVarInfo, out computationTemplate, out isUnnested))
			{
				this._groupAggregateVarInfoManager.Add(op.Var, groupAggregateVarInfo, computationTemplate, isUnnested);
				return;
			}
			if (op2.OpType == OpType.NewRecord)
			{
				NewRecordOp newRecordOp = (NewRecordOp)op2;
				for (int i = 0; i < child.Children.Count; i++)
				{
					Node subtree = child.Children[i];
					if (GroupAggregateVarComputationTranslator.TryTranslateOverGroupAggregateVar(subtree, true, this._command, this._groupAggregateVarInfoManager, out groupAggregateVarInfo, out computationTemplate, out isUnnested))
					{
						this._groupAggregateVarInfoManager.Add(op.Var, groupAggregateVarInfo, computationTemplate, isUnnested, newRecordOp.Properties[i]);
					}
				}
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00034300 File Offset: 0x00032500
		public override void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
			foreach (Node node in n.Child3.Children)
			{
				Var var = ((VarDefOp)node.Op).Var;
				GroupAggregateVarRefInfo groupAggregateVarRefInfo;
				if (!this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(var, out groupAggregateVarRefInfo))
				{
					this._groupAggregateVarInfoManager.Add(var, new GroupAggregateVarInfo(n, var), this._command.CreateNode(this._command.CreateVarRefOp(var)), false);
				}
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000343A8 File Offset: 0x000325A8
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitDefault(n);
			GroupAggregateVarRefInfo groupAggregateVarRefInfo;
			if (this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(op.Var, out groupAggregateVarRefInfo))
			{
				PlanCompiler.Assert(op.Table.Columns.Count == 1, "Expected one column before NTE");
				this._groupAggregateVarInfoManager.Add(op.Table.Columns[0], groupAggregateVarRefInfo.GroupAggregateVarInfo, groupAggregateVarRefInfo.Computation, true);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00034418 File Offset: 0x00032618
		public override void Visit(FunctionOp op, Node n)
		{
			this.VisitDefault(n);
			if (!PlanCompilerUtil.IsCollectionAggregateFunction(op, n))
			{
				return;
			}
			PlanCompiler.Assert(n.Children.Count == 1, "Aggregate Function must have one argument");
			Node child = n.Child0;
			GroupAggregateVarInfo groupAggregateVarInfo;
			Node node;
			bool flag;
			if (GroupAggregateVarComputationTranslator.TryTranslateOverGroupAggregateVar(n.Child0, false, this._command, this._groupAggregateVarInfoManager, out groupAggregateVarInfo, out node, out flag) && (flag || AggregatePushdownUtil.IsVarRefOverGivenVar(node, groupAggregateVarInfo.GroupAggregateVar)))
			{
				groupAggregateVarInfo.CandidateAggregateNodes.Add(new KeyValuePair<Node, Node>(n, node));
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0003449C File Offset: 0x0003269C
		protected override void VisitDefault(Node n)
		{
			this.VisitChildren(n);
			foreach (Node node in n.Children)
			{
				if (node.Op.Arity != 0)
				{
					this._childToParent.Add(node, n);
				}
			}
		}

		// Token: 0x0400089C RID: 2204
		private readonly Command _command;

		// Token: 0x0400089D RID: 2205
		private readonly GroupAggregateVarInfoManager _groupAggregateVarInfoManager = new GroupAggregateVarInfoManager();

		// Token: 0x0400089E RID: 2206
		private readonly Dictionary<Node, Node> _childToParent = new Dictionary<Node, Node>();
	}
}
