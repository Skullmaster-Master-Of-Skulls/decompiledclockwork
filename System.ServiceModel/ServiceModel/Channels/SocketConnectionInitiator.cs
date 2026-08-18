using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Text;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000850 RID: 2128
	internal class SocketConnectionInitiator : IConnectionInitiator
	{
		// Token: 0x06004FDE RID: 20446 RVA: 0x0012503A File Offset: 0x0012323A
		public SocketConnectionInitiator(int bufferSize)
		{
			this.bufferSize = bufferSize;
			this.connectionBufferPool = new ConnectionBufferPool(bufferSize);
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x00125055 File Offset: 0x00123255
		private IConnection CreateConnection(Socket socket)
		{
			return new SocketConnection(socket, this.connectionBufferPool, false);
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x00125064 File Offset: 0x00123264
		public static Exception ConvertConnectException(SocketException socketException, Uri remoteUri, TimeSpan timeSpent, Exception innerException)
		{
			if (socketException.ErrorCode == 6)
			{
				return new CommunicationObjectAbortedException(socketException.Message, socketException);
			}
			if (socketException.ErrorCode == 10049 || socketException.ErrorCode == 10061 || socketException.ErrorCode == 10050 || socketException.ErrorCode == 10051 || socketException.ErrorCode == 10064 || socketException.ErrorCode == 10065 || socketException.ErrorCode == 10060)
			{
				if (timeSpent == TimeSpan.MaxValue)
				{
					return new EndpointNotFoundException(SR.GetString("TcpConnectError", new object[]
					{
						remoteUri.AbsoluteUri,
						socketException.ErrorCode,
						socketException.Message
					}), innerException);
				}
				return new EndpointNotFoundException(SR.GetString("TcpConnectErrorWithTimeSpan", new object[]
				{
					remoteUri.AbsoluteUri,
					socketException.ErrorCode,
					socketException.Message,
					timeSpent
				}), innerException);
			}
			else
			{
				if (socketException.ErrorCode == 10055)
				{
					return new InsufficientMemoryException(SR.GetString("TcpConnectNoBufs"), innerException);
				}
				if (socketException.ErrorCode == 8 || socketException.ErrorCode == 1450 || socketException.ErrorCode == 14)
				{
					return new InsufficientMemoryException(SR.GetString("InsufficentMemory"), socketException);
				}
				if (timeSpent == TimeSpan.MaxValue)
				{
					return new CommunicationException(SR.GetString("TcpConnectError", new object[]
					{
						remoteUri.AbsoluteUri,
						socketException.ErrorCode,
						socketException.Message
					}), innerException);
				}
				return new CommunicationException(SR.GetString("TcpConnectErrorWithTimeSpan", new object[]
				{
					remoteUri.AbsoluteUri,
					socketException.ErrorCode,
					socketException.Message,
					timeSpent
				}), innerException);
			}
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0012523C File Offset: 0x0012343C
		private static IPAddress[] GetIPAddresses(Uri uri)
		{
			if (uri.HostNameType == UriHostNameType.IPv4 || uri.HostNameType == UriHostNameType.IPv6)
			{
				IPAddress ipaddress = IPAddress.Parse(uri.DnsSafeHost);
				return new IPAddress[]
				{
					ipaddress
				};
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = DnsCache.Resolve(uri);
			}
			catch (SocketException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("UnableToResolveHost", new object[]
				{
					uri.Host
				}), innerException));
			}
			if (iphostEntry.AddressList.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("UnableToResolveHost", new object[]
				{
					uri.Host
				})));
			}
			return iphostEntry.AddressList;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x001252F0 File Offset: 0x001234F0
		private static TimeoutException CreateTimeoutException(Uri uri, TimeSpan timeout, IPAddress[] addresses, int invalidAddressCount, SocketException innerException)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < invalidAddressCount; i++)
			{
				if (addresses[i] != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(addresses[i].ToString());
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TcpConnectingToViaTimedOut", new object[]
			{
				uri.AbsoluteUri,
				timeout.ToString(),
				invalidAddressCount,
				addresses.Length,
				stringBuilder.ToString()
			}), innerException));
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x00125390 File Offset: 0x00123590
		public IConnection Connect(Uri uri, TimeSpan timeout)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262187, SR.GetString("TraceCodeInitiatingTcpConnection"), new StringTraceRecord("Uri", uri.ToString()), this, null);
			}
			int num = uri.Port;
			IPAddress[] ipaddresses = SocketConnectionInitiator.GetIPAddresses(uri);
			Socket socket = null;
			SocketException ex = null;
			if (num == -1)
			{
				num = 808;
			}
			int num2 = 0;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			for (int i = 0; i < ipaddresses.Length; i++)
			{
				if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SocketConnectionInitiator.CreateTimeoutException(uri, timeoutHelper.OriginalTimeout, ipaddresses, num2, ex));
				}
				AddressFamily addressFamily = ipaddresses[i].AddressFamily;
				if (addressFamily == AddressFamily.InterNetworkV6 && !Socket.OSSupportsIPv6)
				{
					ipaddresses[i] = null;
				}
				else
				{
					DateTime utcNow = DateTime.UtcNow;
					try
					{
						socket = new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
						socket.Connect(new IPEndPoint(ipaddresses[i], num));
						ex = null;
						break;
					}
					catch (SocketException ex2)
					{
						num2++;
						SocketConnectionInitiator.TraceConnectFailure(socket, ex2, uri, DateTime.UtcNow - utcNow);
						ex = ex2;
						socket.Close();
					}
				}
			}
			if (socket == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("NoIPEndpointsFoundForHost", new object[]
				{
					uri.Host
				})));
			}
			if (ex != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SocketConnectionInitiator.ConvertConnectException(ex, uri, timeoutHelper.ElapsedTime(), ex));
			}
			return this.CreateConnection(socket);
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x00125508 File Offset: 0x00123708
		public IAsyncResult BeginConnect(Uri uri, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262187, SR.GetString("TraceCodeInitiatingTcpConnection"), new StringTraceRecord("Uri", uri.ToString()), this, null);
			}
			return new SocketConnectionInitiator.ConnectAsyncResult(uri, timeout, callback, state);
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x00125544 File Offset: 0x00123744
		public IConnection EndConnect(IAsyncResult result)
		{
			Socket socket = SocketConnectionInitiator.ConnectAsyncResult.End(result);
			return this.CreateConnection(socket);
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x00125560 File Offset: 0x00123760
		public static void TraceConnectFailure(Socket socket, SocketException socketException, Uri remoteUri, TimeSpan timeSpentInConnect)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				Exception exception = SocketConnectionInitiator.ConvertConnectException(socketException, remoteUri, timeSpentInConnect, socketException);
				TraceUtility.TraceEvent(TraceEventType.Warning, 262191, SR.GetString("TraceCodeTcpConnectError"), socket, exception);
			}
		}

		// Token: 0x0400318C RID: 12684
		private int bufferSize;

		// Token: 0x0400318D RID: 12685
		private ConnectionBufferPool connectionBufferPool;

		// Token: 0x02000D3B RID: 3387
		private class ConnectAsyncResult : AsyncResult
		{
			// Token: 0x06007C39 RID: 31801 RVA: 0x001D038C File Offset: 0x001CE58C
			public ConnectAsyncResult(Uri uri, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.uri = uri;
				this.addresses = SocketConnectionInitiator.GetIPAddresses(uri);
				this.port = uri.Port;
				if (this.port == -1)
				{
					this.port = 808;
				}
				this.currentIndex = 0;
				this.timeout = timeout;
				this.timeoutHelper = new TimeoutHelper(timeout);
				if (Thread.CurrentThread.IsThreadPoolThread)
				{
					if (this.StartConnect())
					{
						base.Complete(true);
						return;
					}
				}
				else
				{
					if (SocketConnectionInitiator.ConnectAsyncResult.startConnectCallback == null)
					{
						SocketConnectionInitiator.ConnectAsyncResult.startConnectCallback = new Action<object>(SocketConnectionInitiator.ConnectAsyncResult.StartConnectCallback);
					}
					ActionItem.Schedule(SocketConnectionInitiator.ConnectAsyncResult.startConnectCallback, this);
				}
			}

			// Token: 0x06007C3A RID: 31802 RVA: 0x001D0430 File Offset: 0x001CE630
			private static void StartConnectCallback(object state)
			{
				SocketConnectionInitiator.ConnectAsyncResult connectAsyncResult = (SocketConnectionInitiator.ConnectAsyncResult)state;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = connectAsyncResult.StartConnect();
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
					connectAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007C3B RID: 31803 RVA: 0x001D0480 File Offset: 0x001CE680
			private bool StartConnect()
			{
				while (this.currentIndex < this.addresses.Length)
				{
					if (this.timeoutHelper.RemainingTime() == TimeSpan.Zero)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SocketConnectionInitiator.CreateTimeoutException(this.uri, this.timeoutHelper.OriginalTimeout, this.addresses, this.invalidAddressCount, this.lastException));
					}
					AddressFamily addressFamily = this.addresses[this.currentIndex].AddressFamily;
					if (addressFamily != AddressFamily.InterNetworkV6 || Socket.OSSupportsIPv6)
					{
						this.connectStartTime = DateTime.UtcNow;
						try
						{
							IPEndPoint remoteEP = new IPEndPoint(this.addresses[this.currentIndex], this.port);
							this.socket = new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
							IAsyncResult asyncResult = this.socket.BeginConnect(remoteEP, SocketConnectionInitiator.ConnectAsyncResult.onConnect, this);
							if (!asyncResult.CompletedSynchronously)
							{
								return false;
							}
							this.socket.EndConnect(asyncResult);
							return true;
						}
						catch (SocketException exception)
						{
							this.invalidAddressCount++;
							this.TraceConnectFailure(exception);
							this.lastException = exception;
							this.currentIndex++;
						}
						continue;
					}
					IPAddress[] array = this.addresses;
					int num = this.currentIndex;
					this.currentIndex = num + 1;
					array[num] = null;
				}
				if (this.socket == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("NoIPEndpointsFoundForHost", new object[]
					{
						this.uri.Host
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SocketConnectionInitiator.ConvertConnectException(this.lastException, this.uri, this.timeoutHelper.ElapsedTime(), this.lastException));
			}

			// Token: 0x06007C3C RID: 31804 RVA: 0x001D063C File Offset: 0x001CE83C
			private void TraceConnectFailure(SocketException exception)
			{
				SocketConnectionInitiator.TraceConnectFailure(this.socket, exception, this.uri, DateTime.UtcNow - this.connectStartTime);
				this.socket.Close();
			}

			// Token: 0x06007C3D RID: 31805 RVA: 0x001D066C File Offset: 0x001CE86C
			private static void OnConnect(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				bool flag = false;
				Exception exception = null;
				SocketConnectionInitiator.ConnectAsyncResult connectAsyncResult = (SocketConnectionInitiator.ConnectAsyncResult)result.AsyncState;
				try
				{
					connectAsyncResult.socket.EndConnect(result);
					flag = true;
				}
				catch (SocketException exception2)
				{
					connectAsyncResult.TraceConnectFailure(exception2);
					connectAsyncResult.lastException = exception2;
					connectAsyncResult.currentIndex++;
					try
					{
						flag = connectAsyncResult.StartConnect();
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
				}
				if (flag)
				{
					connectAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007C3E RID: 31806 RVA: 0x001D0708 File Offset: 0x001CE908
			public static Socket End(IAsyncResult result)
			{
				SocketConnectionInitiator.ConnectAsyncResult connectAsyncResult = AsyncResult.End<SocketConnectionInitiator.ConnectAsyncResult>(result);
				return connectAsyncResult.socket;
			}

			// Token: 0x04004767 RID: 18279
			private IPAddress[] addresses;

			// Token: 0x04004768 RID: 18280
			private int currentIndex;

			// Token: 0x04004769 RID: 18281
			private int port;

			// Token: 0x0400476A RID: 18282
			private SocketException lastException;

			// Token: 0x0400476B RID: 18283
			private TimeSpan timeout;

			// Token: 0x0400476C RID: 18284
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400476D RID: 18285
			private int invalidAddressCount;

			// Token: 0x0400476E RID: 18286
			private DateTime connectStartTime;

			// Token: 0x0400476F RID: 18287
			private Socket socket;

			// Token: 0x04004770 RID: 18288
			private Uri uri;

			// Token: 0x04004771 RID: 18289
			private static Action<object> startConnectCallback;

			// Token: 0x04004772 RID: 18290
			private static AsyncCallback onConnect = Fx.ThunkCallback(new AsyncCallback(SocketConnectionInitiator.ConnectAsyncResult.OnConnect));
		}
	}
}
