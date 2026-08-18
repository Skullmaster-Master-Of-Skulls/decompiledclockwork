using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006F2 RID: 1778
	public class HttpCapabilitiesBase : IFilterResolutionService
	{
		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06005555 RID: 21845 RVA: 0x0012AA4C File Offset: 0x00128C4C
		internal static HttpCapabilitiesBase EmptyHttpCapabilitiesBase
		{
			get
			{
				if (HttpCapabilitiesBase._emptyHttpCapabilitiesBase != null)
				{
					return HttpCapabilitiesBase._emptyHttpCapabilitiesBase;
				}
				object emptyHttpCapabilitiesBaseLock = HttpCapabilitiesBase._emptyHttpCapabilitiesBaseLock;
				lock (emptyHttpCapabilitiesBaseLock)
				{
					if (HttpCapabilitiesBase._emptyHttpCapabilitiesBase != null)
					{
						return HttpCapabilitiesBase._emptyHttpCapabilitiesBase;
					}
					HttpCapabilitiesBase._emptyHttpCapabilitiesBase = new HttpCapabilitiesBase();
				}
				return HttpCapabilitiesBase._emptyHttpCapabilitiesBase;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06005556 RID: 21846 RVA: 0x0012AAB4 File Offset: 0x00128CB4
		// (set) Token: 0x06005557 RID: 21847 RVA: 0x0012AABB File Offset: 0x00128CBB
		public static HttpCapabilitiesProvider BrowserCapabilitiesProvider
		{
			get
			{
				return HttpCapabilitiesBase._browserCapabilitiesProvider;
			}
			set
			{
				HttpCapabilitiesBase._browserCapabilitiesProvider = value;
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06005558 RID: 21848 RVA: 0x0012AAC3 File Offset: 0x00128CC3
		public bool UseOptimizedCacheKey
		{
			get
			{
				return this._useOptimizedCacheKey;
			}
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x0012AACB File Offset: 0x00128CCB
		public void DisableOptimizedCacheKey()
		{
			this._useOptimizedCacheKey = false;
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0012AAD4 File Offset: 0x00128CD4
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		public static HttpCapabilitiesBase GetConfigCapabilities(string configKey, HttpRequest request)
		{
			HttpCapabilitiesBase httpCapabilitiesBase = null;
			if (configKey == "system.web/browserCaps")
			{
				httpCapabilitiesBase = HttpCapabilitiesBase.GetBrowserCapabilities(request);
			}
			else
			{
				HttpCapabilitiesDefaultProvider httpCapabilitiesDefaultProvider = (HttpCapabilitiesDefaultProvider)request.Context.GetSection(configKey);
				if (httpCapabilitiesDefaultProvider != null)
				{
					if (HttpCapabilitiesBase.BrowserCapabilitiesProvider != null)
					{
						httpCapabilitiesDefaultProvider.BrowserCapabilitiesProvider = HttpCapabilitiesBase.BrowserCapabilitiesProvider;
					}
					if (httpCapabilitiesDefaultProvider.BrowserCapabilitiesProvider == null)
					{
						httpCapabilitiesBase = httpCapabilitiesDefaultProvider.Evaluate(request);
					}
					else
					{
						httpCapabilitiesBase = httpCapabilitiesDefaultProvider.BrowserCapabilitiesProvider.GetBrowserCapabilities(request);
					}
				}
			}
			if (httpCapabilitiesBase == null)
			{
				httpCapabilitiesBase = HttpCapabilitiesBase.EmptyHttpCapabilitiesBase;
			}
			return httpCapabilitiesBase;
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0012AB4C File Offset: 0x00128D4C
		internal static HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request)
		{
			HttpCapabilitiesBase httpCapabilitiesBase = null;
			HttpCapabilitiesDefaultProvider httpCapabilitiesDefaultProvider = request.Context.IsRuntimeErrorReported ? RuntimeConfig.GetLKGConfig(request.Context).BrowserCaps : RuntimeConfig.GetConfig(request.Context).BrowserCaps;
			if (httpCapabilitiesDefaultProvider != null)
			{
				if (HttpCapabilitiesBase.BrowserCapabilitiesProvider != null)
				{
					httpCapabilitiesDefaultProvider.BrowserCapabilitiesProvider = HttpCapabilitiesBase.BrowserCapabilitiesProvider;
				}
				if (httpCapabilitiesDefaultProvider.BrowserCapabilitiesProvider == null)
				{
					httpCapabilitiesBase = httpCapabilitiesDefaultProvider.Evaluate(request);
				}
				else
				{
					httpCapabilitiesBase = httpCapabilitiesDefaultProvider.BrowserCapabilitiesProvider.GetBrowserCapabilities(request);
				}
			}
			return (HttpBrowserCapabilities)httpCapabilitiesBase;
		}

		// Token: 0x17001859 RID: 6233
		public virtual string this[string key]
		{
			get
			{
				return (string)this._items[key];
			}
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0012ABD8 File Offset: 0x00128DD8
		public HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
		{
			string htmlTextWriter = this.HtmlTextWriter;
			if (htmlTextWriter != null && htmlTextWriter.Length != 0)
			{
				try
				{
					Type type = BuildManager.GetType(htmlTextWriter, true, false);
					HtmlTextWriter htmlTextWriter2 = (HtmlTextWriter)Activator.CreateInstance(type, new object[]
					{
						w
					});
					if (htmlTextWriter2 != null)
					{
						return htmlTextWriter2;
					}
				}
				catch
				{
					throw new Exception(SR.GetString("Could_not_create_type_instance", new object[]
					{
						htmlTextWriter
					}));
				}
			}
			return this.CreateHtmlTextWriterInternal(w);
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x0012AC5C File Offset: 0x00128E5C
		internal HtmlTextWriter CreateHtmlTextWriterInternal(TextWriter tw)
		{
			Type tagWriter = this.TagWriter;
			if (tagWriter != null)
			{
				return Page.CreateHtmlTextWriterFromType(tw, tagWriter);
			}
			return new Html32TextWriter(tw);
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Init()
		{
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x0012AC88 File Offset: 0x00128E88
		internal void InitInternal(HttpBrowserCapabilities browserCaps)
		{
			if (this._items != null)
			{
				throw new ArgumentException(SR.GetString("Caps_cannot_be_inited_twice"));
			}
			this._items = browserCaps._items;
			this._adapters = browserCaps._adapters;
			this._browsers = browserCaps._browsers;
			this._htmlTextWriter = browserCaps._htmlTextWriter;
			this._useOptimizedCacheKey = browserCaps._useOptimizedCacheKey;
			this.Init();
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x0012ACF0 File Offset: 0x00128EF0
		internal ControlAdapter GetAdapter(Control control)
		{
			if (this._adapters == null || this._adapters.Count == 0)
			{
				return null;
			}
			if (control == null)
			{
				return null;
			}
			Type type = control.GetType();
			object obj = this.AdapterTypes[type];
			if (obj == HttpCapabilitiesBase.s_nullAdapterSingleton)
			{
				return null;
			}
			Type type2 = (Type)obj;
			if (type2 == null)
			{
				Type type3 = type;
				string text = null;
				while (text == null && type3 != typeof(Control))
				{
					string key = type3.AssemblyQualifiedName;
					text = (string)this.Adapters[key];
					if (text == null)
					{
						key = type3.FullName;
						text = (string)this.Adapters[key];
					}
					if (text != null)
					{
						break;
					}
					type3 = type3.BaseType;
				}
				if (string.IsNullOrEmpty(text))
				{
					this.AdapterTypes[type] = HttpCapabilitiesBase.s_nullAdapterSingleton;
					return null;
				}
				type2 = BuildManager.GetType(text, false, false);
				if (type2 == null)
				{
					throw new Exception(SR.GetString("ControlAdapters_TypeNotFound", new object[]
					{
						text
					}));
				}
				this.AdapterTypes[type] = type2;
			}
			IWebObjectFactory adapterFactory = this.GetAdapterFactory(type2);
			ControlAdapter controlAdapter = (ControlAdapter)adapterFactory.CreateInstance();
			controlAdapter._control = control;
			return controlAdapter;
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x0012AE30 File Offset: 0x00129030
		private IWebObjectFactory GetAdapterFactory(Type adapterType)
		{
			if (HttpCapabilitiesBase._controlAdapterFactoryGenerator == null)
			{
				object staticLock = HttpCapabilitiesBase._staticLock;
				lock (staticLock)
				{
					if (HttpCapabilitiesBase._controlAdapterFactoryGenerator == null)
					{
						HttpCapabilitiesBase._controlAdapterFactoryTable = new Hashtable();
						HttpCapabilitiesBase._controlAdapterFactoryGenerator = new FactoryGenerator();
					}
				}
			}
			IWebObjectFactory webObjectFactory = (IWebObjectFactory)HttpCapabilitiesBase._controlAdapterFactoryTable[adapterType];
			if (webObjectFactory == null)
			{
				object syncRoot = HttpCapabilitiesBase._controlAdapterFactoryTable.SyncRoot;
				lock (syncRoot)
				{
					webObjectFactory = (IWebObjectFactory)HttpCapabilitiesBase._controlAdapterFactoryTable[adapterType];
					if (webObjectFactory == null)
					{
						try
						{
							webObjectFactory = HttpCapabilitiesBase._controlAdapterFactoryGenerator.CreateFactory(adapterType);
						}
						catch
						{
							throw new Exception(SR.GetString("Could_not_create_type_instance", new object[]
							{
								adapterType.ToString()
							}));
						}
						HttpCapabilitiesBase._controlAdapterFactoryTable[adapterType] = webObjectFactory;
					}
				}
			}
			return webObjectFactory;
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06005563 RID: 21859 RVA: 0x0012AF2C File Offset: 0x0012912C
		// (set) Token: 0x06005564 RID: 21860 RVA: 0x0012AF34 File Offset: 0x00129134
		public IDictionary Capabilities
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06005565 RID: 21861 RVA: 0x0012AF40 File Offset: 0x00129140
		public IDictionary Adapters
		{
			get
			{
				if (this._adapters == null)
				{
					object staticLock = HttpCapabilitiesBase._staticLock;
					lock (staticLock)
					{
						if (this._adapters == null)
						{
							this._adapters = new Hashtable(StringComparer.OrdinalIgnoreCase);
						}
					}
				}
				return this._adapters;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x06005566 RID: 21862 RVA: 0x0012AFA0 File Offset: 0x001291A0
		// (set) Token: 0x06005567 RID: 21863 RVA: 0x0012AFA8 File Offset: 0x001291A8
		public string HtmlTextWriter
		{
			get
			{
				return this._htmlTextWriter;
			}
			set
			{
				this._htmlTextWriter = value;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x0012AFB4 File Offset: 0x001291B4
		private Hashtable AdapterTypes
		{
			get
			{
				if (this._adapterTypes == null)
				{
					object staticLock = HttpCapabilitiesBase._staticLock;
					lock (staticLock)
					{
						if (this._adapterTypes == null)
						{
							this._adapterTypes = Hashtable.Synchronized(new Hashtable());
						}
					}
				}
				return this._adapterTypes;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x0012B014 File Offset: 0x00129214
		public string Id
		{
			get
			{
				if (this._browsers != null)
				{
					return (string)this._browsers[this._browsers.Count - 1];
				}
				return string.Empty;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x0012B041 File Offset: 0x00129241
		public ArrayList Browsers
		{
			get
			{
				return this._browsers;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x0600556B RID: 21867 RVA: 0x0012B04C File Offset: 0x0012924C
		public Version ClrVersion
		{
			get
			{
				Version[] clrVersions = this.GetClrVersions();
				if (clrVersions != null)
				{
					return clrVersions[clrVersions.Length - 1];
				}
				return null;
			}
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x0012B06C File Offset: 0x0012926C
		public Version[] GetClrVersions()
		{
			string userAgent = HttpCapabilitiesDefaultProvider.GetUserAgent(HttpContext.Current.Request);
			if (string.IsNullOrEmpty(userAgent))
			{
				return null;
			}
			Regex regex = RegexUtil.CreateRegex("\\.NET CLR (?'clrVersion'[0-9\\.]*)", RegexOptions.None);
			MatchCollection matchCollection = regex.Matches(userAgent);
			if (matchCollection.Count == 0)
			{
				return new Version[]
				{
					new Version()
				};
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				try
				{
					Version value = new Version(match.Groups["clrVersion"].Value);
					arrayList.Add(value);
				}
				catch (FormatException)
				{
				}
			}
			arrayList.Sort();
			return (Version[])arrayList.ToArray(typeof(Version));
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x0012B160 File Offset: 0x00129360
		public string Type
		{
			get
			{
				if (!this._havetype)
				{
					this._type = this["type"];
					this._havetype = true;
				}
				return this._type;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x0012B190 File Offset: 0x00129390
		public string Browser
		{
			get
			{
				if (!this._havebrowser)
				{
					this._browser = this["browser"];
					this._havebrowser = true;
				}
				return this._browser;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x0012B1C0 File Offset: 0x001293C0
		public string Version
		{
			get
			{
				if (!this._haveversion)
				{
					this._version = this["version"];
					this._haveversion = true;
				}
				return this._version;
			}
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0012B1F0 File Offset: 0x001293F0
		public int MajorVersion
		{
			get
			{
				if (!this._havemajorversion)
				{
					try
					{
						this._majorversion = int.Parse(this["majorversion"], CultureInfo.InvariantCulture);
						this._havemajorversion = true;
					}
					catch (FormatException e)
					{
						throw this.BuildParseError(e, "majorversion");
					}
				}
				return this._majorversion;
			}
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x0012B258 File Offset: 0x00129458
		private Exception BuildParseError(Exception e, string capsKey)
		{
			string @string = SR.GetString("Invalid_string_from_browser_caps", new object[]
			{
				e.Message,
				capsKey,
				this[capsKey]
			});
			ConfigurationErrorsException e2 = new ConfigurationErrorsException(@string, e);
			HttpUnhandledException ex = new HttpUnhandledException(null, null);
			ex.SetFormatter(new UseLastUnhandledErrorFormatter(e2));
			return ex;
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x0012B2AC File Offset: 0x001294AC
		private bool CapsParseBoolDefault(string capsKey, bool defaultValue)
		{
			string text = this[capsKey];
			if (text == null)
			{
				return defaultValue;
			}
			bool result;
			try
			{
				result = bool.Parse(text);
			}
			catch (FormatException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x0012B2E8 File Offset: 0x001294E8
		private bool CapsParseBool(string capsKey)
		{
			bool result;
			try
			{
				result = bool.Parse(this[capsKey]);
			}
			catch (FormatException e)
			{
				throw this.BuildParseError(e, capsKey);
			}
			return result;
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x0012B320 File Offset: 0x00129520
		public string MinorVersionString
		{
			get
			{
				return this["minorversion"];
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06005575 RID: 21877 RVA: 0x0012B330 File Offset: 0x00129530
		public double MinorVersion
		{
			get
			{
				if (!this._haveminorversion)
				{
					object staticLock = HttpCapabilitiesBase._staticLock;
					lock (staticLock)
					{
						if (!this._haveminorversion)
						{
							try
							{
								this._minorversion = double.Parse(this["minorversion"], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
								this._haveminorversion = true;
							}
							catch (FormatException e)
							{
								string text = this["minorversion"];
								int num = text.IndexOf('.');
								if (num != -1)
								{
									int num2 = text.IndexOf('.', num + 1);
									if (num2 != -1)
									{
										try
										{
											this._minorversion = double.Parse(text.Substring(0, num2), NumberStyles.Float, NumberFormatInfo.InvariantInfo);
											Thread.MemoryBarrier();
											this._haveminorversion = true;
										}
										catch (FormatException)
										{
										}
									}
								}
								if (!this._haveminorversion)
								{
									throw this.BuildParseError(e, "minorversion");
								}
							}
						}
					}
				}
				return this._minorversion;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0012B448 File Offset: 0x00129648
		public string Platform
		{
			get
			{
				if (!this._haveplatform)
				{
					this._platform = this["platform"];
					this._haveplatform = true;
				}
				return this._platform;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x0012B478 File Offset: 0x00129678
		public Type TagWriter
		{
			get
			{
				try
				{
					if (!this._havetagwriter)
					{
						string text = this["tagwriter"];
						if (string.IsNullOrEmpty(text))
						{
							this._tagwriter = null;
						}
						else if (string.Compare(text, typeof(HtmlTextWriter).FullName, StringComparison.Ordinal) == 0)
						{
							this._tagwriter = typeof(HtmlTextWriter);
						}
						else
						{
							this._tagwriter = BuildManager.GetType(text, true);
						}
						this._havetagwriter = true;
					}
				}
				catch (Exception e)
				{
					throw this.BuildParseError(e, "tagwriter");
				}
				return this._tagwriter;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x0012B51C File Offset: 0x0012971C
		public Version EcmaScriptVersion
		{
			get
			{
				if (!this._haveecmascriptversion)
				{
					this._ecmascriptversion = new Version(this["ecmascriptversion"]);
					this._haveecmascriptversion = true;
				}
				return this._ecmascriptversion;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06005579 RID: 21881 RVA: 0x0012B551 File Offset: 0x00129751
		public Version MSDomVersion
		{
			get
			{
				if (!this._havemsdomversion)
				{
					this._msdomversion = new Version(this["msdomversion"]);
					this._havemsdomversion = true;
				}
				return this._msdomversion;
			}
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x0012B586 File Offset: 0x00129786
		public Version W3CDomVersion
		{
			get
			{
				if (!this._havew3cdomversion)
				{
					this._w3cdomversion = new Version(this["w3cdomversion"]);
					this._havew3cdomversion = true;
				}
				return this._w3cdomversion;
			}
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x0600557B RID: 21883 RVA: 0x0012B5BB File Offset: 0x001297BB
		public bool Beta
		{
			get
			{
				if (!this._havebeta)
				{
					this._beta = this.CapsParseBool("beta");
					this._havebeta = true;
				}
				return this._beta;
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x0012B5EB File Offset: 0x001297EB
		public bool Crawler
		{
			get
			{
				if (!this._havecrawler)
				{
					this._crawler = this.CapsParseBool("crawler");
					this._havecrawler = true;
				}
				return this._crawler;
			}
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x0600557D RID: 21885 RVA: 0x0012B61B File Offset: 0x0012981B
		public bool AOL
		{
			get
			{
				if (!this._haveaol)
				{
					this._aol = this.CapsParseBool("aol");
					this._haveaol = true;
				}
				return this._aol;
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x0012B64B File Offset: 0x0012984B
		public bool Win16
		{
			get
			{
				if (!this._havewin16)
				{
					this._win16 = this.CapsParseBool("win16");
					this._havewin16 = true;
				}
				return this._win16;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x0600557F RID: 21887 RVA: 0x0012B67B File Offset: 0x0012987B
		public bool Win32
		{
			get
			{
				if (!this._havewin32)
				{
					this._win32 = this.CapsParseBool("win32");
					this._havewin32 = true;
				}
				return this._win32;
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x0012B6AB File Offset: 0x001298AB
		public bool Frames
		{
			get
			{
				if (!this._haveframes)
				{
					this._frames = this.CapsParseBool("frames");
					this._haveframes = true;
				}
				return this._frames;
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06005581 RID: 21889 RVA: 0x0012B6DB File Offset: 0x001298DB
		public bool RequiresControlStateInSession
		{
			get
			{
				if (!this._haverequiresControlStateInSession)
				{
					if (this["requiresControlStateInSession"] != null)
					{
						this._requiresControlStateInSession = this.CapsParseBoolDefault("requiresControlStateInSession", false);
					}
					this._haverequiresControlStateInSession = true;
				}
				return this._requiresControlStateInSession;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x0012B719 File Offset: 0x00129919
		public bool Tables
		{
			get
			{
				if (!this._havetables)
				{
					this._tables = this.CapsParseBool("tables");
					this._havetables = true;
				}
				return this._tables;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06005583 RID: 21891 RVA: 0x0012B749 File Offset: 0x00129949
		public bool Cookies
		{
			get
			{
				if (!this._havecookies)
				{
					this._cookies = this.CapsParseBool("cookies");
					this._havecookies = true;
				}
				return this._cookies;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x06005584 RID: 21892 RVA: 0x0012B779 File Offset: 0x00129979
		public bool VBScript
		{
			get
			{
				if (!this._havevbscript)
				{
					this._vbscript = this.CapsParseBool("vbscript");
					this._havevbscript = true;
				}
				return this._vbscript;
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06005585 RID: 21893 RVA: 0x0012B7A9 File Offset: 0x001299A9
		[Obsolete("The recommended alternative is the EcmaScriptVersion property. A Major version value greater than or equal to 1 implies JavaScript support. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool JavaScript
		{
			get
			{
				if (!this._havejavascript)
				{
					this._javascript = this.CapsParseBool("javascript");
					this._havejavascript = true;
				}
				return this._javascript;
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06005586 RID: 21894 RVA: 0x0012B7D9 File Offset: 0x001299D9
		public bool JavaApplets
		{
			get
			{
				if (!this._havejavaapplets)
				{
					this._javaapplets = this.CapsParseBool("javaapplets");
					this._havejavaapplets = true;
				}
				return this._javaapplets;
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06005587 RID: 21895 RVA: 0x0012B809 File Offset: 0x00129A09
		public Version JScriptVersion
		{
			get
			{
				if (!this._havejscriptversion)
				{
					this._jscriptversion = new Version(this["jscriptversion"]);
					this._havejscriptversion = true;
				}
				return this._jscriptversion;
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x06005588 RID: 21896 RVA: 0x0012B83E File Offset: 0x00129A3E
		public bool ActiveXControls
		{
			get
			{
				if (!this._haveactivexcontrols)
				{
					this._activexcontrols = this.CapsParseBool("activexcontrols");
					this._haveactivexcontrols = true;
				}
				return this._activexcontrols;
			}
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06005589 RID: 21897 RVA: 0x0012B86E File Offset: 0x00129A6E
		public bool BackgroundSounds
		{
			get
			{
				if (!this._havebackgroundsounds)
				{
					this._backgroundsounds = this.CapsParseBool("backgroundsounds");
					this._havebackgroundsounds = true;
				}
				return this._backgroundsounds;
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x0600558A RID: 21898 RVA: 0x0012B89E File Offset: 0x00129A9E
		public bool CDF
		{
			get
			{
				if (!this._havecdf)
				{
					this._cdf = this.CapsParseBool("cdf");
					this._havecdf = true;
				}
				return this._cdf;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x0600558B RID: 21899 RVA: 0x0012B8CE File Offset: 0x00129ACE
		public virtual string MobileDeviceManufacturer
		{
			get
			{
				if (!this._haveMobileDeviceManufacturer)
				{
					this._mobileDeviceManufacturer = this["mobileDeviceManufacturer"];
					this._haveMobileDeviceManufacturer = true;
				}
				return this._mobileDeviceManufacturer;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x0600558C RID: 21900 RVA: 0x0012B8FE File Offset: 0x00129AFE
		public virtual string MobileDeviceModel
		{
			get
			{
				if (!this._haveMobileDeviceModel)
				{
					this._mobileDeviceModel = this["mobileDeviceModel"];
					this._haveMobileDeviceModel = true;
				}
				return this._mobileDeviceModel;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x0600558D RID: 21901 RVA: 0x0012B92E File Offset: 0x00129B2E
		public virtual string GatewayVersion
		{
			get
			{
				if (!this._haveGatewayVersion)
				{
					this._gatewayVersion = this["gatewayVersion"];
					this._haveGatewayVersion = true;
				}
				return this._gatewayVersion;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x0600558E RID: 21902 RVA: 0x0012B95E File Offset: 0x00129B5E
		public virtual int GatewayMajorVersion
		{
			get
			{
				if (!this._haveGatewayMajorVersion)
				{
					this._gatewayMajorVersion = Convert.ToInt32(this["gatewayMajorVersion"], CultureInfo.InvariantCulture);
					this._haveGatewayMajorVersion = true;
				}
				return this._gatewayMajorVersion;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x0600558F RID: 21903 RVA: 0x0012B998 File Offset: 0x00129B98
		public virtual double GatewayMinorVersion
		{
			get
			{
				if (!this._haveGatewayMinorVersion)
				{
					this._gatewayMinorVersion = double.Parse(this["gatewayMinorVersion"], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
					this._haveGatewayMinorVersion = true;
				}
				return this._gatewayMinorVersion;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06005590 RID: 21904 RVA: 0x0012B9D3 File Offset: 0x00129BD3
		public virtual string PreferredRenderingType
		{
			get
			{
				if (!this._havePreferredRenderingType)
				{
					this._preferredRenderingType = this["preferredRenderingType"];
					this._havePreferredRenderingType = true;
				}
				return this._preferredRenderingType;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06005591 RID: 21905 RVA: 0x0012BA03 File Offset: 0x00129C03
		public virtual string PreferredRequestEncoding
		{
			get
			{
				if (!this._havePreferredRequestEncoding)
				{
					this._preferredRequestEncoding = this["preferredRequestEncoding"];
					Thread.MemoryBarrier();
					this._havePreferredRequestEncoding = true;
				}
				return this._preferredRequestEncoding;
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x0012BA38 File Offset: 0x00129C38
		public virtual string PreferredResponseEncoding
		{
			get
			{
				if (!this._havePreferredResponseEncoding)
				{
					this._preferredResponseEncoding = this["preferredResponseEncoding"];
					this._havePreferredResponseEncoding = true;
				}
				return this._preferredResponseEncoding;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06005593 RID: 21907 RVA: 0x0012BA68 File Offset: 0x00129C68
		public virtual string PreferredRenderingMime
		{
			get
			{
				if (!this._havePreferredRenderingMime)
				{
					this._preferredRenderingMime = this["preferredRenderingMime"];
					this._havePreferredRenderingMime = true;
				}
				return this._preferredRenderingMime;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06005594 RID: 21908 RVA: 0x0012BA98 File Offset: 0x00129C98
		public virtual string PreferredImageMime
		{
			get
			{
				if (!this._havePreferredImageMime)
				{
					this._preferredImageMime = this["preferredImageMime"];
					this._havePreferredImageMime = true;
				}
				return this._preferredImageMime;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06005595 RID: 21909 RVA: 0x0012BAC8 File Offset: 0x00129CC8
		public virtual int ScreenCharactersWidth
		{
			get
			{
				if (!this._haveScreenCharactersWidth)
				{
					if (this["screenCharactersWidth"] == null)
					{
						int num = 640;
						int num2 = 8;
						if (this["screenPixelsWidth"] != null && this["characterWidth"] != null)
						{
							num = Convert.ToInt32(this["screenPixelsWidth"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterWidth"], CultureInfo.InvariantCulture);
						}
						else if (this["screenPixelsWidth"] != null)
						{
							num = Convert.ToInt32(this["screenPixelsWidth"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["defaultCharacterWidth"], CultureInfo.InvariantCulture);
						}
						else if (this["characterWidth"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenPixelsWidth"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterWidth"], CultureInfo.InvariantCulture);
						}
						else if (this["defaultScreenCharactersWidth"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenCharactersWidth"], CultureInfo.InvariantCulture);
							num2 = 1;
						}
						this._screenCharactersWidth = num / num2;
					}
					else
					{
						this._screenCharactersWidth = Convert.ToInt32(this["screenCharactersWidth"], CultureInfo.InvariantCulture);
					}
					this._haveScreenCharactersWidth = true;
				}
				return this._screenCharactersWidth;
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06005596 RID: 21910 RVA: 0x0012BC1C File Offset: 0x00129E1C
		public virtual int ScreenCharactersHeight
		{
			get
			{
				if (!this._haveScreenCharactersHeight)
				{
					if (this["screenCharactersHeight"] == null)
					{
						int num = 480;
						int num2 = 12;
						if (this["screenPixelsHeight"] != null && this["characterHeight"] != null)
						{
							num = Convert.ToInt32(this["screenPixelsHeight"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterHeight"], CultureInfo.InvariantCulture);
						}
						else if (this["screenPixelsHeight"] != null)
						{
							num = Convert.ToInt32(this["screenPixelsHeight"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["defaultCharacterHeight"], CultureInfo.InvariantCulture);
						}
						else if (this["characterHeight"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenPixelsHeight"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterHeight"], CultureInfo.InvariantCulture);
						}
						else if (this["defaultScreenCharactersHeight"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenCharactersHeight"], CultureInfo.InvariantCulture);
							num2 = 1;
						}
						this._screenCharactersHeight = num / num2;
					}
					else
					{
						this._screenCharactersHeight = Convert.ToInt32(this["screenCharactersHeight"], CultureInfo.InvariantCulture);
					}
					this._haveScreenCharactersHeight = true;
				}
				return this._screenCharactersHeight;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06005597 RID: 21911 RVA: 0x0012BD70 File Offset: 0x00129F70
		public virtual int ScreenPixelsWidth
		{
			get
			{
				if (!this._haveScreenPixelsWidth)
				{
					if (this["screenPixelsWidth"] == null)
					{
						int num = 80;
						int num2 = 8;
						if (this["screenCharactersWidth"] != null && this["characterWidth"] != null)
						{
							num = Convert.ToInt32(this["screenCharactersWidth"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterWidth"], CultureInfo.InvariantCulture);
						}
						else if (this["screenCharactersWidth"] != null)
						{
							num = Convert.ToInt32(this["screenCharactersWidth"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["defaultCharacterWidth"], CultureInfo.InvariantCulture);
						}
						else if (this["characterWidth"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenCharactersWidth"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterWidth"], CultureInfo.InvariantCulture);
						}
						else if (this["defaultScreenPixelsWidth"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenPixelsWidth"], CultureInfo.InvariantCulture);
							num2 = 1;
						}
						this._screenPixelsWidth = num * num2;
					}
					else
					{
						this._screenPixelsWidth = Convert.ToInt32(this["screenPixelsWidth"], CultureInfo.InvariantCulture);
					}
					this._haveScreenPixelsWidth = true;
				}
				return this._screenPixelsWidth;
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06005598 RID: 21912 RVA: 0x0012BEC0 File Offset: 0x0012A0C0
		public virtual int ScreenPixelsHeight
		{
			get
			{
				if (!this._haveScreenPixelsHeight)
				{
					if (this["screenPixelsHeight"] == null)
					{
						int num = 40;
						int num2 = 12;
						if (this["screenCharactersHeight"] != null && this["characterHeight"] != null)
						{
							num = Convert.ToInt32(this["screenCharactersHeight"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterHeight"], CultureInfo.InvariantCulture);
						}
						else if (this["screenCharactersHeight"] != null)
						{
							num = Convert.ToInt32(this["screenCharactersHeight"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["defaultCharacterHeight"], CultureInfo.InvariantCulture);
						}
						else if (this["characterHeight"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenCharactersHeight"], CultureInfo.InvariantCulture);
							num2 = Convert.ToInt32(this["characterHeight"], CultureInfo.InvariantCulture);
						}
						else if (this["defaultScreenPixelsHeight"] != null)
						{
							num = Convert.ToInt32(this["defaultScreenPixelsHeight"], CultureInfo.InvariantCulture);
							num2 = 1;
						}
						this._screenPixelsHeight = num * num2;
					}
					else
					{
						this._screenPixelsHeight = Convert.ToInt32(this["screenPixelsHeight"], CultureInfo.InvariantCulture);
					}
					this._haveScreenPixelsHeight = true;
				}
				return this._screenPixelsHeight;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06005599 RID: 21913 RVA: 0x0012C011 File Offset: 0x0012A211
		public virtual int ScreenBitDepth
		{
			get
			{
				if (!this._haveScreenBitDepth)
				{
					this._screenBitDepth = Convert.ToInt32(this["screenBitDepth"], CultureInfo.InvariantCulture);
					this._haveScreenBitDepth = true;
				}
				return this._screenBitDepth;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x0012C04C File Offset: 0x0012A24C
		public virtual bool IsColor
		{
			get
			{
				if (!this._haveIsColor)
				{
					if (this["isColor"] == null)
					{
						this._isColor = false;
					}
					else
					{
						this._isColor = Convert.ToBoolean(this["isColor"], CultureInfo.InvariantCulture);
					}
					this._haveIsColor = true;
				}
				return this._isColor;
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x0600559B RID: 21915 RVA: 0x0012C0AB File Offset: 0x0012A2AB
		public virtual string InputType
		{
			get
			{
				if (!this._haveInputType)
				{
					this._inputType = this["inputType"];
					this._haveInputType = true;
				}
				return this._inputType;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x0012C0DB File Offset: 0x0012A2DB
		public virtual int NumberOfSoftkeys
		{
			get
			{
				if (!this._haveNumberOfSoftkeys)
				{
					this._numberOfSoftkeys = Convert.ToInt32(this["numberOfSoftkeys"], CultureInfo.InvariantCulture);
					this._haveNumberOfSoftkeys = true;
				}
				return this._numberOfSoftkeys;
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x0600559D RID: 21917 RVA: 0x0012C115 File Offset: 0x0012A315
		public virtual int MaximumSoftkeyLabelLength
		{
			get
			{
				if (!this._haveMaximumSoftkeyLabelLength)
				{
					this._maximumSoftkeyLabelLength = Convert.ToInt32(this["maximumSoftkeyLabelLength"], CultureInfo.InvariantCulture);
					this._haveMaximumSoftkeyLabelLength = true;
				}
				return this._maximumSoftkeyLabelLength;
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x0600559E RID: 21918 RVA: 0x0012C14F File Offset: 0x0012A34F
		public virtual bool CanInitiateVoiceCall
		{
			get
			{
				if (!this._haveCanInitiateVoiceCall)
				{
					this._canInitiateVoiceCall = this.CapsParseBoolDefault("canInitiateVoiceCall", false);
					this._haveCanInitiateVoiceCall = true;
				}
				return this._canInitiateVoiceCall;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x0600559F RID: 21919 RVA: 0x0012C180 File Offset: 0x0012A380
		public virtual bool CanSendMail
		{
			get
			{
				if (!this._haveCanSendMail)
				{
					this._canSendMail = this.CapsParseBoolDefault("canSendMail", true);
					this._haveCanSendMail = true;
				}
				return this._canSendMail;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x0012C1B1 File Offset: 0x0012A3B1
		public virtual bool HasBackButton
		{
			get
			{
				if (!this._haveHasBackButton)
				{
					this._hasBackButton = this.CapsParseBoolDefault("hasBackButton", true);
					this._haveHasBackButton = true;
				}
				return this._hasBackButton;
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x060055A1 RID: 21921 RVA: 0x0012C1E2 File Offset: 0x0012A3E2
		public virtual bool RendersWmlDoAcceptsInline
		{
			get
			{
				if (!this._haveRendersWmlDoAcceptsInline)
				{
					this._rendersWmlDoAcceptsInline = this.CapsParseBoolDefault("rendersWmlDoAcceptsInline", true);
					this._haveRendersWmlDoAcceptsInline = true;
				}
				return this._rendersWmlDoAcceptsInline;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x060055A2 RID: 21922 RVA: 0x0012C213 File Offset: 0x0012A413
		public virtual bool RendersWmlSelectsAsMenuCards
		{
			get
			{
				if (!this._haveRendersWmlSelectsAsMenuCards)
				{
					this._rendersWmlSelectsAsMenuCards = this.CapsParseBoolDefault("rendersWmlSelectsAsMenuCards", false);
					this._haveRendersWmlSelectsAsMenuCards = true;
				}
				return this._rendersWmlSelectsAsMenuCards;
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x0012C244 File Offset: 0x0012A444
		public virtual bool RendersBreaksAfterWmlAnchor
		{
			get
			{
				if (!this._haveRendersBreaksAfterWmlAnchor)
				{
					this._rendersBreaksAfterWmlAnchor = this.CapsParseBoolDefault("rendersBreaksAfterWmlAnchor", true);
					this._haveRendersBreaksAfterWmlAnchor = true;
				}
				return this._rendersBreaksAfterWmlAnchor;
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x060055A4 RID: 21924 RVA: 0x0012C275 File Offset: 0x0012A475
		public virtual bool RendersBreaksAfterWmlInput
		{
			get
			{
				if (!this._haveRendersBreaksAfterWmlInput)
				{
					this._rendersBreaksAfterWmlInput = this.CapsParseBoolDefault("rendersBreaksAfterWmlInput", true);
					this._haveRendersBreaksAfterWmlInput = true;
				}
				return this._rendersBreaksAfterWmlInput;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x060055A5 RID: 21925 RVA: 0x0012C2A6 File Offset: 0x0012A4A6
		public virtual bool RendersBreakBeforeWmlSelectAndInput
		{
			get
			{
				if (!this._haveRendersBreakBeforeWmlSelectAndInput)
				{
					this._rendersBreakBeforeWmlSelectAndInput = this.CapsParseBoolDefault("rendersBreakBeforeWmlSelectAndInput", false);
					this._haveRendersBreakBeforeWmlSelectAndInput = true;
				}
				return this._rendersBreakBeforeWmlSelectAndInput;
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x060055A6 RID: 21926 RVA: 0x0012C2D7 File Offset: 0x0012A4D7
		public virtual bool RequiresPhoneNumbersAsPlainText
		{
			get
			{
				if (!this._haveRequiresPhoneNumbersAsPlainText)
				{
					this._requiresPhoneNumbersAsPlainText = this.CapsParseBoolDefault("requiresPhoneNumbersAsPlainText", false);
					this._haveRequiresPhoneNumbersAsPlainText = true;
				}
				return this._requiresPhoneNumbersAsPlainText;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x060055A7 RID: 21927 RVA: 0x0012C308 File Offset: 0x0012A508
		public virtual bool RequiresUrlEncodedPostfieldValues
		{
			get
			{
				if (!this._haveRequiresUrlEncodedPostfieldValues)
				{
					this._requiresUrlEncodedPostfieldValues = this.CapsParseBoolDefault("requiresUrlEncodedPostfieldValues", true);
					this._haveRequiresUrlEncodedPostfieldValues = true;
				}
				return this._requiresUrlEncodedPostfieldValues;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x060055A8 RID: 21928 RVA: 0x0012C33C File Offset: 0x0012A53C
		public virtual string RequiredMetaTagNameValue
		{
			get
			{
				if (!this._haveRequiredMetaTagNameValue)
				{
					string text = this["requiredMetaTagNameValue"];
					if (!string.IsNullOrEmpty(text))
					{
						this._requiredMetaTagNameValue = text;
					}
					else
					{
						this._requiredMetaTagNameValue = null;
					}
					this._haveRequiredMetaTagNameValue = true;
				}
				return this._requiredMetaTagNameValue;
			}
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x0012C38C File Offset: 0x0012A58C
		public virtual bool RendersBreaksAfterHtmlLists
		{
			get
			{
				if (!this._haveRendersBreaksAfterHtmlLists)
				{
					this._rendersBreaksAfterHtmlLists = this.CapsParseBoolDefault("rendersBreaksAfterHtmlLists", true);
					this._haveRendersBreaksAfterHtmlLists = true;
				}
				return this._rendersBreaksAfterHtmlLists;
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x060055AA RID: 21930 RVA: 0x0012C3BD File Offset: 0x0012A5BD
		public virtual bool RequiresUniqueHtmlInputNames
		{
			get
			{
				if (!this._haveRequiresUniqueHtmlInputNames)
				{
					this._requiresUniqueHtmlInputNames = this.CapsParseBoolDefault("requiresUniqueHtmlInputNames", false);
					this._haveRequiresUniqueHtmlInputNames = true;
				}
				return this._requiresUniqueHtmlInputNames;
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x0012C3EE File Offset: 0x0012A5EE
		public virtual bool RequiresUniqueHtmlCheckboxNames
		{
			get
			{
				if (!this._haveRequiresUniqueHtmlCheckboxNames)
				{
					this._requiresUniqueHtmlCheckboxNames = this.CapsParseBoolDefault("requiresUniqueHtmlCheckboxNames", false);
					this._haveRequiresUniqueHtmlCheckboxNames = true;
				}
				return this._requiresUniqueHtmlCheckboxNames;
			}
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x060055AC RID: 21932 RVA: 0x0012C41F File Offset: 0x0012A61F
		public virtual bool SupportsCss
		{
			get
			{
				if (!this._haveSupportsCss)
				{
					this._supportsCss = this.CapsParseBoolDefault("supportsCss", false);
					this._haveSupportsCss = true;
				}
				return this._supportsCss;
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x060055AD RID: 21933 RVA: 0x0012C450 File Offset: 0x0012A650
		public virtual bool HidesRightAlignedMultiselectScrollbars
		{
			get
			{
				if (!this._haveHidesRightAlignedMultiselectScrollbars)
				{
					this._hidesRightAlignedMultiselectScrollbars = this.CapsParseBoolDefault("hidesRightAlignedMultiselectScrollbars", false);
					this._haveHidesRightAlignedMultiselectScrollbars = true;
				}
				return this._hidesRightAlignedMultiselectScrollbars;
			}
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x060055AE RID: 21934 RVA: 0x0012C481 File Offset: 0x0012A681
		public virtual bool IsMobileDevice
		{
			get
			{
				if (!this._haveIsMobileDevice)
				{
					this._isMobileDevice = this.CapsParseBoolDefault("isMobileDevice", false);
					this._haveIsMobileDevice = true;
				}
				return this._isMobileDevice;
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x060055AF RID: 21935 RVA: 0x0012C4B2 File Offset: 0x0012A6B2
		public virtual bool RequiresAttributeColonSubstitution
		{
			get
			{
				if (!this._haveRequiresAttributeColonSubstitution)
				{
					this._requiresAttributeColonSubstitution = this.CapsParseBoolDefault("requiresAttributeColonSubstitution", false);
					this._haveRequiresAttributeColonSubstitution = true;
				}
				return this._requiresAttributeColonSubstitution;
			}
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x060055B0 RID: 21936 RVA: 0x0012C4E3 File Offset: 0x0012A6E3
		public virtual bool CanRenderOneventAndPrevElementsTogether
		{
			get
			{
				if (!this._haveCanRenderOneventAndPrevElementsTogether)
				{
					this._canRenderOneventAndPrevElementsTogether = this.CapsParseBoolDefault("canRenderOneventAndPrevElementsTogether", true);
					this._haveCanRenderOneventAndPrevElementsTogether = true;
				}
				return this._canRenderOneventAndPrevElementsTogether;
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x060055B1 RID: 21937 RVA: 0x0012C514 File Offset: 0x0012A714
		public virtual bool CanRenderInputAndSelectElementsTogether
		{
			get
			{
				if (!this._haveCanRenderInputAndSelectElementsTogether)
				{
					this._canRenderInputAndSelectElementsTogether = this.CapsParseBoolDefault("canRenderInputAndSelectElementsTogether", true);
					this._haveCanRenderInputAndSelectElementsTogether = true;
				}
				return this._canRenderInputAndSelectElementsTogether;
			}
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x060055B2 RID: 21938 RVA: 0x0012C545 File Offset: 0x0012A745
		public virtual bool CanRenderAfterInputOrSelectElement
		{
			get
			{
				if (!this._haveCanRenderAfterInputOrSelectElement)
				{
					this._canRenderAfterInputOrSelectElement = this.CapsParseBoolDefault("canRenderAfterInputOrSelectElement", true);
					this._haveCanRenderAfterInputOrSelectElement = true;
				}
				return this._canRenderAfterInputOrSelectElement;
			}
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x060055B3 RID: 21939 RVA: 0x0012C576 File Offset: 0x0012A776
		public virtual bool CanRenderPostBackCards
		{
			get
			{
				if (!this._haveCanRenderPostBackCards)
				{
					this._canRenderPostBackCards = this.CapsParseBoolDefault("canRenderPostBackCards", true);
					this._haveCanRenderPostBackCards = true;
				}
				return this._canRenderPostBackCards;
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x060055B4 RID: 21940 RVA: 0x0012C5A7 File Offset: 0x0012A7A7
		public virtual bool CanRenderMixedSelects
		{
			get
			{
				if (!this._haveCanRenderMixedSelects)
				{
					this._canRenderMixedSelects = this.CapsParseBoolDefault("canRenderMixedSelects", true);
					this._haveCanRenderMixedSelects = true;
				}
				return this._canRenderMixedSelects;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x060055B5 RID: 21941 RVA: 0x0012C5D8 File Offset: 0x0012A7D8
		public virtual bool CanCombineFormsInDeck
		{
			get
			{
				if (!this._haveCanCombineFormsInDeck)
				{
					this._canCombineFormsInDeck = this.CapsParseBoolDefault("canCombineFormsInDeck", true);
					this._haveCanCombineFormsInDeck = true;
				}
				return this._canCombineFormsInDeck;
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x060055B6 RID: 21942 RVA: 0x0012C609 File Offset: 0x0012A809
		public virtual bool CanRenderSetvarZeroWithMultiSelectionList
		{
			get
			{
				if (!this._haveCanRenderSetvarZeroWithMultiSelectionList)
				{
					this._canRenderSetvarZeroWithMultiSelectionList = this.CapsParseBoolDefault("canRenderSetvarZeroWithMultiSelectionList", true);
					this._haveCanRenderSetvarZeroWithMultiSelectionList = true;
				}
				return this._canRenderSetvarZeroWithMultiSelectionList;
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x060055B7 RID: 21943 RVA: 0x0012C63A File Offset: 0x0012A83A
		public virtual bool SupportsImageSubmit
		{
			get
			{
				if (!this._haveSupportsImageSubmit)
				{
					this._supportsImageSubmit = this.CapsParseBoolDefault("supportsImageSubmit", false);
					this._haveSupportsImageSubmit = true;
				}
				return this._supportsImageSubmit;
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x0012C66B File Offset: 0x0012A86B
		public virtual bool RequiresUniqueFilePathSuffix
		{
			get
			{
				if (!this._haveRequiresUniqueFilePathSuffix)
				{
					this._requiresUniqueFilePathSuffix = this.CapsParseBoolDefault("requiresUniqueFilePathSuffix", false);
					this._haveRequiresUniqueFilePathSuffix = true;
				}
				return this._requiresUniqueFilePathSuffix;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x060055B9 RID: 21945 RVA: 0x0012C69C File Offset: 0x0012A89C
		public virtual bool RequiresNoBreakInFormatting
		{
			get
			{
				if (!this._haveRequiresNoBreakInFormatting)
				{
					this._requiresNoBreakInFormatting = this.CapsParseBoolDefault("requiresNoBreakInFormatting", false);
					this._haveRequiresNoBreakInFormatting = true;
				}
				return this._requiresNoBreakInFormatting;
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x060055BA RID: 21946 RVA: 0x0012C6CD File Offset: 0x0012A8CD
		public virtual bool RequiresLeadingPageBreak
		{
			get
			{
				if (!this._haveRequiresLeadingPageBreak)
				{
					this._requiresLeadingPageBreak = this.CapsParseBoolDefault("requiresLeadingPageBreak", false);
					this._haveRequiresLeadingPageBreak = true;
				}
				return this._requiresLeadingPageBreak;
			}
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x060055BB RID: 21947 RVA: 0x0012C6FE File Offset: 0x0012A8FE
		public virtual bool SupportsSelectMultiple
		{
			get
			{
				if (!this._haveSupportsSelectMultiple)
				{
					this._supportsSelectMultiple = this.CapsParseBoolDefault("supportsSelectMultiple", false);
					this._haveSupportsSelectMultiple = true;
				}
				return this._supportsSelectMultiple;
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x060055BC RID: 21948 RVA: 0x0012C72F File Offset: 0x0012A92F
		public virtual bool SupportsBold
		{
			get
			{
				if (!this._haveSupportsBold)
				{
					this._supportsBold = this.CapsParseBoolDefault("supportsBold", true);
					this._haveSupportsBold = true;
				}
				return this._supportsBold;
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x060055BD RID: 21949 RVA: 0x0012C760 File Offset: 0x0012A960
		public virtual bool SupportsItalic
		{
			get
			{
				if (!this._haveSupportsItalic)
				{
					this._supportsItalic = this.CapsParseBoolDefault("supportsItalic", true);
					this._haveSupportsItalic = true;
				}
				return this._supportsItalic;
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x0012C791 File Offset: 0x0012A991
		public virtual bool SupportsFontSize
		{
			get
			{
				if (!this._haveSupportsFontSize)
				{
					this._supportsFontSize = this.CapsParseBoolDefault("supportsFontSize", false);
					this._haveSupportsFontSize = true;
				}
				return this._supportsFontSize;
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x060055BF RID: 21951 RVA: 0x0012C7C2 File Offset: 0x0012A9C2
		public virtual bool SupportsFontName
		{
			get
			{
				if (!this._haveSupportsFontName)
				{
					this._supportsFontName = this.CapsParseBoolDefault("supportsFontName", false);
					this._haveSupportsFontName = true;
				}
				return this._supportsFontName;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x060055C0 RID: 21952 RVA: 0x0012C7F3 File Offset: 0x0012A9F3
		public virtual bool SupportsFontColor
		{
			get
			{
				if (!this._haveSupportsFontColor)
				{
					this._supportsFontColor = this.CapsParseBoolDefault("supportsFontColor", false);
					this._haveSupportsFontColor = true;
				}
				return this._supportsFontColor;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x060055C1 RID: 21953 RVA: 0x0012C824 File Offset: 0x0012AA24
		public virtual bool SupportsBodyColor
		{
			get
			{
				if (!this._haveSupportsBodyColor)
				{
					this._supportsBodyColor = this.CapsParseBoolDefault("supportsBodyColor", false);
					this._haveSupportsBodyColor = true;
				}
				return this._supportsBodyColor;
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x060055C2 RID: 21954 RVA: 0x0012C855 File Offset: 0x0012AA55
		public virtual bool SupportsDivAlign
		{
			get
			{
				if (!this._haveSupportsDivAlign)
				{
					this._supportsDivAlign = this.CapsParseBoolDefault("supportsDivAlign", false);
					this._haveSupportsDivAlign = true;
				}
				return this._supportsDivAlign;
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x0012C886 File Offset: 0x0012AA86
		public virtual bool SupportsDivNoWrap
		{
			get
			{
				if (!this._haveSupportsDivNoWrap)
				{
					this._supportsDivNoWrap = this.CapsParseBoolDefault("supportsDivNoWrap", false);
					this._haveSupportsDivNoWrap = true;
				}
				return this._supportsDivNoWrap;
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x060055C4 RID: 21956 RVA: 0x0012C8B7 File Offset: 0x0012AAB7
		internal bool SupportsMaintainScrollPositionOnPostback
		{
			get
			{
				if (!this._haveSupportsMaintainScrollPositionOnPostback)
				{
					this._supportsMaintainScrollPositionOnPostback = this.CapsParseBoolDefault("supportsMaintainScrollPositionOnPostback", false);
					this._haveSupportsMaintainScrollPositionOnPostback = true;
				}
				return this._supportsMaintainScrollPositionOnPostback;
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x060055C5 RID: 21957 RVA: 0x0012C8E8 File Offset: 0x0012AAE8
		public virtual bool RequiresContentTypeMetaTag
		{
			get
			{
				if (!this._haveRequiresContentTypeMetaTag)
				{
					this._requiresContentTypeMetaTag = this.CapsParseBoolDefault("requiresContentTypeMetaTag", false);
					this._haveRequiresContentTypeMetaTag = true;
				}
				return this._requiresContentTypeMetaTag;
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x060055C6 RID: 21958 RVA: 0x0012C919 File Offset: 0x0012AB19
		public virtual bool RequiresDBCSCharacter
		{
			get
			{
				if (!this._haveRequiresDBCSCharacter)
				{
					this._requiresDBCSCharacter = this.CapsParseBoolDefault("requiresDBCSCharacter", false);
					this._haveRequiresDBCSCharacter = true;
				}
				return this._requiresDBCSCharacter;
			}
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x060055C7 RID: 21959 RVA: 0x0012C94A File Offset: 0x0012AB4A
		public virtual bool RequiresHtmlAdaptiveErrorReporting
		{
			get
			{
				if (!this._haveRequiresHtmlAdaptiveErrorReporting)
				{
					this._requiresHtmlAdaptiveErrorReporting = this.CapsParseBoolDefault("requiresHtmlAdaptiveErrorReporting", false);
					this._haveRequiresHtmlAdaptiveErrorReporting = true;
				}
				return this._requiresHtmlAdaptiveErrorReporting;
			}
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x060055C8 RID: 21960 RVA: 0x0012C97B File Offset: 0x0012AB7B
		public virtual bool RequiresOutputOptimization
		{
			get
			{
				if (!this._haveRequiresOutputOptimization)
				{
					this._requiresOutputOptimization = this.CapsParseBoolDefault("requiresOutputOptimization", false);
					this._haveRequiresOutputOptimization = true;
				}
				return this._requiresOutputOptimization;
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x060055C9 RID: 21961 RVA: 0x0012C9AC File Offset: 0x0012ABAC
		public virtual bool SupportsAccesskeyAttribute
		{
			get
			{
				if (!this._haveSupportsAccesskeyAttribute)
				{
					this._supportsAccesskeyAttribute = this.CapsParseBoolDefault("supportsAccesskeyAttribute", false);
					this._haveSupportsAccesskeyAttribute = true;
				}
				return this._supportsAccesskeyAttribute;
			}
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x060055CA RID: 21962 RVA: 0x0012C9DD File Offset: 0x0012ABDD
		public virtual bool SupportsInputIStyle
		{
			get
			{
				if (!this._haveSupportsInputIStyle)
				{
					this._supportsInputIStyle = this.CapsParseBoolDefault("supportsInputIStyle", false);
					this._haveSupportsInputIStyle = true;
				}
				return this._supportsInputIStyle;
			}
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x060055CB RID: 21963 RVA: 0x0012CA0E File Offset: 0x0012AC0E
		public virtual bool SupportsInputMode
		{
			get
			{
				if (!this._haveSupportsInputMode)
				{
					this._supportsInputMode = this.CapsParseBoolDefault("supportsInputMode", false);
					this._haveSupportsInputMode = true;
				}
				return this._supportsInputMode;
			}
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x060055CC RID: 21964 RVA: 0x0012CA3F File Offset: 0x0012AC3F
		public virtual bool SupportsIModeSymbols
		{
			get
			{
				if (!this._haveSupportsIModeSymbols)
				{
					this._supportsIModeSymbols = this.CapsParseBoolDefault("supportsIModeSymbols", false);
					this._haveSupportsIModeSymbols = true;
				}
				return this._supportsIModeSymbols;
			}
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x060055CD RID: 21965 RVA: 0x0012CA70 File Offset: 0x0012AC70
		public virtual bool SupportsJPhoneSymbols
		{
			get
			{
				if (!this._haveSupportsJPhoneSymbols)
				{
					this._supportsJPhoneSymbols = this.CapsParseBoolDefault("supportsJPhoneSymbols", false);
					this._haveSupportsJPhoneSymbols = true;
				}
				return this._supportsJPhoneSymbols;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x0012CAA1 File Offset: 0x0012ACA1
		public virtual bool SupportsJPhoneMultiMediaAttributes
		{
			get
			{
				if (!this._haveSupportsJPhoneMultiMediaAttributes)
				{
					this._supportsJPhoneMultiMediaAttributes = this.CapsParseBoolDefault("supportsJPhoneMultiMediaAttributes", false);
					this._haveSupportsJPhoneMultiMediaAttributes = true;
				}
				return this._supportsJPhoneMultiMediaAttributes;
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x0012CAD2 File Offset: 0x0012ACD2
		public virtual int MaximumRenderedPageSize
		{
			get
			{
				if (!this._haveMaximumRenderedPageSize)
				{
					this._maximumRenderedPageSize = Convert.ToInt32(this["maximumRenderedPageSize"], CultureInfo.InvariantCulture);
					this._haveMaximumRenderedPageSize = true;
				}
				return this._maximumRenderedPageSize;
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x060055D0 RID: 21968 RVA: 0x0012CB0C File Offset: 0x0012AD0C
		public virtual bool RequiresSpecialViewStateEncoding
		{
			get
			{
				if (!this._haveRequiresSpecialViewStateEncoding)
				{
					this._requiresSpecialViewStateEncoding = this.CapsParseBoolDefault("requiresSpecialViewStateEncoding", false);
					this._haveRequiresSpecialViewStateEncoding = true;
				}
				return this._requiresSpecialViewStateEncoding;
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x0012CB3D File Offset: 0x0012AD3D
		public virtual bool SupportsQueryStringInFormAction
		{
			get
			{
				if (!this._haveSupportsQueryStringInFormAction)
				{
					this._supportsQueryStringInFormAction = this.CapsParseBoolDefault("supportsQueryStringInFormAction", true);
					this._haveSupportsQueryStringInFormAction = true;
				}
				return this._supportsQueryStringInFormAction;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x0012CB6E File Offset: 0x0012AD6E
		public virtual bool SupportsCacheControlMetaTag
		{
			get
			{
				if (!this._haveSupportsCacheControlMetaTag)
				{
					this._supportsCacheControlMetaTag = this.CapsParseBoolDefault("supportsCacheControlMetaTag", true);
					this._haveSupportsCacheControlMetaTag = true;
				}
				return this._supportsCacheControlMetaTag;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x0012CB9F File Offset: 0x0012AD9F
		public virtual bool SupportsUncheck
		{
			get
			{
				if (!this._haveSupportsUncheck)
				{
					this._supportsUncheck = this.CapsParseBoolDefault("supportsUncheck", true);
					this._haveSupportsUncheck = true;
				}
				return this._supportsUncheck;
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x0012CBD0 File Offset: 0x0012ADD0
		public virtual bool CanRenderEmptySelects
		{
			get
			{
				if (!this._haveCanRenderEmptySelects)
				{
					this._canRenderEmptySelects = this.CapsParseBoolDefault("canRenderEmptySelects", true);
					this._haveCanRenderEmptySelects = true;
				}
				return this._canRenderEmptySelects;
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x0012CC01 File Offset: 0x0012AE01
		public virtual bool SupportsRedirectWithCookie
		{
			get
			{
				if (!this._haveSupportsRedirectWithCookie)
				{
					this._supportsRedirectWithCookie = this.CapsParseBoolDefault("supportsRedirectWithCookie", true);
					this._haveSupportsRedirectWithCookie = true;
				}
				return this._supportsRedirectWithCookie;
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x0012CC32 File Offset: 0x0012AE32
		public virtual bool SupportsEmptyStringInCookieValue
		{
			get
			{
				if (!this._haveSupportsEmptyStringInCookieValue)
				{
					this._supportsEmptyStringInCookieValue = this.CapsParseBoolDefault("supportsEmptyStringInCookieValue", true);
					this._haveSupportsEmptyStringInCookieValue = true;
				}
				return this._supportsEmptyStringInCookieValue;
			}
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x060055D7 RID: 21975 RVA: 0x0012CC64 File Offset: 0x0012AE64
		public virtual int DefaultSubmitButtonLimit
		{
			get
			{
				if (!this._haveDefaultSubmitButtonLimit)
				{
					this._defaultSubmitButtonLimit = ((this["defaultSubmitButtonLimit"] != null) ? Convert.ToInt32(this["defaultSubmitButtonLimit"], CultureInfo.InvariantCulture) : 1);
					this._haveDefaultSubmitButtonLimit = true;
				}
				return this._defaultSubmitButtonLimit;
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x060055D8 RID: 21976 RVA: 0x0012CCBB File Offset: 0x0012AEBB
		public virtual bool SupportsXmlHttp
		{
			get
			{
				if (!this._haveSupportsXmlHttp)
				{
					this._supportsXmlHttp = this.CapsParseBoolDefault("supportsXmlHttp", false);
					this._haveSupportsXmlHttp = true;
				}
				return this._supportsXmlHttp;
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x0012CCEC File Offset: 0x0012AEEC
		public virtual bool SupportsCallback
		{
			get
			{
				if (!this._haveSupportsCallback)
				{
					this._supportsCallback = this.CapsParseBoolDefault("supportsCallback", false);
					this._haveSupportsCallback = true;
				}
				return this._supportsCallback;
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x060055DA RID: 21978 RVA: 0x0012CD1D File Offset: 0x0012AF1D
		public virtual int MaximumHrefLength
		{
			get
			{
				if (!this._haveMaximumHrefLength)
				{
					this._maximumHrefLength = Convert.ToInt32(this["maximumHrefLength"], CultureInfo.InvariantCulture);
					this._haveMaximumHrefLength = true;
				}
				return this._maximumHrefLength;
			}
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x0012CD58 File Offset: 0x0012AF58
		public bool IsBrowser(string browserName)
		{
			if (string.IsNullOrEmpty(browserName))
			{
				return false;
			}
			if (this._browsers == null)
			{
				return false;
			}
			for (int i = 0; i < this._browsers.Count; i++)
			{
				if (string.Equals(browserName, (string)this._browsers[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x0012CDAC File Offset: 0x0012AFAC
		public void AddBrowser(string browserName)
		{
			if (this._browsers == null)
			{
				object staticLock = HttpCapabilitiesBase._staticLock;
				lock (staticLock)
				{
					if (this._browsers == null)
					{
						this._browsers = new ArrayList(6);
					}
				}
			}
			this._browsers.Add(browserName.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x0012CE18 File Offset: 0x0012B018
		bool IFilterResolutionService.EvaluateFilter(string filterName)
		{
			return this.IsBrowser(filterName);
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x000767F7 File Offset: 0x000749F7
		int IFilterResolutionService.CompareFilters(string filter1, string filter2)
		{
			return BrowserCapabilitiesCompiler.BrowserCapabilitiesFactory.CompareFilters(filter1, filter2);
		}

		// Token: 0x04002CBC RID: 11452
		private static FactoryGenerator _controlAdapterFactoryGenerator;

		// Token: 0x04002CBD RID: 11453
		private static Hashtable _controlAdapterFactoryTable;

		// Token: 0x04002CBE RID: 11454
		private static object _staticLock = new object();

		// Token: 0x04002CBF RID: 11455
		private static object s_nullAdapterSingleton = new object();

		// Token: 0x04002CC0 RID: 11456
		private bool _useOptimizedCacheKey = true;

		// Token: 0x04002CC1 RID: 11457
		private static object _emptyHttpCapabilitiesBaseLock = new object();

		// Token: 0x04002CC2 RID: 11458
		private static HttpCapabilitiesProvider _browserCapabilitiesProvider = null;

		// Token: 0x04002CC3 RID: 11459
		private static HttpCapabilitiesBase _emptyHttpCapabilitiesBase;

		// Token: 0x04002CC4 RID: 11460
		private Hashtable _adapterTypes;

		// Token: 0x04002CC5 RID: 11461
		private IDictionary _adapters;

		// Token: 0x04002CC6 RID: 11462
		private string _htmlTextWriter;

		// Token: 0x04002CC7 RID: 11463
		private IDictionary _items;

		// Token: 0x04002CC8 RID: 11464
		private ArrayList _browsers;

		// Token: 0x04002CC9 RID: 11465
		private volatile string _type;

		// Token: 0x04002CCA RID: 11466
		private volatile string _browser;

		// Token: 0x04002CCB RID: 11467
		private volatile string _version;

		// Token: 0x04002CCC RID: 11468
		private volatile int _majorversion;

		// Token: 0x04002CCD RID: 11469
		private double _minorversion;

		// Token: 0x04002CCE RID: 11470
		private volatile string _platform;

		// Token: 0x04002CCF RID: 11471
		private volatile Type _tagwriter;

		// Token: 0x04002CD0 RID: 11472
		private volatile Version _ecmascriptversion;

		// Token: 0x04002CD1 RID: 11473
		private volatile Version _jscriptversion;

		// Token: 0x04002CD2 RID: 11474
		private volatile Version _msdomversion;

		// Token: 0x04002CD3 RID: 11475
		private volatile Version _w3cdomversion;

		// Token: 0x04002CD4 RID: 11476
		private volatile bool _beta;

		// Token: 0x04002CD5 RID: 11477
		private volatile bool _crawler;

		// Token: 0x04002CD6 RID: 11478
		private volatile bool _aol;

		// Token: 0x04002CD7 RID: 11479
		private volatile bool _win16;

		// Token: 0x04002CD8 RID: 11480
		private volatile bool _win32;

		// Token: 0x04002CD9 RID: 11481
		private volatile bool _requiresControlStateInSession;

		// Token: 0x04002CDA RID: 11482
		private volatile bool _frames;

		// Token: 0x04002CDB RID: 11483
		private volatile bool _tables;

		// Token: 0x04002CDC RID: 11484
		private volatile bool _cookies;

		// Token: 0x04002CDD RID: 11485
		private volatile bool _vbscript;

		// Token: 0x04002CDE RID: 11486
		private volatile bool _javascript;

		// Token: 0x04002CDF RID: 11487
		private volatile bool _javaapplets;

		// Token: 0x04002CE0 RID: 11488
		private volatile bool _activexcontrols;

		// Token: 0x04002CE1 RID: 11489
		private volatile bool _backgroundsounds;

		// Token: 0x04002CE2 RID: 11490
		private volatile bool _cdf;

		// Token: 0x04002CE3 RID: 11491
		private volatile bool _havetype;

		// Token: 0x04002CE4 RID: 11492
		private volatile bool _havebrowser;

		// Token: 0x04002CE5 RID: 11493
		private volatile bool _haveversion;

		// Token: 0x04002CE6 RID: 11494
		private volatile bool _havemajorversion;

		// Token: 0x04002CE7 RID: 11495
		private volatile bool _haveminorversion;

		// Token: 0x04002CE8 RID: 11496
		private volatile bool _haveplatform;

		// Token: 0x04002CE9 RID: 11497
		private volatile bool _havetagwriter;

		// Token: 0x04002CEA RID: 11498
		private volatile bool _haveecmascriptversion;

		// Token: 0x04002CEB RID: 11499
		private volatile bool _havemsdomversion;

		// Token: 0x04002CEC RID: 11500
		private volatile bool _havew3cdomversion;

		// Token: 0x04002CED RID: 11501
		private volatile bool _havebeta;

		// Token: 0x04002CEE RID: 11502
		private volatile bool _havecrawler;

		// Token: 0x04002CEF RID: 11503
		private volatile bool _haveaol;

		// Token: 0x04002CF0 RID: 11504
		private volatile bool _havewin16;

		// Token: 0x04002CF1 RID: 11505
		private volatile bool _havewin32;

		// Token: 0x04002CF2 RID: 11506
		private volatile bool _haveframes;

		// Token: 0x04002CF3 RID: 11507
		private volatile bool _haverequiresControlStateInSession;

		// Token: 0x04002CF4 RID: 11508
		private volatile bool _havetables;

		// Token: 0x04002CF5 RID: 11509
		private volatile bool _havecookies;

		// Token: 0x04002CF6 RID: 11510
		private volatile bool _havevbscript;

		// Token: 0x04002CF7 RID: 11511
		private volatile bool _havejavascript;

		// Token: 0x04002CF8 RID: 11512
		private volatile bool _havejavaapplets;

		// Token: 0x04002CF9 RID: 11513
		private volatile bool _haveactivexcontrols;

		// Token: 0x04002CFA RID: 11514
		private volatile bool _havebackgroundsounds;

		// Token: 0x04002CFB RID: 11515
		private volatile bool _havecdf;

		// Token: 0x04002CFC RID: 11516
		private volatile string _mobileDeviceManufacturer;

		// Token: 0x04002CFD RID: 11517
		private volatile string _mobileDeviceModel;

		// Token: 0x04002CFE RID: 11518
		private volatile string _gatewayVersion;

		// Token: 0x04002CFF RID: 11519
		private volatile int _gatewayMajorVersion;

		// Token: 0x04002D00 RID: 11520
		private double _gatewayMinorVersion;

		// Token: 0x04002D01 RID: 11521
		private volatile string _preferredRenderingType;

		// Token: 0x04002D02 RID: 11522
		private volatile string _preferredRenderingMime;

		// Token: 0x04002D03 RID: 11523
		private volatile string _preferredImageMime;

		// Token: 0x04002D04 RID: 11524
		private volatile string _requiredMetaTagNameValue;

		// Token: 0x04002D05 RID: 11525
		private volatile string _preferredRequestEncoding;

		// Token: 0x04002D06 RID: 11526
		private volatile string _preferredResponseEncoding;

		// Token: 0x04002D07 RID: 11527
		private volatile int _screenCharactersWidth;

		// Token: 0x04002D08 RID: 11528
		private volatile int _screenCharactersHeight;

		// Token: 0x04002D09 RID: 11529
		private volatile int _screenPixelsWidth;

		// Token: 0x04002D0A RID: 11530
		private volatile int _screenPixelsHeight;

		// Token: 0x04002D0B RID: 11531
		private volatile int _screenBitDepth;

		// Token: 0x04002D0C RID: 11532
		private volatile bool _isColor;

		// Token: 0x04002D0D RID: 11533
		private volatile string _inputType;

		// Token: 0x04002D0E RID: 11534
		private volatile int _numberOfSoftkeys;

		// Token: 0x04002D0F RID: 11535
		private volatile int _maximumSoftkeyLabelLength;

		// Token: 0x04002D10 RID: 11536
		private volatile bool _canInitiateVoiceCall;

		// Token: 0x04002D11 RID: 11537
		private volatile bool _canSendMail;

		// Token: 0x04002D12 RID: 11538
		private volatile bool _hasBackButton;

		// Token: 0x04002D13 RID: 11539
		private volatile bool _rendersWmlDoAcceptsInline;

		// Token: 0x04002D14 RID: 11540
		private volatile bool _rendersWmlSelectsAsMenuCards;

		// Token: 0x04002D15 RID: 11541
		private volatile bool _rendersBreaksAfterWmlAnchor;

		// Token: 0x04002D16 RID: 11542
		private volatile bool _rendersBreaksAfterWmlInput;

		// Token: 0x04002D17 RID: 11543
		private volatile bool _rendersBreakBeforeWmlSelectAndInput;

		// Token: 0x04002D18 RID: 11544
		private volatile bool _requiresPhoneNumbersAsPlainText;

		// Token: 0x04002D19 RID: 11545
		private volatile bool _requiresAttributeColonSubstitution;

		// Token: 0x04002D1A RID: 11546
		private volatile bool _requiresUrlEncodedPostfieldValues;

		// Token: 0x04002D1B RID: 11547
		private volatile bool _rendersBreaksAfterHtmlLists;

		// Token: 0x04002D1C RID: 11548
		private volatile bool _requiresUniqueHtmlCheckboxNames;

		// Token: 0x04002D1D RID: 11549
		private volatile bool _requiresUniqueHtmlInputNames;

		// Token: 0x04002D1E RID: 11550
		private volatile bool _supportsCss;

		// Token: 0x04002D1F RID: 11551
		private volatile bool _hidesRightAlignedMultiselectScrollbars;

		// Token: 0x04002D20 RID: 11552
		private volatile bool _isMobileDevice;

		// Token: 0x04002D21 RID: 11553
		private volatile bool _canRenderOneventAndPrevElementsTogether;

		// Token: 0x04002D22 RID: 11554
		private volatile bool _canRenderInputAndSelectElementsTogether;

		// Token: 0x04002D23 RID: 11555
		private volatile bool _canRenderAfterInputOrSelectElement;

		// Token: 0x04002D24 RID: 11556
		private volatile bool _canRenderPostBackCards;

		// Token: 0x04002D25 RID: 11557
		private volatile bool _canRenderMixedSelects;

		// Token: 0x04002D26 RID: 11558
		private volatile bool _canCombineFormsInDeck;

		// Token: 0x04002D27 RID: 11559
		private volatile bool _canRenderSetvarZeroWithMultiSelectionList;

		// Token: 0x04002D28 RID: 11560
		private volatile bool _supportsImageSubmit;

		// Token: 0x04002D29 RID: 11561
		private volatile bool _requiresUniqueFilePathSuffix;

		// Token: 0x04002D2A RID: 11562
		private volatile bool _requiresNoBreakInFormatting;

		// Token: 0x04002D2B RID: 11563
		private volatile bool _requiresLeadingPageBreak;

		// Token: 0x04002D2C RID: 11564
		private volatile bool _supportsSelectMultiple;

		// Token: 0x04002D2D RID: 11565
		private volatile bool _supportsBold;

		// Token: 0x04002D2E RID: 11566
		private volatile bool _supportsItalic;

		// Token: 0x04002D2F RID: 11567
		private volatile bool _supportsFontSize;

		// Token: 0x04002D30 RID: 11568
		private volatile bool _supportsFontName;

		// Token: 0x04002D31 RID: 11569
		private volatile bool _supportsFontColor;

		// Token: 0x04002D32 RID: 11570
		private volatile bool _supportsBodyColor;

		// Token: 0x04002D33 RID: 11571
		private volatile bool _supportsDivAlign;

		// Token: 0x04002D34 RID: 11572
		private volatile bool _supportsDivNoWrap;

		// Token: 0x04002D35 RID: 11573
		private volatile bool _requiresHtmlAdaptiveErrorReporting;

		// Token: 0x04002D36 RID: 11574
		private volatile bool _requiresContentTypeMetaTag;

		// Token: 0x04002D37 RID: 11575
		private volatile bool _requiresDBCSCharacter;

		// Token: 0x04002D38 RID: 11576
		private volatile bool _requiresOutputOptimization;

		// Token: 0x04002D39 RID: 11577
		private volatile bool _supportsAccesskeyAttribute;

		// Token: 0x04002D3A RID: 11578
		private volatile bool _supportsInputIStyle;

		// Token: 0x04002D3B RID: 11579
		private volatile bool _supportsInputMode;

		// Token: 0x04002D3C RID: 11580
		private volatile bool _supportsIModeSymbols;

		// Token: 0x04002D3D RID: 11581
		private volatile bool _supportsJPhoneSymbols;

		// Token: 0x04002D3E RID: 11582
		private volatile bool _supportsJPhoneMultiMediaAttributes;

		// Token: 0x04002D3F RID: 11583
		private volatile int _maximumRenderedPageSize;

		// Token: 0x04002D40 RID: 11584
		private volatile bool _requiresSpecialViewStateEncoding;

		// Token: 0x04002D41 RID: 11585
		private volatile bool _supportsQueryStringInFormAction;

		// Token: 0x04002D42 RID: 11586
		private volatile bool _supportsCacheControlMetaTag;

		// Token: 0x04002D43 RID: 11587
		private volatile bool _supportsUncheck;

		// Token: 0x04002D44 RID: 11588
		private volatile bool _canRenderEmptySelects;

		// Token: 0x04002D45 RID: 11589
		private volatile bool _supportsRedirectWithCookie;

		// Token: 0x04002D46 RID: 11590
		private volatile bool _supportsEmptyStringInCookieValue;

		// Token: 0x04002D47 RID: 11591
		private volatile int _defaultSubmitButtonLimit;

		// Token: 0x04002D48 RID: 11592
		private volatile bool _supportsXmlHttp;

		// Token: 0x04002D49 RID: 11593
		private volatile bool _supportsCallback;

		// Token: 0x04002D4A RID: 11594
		private volatile bool _supportsMaintainScrollPositionOnPostback;

		// Token: 0x04002D4B RID: 11595
		private volatile int _maximumHrefLength;

		// Token: 0x04002D4C RID: 11596
		private volatile bool _haveMobileDeviceManufacturer;

		// Token: 0x04002D4D RID: 11597
		private volatile bool _haveMobileDeviceModel;

		// Token: 0x04002D4E RID: 11598
		private volatile bool _haveGatewayVersion;

		// Token: 0x04002D4F RID: 11599
		private volatile bool _haveGatewayMajorVersion;

		// Token: 0x04002D50 RID: 11600
		private volatile bool _haveGatewayMinorVersion;

		// Token: 0x04002D51 RID: 11601
		private volatile bool _havePreferredRenderingType;

		// Token: 0x04002D52 RID: 11602
		private volatile bool _havePreferredRenderingMime;

		// Token: 0x04002D53 RID: 11603
		private volatile bool _havePreferredImageMime;

		// Token: 0x04002D54 RID: 11604
		private volatile bool _havePreferredRequestEncoding;

		// Token: 0x04002D55 RID: 11605
		private volatile bool _havePreferredResponseEncoding;

		// Token: 0x04002D56 RID: 11606
		private volatile bool _haveScreenCharactersWidth;

		// Token: 0x04002D57 RID: 11607
		private volatile bool _haveScreenCharactersHeight;

		// Token: 0x04002D58 RID: 11608
		private volatile bool _haveScreenPixelsWidth;

		// Token: 0x04002D59 RID: 11609
		private volatile bool _haveScreenPixelsHeight;

		// Token: 0x04002D5A RID: 11610
		private volatile bool _haveScreenBitDepth;

		// Token: 0x04002D5B RID: 11611
		private volatile bool _haveIsColor;

		// Token: 0x04002D5C RID: 11612
		private volatile bool _haveInputType;

		// Token: 0x04002D5D RID: 11613
		private volatile bool _haveNumberOfSoftkeys;

		// Token: 0x04002D5E RID: 11614
		private volatile bool _haveMaximumSoftkeyLabelLength;

		// Token: 0x04002D5F RID: 11615
		private volatile bool _haveCanInitiateVoiceCall;

		// Token: 0x04002D60 RID: 11616
		private volatile bool _haveCanSendMail;

		// Token: 0x04002D61 RID: 11617
		private volatile bool _haveHasBackButton;

		// Token: 0x04002D62 RID: 11618
		private volatile bool _haveRendersWmlDoAcceptsInline;

		// Token: 0x04002D63 RID: 11619
		private volatile bool _haveRendersWmlSelectsAsMenuCards;

		// Token: 0x04002D64 RID: 11620
		private volatile bool _haveRendersBreaksAfterWmlAnchor;

		// Token: 0x04002D65 RID: 11621
		private volatile bool _haveRendersBreaksAfterWmlInput;

		// Token: 0x04002D66 RID: 11622
		private volatile bool _haveRendersBreakBeforeWmlSelectAndInput;

		// Token: 0x04002D67 RID: 11623
		private volatile bool _haveRequiresPhoneNumbersAsPlainText;

		// Token: 0x04002D68 RID: 11624
		private volatile bool _haveRequiresUrlEncodedPostfieldValues;

		// Token: 0x04002D69 RID: 11625
		private volatile bool _haveRequiredMetaTagNameValue;

		// Token: 0x04002D6A RID: 11626
		private volatile bool _haveRendersBreaksAfterHtmlLists;

		// Token: 0x04002D6B RID: 11627
		private volatile bool _haveRequiresUniqueHtmlCheckboxNames;

		// Token: 0x04002D6C RID: 11628
		private volatile bool _haveRequiresUniqueHtmlInputNames;

		// Token: 0x04002D6D RID: 11629
		private volatile bool _haveSupportsCss;

		// Token: 0x04002D6E RID: 11630
		private volatile bool _haveHidesRightAlignedMultiselectScrollbars;

		// Token: 0x04002D6F RID: 11631
		private volatile bool _haveIsMobileDevice;

		// Token: 0x04002D70 RID: 11632
		private volatile bool _haveCanRenderOneventAndPrevElementsTogether;

		// Token: 0x04002D71 RID: 11633
		private volatile bool _haveCanRenderInputAndSelectElementsTogether;

		// Token: 0x04002D72 RID: 11634
		private volatile bool _haveCanRenderAfterInputOrSelectElement;

		// Token: 0x04002D73 RID: 11635
		private volatile bool _haveCanRenderPostBackCards;

		// Token: 0x04002D74 RID: 11636
		private volatile bool _haveCanCombineFormsInDeck;

		// Token: 0x04002D75 RID: 11637
		private volatile bool _haveCanRenderMixedSelects;

		// Token: 0x04002D76 RID: 11638
		private volatile bool _haveCanRenderSetvarZeroWithMultiSelectionList;

		// Token: 0x04002D77 RID: 11639
		private volatile bool _haveSupportsImageSubmit;

		// Token: 0x04002D78 RID: 11640
		private volatile bool _haveRequiresUniqueFilePathSuffix;

		// Token: 0x04002D79 RID: 11641
		private volatile bool _haveRequiresNoBreakInFormatting;

		// Token: 0x04002D7A RID: 11642
		private volatile bool _haveRequiresLeadingPageBreak;

		// Token: 0x04002D7B RID: 11643
		private volatile bool _haveSupportsSelectMultiple;

		// Token: 0x04002D7C RID: 11644
		private volatile bool _haveRequiresAttributeColonSubstitution;

		// Token: 0x04002D7D RID: 11645
		private volatile bool _haveRequiresHtmlAdaptiveErrorReporting;

		// Token: 0x04002D7E RID: 11646
		private volatile bool _haveRequiresContentTypeMetaTag;

		// Token: 0x04002D7F RID: 11647
		private volatile bool _haveRequiresDBCSCharacter;

		// Token: 0x04002D80 RID: 11648
		private volatile bool _haveRequiresOutputOptimization;

		// Token: 0x04002D81 RID: 11649
		private volatile bool _haveSupportsAccesskeyAttribute;

		// Token: 0x04002D82 RID: 11650
		private volatile bool _haveSupportsInputIStyle;

		// Token: 0x04002D83 RID: 11651
		private volatile bool _haveSupportsInputMode;

		// Token: 0x04002D84 RID: 11652
		private volatile bool _haveSupportsIModeSymbols;

		// Token: 0x04002D85 RID: 11653
		private volatile bool _haveSupportsJPhoneSymbols;

		// Token: 0x04002D86 RID: 11654
		private volatile bool _haveSupportsJPhoneMultiMediaAttributes;

		// Token: 0x04002D87 RID: 11655
		private volatile bool _haveSupportsRedirectWithCookie;

		// Token: 0x04002D88 RID: 11656
		private volatile bool _haveSupportsEmptyStringInCookieValue;

		// Token: 0x04002D89 RID: 11657
		private volatile bool _haveSupportsBold;

		// Token: 0x04002D8A RID: 11658
		private volatile bool _haveSupportsItalic;

		// Token: 0x04002D8B RID: 11659
		private volatile bool _haveSupportsFontSize;

		// Token: 0x04002D8C RID: 11660
		private volatile bool _haveSupportsFontName;

		// Token: 0x04002D8D RID: 11661
		private volatile bool _haveSupportsFontColor;

		// Token: 0x04002D8E RID: 11662
		private volatile bool _haveSupportsBodyColor;

		// Token: 0x04002D8F RID: 11663
		private volatile bool _haveSupportsDivAlign;

		// Token: 0x04002D90 RID: 11664
		private volatile bool _haveSupportsDivNoWrap;

		// Token: 0x04002D91 RID: 11665
		private volatile bool _haveMaximumRenderedPageSize;

		// Token: 0x04002D92 RID: 11666
		private volatile bool _haveRequiresSpecialViewStateEncoding;

		// Token: 0x04002D93 RID: 11667
		private volatile bool _haveSupportsQueryStringInFormAction;

		// Token: 0x04002D94 RID: 11668
		private volatile bool _haveSupportsCacheControlMetaTag;

		// Token: 0x04002D95 RID: 11669
		private volatile bool _haveSupportsUncheck;

		// Token: 0x04002D96 RID: 11670
		private volatile bool _haveCanRenderEmptySelects;

		// Token: 0x04002D97 RID: 11671
		private volatile bool _haveDefaultSubmitButtonLimit;

		// Token: 0x04002D98 RID: 11672
		private volatile bool _haveSupportsXmlHttp;

		// Token: 0x04002D99 RID: 11673
		private volatile bool _haveSupportsCallback;

		// Token: 0x04002D9A RID: 11674
		private volatile bool _haveSupportsMaintainScrollPositionOnPostback;

		// Token: 0x04002D9B RID: 11675
		private volatile bool _haveMaximumHrefLength;

		// Token: 0x04002D9C RID: 11676
		private volatile bool _havejscriptversion;
	}
}
