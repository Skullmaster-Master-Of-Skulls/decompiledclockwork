using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.ICore.AppointmentsRecurring
{
	// Token: 0x02000027 RID: 39
	public interface IRecurringAppointmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600011F RID: 287
		AppointmentRecurringInfo LoadCurrentRecurringAppointmentsSet(int MasterGroupCode);

		// Token: 0x06000120 RID: 288
		void UpdateRecurringAppointmentGroupInformationAndDates(AppointmentRecurringInfo RecurringItems);

		// Token: 0x06000121 RID: 289
		void RemoveAllRecurringAppointmentsExceptionMaster(int MasterGroupCode, int AppointmentId);

		// Token: 0x06000122 RID: 290
		void UpdateRecurringGroupCode(bool RunInTransaction, int AppointmentId, int GroupCode);

		// Token: 0x06000123 RID: 291
		IList<RecurringInstance> UpdateRecurringAppointmentInstances(BaseExtendedAppointment MasterAppointment, IList<RecurringInstance> RecurringInstances, RecurringInstanceSetModifyBehaviour ModifyBehaivour);

		// Token: 0x06000124 RID: 292
		bool IsUserAllowedToEditAllAppointmentsInARecurringSet(int AppointmentId, int PersonId);

		// Token: 0x06000125 RID: 293
		IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId);

		// Token: 0x06000126 RID: 294
		IList<AppointmentForNotification> UpdateRecurringAppointmentAttendees(int groupCode, int appIdAlreadyUpdated, IList<Attendee> attendeesAdded, IList<Attendee> attendeesModified, IList<int> attendeePersonIdsRemoved);

		// Token: 0x06000127 RID: 295
		IList<RecurringInstance> UpdateRecurringWorkshopAppointmentInstances(WorkshopAppointment workshopApp, IList<RecurringInstance> RecurringInstances, RecurringInstanceSetModifyBehaviour ModifyBehaivour);
	}
}
