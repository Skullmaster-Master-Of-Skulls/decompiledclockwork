using System;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Core.Impl;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters
{
	// Token: 0x020000A6 RID: 166
	public static class BaseMessageReqAdapter
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0001C008 File Offset: 0x0001A208
		public static T GetOperationContext<T>(this BaseMessageReq request) where T : OperationContext
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				t.WhoAmI = request.WhoAmI;
				t.AppContext = request.ApplicationContext;
				t.TenantId = request.TenantId;
				bool flag2 = t is ClockWorkServerOperationContext;
				if (flag2)
				{
					ServerExecutingContext serverExecutingContext = ObjectFactory.Resolve<ServerExecutingContext>();
					ClockWorkServerOperationContext clockWorkServerOperationContext = t as ClockWorkServerOperationContext;
					clockWorkServerOperationContext.ClockWorkServerInstanceName = serverExecutingContext.ClockWorkServerInstanceName;
					clockWorkServerOperationContext.ClockWorkServerVirtualDirectory = serverExecutingContext.ServerVirtualApplicationName;
				}
				result = t;
			}
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public static OperationContext GetOperationContext(this BaseMessageReq request)
		{
			return request.GetOperationContext<OperationContext>();
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		public static T GetOperationContext<T>(this BaseMessageContractReq request) where T : OperationContext
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			bool flag = t != null;
			if (flag)
			{
				t.WhoAmI = request.WhoAmI;
				t.AppContext = request.ApplicationContext;
			}
			return t;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001C140 File Offset: 0x0001A340
		public static OperationContext GetOperationContext(this BaseMessageContractReq request)
		{
			return request.GetOperationContext<OperationContext>();
		}
	}
}
