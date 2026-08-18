using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D8 RID: 2008
	internal class ConnectionAcceptor : IDisposable
	{
		// Token: 0x06004BB7 RID: 19383 RVA: 0x00114657 File Offset: 0x00112857
		public ConnectionAcceptor(IConnectionListener listener, int maxAccepts, int maxPendingConnections, ConnectionAvailableCallback callback) : this(listener, maxAccepts, maxPendingConnections, callback, null)
		{
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x00114668 File Offset: 0x00112868
		public ConnectionAcceptor(IConnectionListener listener, int maxAccepts, int maxPendingConnections, ConnectionAvailableCallback callback, ErrorCallback errorCallback)
		{
			if (maxAccepts <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxAccepts", maxAccepts, SR.GetString("ValueMustBePositive")));
			}
			this.listener = listener;
			this.maxAccepts = maxAccepts;
			this.maxPendingConnections = maxPendingConnections;
			this.callback = callback;
			this.errorCallback = errorCallback;
			this.onConnectionDequeued = new Action(this.OnConnectionDequeued);
			this.acceptCompletedCallback = Fx.ThunkCallback(new AsyncCallback(this.AcceptCompletedCallback));
			this.scheduleAcceptCallback = new Action<object>(this.ScheduleAcceptCallback);
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x00114704 File Offset: 0x00112904
		private bool IsAcceptNecessary
		{
			get
			{
				return this.pendingAccepts < this.maxAccepts && this.connections + this.pendingAccepts < this.maxPendingConnections && !this.isDisposed;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x00114734 File Offset: 0x00112934
		public int ConnectionCount
		{
			get
			{
				return this.connections;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004BBB RID: 19387 RVA: 0x0011473C File Offset: 0x0011293C
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00114740 File Offset: 0x00112940
		private void AcceptIfNecessary(bool startAccepting)
		{
			if (this.IsAcceptNecessary)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					while (this.IsAcceptNecessary)
					{
						IAsyncResult asyncResult = null;
						Exception ex = null;
						try
						{
							asyncResult = this.listener.BeginAccept(this.acceptCompletedCallback, null);
						}
						catch (CommunicationException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							if (startAccepting)
							{
								throw;
							}
							if (this.errorCallback == null && !ExceptionHandler.HandleTransportExceptionHelper(ex2))
							{
								throw;
							}
							ex = ex2;
						}
						if (ex != null && this.errorCallback != null)
						{
							this.errorCallback(ex);
						}
						if (asyncResult != null)
						{
							if (asyncResult.CompletedSynchronously)
							{
								ActionItem.Schedule(this.scheduleAcceptCallback, asyncResult);
							}
							this.pendingAccepts++;
						}
					}
				}
			}
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00114838 File Offset: 0x00112A38
		private void AcceptCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			this.HandleCompletedAccept(result);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0011484C File Offset: 0x00112A4C
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.listener.Dispose();
				}
			}
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x001148A0 File Offset: 0x00112AA0
		private void HandleCompletedAccept(IAsyncResult result)
		{
			IConnection connection = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				bool flag2 = false;
				Exception ex = null;
				try
				{
					if (!this.isDisposed)
					{
						connection = this.listener.EndAccept(result);
						if (connection != null)
						{
							if (this.connections + 1 >= this.maxPendingConnections)
							{
								if (TD.MaxPendingConnectionsExceededIsEnabled())
								{
									TD.MaxPendingConnectionsExceeded(SR.GetString("TraceCodeMaxPendingConnectionsReached"));
								}
								if (DiagnosticUtility.ShouldTraceWarning)
								{
									TraceUtility.TraceEvent(TraceEventType.Warning, 262180, SR.GetString("TraceCodeMaxPendingConnectionsReached"), new StringTraceRecord("MaxPendingConnections", this.maxPendingConnections.ToString(CultureInfo.InvariantCulture)), this, null);
								}
							}
							else if (TD.PendingConnectionsRatioIsEnabled())
							{
								TD.PendingConnectionsRatio(this.connections + 1, this.maxPendingConnections);
							}
							this.connections++;
						}
					}
					flag2 = true;
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (this.errorCallback == null && !ExceptionHandler.HandleTransportExceptionHelper(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				finally
				{
					if (!flag2)
					{
						connection = null;
					}
					this.pendingAccepts--;
					if (this.pendingAccepts == 0 && TD.PendingAcceptsAtZeroIsEnabled())
					{
						TD.PendingAcceptsAtZero();
					}
				}
				if (ex != null && this.errorCallback != null)
				{
					this.errorCallback(ex);
				}
			}
			this.AcceptIfNecessary(false);
			if (connection != null)
			{
				this.callback(connection, this.onConnectionDequeued);
			}
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x00114A70 File Offset: 0x00112C70
		private void OnConnectionDequeued()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.connections--;
				if (TD.PendingConnectionsRatioIsEnabled())
				{
					TD.PendingConnectionsRatio(this.connections, this.maxPendingConnections);
				}
			}
			this.AcceptIfNecessary(false);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x00114AD8 File Offset: 0x00112CD8
		private void ScheduleAcceptCallback(object state)
		{
			this.HandleCompletedAccept((IAsyncResult)state);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x00114AE6 File Offset: 0x00112CE6
		public void StartAccepting()
		{
			this.listener.Listen();
			this.AcceptIfNecessary(true);
		}

		// Token: 0x04002F54 RID: 12116
		private int maxAccepts;

		// Token: 0x04002F55 RID: 12117
		private int maxPendingConnections;

		// Token: 0x04002F56 RID: 12118
		private int connections;

		// Token: 0x04002F57 RID: 12119
		private int pendingAccepts;

		// Token: 0x04002F58 RID: 12120
		private IConnectionListener listener;

		// Token: 0x04002F59 RID: 12121
		private AsyncCallback acceptCompletedCallback;

		// Token: 0x04002F5A RID: 12122
		private Action<object> scheduleAcceptCallback;

		// Token: 0x04002F5B RID: 12123
		private Action onConnectionDequeued;

		// Token: 0x04002F5C RID: 12124
		private bool isDisposed;

		// Token: 0x04002F5D RID: 12125
		private ConnectionAvailableCallback callback;

		// Token: 0x04002F5E RID: 12126
		private ErrorCallback errorCallback;
	}
}
