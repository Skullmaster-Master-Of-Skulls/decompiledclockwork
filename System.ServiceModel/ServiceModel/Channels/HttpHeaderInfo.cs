using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200088A RID: 2186
	internal class HttpHeaderInfo
	{
		// Token: 0x060052F5 RID: 21237 RVA: 0x00131928 File Offset: 0x0012FB28
		static HttpHeaderInfo()
		{
			HttpHeaderInfo.AddKnownHeaders(from enumString in Enum.GetNames(HttpHeaderInfo.httpRequestHeaderType)
			select HttpHeaderInfo.GetHeaderString(enumString), true);
			HttpHeaderInfo.AddKnownHeaders(from enumString in Enum.GetNames(HttpHeaderInfo.httpResponseHeaderType)
			select HttpHeaderInfo.GetHeaderString(enumString), false);
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00131ABB File Offset: 0x0012FCBB
		private HttpHeaderInfo(string name, bool isUnknownHeader = false)
		{
			this.Name = name;
			this.isUnknownHeader = isUnknownHeader;
			if (this.isUnknownHeader)
			{
				this.IsRequestHeader = true;
				this.IsResponseHeader = true;
				this.IsContentHeader = true;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x060052F7 RID: 21239 RVA: 0x00131AEE File Offset: 0x0012FCEE
		// (set) Token: 0x060052F8 RID: 21240 RVA: 0x00131AF6 File Offset: 0x0012FCF6
		public string Name { get; private set; }

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x00131AFF File Offset: 0x0012FCFF
		// (set) Token: 0x060052FA RID: 21242 RVA: 0x00131B07 File Offset: 0x0012FD07
		public bool IsRequestHeader { get; private set; }

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x060052FB RID: 21243 RVA: 0x00131B10 File Offset: 0x0012FD10
		// (set) Token: 0x060052FC RID: 21244 RVA: 0x00131B18 File Offset: 0x0012FD18
		public bool IsResponseHeader { get; private set; }

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x060052FD RID: 21245 RVA: 0x00131B21 File Offset: 0x0012FD21
		// (set) Token: 0x060052FE RID: 21246 RVA: 0x00131B29 File Offset: 0x0012FD29
		public bool IsContentHeader { get; private set; }

		// Token: 0x060052FF RID: 21247 RVA: 0x00131B34 File Offset: 0x0012FD34
		public static HttpHeaderInfo Create(string headerName)
		{
			HttpHeaderInfo result;
			if (!HttpHeaderInfo.knownHeadersInfos.TryGetValue(headerName, out result))
			{
				result = new HttpHeaderInfo(headerName, true);
			}
			return result;
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x00131B59 File Offset: 0x0012FD59
		public bool TryAddHeader(HttpHeaders headers, string value)
		{
			if (!headers.TryAddWithoutValidation(this.Name, value))
			{
				this.UpdateHeaderInfo(headers);
				return false;
			}
			return true;
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x00131B74 File Offset: 0x0012FD74
		public bool TryRemoveHeader(HttpHeaders headers)
		{
			try
			{
				headers.Remove(this.Name);
				return true;
			}
			catch (InvalidOperationException exception)
			{
				FxTrace.Exception.TraceHandledException(exception, TraceEventType.Information);
				this.UpdateHeaderInfo(headers);
			}
			return false;
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x00131BBC File Offset: 0x0012FDBC
		public IEnumerable<string> TryGetHeader(HttpHeaders headers)
		{
			IEnumerable<string> result = null;
			if (!headers.TryGetValues(this.Name, out result))
			{
				result = null;
				this.UpdateHeaderInfo(headers);
			}
			return result;
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x00131BE8 File Offset: 0x0012FDE8
		private static void AddKnownHeaders(IEnumerable<string> headers, bool asRequestHeader)
		{
			foreach (string text in headers)
			{
				HttpHeaderInfo httpHeaderInfo = null;
				if (!HttpHeaderInfo.knownHeadersInfos.TryGetValue(text, out httpHeaderInfo) || !httpHeaderInfo.IsContentHeader)
				{
					if (httpHeaderInfo == null)
					{
						httpHeaderInfo = new HttpHeaderInfo(text, false);
						HttpHeaderInfo.knownHeadersInfos.TryAdd(httpHeaderInfo.Name, httpHeaderInfo);
					}
					if (asRequestHeader)
					{
						httpHeaderInfo.IsRequestHeader = true;
					}
					else
					{
						httpHeaderInfo.IsResponseHeader = true;
					}
				}
			}
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x00131C74 File Offset: 0x0012FE74
		private static string GetHeaderString(string headerEnumString)
		{
			if (string.Equals(headerEnumString, HttpResponseHeader.ETag.ToString(), StringComparison.Ordinal))
			{
				return headerEnumString;
			}
			StringBuilder stringBuilder = new StringBuilder(headerEnumString);
			for (int i = stringBuilder.Length - 2; i > 0; i--)
			{
				if (char.IsUpper(stringBuilder[i]) && char.IsLower(stringBuilder[i + 1]))
				{
					stringBuilder.Insert(i, '-');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x00131CE4 File Offset: 0x0012FEE4
		private void UpdateHeaderInfo(HttpHeaders headers)
		{
			if (headers is HttpContentHeaders)
			{
				this.IsContentHeader = false;
			}
			else if (headers is HttpRequestHeaders)
			{
				this.IsRequestHeader = false;
			}
			else if (headers is HttpResponseHeaders)
			{
				this.IsResponseHeader = false;
			}
			if (this.isUnknownHeader)
			{
				this.isUnknownHeader = !HttpHeaderInfo.knownHeadersInfos.TryAdd(this.Name, this);
			}
		}

		// Token: 0x040032A1 RID: 12961
		private static readonly HttpHeaderInfo[] knownContentHeaders = new HttpHeaderInfo[]
		{
			new HttpHeaderInfo("Allow", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Encoding", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Language", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Length", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Location", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-MD5", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Range", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Type", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Expires", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Last-Modified", false)
			{
				IsContentHeader = true
			},
			new HttpHeaderInfo("Content-Disposition", false)
			{
				IsContentHeader = true
			}
		};

		// Token: 0x040032A2 RID: 12962
		private static readonly Type httpRequestHeaderType = typeof(HttpRequestHeader);

		// Token: 0x040032A3 RID: 12963
		private static readonly Type httpResponseHeaderType = typeof(HttpResponseHeader);

		// Token: 0x040032A4 RID: 12964
		private static ConcurrentDictionary<string, HttpHeaderInfo> knownHeadersInfos = new ConcurrentDictionary<string, HttpHeaderInfo>(HttpHeaderInfo.knownContentHeaders.ToDictionary((HttpHeaderInfo headerInfo) => headerInfo.Name), StringComparer.OrdinalIgnoreCase);

		// Token: 0x040032A5 RID: 12965
		private bool isUnknownHeader;
	}
}
