using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000AD RID: 173
	public sealed class HttpRequest
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00017F69 File Offset: 0x00016169
		internal HttpRequest(HttpWorkerRequest wr, HttpContext context)
		{
			this._wr = wr;
			this._context = context;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00017F88 File Offset: 0x00016188
		public HttpRequest(string filename, string url, string queryString)
		{
			this._wr = null;
			this._pathTranslated = filename;
			this._httpMethod = "GET";
			this._url = new Uri(url);
			this._path = VirtualPath.CreateAbsolute(this._url.AbsolutePath);
			this._queryStringText = queryString;
			this._queryStringOverriden = true;
			this._queryString = new HttpValueCollection(this._queryStringText, true, true, Encoding.Default);
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001800C File Offset: 0x0001620C
		internal HttpRequest(VirtualPath virtualPath, string queryString)
		{
			this._wr = null;
			this._pathTranslated = virtualPath.MapPath();
			this._httpMethod = "GET";
			this._url = new Uri("http://localhost" + virtualPath.VirtualPathString);
			this._path = virtualPath;
			this._queryStringText = queryString;
			this._queryStringOverriden = true;
			this._queryString = new HttpValueCollection(this._queryStringText, true, true, Encoding.Default);
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00018093 File Offset: 0x00016293
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0001809B File Offset: 0x0001629B
		internal bool NeedToInsertEntityBody
		{
			get
			{
				return this._needToInsertEntityBody;
			}
			set
			{
				this._needToInsertEntityBody = value;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000180A4 File Offset: 0x000162A4
		internal void SetRawContent(HttpRawUploadedContent rawContent)
		{
			if (rawContent.Length > 0)
			{
				this.NeedToInsertEntityBody = true;
			}
			this._rawContent = rawContent;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x000180BD File Offset: 0x000162BD
		internal byte[] EntityBody
		{
			get
			{
				if (!this.NeedToInsertEntityBody)
				{
					return null;
				}
				return this._rawContent.GetAsByteArray();
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000180D4 File Offset: 0x000162D4
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x000180EA File Offset: 0x000162EA
		internal string ClientTarget
		{
			get
			{
				if (this._clientTarget != null)
				{
					return this._clientTarget;
				}
				return string.Empty;
			}
			set
			{
				this._clientTarget = value;
				this._browsercaps = null;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000180FA File Offset: 0x000162FA
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00018102 File Offset: 0x00016302
		internal HttpContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0001810C File Offset: 0x0001630C
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0001814D File Offset: 0x0001634D
		public RequestContext RequestContext
		{
			get
			{
				if (this._requestContext == null)
				{
					HttpContext httpContext = this.Context ?? HttpContext.Current;
					this._requestContext = new RequestContext(new HttpContextWrapper(httpContext), new RouteData());
				}
				return this._requestContext;
			}
			set
			{
				this._requestContext = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00018156 File Offset: 0x00016356
		private bool HasTransitionedToWebSocketRequest
		{
			get
			{
				return this.Context != null && this.Context.HasWebSocketRequestTransitionCompleted;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001816D File Offset: 0x0001636D
		internal HttpResponse Response
		{
			get
			{
				if (this._context == null)
				{
					return null;
				}
				return this._context.Response;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00018184 File Offset: 0x00016384
		public bool IsLocal
		{
			get
			{
				return this._wr != null && this._wr.IsLocal();
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001819B File Offset: 0x0001639B
		internal void Dispose()
		{
			if (this._serverVariables != null)
			{
				this._serverVariables.Dispose();
			}
			if (this._rawContent != null)
			{
				this._rawContent.Dispose();
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000181C4 File Offset: 0x000163C4
		internal static string[] ParseMultivalueHeader(string s)
		{
			int num = (s != null) ? s.Length : 0;
			if (num == 0)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			int i = 0;
			while (i < num)
			{
				int num2 = s.IndexOf(',', i);
				if (num2 < 0)
				{
					num2 = num;
				}
				arrayList.Add(s.Substring(i, num2 - i));
				i = num2 + 1;
				if (i < num && s[i] == ' ')
				{
					i++;
				}
			}
			int count = arrayList.Count;
			if (count == 0)
			{
				return null;
			}
			string[] array = new string[count];
			arrayList.CopyTo(0, array, 0, count);
			return array;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00018250 File Offset: 0x00016450
		private void FillInQueryStringCollection()
		{
			byte[] queryStringBytes = this.QueryStringBytes;
			if (queryStringBytes != null)
			{
				if (queryStringBytes.Length != 0)
				{
					this._queryString.FillFromEncodedBytes(queryStringBytes, this.QueryStringEncoding);
					return;
				}
			}
			else if (!string.IsNullOrEmpty(this.QueryStringText))
			{
				this._queryString.FillFromString(this.QueryStringText, true, this.QueryStringEncoding);
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000182A4 File Offset: 0x000164A4
		private void FillInFormCollection()
		{
			if (this._wr == null)
			{
				return;
			}
			if (!this._wr.HasEntityBody())
			{
				return;
			}
			string contentType = this.ContentType;
			if (contentType == null)
			{
				return;
			}
			if (this._readEntityBodyMode == ReadEntityBodyMode.Bufferless)
			{
				return;
			}
			if (StringUtil.StringStartsWithIgnoreCase(contentType, "application/x-www-form-urlencoded"))
			{
				byte[] array = null;
				HttpRawUploadedContent entireRawContent = this.GetEntireRawContent();
				if (entireRawContent != null)
				{
					array = entireRawContent.GetAsByteArray();
				}
				if (array == null)
				{
					return;
				}
				try
				{
					this._form.FillFromEncodedBytes(array, this.ContentEncoding);
					return;
				}
				catch (Exception innerException)
				{
					throw new HttpException(SR.GetString("Invalid_urlencoded_form_data"), innerException);
				}
			}
			if (StringUtil.StringStartsWithIgnoreCase(contentType, "multipart/form-data"))
			{
				MultipartContentElement[] multipartContent = this.GetMultipartContent();
				if (multipartContent != null)
				{
					for (int i = 0; i < multipartContent.Length; i++)
					{
						if (multipartContent[i].IsFormItem)
						{
							this._form.ThrowIfMaxHttpCollectionKeysExceeded();
							this._form.Add(multipartContent[i].Name, multipartContent[i].GetAsString(this.ContentEncoding));
						}
					}
				}
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000183A4 File Offset: 0x000165A4
		private void FillInHeadersCollection()
		{
			if (this._wr == null)
			{
				return;
			}
			for (int i = 0; i < 40; i++)
			{
				string knownRequestHeader = this._wr.GetKnownRequestHeader(i);
				if (!string.IsNullOrEmpty(knownRequestHeader))
				{
					string knownRequestHeaderName = HttpWorkerRequest.GetKnownRequestHeaderName(i);
					this._headers.SynchronizeHeader(knownRequestHeaderName, knownRequestHeader);
				}
			}
			string[][] unknownRequestHeaders = this._wr.GetUnknownRequestHeaders();
			if (unknownRequestHeaders != null)
			{
				for (int j = 0; j < unknownRequestHeaders.Length; j++)
				{
					this._headers.SynchronizeHeader(unknownRequestHeaders[j][0], unknownRequestHeaders[j][1]);
				}
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00018428 File Offset: 0x00016628
		private static string ServerVariableNameFromHeader(string header)
		{
			return "HTTP_" + header.ToUpper(CultureInfo.InvariantCulture).Replace('-', '_');
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00018448 File Offset: 0x00016648
		private string CombineAllHeaders(bool asRaw)
		{
			if (this._wr == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			for (int i = 0; i < 40; i++)
			{
				string knownRequestHeader = this._wr.GetKnownRequestHeader(i);
				if (!string.IsNullOrEmpty(knownRequestHeader))
				{
					string text;
					if (!asRaw)
					{
						text = HttpWorkerRequest.GetServerVariableNameFromKnownRequestHeaderIndex(i);
					}
					else
					{
						text = HttpWorkerRequest.GetKnownRequestHeaderName(i);
					}
					if (text != null)
					{
						stringBuilder.Append(text);
						stringBuilder.Append(asRaw ? ": " : ":");
						stringBuilder.Append(knownRequestHeader);
						stringBuilder.Append("\r\n");
					}
				}
			}
			string[][] unknownRequestHeaders = this._wr.GetUnknownRequestHeaders();
			if (unknownRequestHeaders != null)
			{
				for (int j = 0; j < unknownRequestHeaders.Length; j++)
				{
					string text2 = unknownRequestHeaders[j][0];
					if (!asRaw)
					{
						text2 = HttpRequest.ServerVariableNameFromHeader(text2);
					}
					stringBuilder.Append(text2);
					stringBuilder.Append(asRaw ? ": " : ":");
					stringBuilder.Append(unknownRequestHeaders[j][1]);
					stringBuilder.Append("\r\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00018550 File Offset: 0x00016750
		internal string CalcDynamicServerVariable(DynamicServerVariable var)
		{
			string result = null;
			switch (var)
			{
			case DynamicServerVariable.AUTH_TYPE:
				if (this._context.User != null && this._context.User.Identity.IsAuthenticated)
				{
					result = this._context.User.Identity.AuthenticationType;
				}
				else
				{
					result = string.Empty;
				}
				break;
			case DynamicServerVariable.AUTH_USER:
				if (this._context.User != null && this._context.User.Identity.IsAuthenticated)
				{
					result = this._context.User.Identity.Name;
				}
				else
				{
					result = string.Empty;
				}
				break;
			case DynamicServerVariable.PATH_INFO:
				result = this.Path;
				break;
			case DynamicServerVariable.PATH_TRANSLATED:
				result = this.PhysicalPathInternal;
				break;
			case DynamicServerVariable.QUERY_STRING:
				result = this.QueryStringText;
				break;
			case DynamicServerVariable.SCRIPT_NAME:
				result = this.FilePath;
				break;
			}
			return result;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001862F File Offset: 0x0001682F
		private void AddServerVariableToCollection(string name, DynamicServerVariable var)
		{
			this._serverVariables.AddDynamic(name, var);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001863E File Offset: 0x0001683E
		private void AddServerVariableToCollection(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			this._serverVariables.AddStatic(name, value);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00018657 File Offset: 0x00016857
		private void AddServerVariableToCollection(string name)
		{
			this._serverVariables.AddStatic(name, this._wr.GetServerVariable(name));
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00018674 File Offset: 0x00016874
		internal void FillInServerVariablesCollection()
		{
			if (this._wr == null)
			{
				return;
			}
			this.AddServerVariableToCollection("ALL_HTTP", this.CombineAllHeaders(false));
			this.AddServerVariableToCollection("ALL_RAW", this.CombineAllHeaders(true));
			this.AddServerVariableToCollection("APPL_MD_PATH");
			this.AddServerVariableToCollection("APPL_PHYSICAL_PATH", this._wr.GetAppPathTranslated());
			this.AddServerVariableToCollection("AUTH_TYPE", DynamicServerVariable.AUTH_TYPE);
			this.AddServerVariableToCollection("AUTH_USER", DynamicServerVariable.AUTH_USER);
			this.AddServerVariableToCollection("AUTH_PASSWORD");
			this.AddServerVariableToCollection("LOGON_USER");
			this.AddServerVariableToCollection("REMOTE_USER", DynamicServerVariable.AUTH_USER);
			this.AddServerVariableToCollection("CERT_COOKIE");
			this.AddServerVariableToCollection("CERT_FLAGS");
			this.AddServerVariableToCollection("CERT_ISSUER");
			this.AddServerVariableToCollection("CERT_KEYSIZE");
			this.AddServerVariableToCollection("CERT_SECRETKEYSIZE");
			this.AddServerVariableToCollection("CERT_SERIALNUMBER");
			this.AddServerVariableToCollection("CERT_SERVER_ISSUER");
			this.AddServerVariableToCollection("CERT_SERVER_SUBJECT");
			this.AddServerVariableToCollection("CERT_SUBJECT");
			string knownRequestHeader = this._wr.GetKnownRequestHeader(11);
			this.AddServerVariableToCollection("CONTENT_LENGTH", (knownRequestHeader != null) ? knownRequestHeader : "0");
			this.AddServerVariableToCollection("CONTENT_TYPE", this.ContentType);
			this.AddServerVariableToCollection("GATEWAY_INTERFACE");
			this.AddServerVariableToCollection("HTTPS");
			this.AddServerVariableToCollection("HTTPS_KEYSIZE");
			this.AddServerVariableToCollection("HTTPS_SECRETKEYSIZE");
			this.AddServerVariableToCollection("HTTPS_SERVER_ISSUER");
			this.AddServerVariableToCollection("HTTPS_SERVER_SUBJECT");
			this.AddServerVariableToCollection("INSTANCE_ID");
			this.AddServerVariableToCollection("INSTANCE_META_PATH");
			this.AddServerVariableToCollection("LOCAL_ADDR", this._wr.GetLocalAddress());
			this.AddServerVariableToCollection("PATH_INFO", DynamicServerVariable.PATH_INFO);
			this.AddServerVariableToCollection("PATH_TRANSLATED", DynamicServerVariable.PATH_TRANSLATED);
			this.AddServerVariableToCollection("QUERY_STRING", DynamicServerVariable.QUERY_STRING);
			this.AddServerVariableToCollection("REMOTE_ADDR", this.UserHostAddress);
			this.AddServerVariableToCollection("REMOTE_HOST", this.UserHostName);
			this.AddServerVariableToCollection("REMOTE_PORT");
			this.AddServerVariableToCollection("REQUEST_METHOD", this.HttpMethod);
			this.AddServerVariableToCollection("SCRIPT_NAME", DynamicServerVariable.SCRIPT_NAME);
			this.AddServerVariableToCollection("SERVER_NAME", this._wr.GetServerName());
			this.AddServerVariableToCollection("SERVER_PORT", this._wr.GetLocalPortAsString());
			this.AddServerVariableToCollection("SERVER_PORT_SECURE", this._wr.IsSecure() ? "1" : "0");
			this.AddServerVariableToCollection("SERVER_PROTOCOL", this._wr.GetHttpVersion());
			this.AddServerVariableToCollection("SERVER_SOFTWARE");
			this.AddServerVariableToCollection("URL", DynamicServerVariable.SCRIPT_NAME);
			for (int i = 0; i < 40; i++)
			{
				string knownRequestHeader2 = this._wr.GetKnownRequestHeader(i);
				if (!string.IsNullOrEmpty(knownRequestHeader2))
				{
					this.AddServerVariableToCollection(HttpWorkerRequest.GetServerVariableNameFromKnownRequestHeaderIndex(i), knownRequestHeader2);
				}
			}
			string[][] unknownRequestHeaders = this._wr.GetUnknownRequestHeaders();
			if (unknownRequestHeaders != null)
			{
				for (int j = 0; j < unknownRequestHeaders.Length; j++)
				{
					this.AddServerVariableToCollection(HttpRequest.ServerVariableNameFromHeader(unknownRequestHeaders[j][0]), unknownRequestHeaders[j][1]);
				}
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00018964 File Offset: 0x00016B64
		internal static HttpCookie CreateCookieFromString(string s, bool useConfiguredDefaults)
		{
			HttpCookie httpCookie = new HttpCookie();
			if (useConfiguredDefaults)
			{
				httpCookie.SetDefaultsFromConfig();
			}
			int num = (s != null) ? s.Length : 0;
			int i = 0;
			bool flag = true;
			int num2 = 1;
			while (i < num)
			{
				int num3 = s.IndexOf('&', i);
				if (num3 < 0)
				{
					num3 = num;
				}
				int num4;
				if (flag)
				{
					num4 = s.IndexOf('=', i);
					if (num4 >= 0 && num4 < num3)
					{
						httpCookie.Name = s.Substring(i, num4 - i);
						i = num4 + 1;
					}
					else if (num3 == num)
					{
						httpCookie.Name = s;
						break;
					}
					flag = false;
				}
				num4 = s.IndexOf('=', i);
				if (num4 < 0 && num3 == num && num2 == 0)
				{
					httpCookie.Value = s.Substring(i, num - i);
				}
				else if (num4 >= 0 && num4 < num3)
				{
					httpCookie.Values.Add(s.Substring(i, num4 - i), s.Substring(num4 + 1, num3 - num4 - 1));
					num2++;
				}
				else
				{
					httpCookie.Values.Add(null, s.Substring(i, num3 - i));
					num2++;
				}
				i = num3 + 1;
			}
			return httpCookie;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00018A78 File Offset: 0x00016C78
		internal void FillInCookiesCollection(HttpCookieCollection cookieCollection, bool includeResponse)
		{
			if (this._wr == null)
			{
				return;
			}
			string knownRequestHeader = this._wr.GetKnownRequestHeader(25);
			int num = (knownRequestHeader != null) ? knownRequestHeader.Length : 0;
			int i = 0;
			HttpCookie httpCookie = null;
			while (i < num)
			{
				int j;
				for (j = i; j < num; j++)
				{
					char c = knownRequestHeader[j];
					if (c == ';')
					{
						break;
					}
				}
				string text = knownRequestHeader.Substring(i, j - i).Trim();
				i = j + 1;
				if (text.Length != 0)
				{
					HttpCookie httpCookie2 = HttpRequest.CreateCookieFromString(text, AppSettings.FixCookieDefaults);
					if (httpCookie != null)
					{
						string name = httpCookie2.Name;
						if (name != null && name.Length > 0 && name[0] == '$')
						{
							if (StringUtil.EqualsIgnoreCase(name, "$Path"))
							{
								httpCookie.Path = httpCookie2.Value;
								continue;
							}
							if (StringUtil.EqualsIgnoreCase(name, "$Domain"))
							{
								httpCookie.Domain = httpCookie2.Value;
								continue;
							}
							continue;
						}
					}
					cookieCollection.AddCookie(httpCookie2, true);
					httpCookie = httpCookie2;
				}
			}
			if (includeResponse)
			{
				HttpCookieCollection httpCookieCollection = this._storedResponseCookies;
				if (httpCookieCollection == null && !this.HasTransitionedToWebSocketRequest && this.Response != null)
				{
					httpCookieCollection = this.Response.GetCookiesNoCreate();
				}
				if (httpCookieCollection != null && httpCookieCollection.Count > 0)
				{
					if (AppSettings.AvoidDuplicatedSetCookie)
					{
						cookieCollection.Append(httpCookieCollection);
					}
					else
					{
						HttpCookie[] array = new HttpCookie[httpCookieCollection.Count];
						httpCookieCollection.CopyTo(array, 0);
						for (int k = 0; k < array.Length; k++)
						{
							cookieCollection.AddCookie(array[k], true);
						}
					}
				}
				this._storedResponseCookies = null;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00018BFD File Offset: 0x00016DFD
		internal void StoreReferenceToResponseCookies(HttpCookieCollection responseCookies)
		{
			this._storedResponseCookies = responseCookies;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00018C08 File Offset: 0x00016E08
		private void FillInParamsCollection()
		{
			this._params.Add(this.QueryString);
			this._params.Add(this.Form);
			this._params.Add(this.Cookies);
			this._params.Add(this.ServerVariables);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00018C5C File Offset: 0x00016E5C
		private void FillInFilesCollection()
		{
			if (this._wr == null)
			{
				return;
			}
			if (!StringUtil.StringStartsWithIgnoreCase(this.ContentType, "multipart/form-data"))
			{
				return;
			}
			MultipartContentElement[] multipartContent = this.GetMultipartContent();
			if (multipartContent == null)
			{
				return;
			}
			for (int i = 0; i < multipartContent.Length; i++)
			{
				if (multipartContent[i].IsFile)
				{
					HttpPostedFile asPostedFile = multipartContent[i].GetAsPostedFile();
					this._files.AddFile(multipartContent[i].Name, asPostedFile);
				}
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00018CC8 File Offset: 0x00016EC8
		private static string GetAttributeFromHeader(string headerValue, string attrName)
		{
			if (headerValue == null)
			{
				return null;
			}
			int length = headerValue.Length;
			int length2 = attrName.Length;
			int i;
			for (i = 1; i < length; i += length2)
			{
				i = CultureInfo.InvariantCulture.CompareInfo.IndexOf(headerValue, attrName, i, CompareOptions.IgnoreCase);
				if (i < 0 || i + length2 >= length)
				{
					break;
				}
				char c = headerValue[i - 1];
				char c2 = headerValue[i + length2];
				if ((c == ';' || c == ',' || char.IsWhiteSpace(c)) && (c2 == '=' || char.IsWhiteSpace(c2)))
				{
					break;
				}
			}
			if (i < 0 || i >= length)
			{
				return null;
			}
			i += length2;
			while (i < length && char.IsWhiteSpace(headerValue[i]))
			{
				i++;
			}
			if (i >= length || headerValue[i] != '=')
			{
				return null;
			}
			i++;
			while (i < length && char.IsWhiteSpace(headerValue[i]))
			{
				i++;
			}
			if (i >= length)
			{
				return null;
			}
			string result;
			if (i < length && headerValue[i] == '"')
			{
				if (i == length - 1)
				{
					return null;
				}
				int num = headerValue.IndexOf('"', i + 1);
				if (num < 0 || num == i + 1)
				{
					return null;
				}
				result = headerValue.Substring(i + 1, num - i - 1).Trim();
			}
			else
			{
				int num = i;
				while (num < length && headerValue[num] != ' ' && headerValue[num] != ',' && (AppSettings.UseLegacyMultiValueHeaderHandling || headerValue[num] != ';'))
				{
					num++;
				}
				if (num == i)
				{
					return null;
				}
				result = headerValue.Substring(i, num - i).Trim();
			}
			return result;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00018E44 File Offset: 0x00017044
		private Encoding GetEncodingFromHeaders()
		{
			if (this.UserAgent != null && CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this.UserAgent, "UP"))
			{
				string text = this.Headers["x-up-devcap-post-charset"];
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						return Encoding.GetEncoding(text);
					}
					catch
					{
					}
				}
			}
			if (!this._wr.HasEntityBody())
			{
				return null;
			}
			string contentType = this.ContentType;
			if (contentType == null)
			{
				return null;
			}
			string attributeFromHeader = HttpRequest.GetAttributeFromHeader(contentType, "charset");
			if (attributeFromHeader == null)
			{
				return null;
			}
			Encoding result = null;
			try
			{
				result = Encoding.GetEncoding(attributeFromHeader);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00018EF8 File Offset: 0x000170F8
		private HttpRawUploadedContent GetEntireRawContent()
		{
			if (this._wr == null)
			{
				return null;
			}
			if (this._rawContent != null)
			{
				if (this._installedFilter != null && !this._filterApplied)
				{
					this.ApplyFilter(ref this._rawContent, RuntimeConfig.GetConfig(this._context).HttpRuntime.RequestLengthDiskThresholdBytes);
				}
				return this._rawContent;
			}
			if (this._readEntityBodyMode == ReadEntityBodyMode.None)
			{
				this._readEntityBodyMode = ReadEntityBodyMode.Classic;
			}
			else
			{
				if (this._readEntityBodyMode == ReadEntityBodyMode.Buffered)
				{
					throw new InvalidOperationException(SR.GetString("Invalid_operation_with_get_buffered_input_stream"));
				}
				if (this._readEntityBodyMode == ReadEntityBodyMode.Bufferless)
				{
					throw new HttpException(SR.GetString("Incompatible_with_get_bufferless_input_stream"));
				}
			}
			HttpRuntimeSection httpRuntime = RuntimeConfig.GetConfig(this._context).HttpRuntime;
			int maxRequestLengthBytes = httpRuntime.MaxRequestLengthBytes;
			if (this.ContentLength > maxRequestLengthBytes)
			{
				if (!(this._wr is IIS7WorkerRequest))
				{
					this.Response.CloseConnectionAfterError();
				}
				throw new HttpException(SR.GetString("Max_request_length_exceeded"), null, 3004);
			}
			int requestLengthDiskThresholdBytes = httpRuntime.RequestLengthDiskThresholdBytes;
			HttpRawUploadedContent httpRawUploadedContent = new HttpRawUploadedContent(requestLengthDiskThresholdBytes, this.ContentLength);
			byte[] preloadedEntityBody = this._wr.GetPreloadedEntityBody();
			if (preloadedEntityBody != null)
			{
				this._wr.UpdateRequestCounters(preloadedEntityBody.Length);
				httpRawUploadedContent.AddBytes(preloadedEntityBody, 0, preloadedEntityBody.Length);
			}
			if (!this._wr.IsEntireEntityBodyIsPreloaded())
			{
				int i = (this.ContentLength > 0) ? (this.ContentLength - httpRawUploadedContent.Length) : int.MaxValue;
				HttpApplication applicationInstance = this._context.ApplicationInstance;
				byte[] array = (applicationInstance != null) ? applicationInstance.EntityBuffer : new byte[8192];
				int num = httpRawUploadedContent.Length;
				while (i > 0)
				{
					int num2 = array.Length;
					if (num2 > i)
					{
						num2 = i;
					}
					int num3 = this._wr.ReadEntityBody(array, num2);
					if (num3 <= 0)
					{
						break;
					}
					this._wr.UpdateRequestCounters(num3);
					httpRawUploadedContent.AddBytes(array, 0, num3);
					i -= num3;
					num += num3;
					if (num > maxRequestLengthBytes)
					{
						throw new HttpException(SR.GetString("Max_request_length_exceeded"), null, 3004);
					}
					if (i > 0 && this._context.HasTimeoutExpired)
					{
						throw new HttpException(SR.GetString("Request_timed_out"));
					}
				}
			}
			httpRawUploadedContent.DoneAddingBytes();
			if (this._installedFilter != null)
			{
				this.ApplyFilter(ref httpRawUploadedContent, requestLengthDiskThresholdBytes);
			}
			this.SetRawContent(httpRawUploadedContent);
			return this._rawContent;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001913C File Offset: 0x0001733C
		private void ApplyFilter(ref HttpRawUploadedContent rawContent, int fileThreshold)
		{
			if (this._installedFilter != null)
			{
				this._filterApplied = true;
				if (rawContent.Length > 0)
				{
					try
					{
						try
						{
							this._filterSource.SetContent(rawContent);
							HttpRawUploadedContent httpRawUploadedContent = new HttpRawUploadedContent(fileThreshold, rawContent.Length);
							HttpApplication applicationInstance = this._context.ApplicationInstance;
							byte[] array = (applicationInstance != null) ? applicationInstance.EntityBuffer : new byte[8192];
							for (;;)
							{
								int num = this._installedFilter.Read(array, 0, array.Length);
								if (num == 0)
								{
									break;
								}
								httpRawUploadedContent.AddBytes(array, 0, num);
							}
							httpRawUploadedContent.DoneAddingBytes();
							rawContent = httpRawUploadedContent;
						}
						finally
						{
							this._filterSource.SetContent(null);
						}
					}
					catch
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000191FC File Offset: 0x000173FC
		private MultipartContentElement[] GetMultipartContent()
		{
			if (this._multipartContentElements != null)
			{
				return this._multipartContentElements;
			}
			byte[] multipartBoundary = this.GetMultipartBoundary();
			if (multipartBoundary == null)
			{
				return new MultipartContentElement[0];
			}
			HttpRawUploadedContent entireRawContent = this.GetEntireRawContent();
			if (entireRawContent == null)
			{
				return new MultipartContentElement[0];
			}
			this._multipartContentElements = HttpMultipartContentTemplateParser.Parse(entireRawContent, entireRawContent.Length, multipartBoundary, this.ContentEncoding);
			return this._multipartContentElements;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001925C File Offset: 0x0001745C
		private byte[] GetMultipartBoundary()
		{
			string text = HttpRequest.GetAttributeFromHeader(this.ContentType, "boundary");
			if (text == null)
			{
				return null;
			}
			text = "--" + text;
			return Encoding.ASCII.GetBytes(text.ToCharArray());
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001929C File Offset: 0x0001749C
		internal void AddResponseCookie(HttpCookie cookie)
		{
			if (this._cookies != null)
			{
				this._cookies.AddCookie(cookie, true);
			}
			if (this._params != null)
			{
				this._params.MakeReadWrite();
				this._params.Add(cookie.Name, cookie.Value);
				this._params.MakeReadOnly();
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000192F4 File Offset: 0x000174F4
		internal void ResetCookies()
		{
			if (this._cookies != null)
			{
				this._cookies.Reset();
				this.FillInCookiesCollection(this._cookies, true);
			}
			if (this._params != null)
			{
				this._params.MakeReadWrite();
				this._params.Reset();
				this.FillInParamsCollection();
				this._params.MakeReadOnly();
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00019350 File Offset: 0x00017550
		public string HttpMethod
		{
			get
			{
				if (this._httpMethod == null)
				{
					this._httpMethod = this._wr.GetHttpVerbName();
				}
				return this._httpMethod;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00019374 File Offset: 0x00017574
		internal HttpVerb HttpVerb
		{
			get
			{
				if (this._httpVerb == HttpVerb.Unparsed)
				{
					this._httpVerb = HttpVerb.Unknown;
					string httpMethod = this.HttpMethod;
					if (httpMethod != null)
					{
						switch (httpMethod.Length)
						{
						case 3:
							if (httpMethod == "GET")
							{
								this._httpVerb = HttpVerb.GET;
							}
							else if (httpMethod == "PUT")
							{
								this._httpVerb = HttpVerb.PUT;
							}
							break;
						case 4:
							if (httpMethod == "POST")
							{
								this._httpVerb = HttpVerb.POST;
							}
							else if (httpMethod == "HEAD")
							{
								this._httpVerb = HttpVerb.HEAD;
							}
							break;
						case 5:
							if (httpMethod == "DEBUG")
							{
								this._httpVerb = HttpVerb.DEBUG;
							}
							break;
						case 6:
							if (httpMethod == "DELETE")
							{
								this._httpVerb = HttpVerb.DELETE;
							}
							break;
						}
					}
				}
				return this._httpVerb;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001944C File Offset: 0x0001764C
		internal bool IsDebuggingRequest
		{
			get
			{
				return this.HttpVerb == HttpVerb.DEBUG;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00019457 File Offset: 0x00017657
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0001946E File Offset: 0x0001766E
		public string RequestType
		{
			get
			{
				if (this._requestType == null)
				{
					return this.HttpMethod;
				}
				return this._requestType;
			}
			set
			{
				this._requestType = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00019477 File Offset: 0x00017677
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x000194B5 File Offset: 0x000176B5
		public string ContentType
		{
			get
			{
				if (this._contentType == null)
				{
					if (this._wr != null)
					{
						this._contentType = this._wr.GetKnownRequestHeader(12);
					}
					if (this._contentType == null)
					{
						this._contentType = string.Empty;
					}
				}
				return this._contentType;
			}
			set
			{
				this._contentType = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000194C0 File Offset: 0x000176C0
		public int ContentLength
		{
			get
			{
				if (this._contentLength == -1 && this._wr != null)
				{
					string knownRequestHeader = this._wr.GetKnownRequestHeader(11);
					if (knownRequestHeader != null)
					{
						try
						{
							this._contentLength = int.Parse(knownRequestHeader, CultureInfo.InvariantCulture);
							goto IL_5D;
						}
						catch
						{
							goto IL_5D;
						}
					}
					if (this._wr.IsEntireEntityBodyIsPreloaded())
					{
						byte[] preloadedEntityBody = this._wr.GetPreloadedEntityBody();
						if (preloadedEntityBody != null)
						{
							this._contentLength = preloadedEntityBody.Length;
						}
					}
				}
				IL_5D:
				if (this._contentLength < 0)
				{
					return 0;
				}
				return this._contentLength;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0001954C File Offset: 0x0001774C
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x000195D6 File Offset: 0x000177D6
		public Encoding ContentEncoding
		{
			get
			{
				if (this._flags[32] && this._encoding != null)
				{
					return this._encoding;
				}
				this._encoding = this.GetEncodingFromHeaders();
				if (this._encoding is UTF7Encoding && !AppSettings.AllowUtf7RequestContentEncoding)
				{
					this._encoding = null;
				}
				if (this._encoding == null)
				{
					GlobalizationSection globalization = RuntimeConfig.GetLKGConfig(this._context).Globalization;
					this._encoding = globalization.RequestEncoding;
				}
				this._flags.Set(32);
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._flags.Set(32);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000195EC File Offset: 0x000177EC
		internal Encoding QueryStringEncoding
		{
			get
			{
				Encoding contentEncoding = this.ContentEncoding;
				if (!contentEncoding.Equals(Encoding.Unicode))
				{
					return contentEncoding;
				}
				return Encoding.UTF8;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00019614 File Offset: 0x00017814
		public string[] AcceptTypes
		{
			get
			{
				if (this._acceptTypes == null && this._wr != null)
				{
					this._acceptTypes = HttpRequest.ParseMultivalueHeader(this._wr.GetKnownRequestHeader(20));
				}
				return this._acceptTypes;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00019644 File Offset: 0x00017844
		public bool IsAuthenticated
		{
			get
			{
				return this._context.User != null && this._context.User.Identity != null && this._context.User.Identity.IsAuthenticated;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001967C File Offset: 0x0001787C
		public bool IsSecureConnection
		{
			get
			{
				return this._wr != null && this._wr.IsSecure();
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00019694 File Offset: 0x00017894
		public string Path
		{
			get
			{
				string unvalidatedPath = this.GetUnvalidatedPath();
				if (this._flags[256])
				{
					this._flags.Clear(256);
					this.ValidateString(unvalidatedPath, null, RequestValidationSource.Path);
				}
				return unvalidatedPath;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000196D4 File Offset: 0x000178D4
		internal VirtualPath PathObject
		{
			get
			{
				if (this._path == null)
				{
					this._path = VirtualPath.Create(this._wr.GetUriPath(), VirtualPathOptions.AllowAbsolutePath);
				}
				return this._path;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00019701 File Offset: 0x00017901
		internal string GetUnvalidatedPath()
		{
			return this.PathObject.VirtualPathString;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001970E File Offset: 0x0001790E
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00019716 File Offset: 0x00017916
		public string AnonymousID
		{
			get
			{
				return this._anonymousId;
			}
			internal set
			{
				this._anonymousId = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00019720 File Offset: 0x00017920
		internal string PathWithQueryString
		{
			get
			{
				string queryStringText = this.QueryStringText;
				if (string.IsNullOrEmpty(queryStringText))
				{
					return this.Path;
				}
				return this.Path + "?" + queryStringText;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00019754 File Offset: 0x00017954
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0001979F File Offset: 0x0001799F
		internal VirtualPath ClientFilePath
		{
			get
			{
				if (this._clientFilePath == null)
				{
					string text = this.RawUrl;
					int num = text.IndexOf('?');
					if (num > -1)
					{
						text = text.Substring(0, num);
					}
					this._clientFilePath = VirtualPath.Create(text, VirtualPathOptions.AllowAbsolutePath);
				}
				return this._clientFilePath;
			}
			set
			{
				this._clientFilePath = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000197A8 File Offset: 0x000179A8
		internal VirtualPath ClientBaseDir
		{
			get
			{
				if (this._clientBaseDir == null)
				{
					if (this.ClientFilePath.HasTrailingSlash)
					{
						this._clientBaseDir = this.ClientFilePath;
					}
					else
					{
						this._clientBaseDir = this.ClientFilePath.Parent;
					}
				}
				return this._clientBaseDir;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000197F5 File Offset: 0x000179F5
		public string FilePath
		{
			get
			{
				return VirtualPath.GetVirtualPathString(this.FilePathObject);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00019804 File Offset: 0x00017A04
		internal VirtualPath FilePathObject
		{
			get
			{
				if (this._filePath != null)
				{
					return this._filePath;
				}
				if (!this._computePathInfo)
				{
					if (this._wr != null)
					{
						this._filePath = this._wr.GetFilePathObject();
					}
					else
					{
						this._filePath = this.PathObject;
					}
				}
				else if (this._context != null)
				{
					this._filePath = this.PathObject;
					int length = this._context.GetFilePathData().Path.VirtualPathStringNoTrailingSlash.Length;
					string path = this.Path;
					int length2 = path.Length;
					if (length2 != length && (length2 - length != 1 || path[length2 - 1] != '/' || path.IndexOf('.') > -1))
					{
						this._filePath = VirtualPath.CreateAbsolute(this.Path.Substring(0, length));
					}
				}
				return this._filePath;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x000198D7 File Offset: 0x00017AD7
		public string CurrentExecutionFilePath
		{
			get
			{
				return this.CurrentExecutionFilePathObject.VirtualPathString;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000198E4 File Offset: 0x00017AE4
		public string CurrentExecutionFilePathExtension
		{
			get
			{
				return UrlPath.GetExtension(this.CurrentExecutionFilePathObject.VirtualPathString);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000198F6 File Offset: 0x00017AF6
		internal VirtualPath CurrentExecutionFilePathObject
		{
			get
			{
				if (this._currentExecutionFilePath != null)
				{
					return this._currentExecutionFilePath;
				}
				return this.FilePathObject;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00019914 File Offset: 0x00017B14
		internal VirtualPath SwitchCurrentExecutionFilePath(VirtualPath path)
		{
			VirtualPath currentExecutionFilePath = this._currentExecutionFilePath;
			this._currentExecutionFilePath = path;
			return currentExecutionFilePath;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00019930 File Offset: 0x00017B30
		public string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				return UrlPath.MakeVirtualPathAppRelative(this.CurrentExecutionFilePath);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00019940 File Offset: 0x00017B40
		public string PathInfo
		{
			get
			{
				string unvalidatedPathInfo = this.GetUnvalidatedPathInfo();
				if (this._flags[512])
				{
					this._flags.Clear(512);
					this.ValidateString(unvalidatedPathInfo, null, RequestValidationSource.PathInfo);
				}
				return unvalidatedPathInfo;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00019980 File Offset: 0x00017B80
		internal VirtualPath PathInfoObject
		{
			get
			{
				if (this._pathInfo != null)
				{
					return this._pathInfo;
				}
				if (!this._computePathInfo && this._wr != null)
				{
					this._pathInfo = VirtualPath.CreateAbsoluteAllowNull(this._wr.GetPathInfo());
				}
				if (this._pathInfo == null && this._context != null)
				{
					VirtualPath pathObject = this.PathObject;
					int length = pathObject.VirtualPathString.Length;
					VirtualPath filePathObject = this.FilePathObject;
					int length2 = filePathObject.VirtualPathString.Length;
					if (filePathObject == null)
					{
						this._pathInfo = pathObject;
					}
					else if (pathObject == null || length <= length2)
					{
						this._pathInfo = null;
					}
					else
					{
						string virtualPath = pathObject.VirtualPathString.Substring(length2, length - length2);
						this._pathInfo = VirtualPath.CreateAbsolute(virtualPath);
					}
				}
				return this._pathInfo;
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00019A50 File Offset: 0x00017C50
		internal string GetUnvalidatedPathInfo()
		{
			VirtualPath pathInfoObject = this.PathInfoObject;
			if (!(pathInfoObject == null))
			{
				return pathInfoObject.VirtualPathString;
			}
			return string.Empty;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00019A7C File Offset: 0x00017C7C
		public string PhysicalPath
		{
			get
			{
				string physicalPathInternal = this.PhysicalPathInternal;
				InternalSecurityPermissions.PathDiscovery(physicalPathInternal).Demand();
				return physicalPathInternal;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00019A9C File Offset: 0x00017C9C
		internal string PhysicalPathInternal
		{
			get
			{
				if (this._pathTranslated == null)
				{
					if (!this._computePathInfo)
					{
						this._pathTranslated = this._wr.GetFilePathTranslated();
						if (HttpRuntime.IsMapPathRelaxed)
						{
							this._pathTranslated = HttpRuntime.GetRelaxedMapPathResult(this._pathTranslated);
						}
					}
					if (this._pathTranslated == null && this._wr != null)
					{
						this._pathTranslated = HostingEnvironment.MapPathInternal(this.FilePath);
					}
				}
				return this._pathTranslated;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00019B09 File Offset: 0x00017D09
		public string ApplicationPath
		{
			get
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00019B10 File Offset: 0x00017D10
		internal VirtualPath ApplicationPathObject
		{
			get
			{
				return HttpRuntime.AppDomainAppVirtualPathObject;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00019B17 File Offset: 0x00017D17
		public string PhysicalApplicationPath
		{
			get
			{
				InternalSecurityPermissions.AppPathDiscovery.Demand();
				if (this._wr != null)
				{
					return this._wr.GetAppPathTranslated();
				}
				return null;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00019B38 File Offset: 0x00017D38
		public string UserAgent
		{
			get
			{
				if (this._wr != null)
				{
					return this._wr.GetKnownRequestHeader(39);
				}
				return null;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00019B51 File Offset: 0x00017D51
		public string[] UserLanguages
		{
			get
			{
				if (this._userLanguages == null && this._wr != null)
				{
					this._userLanguages = HttpRequest.ParseMultivalueHeader(this._wr.GetKnownRequestHeader(23));
				}
				return this._userLanguages;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00019B84 File Offset: 0x00017D84
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00019BFC File Offset: 0x00017DFC
		public HttpBrowserCapabilities Browser
		{
			get
			{
				if (this._browsercaps != null)
				{
					return this._browsercaps;
				}
				if (!HttpRequest.s_browserCapsEvaled)
				{
					object obj = HttpRequest.s_browserLock;
					lock (obj)
					{
						if (!HttpRequest.s_browserCapsEvaled)
						{
							HttpCapabilitiesBase.GetBrowserCapabilities(this);
						}
						HttpRequest.s_browserCapsEvaled = true;
					}
				}
				this._browsercaps = HttpCapabilitiesBase.GetBrowserCapabilities(this);
				return this._browsercaps;
			}
			set
			{
				this._browsercaps = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00019C08 File Offset: 0x00017E08
		public string UserHostName
		{
			get
			{
				string text = (this._wr != null) ? this._wr.GetRemoteName() : null;
				if (string.IsNullOrEmpty(text))
				{
					text = this.UserHostAddress;
				}
				return text;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00019C3C File Offset: 0x00017E3C
		public string UserHostAddress
		{
			get
			{
				if (this._wr != null)
				{
					return this._wr.GetRemoteAddress();
				}
				return null;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00019C53 File Offset: 0x00017E53
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00019C92 File Offset: 0x00017E92
		public string RawUrl
		{
			get
			{
				this.EnsureRawUrl();
				if (this._flags[128])
				{
					this._flags.Clear(128);
					this.ValidateString(this._rawUrl, null, RequestValidationSource.RawUrl);
				}
				return this._rawUrl;
			}
			internal set
			{
				this._rawUrl = value;
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00019C9C File Offset: 0x00017E9C
		internal string EnsureRawUrl()
		{
			if (this._rawUrl == null)
			{
				string rawUrl;
				if (this._wr != null)
				{
					rawUrl = this._wr.GetRawUrl();
				}
				else
				{
					string unvalidatedPath = this.GetUnvalidatedPath();
					string queryStringText = this.QueryStringText;
					if (!string.IsNullOrEmpty(queryStringText))
					{
						rawUrl = unvalidatedPath + "?" + queryStringText;
					}
					else
					{
						rawUrl = unvalidatedPath;
					}
				}
				this._rawUrl = rawUrl;
			}
			return this._rawUrl;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00019CFC File Offset: 0x00017EFC
		internal string UrlInternal
		{
			get
			{
				string text = this.QueryStringText;
				if (!string.IsNullOrEmpty(text))
				{
					text = "?" + HttpEncoder.CollapsePercentUFromStringInternal(text, this.QueryStringEncoding);
				}
				if (AppSettings.UseHostHeaderForRequestUrl)
				{
					string knownRequestHeader = this._wr.GetKnownRequestHeader(28);
					try
					{
						if (!string.IsNullOrEmpty(knownRequestHeader))
						{
							string text2 = string.Concat(new string[]
							{
								this._wr.GetProtocol(),
								"://",
								knownRequestHeader,
								this.Path,
								text
							});
							this._url = new Uri(text2);
							return text2;
						}
					}
					catch (UriFormatException)
					{
					}
				}
				string text3 = this._wr.GetServerName();
				if (text3.IndexOf(':') >= 0 && text3[0] != '[')
				{
					text3 = "[" + text3 + "]";
				}
				if (this._wr.GetLocalPortAsString() == "80")
				{
					return string.Concat(new string[]
					{
						this._wr.GetProtocol(),
						"://",
						text3,
						this.Path,
						text
					});
				}
				return string.Concat(new string[]
				{
					this._wr.GetProtocol(),
					"://",
					text3,
					":",
					this._wr.GetLocalPortAsString(),
					this.Path,
					text
				});
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00019E70 File Offset: 0x00018070
		public Uri Url
		{
			get
			{
				if (this._url == null && this._wr != null)
				{
					this._url = this.BuildUrl(() => this.Path);
				}
				return this._url;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00019EA8 File Offset: 0x000180A8
		internal Uri BuildUrl(Func<string> pathAccessor)
		{
			Uri uri = null;
			string text = this.QueryStringText;
			if (!string.IsNullOrEmpty(text))
			{
				text = "?" + HttpEncoder.CollapsePercentUFromStringInternal(text, this.QueryStringEncoding);
			}
			if (AppSettings.UseHostHeaderForRequestUrl)
			{
				string knownRequestHeader = this._wr.GetKnownRequestHeader(28);
				try
				{
					if (!string.IsNullOrEmpty(knownRequestHeader))
					{
						uri = UriUtil.BuildUri(this._wr.GetProtocol(), Uri.UnescapeDataString(knownRequestHeader), null, pathAccessor(), text);
					}
				}
				catch (UriFormatException)
				{
				}
			}
			if (uri == null)
			{
				string text2 = this._wr.GetServerName();
				if (text2.IndexOf(':') >= 0 && text2[0] != '[')
				{
					text2 = "[" + text2 + "]";
				}
				uri = UriUtil.BuildUri(this._wr.GetProtocol(), Uri.UnescapeDataString(text2), this._wr.GetLocalPortAsString(), pathAccessor(), text);
			}
			return uri;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00019F94 File Offset: 0x00018194
		public Uri UrlReferrer
		{
			get
			{
				if (this._referrer == null && this._wr != null)
				{
					string knownRequestHeader = this._wr.GetKnownRequestHeader(36);
					if (!string.IsNullOrEmpty(knownRequestHeader))
					{
						try
						{
							if (knownRequestHeader.IndexOf("://", StringComparison.Ordinal) >= 0)
							{
								this._referrer = new Uri(knownRequestHeader);
							}
							else
							{
								this._referrer = new Uri(this.Url, knownRequestHeader);
							}
						}
						catch (HttpException)
						{
							this._referrer = null;
						}
					}
				}
				return this._referrer;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001A020 File Offset: 0x00018220
		internal string IfModifiedSince
		{
			get
			{
				if (this._wr == null)
				{
					return null;
				}
				return this._wr.GetKnownRequestHeader(30);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001A039 File Offset: 0x00018239
		internal string IfNoneMatch
		{
			get
			{
				if (this._wr == null)
				{
					return null;
				}
				return this._wr.GetKnownRequestHeader(31);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001A052 File Offset: 0x00018252
		public NameValueCollection Params
		{
			get
			{
				if (HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low))
				{
					return this.GetParams();
				}
				return this.GetParamsWithDemand();
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001A06D File Offset: 0x0001826D
		internal void InvalidateParams()
		{
			this._params = null;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001A076 File Offset: 0x00018276
		private NameValueCollection GetParams()
		{
			if (this._params == null)
			{
				this._params = new HttpValueCollection(64);
				this.FillInParamsCollection();
				this._params.MakeReadOnly();
			}
			return this._params;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001A0A4 File Offset: 0x000182A4
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		private NameValueCollection GetParamsWithDemand()
		{
			return this.GetParams();
		}

		// Token: 0x1700043B RID: 1083
		public string this[string key]
		{
			get
			{
				string text = this.QueryString[key];
				if (text != null)
				{
					return text;
				}
				text = this.Form[key];
				if (text != null)
				{
					return text;
				}
				HttpCookie httpCookie = this.Cookies[key];
				if (httpCookie != null)
				{
					return httpCookie.Value;
				}
				text = this.ServerVariables[key];
				if (text != null)
				{
					return text;
				}
				return null;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001A108 File Offset: 0x00018308
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0001A1B4 File Offset: 0x000183B4
		internal string QueryStringText
		{
			get
			{
				if (this._queryStringText == null)
				{
					if (this._wr != null)
					{
						byte[] queryStringBytes = this.QueryStringBytes;
						if (queryStringBytes != null)
						{
							if (queryStringBytes.Length != 0)
							{
								this._queryStringText = this.QueryStringEncoding.GetString(queryStringBytes);
							}
							else
							{
								this._queryStringText = string.Empty;
							}
						}
						else
						{
							this._queryStringText = this._wr.GetQueryString();
						}
					}
					if (this._queryStringText == null)
					{
						this._queryStringText = string.Empty;
					}
					if (this._queryStringText.Length > 0 && !AppSettings.UseLegacyRequestUrlGeneration)
					{
						this._queryStringText = this._queryStringText.Replace("#", "%23");
					}
				}
				return this._queryStringText;
			}
			set
			{
				this._queryStringText = value;
				this._queryStringOverriden = true;
				if (this._queryString != null)
				{
					this._params = null;
					this._queryString.MakeReadWrite();
					this._queryString.Reset();
					this.FillInQueryStringCollection();
					this._queryString.MakeReadOnly();
					this.Unvalidated.InvalidateQueryString();
				}
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001A210 File Offset: 0x00018410
		internal byte[] QueryStringBytes
		{
			get
			{
				if (this._queryStringOverriden)
				{
					return null;
				}
				if (this._queryStringBytes == null && this._wr != null)
				{
					this._queryStringBytes = this._wr.GetQueryStringRawBytes();
				}
				return this._queryStringBytes;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001A243 File Offset: 0x00018443
		public NameValueCollection QueryString
		{
			get
			{
				this.EnsureQueryString();
				if (this._flags[1])
				{
					this._flags.Clear(1);
					this.ValidateHttpValueCollection(this._queryString, RequestValidationSource.QueryString);
				}
				return this._queryString;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001A279 File Offset: 0x00018479
		internal HttpValueCollection EnsureQueryString()
		{
			if (this._queryString == null)
			{
				this._queryString = new HttpValueCollection();
				if (this._wr != null)
				{
					this.FillInQueryStringCollection();
				}
				this._queryString.MakeReadOnly();
			}
			return this._queryString;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001A2B0 File Offset: 0x000184B0
		internal bool HasQueryString
		{
			get
			{
				if (this._queryString != null)
				{
					return this._queryString.Count > 0;
				}
				byte[] queryStringBytes = this.QueryStringBytes;
				if (queryStringBytes != null)
				{
					return queryStringBytes.Length != 0;
				}
				return this.QueryStringText.Length > 0;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001A2F2 File Offset: 0x000184F2
		public NameValueCollection Form
		{
			get
			{
				this.EnsureForm();
				if (this._flags[2])
				{
					this._flags.Clear(2);
					this.ValidateHttpValueCollection(this._form, RequestValidationSource.Form);
				}
				return this._form;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001A328 File Offset: 0x00018528
		internal HttpValueCollection EnsureForm()
		{
			if (this._form == null)
			{
				this._form = new HttpValueCollection();
				if (this._wr != null)
				{
					this.FillInFormCollection();
				}
				this._form.MakeReadOnly();
			}
			return this._form;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001A35C File Offset: 0x0001855C
		internal bool HasForm
		{
			get
			{
				if (this._form != null)
				{
					return this._form.Count > 0;
				}
				return (this._wr == null || this._wr.HasEntityBody()) && this.Form.Count > 0;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001A39C File Offset: 0x0001859C
		internal HttpValueCollection SwitchForm(HttpValueCollection form)
		{
			HttpValueCollection form2 = this._form;
			this._form = form;
			this.Unvalidated.InvalidateForm();
			return form2;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public NameValueCollection Headers
		{
			get
			{
				this.EnsureHeaders();
				if (this._flags[8])
				{
					this._flags.Clear(8);
					this.ValidateHttpValueCollection(this._headers, RequestValidationSource.Headers);
				}
				if (this._flags[65536])
				{
					this._flags.Clear(65536);
					this.ValidateCookielessHeaderIfRequiredByConfig(this._headers["AspFilterSessionId"]);
				}
				return this._headers;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001A440 File Offset: 0x00018640
		internal HttpHeaderCollection EnsureHeaders()
		{
			if (this._headers == null)
			{
				this._headers = new HttpHeaderCollection(this._wr, this, 8);
				if (this._wr != null)
				{
					this.FillInHeadersCollection();
				}
				if (!(this._wr is IIS7WorkerRequest))
				{
					this._headers.MakeReadOnly();
				}
			}
			return this._headers;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0001A494 File Offset: 0x00018694
		public UnvalidatedRequestValues Unvalidated
		{
			get
			{
				if (this._unvalidatedRequestValues == null)
				{
					this._unvalidatedRequestValues = new UnvalidatedRequestValues(this);
				}
				return this._unvalidatedRequestValues;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001A4B0 File Offset: 0x000186B0
		public NameValueCollection ServerVariables
		{
			get
			{
				if (HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low))
				{
					return this.GetServerVars();
				}
				return this.GetServerVarsWithDemand();
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001A4CB File Offset: 0x000186CB
		internal NameValueCollection GetServerVarsWithoutDemand()
		{
			return this.GetServerVars();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001A4CB File Offset: 0x000186CB
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		private NameValueCollection GetServerVarsWithDemand()
		{
			return this.GetServerVars();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001A4D3 File Offset: 0x000186D3
		private NameValueCollection GetServerVars()
		{
			if (this._serverVariables == null)
			{
				this._serverVariables = new HttpServerVarsCollection(this._wr, this);
				if (!(this._wr is IIS7WorkerRequest))
				{
					this._serverVariables.MakeReadOnly();
				}
			}
			return this._serverVariables;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001A510 File Offset: 0x00018710
		internal void SetSkipAuthorization(bool value)
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				return;
			}
			if (this._serverVariables == null)
			{
				iis7WorkerRequest.SetServerVariable("IS_LOGIN_PAGE", value ? "1" : null);
				return;
			}
			this._serverVariables.SetNoDemand("IS_LOGIN_PAGE", value ? "1" : null);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001A568 File Offset: 0x00018768
		internal void SetDynamicCompression(bool enable)
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				return;
			}
			if (this._serverVariables == null)
			{
				iis7WorkerRequest.SetServerVariable("IIS_EnableDynamicCompression", enable ? null : "0");
				return;
			}
			this._serverVariables.SetNoDemand("IIS_EnableDynamicCompression", enable ? null : "0");
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001A5C0 File Offset: 0x000187C0
		internal void AppendToLogQueryString(string logData)
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null || string.IsNullOrEmpty(logData))
			{
				return;
			}
			if (this._serverVariables == null)
			{
				string serverVariable = iis7WorkerRequest.GetServerVariable("LOG_QUERY_STRING");
				if (string.IsNullOrEmpty(serverVariable))
				{
					iis7WorkerRequest.SetServerVariable("LOG_QUERY_STRING", this.QueryStringText + logData);
					return;
				}
				iis7WorkerRequest.SetServerVariable("LOG_QUERY_STRING", serverVariable + logData);
				return;
			}
			else
			{
				string text = this._serverVariables.Get("LOG_QUERY_STRING");
				if (string.IsNullOrEmpty(text))
				{
					this._serverVariables.SetNoDemand("LOG_QUERY_STRING", this.QueryStringText + logData);
					return;
				}
				this._serverVariables.SetNoDemand("LOG_QUERY_STRING", text + logData);
				return;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0001A679 File Offset: 0x00018879
		public HttpCookieCollection Cookies
		{
			get
			{
				this.EnsureCookies();
				if (this._flags[4])
				{
					this._flags.Clear(4);
					this.ValidateCookieCollection(this._cookies);
				}
				return this._cookies;
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001A6B0 File Offset: 0x000188B0
		internal HttpCookieCollection EnsureCookies()
		{
			if (this._cookies == null)
			{
				this._cookies = new HttpCookieCollection(null, false);
				if (this._wr != null)
				{
					this.FillInCookiesCollection(this._cookies, true);
				}
				if (this.HasTransitionedToWebSocketRequest)
				{
					this._cookies.MakeReadOnly();
				}
			}
			return this._cookies;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0001A700 File Offset: 0x00018900
		public HttpFileCollection Files
		{
			get
			{
				this.EnsureFiles();
				if (this._flags[64])
				{
					this._flags.Clear(64);
					this.ValidatePostedFileCollection(this._files);
				}
				return this._files;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001A738 File Offset: 0x00018938
		internal HttpFileCollection EnsureFiles()
		{
			if (this._files == null)
			{
				if (this._readEntityBodyMode == ReadEntityBodyMode.Bufferless)
				{
					throw new HttpException(SR.GetString("Incompatible_with_get_bufferless_input_stream"));
				}
				this._files = new HttpFileCollection();
				if (this._wr != null)
				{
					this.FillInFilesCollection();
				}
			}
			return this._files;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001A788 File Offset: 0x00018988
		public Stream InputStream
		{
			get
			{
				if (this._inputStream == null)
				{
					if (this._readEntityBodyMode == ReadEntityBodyMode.Bufferless)
					{
						throw new HttpException(SR.GetString("Incompatible_with_get_bufferless_input_stream"));
					}
					HttpRawUploadedContent httpRawUploadedContent = null;
					if (this._wr != null)
					{
						httpRawUploadedContent = this.GetEntireRawContent();
					}
					if (httpRawUploadedContent != null)
					{
						this._inputStream = new HttpInputStream(httpRawUploadedContent, 0, httpRawUploadedContent.Length);
					}
					else
					{
						this._inputStream = new HttpInputStream(null, 0, 0);
					}
				}
				return this._inputStream;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public int TotalBytes
		{
			get
			{
				Stream stream = (this._readEntityBodyStream != null) ? this._readEntityBodyStream : this.InputStream;
				if (stream == null)
				{
					return 0;
				}
				return (int)stream.Length;
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001A824 File Offset: 0x00018A24
		public byte[] BinaryRead(int count)
		{
			if (this._readEntityBodyMode == ReadEntityBodyMode.Bufferless)
			{
				throw new HttpException(SR.GetString("Incompatible_with_get_bufferless_input_stream"));
			}
			if (count < 0 || count > this.TotalBytes)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[count];
			int num = this.InputStream.Read(array, 0, count);
			if (num != count)
			{
				byte[] array2 = new byte[num];
				if (num > 0)
				{
					Array.Copy(array, array2, num);
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0001A89C File Offset: 0x00018A9C
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0001A8C6 File Offset: 0x00018AC6
		public Stream Filter
		{
			get
			{
				if (this._installedFilter != null)
				{
					return this._installedFilter;
				}
				if (this._filterSource == null)
				{
					this._filterSource = new HttpInputStreamFilterSource();
				}
				return this._filterSource;
			}
			set
			{
				if (this._filterSource == null)
				{
					throw new HttpException(SR.GetString("Invalid_request_filter"));
				}
				this._installedFilter = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001A8E7 File Offset: 0x00018AE7
		public HttpClientCertificate ClientCertificate
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				if (this._clientCertificate == null)
				{
					this._clientCertificate = this.CreateHttpClientCertificateWithAssert();
				}
				return this._clientCertificate;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001A903 File Offset: 0x00018B03
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private HttpClientCertificate CreateHttpClientCertificateWithAssert()
		{
			return new HttpClientCertificate(this._context);
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0001A910 File Offset: 0x00018B10
		public WindowsIdentity LogonUserIdentity
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
			get
			{
				if (this._logonUserIdentity == null && this._wr != null)
				{
					if (this._wr is IIS7WorkerRequest && this._context.NotificationContext != null && ((this._context.NotificationContext.CurrentNotification == RequestNotification.AuthenticateRequest && !this._context.NotificationContext.IsPostNotification) || this._context.NotificationContext.CurrentNotification < RequestNotification.AuthenticateRequest))
					{
						throw new InvalidOperationException(SR.GetString("Invalid_before_authentication"));
					}
					this._logonUserIdentity = this._wr.GetLogonUserIdentity();
				}
				return this._logonUserIdentity;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001A9A6 File Offset: 0x00018BA6
		private bool GranularValidationEnabled
		{
			get
			{
				return this._flags[1073741824];
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		private bool RequestValidationSuppressed
		{
			get
			{
				return this._flags[int.MinValue];
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001A9CC File Offset: 0x00018BCC
		public void ValidateInput()
		{
			if (this.ValidateInputWasCalled || this.RequestValidationSuppressed)
			{
				return;
			}
			this._flags.Set(32768);
			this._flags.Set(1);
			this._flags.Set(2);
			this._flags.Set(4);
			this._flags.Set(64);
			this._flags.Set(128);
			this._flags.Set(256);
			this._flags.Set(512);
			this._flags.Set(8);
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001AA67 File Offset: 0x00018C67
		internal bool ValidateInputWasCalled
		{
			get
			{
				return this._flags[32768];
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001AA7C File Offset: 0x00018C7C
		private bool CanValidateRequest()
		{
			return this._wr != null && !(this._wr is StateHttpWorkerRequest) && (!(this._wr is IIS7WorkerRequest) || (this._context.Response.StatusCode != 404 && this._context.Response.StatusCode != 400) || this._context.NotificationContext == null || (this._context.NotificationContext.CurrentNotification != RequestNotification.LogRequest && this._context.NotificationContext.CurrentNotification != RequestNotification.EndRequest));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001AB1C File Offset: 0x00018D1C
		internal void ValidateInputIfRequiredByConfig()
		{
			RuntimeConfig config = RuntimeConfig.GetConfig(this.Context);
			HttpRuntimeSection httpRuntime = config.HttpRuntime;
			if (this.CanValidateRequest())
			{
				string path = this.Path;
				if (path.Length > httpRuntime.MaxUrlLength)
				{
					throw new HttpException(400, SR.GetString("Url_too_long"));
				}
				if (this.QueryStringText.Length > httpRuntime.MaxQueryStringLength)
				{
					throw new HttpException(400, SR.GetString("QueryString_too_long"));
				}
				char[] requestPathInvalidCharactersArray = httpRuntime.RequestPathInvalidCharactersArray;
				if (requestPathInvalidCharactersArray != null && requestPathInvalidCharactersArray.Length != 0)
				{
					int num = path.IndexOfAny(requestPathInvalidCharactersArray);
					if (num >= 0)
					{
						string text = new string(path[num], 1);
						throw new HttpException(400, SR.GetString("Dangerous_input_detected", new object[]
						{
							"Request.Path",
							text
						}));
					}
					this._flags.Set(65536);
				}
			}
			Version requestValidationMode = httpRuntime.RequestValidationMode;
			if (requestValidationMode == VersionUtil.Framework00)
			{
				this._flags[int.MinValue] = true;
				return;
			}
			if (requestValidationMode >= VersionUtil.Framework40)
			{
				this.ValidateInput();
				if (requestValidationMode >= VersionUtil.Framework45)
				{
					this.EnableGranularRequestValidation();
				}
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001AC50 File Offset: 0x00018E50
		internal void ValidateCookielessHeaderIfRequiredByConfig(string header)
		{
			if (string.IsNullOrEmpty(header))
			{
				return;
			}
			if (!this.CanValidateRequest())
			{
				return;
			}
			char[] requestPathInvalidCharactersArray = RuntimeConfig.GetConfig(this.Context).HttpRuntime.RequestPathInvalidCharactersArray;
			if (requestPathInvalidCharactersArray != null && requestPathInvalidCharactersArray.Length != 0)
			{
				int num = header.IndexOfAny(requestPathInvalidCharactersArray);
				if (num >= 0)
				{
					string text = new string(header[num], 1);
					throw new HttpException(400, SR.GetString("Dangerous_input_detected", new object[]
					{
						"Request.Path",
						text
					}));
				}
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001ACCD File Offset: 0x00018ECD
		private static string RemoveNullCharacters(string s)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('\0') > -1)
			{
				return s.Replace("\0", string.Empty);
			}
			return s;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		private void ValidateString(string value, string collectionKey, RequestValidationSource requestCollection)
		{
			value = HttpRequest.RemoveNullCharacters(value);
			HttpContext context = this.HasTransitionedToWebSocketRequest ? null : this.Context;
			int num;
			if (!RequestValidator.Current.IsValidRequestString(context, value, requestCollection, collectionKey, out num))
			{
				string text = collectionKey + "=\"";
				int num2 = num - 10;
				if (num2 <= 0)
				{
					num2 = 0;
				}
				else
				{
					text += "...";
				}
				int num3 = num + 20;
				if (num3 >= value.Length)
				{
					num3 = value.Length;
					text = text + value.Substring(num2, num3 - num2) + "\"";
				}
				else
				{
					text = text + value.Substring(num2, num3 - num2) + "...\"";
				}
				string requestValidationSourceName = HttpRequest.GetRequestValidationSourceName(requestCollection);
				throw new HttpRequestValidationException(SR.GetString("Dangerous_input_detected", new object[]
				{
					requestValidationSourceName,
					text
				}));
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		internal void EnableGranularRequestValidation()
		{
			this._flags[1073741824] = true;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		private static string GetRequestValidationSourceName(RequestValidationSource requestCollection)
		{
			switch (requestCollection)
			{
			case RequestValidationSource.QueryString:
				return "Request.QueryString";
			case RequestValidationSource.Form:
				return "Request.Form";
			case RequestValidationSource.Cookies:
				return "Request.Cookies";
			case RequestValidationSource.Files:
				return "Request.Files";
			case RequestValidationSource.RawUrl:
				return "Request.RawUrl";
			case RequestValidationSource.Path:
				return "Request.Path";
			case RequestValidationSource.PathInfo:
				return "Request.PathInfo";
			case RequestValidationSource.Headers:
				return "Request.Headers";
			default:
				return "Request." + requestCollection.ToString();
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001AE50 File Offset: 0x00019050
		private void ValidateHttpValueCollection(HttpValueCollection collection, RequestValidationSource requestCollection)
		{
			if (this.GranularValidationEnabled)
			{
				collection.EnableGranularValidation(delegate(string key, string value)
				{
					this.ValidateString(value, key, requestCollection);
				});
				return;
			}
			int count = collection.Count;
			for (int i = 0; i < count; i++)
			{
				string key2 = collection.GetKey(i);
				if (HttpValueCollection.KeyIsCandidateForValidation(key2))
				{
					string value2 = collection.Get(i);
					if (!string.IsNullOrEmpty(value2))
					{
						this.ValidateString(value2, key2, requestCollection);
					}
				}
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001AED0 File Offset: 0x000190D0
		private void ValidateCookieCollection(HttpCookieCollection cc)
		{
			if (this.GranularValidationEnabled)
			{
				cc.EnableGranularValidation(delegate(string key, string value)
				{
					this.ValidateString(value, key, RequestValidationSource.Cookies);
				});
				return;
			}
			int count = cc.Count;
			for (int i = 0; i < count; i++)
			{
				string key2 = cc.GetKey(i);
				string value2 = cc.Get(i).Value;
				if (!string.IsNullOrEmpty(value2))
				{
					this.ValidateString(value2, key2, RequestValidationSource.Cookies);
				}
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001AF34 File Offset: 0x00019134
		private void ValidatePostedFileCollection(HttpFileCollection col)
		{
			if (this.GranularValidationEnabled)
			{
				col.EnableGranularValidation(delegate(string key, string value)
				{
					this.ValidateString(value, "filename", RequestValidationSource.Files);
				});
				return;
			}
			for (int i = 0; i < col.Count; i++)
			{
				string fileName = col[i].FileName;
				this.ValidateString(fileName, "filename", RequestValidationSource.Files);
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001AF88 File Offset: 0x00019188
		internal void ClearReferencesForWebSocketProcessing()
		{
			bool validateInputWasCalled = this.ValidateInputWasCalled;
			ReflectionUtil.Reset<HttpRequest>(this);
			if (validateInputWasCalled)
			{
				this.ValidateInput();
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001AFAC File Offset: 0x000191AC
		public int[] MapImageCoordinates(string imageFieldName)
		{
			double[] array = HttpRequest.MapImageCoordinatatesInternal(imageFieldName, this.HttpVerb, this.QueryString, this.Form);
			if (array != null)
			{
				return new int[]
				{
					(int)array[0],
					(int)array[1]
				};
			}
			return null;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001AFEB File Offset: 0x000191EB
		public double[] MapRawImageCoordinates(string imageFieldName)
		{
			return HttpRequest.MapImageCoordinatatesInternal(imageFieldName, this.HttpVerb, this.QueryString, this.Form);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001B008 File Offset: 0x00019208
		internal static double[] MapImageCoordinatatesInternal(string imageFieldName, HttpVerb verb, NameValueCollection queryString, NameValueCollection form)
		{
			switch (verb)
			{
			case HttpVerb.GET:
			case HttpVerb.HEAD:
			{
				NameValueCollection nameValueCollection = queryString;
				goto IL_26;
			}
			case HttpVerb.POST:
			{
				NameValueCollection nameValueCollection = form;
				goto IL_26;
			}
			}
			return null;
			IL_26:
			double[] result = null;
			try
			{
				NameValueCollection nameValueCollection;
				string text = nameValueCollection[imageFieldName + ".x"];
				string text2 = nameValueCollection[imageFieldName + ".y"];
				double num;
				double num2;
				if (text != null && text2 != null && HttpUtility.TryParseCoordinates(text, out num) && HttpUtility.TryParseCoordinates(text2, out num2))
				{
					result = new double[]
					{
						num,
						num2
					};
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001B0A4 File Offset: 0x000192A4
		public void SaveAs(string filename, bool includeHeaders)
		{
			if (!System.IO.Path.IsPathRooted(filename))
			{
				HttpRuntimeSection httpRuntime = RuntimeConfig.GetConfig(this._context).HttpRuntime;
				if (httpRuntime.RequireRootedSaveAsPath)
				{
					throw new HttpException(SR.GetString("SaveAs_requires_rooted_path", new object[]
					{
						filename
					}));
				}
			}
			FileStream fileStream = new FileStream(filename, FileMode.Create);
			try
			{
				if (includeHeaders)
				{
					TextWriter textWriter = new StreamWriter(fileStream);
					textWriter.Write(this.HttpMethod + " " + this.Path);
					string queryStringText = this.QueryStringText;
					if (!string.IsNullOrEmpty(queryStringText))
					{
						textWriter.Write("?" + queryStringText);
					}
					if (this._wr != null)
					{
						textWriter.Write(" " + this._wr.GetHttpVersion() + "\r\n");
						textWriter.Write(this.CombineAllHeaders(true));
					}
					else
					{
						textWriter.Write("\r\n");
					}
					textWriter.Write("\r\n");
					textWriter.Flush();
				}
				HttpInputStream httpInputStream = (HttpInputStream)this.InputStream;
				httpInputStream.WriteTo(fileStream);
				fileStream.Flush();
			}
			finally
			{
				fileStream.Close();
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001B1C8 File Offset: 0x000193C8
		public string MapPath(string virtualPath)
		{
			return this.MapPath(VirtualPath.CreateAllowNull(virtualPath));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001B1D6 File Offset: 0x000193D6
		internal string MapPath(VirtualPath virtualPath)
		{
			if (this._wr != null)
			{
				return this.MapPath(virtualPath, this.FilePathObject, true);
			}
			return virtualPath.MapPath();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001B1F8 File Offset: 0x000193F8
		public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			VirtualPath baseVirtualDir2;
			if (string.IsNullOrEmpty(baseVirtualDir))
			{
				baseVirtualDir2 = this.FilePathObject;
			}
			else
			{
				baseVirtualDir2 = VirtualPath.CreateTrailingSlash(baseVirtualDir);
			}
			return this.MapPath(VirtualPath.CreateAllowNull(virtualPath), baseVirtualDir2, allowCrossAppMapping);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001B22C File Offset: 0x0001942C
		internal string MapPath(VirtualPath virtualPath, VirtualPath baseVirtualDir, bool allowCrossAppMapping)
		{
			if (this._wr == null)
			{
				throw new HttpException(SR.GetString("Cannot_map_path_without_context"));
			}
			if (virtualPath == null)
			{
				virtualPath = VirtualPath.Create(".");
			}
			VirtualPath virtualPath2 = virtualPath;
			if (baseVirtualDir != null)
			{
				virtualPath = baseVirtualDir.Combine(virtualPath);
			}
			if (!allowCrossAppMapping)
			{
				virtualPath.FailIfNotWithinAppRoot();
			}
			string text = virtualPath.MapPathInternal();
			if (virtualPath.VirtualPathString == "/" && virtualPath2.VirtualPathString != "/" && !virtualPath2.HasTrailingSlash && UrlPath.PathEndsWithExtraSlash(text))
			{
				text = text.Substring(0, text.Length - 1);
			}
			InternalSecurityPermissions.PathDiscovery(text).Demand();
			return text;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001B2DC File Offset: 0x000194DC
		internal void InternalRewritePath(VirtualPath newPath, string newQueryString, bool rebaseClientPath)
		{
			this._pathTranslated = null;
			this._pathInfo = null;
			this._filePath = null;
			this._url = null;
			this.Unvalidated.InvalidateUrl();
			string rawUrl = this.RawUrl;
			this._path = newPath;
			if (rebaseClientPath)
			{
				this._clientBaseDir = null;
				this._clientFilePath = newPath;
			}
			this._computePathInfo = true;
			if (newQueryString != null)
			{
				this.QueryStringText = newQueryString;
			}
			this._rewrittenUrl = this._path.VirtualPathString;
			string queryStringText = this.QueryStringText;
			if (!string.IsNullOrEmpty(queryStringText))
			{
				this._rewrittenUrl = this._rewrittenUrl + "?" + queryStringText;
			}
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				iis7WorkerRequest.RewriteNotifyPipeline(this._path.VirtualPathString, newQueryString, rebaseClientPath);
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001B39C File Offset: 0x0001959C
		internal void InternalRewritePath(VirtualPath newFilePath, VirtualPath newPathInfo, string newQueryString, bool setClientFilePath)
		{
			this._pathTranslated = ((this._wr != null) ? newFilePath.MapPathInternal() : null);
			this._pathInfo = newPathInfo;
			this._filePath = newFilePath;
			this._url = null;
			this.Unvalidated.InvalidateUrl();
			string rawUrl = this.RawUrl;
			if (newPathInfo == null)
			{
				this._path = newFilePath;
			}
			else
			{
				string virtualPath = newFilePath.VirtualPathStringWhicheverAvailable + "/" + newPathInfo.VirtualPathString;
				this._path = VirtualPath.Create(virtualPath);
			}
			if (newQueryString != null)
			{
				this.QueryStringText = newQueryString;
			}
			this._rewrittenUrl = this._path.VirtualPathString;
			string queryStringText = this.QueryStringText;
			if (!string.IsNullOrEmpty(queryStringText))
			{
				this._rewrittenUrl = this._rewrittenUrl + "?" + queryStringText;
			}
			this._computePathInfo = false;
			if (setClientFilePath)
			{
				this._clientFilePath = newFilePath;
			}
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				string newPath = (this._path != null && this._path.VirtualPathString != null) ? this._path.VirtualPathString : string.Empty;
				iis7WorkerRequest.RewriteNotifyPipeline(newPath, newQueryString, setClientFilePath);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001B4B8 File Offset: 0x000196B8
		internal string RewrittenUrl
		{
			get
			{
				return this._rewrittenUrl;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001B4C0 File Offset: 0x000196C0
		internal string FetchServerVariable(string variable)
		{
			return this._wr.GetServerVariable(variable);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001B4D0 File Offset: 0x000196D0
		internal void SynchronizeServerVariable(string name, string value)
		{
			if (name == "IS_LOGIN_PAGE")
			{
				bool value2 = value != null && value != "0";
				this._context.SetSkipAuthorizationNoDemand(value2, true);
			}
			HttpServerVarsCollection httpServerVarsCollection = this.ServerVariables as HttpServerVarsCollection;
			if (httpServerVarsCollection != null)
			{
				httpServerVarsCollection.SynchronizeServerVariable(name, value, true);
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001B524 File Offset: 0x00019724
		internal void SynchronizeHeader(string name, string value)
		{
			HttpHeaderCollection httpHeaderCollection = this.Headers as HttpHeaderCollection;
			if (httpHeaderCollection != null)
			{
				httpHeaderCollection.SynchronizeHeader(name, value);
			}
			HttpServerVarsCollection httpServerVarsCollection = this.ServerVariables as HttpServerVarsCollection;
			if (httpServerVarsCollection != null)
			{
				string name2 = "HTTP_" + name.ToUpper(CultureInfo.InvariantCulture).Replace('-', '_');
				httpServerVarsCollection.SynchronizeServerVariable(name2, value, true);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001B580 File Offset: 0x00019780
		public ChannelBinding HttpChannelBinding
		{
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				if (this._wr is IIS7WorkerRequest)
				{
					return ((IIS7WorkerRequest)this._wr).HttpChannelBindingToken;
				}
				if (this._wr is ISAPIWorkerRequestInProc)
				{
					return ((ISAPIWorkerRequestInProc)this._wr).HttpChannelBindingToken;
				}
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001B5D0 File Offset: 0x000197D0
		public ITlsTokenBindingInfo TlsTokenBindingInfo
		{
			get
			{
				if (!this._tlsTokenBindingInfoResolved)
				{
					IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
					if (iis7WorkerRequest != null)
					{
						this._tlsTokenBindingInfo = iis7WorkerRequest.GetTlsTokenBindingInfo();
					}
					this._tlsTokenBindingInfoResolved = true;
				}
				return this._tlsTokenBindingInfo;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001B610 File Offset: 0x00019810
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		public void InsertEntityBody(byte[] buffer, int offset, int count)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("InvalidOffsetOrCount", new object[]
				{
					"offset",
					"count"
				}));
			}
			iis7WorkerRequest.InsertEntityBody(buffer, offset, count);
			this.NeedToInsertEntityBody = false;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001B6AC File Offset: 0x000198AC
		public void InsertEntityBody()
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			byte[] entityBody = this.EntityBody;
			if (entityBody == null)
			{
				return;
			}
			iis7WorkerRequest.InsertEntityBody(entityBody, 0, entityBody.Length);
			this.NeedToInsertEntityBody = false;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public ReadEntityBodyMode ReadEntityBodyMode
		{
			get
			{
				return this._readEntityBodyMode;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001B6FD File Offset: 0x000198FD
		public Stream GetBufferlessInputStream()
		{
			return this.GetInputStream(false, false);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001B707 File Offset: 0x00019907
		public Stream GetBufferlessInputStream(bool disableMaxRequestLength)
		{
			return this.GetInputStream(false, disableMaxRequestLength);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001B711 File Offset: 0x00019911
		public Stream GetBufferedInputStream()
		{
			return this.GetInputStream(true, false);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001B71C File Offset: 0x0001991C
		private Stream GetInputStream(bool persistEntityBody, bool disableMaxRequestLength = false)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			ReadEntityBodyMode readEntityBodyMode = persistEntityBody ? ReadEntityBodyMode.Buffered : ReadEntityBodyMode.Bufferless;
			ReadEntityBodyMode readEntityBodyMode2 = this._readEntityBodyMode;
			if (readEntityBodyMode2 == ReadEntityBodyMode.None)
			{
				this._readEntityBodyMode = readEntityBodyMode;
				this._readEntityBodyStream = new HttpBufferlessInputStream(this._context, persistEntityBody, disableMaxRequestLength);
			}
			else
			{
				if (readEntityBodyMode2 == ReadEntityBodyMode.Classic)
				{
					throw new HttpException(SR.GetString("Incompatible_with_input_stream"));
				}
				if (readEntityBodyMode2 != readEntityBodyMode)
				{
					throw new HttpException(persistEntityBody ? SR.GetString("Incompatible_with_get_bufferless_input_stream") : SR.GetString("Incompatible_with_get_buffered_input_stream"));
				}
			}
			return this._readEntityBodyStream;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001B79C File Offset: 0x0001999C
		public void Abort()
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				iis7WorkerRequest.AbortConnection();
				return;
			}
			throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001B7CE File Offset: 0x000199CE
		internal void EnsureHasNotTransitionedToWebSocket()
		{
			if (this.Context != null)
			{
				this.Context.EnsureHasNotTransitionedToWebSocket();
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public CancellationToken TimedOutToken
		{
			get
			{
				this.EnsureHasNotTransitionedToWebSocket();
				HttpContext context = this.Context;
				if (context == null)
				{
					return default(CancellationToken);
				}
				return context.TimedOutToken;
			}
		}

		// Token: 0x040003E0 RID: 992
		[DoNotReset]
		private HttpWorkerRequest _wr;

		// Token: 0x040003E1 RID: 993
		[DoNotReset]
		private HttpContext _context;

		// Token: 0x040003E2 RID: 994
		private string _httpMethod;

		// Token: 0x040003E3 RID: 995
		private HttpVerb _httpVerb;

		// Token: 0x040003E4 RID: 996
		private string _requestType;

		// Token: 0x040003E5 RID: 997
		private VirtualPath _path;

		// Token: 0x040003E6 RID: 998
		private string _rewrittenUrl;

		// Token: 0x040003E7 RID: 999
		private bool _computePathInfo;

		// Token: 0x040003E8 RID: 1000
		private VirtualPath _filePath;

		// Token: 0x040003E9 RID: 1001
		private VirtualPath _currentExecutionFilePath;

		// Token: 0x040003EA RID: 1002
		private VirtualPath _pathInfo;

		// Token: 0x040003EB RID: 1003
		private string _queryStringText;

		// Token: 0x040003EC RID: 1004
		private bool _queryStringOverriden;

		// Token: 0x040003ED RID: 1005
		private byte[] _queryStringBytes;

		// Token: 0x040003EE RID: 1006
		private string _pathTranslated;

		// Token: 0x040003EF RID: 1007
		private string _contentType;

		// Token: 0x040003F0 RID: 1008
		private int _contentLength = -1;

		// Token: 0x040003F1 RID: 1009
		private string _clientTarget;

		// Token: 0x040003F2 RID: 1010
		private string[] _acceptTypes;

		// Token: 0x040003F3 RID: 1011
		private string[] _userLanguages;

		// Token: 0x040003F4 RID: 1012
		private HttpBrowserCapabilities _browsercaps;

		// Token: 0x040003F5 RID: 1013
		private Uri _url;

		// Token: 0x040003F6 RID: 1014
		private Uri _referrer;

		// Token: 0x040003F7 RID: 1015
		private HttpInputStream _inputStream;

		// Token: 0x040003F8 RID: 1016
		private HttpClientCertificate _clientCertificate;

		// Token: 0x040003F9 RID: 1017
		private bool _tlsTokenBindingInfoResolved;

		// Token: 0x040003FA RID: 1018
		private ITlsTokenBindingInfo _tlsTokenBindingInfo;

		// Token: 0x040003FB RID: 1019
		private WindowsIdentity _logonUserIdentity;

		// Token: 0x040003FC RID: 1020
		[DoNotReset]
		private RequestContext _requestContext;

		// Token: 0x040003FD RID: 1021
		private string _rawUrl;

		// Token: 0x040003FE RID: 1022
		private Stream _readEntityBodyStream;

		// Token: 0x040003FF RID: 1023
		private ReadEntityBodyMode _readEntityBodyMode;

		// Token: 0x04000400 RID: 1024
		private UnvalidatedRequestValues _unvalidatedRequestValues;

		// Token: 0x04000401 RID: 1025
		private HttpValueCollection _params;

		// Token: 0x04000402 RID: 1026
		private HttpValueCollection _queryString;

		// Token: 0x04000403 RID: 1027
		private HttpValueCollection _form;

		// Token: 0x04000404 RID: 1028
		private HttpHeaderCollection _headers;

		// Token: 0x04000405 RID: 1029
		private HttpServerVarsCollection _serverVariables;

		// Token: 0x04000406 RID: 1030
		private HttpCookieCollection _cookies;

		// Token: 0x04000407 RID: 1031
		[DoNotReset]
		private HttpCookieCollection _storedResponseCookies;

		// Token: 0x04000408 RID: 1032
		private HttpFileCollection _files;

		// Token: 0x04000409 RID: 1033
		private HttpRawUploadedContent _rawContent;

		// Token: 0x0400040A RID: 1034
		private bool _needToInsertEntityBody;

		// Token: 0x0400040B RID: 1035
		private MultipartContentElement[] _multipartContentElements;

		// Token: 0x0400040C RID: 1036
		private Encoding _encoding;

		// Token: 0x0400040D RID: 1037
		private HttpInputStreamFilterSource _filterSource;

		// Token: 0x0400040E RID: 1038
		private Stream _installedFilter;

		// Token: 0x0400040F RID: 1039
		private bool _filterApplied;

		// Token: 0x04000410 RID: 1040
		private SimpleBitVector32 _flags;

		// Token: 0x04000411 RID: 1041
		private const int needToValidateQueryString = 1;

		// Token: 0x04000412 RID: 1042
		private const int needToValidateForm = 2;

		// Token: 0x04000413 RID: 1043
		private const int needToValidateCookies = 4;

		// Token: 0x04000414 RID: 1044
		private const int needToValidateHeaders = 8;

		// Token: 0x04000415 RID: 1045
		private const int needToValidateServerVariables = 16;

		// Token: 0x04000416 RID: 1046
		private const int contentEncodingResolved = 32;

		// Token: 0x04000417 RID: 1047
		private const int needToValidatePostedFiles = 64;

		// Token: 0x04000418 RID: 1048
		private const int needToValidateRawUrl = 128;

		// Token: 0x04000419 RID: 1049
		private const int needToValidatePath = 256;

		// Token: 0x0400041A RID: 1050
		private const int needToValidatePathInfo = 512;

		// Token: 0x0400041B RID: 1051
		private const int hasValidateInputBeenCalled = 32768;

		// Token: 0x0400041C RID: 1052
		private const int needToValidateCookielessHeader = 65536;

		// Token: 0x0400041D RID: 1053
		private const int granularValidationEnabled = 1073741824;

		// Token: 0x0400041E RID: 1054
		private const int requestValidationSuppressed = -2147483648;

		// Token: 0x0400041F RID: 1055
		internal static object s_browserLock = new object();

		// Token: 0x04000420 RID: 1056
		internal static bool s_browserCapsEvaled = false;

		// Token: 0x04000421 RID: 1057
		[DoNotReset]
		private string _anonymousId;

		// Token: 0x04000422 RID: 1058
		private VirtualPath _clientFilePath;

		// Token: 0x04000423 RID: 1059
		private VirtualPath _clientBaseDir;
	}
}
