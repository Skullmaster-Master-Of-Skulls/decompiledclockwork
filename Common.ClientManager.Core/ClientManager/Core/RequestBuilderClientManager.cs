using System;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core
{
	// Token: 0x02000005 RID: 5
	public class RequestBuilderClientManager : IRequestBuilderClientManager
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000028A0 File Offset: 0x00000AA0
		public virtual T CreateRequest<T>() where T : BaseMessageReq
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			bool flag = t != null;
			if (flag)
			{
				this.UpdateRequest<T>(t);
			}
			return t;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028E0 File Offset: 0x00000AE0
		public virtual T UpdateRequest<T>(T request) where T : BaseMessageReq
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			request.WhoAmI = clientCache.WhoAmIId;
			request.Token = clientCache.AuthenticationToken;
			request.ApplicationContext = ObjectFactory.Resolve<ApplicationContext>();
			request.TenantId = clientCache.TenantId;
			BaseReportMessageReq baseReportMessageReq = request as BaseReportMessageReq;
			bool flag = baseReportMessageReq != null;
			if (flag)
			{
				BaseReportMessageReq baseReportMessageReq2 = baseReportMessageReq;
				ApplicationContext applicationContext = baseReportMessageReq.ApplicationContext;
				baseReportMessageReq2.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			}
			return request;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002970 File Offset: 0x00000B70
		public virtual T CreateMessageRequest<T>() where T : BaseMessageContractReq
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			bool flag = t != null;
			if (flag)
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				t.WhoAmI = clientCache.WhoAmIId;
				t.SessionId = ((clientCache.AuthenticationToken != null) ? clientCache.AuthenticationToken.SessionId : null);
				t.ApplicationContext = ObjectFactory.Resolve<ApplicationContext>();
			}
			return t;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029F4 File Offset: 0x00000BF4
		public virtual T UpdateMessageRequest<T>(T request) where T : BaseMessageContractReq
		{
			bool flag = request != null;
			if (flag)
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				request.WhoAmI = clientCache.WhoAmIId;
				request.SessionId = ((clientCache.AuthenticationToken != null) ? clientCache.AuthenticationToken.SessionId : null);
				request.ApplicationContext = ObjectFactory.Resolve<ApplicationContext>();
			}
			return request;
		}
	}
}
