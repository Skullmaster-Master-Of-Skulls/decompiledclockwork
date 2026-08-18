using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A3 RID: 419
	public static class AppCancelReasonOrGroupForestMapper
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x0001F534 File Offset: 0x0001D734
		public static Forest<AppCancelReasonOrGroup> ToDomainObject(this Forest<AppCancelReasonOrGroupDTO> dto)
		{
			Forest<AppCancelReasonOrGroup> forest = new Forest<AppCancelReasonOrGroup>();
			TreeNodeCollection<AppCancelReasonOrGroup> nodes = forest.Nodes;
			AppCancelReasonOrGroupForestMapper.CopyNodes(dto.Nodes, ref forest, ref nodes, null);
			return forest;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001F568 File Offset: 0x0001D768
		private static void CopyNodes(TreeNodeCollection<AppCancelReasonOrGroupDTO> sourceNodes, ref Forest<AppCancelReasonOrGroup> destForest, ref TreeNodeCollection<AppCancelReasonOrGroup> destNodes, TreeNode<AppCancelReasonOrGroup> sourceParentNode)
		{
			foreach (TreeNode<AppCancelReasonOrGroupDTO> treeNode in sourceNodes)
			{
				TreeNode<AppCancelReasonOrGroup> treeNode2 = destForest.AppendNode(sourceParentNode, treeNode.Value.ToDomainObject());
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					TreeNodeCollection<AppCancelReasonOrGroup> nodes = treeNode2.Nodes;
					AppCancelReasonOrGroupForestMapper.CopyNodes(treeNode.Nodes, ref destForest, ref nodes, treeNode2);
				}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		private static void CopyNodes(TreeNodeCollection<AppCancelReasonOrGroup> sourceNodes, ref Forest<AppCancelReasonOrGroupDTO> destForest, ref TreeNodeCollection<AppCancelReasonOrGroupDTO> destNodes, TreeNode<AppCancelReasonOrGroupDTO> sourceParentNode)
		{
			foreach (TreeNode<AppCancelReasonOrGroup> treeNode in sourceNodes)
			{
				TreeNode<AppCancelReasonOrGroupDTO> treeNode2 = destForest.AppendNode(sourceParentNode, treeNode.Value.ToDTO());
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					TreeNodeCollection<AppCancelReasonOrGroupDTO> nodes = treeNode2.Nodes;
					AppCancelReasonOrGroupForestMapper.CopyNodes(treeNode.Nodes, ref destForest, ref nodes, treeNode2);
				}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001F678 File Offset: 0x0001D878
		public static Forest<AppCancelReasonOrGroupDTO> ToDTO(this Forest<AppCancelReasonOrGroup> item)
		{
			Forest<AppCancelReasonOrGroupDTO> forest = new Forest<AppCancelReasonOrGroupDTO>();
			TreeNodeCollection<AppCancelReasonOrGroupDTO> nodes = forest.Nodes;
			AppCancelReasonOrGroupForestMapper.CopyNodes(item.Nodes, ref forest, ref nodes, null);
			return forest;
		}
	}
}
