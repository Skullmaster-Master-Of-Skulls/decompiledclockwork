using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Appointments
{
	// Token: 0x0200006D RID: 109
	public class AppointmentCancelInfoRestClientManager : BearerTokenRestProxy<IAppointmentCancelInfoClientManager>, IAppointmentCancelInfoClientManager, IWebService
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0000C540 File Offset: 0x0000A740
		public AppointmentCancelInfoRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000C54A File Offset: 0x0000A74A
		public AppointmentCancelInfoRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000C555 File Offset: 0x0000A755
		public AppCancelInfoDTO LoadCancelInfoByAppointmentId(int AppointmentId)
		{
			return base.Get<AppCancelInfoDTO>(string.Format("appointmentcancelinfo/appid/{0}", AppointmentId), true);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000C56E File Offset: 0x0000A76E
		public void DeleteCancelInfo(int AppointmentId)
		{
			base.Delete(string.Format("appointmentcancelinfo/appid/{0}", AppointmentId));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C588 File Offset: 0x0000A788
		public void InsertOrUpdateAppointmentCancelInfo(int appId, AppCancelInfoDTO appCancelInfo)
		{
			InsertOrUpdateAppointmentCancelInfoReq insertOrUpdateAppointmentCancelInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentCancelInfoReq>();
			insertOrUpdateAppointmentCancelInfoReq.AppointmentId = appId;
			insertOrUpdateAppointmentCancelInfoReq.AppCancelInfo = appCancelInfo;
			base.Post<InsertOrUpdateAppointmentCancelInfoReq>(insertOrUpdateAppointmentCancelInfoReq, "appointmentcancelinfo");
		}
	}
}
