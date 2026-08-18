using System;
using System.Collections.Specialized;
using System.Net.Http;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x02000011 RID: 17
	internal class HttpRequestMessageWrapper : HttpRequestBase
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000036C8 File Offset: 0x000018C8
		public HttpRequestMessageWrapper(string virtualPathRoot, HttpRequestMessage httpRequest)
		{
			if (virtualPathRoot == null)
			{
				throw Error.ArgumentNull("virtualPathRoot");
			}
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this._virtualPathRoot = virtualPathRoot;
			this._httpRequest = httpRequest;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000036FA File Offset: 0x000018FA
		public override string ApplicationPath
		{
			get
			{
				return this._virtualPathRoot;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003704 File Offset: 0x00001904
		public override string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				string path = this.Path;
				if (path.StartsWith(this._virtualPathRoot, StringComparison.OrdinalIgnoreCase))
				{
					string text = (this._virtualPathRoot.Length == 1) ? path : path.Substring(this._virtualPathRoot.Length);
					return "~" + text.TrimEnd(new char[]
					{
						'/'
					});
				}
				return null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003769 File Offset: 0x00001969
		public override string CurrentExecutionFilePath
		{
			get
			{
				return this.FilePath;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003774 File Offset: 0x00001974
		public override string FilePath
		{
			get
			{
				string path = this.Path;
				if (path.StartsWith(this._virtualPathRoot, StringComparison.OrdinalIgnoreCase))
				{
					return path.TrimEnd(new char[]
					{
						'/'
					});
				}
				return null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000037AC File Offset: 0x000019AC
		public override string HttpMethod
		{
			get
			{
				return this._httpRequest.Method.ToString();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000037BE File Offset: 0x000019BE
		public override bool IsLocal
		{
			get
			{
				return this._httpRequest.IsLocal();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000037CB File Offset: 0x000019CB
		public override string Path
		{
			get
			{
				return "/" + this._httpRequest.RequestUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000037EA File Offset: 0x000019EA
		public override string PathInfo
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000037F1 File Offset: 0x000019F1
		public override NameValueCollection QueryString
		{
			get
			{
				return this._httpRequest.RequestUri.ParseQueryString();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003803 File Offset: 0x00001A03
		public override string RawUrl
		{
			get
			{
				return this._httpRequest.RequestUri.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003818 File Offset: 0x00001A18
		public override string RequestType
		{
			get
			{
				return this._httpRequest.Method.ToString();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000382A File Offset: 0x00001A2A
		public override Uri Url
		{
			get
			{
				return this._httpRequest.RequestUri;
			}
		}

		// Token: 0x0400001B RID: 27
		private readonly string _virtualPathRoot;

		// Token: 0x0400001C RID: 28
		private readonly HttpRequestMessage _httpRequest;
	}
}
