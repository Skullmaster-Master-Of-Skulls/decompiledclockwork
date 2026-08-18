using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Globalization;
using System.Web.Handlers;
using System.Web.Hosting;
using System.Web.Resources;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Security.Cryptography;
using System.Web.UI.Design;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000073 RID: 115
	[DefaultProperty("Scripts")]
	[Designer("System.Web.UI.Design.ScriptManagerDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[NonVisualControl]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(EmbeddedResourceFinder), "System.Web.Resources.ScriptManager.bmp")]
	public class ScriptManager : Control, IPostBackDataHandler, IPostBackEventHandler, IControl, IClientUrlResolver, IScriptManager, IScriptManagerInternal
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00014DAD File Offset: 0x00012FAD
		static ScriptManager()
		{
			ScriptManager.AsyncPostBackErrorEvent = new object();
			ScriptManager.ResolveCompositeScriptReferenceEvent = new object();
			ScriptManager.ResolveScriptReferenceEvent = new object();
			ScriptManager.NavigateEvent = new object();
			ClientScriptManager._scriptResourceMapping = new ScriptResourceMapping();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public ScriptManager()
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00014E50 File Offset: 0x00013050
		internal ScriptManager(IControl control, IPage page, ICompilationSection appLevelCompilationSection, IDeploymentSection deploymentSection, ICustomErrorsSection customErrorsSection, Assembly ajaxFrameworkAssembly, bool isSecureConnection)
		{
			this._control = control;
			this._page = page;
			this._appLevelCompilationSection = appLevelCompilationSection;
			this._deploymentSection = deploymentSection;
			this._customErrorsSection = customErrorsSection;
			this._ajaxFrameworkAssembly = (ajaxFrameworkAssembly ?? ScriptManager.DefaultAjaxFrameworkAssembly);
			this._isSecureConnection = new bool?(isSecureConnection);
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00014EFA File Offset: 0x000130FA
		[ResourceDescription("ScriptManager_AjaxFrameworkAssembly")]
		[Browsable(false)]
		public virtual Assembly AjaxFrameworkAssembly
		{
			get
			{
				return this._ajaxFrameworkAssembly;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00014F02 File Offset: 0x00013102
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00014F0A File Offset: 0x0001310A
		[DefaultValue(true)]
		[ResourceDescription("ScriptManager_AllowCustomErrorsRedirect")]
		[Category("Behavior")]
		public bool AllowCustomErrorsRedirect
		{
			get
			{
				return this._allowCustomErrorsRedirect;
			}
			set
			{
				this._allowCustomErrorsRedirect = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00014F13 File Offset: 0x00013113
		private ICompilationSection AppLevelCompilationSection
		{
			get
			{
				if (this._appLevelCompilationSection != null)
				{
					return this._appLevelCompilationSection;
				}
				return AppLevelCompilationSectionCache.Instance;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00014F29 File Offset: 0x00013129
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00014F3F File Offset: 0x0001313F
		[DefaultValue("")]
		[ResourceDescription("ScriptManager_AsyncPostBackErrorMessage")]
		[Category("Behavior")]
		public string AsyncPostBackErrorMessage
		{
			get
			{
				if (this._asyncPostBackErrorMessage == null)
				{
					return string.Empty;
				}
				return this._asyncPostBackErrorMessage;
			}
			set
			{
				this._asyncPostBackErrorMessage = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00014F48 File Offset: 0x00013148
		[Browsable(false)]
		public string AsyncPostBackSourceElementID
		{
			get
			{
				return this.PageRequestManager.AsyncPostBackSourceElementID;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00014F55 File Offset: 0x00013155
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x00014F5D File Offset: 0x0001315D
		[ResourceDescription("ScriptManager_AsyncPostBackTimeout")]
		[Category("Behavior")]
		[DefaultValue(90)]
		public int AsyncPostBackTimeout
		{
			get
			{
				return this._asyncPostBackTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._asyncPostBackTimeout = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00014F75 File Offset: 0x00013175
		[ResourceDescription("ScriptManager_AuthenticationService")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public AuthenticationServiceManager AuthenticationService
		{
			get
			{
				if (this._authenticationServiceManager == null)
				{
					this._authenticationServiceManager = new AuthenticationServiceManager();
				}
				return this._authenticationServiceManager;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00014F90 File Offset: 0x00013190
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00014FAB File Offset: 0x000131AB
		internal BundleReflectionHelper BundleReflectionHelper
		{
			get
			{
				if (this._bundleReflectionHelper == null)
				{
					this._bundleReflectionHelper = new BundleReflectionHelper();
				}
				return this._bundleReflectionHelper;
			}
			set
			{
				this._bundleReflectionHelper = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00014FB4 File Offset: 0x000131B4
		public static ScriptResourceMapping ScriptResourceMapping
		{
			get
			{
				return (ScriptResourceMapping)ClientScriptManager._scriptResourceMapping;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00014FC0 File Offset: 0x000131C0
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00014FD1 File Offset: 0x000131D1
		[ResourceDescription("ScriptManager_ClientNavigateHandler")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string ClientNavigateHandler
		{
			get
			{
				return this._clientNavigateHandler ?? string.Empty;
			}
			set
			{
				this._clientNavigateHandler = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00014FDA File Offset: 0x000131DA
		[ResourceDescription("ScriptManager_CompositeScript")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public CompositeScriptReference CompositeScript
		{
			get
			{
				if (this._compositeScript == null)
				{
					this._compositeScript = new CompositeScriptReference();
				}
				return this._compositeScript;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00014FF5 File Offset: 0x000131F5
		internal IControl Control
		{
			get
			{
				if (this._control != null)
				{
					return this._control;
				}
				return this;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00015007 File Offset: 0x00013207
		internal ICustomErrorsSection CustomErrorsSection
		{
			[SecurityCritical]
			get
			{
				if (this._customErrorsSection != null)
				{
					return this._customErrorsSection;
				}
				return ScriptManager.GetCustomErrorsSectionWithAssert();
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00015020 File Offset: 0x00013220
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00015118 File Offset: 0x00013318
		internal static Assembly DefaultAjaxFrameworkAssembly
		{
			get
			{
				if (ScriptManager._defaultAjaxFrameworkAssembly == null && !ScriptManager._ajaxFrameworkAssemblyConfigChecked && AssemblyCache._useCompilationSection)
				{
					IEnumerable<Assembly> enumerable;
					if (HostingEnvironment.IsHosted)
					{
						enumerable = BuildManager.GetReferencedAssemblies().OfType<Assembly>();
					}
					else
					{
						CompilationSection compilation = RuntimeConfig.GetAppConfig().Compilation;
						enumerable = compilation.Assemblies.OfType<AssemblyInfo>().SelectMany((AssemblyInfo assemblyInfo) => assemblyInfo.AssemblyInternal);
					}
					foreach (Assembly assembly in enumerable)
					{
						if (assembly != AssemblyCache.SystemWebExtensions)
						{
							AjaxFrameworkAssemblyAttribute ajaxFrameworkAssemblyAttribute = AssemblyCache.GetAjaxFrameworkAssemblyAttribute(assembly);
							if (ajaxFrameworkAssemblyAttribute != null)
							{
								ScriptManager._defaultAjaxFrameworkAssembly = ajaxFrameworkAssemblyAttribute.GetDefaultAjaxFrameworkAssembly(assembly);
								break;
							}
						}
						ScriptManager._ajaxFrameworkAssemblyConfigChecked = true;
					}
					ScriptManager._ajaxFrameworkAssemblyConfigChecked = true;
				}
				return ScriptManager._defaultAjaxFrameworkAssembly ?? AssemblyCache.SystemWebExtensions;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScriptManager._defaultAjaxFrameworkAssembly = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00015134 File Offset: 0x00013334
		private IDeploymentSection DeploymentSection
		{
			get
			{
				if (this._deploymentSection != null)
				{
					return this._deploymentSection;
				}
				return DeploymentSectionCache.Instance;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001514A File Offset: 0x0001334A
		internal bool DeploymentSectionRetail
		{
			get
			{
				return this.DeploymentSection.Retail;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00015157 File Offset: 0x00013357
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00015177 File Offset: 0x00013377
		[ResourceDescription("ScriptManager_EmptyPageUrl")]
		[Category("Appearance")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string EmptyPageUrl
		{
			get
			{
				return (this.ViewState["EmptyPageUrl"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["EmptyPageUrl"] = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001518A File Offset: 0x0001338A
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00015192 File Offset: 0x00013392
		[ResourceDescription("ScriptManager_EnableCdn")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnableCdn
		{
			get
			{
				return this._enableCdn;
			}
			set
			{
				if (this._preRenderCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeEnableCdn);
				}
				this._enableCdn = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000151AE File Offset: 0x000133AE
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000151B6 File Offset: 0x000133B6
		[ResourceDescription("ScriptManager_EnableCdnFallback")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableCdnFallback
		{
			get
			{
				return this._enableCdnFallback;
			}
			set
			{
				if (this._preRenderCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeEnableCdnFallback);
				}
				this._enableCdnFallback = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000151D2 File Offset: 0x000133D2
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000151DA File Offset: 0x000133DA
		[ResourceDescription("ScriptManager_EnableHistory")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnableHistory
		{
			get
			{
				return this._enableHistory;
			}
			set
			{
				if (this._initCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeEnableHistory);
				}
				this._enableHistory = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000151F6 File Offset: 0x000133F6
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000151FE File Offset: 0x000133FE
		[ResourceDescription("ScriptManager_AjaxFrameworkMode")]
		[Category("Behavior")]
		[DefaultValue(AjaxFrameworkMode.Enabled)]
		public AjaxFrameworkMode AjaxFrameworkMode
		{
			get
			{
				return this._ajaxFrameworkMode;
			}
			set
			{
				if (value < AjaxFrameworkMode.Enabled || value > AjaxFrameworkMode.Explicit)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this._initCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeAjaxFrameworkMode);
				}
				this._ajaxFrameworkMode = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001522D File Offset: 0x0001342D
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00015235 File Offset: 0x00013435
		[ResourceDescription("ScriptManager_EnablePageMethods")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnablePageMethods
		{
			get
			{
				return this._enablePageMethods;
			}
			set
			{
				this._enablePageMethods = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001523E File Offset: 0x0001343E
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00015246 File Offset: 0x00013446
		[ResourceDescription("ScriptManager_EnablePartialRendering")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnablePartialRendering
		{
			get
			{
				return this._enablePartialRendering;
			}
			set
			{
				if (this._initCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeEnablePartialRendering);
				}
				this._enablePartialRendering = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00015262 File Offset: 0x00013462
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001526A File Offset: 0x0001346A
		[ResourceDescription("ScriptManager_EnableScriptGlobalization")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnableScriptGlobalization
		{
			get
			{
				return this._enableScriptGlobalization;
			}
			set
			{
				if (this._initCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeEnableScriptGlobalization);
				}
				this._enableScriptGlobalization = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00015286 File Offset: 0x00013486
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001528E File Offset: 0x0001348E
		[ResourceDescription("ScriptManager_EnableScriptLocalization")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableScriptLocalization
		{
			get
			{
				return this._enableScriptLocalization;
			}
			set
			{
				this._enableScriptLocalization = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015297 File Offset: 0x00013497
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0001529F File Offset: 0x0001349F
		[ResourceDescription("ScriptManager_EnableSecureHistoryState")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableSecureHistoryState
		{
			get
			{
				return this._enableSecureHistoryState;
			}
			set
			{
				this._enableSecureHistoryState = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000152A8 File Offset: 0x000134A8
		internal bool HasAuthenticationServiceManager
		{
			get
			{
				return this._authenticationServiceManager != null;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000152B3 File Offset: 0x000134B3
		internal bool HasProfileServiceManager
		{
			get
			{
				return this._profileServiceManager != null;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000152BE File Offset: 0x000134BE
		internal bool HasRoleServiceManager
		{
			get
			{
				return this._roleServiceManager != null;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000152C9 File Offset: 0x000134C9
		[Browsable(false)]
		public bool IsDebuggingEnabled
		{
			get
			{
				if (this.DeploymentSectionRetail)
				{
					return false;
				}
				if (this.ScriptMode == ScriptMode.Auto || this.ScriptMode == ScriptMode.Inherit)
				{
					return this.AppLevelCompilationSection.Debug;
				}
				return this.ScriptMode == ScriptMode.Debug;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000152FB File Offset: 0x000134FB
		[Browsable(false)]
		public bool IsInAsyncPostBack
		{
			get
			{
				return this._isInAsyncPostBack;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00015303 File Offset: 0x00013503
		[Browsable(false)]
		public bool IsNavigating
		{
			get
			{
				return this._isNavigating;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001530B File Offset: 0x0001350B
		internal bool IsRestMethodCall
		{
			get
			{
				if (this._isRestMethodCall == null)
				{
					this._isRestMethodCall = new bool?(this.Context != null && RestHandlerFactory.IsRestMethodCall(this.Context.Request));
				}
				return this._isRestMethodCall.Value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001534C File Offset: 0x0001354C
		internal bool IsSecureConnection
		{
			get
			{
				if (this._isSecureConnection == null)
				{
					this._isSecureConnection = new bool?(this.Context != null && this.Context.Request != null && this.Context.Request.IsSecureConnection);
				}
				return this._isSecureConnection.Value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000153A4 File Offset: 0x000135A4
		internal IPage IPage
		{
			get
			{
				if (this._page != null)
				{
					return this._page;
				}
				Page page = this.Page;
				if (page == null)
				{
					throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
				}
				return new PageWrapper(page);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000153DB File Offset: 0x000135DB
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000153E3 File Offset: 0x000135E3
		[ResourceDescription("ScriptManager_LoadScriptsBeforeUI")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool LoadScriptsBeforeUI
		{
			get
			{
				return this._loadScriptsBeforeUI;
			}
			set
			{
				this._loadScriptsBeforeUI = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000153EC File Offset: 0x000135EC
		private PageRequestManager PageRequestManager
		{
			get
			{
				if (this._pageRequestManager == null)
				{
					this._pageRequestManager = new PageRequestManager(this);
				}
				return this._pageRequestManager;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00015408 File Offset: 0x00013608
		[ResourceDescription("ScriptManager_ProfileService")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ProfileServiceManager ProfileService
		{
			get
			{
				if (this._profileServiceManager == null)
				{
					this._profileServiceManager = new ProfileServiceManager();
				}
				return this._profileServiceManager;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00015423 File Offset: 0x00013623
		internal List<ScriptManagerProxy> Proxies
		{
			get
			{
				if (this._proxies == null)
				{
					this._proxies = new List<ScriptManagerProxy>();
				}
				return this._proxies;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001543E File Offset: 0x0001363E
		[ResourceDescription("ScriptManager_RoleService")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RoleServiceManager RoleService
		{
			get
			{
				if (this._roleServiceManager == null)
				{
					this._roleServiceManager = new RoleServiceManager();
				}
				return this._roleServiceManager;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00015459 File Offset: 0x00013659
		internal ScriptControlManager ScriptControlManager
		{
			get
			{
				if (this._scriptControlManager == null)
				{
					this._scriptControlManager = new ScriptControlManager(this);
				}
				return this._scriptControlManager;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00015475 File Offset: 0x00013675
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0001547D File Offset: 0x0001367D
		[ResourceDescription("ScriptManager_ScriptMode")]
		[Category("Behavior")]
		[DefaultValue(ScriptMode.Auto)]
		public ScriptMode ScriptMode
		{
			get
			{
				return this._scriptMode;
			}
			set
			{
				if (value < ScriptMode.Auto || value > ScriptMode.Release)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._scriptMode = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00015499 File Offset: 0x00013699
		internal ScriptRegistrationManager ScriptRegistration
		{
			get
			{
				if (this._scriptRegistration == null)
				{
					this._scriptRegistration = new ScriptRegistrationManager(this);
				}
				return this._scriptRegistration;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000154B5 File Offset: 0x000136B5
		[ResourceDescription("ScriptManager_Scripts")]
		[Category("Behavior")]
		[Editor("System.Web.UI.Design.CollectionEditorBase, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ScriptReferenceCollection Scripts
		{
			get
			{
				if (this._scripts == null)
				{
					this._scripts = new ScriptReferenceCollection();
				}
				return this._scripts;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000154D0 File Offset: 0x000136D0
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000154E6 File Offset: 0x000136E6
		[ResourceDescription("ScriptManager_ScriptPath")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Obsolete("This property is obsolete. Set the Path property on each individual ScriptReference instead.")]
		public string ScriptPath
		{
			get
			{
				if (this._scriptPath != null)
				{
					return this._scriptPath;
				}
				return string.Empty;
			}
			set
			{
				this._scriptPath = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000154EF File Offset: 0x000136EF
		[ResourceDescription("ScriptManager_Services")]
		[Category("Behavior")]
		[Editor("System.Web.UI.Design.ServiceReferenceCollectionEditor, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ServiceReferenceCollection Services
		{
			get
			{
				if (this._services == null)
				{
					this._services = new ServiceReferenceCollection();
				}
				return this._services;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001550C File Offset: 0x0001370C
		private static HashSet<string> SplitFrameworkScripts
		{
			get
			{
				if (ScriptManager._splitFrameworkScript == null)
				{
					ScriptManager._splitFrameworkScript = new HashSet<string>
					{
						"MicrosoftAjaxComponentModel.js",
						"MicrosoftAjaxComponentModel.debug.js",
						"MicrosoftAjaxCore.js",
						"MicrosoftAjaxCore.debug.js",
						"MicrosoftAjaxGlobalization.js",
						"MicrosoftAjaxGlobalization.debug.js",
						"MicrosoftAjaxHistory.js",
						"MicrosoftAjaxHistory.debug.js",
						"MicrosoftAjaxNetwork.js",
						"MicrosoftAjaxNetwork.debug.js",
						"MicrosoftAjaxSerialization.js",
						"MicrosoftAjaxSerialization.debug.js",
						"MicrosoftAjaxWebServices.js",
						"MicrosoftAjaxWebServices.debug.js"
					};
				}
				return ScriptManager._splitFrameworkScript;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000155DC File Offset: 0x000137DC
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000155EE File Offset: 0x000137EE
		[Browsable(false)]
		[DefaultValue(true)]
		public bool SupportsPartialRendering
		{
			get
			{
				return this.EnablePartialRendering && this._supportsPartialRendering;
			}
			set
			{
				if (!this.EnablePartialRendering)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotSetSupportsPartialRenderingWhenDisabled);
				}
				if (this._initCompleted)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotChangeSupportsPartialRendering);
				}
				this._supportsPartialRendering = value;
				this._supportsPartialRenderingSetByUser = true;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00011F1F File Offset: 0x0001011F
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00002058 File Offset: 0x00000258
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015624 File Offset: 0x00013824
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00015670 File Offset: 0x00013870
		internal bool Zip
		{
			get
			{
				if (!this._zipSet)
				{
					this._zip = HeaderUtility.IsEncodingInAcceptList(this.IPage.Request.Headers["Accept-encoding"], "gzip");
					this._zipSet = true;
				}
				return this._zip;
			}
			set
			{
				this._zip = value;
				this._zipSet = true;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000457 RID: 1111 RVA: 0x00015680 File Offset: 0x00013880
		// (remove) Token: 0x06000458 RID: 1112 RVA: 0x00015693 File Offset: 0x00013893
		[Category("Action")]
		[ResourceDescription("ScriptManager_AsyncPostBackError")]
		public event EventHandler<AsyncPostBackErrorEventArgs> AsyncPostBackError
		{
			add
			{
				base.Events.AddHandler(ScriptManager.AsyncPostBackErrorEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScriptManager.AsyncPostBackErrorEvent, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000459 RID: 1113 RVA: 0x000156A6 File Offset: 0x000138A6
		// (remove) Token: 0x0600045A RID: 1114 RVA: 0x000156B9 File Offset: 0x000138B9
		[Category("Action")]
		[ResourceDescription("ScriptManager_Navigate")]
		public event EventHandler<HistoryEventArgs> Navigate
		{
			add
			{
				base.Events.AddHandler(ScriptManager.NavigateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScriptManager.NavigateEvent, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600045B RID: 1115 RVA: 0x000156CC File Offset: 0x000138CC
		// (remove) Token: 0x0600045C RID: 1116 RVA: 0x000156DF File Offset: 0x000138DF
		[Category("Action")]
		[ResourceDescription("ScriptManager_ResolveCompositeScriptReference")]
		public event EventHandler<CompositeScriptReferenceEventArgs> ResolveCompositeScriptReference
		{
			add
			{
				base.Events.AddHandler(ScriptManager.ResolveCompositeScriptReferenceEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScriptManager.ResolveCompositeScriptReferenceEvent, value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600045D RID: 1117 RVA: 0x000156F2 File Offset: 0x000138F2
		// (remove) Token: 0x0600045E RID: 1118 RVA: 0x00015705 File Offset: 0x00013905
		[Category("Action")]
		[ResourceDescription("ScriptManager_ResolveScriptReference")]
		public event EventHandler<ScriptReferenceEventArgs> ResolveScriptReference
		{
			add
			{
				base.Events.AddHandler(ScriptManager.ResolveScriptReferenceEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScriptManager.ResolveScriptReferenceEvent, value);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015718 File Offset: 0x00013918
		public void AddHistoryPoint(string key, string value)
		{
			this.AddHistoryPoint(key, value, null);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015723 File Offset: 0x00013923
		public void AddHistoryPoint(string key, string value, string title)
		{
			this.PrepareNewHistoryPoint();
			this.SetStateValue(key, value);
			this.SetPageTitle(title);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001573C File Offset: 0x0001393C
		public void AddHistoryPoint(NameValueCollection state, string title)
		{
			this.PrepareNewHistoryPoint();
			foreach (object obj in state)
			{
				string text = (string)obj;
				this.SetStateValue(text, state[text]);
			}
			this.SetPageTitle(title);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000157A4 File Offset: 0x000139A4
		private void AddFrameworkLoadedCheck()
		{
			this.IPage.ClientScript.RegisterClientScriptBlock(typeof(ScriptManager), "FrameworkLoadedCheck", "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\nif (typeof(Sys) === 'undefined') throw new Error('" + HttpUtility.JavaScriptStringEncode(AtlasWeb.ScriptManager_FrameworkFailedToLoad) + "');\r\n//]]>\r\n</script>\r\n", false);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000157E0 File Offset: 0x000139E0
		private ScriptReferenceBase AddFrameworkScript(ScriptReference frameworkScript, List<ScriptReferenceBase> scripts, bool webFormsWithoutAjax)
		{
			int index = 0;
			ScriptReferenceBase scriptReferenceBase = frameworkScript;
			if (scripts.Count != 0)
			{
				string effectiveResourceName = frameworkScript.EffectiveResourceName;
				string text = null;
				if (string.IsNullOrEmpty(effectiveResourceName))
				{
					text = frameworkScript.EffectivePath;
				}
				Assembly assembly = frameworkScript.GetAssembly(this);
				int i = 0;
				while (i < scripts.Count)
				{
					ScriptReferenceBase scriptReferenceBase2 = scripts[i];
					ScriptReference scriptReference = scriptReferenceBase2 as ScriptReference;
					if (scriptReference != null && ((!string.IsNullOrEmpty(effectiveResourceName) && scriptReference.EffectiveResourceName == effectiveResourceName && scriptReference.GetAssembly(this) == assembly) || (!string.IsNullOrEmpty(text) && scriptReference.ScriptInfo.Path == text)))
					{
						if (webFormsWithoutAjax || i == 0)
						{
							scriptReferenceBase2.AlwaysLoadBeforeUI = true;
							return scriptReferenceBase2;
						}
						scriptReferenceBase = scriptReferenceBase2;
						scripts.Remove(scriptReferenceBase2);
						break;
					}
					else
					{
						CompositeScriptReference compositeScriptReference = scriptReferenceBase2 as CompositeScriptReference;
						if (compositeScriptReference != null)
						{
							bool flag = false;
							foreach (ScriptReference scriptReference2 in compositeScriptReference.Scripts)
							{
								if ((!string.IsNullOrEmpty(effectiveResourceName) && scriptReference2.EffectiveResourceName == effectiveResourceName && scriptReference2.GetAssembly(this) == assembly) || (!string.IsNullOrEmpty(text) && scriptReference2.ScriptInfo.Path == text))
								{
									if (webFormsWithoutAjax || i == 0)
									{
										scriptReferenceBase2.AlwaysLoadBeforeUI = true;
										return scriptReferenceBase2;
									}
									scriptReferenceBase = scriptReferenceBase2;
									scripts.Remove(scriptReferenceBase2);
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								goto IL_167;
							}
							break;
						}
						IL_167:
						i++;
					}
				}
				if (webFormsWithoutAjax)
				{
					index = scripts.Count;
				}
			}
			scriptReferenceBase.AlwaysLoadBeforeUI = true;
			scripts.Insert(index, scriptReferenceBase);
			return scriptReferenceBase;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00015994 File Offset: 0x00013B94
		internal void AddFrameworkScripts(List<ScriptReferenceBase> scripts)
		{
			AjaxFrameworkMode ajaxFrameworkMode = this.AjaxFrameworkMode;
			if (ajaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				this._appServicesInitializationScript = this.GetApplicationServicesInitializationScript();
				if (ajaxFrameworkMode == AjaxFrameworkMode.Enabled && !string.IsNullOrEmpty(this._appServicesInitializationScript))
				{
					ScriptReference frameworkScript = new ScriptReference("MicrosoftAjaxApplicationServices.js", this, this);
					this._applicationServicesReference = this.AddFrameworkScript(frameworkScript, scripts, false);
				}
			}
			if (this.SupportsPartialRendering && ajaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				ScriptReference frameworkScript2 = new ScriptReference("MicrosoftAjaxWebForms.js", this, this);
				this.AddFrameworkScript(frameworkScript2, scripts, this.AjaxFrameworkMode == AjaxFrameworkMode.Explicit);
			}
			if (ajaxFrameworkMode == AjaxFrameworkMode.Enabled)
			{
				ScriptReference scriptReference = new ScriptReference("MicrosoftAjax.js", this, this);
				scriptReference.IsDefiningSys = true;
				this._scriptPathsDefiningSys.Add(scriptReference.EffectivePath);
				this.AddFrameworkScript(scriptReference, scripts, false);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00015A44 File Offset: 0x00013C44
		internal void AddScriptCollections(List<ScriptReferenceBase> scripts, IEnumerable<ScriptManagerProxy> proxies)
		{
			if (this._compositeScript != null && this._compositeScript.Scripts.Count != 0)
			{
				this._compositeScript.ClientUrlResolver = this.Control;
				this._compositeScript.ContainingControl = this;
				this._compositeScript.IsStaticReference = true;
				scripts.Add(this._compositeScript);
			}
			if (this._scripts != null)
			{
				foreach (ScriptReference scriptReference in this._scripts)
				{
					if (scriptReference.IsAjaxFrameworkScript(this) && (scriptReference.Name.StartsWith("MicrosoftAjax.", StringComparison.OrdinalIgnoreCase) || scriptReference.Name.StartsWith("MicrosoftAjaxCore.", StringComparison.OrdinalIgnoreCase)))
					{
						scriptReference.IsDefiningSys = true;
						this._scriptPathsDefiningSys.Add(scriptReference.EffectivePath);
					}
					scriptReference.ClientUrlResolver = this.Control;
					scriptReference.ContainingControl = this;
					scriptReference.IsStaticReference = true;
					scripts.Add(scriptReference);
				}
			}
			if (proxies != null)
			{
				foreach (ScriptManagerProxy scriptManagerProxy in proxies)
				{
					scriptManagerProxy.CollectScripts(scripts);
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00015B88 File Offset: 0x00013D88
		internal string CreateUniqueScriptKey()
		{
			this._uniqueScriptCounter++;
			return "UniqueScript_" + this._uniqueScriptCounter.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00015BB4 File Offset: 0x00013DB4
		private string GetApplicationServicesInitializationScript()
		{
			StringBuilder stringBuilder = null;
			ProfileServiceManager.ConfigureProfileService(ref stringBuilder, this.Context, this, this._proxies);
			AuthenticationServiceManager.ConfigureAuthenticationService(ref stringBuilder, this.Context, this, this._proxies);
			RoleServiceManager.ConfigureRoleService(ref stringBuilder, this.Context, this, this._proxies);
			if (stringBuilder != null && stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00015C13 File Offset: 0x00013E13
		public static ScriptManager GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(ScriptManager)] as ScriptManager;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015C3D File Offset: 0x00013E3D
		[SecurityCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private static ICustomErrorsSection GetCustomErrorsSectionWithAssert()
		{
			return new CustomErrorsSectionWrapper((CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors"));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00015C53 File Offset: 0x00013E53
		public ReadOnlyCollection<RegisteredArrayDeclaration> GetRegisteredArrayDeclarations()
		{
			return new ReadOnlyCollection<RegisteredArrayDeclaration>(this.ScriptRegistration.ScriptArrays);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00015C65 File Offset: 0x00013E65
		public ReadOnlyCollection<RegisteredScript> GetRegisteredClientScriptBlocks()
		{
			return new ReadOnlyCollection<RegisteredScript>(this.ScriptRegistration.ScriptBlocks);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015C77 File Offset: 0x00013E77
		public ReadOnlyCollection<RegisteredDisposeScript> GetRegisteredDisposeScripts()
		{
			return new ReadOnlyCollection<RegisteredDisposeScript>(this.ScriptRegistration.ScriptDisposes);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00015C89 File Offset: 0x00013E89
		public ReadOnlyCollection<RegisteredExpandoAttribute> GetRegisteredExpandoAttributes()
		{
			return new ReadOnlyCollection<RegisteredExpandoAttribute>(this.ScriptRegistration.ScriptExpandos);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015C9B File Offset: 0x00013E9B
		public ReadOnlyCollection<RegisteredHiddenField> GetRegisteredHiddenFields()
		{
			return new ReadOnlyCollection<RegisteredHiddenField>(this.ScriptRegistration.ScriptHiddenFields);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00015CAD File Offset: 0x00013EAD
		public ReadOnlyCollection<RegisteredScript> GetRegisteredOnSubmitStatements()
		{
			return new ReadOnlyCollection<RegisteredScript>(this.ScriptRegistration.ScriptSubmitStatements);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00015CBF File Offset: 0x00013EBF
		public ReadOnlyCollection<RegisteredScript> GetRegisteredStartupScripts()
		{
			return new ReadOnlyCollection<RegisteredScript>(this.ScriptRegistration.ScriptStartupBlocks);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00015CD1 File Offset: 0x00013ED1
		internal string GetScriptResourceUrl(string resourceName, Assembly assembly)
		{
			return ScriptResourceHandler.GetScriptResourceUrl(assembly, resourceName, this.EnableScriptLocalization ? CultureInfo.CurrentUICulture : CultureInfo.InvariantCulture, this.Zip);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public string GetStateString()
		{
			if (this.EnableSecureHistoryState)
			{
				ScriptManager.StatePersister statePersister = new ScriptManager.StatePersister(this.Page);
				return statePersister.Serialize(this._initialState);
			}
			if (this._initialState == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object obj in this._initialState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!flag)
				{
					stringBuilder.Append('&');
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(HttpUtility.UrlEncode((string)dictionaryEntry.Key));
				stringBuilder.Append('=');
				stringBuilder.Append(HttpUtility.UrlEncode((string)dictionaryEntry.Value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015DD0 File Offset: 0x00013FD0
		private void LoadHistoryState(string serverState)
		{
			NameValueCollection nameValueCollection;
			if (string.IsNullOrEmpty(serverState))
			{
				this._initialState = new Hashtable(StringComparer.Ordinal);
				nameValueCollection = new NameValueCollection();
			}
			else
			{
				if (this.EnableSecureHistoryState)
				{
					ScriptManager.StatePersister statePersister = new ScriptManager.StatePersister(this.Page);
					this._initialState = (Hashtable)statePersister.Deserialize(serverState);
					nameValueCollection = new NameValueCollection();
					using (IDictionaryEnumerator enumerator = this._initialState.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							nameValueCollection.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
						goto IL_110;
					}
				}
				nameValueCollection = HttpUtility.ParseQueryString(serverState);
				this._initialState = new Hashtable(nameValueCollection.Count, StringComparer.Ordinal);
				foreach (object obj2 in nameValueCollection)
				{
					string text = (string)obj2;
					this._initialState.Add(text, nameValueCollection[text]);
				}
			}
			IL_110:
			HistoryEventArgs e = new HistoryEventArgs(nameValueCollection);
			this.RaiseNavigate(e);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015F18 File Offset: 0x00014118
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (this.IsInAsyncPostBack)
			{
				this.PageRequestManager.LoadPostData(postDataKey, postCollection);
			}
			else if (this.EnableHistory && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				string serverState = postCollection[postDataKey];
				this.LoadHistoryState(serverState);
			}
			return false;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00015F5D File Offset: 0x0001415D
		private bool NeedToLoadBeforeUI(ScriptReference script, AjaxFrameworkMode ajaxMode)
		{
			return script.IsFromSystemWeb() || (ajaxMode == AjaxFrameworkMode.Explicit && script.IsAjaxFrameworkScript(this) && ScriptManager.SplitFrameworkScripts.Contains(script.EffectiveResourceName));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00015F8C File Offset: 0x0001418C
		protected internal virtual void OnAsyncPostBackError(AsyncPostBackErrorEventArgs e)
		{
			EventHandler<AsyncPostBackErrorEventArgs> eventHandler = (EventHandler<AsyncPostBackErrorEventArgs>)base.Events[ScriptManager.AsyncPostBackErrorEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00015FBC File Offset: 0x000141BC
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				Assembly ajaxFrameworkAssembly = this.AjaxFrameworkAssembly;
				if (ajaxFrameworkAssembly != null && ajaxFrameworkAssembly != AssemblyCache.SystemWebExtensions && AssemblyCache.GetVersion(ajaxFrameworkAssembly) <= AssemblyCache.GetVersion(AssemblyCache.SystemWebExtensions))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.ScriptManager_MustHaveGreaterVersion, new object[]
					{
						ajaxFrameworkAssembly,
						AssemblyCache.GetVersion(AssemblyCache.SystemWebExtensions)
					}));
				}
				IPage ipage = this.IPage;
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current != null)
				{
					throw new InvalidOperationException(AtlasWeb.ScriptManager_OnlyOneScriptManager);
				}
				ipage.Items[typeof(IScriptManager)] = this;
				ipage.Items[typeof(ScriptManager)] = this;
				ipage.InitComplete += this.OnPageInitComplete;
				ipage.PreRenderComplete += this.OnPagePreRenderComplete;
				if (ipage.IsPostBack)
				{
					this._isInAsyncPostBack = PageRequestManager.IsAsyncPostBackRequest(ipage.Request);
				}
				this.PageRequestManager.OnInit();
				ipage.PreRender += this.ScriptControlManager.OnPagePreRender;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000160E8 File Offset: 0x000142E8
		private void RaiseNavigate(HistoryEventArgs e)
		{
			EventHandler<HistoryEventArgs> eventHandler = (EventHandler<HistoryEventArgs>)base.Events[ScriptManager.NavigateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			foreach (ScriptManagerProxy scriptManagerProxy in this.Proxies)
			{
				eventHandler = scriptManagerProxy.NavigateEvent;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00016168 File Offset: 0x00014368
		private void OnPagePreRenderComplete(object sender, EventArgs e)
		{
			this._preRenderCompleted = true;
			if (!this.IsInAsyncPostBack)
			{
				if (this.SupportsPartialRendering && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
				{
					this.IPage.ClientScript.GetPostBackEventReference(new PostBackOptions(this, null, null, false, false, false, false, true, null));
				}
				this.RegisterGlobalizationScriptBlock();
				this.RegisterScripts();
				this.RegisterServices();
				return;
			}
			this.RegisterScripts();
			if (this.EnableHistory && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				if (this._initialState != null && this._initialState.Count == 0)
				{
					this._initialState = null;
				}
				if (this._newPointCreated)
				{
					this.RegisterDataItem(this, this.GetStateString(), false);
				}
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00016210 File Offset: 0x00014410
		private void OnPageInitComplete(object sender, EventArgs e)
		{
			if (this.IPage.IsPostBack && this.IsInAsyncPostBack && !this.SupportsPartialRendering)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptManager_AsyncPostBackNotInPartialRenderingMode);
			}
			this._initCompleted = true;
			if (this.EnableHistory && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				this.RegisterAsyncPostBackControl(this);
				if (this.IPage.IsPostBack)
				{
					this._isNavigating = (this.IPage.Request["__EVENTTARGET"] == this.UniqueID);
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00016297 File Offset: 0x00014497
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.IsInAsyncPostBack)
			{
				this.PageRequestManager.OnPreRender();
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000162B4 File Offset: 0x000144B4
		protected virtual void OnResolveCompositeScriptReference(CompositeScriptReferenceEventArgs e)
		{
			EventHandler<CompositeScriptReferenceEventArgs> eventHandler = (EventHandler<CompositeScriptReferenceEventArgs>)base.Events[ScriptManager.ResolveCompositeScriptReferenceEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000162E4 File Offset: 0x000144E4
		protected virtual void OnResolveScriptReference(ScriptReferenceEventArgs e)
		{
			EventHandler<ScriptReferenceEventArgs> eventHandler = (EventHandler<ScriptReferenceEventArgs>)base.Events[ScriptManager.ResolveScriptReferenceEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00016314 File Offset: 0x00014514
		private void PrepareNewHistoryPoint()
		{
			if (!this.EnableHistory)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotAddHistoryPointWithHistoryDisabled);
			}
			if (!this.IsInAsyncPostBack)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotAddHistoryPointOutsideOfAsyncPostBack);
			}
			this._newPointCreated = true;
			if (this._initialState == null)
			{
				this._initialState = new Hashtable(StringComparer.Ordinal);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00016366 File Offset: 0x00014566
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			this.LoadHistoryState(eventArgument);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000032F4 File Offset: 0x000014F4
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001636F File Offset: 0x0001456F
		public static void RegisterArrayDeclaration(Page page, string arrayName, string arrayValue)
		{
			ScriptRegistrationManager.RegisterArrayDeclaration(page, arrayName, arrayValue);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001636F File Offset: 0x0001456F
		public static void RegisterArrayDeclaration(Control control, string arrayName, string arrayValue)
		{
			ScriptRegistrationManager.RegisterArrayDeclaration(control, arrayName, arrayValue);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016379 File Offset: 0x00014579
		public void RegisterAsyncPostBackControl(Control control)
		{
			this.PageRequestManager.RegisterAsyncPostBackControl(control);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016387 File Offset: 0x00014587
		internal virtual void RegisterClientScriptBlockInternal(Control control, Type type, string key, string script, bool addScriptTags)
		{
			ScriptManager.RegisterClientScriptBlock(control, type, key, script, addScriptTags);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016395 File Offset: 0x00014595
		public static void RegisterClientScriptBlock(Page page, Type type, string key, string script, bool addScriptTags)
		{
			ScriptRegistrationManager.RegisterClientScriptBlock(page, type, key, script, addScriptTags);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016395 File Offset: 0x00014595
		public static void RegisterClientScriptBlock(Control control, Type type, string key, string script, bool addScriptTags)
		{
			ScriptRegistrationManager.RegisterClientScriptBlock(control, type, key, script, addScriptTags);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000163A2 File Offset: 0x000145A2
		internal virtual void RegisterClientScriptIncludeInternal(Control control, Type type, string key, string url)
		{
			ScriptManager.RegisterClientScriptInclude(control, type, key, url);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000163AE File Offset: 0x000145AE
		public static void RegisterClientScriptInclude(Page page, Type type, string key, string url)
		{
			ScriptRegistrationManager.RegisterClientScriptInclude(page, type, key, url);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000163AE File Offset: 0x000145AE
		public static void RegisterClientScriptInclude(Control control, Type type, string key, string url)
		{
			ScriptRegistrationManager.RegisterClientScriptInclude(control, type, key, url);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000163B9 File Offset: 0x000145B9
		public static void RegisterClientScriptResource(Page page, Type type, string resourceName)
		{
			ScriptRegistrationManager.RegisterClientScriptResource(page, type, resourceName);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000163B9 File Offset: 0x000145B9
		public static void RegisterClientScriptResource(Control control, Type type, string resourceName)
		{
			ScriptRegistrationManager.RegisterClientScriptResource(control, type, resourceName);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000163C4 File Offset: 0x000145C4
		private static bool TryRegisterNamedClientScriptResourceUsingScriptReference(Page page, string resourceName)
		{
			if (page != null)
			{
				ScriptManager current = ScriptManager.GetCurrent(page);
				ScriptResourceDefinition definition = ScriptManager.ScriptResourceMapping.GetDefinition(resourceName);
				if (current != null && definition != null)
				{
					current.Scripts.Add(new ScriptReference
					{
						Name = resourceName
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00016407 File Offset: 0x00014607
		public static void RegisterNamedClientScriptResource(Control control, string resourceName)
		{
			if (control != null && ScriptManager.TryRegisterNamedClientScriptResourceUsingScriptReference(control.Page, resourceName))
			{
				return;
			}
			ScriptManager.RegisterClientScriptResource(control, typeof(ScriptManager), resourceName);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001642C File Offset: 0x0001462C
		public static void RegisterNamedClientScriptResource(Page page, string resourceName)
		{
			if (ScriptManager.TryRegisterNamedClientScriptResourceUsingScriptReference(page, resourceName))
			{
				return;
			}
			ScriptManager.RegisterClientScriptResource(page, typeof(ScriptManager), resourceName);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016449 File Offset: 0x00014649
		public void RegisterDataItem(Control control, string dataItem)
		{
			this.RegisterDataItem(control, dataItem, false);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016454 File Offset: 0x00014654
		public void RegisterDataItem(Control control, string dataItem, bool isJsonSerialized)
		{
			this.PageRequestManager.RegisterDataItem(control, dataItem, isJsonSerialized);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016464 File Offset: 0x00014664
		public void RegisterDispose(Control control, string disposeScript)
		{
			if (this.SupportsPartialRendering && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				this.ScriptRegistration.RegisterDispose(control, disposeScript);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00016484 File Offset: 0x00014684
		public static void RegisterExpandoAttribute(Control control, string controlId, string attributeName, string attributeValue, bool encode)
		{
			ScriptRegistrationManager.RegisterExpandoAttribute(control, controlId, attributeName, attributeValue, encode);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00016491 File Offset: 0x00014691
		public void RegisterExtenderControl<TExtenderControl>(TExtenderControl extenderControl, Control targetControl) where TExtenderControl : Control, IExtenderControl
		{
			this.ScriptControlManager.RegisterExtenderControl<TExtenderControl>(extenderControl, targetControl);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000164A0 File Offset: 0x000146A0
		private void RegisterGlobalizationScriptBlock()
		{
			if (this.EnableScriptGlobalization && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				Tuple<string, string> clientCultureScriptBlock = ClientCultureInfo.GetClientCultureScriptBlock(CultureInfo.CurrentCulture);
				if (clientCultureScriptBlock != null && !string.IsNullOrEmpty(clientCultureScriptBlock.Item1))
				{
					if (this.IsDebuggingEnabled && this.AjaxFrameworkMode == AjaxFrameworkMode.Explicit)
					{
						string script = "Type._checkDependency('MicrosoftAjaxGlobalization.js', 'ScriptManager.EnableScriptGlobalization');\r\n";
						ScriptRegistrationManager.RegisterStartupScript(this, typeof(ScriptManager), "CultureInfoScriptCheck", script, true);
					}
					ScriptRegistrationManager.RegisterClientScriptBlock(this, typeof(ScriptManager), "CultureInfo", clientCultureScriptBlock.Item1, true);
					if (!string.IsNullOrEmpty(clientCultureScriptBlock.Item2))
					{
						ScriptReference scriptReference = new ScriptReference(clientCultureScriptBlock.Item2, null);
						scriptReference.IgnoreScriptPath = true;
						scriptReference.AlwaysLoadBeforeUI = true;
						this.Scripts.Add(scriptReference);
					}
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016560 File Offset: 0x00014760
		public static void RegisterHiddenField(Page page, string hiddenFieldName, string hiddenFieldInitialValue)
		{
			ScriptRegistrationManager.RegisterHiddenField(page, hiddenFieldName, hiddenFieldInitialValue);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00016560 File Offset: 0x00014760
		public static void RegisterHiddenField(Control control, string hiddenFieldName, string hiddenFieldInitialValue)
		{
			ScriptRegistrationManager.RegisterHiddenField(control, hiddenFieldName, hiddenFieldInitialValue);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001656A File Offset: 0x0001476A
		public static void RegisterOnSubmitStatement(Page page, Type type, string key, string script)
		{
			ScriptRegistrationManager.RegisterOnSubmitStatement(page, type, key, script);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001656A File Offset: 0x0001476A
		public static void RegisterOnSubmitStatement(Control control, Type type, string key, string script)
		{
			ScriptRegistrationManager.RegisterOnSubmitStatement(control, type, key, script);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00016575 File Offset: 0x00014775
		public void RegisterScriptControl<TScriptControl>(TScriptControl scriptControl) where TScriptControl : Control, IScriptControl
		{
			this.ScriptControlManager.RegisterScriptControl<TScriptControl>(scriptControl);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00016583 File Offset: 0x00014783
		public void RegisterScriptDescriptors(IExtenderControl extenderControl)
		{
			this.ScriptControlManager.RegisterScriptDescriptors(extenderControl);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016591 File Offset: 0x00014791
		public void RegisterScriptDescriptors(IScriptControl scriptControl)
		{
			this.ScriptControlManager.RegisterScriptDescriptors(scriptControl);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001659F File Offset: 0x0001479F
		public void RegisterPostBackControl(Control control)
		{
			this.PageRequestManager.RegisterPostBackControl(control);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000165B0 File Offset: 0x000147B0
		private static string GetEffectivePath(ScriptReferenceBase scriptRef)
		{
			string text = scriptRef.Path;
			if (string.IsNullOrEmpty(text))
			{
				ScriptReference scriptReference = scriptRef as ScriptReference;
				if (scriptReference != null)
				{
					text = scriptReference.EffectivePath;
				}
			}
			return text;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000165E0 File Offset: 0x000147E0
		internal List<ScriptReferenceBase> ProcessBundleReferences(List<ScriptReferenceBase> scripts)
		{
			object bundleResolver = this.BundleReflectionHelper.BundleResolver;
			if (bundleResolver == null)
			{
				return scripts;
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (ScriptReferenceBase scriptReferenceBase in scripts)
			{
				string effectivePath = ScriptManager.GetEffectivePath(scriptReferenceBase);
				if (this.BundleReflectionHelper.IsBundleVirtualPath(effectivePath))
				{
					scriptReferenceBase.IsBundleReference = true;
					IEnumerable<string> bundleContents = this.BundleReflectionHelper.GetBundleContents(effectivePath);
					if (bundleContents != null)
					{
						foreach (string item in bundleContents)
						{
							hashSet.Add(item);
							if (this._scriptPathsDefiningSys.Contains(item))
							{
								scriptReferenceBase.IsDefiningSys = true;
							}
						}
					}
				}
			}
			if (hashSet.Count == 0)
			{
				return scripts;
			}
			List<ScriptReferenceBase> list = new List<ScriptReferenceBase>();
			foreach (ScriptReferenceBase scriptReferenceBase2 in scripts)
			{
				string effectivePath2 = ScriptManager.GetEffectivePath(scriptReferenceBase2);
				if (scriptReferenceBase2.IsBundleReference)
				{
					list.Add(scriptReferenceBase2);
				}
				else if (!hashSet.Contains(effectivePath2))
				{
					list.Add(scriptReferenceBase2);
				}
			}
			return list;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016750 File Offset: 0x00014950
		private void RegisterScripts()
		{
			List<ScriptReferenceBase> list = new List<ScriptReferenceBase>();
			this.AddScriptCollections(list, this._proxies);
			this.ScriptControlManager.AddScriptReferences(list);
			this.AddFrameworkScripts(list);
			foreach (ScriptReferenceBase scriptReferenceBase in list)
			{
				ScriptReference scriptReference = scriptReferenceBase as ScriptReference;
				if (scriptReference != null)
				{
					this.OnResolveScriptReference(new ScriptReferenceEventArgs(scriptReference));
				}
				else
				{
					CompositeScriptReference compositeScriptReference = scriptReferenceBase as CompositeScriptReference;
					if (compositeScriptReference != null)
					{
						this.OnResolveCompositeScriptReference(new CompositeScriptReferenceEventArgs(compositeScriptReference));
					}
				}
			}
			List<ScriptReferenceBase> list2 = this.RemoveDuplicates(list, this.AjaxFrameworkMode, this.LoadScriptsBeforeUI, this.IsInAsyncPostBack ? null : this.IPage.ClientScript, ref this._applicationServicesReference);
			list2 = this.ProcessBundleReferences(list2);
			this.RegisterUniqueScripts(list2);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016830 File Offset: 0x00014A30
		private void RegisterUniqueScripts(List<ScriptReferenceBase> uniqueScripts)
		{
			bool flag = !this.IsDebuggingEnabled || this.IsInAsyncPostBack;
			bool flag2 = !string.IsNullOrEmpty(this._appServicesInitializationScript);
			bool loadScriptsBeforeUI = this.LoadScriptsBeforeUI;
			AjaxFrameworkMode ajaxFrameworkMode = this.AjaxFrameworkMode;
			foreach (ScriptReferenceBase scriptReferenceBase in uniqueScripts)
			{
				string url = scriptReferenceBase.GetUrl(this, this.Zip);
				string key = url;
				if (loadScriptsBeforeUI || scriptReferenceBase.AlwaysLoadBeforeUI)
				{
					this.RegisterClientScriptIncludeInternal(scriptReferenceBase.ContainingControl, typeof(ScriptManager), key, url);
				}
				else
				{
					string script = "\r\n<script src=\"" + HttpUtility.HtmlAttributeEncode(url) + "\" type=\"text/javascript\"></script>";
					this.RegisterStartupScriptInternal(scriptReferenceBase.ContainingControl, typeof(ScriptManager), url, script, false);
				}
				this.RegisterFallbackScript(scriptReferenceBase, key);
				if ((!flag || flag2) && scriptReferenceBase.IsAjaxFrameworkScript(this) && ajaxFrameworkMode != AjaxFrameworkMode.Disabled)
				{
					if (!flag && scriptReferenceBase.IsDefiningSys)
					{
						this.AddFrameworkLoadedCheck();
						flag = true;
					}
					if (flag2 && scriptReferenceBase == this._applicationServicesReference)
					{
						this.IPage.ClientScript.RegisterClientScriptBlock(typeof(ScriptManager), "AppServicesConfig", this._appServicesInitializationScript, true);
						flag2 = false;
					}
				}
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016988 File Offset: 0x00014B88
		private void RegisterFallbackScript(ScriptReferenceBase script, string key)
		{
			if (!this.EnableCdn || !this.EnableCdnFallback)
			{
				return;
			}
			ScriptReference scriptReference = script as ScriptReference;
			if (scriptReference != null)
			{
				ScriptReference.ScriptEffectiveInfo scriptInfo = scriptReference.ScriptInfo;
				if (!string.IsNullOrEmpty(scriptInfo.LoadSuccessExpression))
				{
					string urlInternal = scriptReference.GetUrlInternal(this, this.Zip, false);
					if (string.IsNullOrEmpty(urlInternal))
					{
						return;
					}
					if (this._isInAsyncPostBack)
					{
						ScriptRegistrationManager.RegisterFallbackScriptForAjaxPostbacks(script.ContainingControl, typeof(ScriptManager), key, scriptInfo.LoadSuccessExpression, urlInternal);
						return;
					}
					this.RegisterClientScriptBlockInternal(script.ContainingControl, typeof(ScriptManager), scriptInfo.LoadSuccessExpression, string.Format(CultureInfo.InvariantCulture, "({0})||document.write('<script type=\"text/javascript\" src=\"{1}\"><\\/script>');", new object[]
					{
						scriptInfo.LoadSuccessExpression,
						urlInternal
					}), true);
				}
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016A44 File Offset: 0x00014C44
		private void RegisterServices()
		{
			if (this._services != null)
			{
				foreach (ServiceReference serviceReference in this._services)
				{
					serviceReference.Register(this, this);
				}
			}
			if (this._proxies != null)
			{
				foreach (ScriptManagerProxy scriptManagerProxy in this._proxies)
				{
					scriptManagerProxy.RegisterServices(this);
				}
			}
			if (this.EnablePageMethods)
			{
				string clientProxyScript = PageClientProxyGenerator.GetClientProxyScript(this.Context, this.IPage, this.IsDebuggingEnabled);
				if (!string.IsNullOrEmpty(clientProxyScript))
				{
					this.RegisterClientScriptBlockInternal(this, typeof(ScriptManager), clientProxyScript, clientProxyScript, true);
				}
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016B24 File Offset: 0x00014D24
		private static void RegisterResourceWithClientScriptManager(IClientScriptManager clientScriptManager, Assembly assembly, string key)
		{
			Dictionary<Assembly, Dictionary<string, object>> registeredResourcesToSuppress = clientScriptManager.RegisteredResourcesToSuppress;
			Dictionary<string, object> dictionary;
			if (!registeredResourcesToSuppress.TryGetValue(assembly, out dictionary))
			{
				dictionary = new Dictionary<string, object>();
				registeredResourcesToSuppress[assembly] = dictionary;
			}
			dictionary[key] = true;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00016B60 File Offset: 0x00014D60
		internal List<ScriptReferenceBase> RemoveDuplicates(List<ScriptReferenceBase> scripts, AjaxFrameworkMode ajaxFrameworkMode, bool loadScriptsBeforeUI, IClientScriptManager clientScriptManager, ref ScriptReferenceBase applicationServicesReference)
		{
			int count = scripts.Count;
			if (ajaxFrameworkMode == AjaxFrameworkMode.Enabled)
			{
				if (count == 1)
				{
					ScriptReference scriptReference = scripts[0] as ScriptReference;
					if (scriptReference != null)
					{
						if (clientScriptManager != null && !string.IsNullOrEmpty(scriptReference.EffectiveResourceName))
						{
							ScriptManager.RegisterResourceWithClientScriptManager(clientScriptManager, scriptReference.GetAssembly(this), scriptReference.EffectiveResourceName);
						}
						return scripts;
					}
				}
				else if (count == 2)
				{
					ScriptReference scriptReference2 = scripts[0] as ScriptReference;
					ScriptReference scriptReference3 = scripts[1] as ScriptReference;
					if (scriptReference2 != null && scriptReference3 != null && (scriptReference2.EffectiveResourceName != scriptReference3.EffectiveResourceName || scriptReference2.Assembly != scriptReference3.Assembly))
					{
						if (clientScriptManager != null)
						{
							if (!string.IsNullOrEmpty(scriptReference2.EffectiveResourceName))
							{
								ScriptManager.RegisterResourceWithClientScriptManager(clientScriptManager, scriptReference2.GetAssembly(this), scriptReference2.EffectiveResourceName);
							}
							if (!string.IsNullOrEmpty(scriptReference3.EffectiveResourceName))
							{
								ScriptManager.RegisterResourceWithClientScriptManager(clientScriptManager, scriptReference3.GetAssembly(this), scriptReference3.EffectiveResourceName);
							}
						}
						return scripts;
					}
				}
			}
			HybridDictionary hybridDictionary = new HybridDictionary(count);
			List<ScriptReferenceBase> list = new List<ScriptReferenceBase>(count);
			foreach (ScriptReferenceBase scriptReferenceBase in scripts)
			{
				CompositeScriptReference compositeScriptReference = scriptReferenceBase as CompositeScriptReference;
				if (compositeScriptReference != null)
				{
					bool flag = false;
					foreach (ScriptReference scriptReference4 in compositeScriptReference.Scripts)
					{
						Tuple<string, Assembly> tuple = string.IsNullOrEmpty(scriptReference4.EffectiveResourceName) ? new Tuple<string, Assembly>(scriptReference4.EffectivePath, null) : new Tuple<string, Assembly>(scriptReference4.EffectiveResourceName, scriptReference4.GetAssembly(this));
						if (hybridDictionary.Contains(tuple))
						{
							throw new InvalidOperationException(AtlasWeb.ScriptManager_CannotRegisterScriptInMultipleCompositeReferences);
						}
						if (clientScriptManager != null && tuple.Item2 != null)
						{
							ScriptManager.RegisterResourceWithClientScriptManager(clientScriptManager, tuple.Item2, tuple.Item1);
						}
						if (ajaxFrameworkMode == AjaxFrameworkMode.Explicit && scriptReference4.IsAjaxFrameworkScript(this) && applicationServicesReference == null && scriptReference4.EffectiveResourceName.StartsWith("MicrosoftAjaxApplicationServices.", StringComparison.Ordinal))
						{
							applicationServicesReference = compositeScriptReference;
						}
						if (!loadScriptsBeforeUI && !flag && this.NeedToLoadBeforeUI(scriptReference4, ajaxFrameworkMode))
						{
							compositeScriptReference.AlwaysLoadBeforeUI = true;
							flag = true;
						}
						hybridDictionary.Add(tuple, scriptReference4);
					}
				}
			}
			foreach (ScriptReferenceBase scriptReferenceBase2 in scripts)
			{
				CompositeScriptReference compositeScriptReference2 = scriptReferenceBase2 as CompositeScriptReference;
				if (compositeScriptReference2 != null)
				{
					list.Add(compositeScriptReference2);
				}
				else
				{
					ScriptReference scriptReference5 = scriptReferenceBase2 as ScriptReference;
					if (scriptReference5 != null)
					{
						Tuple<string, Assembly> tuple2 = string.IsNullOrEmpty(scriptReference5.EffectiveResourceName) ? new Tuple<string, Assembly>(scriptReference5.EffectivePath, null) : new Tuple<string, Assembly>(scriptReference5.EffectiveResourceName, scriptReference5.GetAssembly(this));
						if ((ajaxFrameworkMode != AjaxFrameworkMode.Explicit || !scriptReference5.IsAjaxFrameworkScript(this) || !scriptReference5.EffectiveResourceName.StartsWith("MicrosoftAjax.", StringComparison.Ordinal)) && !hybridDictionary.Contains(tuple2))
						{
							if (scriptReference5.IsStaticReference)
							{
								hybridDictionary.Add(tuple2, scriptReference5);
							}
							if (ajaxFrameworkMode == AjaxFrameworkMode.Explicit && scriptReference5.IsAjaxFrameworkScript(this) && applicationServicesReference == null && scriptReference5.EffectiveResourceName.StartsWith("MicrosoftAjaxApplicationServices.", StringComparison.Ordinal))
							{
								applicationServicesReference = scriptReference5;
							}
							if (!loadScriptsBeforeUI && this.NeedToLoadBeforeUI(scriptReference5, ajaxFrameworkMode))
							{
								scriptReference5.AlwaysLoadBeforeUI = true;
							}
							if (clientScriptManager != null && tuple2.Item2 != null)
							{
								ScriptManager.RegisterResourceWithClientScriptManager(clientScriptManager, tuple2.Item2, tuple2.Item1);
							}
							list.Add(scriptReference5);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00016F3C File Offset: 0x0001513C
		internal virtual void RegisterStartupScriptInternal(Control control, Type type, string key, string script, bool addScriptTags)
		{
			ScriptManager.RegisterStartupScript(control, type, key, script, addScriptTags);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016F4A File Offset: 0x0001514A
		public static void RegisterStartupScript(Page page, Type type, string key, string script, bool addScriptTags)
		{
			ScriptRegistrationManager.RegisterStartupScript(page, type, key, script, addScriptTags);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016F4A File Offset: 0x0001514A
		public static void RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags)
		{
			ScriptRegistrationManager.RegisterStartupScript(control, type, key, script, addScriptTags);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016F58 File Offset: 0x00015158
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!this.IsInAsyncPostBack && this.AjaxFrameworkMode != AjaxFrameworkMode.Disabled)
			{
				if (!((IControl)this).DesignMode && this.SupportsPartialRendering)
				{
					this.PageRequestManager.Render(writer);
				}
				if (this.EnableHistory && !base.DesignMode && this.IPage != null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
					writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
					writer.RenderBeginTag(HtmlTextWriterTag.Input);
					writer.RenderEndTag();
					JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer(new SimpleTypeResolver());
					writer.Write("\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
					if (this.IsDebuggingEnabled && this.AjaxFrameworkMode == AjaxFrameworkMode.Explicit)
					{
						writer.WriteLine("Type._checkDependency('MicrosoftAjaxHistory.js', 'ScriptManager.EnableHistory');");
					}
					writer.Write("Sys.Application.setServerId(");
					writer.Write(javaScriptSerializer.Serialize(this.ClientID));
					writer.Write(", ");
					writer.Write(javaScriptSerializer.Serialize(this.UniqueID));
					writer.WriteLine(");");
					if (this._initialState != null && this._initialState.Count != 0)
					{
						writer.Write("Sys.Application.setServerState('");
						writer.Write(HttpUtility.JavaScriptStringEncode(this.GetStateString()));
						writer.WriteLine("');");
					}
					writer.WriteLine("Sys.Application._enableHistoryInScriptManager();");
					writer.Write("//]]>\r\n</script>\r\n");
					if (!string.IsNullOrEmpty(this.ClientNavigateHandler))
					{
						string script = "Sys.Application.add_navigate(" + this.ClientNavigateHandler + ");";
						ScriptManager.RegisterStartupScript(this, typeof(ScriptManager), "HistoryNavigate", script, true);
					}
					HttpBrowserCapabilitiesBase browser = this.IPage.Request.Browser;
					if (browser.Browser.Equals("IE", StringComparison.OrdinalIgnoreCase))
					{
						if (string.IsNullOrEmpty(this.IPage.Title))
						{
							this.IPage.Title = AtlasWeb.ScriptManager_PageUntitled;
						}
						string value = (this.EmptyPageUrl.Length == 0) ? ScriptResourceHandler.GetEmptyPageUrl(this.IPage.Title) : (this.EmptyPageUrl + ((this.EmptyPageUrl.IndexOf('?') != -1) ? "&title=" : "?title=") + this.IPage.Title);
						writer.AddAttribute(HtmlTextWriterAttribute.Id, "__historyFrame");
						writer.AddAttribute(HtmlTextWriterAttribute.Src, value);
						writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
						writer.RenderBeginTag(HtmlTextWriterTag.Iframe);
						writer.RenderEndTag();
					}
				}
			}
			base.Render(writer);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000171C6 File Offset: 0x000153C6
		public void SetFocus(Control control)
		{
			this.PageRequestManager.SetFocus(control);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000171D4 File Offset: 0x000153D4
		private void SetPageTitle(string title)
		{
			if (this.Page != null && this.Page.Header != null)
			{
				this.Page.Title = title;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000171F7 File Offset: 0x000153F7
		public void SetFocus(string clientID)
		{
			this.PageRequestManager.SetFocus(clientID);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00017208 File Offset: 0x00015408
		private void SetStateValue(string key, string value)
		{
			if (value == null)
			{
				if (this._initialState.ContainsKey(key))
				{
					this._initialState.Remove(key);
					return;
				}
			}
			else
			{
				if (this._initialState.ContainsKey(key))
				{
					this._initialState[key] = value;
					return;
				}
				this._initialState.Add(key, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001725C File Offset: 0x0001545C
		HttpContextBase IControl.Context
		{
			get
			{
				return new HttpContextWrapper(this.Context);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00017269 File Offset: 0x00015469
		bool IControl.DesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00017271 File Offset: 0x00015471
		void IScriptManagerInternal.RegisterProxy(ScriptManagerProxy proxy)
		{
			if (!this.Proxies.Contains(proxy))
			{
				this.Proxies.Add(proxy);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001728D File Offset: 0x0001548D
		void IScriptManagerInternal.RegisterUpdatePanel(UpdatePanel updatePanel)
		{
			this.PageRequestManager.RegisterUpdatePanel(updatePanel);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001729B File Offset: 0x0001549B
		void IScriptManagerInternal.UnregisterUpdatePanel(UpdatePanel updatePanel)
		{
			this.PageRequestManager.UnregisterUpdatePanel(updatePanel);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000172A9 File Offset: 0x000154A9
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000172B3 File Offset: 0x000154B3
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000172BB File Offset: 0x000154BB
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000172C4 File Offset: 0x000154C4
		void IScriptManager.RegisterArrayDeclaration(Control control, string arrayName, string arrayValue)
		{
			ScriptManager.RegisterArrayDeclaration(control, arrayName, arrayValue);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016387 File Offset: 0x00014587
		void IScriptManager.RegisterClientScriptBlock(Control control, Type type, string key, string script, bool addScriptTags)
		{
			ScriptManager.RegisterClientScriptBlock(control, type, key, script, addScriptTags);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000163A2 File Offset: 0x000145A2
		void IScriptManager.RegisterClientScriptInclude(Control control, Type type, string key, string url)
		{
			ScriptManager.RegisterClientScriptInclude(control, type, key, url);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000172CE File Offset: 0x000154CE
		void IScriptManager.RegisterClientScriptResource(Control control, Type type, string resourceName)
		{
			ScriptManager.RegisterClientScriptResource(control, type, resourceName);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000172D8 File Offset: 0x000154D8
		void IScriptManager.RegisterDispose(Control control, string disposeScript)
		{
			this.RegisterDispose(control, disposeScript);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000172E2 File Offset: 0x000154E2
		void IScriptManager.RegisterExpandoAttribute(Control control, string controlId, string attributeName, string attributeValue, bool encode)
		{
			ScriptManager.RegisterExpandoAttribute(control, controlId, attributeName, attributeValue, encode);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000172F0 File Offset: 0x000154F0
		void IScriptManager.RegisterHiddenField(Control control, string hiddenFieldName, string hiddenFieldValue)
		{
			ScriptManager.RegisterHiddenField(control, hiddenFieldName, hiddenFieldValue);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000172FA File Offset: 0x000154FA
		void IScriptManager.RegisterOnSubmitStatement(Control control, Type type, string key, string script)
		{
			ScriptManager.RegisterOnSubmitStatement(control, type, key, script);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017306 File Offset: 0x00015506
		void IScriptManager.RegisterPostBackControl(Control control)
		{
			this.RegisterPostBackControl(control);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016F3C File Offset: 0x0001513C
		void IScriptManager.RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags)
		{
			ScriptManager.RegisterStartupScript(control, type, key, script, addScriptTags);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001730F File Offset: 0x0001550F
		void IScriptManager.SetFocusInternal(string clientID)
		{
			this.PageRequestManager.SetFocusInternal(clientID);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001731D File Offset: 0x0001551D
		bool IScriptManager.IsSecureConnection
		{
			get
			{
				return this.IsSecureConnection;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017325 File Offset: 0x00015525
		string IClientUrlResolver.get_AppRelativeTemplateSourceDirectory()
		{
			return base.AppRelativeTemplateSourceDirectory;
		}

		// Token: 0x04000183 RID: 387
		private new readonly IPage _page;

		// Token: 0x04000184 RID: 388
		private readonly IControl _control;

		// Token: 0x04000185 RID: 389
		private readonly ICompilationSection _appLevelCompilationSection;

		// Token: 0x04000186 RID: 390
		private readonly IDeploymentSection _deploymentSection;

		// Token: 0x04000187 RID: 391
		private readonly ICustomErrorsSection _customErrorsSection;

		// Token: 0x04000188 RID: 392
		private static bool _ajaxFrameworkAssemblyConfigChecked;

		// Token: 0x04000189 RID: 393
		private static Assembly _defaultAjaxFrameworkAssembly = null;

		// Token: 0x0400018A RID: 394
		private Assembly _ajaxFrameworkAssembly = ScriptManager.DefaultAjaxFrameworkAssembly;

		// Token: 0x0400018B RID: 395
		private const int AsyncPostBackTimeoutDefault = 90;

		// Token: 0x0400018C RID: 396
		private ScriptMode _scriptMode;

		// Token: 0x0400018D RID: 397
		private string _scriptPath;

		// Token: 0x0400018E RID: 398
		private CompositeScriptReference _compositeScript;

		// Token: 0x0400018F RID: 399
		private ScriptReferenceCollection _scripts;

		// Token: 0x04000190 RID: 400
		private ServiceReferenceCollection _services;

		// Token: 0x04000191 RID: 401
		private bool? _isRestMethodCall;

		// Token: 0x04000192 RID: 402
		private bool? _isSecureConnection;

		// Token: 0x04000193 RID: 403
		private List<ScriptManagerProxy> _proxies;

		// Token: 0x04000194 RID: 404
		private AjaxFrameworkMode _ajaxFrameworkMode;

		// Token: 0x04000195 RID: 405
		private bool _enablePartialRendering = true;

		// Token: 0x04000196 RID: 406
		private bool _supportsPartialRendering = true;

		// Token: 0x04000197 RID: 407
		internal bool _supportsPartialRenderingSetByUser;

		// Token: 0x04000198 RID: 408
		internal ScriptReferenceBase _applicationServicesReference;

		// Token: 0x04000199 RID: 409
		private string _appServicesInitializationScript;

		// Token: 0x0400019A RID: 410
		private bool _enableScriptGlobalization;

		// Token: 0x0400019B RID: 411
		private bool _enableScriptLocalization = true;

		// Token: 0x0400019C RID: 412
		private bool _enablePageMethods;

		// Token: 0x0400019D RID: 413
		private bool _loadScriptsBeforeUI = true;

		// Token: 0x0400019E RID: 414
		private bool _initCompleted;

		// Token: 0x0400019F RID: 415
		private bool _preRenderCompleted;

		// Token: 0x040001A0 RID: 416
		private bool _isInAsyncPostBack;

		// Token: 0x040001A1 RID: 417
		private int _asyncPostBackTimeout = 90;

		// Token: 0x040001A2 RID: 418
		private bool _allowCustomErrorsRedirect = true;

		// Token: 0x040001A3 RID: 419
		private string _asyncPostBackErrorMessage;

		// Token: 0x040001A4 RID: 420
		private bool _zip;

		// Token: 0x040001A5 RID: 421
		private bool _zipSet;

		// Token: 0x040001A6 RID: 422
		private int _uniqueScriptCounter;

		// Token: 0x040001A7 RID: 423
		private bool _enableCdn;

		// Token: 0x040001A8 RID: 424
		private bool _enableCdnFallback = true;

		// Token: 0x040001A9 RID: 425
		private HashSet<string> _scriptPathsDefiningSys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001AD RID: 429
		private static HashSet<string> _splitFrameworkScript;

		// Token: 0x040001AE RID: 430
		private ScriptRegistrationManager _scriptRegistration;

		// Token: 0x040001AF RID: 431
		private PageRequestManager _pageRequestManager;

		// Token: 0x040001B0 RID: 432
		private ScriptControlManager _scriptControlManager;

		// Token: 0x040001B1 RID: 433
		private ProfileServiceManager _profileServiceManager;

		// Token: 0x040001B2 RID: 434
		private AuthenticationServiceManager _authenticationServiceManager;

		// Token: 0x040001B3 RID: 435
		private RoleServiceManager _roleServiceManager;

		// Token: 0x040001B4 RID: 436
		private BundleReflectionHelper _bundleReflectionHelper;

		// Token: 0x040001B5 RID: 437
		private bool _enableSecureHistoryState = true;

		// Token: 0x040001B6 RID: 438
		private bool _enableHistory;

		// Token: 0x040001B7 RID: 439
		private bool _isNavigating;

		// Token: 0x040001B8 RID: 440
		private string _clientNavigateHandler;

		// Token: 0x040001B9 RID: 441
		private Hashtable _initialState;

		// Token: 0x040001BB RID: 443
		private bool _newPointCreated;

		// Token: 0x02000163 RID: 355
		private class StatePersister : PageStatePersister
		{
			// Token: 0x0600100A RID: 4106 RVA: 0x000377CD File Offset: 0x000359CD
			public StatePersister(Page page) : base(page)
			{
			}

			// Token: 0x0600100B RID: 4107 RVA: 0x00002058 File Offset: 0x00000258
			public override void Load()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600100C RID: 4108 RVA: 0x00002058 File Offset: 0x00000258
			public override void Save()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600100D RID: 4109 RVA: 0x000377D6 File Offset: 0x000359D6
			public string Serialize(object state)
			{
				return base.StateFormatter2.Serialize(state, Purpose.WebForms_ScriptManager_HistoryState);
			}

			// Token: 0x0600100E RID: 4110 RVA: 0x000377E9 File Offset: 0x000359E9
			public object Deserialize(string serialized)
			{
				return base.StateFormatter2.Deserialize(serialized, Purpose.WebForms_ScriptManager_HistoryState);
			}
		}
	}
}
