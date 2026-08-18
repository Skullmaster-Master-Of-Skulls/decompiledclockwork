using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000670 RID: 1648
	internal class GroupAggregateVarComputationTranslator : BasicOpVisitorOfNode
	{
		// Token: 0x06004054 RID: 16468 RVA: 0x0012745C File Offset: 0x0012565C
		private GroupAggregateVarComputationTranslator(Command command, GroupAggregateVarInfoManager groupAggregateVarInfoManager)
		{
			this._command = command;
			this._groupAggregateVarInfoManager = groupAggregateVarInfoManager;
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x00127474 File Offset: 0x00125674
		public static bool TryTranslateOverGroupAggregateVar(Node subtree, bool isVarDefinition, Command command, GroupAggregateVarInfoManager groupAggregateVarInfoManager, out GroupAggregateVarInfo groupAggregateVarInfo, out Node templateNode, out bool isUnnested)
		{
			GroupAggregateVarComputationTranslator groupAggregateVarComputationTranslator = new GroupAggregateVarComputationTranslator(command, groupAggregateVarInfoManager);
			Node node = subtree;
			SoftCastOp softCastOp = null;
			if (node.Op.OpType == OpType.SoftCast)
			{
				softCastOp = (SoftCastOp)node.Op;
				node = node.Child0;
			}
			bool flag;
			if (node.Op.OpType == OpType.Collect)
			{
				templateNode = groupAggregateVarComputationTranslator.VisitCollect(node);
				flag = true;
			}
			else
			{
				templateNode = groupAggregateVarComputationTranslator.VisitNode(node);
				flag = false;
			}
			groupAggregateVarInfo = groupAggregateVarComputationTranslator._targetGroupAggregateVarInfo;
			isUnnested = groupAggregateVarComputationTranslator._isUnnested;
			if (groupAggregateVarComputationTranslator._targetGroupAggregateVarInfo == null || templateNode == null)
			{
				return false;
			}
			if (softCastOp != null)
			{
				SoftCastOp op;
				if (flag || (!isVarDefinition && AggregatePushdownUtil.IsVarRefOverGivenVar(templateNode, groupAggregateVarComputationTranslator._targetGroupAggregateVarInfo.GroupAggregateVar)))
				{
					op = command.CreateSoftCastOp(TypeHelpers.GetEdmType<CollectionType>(softCastOp.Type).TypeUsage);
				}
				else
				{
					op = softCastOp;
				}
				templateNode = command.CreateNode(op, templateNode);
			}
			return true;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x00127543 File Offset: 0x00125743
		public override Node Visit(VarRefOp op, Node n)
		{
			return this.TranslateOverGroupAggregateVar(op.Var, null);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x00127554 File Offset: 0x00125754
		public override Node Visit(PropertyOp op, Node n)
		{
			if (n.Child0.Op.OpType != OpType.VarRef)
			{
				return base.Visit(op, n);
			}
			VarRefOp varRefOp = (VarRefOp)n.Child0.Op;
			return this.TranslateOverGroupAggregateVar(varRefOp.Var, op.PropertyInfo);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x001275A0 File Offset: 0x001257A0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node VisitCollect(Node n)
		{
			Node child = n.Child0;
			Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>();
			while (child.Child0.Op.OpType == OpType.Project)
			{
				child = child.Child0;
				if (this.VisitDefault(child.Child1) == null)
				{
					return null;
				}
				foreach (Node node in child.Child1.Children)
				{
					if (GroupAggregateVarComputationTranslator.IsConstant(node.Child0))
					{
						dictionary.Add(((VarDefOp)node.Op).Var, node.Child0);
					}
				}
			}
			if (child.Child0.Op.OpType != OpType.Unnest)
			{
				return null;
			}
			UnnestOp unnestOp = (UnnestOp)child.Child0.Op;
			GroupAggregateVarRefInfo groupAggregateVarRefInfo;
			if (!this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(unnestOp.Var, out groupAggregateVarRefInfo))
			{
				return null;
			}
			if (this._targetGroupAggregateVarInfo == null)
			{
				this._targetGroupAggregateVarInfo = groupAggregateVarRefInfo.GroupAggregateVarInfo;
			}
			else if (this._targetGroupAggregateVarInfo != groupAggregateVarRefInfo.GroupAggregateVarInfo)
			{
				return null;
			}
			if (!this._isUnnested)
			{
				return null;
			}
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)n.Child0.Op;
			PlanCompiler.Assert(physicalProjectOp.Outputs.Count == 1, "Physical project should only have one output at this stage");
			Var var = physicalProjectOp.Outputs[0];
			Node node2 = this.TranslateOverGroupAggregateVar(var, null);
			if (node2 != null)
			{
				this._isUnnested = true;
				return node2;
			}
			Node result;
			if (dictionary.TryGetValue(var, out result))
			{
				this._isUnnested = true;
				return result;
			}
			return null;
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x00127734 File Offset: 0x00125934
		private static bool IsConstant(Node node)
		{
			Node node2 = node;
			while (node2.Op.OpType == OpType.Cast)
			{
				node2 = node2.Child0;
			}
			return PlanCompilerUtil.IsConstantBaseOp(node2.Op.OpType);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x0012776C File Offset: 0x0012596C
		private Node TranslateOverGroupAggregateVar(Var var, EdmMember property)
		{
			GroupAggregateVarRefInfo groupAggregateVarRefInfo;
			EdmMember edmMember;
			if (this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(var, out groupAggregateVarRefInfo))
			{
				edmMember = property;
			}
			else
			{
				if (!this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(var, property, out groupAggregateVarRefInfo))
				{
					return null;
				}
				edmMember = null;
			}
			if (this._targetGroupAggregateVarInfo == null)
			{
				this._targetGroupAggregateVarInfo = groupAggregateVarRefInfo.GroupAggregateVarInfo;
				this._isUnnested = groupAggregateVarRefInfo.IsUnnested;
			}
			else if (this._targetGroupAggregateVarInfo != groupAggregateVarRefInfo.GroupAggregateVarInfo || this._isUnnested != groupAggregateVarRefInfo.IsUnnested)
			{
				return null;
			}
			Node node = groupAggregateVarRefInfo.Computation;
			if (edmMember != null)
			{
				node = this._command.CreateNode(this._command.CreatePropertyOp(edmMember), node);
			}
			return node;
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x00127808 File Offset: 0x00125A08
		protected override Node VisitDefault(Node n)
		{
			List<Node> list = new List<Node>(n.Children.Count);
			bool flag = false;
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = base.VisitNode(n.Children[i]);
				if (node == null)
				{
					return null;
				}
				if (!flag && !object.ReferenceEquals(n.Children[i], node))
				{
					flag = true;
				}
				list.Add(node);
			}
			if (!flag)
			{
				return n;
			}
			return this._command.CreateNode(n.Op, list);
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x0012788D File Offset: 0x00125A8D
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return null;
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x00127890 File Offset: 0x00125A90
		public override Node Visit(AggregateOp op, Node n)
		{
			return null;
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x00127893 File Offset: 0x00125A93
		public override Node Visit(CollectOp op, Node n)
		{
			return null;
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x00127896 File Offset: 0x00125A96
		public override Node Visit(ElementOp op, Node n)
		{
			return null;
		}

		// Token: 0x040017FC RID: 6140
		private GroupAggregateVarInfo _targetGroupAggregateVarInfo;

		// Token: 0x040017FD RID: 6141
		private bool _isUnnested;

		// Token: 0x040017FE RID: 6142
		private readonly Command _command;

		// Token: 0x040017FF RID: 6143
		private readonly GroupAggregateVarInfoManager _groupAggregateVarInfoManager;
	}
}
