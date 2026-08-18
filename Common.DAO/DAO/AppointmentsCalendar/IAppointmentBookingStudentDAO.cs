using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.DAO.AppointmentsCalendar
{
	// Token: 0x020000C4 RID: 196
	public interface IAppointmentBookingStudentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600055D RID: 1373
		IList<Channel> GetAllChannels(string channelsXml, string legacyChannelsXml);
	}
}
