using System;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsReminder
{
	// Token: 0x0200008F RID: 143
	public interface IAppointmentsReminderClientManager : IWebService
	{
		// Token: 0x06000446 RID: 1094
		void AddMeToExclusionList();

		// Token: 0x06000447 RID: 1095
		void RemoveMeFromExclusionList();

		// Token: 0x06000448 RID: 1096
		bool IsAppointmentsReminderEnable();
	}
}
