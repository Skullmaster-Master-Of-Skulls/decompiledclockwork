using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.AppointmentsPointOfContact
{
	// Token: 0x020000BB RID: 187
	public interface IPointOfContactManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000591 RID: 1425
		int CreatePointOfContact(bool runInTransaction, PointOfContact PointOfContact);

		// Token: 0x06000592 RID: 1426
		int CreatePointOfContact(bool runInTransaction, PointOfContact PointOfContact, int overrideAppTypeId);

		// Token: 0x06000593 RID: 1427
		void UpdatePointOfContact(bool runInTransaction, PointOfContact PointOfContact);

		// Token: 0x06000594 RID: 1428
		int SaveEmailAsPointOfContact(bool runInTransaction, int StudentPersonId, int StaffPersonId, TPMailMessage Email, ePointOfContactContext PocContext);

		// Token: 0x06000595 RID: 1429
		int SaveEmailAsPointOfContact(bool runInTransaction, int StudentPersonId, int StaffPersonId, TPMailMessage Email, ePointOfContactContext PocContext, int overrideAppTypeId);

		// Token: 0x06000596 RID: 1430
		void DeletePointOfContact(bool runInTransaction, int AppointmentId);

		// Token: 0x06000597 RID: 1431
		PointOfContact LoadPointOfContactById(int AppointmentId);

		// Token: 0x06000598 RID: 1432
		IList<AppType> LoadAllowedPOCAppointmentTypes(int PersonId);

		// Token: 0x06000599 RID: 1433
		int CreatePointOfContactFromMessage(ePointOfContactContext PocContext, int StudentPersonId, string PlainTextMessage);
	}
}
