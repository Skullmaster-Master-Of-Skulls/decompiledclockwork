using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Profile;
using System.Web.UI;
using System.Web.Util;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x02000803 RID: 2051
	public sealed class BuildManager
	{
		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x001562FB File Offset: 0x001544FB
		internal static BuildManager TheBuildManager
		{
			get
			{
				return BuildManager._theBuildManager;
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x00156302 File Offset: 0x00154502
		internal static bool OptimizeCompilations
		{
			get
			{
				return BuildManager._theBuildManager._optimizeCompilations;
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x060061D7 RID: 25047 RVA: 0x0015630E File Offset: 0x0015450E
		internal static string WebHashFilePath
		{
			get
			{
				return BuildManager._theBuildManager._webHashFilePath;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x0015631A File Offset: 0x0015451A
		internal static CompilationStage CompilationStage
		{
			get
			{
				return BuildManager._theBuildManager._compilationStage;
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x060061D9 RID: 25049 RVA: 0x00156326 File Offset: 0x00154526
		internal static VirtualPath ScriptVirtualDir
		{
			get
			{
				return BuildManager._theBuildManager._scriptVirtualDir;
			}
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x060061DA RID: 25050 RVA: 0x00156332 File Offset: 0x00154532
		internal static VirtualPath GlobalAsaxVirtualPath
		{
			get
			{
				return BuildManager._theBuildManager._globalAsaxVirtualPath;
			}
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00156340 File Offset: 0x00154540
		private BuildManager()
		{
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x001563A8 File Offset: 0x001545A8
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static bool InitializeBuildManager()
		{
			if (BuildManager._initializeException != null)
			{
				throw new HttpException(BuildManager._initializeException.Message, BuildManager._initializeException);
			}
			if (!BuildManager._theBuildManagerInitialized)
			{
				if (!HttpRuntime.FusionInited)
				{
					return false;
				}
				if (HttpRuntime.TrustLevel == null)
				{
					return false;
				}
				BuildManager._theBuildManagerInitialized = true;
				try
				{
					BuildManager._theBuildManager.Initialize();
				}
				catch (Exception initializeException)
				{
					BuildManager._theBuildManagerInitialized = false;
					BuildManager._initializeException = initializeException;
					throw;
				}
			}
			return true;
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x060061DD RID: 25053 RVA: 0x0015641C File Offset: 0x0015461C
		internal static ClientBuildManagerCallback CBMCallback
		{
			get
			{
				return BuildManager._theBuildManager._cbmCallback;
			}
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x00156428 File Offset: 0x00154628
		internal static void ReportParseError(ParserError parseError)
		{
			if (BuildManager.CBMCallback != null)
			{
				BuildManager._parseErrorReported = true;
				BuildManager.CBMCallback.ReportParseError(parseError);
			}
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x00156442 File Offset: 0x00154642
		private void ReportTopLevelCompilationException()
		{
			this.ReportErrorsFromException(this._topLevelFileCompilationException);
			throw new HttpException(this._topLevelFileCompilationException.Message, this._topLevelFileCompilationException);
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x00156468 File Offset: 0x00154668
		private void ReportErrorsFromException(Exception e)
		{
			if (BuildManager.CBMCallback == null)
			{
				return;
			}
			if (e is HttpCompileException)
			{
				CompilerResults results = ((HttpCompileException)e).Results;
				using (IEnumerator enumerator = results.Errors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CompilerError error = (CompilerError)obj;
						BuildManager.CBMCallback.ReportCompilerError(error);
					}
					return;
				}
			}
			if (e is HttpParseException)
			{
				foreach (object obj2 in ((HttpParseException)e).ParserErrors)
				{
					ParserError parseError = (ParserError)obj2;
					BuildManager.ReportParseError(parseError);
				}
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x0015653C File Offset: 0x0015473C
		private List<Assembly> TopLevelReferencedAssemblies
		{
			get
			{
				return this._topLevelReferencedAssemblies;
			}
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x060061E2 RID: 25058 RVA: 0x00156544 File Offset: 0x00154744
		private IDictionary<string, AssemblyReferenceInfo> TopLevelAssembliesIndexTable
		{
			get
			{
				return this._topLevelAssembliesIndexTable;
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x0015654C File Offset: 0x0015474C
		internal static Dictionary<string, string> GenerateFileTable
		{
			get
			{
				if (BuildManager._theBuildManager._generatedFileTable == null)
				{
					BuildManager._theBuildManager._generatedFileTable = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				}
				return BuildManager._theBuildManager._generatedFileTable;
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x060061E4 RID: 25060 RVA: 0x00156578 File Offset: 0x00154778
		public static IList CodeAssemblies
		{
			get
			{
				BuildManager._theBuildManager.EnsureTopLevelFilesCompiled();
				return BuildManager._theBuildManager._codeAssemblies;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0015658E File Offset: 0x0015478E
		internal static Assembly AppResourcesAssembly
		{
			get
			{
				return BuildManager._theBuildManager._appResourcesAssembly;
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x0015659A File Offset: 0x0015479A
		// (set) Token: 0x060061E7 RID: 25063 RVA: 0x001565A6 File Offset: 0x001547A6
		internal static bool ThrowOnFirstParseError
		{
			get
			{
				return BuildManager._theBuildManager._throwOnFirstParseError;
			}
			set
			{
				BuildManager._theBuildManager._throwOnFirstParseError = value;
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x060061E8 RID: 25064 RVA: 0x001565B3 File Offset: 0x001547B3
		// (set) Token: 0x060061E9 RID: 25065 RVA: 0x001565BF File Offset: 0x001547BF
		internal static bool PerformingPrecompilation
		{
			get
			{
				return BuildManager._theBuildManager._performingPrecompilation;
			}
			set
			{
				BuildManager._theBuildManager._performingPrecompilation = value;
			}
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x060061EA RID: 25066 RVA: 0x001565CC File Offset: 0x001547CC
		// (set) Token: 0x060061EB RID: 25067 RVA: 0x001565D8 File Offset: 0x001547D8
		internal static bool SkipTopLevelCompilationExceptions
		{
			get
			{
				return BuildManager._theBuildManager._skipTopLevelCompilationExceptions;
			}
			set
			{
				BuildManager._theBuildManager._skipTopLevelCompilationExceptions = value;
			}
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x001565E5 File Offset: 0x001547E5
		public static void AddReferencedAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			BuildManager.ThrowIfPreAppStartNotRunning();
			BuildManager.s_dynamicallyAddedReferencedAssembly.Add(assembly);
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x0015660C File Offset: 0x0015480C
		internal static ICollection GetReferencedAssemblies(CompilationSection compConfig, int removeIndex)
		{
			AssemblySet assemblySet = new AssemblySet();
			foreach (object obj in compConfig.Assemblies)
			{
				AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
				Assembly[] array = assemblyInfo.AssemblyInternal;
				if (array == null)
				{
					lock (compConfig)
					{
						array = assemblyInfo.AssemblyInternal;
						if (array == null)
						{
							array = (assemblyInfo.AssemblyInternal = compConfig.LoadAssembly(assemblyInfo));
						}
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						assemblySet.Add(array[i]);
					}
				}
			}
			for (int j = 0; j < removeIndex; j++)
			{
				assemblySet.Add(BuildManager.TheBuildManager.TopLevelReferencedAssemblies[j]);
			}
			foreach (Assembly o in BuildManager.s_dynamicallyAddedReferencedAssembly)
			{
				assemblySet.Add(o);
			}
			return assemblySet;
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x0015674C File Offset: 0x0015494C
		internal static ICollection GetReferencedAssemblies(CompilationSection compConfig)
		{
			AssemblySet assemblySet = AssemblySet.Create(BuildManager.TheBuildManager.TopLevelReferencedAssemblies);
			foreach (object obj in compConfig.Assemblies)
			{
				AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
				Assembly[] array = assemblyInfo.AssemblyInternal;
				if (array == null)
				{
					lock (compConfig)
					{
						array = assemblyInfo.AssemblyInternal;
						if (array == null)
						{
							array = (assemblyInfo.AssemblyInternal = compConfig.LoadAssembly(assemblyInfo));
						}
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						assemblySet.Add(array[i]);
					}
				}
			}
			foreach (Assembly o in BuildManager.s_dynamicallyAddedReferencedAssembly)
			{
				assemblySet.Add(o);
			}
			return assemblySet;
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x00156870 File Offset: 0x00154A70
		public static ICollection GetReferencedAssemblies()
		{
			CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
			BuildManager._theBuildManager.EnsureTopLevelFilesCompiled();
			return BuildManager.GetReferencedAssemblies(compilationAppConfig);
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x00156893 File Offset: 0x00154A93
		public static void AddCompilationDependency(string dependency)
		{
			if (string.IsNullOrEmpty(dependency))
			{
				throw new ArgumentException(SR.GetString("Parameter_can_not_be_empty"), "dependency");
			}
			BuildManager.ThrowIfPreAppStartNotRunning();
			BuildManager._theBuildManager._preAppStartHashCodeCombiner.AddObject(dependency);
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x001568C8 File Offset: 0x00154AC8
		private void Initialize()
		{
			AppDomain.CurrentDomain.AssemblyResolve += this.ResolveAssembly;
			this._globalAsaxVirtualPath = HttpRuntime.AppDomainAppVirtualPathObject.SimpleCombine("global.asax");
			this._webHashFilePath = Path.Combine(HttpRuntime.CodegenDirInternal, "hash\\hash.web");
			this._skipTopLevelCompilationExceptions = BuildManagerHost.InClientBuildManager;
			this.SetPrecompilationInfo(HostingEnvironment.HostingParameters);
			MultiTargetingUtil.EnsureFrameworkNamesInitialized();
			if (this._precompTargetPhysicalDir != null)
			{
				this.FailIfPrecompiledApp();
				this.PrecompilationModeInitialize();
			}
			else if (BuildManager.IsPrecompiledApp)
			{
				this.PrecompiledAppRuntimeModeInitialize();
			}
			else
			{
				this.RegularAppRuntimeModeInitialize();
			}
			this._scriptVirtualDir = Util.GetScriptLocation();
			this._excludedTopLevelDirectories = new CaseInsensitiveStringSet();
			this._excludedTopLevelDirectories.Add("bin");
			this._excludedTopLevelDirectories.Add("App_Code");
			this._excludedTopLevelDirectories.Add("App_GlobalResources");
			this._excludedTopLevelDirectories.Add("App_LocalResources");
			this._excludedTopLevelDirectories.Add("App_WebReferences");
			this._excludedTopLevelDirectories.Add("App_Themes");
			this._forbiddenTopLevelDirectories = new CaseInsensitiveStringSet();
			this._forbiddenTopLevelDirectories.Add("App_Code");
			this._forbiddenTopLevelDirectories.Add("App_GlobalResources");
			this._forbiddenTopLevelDirectories.Add("App_LocalResources");
			this._forbiddenTopLevelDirectories.Add("App_WebReferences");
			this._forbiddenTopLevelDirectories.Add("App_Themes");
			this.LoadLicensesAssemblyIfExists();
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x00156A32 File Offset: 0x00154C32
		private void RegularAppRuntimeModeInitialize()
		{
			this._memoryCache = new MemoryBuildResultCache();
			this._codeGenCache = new StandardDiskBuildResultCache(HttpRuntime.CodegenDirInternal);
			this._caches = new BuildResultCache[]
			{
				this._memoryCache,
				this._codeGenCache
			};
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x00156A70 File Offset: 0x00154C70
		private void PrecompiledAppRuntimeModeInitialize()
		{
			this._memoryCache = new MemoryBuildResultCache();
			BuildResultCache buildResultCache = new PrecompiledSiteDiskBuildResultCache(HttpRuntime.BinDirectoryInternal);
			this._codeGenCache = new StandardDiskBuildResultCache(HttpRuntime.CodegenDirInternal);
			this._caches = new BuildResultCache[]
			{
				this._memoryCache,
				buildResultCache,
				this._codeGenCache
			};
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x00156AC8 File Offset: 0x00154CC8
		private void PrecompilationModeInitialize()
		{
			this._memoryCache = new MemoryBuildResultCache();
			this._codeGenCache = new StandardDiskBuildResultCache(HttpRuntime.CodegenDirInternal);
			string cacheDir = Path.Combine(this._precompTargetPhysicalDir, "bin");
			BuildResultCache buildResultCache;
			if (BuildManager.PrecompilingForUpdatableDeployment)
			{
				buildResultCache = new UpdatablePrecompilerDiskBuildResultCache(cacheDir);
			}
			else
			{
				buildResultCache = new PrecompilerDiskBuildResultCache(cacheDir);
			}
			this._caches = new BuildResultCache[]
			{
				this._memoryCache,
				buildResultCache,
				this._codeGenCache
			};
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x00156B3C File Offset: 0x00154D3C
		private void LoadLicensesAssemblyIfExists()
		{
			string path = Path.Combine(HttpRuntime.BinDirectoryInternal, "App_Licenses.dll");
			if (File.Exists(path))
			{
				Assembly.Load("App_Licenses");
			}
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x00156B6C File Offset: 0x00154D6C
		private static void RestorePortableCompilationOutputSnapshot()
		{
			if (BuildManagerHost.InClientBuildManager || !AppSettings.PortableCompilationOutput || string.IsNullOrEmpty(AppSettings.PortableCompilationOutputSnapshotType))
			{
				return;
			}
			Type type = Type.GetType(AppSettings.PortableCompilationOutputSnapshotType, true);
			object[] args = new object[]
			{
				AppSettings.PortableCompilationOutputSnapshotTypeOptions
			};
			type.InvokeMember("RestoreSnapshot", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, type, args, CultureInfo.InvariantCulture);
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x00156BC8 File Offset: 0x00154DC8
		private long CheckTopLevelFilesUpToDate(long cachedHash)
		{
			bool flag = false;
			long result;
			try
			{
				CompilationLock.GetLock(ref flag);
				result = this.CheckTopLevelFilesUpToDateInternal(cachedHash);
			}
			finally
			{
				if (flag)
				{
					CompilationLock.ReleaseLock();
				}
			}
			return result;
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x00156C04 File Offset: 0x00154E04
		private long CheckTopLevelFilesUpToDateInternal(long cachedHash)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			if (cachedHash != 0L)
			{
				this._codeGenCache.RemoveOldTempFiles();
			}
			hashCodeCombiner.AddObject(HttpRuntime.AppDomainAppPathInternal);
			string fullyQualifiedName = typeof(HttpRuntime).Module.FullyQualifiedName;
			if (!AppSettings.PortableCompilationOutput)
			{
				hashCodeCombiner.AddFile(fullyQualifiedName);
			}
			else
			{
				hashCodeCombiner.AddExistingFileVersion(fullyQualifiedName);
			}
			string machineConfigurationFilePath = HttpConfigurationSystem.MachineConfigurationFilePath;
			if (!AppSettings.PortableCompilationOutput)
			{
				hashCodeCombiner.AddFile(machineConfigurationFilePath);
			}
			else
			{
				hashCodeCombiner.AddFileContentHash(machineConfigurationFilePath);
			}
			string rootWebConfigurationFilePath = HttpConfigurationSystem.RootWebConfigurationFilePath;
			if (!AppSettings.PortableCompilationOutput)
			{
				hashCodeCombiner.AddFile(rootWebConfigurationFilePath);
			}
			else
			{
				hashCodeCombiner.AddFileContentHash(rootWebConfigurationFilePath);
			}
			RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
			CompilationSection compilation = appConfig.Compilation;
			if (!BuildManagerHost.InClientBuildManager)
			{
				this._optimizeCompilations = compilation.OptimizeCompilations;
			}
			if (!BuildManager.OptimizeCompilations)
			{
				string binDirectoryInternal = HttpRuntime.BinDirectoryInternal;
				hashCodeCombiner.AddDirectory(binDirectoryInternal);
				hashCodeCombiner.AddResourcesDirectory(HttpRuntime.ResourcesDirectoryVirtualPath.MapPathInternal());
				hashCodeCombiner.AddDirectory(HttpRuntime.WebRefDirectoryVirtualPath.MapPathInternal());
				hashCodeCombiner.AddDirectory(HttpRuntime.CodeDirectoryVirtualPath.MapPathInternal());
				hashCodeCombiner.AddFile(BuildManager.GlobalAsaxVirtualPath.MapPathInternal());
			}
			hashCodeCombiner.AddObject(compilation.RecompilationHash);
			ProfileSection profile = appConfig.Profile;
			hashCodeCombiner.AddObject(profile.RecompilationHash);
			hashCodeCombiner.AddObject(appConfig.Globalization.FileEncoding);
			TrustSection trust = appConfig.Trust;
			hashCodeCombiner.AddObject(trust.Level);
			hashCodeCombiner.AddObject(trust.OriginUrl);
			hashCodeCombiner.AddObject(ProfileManager.Enabled);
			hashCodeCombiner.AddObject(BuildManager.PrecompilingWithDebugInfo);
			this.CheckCodeGenFiles(hashCodeCombiner.CombinedHash, cachedHash);
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x00156D94 File Offset: 0x00154F94
		private void AfterPreAppStartExecute(Tuple<long, long> currentHash, Tuple<long, long> cachedTopLevelFilesHash)
		{
			bool flag = false;
			try
			{
				CompilationLock.GetLock(ref flag);
				this.CheckCodeGenFiles(currentHash.Item2, cachedTopLevelFilesHash.Item2);
				if (!cachedTopLevelFilesHash.Equals(currentHash))
				{
					this._codeGenCache.SavePreservedSpecialFilesCombinedHash(currentHash);
				}
				HttpRuntime.FileChangesMonitor.StartMonitoringFile(this._webHashFilePath, new FileChangeEventHandler(this.OnWebHashFileChange));
			}
			finally
			{
				if (flag)
				{
					CompilationLock.ReleaseLock();
				}
			}
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x00156E0C File Offset: 0x0015500C
		private void CheckCodeGenFiles(long currentHash, long cachedTopLevelFilesHash)
		{
			BuildManager.s_topLevelHash = currentHash;
			if (BuildManager.PrecompilingForCleanBuild || currentHash != cachedTopLevelFilesHash)
			{
				bool precompilingForCleanBuild = BuildManager.PrecompilingForCleanBuild;
				this._codeGenCache.RemoveAllCodegenFiles();
			}
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x00156E30 File Offset: 0x00155030
		private void OnWebHashFileChange(object sender, FileChangeEvent e)
		{
			string text = FileChangesMonitor.GenerateErrorMessage(e.Action, this._webHashFilePath);
			if (text == null)
			{
				text = "Change in " + this._webHashFilePath;
			}
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.BuildManagerChange, text);
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x00156E6C File Offset: 0x0015506C
		internal static bool IsReservedAssemblyName(string assemblyName)
		{
			return string.Compare(assemblyName, "App_Code", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(assemblyName, "App_GlobalResources", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(assemblyName, "App_WebReferences", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(assemblyName, "App_global.asax", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x00156EA9 File Offset: 0x001550A9
		internal static void ThrowIfPreAppStartNotRunning()
		{
			if (BuildManager.PreStartInitStage != PreStartInitStage.DuringPreStartInit)
			{
				throw new InvalidOperationException(SR.GetString("Method_can_only_be_called_during_pre_start_init"));
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x060061FE RID: 25086 RVA: 0x00156EC3 File Offset: 0x001550C3
		// (set) Token: 0x060061FF RID: 25087 RVA: 0x00156ECA File Offset: 0x001550CA
		internal static PreStartInitStage PreStartInitStage { get; private set; }

		// Token: 0x06006200 RID: 25088 RVA: 0x00156ED4 File Offset: 0x001550D4
		internal static void ExecutePreAppStart()
		{
			BuildManager.RestorePortableCompilationOutputSnapshot();
			string text = Path.Combine(HttpRuntime.CodegenDirInternal, "preStartInitList.web");
			Tuple<long, long> preservedSpecialFilesCombinedHash = BuildManager._theBuildManager._codeGenCache.GetPreservedSpecialFilesCombinedHash();
			long item = BuildManager._theBuildManager.CheckTopLevelFilesUpToDate(preservedSpecialFilesCombinedHash.Item1);
			bool flag = false;
			ISet<string> assemblies = BuildManager.CallPreStartInitMethods(text, out flag);
			Tuple<long, long> currentHash = Tuple.Create<long, long>(item, BuildManager._theBuildManager._preAppStartHashCodeCombiner.CombinedHash);
			BuildManager._theBuildManager.AfterPreAppStartExecute(currentHash, preservedSpecialFilesCombinedHash);
			if (flag)
			{
				BuildManager.SavePreStartInitAssembliesToFile(text, assemblies);
			}
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x00156F54 File Offset: 0x00155154
		private static ISet<string> CallPreStartInitMethods(string preStartInitListPath, out bool isRefAssemblyLoaded)
		{
			isRefAssemblyLoaded = false;
			ISet<string> result;
			using (new ApplicationImpersonationContext())
			{
				ICollection<MethodInfo> collection = null;
				ICollection<Assembly> collection2 = BuildManager.LoadCachedPreAppStartAssemblies(preStartInitListPath);
				if (collection2 != null)
				{
					collection = BuildManager.GetPreStartInitMethodsFromAssemblyCollection(collection2, true);
				}
				if (collection == null)
				{
					bool flag = false;
					try
					{
						CompilationLock.GetLock(ref flag);
						collection = BuildManager.GetPreStartInitMethodsFromReferencedAssemblies();
						isRefAssemblyLoaded = true;
					}
					finally
					{
						if (flag)
						{
							CompilationLock.ReleaseLock();
						}
					}
				}
				BuildManager.InvokePreStartInitMethods(collection);
				result = new HashSet<string>(from m in collection
				select m.DeclaringType.Assembly.FullName, StringComparer.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x00157000 File Offset: 0x00155200
		internal static ISet<string> GetPreStartInitAssembliesFromFile(string path)
		{
			if (FileUtil.FileExists(path))
			{
				try
				{
					return new HashSet<string>(File.ReadAllLines(path), StringComparer.OrdinalIgnoreCase);
				}
				catch
				{
					try
					{
						File.Delete(path);
					}
					catch
					{
					}
				}
			}
			return null;
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x00157058 File Offset: 0x00155258
		internal static void SavePreStartInitAssembliesToFile(string path, ISet<string> assemblies)
		{
			bool flag = false;
			try
			{
				CompilationLock.GetLock(ref flag);
				File.WriteAllLines(path, assemblies);
			}
			catch
			{
				try
				{
					File.Delete(path);
				}
				catch
				{
				}
			}
			finally
			{
				if (flag)
				{
					CompilationLock.ReleaseLock();
				}
			}
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x001570B8 File Offset: 0x001552B8
		internal static ICollection<Assembly> LoadCachedPreAppStartAssemblies(string preStartInitListPath)
		{
			ICollection<Assembly> result;
			try
			{
				ISet<string> preStartInitAssembliesFromFile = BuildManager.GetPreStartInitAssembliesFromFile(preStartInitListPath);
				if (preStartInitAssembliesFromFile == null)
				{
					result = null;
				}
				else
				{
					result = preStartInitAssembliesFromFile.Select(new Func<string, Assembly>(Assembly.Load)).Distinct<Assembly>().ToList<Assembly>();
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x00157108 File Offset: 0x00155308
		private static void InvokePreStartInitMethods(ICollection<MethodInfo> methods)
		{
			BuildManager.PreStartInitStage = PreStartInitStage.DuringPreStartInit;
			try
			{
				BuildManager.InvokePreStartInitMethodsCore(methods, new Func<IDisposable>(HostingEnvironment.SetCultures));
				BuildManager.PreStartInitStage = PreStartInitStage.AfterPreStartInit;
			}
			catch
			{
				BuildManager.PreStartInitStage = PreStartInitStage.BeforePreStartInit;
				throw;
			}
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x00157150 File Offset: 0x00155350
		internal static void InvokePreStartInitMethodsCore(ICollection<MethodInfo> methods, Func<IDisposable> setHostingEnvironmentCultures)
		{
			IEnumerable<MethodInfo> enumerable = methods.Distinct<MethodInfo>();
			enumerable = enumerable.OrderBy((MethodInfo m) => m.DeclaringType.AssemblyQualifiedName, StringComparer.OrdinalIgnoreCase).ThenBy((MethodInfo m) => m.Name, StringComparer.OrdinalIgnoreCase);
			foreach (MethodInfo methodInfo in enumerable)
			{
				try
				{
					using (setHostingEnvironmentCultures())
					{
						methodInfo.Invoke(null, null);
					}
				}
				catch (TargetInvocationException ex)
				{
					string text = (ex.InnerException != null) ? ex.InnerException.Message : string.Empty;
					throw new InvalidOperationException(SR.GetString("Pre_application_start_init_method_threw_exception", new object[]
					{
						methodInfo.Name,
						methodInfo.DeclaringType.FullName,
						text
					}), ex.InnerException);
				}
			}
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x00157280 File Offset: 0x00155480
		private static ICollection<MethodInfo> GetPreStartInitMethodsFromReferencedAssemblies()
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(HttpRuntime.AppDomainAppVirtualPath);
			IEnumerable<Assembly> assemblies = BuildManager.GetReferencedAssemblies(compilationConfig).Cast<Assembly>();
			return BuildManager.GetPreStartInitMethodsFromAssemblyCollection(assemblies, false);
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x001572AC File Offset: 0x001554AC
		internal static ICollection<MethodInfo> GetPreStartInitMethodsFromAssemblyCollection(IEnumerable<Assembly> assemblies, bool buildingFromCache)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (Assembly assembly in assemblies)
			{
				PreApplicationStartMethodAttribute[] array = null;
				try
				{
					array = (PreApplicationStartMethodAttribute[])assembly.GetCustomAttributes(typeof(PreApplicationStartMethodAttribute), true);
				}
				catch
				{
				}
				if (array == null || !array.Any<PreApplicationStartMethodAttribute>())
				{
					if (buildingFromCache)
					{
						return null;
					}
				}
				else
				{
					foreach (PreApplicationStartMethodAttribute preApplicationStartMethodAttribute in array)
					{
						MethodInfo methodInfo = null;
						if (preApplicationStartMethodAttribute.Type != null && !string.IsNullOrEmpty(preApplicationStartMethodAttribute.MethodName) && preApplicationStartMethodAttribute.Type.Assembly == assembly)
						{
							methodInfo = BuildManager.FindPreStartInitMethod(preApplicationStartMethodAttribute.Type, preApplicationStartMethodAttribute.MethodName);
						}
						if (!(methodInfo != null))
						{
							throw new HttpException(SR.GetString("Invalid_PreApplicationStartMethodAttribute_value", new object[]
							{
								assembly.FullName,
								(preApplicationStartMethodAttribute.Type != null) ? preApplicationStartMethodAttribute.Type.FullName : string.Empty,
								preApplicationStartMethodAttribute.MethodName
							}));
						}
						list.Add(methodInfo);
					}
				}
			}
			return list;
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x00157428 File Offset: 0x00155628
		internal static MethodInfo FindPreStartInitMethod(Type type, string methodName)
		{
			MethodInfo result = null;
			if (type.IsPublic)
			{
				result = type.GetMethod(methodName, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null);
			}
			return result;
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x00157454 File Offset: 0x00155654
		private Assembly CompileCodeDirectory(VirtualPath virtualDir, CodeDirectoryType dirType, string assemblyName, StringSet excludedSubdirectories)
		{
			bool isDirectoryAllowed = true;
			if (BuildManager.IsPrecompiledApp)
			{
				isDirectoryAllowed = (this.IsUpdatablePrecompiledAppInternal && dirType == CodeDirectoryType.LocalResources);
			}
			AssemblyReferenceInfo assemblyReferenceInfo = new AssemblyReferenceInfo(this._topLevelReferencedAssemblies.Count);
			this._topLevelAssembliesIndexTable[virtualDir.VirtualPathString] = assemblyReferenceInfo;
			Assembly codeDirectoryAssembly = CodeDirectoryCompiler.GetCodeDirectoryAssembly(virtualDir, dirType, assemblyName, excludedSubdirectories, isDirectoryAllowed);
			if (codeDirectoryAssembly != null)
			{
				assemblyReferenceInfo.Assembly = codeDirectoryAssembly;
				if (dirType != CodeDirectoryType.LocalResources)
				{
					this._topLevelReferencedAssemblies.Add(codeDirectoryAssembly);
					if (dirType == CodeDirectoryType.MainCode || dirType == CodeDirectoryType.SubCode)
					{
						if (this._codeAssemblies == null)
						{
							this._codeAssemblies = new ArrayList();
						}
						this._codeAssemblies.Add(codeDirectoryAssembly);
					}
					if (this._assemblyResolveMapping == null)
					{
						this._assemblyResolveMapping = new Hashtable(StringComparer.OrdinalIgnoreCase);
					}
					this._assemblyResolveMapping[assemblyName] = codeDirectoryAssembly;
					if (dirType == CodeDirectoryType.MainCode)
					{
						this._profileType = ProfileBuildProvider.GetProfileTypeFromAssembly(codeDirectoryAssembly, BuildManager.IsPrecompiledApp);
						this._assemblyResolveMapping["__code"] = codeDirectoryAssembly;
					}
				}
			}
			return codeDirectoryAssembly;
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x00157540 File Offset: 0x00155740
		private void CompileResourcesDirectory()
		{
			VirtualPath resourcesDirectoryVirtualPath = HttpRuntime.ResourcesDirectoryVirtualPath;
			this._appResourcesAssembly = this.CompileCodeDirectory(resourcesDirectoryVirtualPath, CodeDirectoryType.AppResources, "App_GlobalResources", null);
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x00157567 File Offset: 0x00155767
		private void CompileWebRefDirectory()
		{
			this.CompileCodeDirectory(HttpRuntime.WebRefDirectoryVirtualPath, CodeDirectoryType.WebReferences, "App_WebReferences", null);
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x0015757C File Offset: 0x0015577C
		private void EnsureExcludedCodeSubDirectoriesComputed()
		{
			if (this._excludedCodeSubdirectories != null)
			{
				return;
			}
			this._excludedCodeSubdirectories = new CaseInsensitiveStringSet();
			CodeSubDirectoriesCollection codeSubDirectories = CompilationUtil.GetCodeSubDirectories();
			if (codeSubDirectories != null)
			{
				foreach (object obj in codeSubDirectories)
				{
					CodeSubDirectory codeSubDirectory = (CodeSubDirectory)obj;
					this._excludedCodeSubdirectories.Add(codeSubDirectory.DirectoryName);
				}
			}
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x001575F8 File Offset: 0x001557F8
		private void CompileCodeDirectories()
		{
			VirtualPath codeDirectoryVirtualPath = HttpRuntime.CodeDirectoryVirtualPath;
			CodeSubDirectoriesCollection codeSubDirectories = CompilationUtil.GetCodeSubDirectories();
			if (codeSubDirectories != null)
			{
				foreach (object obj in codeSubDirectories)
				{
					CodeSubDirectory codeSubDirectory = (CodeSubDirectory)obj;
					VirtualPath virtualDir = codeDirectoryVirtualPath.SimpleCombineWithDir(codeSubDirectory.DirectoryName);
					string assemblyName = "App_SubCode_" + codeSubDirectory.AssemblyName;
					this.CompileCodeDirectory(virtualDir, CodeDirectoryType.SubCode, assemblyName, null);
				}
			}
			this.EnsureExcludedCodeSubDirectoriesComputed();
			this.CompileCodeDirectory(codeDirectoryVirtualPath, CodeDirectoryType.MainCode, "App_Code", this._excludedCodeSubdirectories);
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x001576A0 File Offset: 0x001558A0
		private void CompileGlobalAsax()
		{
			this._globalAsaxBuildResult = ApplicationBuildProvider.GetGlobalAsaxBuildResult(BuildManager.IsPrecompiledApp);
			HttpApplicationFactory.SetupFileChangeNotifications();
			if (this._globalAsaxBuildResult != null)
			{
				Type type = this._globalAsaxBuildResult.ResultType;
				while (type.Assembly != typeof(HttpRuntime).Assembly)
				{
					this._topLevelReferencedAssemblies.Add(type.Assembly);
					type = type.BaseType;
				}
			}
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x0015770C File Offset: 0x0015590C
		internal static void CallAppInitializeMethod()
		{
			BuildManager._theBuildManager.EnsureTopLevelFilesCompiled();
			CodeDirectoryCompiler.CallAppInitializeMethod();
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x00157720 File Offset: 0x00155920
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void EnsureTopLevelFilesCompiled()
		{
			if (BuildManager.PreStartInitStage != PreStartInitStage.AfterPreStartInit)
			{
				throw new InvalidOperationException(SR.GetString("Method_cannot_be_called_during_pre_start_init"));
			}
			if (this._topLevelFileCompilationException != null && !BuildManager.SkipTopLevelCompilationExceptions)
			{
				this.ReportTopLevelCompilationException();
			}
			if (this._topLevelFilesCompiledStarted)
			{
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				bool flag = false;
				BuildManager._parseErrorReported = false;
				try
				{
					CompilationLock.GetLock(ref flag);
					if (this._topLevelFileCompilationException != null && !BuildManager.SkipTopLevelCompilationExceptions)
					{
						this.ReportTopLevelCompilationException();
					}
					if (!this._topLevelFilesCompiledStarted)
					{
						this._topLevelFilesCompiledStarted = true;
						this._topLevelAssembliesIndexTable = new Dictionary<string, AssemblyReferenceInfo>(StringComparer.OrdinalIgnoreCase);
						this._compilationStage = CompilationStage.TopLevelFiles;
						this.CompileResourcesDirectory();
						this.CompileWebRefDirectory();
						this.CompileCodeDirectories();
						this._compilationStage = CompilationStage.GlobalAsax;
						this.CompileGlobalAsax();
						this._compilationStage = CompilationStage.BrowserCapabilities;
						BrowserCapabilitiesCompiler.GetBrowserCapabilitiesType();
						IFilterResolutionService emptyHttpCapabilitiesBase = HttpCapabilitiesBase.EmptyHttpCapabilitiesBase;
						this._compilationStage = CompilationStage.AfterTopLevelFiles;
					}
				}
				catch (Exception ex)
				{
					this._topLevelFileCompilationException = ex;
					if (!BuildManager.SkipTopLevelCompilationExceptions)
					{
						if (!BuildManager._parseErrorReported && !(ex is HttpCompileException))
						{
							this.ReportTopLevelCompilationException();
						}
						throw;
					}
				}
				finally
				{
					this._topLevelFilesCompiledCompleted = true;
					if (flag)
					{
						CompilationLock.ReleaseLock();
					}
				}
			}
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x00157860 File Offset: 0x00155A60
		private static string GenerateRandomFileName()
		{
			byte[] array = new byte[6];
			RNGCryptoServiceProvider rng = BuildManager._rng;
			lock (rng)
			{
				BuildManager._rng.GetBytes(array);
			}
			string text = Convert.ToBase64String(array).ToLower(CultureInfo.InvariantCulture);
			text = text.Replace('/', '-');
			return text.Replace('+', '_');
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x001578D4 File Offset: 0x00155AD4
		internal static string GenerateRandomAssemblyName(string baseName)
		{
			return BuildManager.GenerateRandomAssemblyName(baseName, true);
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x001578E0 File Offset: 0x00155AE0
		internal static string GenerateRandomAssemblyName(string baseName, bool topLevel)
		{
			if (BuildManager.PrecompilingForDeployment)
			{
				return baseName;
			}
			if (BuildManager.OptimizeCompilations && topLevel)
			{
				return baseName;
			}
			return baseName = baseName + "." + BuildManager.GenerateRandomFileName();
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x00157916 File Offset: 0x00155B16
		private static string GetGeneratedAssemblyBaseName(VirtualPath virtualPath)
		{
			return BuildManager.GetCacheKeyFromVirtualPath(virtualPath);
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x0015791E File Offset: 0x00155B1E
		public static Type GetType(string typeName, bool throwOnError)
		{
			return BuildManager.GetType(typeName, throwOnError, false);
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x00157928 File Offset: 0x00155B28
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			Type type = null;
			if (Util.TypeNameContainsAssembly(typeName))
			{
				type = Type.GetType(typeName, throwOnError, ignoreCase);
				if (type != null)
				{
					return type;
				}
			}
			if (!BuildManager.InitializeBuildManager())
			{
				return Type.GetType(typeName, throwOnError, ignoreCase);
			}
			try
			{
				type = typeof(BuildManager).Assembly.GetType(typeName, false, ignoreCase);
			}
			catch (ArgumentException innerException)
			{
				throw new HttpException(SR.GetString("Invalid_type", new object[]
				{
					typeName
				}), innerException);
			}
			if (type != null)
			{
				return type;
			}
			BuildManager._theBuildManager.EnsureTopLevelFilesCompiled();
			type = Util.GetTypeFromAssemblies(BuildManager.TheBuildManager.TopLevelReferencedAssemblies, typeName, ignoreCase);
			if (type != null)
			{
				return type;
			}
			IEnumerable<Assembly> assembliesForAppLevel = BuildManager.GetAssembliesForAppLevel();
			type = Util.GetTypeFromAssemblies(assembliesForAppLevel, typeName, ignoreCase);
			if (type == null && throwOnError)
			{
				throw new HttpException(SR.GetString("Invalid_type", new object[]
				{
					typeName
				}));
			}
			return type;
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x00157A14 File Offset: 0x00155C14
		private static IEnumerable<Assembly> GetAssembliesForAppLevel()
		{
			CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
			AssemblyCollection assemblies = compilationAppConfig.Assemblies;
			if (assemblies == null)
			{
				return BuildManager.s_dynamicallyAddedReferencedAssembly.OfType<Assembly>();
			}
			return assemblies.Cast<AssemblyInfo>().SelectMany((AssemblyInfo ai) => ai.AssemblyInternal).Union(BuildManager.s_dynamicallyAddedReferencedAssembly).Distinct<Assembly>();
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x00157A75 File Offset: 0x00155C75
		internal static Type GetTypeFromCodeAssembly(string typeName, bool ignoreCase)
		{
			if (BuildManager.CodeAssemblies == null)
			{
				return null;
			}
			return Util.GetTypeFromAssemblies(BuildManager.CodeAssemblies, typeName, ignoreCase);
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x00157A8C File Offset: 0x00155C8C
		internal static BuildProvider CreateBuildProvider(VirtualPath virtualPath, CompilationSection compConfig, ICollection referencedAssemblies, bool failIfUnknown)
		{
			return BuildManager.CreateBuildProvider(virtualPath, BuildProviderAppliesTo.Web, compConfig, referencedAssemblies, failIfUnknown);
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x00157A98 File Offset: 0x00155C98
		internal static BuildProvider CreateBuildProvider(VirtualPath virtualPath, BuildProviderAppliesTo neededFor, CompilationSection compConfig, ICollection referencedAssemblies, bool failIfUnknown)
		{
			string extension = virtualPath.Extension;
			Type buildProviderTypeFromExtension = CompilationUtil.GetBuildProviderTypeFromExtension(compConfig, extension, neededFor, failIfUnknown);
			if (buildProviderTypeFromExtension == null)
			{
				return null;
			}
			object obj = HttpRuntime.CreatePublicInstanceByWebObjectActivator(buildProviderTypeFromExtension);
			BuildProvider buildProvider = (BuildProvider)obj;
			buildProvider.SetVirtualPath(virtualPath);
			buildProvider.SetReferencedAssemblies(referencedAssemblies);
			return buildProvider;
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x00157AE0 File Offset: 0x00155CE0
		internal static void AddFolderLevelBuildProviders(BuildProviderSet buildProviders, VirtualPath virtualPath, FolderLevelBuildProviderAppliesTo appliesTo, CompilationSection compConfig, ICollection referencedAssemblies)
		{
			if (buildProviders == null)
			{
				return;
			}
			List<Type> folderLevelBuildProviderTypes = CompilationUtil.GetFolderLevelBuildProviderTypes(compConfig, appliesTo);
			if (folderLevelBuildProviderTypes != null)
			{
				foreach (Type type in folderLevelBuildProviderTypes)
				{
					object obj = HttpRuntime.CreatePublicInstanceByWebObjectActivator(type);
					BuildProvider buildProvider = (BuildProvider)obj;
					buildProvider.SetVirtualPath(virtualPath);
					buildProvider.SetReferencedAssemblies(referencedAssemblies);
					buildProviders.Add(buildProvider);
				}
			}
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x00157B60 File Offset: 0x00155D60
		internal static void ValidateCodeFileVirtualPath(VirtualPath virtualPath)
		{
			BuildManager._theBuildManager.ValidateVirtualPathInternal(virtualPath, false, true);
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x00157B70 File Offset: 0x00155D70
		private void ValidateVirtualPathInternal(VirtualPath virtualPath, bool allowCrossApp, bool codeFile)
		{
			if (!allowCrossApp)
			{
				virtualPath.FailIfNotWithinAppRoot();
			}
			else if (!virtualPath.IsWithinAppRoot)
			{
				return;
			}
			if (HttpRuntime.AppDomainAppVirtualPathObject == virtualPath)
			{
				return;
			}
			int length = HttpRuntime.AppDomainAppVirtualPathString.Length;
			string virtualPathString = virtualPath.VirtualPathString;
			if (virtualPathString.Length < length)
			{
				return;
			}
			int num = virtualPathString.IndexOf('/', length);
			if (num < 0)
			{
				return;
			}
			string text = virtualPathString.Substring(length, num - length);
			if (this._forbiddenTopLevelDirectories.Contains(text))
			{
				throw new HttpException(SR.GetString("Illegal_special_dir", new object[]
				{
					virtualPathString,
					text
				}));
			}
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x00157C04 File Offset: 0x00155E04
		internal static long GetBuildResultHashCodeIfCached(HttpContext context, string virtualPath)
		{
			BuildResult vpathBuildResult = BuildManager.GetVPathBuildResult(context, VirtualPath.Create(virtualPath), true, false);
			if (vpathBuildResult == null)
			{
				return 0L;
			}
			string virtualPathDependenciesHash = vpathBuildResult.VirtualPathDependenciesHash;
			return vpathBuildResult.ComputeHashCode(BuildManager.s_topLevelHash, (long)StringUtil.GetStringHashCode(virtualPathDependenciesHash));
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x00157C3F File Offset: 0x00155E3F
		internal static BuildResult GetVPathBuildResult(VirtualPath virtualPath)
		{
			return BuildManager.GetVPathBuildResult(null, virtualPath, false, false, false, true);
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x00157C4C File Offset: 0x00155E4C
		internal static BuildResult GetVPathBuildResult(HttpContext context, VirtualPath virtualPath)
		{
			return BuildManager.GetVPathBuildResult(context, virtualPath, false, false, false, true);
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x00157C59 File Offset: 0x00155E59
		internal static BuildResult GetVPathBuildResult(HttpContext context, VirtualPath virtualPath, bool noBuild, bool allowCrossApp)
		{
			return BuildManager.GetVPathBuildResult(context, virtualPath, noBuild, allowCrossApp, false, true);
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x00157C66 File Offset: 0x00155E66
		internal static BuildResult GetVPathBuildResult(HttpContext context, VirtualPath virtualPath, bool noBuild, bool allowCrossApp, bool allowBuildInPrecompile, bool ensureIsUpToDate = true)
		{
			if (HttpRuntime.IsFullTrust)
			{
				return BuildManager.GetVPathBuildResultWithNoAssert(context, virtualPath, noBuild, allowCrossApp, allowBuildInPrecompile, true, ensureIsUpToDate);
			}
			return BuildManager.GetVPathBuildResultWithAssert(context, virtualPath, noBuild, allowCrossApp, allowBuildInPrecompile, true, ensureIsUpToDate);
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x00157C8C File Offset: 0x00155E8C
		internal static BuildResult GetVPathBuildResultWithAssert(HttpContext context, VirtualPath virtualPath, bool noBuild, bool allowCrossApp, bool allowBuildInPrecompile)
		{
			return BuildManager.GetVPathBuildResultWithAssert(context, virtualPath, noBuild, allowCrossApp, allowBuildInPrecompile, true, true);
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x00157C9B File Offset: 0x00155E9B
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static BuildResult GetVPathBuildResultWithAssert(HttpContext context, VirtualPath virtualPath, bool noBuild, bool allowCrossApp, bool allowBuildInPrecompile, bool throwIfNotFound, bool ensureIsUpToDate = true)
		{
			return BuildManager.GetVPathBuildResultWithNoAssert(context, virtualPath, noBuild, allowCrossApp, allowBuildInPrecompile, throwIfNotFound, ensureIsUpToDate);
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x00157CAC File Offset: 0x00155EAC
		internal static BuildResult GetVPathBuildResultWithNoAssert(HttpContext context, VirtualPath virtualPath, bool noBuild, bool allowCrossApp, bool allowBuildInPrecompile)
		{
			return BuildManager.GetVPathBuildResultWithNoAssert(context, virtualPath, noBuild, allowCrossApp, allowBuildInPrecompile, true, true);
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x00157CBC File Offset: 0x00155EBC
		internal static BuildResult GetVPathBuildResultWithNoAssert(HttpContext context, VirtualPath virtualPath, bool noBuild, bool allowCrossApp, bool allowBuildInPrecompile, bool throwIfNotFound, bool ensureIsUpToDate = true)
		{
			BuildResult vpathBuildResultInternal;
			using (new ApplicationImpersonationContext())
			{
				vpathBuildResultInternal = BuildManager._theBuildManager.GetVPathBuildResultInternal(virtualPath, noBuild, allowCrossApp, allowBuildInPrecompile, throwIfNotFound, ensureIsUpToDate);
			}
			return vpathBuildResultInternal;
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x00157D00 File Offset: 0x00155F00
		private BuildResult GetVPathBuildResultInternal(VirtualPath virtualPath, bool noBuild, bool allowCrossApp, bool allowBuildInPrecompile, bool throwIfNotFound, bool ensureIsUpToDate = true)
		{
			if (this._compilationStage == CompilationStage.TopLevelFiles)
			{
				throw new HttpException(SR.GetString("Too_early_for_webfile", new object[]
				{
					virtualPath
				}));
			}
			BuildResult buildResult = this.GetVPathBuildResultFromCacheInternal(virtualPath, ensureIsUpToDate);
			if (buildResult != null)
			{
				return buildResult;
			}
			if (noBuild)
			{
				return null;
			}
			this.ValidateVirtualPathInternal(virtualPath, allowCrossApp, false);
			if (throwIfNotFound)
			{
				Util.CheckVirtualFileExists(virtualPath);
			}
			else if (!virtualPath.FileExists())
			{
				return null;
			}
			if (this.IsNonUpdatablePrecompiledApp && !allowBuildInPrecompile)
			{
				throw new HttpException(SR.GetString("Cant_update_precompiled_app", new object[]
				{
					virtualPath
				}));
			}
			bool flag = false;
			try
			{
				CompilationLock.GetLock(ref flag);
				buildResult = this.GetVPathBuildResultFromCacheInternal(virtualPath, ensureIsUpToDate);
				if (buildResult != null)
				{
					return buildResult;
				}
				VirtualPathSet virtualPathSet = CallContext.GetData("CircRefChk") as VirtualPathSet;
				if (virtualPathSet == null)
				{
					virtualPathSet = new VirtualPathSet();
					CallContext.SetData("CircRefChk", virtualPathSet);
				}
				if (virtualPathSet.Contains(virtualPath))
				{
					throw new HttpException(SR.GetString("Circular_include"));
				}
				virtualPathSet.Add(virtualPath);
				try
				{
					this.EnsureTopLevelFilesCompiled();
					buildResult = this.CompileWebFile(virtualPath);
				}
				finally
				{
					virtualPathSet.Remove(virtualPath);
				}
			}
			finally
			{
				if (flag)
				{
					CompilationLock.ReleaseLock();
				}
			}
			return buildResult;
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x00157E28 File Offset: 0x00156028
		private BuildResult CompileWebFile(VirtualPath virtualPath)
		{
			BuildResult buildResult = null;
			string text = null;
			if (this._topLevelFilesCompiledCompleted)
			{
				VirtualPath parent = virtualPath.Parent;
				if (this.IsBatchEnabledForDirectory(parent))
				{
					this.BatchCompileWebDirectory(null, parent, true);
					text = BuildManager.GetCacheKeyFromVirtualPath(virtualPath);
					buildResult = this._memoryCache.GetBuildResult(text);
					if (buildResult == null && DelayLoadType.Enabled)
					{
						buildResult = BuildManager.GetBuildResultFromCache(text);
					}
					if (buildResult != null)
					{
						if (buildResult is BuildResultCompileError)
						{
							throw ((BuildResultCompileError)buildResult).CompileException;
						}
						return buildResult;
					}
				}
			}
			DateTime utcNow = DateTime.UtcNow;
			string outputAssemblyName = "App_Web_" + BuildManager.GenerateRandomAssemblyName(BuildManager.GetGeneratedAssemblyBaseName(virtualPath), false);
			BuildProvidersCompiler buildProvidersCompiler = new BuildProvidersCompiler(virtualPath, outputAssemblyName);
			BuildProvider buildProvider = BuildManager.CreateBuildProvider(virtualPath, buildProvidersCompiler.CompConfig, buildProvidersCompiler.ReferencedAssemblies, true);
			buildProvidersCompiler.SetBuildProviders(new SingleObjectCollection(buildProvider));
			try
			{
				CompilerResults results = buildProvidersCompiler.PerformBuild();
				buildResult = buildProvider.GetBuildResult(results);
			}
			catch (HttpCompileException ex)
			{
				if (ex.DontCache)
				{
					throw;
				}
				buildResult = new BuildResultCompileError(virtualPath, ex);
				buildProvider.SetBuildResultDependencies(buildResult);
				ex.VirtualPathDependencies = buildProvider.VirtualPathDependencies;
				this.CacheVPathBuildResultInternal(virtualPath, buildResult, utcNow);
				ex.DontCache = true;
				throw;
			}
			if (buildResult == null)
			{
				return null;
			}
			this.CacheVPathBuildResultInternal(virtualPath, buildResult, utcNow);
			if (!this._precompilingApp && BuildResultCompiledType.UsesDelayLoadType(buildResult))
			{
				if (text == null)
				{
					text = BuildManager.GetCacheKeyFromVirtualPath(virtualPath);
				}
				buildResult = BuildManager.GetBuildResultFromCache(text);
			}
			return buildResult;
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x00157F80 File Offset: 0x00156180
		private void EnsureFirstTimeDirectoryInit(VirtualPath virtualDir)
		{
			if (BuildManager.PrecompilingForUpdatableDeployment)
			{
				return;
			}
			if (virtualDir == null)
			{
				return;
			}
			if (this._localResourcesAssemblies.Contains(virtualDir))
			{
				return;
			}
			if (!virtualDir.IsWithinAppRoot)
			{
				return;
			}
			VirtualPath virtualPath = virtualDir.SimpleCombineWithDir("App_LocalResources");
			bool flag;
			try
			{
				flag = virtualPath.DirectoryExists();
			}
			catch
			{
				this._localResourcesAssemblies[virtualDir] = null;
				return;
			}
			try
			{
				HttpRuntime.StartListeningToLocalResourcesDirectory(virtualPath);
			}
			catch
			{
				if (flag)
				{
					throw;
				}
			}
			Assembly value = null;
			if (flag)
			{
				string localResourcesAssemblyName = BuildManager.GetLocalResourcesAssemblyName(virtualDir);
				bool flag2 = false;
				try
				{
					CompilationLock.GetLock(ref flag2);
					value = this.CompileCodeDirectory(virtualPath, CodeDirectoryType.LocalResources, localResourcesAssemblyName, null);
				}
				finally
				{
					if (flag2)
					{
						CompilationLock.ReleaseLock();
					}
				}
			}
			this._localResourcesAssemblies[virtualDir] = value;
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x00158050 File Offset: 0x00156250
		private void EnsureFirstTimeDirectoryInitForDependencies(ICollection dependencies)
		{
			foreach (object obj in dependencies)
			{
				string virtualPath = (string)obj;
				VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
				VirtualPath parent = virtualPath2.Parent;
				this.EnsureFirstTimeDirectoryInit(parent);
			}
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x001580B8 File Offset: 0x001562B8
		internal static Assembly GetLocalResourcesAssembly(VirtualPath virtualDir)
		{
			return (Assembly)BuildManager._theBuildManager._localResourcesAssemblies[virtualDir];
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x001580CF File Offset: 0x001562CF
		internal static string GetLocalResourcesAssemblyName(VirtualPath virtualDir)
		{
			return "App_LocalResources." + BuildManager.GetGeneratedAssemblyBaseName(virtualDir);
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x0600622E RID: 25134 RVA: 0x001580E1 File Offset: 0x001562E1
		// (set) Token: 0x0600622F RID: 25135 RVA: 0x001580E8 File Offset: 0x001562E8
		public static bool? BatchCompilationEnabled
		{
			get
			{
				return BuildManager.s_batchCompilationEnabled;
			}
			set
			{
				BuildManager.ThrowIfPreAppStartNotRunning();
				BuildManager.s_batchCompilationEnabled = value;
			}
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x001580F8 File Offset: 0x001562F8
		private bool IsBatchEnabledForDirectory(VirtualPath virtualDir)
		{
			if (BuildManager.CompileWithFixedAssemblyNames)
			{
				return false;
			}
			if (BuildManager.PrecompilingForDeployment)
			{
				return true;
			}
			if (BuildManagerHost.InClientBuildManager && !BuildManager.PerformingPrecompilation)
			{
				return false;
			}
			if (BuildManager.BatchCompilationEnabled != null)
			{
				return BuildManager.BatchCompilationEnabled.Value;
			}
			return CompilationUtil.IsBatchingEnabled(virtualDir.VirtualPathString);
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x00158150 File Offset: 0x00156350
		private bool BatchCompileWebDirectory(VirtualDirectory vdir, VirtualPath virtualDir, bool ignoreErrors)
		{
			if (virtualDir == null)
			{
				virtualDir = vdir.VirtualPathObject;
			}
			if (vdir == null)
			{
				vdir = HostingEnvironment.VirtualPathProvider.GetDirectory(virtualDir);
			}
			CaseInsensitiveStringSet caseInsensitiveStringSet = CallContext.GetData("BatchCompileChk") as CaseInsensitiveStringSet;
			if (caseInsensitiveStringSet == null)
			{
				caseInsensitiveStringSet = new CaseInsensitiveStringSet();
				CallContext.SetData("BatchCompileChk", caseInsensitiveStringSet);
			}
			if (caseInsensitiveStringSet.Contains(vdir.VirtualPath))
			{
				return false;
			}
			caseInsensitiveStringSet.Add(vdir.VirtualPath);
			if (this._precompilingApp)
			{
				ignoreErrors = false;
			}
			return this.BatchCompileWebDirectoryInternal(vdir, ignoreErrors);
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x001581D4 File Offset: 0x001563D4
		private bool BatchCompileWebDirectoryInternal(VirtualDirectory vdir, bool ignoreErrors)
		{
			WebDirectoryBatchCompiler webDirectoryBatchCompiler = new WebDirectoryBatchCompiler(vdir);
			if (ignoreErrors)
			{
				webDirectoryBatchCompiler.SetIgnoreErrors();
				try
				{
					webDirectoryBatchCompiler.Process();
					return true;
				}
				catch
				{
					return false;
				}
			}
			webDirectoryBatchCompiler.Process();
			return true;
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x00158218 File Offset: 0x00156418
		public static Type GetGlobalAsaxType()
		{
			return BuildManager._theBuildManager.GetGlobalAsaxTypeInternal();
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x00158224 File Offset: 0x00156424
		private Type GetGlobalAsaxTypeInternal()
		{
			this.EnsureTopLevelFilesCompiled();
			if (this._globalAsaxBuildResult == null)
			{
				return PageParser.DefaultApplicationBaseType ?? typeof(HttpApplication);
			}
			return this._globalAsaxBuildResult.ResultType;
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x00158253 File Offset: 0x00156453
		internal static BuildResultCompiledGlobalAsaxType GetGlobalAsaxBuildResult()
		{
			return BuildManager._theBuildManager.GetGlobalAsaxBuildResultInternal();
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x0015825F File Offset: 0x0015645F
		private BuildResultCompiledGlobalAsaxType GetGlobalAsaxBuildResultInternal()
		{
			this.EnsureTopLevelFilesCompiled();
			return this._globalAsaxBuildResult;
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x00158270 File Offset: 0x00156470
		internal string[] GetCodeDirectories()
		{
			VirtualPath codeDirectoryVirtualPath = HttpRuntime.CodeDirectoryVirtualPath;
			if (!codeDirectoryVirtualPath.DirectoryExists())
			{
				return new string[0];
			}
			CodeSubDirectoriesCollection codeSubDirectories = CompilationUtil.GetCodeSubDirectories();
			int num = 1;
			if (codeSubDirectories != null)
			{
				num += codeSubDirectories.Count;
			}
			string[] array = new string[num];
			int num2 = 0;
			if (codeSubDirectories != null)
			{
				foreach (object obj in codeSubDirectories)
				{
					CodeSubDirectory codeSubDirectory = (CodeSubDirectory)obj;
					VirtualPath virtualPath = codeDirectoryVirtualPath.SimpleCombineWithDir(codeSubDirectory.DirectoryName);
					array[num2++] = virtualPath.VirtualPathString;
				}
			}
			array[num2++] = codeDirectoryVirtualPath.VirtualPathString;
			return array;
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x0015832C File Offset: 0x0015652C
		internal void GetCodeDirectoryInformation(VirtualPath virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
		{
			CompilationStage compilationStage = this._compilationStage;
			try
			{
				this.GetCodeDirectoryInformationInternal(virtualCodeDir, out codeDomProviderType, out compilerParameters, out generatedFilesDir);
			}
			finally
			{
				this._compilationStage = compilationStage;
			}
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x00158368 File Offset: 0x00156568
		private void GetCodeDirectoryInformationInternal(VirtualPath virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
		{
			StringSet excludedSubdirectories = null;
			CodeDirectoryType dirType;
			if (virtualCodeDir == HttpRuntime.CodeDirectoryVirtualPath)
			{
				this.EnsureExcludedCodeSubDirectoriesComputed();
				excludedSubdirectories = this._excludedCodeSubdirectories;
				dirType = CodeDirectoryType.MainCode;
				this._compilationStage = CompilationStage.TopLevelFiles;
			}
			else if (virtualCodeDir == HttpRuntime.ResourcesDirectoryVirtualPath)
			{
				dirType = CodeDirectoryType.AppResources;
				this._compilationStage = CompilationStage.TopLevelFiles;
			}
			else if (string.Compare(virtualCodeDir.VirtualPathString, 0, HttpRuntime.WebRefDirectoryVirtualPath.VirtualPathString, 0, HttpRuntime.WebRefDirectoryVirtualPath.VirtualPathString.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				virtualCodeDir = HttpRuntime.WebRefDirectoryVirtualPath;
				dirType = CodeDirectoryType.WebReferences;
				this._compilationStage = CompilationStage.TopLevelFiles;
			}
			else if (string.Compare(virtualCodeDir.FileName, "App_LocalResources", StringComparison.OrdinalIgnoreCase) == 0)
			{
				dirType = CodeDirectoryType.LocalResources;
				this._compilationStage = CompilationStage.AfterTopLevelFiles;
			}
			else
			{
				dirType = CodeDirectoryType.SubCode;
				this._compilationStage = CompilationStage.TopLevelFiles;
			}
			AssemblyReferenceInfo assemblyReferenceInfo = BuildManager.TheBuildManager.TopLevelAssembliesIndexTable[virtualCodeDir.VirtualPathString];
			if (assemblyReferenceInfo == null)
			{
				throw new InvalidOperationException(SR.GetString("Invalid_CodeSubDirectory_Not_Exist", new object[]
				{
					virtualCodeDir
				}));
			}
			CodeDirectoryCompiler.GetCodeDirectoryInformation(virtualCodeDir, dirType, excludedSubdirectories, assemblyReferenceInfo.ReferenceIndex, out codeDomProviderType, out compilerParameters, out generatedFilesDir);
			Assembly assembly = assemblyReferenceInfo.Assembly;
			if (assembly != null)
			{
				compilerParameters.OutputAssembly = assembly.Location;
			}
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x00158478 File Offset: 0x00156678
		internal static Type GetProfileType()
		{
			return BuildManager._theBuildManager.GetProfileTypeInternal();
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x00158484 File Offset: 0x00156684
		private Type GetProfileTypeInternal()
		{
			this.EnsureTopLevelFilesCompiled();
			return this._profileType;
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x00158494 File Offset: 0x00156694
		public static ICollection GetVirtualPathDependencies(string virtualPath)
		{
			CompilationSection compilation = RuntimeConfig.GetRootWebConfig().Compilation;
			BuildProvider buildProvider = BuildManager.CreateBuildProvider(VirtualPath.Create(virtualPath), compilation, null, false);
			if (buildProvider == null)
			{
				return null;
			}
			return buildProvider.GetBuildResultVirtualPathDependencies();
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x001584C8 File Offset: 0x001566C8
		internal static string GetCacheKeyFromVirtualPath(VirtualPath virtualPath)
		{
			bool flag;
			return BuildManager.GetCacheKeyFromVirtualPath(virtualPath, out flag);
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x001584E0 File Offset: 0x001566E0
		private static string GetCacheKeyFromVirtualPath(VirtualPath virtualPath, out bool keyFromVPP)
		{
			string text = virtualPath.GetCacheKey();
			if (text != null)
			{
				keyFromVPP = true;
				return text.ToLowerInvariant();
			}
			keyFromVPP = false;
			text = (BuildManager._keyCache[virtualPath.VirtualPathString] as string);
			if (text != null)
			{
				return text;
			}
			text = BuildManager.GetCacheKeyFromVirtualPathInternal(virtualPath);
			BuildManager._keyCache[virtualPath.VirtualPathString] = text;
			return text;
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x00158538 File Offset: 0x00156738
		private static string GetCacheKeyFromVirtualPathInternal(VirtualPath virtualPath)
		{
			string text = virtualPath.AppRelativeVirtualPathString.ToLowerInvariant();
			text = UrlPath.RemoveSlashFromPathIfNeeded(text);
			int num = text.LastIndexOf('/');
			if (text == "~")
			{
				return "root";
			}
			string str = text.Substring(num + 1);
			string s;
			if (num <= 0)
			{
				s = "/";
			}
			else
			{
				s = text.Substring(0, num);
			}
			return str + "." + StringUtil.GetStringHashCode(s).ToString("x", CultureInfo.InvariantCulture);
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x001585B7 File Offset: 0x001567B7
		internal static BuildResult GetVPathBuildResultFromCache(VirtualPath virtualPath)
		{
			return BuildManager.TheBuildManager.GetVPathBuildResultFromCacheInternal(virtualPath, true);
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x001585C8 File Offset: 0x001567C8
		private BuildResult GetVPathBuildResultFromCacheInternal(VirtualPath virtualPath, bool ensureIsUpToDate = true)
		{
			bool keyFromVPP;
			string cacheKeyFromVirtualPath = BuildManager.GetCacheKeyFromVirtualPath(virtualPath, out keyFromVPP);
			return this.GetBuildResultFromCacheInternal(cacheKeyFromVirtualPath, keyFromVPP, virtualPath, 0L, ensureIsUpToDate);
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x001585EA File Offset: 0x001567EA
		internal static BuildResult GetBuildResultFromCache(string cacheKey)
		{
			return BuildManager._theBuildManager.GetBuildResultFromCacheInternal(cacheKey, false, null, 0L, true);
		}

		// Token: 0x06006243 RID: 25155 RVA: 0x001585FC File Offset: 0x001567FC
		internal static BuildResult GetBuildResultFromCache(string cacheKey, VirtualPath virtualPath)
		{
			return BuildManager._theBuildManager.GetBuildResultFromCacheInternal(cacheKey, false, virtualPath, 0L, true);
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x00158610 File Offset: 0x00156810
		private BuildResult GetBuildResultFromCacheInternal(string cacheKey, bool keyFromVPP, VirtualPath virtualPath, long hashCode, bool ensureIsUpToDate = true)
		{
			if (!BuildManager._theBuildManagerInitialized)
			{
				return null;
			}
			BuildResult buildResult = this._memoryCache.GetBuildResult(cacheKey, virtualPath, hashCode, ensureIsUpToDate);
			if (buildResult != null)
			{
				return this.PostProcessFoundBuildResult(buildResult, keyFromVPP, virtualPath);
			}
			BuildResult result;
			lock (this)
			{
				int i = 0;
				while (i < this._caches.Length)
				{
					buildResult = this._caches[i].GetBuildResult(cacheKey, virtualPath, hashCode, ensureIsUpToDate);
					if (buildResult != null)
					{
						if (this._compilationStage == CompilationStage.AfterTopLevelFiles && buildResult.VirtualPathDependencies != null)
						{
							this.EnsureFirstTimeDirectoryInitForDependencies(buildResult.VirtualPathDependencies);
							break;
						}
						break;
					}
					else
					{
						if (i == 0 && virtualPath != null)
						{
							VirtualPath parent = virtualPath.Parent;
							this.EnsureFirstTimeDirectoryInit(parent);
						}
						i++;
					}
				}
				if (buildResult == null)
				{
					result = null;
				}
				else
				{
					buildResult = this.PostProcessFoundBuildResult(buildResult, keyFromVPP, virtualPath);
					if (buildResult == null)
					{
						result = null;
					}
					else
					{
						for (int j = 0; j < i; j++)
						{
							this._caches[j].CacheBuildResult(cacheKey, buildResult, DateTime.UtcNow);
						}
						result = buildResult;
					}
				}
			}
			return result;
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x00158718 File Offset: 0x00156918
		private BuildResult PostProcessFoundBuildResult(BuildResult result, bool keyFromVPP, VirtualPath virtualPath)
		{
			if (!keyFromVPP && virtualPath != null)
			{
				if (AppSettings.VerifyVirtualPathFromDiskCache)
				{
					string text = virtualPath.AppRelativeVirtualPathString;
					if (text.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(1);
					}
					if (!result.VirtualPath.VirtualPathString.EndsWith(text, StringComparison.OrdinalIgnoreCase))
					{
						return null;
					}
				}
				else if (virtualPath != result.VirtualPath)
				{
					return null;
				}
			}
			if (result is BuildResultCompileError)
			{
				HttpCompileException compileException = ((BuildResultCompileError)result).CompileException;
				if (!BuildManager.PerformingPrecompilation)
				{
					this.ReportErrorsFromException(compileException);
				}
				throw compileException;
			}
			return result;
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x001587A0 File Offset: 0x001569A0
		internal static bool CacheVPathBuildResult(VirtualPath virtualPath, BuildResult result, DateTime utcStart)
		{
			return BuildManager._theBuildManager.CacheVPathBuildResultInternal(virtualPath, result, utcStart);
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x001587B0 File Offset: 0x001569B0
		private bool CacheVPathBuildResultInternal(VirtualPath virtualPath, BuildResult result, DateTime utcStart)
		{
			string cacheKeyFromVirtualPath = BuildManager.GetCacheKeyFromVirtualPath(virtualPath);
			return BuildManager.CacheBuildResult(cacheKeyFromVirtualPath, result, utcStart);
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x001587CC File Offset: 0x001569CC
		internal static bool CacheBuildResult(string cacheKey, BuildResult result, DateTime utcStart)
		{
			return BuildManager._theBuildManager.CacheBuildResultInternal(cacheKey, result, 0L, utcStart);
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x001587E0 File Offset: 0x001569E0
		private bool CacheBuildResultInternal(string cacheKey, BuildResult result, long hashCode, DateTime utcStart)
		{
			result.EnsureVirtualPathDependenciesHashComputed();
			for (int i = 0; i < this._caches.Length; i++)
			{
				this._caches[i].CacheBuildResult(cacheKey, result, hashCode, utcStart);
			}
			if (!TimeStampChecker.CheckFilesStillValid(cacheKey, result.VirtualPathDependencies))
			{
				this._memoryCache.RemoveAssemblyAndCleanupDependencies(result as BuildResultCompiledAssemblyBase);
				return false;
			}
			return true;
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x0015883C File Offset: 0x00156A3C
		internal void SetPrecompilationInfo(HostingEnvironmentParameters hostingParameters)
		{
			if (hostingParameters == null || hostingParameters.ClientBuildManagerParameter == null)
			{
				return;
			}
			this._precompilationFlags = hostingParameters.ClientBuildManagerParameter.PrecompilationFlags;
			this._strongNameKeyFile = hostingParameters.ClientBuildManagerParameter.StrongNameKeyFile;
			this._strongNameKeyContainer = hostingParameters.ClientBuildManagerParameter.StrongNameKeyContainer;
			this._precompTargetPhysicalDir = hostingParameters.PrecompilationTargetPhysicalDirectory;
			if (this._precompTargetPhysicalDir == null)
			{
				return;
			}
			if (Util.IsNonEmptyDirectory(this._precompTargetPhysicalDir))
			{
				if ((this._precompilationFlags & PrecompilationFlags.OverwriteTarget) == PrecompilationFlags.Default)
				{
					throw new HttpException(SR.GetString("Dir_not_empty"));
				}
				bool flag;
				if (!BuildManager.ReadPrecompMarkerFile(this._precompTargetPhysicalDir, out flag))
				{
					throw new HttpException(SR.GetString("Dir_not_empty_not_precomp"));
				}
				if (!this.DeletePrecompTargetDirectory())
				{
					Thread.Sleep(250);
					if (!this.DeletePrecompTargetDirectory())
					{
						Thread.Sleep(1000);
						if (!this.DeletePrecompTargetDirectory())
						{
							throw new HttpException(SR.GetString("Cant_delete_dir"));
						}
					}
				}
			}
			this.CreatePrecompMarkerFile();
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x00158928 File Offset: 0x00156B28
		private bool DeletePrecompTargetDirectory()
		{
			try
			{
				if (this._precompTargetPhysicalDir != null)
				{
					foreach (object obj in ((IEnumerable)FileEnumerator.Create(this._precompTargetPhysicalDir)))
					{
						FileData fileData = (FileData)obj;
						if (fileData.IsDirectory)
						{
							Directory.Delete(fileData.FullName, true);
						}
						else
						{
							Util.DeleteFileNoException(fileData.FullName);
						}
					}
				}
			}
			catch
			{
			}
			return !Util.IsNonEmptyDirectory(this._precompTargetPhysicalDir);
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x001589C8 File Offset: 0x00156BC8
		private void FailIfPrecompiledApp()
		{
			if (BuildManager.IsPrecompiledApp)
			{
				throw new HttpException(SR.GetString("Already_precomp"));
			}
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x001589E4 File Offset: 0x00156BE4
		internal void PrecompileApp(ClientBuildManagerCallback callback, IEnumerable<string> excludedVirtualPaths)
		{
			bool skipTopLevelCompilationExceptions = BuildManager.SkipTopLevelCompilationExceptions;
			try
			{
				this._cbmCallback = callback;
				BuildManager.ThrowOnFirstParseError = false;
				BuildManager.SkipTopLevelCompilationExceptions = false;
				this.PrecompileApp(HttpRuntime.AppDomainAppVirtualPathObject, excludedVirtualPaths);
			}
			finally
			{
				BuildManager.SkipTopLevelCompilationExceptions = skipTopLevelCompilationExceptions;
				BuildManager.ThrowOnFirstParseError = true;
				this._cbmCallback = null;
			}
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00158A3C File Offset: 0x00156C3C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void PrecompileApp(VirtualPath startingVirtualDir, IEnumerable<string> excludedVirtualPaths)
		{
			using (new ApplicationImpersonationContext())
			{
				try
				{
					BuildManager.PerformingPrecompilation = true;
					this.PrecompileAppInternal(startingVirtualDir, excludedVirtualPaths);
				}
				catch
				{
					this.DeletePrecompTargetDirectory();
					throw;
				}
				finally
				{
					BuildManager.PerformingPrecompilation = false;
				}
			}
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x00158AA4 File Offset: 0x00156CA4
		private void PrecompileAppInternal(VirtualPath startingVirtualDir, IEnumerable<string> excludedVirtualPaths)
		{
			this.FailIfPrecompiledApp();
			excludedVirtualPaths = (excludedVirtualPaths ?? Enumerable.Empty<string>());
			this._excludedCompilationPaths = (from path in excludedVirtualPaths
			select VirtualPath.Create(UrlPath.Combine("~", path))).ToList<VirtualPath>();
			VirtualDirectory directory = startingVirtualDir.GetDirectory();
			this.EnsureTopLevelFilesCompiled();
			try
			{
				BuildManager._parseErrorReported = false;
				this.PrecompileWebDirectoriesRecursive(directory, true);
				this.PrecompileThemeDirectories();
			}
			catch (HttpParseException e)
			{
				if (!BuildManager._parseErrorReported)
				{
					this.ReportErrorsFromException(e);
				}
				throw;
			}
			if (this._precompTargetPhysicalDir != null)
			{
				string toDir = Path.Combine(this._precompTargetPhysicalDir, "bin");
				this.CopyCompiledAssembliesToDestinationBin(HttpRuntime.CodegenDirInternal, toDir);
			}
			if (this._precompTargetPhysicalDir != null)
			{
				this.CopyStaticFilesRecursive(directory, this._precompTargetPhysicalDir, true);
			}
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x00158B74 File Offset: 0x00156D74
		private void CreatePrecompMarkerFile()
		{
			Directory.CreateDirectory(this._precompTargetPhysicalDir);
			string path = Path.Combine(this._precompTargetPhysicalDir, "PrecompiledApp.config");
			using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8))
			{
				streamWriter.Write("<precompiledApp version=\"2\" updatable=\"");
				if (BuildManager.PrecompilingForUpdatableDeployment)
				{
					streamWriter.Write("true");
				}
				else
				{
					streamWriter.Write("false");
				}
				streamWriter.Write("\"/>");
			}
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00158BFC File Offset: 0x00156DFC
		private static bool ReadPrecompMarkerFile(string appRoot, out bool updatable)
		{
			updatable = false;
			string text = Path.Combine(appRoot, "PrecompiledApp.config");
			if (!File.Exists(text))
			{
				return false;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(text);
			}
			catch
			{
				return false;
			}
			XmlNode documentElement = xmlDocument.DocumentElement;
			if (documentElement == null || documentElement.Name != "precompiledApp")
			{
				return false;
			}
			HandlerBase.GetAndRemoveBooleanAttribute(documentElement, "updatable", ref updatable);
			return true;
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x06006252 RID: 25170 RVA: 0x00158C74 File Offset: 0x00156E74
		internal static bool PrecompilingForDeployment
		{
			get
			{
				return BuildManager._theBuildManager._precompTargetPhysicalDir != null;
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x00158C83 File Offset: 0x00156E83
		internal static bool PrecompilingForUpdatableDeployment
		{
			get
			{
				return BuildManager.PrecompilingForDeployment && (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.Updatable) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06006254 RID: 25172 RVA: 0x00158C9D File Offset: 0x00156E9D
		private static bool PrecompilingForCleanBuild
		{
			get
			{
				return (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.Clean) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06006255 RID: 25173 RVA: 0x00158CAE File Offset: 0x00156EAE
		internal static bool PrecompilingWithDebugInfo
		{
			get
			{
				return BuildManager.PrecompilingForDeployment && (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.ForceDebug) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06006256 RID: 25174 RVA: 0x00158CC8 File Offset: 0x00156EC8
		internal static bool PrecompilingWithCodeAnalysisSymbol
		{
			get
			{
				return (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.CodeAnalysis) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06006257 RID: 25175 RVA: 0x00158CDA File Offset: 0x00156EDA
		private static bool CompileWithFixedAssemblyNames
		{
			get
			{
				return (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.FixedNames) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x00158CEF File Offset: 0x00156EEF
		internal static bool CompileWithAllowPartiallyTrustedCallersAttribute
		{
			get
			{
				return (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.AllowPartiallyTrustedCallers) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06006259 RID: 25177 RVA: 0x00158D01 File Offset: 0x00156F01
		internal static bool CompileWithDelaySignAttribute
		{
			get
			{
				return (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.DelaySign) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x00158D13 File Offset: 0x00156F13
		internal static bool IgnoreBadImageFormatException
		{
			get
			{
				return (BuildManager._theBuildManager._precompilationFlags & PrecompilationFlags.IgnoreBadImageFormatException) > PrecompilationFlags.Default;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x0600625B RID: 25179 RVA: 0x00158D28 File Offset: 0x00156F28
		internal static string StrongNameKeyFile
		{
			get
			{
				return BuildManager._theBuildManager._strongNameKeyFile;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x0600625C RID: 25180 RVA: 0x00158D34 File Offset: 0x00156F34
		internal static string StrongNameKeyContainer
		{
			get
			{
				return BuildManager._theBuildManager._strongNameKeyContainer;
			}
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x00158D40 File Offset: 0x00156F40
		internal static TextWriter GetUpdatableDeploymentTargetWriter(VirtualPath virtualPath, Encoding fileEncoding)
		{
			if (!BuildManager.PrecompilingForUpdatableDeployment)
			{
				return null;
			}
			string text = virtualPath.AppRelativeVirtualPathString;
			text = text.Substring(2);
			string path = Path.Combine(BuildManager._theBuildManager._precompTargetPhysicalDir, text);
			string directoryName = Path.GetDirectoryName(path);
			Directory.CreateDirectory(directoryName);
			return new StreamWriter(path, false, fileEncoding);
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x0600625E RID: 25182 RVA: 0x00158D8C File Offset: 0x00156F8C
		private bool IsPrecompiledAppInternal
		{
			get
			{
				if (!this._isPrecompiledAppComputed)
				{
					this._isPrecompiledApp = BuildManager.ReadPrecompMarkerFile(HttpRuntime.AppDomainAppPathInternal, out this._isUpdatablePrecompiledApp);
					this._isPrecompiledAppComputed = true;
				}
				return this._isPrecompiledApp;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x0600625F RID: 25183 RVA: 0x00158DB9 File Offset: 0x00156FB9
		public static bool IsPrecompiledApp
		{
			get
			{
				return BuildManager._theBuildManager.IsPrecompiledAppInternal;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06006260 RID: 25184 RVA: 0x00158DC5 File Offset: 0x00156FC5
		private bool IsUpdatablePrecompiledAppInternal
		{
			get
			{
				return BuildManager.IsPrecompiledApp && this._isUpdatablePrecompiledApp;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06006261 RID: 25185 RVA: 0x00158DD6 File Offset: 0x00156FD6
		public static bool IsUpdatablePrecompiledApp
		{
			get
			{
				return BuildManager._theBuildManager.IsUpdatablePrecompiledAppInternal;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x06006262 RID: 25186 RVA: 0x00158DE2 File Offset: 0x00156FE2
		private bool IsNonUpdatablePrecompiledApp
		{
			get
			{
				return BuildManager.IsPrecompiledApp && !this._isUpdatablePrecompiledApp;
			}
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00158DF8 File Offset: 0x00156FF8
		private bool IsExcludedFromPrecompilation(VirtualDirectory dir)
		{
			return this._excludedCompilationPaths.Any((VirtualPath path) => UrlPath.IsEqualOrSubpath(path.VirtualPathString, dir.VirtualPath));
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x00158E2C File Offset: 0x0015702C
		private void PrecompileWebDirectoriesRecursive(VirtualDirectory vdir, bool topLevel)
		{
			foreach (object obj in vdir.Directories)
			{
				VirtualDirectory virtualDirectory = (VirtualDirectory)obj;
				if ((!topLevel || !this._excludedTopLevelDirectories.Contains(virtualDirectory.Name)) && !(virtualDirectory.Name == "_vti_cnf") && !this.SourceDirectoryIsInPrecompilationDestination(virtualDirectory) && !this.IsExcludedFromPrecompilation(virtualDirectory))
				{
					this.PrecompileWebDirectoriesRecursive(virtualDirectory, false);
				}
			}
			try
			{
				this._precompilingApp = true;
				if (this.IsBatchEnabledForDirectory(vdir.VirtualPathObject))
				{
					this.BatchCompileWebDirectory(vdir, null, false);
				}
				else
				{
					NonBatchDirectoryCompiler nonBatchDirectoryCompiler = new NonBatchDirectoryCompiler(vdir);
					nonBatchDirectoryCompiler.Process();
				}
			}
			finally
			{
				this._precompilingApp = false;
			}
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00158F08 File Offset: 0x00157108
		private void PrecompileThemeDirectories()
		{
			string path = Path.Combine(HttpRuntime.AppDomainAppPathInternal, "App_Themes");
			if (Directory.Exists(path))
			{
				string[] directories = Directory.GetDirectories(path);
				foreach (string path2 in directories)
				{
					string fileName = Path.GetFileName(path2);
					ThemeDirectoryCompiler.GetThemeBuildResultType(null, fileName);
				}
			}
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x00158F5C File Offset: 0x0015715C
		private void CopyStaticFilesRecursive(VirtualDirectory sourceVdir, string destPhysicalDir, bool topLevel)
		{
			if (this.SourceDirectoryIsInPrecompilationDestination(sourceVdir))
			{
				return;
			}
			if (this.IsExcludedFromPrecompilation(sourceVdir))
			{
				return;
			}
			bool flag = false;
			foreach (object obj in sourceVdir.Children)
			{
				VirtualFileBase virtualFileBase = (VirtualFileBase)obj;
				string text = Path.Combine(destPhysicalDir, virtualFileBase.Name);
				if (virtualFileBase.IsDirectory)
				{
					if ((!topLevel || (!StringUtil.EqualsIgnoreCase(virtualFileBase.Name, "App_Code") && !StringUtil.EqualsIgnoreCase(virtualFileBase.Name, "App_GlobalResources") && !StringUtil.EqualsIgnoreCase(virtualFileBase.Name, "App_WebReferences"))) && (BuildManager.PrecompilingForUpdatableDeployment || !StringUtil.EqualsIgnoreCase(virtualFileBase.Name, "App_LocalResources")))
					{
						this.CopyStaticFilesRecursive(virtualFileBase as VirtualDirectory, text, false);
					}
				}
				else
				{
					if (!flag)
					{
						flag = true;
						Directory.CreateDirectory(destPhysicalDir);
					}
					this.CopyPrecompiledFile(virtualFileBase as VirtualFile, text);
				}
			}
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x00159060 File Offset: 0x00157260
		private void CopyCompiledAssembliesToDestinationBin(string fromDir, string toDir)
		{
			bool flag = false;
			foreach (object obj in ((IEnumerable)FileEnumerator.Create(fromDir)))
			{
				FileData fileData = (FileData)obj;
				if (!flag)
				{
					Directory.CreateDirectory(toDir);
				}
				flag = true;
				if (fileData.IsDirectory)
				{
					if (Util.IsCultureName(fileData.Name))
					{
						string fromDir2 = Path.Combine(fromDir, fileData.Name);
						string toDir2 = Path.Combine(toDir, fileData.Name);
						this.CopyCompiledAssembliesToDestinationBin(fromDir2, toDir2);
					}
				}
				else
				{
					string extension = Path.GetExtension(fileData.Name);
					if ((!(extension != ".dll") || !(extension != ".pdb")) && !DiskBuildResultCache.HasDotDeleteFile(fileData.FullName))
					{
						string sourceFileName = Path.Combine(fromDir, fileData.Name);
						string destFileName = Path.Combine(toDir, fileData.Name);
						File.Copy(sourceFileName, destFileName, true);
					}
				}
			}
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00159164 File Offset: 0x00157364
		private void CopyPrecompiledFile(VirtualFile vfile, string destPhysicalPath)
		{
			bool flag;
			if (CompilationUtil.NeedToCopyFile(vfile.VirtualPathObject, BuildManager.PrecompilingForUpdatableDeployment, out flag))
			{
				string sourceFileName = HostingEnvironment.MapPathInternal(vfile.VirtualPath);
				if (File.Exists(destPhysicalPath))
				{
					BuildResultCompiledType buildResultCompiledType = BuildManager.GetVPathBuildResult(null, vfile.VirtualPathObject, true, false) as BuildResultCompiledType;
					Encoding encodingFromConfigPath = Util.GetEncodingFromConfigPath(vfile.VirtualPathObject);
					string text = Util.StringFromFile(destPhysicalPath, ref encodingFromConfigPath);
					text = text.Replace("__ASPNET_INHERITS", Util.GetAssemblyQualifiedTypeName(buildResultCompiledType.ResultType));
					StreamWriter streamWriter = new StreamWriter(destPhysicalPath, false, encodingFromConfigPath);
					streamWriter.Write(text);
					streamWriter.Close();
				}
				else
				{
					File.Copy(sourceFileName, destPhysicalPath, false);
				}
				Util.ClearReadOnlyAttribute(destPhysicalPath);
				return;
			}
			if (flag)
			{
				StreamWriter streamWriter2 = new StreamWriter(destPhysicalPath);
				streamWriter2.Write(SR.GetString("Precomp_stub_file"));
				streamWriter2.Close();
			}
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x00159230 File Offset: 0x00157430
		private bool SourceDirectoryIsInPrecompilationDestination(VirtualDirectory sourceDir)
		{
			if (this._precompTargetPhysicalDir == null)
			{
				return false;
			}
			string text = HostingEnvironment.MapPathInternal(sourceDir.VirtualPath);
			text = FileUtil.FixUpPhysicalDirectory(text);
			string s = FileUtil.FixUpPhysicalDirectory(this._precompTargetPhysicalDir);
			return StringUtil.StringStartsWithIgnoreCase(text, s);
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x00159270 File Offset: 0x00157470
		internal static void ReportDirectoryCompilationProgress(VirtualPath virtualDir)
		{
			ClientBuildManagerCallback cbmcallback = BuildManager.CBMCallback;
			if (cbmcallback == null)
			{
				return;
			}
			if (!virtualDir.DirectoryExists())
			{
				return;
			}
			string @string = SR.GetString("Directory_progress", new object[]
			{
				virtualDir.VirtualPathString
			});
			cbmcallback.ReportProgress(@string);
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x001592B1 File Offset: 0x001574B1
		public static Type GetCompiledType(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			return BuildManager.GetCompiledType(VirtualPath.Create(virtualPath));
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x001592CC File Offset: 0x001574CC
		internal static Type GetCompiledType(VirtualPath virtualPath, ClientBuildManagerCallback callback)
		{
			bool skipTopLevelCompilationExceptions = BuildManager.SkipTopLevelCompilationExceptions;
			bool throwOnFirstParseError = BuildManager.ThrowOnFirstParseError;
			Type compiledType;
			try
			{
				BuildManager.SkipTopLevelCompilationExceptions = false;
				BuildManager.ThrowOnFirstParseError = false;
				BuildManager._theBuildManager._cbmCallback = callback;
				compiledType = BuildManager.GetCompiledType(virtualPath);
			}
			finally
			{
				BuildManager._theBuildManager._cbmCallback = null;
				BuildManager.SkipTopLevelCompilationExceptions = skipTopLevelCompilationExceptions;
				BuildManager.ThrowOnFirstParseError = throwOnFirstParseError;
			}
			return compiledType;
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x00159330 File Offset: 0x00157530
		internal static Type GetCompiledType(VirtualPath virtualPath)
		{
			ITypedWebObjectFactory virtualPathObjectFactory = BuildManager.GetVirtualPathObjectFactory(virtualPath, null, false);
			BuildResultCompiledType buildResultCompiledType = virtualPathObjectFactory as BuildResultCompiledType;
			if (buildResultCompiledType == null)
			{
				return null;
			}
			return buildResultCompiledType.ResultType;
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x00159358 File Offset: 0x00157558
		public static object CreateInstanceFromVirtualPath(string virtualPath, Type requiredBaseType)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			return BuildManager.CreateInstanceFromVirtualPath(virtualPath2, requiredBaseType, null, false);
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x00159378 File Offset: 0x00157578
		internal static object CreateInstanceFromVirtualPath(VirtualPath virtualPath, Type requiredBaseType, HttpContext context, bool allowCrossApp)
		{
			ITypedWebObjectFactory virtualPathObjectFactory = BuildManager.GetVirtualPathObjectFactory(virtualPath, context, allowCrossApp);
			if (virtualPathObjectFactory == null)
			{
				return null;
			}
			Util.CheckAssignableType(requiredBaseType, virtualPathObjectFactory.InstantiatedType);
			object result;
			using (new ClientImpersonationContext(context))
			{
				result = virtualPathObjectFactory.CreateInstance();
			}
			return result;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x001593CC File Offset: 0x001575CC
		public static IWebObjectFactory GetObjectFactory(string virtualPath, bool throwIfNotFound)
		{
			return BuildManager.GetVirtualPathObjectFactory(VirtualPath.Create(virtualPath), null, false, throwIfNotFound);
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x001593E9 File Offset: 0x001575E9
		private static ITypedWebObjectFactory GetVirtualPathObjectFactory(VirtualPath virtualPath, HttpContext context, bool allowCrossApp)
		{
			return BuildManager.GetVirtualPathObjectFactory(virtualPath, context, allowCrossApp, true);
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x001593F4 File Offset: 0x001575F4
		private static ITypedWebObjectFactory GetVirtualPathObjectFactory(VirtualPath virtualPath, HttpContext context, bool allowCrossApp, bool throwIfNotFound)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			if (BuildManager._theBuildManager._topLevelFileCompilationException != null)
			{
				BuildManager._theBuildManager.ReportTopLevelCompilationException();
			}
			BuildResult buildResult;
			if (HttpRuntime.IsFullTrust)
			{
				buildResult = BuildManager.GetVPathBuildResultWithNoAssert(context, virtualPath, false, allowCrossApp, false, throwIfNotFound, true);
			}
			else
			{
				buildResult = BuildManager.GetVPathBuildResultWithAssert(context, virtualPath, false, allowCrossApp, false, throwIfNotFound, true);
			}
			return buildResult as ITypedWebObjectFactory;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x00159458 File Offset: 0x00157658
		public static Assembly GetCompiledAssembly(string virtualPath)
		{
			BuildResult vpathBuildResult = BuildManager.GetVPathBuildResult(VirtualPath.Create(virtualPath));
			if (vpathBuildResult == null)
			{
				return null;
			}
			BuildResultCompiledAssemblyBase buildResultCompiledAssemblyBase = vpathBuildResult as BuildResultCompiledAssemblyBase;
			if (buildResultCompiledAssemblyBase == null)
			{
				return null;
			}
			return buildResultCompiledAssemblyBase.ResultAssembly;
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x00159488 File Offset: 0x00157688
		public static string GetCompiledCustomString(string virtualPath)
		{
			BuildResult vpathBuildResult = BuildManager.GetVPathBuildResult(VirtualPath.Create(virtualPath));
			if (vpathBuildResult == null)
			{
				return null;
			}
			BuildResultCustomString buildResultCustomString = vpathBuildResult as BuildResultCustomString;
			if (buildResultCustomString == null)
			{
				return null;
			}
			return buildResultCustomString.CustomString;
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x001594B8 File Offset: 0x001576B8
		public static BuildDependencySet GetCachedBuildDependencySet(HttpContext context, string virtualPath)
		{
			return BuildManager.GetCachedBuildDependencySet(context, virtualPath, true);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x001594C4 File Offset: 0x001576C4
		public static BuildDependencySet GetCachedBuildDependencySet(HttpContext context, string virtualPath, bool ensureIsUpToDate)
		{
			BuildResult vpathBuildResult = BuildManager.GetVPathBuildResult(context, VirtualPath.Create(virtualPath), true, false, false, ensureIsUpToDate);
			if (vpathBuildResult == null)
			{
				return null;
			}
			return new BuildDependencySet(vpathBuildResult);
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x06006277 RID: 25207 RVA: 0x001594ED File Offset: 0x001576ED
		public static FrameworkName TargetFramework
		{
			get
			{
				return MultiTargetingUtil.TargetFrameworkName;
			}
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x001594F4 File Offset: 0x001576F4
		private Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			if (this._assemblyResolveMapping == null)
			{
				return null;
			}
			string name = e.Name;
			Assembly assembly = (Assembly)this._assemblyResolveMapping[name];
			if (assembly != null)
			{
				return assembly;
			}
			string normalizedCodeAssemblyName = BuildManager.GetNormalizedCodeAssemblyName(name);
			if (normalizedCodeAssemblyName != null)
			{
				return (Assembly)this._assemblyResolveMapping[normalizedCodeAssemblyName];
			}
			return null;
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x0015954C File Offset: 0x0015774C
		internal static string GetNormalizedCodeAssemblyName(string assemblyName)
		{
			if (assemblyName.StartsWith("App_Code", StringComparison.Ordinal))
			{
				return "App_Code";
			}
			CodeSubDirectoriesCollection codeSubDirectories = CompilationUtil.GetCodeSubDirectories();
			foreach (object obj in codeSubDirectories)
			{
				CodeSubDirectory codeSubDirectory = (CodeSubDirectory)obj;
				if (assemblyName.StartsWith("App_SubCode_" + codeSubDirectory.AssemblyName + ".", StringComparison.Ordinal))
				{
					return codeSubDirectory.AssemblyName;
				}
			}
			return null;
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x001595E0 File Offset: 0x001577E0
		internal static string GetNormalizedTypeName(Type t)
		{
			string fullName = t.Assembly.FullName;
			string normalizedCodeAssemblyName = BuildManager.GetNormalizedCodeAssemblyName(fullName);
			if (normalizedCodeAssemblyName == null)
			{
				return t.AssemblyQualifiedName;
			}
			return t.FullName + ", " + normalizedCodeAssemblyName;
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x0600627B RID: 25211 RVA: 0x00159620 File Offset: 0x00157820
		internal static string CodegenResourceDir
		{
			get
			{
				string text = BuildManager._theBuildManager._codegenResourceDir;
				if (text == null)
				{
					text = Path.Combine(HttpRuntime.CodegenDirInternal, "ResX");
					BuildManager._theBuildManager._codegenResourceDir = text;
				}
				return text;
			}
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x0600627C RID: 25212 RVA: 0x00159658 File Offset: 0x00157858
		private static string UserCachePath
		{
			get
			{
				if (BuildManager._userCachePath == null)
				{
					string text = Path.Combine(HttpRuntime.CodegenDirInternal, "UserCache");
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					BuildManager._userCachePath = text;
				}
				return BuildManager._userCachePath;
			}
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x00159698 File Offset: 0x00157898
		private static string GetUserCacheFilePath(string fileName)
		{
			string text = Path.Combine(BuildManager.UserCachePath, fileName);
			if (Path.GetDirectoryName(text) != BuildManager.UserCachePath)
			{
				throw new ArgumentException();
			}
			return text;
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x001596CC File Offset: 0x001578CC
		public static Stream CreateCachedFile(string fileName)
		{
			new FileIOPermission(FileIOPermissionAccess.AllAccess, HttpRuntime.CodegenDirInternal).Assert();
			string userCacheFilePath = BuildManager.GetUserCacheFilePath(fileName);
			return File.Create(userCacheFilePath);
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x001596F8 File Offset: 0x001578F8
		public static Stream ReadCachedFile(string fileName)
		{
			new FileIOPermission(FileIOPermissionAccess.AllAccess, HttpRuntime.CodegenDirInternal).Assert();
			string userCacheFilePath = BuildManager.GetUserCacheFilePath(fileName);
			if (!File.Exists(userCacheFilePath))
			{
				return null;
			}
			return File.OpenRead(userCacheFilePath);
		}

		// Token: 0x040032DB RID: 13019
		internal const string AssemblyNamePrefix = "App_";

		// Token: 0x040032DC RID: 13020
		internal const string WebAssemblyNamePrefix = "App_Web_";

		// Token: 0x040032DD RID: 13021
		internal const string AppThemeAssemblyNamePrefix = "App_Theme_";

		// Token: 0x040032DE RID: 13022
		internal const string GlobalThemeAssemblyNamePrefix = "App_GlobalTheme_";

		// Token: 0x040032DF RID: 13023
		internal const string AppBrowserCapAssemblyNamePrefix = "App_Browsers";

		// Token: 0x040032E0 RID: 13024
		private const string CodeDirectoryAssemblyName = "App_Code";

		// Token: 0x040032E1 RID: 13025
		internal const string SubCodeDirectoryAssemblyNamePrefix = "App_SubCode_";

		// Token: 0x040032E2 RID: 13026
		private const string ResourcesDirectoryAssemblyName = "App_GlobalResources";

		// Token: 0x040032E3 RID: 13027
		private const string LocalResourcesDirectoryAssemblyName = "App_LocalResources";

		// Token: 0x040032E4 RID: 13028
		private const string WebRefDirectoryAssemblyName = "App_WebReferences";

		// Token: 0x040032E5 RID: 13029
		internal const string GlobalAsaxAssemblyName = "App_global.asax";

		// Token: 0x040032E6 RID: 13030
		private const string LicensesAssemblyName = "App_Licenses";

		// Token: 0x040032E7 RID: 13031
		internal const string UpdatableInheritReplacementToken = "__ASPNET_INHERITS";

		// Token: 0x040032E8 RID: 13032
		private const string CodegenResourceDirectoryName = "ResX";

		// Token: 0x040032E9 RID: 13033
		private static RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

		// Token: 0x040032EA RID: 13034
		private static bool _theBuildManagerInitialized;

		// Token: 0x040032EB RID: 13035
		private static Exception _initializeException;

		// Token: 0x040032EC RID: 13036
		private static BuildManager _theBuildManager = new BuildManager();

		// Token: 0x040032ED RID: 13037
		private static long s_topLevelHash;

		// Token: 0x040032EE RID: 13038
		private readonly HashCodeCombiner _preAppStartHashCodeCombiner = new HashCodeCombiner();

		// Token: 0x040032EF RID: 13039
		private const string precompMarkerFileName = "PrecompiledApp.config";

		// Token: 0x040032F0 RID: 13040
		private string _precompTargetPhysicalDir;

		// Token: 0x040032F1 RID: 13041
		private PrecompilationFlags _precompilationFlags;

		// Token: 0x040032F2 RID: 13042
		private bool _isPrecompiledApp;

		// Token: 0x040032F3 RID: 13043
		private bool _isPrecompiledAppComputed;

		// Token: 0x040032F4 RID: 13044
		private bool _isUpdatablePrecompiledApp;

		// Token: 0x040032F5 RID: 13045
		private bool _precompilingApp;

		// Token: 0x040032F6 RID: 13046
		private string _strongNameKeyFile;

		// Token: 0x040032F7 RID: 13047
		private string _strongNameKeyContainer;

		// Token: 0x040032F8 RID: 13048
		private string _codegenResourceDir;

		// Token: 0x040032F9 RID: 13049
		private bool _optimizeCompilations;

		// Token: 0x040032FA RID: 13050
		private string _webHashFilePath;

		// Token: 0x040032FB RID: 13051
		private BuildResultCache[] _caches;

		// Token: 0x040032FC RID: 13052
		private StandardDiskBuildResultCache _codeGenCache;

		// Token: 0x040032FD RID: 13053
		private MemoryBuildResultCache _memoryCache;

		// Token: 0x040032FE RID: 13054
		private bool _topLevelFilesCompiledStarted;

		// Token: 0x040032FF RID: 13055
		private bool _topLevelFilesCompiledCompleted;

		// Token: 0x04003300 RID: 13056
		private Exception _topLevelFileCompilationException;

		// Token: 0x04003301 RID: 13057
		private BuildResultCompiledGlobalAsaxType _globalAsaxBuildResult;

		// Token: 0x04003302 RID: 13058
		private Type _profileType;

		// Token: 0x04003303 RID: 13059
		private StringSet _excludedTopLevelDirectories;

		// Token: 0x04003304 RID: 13060
		private StringSet _forbiddenTopLevelDirectories;

		// Token: 0x04003305 RID: 13061
		private StringSet _excludedCodeSubdirectories;

		// Token: 0x04003306 RID: 13062
		private List<VirtualPath> _excludedCompilationPaths;

		// Token: 0x04003307 RID: 13063
		private CompilationStage _compilationStage;

		// Token: 0x04003308 RID: 13064
		private VirtualPath _scriptVirtualDir;

		// Token: 0x04003309 RID: 13065
		private VirtualPath _globalAsaxVirtualPath;

		// Token: 0x0400330A RID: 13066
		private ClientBuildManagerCallback _cbmCallback;

		// Token: 0x0400330B RID: 13067
		private static bool _parseErrorReported;

		// Token: 0x0400330C RID: 13068
		private List<Assembly> _topLevelReferencedAssemblies = new List<Assembly>
		{
			typeof(HttpRuntime).Assembly,
			typeof(Component).Assembly
		};

		// Token: 0x0400330D RID: 13069
		private Dictionary<string, AssemblyReferenceInfo> _topLevelAssembliesIndexTable;

		// Token: 0x0400330E RID: 13070
		private Dictionary<string, string> _generatedFileTable;

		// Token: 0x0400330F RID: 13071
		private ArrayList _codeAssemblies;

		// Token: 0x04003310 RID: 13072
		private IDictionary _assemblyResolveMapping;

		// Token: 0x04003311 RID: 13073
		private Assembly _appResourcesAssembly;

		// Token: 0x04003312 RID: 13074
		private bool _throwOnFirstParseError = true;

		// Token: 0x04003313 RID: 13075
		private bool _performingPrecompilation;

		// Token: 0x04003314 RID: 13076
		private bool _skipTopLevelCompilationExceptions;

		// Token: 0x04003315 RID: 13077
		private static HashSet<Assembly> s_dynamicallyAddedReferencedAssembly = new HashSet<Assembly>();

		// Token: 0x04003317 RID: 13079
		private const string CircularReferenceCheckerSlotName = "CircRefChk";

		// Token: 0x04003318 RID: 13080
		private Hashtable _localResourcesAssemblies = new Hashtable();

		// Token: 0x04003319 RID: 13081
		private const string BatchCompilationSlotName = "BatchCompileChk";

		// Token: 0x0400331A RID: 13082
		private static bool? s_batchCompilationEnabled;

		// Token: 0x0400331B RID: 13083
		private static SimpleRecyclingCache _keyCache = new SimpleRecyclingCache();

		// Token: 0x0400331C RID: 13084
		private static string _userCachePath;
	}
}
