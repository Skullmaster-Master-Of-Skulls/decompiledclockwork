using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ConnectionString;

namespace TechnoPro.Common.ICore.ConnectionString
{
	// Token: 0x020000B0 RID: 176
	public interface IClockWorkConnectionStringManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000544 RID: 1348
		string CreateConnectionString(ClockWorkConnectionString ccs);

		// Token: 0x06000545 RID: 1349
		void UpdateConnectionString(ClockWorkConnectionString ccs);

		// Token: 0x06000546 RID: 1350
		void DeleteClockWorkConnectionString(string ccsName);

		// Token: 0x06000547 RID: 1351
		bool ConnectionNameAlreadyExists(string ccsName);

		// Token: 0x06000548 RID: 1352
		void AssignConnectionString(string appId, string ccsName);

		// Token: 0x06000549 RID: 1353
		ClockWorkConnectionString GetConnectionString(string appId);

		// Token: 0x0600054A RID: 1354
		void RemoveAssignedClockWorkConnectionString(string appId);

		// Token: 0x0600054B RID: 1355
		IList<ClockWorkConnectionString> GetConnectionStringList();

		// Token: 0x0600054C RID: 1356
		IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList();

		// Token: 0x0600054D RID: 1357
		IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList(eTechnoProProductNames productName);

		// Token: 0x0600054E RID: 1358
		void ImportFromFile(string filename);

		// Token: 0x0600054F RID: 1359
		void ExportToFile(string filename);

		// Token: 0x06000550 RID: 1360
		void ExportToFile(string filename, eTechnoProProductNames productName);
	}
}
