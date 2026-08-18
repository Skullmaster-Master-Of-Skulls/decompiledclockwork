using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.AppointmentLog
{
	// Token: 0x020000D2 RID: 210
	public interface IAppointmentLogManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600068F RID: 1679
		void LogAppModifications(int appointmentId, eAppointmentModifiedItemType appLogFields);

		// Token: 0x06000690 RID: 1680
		void LogAppDeletion(int appointmentId, eAppointmentModifiedItemType appLogFields);

		// Token: 0x06000691 RID: 1681
		void LogAppCreation(int appointmentId, eAppointmentModifiedItemType appLogFields);
	}
}
