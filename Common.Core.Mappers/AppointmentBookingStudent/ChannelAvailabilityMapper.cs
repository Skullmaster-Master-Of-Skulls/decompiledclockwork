using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent
{
	// Token: 0x02000207 RID: 519
	public static class ChannelAvailabilityMapper
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00025D78 File Offset: 0x00023F78
		static ChannelAvailabilityMapper()
		{
			ChannelUnderlyingPersonMapper.CreateMap();
			Mapper.CreateMap<ChannelAvailabilityDTO, ChannelAvailability>().ForMember((ChannelAvailability pb) => pb.PersonCollection, delegate(IMemberConfigurationExpression<ChannelAvailabilityDTO> m)
			{
				m.MapFrom<IEnumerable<ChannelPersonCollection>>((ChannelAvailabilityDTO pbdto) => (pbdto.PersonCollection == null) ? null : (from g in pbdto.PersonCollection
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<ChannelAvailability, ChannelAvailabilityDTO>().ForMember((ChannelAvailabilityDTO pb) => pb.PersonCollection, delegate(IMemberConfigurationExpression<ChannelAvailability> m)
			{
				m.MapFrom<IEnumerable<ChannelPersonCollectionDTO>>((ChannelAvailability pbdto) => (pbdto.PersonCollection == null) ? null : (from g in pbdto.PersonCollection
				select g.ToDTO()));
			});
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00025E34 File Offset: 0x00024034
		public static ChannelAvailability ToDomainObject(this ChannelAvailabilityDTO dto)
		{
			return Mapper.Map<ChannelAvailabilityDTO, ChannelAvailability>(dto);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00025E4C File Offset: 0x0002404C
		public static ChannelAvailabilityDTO ToDTO(this ChannelAvailability item)
		{
			return Mapper.Map<ChannelAvailability, ChannelAvailabilityDTO>(item);
		}
	}
}
