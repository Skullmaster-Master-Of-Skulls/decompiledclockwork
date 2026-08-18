using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.AppointmentSync
{
	// Token: 0x020000AF RID: 175
	public interface ISyncContactsDAO : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060004B1 RID: 1201
		bool IsValidEmailAddress(string address);

		// Token: 0x060004B2 RID: 1202
		string ResolveEmailAddress(string nameToResolve);

		// Token: 0x060004B3 RID: 1203
		string GetPrimarySmtpAddress(string email);

		// Token: 0x060004B4 RID: 1204
		IList<ExternalAttendee> GetGroupMembers(string email);
	}
}
