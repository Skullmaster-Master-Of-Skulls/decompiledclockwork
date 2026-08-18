using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Email
{
	// Token: 0x0200007A RID: 122
	public interface IAppointmentReminderEmailDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600030F RID: 783
		void LogEmailSent(int StudentPersonId, string BatchEmailTitle, TPMailMessage Email, string Note, int TemplateId = 0);
	}
}
