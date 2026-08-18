using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x0200011D RID: 285
	public static class DynamicFormBaseMapper
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0001783C File Offset: 0x00015A3C
		static DynamicFormBaseMapper()
		{
			DynamicFormMapper.CreateMap();
			Mapper.CreateMap<DynamicFormBase, DynamicFormBaseDTO>().ForMember((DynamicFormBaseDTO pb) => pb.SubForm, delegate(IMemberConfigurationExpression<DynamicFormBase> m)
			{
				m.MapFrom<DynamicFormDTO>((DynamicFormBase p) => (p.SubForm == null) ? null : p.SubForm.ToDTO());
			}).ForMember((DynamicFormBaseDTO pb) => (object)pb.FormType, delegate(IMemberConfigurationExpression<DynamicFormBase> m)
			{
				m.MapFrom<eDynamicFormTypeDTO>((DynamicFormBase p) => (eDynamicFormTypeDTO)p.FormType);
			});
			Mapper.CreateMap<DynamicFormBaseDTO, DynamicFormBase>().ForMember((DynamicFormBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicFormBaseDTO> m)
			{
				m.Ignore();
			}).ForMember((DynamicFormBase pb) => pb.SubForm, delegate(IMemberConfigurationExpression<DynamicFormBaseDTO> m)
			{
				m.MapFrom<DynamicForm>((DynamicFormBaseDTO p) => (p.SubForm == null) ? null : p.SubForm.ToDomainObject());
			}).ForMember((DynamicFormBase pb) => (object)pb.FormType, delegate(IMemberConfigurationExpression<DynamicFormBaseDTO> m)
			{
				m.MapFrom<eDynamicFormType>((DynamicFormBaseDTO p) => (eDynamicFormType)p.FormType);
			});
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}
	}
}
