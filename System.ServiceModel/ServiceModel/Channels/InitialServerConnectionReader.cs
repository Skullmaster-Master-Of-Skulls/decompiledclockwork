using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E5 RID: 2021
	internal abstract class InitialServerConnectionReader : IDisposable
	{
		// Token: 0x06004C6E RID: 19566 RVA: 0x00116E7C File Offset: 0x0011507C
		protected InitialServerConnectionReader(IConnection connection, ConnectionClosedCallback closedCallback) : this(connection, closedCallback, 2048, 256)
		{
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x00116E90 File Offset: 0x00115090
		protected InitialServerConnectionReader(IConnection connection, ConnectionClosedCallback closedCallback, int maxViaSize, int maxContentTypeSize)
		{
			if (connection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("connection");
			}
			if (closedCallback == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("closedCallback");
			}
			this.connection = connection;
			this.closedCallback = closedCallback;
			this.maxContentTypeSize = maxContentTypeSize;
			this.maxViaSize = maxViaSize;
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004C70 RID: 19568 RVA: 0x00116EE6 File Offset: 0x001150E6
		public IConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004C71 RID: 19569 RVA: 0x00116EEE File Offset: 0x001150EE
		// (set) Token: 0x06004C72 RID: 19570 RVA: 0x00116EF6 File Offset: 0x001150F6
		public Action ConnectionDequeuedCallback
		{
			get
			{
				return this.connectionDequeuedCallback;
			}
			set
			{
				this.connectionDequeuedCallback = value;
			}
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x00116F00 File Offset: 0x00115100
		public Action GetConnectionDequeuedCallback()
		{
			Action result = this.connectionDequeuedCallback;
			this.connectionDequeuedCallback = null;
			return result;
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x00116F1C File Offset: 0x0011511C
		protected bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x00116F24 File Offset: 0x00115124
		protected int MaxContentTypeSize
		{
			get
			{
				return this.maxContentTypeSize;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x00116F2C File Offset: 0x0011512C
		protected int MaxViaSize
		{
			get
			{
				return this.maxViaSize;
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x00116F34 File Offset: 0x00115134
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x00116F37 File Offset: 0x00115137
		public void ReleaseConnection()
		{
			this.isClosed = true;
			this.connection = null;
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x00116F48 File Offset: 0x00115148
		public void CloseFromPool(TimeSpan timeout)
		{
			try
			{
				this.Close(timeout);
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x00116FA0 File Offset: 0x001151A0
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isClosed)
				{
					return;
				}
				this.isClosed = true;
			}
			IConnection connection = this.connection;
			if (connection != null)
			{
				connection.Abort();
			}
			if (this.connectionDequeuedCallback != null)
			{
				this.connectionDequeuedCallback();
			}
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x00117010 File Offset: 0x00115210
		protected void Abort()
		{
			this.Abort(null);
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0011701C File Offset: 0x0011521C
		internal void Abort(Exception e)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isClosed)
				{
					return;
				}
				this.isClosed = true;
			}
			try
			{
				if (e != null && DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 262182, SR.GetString("TraceCodeChannelConnectionDropped"), this, e);
				}
				this.connection.Abort();
			}
			finally
			{
				if (this.closedCallback != null)
				{
					this.closedCallback(this);
				}
				if (this.connectionDequeuedCallback != null)
				{
					this.connectionDequeuedCallback();
				}
			}
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x001170CC File Offset: 0x001152CC
		protected void Close(TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isClosed)
				{
					return;
				}
				this.isClosed = true;
			}
			bool flag2 = false;
			try
			{
				this.connection.Close(timeout, true);
				flag2 = true;
			}
			finally
			{
				if (!flag2)
				{
					this.connection.Abort();
				}
				if (this.closedCallback != null)
				{
					this.closedCallback(this);
				}
				if (this.connectionDequeuedCallback != null)
				{
					this.connectionDequeuedCallback();
				}
			}
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0011716C File Offset: 0x0011536C
		internal static void SendFault(IConnection connection, string faultString, byte[] drainBuffer, TimeSpan sendTimeout, int maxRead)
		{
			if (TD.ConnectionReaderSendFaultIsEnabled())
			{
				TD.ConnectionReaderSendFault(faultString);
			}
			EncodedFault encodedFault = new EncodedFault(faultString);
			TimeoutHelper timeoutHelper = new TimeoutHelper(sendTimeout);
			try
			{
				connection.Write(encodedFault.EncodedBytes, 0, encodedFault.EncodedBytes.Length, true, timeoutHelper.RemainingTime());
				connection.Shutdown(timeoutHelper.RemainingTime());
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				connection.Abort();
				return;
			}
			catch (TimeoutException ex)
			{
				if (TD.SendTimeoutIsEnabled())
				{
					TD.SendTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				connection.Abort();
				return;
			}
			int num = 0;
			int num2 = 0;
			do
			{
				try
				{
					num = connection.Read(drainBuffer, 0, drainBuffer.Length, timeoutHelper.RemainingTime());
				}
				catch (CommunicationException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					connection.Abort();
					return;
				}
				catch (TimeoutException ex2)
				{
					if (TD.SendTimeoutIsEnabled())
					{
						TD.SendTimeout(ex2.Message);
					}
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
					connection.Abort();
					return;
				}
				if (num == 0)
				{
					goto IL_FC;
				}
				num2 += num;
			}
			while (num2 <= maxRead && !(timeoutHelper.RemainingTime() <= TimeSpan.Zero));
			connection.Abort();
			return;
			IL_FC:
			ConnectionUtilities.CloseNoThrow(connection, timeoutHelper.RemainingTime());
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x001172B8 File Offset: 0x001154B8
		public static IAsyncResult BeginUpgradeConnection(IConnection connection, StreamUpgradeAcceptor upgradeAcceptor, IDefaultCommunicationTimeouts defaultTimeouts, TimeSpan openTimeout, AsyncCallback callback, object state)
		{
			return new InitialServerConnectionReader.UpgradeConnectionAsyncResult(connection, upgradeAcceptor, defaultTimeouts, openTimeout, callback, state);
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x001172C7 File Offset: 0x001154C7
		public static IConnection EndUpgradeConnection(IAsyncResult result)
		{
			return InitialServerConnectionReader.UpgradeConnectionAsyncResult.End(result);
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x001172D0 File Offset: 0x001154D0
		public static IConnection UpgradeConnection(IConnection connection, StreamUpgradeAcceptor upgradeAcceptor, TimeSpan openTimeout, IDefaultCommunicationTimeouts defaultTimeouts)
		{
			ConnectionStream connectionStream = new ConnectionStream(connection, defaultTimeouts, openTimeout, true);
			Stream stream = upgradeAcceptor.AcceptUpgrade(connectionStream);
			connectionStream.CompleteOpen();
			if (upgradeAcceptor is StreamSecurityUpgradeAcceptor && DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262190, SR.GetString("TraceCodeStreamSecurityUpgradeAccepted"), new StringTraceRecord("Type", upgradeAcceptor.GetType().ToString()), connection, null);
			}
			return new StreamConnection(stream, connectionStream);
		}

		// Token: 0x04002FB0 RID: 12208
		private int maxViaSize;

		// Token: 0x04002FB1 RID: 12209
		private int maxContentTypeSize;

		// Token: 0x04002FB2 RID: 12210
		private IConnection connection;

		// Token: 0x04002FB3 RID: 12211
		private Action connectionDequeuedCallback;

		// Token: 0x04002FB4 RID: 12212
		private ConnectionClosedCallback closedCallback;

		// Token: 0x04002FB5 RID: 12213
		private bool isClosed;

		// Token: 0x02000D07 RID: 3335
		private class UpgradeConnectionAsyncResult : AsyncResult
		{
			// Token: 0x06007AFF RID: 31487 RVA: 0x001CA218 File Offset: 0x001C8418
			public UpgradeConnectionAsyncResult(IConnection connection, StreamUpgradeAcceptor upgradeAcceptor, IDefaultCommunicationTimeouts defaultTimeouts, TimeSpan openTimeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.upgradeAcceptor = upgradeAcceptor;
				this.connectionStream = new ConnectionStream(connection, defaultTimeouts, openTimeout, true);
				bool flag = false;
				IAsyncResult asyncResult = upgradeAcceptor.BeginAcceptUpgrade(this.connectionStream, InitialServerConnectionReader.UpgradeConnectionAsyncResult.onAcceptUpgrade, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteAcceptUpgrade(asyncResult);
					flag = true;
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007B00 RID: 31488 RVA: 0x001CA278 File Offset: 0x001C8478
			public static IConnection End(IAsyncResult result)
			{
				InitialServerConnectionReader.UpgradeConnectionAsyncResult upgradeConnectionAsyncResult = AsyncResult.End<InitialServerConnectionReader.UpgradeConnectionAsyncResult>(result);
				return upgradeConnectionAsyncResult.connection;
			}

			// Token: 0x06007B01 RID: 31489 RVA: 0x001CA294 File Offset: 0x001C8494
			private void CompleteAcceptUpgrade(IAsyncResult result)
			{
				bool flag = false;
				Stream stream;
				try
				{
					stream = this.upgradeAcceptor.EndAcceptUpgrade(result);
					this.connectionStream.CompleteOpen();
					flag = true;
				}
				finally
				{
					if (this.upgradeAcceptor is StreamSecurityUpgradeAcceptor && (DiagnosticUtility.ShouldTraceInformation && flag))
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262190, SR.GetString("TraceCodeStreamSecurityUpgradeAccepted"), new StringTraceRecord("Type", this.upgradeAcceptor.GetType().ToString()), this, null);
					}
				}
				this.connection = new StreamConnection(stream, this.connectionStream);
			}

			// Token: 0x06007B02 RID: 31490 RVA: 0x001CA32C File Offset: 0x001C852C
			private static void OnAcceptUpgrade(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				InitialServerConnectionReader.UpgradeConnectionAsyncResult upgradeConnectionAsyncResult = (InitialServerConnectionReader.UpgradeConnectionAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					upgradeConnectionAsyncResult.CompleteAcceptUpgrade(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				upgradeConnectionAsyncResult.Complete(false, exception);
			}

			// Token: 0x0400464B RID: 17995
			private ConnectionStream connectionStream;

			// Token: 0x0400464C RID: 17996
			private static AsyncCallback onAcceptUpgrade = Fx.ThunkCallback(new AsyncCallback(InitialServerConnectionReader.UpgradeConnectionAsyncResult.OnAcceptUpgrade));

			// Token: 0x0400464D RID: 17997
			private IConnection connection;

			// Token: 0x0400464E RID: 17998
			private StreamUpgradeAcceptor upgradeAcceptor;
		}
	}
}
