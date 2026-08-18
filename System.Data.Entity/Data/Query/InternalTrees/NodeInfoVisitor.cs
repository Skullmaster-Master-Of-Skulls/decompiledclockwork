using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B7 RID: 183
	internal class NodeInfoVisitor : BasicOpVisitorOfT<NodeInfo>
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x00039968 File Offset: 0x00037B68
		internal void RecomputeNodeInfo(Node n)
		{
			if (n.IsNodeInfoInitialized)
			{
				NodeInfo nodeInfo = base.VisitNode(n);
				nodeInfo.ComputeHashValue(this.m_command, n);
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00039992 File Offset: 0x00037B92
		internal NodeInfoVisitor(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000399A1 File Offset: 0x00037BA1
		private NodeInfo GetNodeInfo(Node n)
		{
			return n.GetNodeInfo(this.m_command);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000399AF File Offset: 0x00037BAF
		private ExtendedNodeInfo GetExtendedNodeInfo(Node n)
		{
			return n.GetExtendedNodeInfo(this.m_command);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000399C0 File Offset: 0x00037BC0
		private NodeInfo InitNodeInfo(Node n)
		{
			NodeInfo nodeInfo = this.GetNodeInfo(n);
			nodeInfo.Clear();
			return nodeInfo;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000399DC File Offset: 0x00037BDC
		private ExtendedNodeInfo InitExtendedNodeInfo(Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.GetExtendedNodeInfo(n);
			extendedNodeInfo.Clear();
			return extendedNodeInfo;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000399F8 File Offset: 0x00037BF8
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

		// Token: 0x06000B70 RID: 2928 RVA: 0x00039A68 File Offset: 0x00037C68
		private bool IsDefinitionNonNullable(Node definition, VarVec nonNullableInputs)
		{
			return definition.Op.OpType == OpType.Constant || definition.Op.OpType == OpType.InternalConstant || definition.Op.OpType == OpType.NullSentinel || (definition.Op.OpType == OpType.VarRef && nonNullableInputs.IsSet(((VarRefOp)definition.Op).Var));
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00039AC8 File Offset: 0x00037CC8
		public override NodeInfo Visit(VarRefOp op, Node n)
		{
			NodeInfo nodeInfo = this.InitNodeInfo(n);
			nodeInfo.ExternalReferences.Set(op.Var);
			return nodeInfo;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00039AEF File Offset: 0x00037CEF
		protected override NodeInfo VisitRelOpDefault(RelOp op, Node n)
		{
			return this.Unimplemented(n);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00039AF8 File Offset: 0x00037CF8
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

		// Token: 0x06000B74 RID: 2932 RVA: 0x00039B94 File Offset: 0x00037D94
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

		// Token: 0x06000B75 RID: 2933 RVA: 0x00039CB4 File Offset: 0x00037EB4
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

		// Token: 0x06000B76 RID: 2934 RVA: 0x00039D3C File Offset: 0x00037F3C
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
				if (this.IsDefinitionNonNullable(node.Child0, extendedNodeInfo.NonNullableVisibleDefinitions))
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

		// Token: 0x06000B77 RID: 2935 RVA: 0x00039F74 File Offset: 0x00038174
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

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003A054 File Offset: 0x00038254
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
				if (this.IsDefinitionNonNullable(node.Child0, extendedNodeInfo2.NonNullableDefinitions))
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

		// Token: 0x06000B79 RID: 2937 RVA: 0x0003A244 File Offset: 0x00038444
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

		// Token: 0x06000B7A RID: 2938 RVA: 0x0003A330 File Offset: 0x00038530
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

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003A4C0 File Offset: 0x000386C0
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

		// Token: 0x06000B7C RID: 2940 RVA: 0x0003A5E0 File Offset: 0x000387E0
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

		// Token: 0x06000B7D RID: 2941 RVA: 0x0003A814 File Offset: 0x00038A14
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

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003A8FC File Offset: 0x00038AFC
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

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003A9CC File Offset: 0x00038BCC
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

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003AA3C File Offset: 0x00038C3C
		public override NodeInfo Visit(SingleRowTableOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Keys.NoKeys = false;
			extendedNodeInfo.SetRowCount(RowCount.One, RowCount.One);
			return extendedNodeInfo;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0003AA68 File Offset: 0x00038C68
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

		// Token: 0x06000B82 RID: 2946 RVA: 0x0003AB88 File Offset: 0x00038D88
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

		// Token: 0x040008FA RID: 2298
		private Command m_command;
	}
}
