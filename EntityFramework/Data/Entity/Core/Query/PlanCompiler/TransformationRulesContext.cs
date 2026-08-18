using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A5 RID: 1701
	internal class TransformationRulesContext : RuleProcessingContext
	{
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x0013FFAC File Offset: 0x0013E1AC
		internal PlanCompiler PlanCompiler
		{
			get
			{
				return this.m_compilerState;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x0013FFB4 File Offset: 0x0013E1B4
		internal bool ProjectionPrunningRequired
		{
			get
			{
				return this.m_projectionPrunningRequired;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x0013FFBC File Offset: 0x0013E1BC
		internal bool ReapplyNullabilityRules
		{
			get
			{
				return this.m_reapplyNullabilityRules;
			}
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x0013FFC4 File Offset: 0x0013E1C4
		internal void RemapSubtree(Node subTree)
		{
			this.m_remapper.RemapSubtree(subTree);
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x0013FFD2 File Offset: 0x0013E1D2
		internal void AddVarMapping(Var oldVar, Var newVar)
		{
			this.m_remapper.AddMapping(oldVar, newVar);
			this.m_remappedVars.Set(oldVar);
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x0013FFF0 File Offset: 0x0013E1F0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "scalarOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600436E RID: 17262 RVA: 0x001400A4 File Offset: 0x0013E2A4
		internal Node Copy(Node node)
		{
			if (node.Op.OpType == OpType.VarRef)
			{
				VarRefOp varRefOp = node.Op as VarRefOp;
				return base.Command.CreateNode(base.Command.CreateVarRefOp(varRefOp.Var));
			}
			return OpCopier.Copy(base.Command, node);
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x001400F4 File Offset: 0x0013E2F4
		internal bool IsScalarOpTree(Node node)
		{
			int num = 0;
			return this.IsScalarOpTree(node, null, ref num);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x00140110 File Offset: 0x0013E310
		internal bool IsNonNullable(Var variable)
		{
			if (variable.VarType == VarType.Parameter && !TypeSemantics.IsNullable(variable.Type))
			{
				return true;
			}
			foreach (Node n in this.m_relOpAncestors)
			{
				base.Command.RecomputeNodeInfo(n);
				ExtendedNodeInfo extendedNodeInfo = base.Command.GetExtendedNodeInfo(n);
				if (extendedNodeInfo.NonNullableVisibleDefinitions.IsSet(variable))
				{
					return true;
				}
				if (extendedNodeInfo.LocalDefinitions.IsSet(variable))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x001401E8 File Offset: 0x0013E3E8
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

		// Token: 0x06004372 RID: 17266 RVA: 0x001402B4 File Offset: 0x0013E4B4
		internal static bool IsOpNotSafeForNullSentinelValueChange(OpType optype)
		{
			return optype == OpType.Distinct || optype == OpType.GroupBy || optype == OpType.Intersect || optype == OpType.Except;
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x001402CC File Offset: 0x0013E4CC
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

		// Token: 0x06004374 RID: 17268 RVA: 0x0014033C File Offset: 0x0013E53C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "varRef")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal bool IsScalarOpTree(Node node, Dictionary<Var, int> varRefMap)
		{
			PlanCompiler.Assert(varRefMap != null, "Null varRef map");
			int num = 0;
			return this.IsScalarOpTree(node, varRefMap, ref num);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x00140368 File Offset: 0x0013E568
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "varDef")]
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

		// Token: 0x06004376 RID: 17270 RVA: 0x00140454 File Offset: 0x0013E654
		internal Node BuildNullIfExpression(Var conditionVar, Node expr)
		{
			VarRefOp op = base.Command.CreateVarRefOp(conditionVar);
			Node arg = base.Command.CreateNode(op);
			Node arg2 = base.Command.CreateNode(base.Command.CreateConditionalOp(OpType.IsNull), arg);
			Node arg3 = base.Command.CreateNode(base.Command.CreateNullOp(expr.Op.Type));
			return base.Command.CreateNode(base.Command.CreateCaseOp(expr.Op.Type), arg2, arg3, expr);
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x001404E3 File Offset: 0x0013E6E3
		internal void SuppressFilterPushdown(Node n)
		{
			this.m_suppressions[n] = n;
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x001404F2 File Offset: 0x0013E6F2
		internal bool IsFilterPushdownSuppressed(Node n)
		{
			return this.m_suppressions.ContainsKey(n);
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x00140500 File Offset: 0x0013E700
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

		// Token: 0x0600437A RID: 17274 RVA: 0x00140564 File Offset: 0x0013E764
		internal TransformationRulesContext(PlanCompiler compilerState) : base(compilerState.Command)
		{
			this.m_compilerState = compilerState;
			this.m_remapper = new VarRemapper(compilerState.Command);
			this.m_suppressions = new Dictionary<Node, Node>();
			this.m_remappedVars = compilerState.Command.CreateVarVec();
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x001405BC File Offset: 0x0013E7BC
		internal override void PreProcess(Node n)
		{
			this.m_remapper.RemapNode(n);
			base.Command.RecomputeNodeInfo(n);
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x001405D8 File Offset: 0x0013E7D8
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

		// Token: 0x0600437D RID: 17277 RVA: 0x00140670 File Offset: 0x0013E870
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RelOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal override void PostProcessSubTree(Node subtree)
		{
			if (subtree.Op.IsRelOp)
			{
				PlanCompiler.Assert(this.m_relOpAncestors.Count != 0, "The RelOp ancestors stack is empty when post processing a RelOp subtree");
				Node objB = this.m_relOpAncestors.Pop();
				PlanCompiler.Assert(object.ReferenceEquals(subtree, objB), "The popped ancestor is not equal to the root of the subtree being post processed");
			}
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x001406C4 File Offset: 0x0013E8C4
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

		// Token: 0x0600437F RID: 17279 RVA: 0x00140718 File Offset: 0x0013E918
		internal override int GetHashCode(Node node)
		{
			NodeInfo nodeInfo = base.Command.GetNodeInfo(node);
			return nodeInfo.HashValue;
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x00140738 File Offset: 0x0013E938
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

		// Token: 0x040018FC RID: 6396
		private readonly PlanCompiler m_compilerState;

		// Token: 0x040018FD RID: 6397
		private readonly VarRemapper m_remapper;

		// Token: 0x040018FE RID: 6398
		private readonly Dictionary<Node, Node> m_suppressions;

		// Token: 0x040018FF RID: 6399
		private readonly VarVec m_remappedVars;

		// Token: 0x04001900 RID: 6400
		private bool m_projectionPrunningRequired;

		// Token: 0x04001901 RID: 6401
		private bool m_reapplyNullabilityRules;

		// Token: 0x04001902 RID: 6402
		private readonly Stack<Node> m_relOpAncestors = new Stack<Node>();
	}
}
