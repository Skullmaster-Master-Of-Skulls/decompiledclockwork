using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.Appointment;

namespace TechnoPro.Common.ICore.Legacy
{
	// Token: 0x02000075 RID: 117
	public interface ILegacyAppointmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600034C RID: 844
		IList<AppointmentModifiedHistoryItem> LoadAppointmentModifiedHistory(int AppointmentId);
	}
}
