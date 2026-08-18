using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C5 RID: 197
	public class LegacyAppointmentReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyAppointment>, ILegacyAppointment, IService
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x00014940 File Offset: 0x00012B40
		public LegacyAppointmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001494B File Offset: 0x00012B4B
		public LegacyAppointmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00014958 File Offset: 0x00012B58
		public LoadAsAppointmentModifiedHistoryResp LoadAsAppointmentModifiedHistory(LoadAsAppointmentModifiedHistoryReq Request)
		{
			return this.WrapServiceMethod<LoadAsAppointmentModifiedHistoryResp>(() => this.Proxy.LoadAsAppointmentModifiedHistory(Request));
		}
	}
}
