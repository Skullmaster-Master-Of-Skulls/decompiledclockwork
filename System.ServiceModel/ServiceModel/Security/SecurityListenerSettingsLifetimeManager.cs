using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F6 RID: 758
	internal class SecurityListenerSettingsLifetimeManager
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x0005F260 File Offset: 0x0005D460
		public SecurityListenerSettingsLifetimeManager(SecurityProtocolFactory securityProtocolFactory, SecuritySessionServerSettings sessionSettings, bool sessionMode, IChannelListener innerListener)
		{
			this.securityProtocolFactory = securityProtocolFactory;
			this.sessionSettings = sessionSettings;
			this.sessionMode = sessionMode;
			this.innerListener = innerListener;
			this.referenceCount = 1;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0005F28C File Offset: 0x0005D48C
		public void Abort()
		{
			if (Interlocked.Decrement(ref this.referenceCount) == 0)
			{
				this.AbortCore();
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005F2A1 File Offset: 0x0005D4A1
		public void AddReference()
		{
			Interlocked.Increment(ref this.referenceCount);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0005F2B0 File Offset: 0x0005D4B0
		public void Open(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.securityProtocolFactory != null)
			{
				this.securityProtocolFactory.Open(false, timeoutHelper.RemainingTime());
			}
			if (this.sessionMode && this.sessionSettings != null)
			{
				this.sessionSettings.Open(timeoutHelper.RemainingTime());
			}
			this.innerListener.Open(timeoutHelper.RemainingTime());
			this.SetBufferManager();
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005F31C File Offset: 0x0005D51C
		private void SetBufferManager()
		{
			ITransportFactorySettings property = this.innerListener.GetProperty<ITransportFactorySettings>();
			if (property == null)
			{
				return;
			}
			BufferManager bufferManager = property.BufferManager;
			if (bufferManager == null)
			{
				return;
			}
			if (this.securityProtocolFactory != null)
			{
				this.securityProtocolFactory.StreamBufferManager = bufferManager;
			}
			if (this.sessionMode && this.sessionSettings != null && this.sessionSettings.SessionProtocolFactory != null)
			{
				this.sessionSettings.SessionProtocolFactory.StreamBufferManager = bufferManager;
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005F388 File Offset: 0x0005D588
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			List<OperationWithTimeoutBeginCallback> list = new List<OperationWithTimeoutBeginCallback>(3);
			List<OperationEndCallback> list2 = new List<OperationEndCallback>(3);
			if (this.securityProtocolFactory != null)
			{
				list.Add(new OperationWithTimeoutBeginCallback(this.BeginOpenSecurityProtocolFactory));
				list2.Add(new OperationEndCallback(this.EndOpenSecurityProtocolFactory));
			}
			if (this.sessionMode && this.sessionSettings != null)
			{
				list.Add(new OperationWithTimeoutBeginCallback(this.sessionSettings.BeginOpen));
				list2.Add(new OperationEndCallback(this.sessionSettings.EndOpen));
			}
			list.Add(new OperationWithTimeoutBeginCallback(this.innerListener.BeginOpen));
			list2.Add(new OperationEndCallback(this.innerListener.EndOpen));
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, list.ToArray(), list2.ToArray(), callback, state);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005F451 File Offset: 0x0005D651
		public void EndOpen(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
			this.SetBufferManager();
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0005F45F File Offset: 0x0005D65F
		private IAsyncResult BeginOpenSecurityProtocolFactory(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.securityProtocolFactory.BeginOpen(false, timeout, callback, state);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005F470 File Offset: 0x0005D670
		private void EndOpenSecurityProtocolFactory(IAsyncResult result)
		{
			this.securityProtocolFactory.EndOpen(result);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005F480 File Offset: 0x0005D680
		public void Close(TimeSpan timeout)
		{
			if (Interlocked.Decrement(ref this.referenceCount) == 0)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				bool flag = true;
				try
				{
					if (this.securityProtocolFactory != null)
					{
						this.securityProtocolFactory.Close(false, timeoutHelper.RemainingTime());
					}
					if (this.sessionMode && this.sessionSettings != null)
					{
						this.sessionSettings.Close(timeoutHelper.RemainingTime());
					}
					this.innerListener.Close(timeoutHelper.RemainingTime());
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.AbortCore();
					}
				}
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005F514 File Offset: 0x0005D714
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (Interlocked.Decrement(ref this.referenceCount) == 0)
			{
				bool flag = true;
				try
				{
					List<OperationWithTimeoutBeginCallback> list = new List<OperationWithTimeoutBeginCallback>(3);
					List<OperationEndCallback> list2 = new List<OperationEndCallback>(3);
					if (this.securityProtocolFactory != null)
					{
						list.Add(new OperationWithTimeoutBeginCallback(this.securityProtocolFactory.BeginClose));
						list2.Add(new OperationEndCallback(this.securityProtocolFactory.EndClose));
					}
					if (this.sessionMode && this.sessionSettings != null)
					{
						list.Add(new OperationWithTimeoutBeginCallback(this.sessionSettings.BeginClose));
						list2.Add(new OperationEndCallback(this.sessionSettings.EndClose));
					}
					list.Add(new OperationWithTimeoutBeginCallback(this.innerListener.BeginClose));
					list2.Add(new OperationEndCallback(this.innerListener.EndClose));
					IAsyncResult result = OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, list.ToArray(), list2.ToArray(), callback, state);
					flag = false;
					return result;
				}
				finally
				{
					if (flag)
					{
						this.AbortCore();
					}
				}
			}
			return new SecurityListenerSettingsLifetimeManager.DummyCloseAsyncResult(callback, state);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005F628 File Offset: 0x0005D828
		public void EndClose(IAsyncResult result)
		{
			if (result is SecurityListenerSettingsLifetimeManager.DummyCloseAsyncResult)
			{
				SecurityListenerSettingsLifetimeManager.DummyCloseAsyncResult.End(result);
				return;
			}
			bool flag = true;
			try
			{
				OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.AbortCore();
				}
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005F66C File Offset: 0x0005D86C
		private void AbortCore()
		{
			if (this.securityProtocolFactory != null)
			{
				this.securityProtocolFactory.Close(true, TimeSpan.Zero);
			}
			if (this.sessionMode && this.sessionSettings != null)
			{
				this.sessionSettings.Abort();
			}
			this.innerListener.Abort();
		}

		// Token: 0x04001C9F RID: 7327
		private SecurityProtocolFactory securityProtocolFactory;

		// Token: 0x04001CA0 RID: 7328
		private SecuritySessionServerSettings sessionSettings;

		// Token: 0x04001CA1 RID: 7329
		private bool sessionMode;

		// Token: 0x04001CA2 RID: 7330
		private IChannelListener innerListener;

		// Token: 0x04001CA3 RID: 7331
		private int referenceCount;

		// Token: 0x02000B63 RID: 2915
		private class DummyCloseAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06007240 RID: 29248 RVA: 0x001AA9A2 File Offset: 0x001A8BA2
			public DummyCloseAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}

			// Token: 0x06007241 RID: 29249 RVA: 0x001AA9AC File Offset: 0x001A8BAC
			public new static void End(IAsyncResult result)
			{
				AsyncResult.End<SecurityListenerSettingsLifetimeManager.DummyCloseAsyncResult>(result);
			}
		}
	}
}
