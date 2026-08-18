using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.ClockWorkAudit
{
	// Token: 0x0200009D RID: 157
	public interface IClockWorkAuditDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600040F RID: 1039
		string[] GetDbPatchSqlUserPrivileges();

		// Token: 0x06000410 RID: 1040
		string[] GetClockWorkDbUserPriviliges();

		// Token: 0x06000411 RID: 1041
		string[] GetFilesDbUserPriviliges();

		// Token: 0x06000412 RID: 1042
		bool AreFileDbAndRegularDbTheSameDatabase();
	}
}
