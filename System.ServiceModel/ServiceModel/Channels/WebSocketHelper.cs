using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200087E RID: 2174
	internal static class WebSocketHelper
	{
		// Token: 0x06005251 RID: 21073 RVA: 0x0012F618 File Offset: 0x0012D818
		internal static string ComputeAcceptHeader(string webSocketKey)
		{
			string result;
			using (SHA1 sha = SHA1.Create())
			{
				string s = webSocketKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				result = Convert.ToBase64String(sha.ComputeHash(bytes));
			}
			return result;
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0012F670 File Offset: 0x0012D870
		internal static int ComputeClientBufferSize(long maxReceivedMessageSize)
		{
			return WebSocketHelper.ComputeInternalBufferSize(maxReceivedMessageSize, false);
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0012F679 File Offset: 0x0012D879
		internal static int ComputeServerBufferSize(long maxReceivedMessageSize)
		{
			return WebSocketHelper.ComputeInternalBufferSize(maxReceivedMessageSize, true);
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x0012F684 File Offset: 0x0012D884
		internal static int GetReceiveBufferSize(long maxReceivedMessageSize)
		{
			int val = (maxReceivedMessageSize <= 16384L) ? ((int)maxReceivedMessageSize) : 16384;
			return Math.Max(256, val);
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0012F6AF File Offset: 0x0012D8AF
		internal static bool UseWebSocketTransport(WebSocketTransportUsage transportUsage, bool isContractDuplex)
		{
			return transportUsage == WebSocketTransportUsage.Always || (transportUsage == WebSocketTransportUsage.WhenDuplex && isContractDuplex);
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0012F6C0 File Offset: 0x0012D8C0
		internal static Uri GetWebSocketUri(Uri httpUri)
		{
			UriBuilder uriBuilder = new UriBuilder(httpUri);
			if (Uri.UriSchemeHttp.Equals(httpUri.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				uriBuilder.Scheme = "ws";
			}
			else
			{
				uriBuilder.Scheme = "wss";
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0012F705 File Offset: 0x0012D905
		internal static bool IsWebSocketUri(Uri uri)
		{
			return uri != null && ("ws".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase) || "wss".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0012F738 File Offset: 0x0012D938
		internal static Uri NormalizeWsSchemeWithHttpScheme(Uri uri)
		{
			if (!WebSocketHelper.IsWebSocketUri(uri))
			{
				return uri;
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			string a = uri.Scheme.ToLowerInvariant();
			if (!(a == "ws"))
			{
				if (a == "wss")
				{
					uriBuilder.Scheme = Uri.UriSchemeHttps;
				}
			}
			else
			{
				uriBuilder.Scheme = Uri.UriSchemeHttp;
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x0012F79C File Offset: 0x0012D99C
		internal static bool TryParseSubProtocol(string subProtocolValue, out List<string> subProtocolList)
		{
			subProtocolList = new List<string>();
			if (subProtocolValue != null)
			{
				foreach (string text in subProtocolValue.Split(WebSocketHelper.ProtocolSeparators, StringSplitOptions.RemoveEmptyEntries))
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						text = text.Trim();
						string text2;
						if (WebSocketHelper.IsSubProtocolInvalid(text, out text2))
						{
							FxTrace.Exception.AsWarning(new WebException(SR.GetString("WebSocketInvalidProtocolInvalidCharInProtocolString", new object[]
							{
								text,
								text2
							})));
							return false;
						}
						subProtocolList.Add(text);
					}
				}
			}
			return true;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0012F820 File Offset: 0x0012DA20
		internal static bool IsSubProtocolInvalid(string protocol, out string invalidChar)
		{
			foreach (char c in protocol.ToCharArray())
			{
				if (c < '!' || c > '~')
				{
					invalidChar = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
					{
						(int)c
					});
					return true;
				}
				if (WebSocketHelper.InvalidSeparatorSet.Contains(c))
				{
					invalidChar = c.ToString();
					return true;
				}
			}
			invalidChar = null;
			return false;
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0012F890 File Offset: 0x0012DA90
		internal static string GetCurrentVersion()
		{
			if (WebSocketHelper.currentWebSocketVersion == null)
			{
				WebSocket.RegisterPrefixes();
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("ws://localhost");
				string text = httpWebRequest.Headers["Sec-WebSocket-Version"];
				WebSocketHelper.currentWebSocketVersion = text.Trim();
			}
			return WebSocketHelper.currentWebSocketVersion;
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x0012F8DC File Offset: 0x0012DADC
		internal static WebSocketTransportSettings GetRuntimeWebSocketSettings(WebSocketTransportSettings settings)
		{
			WebSocketTransportSettings webSocketTransportSettings = settings.Clone();
			if (webSocketTransportSettings.MaxPendingConnections == 0)
			{
				webSocketTransportSettings.MaxPendingConnections = WebSocketDefaults.MaxPendingConnectionsCpuCount;
			}
			return webSocketTransportSettings;
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0012F904 File Offset: 0x0012DB04
		internal static bool OSSupportsWebSockets()
		{
			return OSEnvironmentHelper.IsAtLeast(OSVersion.Win8);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0012F90C File Offset: 0x0012DB0C
		internal static void ThrowCorrectException(Exception ex)
		{
			throw WebSocketHelper.ConvertAndTraceException(ex);
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x0012F914 File Offset: 0x0012DB14
		internal static void ThrowCorrectException(Exception ex, TimeSpan timeout, string operation)
		{
			throw WebSocketHelper.ConvertAndTraceException(ex, timeout, operation);
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0012F91E File Offset: 0x0012DB1E
		internal static Exception ConvertAndTraceException(Exception ex)
		{
			return WebSocketHelper.ConvertAndTraceException(ex, TimeSpan.MinValue, null);
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0012F92C File Offset: 0x0012DB2C
		internal static Exception ConvertAndTraceException(Exception ex, TimeSpan timeout, string operation)
		{
			ObjectDisposedException ex2 = ex as ObjectDisposedException;
			if (ex2 != null)
			{
				CommunicationObjectAbortedException ex3 = new CommunicationObjectAbortedException(ex.Message, ex);
				FxTrace.Exception.AsWarning(ex3);
				return ex3;
			}
			AggregateException ex4 = ex as AggregateException;
			if (ex4 == null)
			{
				WebSocketException ex5 = ex as WebSocketException;
				if (ex5 != null)
				{
					WebSocketError webSocketErrorCode = ex5.WebSocketErrorCode;
					if (webSocketErrorCode == WebSocketError.InvalidMessageType || webSocketErrorCode - WebSocketError.UnsupportedVersion <= 1)
					{
						ex = new ProtocolException(ex.Message, ex);
					}
					else
					{
						ex = new CommunicationException(ex.Message, ex);
					}
				}
				return FxTrace.Exception.AsError(ex);
			}
			Exception ex6 = FxTrace.Exception.AsError<OperationCanceledException>(ex4);
			OperationCanceledException ex7 = ex6 as OperationCanceledException;
			if (ex7 != null)
			{
				TimeoutException timeoutException = WebSocketHelper.GetTimeoutException(ex6, timeout, operation);
				FxTrace.Exception.AsWarning(timeoutException);
				return timeoutException;
			}
			Exception ex8 = WebSocketHelper.ConvertAggregateExceptionToCommunicationException(ex4);
			if (ex8 is CommunicationObjectAbortedException)
			{
				FxTrace.Exception.AsWarning(ex8);
				return ex8;
			}
			return FxTrace.Exception.AsError(ex8);
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x0012FA10 File Offset: 0x0012DC10
		internal static Exception ConvertAggregateExceptionToCommunicationException(AggregateException ex)
		{
			Exception ex2 = FxTrace.Exception.AsError<WebSocketException>(ex);
			WebSocketException ex3 = ex2 as WebSocketException;
			if (ex3 != null && ex3.InnerException != null)
			{
				HttpListenerException ex4 = ex3.InnerException as HttpListenerException;
				if (ex4 != null)
				{
					return HttpChannelUtilities.CreateCommunicationException(ex4);
				}
			}
			ObjectDisposedException ex5 = ex2 as ObjectDisposedException;
			if (ex5 != null)
			{
				return new CommunicationObjectAbortedException(ex2.Message, ex2);
			}
			return new CommunicationException(ex2.Message, ex2);
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0012FA74 File Offset: 0x0012DC74
		internal static void ThrowExceptionOnTaskFailure(Task task, TimeSpan timeout, string operation)
		{
			if (task.IsFaulted)
			{
				throw FxTrace.Exception.AsError<CommunicationException>(task.Exception);
			}
			if (task.IsCanceled)
			{
				throw FxTrace.Exception.AsError(WebSocketHelper.GetTimeoutException(null, timeout, operation));
			}
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x0012FAAC File Offset: 0x0012DCAC
		internal static TimeoutException GetTimeoutException(Exception innerException, TimeSpan timeout, string operation)
		{
			string message = string.Empty;
			if (operation != null)
			{
				if (!(operation == "CloseOperation"))
				{
					if (!(operation == "SendOperation"))
					{
						if (!(operation == "ReceiveOperation"))
						{
							message = SR.GetString("WebSocketOperationTimedOut", new object[]
							{
								operation,
								timeout
							});
						}
						else
						{
							message = SR.GetString("WebSocketReceiveTimedOut", new object[]
							{
								timeout
							});
						}
					}
					else
					{
						message = SR.GetString("WebSocketSendTimedOut", new object[]
						{
							timeout
						});
					}
				}
				else
				{
					message = SR.GetString("CloseTimedOut", new object[]
					{
						timeout
					});
				}
			}
			if (innerException != null)
			{
				return new TimeoutException(message, innerException);
			}
			return new TimeoutException(message);
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0012FB74 File Offset: 0x0012DD74
		private static int ComputeInternalBufferSize(long maxReceivedMessageSize, bool isServerBuffer)
		{
			int num = isServerBuffer ? 16 : 16384;
			return 2 * WebSocketHelper.GetReceiveBufferSize(maxReceivedMessageSize) + num + 144 + WebSocketHelper.PropertyBufferSize;
		}

		// Token: 0x04003257 RID: 12887
		internal const int OperationNotStarted = 0;

		// Token: 0x04003258 RID: 12888
		internal const int OperationFinished = 1;

		// Token: 0x04003259 RID: 12889
		internal const string SecWebSocketKey = "Sec-WebSocket-Key";

		// Token: 0x0400325A RID: 12890
		internal const string SecWebSocketVersion = "Sec-WebSocket-Version";

		// Token: 0x0400325B RID: 12891
		internal const string SecWebSocketProtocol = "Sec-WebSocket-Protocol";

		// Token: 0x0400325C RID: 12892
		internal const string SecWebSocketAccept = "Sec-WebSocket-Accept";

		// Token: 0x0400325D RID: 12893
		internal const string MaxPendingConnectionsString = "MaxPendingConnections";

		// Token: 0x0400325E RID: 12894
		internal const string WebSocketTransportSettingsString = "WebSocketTransportSettings";

		// Token: 0x0400325F RID: 12895
		internal const string CloseOperation = "CloseOperation";

		// Token: 0x04003260 RID: 12896
		internal const string SendOperation = "SendOperation";

		// Token: 0x04003261 RID: 12897
		internal const string ReceiveOperation = "ReceiveOperation";

		// Token: 0x04003262 RID: 12898
		internal static readonly char[] ProtocolSeparators = new char[]
		{
			','
		};

		// Token: 0x04003263 RID: 12899
		private const string WebSocketKeyPostString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x04003264 RID: 12900
		private const string SchemeWs = "ws";

		// Token: 0x04003265 RID: 12901
		private const string SchemeWss = "wss";

		// Token: 0x04003266 RID: 12902
		private static readonly int PropertyBufferSize = 2 * Marshal.SizeOf(typeof(uint)) + Marshal.SizeOf(typeof(bool)) + IntPtr.Size;

		// Token: 0x04003267 RID: 12903
		private static readonly HashSet<char> InvalidSeparatorSet = new HashSet<char>(new char[]
		{
			'(',
			')',
			'<',
			'>',
			'@',
			',',
			';',
			':',
			'\\',
			'"',
			'/',
			'[',
			']',
			'?',
			'=',
			'{',
			'}',
			' '
		});

		// Token: 0x04003268 RID: 12904
		private static string currentWebSocketVersion;
	}
}
