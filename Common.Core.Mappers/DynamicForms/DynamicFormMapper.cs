using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x0200011E RID: 286
	public static class DynamicFormMapper
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x00017A14 File Offset: 0x00015C14
		static DynamicFormMapper()
		{
			DynamicFormBaseMapper.CreateMap();
			Mapper.CreateMap<DynamicFormDTO, DynamicForm>().ForMember((DynamicForm pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicFormDTO> m)
			{
				m.Ignore();
			}).ForMember((DynamicForm pb) => pb.SubForm, delegate(IMemberConfigurationExpression<DynamicFormDTO> m)
			{
				m.MapFrom<DynamicForm>((DynamicFormDTO p) => (p.SubForm == null) ? null : p.SubForm.ToDomainObject());
			}).ForMember((DynamicForm pb) => (object)pb.FormType, delegate(IMemberConfigurationExpression<DynamicFormDTO> m)
			{
				m.MapFrom<eDynamicFormTypeDTO>((DynamicFormDTO p) => (eDynamicFormTypeDTO)p.FormType);
			});
			Mapper.CreateMap<DynamicForm, DynamicFormDTO>().ForMember((DynamicFormDTO pb) => pb.SubForm, delegate(IMemberConfigurationExpression<DynamicForm> m)
			{
				m.MapFrom<DynamicFormDTO>((DynamicForm p) => (p.SubForm == null) ? null : p.SubForm.ToDTO());
			}).ForMember((DynamicFormDTO pb) => (object)pb.FormType, delegate(IMemberConfigurationExpression<DynamicForm> m)
			{
				m.MapFrom<eDynamicFormType>((DynamicForm p) => (eDynamicFormType)p.FormType);
			});
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00017BEC File Offset: 0x00015DEC
		public static DynamicForm ToDomainObject(this DynamicFormDTO dynamicFormDTO)
		{
			return Mapper.Map<DynamicFormDTO, DynamicForm>(dynamicFormDTO);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00017C04 File Offset: 0x00015E04
		public static DynamicFormDTO ToDTO(this DynamicForm dynamicForm)
		{
			return Mapper.Map<DynamicForm, DynamicFormDTO>(dynamicForm);
		}
	}
}
