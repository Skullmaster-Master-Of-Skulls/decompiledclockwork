using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.Core.Data;
using TechnoPro.Common.Core.Mappers.Data;
using TechnoPro.Common.ICore.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Data;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000038 RID: 56
	public class DataMaintenanceServiceManager : IDataMaintenance, IService
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000AF30 File Offset: 0x00009130
		public LoadAssignmentsForStaffDropListResp LoadAssignmentsForStaffDropList(LoadAssignmentsForStaffDropListReq Request)
		{
			IDataMaintenanceManager dataMaintenanceManager = new DataMaintenanceManager(Request.GetOperationContext());
			IList<StaffDropListAssignment> list = dataMaintenanceManager.LoadAssignmentsForStaffDropList(Request.StaffDropListControlId, Request.StaffPid);
			LoadAssignmentsForStaffDropListResp loadAssignmentsForStaffDropListResp = new LoadAssignmentsForStaffDropListResp();
			IList<StaffDropListAssignmentDTO> assignments;
			if (list == null)
			{
				assignments = null;
			}
			else
			{
				assignments = (from g in list
				select g.ToDTO()).ToList<StaffDropListAssignmentDTO>();
			}
			loadAssignmentsForStaffDropListResp.Assignments = assignments;
			return loadAssignmentsForStaffDropListResp;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public ReassignStaffDropListResp ReassignStaffDropList(ReassignStaffDropListReq Request)
		{
			IDataMaintenanceManager dataMaintenanceManager = new DataMaintenanceManager(Request.GetOperationContext());
			ReassignStaffDropListResp reassignStaffDropListResp = new ReassignStaffDropListResp();
			ReassignStaffDropListResult reassignStaffDropListResult = dataMaintenanceManager.ReassignStaffDropList(Request.StaffDropListControlId, Request.StaffPidOld, Request.StaffPidNew);
			reassignStaffDropListResp.Result = ((reassignStaffDropListResult != null) ? reassignStaffDropListResult.ToDTO() : null);
			return reassignStaffDropListResp;
		}
	}
}
