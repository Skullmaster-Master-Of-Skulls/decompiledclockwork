using System;
using System.ComponentModel;
using System.Net.WebSockets;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000888 RID: 2184
	[__DynamicallyInvokable]
	public sealed class WebSocketTransportSettings : IEquatable<WebSocketTransportSettings>
	{
		// Token: 0x060052DB RID: 21211 RVA: 0x001314E1 File Offset: 0x0012F6E1
		[__DynamicallyInvokable]
		public WebSocketTransportSettings()
		{
			this.transportUsage = WebSocketTransportUsage.Never;
			this.createNotificationOnConnection = false;
			this.keepAliveInterval = WebSocketDefaults.DefaultKeepAliveInterval;
			this.subProtocol = null;
			this.disablePayloadMasking = false;
			this.maxPendingConnections = 0;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00131518 File Offset: 0x0012F718
		private WebSocketTransportSettings(WebSocketTransportSettings settings)
		{
			this.TransportUsage = settings.TransportUsage;
			this.SubProtocol = settings.SubProtocol;
			this.KeepAliveInterval = settings.KeepAliveInterval;
			this.DisablePayloadMasking = settings.DisablePayloadMasking;
			this.CreateNotificationOnConnection = settings.CreateNotificationOnConnection;
			this.MaxPendingConnections = settings.MaxPendingConnections;
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x00131573 File Offset: 0x0012F773
		// (set) Token: 0x060052DE RID: 21214 RVA: 0x0013157B File Offset: 0x0012F77B
		[DefaultValue(WebSocketTransportUsage.Never)]
		[__DynamicallyInvokable]
		public WebSocketTransportUsage TransportUsage
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transportUsage;
			}
			[__DynamicallyInvokable]
			set
			{
				WebSocketTransportUsageHelper.Validate(value);
				this.transportUsage = value;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x0013158A File Offset: 0x0012F78A
		// (set) Token: 0x060052E0 RID: 21216 RVA: 0x00131592 File Offset: 0x0012F792
		[DefaultValue(false)]
		public bool CreateNotificationOnConnection
		{
			get
			{
				return this.createNotificationOnConnection;
			}
			set
			{
				this.createNotificationOnConnection = value;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x0013159B File Offset: 0x0012F79B
		// (set) Token: 0x060052E2 RID: 21218 RVA: 0x001315A4 File Offset: 0x0012F7A4
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[__DynamicallyInvokable]
		public TimeSpan KeepAliveInterval
		{
			[__DynamicallyInvokable]
			get
			{
				return this.keepAliveInterval;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < TimeSpan.Zero && value != Timeout.InfiniteTimeSpan)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.keepAliveInterval = value;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x00131624 File Offset: 0x0012F824
		// (set) Token: 0x060052E4 RID: 21220 RVA: 0x0013162C File Offset: 0x0012F82C
		[DefaultValue(null)]
		[__DynamicallyInvokable]
		public string SubProtocol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.subProtocol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					if (value == string.Empty)
					{
						throw FxTrace.Exception.Argument("value", SR.GetString("WebSocketInvalidProtocolEmptySubprotocolString"));
					}
					if (value.Split(WebSocketHelper.ProtocolSeparators).Length > 1)
					{
						throw FxTrace.Exception.Argument("value", SR.GetString("WebSocketInvalidProtocolContainsMultipleSubProtocolString", new object[]
						{
							value
						}));
					}
					string text;
					if (WebSocketHelper.IsSubProtocolInvalid(value, out text))
					{
						throw FxTrace.Exception.Argument("value", SR.GetString("WebSocketInvalidProtocolInvalidCharInProtocolString", new object[]
						{
							value,
							text
						}));
					}
				}
				this.subProtocol = value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x060052E5 RID: 21221 RVA: 0x001316D3 File Offset: 0x0012F8D3
		// (set) Token: 0x060052E6 RID: 21222 RVA: 0x001316DB File Offset: 0x0012F8DB
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool DisablePayloadMasking
		{
			[__DynamicallyInvokable]
			get
			{
				return this.disablePayloadMasking;
			}
			[__DynamicallyInvokable]
			set
			{
				this.disablePayloadMasking = value;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x001316E4 File Offset: 0x0012F8E4
		// (set) Token: 0x060052E8 RID: 21224 RVA: 0x001316EC File Offset: 0x0012F8EC
		[DefaultValue(0)]
		public int MaxPendingConnections
		{
			get
			{
				return this.maxPendingConnections;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxPendingConnections = value;
			}
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00131720 File Offset: 0x0012F920
		[__DynamicallyInvokable]
		public bool Equals(WebSocketTransportSettings other)
		{
			return other != null && (this.TransportUsage == other.TransportUsage && this.CreateNotificationOnConnection == other.CreateNotificationOnConnection && this.KeepAliveInterval == other.KeepAliveInterval && this.DisablePayloadMasking == other.DisablePayloadMasking && StringComparer.OrdinalIgnoreCase.Compare(this.SubProtocol, other.SubProtocol) == 0) && this.MaxPendingConnections == other.MaxPendingConnections;
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00131798 File Offset: 0x0012F998
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return base.Equals(obj);
			}
			WebSocketTransportSettings other = obj as WebSocketTransportSettings;
			return this.Equals(other);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x001317C0 File Offset: 0x0012F9C0
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.TransportUsage.GetHashCode() ^ this.CreateNotificationOnConnection.GetHashCode() ^ this.KeepAliveInterval.GetHashCode() ^ this.DisablePayloadMasking.GetHashCode() ^ this.MaxPendingConnections.GetHashCode();
			if (this.SubProtocol != null)
			{
				num ^= this.SubProtocol.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x00131841 File Offset: 0x0012FA41
		internal WebSocketTransportSettings Clone()
		{
			return new WebSocketTransportSettings(this);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00131849 File Offset: 0x0012FA49
		internal TimeSpan GetEffectiveKeepAliveInterval()
		{
			if (!(this.keepAliveInterval == TimeSpan.Zero))
			{
				return this.keepAliveInterval;
			}
			return WebSocket.DefaultKeepAliveInterval;
		}

		// Token: 0x0400328E RID: 12942
		public const string ConnectionOpenedAction = "http://schemas.microsoft.com/2011/02/session/onopen";

		// Token: 0x0400328F RID: 12943
		[__DynamicallyInvokable]
		public const string BinaryMessageReceivedAction = "http://schemas.microsoft.com/2011/02/websockets/onbinarymessage";

		// Token: 0x04003290 RID: 12944
		[__DynamicallyInvokable]
		public const string TextMessageReceivedAction = "http://schemas.microsoft.com/2011/02/websockets/ontextmessage";

		// Token: 0x04003291 RID: 12945
		public const string SoapContentTypeHeader = "soap-content-type";

		// Token: 0x04003292 RID: 12946
		public const string BinaryEncoderTransferModeHeader = "microsoft-binary-transfer-mode";

		// Token: 0x04003293 RID: 12947
		internal const string WebSocketMethod = "WEBSOCKET";

		// Token: 0x04003294 RID: 12948
		internal const string SoapSubProtocol = "soap";

		// Token: 0x04003295 RID: 12949
		internal const string TransportUsageMethodName = "TransportUsage";

		// Token: 0x04003296 RID: 12950
		private WebSocketTransportUsage transportUsage;

		// Token: 0x04003297 RID: 12951
		private bool createNotificationOnConnection;

		// Token: 0x04003298 RID: 12952
		private TimeSpan keepAliveInterval;

		// Token: 0x04003299 RID: 12953
		private string subProtocol;

		// Token: 0x0400329A RID: 12954
		private bool disablePayloadMasking;

		// Token: 0x0400329B RID: 12955
		private int maxPendingConnections;
	}
}
