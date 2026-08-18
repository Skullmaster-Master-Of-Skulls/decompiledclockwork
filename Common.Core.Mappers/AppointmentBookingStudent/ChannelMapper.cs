using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Core.Mappers.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent
{
	// Token: 0x02000208 RID: 520
	public static class ChannelMapper
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00025E64 File Offset: 0x00024064
		static ChannelMapper()
		{
			ChannelAvailabilityMapper.CreateMap();
			BookingFilterParametersMapper.CreateMap();
			Mapper.CreateMap<ChannelDTO, Channel>().ForMember((Channel pb) => pb.Availabilities, delegate(IMemberConfigurationExpression<ChannelDTO> m)
			{
				m.MapFrom<IEnumerable<ChannelAvailability>>((ChannelDTO pbdto) => (pbdto.Availabilities == null) ? null : (from g in pbdto.Availabilities
				select g.ToDomainObject()));
			}).ForMember((Channel pb) => pb.OverrideBookingFilterParameters, delegate(IMemberConfigurationExpression<ChannelDTO> m)
			{
				m.MapFrom<AppointmentBookingFilterParameters>((ChannelDTO pbdto) => (pbdto.OverrideBookingFilterParameters == null) ? null : pbdto.OverrideBookingFilterParameters.ToDomainObject());
			});
			Mapper.CreateMap<Channel, ChannelDTO>().ForMember((ChannelDTO pb) => pb.Availabilities, delegate(IMemberConfigurationExpression<Channel> m)
			{
				m.MapFrom<IEnumerable<ChannelAvailabilityDTO>>((Channel pbdto) => (pbdto.Availabilities == null) ? null : (from g in pbdto.Availabilities
				select g.ToDTO()));
			}).ForMember((ChannelDTO pb) => pb.OverrideBookingFilterParameters, delegate(IMemberConfigurationExpression<Channel> m)
			{
				m.MapFrom<AppointmentBookingFilterParametersDTO>((Channel pbdto) => (pbdto.OverrideBookingFilterParameters == null) ? null : pbdto.OverrideBookingFilterParameters.ToDTO());
			});
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00025FC4 File Offset: 0x000241C4
		public static Channel ToDomainObject(this ChannelDTO dto)
		{
			return Mapper.Map<ChannelDTO, Channel>(dto);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00025FDC File Offset: 0x000241DC
		public static ChannelDTO ToDTO(this Channel item)
		{
			return Mapper.Map<Channel, ChannelDTO>(item);
		}
	}
}
