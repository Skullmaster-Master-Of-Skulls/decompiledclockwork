using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.DAO.AppointmentsWorkshops
{
	// Token: 0x020000B4 RID: 180
	public interface IWorkshopAppointmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004D5 RID: 1237
		IList<WorkshopAppointment> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId, IList<int> AllowedAppTypeIds);

		// Token: 0x060004D6 RID: 1238
		IList<WorkshopAppointment> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId);

		// Token: 0x060004D7 RID: 1239
		WorkshopAppointment LoadWorkshopAppointmentById(int workshopAppId, IList<int> AllowedAppTypeIds);

		// Token: 0x060004D8 RID: 1240
		void UpdateWorkshopAppointment(WorkshopAppointment WorkshopApp);

		// Token: 0x060004D9 RID: 1241
		int CreateWorkshopAppointment(WorkshopAppointment WorkshopApp);

		// Token: 0x060004DA RID: 1242
		void UpdateAppointmentWorkshopId(int AppointmentId, int NewWorkshopId);

		// Token: 0x060004DB RID: 1243
		void UpdateWorkshopAppointmentMaxAttendees(int appointmentId, int newMaxAttendees);
	}
}
