using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Data;

namespace TechnoPro.Common.DAO.Data
{
	// Token: 0x0200008C RID: 140
	public interface IDataMaintenanceDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003A1 RID: 929
		IList<StaffDropListAssignment> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid);

		// Token: 0x060003A2 RID: 930
		void ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew);
	}
}
