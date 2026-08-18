using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Appointments
{
	// Token: 0x02000082 RID: 130
	public class AppointmentCancelInfoClientManager : IAppointmentCancelInfoClientManager, IWebService
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x000153EC File Offset: 0x000135EC
		public AppCancelInfoDTO LoadCancelInfoByAppointmentId(int AppointmentId)
		{
			LoadCancelInfoByAppointmentIdReq loadCancelInfoByAppointmentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCancelInfoByAppointmentIdReq>();
			loadCancelInfoByAppointmentIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IAppointmentCancelInfo>().LoadCancelInfoByAppointmentId(loadCancelInfoByAppointmentIdReq).AppCancelInfo;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015424 File Offset: 0x00013624
		public void DeleteCancelInfo(int AppointmentId)
		{
			DeleteCancelInfoReq deleteCancelInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteCancelInfoReq>();
			deleteCancelInfoReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IAppointmentCancelInfo>().DeleteCancelInfo(deleteCancelInfoReq);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015454 File Offset: 0x00013654
		public void InsertOrUpdateAppointmentCancelInfo(int appId, AppCancelInfoDTO appCancelInfo)
		{
			InsertOrUpdateAppointmentCancelInfoReq insertOrUpdateAppointmentCancelInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentCancelInfoReq>();
			insertOrUpdateAppointmentCancelInfoReq.AppointmentId = appId;
			insertOrUpdateAppointmentCancelInfoReq.AppCancelInfo = appCancelInfo;
			ClientServiceFactory.GetClientInstance<IAppointmentCancelInfo>().InsertOrUpdateAppointmentCancelInfo(insertOrUpdateAppointmentCancelInfoReq);
		}
	}
}
