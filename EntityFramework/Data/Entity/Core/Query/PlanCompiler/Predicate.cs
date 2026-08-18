using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200068E RID: 1678
	internal class Predicate
	{
		// Token: 0x0600421E RID: 16926 RVA: 0x00137C11 File Offset: 0x00135E11
		internal Predicate(Command command)
		{
			this.m_command = command;
			this.m_parts = new List<Node>();
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x00137C2B File Offset: 0x00135E2B
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal Predicate(Command command, Node andTree) : this(command)
		{
			PlanCompiler.Assert(andTree != null, "null node passed to Predicate() constructor");
			this.InitFromAndTree(andTree);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x00137C4C File Offset: 0x00135E4C
		internal void AddPart(Node n)
		{
			this.m_parts.Add(n);
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x00137C5C File Offset: 0x00135E5C
		internal Node BuildAndTree()
		{
			Node node = null;
			foreach (Node node2 in this.m_parts)
			{
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x00137CD0 File Offset: 0x00135ED0
		internal Predicate GetSingleTablePredicates(VarVec tableDefinitions, out Predicate otherPredicates)
		{
			List<Predicate> list;
			this.GetSingleTablePredicates(new List<VarVec>
			{
				tableDefinitions
			}, out list, out otherPredicates);
			return list[0];
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x00137CFC File Offset: 0x00135EFC
		internal void GetEquiJoinPredicates(VarVec leftTableDefinitions, VarVec rightTableDefinitions, out List<Var> leftTableEquiJoinColumns, out List<Var> rightTableEquiJoinColumns, out Predicate otherPredicates)
		{
			otherPredicates = new Predicate(this.m_command);
			leftTableEquiJoinColumns = new List<Var>();
			rightTableEquiJoinColumns = new List<Var>();
			foreach (Node node in this.m_parts)
			{
				Var item;
				Var item2;
				if (Predicate.IsEquiJoinPredicate(node, leftTableDefinitions, rightTableDefinitions, out item, out item2))
				{
					leftTableEquiJoinColumns.Add(item);
					rightTableEquiJoinColumns.Add(item2);
				}
				else
				{
					otherPredicates.AddPart(node);
				}
			}
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x00137D90 File Offset: 0x00135F90
		internal Predicate GetJoinPredicates(VarVec leftTableDefinitions, VarVec rightTableDefinitions, out Predicate otherPredicates)
		{
			Predicate predicate = new Predicate(this.m_command);
			otherPredicates = new Predicate(this.m_command);
			foreach (Node node in this.m_parts)
			{
				Var var;
				Var var2;
				if (Predicate.IsEquiJoinPredicate(node, leftTableDefinitions, rightTableDefinitions, out var, out var2))
				{
					predicate.AddPart(node);
				}
				else
				{
					otherPredicates.AddPart(node);
				}
			}
			return predicate;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x00137E18 File Offset: 0x00136018
		internal bool SatisfiesKey(VarVec keyVars, VarVec definitions)
		{
			if (keyVars.Count > 0)
			{
				VarVec varVec = keyVars.Clone();
				foreach (Node node in this.m_parts)
				{
					if (node.Op.OpType == OpType.EQ)
					{
						Var v;
						if (this.IsKeyPredicate(node.Child0, node.Child1, keyVars, definitions, out v))
						{
							varVec.Clear(v);
						}
						else if (this.IsKeyPredicate(node.Child1, node.Child0, keyVars, definitions, out v))
						{
							varVec.Clear(v);
						}
					}
				}
				return varVec.IsEmpty;
			}
			return false;
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x00137ED0 File Offset: 0x001360D0
		internal bool PreservesNulls(VarVec tableColumns, bool ansiNullSemantics)
		{
			if (!ansiNullSemantics)
			{
				return true;
			}
			foreach (Node simplePredNode in this.m_parts)
			{
				if (!Predicate.PreservesNulls(simplePredNode, tableColumns))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x00137F34 File Offset: 0x00136134
		private void InitFromAndTree(Node andTree)
		{
			if (andTree.Op.OpType == OpType.And)
			{
				this.InitFromAndTree(andTree.Child0);
				this.InitFromAndTree(andTree.Child1);
				return;
			}
			this.m_parts.Add(andTree);
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x00137F6C File Offset: 0x0013616C
		private void GetSingleTablePredicates(List<VarVec> tableDefinitions, out List<Predicate> singleTablePredicates, out Predicate otherPredicates)
		{
			singleTablePredicates = new List<Predicate>();
			foreach (VarVec varVec in tableDefinitions)
			{
				singleTablePredicates.Add(new Predicate(this.m_command));
			}
			otherPredicates = new Predicate(this.m_command);
			VarVec varVec2 = this.m_command.CreateVarVec();
			foreach (Node n in this.m_parts)
			{
				NodeInfo nodeInfo = this.m_command.GetNodeInfo(n);
				bool flag = false;
				for (int i = 0; i < tableDefinitions.Count; i++)
				{
					VarVec varVec3 = tableDefinitions[i];
					if (varVec3 != null)
					{
						varVec2.InitFrom(nodeInfo.ExternalReferences);
						varVec2.Minus(varVec3);
						if (varVec2.IsEmpty)
						{
							flag = true;
							singleTablePredicates[i].AddPart(n);
							break;
						}
					}
				}
				if (!flag)
				{
					otherPredicates.AddPart(n);
				}
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00138090 File Offset: 0x00136290
		private static bool IsEquiJoinPredicate(Node simplePredicateNode, out Var leftVar, out Var rightVar)
		{
			leftVar = null;
			rightVar = null;
			if (simplePredicateNode.Op.OpType != OpType.EQ)
			{
				return false;
			}
			VarRefOp varRefOp = simplePredicateNode.Child0.Op as VarRefOp;
			if (varRefOp == null)
			{
				return false;
			}
			VarRefOp varRefOp2 = simplePredicateNode.Child1.Op as VarRefOp;
			if (varRefOp2 == null)
			{
				return false;
			}
			leftVar = varRefOp.Var;
			rightVar = varRefOp2.Var;
			return true;
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x001380F4 File Offset: 0x001362F4
		private static bool IsEquiJoinPredicate(Node simplePredicateNode, VarVec leftTableDefinitions, VarVec rightTableDefinitions, out Var leftVar, out Var rightVar)
		{
			leftVar = null;
			rightVar = null;
			Var var;
			Var var2;
			if (!Predicate.IsEquiJoinPredicate(simplePredicateNode, out var, out var2))
			{
				return false;
			}
			if (leftTableDefinitions.IsSet(var) && rightTableDefinitions.IsSet(var2))
			{
				leftVar = var;
				rightVar = var2;
			}
			else
			{
				if (!leftTableDefinitions.IsSet(var2) || !rightTableDefinitions.IsSet(var))
				{
					return false;
				}
				leftVar = var2;
				rightVar = var;
			}
			return true;
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x00138150 File Offset: 0x00136350
		private static bool PreservesNulls(Node simplePredNode, VarVec tableColumns)
		{
			OpType opType = simplePredNode.Op.OpType;
			switch (opType)
			{
			case OpType.GT:
			case OpType.GE:
			case OpType.LE:
			case OpType.LT:
			case OpType.EQ:
			case OpType.NE:
			{
				VarRefOp varRefOp = simplePredNode.Child0.Op as VarRefOp;
				if (varRefOp != null && tableColumns.IsSet(varRefOp.Var))
				{
					return false;
				}
				varRefOp = (simplePredNode.Child1.Op as VarRefOp);
				return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
			}
			case OpType.Like:
			{
				ConstantBaseOp constantBaseOp = simplePredNode.Child1.Op as ConstantBaseOp;
				if (constantBaseOp == null || constantBaseOp.OpType == OpType.Null)
				{
					return true;
				}
				VarRefOp varRefOp = simplePredNode.Child0.Op as VarRefOp;
				return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
			}
			default:
			{
				if (opType != OpType.Not)
				{
					return true;
				}
				if (simplePredNode.Child0.Op.OpType != OpType.IsNull)
				{
					return true;
				}
				VarRefOp varRefOp = simplePredNode.Child0.Child0.Op as VarRefOp;
				return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
			}
			}
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x00138268 File Offset: 0x00136468
		private bool IsKeyPredicate(Node left, Node right, VarVec keyVars, VarVec definitions, out Var keyVar)
		{
			keyVar = null;
			if (left.Op.OpType != OpType.VarRef)
			{
				return false;
			}
			VarRefOp varRefOp = (VarRefOp)left.Op;
			keyVar = varRefOp.Var;
			if (!keyVars.IsSet(keyVar))
			{
				return false;
			}
			NodeInfo nodeInfo = this.m_command.GetNodeInfo(right);
			VarVec varVec = nodeInfo.ExternalReferences.Clone();
			varVec.And(definitions);
			return varVec.IsEmpty;
		}

		// Token: 0x0400188D RID: 6285
		private readonly Command m_command;

		// Token: 0x0400188E RID: 6286
		private readonly List<Node> m_parts;
	}
}
