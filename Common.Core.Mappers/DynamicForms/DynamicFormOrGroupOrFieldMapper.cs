using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x0200011F RID: 287
	public static class DynamicFormOrGroupOrFieldMapper
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x00017C1C File Offset: 0x00015E1C
		static DynamicFormOrGroupOrFieldMapper()
		{
			DynamicFormMapper.CreateMap();
			DynamicFieldMapper.CreateMap();
			Mapper.CreateMap<DynamicFormOrGroupOrFieldDTO, DynamicFormOrGroupOrField>();
			Mapper.CreateMap<DynamicFormOrGroupOrField, DynamicFormOrGroupOrFieldDTO>();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00017C38 File Offset: 0x00015E38
		public static DynamicFormOrGroupOrField ToDomainObject(this DynamicFormOrGroupOrFieldDTO dto)
		{
			return Mapper.Map<DynamicFormOrGroupOrFieldDTO, DynamicFormOrGroupOrField>(dto);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00017C50 File Offset: 0x00015E50
		public static DynamicFormOrGroupOrFieldDTO ToDTO(this DynamicFormOrGroupOrField item)
		{
			return Mapper.Map<DynamicFormOrGroupOrField, DynamicFormOrGroupOrFieldDTO>(item);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00017C68 File Offset: 0x00015E68
		private static void CopyNodes(ref Forest<DynamicFormOrGroupOrField> destForest, Forest<DynamicFormOrGroupOrFieldDTO> sourceForest, TreeNode<DynamicFormOrGroupOrFieldDTO> sourceParent, TreeNode<DynamicFormOrGroupOrField> destParent)
		{
			TreeNodeCollection<DynamicFormOrGroupOrFieldDTO> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<DynamicFormOrGroupOrFieldDTO> treeNode in treeNodeCollection)
			{
				TreeNode<DynamicFormOrGroupOrField> destParent2 = destForest.AppendNode(destParent, new DynamicFormOrGroupOrField
				{
					DynamicForm = ((treeNode.Value.DynamicForm == null) ? null : treeNode.Value.DynamicForm.ToDomainObject()),
					Field = ((treeNode.Value.Field == null) ? null : treeNode.Value.Field.ToDomainObject()),
					GroupName = treeNode.Value.GroupName
				});
				DynamicFormOrGroupOrFieldMapper.CopyNodes(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00017D40 File Offset: 0x00015F40
		private static void CopyNodesDTO(ref Forest<DynamicFormOrGroupOrFieldDTO> destForest, Forest<DynamicFormOrGroupOrField> sourceForest, TreeNode<DynamicFormOrGroupOrField> sourceParent, TreeNode<DynamicFormOrGroupOrFieldDTO> destParent)
		{
			TreeNodeCollection<DynamicFormOrGroupOrField> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<DynamicFormOrGroupOrField> treeNode in treeNodeCollection)
			{
				TreeNode<DynamicFormOrGroupOrFieldDTO> destParent2 = destForest.AppendNode(destParent, new DynamicFormOrGroupOrFieldDTO
				{
					DynamicForm = ((treeNode.Value.DynamicForm == null) ? null : treeNode.Value.DynamicForm.ToDTO()),
					Field = ((treeNode.Value.Field == null) ? null : treeNode.Value.Field.ToDTO()),
					GroupName = treeNode.Value.GroupName
				});
				DynamicFormOrGroupOrFieldMapper.CopyNodesDTO(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017E18 File Offset: 0x00016018
		public static Forest<DynamicFormOrGroupOrField> ToDomainObject(this Forest<DynamicFormOrGroupOrFieldDTO> dto)
		{
			Forest<DynamicFormOrGroupOrField> result = new Forest<DynamicFormOrGroupOrField>();
			DynamicFormOrGroupOrFieldMapper.CopyNodes(ref result, dto, null, null);
			return result;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00017E3C File Offset: 0x0001603C
		public static Forest<DynamicFormOrGroupOrFieldDTO> ToDTO(this Forest<DynamicFormOrGroupOrField> item)
		{
			Forest<DynamicFormOrGroupOrFieldDTO> result = new Forest<DynamicFormOrGroupOrFieldDTO>();
			DynamicFormOrGroupOrFieldMapper.CopyNodesDTO(ref result, item, null, null);
			return result;
		}
	}
}
