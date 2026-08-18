using System;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2
{
	// Token: 0x020001DD RID: 477
	public static class TryToBookPotentialBookingMapper
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x000229E4 File Offset: 0x00020BE4
		static TryToBookPotentialBookingMapper()
		{
			Mapper.CreateMap<TryToBookPotentialBookingDTO, TryToBookPotentialBooking>().ForMember((TryToBookPotentialBooking pb) => pb.Room, delegate(IMemberConfigurationExpression<TryToBookPotentialBookingDTO> m)
			{
				m.MapFrom<TryToBookRoom>((TryToBookPotentialBookingDTO pbdto) => (pbdto.Room == null) ? null : pbdto.Room.ToDomainObject());
			});
			Mapper.CreateMap<TryToBookPotentialBooking, TryToBookPotentialBookingDTO>().ForMember((TryToBookPotentialBookingDTO pb) => pb.Room, delegate(IMemberConfigurationExpression<TryToBookPotentialBooking> m)
			{
				m.MapFrom<TryToBookRoomDTO>((TryToBookPotentialBooking pbdto) => (pbdto.Room == null) ? null : pbdto.Room.ToDTO());
			});
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00022A9C File Offset: 0x00020C9C
		public static TryToBookPotentialBooking ToDomainObject(this TryToBookPotentialBookingDTO accommodationForTestDTO)
		{
			return Mapper.Map<TryToBookPotentialBookingDTO, TryToBookPotentialBooking>(accommodationForTestDTO);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00022AB4 File Offset: 0x00020CB4
		public static TryToBookPotentialBookingDTO ToDTO(this TryToBookPotentialBooking accommodationForTest)
		{
			return Mapper.Map<TryToBookPotentialBooking, TryToBookPotentialBookingDTO>(accommodationForTest);
		}
	}
}
