using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007DF RID: 2015
	internal abstract class CommunicationPool<TKey, TItem> where TKey : class where TItem : class
	{
		// Token: 0x06004C3B RID: 19515 RVA: 0x00116435 File Offset: 0x00114635
		protected CommunicationPool(int maxCount)
		{
			this.maxCount = maxCount;
			this.endpointPools = new Dictionary<TKey, CommunicationPool<TKey, TItem>.EndpointConnectionPool>();
			this.openCount = 1;
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x00116456 File Offset: 0x00114656
		public int MaxIdleConnectionPoolCount
		{
			get
			{
				return this.maxCount;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x0011645E File Offset: 0x0011465E
		protected object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004C3E RID: 19518
		protected abstract void AbortItem(TItem item);

		// Token: 0x06004C3F RID: 19519
		protected abstract void CloseItem(TItem item, TimeSpan timeout);

		// Token: 0x06004C40 RID: 19520
		protected abstract void CloseItemAsync(TItem item, TimeSpan timeout);

		// Token: 0x06004C41 RID: 19521
		protected abstract TKey GetPoolKey(EndpointAddress address, Uri via);

		// Token: 0x06004C42 RID: 19522 RVA: 0x00116461 File Offset: 0x00114661
		protected virtual CommunicationPool<TKey, TItem>.EndpointConnectionPool CreateEndpointConnectionPool(TKey key)
		{
			return new CommunicationPool<TKey, TItem>.EndpointConnectionPool(this, key);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0011646C File Offset: 0x0011466C
		public bool Close(TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.openCount <= 0)
				{
					result = true;
				}
				else
				{
					this.openCount--;
					if (this.openCount == 0)
					{
						this.OnClose(timeout);
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x001164D8 File Offset: 0x001146D8
		private List<TItem> PruneIfNecessary()
		{
			List<TItem> list = null;
			this.pruneAccrual++;
			if (this.pruneAccrual > 30)
			{
				this.pruneAccrual = 0;
				list = new List<TItem>();
				foreach (CommunicationPool<TKey, TItem>.EndpointConnectionPool endpointConnectionPool in this.endpointPools.Values)
				{
					endpointConnectionPool.Prune(list);
				}
				List<TKey> list2 = null;
				foreach (KeyValuePair<TKey, CommunicationPool<TKey, TItem>.EndpointConnectionPool> keyValuePair in this.endpointPools)
				{
					if (keyValuePair.Value.CloseIfEmpty())
					{
						if (list2 == null)
						{
							list2 = new List<TKey>();
						}
						list2.Add(keyValuePair.Key);
					}
				}
				if (list2 != null)
				{
					for (int i = 0; i < list2.Count; i++)
					{
						this.endpointPools.Remove(list2[i]);
					}
				}
			}
			return list;
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x001165EC File Offset: 0x001147EC
		private CommunicationPool<TKey, TItem>.EndpointConnectionPool GetEndpointPool(TKey key, TimeSpan timeout)
		{
			CommunicationPool<TKey, TItem>.EndpointConnectionPool endpointConnectionPool = null;
			List<TItem> list = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.endpointPools.TryGetValue(key, out endpointConnectionPool))
				{
					list = this.PruneIfNecessary();
					endpointConnectionPool = this.CreateEndpointConnectionPool(key);
					this.endpointPools.Add(key, endpointConnectionPool);
				}
			}
			if (list != null && list.Count > 0)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(TimeoutHelper.Divide(timeout, 2));
				for (int i = 0; i < list.Count; i++)
				{
					endpointConnectionPool.CloseIdleConnection(list[i], timeoutHelper.RemainingTime());
				}
			}
			return endpointConnectionPool;
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0011669C File Offset: 0x0011489C
		public bool TryOpen()
		{
			object thisLock = this.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.openCount <= 0)
				{
					result = false;
				}
				else
				{
					this.openCount++;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x001166F4 File Offset: 0x001148F4
		protected virtual void OnClosed()
		{
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x001166F8 File Offset: 0x001148F8
		private void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			foreach (CommunicationPool<TKey, TItem>.EndpointConnectionPool endpointConnectionPool in this.endpointPools.Values)
			{
				try
				{
					endpointConnectionPool.Close(timeoutHelper.RemainingTime());
				}
				catch (CommunicationException exception)
				{
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 262146, SR.GetString("TraceCodeConnectionPoolCloseException"), this, exception);
					}
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 262146, SR.GetString("TraceCodeConnectionPoolCloseException"), this, ex);
					}
				}
			}
			this.endpointPools.Clear();
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x001167DC File Offset: 0x001149DC
		public void AddConnection(TKey key, TItem connection, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			CommunicationPool<TKey, TItem>.EndpointConnectionPool endpointPool = this.GetEndpointPool(key, timeoutHelper.RemainingTime());
			endpointPool.AddConnection(connection, timeoutHelper.RemainingTime());
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x00116810 File Offset: 0x00114A10
		public TItem TakeConnection(EndpointAddress address, Uri via, TimeSpan timeout, out TKey key)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			key = this.GetPoolKey(address, via);
			CommunicationPool<TKey, TItem>.EndpointConnectionPool endpointPool = this.GetEndpointPool(key, timeoutHelper.RemainingTime());
			return endpointPool.TakeConnection(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x00116858 File Offset: 0x00114A58
		public void ReturnConnection(TKey key, TItem connection, bool connectionIsStillGood, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			CommunicationPool<TKey, TItem>.EndpointConnectionPool endpointPool = this.GetEndpointPool(key, timeoutHelper.RemainingTime());
			endpointPool.ReturnConnection(connection, connectionIsStillGood, timeoutHelper.RemainingTime());
		}

		// Token: 0x04002F9E RID: 12190
		private Dictionary<TKey, CommunicationPool<TKey, TItem>.EndpointConnectionPool> endpointPools;

		// Token: 0x04002F9F RID: 12191
		private int maxCount;

		// Token: 0x04002FA0 RID: 12192
		private int openCount;

		// Token: 0x04002FA1 RID: 12193
		private int pruneAccrual;

		// Token: 0x04002FA2 RID: 12194
		private const int pruneThreshold = 30;

		// Token: 0x02000D04 RID: 3332
		protected abstract class IdleConnectionPool
		{
			// Token: 0x17001BC2 RID: 7106
			// (get) Token: 0x06007AD7 RID: 31447
			public abstract int Count { get; }

			// Token: 0x06007AD8 RID: 31448
			public abstract bool Add(TItem item);

			// Token: 0x06007AD9 RID: 31449
			public abstract bool Return(TItem item);

			// Token: 0x06007ADA RID: 31450
			public abstract TItem Take(out bool closeItem);
		}

		// Token: 0x02000D05 RID: 3333
		protected class EndpointConnectionPool
		{
			// Token: 0x06007ADC RID: 31452 RVA: 0x001C9776 File Offset: 0x001C7976
			public EndpointConnectionPool(CommunicationPool<TKey, TItem> parent, TKey key)
			{
				this.key = key;
				this.parent = parent;
				this.busyConnections = new List<TItem>();
			}

			// Token: 0x17001BC3 RID: 7107
			// (get) Token: 0x06007ADD RID: 31453 RVA: 0x001C9797 File Offset: 0x001C7997
			protected TKey Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17001BC4 RID: 7108
			// (get) Token: 0x06007ADE RID: 31454 RVA: 0x001C979F File Offset: 0x001C799F
			private CommunicationPool<TKey, TItem>.IdleConnectionPool IdleConnections
			{
				get
				{
					if (this.idleConnections == null)
					{
						this.idleConnections = this.GetIdleConnectionPool();
					}
					return this.idleConnections;
				}
			}

			// Token: 0x17001BC5 RID: 7109
			// (get) Token: 0x06007ADF RID: 31455 RVA: 0x001C97BB File Offset: 0x001C79BB
			protected CommunicationPool<TKey, TItem> Parent
			{
				get
				{
					return this.parent;
				}
			}

			// Token: 0x17001BC6 RID: 7110
			// (get) Token: 0x06007AE0 RID: 31456 RVA: 0x001C97C3 File Offset: 0x001C79C3
			protected object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06007AE1 RID: 31457 RVA: 0x001C97C8 File Offset: 0x001C79C8
			public bool CloseIfEmpty()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.closed)
					{
						if (this.busyConnections.Count > 0)
						{
							return false;
						}
						if (this.idleConnections != null && this.idleConnections.Count > 0)
						{
							return false;
						}
						this.closed = true;
					}
				}
				return true;
			}

			// Token: 0x06007AE2 RID: 31458 RVA: 0x001C9840 File Offset: 0x001C7A40
			protected virtual void AbortItem(TItem item)
			{
				this.parent.AbortItem(item);
			}

			// Token: 0x06007AE3 RID: 31459 RVA: 0x001C984E File Offset: 0x001C7A4E
			protected virtual void CloseItem(TItem item, TimeSpan timeout)
			{
				this.parent.CloseItem(item, timeout);
			}

			// Token: 0x06007AE4 RID: 31460 RVA: 0x001C985D File Offset: 0x001C7A5D
			protected virtual void CloseItemAsync(TItem item, TimeSpan timeout)
			{
				this.parent.CloseItemAsync(item, timeout);
			}

			// Token: 0x06007AE5 RID: 31461 RVA: 0x001C986C File Offset: 0x001C7A6C
			public void Abort()
			{
				if (this.closed)
				{
					return;
				}
				List<TItem> idleItemsToClose = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.closed)
					{
						return;
					}
					this.closed = true;
					idleItemsToClose = this.SnapshotIdleConnections();
				}
				this.AbortConnections(idleItemsToClose);
			}

			// Token: 0x06007AE6 RID: 31462 RVA: 0x001C98D0 File Offset: 0x001C7AD0
			public void Close(TimeSpan timeout)
			{
				List<TItem> list = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.closed)
					{
						return;
					}
					this.closed = true;
					list = this.SnapshotIdleConnections();
				}
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					for (int i = 0; i < list.Count; i++)
					{
						this.CloseItem(list[i], timeoutHelper.RemainingTime());
					}
					list.Clear();
				}
				finally
				{
					this.AbortConnections(list);
				}
			}

			// Token: 0x06007AE7 RID: 31463 RVA: 0x001C9974 File Offset: 0x001C7B74
			private void AbortConnections(List<TItem> idleItemsToClose)
			{
				for (int i = 0; i < idleItemsToClose.Count; i++)
				{
					this.AbortItem(idleItemsToClose[i]);
				}
				for (int j = 0; j < this.busyConnections.Count; j++)
				{
					this.AbortItem(this.busyConnections[j]);
				}
				this.busyConnections.Clear();
			}

			// Token: 0x06007AE8 RID: 31464 RVA: 0x001C99D4 File Offset: 0x001C7BD4
			private List<TItem> SnapshotIdleConnections()
			{
				List<TItem> list = new List<TItem>();
				for (;;)
				{
					bool flag;
					TItem titem = this.IdleConnections.Take(out flag);
					if (titem == null)
					{
						break;
					}
					list.Add(titem);
				}
				return list;
			}

			// Token: 0x06007AE9 RID: 31465 RVA: 0x001C9A08 File Offset: 0x001C7C08
			public void AddConnection(TItem connection, TimeSpan timeout)
			{
				bool flag = false;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.closed)
					{
						if (!this.IdleConnections.Add(connection))
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.CloseIdleConnection(connection, timeout);
				}
			}

			// Token: 0x06007AEA RID: 31466 RVA: 0x001C9A6C File Offset: 0x001C7C6C
			protected virtual CommunicationPool<TKey, TItem>.IdleConnectionPool GetIdleConnectionPool()
			{
				return new CommunicationPool<TKey, TItem>.EndpointConnectionPool.PoolIdleConnectionPool(this.parent.MaxIdleConnectionPoolCount);
			}

			// Token: 0x06007AEB RID: 31467 RVA: 0x001C9A7E File Offset: 0x001C7C7E
			public virtual void Prune(List<TItem> itemsToClose)
			{
			}

			// Token: 0x06007AEC RID: 31468 RVA: 0x001C9A80 File Offset: 0x001C7C80
			public TItem TakeConnection(TimeSpan timeout)
			{
				TItem titem = default(TItem);
				List<TItem> list = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.closed)
					{
						return default(TItem);
					}
					for (;;)
					{
						bool flag2;
						titem = this.IdleConnections.Take(out flag2);
						if (titem == null)
						{
							break;
						}
						if (!flag2)
						{
							goto Block_10;
						}
						if (list == null)
						{
							list = new List<TItem>();
						}
						list.Add(titem);
					}
					goto IL_7A;
					Block_10:
					this.busyConnections.Add(titem);
				}
				IL_7A:
				if (list != null)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(TimeoutHelper.Divide(timeout, 2));
					for (int i = 0; i < list.Count; i++)
					{
						this.CloseIdleConnection(list[i], timeoutHelper.RemainingTime());
					}
				}
				if (TD.ConnectionPoolMissIsEnabled() && titem == null && this.busyConnections != null)
				{
					TD.ConnectionPoolMiss((this.key != null) ? this.key.ToString() : string.Empty, this.busyConnections.Count);
				}
				return titem;
			}

			// Token: 0x06007AED RID: 31469 RVA: 0x001C9BA4 File Offset: 0x001C7DA4
			public void ReturnConnection(TItem connection, bool connectionIsStillGood, TimeSpan timeout)
			{
				bool flag = false;
				bool flag2 = false;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.closed)
					{
						if (this.busyConnections.Remove(connection) && connectionIsStillGood)
						{
							if (!this.IdleConnections.Return(connection))
							{
								flag = true;
							}
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				if (flag)
				{
					this.CloseIdleConnection(connection, timeout);
					return;
				}
				if (flag2)
				{
					this.AbortItem(connection);
					this.OnConnectionAborted();
				}
			}

			// Token: 0x06007AEE RID: 31470 RVA: 0x001C9C30 File Offset: 0x001C7E30
			public void CloseIdleConnection(TItem connection, TimeSpan timeout)
			{
				bool flag = true;
				try
				{
					this.CloseItemAsync(connection, timeout);
					flag = false;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				finally
				{
					if (flag)
					{
						this.AbortItem(connection);
					}
				}
			}

			// Token: 0x06007AEF RID: 31471 RVA: 0x001C9C88 File Offset: 0x001C7E88
			protected virtual void OnConnectionAborted()
			{
			}

			// Token: 0x0400463C RID: 17980
			private TKey key;

			// Token: 0x0400463D RID: 17981
			private List<TItem> busyConnections;

			// Token: 0x0400463E RID: 17982
			private bool closed;

			// Token: 0x0400463F RID: 17983
			private CommunicationPool<TKey, TItem>.IdleConnectionPool idleConnections;

			// Token: 0x04004640 RID: 17984
			private CommunicationPool<TKey, TItem> parent;

			// Token: 0x02000F43 RID: 3907
			protected class PoolIdleConnectionPool : CommunicationPool<TKey, TItem>.IdleConnectionPool
			{
				// Token: 0x060086BB RID: 34491 RVA: 0x001F310B File Offset: 0x001F130B
				public PoolIdleConnectionPool(int maxCount)
				{
					this.idleConnections = new Pool<TItem>(maxCount);
					this.maxCount = maxCount;
				}

				// Token: 0x17001D8E RID: 7566
				// (get) Token: 0x060086BC RID: 34492 RVA: 0x001F3126 File Offset: 0x001F1326
				public override int Count
				{
					get
					{
						return this.idleConnections.Count;
					}
				}

				// Token: 0x060086BD RID: 34493 RVA: 0x001F3133 File Offset: 0x001F1333
				public override bool Add(TItem connection)
				{
					return this.ReturnToPool(connection);
				}

				// Token: 0x060086BE RID: 34494 RVA: 0x001F313C File Offset: 0x001F133C
				public override bool Return(TItem connection)
				{
					return this.ReturnToPool(connection);
				}

				// Token: 0x060086BF RID: 34495 RVA: 0x001F3148 File Offset: 0x001F1348
				private bool ReturnToPool(TItem connection)
				{
					bool flag = this.idleConnections.Return(connection);
					if (!flag)
					{
						if (TD.MaxOutboundConnectionsPerEndpointExceededIsEnabled())
						{
							TD.MaxOutboundConnectionsPerEndpointExceeded(SR.GetString("TraceCodeConnectionPoolMaxOutboundConnectionsPerEndpointQuotaReached", new object[]
							{
								this.maxCount
							}));
						}
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 262149, SR.GetString("TraceCodeConnectionPoolMaxOutboundConnectionsPerEndpointQuotaReached", new object[]
							{
								this.maxCount
							}), this);
						}
					}
					else if (TD.OutboundConnectionsPerEndpointRatioIsEnabled())
					{
						TD.OutboundConnectionsPerEndpointRatio(this.idleConnections.Count, this.maxCount);
					}
					return flag;
				}

				// Token: 0x060086C0 RID: 34496 RVA: 0x001F31E0 File Offset: 0x001F13E0
				public override TItem Take(out bool closeItem)
				{
					closeItem = false;
					TItem result = this.idleConnections.Take();
					if (TD.OutboundConnectionsPerEndpointRatioIsEnabled())
					{
						TD.OutboundConnectionsPerEndpointRatio(this.idleConnections.Count, this.maxCount);
					}
					return result;
				}

				// Token: 0x04004E49 RID: 20041
				private Pool<TItem> idleConnections;

				// Token: 0x04004E4A RID: 20042
				private int maxCount;
			}
		}
	}
}
