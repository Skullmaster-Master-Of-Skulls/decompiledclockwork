using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.AppointmentSync
{
	// Token: 0x020000B2 RID: 178
	public interface IApplicationSyncAdministrationDAO : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060004CF RID: 1231
		DelegatePermissionLevel GetDelegatePermissionLevel(string userEmailAddress);
	}
}
