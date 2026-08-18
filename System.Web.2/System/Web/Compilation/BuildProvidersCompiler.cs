using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace System.Web.Compilation
{
	// Token: 0x0200080E RID: 2062
	internal class BuildProvidersCompiler
	{
		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x060062D9 RID: 25305 RVA: 0x0015A5FF File Offset: 0x001587FF
		internal ICollection ReferencedAssemblies
		{
			get
			{
				return this._referencedAssemblies;
			}
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x0015A607 File Offset: 0x00158807
		internal BuildProvidersCompiler(VirtualPath configPath, string outputAssemblyName) : this(configPath, false, outputAssemblyName)
		{
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x0015A612 File Offset: 0x00158812
		internal BuildProvidersCompiler(VirtualPath configPath, bool supportLocalization, string outputAssemblyName)
		{
			this._configPath = configPath;
			this._supportLocalization = supportLocalization;
			this._compConfig = MTConfigUtil.GetCompilationConfig(this._configPath);
			this._referencedAssemblies = BuildManager.GetReferencedAssemblies(this.CompConfig);
			this._outputAssemblyName = outputAssemblyName;
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x0015A654 File Offset: 0x00158854
		internal BuildProvidersCompiler(VirtualPath configPath, bool supportLocalization, string generatedFilesDir, int index)
		{
			this._configPath = configPath;
			this._supportLocalization = supportLocalization;
			this._compConfig = MTConfigUtil.GetCompilationConfig(this._configPath);
			this._referencedAssemblies = BuildManager.GetReferencedAssemblies(this.CompConfig, index);
			this._generatedFilesDir = generatedFilesDir;
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x060062DD RID: 25309 RVA: 0x0015A6A0 File Offset: 0x001588A0
		internal CompilationSection CompConfig
		{
			get
			{
				return this._compConfig;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x060062DE RID: 25310 RVA: 0x0015A6A8 File Offset: 0x001588A8
		internal string OutputAssemblyName
		{
			get
			{
				return this._outputAssemblyName;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x0015A6B0 File Offset: 0x001588B0
		private bool CbmGenerateOnlyMode
		{
			get
			{
				return this._generatedFilesDir != null;
			}
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x0015A6BB File Offset: 0x001588BB
		internal void SetBuildProviders(ICollection buildProviders)
		{
			this._buildProviders = buildProviders;
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x0015A6C4 File Offset: 0x001588C4
		private void ProcessBuildProviders()
		{
			CompilerType compilerType = null;
			BuildProvider buildProvider = null;
			if (this.OutputAssemblyName != null)
			{
				StandardDiskBuildResultCache.RemoveSatelliteAssemblies(this.OutputAssemblyName);
			}
			ArrayList arrayList = null;
			foreach (object obj in this._buildProviders)
			{
				BuildProvider buildProvider2 = (BuildProvider)obj;
				buildProvider2.SetReferencedAssemblies(this._referencedAssemblies);
				if (!BuildManager.ThrowOnFirstParseError)
				{
					InternalBuildProvider internalBuildProvider = buildProvider2 as InternalBuildProvider;
					if (internalBuildProvider != null)
					{
						internalBuildProvider.ThrowOnFirstParseError = false;
					}
				}
				CompilerType compilerTypeFromBuildProvider = BuildProvider.GetCompilerTypeFromBuildProvider(buildProvider2);
				string text = null;
				if (this._supportLocalization)
				{
					text = buildProvider2.GetCultureName();
				}
				if (compilerTypeFromBuildProvider != null)
				{
					if (text != null)
					{
						throw new HttpException(SR.GetString("Both_culture_and_language", new object[]
						{
							BuildProvider.GetDisplayName(buildProvider2)
						}));
					}
					if (compilerType != null)
					{
						if (!compilerTypeFromBuildProvider.Equals(compilerType))
						{
							throw new HttpException(SR.GetString("Inconsistent_language", new object[]
							{
								BuildProvider.GetDisplayName(buildProvider2),
								BuildProvider.GetDisplayName(buildProvider)
							}));
						}
					}
					else
					{
						buildProvider = buildProvider2;
						compilerType = compilerTypeFromBuildProvider;
						this._assemblyBuilder = compilerType.CreateAssemblyBuilder(this.CompConfig, this._referencedAssemblies, this._generatedFilesDir, this.OutputAssemblyName);
					}
				}
				else if (text != null)
				{
					if (!this.CbmGenerateOnlyMode)
					{
						if (this._satelliteAssemblyBuilders == null)
						{
							this._satelliteAssemblyBuilders = new Hashtable(StringComparer.OrdinalIgnoreCase);
						}
						AssemblyBuilder assemblyBuilder = (AssemblyBuilder)this._satelliteAssemblyBuilders[text];
						if (assemblyBuilder == null)
						{
							assemblyBuilder = CompilerType.GetDefaultAssemblyBuilder(this.CompConfig, this._referencedAssemblies, this._configPath, this.OutputAssemblyName);
							assemblyBuilder.CultureName = text;
							this._satelliteAssemblyBuilders[text] = assemblyBuilder;
						}
						assemblyBuilder.AddBuildProvider(buildProvider2);
						continue;
					}
					continue;
				}
				else if (this._assemblyBuilder == null)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(buildProvider2);
					continue;
				}
				this._assemblyBuilder.AddBuildProvider(buildProvider2);
			}
			if (this._assemblyBuilder == null && arrayList != null)
			{
				this._assemblyBuilder = CompilerType.GetDefaultAssemblyBuilder(this.CompConfig, this._referencedAssemblies, this._configPath, this._generatedFilesDir, this.OutputAssemblyName);
			}
			if (this._assemblyBuilder != null && arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					BuildProvider buildProvider3 = (BuildProvider)obj2;
					this._assemblyBuilder.AddBuildProvider(buildProvider3);
				}
			}
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x0015A964 File Offset: 0x00158B64
		internal CompilerResults PerformBuild()
		{
			this.ProcessBuildProviders();
			if (this._satelliteAssemblyBuilders != null)
			{
				int maxDegreeOfParallelism = Math.Min(this._satelliteAssemblyBuilders.Count, CompilationUtil.MaxConcurrentCompilations);
				try
				{
					IEnumerable<AssemblyBuilder> source = this._satelliteAssemblyBuilders.Values.Cast<AssemblyBuilder>();
					ParallelOptions parallelOptions = new ParallelOptions();
					parallelOptions.MaxDegreeOfParallelism = maxDegreeOfParallelism;
					Parallel.ForEach<AssemblyBuilder>(source, parallelOptions, delegate(AssemblyBuilder assemblyBuilder)
					{
						assemblyBuilder.Compile();
					});
				}
				catch (AggregateException ex)
				{
					ExceptionDispatchInfo.Capture(ex.GetBaseException()).Throw();
				}
			}
			if (this._assemblyBuilder != null)
			{
				return this._assemblyBuilder.Compile();
			}
			return null;
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x0015AA10 File Offset: 0x00158C10
		internal void GenerateSources(out Type codeDomProviderType, out CompilerParameters compilerParameters)
		{
			this.ProcessBuildProviders();
			if (this._assemblyBuilder == null)
			{
				this._assemblyBuilder = CompilerType.GetDefaultAssemblyBuilder(this.CompConfig, this._referencedAssemblies, this._configPath, this._generatedFilesDir, null);
			}
			codeDomProviderType = this._assemblyBuilder.CodeDomProviderType;
			compilerParameters = this._assemblyBuilder.GetCompilerParameters();
		}

		// Token: 0x04003348 RID: 13128
		private ICollection _buildProviders;

		// Token: 0x04003349 RID: 13129
		private VirtualPath _configPath;

		// Token: 0x0400334A RID: 13130
		private bool _supportLocalization;

		// Token: 0x0400334B RID: 13131
		private ICollection _referencedAssemblies;

		// Token: 0x0400334C RID: 13132
		private AssemblyBuilder _assemblyBuilder;

		// Token: 0x0400334D RID: 13133
		private IDictionary _satelliteAssemblyBuilders;

		// Token: 0x0400334E RID: 13134
		private string _generatedFilesDir;

		// Token: 0x0400334F RID: 13135
		private CompilationSection _compConfig;

		// Token: 0x04003350 RID: 13136
		private string _outputAssemblyName;
	}
}
