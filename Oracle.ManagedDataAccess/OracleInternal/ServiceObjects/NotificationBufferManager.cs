using System;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.Network;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A6 RID: 422
	internal class NotificationBufferManager : PoolManager<OraclePoolManager, OraclePool, OracleConnectionImpl>
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x000A3984 File Offset: 0x000A1B84
		internal NotificationBufferManager()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				base.Initialize(new ConnectionString(string.Empty));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000A3A18 File Offset: 0x000A1C18
		public void GetNotificationOraBufPool(OracleCommunication orclCommunication)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				SyncQueueList<ConOraBufPool> syncQueueList = (SyncQueueList<ConOraBufPool>)this.m_htOfOraBufPools[orclCommunication.SDU];
				ConOraBufPool conOraBufPool;
				if (syncQueueList == null || !syncQueueList.Dequeue(out conOraBufPool))
				{
					int num = this.m_cs.m_maxPoolSize;
					if (num > 200)
					{
						num = 200;
					}
					OraBufPool obp = new OraBufPool(num * NotificationBufferManager.s_maxListCapacity);
					conOraBufPool = new ConOraBufPool(obp);
					conOraBufPool.Init(orclCommunication);
				}
				orclCommunication.OraBufPool = conOraBufPool;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000A3AF0 File Offset: 0x000A1CF0
		public void PutNotificationOraBufPool(OracleCommunication orclCommunication)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				ConOraBufPool oraBufPool = orclCommunication.OraBufPool;
				orclCommunication.OraBufPool.Init(orclCommunication);
				SyncQueueList<ConOraBufPool> syncQueueList = (SyncQueueList<ConOraBufPool>)this.m_htOfOraBufPools[orclCommunication.SDU];
				if (syncQueueList == null)
				{
					syncQueueList = new SyncQueueList<ConOraBufPool>(int.MaxValue);
					this.m_htOfOraBufPools.Add(orclCommunication.SDU, syncQueueList);
				}
				if (syncQueueList.Count < NotificationBufferManager.s_maxOraBufPoolsInBuffer)
				{
					syncQueueList.Enqueue(orclCommunication.OraBufPool);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x04001275 RID: 4725
		private static int s_maxListCapacity = 10;

		// Token: 0x04001276 RID: 4726
		private static int s_maxOraBufPoolsInBuffer = 5;

		// Token: 0x04001277 RID: 4727
		public Hashtable m_htOfOraBufPools = Hashtable.Synchronized(new Hashtable());
	}
}
