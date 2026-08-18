using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ConnectionString;

namespace TechnoPro.Common.DAO.ConnectionString
{
	// Token: 0x02000096 RID: 150
	public interface IClockWorkConnectionStringDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003DE RID: 990
		string CreateConnectionString(ClockWorkConnectionString ccs);

		// Token: 0x060003DF RID: 991
		void UpdateConnectionString(ClockWorkConnectionString ccs);

		// Token: 0x060003E0 RID: 992
		void DeleteClockWorkConnectionString(string ccsName);

		// Token: 0x060003E1 RID: 993
		bool ConnectionNameAlreadyExists(string ccsName);

		// Token: 0x060003E2 RID: 994
		void AssignConnectionString(string appId, string ccsName);

		// Token: 0x060003E3 RID: 995
		ClockWorkConnectionString GetConnectionString(string appId);

		// Token: 0x060003E4 RID: 996
		void RemoveAssignedClockWorkConnectionString(string appId);

		// Token: 0x060003E5 RID: 997
		IList<ClockWorkConnectionString> GetConnectionStringList();

		// Token: 0x060003E6 RID: 998
		IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList();

		// Token: 0x060003E7 RID: 999
		IList<ClockWorkApplicationConnectionString> GetAssignedConnectionStringList(eTechnoProProductNames productName);
	}
}
