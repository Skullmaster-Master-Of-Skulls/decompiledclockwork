using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000830 RID: 2096
	internal class CodeDirectoryCompiler
	{
		// Token: 0x060063FB RID: 25595 RVA: 0x0015E169 File Offset: 0x0015C369
		internal static bool IsResourceCodeDirectoryType(CodeDirectoryType dirType)
		{
			return dirType == CodeDirectoryType.AppResources || dirType == CodeDirectoryType.LocalResources;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x0015E178 File Offset: 0x0015C378
		internal static Assembly GetCodeDirectoryAssembly(VirtualPath virtualDir, CodeDirectoryType dirType, string assemblyName, StringSet excludedSubdirectories, bool isDirectoryAllowed)
		{
			string text = virtualDir.MapPath();
			if (!isDirectoryAllowed && Directory.Exists(text))
			{
				throw new HttpException(SR.GetString("Bar_dir_in_precompiled_app", new object[]
				{
					virtualDir
				}));
			}
			bool flag = CodeDirectoryCompiler.IsResourceCodeDirectoryType(dirType);
			BuildResult buildResult = BuildManager.GetBuildResultFromCache(assemblyName);
			Assembly assembly = null;
			if (buildResult != null && buildResult is BuildResultCompiledAssembly)
			{
				if (buildResult is BuildResultMainCodeAssembly)
				{
					CodeDirectoryCompiler._mainCodeBuildResult = (BuildResultMainCodeAssembly)buildResult;
				}
				assembly = ((BuildResultCompiledAssembly)buildResult).ResultAssembly;
				if (!flag)
				{
					return assembly;
				}
				if (!isDirectoryAllowed)
				{
					return assembly;
				}
				BuildResultResourceAssembly buildResultResourceAssembly = (BuildResultResourceAssembly)buildResult;
				string directoryHash = HashCodeCombiner.GetDirectoryHash(virtualDir);
				if (directoryHash == buildResultResourceAssembly.ResourcesDependenciesHash)
				{
					return assembly;
				}
			}
			if (!isDirectoryAllowed)
			{
				return null;
			}
			if (dirType != CodeDirectoryType.LocalResources && !StringUtil.StringStartsWithIgnoreCase(text, HttpRuntime.AppDomainAppPathInternal))
			{
				throw new HttpException(SR.GetString("Virtual_codedir", new object[]
				{
					virtualDir.VirtualPathString
				}));
			}
			if (!Directory.Exists(text))
			{
				if (dirType != CodeDirectoryType.MainCode)
				{
					return null;
				}
				if (!ProfileBuildProvider.HasCompilableProfile)
				{
					return null;
				}
			}
			BuildManager.ReportDirectoryCompilationProgress(virtualDir);
			DateTime utcNow = DateTime.UtcNow;
			CodeDirectoryCompiler codeDirectoryCompiler = new CodeDirectoryCompiler(virtualDir, dirType, excludedSubdirectories);
			string outputAssemblyName;
			if (assembly != null)
			{
				outputAssemblyName = assembly.GetName().Name;
				codeDirectoryCompiler._onlyBuildLocalizedResources = true;
			}
			else
			{
				outputAssemblyName = BuildManager.GenerateRandomAssemblyName(assemblyName);
			}
			BuildProvidersCompiler buildProvidersCompiler = new BuildProvidersCompiler(virtualDir, flag, outputAssemblyName);
			codeDirectoryCompiler._bpc = buildProvidersCompiler;
			codeDirectoryCompiler.FindBuildProviders();
			buildProvidersCompiler.SetBuildProviders(codeDirectoryCompiler._buildProviders);
			CompilerResults compilerResults = buildProvidersCompiler.PerformBuild();
			if (compilerResults != null)
			{
				DateTime t = DateTime.UtcNow.AddMilliseconds(3000.0);
				do
				{
					IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(compilerResults.PathToAssembly);
					if (moduleHandle == IntPtr.Zero)
					{
						goto IL_1CE;
					}
					Thread.Sleep(250);
				}
				while (!(DateTime.UtcNow > t));
				throw new HttpException(SR.GetString("Assembly_already_loaded", new object[]
				{
					compilerResults.PathToAssembly
				}));
				IL_1CE:
				assembly = compilerResults.CompiledAssembly;
			}
			if (assembly == null)
			{
				return null;
			}
			if (dirType == CodeDirectoryType.MainCode)
			{
				CodeDirectoryCompiler._mainCodeBuildResult = new BuildResultMainCodeAssembly(assembly);
				buildResult = CodeDirectoryCompiler._mainCodeBuildResult;
			}
			else if (flag)
			{
				buildResult = new BuildResultResourceAssembly(assembly);
			}
			else
			{
				buildResult = new BuildResultCompiledAssembly(assembly);
			}
			buildResult.VirtualPath = virtualDir;
			if (BuildManager.OptimizeCompilations && dirType != CodeDirectoryType.LocalResources)
			{
				buildResult.AddVirtualPathDependencies(new SingleObjectCollection(virtualDir.AppRelativeVirtualPathString));
			}
			if (dirType != CodeDirectoryType.LocalResources)
			{
				buildResult.CacheToMemory = false;
			}
			BuildManager.CacheBuildResult(assemblyName, buildResult, utcNow);
			return assembly;
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x0015E3CE File Offset: 0x0015C5CE
		internal static void CallAppInitializeMethod()
		{
			if (CodeDirectoryCompiler._mainCodeBuildResult != null)
			{
				CodeDirectoryCompiler._mainCodeBuildResult.CallAppInitializeMethod();
			}
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x0015E3E4 File Offset: 0x0015C5E4
		internal static void GetCodeDirectoryInformation(VirtualPath virtualDir, CodeDirectoryType dirType, StringSet excludedSubdirectories, int index, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
		{
			generatedFilesDir = HttpRuntime.CodegenDirInternal + "\\Sources_" + virtualDir.FileName;
			bool supportLocalization = CodeDirectoryCompiler.IsResourceCodeDirectoryType(dirType);
			BuildProvidersCompiler buildProvidersCompiler = new BuildProvidersCompiler(virtualDir, supportLocalization, generatedFilesDir, index);
			CodeDirectoryCompiler codeDirectoryCompiler = new CodeDirectoryCompiler(virtualDir, dirType, excludedSubdirectories);
			codeDirectoryCompiler._bpc = buildProvidersCompiler;
			codeDirectoryCompiler.FindBuildProviders();
			buildProvidersCompiler.SetBuildProviders(codeDirectoryCompiler._buildProviders);
			buildProvidersCompiler.GenerateSources(out codeDomProviderType, out compilerParameters);
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x0015E448 File Offset: 0x0015C648
		private CodeDirectoryCompiler(VirtualPath virtualDir, CodeDirectoryType dirType, StringSet excludedSubdirectories)
		{
			this._virtualDir = virtualDir;
			this._dirType = dirType;
			this._excludedSubdirectories = excludedSubdirectories;
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x0015E470 File Offset: 0x0015C670
		private void FindBuildProviders()
		{
			if (this._dirType == CodeDirectoryType.MainCode && ProfileBuildProvider.HasCompilableProfile)
			{
				this._buildProviders.Add(ProfileBuildProvider.Create());
			}
			VirtualDirectory directory = HostingEnvironment.VirtualPathProvider.GetDirectory(this._virtualDir);
			this.ProcessDirectoryRecursive(directory, true);
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x0015E4B5 File Offset: 0x0015C6B5
		private void AddFolderLevelBuildProviders(VirtualDirectory vdir, FolderLevelBuildProviderAppliesTo appliesTo)
		{
			BuildManager.AddFolderLevelBuildProviders(this._buildProviders, vdir.VirtualPathObject, appliesTo, this._bpc.CompConfig, this._bpc.ReferencedAssemblies);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x0015E4E0 File Offset: 0x0015C6E0
		private void ProcessDirectoryRecursive(VirtualDirectory vdir, bool topLevel)
		{
			if (this._dirType == CodeDirectoryType.WebReferences)
			{
				BuildProvider buildProvider = new WebReferencesBuildProvider(vdir);
				buildProvider.SetVirtualPath(vdir.VirtualPathObject);
				this._buildProviders.Add(buildProvider);
				this.AddFolderLevelBuildProviders(vdir, FolderLevelBuildProviderAppliesTo.WebReferences);
			}
			else if (this._dirType == CodeDirectoryType.AppResources)
			{
				this.AddFolderLevelBuildProviders(vdir, FolderLevelBuildProviderAppliesTo.GlobalResources);
			}
			else if (this._dirType == CodeDirectoryType.LocalResources)
			{
				this.AddFolderLevelBuildProviders(vdir, FolderLevelBuildProviderAppliesTo.LocalResources);
			}
			else if (this._dirType == CodeDirectoryType.MainCode || this._dirType == CodeDirectoryType.SubCode)
			{
				this.AddFolderLevelBuildProviders(vdir, FolderLevelBuildProviderAppliesTo.Code);
			}
			foreach (object obj in vdir.Children)
			{
				VirtualFileBase virtualFileBase = (VirtualFileBase)obj;
				if (virtualFileBase.IsDirectory)
				{
					if ((!topLevel || this._excludedSubdirectories == null || !this._excludedSubdirectories.Contains(virtualFileBase.Name)) && !(virtualFileBase.Name == "_vti_cnf"))
					{
						this.ProcessDirectoryRecursive(virtualFileBase as VirtualDirectory, false);
					}
				}
				else if (this._dirType != CodeDirectoryType.WebReferences && (!CodeDirectoryCompiler.IsResourceCodeDirectoryType(this._dirType) || !this._onlyBuildLocalizedResources || Util.GetCultureName(virtualFileBase.VirtualPath) != null))
				{
					BuildProvider buildProvider2 = BuildManager.CreateBuildProvider(virtualFileBase.VirtualPathObject, CodeDirectoryCompiler.IsResourceCodeDirectoryType(this._dirType) ? BuildProviderAppliesTo.Resources : BuildProviderAppliesTo.Code, this._bpc.CompConfig, this._bpc.ReferencedAssemblies, false);
					if (buildProvider2 != null)
					{
						if (this._dirType == CodeDirectoryType.LocalResources && buildProvider2 is BaseResourcesBuildProvider)
						{
							((BaseResourcesBuildProvider)buildProvider2).DontGenerateStronglyTypedClass();
						}
						this._buildProviders.Add(buildProvider2);
					}
				}
			}
		}

		// Token: 0x040033C8 RID: 13256
		private VirtualPath _virtualDir;

		// Token: 0x040033C9 RID: 13257
		private CodeDirectoryType _dirType;

		// Token: 0x040033CA RID: 13258
		private StringSet _excludedSubdirectories;

		// Token: 0x040033CB RID: 13259
		private BuildProvidersCompiler _bpc;

		// Token: 0x040033CC RID: 13260
		private BuildProviderSet _buildProviders = new BuildProviderSet();

		// Token: 0x040033CD RID: 13261
		private bool _onlyBuildLocalizedResources;

		// Token: 0x040033CE RID: 13262
		internal static BuildResultMainCodeAssembly _mainCodeBuildResult;

		// Token: 0x040033CF RID: 13263
		internal const string sourcesDirectoryPrefix = "Sources_";
	}
}
