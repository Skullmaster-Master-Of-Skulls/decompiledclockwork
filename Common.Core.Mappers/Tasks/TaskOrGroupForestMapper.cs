using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Mappers.Tasks
{
	// Token: 0x0200004B RID: 75
	public static class TaskOrGroupForestMapper
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00008F48 File Offset: 0x00007148
		public static Forest<TaskOrGroup> ToDomainObject(this Forest<TaskOrGroupDTO> dto)
		{
			Forest<TaskOrGroup> forest = new Forest<TaskOrGroup>();
			TreeNodeCollection<TaskOrGroup> nodes = forest.Nodes;
			TaskOrGroupForestMapper.CopyNodes(dto.Nodes, ref forest, ref nodes, null);
			return forest;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008F7C File Offset: 0x0000717C
		private static void CopyNodes(TreeNodeCollection<TaskOrGroupDTO> sourceNodes, ref Forest<TaskOrGroup> destForest, ref TreeNodeCollection<TaskOrGroup> destNodes, TreeNode<TaskOrGroup> sourceParentNode)
		{
			foreach (TreeNode<TaskOrGroupDTO> treeNode in sourceNodes)
			{
				TreeNode<TaskOrGroup> treeNode2 = destForest.AppendNode(sourceParentNode, treeNode.Value.ToDomainObject());
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					TreeNodeCollection<TaskOrGroup> nodes = treeNode2.Nodes;
					TaskOrGroupForestMapper.CopyNodes(treeNode.Nodes, ref destForest, ref nodes, treeNode2);
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009004 File Offset: 0x00007204
		private static void CopyNodes(TreeNodeCollection<TaskOrGroup> sourceNodes, ref Forest<TaskOrGroupDTO> destForest, ref TreeNodeCollection<TaskOrGroupDTO> destNodes, TreeNode<TaskOrGroupDTO> sourceParentNode)
		{
			foreach (TreeNode<TaskOrGroup> treeNode in sourceNodes)
			{
				TreeNode<TaskOrGroupDTO> treeNode2 = destForest.AppendNode(sourceParentNode, treeNode.Value.ToDTO());
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					TreeNodeCollection<TaskOrGroupDTO> nodes = treeNode2.Nodes;
					TaskOrGroupForestMapper.CopyNodes(treeNode.Nodes, ref destForest, ref nodes, treeNode2);
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000908C File Offset: 0x0000728C
		public static Forest<TaskOrGroupDTO> ToDTO(this Forest<TaskOrGroup> item)
		{
			Forest<TaskOrGroupDTO> forest = new Forest<TaskOrGroupDTO>();
			TreeNodeCollection<TaskOrGroupDTO> nodes = forest.Nodes;
			TaskOrGroupForestMapper.CopyNodes(item.Nodes, ref forest, ref nodes, null);
			return forest;
		}
	}
}
