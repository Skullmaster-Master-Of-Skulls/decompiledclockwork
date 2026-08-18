using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200086E RID: 2158
	[__DynamicallyInvokable]
	public sealed class HttpResponseMessageProperty : IMessageProperty, IMergeEnabledMessageProperty
	{
		// Token: 0x06005195 RID: 20885 RVA: 0x0012C1F9 File Offset: 0x0012A3F9
		[__DynamicallyInvokable]
		public HttpResponseMessageProperty() : this(null)
		{
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x0012C202 File Offset: 0x0012A402
		internal HttpResponseMessageProperty(WebHeaderCollection originalHeaders)
		{
			this.traditionalProperty = new HttpResponseMessageProperty.TraditionalHttpResponseMessageProperty(originalHeaders);
			this.useHttpBackedProperty = false;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x0012C21D File Offset: 0x0012A41D
		internal HttpResponseMessageProperty(HttpResponseMessage httpResponseMessage)
		{
			this.httpBackedProperty = new HttpResponseMessageProperty.HttpResponseMessageBackedProperty(httpResponseMessage);
			this.useHttpBackedProperty = true;
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x0012C238 File Offset: 0x0012A438
		[__DynamicallyInvokable]
		public static string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return "httpResponse";
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x0012C23F File Offset: 0x0012A43F
		[__DynamicallyInvokable]
		public WebHeaderCollection Headers
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.Headers;
				}
				return this.httpBackedProperty.Headers;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x0600519A RID: 20890 RVA: 0x0012C260 File Offset: 0x0012A460
		// (set) Token: 0x0600519B RID: 20891 RVA: 0x0012C284 File Offset: 0x0012A484
		[__DynamicallyInvokable]
		public HttpStatusCode StatusCode
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.StatusCode;
				}
				return this.httpBackedProperty.StatusCode;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < HttpStatusCode.Continue || value > (HttpStatusCode)599)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
					{
						100,
						599
					})));
				}
				if (this.useHttpBackedProperty)
				{
					this.httpBackedProperty.StatusCode = value;
					return;
				}
				this.traditionalProperty.StatusCode = value;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x0600519C RID: 20892 RVA: 0x0012C303 File Offset: 0x0012A503
		internal bool HasStatusCodeBeenSet
		{
			get
			{
				return this.useHttpBackedProperty || this.traditionalProperty.HasStatusCodeBeenSet;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x0600519D RID: 20893 RVA: 0x0012C31A File Offset: 0x0012A51A
		// (set) Token: 0x0600519E RID: 20894 RVA: 0x0012C33B File Offset: 0x0012A53B
		[__DynamicallyInvokable]
		public string StatusDescription
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.StatusDescription;
				}
				return this.httpBackedProperty.StatusDescription;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.useHttpBackedProperty)
				{
					this.httpBackedProperty.StatusDescription = value;
					return;
				}
				this.traditionalProperty.StatusDescription = value;
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x0012C35E File Offset: 0x0012A55E
		// (set) Token: 0x060051A0 RID: 20896 RVA: 0x0012C37F File Offset: 0x0012A57F
		public bool SuppressEntityBody
		{
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.SuppressEntityBody;
				}
				return this.httpBackedProperty.SuppressEntityBody;
			}
			set
			{
				if (this.useHttpBackedProperty)
				{
					this.httpBackedProperty.SuppressEntityBody = value;
					return;
				}
				this.traditionalProperty.SuppressEntityBody = value;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x060051A1 RID: 20897 RVA: 0x0012C3A2 File Offset: 0x0012A5A2
		// (set) Token: 0x060051A2 RID: 20898 RVA: 0x0012C3B9 File Offset: 0x0012A5B9
		public bool SuppressPreamble
		{
			get
			{
				return !this.useHttpBackedProperty && this.traditionalProperty.SuppressPreamble;
			}
			set
			{
				if (!this.useHttpBackedProperty)
				{
					this.traditionalProperty.SuppressPreamble = value;
				}
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x060051A3 RID: 20899 RVA: 0x0012C3CF File Offset: 0x0012A5CF
		private HttpResponseMessage HttpResponseMessage
		{
			get
			{
				if (this.useHttpBackedProperty)
				{
					return this.httpBackedProperty.HttpResponseMessage;
				}
				return null;
			}
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0012C3E8 File Offset: 0x0012A5E8
		internal static HttpResponseMessage GetHttpResponseMessageFromMessage(Message message)
		{
			HttpResponseMessage httpResponseMessage = null;
			HttpResponseMessageProperty value = message.Properties.GetValue<HttpResponseMessageProperty>(HttpResponseMessageProperty.Name);
			if (value != null)
			{
				httpResponseMessage = value.HttpResponseMessage;
				if (httpResponseMessage != null)
				{
					httpResponseMessage.CopyPropertiesFromMessage(message);
					message.EnsureReadMessageState();
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x0012C423 File Offset: 0x0012A623
		[__DynamicallyInvokable]
		IMessageProperty IMessageProperty.CreateCopy()
		{
			if (!this.useHttpBackedProperty || !this.initialCopyPerformed)
			{
				this.initialCopyPerformed = true;
				return this;
			}
			return this.httpBackedProperty.CreateTraditionalResponseMessageProperty();
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x0012C44C File Offset: 0x0012A64C
		bool IMergeEnabledMessageProperty.TryMergeWithProperty(object propertyToMerge)
		{
			if (this.useHttpBackedProperty)
			{
				HttpResponseMessageProperty httpResponseMessageProperty = propertyToMerge as HttpResponseMessageProperty;
				if (httpResponseMessageProperty != null)
				{
					if (!httpResponseMessageProperty.useHttpBackedProperty)
					{
						this.httpBackedProperty.MergeWithTraditionalProperty(httpResponseMessageProperty.traditionalProperty);
						httpResponseMessageProperty.traditionalProperty = null;
						httpResponseMessageProperty.httpBackedProperty = this.httpBackedProperty;
						httpResponseMessageProperty.useHttpBackedProperty = true;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003217 RID: 12823
		private HttpResponseMessageProperty.TraditionalHttpResponseMessageProperty traditionalProperty;

		// Token: 0x04003218 RID: 12824
		private HttpResponseMessageProperty.HttpResponseMessageBackedProperty httpBackedProperty;

		// Token: 0x04003219 RID: 12825
		private bool useHttpBackedProperty;

		// Token: 0x0400321A RID: 12826
		private bool initialCopyPerformed;

		// Token: 0x02000D53 RID: 3411
		private class TraditionalHttpResponseMessageProperty
		{
			// Token: 0x06007CFD RID: 31997 RVA: 0x001D3A26 File Offset: 0x001D1C26
			public TraditionalHttpResponseMessageProperty(WebHeaderCollection originalHeaders)
			{
				this.originalHeaders = originalHeaders;
				this.statusCode = HttpStatusCode.OK;
				this.StatusDescription = null;
			}

			// Token: 0x17001BF2 RID: 7154
			// (get) Token: 0x06007CFE RID: 31998 RVA: 0x001D3A47 File Offset: 0x001D1C47
			public WebHeaderCollection Headers
			{
				get
				{
					if (this.headers == null)
					{
						this.headers = new WebHeaderCollection();
						if (this.originalHeaders != null)
						{
							this.headers.Add(this.originalHeaders);
							this.originalHeaders = null;
						}
					}
					return this.headers;
				}
			}

			// Token: 0x17001BF3 RID: 7155
			// (get) Token: 0x06007CFF RID: 31999 RVA: 0x001D3A82 File Offset: 0x001D1C82
			// (set) Token: 0x06007D00 RID: 32000 RVA: 0x001D3A8A File Offset: 0x001D1C8A
			public HttpStatusCode StatusCode
			{
				get
				{
					return this.statusCode;
				}
				set
				{
					this.statusCode = value;
					this.HasStatusCodeBeenSet = true;
				}
			}

			// Token: 0x17001BF4 RID: 7156
			// (get) Token: 0x06007D01 RID: 32001 RVA: 0x001D3A9A File Offset: 0x001D1C9A
			// (set) Token: 0x06007D02 RID: 32002 RVA: 0x001D3AA2 File Offset: 0x001D1CA2
			public bool HasStatusCodeBeenSet { get; private set; }

			// Token: 0x17001BF5 RID: 7157
			// (get) Token: 0x06007D03 RID: 32003 RVA: 0x001D3AAB File Offset: 0x001D1CAB
			// (set) Token: 0x06007D04 RID: 32004 RVA: 0x001D3AB3 File Offset: 0x001D1CB3
			public string StatusDescription { get; set; }

			// Token: 0x17001BF6 RID: 7158
			// (get) Token: 0x06007D05 RID: 32005 RVA: 0x001D3ABC File Offset: 0x001D1CBC
			// (set) Token: 0x06007D06 RID: 32006 RVA: 0x001D3AC4 File Offset: 0x001D1CC4
			public bool SuppressEntityBody { get; set; }

			// Token: 0x17001BF7 RID: 7159
			// (get) Token: 0x06007D07 RID: 32007 RVA: 0x001D3ACD File Offset: 0x001D1CCD
			// (set) Token: 0x06007D08 RID: 32008 RVA: 0x001D3AD5 File Offset: 0x001D1CD5
			public bool SuppressPreamble { get; set; }

			// Token: 0x040047CF RID: 18383
			public const HttpStatusCode DefaultStatusCode = HttpStatusCode.OK;

			// Token: 0x040047D0 RID: 18384
			public const string DefaultStatusDescription = null;

			// Token: 0x040047D1 RID: 18385
			private WebHeaderCollection headers;

			// Token: 0x040047D2 RID: 18386
			private WebHeaderCollection originalHeaders;

			// Token: 0x040047D3 RID: 18387
			private HttpStatusCode statusCode;
		}

		// Token: 0x02000D54 RID: 3412
		private class HttpResponseMessageBackedProperty
		{
			// Token: 0x06007D09 RID: 32009 RVA: 0x001D3ADE File Offset: 0x001D1CDE
			public HttpResponseMessageBackedProperty(HttpResponseMessage httpResponseMessage)
			{
				this.HttpResponseMessage = httpResponseMessage;
			}

			// Token: 0x17001BF8 RID: 7160
			// (get) Token: 0x06007D0A RID: 32010 RVA: 0x001D3AED File Offset: 0x001D1CED
			// (set) Token: 0x06007D0B RID: 32011 RVA: 0x001D3AF5 File Offset: 0x001D1CF5
			public HttpResponseMessage HttpResponseMessage { get; private set; }

			// Token: 0x17001BF9 RID: 7161
			// (get) Token: 0x06007D0C RID: 32012 RVA: 0x001D3AFE File Offset: 0x001D1CFE
			public WebHeaderCollection Headers
			{
				get
				{
					if (this.headers == null)
					{
						this.headers = new HttpHeadersWebHeaderCollection(this.HttpResponseMessage);
					}
					return this.headers;
				}
			}

			// Token: 0x17001BFA RID: 7162
			// (get) Token: 0x06007D0D RID: 32013 RVA: 0x001D3B1F File Offset: 0x001D1D1F
			// (set) Token: 0x06007D0E RID: 32014 RVA: 0x001D3B2C File Offset: 0x001D1D2C
			public HttpStatusCode StatusCode
			{
				get
				{
					return this.HttpResponseMessage.StatusCode;
				}
				set
				{
					this.HttpResponseMessage.StatusCode = value;
				}
			}

			// Token: 0x17001BFB RID: 7163
			// (get) Token: 0x06007D0F RID: 32015 RVA: 0x001D3B3A File Offset: 0x001D1D3A
			// (set) Token: 0x06007D10 RID: 32016 RVA: 0x001D3B47 File Offset: 0x001D1D47
			public string StatusDescription
			{
				get
				{
					return this.HttpResponseMessage.ReasonPhrase;
				}
				set
				{
					this.HttpResponseMessage.ReasonPhrase = value;
				}
			}

			// Token: 0x17001BFC RID: 7164
			// (get) Token: 0x06007D11 RID: 32017 RVA: 0x001D3B58 File Offset: 0x001D1D58
			// (set) Token: 0x06007D12 RID: 32018 RVA: 0x001D3BA0 File Offset: 0x001D1DA0
			public bool SuppressEntityBody
			{
				get
				{
					HttpContent content = this.HttpResponseMessage.Content;
					if (content != null)
					{
						long? contentLength = content.Headers.ContentLength;
						if (contentLength == null || (contentLength != null && contentLength.Value > 0L))
						{
							return false;
						}
					}
					return true;
				}
				set
				{
					HttpContent content = this.HttpResponseMessage.Content;
					if (value && content != null && (content.Headers.ContentLength == null || content.Headers.ContentLength.Value > 0L))
					{
						HttpContent httpContent = new ByteArrayContent(EmptyArray<byte>.Instance);
						foreach (KeyValuePair<string, IEnumerable<string>> header in content.Headers)
						{
							httpContent.Headers.AddHeaderWithoutValidation(header);
						}
						this.HttpResponseMessage.Content = httpContent;
						content.Dispose();
						return;
					}
					if (!value && content == null)
					{
						this.HttpResponseMessage.Content = new ByteArrayContent(EmptyArray<byte>.Instance);
					}
				}
			}

			// Token: 0x06007D13 RID: 32019 RVA: 0x001D3C74 File Offset: 0x001D1E74
			public HttpResponseMessageProperty CreateTraditionalResponseMessageProperty()
			{
				HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
				httpResponseMessageProperty.Headers.Add(this.Headers);
				if (this.StatusCode != HttpStatusCode.OK)
				{
					httpResponseMessageProperty.StatusCode = this.StatusCode;
				}
				httpResponseMessageProperty.StatusDescription = this.StatusDescription;
				httpResponseMessageProperty.SuppressEntityBody = this.SuppressEntityBody;
				return httpResponseMessageProperty;
			}

			// Token: 0x06007D14 RID: 32020 RVA: 0x001D3CCC File Offset: 0x001D1ECC
			public void MergeWithTraditionalProperty(HttpResponseMessageProperty.TraditionalHttpResponseMessageProperty propertyToMerge)
			{
				if (propertyToMerge.HasStatusCodeBeenSet)
				{
					this.StatusCode = propertyToMerge.StatusCode;
				}
				if (propertyToMerge.StatusDescription != null)
				{
					this.StatusDescription = propertyToMerge.StatusDescription;
				}
				this.SuppressEntityBody = propertyToMerge.SuppressEntityBody;
				WebHeaderCollection webHeaderCollection = propertyToMerge.Headers;
				foreach (string name in webHeaderCollection.AllKeys)
				{
					this.Headers[name] = webHeaderCollection[name];
				}
			}

			// Token: 0x040047D8 RID: 18392
			private HttpHeadersWebHeaderCollection headers;
		}
	}
}
