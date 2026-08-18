using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A0 RID: 1952
	internal class TransportManagerContainer
	{
		// Token: 0x060049D3 RID: 18899 RVA: 0x0010F0C7 File Offset: 0x0010D2C7
		public TransportManagerContainer(TransportChannelListener listener)
		{
			this.listener = listener;
			this.tableLock = listener.TransportManagerTable;
			this.transportManagers = new List<TransportManager>();
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x0010F0F0 File Offset: 0x0010D2F0
		private TransportManagerContainer(TransportManagerContainer source)
		{
			this.listener = source.listener;
			this.tableLock = source.tableLock;
			this.transportManagers = new List<TransportManager>();
			for (int i = 0; i < source.transportManagers.Count; i++)
			{
				this.transportManagers.Add(source.transportManagers[i]);
			}
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x0010F154 File Offset: 0x0010D354
		public static TransportManagerContainer TransferTransportManagers(TransportManagerContainer source)
		{
			TransportManagerContainer result = null;
			object obj = source.tableLock;
			lock (obj)
			{
				if (source.transportManagers.Count > 0)
				{
					result = new TransportManagerContainer(source);
					source.transportManagers.Clear();
				}
			}
			return result;
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x0010F1B4 File Offset: 0x0010D3B4
		public void Abort()
		{
			this.Close(true, TimeSpan.Zero);
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x0010F1C2 File Offset: 0x0010D3C2
		public IAsyncResult BeginOpen(SelectTransportManagersCallback selectTransportManagerCallback, AsyncCallback callback, object state)
		{
			return new TransportManagerContainer.OpenAsyncResult(selectTransportManagerCallback, this, callback, state);
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x0010F1CD File Offset: 0x0010D3CD
		public void EndOpen(IAsyncResult result)
		{
			TransportManagerContainer.OpenAsyncResult.End(result);
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0010F1D8 File Offset: 0x0010D3D8
		public void Open(SelectTransportManagersCallback selectTransportManagerCallback)
		{
			object obj = this.tableLock;
			lock (obj)
			{
				if (!this.closed)
				{
					IList<TransportManager> list = selectTransportManagerCallback();
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							TransportManager transportManager = list[i];
							transportManager.Open(this.listener);
							this.transportManagers.Add(transportManager);
						}
					}
				}
			}
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0010F25C File Offset: 0x0010D45C
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportManagerContainer.CloseAsyncResult(this, callback, timeout, state);
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x0010F267 File Offset: 0x0010D467
		public void EndClose(IAsyncResult result)
		{
			TransportManagerContainer.CloseAsyncResult.End(result);
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0010F26F File Offset: 0x0010D46F
		public void Close(TimeSpan timeout)
		{
			this.Close(false, timeout);
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x0010F27C File Offset: 0x0010D47C
		public void Close(bool aborting, TimeSpan timeout)
		{
			if (this.closed)
			{
				return;
			}
			object obj = this.tableLock;
			lock (obj)
			{
				if (!this.closed)
				{
					this.closed = true;
					IList<TransportManager> list = new List<TransportManager>(this.transportManagers);
					this.transportManagers.Clear();
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					TimeoutException ex = null;
					foreach (TransportManager transportManager in list)
					{
						try
						{
							if (!aborting && ex == null)
							{
								transportManager.Close(this.listener, timeoutHelper.RemainingTime());
							}
							else
							{
								transportManager.Abort(this.listener);
							}
						}
						catch (TimeoutException ex2)
						{
							ex = ex2;
							transportManager.Abort(this.listener);
						}
					}
					if (ex != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnClose", new object[]
						{
							timeout
						}), ex));
					}
				}
			}
		}

		// Token: 0x04002ED4 RID: 11988
		private IList<TransportManager> transportManagers;

		// Token: 0x04002ED5 RID: 11989
		private TransportChannelListener listener;

		// Token: 0x04002ED6 RID: 11990
		private bool closed;

		// Token: 0x04002ED7 RID: 11991
		private object tableLock;

		// Token: 0x02000CEF RID: 3311
		private abstract class OpenOrCloseAsyncResult : TraceAsyncResult
		{
			// Token: 0x06007A6C RID: 31340 RVA: 0x001C8169 File Offset: 0x001C6369
			protected OpenOrCloseAsyncResult(TransportManagerContainer parent, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
			}

			// Token: 0x06007A6D RID: 31341 RVA: 0x001C817A File Offset: 0x001C637A
			protected void Begin()
			{
				ActionItem.Schedule(TransportManagerContainer.OpenOrCloseAsyncResult.scheduledCallback, this);
			}

			// Token: 0x06007A6E RID: 31342 RVA: 0x001C8187 File Offset: 0x001C6387
			private static void OnScheduled(object state)
			{
				((TransportManagerContainer.OpenOrCloseAsyncResult)state).OnScheduled();
			}

			// Token: 0x06007A6F RID: 31343 RVA: 0x001C8194 File Offset: 0x001C6394
			private void OnScheduled()
			{
				using (ServiceModelActivity.BoundOperation(base.CallbackActivity))
				{
					Exception exception = null;
					try
					{
						this.OnScheduled(this.parent);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007A70 RID: 31344
			protected abstract void OnScheduled(TransportManagerContainer parent);

			// Token: 0x04004605 RID: 17925
			private TransportManagerContainer parent;

			// Token: 0x04004606 RID: 17926
			private static Action<object> scheduledCallback = new Action<object>(TransportManagerContainer.OpenOrCloseAsyncResult.OnScheduled);
		}

		// Token: 0x02000CF0 RID: 3312
		private sealed class CloseAsyncResult : TransportManagerContainer.OpenOrCloseAsyncResult
		{
			// Token: 0x06007A72 RID: 31346 RVA: 0x001C820F File Offset: 0x001C640F
			public CloseAsyncResult(TransportManagerContainer parent, AsyncCallback callback, TimeSpan timeout, object state) : base(parent, callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.timeoutHelper.RemainingTime();
				base.Begin();
			}

			// Token: 0x06007A73 RID: 31347 RVA: 0x001C8239 File Offset: 0x001C6439
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<TransportManagerContainer.CloseAsyncResult>(result);
			}

			// Token: 0x06007A74 RID: 31348 RVA: 0x001C8242 File Offset: 0x001C6442
			protected override void OnScheduled(TransportManagerContainer parent)
			{
				parent.Close(this.timeoutHelper.RemainingTime());
			}

			// Token: 0x04004607 RID: 17927
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000CF1 RID: 3313
		private sealed class OpenAsyncResult : TransportManagerContainer.OpenOrCloseAsyncResult
		{
			// Token: 0x06007A75 RID: 31349 RVA: 0x001C8255 File Offset: 0x001C6455
			public OpenAsyncResult(SelectTransportManagersCallback selectTransportManagerCallback, TransportManagerContainer parent, AsyncCallback callback, object state) : base(parent, callback, state)
			{
				this.selectTransportManagerCallback = selectTransportManagerCallback;
				base.Begin();
			}

			// Token: 0x06007A76 RID: 31350 RVA: 0x001C826E File Offset: 0x001C646E
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<TransportManagerContainer.OpenAsyncResult>(result);
			}

			// Token: 0x06007A77 RID: 31351 RVA: 0x001C8277 File Offset: 0x001C6477
			protected override void OnScheduled(TransportManagerContainer parent)
			{
				parent.Open(this.selectTransportManagerCallback);
			}

			// Token: 0x04004608 RID: 17928
			private SelectTransportManagersCallback selectTransportManagerCallback;
		}
	}
}
