using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Appointments
{
	// Token: 0x0200007C RID: 124
	public interface IAppointmentCancelInfoClientManager : IWebService
	{
		// Token: 0x06000398 RID: 920
		AppCancelInfoDTO LoadCancelInfoByAppointmentId(int AppointmentId);

		// Token: 0x06000399 RID: 921
		void DeleteCancelInfo(int AppointmentId);

		// Token: 0x0600039A RID: 922
		void InsertOrUpdateAppointmentCancelInfo(int appId, AppCancelInfoDTO appCancelInfo);
	}
}
