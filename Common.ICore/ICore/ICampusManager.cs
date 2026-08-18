using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000003 RID: 3
	public interface ICampusManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000003 RID: 3
		IList<SchoolCampus> GetCampusList();

		// Token: 0x06000004 RID: 4
		int CreateCampus(SchoolCampus campus);

		// Token: 0x06000005 RID: 5
		void UpdateCampus(SchoolCampus campus);

		// Token: 0x06000006 RID: 6
		void DeleteCampus(int campusId);
	}
}
