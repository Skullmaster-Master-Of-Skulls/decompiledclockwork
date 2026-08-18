using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000184 RID: 388
	public static class AvailabilityScheduleItemsForContextMapper
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		static AvailabilityScheduleItemsForContextMapper()
		{
			AvailabilityScheduleContextMapper.CreateMap();
			AvailabilityScheduleItemInfoMapper.CreateMap();
			Mapper.CreateMap<AvailabilityScheduleItemsForContextDTO, AvailabilityScheduleItemsForContext>().ForMember((AvailabilityScheduleItemsForContext pb) => pb.AvailabilityScheduleItems, delegate(IMemberConfigurationExpression<AvailabilityScheduleItemsForContextDTO> m)
			{
				m.MapFrom<List<AvailabilityScheduleItemInfo>>((AvailabilityScheduleItemsForContextDTO pbdto) => (pbdto.AvailabilityScheduleItems == null) ? null : (from g in pbdto.AvailabilityScheduleItems
				select g.ToDomainObject()).ToList<AvailabilityScheduleItemInfo>());
			}).ForMember((AvailabilityScheduleItemsForContext pb) => pb.Context, delegate(IMemberConfigurationExpression<AvailabilityScheduleItemsForContextDTO> m)
			{
				m.MapFrom<AvailabilityScheduleContext>((AvailabilityScheduleItemsForContextDTO pbdto) => (pbdto.Context == null) ? null : pbdto.Context.ToDomainObject());
			});
			Mapper.CreateMap<AvailabilityScheduleItemsForContext, AvailabilityScheduleItemsForContextDTO>().ForMember((AvailabilityScheduleItemsForContextDTO pb) => pb.AvailabilityScheduleItems, delegate(IMemberConfigurationExpression<AvailabilityScheduleItemsForContext> m)
			{
				m.MapFrom<List<AvailabilityScheduleItemInfoDTO>>((AvailabilityScheduleItemsForContext pbdto) => (pbdto.AvailabilityScheduleItems == null) ? null : (from g in pbdto.AvailabilityScheduleItems
				select g.ToDTO()).ToList<AvailabilityScheduleItemInfoDTO>());
			}).ForMember((AvailabilityScheduleItemsForContextDTO pb) => pb.Context, delegate(IMemberConfigurationExpression<AvailabilityScheduleItemsForContext> m)
			{
				m.MapFrom<AvailabilityScheduleContextDTO>((AvailabilityScheduleItemsForContext pbdto) => (pbdto.Context == null) ? null : pbdto.Context.ToDTO());
			});
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001E404 File Offset: 0x0001C604
		public static AvailabilityScheduleItemsForContext ToDomainObject(this AvailabilityScheduleItemsForContextDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityScheduleItemsForContextDTO, AvailabilityScheduleItemsForContext>(appTypeGroupDTO);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001E41C File Offset: 0x0001C61C
		public static AvailabilityScheduleItemsForContextDTO ToDTO(this AvailabilityScheduleItemsForContext appTypeGroup)
		{
			return Mapper.Map<AvailabilityScheduleItemsForContext, AvailabilityScheduleItemsForContextDTO>(appTypeGroup);
		}
	}
}
