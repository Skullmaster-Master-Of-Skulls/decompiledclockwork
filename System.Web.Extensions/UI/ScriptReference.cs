using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.Handlers;
using System.Web.Resources;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000076 RID: 118
	[DefaultProperty("Path")]
	public class ScriptReference : ScriptReferenceBase
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x00011A08 File Offset: 0x0000FC08
		public ScriptReference()
		{
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000175E4 File Offset: 0x000157E4
		public ScriptReference(string name, string assembly) : this()
		{
			this.Name = name;
			this.Assembly = assembly;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000175FA File Offset: 0x000157FA
		public ScriptReference(string path) : this()
		{
			base.Path = path;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00017609 File Offset: 0x00015809
		internal ScriptReference(string name, IClientUrlResolver clientUrlResolver, Control containingControl) : this()
		{
			this.Name = name;
			base.ClientUrlResolver = clientUrlResolver;
			base.IsStaticReference = true;
			base.ContainingControl = containingControl;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001762D File Offset: 0x0001582D
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00017635 File Offset: 0x00015835
		internal bool IsDirectRegistration { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001763E File Offset: 0x0001583E
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00017654 File Offset: 0x00015854
		[Category("Behavior")]
		[DefaultValue("")]
		[ResourceDescription("ScriptReference_Assembly")]
		public string Assembly
		{
			get
			{
				if (this._assembly != null)
				{
					return this._assembly;
				}
				return string.Empty;
			}
			set
			{
				this._assembly = value;
				this._scriptInfo = null;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00017664 File Offset: 0x00015864
		internal Assembly EffectiveAssembly
		{
			get
			{
				return this.ScriptInfo.Assembly;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00017671 File Offset: 0x00015871
		internal string EffectivePath
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Path))
				{
					return base.Path;
				}
				return this.ScriptInfo.Path;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00017692 File Offset: 0x00015892
		internal string EffectiveResourceName
		{
			get
			{
				return this.ScriptInfo.ResourceName;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001769F File Offset: 0x0001589F
		internal ScriptMode EffectiveScriptMode
		{
			get
			{
				if (base.ScriptMode != ScriptMode.Auto)
				{
					return base.ScriptMode;
				}
				if (!string.IsNullOrEmpty(this.EffectiveResourceName) || (string.IsNullOrEmpty(base.Path) && !string.IsNullOrEmpty(this.ScriptInfo.DebugPath)))
				{
					return ScriptMode.Inherit;
				}
				return ScriptMode.Release;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000176DF File Offset: 0x000158DF
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000176E7 File Offset: 0x000158E7
		[Category("Behavior")]
		[DefaultValue(false)]
		[ResourceDescription("ScriptReference_IgnoreScriptPath")]
		[Obsolete("This property is obsolete. Instead of using ScriptManager.ScriptPath, set the Path property on each individual ScriptReference.")]
		public bool IgnoreScriptPath
		{
			get
			{
				return this._ignoreScriptPath;
			}
			set
			{
				this._ignoreScriptPath = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000176F0 File Offset: 0x000158F0
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00017706 File Offset: 0x00015906
		[Category("Behavior")]
		[DefaultValue("")]
		[ResourceDescription("ScriptReference_Name")]
		public string Name
		{
			get
			{
				if (this._name != null)
				{
					return this._name;
				}
				return string.Empty;
			}
			set
			{
				this._name = value;
				this._scriptInfo = null;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00017716 File Offset: 0x00015916
		internal ScriptReference.ScriptEffectiveInfo ScriptInfo
		{
			get
			{
				if (this._scriptInfo == null)
				{
					this._scriptInfo = new ScriptReference.ScriptEffectiveInfo(this);
				}
				return this._scriptInfo;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017734 File Offset: 0x00015934
		private string AddCultureName(ScriptManager scriptManager, string resourceName)
		{
			CultureInfo cultureInfo = scriptManager.EnableScriptLocalization ? this.DetermineCulture(scriptManager) : CultureInfo.InvariantCulture;
			if (!cultureInfo.Equals(CultureInfo.InvariantCulture))
			{
				return ScriptReference.AddCultureName(cultureInfo, resourceName);
			}
			return resourceName;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001776E File Offset: 0x0001596E
		private static string AddCultureName(CultureInfo culture, string resourceName)
		{
			if (resourceName.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
			{
				resourceName = resourceName.Substring(0, resourceName.Length - 2) + culture.Name + ".js";
			}
			return resourceName;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000177A0 File Offset: 0x000159A0
		internal bool DetermineResourceNameAndAssembly(ScriptManager scriptManager, bool isDebuggingEnabled, ref string resourceName, ref Assembly assembly)
		{
			if (assembly == scriptManager.AjaxFrameworkAssembly)
			{
				assembly = this.ApplyFallbackResource(assembly, resourceName);
			}
			bool flag = this.ShouldUseDebugScript(resourceName, assembly, isDebuggingEnabled, scriptManager.AjaxFrameworkAssembly);
			if (flag)
			{
				resourceName = ScriptReference.GetDebugName(resourceName);
			}
			return flag;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000177EC File Offset: 0x000159EC
		internal CultureInfo DetermineCulture(ScriptManager scriptManager)
		{
			if (base.ResourceUICultures != null && base.ResourceUICultures.Length != 0)
			{
				CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
				while (!cultureInfo.Equals(CultureInfo.InvariantCulture))
				{
					string a = cultureInfo.ToString();
					foreach (string text in base.ResourceUICultures)
					{
						if (string.Equals(a, text.Trim(), StringComparison.OrdinalIgnoreCase))
						{
							return cultureInfo;
						}
					}
					cultureInfo = cultureInfo.Parent;
				}
				return cultureInfo;
			}
			if (!string.IsNullOrEmpty(this.EffectiveResourceName))
			{
				return ScriptResourceHandler.DetermineNearestAvailableCulture(this.GetAssembly(scriptManager), this.EffectiveResourceName, CultureInfo.CurrentUICulture);
			}
			return CultureInfo.InvariantCulture;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00017885 File Offset: 0x00015A85
		internal Assembly GetAssembly()
		{
			if (!string.IsNullOrEmpty(this.Assembly))
			{
				return AssemblyCache.Load(this.Assembly);
			}
			return null;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000178A4 File Offset: 0x00015AA4
		internal Assembly GetAssembly(ScriptManager scriptManager)
		{
			Assembly effectiveAssembly = this.EffectiveAssembly;
			if (effectiveAssembly == null)
			{
				return scriptManager.AjaxFrameworkAssembly;
			}
			if (!(effectiveAssembly == AssemblyCache.SystemWebExtensions))
			{
				return effectiveAssembly;
			}
			return scriptManager.AjaxFrameworkAssembly;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000178DD File Offset: 0x00015ADD
		private static string GetDebugName(string releaseName)
		{
			if (!releaseName.EndsWith(".js", StringComparison.Ordinal))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.ScriptReference_InvalidReleaseScriptName, new object[]
				{
					releaseName
				}));
			}
			return ScriptReferenceBase.ReplaceExtension(releaseName);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00017914 File Offset: 0x00015B14
		internal string GetPath(ScriptManager scriptManager, string releasePath, string predeterminedDebugPath, bool isDebuggingEnabled)
		{
			if (!string.IsNullOrEmpty(this.EffectiveResourceName))
			{
				Assembly assembly = this.GetAssembly(scriptManager);
				string effectiveResourceName = this.EffectiveResourceName;
				isDebuggingEnabled = this.DetermineResourceNameAndAssembly(scriptManager, isDebuggingEnabled, ref effectiveResourceName, ref assembly);
			}
			string resourceName;
			if (isDebuggingEnabled)
			{
				resourceName = (string.IsNullOrEmpty(predeterminedDebugPath) ? ScriptReferenceBase.GetDebugPath(releasePath) : predeterminedDebugPath);
			}
			else
			{
				resourceName = releasePath;
			}
			return this.AddCultureName(scriptManager, resourceName);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001796E File Offset: 0x00015B6E
		internal Assembly ApplyFallbackResource(Assembly assembly, string releaseName)
		{
			if (assembly != AssemblyCache.SystemWebExtensions && !WebResourceUtil.AssemblyContainsWebResource(assembly, releaseName))
			{
				assembly = AssemblyCache.SystemWebExtensions;
			}
			return assembly;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001798E File Offset: 0x00015B8E
		internal static string GetScriptPath(string resourceName, Assembly assembly, CultureInfo culture, string scriptPath)
		{
			return scriptPath + "/" + ScriptReference.GetScriptPathCached(resourceName, assembly, culture);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000179A4 File Offset: 0x00015BA4
		private static string GetScriptPathCached(string resourceName, Assembly assembly, CultureInfo culture)
		{
			Tuple<string, Assembly, CultureInfo> key = Tuple.Create<string, Assembly, CultureInfo>(resourceName, assembly, culture);
			string text = (string)ScriptReference._scriptPathCache[key];
			if (text == null)
			{
				AssemblyName assemblyName = new AssemblyName(assembly.FullName);
				string name = assemblyName.Name;
				string text2 = assemblyName.Version.ToString();
				string assemblyFileVersion = AssemblyUtil.GetAssemblyFileVersion(assembly);
				if (!culture.Equals(CultureInfo.InvariantCulture))
				{
					resourceName = ScriptReference.AddCultureName(culture, resourceName);
				}
				text = string.Join("/", new string[]
				{
					HttpUtility.UrlEncode(name),
					text2,
					HttpUtility.UrlEncode(assemblyFileVersion),
					HttpUtility.UrlEncode(resourceName)
				});
				ScriptReference._scriptPathCache[key] = text;
			}
			return text;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017A4C File Offset: 0x00015C4C
		protected internal override string GetUrl(ScriptManager scriptManager, bool zip)
		{
			bool flag = !string.IsNullOrEmpty(this.Name);
			bool flag2 = !string.IsNullOrEmpty(this.Assembly);
			if (!flag && string.IsNullOrEmpty(base.Path))
			{
				throw new InvalidOperationException(AtlasWeb.ScriptReference_NameAndPathCannotBeEmpty);
			}
			if (flag2 && !flag)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptReference_AssemblyRequiresName);
			}
			return this.GetUrlInternal(scriptManager, zip);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017AAC File Offset: 0x00015CAC
		internal string GetUrlInternal(ScriptManager scriptManager, bool zip)
		{
			bool useCdnPath = scriptManager != null && scriptManager.EnableCdn;
			return this.GetUrlInternal(scriptManager, zip, useCdnPath);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017AD0 File Offset: 0x00015CD0
		internal string GetUrlInternal(ScriptManager scriptManager, bool zip, bool useCdnPath)
		{
			if (!string.IsNullOrEmpty(this.EffectiveResourceName) && !this.IsAjaxFrameworkScript(scriptManager) && AssemblyCache.IsAjaxFrameworkAssembly(this.GetAssembly(scriptManager)))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.ScriptReference_ResourceRequiresAjaxAssembly, new object[]
				{
					this.EffectiveResourceName,
					this.GetAssembly(scriptManager)
				}));
			}
			if (!string.IsNullOrEmpty(base.Path))
			{
				return this.GetUrlFromPath(scriptManager, base.Path, null);
			}
			if (!string.IsNullOrEmpty(this.ScriptInfo.Path))
			{
				if (useCdnPath)
				{
					string effectiveResourceName = this.EffectiveResourceName;
					Assembly assembly = null;
					bool hasDebugResource = false;
					if (!string.IsNullOrEmpty(effectiveResourceName))
					{
						assembly = this.GetAssembly(scriptManager);
						hasDebugResource = this.DetermineResourceNameAndAssembly(scriptManager, this.IsDebuggingEnabled(scriptManager), ref effectiveResourceName, ref assembly);
					}
					string urlForCdn = this.GetUrlForCdn(scriptManager, effectiveResourceName, assembly, hasDebugResource);
					if (!string.IsNullOrEmpty(urlForCdn))
					{
						return urlForCdn;
					}
				}
				return this.GetUrlFromPath(scriptManager, this.ScriptInfo.Path, this.ScriptInfo.DebugPath);
			}
			return this.GetUrlFromName(scriptManager, scriptManager.Control, zip, useCdnPath);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017BD0 File Offset: 0x00015DD0
		private string GetUrlForCdn(ScriptManager scriptManager, string resourceName, Assembly assembly, bool hasDebugResource)
		{
			bool flag = this.IsDebuggingEnabled(scriptManager);
			bool flag2 = !string.IsNullOrEmpty(resourceName);
			bool isSecureConnection = scriptManager.IsSecureConnection;
			flag = (flag && (hasDebugResource || !flag2));
			string text = flag ? (isSecureConnection ? this.ScriptInfo.CdnDebugPathSecureConnection : this.ScriptInfo.CdnDebugPath) : (isSecureConnection ? this.ScriptInfo.CdnPathSecureConnection : this.ScriptInfo.CdnPath);
			if (flag2 && string.IsNullOrEmpty(text) && string.IsNullOrEmpty(flag ? this.ScriptInfo.CdnDebugPath : this.ScriptInfo.CdnPath))
			{
				ScriptResourceInfo instance = ScriptResourceInfo.GetInstance(assembly, resourceName);
				if (instance != null)
				{
					text = (isSecureConnection ? instance.CdnPathSecureConnection : instance.CdnPath);
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return base.ClientUrlResolver.ResolveClientUrl(this.AddCultureName(scriptManager, text));
			}
			return null;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00017CB0 File Offset: 0x00015EB0
		private string GetUrlFromName(ScriptManager scriptManager, IControl scriptManagerControl, bool zip, bool useCdnPath)
		{
			string effectiveResourceName = this.EffectiveResourceName;
			Assembly assembly = this.GetAssembly(scriptManager);
			bool hasDebugResource = this.DetermineResourceNameAndAssembly(scriptManager, this.IsDebuggingEnabled(scriptManager), ref effectiveResourceName, ref assembly);
			if (useCdnPath)
			{
				string urlForCdn = this.GetUrlForCdn(scriptManager, effectiveResourceName, assembly, hasDebugResource);
				if (!string.IsNullOrEmpty(urlForCdn))
				{
					return urlForCdn;
				}
			}
			CultureInfo culture = scriptManager.EnableScriptLocalization ? this.DetermineCulture(scriptManager) : CultureInfo.InvariantCulture;
			if (this.IgnoreScriptPath || string.IsNullOrEmpty(scriptManager.ScriptPath))
			{
				return ScriptResourceHandler.GetScriptResourceUrl(assembly, effectiveResourceName, culture, zip);
			}
			string scriptPath = ScriptReference.GetScriptPath(effectiveResourceName, assembly, culture, scriptManager.ScriptPath);
			if (base.IsBundleReference)
			{
				return scriptManager.BundleReflectionHelper.GetBundleUrl(scriptPath);
			}
			return scriptManagerControl.ResolveClientUrl(scriptPath);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017D60 File Offset: 0x00015F60
		private string GetUrlFromPath(ScriptManager scriptManager, string releasePath, string predeterminedDebugPath)
		{
			string path = this.GetPath(scriptManager, releasePath, predeterminedDebugPath, this.IsDebuggingEnabled(scriptManager));
			if (base.IsBundleReference)
			{
				return scriptManager.BundleReflectionHelper.GetBundleUrl(path);
			}
			return base.ClientUrlResolver.ResolveClientUrl(path);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017DA0 File Offset: 0x00015FA0
		private bool IsDebuggingEnabled(ScriptManager scriptManager)
		{
			if (this.IsDirectRegistration || scriptManager.DeploymentSectionRetail)
			{
				return false;
			}
			switch (this.EffectiveScriptMode)
			{
			case ScriptMode.Inherit:
				return scriptManager.IsDebuggingEnabled;
			case ScriptMode.Debug:
				return true;
			case ScriptMode.Release:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017DE8 File Offset: 0x00015FE8
		protected internal override bool IsAjaxFrameworkScript(ScriptManager scriptManager)
		{
			return this.GetAssembly(scriptManager) == scriptManager.AjaxFrameworkAssembly;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00017DFC File Offset: 0x00015FFC
		[Obsolete("This method is obsolete. Use IsAjaxFrameworkScript(ScriptManager) instead.")]
		protected internal override bool IsFromSystemWebExtensions()
		{
			return this.EffectiveAssembly == AssemblyCache.SystemWebExtensions;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017E0E File Offset: 0x0001600E
		internal bool IsFromSystemWeb()
		{
			return this.EffectiveAssembly == AssemblyCache.SystemWeb;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017E20 File Offset: 0x00016020
		internal bool ShouldUseDebugScript(string releaseName, Assembly assembly, bool isDebuggingEnabled, Assembly currentAjaxAssembly)
		{
			string text = null;
			bool flag;
			if (isDebuggingEnabled)
			{
				text = ScriptReference.GetDebugName(releaseName);
				flag = (base.ScriptMode != ScriptMode.Auto || WebResourceUtil.AssemblyContainsWebResource(assembly, text));
			}
			else
			{
				flag = false;
			}
			if (!this.IsDirectRegistration)
			{
				WebResourceUtil.VerifyAssemblyContainsReleaseWebResource(assembly, releaseName, currentAjaxAssembly);
				if (flag)
				{
					WebResourceUtil.VerifyAssemblyContainsDebugWebResource(assembly, text);
				}
			}
			return flag;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017E70 File Offset: 0x00016070
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			if (!string.IsNullOrEmpty(base.Path))
			{
				return base.Path;
			}
			return base.GetType().Name;
		}

		// Token: 0x040001C9 RID: 457
		private static readonly Hashtable _scriptPathCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040001CA RID: 458
		private string _assembly;

		// Token: 0x040001CB RID: 459
		private bool _ignoreScriptPath;

		// Token: 0x040001CC RID: 460
		private string _name;

		// Token: 0x040001CD RID: 461
		private ScriptReference.ScriptEffectiveInfo _scriptInfo;

		// Token: 0x02000165 RID: 357
		internal class ScriptEffectiveInfo
		{
			// Token: 0x06001012 RID: 4114 RVA: 0x00037810 File Offset: 0x00035A10
			public ScriptEffectiveInfo(ScriptReference scriptReference)
			{
				ScriptResourceDefinition definition = ScriptManager.ScriptResourceMapping.GetDefinition(scriptReference);
				string text = scriptReference.Name;
				string path = scriptReference.Path;
				Assembly assembly = scriptReference.GetAssembly();
				if (definition != null)
				{
					if (string.IsNullOrEmpty(path))
					{
						path = definition.Path;
						this._debugPath = definition.DebugPath;
					}
					text = definition.ResourceName;
					assembly = definition.ResourceAssembly;
					this._cdnPath = definition.CdnPath;
					this._cdnDebugPath = definition.CdnDebugPath;
					this._cdnPathSecureConnection = definition.CdnPathSecureConnection;
					this._cdnDebugPathSecureConnection = definition.CdnDebugPathSecureConnection;
					this.LoadSuccessExpression = definition.LoadSuccessExpression;
				}
				else if (assembly == null && !string.IsNullOrEmpty(text))
				{
					assembly = AssemblyCache.SystemWebExtensions;
				}
				this._resourceName = text;
				this._assembly = assembly;
				this._path = path;
				if (assembly != null && !string.IsNullOrEmpty(text) && string.IsNullOrEmpty(this.LoadSuccessExpression))
				{
					ScriptResourceInfo instance = ScriptResourceInfo.GetInstance(assembly, text);
					if (instance != null)
					{
						this.LoadSuccessExpression = instance.LoadSuccessExpression;
					}
				}
			}

			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x06001013 RID: 4115 RVA: 0x00037912 File Offset: 0x00035B12
			public Assembly Assembly
			{
				get
				{
					return this._assembly;
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003791A File Offset: 0x00035B1A
			public string CdnDebugPath
			{
				get
				{
					return this._cdnDebugPath;
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06001015 RID: 4117 RVA: 0x00037922 File Offset: 0x00035B22
			public string CdnPath
			{
				get
				{
					return this._cdnPath;
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x06001016 RID: 4118 RVA: 0x0003792A File Offset: 0x00035B2A
			public string CdnDebugPathSecureConnection
			{
				get
				{
					return this._cdnDebugPathSecureConnection;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x06001017 RID: 4119 RVA: 0x00037932 File Offset: 0x00035B32
			public string CdnPathSecureConnection
			{
				get
				{
					return this._cdnPathSecureConnection;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003793A File Offset: 0x00035B3A
			// (set) Token: 0x06001019 RID: 4121 RVA: 0x00037942 File Offset: 0x00035B42
			public string LoadSuccessExpression { get; private set; }

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003794B File Offset: 0x00035B4B
			public string DebugPath
			{
				get
				{
					return this._debugPath;
				}
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x0600101B RID: 4123 RVA: 0x00037953 File Offset: 0x00035B53
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003795B File Offset: 0x00035B5B
			public string ResourceName
			{
				get
				{
					return this._resourceName;
				}
			}

			// Token: 0x040004DF RID: 1247
			private string _resourceName;

			// Token: 0x040004E0 RID: 1248
			private Assembly _assembly;

			// Token: 0x040004E1 RID: 1249
			private string _path;

			// Token: 0x040004E2 RID: 1250
			private string _debugPath;

			// Token: 0x040004E3 RID: 1251
			private string _cdnPath;

			// Token: 0x040004E4 RID: 1252
			private string _cdnDebugPath;

			// Token: 0x040004E5 RID: 1253
			private string _cdnPathSecureConnection;

			// Token: 0x040004E6 RID: 1254
			private string _cdnDebugPathSecureConnection;
		}
	}
}
