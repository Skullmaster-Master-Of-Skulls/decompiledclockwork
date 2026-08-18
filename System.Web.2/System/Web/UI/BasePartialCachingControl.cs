using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002EA RID: 746
	[ToolboxItem(false)]
	public abstract class BasePartialCachingControl : Control
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x00070D34 File Offset: 0x0006EF34
		internal override void InitRecursive(Control namingContainer)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			this._cacheKey = this.ComputeNonVaryCacheKey(hashCodeCombiner);
			this._nonVaryHashCode = hashCodeCombiner.CombinedHash;
			PartialCachingCacheEntry partialCachingCacheEntry = null;
			object fragment = OutputCache.GetFragment(this._cacheKey, this._provider);
			if (fragment != null)
			{
				ControlCachedVary controlCachedVary = fragment as ControlCachedVary;
				if (controlCachedVary != null)
				{
					string key = this.ComputeVaryCacheKey(hashCodeCombiner, controlCachedVary);
					partialCachingCacheEntry = (PartialCachingCacheEntry)OutputCache.GetFragment(key, this._provider);
					if (partialCachingCacheEntry != null && partialCachingCacheEntry._cachedVaryId != controlCachedVary.CachedVaryId)
					{
						partialCachingCacheEntry = null;
						OutputCache.RemoveFragment(key, this._provider);
					}
				}
				else
				{
					partialCachingCacheEntry = (PartialCachingCacheEntry)fragment;
				}
			}
			if (partialCachingCacheEntry == null)
			{
				this._cacheEntry = new PartialCachingCacheEntry();
				this._cachedCtrl = this.CreateCachedControl();
				this.Controls.Add(this._cachedCtrl);
				this.Page.PushCachingControl(this);
				base.InitRecursive(namingContainer);
				this.Page.PopCachingControl();
				return;
			}
			this._outputString = partialCachingCacheEntry.OutputString;
			this._cssStyleString = partialCachingCacheEntry.CssStyleString;
			if (partialCachingCacheEntry.RegisteredClientCalls != null)
			{
				foreach (object obj in partialCachingCacheEntry.RegisteredClientCalls)
				{
					RegisterCallData registerCallData = (RegisterCallData)obj;
					switch (registerCallData.Type)
					{
					case ClientAPIRegisterType.WebFormsScript:
						this.Page.RegisterWebFormsScript();
						break;
					case ClientAPIRegisterType.PostBackScript:
						this.Page.RegisterPostBackScript();
						break;
					case ClientAPIRegisterType.FocusScript:
						this.Page.RegisterFocusScript();
						break;
					case ClientAPIRegisterType.ClientScriptBlocks:
					case ClientAPIRegisterType.ClientScriptBlocksWithoutTags:
					case ClientAPIRegisterType.ClientStartupScripts:
					case ClientAPIRegisterType.ClientStartupScriptsWithoutTags:
						this.Page.ClientScript.RegisterScriptBlock(registerCallData.Key, registerCallData.StringParam2, registerCallData.Type);
						break;
					case ClientAPIRegisterType.OnSubmitStatement:
						this.Page.ClientScript.RegisterOnSubmitStatementInternal(registerCallData.Key, registerCallData.StringParam2);
						break;
					case ClientAPIRegisterType.ArrayDeclaration:
						this.Page.ClientScript.RegisterArrayDeclaration(registerCallData.StringParam1, registerCallData.StringParam2);
						break;
					case ClientAPIRegisterType.HiddenField:
						this.Page.ClientScript.RegisterHiddenField(registerCallData.StringParam1, registerCallData.StringParam2);
						break;
					case ClientAPIRegisterType.ExpandoAttribute:
						this.Page.ClientScript.RegisterExpandoAttribute(registerCallData.StringParam1, registerCallData.StringParam2, registerCallData.StringParam3, false);
						break;
					case ClientAPIRegisterType.EventValidation:
						if (this._registeredCallDataForEventValidation == null)
						{
							this._registeredCallDataForEventValidation = new ArrayList();
						}
						this._registeredCallDataForEventValidation.Add(registerCallData);
						break;
					}
				}
			}
			base.InitRecursive(namingContainer);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x00070FEC File Offset: 0x0006F1EC
		internal override void LoadRecursive()
		{
			if (this._outputString != null)
			{
				base.LoadRecursive();
				return;
			}
			this.Page.PushCachingControl(this);
			base.LoadRecursive();
			this.Page.PopCachingControl();
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007101C File Offset: 0x0006F21C
		internal override void PreRenderRecursiveInternal()
		{
			if (this._outputString != null)
			{
				base.PreRenderRecursiveInternal();
				if (this._cssStyleString != null && this.Page.Header != null)
				{
					this.Page.Header.RegisterCssStyleString(this._cssStyleString);
				}
				return;
			}
			this.Page.PushCachingControl(this);
			base.PreRenderRecursiveInternal();
			this.Page.PopCachingControl();
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00071080 File Offset: 0x0006F280
		public override void Dispose()
		{
			if (this._cacheDependency != null)
			{
				this._cacheDependency.Dispose();
				this._cacheDependency = null;
			}
			base.Dispose();
		}

		// Token: 0x060022AB RID: 8875
		internal abstract Control CreateCachedControl();

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x000710A2 File Offset: 0x0006F2A2
		// (set) Token: 0x060022AD RID: 8877 RVA: 0x000710AA File Offset: 0x0006F2AA
		public CacheDependency Dependency
		{
			get
			{
				return this._cacheDependency;
			}
			set
			{
				this._cacheDependency = value;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x000710B3 File Offset: 0x0006F2B3
		public ControlCachePolicy CachePolicy
		{
			get
			{
				if (this._cachePolicy == null)
				{
					this._cachePolicy = new ControlCachePolicy(this);
				}
				return this._cachePolicy;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x000710CF File Offset: 0x0006F2CF
		internal HttpCacheVaryByParams VaryByParams
		{
			get
			{
				if (this._varyByParamsCollection == null)
				{
					this._varyByParamsCollection = new HttpCacheVaryByParams();
					this._varyByParamsCollection.IgnoreParams = true;
				}
				return this._varyByParamsCollection;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000710F6 File Offset: 0x0006F2F6
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x00071116 File Offset: 0x0006F316
		internal string VaryByControl
		{
			get
			{
				if (this._varyByControlsCollection == null)
				{
					return string.Empty;
				}
				return string.Join(";", this._varyByControlsCollection);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this._varyByControlsCollection = null;
					return;
				}
				this._varyByControlsCollection = value.Split(new char[]
				{
					';'
				});
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0007113F File Offset: 0x0006F33F
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x0007116C File Offset: 0x0006F36C
		internal TimeSpan Duration
		{
			get
			{
				if (this._utcExpirationTime == DateTime.MaxValue)
				{
					return TimeSpan.MaxValue;
				}
				return this._utcExpirationTime - DateTime.UtcNow;
			}
			set
			{
				if (value == TimeSpan.MaxValue)
				{
					this._utcExpirationTime = DateTime.MaxValue;
					return;
				}
				this._utcExpirationTime = DateTime.UtcNow.Add(value);
			}
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000711A8 File Offset: 0x0006F3A8
		private void RegisterValidationEvents()
		{
			if (this._registeredCallDataForEventValidation != null)
			{
				foreach (object obj in this._registeredCallDataForEventValidation)
				{
					RegisterCallData registerCallData = (RegisterCallData)obj;
					this.Page.ClientScript.RegisterForEventValidation(registerCallData.StringParam1, registerCallData.StringParam2);
				}
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00071220 File Offset: 0x0006F420
		internal void RegisterStyleInfo(SelectorStyleInfo selectorInfo)
		{
			if (this._registeredStyleInfo == null)
			{
				this._registeredStyleInfo = new ArrayList();
			}
			this._registeredStyleInfo.Add(selectorInfo);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00071244 File Offset: 0x0006F444
		protected internal override void Render(HtmlTextWriter output)
		{
			CacheDependency cacheDependency = null;
			if (this._outputString != null)
			{
				output.Write(this._outputString);
				this.RegisterValidationEvents();
				return;
			}
			if (this._cachingDisabled || !RuntimeConfig.GetAppConfig().OutputCache.EnableFragmentCache)
			{
				this._cachedCtrl.RenderControl(output);
				return;
			}
			if (this._sqlDependency != null)
			{
				cacheDependency = SqlCacheDependency.CreateOutputCacheDependency(this._sqlDependency);
			}
			this._cacheEntry.CssStyleString = this.GetCssStyleRenderString(output.GetType());
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = Page.CreateHtmlTextWriterFromType(stringWriter, output.GetType());
			TextWriter writer2 = this.Context.Response.SwitchWriter(stringWriter);
			try
			{
				this.Page.PushCachingControl(this);
				this._cachedCtrl.RenderControl(writer);
				this.Page.PopCachingControl();
			}
			finally
			{
				this.Context.Response.SwitchWriter(writer2);
			}
			this._cacheEntry.OutputString = stringWriter.ToString();
			output.Write(this._cacheEntry.OutputString);
			CacheDependency cacheDependency2 = this._cacheDependency;
			if (cacheDependency != null)
			{
				if (cacheDependency2 == null)
				{
					cacheDependency2 = cacheDependency;
				}
				else
				{
					AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
					aggregateCacheDependency.Add(new CacheDependency[]
					{
						cacheDependency2
					});
					aggregateCacheDependency.Add(new CacheDependency[]
					{
						cacheDependency
					});
					cacheDependency2 = aggregateCacheDependency;
				}
			}
			ControlCachedVary cachedVary = null;
			string fragmentKey;
			if (this._varyByParamsCollection == null && this._varyByControlsCollection == null && this._varyByCustom == null)
			{
				fragmentKey = this._cacheKey;
			}
			else
			{
				string[] varyByParams = null;
				if (this._varyByParamsCollection != null)
				{
					varyByParams = this._varyByParamsCollection.GetParams();
				}
				cachedVary = new ControlCachedVary(varyByParams, this._varyByControlsCollection, this._varyByCustom);
				HashCodeCombiner combinedHashCode = new HashCodeCombiner(this._nonVaryHashCode);
				fragmentKey = this.ComputeVaryCacheKey(combinedHashCode, cachedVary);
			}
			DateTime absExp;
			TimeSpan slidingExp;
			if (this._useSlidingExpiration)
			{
				absExp = Cache.NoAbsoluteExpiration;
				slidingExp = this._utcExpirationTime - DateTime.UtcNow;
			}
			else
			{
				absExp = this._utcExpirationTime;
				slidingExp = Cache.NoSlidingExpiration;
			}
			try
			{
				OutputCache.InsertFragment(this._cacheKey, cachedVary, fragmentKey, this._cacheEntry, cacheDependency2, absExp, slidingExp, this._provider);
			}
			catch
			{
				if (cacheDependency2 != null)
				{
					cacheDependency2.Dispose();
				}
				throw;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00071468 File Offset: 0x0006F668
		private string ComputeNonVaryCacheKey(HashCodeCombiner combinedHashCode)
		{
			combinedHashCode.AddObject(this._guid);
			HttpBrowserCapabilities browser = this.Context.Request.Browser;
			if (browser != null)
			{
				combinedHashCode.AddObject(browser.TagWriter);
			}
			return "l" + combinedHashCode.CombinedHashString;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000714B4 File Offset: 0x0006F6B4
		private string ComputeVaryCacheKey(HashCodeCombiner combinedHashCode, ControlCachedVary cachedVary)
		{
			combinedHashCode.AddInt(1);
			HttpRequest request = this.Page.Request;
			NameValueCollection nameValueCollection;
			if (request != null && request.HttpVerb == HttpVerb.POST)
			{
				nameValueCollection = new NameValueCollection(request.QueryString);
				nameValueCollection.Add(request.Form);
			}
			else
			{
				nameValueCollection = this.Page.RequestValueCollection;
				if (nameValueCollection == null)
				{
					nameValueCollection = this.Page.GetCollectionBasedOnMethod(true);
				}
			}
			if (cachedVary._varyByParams != null)
			{
				ICollection collection;
				if (cachedVary._varyByParams.Length == 1 && cachedVary._varyByParams[0] == "*")
				{
					collection = nameValueCollection;
				}
				else
				{
					collection = cachedVary._varyByParams;
				}
				foreach (object obj in collection)
				{
					string text = (string)obj;
					combinedHashCode.AddCaseInsensitiveString(text);
					string text2 = nameValueCollection[text];
					if (text2 != null)
					{
						combinedHashCode.AddObject(text2);
					}
				}
			}
			if (cachedVary._varyByControls != null)
			{
				string str;
				if (this.NamingContainer == this.Page)
				{
					str = string.Empty;
				}
				else
				{
					str = this.NamingContainer.UniqueID;
					str += base.IdSeparator.ToString();
				}
				str = str + this._ctrlID + base.IdSeparator.ToString();
				foreach (string text3 in cachedVary._varyByControls)
				{
					string text4 = str + text3.Trim();
					combinedHashCode.AddCaseInsensitiveString(text4);
					string text5 = nameValueCollection[text4];
					if (text5 != null)
					{
						combinedHashCode.AddObject(nameValueCollection[text4]);
					}
				}
			}
			if (cachedVary._varyByCustom != null)
			{
				string varyByCustomString = this.Context.ApplicationInstance.GetVaryByCustomString(this.Context, cachedVary._varyByCustom);
				if (varyByCustomString != null)
				{
					combinedHashCode.AddObject(varyByCustomString);
				}
			}
			return "l" + combinedHashCode.CombinedHashString;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000716A8 File Offset: 0x0006F8A8
		private string GetCssStyleRenderString(Type htmlTextWriterType)
		{
			if (this._registeredStyleInfo == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			HtmlTextWriter writer = Page.CreateHtmlTextWriterFromType(stringWriter, htmlTextWriterType);
			CssTextWriter cssWriter = new CssTextWriter(writer);
			foreach (object obj in this._registeredStyleInfo)
			{
				SelectorStyleInfo selectorStyleInfo = (SelectorStyleInfo)obj;
				HtmlHead.RenderCssRule(cssWriter, selectorStyleInfo.selector, selectorStyleInfo.style, selectorStyleInfo.urlResolver);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00071744 File Offset: 0x0006F944
		internal void SetVaryByParamsCollectionFromString(string varyByParams)
		{
			if (varyByParams == null)
			{
				return;
			}
			string[] @params = varyByParams.Split(new char[]
			{
				';'
			});
			this._varyByParamsCollection = new HttpCacheVaryByParams();
			this._varyByParamsCollection.SetParams(@params);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007177E File Offset: 0x0006F97E
		internal void RegisterPostBackScript()
		{
			this.RegisterClientCall(ClientAPIRegisterType.PostBackScript, string.Empty, null);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007178D File Offset: 0x0006F98D
		internal void RegisterFocusScript()
		{
			this.RegisterClientCall(ClientAPIRegisterType.FocusScript, string.Empty, null);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007179C File Offset: 0x0006F99C
		internal void RegisterWebFormsScript()
		{
			this.RegisterClientCall(ClientAPIRegisterType.WebFormsScript, string.Empty, null);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000717AC File Offset: 0x0006F9AC
		private void RegisterClientCall(ClientAPIRegisterType type, ScriptKey scriptKey, string stringParam2)
		{
			RegisterCallData registerCallData = new RegisterCallData();
			registerCallData.Type = type;
			registerCallData.Key = scriptKey;
			registerCallData.StringParam2 = stringParam2;
			if (this._cacheEntry.RegisteredClientCalls == null)
			{
				this._cacheEntry.RegisteredClientCalls = new ArrayList();
			}
			this._cacheEntry.RegisteredClientCalls.Add(registerCallData);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00071803 File Offset: 0x0006FA03
		private void RegisterClientCall(ClientAPIRegisterType type, string stringParam1, string stringParam2)
		{
			this.RegisterClientCall(type, stringParam1, stringParam2, null);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x00071810 File Offset: 0x0006FA10
		private void RegisterClientCall(ClientAPIRegisterType type, string stringParam1, string stringParam2, string stringParam3)
		{
			RegisterCallData registerCallData = new RegisterCallData();
			registerCallData.Type = type;
			registerCallData.StringParam1 = stringParam1;
			registerCallData.StringParam2 = stringParam2;
			registerCallData.StringParam3 = stringParam3;
			if (this._cacheEntry.RegisteredClientCalls == null)
			{
				this._cacheEntry.RegisteredClientCalls = new ArrayList();
			}
			this._cacheEntry.RegisteredClientCalls.Add(registerCallData);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007186F File Offset: 0x0006FA6F
		internal void RegisterScriptBlock(ClientAPIRegisterType type, ScriptKey key, string script)
		{
			this.RegisterClientCall(type, key, script);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007187A File Offset: 0x0006FA7A
		internal void RegisterOnSubmitStatement(ScriptKey key, string script)
		{
			this.RegisterClientCall(ClientAPIRegisterType.OnSubmitStatement, key, script);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x00071885 File Offset: 0x0006FA85
		internal void RegisterArrayDeclaration(string arrayName, string arrayValue)
		{
			this.RegisterClientCall(ClientAPIRegisterType.ArrayDeclaration, arrayName, arrayValue);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00071890 File Offset: 0x0006FA90
		internal void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
		{
			this.RegisterClientCall(ClientAPIRegisterType.HiddenField, hiddenFieldName, hiddenFieldInitialValue);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007189C File Offset: 0x0006FA9C
		internal void RegisterExpandoAttribute(string controlID, string attributeName, string attributeValue)
		{
			this.RegisterClientCall(ClientAPIRegisterType.ExpandoAttribute, controlID, attributeName, attributeValue);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000718A9 File Offset: 0x0006FAA9
		internal void RegisterForEventValidation(string uniqueID, string argument)
		{
			this.RegisterClientCall(ClientAPIRegisterType.EventValidation, uniqueID, argument);
		}

		// Token: 0x04001C5A RID: 7258
		internal Control _cachedCtrl;

		// Token: 0x04001C5B RID: 7259
		private long _nonVaryHashCode;

		// Token: 0x04001C5C RID: 7260
		internal string _ctrlID;

		// Token: 0x04001C5D RID: 7261
		internal string _guid;

		// Token: 0x04001C5E RID: 7262
		internal DateTime _utcExpirationTime;

		// Token: 0x04001C5F RID: 7263
		internal bool _useSlidingExpiration;

		// Token: 0x04001C60 RID: 7264
		internal HttpCacheVaryByParams _varyByParamsCollection;

		// Token: 0x04001C61 RID: 7265
		internal string[] _varyByControlsCollection;

		// Token: 0x04001C62 RID: 7266
		internal string _varyByCustom;

		// Token: 0x04001C63 RID: 7267
		internal string _sqlDependency;

		// Token: 0x04001C64 RID: 7268
		internal string _provider;

		// Token: 0x04001C65 RID: 7269
		internal bool _cachingDisabled;

		// Token: 0x04001C66 RID: 7270
		private string _outputString;

		// Token: 0x04001C67 RID: 7271
		private string _cssStyleString;

		// Token: 0x04001C68 RID: 7272
		private string _cacheKey;

		// Token: 0x04001C69 RID: 7273
		private CacheDependency _cacheDependency;

		// Token: 0x04001C6A RID: 7274
		private PartialCachingCacheEntry _cacheEntry;

		// Token: 0x04001C6B RID: 7275
		private ControlCachePolicy _cachePolicy;

		// Token: 0x04001C6C RID: 7276
		private ArrayList _registeredCallDataForEventValidation;

		// Token: 0x04001C6D RID: 7277
		private ArrayList _registeredStyleInfo;

		// Token: 0x04001C6E RID: 7278
		internal const char varySeparator = ';';

		// Token: 0x04001C6F RID: 7279
		internal const string varySeparatorString = ";";
	}
}
