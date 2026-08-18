using System;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000651 RID: 1617
	internal static class ApplyOpRules
	{
		// Token: 0x06003F32 RID: 16178 RVA: 0x00120F88 File Offset: 0x0011F188
		private static bool ProcessApplyOverFilter(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			if (transformationRulesContext.PlanCompiler.TransformationsDeferred)
			{
				return false;
			}
			Node child = applyNode.Child1;
			Command command = context.Command;
			NodeInfo nodeInfo = command.GetNodeInfo(child.Child0);
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(applyNode.Child0);
			if (nodeInfo.ExternalReferences.Overlaps(extendedNodeInfo.Definitions))
			{
				return false;
			}
			JoinBaseOp op;
			if (applyNode.Op.OpType == OpType.CrossApply)
			{
				op = command.CreateInnerJoinOp();
			}
			else
			{
				op = command.CreateLeftOuterJoinOp();
			}
			newNode = command.CreateNode(op, applyNode.Child0, child.Child0, child.Child1);
			return true;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x00121030 File Offset: 0x0011F230
		private static bool ProcessOuterApplyOverDummyProjectOverFilter(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			ProjectOp projectOp = (ProjectOp)child.Op;
			Node child2 = child.Child0;
			Node child3 = child2.Child0;
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child3);
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(applyNode.Child0);
			if (projectOp.Outputs.Overlaps(extendedNodeInfo2.Definitions) || extendedNodeInfo.ExternalReferences.Overlaps(extendedNodeInfo2.Definitions))
			{
				return false;
			}
			bool flag = false;
			Node arg = null;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Var first;
			bool flag2;
			if (TransformationRulesContext.TryGetInt32Var(extendedNodeInfo.NonNullableDefinitions, out first))
			{
				flag2 = true;
			}
			else
			{
				first = extendedNodeInfo.NonNullableDefinitions.First;
				flag2 = false;
			}
			if (first != null)
			{
				flag = true;
				Node child4 = child.Child1.Child0;
				if (child4.Child0.Op.OpType == OpType.NullSentinel && flag2 && transformationRulesContext.CanChangeNullSentinelValue)
				{
					child4.Child0 = context.Command.CreateNode(context.Command.CreateVarRefOp(first));
				}
				else
				{
					child4.Child0 = transformationRulesContext.BuildNullIfExpression(first, child4.Child0);
				}
				command.RecomputeNodeInfo(child4);
				command.RecomputeNodeInfo(child.Child1);
				arg = child3;
			}
			else
			{
				arg = child;
				NodeInfo nodeInfo = command.GetNodeInfo(child2.Child1);
				foreach (Var v in nodeInfo.ExternalReferences)
				{
					if (extendedNodeInfo.Definitions.IsSet(v))
					{
						projectOp.Outputs.Set(v);
					}
				}
				child.Child0 = child3;
			}
			context.Command.RecomputeNodeInfo(child);
			Node node = command.CreateNode(command.CreateLeftOuterJoinOp(), applyNode.Child0, arg, child2.Child1);
			if (flag)
			{
				ExtendedNodeInfo extendedNodeInfo3 = command.GetExtendedNodeInfo(node);
				child.Child0 = node;
				projectOp.Outputs.Or(extendedNodeInfo3.Definitions);
				newNode = child;
			}
			else
			{
				newNode = node;
			}
			return true;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00121240 File Offset: 0x0011F440
		private static bool ProcessCrossApplyOverProject(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			ProjectOp projectOp = (ProjectOp)child.Op;
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(applyNode);
			VarVec varVec = command.CreateVarVec(projectOp.Outputs);
			varVec.Or(extendedNodeInfo.Definitions);
			projectOp.Outputs.InitFrom(varVec);
			applyNode.Child1 = child.Child0;
			context.Command.RecomputeNodeInfo(applyNode);
			child.Child0 = applyNode;
			newNode = child;
			return true;
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x001212C0 File Offset: 0x0011F4C0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static bool ProcessOuterApplyOverProject(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			Node child2 = child.Child1;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			ExtendedNodeInfo extendedNodeInfo = context.Command.GetExtendedNodeInfo(child.Child0);
			Var first = extendedNodeInfo.NonNullableDefinitions.First;
			if (first == null && child2.Children.Count == 1 && (child2.Child0.Child0.Op.OpType == OpType.InternalConstant || child2.Child0.Child0.Op.OpType == OpType.NullSentinel))
			{
				return false;
			}
			Command command = context.Command;
			Node node = null;
			InternalConstantOp internalConstantOp = null;
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(child.Child0);
			bool flag = false;
			foreach (Node node2 in child2.Children)
			{
				PlanCompiler.Assert(node2.Op.OpType == OpType.VarDef, "Expected VarDefOp. Found " + node2.Op.OpType + " instead");
				VarRefOp varRefOp = node2.Child0.Op as VarRefOp;
				if (varRefOp == null || !extendedNodeInfo2.Definitions.IsSet(varRefOp.Var))
				{
					if (first == null)
					{
						internalConstantOp = command.CreateInternalConstantOp(command.IntegerType, 1);
						Node definingExpr = command.CreateNode(internalConstantOp);
						Node arg = command.CreateVarDefListNode(definingExpr, out first);
						ProjectOp projectOp = command.CreateProjectOp(first);
						projectOp.Outputs.Or(extendedNodeInfo2.Definitions);
						node = command.CreateNode(projectOp, child.Child0, arg);
					}
					Node child3;
					if (internalConstantOp != null && (internalConstantOp.IsEquivalent(node2.Child0.Op) || node2.Child0.Op.OpType == OpType.NullSentinel))
					{
						child3 = command.CreateNode(command.CreateVarRefOp(first));
					}
					else
					{
						child3 = transformationRulesContext.BuildNullIfExpression(first, node2.Child0);
					}
					node2.Child0 = child3;
					command.RecomputeNodeInfo(node2);
					flag = true;
				}
			}
			if (flag)
			{
				command.RecomputeNodeInfo(child2);
			}
			applyNode.Child1 = ((node != null) ? node : child.Child0);
			command.RecomputeNodeInfo(applyNode);
			child.Child0 = applyNode;
			ExtendedNodeInfo extendedNodeInfo3 = command.GetExtendedNodeInfo(applyNode.Child0);
			ProjectOp projectOp2 = (ProjectOp)child.Op;
			projectOp2.Outputs.Or(extendedNodeInfo3.Definitions);
			newNode = child;
			return true;
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0012154C File Offset: 0x0011F74C
		private static bool ProcessApplyOverAnything(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child0;
			Node child2 = applyNode.Child1;
			ApplyBaseOp applyBaseOp = (ApplyBaseOp)applyNode.Op;
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child2);
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(child);
			bool flag = false;
			if (applyBaseOp.OpType == OpType.OuterApply && extendedNodeInfo.MinRows >= RowCount.One)
			{
				applyBaseOp = command.CreateCrossApplyOp();
				flag = true;
			}
			if (!extendedNodeInfo.ExternalReferences.Overlaps(extendedNodeInfo2.Definitions))
			{
				if (applyBaseOp.OpType == OpType.CrossApply)
				{
					newNode = command.CreateNode(command.CreateCrossJoinOp(), child, child2);
				}
				else
				{
					LeftOuterJoinOp op = command.CreateLeftOuterJoinOp();
					ConstantPredicateOp op2 = command.CreateTrueOp();
					Node arg = command.CreateNode(op2);
					newNode = command.CreateNode(op, child, child2, arg);
				}
				return true;
			}
			if (flag)
			{
				newNode = command.CreateNode(applyBaseOp, child, child2);
				return true;
			}
			return false;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x00121620 File Offset: 0x0011F820
		private static bool ProcessApplyIntoScalarSubquery(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(applyNode.Child1);
			OpType opType = applyNode.Op.OpType;
			if (!ApplyOpRules.CanRewriteApply(applyNode.Child1, extendedNodeInfo, opType))
			{
				newNode = applyNode;
				return false;
			}
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(applyNode.Child0);
			Var first = extendedNodeInfo.Definitions.First;
			VarVec varVec = command.CreateVarVec(extendedNodeInfo2.Definitions);
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			transformationRulesContext.RemapSubtree(applyNode.Child1);
			ApplyOpRules.VarDefinitionRemapper.RemapSubtree(applyNode.Child1, command, first);
			Node definingExpr = command.CreateNode(command.CreateElementOp(first.Type), applyNode.Child1);
			Var var;
			Node arg = command.CreateVarDefListNode(definingExpr, out var);
			varVec.Set(var);
			newNode = command.CreateNode(command.CreateProjectOp(varVec), applyNode.Child0, arg);
			transformationRulesContext.AddVarMapping(first, var);
			return true;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x00121700 File Offset: 0x0011F900
		private static bool CanRewriteApply(Node rightChild, ExtendedNodeInfo applyRightChildNodeInfo, OpType applyKind)
		{
			return applyRightChildNodeInfo.Definitions.Count == 1 && applyRightChildNodeInfo.MaxRows == RowCount.One && (applyKind != OpType.CrossApply || applyRightChildNodeInfo.MinRows == RowCount.One) && ApplyOpRules.OutputCountVisitor.CountOutputs(rightChild) == 1;
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0012173C File Offset: 0x0011F93C
		private static bool ProcessCrossApplyOverLeftOuterJoinOverSingleRowTable(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)child.Child2.Op;
			if (constantPredicateOp.IsFalse)
			{
				return false;
			}
			applyNode.Op = context.Command.CreateOuterApplyOp();
			applyNode.Child1 = child.Child1;
			return true;
		}

		// Token: 0x04001796 RID: 6038
		internal static readonly PatternMatchRule Rule_CrossApplyOverFilter = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverFilter));

		// Token: 0x04001797 RID: 6039
		internal static readonly PatternMatchRule Rule_OuterApplyOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverFilter));

		// Token: 0x04001798 RID: 6040
		internal static readonly PatternMatchRule Rule_OuterApplyOverProjectInternalConstantOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(FilterOp.Pattern, new Node[]
				{
					new Node(LeafOp.Pattern, new Node[0]),
					new Node(LeafOp.Pattern, new Node[0])
				}),
				new Node(VarDefListOp.Pattern, new Node[]
				{
					new Node(VarDefOp.Pattern, new Node[]
					{
						new Node(InternalConstantOp.Pattern, new Node[0])
					})
				})
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverDummyProjectOverFilter));

		// Token: 0x04001799 RID: 6041
		internal static readonly PatternMatchRule Rule_OuterApplyOverProjectNullSentinelOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(FilterOp.Pattern, new Node[]
				{
					new Node(LeafOp.Pattern, new Node[0]),
					new Node(LeafOp.Pattern, new Node[0])
				}),
				new Node(VarDefListOp.Pattern, new Node[]
				{
					new Node(VarDefOp.Pattern, new Node[]
					{
						new Node(NullSentinelOp.Pattern, new Node[0])
					})
				})
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverDummyProjectOverFilter));

		// Token: 0x0400179A RID: 6042
		internal static readonly PatternMatchRule Rule_CrossApplyOverProject = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessCrossApplyOverProject));

		// Token: 0x0400179B RID: 6043
		internal static readonly PatternMatchRule Rule_OuterApplyOverProject = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverProject));

		// Token: 0x0400179C RID: 6044
		internal static readonly PatternMatchRule Rule_CrossApplyOverAnything = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverAnything));

		// Token: 0x0400179D RID: 6045
		internal static readonly PatternMatchRule Rule_OuterApplyOverAnything = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverAnything));

		// Token: 0x0400179E RID: 6046
		internal static readonly PatternMatchRule Rule_CrossApplyIntoScalarSubquery = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyIntoScalarSubquery));

		// Token: 0x0400179F RID: 6047
		internal static readonly PatternMatchRule Rule_OuterApplyIntoScalarSubquery = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyIntoScalarSubquery));

		// Token: 0x040017A0 RID: 6048
		internal static readonly PatternMatchRule Rule_CrossApplyOverLeftOuterJoinOverSingleRowTable = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeftOuterJoinOp.Pattern, new Node[]
			{
				new Node(SingleRowTableOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(ConstantPredicateOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessCrossApplyOverLeftOuterJoinOverSingleRowTable));

		// Token: 0x040017A1 RID: 6049
		internal static readonly Rule[] Rules = new Rule[]
		{
			ApplyOpRules.Rule_CrossApplyOverAnything,
			ApplyOpRules.Rule_CrossApplyOverFilter,
			ApplyOpRules.Rule_CrossApplyOverProject,
			ApplyOpRules.Rule_OuterApplyOverAnything,
			ApplyOpRules.Rule_OuterApplyOverProjectInternalConstantOverFilter,
			ApplyOpRules.Rule_OuterApplyOverProjectNullSentinelOverFilter,
			ApplyOpRules.Rule_OuterApplyOverProject,
			ApplyOpRules.Rule_OuterApplyOverFilter,
			ApplyOpRules.Rule_CrossApplyOverLeftOuterJoinOverSingleRowTable,
			ApplyOpRules.Rule_CrossApplyIntoScalarSubquery,
			ApplyOpRules.Rule_OuterApplyIntoScalarSubquery
		};

		// Token: 0x02000652 RID: 1618
		internal class OutputCountVisitor : BasicOpVisitorOfT<int>
		{
			// Token: 0x06003F3B RID: 16187 RVA: 0x00121D84 File Offset: 0x0011FF84
			internal static int CountOutputs(Node node)
			{
				ApplyOpRules.OutputCountVisitor outputCountVisitor = new ApplyOpRules.OutputCountVisitor();
				return outputCountVisitor.VisitNode(node);
			}

			// Token: 0x06003F3C RID: 16188 RVA: 0x00121DA0 File Offset: 0x0011FFA0
			internal new int VisitChildren(Node n)
			{
				int num = 0;
				foreach (Node n2 in n.Children)
				{
					num += base.VisitNode(n2);
				}
				return num;
			}

			// Token: 0x06003F3D RID: 16189 RVA: 0x00121DFC File Offset: 0x0011FFFC
			protected override int VisitDefault(Node n)
			{
				return this.VisitChildren(n);
			}

			// Token: 0x06003F3E RID: 16190 RVA: 0x00121E05 File Offset: 0x00120005
			protected override int VisitSetOp(SetOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06003F3F RID: 16191 RVA: 0x00121E12 File Offset: 0x00120012
			public override int Visit(DistinctOp op, Node n)
			{
				return op.Keys.Count;
			}

			// Token: 0x06003F40 RID: 16192 RVA: 0x00121E1F File Offset: 0x0012001F
			public override int Visit(FilterOp op, Node n)
			{
				return base.VisitNode(n.Child0);
			}

			// Token: 0x06003F41 RID: 16193 RVA: 0x00121E2D File Offset: 0x0012002D
			public override int Visit(GroupByOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06003F42 RID: 16194 RVA: 0x00121E3A File Offset: 0x0012003A
			public override int Visit(ProjectOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06003F43 RID: 16195 RVA: 0x00121E47 File Offset: 0x00120047
			public override int Visit(ScanTableOp op, Node n)
			{
				return op.Table.Columns.Count;
			}

			// Token: 0x06003F44 RID: 16196 RVA: 0x00121E59 File Offset: 0x00120059
			public override int Visit(SingleRowTableOp op, Node n)
			{
				return 0;
			}

			// Token: 0x06003F45 RID: 16197 RVA: 0x00121E5C File Offset: 0x0012005C
			protected override int VisitSortOp(SortBaseOp op, Node n)
			{
				return base.VisitNode(n.Child0);
			}
		}

		// Token: 0x02000654 RID: 1620
		internal class VarDefinitionRemapper : VarRemapper
		{
			// Token: 0x06003F5F RID: 16223 RVA: 0x001224BF File Offset: 0x001206BF
			private VarDefinitionRemapper(Var oldVar, Command command) : base(command)
			{
				this.m_oldVar = oldVar;
			}

			// Token: 0x06003F60 RID: 16224 RVA: 0x001224D0 File Offset: 0x001206D0
			internal static void RemapSubtree(Node root, Command command, Var oldVar)
			{
				ApplyOpRules.VarDefinitionRemapper varDefinitionRemapper = new ApplyOpRules.VarDefinitionRemapper(oldVar, command);
				varDefinitionRemapper.RemapSubtree(root);
			}

			// Token: 0x06003F61 RID: 16225 RVA: 0x001224EC File Offset: 0x001206EC
			internal override void RemapSubtree(Node subTree)
			{
				foreach (Node subTree2 in subTree.Children)
				{
					this.RemapSubtree(subTree2);
				}
				this.VisitNode(subTree);
				this.m_command.RecomputeNodeInfo(subTree);
			}

			// Token: 0x06003F62 RID: 16226 RVA: 0x00122554 File Offset: 0x00120754
			public override void Visit(VarDefOp op, Node n)
			{
				if (op.Var == this.m_oldVar)
				{
					Var var = this.m_command.CreateComputedVar(n.Child0.Op.Type);
					n.Op = this.m_command.CreateVarDefOp(var);
					base.AddMapping(this.m_oldVar, var);
				}
			}

			// Token: 0x06003F63 RID: 16227 RVA: 0x001225AC File Offset: 0x001207AC
			public override void Visit(ScanTableOp op, Node n)
			{
				if (op.Table.Columns.Contains(this.m_oldVar))
				{
					ScanTableOp scanTableOp = this.m_command.CreateScanTableOp(op.Table.TableMetadata);
					this.m_command.CreateVarDefListOp();
					for (int i = 0; i < op.Table.Columns.Count; i++)
					{
						base.AddMapping(op.Table.Columns[i], scanTableOp.Table.Columns[i]);
					}
					n.Op = scanTableOp;
				}
			}

			// Token: 0x06003F64 RID: 16228 RVA: 0x00122640 File Offset: 0x00120840
			protected override void VisitSetOp(SetOp op, Node n)
			{
				base.VisitSetOp(op, n);
				if (op.Outputs.IsSet(this.m_oldVar))
				{
					Var var = this.m_command.CreateSetOpVar(this.m_oldVar.Type);
					op.Outputs.Clear(this.m_oldVar);
					op.Outputs.Set(var);
					this.RemapVarMapKey(op.VarMap[0], var);
					this.RemapVarMapKey(op.VarMap[1], var);
					base.AddMapping(this.m_oldVar, var);
				}
			}

			// Token: 0x06003F65 RID: 16229 RVA: 0x001226C8 File Offset: 0x001208C8
			private void RemapVarMapKey(VarMap varMap, Var newVar)
			{
				Var value = varMap[this.m_oldVar];
				varMap.Remove(this.m_oldVar);
				varMap.Add(newVar, value);
			}

			// Token: 0x040017A4 RID: 6052
			private readonly Var m_oldVar;
		}
	}
}
