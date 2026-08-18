using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.Mappers.Templates
{
	// Token: 0x0200003A RID: 58
	public static class TemplateOrGroupForestMapper
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00007180 File Offset: 0x00005380
		public static Forest<TemplateOrGroup> ToDomainObject(this Forest<TemplateOrGroupDTO> dto)
		{
			Forest<TemplateOrGroup> result = new Forest<TemplateOrGroup>();
			TemplateOrGroupForestMapper.CopyNodes(ref result, dto, null, null);
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000071A4 File Offset: 0x000053A4
		private static void CopyNodes(ref Forest<TemplateOrGroup> destForest, Forest<TemplateOrGroupDTO> sourceForest, TreeNode<TemplateOrGroupDTO> sourceParent, TreeNode<TemplateOrGroup> destParent)
		{
			TreeNodeCollection<TemplateOrGroupDTO> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<TemplateOrGroupDTO> treeNode in treeNodeCollection)
			{
				TreeNode<TemplateOrGroup> destParent2 = destForest.AppendNode(destParent, new TemplateOrGroup
				{
					Template = treeNode.Value.Template.ToDomainObject(),
					Group = treeNode.Value.Group.ToDomainObject()
				});
				TemplateOrGroupForestMapper.CopyNodes(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007244 File Offset: 0x00005444
		private static void CopyNodesDTO(ref Forest<TemplateOrGroupDTO> destForest, Forest<TemplateOrGroup> sourceForest, TreeNode<TemplateOrGroup> sourceParent, TreeNode<TemplateOrGroupDTO> destParent)
		{
			TreeNodeCollection<TemplateOrGroup> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<TemplateOrGroup> treeNode in treeNodeCollection)
			{
				TreeNode<TemplateOrGroupDTO> destParent2 = destForest.AppendNode(destParent, new TemplateOrGroupDTO
				{
					Template = treeNode.Value.Template.ToDTO(),
					Group = treeNode.Value.Group.ToDTO()
				});
				TemplateOrGroupForestMapper.CopyNodesDTO(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000072E4 File Offset: 0x000054E4
		public static Forest<TemplateOrGroupDTO> ToDTO(this Forest<TemplateOrGroup> item)
		{
			Forest<TemplateOrGroupDTO> result = new Forest<TemplateOrGroupDTO>();
			TemplateOrGroupForestMapper.CopyNodesDTO(ref result, item, null, null);
			return result;
		}
	}
}
