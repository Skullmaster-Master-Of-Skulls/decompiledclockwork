using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Data;
using TechnoPro.Common.DAO.Impl.Data;
using TechnoPro.Common.ICore.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Data;

namespace TechnoPro.Common.Core.Data
{
	// Token: 0x02000107 RID: 263
	public class DataMaintenanceManager : IDataMaintenanceManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x00045196 File Offset: 0x00043396
		public DataMaintenanceManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000451A8 File Offset: 0x000433A8
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x000451B0 File Offset: 0x000433B0
		public OperationContext OpContext { get; set; }

		// Token: 0x06000ABA RID: 2746 RVA: 0x000451BC File Offset: 0x000433BC
		public IList<StaffDropListAssignment> LoadAssignmentsForStaffDropList(int staffDropListCid, int staffPid)
		{
			IDataMaintenanceDAO dataMaintenanceDAO = new DataMaintenanceDAO(this.OpContext);
			return dataMaintenanceDAO.LoadAssignmentsForStaffDropList(staffDropListCid, staffPid);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000451E4 File Offset: 0x000433E4
		public ReassignStaffDropListResult ReassignStaffDropList(int staffDropListCid, int staffPidOld, int staffPidNew)
		{
			IDataMaintenanceDAO dataMaintenanceDAO = new DataMaintenanceDAO(this.OpContext);
			ReassignStaffDropListResult result;
			try
			{
				dataMaintenanceDAO.ReassignStaffDropList(staffDropListCid, staffPidOld, staffPidNew);
				result = new ReassignStaffDropListResult
				{
					WasSuccessful = true
				};
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataMaintenanceManager:ReassignStaffDropList:cid={0}:pidold={1}:pidnew={2}:err={3}", new object[]
				{
					staffDropListCid,
					staffPidOld,
					staffPidNew,
					ex.ToString()
				});
				result = new ReassignStaffDropListResult
				{
					ErrorMessage = ex.Message
				};
			}
			return result;
		}
	}
}
