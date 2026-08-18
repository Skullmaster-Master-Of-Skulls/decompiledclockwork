using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x0200080F RID: 2063
	internal class WebDirectoryBatchCompiler
	{
		// Token: 0x060062E4 RID: 25316 RVA: 0x0015AA6C File Offset: 0x00158C6C
		internal WebDirectoryBatchCompiler(VirtualDirectory vdir)
		{
			this._vdir = vdir;
			this._utcStart = DateTime.UtcNow;
			this._compConfig = MTConfigUtil.GetCompilationConfig(this._vdir.VirtualPath);
			this._referencedAssemblies = BuildManager.GetReferencedAssemblies(this._compConfig);
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x0015AAC8 File Offset: 0x00158CC8
		internal void SetIgnoreErrors()
		{
			this._ignoreProvidersWithErrors = true;
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x0015AAD4 File Offset: 0x00158CD4
		internal void Process()
		{
			this.AddBuildProviders(true);
			if (this._buildProviders.Count == 0)
			{
				return;
			}
			BuildManager.ReportDirectoryCompilationProgress(this._vdir.VirtualPathObject);
			this.GetBuildResultDependencies();
			this.ProcessDependencies();
			foreach (ArrayList buildProviders in this._nonDependentBuckets)
			{
				if (!this.CompileNonDependentBuildProviders(buildProviders))
				{
					break;
				}
			}
			if (this._parserErrors != null && this._parserErrors.Count > 0)
			{
				HttpParseException ex = new HttpParseException(this._firstException.Message, this._firstException, this._firstException.VirtualPath, this._firstException.Source, this._firstException.Line);
				for (int j = 1; j < this._parserErrors.Count; j++)
				{
					ex.ParserErrors.Add(this._parserErrors[j]);
				}
				throw ex;
			}
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x0015ABB8 File Offset: 0x00158DB8
		private void AddBuildProviders(bool retryIfDeletionHappens)
		{
			DiskBuildResultCache.ResetAssemblyDeleted();
			foreach (object obj in this._vdir.Files)
			{
				VirtualFile virtualFile = (VirtualFile)obj;
				BuildResult buildResult = null;
				try
				{
					buildResult = BuildManager.GetVPathBuildResultFromCache(virtualFile.VirtualPathObject);
				}
				catch
				{
					if (!BuildManager.PerformingPrecompilation)
					{
						continue;
					}
				}
				if (buildResult == null)
				{
					BuildProvider buildProvider = BuildManager.CreateBuildProvider(virtualFile.VirtualPathObject, this._compConfig, this._referencedAssemblies, false);
					if (buildProvider != null)
					{
						this._buildProviders[virtualFile.VirtualPath] = buildProvider;
					}
				}
			}
			if (DiskBuildResultCache.InUseAssemblyWasDeleted && retryIfDeletionHappens && BuildManager.PerformingPrecompilation)
			{
				this.AddBuildProviders(false);
			}
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x0015AC88 File Offset: 0x00158E88
		private void CacheAssemblyResults(AssemblyBuilder assemblyBuilder, CompilerResults results)
		{
			foreach (object obj in assemblyBuilder.BuildProviders)
			{
				BuildProvider buildProvider = (BuildProvider)obj;
				BuildResult buildResult = buildProvider.GetBuildResult(results);
				if (buildResult != null && !BuildManager.CacheVPathBuildResult(buildProvider.VirtualPathObject, buildResult, this._utcStart))
				{
					break;
				}
			}
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x0015ACFC File Offset: 0x00158EFC
		private void CacheCompileErrors(AssemblyBuilder assemblyBuilder, CompilerResults results)
		{
			BuildProvider buildProvider = null;
			foreach (object obj in results.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning)
				{
					BuildProvider buildProviderFromLinePragma = assemblyBuilder.GetBuildProviderFromLinePragma(compilerError.FileName);
					if (buildProviderFromLinePragma != null && buildProviderFromLinePragma is BaseTemplateBuildProvider && buildProviderFromLinePragma != buildProvider)
					{
						buildProvider = buildProviderFromLinePragma;
						CompilerResults compilerResults = new CompilerResults(null);
						foreach (string value in results.Output)
						{
							compilerResults.Output.Add(value);
						}
						compilerResults.PathToAssembly = results.PathToAssembly;
						compilerResults.NativeCompilerReturnValue = results.NativeCompilerReturnValue;
						compilerResults.Errors.Add(compilerError);
						HttpCompileException compileException = new HttpCompileException(compilerResults, assemblyBuilder.GetGeneratedSourceFromBuildProvider(buildProviderFromLinePragma));
						BuildResult buildResult = new BuildResultCompileError(buildProviderFromLinePragma.VirtualPathObject, compileException);
						buildProviderFromLinePragma.SetBuildResultDependencies(buildResult);
						BuildManager.CacheVPathBuildResult(buildProviderFromLinePragma.VirtualPathObject, buildResult, this._utcStart);
					}
				}
			}
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x0015AE64 File Offset: 0x00159064
		private void GetBuildResultDependencies()
		{
			foreach (object obj in this._buildProviders.Values)
			{
				BuildProvider buildProvider = (BuildProvider)obj;
				ICollection buildResultVirtualPathDependencies = buildProvider.GetBuildResultVirtualPathDependencies();
				if (buildResultVirtualPathDependencies != null)
				{
					foreach (object obj2 in buildResultVirtualPathDependencies)
					{
						string key = (string)obj2;
						BuildProvider buildProvider2 = (BuildProvider)this._buildProviders[key];
						if (buildProvider2 != null)
						{
							buildProvider.AddBuildProviderDependency(buildProvider2);
						}
					}
				}
			}
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x0015AF28 File Offset: 0x00159128
		private void ProcessDependencies()
		{
			int num = 0;
			Hashtable hashtable = new Hashtable();
			Stack stack = new Stack();
			foreach (object obj in this._buildProviders.Values)
			{
				BuildProvider obj2 = (BuildProvider)obj;
				stack.Push(obj2);
				while (stack.Count > 0)
				{
					BuildProvider buildProvider = (BuildProvider)stack.Peek();
					bool flag = false;
					int num2 = 0;
					if (buildProvider.BuildProviderDependencies != null)
					{
						foreach (object obj3 in ((IEnumerable)buildProvider.BuildProviderDependencies))
						{
							BuildProvider buildProvider2 = (BuildProvider)obj3;
							if (hashtable.ContainsKey(buildProvider2))
							{
								if (num2 <= (int)hashtable[buildProvider2])
								{
									num2 = (int)hashtable[buildProvider2] + 1;
								}
								else if ((int)hashtable[buildProvider2] == -1)
								{
									throw new HttpException(SR.GetString("File_Circular_Reference", new object[]
									{
										buildProvider2.VirtualPath
									}));
								}
							}
							else
							{
								flag = true;
								stack.Push(buildProvider2);
							}
						}
					}
					if (flag)
					{
						hashtable[buildProvider] = -1;
					}
					else
					{
						stack.Pop();
						hashtable[buildProvider] = num2;
						if (num <= num2)
						{
							num = num2 + 1;
						}
					}
				}
			}
			this._nonDependentBuckets = new ArrayList[num];
			IDictionaryEnumerator enumerator3 = hashtable.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				int num3 = (int)enumerator3.Value;
				if (this._nonDependentBuckets[num3] == null)
				{
					this._nonDependentBuckets[num3] = new ArrayList();
				}
				this._nonDependentBuckets[num3].Add(enumerator3.Key);
			}
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x0015B12C File Offset: 0x0015932C
		private bool IsBuildProviderSkipable(BuildProvider buildProvider)
		{
			return !buildProvider.IsDependedOn && (buildProvider is SourceFileBuildProvider || buildProvider is ResXBuildProvider);
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x0015B150 File Offset: 0x00159350
		private bool CompileNonDependentBuildProviders(ICollection buildProviders)
		{
			IDictionary dictionary = new Hashtable();
			ArrayList arrayList = null;
			AssemblyBuilder assemblyBuilder = null;
			bool flag = false;
			foreach (object obj in buildProviders)
			{
				BuildProvider buildProvider = (BuildProvider)obj;
				if (!this.IsBuildProviderSkipable(buildProvider))
				{
					if (!BuildManager.ThrowOnFirstParseError)
					{
						InternalBuildProvider internalBuildProvider = buildProvider as InternalBuildProvider;
						if (internalBuildProvider != null)
						{
							internalBuildProvider.ThrowOnFirstParseError = false;
						}
					}
					CompilerType compilerType = null;
					try
					{
						compilerType = BuildProvider.GetCompilerTypeFromBuildProvider(buildProvider);
					}
					catch (HttpParseException ex)
					{
						if (this._ignoreProvidersWithErrors)
						{
							continue;
						}
						flag = true;
						if (this._firstException == null)
						{
							this._firstException = ex;
						}
						if (this._parserErrors == null)
						{
							this._parserErrors = new ParserErrorCollection();
						}
						this._parserErrors.AddRange(ex.ParserErrors);
						continue;
					}
					catch
					{
						if (this._ignoreProvidersWithErrors)
						{
							continue;
						}
						throw;
					}
					AssemblyBuilder assemblyBuilder2 = assemblyBuilder;
					ICollection generatedTypeNames = buildProvider.GetGeneratedTypeNames();
					if (compilerType == null)
					{
						if (assemblyBuilder == null || assemblyBuilder.IsBatchFull || assemblyBuilder.ContainsTypeNames(generatedTypeNames))
						{
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							arrayList.Add(buildProvider);
							continue;
						}
					}
					else
					{
						assemblyBuilder2 = (AssemblyBuilder)dictionary[compilerType];
					}
					if (assemblyBuilder2 == null || assemblyBuilder2.IsBatchFull || assemblyBuilder2.ContainsTypeNames(generatedTypeNames))
					{
						if (assemblyBuilder2 != null)
						{
							this.CompileAssemblyBuilder(assemblyBuilder2);
						}
						AssemblyBuilder assemblyBuilder3 = compilerType.CreateAssemblyBuilder(this._compConfig, this._referencedAssemblies);
						dictionary[compilerType] = assemblyBuilder3;
						if (assemblyBuilder == null || assemblyBuilder == assemblyBuilder2)
						{
							assemblyBuilder = assemblyBuilder3;
						}
						assemblyBuilder2 = assemblyBuilder3;
					}
					assemblyBuilder2.AddTypeNames(generatedTypeNames);
					assemblyBuilder2.AddBuildProvider(buildProvider);
				}
			}
			if (flag)
			{
				return false;
			}
			if (arrayList != null)
			{
				bool flag2 = assemblyBuilder == null;
				foreach (object obj2 in arrayList)
				{
					BuildProvider buildProvider2 = (BuildProvider)obj2;
					ICollection generatedTypeNames2 = buildProvider2.GetGeneratedTypeNames();
					if (assemblyBuilder == null || assemblyBuilder.IsBatchFull || assemblyBuilder.ContainsTypeNames(generatedTypeNames2))
					{
						if (assemblyBuilder != null)
						{
							this.CompileAssemblyBuilder(assemblyBuilder);
						}
						assemblyBuilder = CompilerType.GetDefaultAssemblyBuilder(this._compConfig, this._referencedAssemblies, this._vdir.VirtualPathObject, null);
						flag2 = true;
					}
					assemblyBuilder.AddTypeNames(generatedTypeNames2);
					assemblyBuilder.AddBuildProvider(buildProvider2);
				}
				if (flag2)
				{
					this.CompileAssemblyBuilder(assemblyBuilder);
				}
			}
			this.CompileAssemblyBuilderParallel(dictionary.Values);
			return true;
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x0015B404 File Offset: 0x00159604
		private void CompileAssemblyBuilderParallel(ICollection assemblyBuilders)
		{
			int num = Math.Min(assemblyBuilders.Count, CompilationUtil.MaxConcurrentCompilations);
			if (num < 2)
			{
				using (IEnumerator enumerator = assemblyBuilders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						AssemblyBuilder builder2 = (AssemblyBuilder)obj;
						this.CompileAssemblyBuilder(builder2);
					}
					return;
				}
			}
			ConcurrentDictionary<AssemblyBuilder, CompilerResults> buildResults = new ConcurrentDictionary<AssemblyBuilder, CompilerResults>();
			ConcurrentDictionary<AssemblyBuilder, CompilerResults> buildErrors = new ConcurrentDictionary<AssemblyBuilder, CompilerResults>();
			try
			{
				Parallel.ForEach<AssemblyBuilder>(assemblyBuilders.Cast<AssemblyBuilder>(), new ParallelOptions
				{
					MaxDegreeOfParallelism = num
				}, delegate(AssemblyBuilder builder)
				{
					CompilerResults value;
					try
					{
						value = builder.Compile();
					}
					catch (HttpCompileException ex2)
					{
						buildErrors[builder] = ex2.Results;
						throw;
					}
					buildResults[builder] = value;
				});
			}
			catch (AggregateException ex)
			{
				ExceptionDispatchInfo.Capture(ex.GetBaseException()).Throw();
			}
			finally
			{
				foreach (KeyValuePair<AssemblyBuilder, CompilerResults> keyValuePair in buildErrors)
				{
					this.CacheCompileErrors(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<AssemblyBuilder, CompilerResults> keyValuePair2 in buildResults)
				{
					this.CacheAssemblyResults(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x0015B58C File Offset: 0x0015978C
		private void CompileAssemblyBuilder(AssemblyBuilder builder)
		{
			CompilerResults results;
			try
			{
				results = builder.Compile();
			}
			catch (HttpCompileException ex)
			{
				this.CacheCompileErrors(builder, ex.Results);
				throw;
			}
			this.CacheAssemblyResults(builder, results);
		}

		// Token: 0x04003351 RID: 13137
		private DateTime _utcStart;

		// Token: 0x04003352 RID: 13138
		private ICollection _referencedAssemblies;

		// Token: 0x04003353 RID: 13139
		private CompilationSection _compConfig;

		// Token: 0x04003354 RID: 13140
		private IDictionary _buildProviders = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003355 RID: 13141
		private VirtualDirectory _vdir;

		// Token: 0x04003356 RID: 13142
		private ArrayList[] _nonDependentBuckets;

		// Token: 0x04003357 RID: 13143
		private bool _ignoreProvidersWithErrors;

		// Token: 0x04003358 RID: 13144
		private ParserErrorCollection _parserErrors;

		// Token: 0x04003359 RID: 13145
		private HttpParseException _firstException;
	}
}
