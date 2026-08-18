using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.Mappers.AppointmentsWorkshops
{
	// Token: 0x0200019E RID: 414
	public static class WorkshopDefinitionOrAppTypeForestMapper
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
		public static Forest<WorkshopDefinitionOrAppType> ToDomainObject(this Forest<WorkshopDefinitionOrAppTypeDTO> dto)
		{
			Forest<WorkshopDefinitionOrAppType> result = new Forest<WorkshopDefinitionOrAppType>();
			WorkshopDefinitionOrAppTypeForestMapper.CopyNodes(ref result, dto, null, null);
			return result;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		private static void CopyNodes(ref Forest<WorkshopDefinitionOrAppType> destForest, Forest<WorkshopDefinitionOrAppTypeDTO> sourceForest, TreeNode<WorkshopDefinitionOrAppTypeDTO> sourceParent, TreeNode<WorkshopDefinitionOrAppType> destParent)
		{
			TreeNodeCollection<WorkshopDefinitionOrAppTypeDTO> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<WorkshopDefinitionOrAppTypeDTO> treeNode in treeNodeCollection)
			{
				TreeNode<WorkshopDefinitionOrAppType> destParent2 = destForest.AppendNode(destParent, new WorkshopDefinitionOrAppType
				{
					WorkshopDefinition = treeNode.Value.WorkshopDefinition.ToDomainObject(),
					AppType = treeNode.Value.AppType.ToDomainObject()
				});
				WorkshopDefinitionOrAppTypeForestMapper.CopyNodes(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001F284 File Offset: 0x0001D484
		private static void CopyNodesDTO(ref Forest<WorkshopDefinitionOrAppTypeDTO> destForest, Forest<WorkshopDefinitionOrAppType> sourceForest, TreeNode<WorkshopDefinitionOrAppType> sourceParent, TreeNode<WorkshopDefinitionOrAppTypeDTO> destParent)
		{
			TreeNodeCollection<WorkshopDefinitionOrAppType> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<WorkshopDefinitionOrAppType> treeNode in treeNodeCollection)
			{
				TreeNode<WorkshopDefinitionOrAppTypeDTO> destParent2 = destForest.AppendNode(destParent, new WorkshopDefinitionOrAppTypeDTO
				{
					WorkshopDefinition = treeNode.Value.WorkshopDefinition.ToDTO(),
					AppType = treeNode.Value.AppType.ToDTO()
				});
				WorkshopDefinitionOrAppTypeForestMapper.CopyNodesDTO(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001F324 File Offset: 0x0001D524
		public static Forest<WorkshopDefinitionOrAppTypeDTO> ToDTO(this Forest<WorkshopDefinitionOrAppType> item)
		{
			Forest<WorkshopDefinitionOrAppTypeDTO> result = new Forest<WorkshopDefinitionOrAppTypeDTO>();
			WorkshopDefinitionOrAppTypeForestMapper.CopyNodesDTO(ref result, item, null, null);
			return result;
		}
	}
}
