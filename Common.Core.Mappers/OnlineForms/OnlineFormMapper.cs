using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.OnlineForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.OnlineForms
{
	// Token: 0x020000B2 RID: 178
	public static class OnlineFormMapper
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000F60C File Offset: 0x0000D80C
		static OnlineFormMapper()
		{
			DynamicFormMapper.CreateMap();
			GroupMapper.CreateMap();
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<OnlineForm, OnlineFormDTO>().ForMember((OnlineFormDTO pb) => pb.Form, delegate(IMemberConfigurationExpression<OnlineForm> m)
			{
				m.MapFrom<DynamicFormDTO>((OnlineForm pbdto) => (pbdto.Form == null) ? null : pbdto.Form.ToDTO());
			}).ForMember((OnlineFormDTO pb) => pb.RestrictedToGroup, delegate(IMemberConfigurationExpression<OnlineForm> m)
			{
				m.MapFrom<GroupDTO>((OnlineForm pbdto) => (pbdto.RestrictedToGroup == null) ? null : pbdto.RestrictedToGroup.ToDTO());
			}).ForMember((OnlineFormDTO pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<OnlineForm> m)
			{
				m.MapFrom<BasicPersonDTO>((OnlineForm pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDTO());
			}).ForMember((OnlineFormDTO pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<OnlineForm> m)
			{
				m.MapFrom<BasicPersonDTO>((OnlineForm pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDTO());
			});
			Mapper.CreateMap<OnlineFormDTO, OnlineForm>().ForMember((OnlineForm pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<OnlineFormDTO> m)
			{
				m.Ignore();
			}).ForMember((OnlineForm pb) => pb.Form, delegate(IMemberConfigurationExpression<OnlineFormDTO> m)
			{
				m.MapFrom<DynamicForm>((OnlineFormDTO pbdto) => (pbdto.Form == null) ? null : pbdto.Form.ToDomainObject());
			}).ForMember((OnlineForm pb) => pb.RestrictedToGroup, delegate(IMemberConfigurationExpression<OnlineFormDTO> m)
			{
				m.MapFrom<Group>((OnlineFormDTO pbdto) => (pbdto.RestrictedToGroup == null) ? null : pbdto.RestrictedToGroup.ToDomainObject());
			}).ForMember((OnlineForm pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<OnlineFormDTO> m)
			{
				m.MapFrom<BasicPerson>((OnlineFormDTO pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDomainObject());
			}).ForMember((OnlineForm pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<OnlineFormDTO> m)
			{
				m.MapFrom<BasicPerson>((OnlineFormDTO pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDomainObject());
			});
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public static OnlineForm ToDomainObject(this OnlineFormDTO onlineFormDTO)
		{
			return Mapper.Map<OnlineFormDTO, OnlineForm>(onlineFormDTO);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000F924 File Offset: 0x0000DB24
		public static OnlineFormDTO ToDTO(this OnlineForm onlineForm)
		{
			return Mapper.Map<OnlineForm, OnlineFormDTO>(onlineForm);
		}
	}
}
