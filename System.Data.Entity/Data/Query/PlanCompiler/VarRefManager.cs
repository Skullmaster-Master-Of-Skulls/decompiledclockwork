using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000044 RID: 68
	internal class VarRefManager
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x00018994 File Offset: 0x00016B94
		internal VarRefManager(Command command)
		{
			this.m_nodeToParentMap = new Dictionary<Node, Node>();
			this.m_nodeToSiblingNumber = new Dictionary<Node, int>();
			this.m_command = command;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000189BC File Offset: 0x00016BBC
		internal void AddChildren(Node parent)
		{
			for (int i = 0; i < parent.Children.Count; i++)
			{
				this.m_nodeToParentMap[parent.Children[i]] = parent;
				this.m_nodeToSiblingNumber[parent.Children[i]] = i;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00018A10 File Offset: 0x00016C10
		internal bool HasKeyReferences(VarVec keys, Node definingNode, Node targetJoinNode)
		{
			Node key = definingNode;
			bool flag = true;
			Node node;
			while (flag & this.m_nodeToParentMap.TryGetValue(key, out node))
			{
				if (node != targetJoinNode)
				{
					if (VarRefManager.HasVarReferencesShallow(node, keys, this.m_nodeToSiblingNumber[key], out flag))
					{
						return true;
					}
					for (int i = this.m_nodeToSiblingNumber[key] + 1; i < node.Children.Count; i++)
					{
						if (node.Children[i].GetNodeInfo(this.m_command).ExternalReferences.Overlaps(keys))
						{
							return true;
						}
					}
				}
				key = node;
			}
			return false;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018AA0 File Offset: 0x00016CA0
		private static bool HasVarReferencesShallow(Node node, VarVec vars, int childIndex, out bool continueUp)
		{
			OpType opType = node.Op.OpType;
			if (opType != OpType.Project)
			{
				switch (opType)
				{
				case OpType.Sort:
				case OpType.ConstrainedSort:
					continueUp = true;
					return VarRefManager.HasVarReferences(((SortBaseOp)node.Op).Keys, vars);
				case OpType.GroupBy:
					continueUp = false;
					return VarRefManager.HasVarReferences(((GroupByOp)node.Op).Keys, vars);
				case OpType.UnionAll:
				case OpType.Intersect:
				case OpType.Except:
					continueUp = false;
					return VarRefManager.HasVarReferences((SetOp)node.Op, vars, childIndex);
				case OpType.Distinct:
					continueUp = false;
					return VarRefManager.HasVarReferences(((DistinctOp)node.Op).Keys, vars);
				case OpType.PhysicalProject:
					continueUp = false;
					return VarRefManager.HasVarReferences(((PhysicalProjectOp)node.Op).Outputs, vars);
				}
				continueUp = true;
				return false;
			}
			continueUp = false;
			return VarRefManager.HasVarReferences(((ProjectOp)node.Op).Outputs, vars);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00018BA4 File Offset: 0x00016DA4
		private static bool HasVarReferences(VarList listToCheck, VarVec vars)
		{
			foreach (Var item in vars)
			{
				if (listToCheck.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00018BF8 File Offset: 0x00016DF8
		private static bool HasVarReferences(VarVec listToCheck, VarVec vars)
		{
			return listToCheck.Overlaps(vars);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018C04 File Offset: 0x00016E04
		private static bool HasVarReferences(List<SortKey> listToCheck, VarVec vars)
		{
			foreach (SortKey sortKey in listToCheck)
			{
				if (vars.IsSet(sortKey.Var))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00018C60 File Offset: 0x00016E60
		private static bool HasVarReferences(SetOp op, VarVec vars, int index)
		{
			foreach (Var v in op.VarMap[index].Values)
			{
				if (vars.IsSet(v))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000759 RID: 1881
		private Dictionary<Node, Node> m_nodeToParentMap;

		// Token: 0x0400075A RID: 1882
		private Dictionary<Node, int> m_nodeToSiblingNumber;

		// Token: 0x0400075B RID: 1883
		private Command m_command;
	}
}
