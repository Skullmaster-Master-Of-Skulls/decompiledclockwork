using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000809 RID: 2057
	public abstract class BuildProvider
	{
		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual CompilerType CodeCompilerType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual Type GetGeneratedType(CompilerResults results)
		{
			return null;
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetCustomString(CompilerResults results)
		{
			return null;
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x00007722 File Offset: 0x00005922
		public virtual BuildProviderResultFlags GetResultFlags(CompilerResults results)
		{
			return BuildProviderResultFlags.Default;
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ProcessCompileErrors(CompilerResults results)
		{
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x0000298D File Offset: 0x00000B8D
		internal virtual ICollection GetBuildResultVirtualPathDependencies()
		{
			return null;
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x060062AB RID: 25259 RVA: 0x0015A169 File Offset: 0x00158369
		public virtual ICollection VirtualPathDependencies
		{
			get
			{
				return new SingleObjectCollection(this.VirtualPath);
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x060062AC RID: 25260 RVA: 0x0015A176 File Offset: 0x00158376
		protected internal string VirtualPath
		{
			get
			{
				return System.Web.VirtualPath.GetVirtualPathString(this._virtualPath);
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x060062AD RID: 25261 RVA: 0x0015A183 File Offset: 0x00158383
		internal VirtualPath VirtualPathObject
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x0015A18B File Offset: 0x0015838B
		protected Stream OpenStream()
		{
			return this.OpenStream(this.VirtualPath);
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x0015A199 File Offset: 0x00158399
		protected Stream OpenStream(string virtualPath)
		{
			return VirtualPathProvider.OpenFile(virtualPath);
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x0015A1A1 File Offset: 0x001583A1
		internal Stream OpenStream(VirtualPath virtualPath)
		{
			return virtualPath.OpenFile();
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x0015A1A9 File Offset: 0x001583A9
		protected TextReader OpenReader()
		{
			return this.OpenReader(this.VirtualPathObject);
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0015A1B7 File Offset: 0x001583B7
		protected TextReader OpenReader(string virtualPath)
		{
			return this.OpenReader(System.Web.VirtualPath.Create(virtualPath));
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x0015A1C8 File Offset: 0x001583C8
		internal TextReader OpenReader(VirtualPath virtualPath)
		{
			Stream stream = this.OpenStream(virtualPath);
			return Util.ReaderFromStream(stream, virtualPath);
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x0015A1E4 File Offset: 0x001583E4
		public static void RegisterBuildProvider(string extension, Type providerType)
		{
			if (string.IsNullOrEmpty(extension))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("extension");
			}
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			if (!typeof(BuildProvider).IsAssignableFrom(providerType))
			{
				throw ExceptionUtil.ParameterInvalid("providerType");
			}
			BuildManager.ThrowIfPreAppStartNotRunning();
			BuildProvider.s_dynamicallyRegisteredProviders[extension] = new BuildProvider.CompilationBuildProviderInfo(providerType);
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x0015A24C File Offset: 0x0015844C
		internal static BuildProviderInfo GetBuildProviderInfo(CompilationSection config, string extension)
		{
			BuildProvider buildProvider = config.BuildProviders[extension];
			if (buildProvider != null)
			{
				return buildProvider.BuildProviderInfo;
			}
			BuildProviderInfo result = null;
			BuildProvider.s_dynamicallyRegisteredProviders.TryGetValue(extension, out result);
			return result;
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x060062B6 RID: 25270 RVA: 0x0015A281 File Offset: 0x00158481
		protected ICollection ReferencedAssemblies
		{
			get
			{
				return this._referencedAssemblies;
			}
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x0015A289 File Offset: 0x00158489
		protected CompilerType GetDefaultCompilerTypeForLanguage(string language)
		{
			return CompilationUtil.GetCompilerInfoFromLanguage(this.VirtualPathObject, language);
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x0015A297 File Offset: 0x00158497
		protected CompilerType GetDefaultCompilerType()
		{
			return CompilationUtil.GetDefaultLanguageCompilerInfo(null, this.VirtualPathObject);
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x060062B9 RID: 25273 RVA: 0x0015A2A5 File Offset: 0x001584A5
		internal BuildProviderSet BuildProviderDependencies
		{
			get
			{
				return this._buildProviderDependencies;
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x0015A2AD File Offset: 0x001584AD
		internal bool IsDependedOn
		{
			get
			{
				return this.flags[1];
			}
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x0015A2BB File Offset: 0x001584BB
		internal void SetNoBuildResult()
		{
			this.flags[2] = true;
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x0015A2CA File Offset: 0x001584CA
		internal void SetContributedCode()
		{
			this.flags[32] = true;
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x0015A2DA File Offset: 0x001584DA
		internal void SetVirtualPath(VirtualPath virtualPath)
		{
			this._virtualPath = virtualPath;
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x0015A2E3 File Offset: 0x001584E3
		internal void SetReferencedAssemblies(ICollection referencedAssemblies)
		{
			this._referencedAssemblies = referencedAssemblies;
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x0015A2EC File Offset: 0x001584EC
		internal void AddBuildProviderDependency(BuildProvider dependentBuildProvider)
		{
			if (this._buildProviderDependencies == null)
			{
				this._buildProviderDependencies = new BuildProviderSet();
			}
			this._buildProviderDependencies.Add(dependentBuildProvider);
			dependentBuildProvider.flags[1] = true;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0015A31A File Offset: 0x0015851A
		internal string GetCultureName()
		{
			return Util.GetCultureName(this.VirtualPath);
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0015A328 File Offset: 0x00158528
		internal BuildResult GetBuildResult(CompilerResults results)
		{
			BuildResult buildResult = this.CreateBuildResult(results);
			if (buildResult == null)
			{
				return null;
			}
			buildResult.VirtualPath = this.VirtualPathObject;
			this.SetBuildResultDependencies(buildResult);
			return buildResult;
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0015A358 File Offset: 0x00158558
		internal virtual BuildResult CreateBuildResult(CompilerResults results)
		{
			if (this.flags[2])
			{
				return null;
			}
			if (!BuildManagerHost.InClientBuildManager && results != null)
			{
				Assembly compiledAssembly = results.CompiledAssembly;
			}
			Type generatedType = this.GetGeneratedType(results);
			BuildResult buildResult;
			if (generatedType != null)
			{
				BuildResultCompiledType buildResultCompiledType = this.CreateBuildResult(generatedType);
				if (!buildResultCompiledType.IsDelayLoadType && (results == null || generatedType.Assembly != results.CompiledAssembly))
				{
					buildResultCompiledType.UsesExistingAssembly = true;
				}
				buildResult = buildResultCompiledType;
			}
			else
			{
				string customString = this.GetCustomString(results);
				if (customString != null)
				{
					buildResult = new BuildResultCustomString(this.flags[32] ? results.CompiledAssembly : null, customString);
				}
				else
				{
					if (results == null)
					{
						return null;
					}
					buildResult = new BuildResultCompiledAssembly(results.CompiledAssembly);
				}
			}
			int num = (int)this.GetResultFlags(results);
			if (num != 0)
			{
				num &= 65535;
				buildResult.Flags |= num;
			}
			return buildResult;
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x0015A42E File Offset: 0x0015862E
		internal virtual BuildResultCompiledType CreateBuildResult(Type t)
		{
			return new BuildResultCompiledType(t);
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x0015A436 File Offset: 0x00158636
		internal void SetBuildResultDependencies(BuildResult result)
		{
			result.AddVirtualPathDependencies(this.VirtualPathDependencies);
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x0015A444 File Offset: 0x00158644
		internal static CompilerType GetCompilerTypeFromBuildProvider(BuildProvider buildProvider)
		{
			HttpContext httpContext = null;
			if (EtwTrace.IsTraceEnabled(5, 1) && (httpContext = HttpContext.Current) != null)
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_PARSE_ENTER, httpContext.WorkerRequest);
			}
			CompilerType result;
			try
			{
				CompilerType codeCompilerType = buildProvider.CodeCompilerType;
				if (codeCompilerType != null)
				{
					CompilationUtil.CheckCompilerOptionsAllowed(codeCompilerType.CompilerParameters.CompilerOptions, false, null, 0);
				}
				result = codeCompilerType;
			}
			finally
			{
				if (EtwTrace.IsTraceEnabled(5, 1) && httpContext != null)
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_PARSE_LEAVE, httpContext.WorkerRequest);
				}
			}
			return result;
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x0015A4C0 File Offset: 0x001586C0
		internal static string GetDisplayName(BuildProvider buildProvider)
		{
			if (buildProvider.VirtualPath != null)
			{
				return buildProvider.VirtualPath;
			}
			return buildProvider.GetType().Name;
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x0000298D File Offset: 0x00000B8D
		internal virtual ICollection GetGeneratedTypeNames()
		{
			return null;
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x060062C8 RID: 25288 RVA: 0x0015A4DC File Offset: 0x001586DC
		// (set) Token: 0x060062C9 RID: 25289 RVA: 0x0015A4EA File Offset: 0x001586EA
		internal virtual bool IgnoreParseErrors
		{
			get
			{
				return this.flags[4];
			}
			set
			{
				this.flags[4] = value;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x0015A4F9 File Offset: 0x001586F9
		// (set) Token: 0x060062CB RID: 25291 RVA: 0x0015A507 File Offset: 0x00158707
		internal bool IgnoreControlProperties
		{
			get
			{
				return this.flags[8];
			}
			set
			{
				this.flags[8] = value;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x060062CC RID: 25292 RVA: 0x0015A516 File Offset: 0x00158716
		// (set) Token: 0x060062CD RID: 25293 RVA: 0x0015A528 File Offset: 0x00158728
		internal bool ThrowOnFirstParseError
		{
			get
			{
				return !this.flags[16];
			}
			set
			{
				this.flags[16] = !value;
			}
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x060062CE RID: 25294 RVA: 0x0000298D File Offset: 0x00000B8D
		internal virtual IAssemblyDependencyParser AssemblyDependencyParser
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x0015A53C File Offset: 0x0015873C
		protected internal virtual CodeCompileUnit GetCodeCompileUnit(out IDictionary linePragmasTable)
		{
			string value = Util.StringFromVirtualPath(this.VirtualPathObject);
			CodeSnippetCompileUnit result = new CodeSnippetCompileUnit(value);
			LinePragmaCodeInfo value2 = new LinePragmaCodeInfo(1, 1, 1, -1, false);
			linePragmasTable = new Hashtable();
			linePragmasTable[1] = value2;
			return result;
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x0000298D File Offset: 0x00000B8D
		internal virtual ICollection GetCompileWithDependencies()
		{
			return null;
		}

		// Token: 0x04003336 RID: 13110
		private static Dictionary<string, BuildProviderInfo> s_dynamicallyRegisteredProviders = new Dictionary<string, BuildProviderInfo>();

		// Token: 0x04003337 RID: 13111
		internal SimpleBitVector32 flags;

		// Token: 0x04003338 RID: 13112
		internal const int isDependedOn = 1;

		// Token: 0x04003339 RID: 13113
		internal const int noBuildResult = 2;

		// Token: 0x0400333A RID: 13114
		internal const int ignoreParseErrors = 4;

		// Token: 0x0400333B RID: 13115
		internal const int ignoreControlProperties = 8;

		// Token: 0x0400333C RID: 13116
		internal const int dontThrowOnFirstParseError = 16;

		// Token: 0x0400333D RID: 13117
		internal const int contributedCode = 32;

		// Token: 0x0400333E RID: 13118
		private VirtualPath _virtualPath;

		// Token: 0x0400333F RID: 13119
		private ICollection _referencedAssemblies;

		// Token: 0x04003340 RID: 13120
		private BuildProviderSet _buildProviderDependencies;

		// Token: 0x02000A6F RID: 2671
		private class CompilationBuildProviderInfo : BuildProviderInfo
		{
			// Token: 0x06006F2A RID: 28458 RVA: 0x0018B923 File Offset: 0x00189B23
			public CompilationBuildProviderInfo(Type type)
			{
				this._type = type;
			}

			// Token: 0x17001E48 RID: 7752
			// (get) Token: 0x06006F2B RID: 28459 RVA: 0x0018B932 File Offset: 0x00189B32
			internal override Type Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x04003BA7 RID: 15271
			private readonly Type _type;
		}
	}
}
