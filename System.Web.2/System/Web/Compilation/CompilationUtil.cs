using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace System.Web.Compilation
{
	// Token: 0x02000834 RID: 2100
	internal static class CompilationUtil
	{
		// Token: 0x06006419 RID: 25625 RVA: 0x0015F190 File Offset: 0x0015D390
		internal static bool IsDebuggingEnabled(HttpContext context)
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(context);
			return compilationConfig.Debug;
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x0015F1AC File Offset: 0x0015D3AC
		internal static bool IsBatchingEnabled(string configPath)
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(configPath);
			return compilationConfig.Batch;
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x0015F1C8 File Offset: 0x0015D3C8
		internal static int GetRecompilationsBeforeAppRestarts()
		{
			CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
			return compilationAppConfig.NumRecompilesBeforeAppRestart;
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x0015F1E1 File Offset: 0x0015D3E1
		internal static CompilerType GetCodeDefaultLanguageCompilerInfo()
		{
			return new CompilerType(typeof(VBCodeProvider), null);
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x0015F1F3 File Offset: 0x0015D3F3
		internal static CompilerType GetDefaultLanguageCompilerInfo(CompilationSection compConfig, VirtualPath configPath)
		{
			if (compConfig == null)
			{
				compConfig = MTConfigUtil.GetCompilationConfig(configPath);
			}
			if (compConfig.DefaultLanguage == null)
			{
				return CompilationUtil.GetCodeDefaultLanguageCompilerInfo();
			}
			return compConfig.GetCompilerInfoFromLanguage(compConfig.DefaultLanguage);
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0015F21C File Offset: 0x0015D41C
		internal static CompilerType GetCompilerInfoFromVirtualPath(VirtualPath virtualPath)
		{
			string extension = virtualPath.Extension;
			if (extension.Length == 0)
			{
				throw new HttpException(SR.GetString("Empty_extension", new object[]
				{
					virtualPath
				}));
			}
			return CompilationUtil.GetCompilerInfoFromExtension(virtualPath, extension);
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x0015F25C File Offset: 0x0015D45C
		private static CompilerType GetCompilerInfoFromExtension(VirtualPath configPath, string extension)
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(configPath);
			return compilationConfig.GetCompilerInfoFromExtension(extension, true);
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x0015F278 File Offset: 0x0015D478
		internal static CompilerType GetCompilerInfoFromLanguage(VirtualPath configPath, string language)
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(configPath);
			return compilationConfig.GetCompilerInfoFromLanguage(language);
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x0015F293 File Offset: 0x0015D493
		internal static CompilerType GetCSharpCompilerInfo(CompilationSection compConfig, VirtualPath configPath)
		{
			if (compConfig == null)
			{
				compConfig = MTConfigUtil.GetCompilationConfig(configPath);
			}
			if (compConfig.DefaultLanguage == null)
			{
				return new CompilerType(typeof(CSharpCodeProvider), null);
			}
			return compConfig.GetCompilerInfoFromLanguage("c#");
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x0015F2C4 File Offset: 0x0015D4C4
		internal static CodeSubDirectoriesCollection GetCodeSubDirectories()
		{
			CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
			CodeSubDirectoriesCollection codeSubDirectories = compilationAppConfig.CodeSubDirectories;
			if (codeSubDirectories != null)
			{
				codeSubDirectories.EnsureRuntimeValidation();
			}
			return codeSubDirectories;
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x0015F2E8 File Offset: 0x0015D4E8
		internal static long GetRecompilationHash(CompilationSection ps)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddObject(ps.Debug);
			hashCodeCombiner.AddObject(ps.TargetFramework);
			hashCodeCombiner.AddObject(ps.Strict);
			hashCodeCombiner.AddObject(ps.Explicit);
			hashCodeCombiner.AddObject(ps.Batch);
			hashCodeCombiner.AddObject(ps.OptimizeCompilations);
			hashCodeCombiner.AddObject(ps.BatchTimeout);
			hashCodeCombiner.AddObject(ps.MaxBatchGeneratedFileSize);
			hashCodeCombiner.AddObject(ps.MaxBatchSize);
			hashCodeCombiner.AddObject(ps.NumRecompilesBeforeAppRestart);
			hashCodeCombiner.AddObject(ps.DefaultLanguage);
			hashCodeCombiner.AddObject(ps.UrlLinePragmas);
			hashCodeCombiner.AddObject(ps.DisableObsoleteWarnings);
			if (ps.AssemblyPostProcessorTypeInternal != null)
			{
				hashCodeCombiner.AddObject(ps.AssemblyPostProcessorTypeInternal.FullName);
			}
			if (!string.IsNullOrWhiteSpace(ps.ControlBuilderInterceptorType))
			{
				hashCodeCombiner.AddObject(ps.ControlBuilderInterceptorType);
			}
			foreach (object obj in ps.Compilers)
			{
				Compiler compiler = (Compiler)obj;
				hashCodeCombiner.AddObject(compiler.Language);
				hashCodeCombiner.AddObject(compiler.Extension);
				hashCodeCombiner.AddObject(compiler.Type);
				hashCodeCombiner.AddObject(compiler.WarningLevel);
				hashCodeCombiner.AddObject(compiler.CompilerOptions);
			}
			foreach (object obj2 in ps.ExpressionBuilders)
			{
				ExpressionBuilder expressionBuilder = (ExpressionBuilder)obj2;
				hashCodeCombiner.AddObject(expressionBuilder.ExpressionPrefix);
				hashCodeCombiner.AddObject(expressionBuilder.Type);
			}
			AssemblyCollection assemblies = ps.Assemblies;
			if (assemblies.Count == 0)
			{
				hashCodeCombiner.AddObject("__clearassemblies");
			}
			else
			{
				foreach (object obj3 in assemblies)
				{
					AssemblyInfo assemblyInfo = (AssemblyInfo)obj3;
					hashCodeCombiner.AddObject(assemblyInfo.Assembly);
				}
			}
			BuildProviderCollection buildProviders = ps.BuildProviders;
			if (buildProviders.Count == 0)
			{
				hashCodeCombiner.AddObject("__clearbuildproviders");
			}
			else
			{
				foreach (object obj4 in buildProviders)
				{
					BuildProvider buildProvider = (BuildProvider)obj4;
					hashCodeCombiner.AddObject(buildProvider.Type);
					hashCodeCombiner.AddObject(buildProvider.Extension);
				}
			}
			FolderLevelBuildProviderCollection folderLevelBuildProviders = ps.FolderLevelBuildProviders;
			if (folderLevelBuildProviders.Count == 0)
			{
				hashCodeCombiner.AddObject("__clearfolderlevelbuildproviders");
			}
			else
			{
				foreach (object obj5 in folderLevelBuildProviders)
				{
					FolderLevelBuildProvider folderLevelBuildProvider = (FolderLevelBuildProvider)obj5;
					hashCodeCombiner.AddObject(folderLevelBuildProvider.Type);
					hashCodeCombiner.AddObject(folderLevelBuildProvider.Name);
				}
			}
			CodeSubDirectoriesCollection codeSubDirectories = ps.CodeSubDirectories;
			if (codeSubDirectories.Count == 0)
			{
				hashCodeCombiner.AddObject("__clearcodesubdirs");
			}
			else
			{
				foreach (object obj6 in codeSubDirectories)
				{
					CodeSubDirectory codeSubDirectory = (CodeSubDirectory)obj6;
					hashCodeCombiner.AddObject(codeSubDirectory.DirectoryName);
				}
			}
			CompilerInfo[] allCompilerInfo = CodeDomProvider.GetAllCompilerInfo();
			if (allCompilerInfo != null)
			{
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo("cpp");
				foreach (CompilerInfo compilerInfo2 in allCompilerInfo)
				{
					if (compilerInfo2 != compilerInfo && compilerInfo2.IsCodeDomProviderTypeValid)
					{
						CompilerParameters compilerParameters = compilerInfo2.CreateDefaultCompilerParameters();
						string compilerOptions = compilerParameters.CompilerOptions;
						if (!string.IsNullOrEmpty(compilerOptions))
						{
							Type codeDomProviderType = compilerInfo2.CodeDomProviderType;
							if (codeDomProviderType != null)
							{
								hashCodeCombiner.AddObject(codeDomProviderType.FullName);
							}
							hashCodeCombiner.AddObject(compilerOptions);
						}
						if (!(compilerInfo2.CodeDomProviderType == null))
						{
							IDictionary<string, string> providerOptions = CompilationUtil.GetProviderOptions(compilerInfo2);
							if (providerOptions != null && providerOptions.Count > 0)
							{
								string fullName = compilerInfo2.CodeDomProviderType.FullName;
								foreach (string text in providerOptions.Keys)
								{
									string text2 = providerOptions[text];
									hashCodeCombiner.AddObject(string.Concat(new string[]
									{
										fullName,
										":",
										text,
										"=",
										text2
									}));
								}
							}
						}
					}
				}
			}
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x0015F7E4 File Offset: 0x0015D9E4
		internal static Type GetBuildProviderTypeFromExtension(VirtualPath configPath, string extension, BuildProviderAppliesTo neededFor, bool failIfUnknown)
		{
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(configPath);
			return CompilationUtil.GetBuildProviderTypeFromExtension(compilationConfig, extension, neededFor, failIfUnknown);
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x0015F804 File Offset: 0x0015DA04
		internal static Type GetBuildProviderTypeFromExtension(CompilationSection config, string extension, BuildProviderAppliesTo neededFor, bool failIfUnknown)
		{
			BuildProviderInfo buildProviderInfo = BuildProvider.GetBuildProviderInfo(config, extension);
			Type type = null;
			if (buildProviderInfo != null && buildProviderInfo.Type != typeof(IgnoreFileBuildProvider) && buildProviderInfo.Type != typeof(ForceCopyBuildProvider))
			{
				type = buildProviderInfo.Type;
			}
			if (neededFor == BuildProviderAppliesTo.Web && BuildManager.PrecompilingForUpdatableDeployment && !typeof(BaseTemplateBuildProvider).IsAssignableFrom(type))
			{
				type = null;
			}
			if (type != null)
			{
				if ((neededFor & buildProviderInfo.AppliesTo) != (BuildProviderAppliesTo)0)
				{
					return type;
				}
			}
			else if (neededFor != BuildProviderAppliesTo.Resources && config.GetCompilerInfoFromExtension(extension, false) != null)
			{
				return typeof(SourceFileBuildProvider);
			}
			if (failIfUnknown)
			{
				throw new HttpException(SR.GetString("Unknown_buildprovider_extension", new object[]
				{
					extension,
					neededFor.ToString()
				}));
			}
			return null;
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x0015F8D0 File Offset: 0x0015DAD0
		internal static List<Type> GetFolderLevelBuildProviderTypes(CompilationSection config, FolderLevelBuildProviderAppliesTo appliesTo)
		{
			FolderLevelBuildProviderCollection folderLevelBuildProviders = config.FolderLevelBuildProviders;
			return folderLevelBuildProviders.GetBuildProviderTypes(appliesTo);
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x0015F8EC File Offset: 0x0015DAEC
		internal static void CheckCompilerDirectoryPathAllowed(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				return;
			}
			if (!providerOptions.ContainsKey("CompilerDirectoryPath"))
			{
				return;
			}
			if (!HttpRuntime.HasUnmanagedPermission())
			{
				string @string = SR.GetString("Insufficient_trust_for_attribute", new object[]
				{
					"CompilerDirectoryPath"
				});
				throw new HttpException(@string);
			}
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x0015F934 File Offset: 0x0015DB34
		internal static void CheckCompilerOptionsAllowed(string compilerOptions, bool config, string file, int line)
		{
			if (string.IsNullOrEmpty(compilerOptions))
			{
				return;
			}
			if (HttpRuntime.HasUnmanagedPermission())
			{
				return;
			}
			string @string = SR.GetString("Insufficient_trust_for_attribute", new object[]
			{
				"compilerOptions"
			});
			if (config)
			{
				throw new ConfigurationErrorsException(@string, file, line);
			}
			throw new HttpException(@string);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x0015F980 File Offset: 0x0015DB80
		internal static bool NeedToCopyFile(VirtualPath virtualPath, bool updatable, out bool createStub)
		{
			createStub = false;
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(virtualPath);
			string extension = virtualPath.Extension;
			BuildProviderInfo buildProviderInfo = BuildProvider.GetBuildProviderInfo(compilationConfig, extension);
			if (buildProviderInfo == null)
			{
				return compilationConfig.GetCompilerInfoFromExtension(extension, false) == null && !StringUtil.EqualsIgnoreCase(extension, ".asax") && (updatable || !StringUtil.EqualsIgnoreCase(extension, ".skin"));
			}
			if ((BuildProviderAppliesTo.Web & buildProviderInfo.AppliesTo) == (BuildProviderAppliesTo)0)
			{
				return true;
			}
			if (buildProviderInfo.Type == typeof(ForceCopyBuildProvider))
			{
				return true;
			}
			if (buildProviderInfo.Type != typeof(IgnoreFileBuildProvider) && BuildManager.PrecompilingForUpdatableDeployment)
			{
				return true;
			}
			createStub = true;
			if (buildProviderInfo.Type == typeof(UserControlBuildProvider) || buildProviderInfo.Type == typeof(MasterPageBuildProvider) || buildProviderInfo.Type == typeof(IgnoreFileBuildProvider))
			{
				createStub = false;
			}
			return false;
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x0015FA6C File Offset: 0x0015DC6C
		internal static Type LoadTypeWithChecks(string typeName, Type requiredBaseType, Type requiredBaseType2, ConfigurationElement elem, string propertyName)
		{
			Type type = ConfigUtil.GetType(typeName, propertyName, elem);
			if (requiredBaseType2 == null)
			{
				ConfigUtil.CheckAssignableType(requiredBaseType, type, elem, propertyName);
			}
			else
			{
				ConfigUtil.CheckAssignableType(requiredBaseType, requiredBaseType2, type, elem, propertyName);
			}
			return type;
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x0015FAA4 File Offset: 0x0015DCA4
		internal static CodeDomProvider CreateCodeDomProvider(Type codeDomProviderType)
		{
			CodeDomProvider codeDomProvider = CompilationUtil.CreateCodeDomProviderWithPropertyOptions(codeDomProviderType);
			if (codeDomProvider != null)
			{
				return codeDomProvider;
			}
			return (CodeDomProvider)Activator.CreateInstance(codeDomProviderType);
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x0015FAC8 File Offset: 0x0015DCC8
		internal static CodeDomProvider CreateCodeDomProviderNonPublic(Type codeDomProviderType)
		{
			CodeDomProvider codeDomProvider = CompilationUtil.CreateCodeDomProviderWithPropertyOptions(codeDomProviderType);
			if (codeDomProvider != null)
			{
				return codeDomProvider;
			}
			return (CodeDomProvider)HttpRuntime.CreateNonPublicInstance(codeDomProviderType);
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x0015FAEC File Offset: 0x0015DCEC
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static CodeDomProvider CreateCodeDomProviderWithPropertyOptions(Type codeDomProviderType)
		{
			IDictionary<string, string> providerOptions = CompilationUtil.GetProviderOptions(codeDomProviderType);
			IDictionary<string, string> dictionary;
			if (providerOptions != null)
			{
				dictionary = new Dictionary<string, string>(providerOptions);
			}
			else
			{
				dictionary = new Dictionary<string, string>();
			}
			CompilationUtil.CheckCompilerDirectoryPathAllowed(dictionary);
			bool flag = false;
			if (MultiTargetingUtil.IsTargetFramework20)
			{
				dictionary["CompilerVersion"] = "v2.0";
			}
			else if (MultiTargetingUtil.IsTargetFramework35)
			{
				dictionary["CompilerVersion"] = "v3.5";
			}
			else
			{
				string compilerVersion = CompilationUtil.GetCompilerVersion(codeDomProviderType);
				Version versionFromVString = CompilationUtil.GetVersionFromVString(compilerVersion);
				if (versionFromVString != null && versionFromVString < MultiTargetingUtil.Version40)
				{
					dictionary["CompilerVersion"] = "v4.0";
				}
			}
			if (dictionary != null && dictionary.Count > 0)
			{
				ConstructorInfo constructor = codeDomProviderType.GetConstructor(new Type[]
				{
					typeof(IDictionary<string, string>)
				});
				CodeDomProvider result = null;
				if (constructor != null)
				{
					CodeDomProvider codeDomProvider = (CodeDomProvider)Activator.CreateInstance(codeDomProviderType);
					string fileExtension = codeDomProvider.FileExtension;
					string languageFromExtension = CodeDomProvider.GetLanguageFromExtension(fileExtension);
					result = CodeDomProvider.CreateProvider(languageFromExtension, dictionary);
				}
				if (flag)
				{
					dictionary.Remove("CompilerDirectoryPath");
				}
				return result;
			}
			return null;
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x0015FBF8 File Offset: 0x0015DDF8
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static IDictionary<string, string> GetProviderOptions(Type codeDomProviderType)
		{
			CodeDomProvider codeDomProvider = (CodeDomProvider)Activator.CreateInstance(codeDomProviderType);
			string fileExtension = codeDomProvider.FileExtension;
			if (CodeDomProvider.IsDefinedExtension(fileExtension))
			{
				CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(CodeDomProvider.GetLanguageFromExtension(fileExtension));
				return CompilationUtil.GetProviderOptions(compilerInfo);
			}
			return null;
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x0015FC34 File Offset: 0x0015DE34
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static IDictionary<string, string> GetProviderOptions(CompilerInfo ci)
		{
			PropertyInfo property = ci.GetType().GetProperty("ProviderOptions", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				return (IDictionary<string, string>)property.GetValue(ci, null);
			}
			return null;
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x0015FC6C File Offset: 0x0015DE6C
		internal static string GetCompilerVersion(Type codeDomProviderType)
		{
			return CompilationUtil.GetProviderOption(codeDomProviderType, "CompilerVersion");
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x0015FC7C File Offset: 0x0015DE7C
		internal static string GetProviderOption(Type codeDomProviderType, string providerOption)
		{
			IDictionary<string, string> providerOptions = CompilationUtil.GetProviderOptions(codeDomProviderType);
			string result;
			if (providerOptions != null && providerOptions.TryGetValue(providerOption, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x0015FCA1 File Offset: 0x0015DEA1
		internal static bool IsCompilerVersion35(string compilerVersion)
		{
			return compilerVersion == "v3.5";
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x0015FCB4 File Offset: 0x0015DEB4
		internal static bool IsCompilerVersion35(Type codeDomProviderType)
		{
			string compilerVersion = CompilationUtil.GetCompilerVersion(codeDomProviderType);
			return CompilationUtil.IsCompilerVersion35(compilerVersion);
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x0015FCD0 File Offset: 0x0015DED0
		internal static bool IsCompilerVersion35OrAbove(Type codeDomProviderType)
		{
			string compilerVersion = CompilationUtil.GetCompilerVersion(codeDomProviderType);
			return CompilationUtil.IsCompilerVersion35(compilerVersion) || !MultiTargetingUtil.IsTargetFramework20;
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x0015FCF8 File Offset: 0x0015DEF8
		internal static bool WarnAsError(Type codeDomProviderType)
		{
			string providerOption = CompilationUtil.GetProviderOption(codeDomProviderType, "WarnAsError");
			bool flag;
			return providerOption != null && bool.TryParse(providerOption, out flag) && flag;
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x0015FD21 File Offset: 0x0015DF21
		internal static Version GetVersionFromVString(string version)
		{
			if (string.IsNullOrEmpty(version))
			{
				return null;
			}
			return new Version(version.Substring(1));
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06006437 RID: 25655 RVA: 0x0015FD3C File Offset: 0x0015DF3C
		internal static int MaxConcurrentCompilations
		{
			get
			{
				if (CompilationUtil._maxConcurrentCompilations == 0)
				{
					int num;
					if (AppSettings.MaxConcurrentCompilations != null && AppSettings.MaxConcurrentCompilations.Value >= 0)
					{
						num = AppSettings.MaxConcurrentCompilations.Value;
					}
					else
					{
						CompilationSection compilationAppConfig = MTConfigUtil.GetCompilationAppConfig();
						num = compilationAppConfig.MaxConcurrentCompilations;
					}
					if (num <= 0)
					{
						num = Environment.ProcessorCount;
					}
					Interlocked.CompareExchange(ref CompilationUtil._maxConcurrentCompilations, num, 0);
				}
				return CompilationUtil._maxConcurrentCompilations;
			}
		}

		// Token: 0x040033D7 RID: 13271
		internal const string CodeDomProviderOptionPath = "system.codedom/compilers/compiler/ProviderOption/";

		// Token: 0x040033D8 RID: 13272
		private const string CompilerDirectoryPath = "CompilerDirectoryPath";

		// Token: 0x040033D9 RID: 13273
		private static int _maxConcurrentCompilations;
	}
}
