using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.Common.DAO.PerformanceTesting
{
	// Token: 0x0200003C RID: 60
	public interface IPerformanceTestDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600011C RID: 284
		List<Appointment> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime);
	}
}
