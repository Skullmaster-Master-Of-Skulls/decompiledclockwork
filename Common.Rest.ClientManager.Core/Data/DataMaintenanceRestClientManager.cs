using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Data
{
	// Token: 0x0200005A RID: 90
	public class DataMaintenanceRestClientManager : BearerTokenRestProxy<IDataMaintenanceClientManager>, IDataMaintenanceClientManager, IWebService
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000AAAE File Offset: 0x00008CAE
		public DataMaintenanceRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public DataMaintenanceRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		public IList<StaffDropListAssignmentDTO> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid)
		{
			return base.GetMany<StaffDropListAssignmentDTO>(string.Format("datamaintenance/assigmentsforstaffdroplist/staffdroplistcid/{0}/staffpid/{1}", staffDropListCid, staffPid), true);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		public ReassignStaffDropListResultDTO ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew)
		{
			ReassignStaffDropListReq reassignStaffDropListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReassignStaffDropListReq>();
			reassignStaffDropListReq.StaffDropListControlId = staffDropListCid;
			reassignStaffDropListReq.StaffPidOld = staffPidOld;
			reassignStaffDropListReq.StaffPidNew = staffPidNew;
			return base.Post<ReassignStaffDropListReq, ReassignStaffDropListResultDTO>(reassignStaffDropListReq, "datamaintenance/reassignstaffdroplist");
		}
	}
}
