using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.Services.Pool
{
	// Token: 0x02000173 RID: 371
	public class ProxyConnectionPool : IDisposable
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x000254D8 File Offset: 0x000236D8
		private ProxyConnectionPool()
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000254F8 File Offset: 0x000236F8
		public static void Register<T>(T connection)
		{
			bool enableConnectionPool = ProxyConnectionPool.EnableConnectionPool;
			if (enableConnectionPool)
			{
				bool flag = ProxyConnectionPool.ExcludeTypeList.Contains(typeof(T));
				if (!flag)
				{
					IClientBase clientBase = connection as IClientBase;
					bool flag2 = clientBase == null;
					if (flag2)
					{
						throw new InvalidOperationException("Connection is not of type IClientBase. Are you sure this connection was created by this dynamic proxy?");
					}
					clientBase.ProxyCreated += ProxyConnectionPool.clientBase_ProxyCreated;
					ProxyConnectionPool.Current.InternalRegister(clientBase);
					ProxyConnectionPool.Current._openProxiesByType[typeof(T)] = clientBase;
				}
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00025588 File Offset: 0x00023788
		internal static T RequestFromPool<T>() where T : class
		{
			bool enableConnectionPool = ProxyConnectionPool.EnableConnectionPool;
			if (enableConnectionPool)
			{
				IClientBase clientBase;
				bool flag = ProxyConnectionPool.Current._openProxiesByType.TryGetValue(typeof(T), out clientBase);
				if (flag)
				{
					return (T)((object)clientBase);
				}
			}
			return default(T);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000255D8 File Offset: 0x000237D8
		private static void clientBase_ProxyCreated(IClientBase proxy)
		{
			ProxyConnectionPool.Current.InternalRegister(proxy);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000255E7 File Offset: 0x000237E7
		private void InternalRegister(IClientBase clientBase)
		{
			this._openProxies[clientBase] = clientBase;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000255F8 File Offset: 0x000237F8
		public void DisposeAllConnections()
		{
			List<IClientBase> list = new List<IClientBase>(this._openProxies.Values);
			this._openProxies.Clear();
			this._openProxiesByType.Clear();
			foreach (IClientBase clientBase in list)
			{
				bool flag = false;
				Exception ex = null;
				try
				{
					clientBase.Close();
					flag = true;
				}
				catch (ChannelTerminatedException ex2)
				{
					CWLogger.Logger.Error("ProxyConnectionPool:DisposeAllConnections:Reason={0}:Error={1}", "Typically thrown on the client when a channel is terminated due to the server closing the connection.", ex2.ToString());
					ex = ex2;
				}
				catch (EndpointNotFoundException ex3)
				{
					CWLogger.Logger.Error("ProxyConnectionPool:DisposeAllConnections:Reason={0}:Error={1}", "A remote endpoint could not be found or reached.  The endpoint may not be found or reachable because the remote endpoint is down, the remote endpoint is unreachable, or because the remote network is unreachable.", ex3.ToString());
					ex = ex3;
				}
				catch (ServerTooBusyException ex4)
				{
					CWLogger.Logger.Error("ProxyConnectionPool:DisposeAllConnections:Reason={0}:Error={1}", "Server is too busy to accept a message.", ex4.ToString());
					ex = ex4;
				}
				catch (Exception ex5)
				{
					CWLogger.Logger.Error("ProxyConnectionPool:DisposeAllConnections:Reason={0}:Error={1}", "General exception.", ex5.ToString());
					ex = ex5;
				}
				bool flag2 = ex != null || !flag;
				if (flag2)
				{
					try
					{
						clientBase.Abort();
					}
					catch
					{
					}
				}
			}
			list.Clear();
			bool flag3 = ProxyConnectionPool._proxyConnectionPool == this;
			if (flag3)
			{
				ProxyConnectionPool._proxyConnectionPool = null;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0002579C File Offset: 0x0002399C
		public static ProxyConnectionPool Current
		{
			get
			{
				bool flag = HttpContext.Current != null;
				ProxyConnectionPool result;
				if (flag)
				{
					ProxyConnectionPool proxyConnectionPool = HttpContext.Current.Items["TechnoPro.ClockWorkServer.ProxyConnectionPool"] as ProxyConnectionPool;
					bool flag2 = proxyConnectionPool == null;
					if (flag2)
					{
						proxyConnectionPool = new ProxyConnectionPool();
						HttpContext.Current.Items["TechnoPro.ClockWorkServer.ProxyConnectionPool"] = proxyConnectionPool;
					}
					result = proxyConnectionPool;
				}
				else
				{
					ProxyConnectionPool.ProxyConnectionPoolExtension proxyConnectionPoolExtension = ProxyConnectionPool.ProxyConnectionPoolExtension.Current;
					bool flag3 = proxyConnectionPoolExtension != null;
					if (flag3)
					{
						result = proxyConnectionPoolExtension.Pool;
					}
					else
					{
						bool flag4 = ProxyConnectionPool._proxyConnectionPool == null;
						if (flag4)
						{
							ProxyConnectionPool._proxyConnectionPool = new ProxyConnectionPool();
						}
						result = ProxyConnectionPool._proxyConnectionPool;
					}
				}
				return result;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0002583C File Offset: 0x00023A3C
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x00025843 File Offset: 0x00023A43
		public static bool EnableConnectionPool { get; set; }

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002584B File Offset: 0x00023A4B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00025860 File Offset: 0x00023A60
		~ProxyConnectionPool()
		{
			this.Dispose(false);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00025894 File Offset: 0x00023A94
		private void Dispose(bool disposing)
		{
			bool flag = disposing && ProxyConnectionPool.EnableConnectionPool;
			if (flag)
			{
				this.DisposeAllConnections();
			}
		}

		// Token: 0x04000028 RID: 40
		private const string HttpContextPoolKey = "TechnoPro.ClockWorkServer.ProxyConnectionPool";

		// Token: 0x04000029 RID: 41
		private readonly Dictionary<IClientBase, IClientBase> _openProxies = new Dictionary<IClientBase, IClientBase>();

		// Token: 0x0400002A RID: 42
		private readonly Dictionary<Type, IClientBase> _openProxiesByType = new Dictionary<Type, IClientBase>();

		// Token: 0x0400002B RID: 43
		private static readonly IList<Type> ExcludeTypeList = new List<Type>
		{
			typeof(IMessaging),
			typeof(IMembership)
		};

		// Token: 0x0400002C RID: 44
		private static ProxyConnectionPool _proxyConnectionPool;

		// Token: 0x02000742 RID: 1858
		private class ProxyConnectionPoolExtension : IExtension<OperationContext>
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000351E0 File Offset: 0x000333E0
			public static ProxyConnectionPool.ProxyConnectionPoolExtension Current
			{
				get
				{
					bool flag = OperationContext.Current == null;
					ProxyConnectionPool.ProxyConnectionPoolExtension result;
					if (flag)
					{
						result = null;
					}
					else
					{
						ProxyConnectionPool.ProxyConnectionPoolExtension proxyConnectionPoolExtension = OperationContext.Current.Extensions.Find<ProxyConnectionPool.ProxyConnectionPoolExtension>();
						bool flag2 = proxyConnectionPoolExtension == null;
						if (flag2)
						{
							proxyConnectionPoolExtension = new ProxyConnectionPool.ProxyConnectionPoolExtension();
							OperationContext.Current.Extensions.Add(proxyConnectionPoolExtension);
						}
						result = proxyConnectionPoolExtension;
					}
					return result;
				}
			}

			// Token: 0x06001A89 RID: 6793 RVA: 0x00025908 File Offset: 0x00023B08
			public void Attach(OperationContext owner)
			{
			}

			// Token: 0x06001A8A RID: 6794 RVA: 0x00025908 File Offset: 0x00023B08
			public void Detach(OperationContext owner)
			{
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00035234 File Offset: 0x00033434
			public ProxyConnectionPool Pool
			{
				get
				{
					return this._pool;
				}
			}

			// Token: 0x04000DA5 RID: 3493
			private readonly ProxyConnectionPool _pool = new ProxyConnectionPool();
		}
	}
}
