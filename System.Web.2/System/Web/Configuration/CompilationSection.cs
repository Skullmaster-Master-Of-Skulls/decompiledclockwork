using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006C3 RID: 1731
	public sealed class CompilationSection : ConfigurationSection
	{
		// Token: 0x06005368 RID: 21352 RVA: 0x00125034 File Offset: 0x00123234
		static CompilationSection()
		{
			CompilationSection._properties = new ConfigurationPropertyCollection();
			CompilationSection._properties.Add(CompilationSection._propTempDirectory);
			CompilationSection._properties.Add(CompilationSection._propDebug);
			CompilationSection._properties.Add(CompilationSection._propStrict);
			CompilationSection._properties.Add(CompilationSection._propExplicit);
			CompilationSection._properties.Add(CompilationSection._propBatch);
			CompilationSection._properties.Add(CompilationSection._propOptimizeCompilations);
			CompilationSection._properties.Add(CompilationSection._propBatchTimeout);
			CompilationSection._properties.Add(CompilationSection._propMaxBatchSize);
			CompilationSection._properties.Add(CompilationSection._propMaxBatchGeneratedFileSize);
			CompilationSection._properties.Add(CompilationSection._propNumRecompilesBeforeAppRestart);
			CompilationSection._properties.Add(CompilationSection._propDefaultLanguage);
			CompilationSection._properties.Add(CompilationSection._propTargetFramework);
			CompilationSection._properties.Add(CompilationSection._propCompilers);
			CompilationSection._properties.Add(CompilationSection._propAssemblies);
			CompilationSection._properties.Add(CompilationSection._propBuildProviders);
			CompilationSection._properties.Add(CompilationSection._propFolderLevelBuildProviders);
			CompilationSection._properties.Add(CompilationSection._propExpressionBuilders);
			CompilationSection._properties.Add(CompilationSection._propUrlLinePragmas);
			CompilationSection._properties.Add(CompilationSection._propCodeSubDirs);
			CompilationSection._properties.Add(CompilationSection._propAssemblyPreprocessorType);
			CompilationSection._properties.Add(CompilationSection._propEnablePrefetchOptimization);
			CompilationSection._properties.Add(CompilationSection._propProfileGuidedOptimizations);
			CompilationSection._properties.Add(CompilationSection._propControlBuilderInterceptorType);
			CompilationSection._properties.Add(CompilationSection._propDisableObsoleteWarnings);
			CompilationSection._properties.Add(CompilationSection._propMaxConcurrentCompilations);
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x001254F5 File Offset: 0x001236F5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilationSection._properties;
			}
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x001254FC File Offset: 0x001236FC
		protected override object GetRuntimeObject()
		{
			this._isRuntimeObject = true;
			return base.GetRuntimeObject();
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x0012550B File Offset: 0x0012370B
		// (set) Token: 0x0600536D RID: 21357 RVA: 0x0012551D File Offset: 0x0012371D
		[ConfigurationProperty("tempDirectory", DefaultValue = "")]
		public string TempDirectory
		{
			get
			{
				return (string)base[CompilationSection._propTempDirectory];
			}
			set
			{
				base[CompilationSection._propTempDirectory] = value;
			}
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0012552C File Offset: 0x0012372C
		internal void GetTempDirectoryErrorInfo(out string tempDirAttribName, out string configFileName, out int configLineNumber)
		{
			tempDirAttribName = "tempDirectory";
			configFileName = base.ElementInformation.Properties["tempDirectory"].Source;
			configLineNumber = base.ElementInformation.Properties["tempDirectory"].LineNumber;
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x0600536F RID: 21359 RVA: 0x00125578 File Offset: 0x00123778
		// (set) Token: 0x06005370 RID: 21360 RVA: 0x0012558A File Offset: 0x0012378A
		[ConfigurationProperty("debug", DefaultValue = false)]
		public bool Debug
		{
			get
			{
				return (bool)base[CompilationSection._propDebug];
			}
			set
			{
				base[CompilationSection._propDebug] = value;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06005371 RID: 21361 RVA: 0x0012559D File Offset: 0x0012379D
		// (set) Token: 0x06005372 RID: 21362 RVA: 0x001255AF File Offset: 0x001237AF
		[ConfigurationProperty("strict", DefaultValue = false)]
		public bool Strict
		{
			get
			{
				return (bool)base[CompilationSection._propStrict];
			}
			set
			{
				base[CompilationSection._propStrict] = value;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06005373 RID: 21363 RVA: 0x001255C2 File Offset: 0x001237C2
		// (set) Token: 0x06005374 RID: 21364 RVA: 0x001255D4 File Offset: 0x001237D4
		[ConfigurationProperty("explicit", DefaultValue = true)]
		public bool Explicit
		{
			get
			{
				return (bool)base[CompilationSection._propExplicit];
			}
			set
			{
				base[CompilationSection._propExplicit] = value;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06005375 RID: 21365 RVA: 0x001255E7 File Offset: 0x001237E7
		// (set) Token: 0x06005376 RID: 21366 RVA: 0x001255F9 File Offset: 0x001237F9
		[ConfigurationProperty("batch", DefaultValue = true)]
		public bool Batch
		{
			get
			{
				return (bool)base[CompilationSection._propBatch];
			}
			set
			{
				base[CompilationSection._propBatch] = value;
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06005377 RID: 21367 RVA: 0x0012560C File Offset: 0x0012380C
		// (set) Token: 0x06005378 RID: 21368 RVA: 0x0012561E File Offset: 0x0012381E
		[ConfigurationProperty("optimizeCompilations", DefaultValue = false)]
		public bool OptimizeCompilations
		{
			get
			{
				return (bool)base[CompilationSection._propOptimizeCompilations];
			}
			set
			{
				base[CompilationSection._propOptimizeCompilations] = value;
			}
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x00125631 File Offset: 0x00123831
		// (set) Token: 0x0600537A RID: 21370 RVA: 0x00125643 File Offset: 0x00123843
		[ConfigurationProperty("urlLinePragmas", DefaultValue = false)]
		public bool UrlLinePragmas
		{
			get
			{
				return (bool)base[CompilationSection._propUrlLinePragmas];
			}
			set
			{
				base[CompilationSection._propUrlLinePragmas] = value;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x00125656 File Offset: 0x00123856
		// (set) Token: 0x0600537C RID: 21372 RVA: 0x00125668 File Offset: 0x00123868
		[ConfigurationProperty("batchTimeout", DefaultValue = "00:15:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan BatchTimeout
		{
			get
			{
				return (TimeSpan)base[CompilationSection._propBatchTimeout];
			}
			set
			{
				base[CompilationSection._propBatchTimeout] = value;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x0012567B File Offset: 0x0012387B
		// (set) Token: 0x0600537E RID: 21374 RVA: 0x0012568D File Offset: 0x0012388D
		[ConfigurationProperty("maxBatchSize", DefaultValue = 1000)]
		public int MaxBatchSize
		{
			get
			{
				return (int)base[CompilationSection._propMaxBatchSize];
			}
			set
			{
				base[CompilationSection._propMaxBatchSize] = value;
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x001256A0 File Offset: 0x001238A0
		// (set) Token: 0x06005380 RID: 21376 RVA: 0x001256B2 File Offset: 0x001238B2
		[ConfigurationProperty("maxBatchGeneratedFileSize", DefaultValue = 1000)]
		public int MaxBatchGeneratedFileSize
		{
			get
			{
				return (int)base[CompilationSection._propMaxBatchGeneratedFileSize];
			}
			set
			{
				base[CompilationSection._propMaxBatchGeneratedFileSize] = value;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x001256C5 File Offset: 0x001238C5
		// (set) Token: 0x06005382 RID: 21378 RVA: 0x001256D7 File Offset: 0x001238D7
		[ConfigurationProperty("numRecompilesBeforeAppRestart", DefaultValue = 15)]
		public int NumRecompilesBeforeAppRestart
		{
			get
			{
				return (int)base[CompilationSection._propNumRecompilesBeforeAppRestart];
			}
			set
			{
				base[CompilationSection._propNumRecompilesBeforeAppRestart] = value;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x001256EA File Offset: 0x001238EA
		// (set) Token: 0x06005384 RID: 21380 RVA: 0x001256FC File Offset: 0x001238FC
		[ConfigurationProperty("defaultLanguage", DefaultValue = "vb")]
		public string DefaultLanguage
		{
			get
			{
				return (string)base[CompilationSection._propDefaultLanguage];
			}
			set
			{
				base[CompilationSection._propDefaultLanguage] = value;
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x0012570A File Offset: 0x0012390A
		// (set) Token: 0x06005386 RID: 21382 RVA: 0x0012571C File Offset: 0x0012391C
		[ConfigurationProperty("targetFramework", DefaultValue = null)]
		public string TargetFramework
		{
			get
			{
				return (string)base[CompilationSection._propTargetFramework];
			}
			set
			{
				base[CompilationSection._propTargetFramework] = value;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06005387 RID: 21383 RVA: 0x0012572A File Offset: 0x0012392A
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CompilationSection._propCompilers];
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x0012573C File Offset: 0x0012393C
		[ConfigurationProperty("assemblies")]
		public AssemblyCollection Assemblies
		{
			get
			{
				if (this._isRuntimeObject || BuildManagerHost.InClientBuildManager)
				{
					this.EnsureReferenceSet();
				}
				return this.GetAssembliesCollection();
			}
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x00125759 File Offset: 0x00123959
		private AssemblyCollection GetAssembliesCollection()
		{
			return (AssemblyCollection)base[CompilationSection._propAssemblies];
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x0600538A RID: 21386 RVA: 0x0012576B File Offset: 0x0012396B
		[ConfigurationProperty("buildProviders")]
		public BuildProviderCollection BuildProviders
		{
			get
			{
				return (BuildProviderCollection)base[CompilationSection._propBuildProviders];
			}
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0012577D File Offset: 0x0012397D
		private FolderLevelBuildProviderCollection GetFolderLevelBuildProviders()
		{
			return (FolderLevelBuildProviderCollection)base[CompilationSection._propFolderLevelBuildProviders];
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x0600538C RID: 21388 RVA: 0x0012578F File Offset: 0x0012398F
		[ConfigurationProperty("folderLevelBuildProviders")]
		public FolderLevelBuildProviderCollection FolderLevelBuildProviders
		{
			get
			{
				return this.GetFolderLevelBuildProviders();
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x00125797 File Offset: 0x00123997
		[ConfigurationProperty("expressionBuilders")]
		public ExpressionBuilderCollection ExpressionBuilders
		{
			get
			{
				return (ExpressionBuilderCollection)base[CompilationSection._propExpressionBuilders];
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x0600538E RID: 21390 RVA: 0x001257A9 File Offset: 0x001239A9
		// (set) Token: 0x0600538F RID: 21391 RVA: 0x001257BB File Offset: 0x001239BB
		[ConfigurationProperty("assemblyPostProcessorType", DefaultValue = "")]
		public string AssemblyPostProcessorType
		{
			get
			{
				return (string)base[CompilationSection._propAssemblyPreprocessorType];
			}
			set
			{
				base[CompilationSection._propAssemblyPreprocessorType] = value;
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06005390 RID: 21392 RVA: 0x001257CC File Offset: 0x001239CC
		internal Type AssemblyPostProcessorTypeInternal
		{
			get
			{
				if (this._assemblyPostProcessorType == null && !string.IsNullOrEmpty(this.AssemblyPostProcessorType))
				{
					lock (this)
					{
						if (this._assemblyPostProcessorType == null)
						{
							if (!HttpRuntime.HasUnmanagedPermission())
							{
								throw new ConfigurationErrorsException(SR.GetString("Insufficient_trust_for_attribute", new object[]
								{
									"assemblyPostProcessorType"
								}), base.ElementInformation.Properties["assemblyPostProcessorType"].Source, base.ElementInformation.Properties["assemblyPostProcessorType"].LineNumber);
							}
							Type type = ConfigUtil.GetType(this.AssemblyPostProcessorType, "assemblyPostProcessorType", this);
							ConfigUtil.CheckBaseType(typeof(IAssemblyPostProcessor), type, "assemblyPostProcessorType", this);
							this._assemblyPostProcessorType = type;
						}
					}
				}
				return this._assemblyPostProcessorType;
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x001258C4 File Offset: 0x00123AC4
		[ConfigurationProperty("codeSubDirectories")]
		public CodeSubDirectoriesCollection CodeSubDirectories
		{
			get
			{
				return (CodeSubDirectoriesCollection)base[CompilationSection._propCodeSubDirs];
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06005392 RID: 21394 RVA: 0x001258D6 File Offset: 0x00123AD6
		// (set) Token: 0x06005393 RID: 21395 RVA: 0x001258E8 File Offset: 0x00123AE8
		[ConfigurationProperty("enablePrefetchOptimization", DefaultValue = false)]
		public bool EnablePrefetchOptimization
		{
			get
			{
				return (bool)base[CompilationSection._propEnablePrefetchOptimization];
			}
			set
			{
				base[CompilationSection._propEnablePrefetchOptimization] = value;
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x001258FB File Offset: 0x00123AFB
		// (set) Token: 0x06005395 RID: 21397 RVA: 0x0012590D File Offset: 0x00123B0D
		[ConfigurationProperty("profileGuidedOptimizations", DefaultValue = ProfileGuidedOptimizationsFlags.All)]
		public ProfileGuidedOptimizationsFlags ProfileGuidedOptimizations
		{
			get
			{
				return (ProfileGuidedOptimizationsFlags)base[CompilationSection._propProfileGuidedOptimizations];
			}
			set
			{
				base[CompilationSection._propProfileGuidedOptimizations] = value;
			}
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x00125920 File Offset: 0x00123B20
		// (set) Token: 0x06005397 RID: 21399 RVA: 0x00125932 File Offset: 0x00123B32
		[ConfigurationProperty("controlBuilderInterceptorType", DefaultValue = "")]
		public string ControlBuilderInterceptorType
		{
			get
			{
				return (string)base[CompilationSection._propControlBuilderInterceptorType];
			}
			set
			{
				base[CompilationSection._propControlBuilderInterceptorType] = value;
			}
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x00125940 File Offset: 0x00123B40
		// (set) Token: 0x06005399 RID: 21401 RVA: 0x00125952 File Offset: 0x00123B52
		[ConfigurationProperty("disableObsoleteWarnings", DefaultValue = true)]
		public bool DisableObsoleteWarnings
		{
			get
			{
				return (bool)base[CompilationSection._propDisableObsoleteWarnings];
			}
			set
			{
				base[CompilationSection._propDisableObsoleteWarnings] = value;
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x0600539A RID: 21402 RVA: 0x00125965 File Offset: 0x00123B65
		// (set) Token: 0x0600539B RID: 21403 RVA: 0x00125977 File Offset: 0x00123B77
		[ConfigurationProperty("maxConcurrentCompilations", DefaultValue = 1)]
		public int MaxConcurrentCompilations
		{
			get
			{
				return (int)base[CompilationSection._propMaxConcurrentCompilations];
			}
			set
			{
				base[CompilationSection._propMaxConcurrentCompilations] = value;
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0012598C File Offset: 0x00123B8C
		private void EnsureCompilerCacheInit()
		{
			if (this._compilerLanguages == null)
			{
				lock (this)
				{
					if (this._compilerLanguages == null)
					{
						Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
						this._compilerExtensions = new Hashtable(StringComparer.OrdinalIgnoreCase);
						foreach (object obj in this.Compilers)
						{
							Compiler compiler = (Compiler)obj;
							string[] array = compiler.Language.Split(new char[]
							{
								';'
							});
							string[] array2 = compiler.Extension.Split(new char[]
							{
								';'
							});
							foreach (string key in array)
							{
								hashtable[key] = compiler;
							}
							foreach (string key2 in array2)
							{
								this._compilerExtensions[key2] = compiler;
							}
						}
						this._compilerLanguages = hashtable;
					}
				}
			}
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x00125ACC File Offset: 0x00123CCC
		internal CompilerType GetCompilerInfoFromExtension(string extension, bool throwOnFail)
		{
			this.EnsureCompilerCacheInit();
			object obj = this._compilerExtensions[extension];
			Compiler compiler = obj as Compiler;
			CompilerType compilerType;
			if (compiler != null)
			{
				compilerType = compiler.CompilerTypeInternal;
				this._compilerExtensions[extension] = compilerType;
			}
			else
			{
				compilerType = (obj as CompilerType);
			}
			if (compilerType == null && CodeDomProvider.IsDefinedExtension(extension))
			{
				string languageFromExtension = CodeDomProvider.GetLanguageFromExtension(extension);
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(languageFromExtension);
				compilerType = new CompilerType(compilerInfo.CodeDomProviderType, compilerInfo.CreateDefaultCompilerParameters());
				this._compilerExtensions[extension] = compilerType;
			}
			if (compilerType != null)
			{
				compilerType = compilerType.Clone();
				compilerType.CompilerParameters.IncludeDebugInformation = this.Debug;
				return compilerType;
			}
			if (!throwOnFail)
			{
				return null;
			}
			throw new HttpException(SR.GetString("Invalid_lang_extension", new object[]
			{
				extension
			}));
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00125B8C File Offset: 0x00123D8C
		internal CompilerType GetCompilerInfoFromLanguage(string language)
		{
			this.EnsureCompilerCacheInit();
			object obj = this._compilerLanguages[language];
			Compiler compiler = obj as Compiler;
			CompilerType compilerType;
			if (compiler != null)
			{
				compilerType = compiler.CompilerTypeInternal;
				this._compilerLanguages[language] = compilerType;
			}
			else
			{
				compilerType = (obj as CompilerType);
			}
			if (compilerType == null && CodeDomProvider.IsDefinedLanguage(language))
			{
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
				compilerType = new CompilerType(compilerInfo.CodeDomProviderType, compilerInfo.CreateDefaultCompilerParameters());
				this._compilerLanguages[language] = compilerType;
			}
			if (compilerType == null)
			{
				throw new HttpException(SR.GetString("Invalid_lang", new object[]
				{
					language
				}));
			}
			CompilationUtil.CheckCompilerOptionsAllowed(compilerType.CompilerParameters.CompilerOptions, true, null, 0);
			compilerType = compilerType.Clone();
			compilerType.CompilerParameters.IncludeDebugInformation = this.Debug;
			return compilerType;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00125C50 File Offset: 0x00123E50
		private void EnsureReferenceSet()
		{
			if (!this._referenceSet)
			{
				foreach (object obj in this.GetAssembliesCollection())
				{
					AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
					assemblyInfo.SetCompilationReference(this);
				}
				this._referenceSet = true;
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00125CB8 File Offset: 0x00123EB8
		internal static string GetOriginalAssemblyName(Assembly a)
		{
			string result = null;
			if (!CompilationSection._assemblyNames.Value.TryGetValue(a, out result))
			{
				result = a.FullName;
			}
			return result;
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x00125CE4 File Offset: 0x00123EE4
		internal Assembly[] LoadAssembly(AssemblyInfo ai)
		{
			Assembly[] result = null;
			if (ai.Assembly == "*")
			{
				result = this.LoadAllAssembliesFromAppDomainBinDirectory();
			}
			else
			{
				Assembly assembly = this.LoadAssemblyHelper(ai.Assembly, false);
				if (assembly != null)
				{
					result = new Assembly[]
					{
						assembly
					};
					CompilationSection.RecordAssembly(ai.Assembly, assembly);
				}
			}
			return result;
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00125D40 File Offset: 0x00123F40
		internal static Assembly LoadAndRecordAssembly(AssemblyName name)
		{
			Assembly assembly = Assembly.Load(name);
			CompilationSection.RecordAssembly(name.FullName, assembly);
			return assembly;
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x00125D61 File Offset: 0x00123F61
		internal static void RecordAssembly(string assemblyName, Assembly a)
		{
			if (!CompilationSection._assemblyNames.Value.ContainsKey(a))
			{
				CompilationSection._assemblyNames.Value.TryAdd(a, assemblyName);
			}
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x00125D88 File Offset: 0x00123F88
		internal Assembly LoadAssembly(string assemblyName, bool throwOnFail)
		{
			try
			{
				Assembly assembly = Assembly.Load(assemblyName);
				CompilationSection.RecordAssembly(assemblyName, assembly);
				return assembly;
			}
			catch
			{
				AssemblyName assemblyName2 = new AssemblyName(assemblyName);
				byte[] publicKeyToken = assemblyName2.GetPublicKeyToken();
				if ((publicKeyToken == null || publicKeyToken.Length == 0) && assemblyName2.Version == null)
				{
					this.EnsureReferenceSet();
					foreach (object obj in this.GetAssembliesCollection())
					{
						AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
						Assembly[] assemblyInternal = assemblyInfo.AssemblyInternal;
						if (assemblyInternal != null)
						{
							for (int i = 0; i < assemblyInternal.Length; i++)
							{
								if (StringUtil.EqualsIgnoreCase(assemblyName2.Name, new AssemblyName(assemblyInternal[i].FullName).Name))
								{
									return assemblyInternal[i];
								}
							}
						}
					}
				}
				if (throwOnFail)
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00125E88 File Offset: 0x00124088
		private Assembly LoadAssemblyHelper(string assemblyName, bool starDirective)
		{
			Assembly result = null;
			try
			{
				result = Assembly.Load(assemblyName);
			}
			catch (Exception ex)
			{
				bool flag = false;
				if (starDirective)
				{
					int hrforException = Marshal.GetHRForException(ex);
					if (hrforException == -2146234344)
					{
						flag = true;
					}
				}
				if (BuildManager.IgnoreBadImageFormatException)
				{
					BadImageFormatException ex2 = ex as BadImageFormatException;
					if (ex2 != null)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					string text = ex.Message;
					if (string.IsNullOrEmpty(text))
					{
						if (ex is FileLoadException)
						{
							text = SR.GetString("Config_base_file_load_exception_no_message", new object[]
							{
								"assembly"
							});
						}
						else if (ex is BadImageFormatException)
						{
							text = SR.GetString("Config_base_bad_image_exception_no_message", new object[]
							{
								assemblyName
							});
						}
						else
						{
							text = SR.GetString("Config_base_report_exception_type", new object[]
							{
								ex.GetType().ToString()
							});
						}
					}
					string source = base.ElementInformation.Properties["assemblies"].Source;
					int lineNumber = base.ElementInformation.Properties["assemblies"].LineNumber;
					if (starDirective)
					{
						assemblyName = "*";
					}
					if (this.Assemblies[assemblyName] != null)
					{
						source = this.Assemblies[assemblyName].ElementInformation.Source;
						lineNumber = this.Assemblies[assemblyName].ElementInformation.LineNumber;
					}
					throw new ConfigurationErrorsException(text, ex, source, lineNumber);
				}
			}
			return result;
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00125FF8 File Offset: 0x001241F8
		internal Assembly[] LoadAllAssembliesFromAppDomainBinDirectory()
		{
			string binDirectoryInternal = HttpRuntime.BinDirectoryInternal;
			Assembly assembly = null;
			Assembly[] array = null;
			if (FileUtil.DirectoryExists(binDirectoryInternal))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(binDirectoryInternal);
				FileInfo[] files = directoryInfo.GetFiles("*.dll");
				if (files.Length != 0)
				{
					ArrayList arrayList = new ArrayList(files.Length);
					for (int i = 0; i < files.Length; i++)
					{
						string assemblyNameFromFileName = Util.GetAssemblyNameFromFileName(files[i].Name);
						if (!assemblyNameFromFileName.StartsWith("App_Web_", StringComparison.Ordinal))
						{
							if (!this.GetAssembliesCollection().IsRemoved(assemblyNameFromFileName))
							{
								assembly = this.LoadAssemblyHelper(assemblyNameFromFileName, true);
							}
							if (assembly != null)
							{
								arrayList.Add(assembly);
							}
						}
					}
					array = (Assembly[])arrayList.ToArray(typeof(Assembly));
				}
			}
			if (array == null)
			{
				array = new Assembly[0];
			}
			return array;
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x001260BC File Offset: 0x001242BC
		internal long RecompilationHash
		{
			get
			{
				if (this._recompilationHash == -1L)
				{
					lock (this)
					{
						if (this._recompilationHash == -1L)
						{
							this._recompilationHash = CompilationUtil.GetRecompilationHash(this);
						}
					}
				}
				return this._recompilationHash;
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00126118 File Offset: 0x00124318
		protected override void PostDeserialize()
		{
			WebContext webContext = base.EvaluationContext.HostingContext as WebContext;
			if (webContext != null && webContext.ApplicationLevel == WebApplicationLevel.BelowApplication)
			{
				if (this.CodeSubDirectories.ElementInformation.IsPresent)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_element_below_app_illegal", new object[]
					{
						CompilationSection._propCodeSubDirs.Name
					}), this.CodeSubDirectories.ElementInformation.Source, this.CodeSubDirectories.ElementInformation.LineNumber);
				}
				if (this.BuildProviders.ElementInformation.IsPresent)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_element_below_app_illegal", new object[]
					{
						CompilationSection._propBuildProviders.Name
					}), this.BuildProviders.ElementInformation.Source, this.BuildProviders.ElementInformation.LineNumber);
				}
				if (this.FolderLevelBuildProviders.ElementInformation.IsPresent)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_element_below_app_illegal", new object[]
					{
						CompilationSection._propFolderLevelBuildProviders.Name
					}), this.FolderLevelBuildProviders.ElementInformation.Source, this.FolderLevelBuildProviders.ElementInformation.LineNumber);
				}
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x00126248 File Offset: 0x00124448
		internal Type ControlBuilderInterceptorTypeInternal
		{
			get
			{
				if (this._controlBuilderInterceptorType == null && !string.IsNullOrWhiteSpace(this.ControlBuilderInterceptorType))
				{
					lock (this)
					{
						if (this._controlBuilderInterceptorType == null)
						{
							this._controlBuilderInterceptorType = CompilationUtil.LoadTypeWithChecks(this.ControlBuilderInterceptorType, typeof(ControlBuilderInterceptor), null, this, "controlBuilderInterceptorType");
						}
					}
				}
				return this._controlBuilderInterceptorType;
			}
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x001262D0 File Offset: 0x001244D0
		protected override void SetReadOnly()
		{
			ConfigUtil.SetFX45DefaultValue(this, CompilationSection._propTargetFramework, BinaryCompatibility.Current.TargetFramework.ToString());
			base.SetReadOnly();
		}

		// Token: 0x04002BE7 RID: 11239
		private const string tempDirectoryAttributeName = "tempDirectory";

		// Token: 0x04002BE8 RID: 11240
		private const string assemblyPostProcessorTypeAttributeName = "assemblyPostProcessorType";

		// Token: 0x04002BE9 RID: 11241
		private const string controlBuilderInterceptorTypeAttributeName = "controlBuilderInterceptorType";

		// Token: 0x04002BEA RID: 11242
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002BEB RID: 11243
		private static readonly ConfigurationProperty _propTempDirectory = new ConfigurationProperty("tempDirectory", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002BEC RID: 11244
		private static readonly ConfigurationProperty _propDebug = new ConfigurationProperty("debug", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002BED RID: 11245
		private static readonly ConfigurationProperty _propStrict = new ConfigurationProperty("strict", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002BEE RID: 11246
		private static readonly ConfigurationProperty _propExplicit = new ConfigurationProperty("explicit", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002BEF RID: 11247
		private static readonly ConfigurationProperty _propBatch = new ConfigurationProperty("batch", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF0 RID: 11248
		private static readonly ConfigurationProperty _propOptimizeCompilations = new ConfigurationProperty("optimizeCompilations", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF1 RID: 11249
		private static readonly ConfigurationProperty _propBatchTimeout = new ConfigurationProperty("batchTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(15.0), StdValidatorsAndConverters.TimeSpanSecondsOrInfiniteConverter, StdValidatorsAndConverters.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF2 RID: 11250
		private static readonly ConfigurationProperty _propMaxBatchSize = new ConfigurationProperty("maxBatchSize", typeof(int), 1000, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF3 RID: 11251
		private static readonly ConfigurationProperty _propMaxBatchGeneratedFileSize = new ConfigurationProperty("maxBatchGeneratedFileSize", typeof(int), 1000, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF4 RID: 11252
		private static readonly ConfigurationProperty _propNumRecompilesBeforeAppRestart = new ConfigurationProperty("numRecompilesBeforeAppRestart", typeof(int), 15, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF5 RID: 11253
		private static readonly ConfigurationProperty _propDefaultLanguage = new ConfigurationProperty("defaultLanguage", typeof(string), "vb", ConfigurationPropertyOptions.None);

		// Token: 0x04002BF6 RID: 11254
		private static readonly ConfigurationProperty _propTargetFramework = new ConfigurationProperty("targetFramework", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002BF7 RID: 11255
		private static readonly ConfigurationProperty _propCompilers = new ConfigurationProperty("compilers", typeof(CompilerCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002BF8 RID: 11256
		private static readonly ConfigurationProperty _propAssemblies = new ConfigurationProperty("assemblies", typeof(AssemblyCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002BF9 RID: 11257
		private static readonly ConfigurationProperty _propBuildProviders = new ConfigurationProperty("buildProviders", typeof(BuildProviderCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002BFA RID: 11258
		private static readonly ConfigurationProperty _propFolderLevelBuildProviders = new ConfigurationProperty("folderLevelBuildProviders", typeof(FolderLevelBuildProviderCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002BFB RID: 11259
		private static readonly ConfigurationProperty _propExpressionBuilders = new ConfigurationProperty("expressionBuilders", typeof(ExpressionBuilderCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002BFC RID: 11260
		private static readonly ConfigurationProperty _propUrlLinePragmas = new ConfigurationProperty("urlLinePragmas", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002BFD RID: 11261
		private static readonly ConfigurationProperty _propCodeSubDirs = new ConfigurationProperty("codeSubDirectories", typeof(CodeSubDirectoriesCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002BFE RID: 11262
		private static readonly ConfigurationProperty _propAssemblyPreprocessorType = new ConfigurationProperty("assemblyPostProcessorType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002BFF RID: 11263
		private static readonly ConfigurationProperty _propEnablePrefetchOptimization = new ConfigurationProperty("enablePrefetchOptimization", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C00 RID: 11264
		private static readonly ConfigurationProperty _propProfileGuidedOptimizations = new ConfigurationProperty("profileGuidedOptimizations", typeof(ProfileGuidedOptimizationsFlags), ProfileGuidedOptimizationsFlags.All, ConfigurationPropertyOptions.None);

		// Token: 0x04002C01 RID: 11265
		private static readonly ConfigurationProperty _propControlBuilderInterceptorType = new ConfigurationProperty("controlBuilderInterceptorType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C02 RID: 11266
		private static readonly ConfigurationProperty _propDisableObsoleteWarnings = new ConfigurationProperty("disableObsoleteWarnings", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002C03 RID: 11267
		private static readonly ConfigurationProperty _propMaxConcurrentCompilations = new ConfigurationProperty("maxConcurrentCompilations", typeof(int), 1, ConfigurationPropertyOptions.None);

		// Token: 0x04002C04 RID: 11268
		private const char fieldSeparator = ';';

		// Token: 0x04002C05 RID: 11269
		private bool _referenceSet;

		// Token: 0x04002C06 RID: 11270
		private Hashtable _compilerLanguages;

		// Token: 0x04002C07 RID: 11271
		private Hashtable _compilerExtensions;

		// Token: 0x04002C08 RID: 11272
		private long _recompilationHash = -1L;

		// Token: 0x04002C09 RID: 11273
		private bool _isRuntimeObject;

		// Token: 0x04002C0A RID: 11274
		private Type _assemblyPostProcessorType;

		// Token: 0x04002C0B RID: 11275
		private Type _controlBuilderInterceptorType;

		// Token: 0x04002C0C RID: 11276
		private static readonly Lazy<ConcurrentDictionary<Assembly, string>> _assemblyNames = new Lazy<ConcurrentDictionary<Assembly, string>>();
	}
}
