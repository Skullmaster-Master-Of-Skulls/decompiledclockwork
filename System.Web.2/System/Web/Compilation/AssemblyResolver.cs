using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Web.Configuration;
using System.Web.UI;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;

namespace System.Web.Compilation
{
	// Token: 0x020007F9 RID: 2041
	internal class AssemblyResolver
	{
		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x06006145 RID: 24901 RVA: 0x0015042C File Offset: 0x0014E62C
		private static IList<string> TargetFrameworkReferenceAssemblyPaths
		{
			get
			{
				if (AssemblyResolver.s_targetFrameworkReferenceAssemblyPaths == null)
				{
					IList<string> pathToReferenceAssemblies = AssemblyResolver.GetPathToReferenceAssemblies(MultiTargetingUtil.TargetFrameworkName);
					int count = pathToReferenceAssemblies.Count;
					if (MultiTargetingUtil.IsTargetFramework20 || MultiTargetingUtil.IsTargetFramework35)
					{
						string pathToDotNetFramework = ToolLocationHelper.GetPathToDotNetFramework(TargetDotNetFrameworkVersion.Version35);
						if (string.IsNullOrEmpty(pathToDotNetFramework))
						{
							throw new HttpException(SR.GetString("Downlevel_requires_35"));
						}
						IList<string> pathToReferenceAssemblies2 = AssemblyResolver.GetPathToReferenceAssemblies(MultiTargetingUtil.FrameworkNameV30);
						IList<string> pathToReferenceAssemblies3 = AssemblyResolver.GetPathToReferenceAssemblies(MultiTargetingUtil.FrameworkNameV20);
						bool flag = MultiTargetingUtil.IsTargetFramework35 && (pathToReferenceAssemblies2.Count == count || pathToReferenceAssemblies3.Count == count);
						if (count == 0 || flag)
						{
							throw new HttpException(SR.GetString("Reference_assemblies_not_found"));
						}
					}
					else if (BuildManagerHost.SupportsMultiTargeting && count == 0)
					{
						throw new HttpException(SR.GetString("Reference_assemblies_not_found"));
					}
					AssemblyResolver.s_targetFrameworkReferenceAssemblyPaths = pathToReferenceAssemblies;
				}
				return AssemblyResolver.s_targetFrameworkReferenceAssemblyPaths;
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x06006146 RID: 24902 RVA: 0x001504FC File Offset: 0x0014E6FC
		private static IList<string> HigherFrameworkReferenceAssemblyPaths
		{
			get
			{
				if (AssemblyResolver.s_higherFrameworkReferenceAssemblyPaths == null)
				{
					List<string> list = new List<string>();
					FrameworkName targetFrameworkName = MultiTargetingUtil.TargetFrameworkName;
					foreach (FrameworkName frameworkName in MultiTargetingUtil.KnownFrameworkNames)
					{
						if (string.Equals(frameworkName.Identifier, targetFrameworkName.Identifier, StringComparison.OrdinalIgnoreCase) && string.Equals(frameworkName.Profile, targetFrameworkName.Profile, StringComparison.OrdinalIgnoreCase))
						{
							Version version = frameworkName.Version;
							Version version2 = targetFrameworkName.Version;
							if (version2 < version)
							{
								list.AddRange(AssemblyResolver.GetPathToReferenceAssemblies(frameworkName));
							}
						}
					}
					AssemblyResolver.s_higherFrameworkReferenceAssemblyPaths = list;
				}
				return AssemblyResolver.s_higherFrameworkReferenceAssemblyPaths;
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x06006147 RID: 24903 RVA: 0x001505B8 File Offset: 0x0014E7B8
		private static IList<string> FullProfileReferenceAssemblyPaths
		{
			get
			{
				if (AssemblyResolver.s_fullProfileReferenceAssemblyPaths == null)
				{
					List<string> list = new List<string>();
					FrameworkName targetFrameworkName = MultiTargetingUtil.TargetFrameworkName;
					FrameworkName frameworkName = new FrameworkName(targetFrameworkName.Identifier, targetFrameworkName.Version);
					list.AddRange(AssemblyResolver.GetPathToReferenceAssemblies(frameworkName));
					AssemblyResolver.s_fullProfileReferenceAssemblyPaths = list;
				}
				return AssemblyResolver.s_fullProfileReferenceAssemblyPaths;
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x06006148 RID: 24904 RVA: 0x00150604 File Offset: 0x0014E804
		private static bool NeedToCheckFullProfile
		{
			get
			{
				if (AssemblyResolver.s_needToCheckFullProfile == null)
				{
					IEnumerable<string> source = AssemblyResolver.FullProfileReferenceAssemblyPaths.Except(AssemblyResolver.TargetFrameworkReferenceAssemblyPaths, StringComparer.OrdinalIgnoreCase);
					if (source.Count<string>() == 0)
					{
						AssemblyResolver.s_needToCheckFullProfile = new bool?(false);
					}
					else
					{
						AssemblyResolver.s_needToCheckFullProfile = new bool?(true);
					}
				}
				return AssemblyResolver.s_needToCheckFullProfile.Value;
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x06006149 RID: 24905 RVA: 0x0015065C File Offset: 0x0014E85C
		private static Dictionary<Assembly, string> AssemblyLocations
		{
			get
			{
				if (AssemblyResolver.s_assemblyLocations == null)
				{
					AssemblyResolver.s_assemblyLocations = new Dictionary<Assembly, string>();
				}
				return AssemblyResolver.s_assemblyLocations;
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x0600614A RID: 24906 RVA: 0x00150674 File Offset: 0x0014E874
		private static Dictionary<Assembly, AssemblyResolutionResult> AssemblyResolutionResults
		{
			get
			{
				if (AssemblyResolver.s_assemblyResults == null)
				{
					AssemblyResolver.s_assemblyResults = new Dictionary<Assembly, AssemblyResolutionResult>();
				}
				return AssemblyResolver.s_assemblyResults;
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x0600614B RID: 24907 RVA: 0x0015068C File Offset: 0x0014E88C
		private static Dictionary<Assembly, ReferenceAssemblyType> ReferenceAssemblyTypes
		{
			get
			{
				if (AssemblyResolver.s_assemblyTypes == null)
				{
					AssemblyResolver.s_assemblyTypes = new Dictionary<Assembly, ReferenceAssemblyType>();
				}
				return AssemblyResolver.s_assemblyTypes;
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x0600614C RID: 24908 RVA: 0x001506A4 File Offset: 0x0014E8A4
		private static ConcurrentDictionary<string, Version> AssemblyVersions
		{
			get
			{
				return AssemblyResolver.s_assemblyVersions.Value;
			}
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x001506B0 File Offset: 0x0014E8B0
		private static Version GetAssemblyVersion(string path)
		{
			Version version = null;
			ConcurrentDictionary<string, Version> assemblyVersions = AssemblyResolver.AssemblyVersions;
			if (!assemblyVersions.TryGetValue(path, out version))
			{
				try
				{
					AssemblyName assemblyName = AssemblyName.GetAssemblyName(path);
					version = assemblyName.Version;
				}
				catch
				{
				}
				assemblyVersions.TryAdd(path, version);
			}
			return version;
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x00150700 File Offset: 0x0014E900
		private static AssemblyResolutionResult ResolveAssembly(string assemblyName, IList<string> searchPaths, IList<string> targetFrameworkDirectories, bool checkDependencies)
		{
			ResolveAssemblyReference resolveAssemblyReference = new ResolveAssemblyReference();
			MockEngine mockEngine = new MockEngine();
			resolveAssemblyReference.BuildEngine = mockEngine;
			if (searchPaths != null)
			{
				resolveAssemblyReference.SearchPaths = searchPaths.ToArray<string>();
			}
			if (targetFrameworkDirectories != null)
			{
				resolveAssemblyReference.TargetFrameworkDirectories = targetFrameworkDirectories.ToArray<string>();
			}
			resolveAssemblyReference.Assemblies = new ITaskItem[]
			{
				new TaskItem(assemblyName)
			};
			resolveAssemblyReference.Silent = true;
			resolveAssemblyReference.Execute();
			AssemblyResolutionResult assemblyResolutionResult = new AssemblyResolutionResult();
			List<string> list = new List<string>();
			foreach (ITaskItem taskItem in resolveAssemblyReference.ResolvedFiles)
			{
				list.Add(taskItem.ItemSpec);
			}
			if (checkDependencies)
			{
				AssemblyResolver.CheckOutOfRangeDependencies(assemblyName);
			}
			assemblyResolutionResult.ResolvedFiles = list.ToArray();
			assemblyResolutionResult.Warnings = mockEngine.Warnings;
			assemblyResolutionResult.Errors = mockEngine.Errors;
			return assemblyResolutionResult;
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x001507CC File Offset: 0x0014E9CC
		private static void CheckOutOfRangeDependencies(string assemblyName)
		{
			string text = null;
			Assembly assembly = Assembly.Load(assemblyName);
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			if (assembly.GetName().Version != assemblyName2.Version)
			{
				return;
			}
			foreach (AssemblyName assemblyName3 in assembly.GetReferencedAssemblies())
			{
				try
				{
					Assembly a = CompilationSection.LoadAndRecordAssembly(assemblyName3);
					string path;
					ReferenceAssemblyType pathToReferenceAssembly = AssemblyResolver.GetPathToReferenceAssembly(a, out path, null, null, false);
					Version assemblyVersion = AssemblyResolver.GetAssemblyVersion(path);
					if (!(assemblyVersion == null))
					{
						if ((pathToReferenceAssembly == ReferenceAssemblyType.FrameworkAssembly && assemblyVersion < assemblyName3.Version) || pathToReferenceAssembly == ReferenceAssemblyType.FrameworkAssemblyOnlyPresentInHigherVersion)
						{
							if (text == null)
							{
								text = assemblyName3.FullName;
							}
							else
							{
								text = text + "; " + assemblyName3.FullName;
							}
						}
					}
				}
				catch
				{
				}
			}
			if (text != null)
			{
				string @string = SR.GetString("Higher_dependencies", new object[]
				{
					assemblyName,
					text
				});
				AssemblyResolver.ReportWarningOrError(@string);
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x001508C0 File Offset: 0x0014EAC0
		private static void ReportWarningOrError(string message)
		{
			if (AssemblyResolver.WarnAsError)
			{
				throw new HttpCompileException(message);
			}
			CompilerError compilerError = new CompilerError();
			compilerError.ErrorText = message;
			compilerError.IsWarning = true;
			if (BuildManager.CBMCallback != null)
			{
				BuildManager.CBMCallback.ReportCompilerError(compilerError);
			}
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x00150901 File Offset: 0x0014EB01
		internal static ReferenceAssemblyType GetPathToReferenceAssembly(Assembly a, out string path)
		{
			return AssemblyResolver.GetPathToReferenceAssembly(a, out path, null, null);
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x0015090C File Offset: 0x0014EB0C
		private static void StoreResults(Assembly a, string path, AssemblyResolutionResult result, ReferenceAssemblyType assemblyType)
		{
			object obj = AssemblyResolver.s_lock;
			lock (obj)
			{
				if (!AssemblyResolver.AssemblyLocations.ContainsKey(a))
				{
					AssemblyResolver.AssemblyLocations.Add(a, path);
					AssemblyResolver.AssemblyResolutionResults.Add(a, result);
					AssemblyResolver.ReferenceAssemblyTypes.Add(a, assemblyType);
				}
			}
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x00150978 File Offset: 0x0014EB78
		internal static ReferenceAssemblyType GetPathToReferenceAssembly(Assembly a, out string path, ICollection<BuildErrorEventArgs> errors, ICollection<BuildWarningEventArgs> warnings)
		{
			return AssemblyResolver.GetPathToReferenceAssembly(a, out path, errors, warnings, true);
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x00150984 File Offset: 0x0014EB84
		internal static ReferenceAssemblyType GetPathToReferenceAssembly(Assembly a, out string path, ICollection<BuildErrorEventArgs> errors, ICollection<BuildWarningEventArgs> warnings, bool checkDependencies)
		{
			object obj = AssemblyResolver.s_lock;
			lock (obj)
			{
				if (AssemblyResolver.AssemblyLocations.TryGetValue(a, out path))
				{
					return AssemblyResolver.ReferenceAssemblyTypes[a];
				}
			}
			if (AssemblyResolver.TargetFrameworkReferenceAssemblyPaths == null || AssemblyResolver.TargetFrameworkReferenceAssemblyPaths.Count == 0)
			{
				path = Util.GetAssemblyCodeBase(a);
				return ReferenceAssemblyType.FrameworkAssembly;
			}
			AssemblyResolutionResult result = null;
			ReferenceAssemblyType referenceAssemblyType = ReferenceAssemblyType.NonFrameworkAssembly;
			if (BuildResultCompiledAssemblyBase.AssemblyIsInCodegenDir(a))
			{
				path = Util.GetAssemblyCodeBase(a);
			}
			else
			{
				referenceAssemblyType = AssemblyResolver.GetPathToReferenceAssembly(a, out path, errors, warnings, checkDependencies, true, out result);
			}
			AssemblyResolver.StoreResults(a, path, result, referenceAssemblyType);
			return referenceAssemblyType;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x00150A2C File Offset: 0x0014EC2C
		private static ReferenceAssemblyType GetPathToReferenceAssembly(Assembly a, out string path, ICollection<BuildErrorEventArgs> errors, ICollection<BuildWarningEventArgs> warnings, bool checkDependencies, bool useFullName, out AssemblyResolutionResult result)
		{
			string name = a.GetName().Name;
			string text;
			if (useFullName)
			{
				text = CompilationSection.GetOriginalAssemblyName(a);
			}
			else
			{
				text = name;
			}
			result = AssemblyResolver.ResolveAssembly(text, AssemblyResolver.TargetFrameworkReferenceAssemblyPaths, AssemblyResolver.TargetFrameworkReferenceAssemblyPaths, false);
			if (result.ResolvedFiles != null && result.ResolvedFiles.Count > 0)
			{
				path = result.ResolvedFiles.FirstOrDefault<string>();
				return ReferenceAssemblyType.FrameworkAssembly;
			}
			result = AssemblyResolver.ResolveAssembly(text, AssemblyResolver.HigherFrameworkReferenceAssemblyPaths, AssemblyResolver.HigherFrameworkReferenceAssemblyPaths, false);
			if (result.ResolvedFiles != null && result.ResolvedFiles.Count > 0)
			{
				path = result.ResolvedFiles.FirstOrDefault<string>();
				return ReferenceAssemblyType.FrameworkAssemblyOnlyPresentInHigherVersion;
			}
			if (AssemblyResolver.NeedToCheckFullProfile)
			{
				result = AssemblyResolver.ResolveAssembly(text, AssemblyResolver.FullProfileReferenceAssemblyPaths, AssemblyResolver.FullProfileReferenceAssemblyPaths, false);
				if (result.ResolvedFiles != null && result.ResolvedFiles.Count > 0)
				{
					path = result.ResolvedFiles.FirstOrDefault<string>();
					string text2 = "";
					if (!string.IsNullOrEmpty(MultiTargetingUtil.TargetFrameworkName.Profile))
					{
						text2 = " '" + MultiTargetingUtil.TargetFrameworkName.Profile + "'";
					}
					AssemblyResolver.ReportWarningOrError(SR.GetString("Assembly_not_found_in_profile", new object[]
					{
						text,
						text2
					}));
					return ReferenceAssemblyType.FrameworkAssemblyOnlyPresentInHigherVersion;
				}
			}
			List<string> list = new List<string>();
			list.AddRange(AssemblyResolver.TargetFrameworkReferenceAssemblyPaths);
			list.Add(Path.GetDirectoryName(a.Location));
			if (useFullName)
			{
				list.Add("{GAC}");
			}
			if (!useFullName)
			{
				text = a.GetName().FullName;
			}
			result = AssemblyResolver.ResolveAssembly(text, list, AssemblyResolver.TargetFrameworkReferenceAssemblyPaths, checkDependencies);
			path = result.ResolvedFiles.FirstOrDefault<string>();
			if (string.IsNullOrEmpty(path))
			{
				path = Util.GetAssemblyCodeBase(a);
			}
			if (useFullName)
			{
				AssemblyResolutionResult assemblyResolutionResult = AssemblyResolver.ResolveAssembly(name, AssemblyResolver.HigherFrameworkReferenceAssemblyPaths, AssemblyResolver.HigherFrameworkReferenceAssemblyPaths, false);
				if (assemblyResolutionResult.ResolvedFiles != null && assemblyResolutionResult.ResolvedFiles.Count > 0)
				{
					return ReferenceAssemblyType.FrameworkAssembly;
				}
			}
			return ReferenceAssemblyType.NonFrameworkAssembly;
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x00150C0D File Offset: 0x0014EE0D
		private static IList<string> GetPathToReferenceAssemblies(FrameworkName frameworkName)
		{
			return ToolLocationHelper.GetPathToReferenceAssemblies(frameworkName);
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x06006157 RID: 24919 RVA: 0x00150C18 File Offset: 0x0014EE18
		private static bool WarnAsError
		{
			get
			{
				if (AssemblyResolver.s_warnAsError == null)
				{
					object obj = AssemblyResolver.s_warnAsErrorLock;
					lock (obj)
					{
						if (AssemblyResolver.s_warnAsError == null)
						{
							AssemblyResolver.s_warnAsError = new bool?(false);
							CompilerInfo[] allCompilerInfo = CodeDomProvider.GetAllCompilerInfo();
							foreach (CompilerInfo compilerInfo in allCompilerInfo)
							{
								if (compilerInfo != null && compilerInfo.IsCodeDomProviderTypeValid && CompilationUtil.WarnAsError(compilerInfo.CodeDomProviderType))
								{
									AssemblyResolver.s_warnAsError = new bool?(true);
									break;
								}
							}
						}
					}
				}
				return AssemblyResolver.s_warnAsError.Value;
			}
		}

		// Token: 0x04003291 RID: 12945
		private static Dictionary<Assembly, string> s_assemblyLocations;

		// Token: 0x04003292 RID: 12946
		private static Dictionary<Assembly, AssemblyResolutionResult> s_assemblyResults;

		// Token: 0x04003293 RID: 12947
		private static Dictionary<Assembly, ReferenceAssemblyType> s_assemblyTypes;

		// Token: 0x04003294 RID: 12948
		private static object s_lock = new object();

		// Token: 0x04003295 RID: 12949
		private static IList<string> s_targetFrameworkReferenceAssemblyPaths;

		// Token: 0x04003296 RID: 12950
		private static IList<string> s_higherFrameworkReferenceAssemblyPaths;

		// Token: 0x04003297 RID: 12951
		private static IList<string> s_fullProfileReferenceAssemblyPaths;

		// Token: 0x04003298 RID: 12952
		private static bool? s_needToCheckFullProfile;

		// Token: 0x04003299 RID: 12953
		private static bool? s_warnAsError = null;

		// Token: 0x0400329A RID: 12954
		private static object s_warnAsErrorLock = new object();

		// Token: 0x0400329B RID: 12955
		private static readonly Lazy<ConcurrentDictionary<string, Version>> s_assemblyVersions = new Lazy<ConcurrentDictionary<string, Version>>(() => new ConcurrentDictionary<string, Version>(StringComparer.OrdinalIgnoreCase));
	}
}
