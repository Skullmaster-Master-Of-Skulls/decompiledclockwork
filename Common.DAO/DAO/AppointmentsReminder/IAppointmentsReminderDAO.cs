using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;

namespace TechnoPro.Common.DAO.AppointmentsReminder
{
	// Token: 0x020000C2 RID: 194
	public interface IAppointmentsReminderDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600053B RID: 1339
		IList<AppointmentReminder> LoadAppointmentsReminder();

		// Token: 0x0600053C RID: 1340
		void ChangeAppointmentsReminderNotificationStatus(IList<int> appReminderIdList, bool alreadyNotified);

		// Token: 0x0600053D RID: 1341
		bool AddPeopleToExclusionList(int personId);

		// Token: 0x0600053E RID: 1342
		bool RemovePeopleFromExclusionList(int personId);

		// Token: 0x0600053F RID: 1343
		bool IsPersonInExclusionList(int personId);

		// Token: 0x06000540 RID: 1344
		IList<int> LoadPeopleExclusionList();

		// Token: 0x06000541 RID: 1345
		IList<int> LoadGroupInclusionList();

		// Token: 0x06000542 RID: 1346
		int AddAppointmentReminder(AppointmentReminder appReminder);

		// Token: 0x06000543 RID: 1347
		void UpdateAppointmentReminder(AppointmentReminder appReminder);

		// Token: 0x06000544 RID: 1348
		void DeleteAppointmentReminder(int appointmentID, int attPersonID);
	}
}
