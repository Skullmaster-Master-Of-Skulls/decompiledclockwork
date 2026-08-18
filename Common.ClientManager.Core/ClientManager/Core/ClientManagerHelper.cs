using System;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core
{
	// Token: 0x02000004 RID: 4
	[Obsolete("Use ObjectFactory.Resolve<IRequestBuilderClientManager>() instead")]
	public static class ClientManagerHelper
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000026EC File Offset: 0x000008EC
		public static void UpdateRequest(BaseMessageReq request)
		{
			bool flag = ClientManagerHelper.OverrideUpdateRequest != null;
			if (flag)
			{
				ClientManagerHelper.OverrideUpdateRequest(request);
			}
			else
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				request.WhoAmI = clientCache.WhoAmIId;
				request.Token = clientCache.AuthenticationToken;
				request.ApplicationContext = ObjectFactory.Resolve<ApplicationContext>();
				request.TenantId = clientCache.TenantId;
				BaseReportMessageReq baseReportMessageReq = request as BaseReportMessageReq;
				bool flag2 = baseReportMessageReq != null;
				if (flag2)
				{
					BaseReportMessageReq baseReportMessageReq2 = baseReportMessageReq;
					ApplicationContext applicationContext = baseReportMessageReq.ApplicationContext;
					baseReportMessageReq2.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002778 File Offset: 0x00000978
		public static T CreateRequest<T>() where T : BaseMessageReq
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			bool flag = t != null;
			if (flag)
			{
				ClientManagerHelper.UpdateRequest(t);
			}
			return t;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027BC File Offset: 0x000009BC
		public static T CreateMessageRequest<T>() where T : BaseMessageContractReq
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

		// Token: 0x06000022 RID: 34 RVA: 0x00002840 File Offset: 0x00000A40
		public static T UpdateMessageRequest<T>(this BaseMessageContractReq request) where T : BaseMessageContractReq
		{
			bool flag = request != null;
			if (flag)
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				request.WhoAmI = clientCache.WhoAmIId;
				request.SessionId = ((clientCache.AuthenticationToken != null) ? clientCache.AuthenticationToken.SessionId : null);
				request.ApplicationContext = ObjectFactory.Resolve<ApplicationContext>();
			}
			return (T)((object)request);
		}

		// Token: 0x04000003 RID: 3
		public static Action<BaseMessageReq> OverrideUpdateRequest;
	}
}
