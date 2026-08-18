using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200086D RID: 2157
	[__DynamicallyInvokable]
	public sealed class HttpRequestMessageProperty : IMessageProperty, IMergeEnabledMessageProperty
	{
		// Token: 0x06005186 RID: 20870 RVA: 0x0012BFCD File Offset: 0x0012A1CD
		[__DynamicallyInvokable]
		public HttpRequestMessageProperty() : this(null)
		{
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0012BFD6 File Offset: 0x0012A1D6
		internal HttpRequestMessageProperty(HttpRequestMessageProperty.IHttpHeaderProvider httpHeaderProvider)
		{
			this.traditionalProperty = new HttpRequestMessageProperty.TraditionalHttpRequestMessageProperty(httpHeaderProvider);
			this.useHttpBackedProperty = false;
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0012BFF1 File Offset: 0x0012A1F1
		internal HttpRequestMessageProperty(HttpRequestMessage httpRequestMessage)
		{
			this.httpBackedProperty = new HttpRequestMessageProperty.HttpRequestMessageBackedProperty(httpRequestMessage);
			this.useHttpBackedProperty = true;
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06005189 RID: 20873 RVA: 0x0012C00C File Offset: 0x0012A20C
		[__DynamicallyInvokable]
		public static string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return "httpRequest";
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x0012C013 File Offset: 0x0012A213
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

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x0600518B RID: 20875 RVA: 0x0012C034 File Offset: 0x0012A234
		// (set) Token: 0x0600518C RID: 20876 RVA: 0x0012C055 File Offset: 0x0012A255
		[__DynamicallyInvokable]
		public string Method
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.Method;
				}
				return this.httpBackedProperty.Method;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (this.useHttpBackedProperty)
				{
					this.httpBackedProperty.Method = value;
					return;
				}
				this.traditionalProperty.Method = value;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x0012C08B File Offset: 0x0012A28B
		// (set) Token: 0x0600518E RID: 20878 RVA: 0x0012C0AC File Offset: 0x0012A2AC
		[__DynamicallyInvokable]
		public string QueryString
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.QueryString;
				}
				return this.httpBackedProperty.QueryString;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (this.useHttpBackedProperty)
				{
					this.httpBackedProperty.QueryString = value;
					return;
				}
				this.traditionalProperty.QueryString = value;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x0012C0E2 File Offset: 0x0012A2E2
		// (set) Token: 0x06005190 RID: 20880 RVA: 0x0012C103 File Offset: 0x0012A303
		[__DynamicallyInvokable]
		public bool SuppressEntityBody
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.useHttpBackedProperty)
				{
					return this.traditionalProperty.SuppressEntityBody;
				}
				return this.httpBackedProperty.SuppressEntityBody;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x0012C126 File Offset: 0x0012A326
		private HttpRequestMessage HttpRequestMessage
		{
			get
			{
				if (this.useHttpBackedProperty)
				{
					return this.httpBackedProperty.HttpRequestMessage;
				}
				return null;
			}
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0012C140 File Offset: 0x0012A340
		internal static HttpRequestMessage GetHttpRequestMessageFromMessage(Message message)
		{
			HttpRequestMessage httpRequestMessage = null;
			HttpRequestMessageProperty value = message.Properties.GetValue<HttpRequestMessageProperty>(HttpRequestMessageProperty.Name);
			if (value != null)
			{
				httpRequestMessage = value.HttpRequestMessage;
				if (httpRequestMessage != null)
				{
					httpRequestMessage.CopyPropertiesFromMessage(message);
					message.EnsureReadMessageState();
				}
			}
			return httpRequestMessage;
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0012C17B File Offset: 0x0012A37B
		[__DynamicallyInvokable]
		IMessageProperty IMessageProperty.CreateCopy()
		{
			if (!this.useHttpBackedProperty || !this.initialCopyPerformed)
			{
				this.initialCopyPerformed = true;
				return this;
			}
			return this.httpBackedProperty.CreateTraditionalRequestMessageProperty();
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0012C1A4 File Offset: 0x0012A3A4
		bool IMergeEnabledMessageProperty.TryMergeWithProperty(object propertyToMerge)
		{
			if (this.useHttpBackedProperty)
			{
				HttpRequestMessageProperty httpRequestMessageProperty = propertyToMerge as HttpRequestMessageProperty;
				if (httpRequestMessageProperty != null)
				{
					if (!httpRequestMessageProperty.useHttpBackedProperty)
					{
						this.httpBackedProperty.MergeWithTraditionalProperty(httpRequestMessageProperty.traditionalProperty);
						httpRequestMessageProperty.traditionalProperty = null;
						httpRequestMessageProperty.httpBackedProperty = this.httpBackedProperty;
						httpRequestMessageProperty.useHttpBackedProperty = true;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003213 RID: 12819
		private HttpRequestMessageProperty.TraditionalHttpRequestMessageProperty traditionalProperty;

		// Token: 0x04003214 RID: 12820
		private HttpRequestMessageProperty.HttpRequestMessageBackedProperty httpBackedProperty;

		// Token: 0x04003215 RID: 12821
		private bool initialCopyPerformed;

		// Token: 0x04003216 RID: 12822
		private bool useHttpBackedProperty;

		// Token: 0x02000D50 RID: 3408
		internal interface IHttpHeaderProvider
		{
			// Token: 0x06007CE6 RID: 31974
			void CopyHeaders(WebHeaderCollection headers);
		}

		// Token: 0x02000D51 RID: 3409
		private class TraditionalHttpRequestMessageProperty
		{
			// Token: 0x06007CE7 RID: 31975 RVA: 0x001D36B0 File Offset: 0x001D18B0
			public TraditionalHttpRequestMessageProperty(HttpRequestMessageProperty.IHttpHeaderProvider httpHeaderProvider)
			{
				this.httpHeaderProvider = httpHeaderProvider;
				this.method = "POST";
				this.QueryString = "";
			}

			// Token: 0x17001BE8 RID: 7144
			// (get) Token: 0x06007CE8 RID: 31976 RVA: 0x001D36D5 File Offset: 0x001D18D5
			public WebHeaderCollection Headers
			{
				get
				{
					if (this.headers == null)
					{
						this.headers = new WebHeaderCollection();
						if (this.httpHeaderProvider != null)
						{
							this.httpHeaderProvider.CopyHeaders(this.headers);
							this.httpHeaderProvider = null;
						}
					}
					return this.headers;
				}
			}

			// Token: 0x17001BE9 RID: 7145
			// (get) Token: 0x06007CE9 RID: 31977 RVA: 0x001D3710 File Offset: 0x001D1910
			// (set) Token: 0x06007CEA RID: 31978 RVA: 0x001D3718 File Offset: 0x001D1918
			public string Method
			{
				get
				{
					return this.method;
				}
				set
				{
					this.method = value;
					this.HasMethodBeenSet = true;
				}
			}

			// Token: 0x17001BEA RID: 7146
			// (get) Token: 0x06007CEB RID: 31979 RVA: 0x001D3728 File Offset: 0x001D1928
			// (set) Token: 0x06007CEC RID: 31980 RVA: 0x001D3730 File Offset: 0x001D1930
			public bool HasMethodBeenSet { get; private set; }

			// Token: 0x17001BEB RID: 7147
			// (get) Token: 0x06007CED RID: 31981 RVA: 0x001D3739 File Offset: 0x001D1939
			// (set) Token: 0x06007CEE RID: 31982 RVA: 0x001D3741 File Offset: 0x001D1941
			public string QueryString { get; set; }

			// Token: 0x17001BEC RID: 7148
			// (get) Token: 0x06007CEF RID: 31983 RVA: 0x001D374A File Offset: 0x001D194A
			// (set) Token: 0x06007CF0 RID: 31984 RVA: 0x001D3752 File Offset: 0x001D1952
			public bool SuppressEntityBody { get; set; }

			// Token: 0x040047C5 RID: 18373
			public const string DefaultMethod = "POST";

			// Token: 0x040047C6 RID: 18374
			public const string DefaultQueryString = "";

			// Token: 0x040047C7 RID: 18375
			private WebHeaderCollection headers;

			// Token: 0x040047C8 RID: 18376
			private HttpRequestMessageProperty.IHttpHeaderProvider httpHeaderProvider;

			// Token: 0x040047C9 RID: 18377
			private string method;
		}

		// Token: 0x02000D52 RID: 3410
		private class HttpRequestMessageBackedProperty
		{
			// Token: 0x06007CF1 RID: 31985 RVA: 0x001D375B File Offset: 0x001D195B
			public HttpRequestMessageBackedProperty(HttpRequestMessage httpRequestMessage)
			{
				this.HttpRequestMessage = httpRequestMessage;
			}

			// Token: 0x17001BED RID: 7149
			// (get) Token: 0x06007CF2 RID: 31986 RVA: 0x001D376A File Offset: 0x001D196A
			// (set) Token: 0x06007CF3 RID: 31987 RVA: 0x001D3772 File Offset: 0x001D1972
			public HttpRequestMessage HttpRequestMessage { get; private set; }

			// Token: 0x17001BEE RID: 7150
			// (get) Token: 0x06007CF4 RID: 31988 RVA: 0x001D377B File Offset: 0x001D197B
			public WebHeaderCollection Headers
			{
				get
				{
					if (this.headers == null)
					{
						this.headers = new HttpHeadersWebHeaderCollection(this.HttpRequestMessage);
					}
					return this.headers;
				}
			}

			// Token: 0x17001BEF RID: 7151
			// (get) Token: 0x06007CF5 RID: 31989 RVA: 0x001D379C File Offset: 0x001D199C
			// (set) Token: 0x06007CF6 RID: 31990 RVA: 0x001D37AE File Offset: 0x001D19AE
			public string Method
			{
				get
				{
					return this.HttpRequestMessage.Method.Method;
				}
				set
				{
					this.HttpRequestMessage.Method = new HttpMethod(value);
				}
			}

			// Token: 0x17001BF0 RID: 7152
			// (get) Token: 0x06007CF7 RID: 31991 RVA: 0x001D37C4 File Offset: 0x001D19C4
			// (set) Token: 0x06007CF8 RID: 31992 RVA: 0x001D37F8 File Offset: 0x001D19F8
			public string QueryString
			{
				get
				{
					string query = this.HttpRequestMessage.RequestUri.Query;
					if (query.Length <= 0)
					{
						return string.Empty;
					}
					return query.Substring(1);
				}
				set
				{
					UriBuilder uriBuilder = new UriBuilder(this.HttpRequestMessage.RequestUri);
					uriBuilder.Query = value;
					this.HttpRequestMessage.RequestUri = uriBuilder.Uri;
				}
			}

			// Token: 0x17001BF1 RID: 7153
			// (get) Token: 0x06007CF9 RID: 31993 RVA: 0x001D3830 File Offset: 0x001D1A30
			// (set) Token: 0x06007CFA RID: 31994 RVA: 0x001D3878 File Offset: 0x001D1A78
			public bool SuppressEntityBody
			{
				get
				{
					HttpContent content = this.HttpRequestMessage.Content;
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
					HttpContent content = this.HttpRequestMessage.Content;
					if (value && content != null && (content.Headers.ContentLength == null || content.Headers.ContentLength.Value > 0L))
					{
						HttpContent httpContent = new ByteArrayContent(EmptyArray<byte>.Instance);
						foreach (KeyValuePair<string, IEnumerable<string>> header in content.Headers)
						{
							httpContent.Headers.AddHeaderWithoutValidation(header);
						}
						this.HttpRequestMessage.Content = httpContent;
						content.Dispose();
						return;
					}
					if (!value && content == null)
					{
						this.HttpRequestMessage.Content = new ByteArrayContent(EmptyArray<byte>.Instance);
					}
				}
			}

			// Token: 0x06007CFB RID: 31995 RVA: 0x001D394C File Offset: 0x001D1B4C
			public HttpRequestMessageProperty CreateTraditionalRequestMessageProperty()
			{
				HttpRequestMessageProperty httpRequestMessageProperty = new HttpRequestMessageProperty();
				httpRequestMessageProperty.Headers.Add(this.Headers);
				if (this.Method != "POST")
				{
					httpRequestMessageProperty.Method = this.Method;
				}
				httpRequestMessageProperty.QueryString = this.QueryString;
				httpRequestMessageProperty.SuppressEntityBody = this.SuppressEntityBody;
				return httpRequestMessageProperty;
			}

			// Token: 0x06007CFC RID: 31996 RVA: 0x001D39A8 File Offset: 0x001D1BA8
			public void MergeWithTraditionalProperty(HttpRequestMessageProperty.TraditionalHttpRequestMessageProperty propertyToMerge)
			{
				if (propertyToMerge.HasMethodBeenSet)
				{
					this.Method = propertyToMerge.Method;
				}
				if (propertyToMerge.QueryString != "")
				{
					this.QueryString = propertyToMerge.QueryString;
				}
				this.SuppressEntityBody = propertyToMerge.SuppressEntityBody;
				WebHeaderCollection webHeaderCollection = propertyToMerge.Headers;
				foreach (string name in webHeaderCollection.AllKeys)
				{
					this.Headers[name] = webHeaderCollection[name];
				}
			}

			// Token: 0x040047CD RID: 18381
			private HttpHeadersWebHeaderCollection headers;
		}
	}
}
