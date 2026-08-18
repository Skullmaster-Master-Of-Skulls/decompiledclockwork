using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.WebSockets;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000889 RID: 2185
	public sealed class WebSocketMessageProperty
	{
		// Token: 0x060052EE RID: 21230 RVA: 0x00131869 File Offset: 0x0012FA69
		public WebSocketMessageProperty()
		{
			this.messageType = WebSocketMessageType.Binary;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00131878 File Offset: 0x0012FA78
		internal WebSocketMessageProperty(WebSocketContext context, string subProtocol, WebSocketMessageType incomingMessageType, ReadOnlyDictionary<string, object> properties)
		{
			this.context = context;
			this.subProtocol = subProtocol;
			this.messageType = incomingMessageType;
			this.properties = properties;
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x060052F0 RID: 21232 RVA: 0x0013189D File Offset: 0x0012FA9D
		public WebSocketContext WebSocketContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x001318A5 File Offset: 0x0012FAA5
		public string SubProtocol
		{
			get
			{
				return this.subProtocol;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x001318AD File Offset: 0x0012FAAD
		// (set) Token: 0x060052F3 RID: 21235 RVA: 0x001318B5 File Offset: 0x0012FAB5
		public WebSocketMessageType MessageType
		{
			get
			{
				return this.messageType;
			}
			set
			{
				this.messageType = value;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x001318C0 File Offset: 0x0012FAC0
		public ReadOnlyDictionary<string, object> OpeningHandshakeProperties
		{
			get
			{
				if (this.properties == null)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WebSocketOpeningHandshakePropertiesNotAvailable", new object[]
					{
						"RequestMessage",
						typeof(HttpResponseMessage).Name,
						typeof(DelegatingHandler).Name
					})));
				}
				return this.properties;
			}
		}

		// Token: 0x0400329C RID: 12956
		public const string Name = "WebSocketMessageProperty";

		// Token: 0x0400329D RID: 12957
		private WebSocketContext context;

		// Token: 0x0400329E RID: 12958
		private string subProtocol;

		// Token: 0x0400329F RID: 12959
		private WebSocketMessageType messageType;

		// Token: 0x040032A0 RID: 12960
		private ReadOnlyDictionary<string, object> properties;
	}
}
