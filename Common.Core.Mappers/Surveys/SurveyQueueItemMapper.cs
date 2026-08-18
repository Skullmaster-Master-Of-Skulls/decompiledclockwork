using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.Core.Mappers.Surveys
{
	// Token: 0x0200004F RID: 79
	public static class SurveyQueueItemMapper
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000094E4 File Offset: 0x000076E4
		static SurveyQueueItemMapper()
		{
			BasicPersonMapper.CreateMap();
			SurveyForDisplayMapper.CreateMap();
			SurveyStatusMapper.CreateMap();
			Mapper.CreateMap<SurveyQueueItem, SurveyQueueItemDTO>().ForMember((SurveyQueueItemDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<SurveyQueueItem> m)
			{
				m.MapFrom<BasicPersonDTO>((SurveyQueueItem pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((SurveyQueueItemDTO pb) => pb.AssignedCounsellor, delegate(IMemberConfigurationExpression<SurveyQueueItem> m)
			{
				m.MapFrom<BasicPersonDTO>((SurveyQueueItem pbdto) => (pbdto.AssignedCounsellor == null) ? null : pbdto.AssignedCounsellor.ToDTO());
			}).ForMember((SurveyQueueItemDTO pb) => pb.Survey, delegate(IMemberConfigurationExpression<SurveyQueueItem> m)
			{
				m.MapFrom<SurveyForDisplayDTO>((SurveyQueueItem pbdto) => (pbdto.Survey == null) ? null : pbdto.Survey.ToDTO());
			}).ForMember((SurveyQueueItemDTO pb) => pb.Status, delegate(IMemberConfigurationExpression<SurveyQueueItem> m)
			{
				m.MapFrom<SurveyStatusDTO>((SurveyQueueItem pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDTO());
			});
			Mapper.CreateMap<SurveyQueueItemDTO, SurveyQueueItem>().ForMember((SurveyQueueItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SurveyQueueItemDTO> m)
			{
				m.Ignore();
			}).ForMember((SurveyQueueItem pb) => pb.Student, delegate(IMemberConfigurationExpression<SurveyQueueItemDTO> m)
			{
				m.MapFrom<BasicPerson>((SurveyQueueItemDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((SurveyQueueItem pb) => pb.AssignedCounsellor, delegate(IMemberConfigurationExpression<SurveyQueueItemDTO> m)
			{
				m.MapFrom<BasicPerson>((SurveyQueueItemDTO pbdto) => (pbdto.AssignedCounsellor == null) ? null : pbdto.AssignedCounsellor.ToDomainObject());
			}).ForMember((SurveyQueueItem pb) => pb.Survey, delegate(IMemberConfigurationExpression<SurveyQueueItemDTO> m)
			{
				m.MapFrom<SurveyForDisplay>((SurveyQueueItemDTO pbdto) => (pbdto.Survey == null) ? null : pbdto.Survey.ToDomainObject());
			}).ForMember((SurveyQueueItem pb) => pb.Status, delegate(IMemberConfigurationExpression<SurveyQueueItemDTO> m)
			{
				m.MapFrom<BasicPerson>((SurveyQueueItemDTO pbdto) => (pbdto.Status == null) ? null : pbdto.AssignedCounsellor.ToDomainObject());
			});
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000097E4 File Offset: 0x000079E4
		public static SurveyQueueItem ToDomainObject(this SurveyQueueItemDTO surveyDTO)
		{
			return Mapper.Map<SurveyQueueItemDTO, SurveyQueueItem>(surveyDTO);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000097FC File Offset: 0x000079FC
		public static SurveyQueueItemDTO ToDTO(this SurveyQueueItem survey)
		{
			return Mapper.Map<SurveyQueueItem, SurveyQueueItemDTO>(survey);
		}
	}
}
