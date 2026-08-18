using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000BF RID: 191
	public interface IApplicationSyncAdministrationManager : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060005BF RID: 1471
		DelegatePermissionLevel GetDelegatePermissionLevel(string userEmailAddress);

		// Token: 0x060005C0 RID: 1472
		string GetPrimarySmtpAddress(string email);

		// Token: 0x060005C1 RID: 1473
		void FillUniqueId2FieldInDatabase();

		// Token: 0x060005C2 RID: 1474
		ProductLicenseState GetCalendarSyncLicenseStatus(out DateTime? expiryDate);

		// Token: 0x060005C3 RID: 1475
		LicenseKeyInfo GetCalendarSyncProductKey();
	}
}
