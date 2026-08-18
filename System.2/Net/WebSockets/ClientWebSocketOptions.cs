using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.WebSockets
{
	// Token: 0x0200022B RID: 555
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x0006C54C File Offset: 0x0006A74C
		internal ClientWebSocketOptions()
		{
			this.requestedSubProtocols = new List<string>();
			this.requestHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
			this.Proxy = WebRequest.DefaultWebProxy;
			this.receiveBufferSize = 16384;
			this.sendBufferSize = 16384;
			this.keepAliveInterval = WebSocket.DefaultKeepAliveInterval;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0006C5A2 File Offset: 0x0006A7A2
		public void SetRequestHeader(string headerName, string headerValue)
		{
			this.ThrowIfReadOnly();
			this.requestHeaders.Set(headerName, headerValue);
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0006C5B7 File Offset: 0x0006A7B7
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this.requestHeaders;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0006C5BF File Offset: 0x0006A7BF
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0006C5C7 File Offset: 0x0006A7C7
		public bool UseDefaultCredentials
		{
			get
			{
				return this.useDefaultCredentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.useDefaultCredentials = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0006C5D6 File Offset: 0x0006A7D6
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0006C5DE File Offset: 0x0006A7DE
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.credentials = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0006C5ED File Offset: 0x0006A7ED
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0006C5F5 File Offset: 0x0006A7F5
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.proxy = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0006C604 File Offset: 0x0006A804
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0006C61F File Offset: 0x0006A81F
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.clientCertificates = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0006C63C File Offset: 0x0006A83C
		internal X509CertificateCollection InternalClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0006C644 File Offset: 0x0006A844
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x0006C64C File Offset: 0x0006A84C
		public CookieContainer Cookies
		{
			get
			{
				return this.cookies;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.cookies = value;
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0006C65B File Offset: 0x0006A85B
		public void SetBuffer(int receiveBufferSize, int sendBufferSize)
		{
			this.ThrowIfReadOnly();
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			this.buffer = null;
			this.receiveBufferSize = receiveBufferSize;
			this.sendBufferSize = sendBufferSize;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0006C684 File Offset: 0x0006A884
		public void SetBuffer(int receiveBufferSize, int sendBufferSize, ArraySegment<byte> buffer)
		{
			this.ThrowIfReadOnly();
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			WebSocketHelpers.ValidateArraySegment<byte>(buffer, "buffer");
			WebSocketBuffer.Validate(buffer.Count, receiveBufferSize, sendBufferSize, false);
			this.receiveBufferSize = receiveBufferSize;
			this.sendBufferSize = sendBufferSize;
			if (AppDomain.CurrentDomain.IsFullyTrusted)
			{
				this.buffer = new ArraySegment<byte>?(buffer);
				return;
			}
			this.buffer = null;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0006C6EB File Offset: 0x0006A8EB
		internal int ReceiveBufferSize
		{
			get
			{
				return this.receiveBufferSize;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0006C6F3 File Offset: 0x0006A8F3
		internal int SendBufferSize
		{
			get
			{
				return this.sendBufferSize;
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0006C6FB File Offset: 0x0006A8FB
		internal ArraySegment<byte> GetOrCreateBuffer()
		{
			if (this.buffer == null)
			{
				this.buffer = new ArraySegment<byte>?(WebSocket.CreateClientBuffer(this.receiveBufferSize, this.sendBufferSize));
			}
			return this.buffer.Value;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0006C734 File Offset: 0x0006A934
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketHelpers.ValidateSubprotocol(subProtocol);
			foreach (string a in this.requestedSubProtocols)
			{
				if (string.Equals(a, subProtocol, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(SR.GetString("net_WebSockets_NoDuplicateProtocol", new object[]
					{
						subProtocol
					}), "subProtocol");
				}
			}
			this.requestedSubProtocols.Add(subProtocol);
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0006C7BC File Offset: 0x0006A9BC
		internal IList<string> RequestedSubProtocols
		{
			get
			{
				return this.requestedSubProtocols;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0006C7C4 File Offset: 0x0006A9C4
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x0006C7CC File Offset: 0x0006A9CC
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this.keepAliveInterval;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value < Timeout.InfiniteTimeSpan)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[]
					{
						Timeout.InfiniteTimeSpan.ToString()
					}));
				}
				this.keepAliveInterval = value;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0006C82A File Offset: 0x0006AA2A
		internal void SetToReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0006C833 File Offset: 0x0006AA33
		private void ThrowIfReadOnly()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_AlreadyStarted"));
			}
		}

		// Token: 0x0400163C RID: 5692
		private bool isReadOnly;

		// Token: 0x0400163D RID: 5693
		private readonly IList<string> requestedSubProtocols;

		// Token: 0x0400163E RID: 5694
		private readonly WebHeaderCollection requestHeaders;

		// Token: 0x0400163F RID: 5695
		private TimeSpan keepAliveInterval;

		// Token: 0x04001640 RID: 5696
		private int receiveBufferSize;

		// Token: 0x04001641 RID: 5697
		private int sendBufferSize;

		// Token: 0x04001642 RID: 5698
		private ArraySegment<byte>? buffer;

		// Token: 0x04001643 RID: 5699
		private bool useDefaultCredentials;

		// Token: 0x04001644 RID: 5700
		private ICredentials credentials;

		// Token: 0x04001645 RID: 5701
		private IWebProxy proxy;

		// Token: 0x04001646 RID: 5702
		private X509CertificateCollection clientCertificates;

		// Token: 0x04001647 RID: 5703
		private CookieContainer cookies;
	}
}
