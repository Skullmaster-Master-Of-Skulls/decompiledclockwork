using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.ICore.AppointmentsWorkshops
{
	// Token: 0x020000C4 RID: 196
	public interface IWorkshopAppointmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005E2 RID: 1506
		IList<WorkshopAppointment> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId);

		// Token: 0x060005E3 RID: 1507
		IList<WorkshopAppointment> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId);

		// Token: 0x060005E4 RID: 1508
		int CreateWorkshopAppointment(bool runInTransaction, WorkshopAppointment WorkshopApp);

		// Token: 0x060005E5 RID: 1509
		void UpdateWorkshopAppointment(bool runInTransaction, WorkshopAppointment WorkshopApp);

		// Token: 0x060005E6 RID: 1510
		WorkshopAppointment LoadWorkshopAppointmentById(int workshopAppId);

		// Token: 0x060005E7 RID: 1511
		void DeleteWorkshopAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x060005E8 RID: 1512
		void UncancelWorkshopAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x060005E9 RID: 1513
		void CancelWorkshopAppointment(bool runInTransaction, int AppointmentId, AppCancelInfo CancelReason);

		// Token: 0x060005EA RID: 1514
		IList<AppType> GetWorkshopGroups();

		// Token: 0x060005EB RID: 1515
		void UpdateAppointmentWorkshopId(bool runInTransaction, int AppointmentId, int NewWorkshopId);

		// Token: 0x060005EC RID: 1516
		void UpdateWorkshopAppointmentMaxAttendees(int appointmentId, int newMaxAttendees);

		// Token: 0x060005ED RID: 1517
		bool IsAppointmentAWorkshop(int appointmentId);
	}
}
