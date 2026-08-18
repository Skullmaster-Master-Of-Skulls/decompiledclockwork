using System;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x02000399 RID: 921
	internal abstract class MultipleConnectAsync
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x000A4334 File Offset: 0x000A2534
		public bool StartConnectAsync(SocketAsyncEventArgs args, DnsEndPoint endPoint)
		{
			object obj = this.lockObject;
			bool result;
			lock (obj)
			{
				this.userArgs = args;
				this.endPoint = endPoint;
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					this.SyncFail(new SocketException(SocketError.OperationAborted));
					result = false;
				}
				else
				{
					this.state = MultipleConnectAsync.State.DnsQuery;
					IAsyncResult asyncResult = Dns.BeginGetHostAddresses(endPoint.Host, new AsyncCallback(this.DnsCallback), null);
					if (asyncResult.CompletedSynchronously)
					{
						result = this.DoDnsCallback(asyncResult, true);
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000A43D0 File Offset: 0x000A25D0
		private void DnsCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.DoDnsCallback(result, false);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000A43E4 File Offset: 0x000A25E4
		private bool DoDnsCallback(IAsyncResult result, bool sync)
		{
			Exception ex = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					return true;
				}
				try
				{
					this.addressList = Dns.EndGetHostAddresses(result);
				}
				catch (Exception ex2)
				{
					this.state = MultipleConnectAsync.State.Completed;
					ex = ex2;
				}
				if (ex == null)
				{
					this.state = MultipleConnectAsync.State.ConnectAttempt;
					this.internalArgs = new SocketAsyncEventArgs();
					this.internalArgs.Completed += this.InternalConnectCallback;
					this.internalArgs.SetBuffer(this.userArgs.Buffer, this.userArgs.Offset, this.userArgs.Count);
					ex = this.AttemptConnection();
					if (ex != null)
					{
						this.state = MultipleConnectAsync.State.Completed;
					}
				}
			}
			return ex == null || this.Fail(sync, ex);
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000A44D4 File Offset: 0x000A26D4
		private void InternalConnectCallback(object sender, SocketAsyncEventArgs args)
		{
			Exception ex = null;
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.state == MultipleConnectAsync.State.Canceled)
				{
					ex = new SocketException(SocketError.OperationAborted);
				}
				else if (args.SocketError == SocketError.Success)
				{
					this.state = MultipleConnectAsync.State.Completed;
				}
				else if (args.SocketError == SocketError.OperationAborted)
				{
					ex = new SocketException(SocketError.OperationAborted);
					this.state = MultipleConnectAsync.State.Canceled;
				}
				else
				{
					SocketError socketError = args.SocketError;
					Exception ex2 = this.AttemptConnection();
					if (ex2 == null)
					{
						return;
					}
					SocketException ex3 = ex2 as SocketException;
					if (ex3 != null && ex3.SocketErrorCode == SocketError.NoData)
					{
						ex = new SocketException(socketError);
					}
					else
					{
						ex = ex2;
					}
					this.state = MultipleConnectAsync.State.Completed;
				}
			}
			if (ex == null)
			{
				this.Succeed();
				return;
			}
			this.AsyncFail(ex);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000A45B0 File Offset: 0x000A27B0
		private Exception AttemptConnection()
		{
			try
			{
				Socket socket = null;
				IPAddress ipaddress = this.GetNextAddress(out socket);
				if (ipaddress == null)
				{
					return new SocketException(SocketError.NoData);
				}
				this.internalArgs.RemoteEndPoint = new IPEndPoint(ipaddress, this.endPoint.Port);
				if (!socket.ConnectAsync(this.internalArgs))
				{
					return new SocketException(this.internalArgs.SocketError);
				}
			}
			catch (ObjectDisposedException)
			{
				return new SocketException(SocketError.OperationAborted);
			}
			catch (Exception result)
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002271 RID: 8817
		protected abstract void OnSucceed();

		// Token: 0x06002272 RID: 8818 RVA: 0x000A464C File Offset: 0x000A284C
		protected void Succeed()
		{
			this.OnSucceed();
			this.userArgs.FinishWrapperConnectSuccess(this.internalArgs.ConnectSocket, this.internalArgs.BytesTransferred, this.internalArgs.SocketFlags);
			this.internalArgs.Dispose();
		}

		// Token: 0x06002273 RID: 8819
		protected abstract void OnFail(bool abortive);

		// Token: 0x06002274 RID: 8820 RVA: 0x000A468B File Offset: 0x000A288B
		private bool Fail(bool sync, Exception e)
		{
			if (sync)
			{
				this.SyncFail(e);
				return false;
			}
			this.AsyncFail(e);
			return true;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000A46A4 File Offset: 0x000A28A4
		private void SyncFail(Exception e)
		{
			this.OnFail(false);
			if (this.internalArgs != null)
			{
				this.internalArgs.Dispose();
			}
			SocketException ex = e as SocketException;
			if (ex != null)
			{
				this.userArgs.FinishConnectByNameSyncFailure(ex, 0, SocketFlags.None);
				return;
			}
			throw e;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000A46E5 File Offset: 0x000A28E5
		private void AsyncFail(Exception e)
		{
			this.OnFail(false);
			if (this.internalArgs != null)
			{
				this.internalArgs.Dispose();
			}
			this.userArgs.FinishOperationAsyncFailure(e, 0, SocketFlags.None);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000A4710 File Offset: 0x000A2910
		public void Cancel()
		{
			bool flag = false;
			object obj = this.lockObject;
			lock (obj)
			{
				switch (this.state)
				{
				case MultipleConnectAsync.State.NotStarted:
					flag = true;
					break;
				case MultipleConnectAsync.State.DnsQuery:
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallAsyncFail));
					flag = true;
					break;
				case MultipleConnectAsync.State.ConnectAttempt:
					flag = true;
					break;
				}
				this.state = MultipleConnectAsync.State.Canceled;
			}
			if (flag)
			{
				this.OnFail(true);
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000A4798 File Offset: 0x000A2998
		private void CallAsyncFail(object ignored)
		{
			this.AsyncFail(new SocketException(SocketError.OperationAborted));
		}

		// Token: 0x06002279 RID: 8825
		protected abstract IPAddress GetNextAddress(out Socket attemptSocket);

		// Token: 0x04001F7D RID: 8061
		protected SocketAsyncEventArgs userArgs;

		// Token: 0x04001F7E RID: 8062
		protected SocketAsyncEventArgs internalArgs;

		// Token: 0x04001F7F RID: 8063
		protected DnsEndPoint endPoint;

		// Token: 0x04001F80 RID: 8064
		protected IPAddress[] addressList;

		// Token: 0x04001F81 RID: 8065
		protected int nextAddress;

		// Token: 0x04001F82 RID: 8066
		private MultipleConnectAsync.State state;

		// Token: 0x04001F83 RID: 8067
		private object lockObject = new object();

		// Token: 0x020007E1 RID: 2017
		private enum State
		{
			// Token: 0x040034E2 RID: 13538
			NotStarted,
			// Token: 0x040034E3 RID: 13539
			DnsQuery,
			// Token: 0x040034E4 RID: 13540
			ConnectAttempt,
			// Token: 0x040034E5 RID: 13541
			Completed,
			// Token: 0x040034E6 RID: 13542
			Canceled
		}
	}
}
