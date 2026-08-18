using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Data;

namespace TechnoPro.Common.ICore.Data
{
	// Token: 0x020000A4 RID: 164
	public interface IDataMaintenanceManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004CE RID: 1230
		IList<StaffDropListAssignment> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid);

		// Token: 0x060004CF RID: 1231
		ReassignStaffDropListResult ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew);
	}
}
