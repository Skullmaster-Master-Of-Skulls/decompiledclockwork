using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Data
{
	// Token: 0x02000065 RID: 101
	public interface IDataMaintenanceClientManager : IWebService
	{
		// Token: 0x06000306 RID: 774
		IList<StaffDropListAssignmentDTO> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid);

		// Token: 0x06000307 RID: 775
		ReassignStaffDropListResultDTO ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew);
	}
}
