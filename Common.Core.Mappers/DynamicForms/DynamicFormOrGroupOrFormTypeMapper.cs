using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000120 RID: 288
	public static class DynamicFormOrGroupOrFormTypeMapper
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00017E60 File Offset: 0x00016060
		static DynamicFormOrGroupOrFormTypeMapper()
		{
			DynamicFormMapper.CreateMap();
			Mapper.CreateMap<DynamicFormOrGroupOrFormTypeDTO, DynamicFormOrGroupOrFormType>().ForMember((DynamicFormOrGroupOrFormType pb) => (object)pb.DynamicFormType, delegate(IMemberConfigurationExpression<DynamicFormOrGroupOrFormTypeDTO> m)
			{
				m.MapFrom<eDynamicFormTypeDTO?>((DynamicFormOrGroupOrFormTypeDTO pbdto) => pbdto.DynamicFormType.HasValue ? ((eDynamicFormTypeDTO?)((eDynamicFormTypeDTO)pbdto.DynamicFormType)) : ((eDynamicFormTypeDTO?)null));
			});
			Mapper.CreateMap<DynamicFormOrGroupOrFormType, DynamicFormOrGroupOrFormTypeDTO>().ForMember((DynamicFormOrGroupOrFormTypeDTO pb) => (object)pb.DynamicFormType, delegate(IMemberConfigurationExpression<DynamicFormOrGroupOrFormType> m)
			{
				m.MapFrom<eDynamicFormType?>((DynamicFormOrGroupOrFormType pbdto) => pbdto.DynamicFormType.HasValue ? ((eDynamicFormType?)((eDynamicFormType)pbdto.DynamicFormType)) : ((eDynamicFormType?)null));
			});
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00017F3C File Offset: 0x0001613C
		public static DynamicFormOrGroupOrFormType ToDomainObject(this DynamicFormOrGroupOrFormTypeDTO dto)
		{
			return Mapper.Map<DynamicFormOrGroupOrFormTypeDTO, DynamicFormOrGroupOrFormType>(dto);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017F54 File Offset: 0x00016154
		public static DynamicFormOrGroupOrFormTypeDTO ToDTO(this DynamicFormOrGroupOrFormType item)
		{
			return Mapper.Map<DynamicFormOrGroupOrFormType, DynamicFormOrGroupOrFormTypeDTO>(item);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00017F6C File Offset: 0x0001616C
		private static void CopyNodes(ref Forest<DynamicFormOrGroupOrFormType> destForest, Forest<DynamicFormOrGroupOrFormTypeDTO> sourceForest, TreeNode<DynamicFormOrGroupOrFormTypeDTO> sourceParent, TreeNode<DynamicFormOrGroupOrFormType> destParent)
		{
			TreeNodeCollection<DynamicFormOrGroupOrFormTypeDTO> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<DynamicFormOrGroupOrFormTypeDTO> treeNode in treeNodeCollection)
			{
				TreeNode<DynamicFormOrGroupOrFormType> destParent2 = destForest.AppendNode(destParent, new DynamicFormOrGroupOrFormType
				{
					DynamicForm = ((treeNode.Value.DynamicForm == null) ? null : treeNode.Value.DynamicForm.ToDomainObject()),
					DynamicFormType = ((treeNode.Value.DynamicFormType != null) ? new eDynamicFormType?((eDynamicFormType)treeNode.Value.DynamicFormType.Value) : null),
					GroupName = treeNode.Value.GroupName
				});
				DynamicFormOrGroupOrFormTypeMapper.CopyNodes(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00018060 File Offset: 0x00016260
		private static void CopyNodesDTO(ref Forest<DynamicFormOrGroupOrFormTypeDTO> destForest, Forest<DynamicFormOrGroupOrFormType> sourceForest, TreeNode<DynamicFormOrGroupOrFormType> sourceParent, TreeNode<DynamicFormOrGroupOrFormTypeDTO> destParent)
		{
			TreeNodeCollection<DynamicFormOrGroupOrFormType> treeNodeCollection = (sourceParent == null) ? sourceForest.Nodes : sourceParent.Nodes;
			foreach (TreeNode<DynamicFormOrGroupOrFormType> treeNode in treeNodeCollection)
			{
				TreeNode<DynamicFormOrGroupOrFormTypeDTO> destParent2 = destForest.AppendNode(destParent, new DynamicFormOrGroupOrFormTypeDTO
				{
					DynamicForm = ((treeNode.Value.DynamicForm == null) ? null : treeNode.Value.DynamicForm.ToDTO()),
					DynamicFormType = ((treeNode.Value.DynamicFormType != null) ? new eDynamicFormTypeDTO?((eDynamicFormTypeDTO)treeNode.Value.DynamicFormType.Value) : null),
					GroupName = treeNode.Value.GroupName
				});
				DynamicFormOrGroupOrFormTypeMapper.CopyNodesDTO(ref destForest, sourceForest, treeNode, destParent2);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00018154 File Offset: 0x00016354
		public static Forest<DynamicFormOrGroupOrFormType> ToDomainObject(this Forest<DynamicFormOrGroupOrFormTypeDTO> dto)
		{
			Forest<DynamicFormOrGroupOrFormType> result = new Forest<DynamicFormOrGroupOrFormType>();
			DynamicFormOrGroupOrFormTypeMapper.CopyNodes(ref result, dto, null, null);
			return result;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00018178 File Offset: 0x00016378
		public static Forest<DynamicFormOrGroupOrFormTypeDTO> ToDTO(this Forest<DynamicFormOrGroupOrFormType> item)
		{
			Forest<DynamicFormOrGroupOrFormTypeDTO> result = new Forest<DynamicFormOrGroupOrFormTypeDTO>();
			DynamicFormOrGroupOrFormTypeMapper.CopyNodesDTO(ref result, item, null, null);
			return result;
		}
	}
}
