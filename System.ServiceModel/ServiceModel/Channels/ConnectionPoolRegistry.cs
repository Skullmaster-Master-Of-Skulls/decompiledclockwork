using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E2 RID: 2018
	internal abstract class ConnectionPoolRegistry
	{
		// Token: 0x06004C61 RID: 19553 RVA: 0x00116D08 File Offset: 0x00114F08
		protected ConnectionPoolRegistry()
		{
			this.registry = new Dictionary<string, List<ConnectionPool>>();
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004C62 RID: 19554 RVA: 0x00116D1B File Offset: 0x00114F1B
		private object ThisLock
		{
			get
			{
				return this.registry;
			}
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x00116D24 File Offset: 0x00114F24
		public ConnectionPool Lookup(IConnectionOrientedTransportChannelFactorySettings settings)
		{
			ConnectionPool connectionPool = null;
			string connectionPoolGroupName = settings.ConnectionPoolGroupName;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				List<ConnectionPool> list = null;
				if (this.registry.TryGetValue(connectionPoolGroupName, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].IsCompatible(settings) && list[i].TryOpen())
						{
							connectionPool = list[i];
							break;
						}
					}
				}
				else
				{
					list = new List<ConnectionPool>();
					this.registry.Add(connectionPoolGroupName, list);
				}
				if (connectionPool == null)
				{
					connectionPool = this.CreatePool(settings);
					list.Add(connectionPool);
				}
			}
			return connectionPool;
		}

		// Token: 0x06004C64 RID: 19556
		protected abstract ConnectionPool CreatePool(IConnectionOrientedTransportChannelFactorySettings settings);

		// Token: 0x06004C65 RID: 19557 RVA: 0x00116DE8 File Offset: 0x00114FE8
		public void Release(ConnectionPool pool, TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (pool.Close(timeout))
				{
					List<ConnectionPool> list = this.registry[pool.Name];
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] == pool)
						{
							list.RemoveAt(i);
							break;
						}
					}
					if (list.Count == 0)
					{
						this.registry.Remove(pool.Name);
					}
				}
			}
		}

		// Token: 0x04002FAF RID: 12207
		private Dictionary<string, List<ConnectionPool>> registry;
	}
}
