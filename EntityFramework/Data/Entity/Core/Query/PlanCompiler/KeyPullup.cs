using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000682 RID: 1666
	internal class KeyPullup : BasicOpVisitor
	{
		// Token: 0x06004155 RID: 16725 RVA: 0x0012F7C4 File Offset: 0x0012D9C4
		internal KeyPullup(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x0012F7D4 File Offset: 0x0012D9D4
		internal KeyVec GetKeys(Node node)
		{
			ExtendedNodeInfo extendedNodeInfo = node.GetExtendedNodeInfo(this.m_command);
			if (extendedNodeInfo.Keys.NoKeys)
			{
				this.VisitNode(node);
			}
			return extendedNodeInfo.Keys;
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x0012F808 File Offset: 0x0012DA08
		protected override void VisitChildren(Node n)
		{
			foreach (Node node in n.Children)
			{
				if (node.Op.IsRelOp || node.Op.IsPhysicalOp)
				{
					this.GetKeys(node);
				}
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x0012F878 File Offset: 0x0012DA78
		protected override void VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x0012F88D File Offset: 0x0012DA8D
		public override void Visit(ScanTableOp op, Node n)
		{
			op.Table.ReferencedColumns.Or(op.Table.Keys);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x0012F8B8 File Offset: 0x0012DAB8
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitChildren(n);
			ExtendedNodeInfo extendedNodeInfo = n.Child0.GetExtendedNodeInfo(this.m_command);
			if (!extendedNodeInfo.Keys.NoKeys)
			{
				VarVec varVec = this.m_command.CreateVarVec(op.Outputs);
				Dictionary<Var, Var> varMap = NodeInfoVisitor.ComputeVarRemappings(n.Child1);
				VarVec other = extendedNodeInfo.Keys.KeyVars.Remap(varMap);
				varVec.Or(other);
				op.Outputs.InitFrom(varVec);
			}
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0012F93C File Offset: 0x0012DB3C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override void Visit(UnionAllOp op, Node n)
		{
			this.VisitChildren(n);
			Var var = this.m_command.CreateSetOpVar(this.m_command.IntegerType);
			VarList varList = Command.CreateVarList();
			VarVec[] array = new VarVec[n.Children.Count];
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = n.Children[i];
				ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(node);
				VarVec v = extendedNodeInfo.Keys.KeyVars.Remap(op.VarMap[i]);
				array[i] = this.m_command.CreateVarVec(v);
				array[i].Minus(op.Outputs);
				if (OpType.UnionAll == node.Op.OpType)
				{
					UnionAllOp unionAllOp = (UnionAllOp)node.Op;
					array[i].Clear(unionAllOp.BranchDiscriminator);
				}
				varList.AddRange(array[i]);
			}
			VarList varList2 = Command.CreateVarList();
			foreach (Var var2 in varList)
			{
				Var item = this.m_command.CreateSetOpVar(var2.Type);
				varList2.Add(item);
			}
			for (int j = 0; j < n.Children.Count; j++)
			{
				Node node2 = n.Children[j];
				ExtendedNodeInfo extendedNodeInfo2 = this.m_command.GetExtendedNodeInfo(node2);
				VarVec varVec = this.m_command.CreateVarVec();
				List<Node> list = new List<Node>();
				Var branchDiscriminator;
				if (OpType.UnionAll == node2.Op.OpType && ((UnionAllOp)node2.Op).BranchDiscriminator != null)
				{
					branchDiscriminator = ((UnionAllOp)node2.Op).BranchDiscriminator;
					if (!op.VarMap[j].ContainsValue(branchDiscriminator))
					{
						op.VarMap[j].Add(var, branchDiscriminator);
					}
					else
					{
						PlanCompiler.Assert(0 == j, "right branch has a discriminator var that the left branch doesn't have?");
						VarMap reverseMap = op.VarMap[j].GetReverseMap();
						var = reverseMap[branchDiscriminator];
					}
				}
				else
				{
					list.Add(this.m_command.CreateVarDefNode(this.m_command.CreateNode(this.m_command.CreateConstantOp(this.m_command.IntegerType, this.m_command.NextBranchDiscriminatorValue)), out branchDiscriminator));
					varVec.Set(branchDiscriminator);
					op.VarMap[j].Add(var, branchDiscriminator);
				}
				for (int k = 0; k < varList.Count; k++)
				{
					Var var3 = varList[k];
					if (!array[j].IsSet(var3))
					{
						list.Add(this.m_command.CreateVarDefNode(this.m_command.CreateNode(this.m_command.CreateNullOp(var3.Type)), out var3));
						varVec.Set(var3);
					}
					op.VarMap[j].Add(varList2[k], var3);
				}
				if (varVec.IsEmpty)
				{
					extendedNodeInfo2.Keys.KeyVars.Set(branchDiscriminator);
				}
				else
				{
					PlanCompiler.Assert(list.Count != 0, "no new nodes?");
					foreach (Var v2 in op.VarMap[j].Values)
					{
						varVec.Set(v2);
					}
					n.Children[j] = this.m_command.CreateNode(this.m_command.CreateProjectOp(varVec), node2, this.m_command.CreateNode(this.m_command.CreateVarDefListOp(), list));
					this.m_command.RecomputeNodeInfo(n.Children[j]);
					ExtendedNodeInfo extendedNodeInfo3 = this.m_command.GetExtendedNodeInfo(n.Children[j]);
					extendedNodeInfo3.Keys.KeyVars.InitFrom(extendedNodeInfo2.Keys.KeyVars);
					extendedNodeInfo3.Keys.KeyVars.Set(branchDiscriminator);
				}
			}
			n.Op = this.m_command.CreateUnionAllOp(op.VarMap[0], op.VarMap[1], var);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x04001855 RID: 6229
		private readonly Command m_command;
	}
}
