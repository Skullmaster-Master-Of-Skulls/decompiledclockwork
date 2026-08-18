using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.AlertTrigger
{
	// Token: 0x020000D0 RID: 208
	public interface IAlertTriggerDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000619 RID: 1561
		int[] FindFieldsFilledInForUserPerStudent(int pid, int[] cidsToCheck);

		// Token: 0x0600061A RID: 1562
		int[] FindFieldsFilledInForUserPerAppointment(int pid, int[] cidsToCheck);
	}
}
