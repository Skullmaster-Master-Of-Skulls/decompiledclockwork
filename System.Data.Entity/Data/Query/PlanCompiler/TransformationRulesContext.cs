using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200006E RID: 110
	internal class TransformationRulesContext : RuleProcessingContext
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0002DF8D File Offset: 0x0002C18D
		internal bool ProjectionPrunningRequired
		{
			get
			{
				return this.m_projectionPrunningRequired;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0002DF95 File Offset: 0x0002C195
		internal bool ReapplyNullabilityRules
		{
			get
			{
				return this.m_reapplyNullabilityRules;
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002DF9D File Offset: 0x0002C19D
		internal void RemapSubtree(Node subTree)
		{
			this.m_remapper.RemapSubtree(subTree);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002DFAB File Offset: 0x0002C1AB
		internal void AddVarMapping(Var oldVar, Var newVar)
		{
			this.m_remapper.AddMapping(oldVar, newVar);
			this.m_remappedVars.Set(oldVar);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		internal Node ReMap(Node node, Dictionary<Var, Node> varMap)
		{
			PlanCompiler.Assert(node.Op.IsScalarOp, "Expected a scalarOp: Found " + Dump.AutoString.ToString(node.Op.OpType));
			if (node.Op.OpType != OpType.VarRef)
			{
				for (int i = 0; i < node.Children.Count; i++)
				{
					node.Children[i] = this.ReMap(node.Children[i], varMap);
				}
				base.Command.RecomputeNodeInfo(node);
				return node;
			}
			VarRefOp varRefOp = node.Op as VarRefOp;
			Node node2 = null;
			if (varMap.TryGetValue(varRefOp.Var, out node2))
			{
				node2 = this.Copy(node2);
				return node2;
			}
			return node;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002E07C File Offset: 0x0002C27C
		internal Node Copy(Node node)
		{
			if (node.Op.OpType == OpType.VarRef)
			{
				VarRefOp varRefOp = node.Op as VarRefOp;
				return base.Command.CreateNode(base.Command.CreateVarRefOp(varRefOp.Var));
			}
			return OpCopier.Copy(base.Command, node);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002E0CC File Offset: 0x0002C2CC
		internal bool IsScalarOpTree(Node node)
		{
			int num = 0;
			return this.IsScalarOpTree(node, null, ref num);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		internal bool IsNonNullable(Var var)
		{
			foreach (Node n in this.m_relOpAncestors)
			{
				base.Command.RecomputeNodeInfo(n);
				ExtendedNodeInfo extendedNodeInfo = base.Command.GetExtendedNodeInfo(n);
				if (extendedNodeInfo.NonNullableVisibleDefinitions.IsSet(var))
				{
					return true;
				}
				if (extendedNodeInfo.LocalDefinitions.IsSet(var))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0002E174 File Offset: 0x0002C374
		internal bool CanChangeNullSentinelValue
		{
			get
			{
				if (this.m_compilerState.HasSortingOnNullSentinels)
				{
					return false;
				}
				if (this.m_relOpAncestors.Any((Node a) => TransformationRulesContext.IsOpNotSafeForNullSentinelValueChange(a.Op.OpType)))
				{
					return false;
				}
				IEnumerable<Node> enumerable = from a in this.m_relOpAncestors
				where a.Op.OpType == OpType.CrossApply || a.Op.OpType == OpType.OuterApply
				select a;
				foreach (Node node in enumerable)
				{
					if (!this.m_relOpAncestors.Contains(node.Child1) && TransformationRulesContext.HasOpNotSafeForNullSentinelValueChange(node.Child1))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002E244 File Offset: 0x0002C444
		internal static bool IsOpNotSafeForNullSentinelValueChange(OpType optype)
		{
			return optype == OpType.Distinct || optype == OpType.GroupBy || optype == OpType.Intersect || optype == OpType.Except;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002E25C File Offset: 0x0002C45C
		internal static bool HasOpNotSafeForNullSentinelValueChange(Node n)
		{
			if (TransformationRulesContext.IsOpNotSafeForNullSentinelValueChange(n.Op.OpType))
			{
				return true;
			}
			foreach (Node n2 in n.Children)
			{
				if (TransformationRulesContext.HasOpNotSafeForNullSentinelValueChange(n2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002E2CC File Offset: 0x0002C4CC
		internal bool IsScalarOpTree(Node node, Dictionary<Var, int> varRefMap)
		{
			PlanCompiler.Assert(varRefMap != null, "Null varRef map");
			int num = 0;
			return this.IsScalarOpTree(node, varRefMap, ref num);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
		internal Dictionary<Var, Node> GetVarMap(Node varDefListNode, Dictionary<Var, int> varRefMap)
		{
			VarDefListOp varDefListOp = (VarDefListOp)varDefListNode.Op;
			Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>();
			foreach (Node node in varDefListNode.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node.Op;
				int num = 0;
				int num2 = 0;
				if (!this.IsScalarOpTree(node.Child0, null, ref num))
				{
					return null;
				}
				if (num > 100 && varRefMap != null && varRefMap.TryGetValue(varDefOp.Var, out num2) && num2 > 2)
				{
					return null;
				}
				Node node2;
				if (dictionary.TryGetValue(varDefOp.Var, out node2))
				{
					PlanCompiler.Assert(node2 == node.Child0, "reusing varDef for different Node?");
				}
				else
				{
					dictionary.Add(varDefOp.Var, node.Child0);
				}
			}
			return dictionary;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
		internal Node BuildNullIfExpression(Var conditionVar, Node expr)
		{
			VarRefOp op = base.Command.CreateVarRefOp(conditionVar);
			Node arg = base.Command.CreateNode(op);
			Node arg2 = base.Command.CreateNode(base.Command.CreateConditionalOp(OpType.IsNull), arg);
			Node arg3 = base.Command.CreateNode(base.Command.CreateNullOp(expr.Op.Type));
			return base.Command.CreateNode(base.Command.CreateCaseOp(expr.Op.Type), arg2, arg3, expr);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002E473 File Offset: 0x0002C673
		internal void SuppressFilterPushdown(Node n)
		{
			this.m_suppressions[n] = n;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002E482 File Offset: 0x0002C682
		internal bool IsFilterPushdownSuppressed(Node n)
		{
			return this.m_suppressions.ContainsKey(n);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002E490 File Offset: 0x0002C690
		internal static bool TryGetInt32Var(IEnumerable<Var> varList, out Var int32Var)
		{
			foreach (Var var in varList)
			{
				PrimitiveTypeKind primitiveTypeKind;
				if (TypeHelpers.TryGetPrimitiveTypeKind(var.Type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Int32)
				{
					int32Var = var;
					return true;
				}
			}
			int32Var = null;
			return false;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
		internal TransformationRulesContext(PlanCompiler compilerState) : base(compilerState.Command)
		{
			this.m_compilerState = compilerState;
			this.m_remapper = new VarRemapper(compilerState.Command);
			this.m_suppressions = new Dictionary<Node, Node>();
			this.m_remappedVars = compilerState.Command.CreateVarVec();
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002E54C File Offset: 0x0002C74C
		internal override void PreProcess(Node n)
		{
			this.m_remapper.RemapNode(n);
			base.Command.RecomputeNodeInfo(n);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002E568 File Offset: 0x0002C768
		internal override void PreProcessSubTree(Node subTree)
		{
			if (subTree.Op.IsRelOp)
			{
				this.m_relOpAncestors.Push(subTree);
			}
			if (this.m_remappedVars.IsEmpty)
			{
				return;
			}
			NodeInfo nodeInfo = base.Command.GetNodeInfo(subTree);
			foreach (Var v in nodeInfo.ExternalReferences)
			{
				if (this.m_remappedVars.IsSet(v))
				{
					this.m_remapper.RemapSubtree(subTree);
					break;
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002E600 File Offset: 0x0002C800
		internal override void PostProcessSubTree(Node subtree)
		{
			if (subtree.Op.IsRelOp)
			{
				PlanCompiler.Assert(this.m_relOpAncestors.Count != 0, "The RelOp ancestors stack is empty when post processing a RelOp subtree");
				Node node = this.m_relOpAncestors.Pop();
				PlanCompiler.Assert(subtree == node, "The popped ancestor is not equal to the root of the subtree being post processed");
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002E64C File Offset: 0x0002C84C
		internal override void PostProcess(Node n, Rule rule)
		{
			if (rule != null)
			{
				if (!this.m_projectionPrunningRequired && TransformationRules.RulesRequiringProjectionPruning.Contains(rule))
				{
					this.m_projectionPrunningRequired = true;
				}
				if (!this.m_reapplyNullabilityRules && TransformationRules.RulesRequiringNullabilityRulesToBeReapplied.Contains(rule))
				{
					this.m_reapplyNullabilityRules = true;
				}
				base.Command.RecomputeNodeInfo(n);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002E6A0 File Offset: 0x0002C8A0
		internal override int GetHashCode(Node node)
		{
			NodeInfo nodeInfo = base.Command.GetNodeInfo(node);
			return nodeInfo.HashValue;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002E6C0 File Offset: 0x0002C8C0
		private bool IsScalarOpTree(Node node, Dictionary<Var, int> varRefMap, ref int nonLeafNodeCount)
		{
			if (!node.Op.IsScalarOp)
			{
				return false;
			}
			if (node.HasChild0)
			{
				nonLeafNodeCount++;
			}
			if (varRefMap != null && node.Op.OpType == OpType.VarRef)
			{
				VarRefOp varRefOp = (VarRefOp)node.Op;
				int num;
				if (!varRefMap.TryGetValue(varRefOp.Var, out num))
				{
					num = 1;
				}
				else
				{
					num++;
				}
				varRefMap[varRefOp.Var] = num;
			}
			foreach (Node node2 in node.Children)
			{
				if (!this.IsScalarOpTree(node2, varRefMap, ref nonLeafNodeCount))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400080C RID: 2060
		private readonly PlanCompiler m_compilerState;

		// Token: 0x0400080D RID: 2061
		private readonly VarRemapper m_remapper;

		// Token: 0x0400080E RID: 2062
		private readonly Dictionary<Node, Node> m_suppressions;

		// Token: 0x0400080F RID: 2063
		private readonly VarVec m_remappedVars;

		// Token: 0x04000810 RID: 2064
		private bool m_projectionPrunningRequired;

		// Token: 0x04000811 RID: 2065
		private bool m_reapplyNullabilityRules;

		// Token: 0x04000812 RID: 2066
		private Stack<Node> m_relOpAncestors = new Stack<Node>();
	}
}
