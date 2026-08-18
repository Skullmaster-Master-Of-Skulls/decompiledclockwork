using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000609 RID: 1545
	internal class NodeInfoVisitor : BasicOpVisitorOfT<NodeInfo>
	{
		// Token: 0x06003CCF RID: 15567 RVA: 0x00119798 File Offset: 0x00117998
		internal void RecomputeNodeInfo(Node n)
		{
			if (n.IsNodeInfoInitialized)
			{
				NodeInfo nodeInfo = base.VisitNode(n);
				nodeInfo.ComputeHashValue(this.m_command, n);
			}
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x001197C2 File Offset: 0x001179C2
		internal NodeInfoVisitor(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x001197D1 File Offset: 0x001179D1
		private NodeInfo GetNodeInfo(Node n)
		{
			return n.GetNodeInfo(this.m_command);
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x001197DF File Offset: 0x001179DF
		private ExtendedNodeInfo GetExtendedNodeInfo(Node n)
		{
			return n.GetExtendedNodeInfo(this.m_command);
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x001197F0 File Offset: 0x001179F0
		private NodeInfo InitNodeInfo(Node n)
		{
			NodeInfo nodeInfo = this.GetNodeInfo(n);
			nodeInfo.Clear();
			return nodeInfo;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x0011980C File Offset: 0x00117A0C
		private ExtendedNodeInfo InitExtendedNodeInfo(Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.GetExtendedNodeInfo(n);
			extendedNodeInfo.Clear();
			return extendedNodeInfo;
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x00119828 File Offset: 0x00117A28
		protected override NodeInfo VisitDefault(Node n)
		{
			NodeInfo nodeInfo = this.InitNodeInfo(n);
			foreach (Node n2 in n.Children)
			{
				NodeInfo nodeInfo2 = this.GetNodeInfo(n2);
				nodeInfo.ExternalReferences.Or(nodeInfo2.ExternalReferences);
			}
			return nodeInfo;
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00119898 File Offset: 0x00117A98
		private static bool IsDefinitionNonNullable(Node definition, VarVec nonNullableInputs)
		{
			return definition.Op.OpType == OpType.Constant || definition.Op.OpType == OpType.InternalConstant || definition.Op.OpType == OpType.NullSentinel || (definition.Op.OpType == OpType.VarRef && nonNullableInputs.IsSet(((VarRefOp)definition.Op).Var));
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x001198F8 File Offset: 0x00117AF8
		public override NodeInfo Visit(VarRefOp op, Node n)
		{
			NodeInfo nodeInfo = this.InitNodeInfo(n);
			nodeInfo.ExternalReferences.Set(op.Var);
			return nodeInfo;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x0011991F File Offset: 0x00117B1F
		protected override NodeInfo VisitRelOpDefault(RelOp op, Node n)
		{
			return this.Unimplemented(n);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00119928 File Offset: 0x00117B28
		protected override NodeInfo VisitTableOp(ScanTableBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.LocalDefinitions.Or(op.Table.ReferencedColumns);
			extendedNodeInfo.Definitions.Or(op.Table.ReferencedColumns);
			if (op.Table.ReferencedColumns.Subsumes(op.Table.Keys))
			{
				extendedNodeInfo.Keys.InitFrom(op.Table.Keys);
			}
			extendedNodeInfo.NonNullableDefinitions.Or(op.Table.NonNullableColumns);
			extendedNodeInfo.NonNullableDefinitions.And(extendedNodeInfo.Definitions);
			return extendedNodeInfo;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x001199C4 File Offset: 0x00117BC4
		public override NodeInfo Visit(UnnestOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			foreach (Var v in op.Table.Columns)
			{
				extendedNodeInfo.LocalDefinitions.Set(v);
				extendedNodeInfo.Definitions.Set(v);
			}
			if (n.Child0.Op.OpType == OpType.VarDef && n.Child0.Child0.Op.OpType == OpType.Function && op.Table.Keys.Count > 0 && op.Table.ReferencedColumns.Subsumes(op.Table.Keys))
			{
				extendedNodeInfo.Keys.InitFrom(op.Table.Keys);
			}
			if (n.HasChild0)
			{
				NodeInfo nodeInfo = this.GetNodeInfo(n.Child0);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			}
			else
			{
				extendedNodeInfo.ExternalReferences.Set(op.Var);
			}
			return extendedNodeInfo;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x00119AE4 File Offset: 0x00117CE4
		internal static Dictionary<Var, Var> ComputeVarRemappings(Node varDefListNode)
		{
			Dictionary<Var, Var> dictionary = new Dictionary<Var, Var>();
			foreach (Node node in varDefListNode.Children)
			{
				VarRefOp varRefOp = node.Child0.Op as VarRefOp;
				if (varRefOp != null)
				{
					VarDefOp varDefOp = node.Op as VarDefOp;
					dictionary[varRefOp.Var] = varDefOp.Var;
				}
			}
			return dictionary;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00119B6C File Offset: 0x00117D6C
		public override NodeInfo Visit(ProjectOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			foreach (Var v in op.Outputs)
			{
				if (extendedNodeInfo2.Definitions.IsSet(v))
				{
					extendedNodeInfo.Definitions.Set(v);
				}
				else
				{
					extendedNodeInfo.ExternalReferences.Set(v);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(op.Outputs);
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			foreach (Node node in n.Child1.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				NodeInfo nodeInfo = this.GetNodeInfo(node.Child0);
				extendedNodeInfo.LocalDefinitions.Set(varDefOp.Var);
				extendedNodeInfo.ExternalReferences.Clear(varDefOp.Var);
				extendedNodeInfo.Definitions.Set(varDefOp.Var);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
				if (NodeInfoVisitor.IsDefinitionNonNullable(node.Child0, extendedNodeInfo.NonNullableVisibleDefinitions))
				{
					extendedNodeInfo.NonNullableDefinitions.Set(varDefOp.Var);
				}
			}
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.Keys.NoKeys = true;
			if (!extendedNodeInfo2.Keys.NoKeys)
			{
				VarVec varVec = this.m_command.CreateVarVec(extendedNodeInfo2.Keys.KeyVars);
				Dictionary<Var, Var> varMap = NodeInfoVisitor.ComputeVarRemappings(n.Child1);
				VarVec varVec2 = varVec.Remap(varMap);
				VarVec varSet = varVec2.Clone();
				VarVec other = this.m_command.CreateVarVec(op.Outputs);
				varVec2.Minus(other);
				if (varVec2.IsEmpty)
				{
					extendedNodeInfo.Keys.InitFrom(varSet);
				}
			}
			extendedNodeInfo.InitRowCountFrom(extendedNodeInfo2);
			return extendedNodeInfo;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x00119DA4 File Offset: 0x00117FA4
		public override NodeInfo Visit(FilterOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			NodeInfo nodeInfo = this.GetNodeInfo(n.Child1);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.MinRows = RowCount.Zero;
			ConstantPredicateOp constantPredicateOp = n.Child1.Op as ConstantPredicateOp;
			if (constantPredicateOp != null && constantPredicateOp.IsFalse)
			{
				extendedNodeInfo.MaxRows = RowCount.Zero;
			}
			else
			{
				extendedNodeInfo.MaxRows = extendedNodeInfo2.MaxRows;
			}
			return extendedNodeInfo;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x00119E84 File Offset: 0x00118084
		protected override NodeInfo VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			extendedNodeInfo.LocalDefinitions.InitFrom(extendedNodeInfo.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			foreach (Node node in n.Child1.Children)
			{
				NodeInfo nodeInfo = this.GetNodeInfo(node.Child0);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
				if (NodeInfoVisitor.IsDefinitionNonNullable(node.Child0, extendedNodeInfo2.NonNullableDefinitions))
				{
					extendedNodeInfo.NonNullableDefinitions.Set(((VarDefOp)node.Op).Var);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(op.Keys);
			for (int i = 2; i < n.Children.Count; i++)
			{
				foreach (Node node2 in n.Children[i].Children)
				{
					NodeInfo nodeInfo2 = this.GetNodeInfo(node2.Child0);
					extendedNodeInfo.ExternalReferences.Or(nodeInfo2.ExternalReferences);
				}
			}
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(op.Keys);
			extendedNodeInfo.MinRows = (op.Keys.IsEmpty ? RowCount.One : ((extendedNodeInfo2.MinRows == RowCount.One) ? RowCount.One : RowCount.Zero));
			extendedNodeInfo.MaxRows = (op.Keys.IsEmpty ? RowCount.One : extendedNodeInfo2.MaxRows);
			return extendedNodeInfo;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x0011A074 File Offset: 0x00118274
		public override NodeInfo Visit(CrossJoinOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			List<KeyVec> list = new List<KeyVec>();
			RowCount rowCount = RowCount.Zero;
			RowCount rowCount2 = RowCount.One;
			foreach (Node n2 in n.Children)
			{
				ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n2);
				extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
				extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
				list.Add(extendedNodeInfo2.Keys);
				extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo2.NonNullableDefinitions);
				if (extendedNodeInfo2.MaxRows > rowCount)
				{
					rowCount = extendedNodeInfo2.MaxRows;
				}
				if (extendedNodeInfo2.MinRows < rowCount2)
				{
					rowCount2 = extendedNodeInfo2.MinRows;
				}
			}
			extendedNodeInfo.Keys.InitFrom(list);
			extendedNodeInfo.SetRowCount(rowCount2, rowCount);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x0011A160 File Offset: 0x00118360
		protected override NodeInfo VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (op.OpType != OpType.InnerJoin && op.OpType != OpType.LeftOuterJoin && op.OpType != OpType.FullOuterJoin)
			{
				return this.Unimplemented(n);
			}
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			ExtendedNodeInfo extendedNodeInfo3 = this.GetExtendedNodeInfo(n.Child1);
			NodeInfo nodeInfo = this.GetNodeInfo(n.Child2);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo3.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo3.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys, extendedNodeInfo3.Keys);
			if (op.OpType == OpType.InnerJoin || op.OpType == OpType.LeftOuterJoin)
			{
				extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			}
			if (op.OpType == OpType.InnerJoin)
			{
				extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			}
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			RowCount minRows;
			RowCount maxRows;
			if (op.OpType == OpType.FullOuterJoin)
			{
				minRows = RowCount.Zero;
				maxRows = RowCount.Unbounded;
			}
			else
			{
				if (extendedNodeInfo2.MaxRows > RowCount.One || extendedNodeInfo3.MaxRows > RowCount.One)
				{
					maxRows = RowCount.Unbounded;
				}
				else
				{
					maxRows = RowCount.One;
				}
				if (op.OpType == OpType.LeftOuterJoin)
				{
					minRows = extendedNodeInfo2.MinRows;
				}
				else
				{
					minRows = RowCount.Zero;
				}
			}
			extendedNodeInfo.SetRowCount(minRows, maxRows);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x0011A2F0 File Offset: 0x001184F0
		protected override NodeInfo VisitApplyOp(ApplyBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			ExtendedNodeInfo extendedNodeInfo3 = this.GetExtendedNodeInfo(n.Child1);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo3.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo3.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys, extendedNodeInfo3.Keys);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			if (op.OpType == OpType.CrossApply)
			{
				extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			}
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			RowCount maxRows;
			if (extendedNodeInfo2.MaxRows <= RowCount.One && extendedNodeInfo3.MaxRows <= RowCount.One)
			{
				maxRows = RowCount.One;
			}
			else
			{
				maxRows = RowCount.Unbounded;
			}
			RowCount minRows = (op.OpType == OpType.CrossApply) ? RowCount.Zero : extendedNodeInfo2.MinRows;
			extendedNodeInfo.SetRowCount(minRows, maxRows);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x0011A410 File Offset: 0x00118610
		protected override NodeInfo VisitSetOp(SetOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			extendedNodeInfo.LocalDefinitions.InitFrom(op.Outputs);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			ExtendedNodeInfo extendedNodeInfo3 = this.GetExtendedNodeInfo(n.Child1);
			RowCount minRows = RowCount.Zero;
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo3.ExternalReferences);
			if (op.OpType == OpType.UnionAll)
			{
				minRows = ((extendedNodeInfo2.MinRows > extendedNodeInfo3.MinRows) ? extendedNodeInfo2.MinRows : extendedNodeInfo3.MinRows);
			}
			if (op.OpType == OpType.Intersect || op.OpType == OpType.Except)
			{
				extendedNodeInfo.Keys.InitFrom(op.Outputs);
			}
			else
			{
				UnionAllOp unionAllOp = (UnionAllOp)op;
				if (unionAllOp.BranchDiscriminator == null)
				{
					extendedNodeInfo.Keys.NoKeys = true;
				}
				else
				{
					VarVec varVec = this.m_command.CreateVarVec();
					for (int i = 0; i < n.Children.Count; i++)
					{
						ExtendedNodeInfo extendedNodeInfo4 = n.Children[i].GetExtendedNodeInfo(this.m_command);
						if (extendedNodeInfo4.Keys.NoKeys || extendedNodeInfo4.Keys.KeyVars.IsEmpty)
						{
							varVec.Clear();
							break;
						}
						VarVec other = extendedNodeInfo4.Keys.KeyVars.Remap(unionAllOp.VarMap[i].GetReverseMap());
						varVec.Or(other);
					}
					if (varVec.IsEmpty)
					{
						extendedNodeInfo.Keys.NoKeys = true;
					}
					else
					{
						extendedNodeInfo.Keys.InitFrom(varVec);
					}
				}
			}
			VarVec other2 = extendedNodeInfo2.NonNullableDefinitions.Remap(op.VarMap[0].GetReverseMap());
			extendedNodeInfo.NonNullableDefinitions.InitFrom(other2);
			if (op.OpType != OpType.Except)
			{
				VarVec other3 = extendedNodeInfo3.NonNullableDefinitions.Remap(op.VarMap[1].GetReverseMap());
				if (op.OpType == OpType.Intersect)
				{
					extendedNodeInfo.NonNullableDefinitions.Or(other3);
				}
				else
				{
					extendedNodeInfo.NonNullableDefinitions.And(other3);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.And(op.Outputs);
			extendedNodeInfo.MinRows = minRows;
			return extendedNodeInfo;
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x0011A644 File Offset: 0x00118844
		protected override NodeInfo VisitSortOp(SortBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.InitRowCountFrom(extendedNodeInfo2);
			if (OpType.ConstrainedSort == op.OpType && n.Child2.Op.OpType == OpType.Constant && !((ConstrainedSortOp)op).WithTies)
			{
				ConstantBaseOp constantBaseOp = (ConstantBaseOp)n.Child2.Op;
				if (TypeHelpers.IsIntegerConstant(constantBaseOp.Type, constantBaseOp.Value, 1L))
				{
					extendedNodeInfo.SetRowCount(RowCount.Zero, RowCount.One);
				}
			}
			return extendedNodeInfo;
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x0011A72C File Offset: 0x0011892C
		public override NodeInfo Visit(DistinctOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Keys.InitFrom(op.Keys, true);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.ExternalReferences.InitFrom(extendedNodeInfo2.ExternalReferences);
			foreach (Var v in op.Keys)
			{
				if (extendedNodeInfo2.Definitions.IsSet(v))
				{
					extendedNodeInfo.Definitions.Set(v);
				}
				else
				{
					extendedNodeInfo.ExternalReferences.Set(v);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(op.Keys);
			extendedNodeInfo.InitRowCountFrom(extendedNodeInfo2);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x0011A7FC File Offset: 0x001189FC
		public override NodeInfo Visit(SingleRowOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.Definitions.InitFrom(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
			extendedNodeInfo.ExternalReferences.InitFrom(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.SetRowCount(RowCount.Zero, RowCount.One);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x0011A86C File Offset: 0x00118A6C
		public override NodeInfo Visit(SingleRowTableOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Keys.NoKeys = false;
			extendedNodeInfo.SetRowCount(RowCount.One, RowCount.One);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x0011A898 File Offset: 0x00118A98
		public override NodeInfo Visit(PhysicalProjectOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			foreach (Node n2 in n.Children)
			{
				NodeInfo nodeInfo = this.GetNodeInfo(n2);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			}
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			extendedNodeInfo.LocalDefinitions.InitFrom(extendedNodeInfo.Definitions);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			if (!extendedNodeInfo2.Keys.NoKeys)
			{
				VarVec varVec = this.m_command.CreateVarVec(extendedNodeInfo2.Keys.KeyVars);
				varVec.Minus(extendedNodeInfo.Definitions);
				if (varVec.IsEmpty)
				{
					extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(extendedNodeInfo.Definitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.Or(extendedNodeInfo2.NonNullableVisibleDefinitions);
			return extendedNodeInfo;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x0011A9B8 File Offset: 0x00118BB8
		protected override NodeInfo VisitNestOp(NestBaseOp op, Node n)
		{
			SingleStreamNestOp singleStreamNestOp = op as SingleStreamNestOp;
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			foreach (CollectionInfo collectionInfo in op.CollectionInfo)
			{
				extendedNodeInfo.LocalDefinitions.Set(collectionInfo.CollectionVar);
			}
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			foreach (Node n2 in n.Children)
			{
				extendedNodeInfo.ExternalReferences.Or(this.GetExtendedNodeInfo(n2).ExternalReferences);
			}
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo.Definitions);
			if (singleStreamNestOp == null)
			{
				extendedNodeInfo.Keys.InitFrom(this.GetExtendedNodeInfo(n.Child0).Keys);
			}
			else
			{
				extendedNodeInfo.Keys.InitFrom(singleStreamNestOp.Keys);
			}
			return extendedNodeInfo;
		}

		// Token: 0x040016BF RID: 5823
		private readonly Command m_command;
	}
}
