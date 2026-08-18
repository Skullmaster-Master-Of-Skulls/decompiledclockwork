using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000018 RID: 24
	public class AppointmentLogReusableClientProxy : WCFReusableClientProxy<IAppointmentLog>, IAppointmentLog, IService
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000058AA File Offset: 0x00003AAA
		public AppointmentLogReusableClientProxy(string endpoint) : base(endpoint)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000058BD File Offset: 0x00003ABD
		public AppointmentLogReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
			base.IncludeProxyHeader = false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000058D4 File Offset: 0x00003AD4
		public void LogAppModifications(LogAppModificationsReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.LogAppModifications(request);
			});
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000590C File Offset: 0x00003B0C
		public void LogAppDeletion(LogAppDeletionReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.LogAppDeletion(request);
			});
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005944 File Offset: 0x00003B44
		public void LogAppCreation(LogAppCreationReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.LogAppCreation(request);
			});
		}
	}
}
