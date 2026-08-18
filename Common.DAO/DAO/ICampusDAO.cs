using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO
{
	// Token: 0x0200000A RID: 10
	public interface ICampusDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600000B RID: 11
		IList<SchoolCampus> GetCampusList();

		// Token: 0x0600000C RID: 12
		int CreateCampus(SchoolCampus campus);

		// Token: 0x0600000D RID: 13
		void UpdateCampus(SchoolCampus campus);

		// Token: 0x0600000E RID: 14
		void DeleteCampus(int campusId);
	}
}
