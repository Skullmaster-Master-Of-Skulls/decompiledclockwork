using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001ED RID: 493
	public static class FindPotentialBookingsReqMapper
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x00023854 File Offset: 0x00021A54
		static FindPotentialBookingsReqMapper()
		{
			TestMapper.CreateMap();
			AccommodationMapper.CreateMap();
			AssetMapper.CreateMap();
			RoomMapper.CreateMap();
			SpecialAccommodationMapper.CreateMap();
			TestRuleMapper.CreateMap();
			BookingMapper.CreateMap();
			CustomTestBookingRulesClassMapper.CreateMap();
			Mapper.CreateMap<FindPotentialBookingsReqDTO, FindPotentialBookingsReq>().ForMember((FindPotentialBookingsReq pb) => pb.Accommodations, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<Accommodation>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.Accommodations == null) ? null : pbdto.Accommodations.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsReq pb) => pb.AvailableAssets, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<Asset>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.AvailableAssets == null) ? null : pbdto.AvailableAssets.ToList<AssetDTO>().ConvertAll<Asset>((AssetDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsReq pb) => pb.AvailableRooms0, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<Room>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.AvailableRooms0 == null) ? null : pbdto.AvailableRooms0.ToList<RoomDTO>().ConvertAll<Room>((RoomDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsReq pb) => pb.ClassTest, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<Test>((FindPotentialBookingsReqDTO pbdto) => (pbdto.ClassTest == null) ? null : pbdto.ClassTest.ToDomainObject());
			}).ForMember((FindPotentialBookingsReq pb) => pb.CustomTestBookingRules, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<CustomTestBookingRulesClass>((FindPotentialBookingsReqDTO pbdto) => (pbdto.CustomTestBookingRules == null) ? null : pbdto.CustomTestBookingRules.ToDomainObject());
			}).ForMember((FindPotentialBookingsReq pb) => pb.IgnoreStudentAppointmentIds, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<int>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.IgnoreStudentAppointmentIds == null) ? null : pbdto.IgnoreStudentAppointmentIds.ToList<int>());
			}).ForMember((FindPotentialBookingsReq pb) => pb.Rules, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<TestRule>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.Rules == null) ? null : pbdto.Rules.ToList<TestRuleDTO>().ConvertAll<TestRule>((TestRuleDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsReq pb) => pb.SpecialAccommodations, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<SpecialAccommodation>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.SpecialAccommodations == null) ? null : pbdto.SpecialAccommodations.ToList<SpecialAccommodationDTO>().ConvertAll<SpecialAccommodation>((SpecialAccommodationDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsReq pb) => pb.UnavailableRoomBookings, delegate(IMemberConfigurationExpression<FindPotentialBookingsReqDTO> m)
			{
				m.MapFrom<List<Booking>>((FindPotentialBookingsReqDTO pbdto) => (pbdto.UnavailableRoomBookings == null) ? null : pbdto.UnavailableRoomBookings.ToList<BookingDTO>().ConvertAll<Booking>((BookingDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<FindPotentialBookingsReq, FindPotentialBookingsReqDTO>().ForMember((FindPotentialBookingsReqDTO pb) => pb.Accommodations, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<AccommodationDTO>>((FindPotentialBookingsReq pbdto) => (pbdto.Accommodations == null) ? null : pbdto.Accommodations.ToList<Accommodation>().ConvertAll<AccommodationDTO>((Accommodation g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.AvailableAssets, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<AssetDTO>>((FindPotentialBookingsReq pbdto) => (pbdto.AvailableAssets == null) ? null : pbdto.AvailableAssets.ToList<Asset>().ConvertAll<AssetDTO>((Asset g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.AvailableRooms0, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<RoomDTO>>((FindPotentialBookingsReq pbdto) => (pbdto.AvailableRooms0 == null) ? null : pbdto.AvailableRooms0.ToList<Room>().ConvertAll<RoomDTO>((Room g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.ClassTest, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<TestDTO>((FindPotentialBookingsReq pbdto) => (pbdto.ClassTest == null) ? null : pbdto.ClassTest.ToDTO());
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.CustomTestBookingRules, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<CustomTestBookingRulesClassDTO>((FindPotentialBookingsReq pbdto) => (pbdto.CustomTestBookingRules == null) ? null : pbdto.CustomTestBookingRules.ToDTO());
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.IgnoreStudentAppointmentIds, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<int>>((FindPotentialBookingsReq pbdto) => (pbdto.IgnoreStudentAppointmentIds == null) ? null : pbdto.IgnoreStudentAppointmentIds.ToList<int>());
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.Rules, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<TestRuleDTO>>((FindPotentialBookingsReq pbdto) => (pbdto.Rules == null) ? null : pbdto.Rules.ToList<TestRule>().ConvertAll<TestRuleDTO>((TestRule g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.SpecialAccommodations, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<SpecialAccommodationDTO>>((FindPotentialBookingsReq pbdto) => (pbdto.SpecialAccommodations == null) ? null : pbdto.SpecialAccommodations.ToList<SpecialAccommodation>().ConvertAll<SpecialAccommodationDTO>((SpecialAccommodation g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsReqDTO pb) => pb.UnavailableRoomBookings, delegate(IMemberConfigurationExpression<FindPotentialBookingsReq> m)
			{
				m.MapFrom<List<BookingDTO>>((FindPotentialBookingsReq pbdto) => (pbdto.UnavailableRoomBookings == null) ? null : pbdto.UnavailableRoomBookings.ToList<Booking>().ConvertAll<BookingDTO>((Booking g) => g.ToDTO()));
			});
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00023E1C File Offset: 0x0002201C
		public static FindPotentialBookingsReq ToDomainObject(this FindPotentialBookingsReqDTO accommodationForTestDTO)
		{
			return Mapper.Map<FindPotentialBookingsReqDTO, FindPotentialBookingsReq>(accommodationForTestDTO);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00023E34 File Offset: 0x00022034
		public static FindPotentialBookingsReqDTO ToDTO(this FindPotentialBookingsReq accommodationForTest)
		{
			return Mapper.Map<FindPotentialBookingsReq, FindPotentialBookingsReqDTO>(accommodationForTest);
		}
	}
}
