using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;

namespace TechnoPro.Common.ICore.AppointmentsReminder
{
	// Token: 0x020000D0 RID: 208
	public interface IAppointmentsReminderManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000665 RID: 1637
		IList<AppointmentReminder> LoadAppointmentsReminder();

		// Token: 0x06000666 RID: 1638
		void ChangeAppointmentsReminderNotificationStatus(IList<int> appReminderIdList, bool alreadyNotified);

		// Token: 0x06000667 RID: 1639
		void AddPeopleToExclusionList(int personId);

		// Token: 0x06000668 RID: 1640
		void RemovePeopleFromExclusionList(int personId);

		// Token: 0x06000669 RID: 1641
		IList<int> LoadPeopleExclusionList();

		// Token: 0x0600066A RID: 1642
		IList<int> LoadGroupInclusionList();

		// Token: 0x0600066B RID: 1643
		bool IsAppointmentsReminderEnable();

		// Token: 0x0600066C RID: 1644
		int AddAppointmentReminder(AppointmentReminder appReminder);

		// Token: 0x0600066D RID: 1645
		void UpdateAppointmentReminder(AppointmentReminder appReminder);

		// Token: 0x0600066E RID: 1646
		void DeleteAppointmentReminder(int appointmentID, int attPersonID);
	}
}
