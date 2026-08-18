using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200007D RID: 125
	public class TestExamSeatRestClientManager : BearerTokenRestProxy<ITestExamSeatClientManager>, ITestExamSeatClientManager, IWebService
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public TestExamSeatRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000DB5E File Offset: 0x0000BD5E
		public TestExamSeatRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000DB69 File Offset: 0x0000BD69
		public IList<AppointmentRoomDTO> LoadAllowedSeats(eTestExamSeatType TestType)
		{
			return base.GetMany<AppointmentRoomDTO>(string.Format("testexamseat/allowedseats/classtesttype/{0}", TestType), true);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000DB82 File Offset: 0x0000BD82
		public AppointmentRoomDTO LoadSeatById(int RoomId)
		{
			return base.Get<AppointmentRoomDTO>(string.Format("testexamseat/seat/roomid/{0}", RoomId), true);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000DB9B File Offset: 0x0000BD9B
		public IList<AppointmentRoomWithAvailabilityDTO> LoadRoomsWithAvailability(eTestExamSeatType TestType, DateTime StartDateTime, DateTime EndDateTime, IList<int> RoomIdsToIgnore)
		{
			return base.GetMany<AppointmentRoomWithAvailabilityDTO>(string.Format("testexamseat/roomswithavailability/testtype/{0}/range/{1}/{2}/roomidstoignore/{3}", new object[]
			{
				TestType,
				StartDateTime,
				EndDateTime,
				RoomIdsToIgnore
			}), true);
		}
	}
}
