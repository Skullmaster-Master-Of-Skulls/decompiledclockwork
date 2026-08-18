using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200080E RID: 2062
	internal abstract class IdlingCommunicationPool<TKey, TItem> : CommunicationPool<TKey, TItem> where TKey : class where TItem : class
	{
		// Token: 0x06004D2B RID: 19755 RVA: 0x00119EB9 File Offset: 0x001180B9
		protected IdlingCommunicationPool(int maxCount, TimeSpan idleTimeout, TimeSpan leaseTimeout) : base(maxCount)
		{
			this.idleTimeout = idleTimeout;
			this.leaseTimeout = leaseTimeout;
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x00119ED0 File Offset: 0x001180D0
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x00119ED8 File Offset: 0x001180D8
		protected TimeSpan LeaseTimeout
		{
			get
			{
				return this.leaseTimeout;
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x00119EE0 File Offset: 0x001180E0
		protected override void CloseItemAsync(TItem item, TimeSpan timeout)
		{
			this.CloseItem(item, timeout);
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x00119EEA File Offset: 0x001180EA
		protected override CommunicationPool<TKey, TItem>.EndpointConnectionPool CreateEndpointConnectionPool(TKey key)
		{
			if (this.idleTimeout != TimeSpan.MaxValue || this.leaseTimeout != TimeSpan.MaxValue)
			{
				return new IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool(this, key);
			}
			return base.CreateEndpointConnectionPool(key);
		}

		// Token: 0x0400304A RID: 12362
		private TimeSpan idleTimeout;

		// Token: 0x0400304B RID: 12363
		private TimeSpan leaseTimeout;

		// Token: 0x02000D14 RID: 3348
		protected class IdleTimeoutEndpointConnectionPool : CommunicationPool<TKey, TItem>.EndpointConnectionPool
		{
			// Token: 0x06007B31 RID: 31537 RVA: 0x001CB2CB File Offset: 0x001C94CB
			public IdleTimeoutEndpointConnectionPool(IdlingCommunicationPool<TKey, TItem> parent, TKey key) : base(parent, key)
			{
				this.connections = new IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool(this, base.ThisLock);
			}

			// Token: 0x06007B32 RID: 31538 RVA: 0x001CB2E7 File Offset: 0x001C94E7
			protected override CommunicationPool<TKey, TItem>.IdleConnectionPool GetIdleConnectionPool()
			{
				return this.connections;
			}

			// Token: 0x06007B33 RID: 31539 RVA: 0x001CB2EF File Offset: 0x001C94EF
			protected override void AbortItem(TItem item)
			{
				this.connections.OnItemClosing(item);
				base.AbortItem(item);
			}

			// Token: 0x06007B34 RID: 31540 RVA: 0x001CB304 File Offset: 0x001C9504
			protected override void CloseItemAsync(TItem item, TimeSpan timeout)
			{
				this.connections.OnItemClosing(item);
				base.CloseItemAsync(item, timeout);
			}

			// Token: 0x06007B35 RID: 31541 RVA: 0x001CB31A File Offset: 0x001C951A
			protected override void CloseItem(TItem item, TimeSpan timeout)
			{
				this.connections.OnItemClosing(item);
				base.CloseItem(item, timeout);
			}

			// Token: 0x06007B36 RID: 31542 RVA: 0x001CB330 File Offset: 0x001C9530
			public override void Prune(List<TItem> itemsToClose)
			{
				if (this.connections != null)
				{
					this.connections.Prune(itemsToClose, false);
				}
			}

			// Token: 0x040046B3 RID: 18099
			private IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool connections;

			// Token: 0x02000F45 RID: 3909
			protected class IdleTimeoutIdleConnectionPool : CommunicationPool<TKey, TItem>.EndpointConnectionPool.PoolIdleConnectionPool
			{
				// Token: 0x060086C3 RID: 34499 RVA: 0x001F3288 File Offset: 0x001F1488
				public IdleTimeoutIdleConnectionPool(IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool parent, object thisLock) : base(parent.Parent.MaxIdleConnectionPoolCount)
				{
					this.parent = parent;
					IdlingCommunicationPool<TKey, TItem> idlingCommunicationPool = (IdlingCommunicationPool<TKey, TItem>)parent.Parent;
					this.idleTimeout = idlingCommunicationPool.idleTimeout;
					this.leaseTimeout = idlingCommunicationPool.leaseTimeout;
					this.thisLock = thisLock;
					this.connectionMapping = new Dictionary<TItem, IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.IdlingConnectionSettings>();
				}

				// Token: 0x060086C4 RID: 34500 RVA: 0x001F32E4 File Offset: 0x001F14E4
				public override bool Add(TItem connection)
				{
					this.ThrowPendingException();
					bool flag = base.Add(connection);
					if (flag)
					{
						this.connectionMapping.Add(connection, new IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.IdlingConnectionSettings());
						this.StartTimerIfNecessary();
					}
					return flag;
				}

				// Token: 0x060086C5 RID: 34501 RVA: 0x001F331C File Offset: 0x001F151C
				public override bool Return(TItem connection)
				{
					this.ThrowPendingException();
					if (!this.connectionMapping.ContainsKey(connection))
					{
						return false;
					}
					bool flag = base.Return(connection);
					if (flag)
					{
						this.connectionMapping[connection].LastUsage = DateTime.UtcNow;
						this.StartTimerIfNecessary();
					}
					return flag;
				}

				// Token: 0x060086C6 RID: 34502 RVA: 0x001F3368 File Offset: 0x001F1568
				public override TItem Take(out bool closeItem)
				{
					this.ThrowPendingException();
					DateTime utcNow = DateTime.UtcNow;
					TItem titem = base.Take(out closeItem);
					if (!closeItem)
					{
						closeItem = this.IdleOutConnection(titem, utcNow);
					}
					return titem;
				}

				// Token: 0x060086C7 RID: 34503 RVA: 0x001F3398 File Offset: 0x001F1598
				public void OnItemClosing(TItem connection)
				{
					this.ThrowPendingException();
					object obj = this.thisLock;
					lock (obj)
					{
						this.connectionMapping.Remove(connection);
					}
				}

				// Token: 0x060086C8 RID: 34504 RVA: 0x001F33E8 File Offset: 0x001F15E8
				private void CancelTimer()
				{
					if (this.idleTimer != null)
					{
						this.idleTimer.Cancel();
					}
				}

				// Token: 0x060086C9 RID: 34505 RVA: 0x001F3400 File Offset: 0x001F1600
				private void StartTimerIfNecessary()
				{
					if (this.Count > 1)
					{
						if (this.idleTimer == null)
						{
							if (IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.onIdle == null)
							{
								IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.onIdle = new Action<object>(IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.OnIdle);
							}
							this.idleTimer = new IOThreadTimer(IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.onIdle, this, false);
						}
						this.idleTimer.Set(this.idleTimeout);
					}
				}

				// Token: 0x060086CA RID: 34506 RVA: 0x001F345C File Offset: 0x001F165C
				private static void OnIdle(object state)
				{
					IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool idleTimeoutIdleConnectionPool = (IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool)state;
					idleTimeoutIdleConnectionPool.OnIdle();
				}

				// Token: 0x060086CB RID: 34507 RVA: 0x001F3478 File Offset: 0x001F1678
				private void OnIdle()
				{
					List<TItem> list = new List<TItem>();
					object obj = this.thisLock;
					lock (obj)
					{
						try
						{
							this.Prune(list, true);
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
							this.pendingException = exception;
							this.CancelTimer();
						}
					}
					TimeoutHelper timeoutHelper = new TimeoutHelper(TimeoutHelper.Divide(this.idleTimeout, 2));
					for (int i = 0; i < list.Count; i++)
					{
						this.parent.CloseIdleConnection(list[i], timeoutHelper.RemainingTime());
					}
				}

				// Token: 0x060086CC RID: 34508 RVA: 0x001F352C File Offset: 0x001F172C
				public void Prune(List<TItem> itemsToClose, bool calledFromTimer)
				{
					if (!calledFromTimer)
					{
						this.ThrowPendingException();
					}
					if (this.Count == 0)
					{
						return;
					}
					DateTime utcNow = DateTime.UtcNow;
					bool flag = false;
					object obj = this.thisLock;
					lock (obj)
					{
						TItem[] array = new TItem[this.Count];
						for (int i = 0; i < array.Length; i++)
						{
							bool flag3;
							array[i] = base.Take(out flag3);
							if (flag3 || this.IdleOutConnection(array[i], utcNow))
							{
								itemsToClose.Add(array[i]);
								array[i] = default(TItem);
							}
						}
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j] != null)
							{
								bool flag4 = base.Return(array[j]);
							}
						}
						flag = (this.Count > 0);
					}
					if (calledFromTimer && flag)
					{
						this.idleTimer.Set(this.idleTimeout);
					}
				}

				// Token: 0x060086CD RID: 34509 RVA: 0x001F3640 File Offset: 0x001F1840
				private bool IdleOutConnection(TItem connection, DateTime now)
				{
					if (connection == null)
					{
						return false;
					}
					bool result = false;
					IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.IdlingConnectionSettings idlingConnectionSettings = this.connectionMapping[connection];
					if (now > idlingConnectionSettings.LastUsage + this.idleTimeout)
					{
						this.TraceConnectionIdleTimeoutExpired();
						result = true;
					}
					else if (now - idlingConnectionSettings.CreationTime >= this.leaseTimeout)
					{
						this.TraceConnectionLeaseTimeoutExpired();
						result = true;
					}
					return result;
				}

				// Token: 0x060086CE RID: 34510 RVA: 0x001F36AC File Offset: 0x001F18AC
				private void ThrowPendingException()
				{
					if (this.pendingException != null)
					{
						object obj = this.thisLock;
						lock (obj)
						{
							if (this.pendingException != null)
							{
								Exception exception = this.pendingException;
								this.pendingException = null;
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
							}
						}
					}
				}

				// Token: 0x060086CF RID: 34511 RVA: 0x001F3710 File Offset: 0x001F1910
				private void TraceConnectionLeaseTimeoutExpired()
				{
					if (TD.LeaseTimeoutIsEnabled())
					{
						TD.LeaseTimeout(SR.GetString("TraceCodeConnectionPoolLeaseTimeoutReached", new object[]
						{
							this.leaseTimeout
						}), this.parent.Key.ToString());
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262148, SR.GetString("TraceCodeConnectionPoolLeaseTimeoutReached", new object[]
						{
							this.leaseTimeout
						}), this);
					}
				}

				// Token: 0x060086D0 RID: 34512 RVA: 0x001F3790 File Offset: 0x001F1990
				private void TraceConnectionIdleTimeoutExpired()
				{
					if (TD.IdleTimeoutIsEnabled())
					{
						TD.IdleTimeout(SR.GetString("TraceCodeConnectionPoolIdleTimeoutReached", new object[]
						{
							this.idleTimeout
						}), this.parent.Key.ToString());
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262147, SR.GetString("TraceCodeConnectionPoolIdleTimeoutReached", new object[]
						{
							this.idleTimeout
						}), this);
					}
				}

				// Token: 0x04004E4C RID: 20044
				private const int timerThreshold = 1;

				// Token: 0x04004E4D RID: 20045
				private IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool parent;

				// Token: 0x04004E4E RID: 20046
				private TimeSpan idleTimeout;

				// Token: 0x04004E4F RID: 20047
				private TimeSpan leaseTimeout;

				// Token: 0x04004E50 RID: 20048
				private IOThreadTimer idleTimer;

				// Token: 0x04004E51 RID: 20049
				private static Action<object> onIdle;

				// Token: 0x04004E52 RID: 20050
				private object thisLock;

				// Token: 0x04004E53 RID: 20051
				private Exception pendingException;

				// Token: 0x04004E54 RID: 20052
				private Dictionary<TItem, IdlingCommunicationPool<TKey, TItem>.IdleTimeoutEndpointConnectionPool.IdleTimeoutIdleConnectionPool.IdlingConnectionSettings> connectionMapping;

				// Token: 0x02000FC6 RID: 4038
				private class IdlingConnectionSettings
				{
					// Token: 0x060088DD RID: 35037 RVA: 0x001FDC62 File Offset: 0x001FBE62
					public IdlingConnectionSettings()
					{
						this.creationTime = DateTime.UtcNow;
						this.lastUsage = this.creationTime;
					}

					// Token: 0x17001DB4 RID: 7604
					// (get) Token: 0x060088DE RID: 35038 RVA: 0x001FDC81 File Offset: 0x001FBE81
					public DateTime CreationTime
					{
						get
						{
							return this.creationTime;
						}
					}

					// Token: 0x17001DB5 RID: 7605
					// (get) Token: 0x060088DF RID: 35039 RVA: 0x001FDC89 File Offset: 0x001FBE89
					// (set) Token: 0x060088E0 RID: 35040 RVA: 0x001FDC91 File Offset: 0x001FBE91
					public DateTime LastUsage
					{
						get
						{
							return this.lastUsage;
						}
						set
						{
							this.lastUsage = value;
						}
					}

					// Token: 0x04005077 RID: 20599
					private DateTime creationTime;

					// Token: 0x04005078 RID: 20600
					private DateTime lastUsage;
				}
			}
		}
	}
}
