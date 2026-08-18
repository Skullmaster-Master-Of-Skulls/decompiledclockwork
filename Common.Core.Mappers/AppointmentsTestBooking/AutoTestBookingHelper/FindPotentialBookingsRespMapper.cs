using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001EE RID: 494
	public static class FindPotentialBookingsRespMapper
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x00023E4C File Offset: 0x0002204C
		static FindPotentialBookingsRespMapper()
		{
			PrivateNoteMapper.CreateMap();
			BookingResultsMapper.CreateMap();
			PotentialTestMapper.CreateMap();
			Mapper.CreateMap<FindPotentialBookingsRespDTO, FindPotentialBookingsResp>().ForMember((FindPotentialBookingsResp pb) => pb.BookingResults, delegate(IMemberConfigurationExpression<FindPotentialBookingsRespDTO> m)
			{
				m.MapFrom<BookingResults>((FindPotentialBookingsRespDTO pbdto) => (pbdto.BookingResults == null) ? null : pbdto.BookingResults.ToDomainObject());
			}).ForMember((FindPotentialBookingsResp pb) => pb.IconIds, delegate(IMemberConfigurationExpression<FindPotentialBookingsRespDTO> m)
			{
				m.MapFrom<List<int>>((FindPotentialBookingsRespDTO pbdto) => (pbdto.IconIds == null) ? null : pbdto.IconIds.ToList<int>());
			}).ForMember((FindPotentialBookingsResp pb) => pb.PotentialTests, delegate(IMemberConfigurationExpression<FindPotentialBookingsRespDTO> m)
			{
				m.MapFrom<List<PotentialTest>>((FindPotentialBookingsRespDTO pbdto) => (pbdto.PotentialTests == null) ? null : pbdto.PotentialTests.ToList<PotentialTestDTO>().ConvertAll<PotentialTest>((PotentialTestDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsResp pb) => pb.PrivateNotes, delegate(IMemberConfigurationExpression<FindPotentialBookingsRespDTO> m)
			{
				m.MapFrom<List<PrivateNote>>((FindPotentialBookingsRespDTO pbdto) => (pbdto.PrivateNotes == null) ? null : pbdto.PrivateNotes.ToList<PrivateNoteDTO>().ConvertAll<PrivateNote>((PrivateNoteDTO g) => g.ToDomainObject()));
			}).ForMember((FindPotentialBookingsResp pb) => pb.DebugNotes, delegate(IMemberConfigurationExpression<FindPotentialBookingsRespDTO> m)
			{
				m.MapFrom<List<string>>((FindPotentialBookingsRespDTO pbdto) => (pbdto.DebugNotes == null) ? null : pbdto.DebugNotes.ToList<string>());
			});
			Mapper.CreateMap<FindPotentialBookingsResp, FindPotentialBookingsRespDTO>().ForMember((FindPotentialBookingsRespDTO pb) => pb.BookingResults, delegate(IMemberConfigurationExpression<FindPotentialBookingsResp> m)
			{
				m.MapFrom<BookingResultsDTO>((FindPotentialBookingsResp pbdto) => (pbdto.BookingResults == null) ? null : pbdto.BookingResults.ToDTO());
			}).ForMember((FindPotentialBookingsRespDTO pb) => pb.IconIds, delegate(IMemberConfigurationExpression<FindPotentialBookingsResp> m)
			{
				m.MapFrom<List<int>>((FindPotentialBookingsResp pbdto) => (pbdto.IconIds == null) ? null : pbdto.IconIds.ToList<int>());
			}).ForMember((FindPotentialBookingsRespDTO pb) => pb.PotentialTests, delegate(IMemberConfigurationExpression<FindPotentialBookingsResp> m)
			{
				m.MapFrom<List<PotentialTestDTO>>((FindPotentialBookingsResp pbdto) => (pbdto.PotentialTests == null) ? null : pbdto.PotentialTests.ToList<PotentialTest>().ConvertAll<PotentialTestDTO>((PotentialTest g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsRespDTO pb) => pb.PrivateNotes, delegate(IMemberConfigurationExpression<FindPotentialBookingsResp> m)
			{
				m.MapFrom<List<PrivateNoteDTO>>((FindPotentialBookingsResp pbdto) => (pbdto.PrivateNotes == null) ? null : pbdto.PrivateNotes.ToList<PrivateNote>().ConvertAll<PrivateNoteDTO>((PrivateNote g) => g.ToDTO()));
			}).ForMember((FindPotentialBookingsRespDTO pb) => pb.DebugNotes, delegate(IMemberConfigurationExpression<FindPotentialBookingsResp> m)
			{
				m.MapFrom<List<string>>((FindPotentialBookingsResp pbdto) => (pbdto.DebugNotes == null) ? null : pbdto.DebugNotes.ToList<string>());
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00024184 File Offset: 0x00022384
		public static FindPotentialBookingsResp ToDomainObject(this FindPotentialBookingsRespDTO accommodationForTestDTO)
		{
			return Mapper.Map<FindPotentialBookingsRespDTO, FindPotentialBookingsResp>(accommodationForTestDTO);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002419C File Offset: 0x0002239C
		public static FindPotentialBookingsRespDTO ToDTO(this FindPotentialBookingsResp accommodationForTest)
		{
			return Mapper.Map<FindPotentialBookingsResp, FindPotentialBookingsRespDTO>(accommodationForTest);
		}
	}
}
