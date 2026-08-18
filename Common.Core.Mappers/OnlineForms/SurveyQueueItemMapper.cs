using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.OnlineForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.OnlineForms
{
	// Token: 0x020000B3 RID: 179
	public static class SurveyQueueItemMapper
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000F93C File Offset: 0x0000DB3C
		static SurveyQueueItemMapper()
		{
			BasicPersonMapper.CreateMap();
			SurveyForDisplayMapper.CreateMap();
			OnlineFormStatusMapper.CreateMap();
			Mapper.CreateMap<OnlineFormQueueItem, OnlineFormQueueItemDTO>().ForMember((OnlineFormQueueItemDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<OnlineFormQueueItem> m)
			{
				m.MapFrom<BasicPersonDTO>((OnlineFormQueueItem pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((OnlineFormQueueItemDTO pb) => pb.AssignedCounsellor, delegate(IMemberConfigurationExpression<OnlineFormQueueItem> m)
			{
				m.MapFrom<BasicPersonDTO>((OnlineFormQueueItem pbdto) => (pbdto.AssignedCounsellor == null) ? null : pbdto.AssignedCounsellor.ToDTO());
			}).ForMember((OnlineFormQueueItemDTO pb) => pb.OnlineForm, delegate(IMemberConfigurationExpression<OnlineFormQueueItem> m)
			{
				m.MapFrom<OnlineFormForDisplayDTO>((OnlineFormQueueItem pbdto) => (pbdto.OnlineForm == null) ? null : pbdto.OnlineForm.ToDTO());
			}).ForMember((OnlineFormQueueItemDTO pb) => pb.Status, delegate(IMemberConfigurationExpression<OnlineFormQueueItem> m)
			{
				m.MapFrom<OnlineFormStatusDTO>((OnlineFormQueueItem pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDTO());
			});
			Mapper.CreateMap<OnlineFormQueueItemDTO, OnlineFormQueueItem>().ForMember((OnlineFormQueueItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<OnlineFormQueueItemDTO> m)
			{
				m.Ignore();
			}).ForMember((OnlineFormQueueItem pb) => pb.Student, delegate(IMemberConfigurationExpression<OnlineFormQueueItemDTO> m)
			{
				m.MapFrom<BasicPerson>((OnlineFormQueueItemDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((OnlineFormQueueItem pb) => pb.AssignedCounsellor, delegate(IMemberConfigurationExpression<OnlineFormQueueItemDTO> m)
			{
				m.MapFrom<BasicPerson>((OnlineFormQueueItemDTO pbdto) => (pbdto.AssignedCounsellor == null) ? null : pbdto.AssignedCounsellor.ToDomainObject());
			}).ForMember((OnlineFormQueueItem pb) => pb.OnlineForm, delegate(IMemberConfigurationExpression<OnlineFormQueueItemDTO> m)
			{
				m.MapFrom<OnlineFormForDisplay>((OnlineFormQueueItemDTO pbdto) => (pbdto.OnlineForm == null) ? null : pbdto.OnlineForm.ToDomainObject());
			}).ForMember((OnlineFormQueueItem pb) => pb.Status, delegate(IMemberConfigurationExpression<OnlineFormQueueItemDTO> m)
			{
				m.MapFrom<BasicPerson>((OnlineFormQueueItemDTO pbdto) => (pbdto.Status == null) ? null : pbdto.AssignedCounsellor.ToDomainObject());
			});
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		public static OnlineFormQueueItem ToDomainObject(this OnlineFormQueueItemDTO onlineFormQueueItemDTO)
		{
			return Mapper.Map<OnlineFormQueueItemDTO, OnlineFormQueueItem>(onlineFormQueueItemDTO);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000FC54 File Offset: 0x0000DE54
		public static OnlineFormQueueItemDTO ToDTO(this OnlineFormQueueItem onlineFormQueueItem)
		{
			return Mapper.Map<OnlineFormQueueItem, OnlineFormQueueItemDTO>(onlineFormQueueItem);
		}
	}
}
