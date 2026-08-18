using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.Emailing
{
	// Token: 0x02000090 RID: 144
	public interface IAppointmentReminderEmailManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000414 RID: 1044
		IList<TPMailResult> SendAppointmentReminderEmailsToStudents(DateTime DayToSendRemindersFor, int EmailTemplateId, string BatchEmailTitle, bool CopyEmailToPointOfContact = true, int IconIdToIndicateEmailWasSent = 121, int[] AppTypeIds = null, string TestModeEmail = null);

		// Token: 0x06000415 RID: 1045
		IList<TPMailResult> SendNoshowReminderEmailsToStudents(int EmailTemplateId, string BatchEmailTitle, bool CopyEmailToPointOfContact = true, int IconIdToIndicateEmailWasSent = 121, int[] AppTypeIds = null, string TestModeEmail = null, DateTime? MinDateToCheckFrom = null);
	}
}
