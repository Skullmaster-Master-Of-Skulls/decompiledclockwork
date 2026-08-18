using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x020000B4 RID: 180
	internal sealed class OutputCacheModule : IHttpModule
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x00026689 File Offset: 0x00025689
		internal OutputCacheModule()
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00026694 File Offset: 0x00025694
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
							nameValueCollection = request.ServerVariables;
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
						goto IL_ED;
					default:
						goto IL_ED;
					}
					IL_12B:
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
					IL_ED:
					stringBuilder.Append("F");
					if (verb != HttpVerb.POST)
					{
						goto IL_12B;
					}
					array = cachedVary._params;
					if (request.HasForm && (array != null || cachedVary._varyByAllParams))
					{
						nameValueCollection = request.Form;
						flag = cachedVary._varyByAllParams;
						goto IL_12B;
					}
					goto IL_12B;
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
						byte[] inArray = MachineKeySection.HashData(asByteArray, null, 0, asByteArray.Length);
						string text2 = Convert.ToBase64String(inArray);
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

		// Token: 0x06000896 RID: 2198 RVA: 0x000269F0 File Offset: 0x000259F0
		private string CreateOutputCachedItemKey(HttpContext context, CachedVary cachedVary)
		{
			return OutputCacheModule.CreateOutputCachedItemKey(context.Request.Path, context.Request.HttpVerb, context, cachedVary);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00026A10 File Offset: 0x00025A10
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
				if (contentEncodings[j] == text)
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00026B2C File Offset: 0x00025B2C
		private static double GetAcceptableEncodingHelper(string coding, string acceptEncoding)
		{
			double result = -1.0;
			int i = 0;
			int length = coding.Length;
			int length2 = acceptEncoding.Length;
			int num = length2 - length;
			while (i < num)
			{
				int num2 = acceptEncoding.IndexOf(coding, i, StringComparison.Ordinal);
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

		// Token: 0x06000899 RID: 2201 RVA: 0x00026C08 File Offset: 0x00025C08
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

		// Token: 0x0600089A RID: 2202 RVA: 0x00026CA4 File Offset: 0x00025CA4
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

		// Token: 0x0600089B RID: 2203 RVA: 0x00026CF0 File Offset: 0x00025CF0
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

		// Token: 0x0600089C RID: 2204 RVA: 0x00026D59 File Offset: 0x00025D59
		private void RecordCacheMiss()
		{
			if (!this._recordedCacheMiss)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_RATIO_BASE);
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_MISSES);
				this._recordedCacheMiss = true;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00026D78 File Offset: 0x00025D78
		void IHttpModule.Init(HttpApplication app)
		{
			OutputCacheSection outputCache = RuntimeConfig.GetAppConfig().OutputCache;
			if (outputCache.EnableOutputCache)
			{
				app.ResolveRequestCache += this.OnEnter;
				app.UpdateRequestCache += this.OnLeave;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00026DBC File Offset: 0x00025DBC
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00026DC0 File Offset: 0x00025DC0
		internal void OnEnter(object source, EventArgs eventArgs)
		{
			string[] array = null;
			this._key = null;
			this._recordedCacheMiss = false;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			context.GetFilePathData();
			if (OutputCacheModule.s_cEntries <= 0)
			{
				return;
			}
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			switch (request.HttpVerb)
			{
			case HttpVerb.GET:
			case HttpVerb.HEAD:
			case HttpVerb.POST:
			{
				string text = this._key = this.CreateOutputCachedItemKey(context, null);
				CacheInternal cacheInternal = HttpRuntime.CacheInternal;
				object obj = cacheInternal.Get(text);
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
						obj = cacheInternal.Get(text);
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
									obj = cacheInternal.Get(text + contentEncodings[acceptableEncoding]);
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
							obj = cacheInternal.Get(text);
						}
					}
					if (obj == null)
					{
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
							cacheInternal.Remove(text);
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
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00027404 File Offset: 0x00026404
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
				if (httpCachePolicy.IsModified() && response.Cookies.Count <= 0 && response.StatusCode == 200 && (request.HttpVerb == HttpVerb.GET || request.HttpVerb == HttpVerb.POST) && response.IsBuffered())
				{
					bool flag2 = false;
					if (httpCachePolicy.GetCacheability() == HttpCacheability.Public && context.RequestRequiresAuthorization())
					{
						httpCachePolicy.SetCacheability(HttpCacheability.Private);
						flag2 = true;
					}
					if ((httpCachePolicy.GetCacheability() == HttpCacheability.Public || httpCachePolicy.GetCacheability() == HttpCacheability.ServerAndPrivate || httpCachePolicy.GetCacheability() == HttpCacheability.Server || flag2) && !httpCachePolicy.GetNoServerCaching() && (httpCachePolicy.HasExpirationPolicy() || httpCachePolicy.HasValidationPolicy()) && !httpCachePolicy.VaryByHeaders.GetVaryByUnspecifiedParameters() && (httpCachePolicy.VaryByParams.AcceptsParams() || (request.HttpVerb != HttpVerb.POST && !request.HasQueryString)) && (!httpCachePolicy.VaryByContentEncodings.IsModified() || httpCachePolicy.VaryByContentEncodings.IsCacheableEncoding(context.Response.GetHttpHeaderContentEncoding())))
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
			CacheInternal cacheInternal = HttpRuntime.CacheInternal;
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
			HttpRawResponse snapshot = response.GetSnapshot();
			string kernelCacheUrl = response.SetupKernelCaching(null);
			CachedRawResponse memoryRawResponse = new CachedRawResponse(snapshot, currentSettings, kernelCacheUrl);
			this.InsertResponse(response, context, text, currentSettings, cachedVary, memoryRawResponse);
			this._key = null;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000276D8 File Offset: 0x000266D8
		internal bool InsertResponse(HttpResponse response, HttpContext context, string keyRawResponse, HttpCachePolicySettings settings, CachedVary cachedVary, CachedRawResponse memoryRawResponse)
		{
			CacheInternal cacheInternal = HttpRuntime.CacheInternal;
			DateTime d;
			if (settings.UtcTimestampCreated != DateTime.MinValue)
			{
				d = settings.UtcTimestampCreated;
			}
			else
			{
				d = context.UtcTimestamp;
			}
			DateTime dateTime = DateTime.MaxValue;
			TimeSpan slidingExpiration;
			if (settings.SlidingExpiration)
			{
				slidingExpiration = settings.SlidingDelta;
				dateTime = Cache.NoAbsoluteExpiration;
			}
			else
			{
				slidingExpiration = Cache.NoSlidingExpiration;
				if (settings.IsMaxAgeSet)
				{
					dateTime = d + settings.MaxAge;
				}
				else if (settings.IsExpiresSet)
				{
					dateTime = settings.UtcExpires;
				}
			}
			if (dateTime < DateTime.UtcNow)
			{
				return false;
			}
			CacheDependency dependencyVary;
			if (cachedVary == null)
			{
				dependencyVary = null;
			}
			else
			{
				CachedVary cachedVary2 = (CachedVary)cacheInternal.UtcAdd(this._key, cachedVary, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
				if (cachedVary2 != null)
				{
					if (cachedVary.Equals(cachedVary2))
					{
						cachedVary = cachedVary2;
					}
					else
					{
						cacheInternal.UtcInsert(this._key, cachedVary);
					}
				}
				dependencyVary = new CacheDependency(0, null, new string[]
				{
					this._key
				});
			}
			CacheDependency dependencies = response.CreateCacheDependencyForResponse(dependencyVary);
			Interlocked.Increment(ref OutputCacheModule.s_cEntries);
			try
			{
				cacheInternal.UtcInsert(keyRawResponse, memoryRawResponse, dependencies, dateTime, slidingExpiration, CacheItemPriority.Normal, OutputCacheModule.s_cacheItemRemovedCallback);
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_ENTRIES);
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_TURNOVER_RATE);
			}
			catch
			{
				Interlocked.Decrement(ref OutputCacheModule.s_cEntries);
				throw;
			}
			return true;
		}

		// Token: 0x040011D4 RID: 4564
		private const int MAX_POST_KEY_LENGTH = 15000;

		// Token: 0x040011D5 RID: 4565
		private const string NULL_VARYBY_VALUE = "+n+";

		// Token: 0x040011D6 RID: 4566
		private const string ERROR_VARYBY_VALUE = "+e+";

		// Token: 0x040011D7 RID: 4567
		internal const string TAG_OUTPUTCACHE = "OutputCache";

		// Token: 0x040011D8 RID: 4568
		private const string OUTPUTCACHE_KEYPREFIX_POST = "a1";

		// Token: 0x040011D9 RID: 4569
		private const string OUTPUTCACHE_KEYPREFIX_GET = "a2";

		// Token: 0x040011DA RID: 4570
		private const string IDENTITY = "identity";

		// Token: 0x040011DB RID: 4571
		private const string ASTERISK = "*";

		// Token: 0x040011DC RID: 4572
		internal static readonly char[] s_fieldSeparators = new char[]
		{
			',',
			' '
		};

		// Token: 0x040011DD RID: 4573
		private static CacheItemRemovedCallback s_cacheItemRemovedCallback = new CacheItemRemovedCallback(new OutputCacheItemRemoved().CacheItemRemovedCallback);

		// Token: 0x040011DE RID: 4574
		internal static int s_cEntries;

		// Token: 0x040011DF RID: 4575
		private string _key;

		// Token: 0x040011E0 RID: 4576
		private bool _recordedCacheMiss;
	}
}
