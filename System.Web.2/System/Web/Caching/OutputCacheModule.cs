using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web.Configuration;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x02000899 RID: 2201
	internal sealed class OutputCacheModule : IHttpModule
	{
		// Token: 0x0600672D RID: 26413 RVA: 0x000030B5 File Offset: 0x000012B5
		internal OutputCacheModule()
		{
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x0016C428 File Offset: 0x0016A628
		internal static string CreateOutputCachedItemKey(string path, HttpVerb verb, HttpContext context, CachedVary cachedVary)
		{
			StringBuilder stringBuilder;
			if (verb == HttpVerb.POST)
			{
				stringBuilder = new StringBuilder("a1", path.Length + "a1".Length);
			}
			else
			{
				stringBuilder = new StringBuilder("a2", path.Length + "a2".Length);
			}
			stringBuilder.Append(CultureInfo.InvariantCulture.TextInfo.ToLower(path));
			if (cachedVary != null)
			{
				HttpRequest request = context.Request;
				int i = 0;
				while (i <= 2)
				{
					string[] array = null;
					NameValueCollection nameValueCollection = null;
					bool flag = false;
					switch (i)
					{
					case 0:
						stringBuilder.Append("H");
						array = cachedVary._headers;
						if (array != null)
						{
							nameValueCollection = request.GetServerVarsWithoutDemand();
						}
						break;
					case 1:
						stringBuilder.Append("Q");
						array = cachedVary._params;
						if (request.HasQueryString && (array != null || cachedVary._varyByAllParams))
						{
							nameValueCollection = request.QueryString;
							flag = cachedVary._varyByAllParams;
						}
						break;
					case 2:
						goto IL_E9;
					default:
						goto IL_E9;
					}
					IL_127:
					if (flag && nameValueCollection.Count > 0)
					{
						array = nameValueCollection.AllKeys;
						for (int j = array.Length - 1; j >= 0; j--)
						{
							if (array[j] != null)
							{
								array[j] = CultureInfo.InvariantCulture.TextInfo.ToLower(array[j]);
							}
						}
						Array.Sort(array, InvariantComparer.Default);
					}
					if (array != null)
					{
						int j = 0;
						int num = array.Length;
						while (j < num)
						{
							string text = array[j];
							string text2;
							if (nameValueCollection == null)
							{
								text2 = "+n+";
							}
							else
							{
								text2 = nameValueCollection[text];
								if (text2 == null)
								{
									text2 = "+n+";
								}
							}
							stringBuilder.Append("N");
							stringBuilder.Append(text);
							stringBuilder.Append("V");
							stringBuilder.Append(text2);
							j++;
						}
					}
					i++;
					continue;
					IL_E9:
					stringBuilder.Append("F");
					if (verb != HttpVerb.POST)
					{
						goto IL_127;
					}
					array = cachedVary._params;
					if (request.HasForm && (array != null || cachedVary._varyByAllParams))
					{
						nameValueCollection = request.Form;
						flag = cachedVary._varyByAllParams;
						goto IL_127;
					}
					goto IL_127;
				}
				stringBuilder.Append("C");
				if (cachedVary._varyByCustom != null)
				{
					stringBuilder.Append("N");
					stringBuilder.Append(cachedVary._varyByCustom);
					stringBuilder.Append("V");
					string text2;
					try
					{
						text2 = context.ApplicationInstance.GetVaryByCustomString(context, cachedVary._varyByCustom);
						if (text2 == null)
						{
							text2 = "+n+";
						}
					}
					catch (Exception error)
					{
						text2 = "+e+";
						HttpApplicationFactory.RaiseError(error);
					}
					stringBuilder.Append(text2);
				}
				stringBuilder.Append("D");
				if (verb == HttpVerb.POST && cachedVary._varyByAllParams && request.Form.Count == 0)
				{
					int contentLength = request.ContentLength;
					if (contentLength > 15000 || contentLength < 0)
					{
						return null;
					}
					if (contentLength > 0)
					{
						byte[] asByteArray = ((HttpInputStream)request.InputStream).GetAsByteArray();
						if (asByteArray == null)
						{
							return null;
						}
						string text2 = Convert.ToBase64String(CryptoUtil.ComputeSHA256Hash(asByteArray));
						stringBuilder.Append(text2);
					}
				}
				stringBuilder.Append("E");
				string[] contentEncodings = cachedVary._contentEncodings;
				if (contentEncodings != null)
				{
					string httpHeaderContentEncoding = context.Response.GetHttpHeaderContentEncoding();
					if (httpHeaderContentEncoding != null)
					{
						for (int k = 0; k < contentEncodings.Length; k++)
						{
							if (contentEncodings[k] == httpHeaderContentEncoding)
							{
								stringBuilder.Append(httpHeaderContentEncoding);
								break;
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x0016C774 File Offset: 0x0016A974
		private string CreateOutputCachedItemKey(HttpContext context, CachedVary cachedVary)
		{
			return OutputCacheModule.CreateOutputCachedItemKey(context.Request.Path, context.Request.HttpVerb, context, cachedVary);
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x0016C794 File Offset: 0x0016A994
		private static int GetAcceptableEncoding(string[] contentEncodings, int startIndex, string acceptEncoding)
		{
			if (string.IsNullOrEmpty(acceptEncoding))
			{
				return -1;
			}
			int num = acceptEncoding.IndexOf(',');
			if (num != -1)
			{
				int num2 = -1;
				double num3 = 0.0;
				for (int i = startIndex; i < contentEncodings.Length; i++)
				{
					string coding = contentEncodings[i];
					double acceptableEncodingHelper = OutputCacheModule.GetAcceptableEncodingHelper(coding, acceptEncoding);
					if (acceptableEncodingHelper == 1.0)
					{
						return i;
					}
					if (acceptableEncodingHelper > num3)
					{
						num2 = i;
						num3 = acceptableEncodingHelper;
					}
				}
				if (num2 == -1 && !OutputCacheModule.IsIdentityAcceptable(acceptEncoding))
				{
					num2 = -2;
				}
				return num2;
			}
			string text = acceptEncoding;
			num = acceptEncoding.IndexOf(';');
			if (num > -1)
			{
				int num4 = acceptEncoding.IndexOf(' ');
				if (num4 > -1 && num4 < num)
				{
					num = num4;
				}
				text = acceptEncoding.Substring(0, num);
				if (OutputCacheModule.ParseWeight(acceptEncoding, num) == 0.0)
				{
					if (!(text != "identity") || !(text != "*"))
					{
						return -2;
					}
					return -1;
				}
			}
			if (text == "*")
			{
				return startIndex;
			}
			for (int j = startIndex; j < contentEncodings.Length; j++)
			{
				if (StringUtil.EqualsIgnoreCase(contentEncodings[j], text))
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x0016C8B0 File Offset: 0x0016AAB0
		private static double GetAcceptableEncodingHelper(string coding, string acceptEncoding)
		{
			double result = -1.0;
			int i = 0;
			int length = coding.Length;
			int length2 = acceptEncoding.Length;
			int num = length2 - length;
			while (i < num)
			{
				int num2 = acceptEncoding.IndexOf(coding, i, StringComparison.OrdinalIgnoreCase);
				if (num2 != -1)
				{
					if (num2 != 0)
					{
						char c = acceptEncoding[num2 - 1];
						if (c != ' ' && c != ',')
						{
							i = num2 + 1;
							continue;
						}
					}
					int num3 = num2 + length;
					char c2 = '\0';
					if (num3 < length2)
					{
						c2 = acceptEncoding[num3];
						while (c2 == ' ' && ++num3 < length2)
						{
							c2 = acceptEncoding[num3];
						}
						if (c2 != ' ' && c2 != ',' && c2 != ';')
						{
							i = num2 + 1;
							continue;
						}
					}
					result = ((c2 == ';') ? OutputCacheModule.ParseWeight(acceptEncoding, num3) : 1.0);
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x0016C98C File Offset: 0x0016AB8C
		private static double ParseWeight(string acceptEncoding, int startIndex)
		{
			double result = 1.0;
			int num = acceptEncoding.IndexOf(',', startIndex);
			if (num == -1)
			{
				num = acceptEncoding.Length;
			}
			int num2 = acceptEncoding.IndexOf('q', startIndex);
			if (num2 > -1 && num2 < num)
			{
				int num3 = acceptEncoding.IndexOf('=', num2);
				if (num3 > -1 && num3 < num)
				{
					string s = acceptEncoding.Substring(num3 + 1, num - (num3 + 1));
					double num4;
					if (double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num4))
					{
						result = ((num4 >= 0.0 && num4 <= 1.0) ? num4 : 1.0);
					}
				}
			}
			return result;
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x0016CA28 File Offset: 0x0016AC28
		private static bool IsIdentityAcceptable(string acceptEncoding)
		{
			bool result = true;
			double acceptableEncodingHelper = OutputCacheModule.GetAcceptableEncodingHelper("identity", acceptEncoding);
			if (acceptableEncodingHelper == 0.0 || (acceptableEncodingHelper <= 0.0 && OutputCacheModule.GetAcceptableEncodingHelper("*", acceptEncoding) == 0.0))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x0016CA74 File Offset: 0x0016AC74
		private static bool IsAcceptableEncoding(string contentEncoding, string acceptEncoding)
		{
			if (string.IsNullOrEmpty(contentEncoding))
			{
				contentEncoding = "identity";
			}
			if (string.IsNullOrEmpty(acceptEncoding))
			{
				return contentEncoding == "identity";
			}
			double acceptableEncodingHelper = OutputCacheModule.GetAcceptableEncodingHelper(contentEncoding, acceptEncoding);
			return acceptableEncodingHelper != 0.0 && (acceptableEncodingHelper > 0.0 || OutputCacheModule.GetAcceptableEncodingHelper("*", acceptEncoding) != 0.0);
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x0016CADD File Offset: 0x0016ACDD
		private void RecordCacheMiss()
		{
			if (!this._recordedCacheMiss)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_RATIO_BASE);
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_MISSES);
				this._recordedCacheMiss = true;
			}
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x0016CAFC File Offset: 0x0016ACFC
		void IHttpModule.Init(HttpApplication app)
		{
			OutputCacheSection outputCache = RuntimeConfig.GetAppConfig().OutputCache;
			if (outputCache.EnableOutputCache)
			{
				app.ResolveRequestCache += this.OnEnter;
				app.UpdateRequestCache += this.OnLeave;
			}
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x00006164 File Offset: 0x00004364
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x0016CB40 File Offset: 0x0016AD40
		internal void OnEnter(object source, EventArgs eventArgs)
		{
			this._key = null;
			this._recordedCacheMiss = false;
			if (!OutputCache.InUse)
			{
				return;
			}
			string[] array = null;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			CachedPathData filePathData = context.GetFilePathData();
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			HttpVerb httpVerb = request.HttpVerb;
			if (httpVerb != HttpVerb.GET && httpVerb - HttpVerb.HEAD > 1)
			{
				return;
			}
			string text = this._key = this.CreateOutputCachedItemKey(context, null);
			object obj = OutputCache.Get(text);
			if (obj == null)
			{
				return;
			}
			CachedVary cachedVary = obj as CachedVary;
			if (cachedVary != null)
			{
				text = this.CreateOutputCachedItemKey(context, cachedVary);
				if (text == null)
				{
					return;
				}
				if (cachedVary._contentEncodings == null)
				{
					obj = OutputCache.Get(text);
				}
				else
				{
					obj = null;
					bool flag = true;
					string knownRequestHeader = context.WorkerRequest.GetKnownRequestHeader(22);
					if (knownRequestHeader != null)
					{
						string[] contentEncodings = cachedVary._contentEncodings;
						int num = 0;
						bool flag2 = false;
						while (!flag2)
						{
							flag2 = true;
							int acceptableEncoding = OutputCacheModule.GetAcceptableEncoding(contentEncodings, num, knownRequestHeader);
							if (acceptableEncoding > -1)
							{
								flag = false;
								obj = OutputCache.Get(text + contentEncodings[acceptableEncoding]);
								if (obj == null)
								{
									num = acceptableEncoding + 1;
									if (num < contentEncodings.Length)
									{
										flag2 = false;
									}
								}
							}
							else if (acceptableEncoding == -2)
							{
								flag = false;
							}
						}
					}
					if (obj == null && flag)
					{
						obj = OutputCache.Get(text);
					}
				}
				if (obj == null || ((CachedRawResponse)obj)._cachedVaryId != cachedVary.CachedVaryId)
				{
					if (obj != null)
					{
						OutputCache.Remove(text, context);
					}
					return;
				}
			}
			CachedRawResponse cachedRawResponse = (CachedRawResponse)obj;
			HttpCachePolicySettings settings = cachedRawResponse._settings;
			if (cachedVary == null && !settings.IgnoreParams)
			{
				if (request.HttpVerb == HttpVerb.POST)
				{
					this.RecordCacheMiss();
					return;
				}
				if (request.HasQueryString)
				{
					this.RecordCacheMiss();
					return;
				}
			}
			if (settings.IgnoreRangeRequests)
			{
				string s = request.Headers["Range"];
				if (StringUtil.StringStartsWithIgnoreCase(s, "bytes"))
				{
					return;
				}
			}
			if (!settings.HasValidationPolicy())
			{
				string text2 = request.Headers["Cache-Control"];
				if (text2 != null)
				{
					foreach (string text3 in text2.Split(OutputCacheModule.s_fieldSeparators))
					{
						if (text3 == "no-cache" || text3 == "no-store")
						{
							this.RecordCacheMiss();
							return;
						}
						if (StringUtil.StringStartsWith(text3, "max-age="))
						{
							int num2;
							try
							{
								num2 = Convert.ToInt32(text3.Substring(8), CultureInfo.InvariantCulture);
							}
							catch
							{
								num2 = -1;
							}
							if (num2 >= 0)
							{
								int num3 = (int)((context.UtcTimestamp.Ticks - settings.UtcTimestampCreated.Ticks) / 10000000L);
								if (num3 >= num2)
								{
									this.RecordCacheMiss();
									return;
								}
							}
						}
						else if (StringUtil.StringStartsWith(text3, "min-fresh="))
						{
							int num4;
							try
							{
								num4 = Convert.ToInt32(text3.Substring(10), CultureInfo.InvariantCulture);
							}
							catch
							{
								num4 = -1;
							}
							if (num4 >= 0 && settings.IsExpiresSet && !settings.SlidingExpiration)
							{
								int num5 = (int)((settings.UtcExpires.Ticks - context.UtcTimestamp.Ticks) / 10000000L);
								if (num5 < num4)
								{
									this.RecordCacheMiss();
									return;
								}
							}
						}
					}
				}
				string text4 = request.Headers["Pragma"];
				if (text4 != null)
				{
					string[] array2 = text4.Split(OutputCacheModule.s_fieldSeparators);
					for (int i = 0; i < array2.Length; i++)
					{
						if (array2[i] == "no-cache")
						{
							this.RecordCacheMiss();
							return;
						}
					}
				}
			}
			else if (settings.ValidationCallbackInfo != null)
			{
				HttpValidationStatus httpValidationStatus = HttpValidationStatus.Valid;
				HttpValidationStatus httpValidationStatus2 = httpValidationStatus;
				int i = 0;
				int num6 = settings.ValidationCallbackInfo.Length;
				while (i < num6)
				{
					ValidationCallbackInfo validationCallbackInfo = settings.ValidationCallbackInfo[i];
					try
					{
						validationCallbackInfo.handler(context, validationCallbackInfo.data, ref httpValidationStatus);
					}
					catch (Exception error)
					{
						httpValidationStatus = HttpValidationStatus.Invalid;
						HttpApplicationFactory.RaiseError(error);
					}
					switch (httpValidationStatus)
					{
					case HttpValidationStatus.Invalid:
						OutputCache.Remove(text, context);
						this.RecordCacheMiss();
						return;
					case HttpValidationStatus.IgnoreThisRequest:
						httpValidationStatus2 = HttpValidationStatus.IgnoreThisRequest;
						break;
					case HttpValidationStatus.Valid:
						break;
					default:
						httpValidationStatus = httpValidationStatus2;
						break;
					}
					i++;
				}
				if (httpValidationStatus2 == HttpValidationStatus.IgnoreThisRequest)
				{
					this.RecordCacheMiss();
					return;
				}
			}
			HttpRawResponse rawResponse = cachedRawResponse._rawResponse;
			if (cachedVary == null || cachedVary._contentEncodings == null)
			{
				string acceptEncoding = request.Headers["Accept-Encoding"];
				string contentEncoding = null;
				ArrayList headers = rawResponse.Headers;
				if (headers != null)
				{
					foreach (object obj2 in headers)
					{
						HttpResponseHeader httpResponseHeader = (HttpResponseHeader)obj2;
						if (httpResponseHeader.Name == "Content-Encoding")
						{
							contentEncoding = httpResponseHeader.Value;
							break;
						}
					}
				}
				if (!OutputCacheModule.IsAcceptableEncoding(contentEncoding, acceptEncoding))
				{
					this.RecordCacheMiss();
					return;
				}
			}
			int num7 = -1;
			if (!rawResponse.HasSubstBlocks)
			{
				string ifModifiedSince = request.IfModifiedSince;
				if (ifModifiedSince != null)
				{
					num7 = 0;
					try
					{
						DateTime dateTime = HttpDate.UtcParse(ifModifiedSince);
						if (settings.IsLastModifiedSet && settings.UtcLastModified <= dateTime && dateTime <= context.UtcTimestamp)
						{
							num7 = 1;
						}
					}
					catch
					{
					}
				}
				if (num7 != 0)
				{
					string ifNoneMatch = request.IfNoneMatch;
					if (ifNoneMatch != null)
					{
						num7 = 0;
						string[] array3 = ifNoneMatch.Split(OutputCacheModule.s_fieldSeparators);
						int i = 0;
						int num6 = array3.Length;
						while (i < num6)
						{
							if (i == 0 && array3[i].Equals("*"))
							{
								num7 = 1;
								break;
							}
							if (array3[i].Equals(settings.ETag))
							{
								num7 = 1;
								break;
							}
							i++;
						}
					}
				}
			}
			if (num7 == 1)
			{
				response.ClearAll();
				response.StatusCode = 304;
			}
			else
			{
				bool sendBody = request.HttpVerb != HttpVerb.HEAD;
				response.UseSnapshot(rawResponse, sendBody);
			}
			response.Cache.ResetFromHttpCachePolicySettings(settings, context.UtcTimestamp);
			string kernelCacheUrl = cachedRawResponse._kernelCacheUrl;
			if (kernelCacheUrl != null)
			{
				response.SetupKernelCaching(kernelCacheUrl);
			}
			PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_RATIO_BASE);
			PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_HITS);
			this._key = null;
			this._recordedCacheMiss = false;
			httpApplication.CompleteRequest();
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x0016D188 File Offset: 0x0016B388
		internal void OnLeave(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			HttpCachePolicy httpCachePolicy = null;
			bool flag = false;
			if (response.HasCachePolicy)
			{
				httpCachePolicy = response.Cache;
				if (httpCachePolicy.IsModified() && response.StatusCode == 200 && (request.HttpVerb == HttpVerb.GET || request.HttpVerb == HttpVerb.POST) && response.IsBuffered())
				{
					bool flag2 = false;
					if (httpCachePolicy.GetCacheability() == HttpCacheability.Public && context.RequestRequiresAuthorization())
					{
						httpCachePolicy.SetCacheability(HttpCacheability.Private);
						flag2 = true;
					}
					if ((httpCachePolicy.GetCacheability() == HttpCacheability.Public || httpCachePolicy.GetCacheability() == HttpCacheability.ServerAndPrivate || httpCachePolicy.GetCacheability() == HttpCacheability.Server || flag2) && !httpCachePolicy.GetNoServerCaching() && !response.ContainsNonShareableCookies() && (httpCachePolicy.HasExpirationPolicy() || httpCachePolicy.HasValidationPolicy()) && !httpCachePolicy.VaryByHeaders.GetVaryByUnspecifiedParameters() && (httpCachePolicy.VaryByParams.AcceptsParams() || (request.HttpVerb != HttpVerb.POST && !request.HasQueryString)) && (!httpCachePolicy.VaryByContentEncodings.IsModified() || httpCachePolicy.VaryByContentEncodings.IsCacheableEncoding(context.Response.GetHttpHeaderContentEncoding())))
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			this.RecordCacheMiss();
			HttpCachePolicySettings currentSettings = httpCachePolicy.GetCurrentSettings(response);
			string[] varyByContentEncodings = currentSettings.VaryByContentEncodings;
			string[] varyByHeaders = currentSettings.VaryByHeaders;
			string[] array;
			if (currentSettings.IgnoreParams)
			{
				array = null;
			}
			else
			{
				array = currentSettings.VaryByParams;
			}
			if (this._key == null)
			{
				this._key = this.CreateOutputCachedItemKey(context, null);
			}
			string text;
			CachedVary cachedVary;
			if (varyByContentEncodings == null && varyByHeaders == null && array == null && currentSettings.VaryByCustom == null)
			{
				text = this._key;
				cachedVary = null;
			}
			else
			{
				if (varyByHeaders != null)
				{
					int i = 0;
					int num = varyByHeaders.Length;
					while (i < num)
					{
						varyByHeaders[i] = "HTTP_" + CultureInfo.InvariantCulture.TextInfo.ToUpper(varyByHeaders[i].Replace('-', '_'));
						i++;
					}
				}
				bool flag3 = false;
				if (array != null)
				{
					flag3 = (array.Length == 1 && array[0] == "*");
					if (flag3)
					{
						array = null;
					}
					else
					{
						int i = 0;
						int num = array.Length;
						while (i < num)
						{
							array[i] = CultureInfo.InvariantCulture.TextInfo.ToLower(array[i]);
							i++;
						}
					}
				}
				cachedVary = new CachedVary(varyByContentEncodings, varyByHeaders, array, flag3, currentSettings.VaryByCustom);
				text = this.CreateOutputCachedItemKey(context, cachedVary);
				if (text == null)
				{
					return;
				}
				if (!response.IsBuffered())
				{
					return;
				}
			}
			DateTime dateTime = Cache.NoAbsoluteExpiration;
			TimeSpan slidingExp = Cache.NoSlidingExpiration;
			if (currentSettings.SlidingExpiration)
			{
				slidingExp = currentSettings.SlidingDelta;
			}
			else if (currentSettings.IsMaxAgeSet)
			{
				DateTime d = (currentSettings.UtcTimestampCreated != DateTime.MinValue) ? currentSettings.UtcTimestampCreated : context.UtcTimestamp;
				dateTime = d + currentSettings.MaxAge;
			}
			else if (currentSettings.IsExpiresSet)
			{
				dateTime = currentSettings.UtcExpires;
			}
			if (dateTime > DateTime.UtcNow)
			{
				HttpRawResponse snapshot = response.GetSnapshot();
				string kernelCacheUrl = response.SetupKernelCaching(null);
				Guid cachedVaryId = (cachedVary != null) ? cachedVary.CachedVaryId : Guid.Empty;
				CachedRawResponse rawResponse = new CachedRawResponse(snapshot, currentSettings, kernelCacheUrl, cachedVaryId);
				CacheDependency cacheDependency = response.CreateCacheDependencyForResponse();
				try
				{
					OutputCache.InsertResponse(this._key, cachedVary, text, rawResponse, cacheDependency, dateTime, slidingExp);
				}
				catch
				{
					if (cacheDependency != null)
					{
						cacheDependency.Dispose();
					}
					throw;
				}
			}
			this._key = null;
		}

		// Token: 0x04003551 RID: 13649
		private const int MAX_POST_KEY_LENGTH = 15000;

		// Token: 0x04003552 RID: 13650
		private const string NULL_VARYBY_VALUE = "+n+";

		// Token: 0x04003553 RID: 13651
		private const string ERROR_VARYBY_VALUE = "+e+";

		// Token: 0x04003554 RID: 13652
		internal const string TAG_OUTPUTCACHE = "OutputCache";

		// Token: 0x04003555 RID: 13653
		private const string OUTPUTCACHE_KEYPREFIX_POST = "a1";

		// Token: 0x04003556 RID: 13654
		private const string OUTPUTCACHE_KEYPREFIX_GET = "a2";

		// Token: 0x04003557 RID: 13655
		private const string IDENTITY = "identity";

		// Token: 0x04003558 RID: 13656
		private const string ASTERISK = "*";

		// Token: 0x04003559 RID: 13657
		internal static readonly char[] s_fieldSeparators = new char[]
		{
			',',
			' '
		};

		// Token: 0x0400355A RID: 13658
		private string _key;

		// Token: 0x0400355B RID: 13659
		private bool _recordedCacheMiss;
	}
}
