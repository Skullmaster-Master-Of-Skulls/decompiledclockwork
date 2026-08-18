using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000094 RID: 148
	public static class ReportOrGroupForestMapper
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		public static Forest<ReportOrGroup> ToDomainObject(this Forest<ReportOrGroupDTO> dto)
		{
			Forest<ReportOrGroup> result = new Forest<ReportOrGroup>();
			ReportOrGroupForestMapper.CopyNodes(ref result, dto, null, null);
			return result;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		private static void CopyNodes(ref Forest<ReportOrGroup> destForest, Forest<ReportOrGroupDTO> sourceForest, TreeNode<ReportOrGroupDTO> sourceParent, TreeNode<ReportOrGroup> destParent)
		{
			TreeNodeCollection<ReportOrGroupDTO> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<ReportOrGroupDTO> treeNode in treeNodeCollection)
			{
				TreeNode<ReportOrGroup> destParent2 = destForest.AppendNode(destParent, new ReportOrGroup
				{
					Report = treeNode.Value.Report.ToDomainObject(),
					Group = treeNode.Value.Group.ToDomainObject()
				});
				ReportOrGroupForestMapper.CopyNodes(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E27C File Offset: 0x0000C47C
		private static void CopyNodesDTO(ref Forest<ReportOrGroupDTO> destForest, Forest<ReportOrGroup> sourceForest, TreeNode<ReportOrGroup> sourceParent, TreeNode<ReportOrGroupDTO> destParent)
		{
			TreeNodeCollection<ReportOrGroup> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<ReportOrGroup> treeNode in treeNodeCollection)
			{
				TreeNode<ReportOrGroupDTO> destParent2 = destForest.AppendNode(destParent, new ReportOrGroupDTO
				{
					Report = treeNode.Value.Report.ToDTO(),
					Group = treeNode.Value.Group.ToDTO()
				});
				ReportOrGroupForestMapper.CopyNodesDTO(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E31C File Offset: 0x0000C51C
		public static Forest<ReportOrGroupDTO> ToDTO(this Forest<ReportOrGroup> item)
		{
			Forest<ReportOrGroupDTO> result = new Forest<ReportOrGroupDTO>();
			ReportOrGroupForestMapper.CopyNodesDTO(ref result, item, null, null);
			return result;
		}
	}
}
