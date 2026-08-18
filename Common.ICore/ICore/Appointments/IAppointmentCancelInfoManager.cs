using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E0 RID: 224
	public interface IAppointmentCancelInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006F7 RID: 1783
		AppCancelInfo LoadCancelInfoByAppointmentId(int AppointmentId);

		// Token: 0x060006F8 RID: 1784
		void DeleteCancelInfo(bool runInTransaction, int AppointmentId);

		// Token: 0x060006F9 RID: 1785
		void InsertOrUpdateAppointmentCancelInfo(bool runInTransaction, int appId, AppCancelInfo appCancelInfo);
	}
}
