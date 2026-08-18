using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net.WebSockets
{
	// Token: 0x0200023A RID: 570
	internal static class WebSocketProtocolComponent
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x0006FFB0 File Offset: 0x0006E1B0
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		static WebSocketProtocolComponent()
		{
			WebSocketProtocolComponent.s_DllFileName = Path.Combine(Environment.SystemDirectory, "websocket.dll");
			WebSocketProtocolComponent.s_WebSocketDllHandle = SafeLoadLibrary.LoadLibraryEx(WebSocketProtocolComponent.s_DllFileName);
			if (!WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketProtocolComponent.s_SupportedVersion = WebSocketProtocolComponent.GetSupportedVersion();
				WebSocketProtocolComponent.s_ServerFakeRequestHeaders = new WebSocketProtocolComponent.HttpHeader[]
				{
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Connection",
						NameLength = (uint)"Connection".Length,
						Value = "Upgrade",
						ValueLength = (uint)"Upgrade".Length
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Upgrade",
						NameLength = (uint)"Upgrade".Length,
						Value = "websocket",
						ValueLength = (uint)"websocket".Length
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Host",
						NameLength = (uint)"Host".Length,
						Value = string.Empty,
						ValueLength = 0U
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Sec-WebSocket-Version",
						NameLength = (uint)"Sec-WebSocket-Version".Length,
						Value = WebSocketProtocolComponent.s_SupportedVersion,
						ValueLength = (uint)WebSocketProtocolComponent.s_SupportedVersion.Length
					},
					new WebSocketProtocolComponent.HttpHeader
					{
						Name = "Sec-WebSocket-Key",
						NameLength = (uint)"Sec-WebSocket-Key".Length,
						Value = WebSocketProtocolComponent.s_DummyWebsocketKeyBase64,
						ValueLength = (uint)WebSocketProtocolComponent.s_DummyWebsocketKeyBase64.Length
					}
				};
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0007021D File Offset: 0x0006E41D
		internal static string SupportedVersion
		{
			get
			{
				if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
				{
					WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
				}
				return WebSocketProtocolComponent.s_SupportedVersion;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00070235 File Offset: 0x0006E435
		internal static bool IsSupported
		{
			get
			{
				return !WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid;
			}
		}

		// Token: 0x0600158F RID: 5519
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketCreateClientHandle", ExactSpelling = true)]
		private static extern int WebSocketCreateClientHandle_Raw([In] WebSocketProtocolComponent.Property[] properties, [In] uint propertyCount, out SafeWebSocketHandle webSocketHandle);

		// Token: 0x06001590 RID: 5520
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketBeginClientHandshake", ExactSpelling = true)]
		private static extern int WebSocketBeginClientHandshake_Raw([In] SafeHandle webSocketHandle, [In] IntPtr subProtocols, [In] uint subProtocolCount, [In] IntPtr extensions, [In] uint extensionCount, [In] WebSocketProtocolComponent.HttpHeader[] initialHeaders, [In] uint initialHeaderCount, out IntPtr additionalHeadersPtr, out uint additionalHeaderCount);

		// Token: 0x06001591 RID: 5521
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketEndClientHandshake", ExactSpelling = true)]
		private static extern int WebSocketEndClientHandshake_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.HttpHeader[] responseHeaders, [In] uint responseHeaderCount, [In] [Out] IntPtr selectedExtensions, [In] IntPtr selectedExtensionCount, [In] IntPtr selectedSubProtocol);

		// Token: 0x06001592 RID: 5522
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketBeginServerHandshake", ExactSpelling = true)]
		private static extern int WebSocketBeginServerHandshake_Raw([In] SafeHandle webSocketHandle, [In] IntPtr subProtocol, [In] IntPtr extensions, [In] uint extensionCount, [In] WebSocketProtocolComponent.HttpHeader[] requestHeaders, [In] uint requestHeaderCount, out IntPtr responseHeadersPtr, out uint responseHeaderCount);

		// Token: 0x06001593 RID: 5523
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketEndServerHandshake", ExactSpelling = true)]
		private static extern int WebSocketEndServerHandshake_Raw([In] SafeHandle webSocketHandle);

		// Token: 0x06001594 RID: 5524
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketCreateServerHandle", ExactSpelling = true)]
		private static extern int WebSocketCreateServerHandle_Raw([In] WebSocketProtocolComponent.Property[] properties, [In] uint propertyCount, out SafeWebSocketHandle webSocketHandle);

		// Token: 0x06001595 RID: 5525
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketAbortHandle", ExactSpelling = true)]
		private static extern void WebSocketAbortHandle_Raw([In] SafeHandle webSocketHandle);

		// Token: 0x06001596 RID: 5526
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketDeleteHandle", ExactSpelling = true)]
		private static extern void WebSocketDeleteHandle_Raw([In] IntPtr webSocketHandle);

		// Token: 0x06001597 RID: 5527
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketSend", ExactSpelling = true)]
		private static extern int WebSocketSend_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.BufferType bufferType, [In] ref WebSocketProtocolComponent.Buffer buffer, [In] IntPtr applicationContext);

		// Token: 0x06001598 RID: 5528
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketSend", ExactSpelling = true)]
		private static extern int WebSocketSendWithoutBody_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.BufferType bufferType, [In] IntPtr buffer, [In] IntPtr applicationContext);

		// Token: 0x06001599 RID: 5529
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketReceive", ExactSpelling = true)]
		private static extern int WebSocketReceive_Raw([In] SafeHandle webSocketHandle, [In] IntPtr buffers, [In] IntPtr applicationContext);

		// Token: 0x0600159A RID: 5530
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketGetAction", ExactSpelling = true)]
		private static extern int WebSocketGetAction_Raw([In] SafeHandle webSocketHandle, [In] WebSocketProtocolComponent.ActionQueue actionQueue, [In] [Out] WebSocketProtocolComponent.Buffer[] dataBuffers, [In] [Out] ref uint dataBufferCount, out WebSocketProtocolComponent.Action action, out WebSocketProtocolComponent.BufferType bufferType, out IntPtr applicationContext, out IntPtr actionContext);

		// Token: 0x0600159B RID: 5531
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketCompleteAction", ExactSpelling = true)]
		private static extern void WebSocketCompleteAction_Raw([In] SafeHandle webSocketHandle, [In] IntPtr actionContext, [In] uint bytesTransferred);

		// Token: 0x0600159C RID: 5532
		[SuppressUnmanagedCodeSecurity]
		[DllImport("websocket.dll", EntryPoint = "WebSocketGetGlobalProperty", ExactSpelling = true)]
		private static extern int WebSocketGetGlobalProperty_Raw([In] WebSocketProtocolComponent.PropertyType property, [In] [Out] ref uint value, [In] [Out] ref uint size);

		// Token: 0x0600159D RID: 5533 RVA: 0x00070244 File Offset: 0x0006E444
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static string GetSupportedVersion()
		{
			if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			SafeWebSocketHandle safeWebSocketHandle = null;
			string result;
			try
			{
				int errorCode = WebSocketProtocolComponent.WebSocketCreateClientHandle_Raw(null, 0U, out safeWebSocketHandle);
				WebSocketProtocolComponent.ThrowOnError(errorCode);
				if (safeWebSocketHandle == null || safeWebSocketHandle.IsInvalid)
				{
					WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
				}
				IntPtr nativeHeadersPtr;
				uint nativeHeaderCount;
				errorCode = WebSocketProtocolComponent.WebSocketBeginClientHandshake_Raw(safeWebSocketHandle, IntPtr.Zero, 0U, IntPtr.Zero, 0U, WebSocketProtocolComponent.s_InitialClientRequestHeaders, (uint)WebSocketProtocolComponent.s_InitialClientRequestHeaders.Length, out nativeHeadersPtr, out nativeHeaderCount);
				WebSocketProtocolComponent.ThrowOnError(errorCode);
				WebSocketProtocolComponent.HttpHeader[] array = WebSocketProtocolComponent.MarshalHttpHeaders(nativeHeadersPtr, (int)nativeHeaderCount);
				string text = null;
				foreach (WebSocketProtocolComponent.HttpHeader httpHeader in array)
				{
					if (string.Compare(httpHeader.Name, "Sec-WebSocket-Version", StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = httpHeader.Value;
						break;
					}
				}
				result = text;
			}
			finally
			{
				if (safeWebSocketHandle != null)
				{
					safeWebSocketHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0007031C File Offset: 0x0006E51C
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketCreateClientHandle(WebSocketProtocolComponent.Property[] properties, out SafeWebSocketHandle webSocketHandle)
		{
			uint propertyCount = (uint)((properties == null) ? 0 : properties.Length);
			if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			int errorCode = WebSocketProtocolComponent.WebSocketCreateClientHandle_Raw(properties, propertyCount, out webSocketHandle);
			WebSocketProtocolComponent.ThrowOnError(errorCode);
			if (webSocketHandle == null || webSocketHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			IntPtr nativeHeadersPtr;
			uint nativeHeaderCount;
			errorCode = WebSocketProtocolComponent.WebSocketBeginClientHandshake_Raw(webSocketHandle, IntPtr.Zero, 0U, IntPtr.Zero, 0U, WebSocketProtocolComponent.s_InitialClientRequestHeaders, (uint)WebSocketProtocolComponent.s_InitialClientRequestHeaders.Length, out nativeHeadersPtr, out nativeHeaderCount);
			WebSocketProtocolComponent.ThrowOnError(errorCode);
			WebSocketProtocolComponent.HttpHeader[] array = WebSocketProtocolComponent.MarshalHttpHeaders(nativeHeadersPtr, (int)nativeHeaderCount);
			string secWebSocketKey = null;
			foreach (WebSocketProtocolComponent.HttpHeader httpHeader in array)
			{
				if (string.Compare(httpHeader.Name, "Sec-WebSocket-Key", StringComparison.OrdinalIgnoreCase) == 0)
				{
					secWebSocketKey = httpHeader.Value;
					break;
				}
			}
			string secWebSocketAcceptString = WebSocketHelpers.GetSecWebSocketAcceptString(secWebSocketKey);
			WebSocketProtocolComponent.HttpHeader[] array3 = new WebSocketProtocolComponent.HttpHeader[]
			{
				new WebSocketProtocolComponent.HttpHeader
				{
					Name = "Connection",
					NameLength = (uint)"Connection".Length,
					Value = "Upgrade",
					ValueLength = (uint)"Upgrade".Length
				},
				new WebSocketProtocolComponent.HttpHeader
				{
					Name = "Upgrade",
					NameLength = (uint)"Upgrade".Length,
					Value = "websocket",
					ValueLength = (uint)"websocket".Length
				},
				new WebSocketProtocolComponent.HttpHeader
				{
					Name = "Sec-WebSocket-Accept",
					NameLength = (uint)"Sec-WebSocket-Accept".Length,
					Value = secWebSocketAcceptString,
					ValueLength = (uint)secWebSocketAcceptString.Length
				}
			};
			errorCode = WebSocketProtocolComponent.WebSocketEndClientHandshake_Raw(webSocketHandle, array3, (uint)array3.Length, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			WebSocketProtocolComponent.ThrowOnError(errorCode);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000704EC File Offset: 0x0006E6EC
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketCreateServerHandle(WebSocketProtocolComponent.Property[] properties, int propertyCount, out SafeWebSocketHandle webSocketHandle)
		{
			if (WebSocketProtocolComponent.s_WebSocketDllHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			int errorCode = WebSocketProtocolComponent.WebSocketCreateServerHandle_Raw(properties, (uint)propertyCount, out webSocketHandle);
			WebSocketProtocolComponent.ThrowOnError(errorCode);
			if (webSocketHandle == null || webSocketHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			IntPtr nativeHeadersPtr;
			uint nativeHeaderCount;
			errorCode = WebSocketProtocolComponent.WebSocketBeginServerHandshake_Raw(webSocketHandle, IntPtr.Zero, IntPtr.Zero, 0U, WebSocketProtocolComponent.s_ServerFakeRequestHeaders, (uint)WebSocketProtocolComponent.s_ServerFakeRequestHeaders.Length, out nativeHeadersPtr, out nativeHeaderCount);
			WebSocketProtocolComponent.ThrowOnError(errorCode);
			WebSocketProtocolComponent.HttpHeader[] array = WebSocketProtocolComponent.MarshalHttpHeaders(nativeHeadersPtr, (int)nativeHeaderCount);
			errorCode = WebSocketProtocolComponent.WebSocketEndServerHandshake_Raw(webSocketHandle);
			WebSocketProtocolComponent.ThrowOnError(errorCode);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0007056A File Offset: 0x0006E76A
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketAbortHandle(SafeHandle webSocketHandle)
		{
			WebSocketProtocolComponent.WebSocketAbortHandle_Raw(webSocketHandle);
			WebSocketProtocolComponent.DrainActionQueue(webSocketHandle, WebSocketProtocolComponent.ActionQueue.Send);
			WebSocketProtocolComponent.DrainActionQueue(webSocketHandle, WebSocketProtocolComponent.ActionQueue.Receive);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00070580 File Offset: 0x0006E780
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketDeleteHandle(IntPtr webSocketPtr)
		{
			WebSocketProtocolComponent.WebSocketDeleteHandle_Raw(webSocketPtr);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00070588 File Offset: 0x0006E788
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketSend(WebSocketBase webSocket, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Buffer buffer)
		{
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int errorCode;
			try
			{
				errorCode = WebSocketProtocolComponent.WebSocketSend_Raw(webSocket.SessionHandle, bufferType, ref buffer, IntPtr.Zero);
			}
			catch (ObjectDisposedException innerException)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, innerException);
			}
			WebSocketProtocolComponent.ThrowOnError(errorCode);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000705D0 File Offset: 0x0006E7D0
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketSendWithoutBody(WebSocketBase webSocket, WebSocketProtocolComponent.BufferType bufferType)
		{
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int errorCode;
			try
			{
				errorCode = WebSocketProtocolComponent.WebSocketSendWithoutBody_Raw(webSocket.SessionHandle, bufferType, IntPtr.Zero, IntPtr.Zero);
			}
			catch (ObjectDisposedException innerException)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, innerException);
			}
			WebSocketProtocolComponent.ThrowOnError(errorCode);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0007061C File Offset: 0x0006E81C
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketReceive(WebSocketBase webSocket)
		{
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int errorCode;
			try
			{
				errorCode = WebSocketProtocolComponent.WebSocketReceive_Raw(webSocket.SessionHandle, IntPtr.Zero, IntPtr.Zero);
			}
			catch (ObjectDisposedException innerException)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, innerException);
			}
			WebSocketProtocolComponent.ThrowOnError(errorCode);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00070668 File Offset: 0x0006E868
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketGetAction(WebSocketBase webSocket, WebSocketProtocolComponent.ActionQueue actionQueue, WebSocketProtocolComponent.Buffer[] dataBuffers, ref uint dataBufferCount, out WebSocketProtocolComponent.Action action, out WebSocketProtocolComponent.BufferType bufferType, out IntPtr actionContext)
		{
			action = WebSocketProtocolComponent.Action.NoAction;
			bufferType = WebSocketProtocolComponent.BufferType.None;
			actionContext = IntPtr.Zero;
			WebSocketProtocolComponent.ThrowIfSessionHandleClosed(webSocket);
			int errorCode;
			try
			{
				IntPtr intPtr;
				errorCode = WebSocketProtocolComponent.WebSocketGetAction_Raw(webSocket.SessionHandle, actionQueue, dataBuffers, ref dataBufferCount, out action, out bufferType, out intPtr, out actionContext);
			}
			catch (ObjectDisposedException innerException)
			{
				throw WebSocketProtocolComponent.ConvertObjectDisposedException(webSocket, innerException);
			}
			WebSocketProtocolComponent.ThrowOnError(errorCode);
			webSocket.ValidateNativeBuffers(action, bufferType, dataBuffers, dataBufferCount);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000706D4 File Offset: 0x0006E8D4
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void WebSocketCompleteAction(WebSocketBase webSocket, IntPtr actionContext, int bytesTransferred)
		{
			if (webSocket.SessionHandle.IsClosed)
			{
				return;
			}
			try
			{
				WebSocketProtocolComponent.WebSocketCompleteAction_Raw(webSocket.SessionHandle, actionContext, (uint)bytesTransferred);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00070714 File Offset: 0x0006E914
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static TimeSpan WebSocketGetDefaultKeepAliveInterval()
		{
			uint num = 0U;
			uint num2 = 4U;
			int hr = WebSocketProtocolComponent.WebSocketGetGlobalProperty_Raw(WebSocketProtocolComponent.PropertyType.KeepAliveInterval, ref num, ref num2);
			if (!WebSocketProtocolComponent.Succeeded(hr))
			{
				return Timeout.InfiniteTimeSpan;
			}
			return TimeSpan.FromMilliseconds(num);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00070748 File Offset: 0x0006E948
		private static void DrainActionQueue(SafeHandle webSocketHandle, WebSocketProtocolComponent.ActionQueue actionQueue)
		{
			for (;;)
			{
				WebSocketProtocolComponent.Buffer[] dataBuffers = new WebSocketProtocolComponent.Buffer[1];
				uint num = 1U;
				WebSocketProtocolComponent.Action action;
				WebSocketProtocolComponent.BufferType bufferType;
				IntPtr intPtr;
				IntPtr actionContext;
				int hr = WebSocketProtocolComponent.WebSocketGetAction_Raw(webSocketHandle, actionQueue, dataBuffers, ref num, out action, out bufferType, out intPtr, out actionContext);
				if (!WebSocketProtocolComponent.Succeeded(hr))
				{
					break;
				}
				if (action == WebSocketProtocolComponent.Action.NoAction)
				{
					return;
				}
				WebSocketProtocolComponent.WebSocketCompleteAction_Raw(webSocketHandle, actionContext, 0U);
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0007078C File Offset: 0x0006E98C
		private static void MarshalAndVerifyHttpHeader(IntPtr httpHeaderPtr, ref WebSocketProtocolComponent.HttpHeader httpHeader)
		{
			IntPtr intPtr = Marshal.ReadIntPtr(httpHeaderPtr);
			IntPtr ptr = IntPtr.Add(httpHeaderPtr, IntPtr.Size);
			int num = Marshal.ReadInt32(ptr);
			if (intPtr != IntPtr.Zero)
			{
				httpHeader.Name = Marshal.PtrToStringAnsi(intPtr, num);
			}
			if ((httpHeader.Name == null && num != 0) || (httpHeader.Name != null && num != httpHeader.Name.Length))
			{
				throw new AccessViolationException();
			}
			int offset = 2 * IntPtr.Size;
			int offset2 = 3 * IntPtr.Size;
			IntPtr ptr2 = Marshal.ReadIntPtr(IntPtr.Add(httpHeaderPtr, offset));
			ptr = IntPtr.Add(httpHeaderPtr, offset2);
			num = Marshal.ReadInt32(ptr);
			httpHeader.Value = Marshal.PtrToStringAnsi(ptr2, num);
			if ((httpHeader.Value == null && num != 0) || (httpHeader.Value != null && num != httpHeader.Value.Length))
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00070858 File Offset: 0x0006EA58
		private static WebSocketProtocolComponent.HttpHeader[] MarshalHttpHeaders(IntPtr nativeHeadersPtr, int nativeHeaderCount)
		{
			WebSocketProtocolComponent.HttpHeader[] array = new WebSocketProtocolComponent.HttpHeader[nativeHeaderCount];
			int num = 4 * IntPtr.Size;
			for (int i = 0; i < nativeHeaderCount; i++)
			{
				int offset = num * i;
				IntPtr httpHeaderPtr = IntPtr.Add(nativeHeadersPtr, offset);
				WebSocketProtocolComponent.MarshalAndVerifyHttpHeader(httpHeaderPtr, ref array[i]);
			}
			return array;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0007089C File Offset: 0x0006EA9C
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000708A5 File Offset: 0x0006EAA5
		private static void ThrowOnError(int errorCode)
		{
			if (WebSocketProtocolComponent.Succeeded(errorCode))
			{
				return;
			}
			throw new WebSocketException(errorCode);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000708B8 File Offset: 0x0006EAB8
		private static void ThrowIfSessionHandleClosed(WebSocketBase webSocket)
		{
			if (webSocket.SessionHandle.IsClosed)
			{
				throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
				{
					webSocket.GetType().FullName,
					webSocket.State
				}));
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00070906 File Offset: 0x0006EB06
		private static WebSocketException ConvertObjectDisposedException(WebSocketBase webSocket, ObjectDisposedException innerException)
		{
			return new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
			{
				webSocket.GetType().FullName,
				webSocket.State
			}), innerException);
		}

		// Token: 0x040016CE RID: 5838
		private const string WEBSOCKET = "websocket.dll";

		// Token: 0x040016CF RID: 5839
		private static readonly string s_DllFileName;

		// Token: 0x040016D0 RID: 5840
		private static readonly string s_DummyWebsocketKeyBase64 = Convert.ToBase64String(new byte[16]);

		// Token: 0x040016D1 RID: 5841
		private static readonly SafeLoadLibrary s_WebSocketDllHandle;

		// Token: 0x040016D2 RID: 5842
		private static readonly string s_SupportedVersion;

		// Token: 0x040016D3 RID: 5843
		private static readonly WebSocketProtocolComponent.HttpHeader[] s_InitialClientRequestHeaders = new WebSocketProtocolComponent.HttpHeader[]
		{
			new WebSocketProtocolComponent.HttpHeader
			{
				Name = "Connection",
				NameLength = (uint)"Connection".Length,
				Value = "Upgrade",
				ValueLength = (uint)"Upgrade".Length
			},
			new WebSocketProtocolComponent.HttpHeader
			{
				Name = "Upgrade",
				NameLength = (uint)"Upgrade".Length,
				Value = "websocket",
				ValueLength = (uint)"websocket".Length
			}
		};

		// Token: 0x040016D4 RID: 5844
		private static readonly WebSocketProtocolComponent.HttpHeader[] s_ServerFakeRequestHeaders;

		// Token: 0x02000788 RID: 1928
		internal static class Errors
		{
			// Token: 0x04003357 RID: 13143
			internal const int E_INVALID_OPERATION = -2147483568;

			// Token: 0x04003358 RID: 13144
			internal const int E_INVALID_PROTOCOL_OPERATION = -2147483567;

			// Token: 0x04003359 RID: 13145
			internal const int E_INVALID_PROTOCOL_FORMAT = -2147483566;

			// Token: 0x0400335A RID: 13146
			internal const int E_NUMERIC_OVERFLOW = -2147483565;

			// Token: 0x0400335B RID: 13147
			internal const int E_FAIL = -2147467259;
		}

		// Token: 0x02000789 RID: 1929
		internal enum Action
		{
			// Token: 0x0400335D RID: 13149
			NoAction,
			// Token: 0x0400335E RID: 13150
			SendToNetwork,
			// Token: 0x0400335F RID: 13151
			IndicateSendComplete,
			// Token: 0x04003360 RID: 13152
			ReceiveFromNetwork,
			// Token: 0x04003361 RID: 13153
			IndicateReceiveComplete
		}

		// Token: 0x0200078A RID: 1930
		internal enum BufferType : uint
		{
			// Token: 0x04003363 RID: 13155
			None,
			// Token: 0x04003364 RID: 13156
			UTF8Message = 2147483648U,
			// Token: 0x04003365 RID: 13157
			UTF8Fragment,
			// Token: 0x04003366 RID: 13158
			BinaryMessage,
			// Token: 0x04003367 RID: 13159
			BinaryFragment,
			// Token: 0x04003368 RID: 13160
			Close,
			// Token: 0x04003369 RID: 13161
			PingPong,
			// Token: 0x0400336A RID: 13162
			UnsolicitedPong
		}

		// Token: 0x0200078B RID: 1931
		internal enum PropertyType
		{
			// Token: 0x0400336C RID: 13164
			ReceiveBufferSize,
			// Token: 0x0400336D RID: 13165
			SendBufferSize,
			// Token: 0x0400336E RID: 13166
			DisableMasking,
			// Token: 0x0400336F RID: 13167
			AllocatedBuffer,
			// Token: 0x04003370 RID: 13168
			DisableUtf8Verification,
			// Token: 0x04003371 RID: 13169
			KeepAliveInterval
		}

		// Token: 0x0200078C RID: 1932
		internal enum ActionQueue
		{
			// Token: 0x04003373 RID: 13171
			Send = 1,
			// Token: 0x04003374 RID: 13172
			Receive
		}

		// Token: 0x0200078D RID: 1933
		internal struct Property
		{
			// Token: 0x04003375 RID: 13173
			internal WebSocketProtocolComponent.PropertyType Type;

			// Token: 0x04003376 RID: 13174
			internal IntPtr PropertyData;

			// Token: 0x04003377 RID: 13175
			internal uint PropertySize;
		}

		// Token: 0x0200078E RID: 1934
		[StructLayout(LayoutKind.Explicit)]
		internal struct Buffer
		{
			// Token: 0x04003378 RID: 13176
			[FieldOffset(0)]
			internal WebSocketProtocolComponent.DataBuffer Data;

			// Token: 0x04003379 RID: 13177
			[FieldOffset(0)]
			internal WebSocketProtocolComponent.CloseBuffer CloseStatus;
		}

		// Token: 0x0200078F RID: 1935
		internal struct DataBuffer
		{
			// Token: 0x0400337A RID: 13178
			internal IntPtr BufferData;

			// Token: 0x0400337B RID: 13179
			internal uint BufferLength;
		}

		// Token: 0x02000790 RID: 1936
		internal struct CloseBuffer
		{
			// Token: 0x0400337C RID: 13180
			internal IntPtr ReasonData;

			// Token: 0x0400337D RID: 13181
			internal uint ReasonLength;

			// Token: 0x0400337E RID: 13182
			internal ushort CloseStatus;
		}

		// Token: 0x02000791 RID: 1937
		internal struct HttpHeader
		{
			// Token: 0x0400337F RID: 13183
			[MarshalAs(UnmanagedType.LPStr)]
			internal string Name;

			// Token: 0x04003380 RID: 13184
			internal uint NameLength;

			// Token: 0x04003381 RID: 13185
			[MarshalAs(UnmanagedType.LPStr)]
			internal string Value;

			// Token: 0x04003382 RID: 13186
			internal uint ValueLength;
		}
	}
}
