using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200082A RID: 2090
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public sealed class ClientBuildManager : MarshalByRefObject, IDisposable
	{
		// Token: 0x14000130 RID: 304
		// (add) Token: 0x060063BB RID: 25531 RVA: 0x0015D800 File Offset: 0x0015BA00
		// (remove) Token: 0x060063BC RID: 25532 RVA: 0x0015D838 File Offset: 0x0015BA38
		public event BuildManagerHostUnloadEventHandler AppDomainUnloaded;

		// Token: 0x14000131 RID: 305
		// (add) Token: 0x060063BD RID: 25533 RVA: 0x0015D870 File Offset: 0x0015BA70
		// (remove) Token: 0x060063BE RID: 25534 RVA: 0x0015D8A8 File Offset: 0x0015BAA8
		public event EventHandler AppDomainStarted;

		// Token: 0x14000132 RID: 306
		// (add) Token: 0x060063BF RID: 25535 RVA: 0x0015D8E0 File Offset: 0x0015BAE0
		// (remove) Token: 0x060063C0 RID: 25536 RVA: 0x0015D918 File Offset: 0x0015BB18
		public event BuildManagerHostUnloadEventHandler AppDomainShutdown;

		// Token: 0x060063C1 RID: 25537 RVA: 0x0015D94D File Offset: 0x0015BB4D
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir) : this(appVirtualDir, appPhysicalSourceDir, null, null)
		{
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0015D959 File Offset: 0x0015BB59
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir) : this(appVirtualDir, appPhysicalSourceDir, appPhysicalTargetDir, null)
		{
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0015D965 File Offset: 0x0015BB65
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter) : this(appVirtualDir, appPhysicalSourceDir, appPhysicalTargetDir, parameter, null)
		{
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0015D974 File Offset: 0x0015BB74
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter, TypeDescriptionProvider typeDescriptionProvider)
		{
			if (parameter == null)
			{
				parameter = new ClientBuildManagerParameter();
			}
			this.InitializeCBMTDPBridge(typeDescriptionProvider);
			if (!string.IsNullOrEmpty(appPhysicalTargetDir))
			{
				parameter.PrecompilationFlags |= PrecompilationFlags.Clean;
			}
			this._hostingParameters = new HostingEnvironmentParameters();
			this._hostingParameters.HostingFlags = (HostingEnvironmentFlags.DontCallAppInitialize | HostingEnvironmentFlags.ClientBuildManager);
			this._hostingParameters.ClientBuildManagerParameter = parameter;
			this._hostingParameters.PrecompilationTargetPhysicalDirectory = appPhysicalTargetDir;
			if (typeDescriptionProvider != null)
			{
				this._hostingParameters.HostingFlags |= HostingEnvironmentFlags.SupportsMultiTargeting;
			}
			if (appVirtualDir[0] != '/')
			{
				appVirtualDir = "/" + appVirtualDir;
			}
			if (appPhysicalSourceDir == null && appVirtualDir.StartsWith("/IISExpress/", StringComparison.OrdinalIgnoreCase) && appVirtualDir.Length > "/IISExpress/".Length)
			{
				int num = appVirtualDir.IndexOf('/', "/IISExpress/".Length);
				if (num > 0)
				{
					this._hostingParameters.IISExpressVersion = appVirtualDir.Substring("/IISExpress/".Length, num - "/IISExpress/".Length);
					appVirtualDir = appVirtualDir.Substring(num);
				}
			}
			this.Initialize(VirtualPath.CreateNonRelative(appVirtualDir), appPhysicalSourceDir);
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x060063C5 RID: 25541 RVA: 0x0015DA94 File Offset: 0x0015BC94
		public string CodeGenDir
		{
			get
			{
				if (this._codeGenDir == null)
				{
					this.EnsureHostCreated();
					this._codeGenDir = this._host.CodeGenDir;
				}
				return this._codeGenDir;
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x060063C6 RID: 25542 RVA: 0x0015DABB File Offset: 0x0015BCBB
		public bool IsHostCreated
		{
			get
			{
				return this._host != null;
			}
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0015DAC8 File Offset: 0x0015BCC8
		public IRegisteredObject CreateObject(Type type, bool failIfExists)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.EnsureHostCreated();
			this._host.RegisterAssembly(type.Assembly.FullName, type.Assembly.Location);
			ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
			return applicationManager.CreateObjectInternal(this._appId, type, this._appHost, failIfExists, this._hostingParameters);
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0015DB30 File Offset: 0x0015BD30
		public string[] GetAppDomainShutdownDirectories()
		{
			return FileChangesMonitor.s_dirsToMonitor;
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0015DB37 File Offset: 0x0015BD37
		public void CompileApplicationDependencies()
		{
			this.EnsureHostCreated();
			this._host.CompileApplicationDependencies();
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x0015DB4A File Offset: 0x0015BD4A
		public IDictionary GetBrowserDefinitions()
		{
			this.EnsureHostCreated();
			return this._host.GetBrowserDefinitions();
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x0015DB5D File Offset: 0x0015BD5D
		public string GetGeneratedSourceFile(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.EnsureHostCreated();
			return this._host.GetGeneratedSourceFile(VirtualPath.CreateTrailingSlash(virtualPath));
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x0015DB84 File Offset: 0x0015BD84
		public string GetGeneratedFileVirtualPath(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			this.EnsureHostCreated();
			return this._host.GetGeneratedFileVirtualPath(filePath);
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0015DBA6 File Offset: 0x0015BDA6
		public string[] GetVirtualCodeDirectories()
		{
			this.EnsureHostCreated();
			return this._host.GetVirtualCodeDirectories();
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x0015DBB9 File Offset: 0x0015BDB9
		public string[] GetTopLevelAssemblyReferences(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.EnsureHostCreated();
			return this._host.GetTopLevelAssemblyReferences(VirtualPath.Create(virtualPath));
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x0015DBE0 File Offset: 0x0015BDE0
		public void GetCodeDirectoryInformation(string virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
		{
			if (virtualCodeDir == null)
			{
				throw new ArgumentNullException("virtualCodeDir");
			}
			this.EnsureHostCreated();
			this._host.GetCodeDirectoryInformation(VirtualPath.CreateTrailingSlash(virtualCodeDir), out codeDomProviderType, out compilerParameters, out generatedFilesDir);
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x0015DC0B File Offset: 0x0015BE0B
		public void GetCompilerParameters(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.EnsureHostCreated();
			this._host.GetCompilerParams(VirtualPath.Create(virtualPath), out codeDomProviderType, out compilerParameters);
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x0015DC34 File Offset: 0x0015BE34
		public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			return this.GenerateCodeCompileUnit(virtualPath, null, out codeDomProviderType, out compilerParameters, out linePragmasTable);
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x0015DC42 File Offset: 0x0015BE42
		public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, string virtualFileString, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.EnsureHostCreated();
			return this._host.GenerateCodeCompileUnit(VirtualPath.Create(virtualPath), virtualFileString, out codeDomProviderType, out compilerParameters, out linePragmasTable);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x0015DC6F File Offset: 0x0015BE6F
		public string GenerateCode(string virtualPath, string virtualFileString, out IDictionary linePragmasTable)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.EnsureHostCreated();
			return this._host.GenerateCode(VirtualPath.Create(virtualPath), virtualFileString, out linePragmasTable);
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0015DC98 File Offset: 0x0015BE98
		public Type GetCompiledType(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.EnsureHostCreated();
			string[] compiledTypeAndAssemblyName = this._host.GetCompiledTypeAndAssemblyName(VirtualPath.Create(virtualPath), null);
			if (compiledTypeAndAssemblyName == null)
			{
				return null;
			}
			Assembly assembly = Assembly.LoadFrom(compiledTypeAndAssemblyName[1]);
			return assembly.GetType(compiledTypeAndAssemblyName[0]);
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x0015DCE5 File Offset: 0x0015BEE5
		public void CompileFile(string virtualPath)
		{
			this.CompileFile(virtualPath, null);
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0015DCF0 File Offset: 0x0015BEF0
		public void CompileFile(string virtualPath, ClientBuildManagerCallback callback)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			try
			{
				this.EnsureHostCreated();
				this._host.GetCompiledTypeAndAssemblyName(VirtualPath.Create(virtualPath), callback);
			}
			finally
			{
				if (callback != null)
				{
					RemotingServices.Disconnect(callback);
				}
			}
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x0015DD44 File Offset: 0x0015BF44
		public bool IsCodeAssembly(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			this.EnsureHostCreated();
			return this._host.IsCodeAssembly(assemblyName);
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0015DD74 File Offset: 0x0015BF74
		public bool Unload()
		{
			BuildManagerHost host = this._host;
			if (host != null)
			{
				this._host = null;
				return host.UnloadAppDomain();
			}
			return false;
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x0015DD9A File Offset: 0x0015BF9A
		public void PrecompileApplication()
		{
			this.PrecompileApplication(null);
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x0015DDA3 File Offset: 0x0015BFA3
		public void PrecompileApplication(ClientBuildManagerCallback callback)
		{
			this.PrecompileApplication(callback, false);
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x0015DDB0 File Offset: 0x0015BFB0
		public void PrecompileApplication(ClientBuildManagerCallback callback, bool forceCleanBuild)
		{
			PrecompilationFlags precompilationFlags = this._hostingParameters.ClientBuildManagerParameter.PrecompilationFlags;
			if (forceCleanBuild)
			{
				this._waitForCallBack = (this._host != null);
				this.Unload();
				this._hostingParameters.ClientBuildManagerParameter.PrecompilationFlags = (precompilationFlags | PrecompilationFlags.Clean);
				this.WaitForCallBack();
			}
			try
			{
				this.EnsureHostCreated();
				this._host.PrecompileApp(callback, this._hostingParameters.ClientBuildManagerParameter.ExcludedVirtualPaths);
			}
			finally
			{
				if (forceCleanBuild)
				{
					this._hostingParameters.ClientBuildManagerParameter.PrecompilationFlags = precompilationFlags;
				}
				if (callback != null)
				{
					RemotingServices.Disconnect(callback);
				}
			}
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x0015DE54 File Offset: 0x0015C054
		private void WaitForCallBack()
		{
			int num = 0;
			while (this._waitForCallBack && num <= 50)
			{
				Thread.Sleep(200);
				num++;
			}
			bool waitForCallBack = this._waitForCallBack;
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x0015DE88 File Offset: 0x0015C088
		internal void Initialize(VirtualPath virtualPath, string physicalPath)
		{
			this._virtualPath = virtualPath;
			this._physicalPath = FileUtil.FixUpPhysicalDirectory(physicalPath);
			this._onAppDomainUnloadedCallback = new WaitCallback(this.OnAppDomainUnloadedCallback);
			this._onAppDomainShutdown = new WaitCallback(this.OnAppDomainShutdownCallback);
			this._installPath = RuntimeEnvironment.GetRuntimeDirectory();
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x0015DED8 File Offset: 0x0015C0D8
		private void EnsureHostCreated()
		{
			if (this._host == null)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._host == null)
					{
						this.CreateHost();
					}
				}
			}
			if (this._hostCreationException != null)
			{
				throw new HttpException(this._hostCreationException.Message, this._hostCreationException);
			}
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x0015DF48 File Offset: 0x0015C148
		private void CreateHost()
		{
			this._hostCreationPending = true;
			BuildManagerHost buildManagerHost = null;
			try
			{
				ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
				string appId;
				IApplicationHost appHost;
				buildManagerHost = (BuildManagerHost)applicationManager.CreateObjectWithDefaultAppHostAndAppId(this._physicalPath, this._virtualPath, typeof(BuildManagerHost), false, this._hostingParameters, out appId, out appHost);
				buildManagerHost.AddPendingCall();
				buildManagerHost.Configure(this);
				this._host = buildManagerHost;
				this._appId = appId;
				this._appHost = appHost;
				this._hostCreationException = this._host.InitializationException;
			}
			catch (Exception hostCreationException)
			{
				this._hostCreationException = hostCreationException;
				this._host = buildManagerHost;
			}
			finally
			{
				this._hostCreationPending = false;
				if (buildManagerHost != null)
				{
					if (this.AppDomainStarted != null)
					{
						this.AppDomainStarted(this, EventArgs.Empty);
					}
					buildManagerHost.RemovePendingCall();
				}
			}
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x0015E020 File Offset: 0x0015C220
		internal void OnAppDomainUnloaded(ApplicationShutdownReason reason)
		{
			this._reason = reason;
			this._waitForCallBack = false;
			ThreadPool.QueueUserWorkItem(this._onAppDomainUnloadedCallback);
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x0015E03C File Offset: 0x0015C23C
		internal void ResetHost()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._host = null;
				this._hostCreationException = null;
			}
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x0015E084 File Offset: 0x0015C284
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void OnAppDomainUnloadedCallback(object unused)
		{
			if (this.AppDomainUnloaded != null)
			{
				this.AppDomainUnloaded(this, new BuildManagerHostUnloadEventArgs(this._reason));
			}
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x0015E0A5 File Offset: 0x0015C2A5
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void OnAppDomainShutdownCallback(object o)
		{
			if (this.AppDomainShutdown != null)
			{
				this.AppDomainShutdown(this, new BuildManagerHostUnloadEventArgs((ApplicationShutdownReason)o));
			}
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0015E0C6 File Offset: 0x0015C2C6
		internal void OnAppDomainShutdown(ApplicationShutdownReason reason)
		{
			ThreadPool.QueueUserWorkItem(this._onAppDomainShutdown, reason);
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0015E0DA File Offset: 0x0015C2DA
		private void InitializeCBMTDPBridge(TypeDescriptionProvider typeDescriptionProvider)
		{
			if (typeDescriptionProvider == null)
			{
				return;
			}
			this._cbmTdpBridge = new ClientBuildManagerTypeDescriptionProviderBridge(typeDescriptionProvider);
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x060063E7 RID: 25575 RVA: 0x0015E0EC File Offset: 0x0015C2EC
		internal ClientBuildManagerTypeDescriptionProviderBridge CBMTypeDescriptionProviderBridge
		{
			get
			{
				return this._cbmTdpBridge;
			}
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x0015E0F4 File Offset: 0x0015C2F4
		void IDisposable.Dispose()
		{
			this.Unload();
		}

		// Token: 0x040033A8 RID: 13224
		private VirtualPath _virtualPath;

		// Token: 0x040033A9 RID: 13225
		private string _physicalPath;

		// Token: 0x040033AA RID: 13226
		private string _installPath;

		// Token: 0x040033AB RID: 13227
		private string _appId;

		// Token: 0x040033AC RID: 13228
		private IApplicationHost _appHost;

		// Token: 0x040033AD RID: 13229
		private string _codeGenDir;

		// Token: 0x040033AE RID: 13230
		private HostingEnvironmentParameters _hostingParameters;

		// Token: 0x040033AF RID: 13231
		private ClientBuildManagerTypeDescriptionProviderBridge _cbmTdpBridge;

		// Token: 0x040033B0 RID: 13232
		private WaitCallback _onAppDomainUnloadedCallback;

		// Token: 0x040033B1 RID: 13233
		private WaitCallback _onAppDomainShutdown;

		// Token: 0x040033B2 RID: 13234
		private ApplicationShutdownReason _reason;

		// Token: 0x040033B3 RID: 13235
		private BuildManagerHost _host;

		// Token: 0x040033B4 RID: 13236
		private Exception _hostCreationException;

		// Token: 0x040033B5 RID: 13237
		private bool _hostCreationPending;

		// Token: 0x040033B9 RID: 13241
		private object _lock = new object();

		// Token: 0x040033BA RID: 13242
		private bool _waitForCallBack;

		// Token: 0x040033BB RID: 13243
		private const string IISExpressPrefix = "/IISExpress/";
	}
}
