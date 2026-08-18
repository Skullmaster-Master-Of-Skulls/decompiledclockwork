using System;
using System.Data.Common;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A4 RID: 164
	public interface IAppointmentCancelInfoDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000447 RID: 1095
		AppCancelInfo LoadCancelInfoByAppointmentId(int AppointmentId);

		// Token: 0x06000448 RID: 1096
		void DeleteCancelInfo(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000449 RID: 1097
		void InsertOrUpdateAppointmentCancelInfo(int appId, AppCancelInfo appCancelInfo, DbTransaction transaction = null);
	}
}
