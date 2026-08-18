using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000023 RID: 35
	public class TestExamSeatServiceManager : ITestExamSeat, IService
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00008444 File Offset: 0x00006644
		public LoadAllowedSeatsResp LoadAllowedSeats(LoadAllowedSeatsReq Request)
		{
			ITestExamSeatManager testExamSeatManager = new TestExamSeatManager(Request.GetOperationContext());
			IList<AppointmentRoom> list = testExamSeatManager.LoadAllowedSeats(Request.ClassTestType);
			return new LoadAllowedSeatsResp
			{
				Seats = ((list == null) ? null : list.ToList<AppointmentRoom>().ConvertAll<AppointmentRoomDTO>(new Converter<AppointmentRoom, AppointmentRoomDTO>(AppointmentRoomMapper.ToDTO)))
			};
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000849C File Offset: 0x0000669C
		public LoadSeatByIdResp LoadSeatById(LoadSeatByIdReq Request)
		{
			ITestExamSeatManager testExamSeatManager = new TestExamSeatManager(Request.GetOperationContext());
			AppointmentRoom appointmentRoom = testExamSeatManager.LoadSeatById(Request.RoomId);
			return new LoadSeatByIdResp
			{
				Seat = ((appointmentRoom == null) ? null : appointmentRoom.ToDTO())
			};
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000084E0 File Offset: 0x000066E0
		public LoadRoomsWithAvailabilityResp LoadRoomsWithAvailability(LoadRoomsWithAvailabilityReq Request)
		{
			ITestExamSeatManager testExamSeatManager = new TestExamSeatManager(Request.GetOperationContext());
			IList<AppointmentRoomWithAvailability> list = testExamSeatManager.LoadRoomsWithAvailability(Request.TestType, Request.StartDateTime, Request.EndDateTime, Request.RoomIdsToIgnore);
			LoadRoomsWithAvailabilityResp loadRoomsWithAvailabilityResp = new LoadRoomsWithAvailabilityResp();
			IList<AppointmentRoomWithAvailabilityDTO> roomsWithAvailability;
			if (list != null)
			{
				roomsWithAvailability = list.ToList<AppointmentRoomWithAvailability>().ConvertAll<AppointmentRoomWithAvailabilityDTO>((AppointmentRoomWithAvailability g) => g.ToDTO());
			}
			else
			{
				roomsWithAvailability = null;
			}
			loadRoomsWithAvailabilityResp.RoomsWithAvailability = roomsWithAvailability;
			return loadRoomsWithAvailabilityResp;
		}
	}
}
