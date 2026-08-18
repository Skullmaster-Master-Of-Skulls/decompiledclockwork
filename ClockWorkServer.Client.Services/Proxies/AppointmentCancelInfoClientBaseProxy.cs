using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200016D RID: 365
	internal class AppointmentCancelInfoClientBaseProxy : ClientBase<IAppointmentCancelInfo>, IAppointmentCancelInfo, IService
	{
		// Token: 0x06000E37 RID: 3639 RVA: 0x00024EA8 File Offset: 0x000230A8
		public AppointmentCancelInfoClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00024EB3 File Offset: 0x000230B3
		public AppointmentCancelInfoClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00024EBF File Offset: 0x000230BF
		public void DeleteCancelInfo(DeleteCancelInfoReq Request)
		{
			base.Channel.DeleteCancelInfo(Request);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00024ECF File Offset: 0x000230CF
		public void InsertOrUpdateAppointmentCancelInfo(InsertOrUpdateAppointmentCancelInfoReq Request)
		{
			base.Channel.InsertOrUpdateAppointmentCancelInfo(Request);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00024EE0 File Offset: 0x000230E0
		public LoadCancelInfoByAppointmentIdResp LoadCancelInfoByAppointmentId(LoadCancelInfoByAppointmentIdReq Request)
		{
			return base.Channel.LoadCancelInfoByAppointmentId(Request);
		}
	}
}
