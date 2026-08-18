using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000C2 RID: 194
	public interface IAppointmentSyncSettingsManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005D8 RID: 1496
		SyncApplicationSettings LoadSyncSettings(string settingsInstanceName);

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060005D9 RID: 1497
		SyncApplicationSettings SyncSettings { get; }
	}
}
