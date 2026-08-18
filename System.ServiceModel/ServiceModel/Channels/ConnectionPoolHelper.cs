using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E1 RID: 2017
	internal abstract class ConnectionPoolHelper
	{
		// Token: 0x06004C52 RID: 19538 RVA: 0x0011694E File Offset: 0x00114B4E
		public ConnectionPoolHelper(ConnectionPool connectionPool, IConnectionInitiator connectionInitiator, Uri via)
		{
			this.connectionInitiator = connectionInitiator;
			this.connectionPool = connectionPool;
			this.via = via;
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004C53 RID: 19539 RVA: 0x0011696B File Offset: 0x00114B6B
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004C54 RID: 19540 RVA: 0x0011696E File Offset: 0x00114B6E
		protected EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x06004C55 RID: 19541
		protected abstract IConnection AcceptPooledConnection(IConnection connection, ref TimeoutHelper timeoutHelper);

		// Token: 0x06004C56 RID: 19542
		protected abstract IAsyncResult BeginAcceptPooledConnection(IConnection connection, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state);

		// Token: 0x06004C57 RID: 19543
		protected abstract IConnection EndAcceptPooledConnection(IAsyncResult result);

		// Token: 0x06004C58 RID: 19544
		protected abstract TimeoutException CreateNewConnectionTimeoutException(TimeSpan timeout, TimeoutException innerException);

		// Token: 0x06004C59 RID: 19545 RVA: 0x0011698A File Offset: 0x00114B8A
		public IAsyncResult BeginEstablishConnection(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ConnectionPoolHelper.EstablishConnectionAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x00116995 File Offset: 0x00114B95
		public IConnection EndEstablishConnection(IAsyncResult result)
		{
			return ConnectionPoolHelper.EstablishConnectionAsyncResult.End(result);
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0011699D File Offset: 0x00114B9D
		private IConnection TakeConnection(TimeSpan timeout)
		{
			return this.connectionPool.TakeConnection(null, this.via, timeout, out this.connectionKey);
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x001169B8 File Offset: 0x00114BB8
		public IConnection EstablishConnection(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IConnection connection = null;
			IConnection result = null;
			bool flag = true;
			EventTraceActivity eventTraceActivity = this.EventTraceActivity;
			if (TD.EstablishConnectionStartIsEnabled())
			{
				TD.EstablishConnectionStart(eventTraceActivity, (this.via != null) ? this.via.AbsoluteUri : string.Empty);
			}
			while (flag)
			{
				connection = this.TakeConnection(timeoutHelper.RemainingTime());
				if (connection == null)
				{
					flag = false;
				}
				else
				{
					bool flag2 = false;
					try
					{
						result = this.AcceptPooledConnection(connection, ref timeoutHelper);
						flag2 = true;
						break;
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (TimeoutException ex)
					{
						if (TD.OpenTimeoutIsEnabled())
						{
							TD.OpenTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					finally
					{
						if (!flag2)
						{
							if (TD.ConnectionPoolPreambleFailedIsEnabled())
							{
								TD.ConnectionPoolPreambleFailed(eventTraceActivity);
							}
							if (DiagnosticUtility.ShouldTraceInformation)
							{
								TraceUtility.TraceEvent(TraceEventType.Information, 262192, SR.GetString("TraceCodeFailedAcceptFromPool", new object[]
								{
									timeoutHelper.RemainingTime()
								}));
							}
							this.connectionPool.ReturnConnection(this.connectionKey, connection, false, TimeSpan.Zero);
						}
					}
				}
			}
			if (!flag)
			{
				bool flag3 = false;
				TimeSpan timeout2 = timeoutHelper.RemainingTime();
				try
				{
					try
					{
						connection = this.connectionInitiator.Connect(this.via, timeout2);
					}
					catch (TimeoutException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateNewConnectionTimeoutException(timeout2, innerException));
					}
					this.connectionInitiator = null;
					result = this.AcceptPooledConnection(connection, ref timeoutHelper);
					flag3 = true;
				}
				finally
				{
					if (!flag3)
					{
						this.connectionKey = null;
						if (connection != null)
						{
							connection.Abort();
						}
					}
				}
			}
			this.SnapshotConnection(result, connection, flag);
			if (TD.EstablishConnectionStopIsEnabled())
			{
				TD.EstablishConnectionStop(eventTraceActivity);
			}
			return result;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x00116B8C File Offset: 0x00114D8C
		private void SnapshotConnection(IConnection upgradedConnection, IConnection rawConnection, bool isConnectionFromPool)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.closed)
				{
					upgradedConnection.Abort();
					if (isConnectionFromPool)
					{
						this.connectionPool.ReturnConnection(this.connectionKey, rawConnection, false, TimeSpan.Zero);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("OperationAbortedDuringConnectionEstablishment", new object[]
					{
						this.via
					})));
				}
				this.upgradedConnection = upgradedConnection;
				this.rawConnection = rawConnection;
				this.isConnectionFromPool = isConnectionFromPool;
			}
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x00116C30 File Offset: 0x00114E30
		public void Abort()
		{
			this.ReleaseConnection(true, TimeSpan.Zero);
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x00116C3E File Offset: 0x00114E3E
		public void Close(TimeSpan timeout)
		{
			this.ReleaseConnection(false, timeout);
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x00116C48 File Offset: 0x00114E48
		private void ReleaseConnection(bool abort, TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			string key;
			IConnection connection;
			IConnection connection2;
			lock (thisLock)
			{
				this.closed = true;
				key = this.connectionKey;
				connection = this.upgradedConnection;
				connection2 = this.rawConnection;
				this.upgradedConnection = null;
				this.rawConnection = null;
			}
			if (connection == null)
			{
				return;
			}
			try
			{
				if (this.isConnectionFromPool)
				{
					this.connectionPool.ReturnConnection(key, connection2, !abort, timeout);
				}
				else if (abort)
				{
					connection.Abort();
				}
				else
				{
					this.connectionPool.AddConnection(key, connection2, timeout);
				}
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				connection.Abort();
			}
		}

		// Token: 0x04002FA6 RID: 12198
		private IConnectionInitiator connectionInitiator;

		// Token: 0x04002FA7 RID: 12199
		private ConnectionPool connectionPool;

		// Token: 0x04002FA8 RID: 12200
		private Uri via;

		// Token: 0x04002FA9 RID: 12201
		private bool closed;

		// Token: 0x04002FAA RID: 12202
		private string connectionKey;

		// Token: 0x04002FAB RID: 12203
		private bool isConnectionFromPool;

		// Token: 0x04002FAC RID: 12204
		private IConnection rawConnection;

		// Token: 0x04002FAD RID: 12205
		private IConnection upgradedConnection;

		// Token: 0x04002FAE RID: 12206
		private EventTraceActivity eventTraceActivity;

		// Token: 0x02000D06 RID: 3334
		private class EstablishConnectionAsyncResult : AsyncResult
		{
			// Token: 0x06007AF0 RID: 31472 RVA: 0x001C9C8C File Offset: 0x001C7E8C
			public EstablishConnectionAsyncResult(ConnectionPoolHelper parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.timeoutHelper = new TimeoutHelper(timeout);
				bool flag = false;
				bool flag2 = false;
				try
				{
					flag2 = this.Begin();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
				if (flag2)
				{
					this.Cleanup();
					base.Complete(true);
				}
			}

			// Token: 0x17001BC7 RID: 7111
			// (get) Token: 0x06007AF1 RID: 31473 RVA: 0x001C9CF0 File Offset: 0x001C7EF0
			private EventTraceActivity EventTraceActivity
			{
				get
				{
					if (this.eventTraceActivity == null)
					{
						this.eventTraceActivity = new EventTraceActivity(false);
					}
					return this.eventTraceActivity;
				}
			}

			// Token: 0x06007AF2 RID: 31474 RVA: 0x001C9D0C File Offset: 0x001C7F0C
			public static IConnection End(IAsyncResult result)
			{
				ConnectionPoolHelper.EstablishConnectionAsyncResult establishConnectionAsyncResult = AsyncResult.End<ConnectionPoolHelper.EstablishConnectionAsyncResult>(result);
				if (TD.EstablishConnectionStopIsEnabled())
				{
					TD.EstablishConnectionStop(establishConnectionAsyncResult.EventTraceActivity);
				}
				return establishConnectionAsyncResult.currentConnection;
			}

			// Token: 0x06007AF3 RID: 31475 RVA: 0x001C9D38 File Offset: 0x001C7F38
			private bool Begin()
			{
				if (TD.EstablishConnectionStartIsEnabled())
				{
					TD.EstablishConnectionStart(this.EventTraceActivity, this.parent.connectionKey);
				}
				IConnection connection = this.parent.TakeConnection(this.timeoutHelper.RemainingTime());
				this.TrackConnection(connection);
				bool flag;
				return this.OpenUsingConnectionPool(out flag) || (!flag && this.OpenUsingNewConnection());
			}

			// Token: 0x06007AF4 RID: 31476 RVA: 0x001C9D98 File Offset: 0x001C7F98
			private bool OpenUsingConnectionPool(out bool openingFromPool)
			{
				openingFromPool = true;
				while (this.currentConnection != null)
				{
					bool flag = false;
					try
					{
						if (!this.ProcessConnection())
						{
							return false;
						}
						flag = true;
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						this.Cleanup();
					}
					catch (TimeoutException ex)
					{
						if (TD.OpenTimeoutIsEnabled())
						{
							TD.OpenTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						this.Cleanup();
					}
					if (flag)
					{
						this.SnapshotConnection();
						return true;
					}
					IConnection connection = this.parent.TakeConnection(this.timeoutHelper.RemainingTime());
					this.TrackConnection(connection);
				}
				openingFromPool = false;
				return false;
			}

			// Token: 0x06007AF5 RID: 31477 RVA: 0x001C9E48 File Offset: 0x001C8048
			private bool OpenUsingNewConnection()
			{
				this.newConnection = true;
				IAsyncResult asyncResult;
				try
				{
					this.connectTimeout = this.timeoutHelper.RemainingTime();
					if (ConnectionPoolHelper.EstablishConnectionAsyncResult.onConnect == null)
					{
						ConnectionPoolHelper.EstablishConnectionAsyncResult.onConnect = Fx.ThunkCallback(new AsyncCallback(ConnectionPoolHelper.EstablishConnectionAsyncResult.OnConnect));
					}
					asyncResult = this.parent.connectionInitiator.BeginConnect(this.parent.via, this.connectTimeout, ConnectionPoolHelper.EstablishConnectionAsyncResult.onConnect, this);
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.parent.CreateNewConnectionTimeoutException(this.connectTimeout, innerException));
				}
				return asyncResult.CompletedSynchronously && this.HandleConnect(asyncResult);
			}

			// Token: 0x06007AF6 RID: 31478 RVA: 0x001C9EF4 File Offset: 0x001C80F4
			private bool HandleConnect(IAsyncResult connectResult)
			{
				try
				{
					this.TrackConnection(this.parent.connectionInitiator.EndConnect(connectResult));
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.parent.CreateNewConnectionTimeoutException(this.connectTimeout, innerException));
				}
				if (this.ProcessConnection())
				{
					this.SnapshotConnection();
					return true;
				}
				return false;
			}

			// Token: 0x06007AF7 RID: 31479 RVA: 0x001C9F5C File Offset: 0x001C815C
			private bool ProcessConnection()
			{
				IAsyncResult asyncResult = this.parent.BeginAcceptPooledConnection(this.rawConnection, ref this.timeoutHelper, ConnectionPoolHelper.EstablishConnectionAsyncResult.onProcessConnection, this);
				return asyncResult.CompletedSynchronously && this.HandleProcessConnection(asyncResult);
			}

			// Token: 0x06007AF8 RID: 31480 RVA: 0x001C9F98 File Offset: 0x001C8198
			private bool HandleProcessConnection(IAsyncResult result)
			{
				this.currentConnection = this.parent.EndAcceptPooledConnection(result);
				this.cleanupConnection = false;
				return true;
			}

			// Token: 0x06007AF9 RID: 31481 RVA: 0x001C9FB4 File Offset: 0x001C81B4
			private void SnapshotConnection()
			{
				this.parent.SnapshotConnection(this.currentConnection, this.rawConnection, !this.newConnection);
			}

			// Token: 0x06007AFA RID: 31482 RVA: 0x001C9FD6 File Offset: 0x001C81D6
			private void TrackConnection(IConnection connection)
			{
				this.cleanupConnection = true;
				this.rawConnection = connection;
				this.currentConnection = connection;
			}

			// Token: 0x06007AFB RID: 31483 RVA: 0x001C9FF0 File Offset: 0x001C81F0
			private void Cleanup()
			{
				if (this.cleanupConnection)
				{
					if (this.newConnection)
					{
						if (this.currentConnection != null)
						{
							this.currentConnection.Abort();
							this.currentConnection = null;
						}
					}
					else if (this.rawConnection != null)
					{
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 262192, SR.GetString("TraceCodeFailedAcceptFromPool", new object[]
							{
								this.timeoutHelper.RemainingTime()
							}));
						}
						this.parent.connectionPool.ReturnConnection(this.parent.connectionKey, this.rawConnection, false, this.timeoutHelper.RemainingTime());
						this.currentConnection = null;
						this.rawConnection = null;
					}
					this.cleanupConnection = false;
				}
			}

			// Token: 0x06007AFC RID: 31484 RVA: 0x001CA0B0 File Offset: 0x001C82B0
			private static void OnConnect(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionPoolHelper.EstablishConnectionAsyncResult establishConnectionAsyncResult = (ConnectionPoolHelper.EstablishConnectionAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					flag = establishConnectionAsyncResult.HandleConnect(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					establishConnectionAsyncResult.Cleanup();
					establishConnectionAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007AFD RID: 31485 RVA: 0x001CA110 File Offset: 0x001C8310
			private static void OnProcessConnection(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionPoolHelper.EstablishConnectionAsyncResult establishConnectionAsyncResult = (ConnectionPoolHelper.EstablishConnectionAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag2;
				try
				{
					bool flag = false;
					try
					{
						flag2 = establishConnectionAsyncResult.HandleProcessConnection(result);
						if (flag2)
						{
							flag = true;
						}
					}
					catch (CommunicationException ex)
					{
						if (!establishConnectionAsyncResult.newConnection)
						{
							DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
							establishConnectionAsyncResult.Cleanup();
							flag2 = establishConnectionAsyncResult.Begin();
						}
						else
						{
							flag2 = true;
							exception = ex;
						}
					}
					catch (TimeoutException ex2)
					{
						if (!establishConnectionAsyncResult.newConnection)
						{
							if (TD.OpenTimeoutIsEnabled())
							{
								TD.OpenTimeout(ex2.Message);
							}
							DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
							establishConnectionAsyncResult.Cleanup();
							flag2 = establishConnectionAsyncResult.Begin();
						}
						else
						{
							flag2 = true;
							exception = ex2;
						}
					}
					if (flag)
					{
						establishConnectionAsyncResult.SnapshotConnection();
					}
				}
				catch (Exception ex3)
				{
					if (Fx.IsFatal(ex3))
					{
						throw;
					}
					flag2 = true;
					exception = ex3;
				}
				if (flag2)
				{
					establishConnectionAsyncResult.Cleanup();
					establishConnectionAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x04004641 RID: 17985
			private ConnectionPoolHelper parent;

			// Token: 0x04004642 RID: 17986
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004643 RID: 17987
			private IConnection currentConnection;

			// Token: 0x04004644 RID: 17988
			private IConnection rawConnection;

			// Token: 0x04004645 RID: 17989
			private bool newConnection;

			// Token: 0x04004646 RID: 17990
			private bool cleanupConnection;

			// Token: 0x04004647 RID: 17991
			private TimeSpan connectTimeout;

			// Token: 0x04004648 RID: 17992
			private static AsyncCallback onConnect;

			// Token: 0x04004649 RID: 17993
			private static AsyncCallback onProcessConnection = Fx.ThunkCallback(new AsyncCallback(ConnectionPoolHelper.EstablishConnectionAsyncResult.OnProcessConnection));

			// Token: 0x0400464A RID: 17994
			private EventTraceActivity eventTraceActivity;
		}
	}
}
