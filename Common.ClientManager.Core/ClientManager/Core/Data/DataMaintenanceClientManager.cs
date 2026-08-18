using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Data
{
	// Token: 0x0200006C RID: 108
	public class DataMaintenanceClientManager : IDataMaintenanceClientManager, IWebService
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x000119DC File Offset: 0x0000FBDC
		public IList<StaffDropListAssignmentDTO> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid)
		{
			LoadAssignmentsForStaffDropListReq loadAssignmentsForStaffDropListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAssignmentsForStaffDropListReq>();
			loadAssignmentsForStaffDropListReq.StaffDropListControlId = staffDropListCid;
			loadAssignmentsForStaffDropListReq.StaffPid = staffPid;
			return ClientServiceFactory.GetClientInstance<IDataMaintenance>().LoadAssignmentsForStaffDropList(loadAssignmentsForStaffDropListReq).Assignments;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011A1C File Offset: 0x0000FC1C
		public ReassignStaffDropListResultDTO ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew)
		{
			ReassignStaffDropListReq reassignStaffDropListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReassignStaffDropListReq>();
			reassignStaffDropListReq.StaffDropListControlId = staffDropListCid;
			reassignStaffDropListReq.StaffPidOld = staffPidOld;
			reassignStaffDropListReq.StaffPidNew = staffPidNew;
			return ClientServiceFactory.GetClientInstance<IDataMaintenance>().ReassignStaffDropList(reassignStaffDropListReq).Result;
		}
	}
}
