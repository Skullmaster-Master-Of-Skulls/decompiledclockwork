using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C6 RID: 198
	internal class LegacyAppointmentClientBaseProxy : ClientBase<ILegacyAppointment>, ILegacyAppointment, IService
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00014990 File Offset: 0x00012B90
		public LegacyAppointmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001499B File Offset: 0x00012B9B
		public LegacyAppointmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000149A8 File Offset: 0x00012BA8
		public LoadAsAppointmentModifiedHistoryResp LoadAsAppointmentModifiedHistory(LoadAsAppointmentModifiedHistoryReq Request)
		{
			return base.Channel.LoadAsAppointmentModifiedHistory(Request);
		}
	}
}
