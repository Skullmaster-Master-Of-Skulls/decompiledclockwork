using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Web.Compilation;
using System.Web.Razor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.WebPages.Razor
{
	// Token: 0x0200000C RID: 12
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web | BuildProviderAppliesTo.Code)]
	public class RazorBuildProvider : BuildProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002E RID: 46 RVA: 0x0000249C File Offset: 0x0000069C
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x000024D0 File Offset: 0x000006D0
		public static event EventHandler<CodeGenerationCompleteEventArgs> CodeGenerationCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002503 File Offset: 0x00000703
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x0000250C File Offset: 0x0000070C
		internal event EventHandler<CodeGenerationCompleteEventArgs> CodeGenerationCompletedInternal
		{
			add
			{
				this._codeGenerationCompletedInternal += value;
			}
			remove
			{
				this._codeGenerationCompletedInternal -= value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00002518 File Offset: 0x00000718
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x0000254C File Offset: 0x0000074C
		public static event EventHandler CodeGenerationStarted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000034 RID: 52 RVA: 0x0000257F File Offset: 0x0000077F
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00002588 File Offset: 0x00000788
		internal event EventHandler CodeGenerationStartedInternal
		{
			add
			{
				this._codeGenerationStartedInternal += value;
			}
			remove
			{
				this._codeGenerationStartedInternal -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000036 RID: 54 RVA: 0x00002594 File Offset: 0x00000794
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x000025C8 File Offset: 0x000007C8
		public static event EventHandler<CompilingPathEventArgs> CompilingPath;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000038 RID: 56 RVA: 0x000025FC File Offset: 0x000007FC
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002634 File Offset: 0x00000834
		private event EventHandler<CodeGenerationCompleteEventArgs> _codeGenerationCompletedInternal;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600003A RID: 58 RVA: 0x0000266C File Offset: 0x0000086C
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x000026A4 File Offset: 0x000008A4
		private event EventHandler _codeGenerationStartedInternal;

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000026D9 File Offset: 0x000008D9
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000026F5 File Offset: 0x000008F5
		internal WebPageRazorHost Host
		{
			get
			{
				if (this._host == null)
				{
					this._host = this.CreateHost();
				}
				return this._host;
			}
			set
			{
				this._host = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000026FE File Offset: 0x000008FE
		public override ICollection VirtualPathDependencies
		{
			get
			{
				if (this._virtualPathDependencies != null)
				{
					return ArrayList.ReadOnly(this._virtualPathDependencies);
				}
				return base.VirtualPathDependencies;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000271A File Offset: 0x0000091A
		public new string VirtualPath
		{
			get
			{
				return base.VirtualPath;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002724 File Offset: 0x00000924
		public AssemblyBuilder AssemblyBuilder
		{
			get
			{
				AssemblyBuilderWrapper assemblyBuilderWrapper = this._assemblyBuilder as AssemblyBuilderWrapper;
				if (assemblyBuilderWrapper != null)
				{
					return assemblyBuilderWrapper.InnerBuilder;
				}
				return null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002748 File Offset: 0x00000948
		internal IAssemblyBuilder AssemblyBuilderInternal
		{
			get
			{
				return this._assemblyBuilder;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000275E File Offset: 0x0000095E
		internal CodeCompileUnit GeneratedCode
		{
			get
			{
				this.EnsureGeneratedCode();
				return this._generatedCode;
			}
			set
			{
				this._generatedCode = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002768 File Offset: 0x00000968
		public override CompilerType CodeCompilerType
		{
			get
			{
				this.EnsureGeneratedCode();
				CompilerType defaultCompilerTypeForLanguage = base.GetDefaultCompilerTypeForLanguage(this.Host.CodeLanguage.LanguageName);
				if (RazorBuildProvider._isFullTrust != false && this.Host.DefaultDebugCompilation)
				{
					try
					{
						RazorBuildProvider.SetIncludeDebugInfoFlag(defaultCompilerTypeForLanguage);
						RazorBuildProvider._isFullTrust = new bool?(true);
					}
					catch (SecurityException)
					{
						RazorBuildProvider._isFullTrust = new bool?(false);
					}
				}
				return defaultCompilerTypeForLanguage;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027F0 File Offset: 0x000009F0
		public void AddVirtualPathDependency(string dependency)
		{
			if (this._virtualPathDependencies == null)
			{
				this._virtualPathDependencies = new ArrayList(base.VirtualPathDependencies);
			}
			this._virtualPathDependencies.Add(dependency);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002818 File Offset: 0x00000A18
		public override Type GetGeneratedType(CompilerResults results)
		{
			return results.CompiledAssembly.GetType(string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
			{
				this.Host.DefaultNamespace,
				this.Host.DefaultClassName
			}));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002863 File Offset: 0x00000A63
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			this.GenerateCodeCore(new AssemblyBuilderWrapper(assemblyBuilder));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002874 File Offset: 0x00000A74
		internal virtual void GenerateCodeCore(IAssemblyBuilder assemblyBuilder)
		{
			this.OnCodeGenerationStarted(assemblyBuilder);
			assemblyBuilder.AddCodeCompileUnit(this, this.GeneratedCode);
			assemblyBuilder.GenerateTypeFactory(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				this.Host.DefaultNamespace,
				this.Host.DefaultClassName
			}));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028CE File Offset: 0x00000ACE
		protected internal virtual TextReader InternalOpenReader()
		{
			return base.OpenReader();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028D8 File Offset: 0x00000AD8
		protected internal virtual WebPageRazorHost CreateHost()
		{
			WebPageRazorHost hostFromConfig = this.GetHostFromConfig();
			CompilingPathEventArgs compilingPathEventArgs = new CompilingPathEventArgs(this.VirtualPath, hostFromConfig);
			this.OnBeforeCompilePath(compilingPathEventArgs);
			return compilingPathEventArgs.Host;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002906 File Offset: 0x00000B06
		protected internal virtual WebPageRazorHost GetHostFromConfig()
		{
			return WebRazorHostFactory.CreateHostFromConfig(this.VirtualPath);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002914 File Offset: 0x00000B14
		protected virtual void OnBeforeCompilePath(CompilingPathEventArgs args)
		{
			EventHandler<CompilingPathEventArgs> compilingPath = RazorBuildProvider.CompilingPath;
			if (compilingPath != null)
			{
				compilingPath(this, args);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002934 File Offset: 0x00000B34
		private void OnCodeGenerationStarted(IAssemblyBuilder assemblyBuilder)
		{
			this._assemblyBuilder = assemblyBuilder;
			EventHandler eventHandler = this._codeGenerationStartedInternal ?? RazorBuildProvider.CodeGenerationStarted;
			if (eventHandler != null)
			{
				eventHandler(this, null);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002964 File Offset: 0x00000B64
		private void OnCodeGenerationCompleted(CodeCompileUnit generatedCode)
		{
			EventHandler<CodeGenerationCompleteEventArgs> eventHandler = this._codeGenerationCompletedInternal ?? RazorBuildProvider.CodeGenerationCompleted;
			if (eventHandler != null)
			{
				eventHandler(this, new CodeGenerationCompleteEventArgs(this.Host.VirtualPath, this.Host.PhysicalPath, generatedCode));
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029A8 File Offset: 0x00000BA8
		private void EnsureGeneratedCode()
		{
			if (this._generatedCode == null)
			{
				RazorTemplateEngine razorTemplateEngine = new RazorTemplateEngine(this.Host);
				GeneratorResults generatorResults = null;
				using (TextReader textReader = this.InternalOpenReader())
				{
					generatorResults = razorTemplateEngine.GenerateCode(textReader, null, null, this.Host.PhysicalPath);
				}
				if (!generatorResults.Success)
				{
					throw RazorBuildProvider.CreateExceptionFromParserError(generatorResults.ParserErrors.Last<RazorError>(), this.VirtualPath);
				}
				this._generatedCode = generatorResults.GeneratedCode;
				this.OnCodeGenerationCompleted(this._generatedCode);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A3C File Offset: 0x00000C3C
		private static HttpParseException CreateExceptionFromParserError(RazorError error, string virtualPath)
		{
			return new HttpParseException(error.Message + Environment.NewLine, null, virtualPath, null, error.Location.LineIndex + 1);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A71 File Offset: 0x00000C71
		private static void SetIncludeDebugInfoFlag(CompilerType compilerType)
		{
			compilerType.CompilerParameters.IncludeDebugInformation = true;
		}

		// Token: 0x04000018 RID: 24
		private static bool? _isFullTrust;

		// Token: 0x04000019 RID: 25
		private CodeCompileUnit _generatedCode;

		// Token: 0x0400001A RID: 26
		private WebPageRazorHost _host;

		// Token: 0x0400001B RID: 27
		private IList _virtualPathDependencies;

		// Token: 0x0400001C RID: 28
		private IAssemblyBuilder _assemblyBuilder;
	}
}
