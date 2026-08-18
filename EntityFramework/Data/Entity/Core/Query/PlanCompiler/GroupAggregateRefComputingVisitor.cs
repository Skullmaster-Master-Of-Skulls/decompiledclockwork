using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200066F RID: 1647
	internal class GroupAggregateRefComputingVisitor : BasicOpVisitor
	{
		// Token: 0x0600404D RID: 16461 RVA: 0x0012711C File Offset: 0x0012531C
		internal static IEnumerable<GroupAggregateVarInfo> Process(Command itree, out TryGetValue tryGetParent)
		{
			GroupAggregateRefComputingVisitor groupAggregateRefComputingVisitor = new GroupAggregateRefComputingVisitor(itree);
			groupAggregateRefComputingVisitor.VisitNode(itree.Root);
			tryGetParent = new TryGetValue(groupAggregateRefComputingVisitor._childToParent.TryGetValue);
			return groupAggregateRefComputingVisitor._groupAggregateVarInfoManager.GroupAggregateVarInfos;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0012715A File Offset: 0x0012535A
		private GroupAggregateRefComputingVisitor(Command itree)
		{
			this._command = itree;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x00127180 File Offset: 0x00125380
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

		// Token: 0x06004050 RID: 16464 RVA: 0x00127250 File Offset: 0x00125450
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

		// Token: 0x06004051 RID: 16465 RVA: 0x001272F8 File Offset: 0x001254F8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004052 RID: 16466 RVA: 0x00127368 File Offset: 0x00125568
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004053 RID: 16467 RVA: 0x001273EC File Offset: 0x001255EC
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

		// Token: 0x040017F9 RID: 6137
		private readonly Command _command;

		// Token: 0x040017FA RID: 6138
		private readonly GroupAggregateVarInfoManager _groupAggregateVarInfoManager = new GroupAggregateVarInfoManager();

		// Token: 0x040017FB RID: 6139
		private readonly Dictionary<Node, Node> _childToParent = new Dictionary<Node, Node>();
	}
}
