using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000019 RID: 25
	internal class AppointmentLogClientBaseProxy : ClientBase<IAppointmentLog>, IAppointmentLog, IService
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00005979 File Offset: 0x00003B79
		public AppointmentLogClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005984 File Offset: 0x00003B84
		public AppointmentLogClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005990 File Offset: 0x00003B90
		public void LogAppModifications(LogAppModificationsReq request)
		{
			base.Channel.LogAppModifications(request);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000059A0 File Offset: 0x00003BA0
		public void LogAppDeletion(LogAppDeletionReq request)
		{
			base.Channel.LogAppDeletion(request);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000059B0 File Offset: 0x00003BB0
		public void LogAppCreation(LogAppCreationReq request)
		{
			base.Channel.LogAppCreation(request);
		}
	}
}
