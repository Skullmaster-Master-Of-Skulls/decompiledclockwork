using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200000C RID: 12
	public class AppointmentLogServiceManager : IAppointmentLog, IService
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000043B0 File Offset: 0x000025B0
		public void LogAppModifications(LogAppModificationsReq request)
		{
			IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(request.GetOperationContext());
			appointmentLogManager.LogAppModifications(request.AppointmentId, request.AppointmentLogFields);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000043E0 File Offset: 0x000025E0
		public void LogAppDeletion(LogAppDeletionReq request)
		{
			IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(request.GetOperationContext());
			appointmentLogManager.LogAppDeletion(request.AppointmentId, request.AppointmentLogFields);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004410 File Offset: 0x00002610
		public void LogAppCreation(LogAppCreationReq request)
		{
			IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(request.GetOperationContext());
			appointmentLogManager.LogAppCreation(request.AppointmentId, request.AppointmentLogFields);
		}
	}
}
