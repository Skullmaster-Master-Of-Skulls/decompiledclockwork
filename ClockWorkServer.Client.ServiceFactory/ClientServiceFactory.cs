using System;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Client.ServiceFactory
{
	// Token: 0x02000002 RID: 2
	public class ClientServiceFactory
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public static bool UseService { get; set; } = true;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002077 File Offset: 0x00000277
		public static bool UseMSMQ { get; set; } = false;

		// Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		public static TInterface GetCloudServiceClientInstance<TInterface>(Uri cloudServiceUri) where TInterface : class, IService
		{
			return AzureClientProxy<TInterface>.GetInstance(cloudServiceUri);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public static TInterface GetClientInstance<TInterface>() where TInterface : class, IService
		{
			return ClientServiceFactory.GetClientInstance<TInterface>(ClientServiceFactory.UseService, false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B8 File Offset: 0x000002B8
		public static TInterface GetClientInstance<TInterface>(bool useServerInstance, bool forceServerInstance = false) where TInterface : class, IService
		{
			bool flag;
			return ClientServiceFactory.GetClientInstance<TInterface>(useServerInstance, out flag, forceServerInstance);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D4 File Offset: 0x000002D4
		public static TInterface GetClientInstance<TInterface>(bool useServerInstance, out bool usedServerInstance, bool forceServerInstance = false) where TInterface : class, IService
		{
			string name = typeof(TInterface).Name;
			string arg = name.Substring(1);
			bool useService = ClientServiceFactory.UseService;
			if (useService)
			{
				useServerInstance = true;
			}
			bool flag = useServerInstance;
			if (flag)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager["cClockWorkServerEnabled"];
				bool flag2 = obj == null || (bool)obj;
				bool flag3 = !flag2;
				if (flag3)
				{
					useServerInstance = false;
				}
			}
			TInterface tinterface = default(TInterface);
			bool flag4 = useServerInstance;
			if (flag4)
			{
				tinterface = WCFClientProxy<TInterface>.GetReusableInstance();
				bool flag5 = tinterface == null && forceServerInstance;
				if (flag5)
				{
					usedServerInstance = false;
					return default(TInterface);
				}
			}
			usedServerInstance = (tinterface != null);
			bool flag6 = tinterface == null;
			if (flag6)
			{
				Type type = Type.GetType(string.Format("TechnoPro.ClockWorkServer.Common.Services.Impl.{0}ServiceManager, ClockWorkServer.Common.Services.Impl", arg));
				bool flag7 = type != null;
				if (flag7)
				{
					tinterface = (TInterface)((object)Activator.CreateInstance(type));
				}
			}
			return tinterface;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021CC File Offset: 0x000003CC
		public static TInterface GetDirectClientInstanceNoServer<TInterface>() where TInterface : class, IService
		{
			string name = typeof(TInterface).Name;
			string arg = name.Substring(1);
			Type type = Type.GetType(string.Format("TechnoPro.ClockWorkServer.Common.Services.Impl.{0}ServiceManager, ClockWorkServer.Common.Services.Impl", arg));
			TInterface result = default(TInterface);
			bool flag = type != null;
			if (flag)
			{
				result = (TInterface)((object)Activator.CreateInstance(type));
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000222C File Offset: 0x0000042C
		public static TInterface GetMsmqClientInstance<TInterface>(ClockWorkServerPreferredConnectionInfo connInfo = null) where TInterface : class, IService
		{
			return ClientServiceFactory.GetMsmqClientInstance<TInterface>(ClientServiceFactory.UseMSMQ, false, connInfo);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000224C File Offset: 0x0000044C
		public static TInterface GetMsmqClientInstance<TInterface>(bool useServerInstance, bool forceServerInstance = false, ClockWorkServerPreferredConnectionInfo connInfo = null) where TInterface : class, IService
		{
			bool flag;
			return ClientServiceFactory.GetMsmqClientInstance<TInterface>(useServerInstance, out flag, forceServerInstance, connInfo);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002268 File Offset: 0x00000468
		public static TInterface GetMsmqClientInstance<TInterface>(bool useServerInstance, out bool usedServerInstance, bool forceServerInstance = false, ClockWorkServerPreferredConnectionInfo connInfo = null) where TInterface : class, IService
		{
			string name = typeof(TInterface).Name;
			string text = name.Substring(1);
			bool useMSMQ = ClientServiceFactory.UseMSMQ;
			if (useMSMQ)
			{
				useServerInstance = true;
			}
			CWLogger.Logger.Trace("ClientServiceFactory::GetMsmqClientInstance: service='{0}' userServerInstance='{1}' forcedServerInstance='{2}'", text, useServerInstance, forceServerInstance);
			bool flag = useServerInstance;
			if (flag)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager["cClockWorkServerEnabled"];
				bool flag2 = obj == null || (bool)obj;
				bool flag3 = !flag2;
				if (flag3)
				{
					useServerInstance = false;
				}
			}
			TInterface tinterface = default(TInterface);
			bool flag4 = useServerInstance;
			if (flag4)
			{
				tinterface = WCFClientProxy<TInterface>.GetMsmqInstance(connInfo);
				bool flag5 = tinterface == null && forceServerInstance;
				if (flag5)
				{
					CWLogger.Logger.Trace("ClientServiceFactory::GetMsmqClientInstance: returning NULL");
					usedServerInstance = false;
					return default(TInterface);
				}
			}
			usedServerInstance = (tinterface != null);
			bool flag6 = tinterface == null;
			if (flag6)
			{
				CWLogger.Logger.Trace("ClientServiceFactory::GetMsmqClientInstance: Getting from service manager ...");
				Type type = Type.GetType(string.Format("TechnoPro.ClockWorkServer.Common.Services.Impl.{0}ServiceManager, ClockWorkServer.Common.Services.Impl", text));
				bool flag7 = type != null;
				if (flag7)
				{
					tinterface = (TInterface)((object)Activator.CreateInstance(type));
				}
				bool flag8 = tinterface != null;
				if (flag8)
				{
					CWLogger.Logger.Trace("ClientServiceFactory::Instance created successfully from service manager");
				}
			}
			return tinterface;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023C0 File Offset: 0x000005C0
		public static TInterface GetAsyncClientInstance<TInterface>() where TInterface : class, IService
		{
			bool flag = true;
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["cClockWorkServerEnabled"];
			bool flag2 = obj == null || (bool)obj;
			bool flag3 = !flag2;
			if (flag3)
			{
				flag = false;
			}
			return flag ? WCFClientProxy<TInterface>.GetAsyncInstance() : default(TInterface);
		}
	}
}
