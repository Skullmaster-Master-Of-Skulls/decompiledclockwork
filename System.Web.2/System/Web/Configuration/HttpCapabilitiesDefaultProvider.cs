using System;
using System.Collections;
using System.Globalization;
using System.Security;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	// Token: 0x020006F4 RID: 1780
	public class HttpCapabilitiesDefaultProvider : HttpCapabilitiesProvider
	{
		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x0012CE56 File Offset: 0x0012B056
		// (set) Token: 0x060055E4 RID: 21988 RVA: 0x0012CE5E File Offset: 0x0012B05E
		public int UserAgentCacheKeyLength
		{
			get
			{
				return this._userAgentCacheKeyLength;
			}
			set
			{
				this._userAgentCacheKeyLength = value;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x060055E5 RID: 21989 RVA: 0x0012CE67 File Offset: 0x0012B067
		// (set) Token: 0x060055E6 RID: 21990 RVA: 0x0012CE6F File Offset: 0x0012B06F
		public Type ResultType
		{
			get
			{
				return this._resultType;
			}
			set
			{
				this._resultType = value;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x060055E7 RID: 21991 RVA: 0x0012CE78 File Offset: 0x0012B078
		// (set) Token: 0x060055E8 RID: 21992 RVA: 0x0012CE80 File Offset: 0x0012B080
		public TimeSpan CacheTime
		{
			get
			{
				return this._cachetime;
			}
			set
			{
				this._cachetime = value;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x0012CE89 File Offset: 0x0012B089
		// (set) Token: 0x060055EA RID: 21994 RVA: 0x0012CE91 File Offset: 0x0012B091
		internal string BrowserCapabilitiesProviderType
		{
			get
			{
				return this._browserCapabilitiesProviderType;
			}
			set
			{
				this._browserCapabilitiesProviderType = value;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x060055EB RID: 21995 RVA: 0x0012CE9C File Offset: 0x0012B09C
		// (set) Token: 0x060055EC RID: 21996 RVA: 0x0012CEDE File Offset: 0x0012B0DE
		internal HttpCapabilitiesProvider BrowserCapabilitiesProvider
		{
			get
			{
				if (this._browserCapabilitiesProvider == null && this.BrowserCapabilitiesProviderType != null)
				{
					Type type = Type.GetType(this.BrowserCapabilitiesProviderType, true, true);
					this._browserCapabilitiesProvider = (HttpCapabilitiesProvider)Activator.CreateInstance(type);
				}
				return this._browserCapabilitiesProvider;
			}
			set
			{
				this._browserCapabilitiesProvider = value;
			}
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0012CEE8 File Offset: 0x0012B0E8
		public HttpCapabilitiesDefaultProvider() : this(RuntimeConfig.GetAppConfig().BrowserCaps)
		{
			if (RuntimeConfig.GetAppConfig().BrowserCaps != null)
			{
				this._userAgentCacheKeyLength = RuntimeConfig.GetAppConfig().BrowserCaps.UserAgentCacheKeyLength;
			}
			if (this._userAgentCacheKeyLength == 0)
			{
				this._userAgentCacheKeyLength = 64;
			}
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0012CF38 File Offset: 0x0012B138
		public HttpCapabilitiesDefaultProvider(HttpCapabilitiesDefaultProvider parent)
		{
			this._cacheKeyPrefix = "e" + Interlocked.Increment(ref HttpCapabilitiesDefaultProvider._idCounter).ToString(CultureInfo.InvariantCulture);
			if (parent == null)
			{
				this.ClearParent();
			}
			else
			{
				this._rule = parent._rule;
				if (parent._variables == null)
				{
					this._variables = null;
				}
				else
				{
					this._variables = new Hashtable(parent._variables);
				}
				this._cachetime = parent._cachetime;
				this._resultType = parent._resultType;
			}
			this.AddDependency(string.Empty);
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x0012CFCE File Offset: 0x0012B1CE
		internal BrowserCapabilitiesFactoryBase BrowserCapFactory
		{
			get
			{
				return BrowserCapabilitiesCompiler.BrowserCapabilitiesFactory;
			}
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x0012CFD5 File Offset: 0x0012B1D5
		internal void ClearParent()
		{
			this._rule = null;
			this._cachetime = TimeSpan.FromSeconds(60.0);
			this._variables = new Hashtable();
			this._resultType = typeof(HttpCapabilitiesBase);
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x0012D00D File Offset: 0x0012B20D
		public void AddDependency(string variable)
		{
			if (variable.Equals("HTTP_USER_AGENT"))
			{
				variable = string.Empty;
			}
			this._variables[variable] = true;
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x0012D035 File Offset: 0x0012B235
		public virtual void AddRuleList(ArrayList ruleList)
		{
			if (ruleList.Count == 0)
			{
				return;
			}
			if (this._rule != null)
			{
				ruleList.Insert(0, this._rule);
			}
			this._rule = new CapabilitiesSection(2, null, null, ruleList);
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x0012D064 File Offset: 0x0012B264
		internal static string GetUserAgent(HttpRequest request)
		{
			string text;
			if (request.ClientTarget.Length > 0)
			{
				text = HttpCapabilitiesDefaultProvider.GetUserAgentFromClientTarget(request.Context.ConfigurationPath, request.ClientTarget);
			}
			else
			{
				text = request.UserAgent;
			}
			if (text != null && text.Length > 512)
			{
				text = text.Substring(0, 512);
			}
			return text;
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x0012D0C0 File Offset: 0x0012B2C0
		internal static string GetUserAgentFromClientTarget(VirtualPath configPath, string clientTarget)
		{
			ClientTargetSection clientTarget2 = RuntimeConfig.GetConfig(configPath).ClientTarget;
			string text = null;
			if (clientTarget2.ClientTargets[clientTarget] != null)
			{
				text = clientTarget2.ClientTargets[clientTarget].UserAgent;
			}
			if (text == null)
			{
				throw new HttpException(SR.GetString("Invalid_client_target", new object[]
				{
					clientTarget
				}));
			}
			return text;
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x0012D11C File Offset: 0x0012B31C
		private void CacheBrowserCapResult(ref HttpCapabilitiesBase result)
		{
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			if (result.Capabilities == null)
			{
				return;
			}
			string text = "z";
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in result.Capabilities.Keys)
			{
				string text2 = (string)obj;
				if (!string.IsNullOrEmpty(text2))
				{
					string text3 = (string)result.Capabilities[text2];
					if (text3 != null)
					{
						stringBuilder.Append(text2);
						stringBuilder.Append("$");
						stringBuilder.Append(text3);
						stringBuilder.Append("$");
					}
				}
			}
			text += stringBuilder.ToString().GetHashCode().ToString(CultureInfo.InvariantCulture);
			HttpCapabilitiesBase httpCapabilitiesBase = internalCache.Get(text) as HttpCapabilitiesBase;
			if (httpCapabilitiesBase != null)
			{
				result = httpCapabilitiesBase;
				return;
			}
			internalCache.Insert(text, result, new CacheInsertOptions
			{
				SlidingExpiration = this._cachetime
			});
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x0012D238 File Offset: 0x0012B438
		public override HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request)
		{
			return (HttpBrowserCapabilities)this.Evaluate(request);
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x0012D248 File Offset: 0x0012B448
		internal HttpCapabilitiesBase Evaluate(HttpRequest request)
		{
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string userAgent = HttpCapabilitiesDefaultProvider.GetUserAgent(request);
			string text = userAgent;
			if (text != null && text.Length > this.UserAgentCacheKeyLength)
			{
				text = text.Substring(0, this.UserAgentCacheKeyLength);
			}
			bool flag = false;
			string text2 = this._cacheKeyPrefix + text;
			object obj = internalCache.Get(text2);
			HttpCapabilitiesBase httpCapabilitiesBase = obj as HttpCapabilitiesBase;
			if (httpCapabilitiesBase != null)
			{
				return httpCapabilitiesBase;
			}
			if (obj == HttpCapabilitiesDefaultProvider._disableOptimisticCachingSingleton)
			{
				flag = true;
			}
			else
			{
				httpCapabilitiesBase = this.EvaluateFinal(request, true);
				if (httpCapabilitiesBase.UseOptimizedCacheKey)
				{
					this.CacheBrowserCapResult(ref httpCapabilitiesBase);
					internalCache.Insert(text2, httpCapabilitiesBase, new CacheInsertOptions
					{
						SlidingExpiration = this._cachetime
					});
					return httpCapabilitiesBase;
				}
			}
			IDictionaryEnumerator enumerator = this._variables.GetEnumerator();
			StringBuilder stringBuilder = new StringBuilder(this._cacheKeyPrefix);
			InternalSecurityPermissions.AspNetHostingPermissionLevelLow.Assert();
			while (enumerator.MoveNext())
			{
				string text3 = (string)enumerator.Key;
				string text4;
				if (text3.Length == 0)
				{
					text4 = userAgent;
				}
				else
				{
					text4 = request.ServerVariables[text3];
				}
				if (text4 != null)
				{
					stringBuilder.Append(text4);
				}
			}
			CodeAccessPermission.RevertAssert();
			stringBuilder.Append(BrowserCapabilitiesFactoryBase.GetBrowserCapKey(this.BrowserCapFactory.InternalGetMatchedHeaders(), request));
			string key = stringBuilder.ToString();
			if (userAgent == null || flag)
			{
				httpCapabilitiesBase = (internalCache.Get(key) as HttpCapabilitiesBase);
				if (httpCapabilitiesBase != null)
				{
					return httpCapabilitiesBase;
				}
			}
			httpCapabilitiesBase = this.EvaluateFinal(request, false);
			this.CacheBrowserCapResult(ref httpCapabilitiesBase);
			internalCache.Insert(key, httpCapabilitiesBase, new CacheInsertOptions
			{
				SlidingExpiration = this._cachetime
			});
			if (text2 != null)
			{
				internalCache.Insert(text2, HttpCapabilitiesDefaultProvider._disableOptimisticCachingSingleton, new CacheInsertOptions
				{
					SlidingExpiration = this._cachetime
				});
			}
			return httpCapabilitiesBase;
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x0012D3EC File Offset: 0x0012B5EC
		internal HttpCapabilitiesBase EvaluateFinal(HttpRequest request, bool onlyEvaluateUserAgent)
		{
			HttpBrowserCapabilities httpBrowserCapabilities = this.BrowserCapFactory.GetHttpBrowserCapabilities(request);
			CapabilitiesState capabilitiesState = new CapabilitiesState(request, httpBrowserCapabilities.Capabilities);
			if (onlyEvaluateUserAgent)
			{
				capabilitiesState.EvaluateOnlyUserAgent = true;
			}
			if (this._rule != null)
			{
				string value = httpBrowserCapabilities["isMobileDevice"];
				httpBrowserCapabilities.Capabilities["isMobileDevice"] = null;
				this._rule.Evaluate(capabilitiesState);
				string text = httpBrowserCapabilities["isMobileDevice"];
				if (text == null)
				{
					httpBrowserCapabilities.Capabilities["isMobileDevice"] = value;
				}
				else if (text.Equals("true"))
				{
					httpBrowserCapabilities.DisableOptimizedCacheKey();
				}
			}
			HttpCapabilitiesBase httpCapabilitiesBase = (HttpCapabilitiesBase)HttpRuntime.CreateNonPublicInstance(this._resultType);
			httpCapabilitiesBase.InitInternal(httpBrowserCapabilities);
			return httpCapabilitiesBase;
		}

		// Token: 0x04002D9D RID: 11677
		internal CapabilitiesRule _rule;

		// Token: 0x04002D9E RID: 11678
		internal Hashtable _variables;

		// Token: 0x04002D9F RID: 11679
		internal Type _resultType;

		// Token: 0x04002DA0 RID: 11680
		internal TimeSpan _cachetime;

		// Token: 0x04002DA1 RID: 11681
		internal string _cacheKeyPrefix;

		// Token: 0x04002DA2 RID: 11682
		private int _userAgentCacheKeyLength;

		// Token: 0x04002DA3 RID: 11683
		private static int _idCounter;

		// Token: 0x04002DA4 RID: 11684
		private const string _isMobileDeviceCapKey = "isMobileDevice";

		// Token: 0x04002DA5 RID: 11685
		private static object _disableOptimisticCachingSingleton = new object();

		// Token: 0x04002DA6 RID: 11686
		private const int _defaultUserAgentCacheKeyLength = 64;

		// Token: 0x04002DA7 RID: 11687
		private string _browserCapabilitiesProviderType;

		// Token: 0x04002DA8 RID: 11688
		private HttpCapabilitiesProvider _browserCapabilitiesProvider;
	}
}
