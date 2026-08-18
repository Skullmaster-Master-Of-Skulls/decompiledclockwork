using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000237 RID: 567
	internal static class WebSocketHelpers
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0006ED59 File Offset: 0x0006CF59
		internal static ArraySegment<byte> EmptyPayload
		{
			get
			{
				return WebSocketHelpers.s_EmptyPayload;
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0006ED60 File Offset: 0x0006CF60
		internal static Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(HttpListenerContext context, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, 16, keepAliveInterval);
			WebSocketHelpers.ValidateArraySegment<byte>(internalBuffer, "internalBuffer");
			WebSocketBuffer.Validate(internalBuffer.Count, receiveBufferSize, 16, true);
			return WebSocketHelpers.AcceptWebSocketAsyncCore(context, subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0006ED94 File Offset: 0x0006CF94
		private static Task<HttpListenerWebSocketContext> AcceptWebSocketAsyncCore(HttpListenerContext context, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			WebSocketHelpers.<AcceptWebSocketAsyncCore>d__17 <AcceptWebSocketAsyncCore>d__;
			<AcceptWebSocketAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder<HttpListenerWebSocketContext>.Create();
			<AcceptWebSocketAsyncCore>d__.context = context;
			<AcceptWebSocketAsyncCore>d__.subProtocol = subProtocol;
			<AcceptWebSocketAsyncCore>d__.receiveBufferSize = receiveBufferSize;
			<AcceptWebSocketAsyncCore>d__.keepAliveInterval = keepAliveInterval;
			<AcceptWebSocketAsyncCore>d__.internalBuffer = internalBuffer;
			<AcceptWebSocketAsyncCore>d__.<>1__state = -1;
			<AcceptWebSocketAsyncCore>d__.<>t__builder.Start<WebSocketHelpers.<AcceptWebSocketAsyncCore>d__17>(ref <AcceptWebSocketAsyncCore>d__);
			return <AcceptWebSocketAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0006EDF8 File Offset: 0x0006CFF8
		internal static string GetSecWebSocketAcceptString(string secWebSocketKey)
		{
			string result;
			using (SHA1 sha = SHA1.Create())
			{
				string s = secWebSocketKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				result = Convert.ToBase64String(sha.ComputeHash(bytes));
			}
			return result;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0006EE50 File Offset: 0x0006D050
		internal static string GetTraceMsgForParameters(int offset, int count, CancellationToken cancellationToken)
		{
			return string.Format(CultureInfo.InvariantCulture, "offset: {0}, count: {1}, cancellationToken.CanBeCanceled: {2}", new object[]
			{
				offset,
				count,
				cancellationToken.CanBeCanceled
			});
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0006EE88 File Offset: 0x0006D088
		internal static bool ProcessWebSocketProtocolHeader(string clientSecWebSocketProtocol, string subProtocol, out string acceptProtocol)
		{
			acceptProtocol = string.Empty;
			if (string.IsNullOrEmpty(clientSecWebSocketProtocol))
			{
				if (subProtocol != null)
				{
					throw new WebSocketException(WebSocketError.UnsupportedProtocol, SR.GetString("net_WebSockets_ClientAcceptingNoProtocols", new object[]
					{
						subProtocol
					}));
				}
				return false;
			}
			else
			{
				if (subProtocol == null)
				{
					return true;
				}
				string[] array = clientSecWebSocketProtocol.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				acceptProtocol = subProtocol;
				for (int i = 0; i < array.Length; i++)
				{
					string strB = array[i].Trim();
					if (string.Compare(acceptProtocol, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				throw new WebSocketException(WebSocketError.UnsupportedProtocol, SR.GetString("net_WebSockets_AcceptUnsupportedProtocol", new object[]
				{
					clientSecWebSocketProtocol,
					subProtocol
				}));
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0006EF21 File Offset: 0x0006D121
		internal static ConfiguredTaskAwaitable SuppressContextFlow(this Task task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0006EF2A File Offset: 0x0006D12A
		internal static ConfiguredTaskAwaitable<T> SuppressContextFlow<T>(this Task<T> task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0006EF33 File Offset: 0x0006D133
		internal static void ValidateBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0006EF6F File Offset: 0x0006D16F
		private static ulong SendWebSocketHeaders(HttpListenerResponse response)
		{
			return (ulong)response.SendHeaders(null, null, UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA | UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_BUFFER_DATA | UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_OPAQUE, true);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0006EF80 File Offset: 0x0006D180
		private static void ValidateWebSocketHeaders(HttpListenerContext context)
		{
			WebSocketHelpers.EnsureHttpSysSupportsWebSockets();
			if (!context.Request.IsWebSocketRequest)
			{
				throw new WebSocketException(WebSocketError.NotAWebSocket, SR.GetString("net_WebSockets_AcceptNotAWebSocket", new object[]
				{
					"ValidateWebSocketHeaders",
					"Connection",
					"Upgrade",
					"websocket",
					context.Request.Headers["Upgrade"]
				}));
			}
			string text = context.Request.Headers["Sec-WebSocket-Version"];
			if (string.IsNullOrEmpty(text))
			{
				throw new WebSocketException(WebSocketError.HeaderError, SR.GetString("net_WebSockets_AcceptHeaderNotFound", new object[]
				{
					"ValidateWebSocketHeaders",
					"Sec-WebSocket-Version"
				}));
			}
			if (string.Compare(text, WebSocketProtocolComponent.SupportedVersion, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new WebSocketException(WebSocketError.UnsupportedVersion, SR.GetString("net_WebSockets_AcceptUnsupportedWebSocketVersion", new object[]
				{
					"ValidateWebSocketHeaders",
					text,
					WebSocketProtocolComponent.SupportedVersion
				}));
			}
			if (string.IsNullOrWhiteSpace(context.Request.Headers["Sec-WebSocket-Key"]))
			{
				throw new WebSocketException(WebSocketError.HeaderError, SR.GetString("net_WebSockets_AcceptHeaderNotFound", new object[]
				{
					"ValidateWebSocketHeaders",
					"Sec-WebSocket-Key"
				}));
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0006F0B0 File Offset: 0x0006D2B0
		internal static void PrepareWebRequest(ref HttpWebRequest request)
		{
			request.Connection = "Upgrade";
			request.Headers["Upgrade"] = "websocket";
			byte[] array = new byte[16];
			Random obj = WebSocketHelpers.s_KeyGenerator;
			lock (obj)
			{
				WebSocketHelpers.s_KeyGenerator.NextBytes(array);
			}
			request.Headers["Sec-WebSocket-Key"] = Convert.ToBase64String(array);
			if (WebSocketProtocolComponent.IsSupported)
			{
				request.Headers["Sec-WebSocket-Version"] = WebSocketProtocolComponent.SupportedVersion;
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0006F154 File Offset: 0x0006D354
		internal static void ValidateSubprotocol(string subProtocol)
		{
			if (string.IsNullOrWhiteSpace(subProtocol))
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidEmptySubProtocol"), "subProtocol");
			}
			char[] array = subProtocol.ToCharArray();
			string text = null;
			foreach (char c in array)
			{
				if (c < '!' || c > '~')
				{
					text = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
					{
						(int)c
					});
					break;
				}
				if (!char.IsLetterOrDigit(c) && "()<>@,;:\\\"/[]?={} ".IndexOf(c) >= 0)
				{
					text = c.ToString();
					break;
				}
			}
			if (text != null)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidCharInProtocolString", new object[]
				{
					subProtocol,
					text
				}), "subProtocol");
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0006F20C File Offset: 0x0006D40C
		internal static void ValidateCloseStatus(WebSocketCloseStatus closeStatus, string statusDescription)
		{
			if (closeStatus == WebSocketCloseStatus.Empty && !string.IsNullOrEmpty(statusDescription))
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_ReasonNotNull", new object[]
				{
					statusDescription,
					WebSocketCloseStatus.Empty
				}), "statusDescription");
			}
			if ((closeStatus >= (WebSocketCloseStatus)0 && closeStatus <= (WebSocketCloseStatus)999) || closeStatus == (WebSocketCloseStatus)1006 || closeStatus == (WebSocketCloseStatus)1015)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidCloseStatusCode", new object[]
				{
					(int)closeStatus
				}), "closeStatus");
			}
			int num = 0;
			if (!string.IsNullOrEmpty(statusDescription))
			{
				num = Encoding.UTF8.GetByteCount(statusDescription);
			}
			if (num > 123)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_InvalidCloseStatusDescription", new object[]
				{
					statusDescription,
					123
				}), "statusDescription");
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0006F2DC File Offset: 0x0006D4DC
		internal static void ValidateOptions(string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval)
		{
			if (subProtocol != null)
			{
				WebSocketHelpers.ValidateSubprotocol(subProtocol);
			}
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			if (keepAliveInterval < Timeout.InfiniteTimeSpan)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[]
				{
					Timeout.InfiniteTimeSpan.ToString()
				}));
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0006F340 File Offset: 0x0006D540
		internal static void ValidateBufferSizes(int receiveBufferSize, int sendBufferSize)
		{
			if (receiveBufferSize < 256)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[]
				{
					256
				}));
			}
			if (sendBufferSize < 16)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[]
				{
					16
				}));
			}
			if (receiveBufferSize > 65536)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooBig", new object[]
				{
					"receiveBufferSize",
					receiveBufferSize,
					65536
				}));
			}
			if (sendBufferSize > 65536)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooBig", new object[]
				{
					"sendBufferSize",
					sendBufferSize,
					65536
				}));
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0006F444 File Offset: 0x0006D644
		internal static void ValidateInnerStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead)
			{
				throw new ArgumentException(SR.GetString("NotReadableStream"), "innerStream");
			}
			if (!innerStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("NotWriteableStream"), "innerStream");
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0006F499 File Offset: 0x0006D699
		internal static void ThrowIfConnectionAborted(Stream connection, bool read)
		{
			if ((!read && !connection.CanWrite) || (read && !connection.CanRead))
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0006F4B8 File Offset: 0x0006D6B8
		internal static void ThrowPlatformNotSupportedException_WSPC()
		{
			throw new PlatformNotSupportedException(SR.GetString("net_WebSockets_UnsupportedPlatform"));
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0006F4C9 File Offset: 0x0006D6C9
		private static void ThrowPlatformNotSupportedException_HTTPSYS()
		{
			throw new PlatformNotSupportedException(SR.GetString("net_WebSockets_UnsupportedPlatform"));
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0006F4DC File Offset: 0x0006D6DC
		internal static void ValidateArraySegment<T>(ArraySegment<T> arraySegment, string parameterName)
		{
			if (arraySegment.Array == null)
			{
				throw new ArgumentNullException(parameterName + ".Array");
			}
			if (arraySegment.Offset < 0 || arraySegment.Offset > arraySegment.Array.Length)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Offset");
			}
			if (arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Count");
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0006F565 File Offset: 0x0006D765
		private static void EnsureHttpSysSupportsWebSockets()
		{
			if (!WebSocketHelpers.s_HttpSysSupportsWebSockets)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_HTTPSYS();
			}
		}

		// Token: 0x040016AE RID: 5806
		internal const string SecWebSocketKeyGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x040016AF RID: 5807
		internal const string WebSocketUpgradeToken = "websocket";

		// Token: 0x040016B0 RID: 5808
		internal const int DefaultReceiveBufferSize = 16384;

		// Token: 0x040016B1 RID: 5809
		internal const int DefaultClientSendBufferSize = 16384;

		// Token: 0x040016B2 RID: 5810
		internal const int MaxControlFramePayloadLength = 123;

		// Token: 0x040016B3 RID: 5811
		internal const int ClientTcpCloseTimeout = 1000;

		// Token: 0x040016B4 RID: 5812
		private const int CloseStatusCodeAbort = 1006;

		// Token: 0x040016B5 RID: 5813
		private const int CloseStatusCodeFailedTLSHandshake = 1015;

		// Token: 0x040016B6 RID: 5814
		private const int InvalidCloseStatusCodesFrom = 0;

		// Token: 0x040016B7 RID: 5815
		private const int InvalidCloseStatusCodesTo = 999;

		// Token: 0x040016B8 RID: 5816
		private const string Separators = "()<>@,;:\\\"/[]?={} ";

		// Token: 0x040016B9 RID: 5817
		private static readonly ArraySegment<byte> s_EmptyPayload = new ArraySegment<byte>(new byte[0], 0, 0);

		// Token: 0x040016BA RID: 5818
		private static readonly Random s_KeyGenerator = new Random();

		// Token: 0x040016BB RID: 5819
		private static volatile bool s_HttpSysSupportsWebSockets = ComNetOS.IsWin8orLater;

		// Token: 0x02000780 RID: 1920
		internal static class MethodNames
		{
			// Token: 0x04003306 RID: 13062
			internal const string AcceptWebSocketAsync = "AcceptWebSocketAsync";

			// Token: 0x04003307 RID: 13063
			internal const string ValidateWebSocketHeaders = "ValidateWebSocketHeaders";
		}
	}
}
