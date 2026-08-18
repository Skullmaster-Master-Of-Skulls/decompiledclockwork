using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000BC RID: 188
	public interface IApplicationSyncFactory : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x0600059A RID: 1434
		IExternalAppointmentManager CreateExternalAppointmentManager();

		// Token: 0x0600059B RID: 1435
		IApplicationSyncAdministrationManager CreateApplicationSyncAdministrationManager();
	}
}
