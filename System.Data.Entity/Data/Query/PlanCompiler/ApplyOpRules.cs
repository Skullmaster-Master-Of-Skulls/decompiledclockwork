using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000074 RID: 116
	internal static class ApplyOpRules
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		private static bool ProcessApplyOverFilter(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
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

		// Token: 0x0600090D RID: 2317 RVA: 0x00030C88 File Offset: 0x0002EE88
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

		// Token: 0x0600090E RID: 2318 RVA: 0x00030E98 File Offset: 0x0002F098
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

		// Token: 0x0600090F RID: 2319 RVA: 0x00030F18 File Offset: 0x0002F118
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
				PlanCompiler.Assert(node2.Op.OpType == OpType.VarDef, "Expected VarDefOp. Found " + node2.Op.OpType.ToString() + " instead");
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

		// Token: 0x06000910 RID: 2320 RVA: 0x000311B0 File Offset: 0x0002F3B0
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

		// Token: 0x06000911 RID: 2321 RVA: 0x00031284 File Offset: 0x0002F484
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

		// Token: 0x06000912 RID: 2322 RVA: 0x00031364 File Offset: 0x0002F564
		private static bool CanRewriteApply(Node rightChild, ExtendedNodeInfo applyRightChildNodeInfo, OpType applyKind)
		{
			return applyRightChildNodeInfo.Definitions.Count == 1 && applyRightChildNodeInfo.MaxRows == RowCount.One && (applyKind != OpType.CrossApply || applyRightChildNodeInfo.MinRows == RowCount.One) && ApplyOpRules.OutputCountVisitor.CountOutputs(rightChild) == 1;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000313A0 File Offset: 0x0002F5A0
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

		// Token: 0x04000841 RID: 2113
		internal static readonly PatternMatchRule Rule_CrossApplyOverFilter = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverFilter));

		// Token: 0x04000842 RID: 2114
		internal static readonly PatternMatchRule Rule_OuterApplyOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverFilter));

		// Token: 0x04000843 RID: 2115
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

		// Token: 0x04000844 RID: 2116
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

		// Token: 0x04000845 RID: 2117
		internal static readonly PatternMatchRule Rule_CrossApplyOverProject = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessCrossApplyOverProject));

		// Token: 0x04000846 RID: 2118
		internal static readonly PatternMatchRule Rule_OuterApplyOverProject = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverProject));

		// Token: 0x04000847 RID: 2119
		internal static readonly PatternMatchRule Rule_CrossApplyOverAnything = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverAnything));

		// Token: 0x04000848 RID: 2120
		internal static readonly PatternMatchRule Rule_OuterApplyOverAnything = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverAnything));

		// Token: 0x04000849 RID: 2121
		internal static readonly PatternMatchRule Rule_CrossApplyIntoScalarSubquery = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyIntoScalarSubquery));

		// Token: 0x0400084A RID: 2122
		internal static readonly PatternMatchRule Rule_OuterApplyIntoScalarSubquery = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyIntoScalarSubquery));

		// Token: 0x0400084B RID: 2123
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

		// Token: 0x0400084C RID: 2124
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

		// Token: 0x02000481 RID: 1153
		internal class OutputCountVisitor : BasicOpVisitorOfT<int>
		{
			// Token: 0x06003B73 RID: 15219 RVA: 0x0003DB68 File Offset: 0x0003BD68
			internal OutputCountVisitor()
			{
			}

			// Token: 0x06003B74 RID: 15220 RVA: 0x000E05E4 File Offset: 0x000DE7E4
			internal static int CountOutputs(Node node)
			{
				ApplyOpRules.OutputCountVisitor outputCountVisitor = new ApplyOpRules.OutputCountVisitor();
				return outputCountVisitor.VisitNode(node);
			}

			// Token: 0x06003B75 RID: 15221 RVA: 0x000E0600 File Offset: 0x000DE800
			internal new int VisitChildren(Node n)
			{
				int num = 0;
				foreach (Node n2 in n.Children)
				{
					num += base.VisitNode(n2);
				}
				return num;
			}

			// Token: 0x06003B76 RID: 15222 RVA: 0x000E065C File Offset: 0x000DE85C
			protected override int VisitDefault(Node n)
			{
				return this.VisitChildren(n);
			}

			// Token: 0x06003B77 RID: 15223 RVA: 0x000E0665 File Offset: 0x000DE865
			protected override int VisitSetOp(SetOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06003B78 RID: 15224 RVA: 0x000E0672 File Offset: 0x000DE872
			public override int Visit(DistinctOp op, Node n)
			{
				return op.Keys.Count;
			}

			// Token: 0x06003B79 RID: 15225 RVA: 0x000E067F File Offset: 0x000DE87F
			public override int Visit(FilterOp op, Node n)
			{
				return base.VisitNode(n.Child0);
			}

			// Token: 0x06003B7A RID: 15226 RVA: 0x000E068D File Offset: 0x000DE88D
			public override int Visit(GroupByOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06003B7B RID: 15227 RVA: 0x000E069A File Offset: 0x000DE89A
			public override int Visit(ProjectOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06003B7C RID: 15228 RVA: 0x000E06A7 File Offset: 0x000DE8A7
			public override int Visit(ScanTableOp op, Node n)
			{
				return op.Table.Columns.Count;
			}

			// Token: 0x06003B7D RID: 15229 RVA: 0x000173E2 File Offset: 0x000155E2
			public override int Visit(SingleRowTableOp op, Node n)
			{
				return 0;
			}

			// Token: 0x06003B7E RID: 15230 RVA: 0x000E067F File Offset: 0x000DE87F
			protected override int VisitSortOp(SortBaseOp op, Node n)
			{
				return base.VisitNode(n.Child0);
			}
		}

		// Token: 0x02000482 RID: 1154
		internal class VarDefinitionRemapper : VarRemapper
		{
			// Token: 0x06003B7F RID: 15231 RVA: 0x000E06B9 File Offset: 0x000DE8B9
			private VarDefinitionRemapper(Var oldVar, Command command) : base(command)
			{
				this.m_oldVar = oldVar;
			}

			// Token: 0x06003B80 RID: 15232 RVA: 0x000E06CC File Offset: 0x000DE8CC
			internal static void RemapSubtree(Node root, Command command, Var oldVar)
			{
				ApplyOpRules.VarDefinitionRemapper varDefinitionRemapper = new ApplyOpRules.VarDefinitionRemapper(oldVar, command);
				varDefinitionRemapper.RemapSubtree(root);
			}

			// Token: 0x06003B81 RID: 15233 RVA: 0x000E06E8 File Offset: 0x000DE8E8
			internal override void RemapSubtree(Node subTree)
			{
				foreach (Node subTree2 in subTree.Children)
				{
					this.RemapSubtree(subTree2);
				}
				this.VisitNode(subTree);
				this.m_command.RecomputeNodeInfo(subTree);
			}

			// Token: 0x06003B82 RID: 15234 RVA: 0x000E0750 File Offset: 0x000DE950
			public override void Visit(VarDefOp op, Node n)
			{
				if (op.Var == this.m_oldVar)
				{
					Var var = this.m_command.CreateComputedVar(n.Child0.Op.Type);
					n.Op = this.m_command.CreateVarDefOp(var);
					base.AddMapping(this.m_oldVar, var);
				}
			}

			// Token: 0x06003B83 RID: 15235 RVA: 0x000E07A8 File Offset: 0x000DE9A8
			public override void Visit(ScanTableOp op, Node n)
			{
				if (op.Table.Columns.Contains(this.m_oldVar))
				{
					ScanTableOp scanTableOp = this.m_command.CreateScanTableOp(op.Table.TableMetadata);
					VarDefListOp varDefListOp = this.m_command.CreateVarDefListOp();
					for (int i = 0; i < op.Table.Columns.Count; i++)
					{
						base.AddMapping(op.Table.Columns[i], scanTableOp.Table.Columns[i]);
					}
					n.Op = scanTableOp;
				}
			}

			// Token: 0x06003B84 RID: 15236 RVA: 0x000E083C File Offset: 0x000DEA3C
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

			// Token: 0x06003B85 RID: 15237 RVA: 0x000E08C4 File Offset: 0x000DEAC4
			private void RemapVarMapKey(VarMap varMap, Var newVar)
			{
				Var value = varMap[this.m_oldVar];
				varMap.Remove(this.m_oldVar);
				varMap.Add(newVar, value);
			}

			// Token: 0x040019BB RID: 6587
			private readonly Var m_oldVar;
		}
	}
}
