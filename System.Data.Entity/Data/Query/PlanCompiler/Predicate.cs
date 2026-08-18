using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200005D RID: 93
	internal class Predicate
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x00028A40 File Offset: 0x00026C40
		internal Predicate(Command command)
		{
			this.m_command = command;
			this.m_parts = new List<Node>();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00028A5A File Offset: 0x00026C5A
		internal Predicate(Command command, Node andTree) : this(command)
		{
			PlanCompiler.Assert(andTree != null, "null node passed to Predicate() constructor");
			this.InitFromAndTree(andTree);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00028A78 File Offset: 0x00026C78
		internal void AddPart(Node n)
		{
			this.m_parts.Add(n);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00028A88 File Offset: 0x00026C88
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

		// Token: 0x060007E0 RID: 2016 RVA: 0x00028AFC File Offset: 0x00026CFC
		internal Predicate GetSingleTablePredicates(VarVec tableDefinitions, out Predicate otherPredicates)
		{
			List<Predicate> list;
			this.GetSingleTablePredicates(new List<VarVec>
			{
				tableDefinitions
			}, out list, out otherPredicates);
			return list[0];
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00028B28 File Offset: 0x00026D28
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

		// Token: 0x060007E2 RID: 2018 RVA: 0x00028BBC File Offset: 0x00026DBC
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

		// Token: 0x060007E3 RID: 2019 RVA: 0x00028C44 File Offset: 0x00026E44
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

		// Token: 0x060007E4 RID: 2020 RVA: 0x00028CFC File Offset: 0x00026EFC
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

		// Token: 0x060007E5 RID: 2021 RVA: 0x00028D60 File Offset: 0x00026F60
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

		// Token: 0x060007E6 RID: 2022 RVA: 0x00028D98 File Offset: 0x00026F98
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

		// Token: 0x060007E7 RID: 2023 RVA: 0x00028EC4 File Offset: 0x000270C4
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

		// Token: 0x060007E8 RID: 2024 RVA: 0x00028F28 File Offset: 0x00027128
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

		// Token: 0x060007E9 RID: 2025 RVA: 0x00028F84 File Offset: 0x00027184
		private static bool PreservesNulls(Node simplePredNode, VarVec tableColumns)
		{
			OpType opType = simplePredNode.Op.OpType;
			if (opType - OpType.GT > 5)
			{
				if (opType != OpType.Like)
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
				else
				{
					ConstantBaseOp constantBaseOp = simplePredNode.Child1.Op as ConstantBaseOp;
					if (constantBaseOp == null || constantBaseOp.OpType == OpType.Null)
					{
						return true;
					}
					VarRefOp varRefOp = simplePredNode.Child0.Op as VarRefOp;
					return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
				}
			}
			else
			{
				VarRefOp varRefOp = simplePredNode.Child0.Op as VarRefOp;
				if (varRefOp != null && tableColumns.IsSet(varRefOp.Var))
				{
					return false;
				}
				varRefOp = (simplePredNode.Child1.Op as VarRefOp);
				return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00029088 File Offset: 0x00027288
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

		// Token: 0x040007E2 RID: 2018
		private Command m_command;

		// Token: 0x040007E3 RID: 2019
		private List<Node> m_parts;
	}
}
