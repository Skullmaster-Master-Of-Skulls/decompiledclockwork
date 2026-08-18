using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200001E RID: 30
	public class AppointmentCancelInfoServiceManager : IAppointmentCancelInfo, IService
	{
		// Token: 0x0600015E RID: 350 RVA: 0x000075CC File Offset: 0x000057CC
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000075E0 File Offset: 0x000057E0
		public LoadCancelInfoByAppointmentIdResp LoadCancelInfoByAppointmentId(LoadCancelInfoByAppointmentIdReq Request)
		{
			IAppointmentCancelInfoManager appointmentCancelInfoManager = new AppointmentCancelInfoManager(Request.GetOperationContext());
			AppCancelInfo appCancelInfo = appointmentCancelInfoManager.LoadCancelInfoByAppointmentId(Request.AppointmentId);
			return new LoadCancelInfoByAppointmentIdResp
			{
				AppCancelInfo = ((appCancelInfo == null) ? null : appCancelInfo.ToDTO())
			};
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007624 File Offset: 0x00005824
		public void DeleteCancelInfo(DeleteCancelInfoReq Request)
		{
			IAppointmentCancelInfoManager appointmentCancelInfoManager = new AppointmentCancelInfoManager(Request.GetOperationContext());
			appointmentCancelInfoManager.DeleteCancelInfo(false, Request.AppointmentId);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000764C File Offset: 0x0000584C
		public void InsertOrUpdateAppointmentCancelInfo(InsertOrUpdateAppointmentCancelInfoReq Request)
		{
			IAppointmentCancelInfoManager appointmentCancelInfoManager = new AppointmentCancelInfoManager(Request.GetOperationContext());
			appointmentCancelInfoManager.InsertOrUpdateAppointmentCancelInfo(false, Request.AppointmentId, Request.AppCancelInfo.ToDomainObject());
		}
	}
}
