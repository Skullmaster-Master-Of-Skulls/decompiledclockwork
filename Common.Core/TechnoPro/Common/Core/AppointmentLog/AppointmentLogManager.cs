using System;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.AppointmentLog
{
	// Token: 0x02000156 RID: 342
	public class AppointmentLogManager : IAppointmentLogManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00071AD3 File Offset: 0x0006FCD3
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x00071ADB File Offset: 0x0006FCDB
		private IAppointmentLogDAO dao { get; set; }

		// Token: 0x06000F33 RID: 3891 RVA: 0x00071AE4 File Offset: 0x0006FCE4
		public AppointmentLogManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentLogDAO(this.OpContext);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00071B08 File Offset: 0x0006FD08
		public void LogAppModifications(int appointmentId, eAppointmentModifiedItemType appLogFields)
		{
			this.dao.LogAppModifications(appointmentId, eHowModifiedCode.InsertUpdate, appLogFields);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00071B1A File Offset: 0x0006FD1A
		public void LogAppDeletion(int appointmentId, eAppointmentModifiedItemType appLogFields)
		{
			this.dao.LogAppModifications(appointmentId, eHowModifiedCode.Delete, appLogFields);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00071B08 File Offset: 0x0006FD08
		public void LogAppCreation(int appointmentId, eAppointmentModifiedItemType appLogFields)
		{
			this.dao.LogAppModifications(appointmentId, eHowModifiedCode.InsertUpdate, appLogFields);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00071B2C File Offset: 0x0006FD2C
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x00071B34 File Offset: 0x0006FD34
		public OperationContext OpContext { get; set; }
	}
}
