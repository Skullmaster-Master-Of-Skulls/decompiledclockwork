using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.Core.Mappers.Surveys
{
	// Token: 0x0200004E RID: 78
	public static class SurveyMapper
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000091B4 File Offset: 0x000073B4
		static SurveyMapper()
		{
			DynamicFormMapper.CreateMap();
			GroupMapper.CreateMap();
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<Survey, SurveyDTO>().ForMember((SurveyDTO pb) => pb.Form, delegate(IMemberConfigurationExpression<Survey> m)
			{
				m.MapFrom<DynamicFormDTO>((Survey pbdto) => (pbdto.Form == null) ? null : pbdto.Form.ToDTO());
			}).ForMember((SurveyDTO pb) => pb.RestrictedToGroup, delegate(IMemberConfigurationExpression<Survey> m)
			{
				m.MapFrom<GroupDTO>((Survey pbdto) => (pbdto.RestrictedToGroup == null) ? null : pbdto.RestrictedToGroup.ToDTO());
			}).ForMember((SurveyDTO pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<Survey> m)
			{
				m.MapFrom<BasicPersonDTO>((Survey pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDTO());
			}).ForMember((SurveyDTO pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<Survey> m)
			{
				m.MapFrom<BasicPersonDTO>((Survey pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDTO());
			});
			Mapper.CreateMap<SurveyDTO, Survey>().ForMember((Survey pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SurveyDTO> m)
			{
				m.Ignore();
			}).ForMember((Survey pb) => pb.Form, delegate(IMemberConfigurationExpression<SurveyDTO> m)
			{
				m.MapFrom<DynamicForm>((SurveyDTO pbdto) => (pbdto.Form == null) ? null : pbdto.Form.ToDomainObject());
			}).ForMember((Survey pb) => pb.RestrictedToGroup, delegate(IMemberConfigurationExpression<SurveyDTO> m)
			{
				m.MapFrom<Group>((SurveyDTO pbdto) => (pbdto.RestrictedToGroup == null) ? null : pbdto.RestrictedToGroup.ToDomainObject());
			}).ForMember((Survey pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<SurveyDTO> m)
			{
				m.MapFrom<BasicPerson>((SurveyDTO pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDomainObject());
			}).ForMember((Survey pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<SurveyDTO> m)
			{
				m.MapFrom<BasicPerson>((SurveyDTO pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDomainObject());
			});
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000094B4 File Offset: 0x000076B4
		public static Survey ToDomainObject(this SurveyDTO surveyDTO)
		{
			return Mapper.Map<SurveyDTO, Survey>(surveyDTO);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000094CC File Offset: 0x000076CC
		public static SurveyDTO ToDTO(this Survey survey)
		{
			return Mapper.Map<Survey, SurveyDTO>(survey);
		}
	}
}
