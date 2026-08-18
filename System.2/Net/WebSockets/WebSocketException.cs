using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.WebSockets
{
	// Token: 0x02000236 RID: 566
	[Serializable]
	public sealed class WebSocketException : Win32Exception
	{
		// Token: 0x0600153B RID: 5435 RVA: 0x0006EAFD File Offset: 0x0006CCFD
		public WebSocketException() : this(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0006EB0A File Offset: 0x0006CD0A
		public WebSocketException(WebSocketError error) : this(error, WebSocketException.GetErrorMessage(error))
		{
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0006EB19 File Offset: 0x0006CD19
		public WebSocketException(WebSocketError error, string message) : base(message)
		{
			this.m_WebSocketErrorCode = error;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0006EB29 File Offset: 0x0006CD29
		public WebSocketException(WebSocketError error, Exception innerException) : this(error, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0006EB39 File Offset: 0x0006CD39
		public WebSocketException(WebSocketError error, string message, Exception innerException) : base(message, innerException)
		{
			this.m_WebSocketErrorCode = error;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0006EB4A File Offset: 0x0006CD4A
		public WebSocketException(int nativeError) : base(nativeError)
		{
			this.m_WebSocketErrorCode = ((!WebSocketProtocolComponent.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0006EB6C File Offset: 0x0006CD6C
		public WebSocketException(int nativeError, string message) : base(nativeError, message)
		{
			this.m_WebSocketErrorCode = ((!WebSocketProtocolComponent.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0006EB8F File Offset: 0x0006CD8F
		public WebSocketException(int nativeError, Exception innerException) : base(SR.GetString("net_WebSockets_Generic"), innerException)
		{
			this.m_WebSocketErrorCode = ((!WebSocketProtocolComponent.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0006EBBB File Offset: 0x0006CDBB
		public WebSocketException(WebSocketError error, int nativeError) : this(error, nativeError, WebSocketException.GetErrorMessage(error))
		{
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0006EBCB File Offset: 0x0006CDCB
		public WebSocketException(WebSocketError error, int nativeError, string message) : base(message)
		{
			this.m_WebSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0006EBE2 File Offset: 0x0006CDE2
		public WebSocketException(WebSocketError error, int nativeError, Exception innerException) : this(error, nativeError, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0006EBF3 File Offset: 0x0006CDF3
		public WebSocketException(WebSocketError error, int nativeError, string message, Exception innerException) : base(message, innerException)
		{
			this.m_WebSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0006EC0C File Offset: 0x0006CE0C
		public WebSocketException(string message) : base(message)
		{
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0006EC15 File Offset: 0x0006CE15
		public WebSocketException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0006EC1F File Offset: 0x0006CE1F
		private WebSocketException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0006EC29 File Offset: 0x0006CE29
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0006EC31 File Offset: 0x0006CE31
		public WebSocketError WebSocketErrorCode
		{
			get
			{
				return this.m_WebSocketErrorCode;
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0006EC3C File Offset: 0x0006CE3C
		private static string GetErrorMessage(WebSocketError error)
		{
			switch (error)
			{
			case WebSocketError.InvalidMessageType:
				return SR.GetString("net_WebSockets_InvalidMessageType_Generic", new object[]
				{
					typeof(WebSocket).Name + "CloseAsync",
					typeof(WebSocket).Name + "CloseOutputAsync"
				});
			case WebSocketError.Faulted:
				return SR.GetString("net_Websockets_WebSocketBaseFaulted");
			case WebSocketError.NotAWebSocket:
				return SR.GetString("net_WebSockets_NotAWebSocket_Generic");
			case WebSocketError.UnsupportedVersion:
				return SR.GetString("net_WebSockets_UnsupportedWebSocketVersion_Generic");
			case WebSocketError.UnsupportedProtocol:
				return SR.GetString("net_WebSockets_UnsupportedProtocol_Generic");
			case WebSocketError.HeaderError:
				return SR.GetString("net_WebSockets_HeaderError_Generic");
			case WebSocketError.ConnectionClosedPrematurely:
				return SR.GetString("net_WebSockets_ConnectionClosedPrematurely_Generic");
			case WebSocketError.InvalidState:
				return SR.GetString("net_WebSockets_InvalidState_Generic");
			}
			return SR.GetString("net_WebSockets_Generic");
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0006ED1A File Offset: 0x0006CF1A
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("WebSocketErrorCode", this.m_WebSocketErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0006ED48 File Offset: 0x0006CF48
		private void SetErrorCodeOnError(int nativeError)
		{
			if (!WebSocketProtocolComponent.Succeeded(nativeError))
			{
				base.HResult = nativeError;
			}
		}

		// Token: 0x040016AD RID: 5805
		private WebSocketError m_WebSocketErrorCode;
	}
}
