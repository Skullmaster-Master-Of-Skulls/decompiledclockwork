using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication.ExtendedProtection;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000867 RID: 2151
	internal class HttpRequestMessageHttpInput : HttpInput, HttpRequestMessageProperty.IHttpHeaderProvider
	{
		// Token: 0x060050F6 RID: 20726 RVA: 0x00129E6A File Offset: 0x0012806A
		public HttpRequestMessageHttpInput(HttpRequestMessage httpRequestMessage, IHttpTransportFactorySettings settings, bool enableChannelBinding, ChannelBinding channelBinding) : base(settings, true, enableChannelBinding)
		{
			this.httpRequestMessage = httpRequestMessage;
			this.channelBinding = channelBinding;
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x00129E84 File Offset: 0x00128084
		public override long ContentLength
		{
			get
			{
				if (this.httpRequestMessage.Content.Headers.ContentLength == null)
				{
					return -1L;
				}
				return this.httpRequestMessage.Content.Headers.ContentLength.Value;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060050F8 RID: 20728 RVA: 0x00129ED0 File Offset: 0x001280D0
		protected override ChannelBinding ChannelBinding
		{
			get
			{
				return this.channelBinding;
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060050F9 RID: 20729 RVA: 0x00129ED8 File Offset: 0x001280D8
		public HttpRequestMessage HttpRequestMessage
		{
			get
			{
				return this.httpRequestMessage;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x00129EE0 File Offset: 0x001280E0
		protected override bool HasContent
		{
			get
			{
				return this.httpRequestMessage.Content.Headers.ContentLength == null || this.httpRequestMessage.Content.Headers.ContentLength.Value > 0L;
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x00129F2F File Offset: 0x0012812F
		protected override string ContentTypeCore
		{
			get
			{
				if (!this.HasContent)
				{
					return null;
				}
				if (this.httpRequestMessage.Content.Headers.ContentType != null)
				{
					return this.httpRequestMessage.Content.Headers.ContentType.MediaType;
				}
				return null;
			}
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x00129F6E File Offset: 0x0012816E
		public override void ConfigureHttpRequestMessage(HttpRequestMessage message)
		{
			throw FxTrace.Exception.AsError(new InvalidOperationException());
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00129F7F File Offset: 0x0012817F
		protected override Stream GetInputStream()
		{
			if (this.httpRequestMessage.Content == null)
			{
				return Stream.Null;
			}
			return this.httpRequestMessage.Content.ReadAsStreamAsync().Result;
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x00129FAC File Offset: 0x001281AC
		protected override void AddProperties(Message message)
		{
			HttpRequestMessageProperty property = new HttpRequestMessageProperty(this.httpRequestMessage);
			message.Properties.Add(HttpRequestMessageProperty.Name, property);
			message.Properties.Via = this.httpRequestMessage.RequestUri;
			foreach (KeyValuePair<string, object> keyValuePair in this.httpRequestMessage.Properties)
			{
				message.Properties.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.httpRequestMessage.Properties.Clear();
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x0012A054 File Offset: 0x00128254
		protected override string SoapActionHeader
		{
			get
			{
				IEnumerable<string> enumerable;
				if (this.httpRequestMessage.Headers.TryGetValues("SOAPAction", out enumerable))
				{
					using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							return enumerator.Current;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x0012A0B8 File Offset: 0x001282B8
		public void CopyHeaders(WebHeaderCollection headers)
		{
			HttpChannelUtilities.CopyHeaders(this.httpRequestMessage, new AddHeaderDelegate(headers.Add));
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0012A0D2 File Offset: 0x001282D2
		internal void SetHttpRequestMessage(HttpRequestMessage httpRequestMessage)
		{
			this.httpRequestMessage = httpRequestMessage;
		}

		// Token: 0x040031EE RID: 12782
		private const string SoapAction = "SOAPAction";

		// Token: 0x040031EF RID: 12783
		private HttpRequestMessage httpRequestMessage;

		// Token: 0x040031F0 RID: 12784
		private ChannelBinding channelBinding;
	}
}
