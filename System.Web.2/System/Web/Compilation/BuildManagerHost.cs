using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000807 RID: 2055
	internal class BuildManagerHost : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x06006282 RID: 25218 RVA: 0x00159766 File Offset: 0x00157966
		// (set) Token: 0x06006283 RID: 25219 RVA: 0x0015976D File Offset: 0x0015796D
		internal static bool InClientBuildManager
		{
			get
			{
				return BuildManagerHost._inClientBuildManager;
			}
			set
			{
				BuildManagerHost._inClientBuildManager = true;
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x06006284 RID: 25220 RVA: 0x00159775 File Offset: 0x00157975
		// (set) Token: 0x06006285 RID: 25221 RVA: 0x0015977C File Offset: 0x0015797C
		internal static bool SupportsMultiTargeting { get; set; }

		// Token: 0x06006286 RID: 25222 RVA: 0x00159784 File Offset: 0x00157984
		public BuildManagerHost()
		{
			HostingEnvironment.RegisterObject(this);
			AppDomain.CurrentDomain.AssemblyResolve += this.ResolveAssembly;
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x001597B3 File Offset: 0x001579B3
		void IRegisteredObject.Stop(bool immediate)
		{
			this.WaitForPendingCallsToFinish();
			HostingEnvironment.UnregisterObject(this);
			if (this._client != null)
			{
				this._client.ResetHost();
			}
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x001597D4 File Offset: 0x001579D4
		internal IApplicationHost ApplicationHost
		{
			get
			{
				return HostingEnvironment.ApplicationHostInternal;
			}
		}

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x001597DC File Offset: 0x001579DC
		internal string CodeGenDir
		{
			get
			{
				this.AddPendingCall();
				string codegenDirInternal;
				try
				{
					codegenDirInternal = HttpRuntime.CodegenDirInternal;
				}
				finally
				{
					this.RemovePendingCall();
				}
				return codegenDirInternal;
			}
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x00159810 File Offset: 0x00157A10
		internal void RegisterAssembly(string assemblyName, string assemblyLocation)
		{
			if (this._assemblyCollection == null)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._assemblyCollection == null)
					{
						this._assemblyCollection = Hashtable.Synchronized(new Hashtable());
					}
				}
			}
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			this._assemblyCollection[assemblyName2.FullName] = assemblyLocation;
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x00159884 File Offset: 0x00157A84
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			if (this._assemblyCollection == null)
			{
				return null;
			}
			string text = (string)this._assemblyCollection[e.Name];
			if (text == null)
			{
				return null;
			}
			return Assembly.LoadFrom(text);
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x001598BD File Offset: 0x00157ABD
		private void WaitForPendingCallsToFinish()
		{
			while (this._pendingCallsCount > 0 && !this._ignorePendingCalls)
			{
				Thread.Sleep(250);
			}
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x001598DC File Offset: 0x00157ADC
		internal void AddPendingCall()
		{
			Interlocked.Increment(ref this._pendingCallsCount);
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x001598EA File Offset: 0x00157AEA
		internal void RemovePendingCall()
		{
			Interlocked.Decrement(ref this._pendingCallsCount);
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x001598F8 File Offset: 0x00157AF8
		private void OnAppDomainShutdown(object o, BuildManagerHostUnloadEventArgs args)
		{
			this._client.OnAppDomainShutdown(args.Reason);
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x0015990C File Offset: 0x00157B0C
		internal void CompileApplicationDependencies()
		{
			this.AddPendingCall();
			try
			{
				this._buildManager.EnsureTopLevelFilesCompiled();
			}
			finally
			{
				this.RemovePendingCall();
			}
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x00159944 File Offset: 0x00157B44
		internal void PrecompileApp(ClientBuildManagerCallback callback, List<string> excludedVirtualPaths)
		{
			this.AddPendingCall();
			try
			{
				this._buildManager.PrecompileApp(callback, excludedVirtualPaths);
			}
			finally
			{
				this.RemovePendingCall();
			}
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x00159980 File Offset: 0x00157B80
		internal IDictionary GetBrowserDefinitions()
		{
			this.AddPendingCall();
			IDictionary result;
			try
			{
				result = BrowserCapabilitiesCompiler.BrowserCapabilitiesFactory.InternalGetBrowserElements();
			}
			finally
			{
				this.RemovePendingCall();
			}
			return result;
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x001599B8 File Offset: 0x00157BB8
		internal string[] GetVirtualCodeDirectories()
		{
			this.AddPendingCall();
			string[] codeDirectories;
			try
			{
				codeDirectories = this._buildManager.GetCodeDirectories();
			}
			finally
			{
				this.RemovePendingCall();
			}
			return codeDirectories;
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x001599F4 File Offset: 0x00157BF4
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void GetCodeDirectoryInformation(VirtualPath virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compParams, out string generatedFilesDir)
		{
			this.AddPendingCall();
			try
			{
				BuildManager.SkipTopLevelCompilationExceptions = true;
				this._buildManager.EnsureTopLevelFilesCompiled();
				virtualCodeDir = virtualCodeDir.CombineWithAppRoot();
				this._buildManager.GetCodeDirectoryInformation(virtualCodeDir, out codeDomProviderType, out compParams, out generatedFilesDir);
			}
			finally
			{
				BuildManager.SkipTopLevelCompilationExceptions = false;
				this.RemovePendingCall();
			}
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x00159A50 File Offset: 0x00157C50
		internal void GetCompilerParams(VirtualPath virtualPath, out Type codeDomProviderType, out CompilerParameters compParams)
		{
			this.AddPendingCall();
			try
			{
				BuildManager.SkipTopLevelCompilationExceptions = true;
				this._buildManager.EnsureTopLevelFilesCompiled();
				this.GetCompilerParamsAndBuildProvider(virtualPath, out codeDomProviderType, out compParams);
				if (compParams != null)
				{
					this.FixupReferencedAssemblies(virtualPath, compParams);
				}
			}
			finally
			{
				BuildManager.SkipTopLevelCompilationExceptions = false;
				this.RemovePendingCall();
			}
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x00159AAC File Offset: 0x00157CAC
		internal string[] GetCompiledTypeAndAssemblyName(VirtualPath virtualPath, ClientBuildManagerCallback callback)
		{
			this.AddPendingCall();
			string[] result;
			try
			{
				virtualPath.CombineWithAppRoot();
				Type compiledType = BuildManager.GetCompiledType(virtualPath, callback);
				if (compiledType == null)
				{
					result = null;
				}
				else
				{
					string assemblyPathFromType = Util.GetAssemblyPathFromType(compiledType);
					result = new string[]
					{
						compiledType.FullName,
						assemblyPathFromType
					};
				}
			}
			finally
			{
				this.RemovePendingCall();
			}
			return result;
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x00159B10 File Offset: 0x00157D10
		internal string GetGeneratedSourceFile(VirtualPath virtualPath)
		{
			this.AddPendingCall();
			string result;
			try
			{
				if (!virtualPath.DirectoryExists())
				{
					throw new ArgumentException(SR.GetString("GetGeneratedSourceFile_Directory_Only", new object[]
					{
						virtualPath.VirtualPathString
					}), "virtualPath");
				}
				Type type;
				CompilerParameters compilerParameters;
				string text;
				this.GetCodeDirectoryInformation(virtualPath, out type, out compilerParameters, out text);
				result = BuildManager.GenerateFileTable[virtualPath.VirtualPathStringNoTrailingSlash];
			}
			finally
			{
				this.RemovePendingCall();
			}
			return result;
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x00159B88 File Offset: 0x00157D88
		internal string GetGeneratedFileVirtualPath(string filePath)
		{
			this.AddPendingCall();
			string result;
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in BuildManager.GenerateFileTable)
				{
					if (filePath.Equals(keyValuePair.Value, StringComparison.Ordinal))
					{
						return keyValuePair.Key;
					}
				}
				result = null;
			}
			finally
			{
				this.RemovePendingCall();
			}
			return result;
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x00159BF0 File Offset: 0x00157DF0
		internal string[] GetTopLevelAssemblyReferences(VirtualPath virtualPath)
		{
			this.AddPendingCall();
			List<Assembly> list = new List<Assembly>();
			try
			{
				virtualPath.CombineWithAppRoot();
				CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(virtualPath);
				foreach (object obj in compilationConfig.Assemblies)
				{
					AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
					Assembly[] assemblyInternal = assemblyInfo.AssemblyInternal;
					for (int i = 0; i < assemblyInternal.Length; i++)
					{
						if (assemblyInternal[i] != null)
						{
							list.Add(assemblyInternal[i]);
						}
					}
				}
			}
			finally
			{
				this.RemovePendingCall();
			}
			StringCollection stringCollection = new StringCollection();
			Util.AddAssembliesToStringCollection(list, stringCollection);
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x00159CD0 File Offset: 0x00157ED0
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal string GenerateCode(VirtualPath virtualPath, string virtualFileString, out IDictionary linePragmasTable)
		{
			this.AddPendingCall();
			string result;
			try
			{
				string text = null;
				Type type;
				CompilerParameters compilerParameters;
				CodeCompileUnit codeCompileUnit = this.GenerateCodeCompileUnit(virtualPath, virtualFileString, out type, out compilerParameters, out linePragmasTable);
				if (codeCompileUnit != null && type != null)
				{
					CodeDomProvider codeDomProvider = CompilationUtil.CreateCodeDomProvider(type);
					CodeGeneratorOptions codeGeneratorOptions = new CodeGeneratorOptions();
					codeGeneratorOptions.BlankLinesBetweenMembers = false;
					codeGeneratorOptions.IndentString = string.Empty;
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					codeDomProvider.GenerateCodeFromCompileUnit(codeCompileUnit, stringWriter, codeGeneratorOptions);
					text = stringWriter.ToString();
				}
				result = text;
			}
			finally
			{
				this.RemovePendingCall();
			}
			return result;
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x00159D60 File Offset: 0x00157F60
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal CodeCompileUnit GenerateCodeCompileUnit(VirtualPath virtualPath, string virtualFileString, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			this.AddPendingCall();
			CodeCompileUnit codeCompileUnit2;
			try
			{
				BuildManager.SkipTopLevelCompilationExceptions = true;
				this._buildManager.EnsureTopLevelFilesCompiled();
				if (virtualFileString == null)
				{
					using (Stream stream = virtualPath.OpenFile())
					{
						TextReader textReader = Util.ReaderFromStream(stream, virtualPath);
						virtualFileString = textReader.ReadToEnd();
					}
				}
				this._virtualPathProvider.RegisterVirtualFile(virtualPath, virtualFileString);
				string cacheKey = BuildManager.GetCacheKeyFromVirtualPath(virtualPath) + "_CBMResult";
				BuildResultCodeCompileUnit buildResultCodeCompileUnit = (BuildResultCodeCompileUnit)BuildManager.GetBuildResultFromCache(cacheKey, virtualPath);
				if (buildResultCodeCompileUnit == null)
				{
					object @lock = this._lock;
					lock (@lock)
					{
						DateTime utcNow = DateTime.UtcNow;
						BuildProvider compilerParamsAndBuildProvider = this.GetCompilerParamsAndBuildProvider(virtualPath, out codeDomProviderType, out compilerParameters);
						if (compilerParamsAndBuildProvider == null)
						{
							linePragmasTable = null;
							return null;
						}
						CodeCompileUnit codeCompileUnit = compilerParamsAndBuildProvider.GetCodeCompileUnit(out linePragmasTable);
						buildResultCodeCompileUnit = new BuildResultCodeCompileUnit(codeDomProviderType, codeCompileUnit, compilerParameters, linePragmasTable);
						buildResultCodeCompileUnit.VirtualPath = virtualPath;
						buildResultCodeCompileUnit.SetCacheKey(cacheKey);
						this.FixupReferencedAssemblies(virtualPath, compilerParameters);
						if (codeCompileUnit != null)
						{
							foreach (string value in compilerParameters.ReferencedAssemblies)
							{
								codeCompileUnit.ReferencedAssemblies.Add(value);
							}
						}
						ICollection virtualPathDependencies = compilerParamsAndBuildProvider.VirtualPathDependencies;
						if (virtualPathDependencies != null)
						{
							buildResultCodeCompileUnit.AddVirtualPathDependencies(virtualPathDependencies);
						}
						BuildManager.CacheBuildResult(cacheKey, buildResultCodeCompileUnit, utcNow);
						return codeCompileUnit;
					}
				}
				codeDomProviderType = buildResultCodeCompileUnit.CodeDomProviderType;
				compilerParameters = buildResultCodeCompileUnit.CompilerParameters;
				linePragmasTable = buildResultCodeCompileUnit.LinePragmasTable;
				this.FixupReferencedAssemblies(virtualPath, compilerParameters);
				codeCompileUnit2 = buildResultCodeCompileUnit.CodeCompileUnit;
			}
			finally
			{
				if (virtualFileString != null)
				{
					this._virtualPathProvider.RevertVirtualFile(virtualPath);
				}
				BuildManager.SkipTopLevelCompilationExceptions = false;
				this.RemovePendingCall();
			}
			return codeCompileUnit2;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x00159F74 File Offset: 0x00158174
		internal bool IsCodeAssembly(string assemblyName)
		{
			return BuildManager.GetNormalizedCodeAssemblyName(assemblyName) != null;
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x00159F80 File Offset: 0x00158180
		private void FixupReferencedAssemblies(VirtualPath virtualPath, CompilerParameters compilerParameters)
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(virtualPath);
			ICollection referencedAssemblies = BuildManager.GetReferencedAssemblies(compilationConfig);
			Util.AddAssembliesToStringCollection(referencedAssemblies, compilerParameters.ReferencedAssemblies);
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x00159FA8 File Offset: 0x001581A8
		private BuildProvider GetCompilerParamsAndBuildProvider(VirtualPath virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters)
		{
			virtualPath.CombineWithAppRoot();
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(virtualPath);
			ICollection referencedAssemblies = BuildManager.GetReferencedAssemblies(compilationConfig);
			BuildProvider buildProvider;
			if (StringUtil.EqualsIgnoreCase(virtualPath.VirtualPathString, BuildManager.GlobalAsaxVirtualPath.VirtualPathString))
			{
				ApplicationBuildProvider applicationBuildProvider = new ApplicationBuildProvider();
				applicationBuildProvider.SetVirtualPath(virtualPath);
				applicationBuildProvider.SetReferencedAssemblies(referencedAssemblies);
				buildProvider = applicationBuildProvider;
			}
			else
			{
				buildProvider = BuildManager.CreateBuildProvider(virtualPath, compilationConfig, referencedAssemblies, true);
			}
			buildProvider.IgnoreParseErrors = true;
			buildProvider.IgnoreControlProperties = true;
			buildProvider.ThrowOnFirstParseError = false;
			CompilerType codeCompilerType = buildProvider.CodeCompilerType;
			if (codeCompilerType == null)
			{
				codeDomProviderType = null;
				compilerParameters = null;
				return null;
			}
			codeDomProviderType = codeCompilerType.CodeDomProviderType;
			compilerParameters = codeCompilerType.CompilerParameters;
			IAssemblyDependencyParser assemblyDependencyParser = buildProvider.AssemblyDependencyParser;
			if (assemblyDependencyParser != null && assemblyDependencyParser.AssemblyDependencies != null)
			{
				Util.AddAssembliesToStringCollection(assemblyDependencyParser.AssemblyDependencies, compilerParameters.ReferencedAssemblies);
			}
			AssemblyBuilder.FixUpCompilerParameters(compilationConfig, codeDomProviderType, compilerParameters);
			return buildProvider;
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x0015A074 File Offset: 0x00158274
		internal void Configure(ClientBuildManager client)
		{
			this.AddPendingCall();
			try
			{
				this._virtualPathProvider = new BuildManagerHost.ClientVirtualPathProvider();
				HostingEnvironment.RegisterVirtualPathProviderInternal(this._virtualPathProvider);
				this._client = client;
				if (this._client.CBMTypeDescriptionProviderBridge != null)
				{
					TargetFrameworkUtil.CBMTypeDescriptionProviderBridge = this._client.CBMTypeDescriptionProviderBridge;
				}
				this._onAppDomainUnload = new EventHandler(this.OnAppDomainUnload);
				Thread.GetDomain().DomainUnload += this._onAppDomainUnload;
				this._buildManager = BuildManager.TheBuildManager;
				HttpRuntime.AppDomainShutdown += this.OnAppDomainShutdown;
			}
			finally
			{
				this.RemovePendingCall();
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x060062A1 RID: 25249 RVA: 0x0015A118 File Offset: 0x00158318
		internal Exception InitializationException
		{
			get
			{
				return HostingEnvironment.InitializationException;
			}
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x0015A11F File Offset: 0x0015831F
		private void OnAppDomainUnload(object unusedObject, EventArgs unusedEventArgs)
		{
			Thread.GetDomain().DomainUnload -= this._onAppDomainUnload;
			if (this._client != null)
			{
				this._client.OnAppDomainUnloaded(HttpRuntime.ShutdownReason);
				this._client = null;
			}
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x0015A150 File Offset: 0x00158350
		internal bool UnloadAppDomain()
		{
			this._ignorePendingCalls = true;
			HttpRuntime.SetUserForcedShutdown();
			return HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.UnloadAppDomainCalled, "CBM called UnloadAppDomain");
		}

		// Token: 0x04003329 RID: 13097
		private ClientBuildManager _client;

		// Token: 0x0400332A RID: 13098
		private BuildManager _buildManager;

		// Token: 0x0400332B RID: 13099
		private int _pendingCallsCount;

		// Token: 0x0400332C RID: 13100
		private EventHandler _onAppDomainUnload;

		// Token: 0x0400332D RID: 13101
		private bool _ignorePendingCalls;

		// Token: 0x0400332E RID: 13102
		private IDictionary _assemblyCollection;

		// Token: 0x0400332F RID: 13103
		private object _lock = new object();

		// Token: 0x04003330 RID: 13104
		private static bool _inClientBuildManager;

		// Token: 0x04003332 RID: 13106
		private BuildManagerHost.ClientVirtualPathProvider _virtualPathProvider;

		// Token: 0x02000A6E RID: 2670
		internal class ClientVirtualPathProvider : VirtualPathProvider
		{
			// Token: 0x06006F23 RID: 28451 RVA: 0x0018B7BD File Offset: 0x001899BD
			internal ClientVirtualPathProvider()
			{
				this._stringDictionary = new HybridDictionary(true);
			}

			// Token: 0x06006F24 RID: 28452 RVA: 0x0018B7D1 File Offset: 0x001899D1
			public override bool FileExists(string virtualPath)
			{
				return this._stringDictionary.Contains(virtualPath) || base.FileExists(virtualPath);
			}

			// Token: 0x06006F25 RID: 28453 RVA: 0x0018B7EA File Offset: 0x001899EA
			public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
			{
				if (virtualPath != null)
				{
					virtualPath = UrlPath.MakeVirtualPathAppAbsolute(virtualPath);
					if (this._stringDictionary.Contains(virtualPath))
					{
						return null;
					}
				}
				return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
			}

			// Token: 0x06006F26 RID: 28454 RVA: 0x0018B810 File Offset: 0x00189A10
			public override VirtualFile GetFile(string virtualPath)
			{
				string text = (string)this._stringDictionary[virtualPath];
				if (text == null)
				{
					return base.GetFile(virtualPath);
				}
				return new BuildManagerHost.ClientVirtualPathProvider.ClientVirtualFile(virtualPath, text);
			}

			// Token: 0x06006F27 RID: 28455 RVA: 0x0018B844 File Offset: 0x00189A44
			public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
			{
				HashCodeCombiner hashCodeCombiner = null;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in virtualPathDependencies)
				{
					string text = (string)obj;
					if (this._stringDictionary.Contains(text))
					{
						if (hashCodeCombiner == null)
						{
							hashCodeCombiner = new HashCodeCombiner();
						}
						hashCodeCombiner.AddInt(StringUtil.GetNonRandomizedHashCode((string)this._stringDictionary[text], false));
					}
					else
					{
						arrayList.Add(text);
					}
				}
				if (hashCodeCombiner == null)
				{
					return base.GetFileHash(virtualPath, virtualPathDependencies);
				}
				hashCodeCombiner.AddObject(base.GetFileHash(virtualPath, arrayList));
				return hashCodeCombiner.CombinedHashString;
			}

			// Token: 0x06006F28 RID: 28456 RVA: 0x0018B8FC File Offset: 0x00189AFC
			internal void RegisterVirtualFile(VirtualPath virtualPath, string virtualFileString)
			{
				this._stringDictionary[virtualPath.VirtualPathString] = virtualFileString;
			}

			// Token: 0x06006F29 RID: 28457 RVA: 0x0018B910 File Offset: 0x00189B10
			internal void RevertVirtualFile(VirtualPath virtualPath)
			{
				this._stringDictionary.Remove(virtualPath.VirtualPathString);
			}

			// Token: 0x04003BA6 RID: 15270
			private IDictionary _stringDictionary;

			// Token: 0x02000A9C RID: 2716
			internal class ClientVirtualFile : VirtualFile
			{
				// Token: 0x06006F86 RID: 28550 RVA: 0x0018D0CB File Offset: 0x0018B2CB
				internal ClientVirtualFile(string virtualPath, string virtualFileString) : base(virtualPath)
				{
					this._virtualFileString = virtualFileString;
				}

				// Token: 0x06006F87 RID: 28551 RVA: 0x0018D0DC File Offset: 0x0018B2DC
				public override Stream Open()
				{
					Stream stream = new MemoryStream();
					StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode);
					streamWriter.Write(this._virtualFileString);
					streamWriter.Flush();
					stream.Seek(0L, SeekOrigin.Begin);
					return stream;
				}

				// Token: 0x04003C18 RID: 15384
				private string _virtualFileString;
			}
		}
	}
}
