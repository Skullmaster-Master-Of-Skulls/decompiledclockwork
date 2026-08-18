using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006AC RID: 1708
	internal class VarRefManager
	{
		// Token: 0x06004395 RID: 17301 RVA: 0x00140973 File Offset: 0x0013EB73
		internal VarRefManager(Command command)
		{
			this.m_nodeToParentMap = new Dictionary<Node, Node>();
			this.m_nodeToSiblingNumber = new Dictionary<Node, int>();
			this.m_command = command;
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00140998 File Offset: 0x0013EB98
		internal void AddChildren(Node parent)
		{
			for (int i = 0; i < parent.Children.Count; i++)
			{
				this.m_nodeToParentMap[parent.Children[i]] = parent;
				this.m_nodeToSiblingNumber[parent.Children[i]] = i;
			}
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x001409EC File Offset: 0x0013EBEC
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

		// Token: 0x06004398 RID: 17304 RVA: 0x00140A7C File Offset: 0x0013EC7C
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

		// Token: 0x06004399 RID: 17305 RVA: 0x00140B80 File Offset: 0x0013ED80
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

		// Token: 0x0600439A RID: 17306 RVA: 0x00140BD4 File Offset: 0x0013EDD4
		private static bool HasVarReferences(VarVec listToCheck, VarVec vars)
		{
			return listToCheck.Overlaps(vars);
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x00140BE0 File Offset: 0x0013EDE0
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

		// Token: 0x0600439C RID: 17308 RVA: 0x00140C3C File Offset: 0x0013EE3C
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

		// Token: 0x0400190F RID: 6415
		private readonly Dictionary<Node, Node> m_nodeToParentMap;

		// Token: 0x04001910 RID: 6416
		private readonly Dictionary<Node, int> m_nodeToSiblingNumber;

		// Token: 0x04001911 RID: 6417
		private readonly Command m_command;
	}
}
