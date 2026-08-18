using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000119 RID: 281
	public static class DynamicFieldForestMapper
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x000174C0 File Offset: 0x000156C0
		public static Forest<DynamicField> ToDomainObject(this Forest<DynamicFieldDTO> dto)
		{
			Forest<DynamicField> forest = new Forest<DynamicField>();
			TreeNodeCollection<DynamicField> nodes = forest.Nodes;
			DynamicFieldForestMapper.CopyNodes(dto.Nodes, ref forest, ref nodes, null);
			return forest;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000174F4 File Offset: 0x000156F4
		private static void CopyNodes(TreeNodeCollection<DynamicFieldDTO> sourceNodes, ref Forest<DynamicField> destForest, ref TreeNodeCollection<DynamicField> destNodes, TreeNode<DynamicField> sourceParentNode)
		{
			foreach (TreeNode<DynamicFieldDTO> treeNode in sourceNodes)
			{
				TreeNode<DynamicField> treeNode2 = destForest.AppendNode(sourceParentNode, treeNode.Value.ToDomainObject());
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					TreeNodeCollection<DynamicField> nodes = treeNode2.Nodes;
					DynamicFieldForestMapper.CopyNodes(treeNode.Nodes, ref destForest, ref nodes, treeNode2);
				}
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001757C File Offset: 0x0001577C
		private static void CopyNodes(TreeNodeCollection<DynamicField> sourceNodes, ref Forest<DynamicFieldDTO> destForest, ref TreeNodeCollection<DynamicFieldDTO> destNodes, TreeNode<DynamicFieldDTO> sourceParentNode)
		{
			foreach (TreeNode<DynamicField> treeNode in sourceNodes)
			{
				TreeNode<DynamicFieldDTO> treeNode2 = destForest.AppendNode(sourceParentNode, treeNode.Value.ToDTO());
				bool flag = treeNode.Nodes.Count > 0;
				if (flag)
				{
					TreeNodeCollection<DynamicFieldDTO> nodes = treeNode2.Nodes;
					DynamicFieldForestMapper.CopyNodes(treeNode.Nodes, ref destForest, ref nodes, treeNode2);
				}
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00017604 File Offset: 0x00015804
		public static Forest<DynamicFieldDTO> ToDTO(this Forest<DynamicField> item)
		{
			Forest<DynamicFieldDTO> forest = new Forest<DynamicFieldDTO>();
			TreeNodeCollection<DynamicFieldDTO> nodes = forest.Nodes;
			DynamicFieldForestMapper.CopyNodes(item.Nodes, ref forest, ref nodes, null);
			return forest;
		}
	}
}
