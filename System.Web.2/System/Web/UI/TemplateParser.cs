using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000313 RID: 787
	public abstract class TemplateParser : BaseParser, IAssemblyDependencyParser
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x00076F2B File Offset: 0x0007512B
		internal CompilationSection CompConfig
		{
			get
			{
				return this._compConfig;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x00076F33 File Offset: 0x00075133
		internal PagesSection PagesConfig
		{
			get
			{
				return this._pagesConfig;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x00076F3B File Offset: 0x0007513B
		internal MainTagNameToTypeMapper TypeMapper
		{
			get
			{
				return this._typeMapper;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x00076F43 File Offset: 0x00075143
		internal ICollection UserControlRegisterEntries
		{
			get
			{
				return this.TypeMapper.UserControlRegisterEntries;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x00076F50 File Offset: 0x00075150
		internal List<TagNamespaceRegisterEntry> TagRegisterEntries
		{
			get
			{
				return this.TypeMapper.TagRegisterEntries;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00076F5D File Offset: 0x0007515D
		internal Stack BuilderStack
		{
			get
			{
				this.EnsureRootBuilderCreated();
				return this._builderStack;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00076F6B File Offset: 0x0007516B
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x00076F73 File Offset: 0x00075173
		public string Text
		{
			get
			{
				return this._text;
			}
			internal set
			{
				this._text = value;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x00076F7C File Offset: 0x0007517C
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x00076F84 File Offset: 0x00075184
		internal Type BaseType
		{
			get
			{
				return this._baseType;
			}
			set
			{
				this._baseType = value;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x00076F8D File Offset: 0x0007518D
		internal string BaseTypeNamespace
		{
			get
			{
				return this._baseTypeNamespace;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00076F95 File Offset: 0x00075195
		internal string BaseTypeName
		{
			get
			{
				return this._baseTypeName;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x00076F9D File Offset: 0x0007519D
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x00076FAC File Offset: 0x000751AC
		internal bool IgnoreControlProperties
		{
			get
			{
				return this.flags[32];
			}
			set
			{
				this.flags[32] = value;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x00076FBC File Offset: 0x000751BC
		// (set) Token: 0x06002479 RID: 9337 RVA: 0x00076FCE File Offset: 0x000751CE
		internal bool ThrowOnFirstParseError
		{
			get
			{
				return this.flags[16777216];
			}
			set
			{
				this.flags[16777216] = value;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x00076FE1 File Offset: 0x000751E1
		internal ArrayList ImplementedInterfaces
		{
			get
			{
				return this._implementedInterfaces;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x00076FE9 File Offset: 0x000751E9
		internal bool HasCodeBehind
		{
			get
			{
				return this.flags[128];
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600247C RID: 9340
		internal abstract Type DefaultBaseType { get; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x00076FFB File Offset: 0x000751FB
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x0007700D File Offset: 0x0007520D
		internal virtual bool FInDesigner
		{
			get
			{
				return this.flags[256];
			}
			set
			{
				this.flags[256] = value;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x00077020 File Offset: 0x00075220
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x00077032 File Offset: 0x00075232
		internal virtual bool IgnoreParseErrors
		{
			get
			{
				return this.flags[512];
			}
			set
			{
				this.flags[512] = value;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00077045 File Offset: 0x00075245
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x00077056 File Offset: 0x00075256
		internal CompilationMode CompilationMode
		{
			get
			{
				if (BuildManager.PrecompilingForDeployment)
				{
					return CompilationMode.Always;
				}
				return this._compilationMode;
			}
			set
			{
				if (value == CompilationMode.Never && this.flags[16])
				{
					this.ProcessError(SR.GetString("Compilmode_not_allowed"));
				}
				this._compilationMode = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00077082 File Offset: 0x00075282
		private ParserErrorCollection ParserErrors
		{
			get
			{
				if (this._parserErrors == null)
				{
					this._parserErrors = new ParserErrorCollection();
				}
				return this._parserErrors;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0007709D File Offset: 0x0007529D
		private bool HasParserErrors
		{
			get
			{
				return this._parserErrors != null && this._parserErrors.Count > 0;
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000770B8 File Offset: 0x000752B8
		protected void ProcessError(string message)
		{
			if (this.IgnoreParseErrors)
			{
				return;
			}
			if (this.ThrowOnFirstParseError)
			{
				throw new HttpException(message);
			}
			ParserError parserError = new ParserError(message, base.CurrentVirtualPath, this._lineNumber);
			this.ParserErrors.Add(parserError);
			BuildManager.ReportParseError(parserError);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00077104 File Offset: 0x00075304
		protected void ProcessException(Exception ex)
		{
			if (this.IgnoreParseErrors)
			{
				return;
			}
			if (!this.ThrowOnFirstParseError && !(ex is HttpCompileException))
			{
				HttpParseException ex2 = ex as HttpParseException;
				ParserError parserError;
				if (ex2 != null)
				{
					parserError = new ParserError(ex2.Message, ex2.VirtualPath, ex2.Line);
				}
				else
				{
					parserError = new ParserError(ex.Message, base.CurrentVirtualPath, this._lineNumber);
				}
				parserError.Exception = ex;
				this.ParserErrors.Add(parserError);
				if (ex2 == null || base.CurrentVirtualPath.Equals(ex2.VirtualPathObject))
				{
					BuildManager.ReportParseError(parserError);
				}
				return;
			}
			if (ex is HttpParseException)
			{
				throw ex;
			}
			throw new HttpParseException(ex.Message, ex);
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool RequiresCompilation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000771AD File Offset: 0x000753AD
		internal virtual bool IsCodeAllowed
		{
			get
			{
				return this.CompilationMode != CompilationMode.Never && (this._pageParserFilter == null || this._pageParserFilter.AllowCode);
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000771D2 File Offset: 0x000753D2
		internal void EnsureCodeAllowed()
		{
			if (!this.IsCodeAllowed)
			{
				this.ProcessError(SR.GetString("Code_not_allowed"));
			}
			this.flags[16] = true;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000771FA File Offset: 0x000753FA
		internal void OnFoundAttributeRequiringCompilation(string attribName)
		{
			if (!this.IsCodeAllowed)
			{
				this.ProcessError(SR.GetString("Attrib_not_allowed", new object[]
				{
					attribName
				}));
			}
			this.flags[16] = true;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0007722C File Offset: 0x0007542C
		internal void OnFoundDirectiveRequiringCompilation(string directiveName)
		{
			if (!this.IsCodeAllowed)
			{
				this.ProcessError(SR.GetString("Directive_not_allowed", new object[]
				{
					directiveName
				}));
			}
			this.flags[16] = true;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0007725E File Offset: 0x0007545E
		internal void OnFoundEventHandler(string directiveName)
		{
			if (!this.IsCodeAllowed)
			{
				this.ProcessError(SR.GetString("Event_not_allowed", new object[]
				{
					directiveName
				}));
			}
			this.flags[16] = true;
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00077290 File Offset: 0x00075490
		// (set) Token: 0x0600248E RID: 9358 RVA: 0x00077298 File Offset: 0x00075498
		internal IDesignerHost DesignerHost
		{
			get
			{
				return this._designerHost;
			}
			set
			{
				this._designerHost = value;
				this._typeResolutionService = null;
				if (this._designerHost != null)
				{
					this._typeResolutionService = (ITypeResolutionService)this._designerHost.GetService(typeof(ITypeResolutionService));
					if (this._typeResolutionService == null)
					{
						throw new ArgumentException(SR.GetString("TypeResService_Needed"));
					}
				}
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool FApplicationFile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x000772F3 File Offset: 0x000754F3
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x000772FB File Offset: 0x000754FB
		internal EventHandler DesignTimeDataBindHandler
		{
			get
			{
				return this._designTimeDataBindHandler;
			}
			set
			{
				this._designTimeDataBindHandler = value;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x00077304 File Offset: 0x00075504
		internal AssemblySet AssemblyDependencies
		{
			get
			{
				return this._assemblyDependencies;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x0007730C File Offset: 0x0007550C
		internal StringSet SourceDependencies
		{
			get
			{
				return this._sourceDependencies;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x00077314 File Offset: 0x00075514
		internal HttpStaticObjectsCollection SessionObjects
		{
			get
			{
				return this._sessionObjects;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x0007731C File Offset: 0x0007551C
		internal HttpStaticObjectsCollection ApplicationObjects
		{
			get
			{
				return this._applicationObjects;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x00077324 File Offset: 0x00075524
		internal RootBuilder RootBuilder
		{
			get
			{
				this.EnsureRootBuilderCreated();
				return this._rootBuilder;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00077332 File Offset: 0x00075532
		internal Hashtable NamespaceEntries
		{
			get
			{
				return this._namespaceEntries;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x0007733A File Offset: 0x0007553A
		internal CompilerType CompilerType
		{
			get
			{
				return this._compilerType;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x00077342 File Offset: 0x00075542
		internal ArrayList ScriptList
		{
			get
			{
				return this._scriptList;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x0007734A File Offset: 0x0007554A
		internal int TypeHashCode
		{
			get
			{
				return this._typeHashCode.CombinedHash32;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x00077357 File Offset: 0x00075557
		internal ArrayList PageObjectList
		{
			get
			{
				return this._pageObjectList;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x0007735F File Offset: 0x0007555F
		internal ParseRecorder ParseRecorders
		{
			get
			{
				return this._parseRecorders;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x00077367 File Offset: 0x00075567
		internal CompilerParameters CompilParams
		{
			get
			{
				return this._compilerType.CompilerParameters;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x00077374 File Offset: 0x00075574
		internal bool FExplicit
		{
			get
			{
				return this.flags[4096];
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00077386 File Offset: 0x00075586
		internal bool FLinePragmas
		{
			get
			{
				return !this.flags[32768];
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x0007739B File Offset: 0x0007559B
		internal bool FStrict
		{
			get
			{
				return this.flags[65536];
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000773AD File Offset: 0x000755AD
		internal VirtualPath CodeFileVirtualPath
		{
			get
			{
				return this._codeFileVirtualPath;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x000773B5 File Offset: 0x000755B5
		internal string GeneratedClassName
		{
			get
			{
				return this._generatedClassName;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000773BD File Offset: 0x000755BD
		internal string GeneratedNamespace
		{
			get
			{
				if (this._generatedNamespace == null)
				{
					return "ASP";
				}
				return this._generatedNamespace;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x000773D4 File Offset: 0x000755D4
		internal ControlBuilderInterceptor ControlBuilderInterceptor
		{
			get
			{
				if (this._controlBuilderInterceptor == null && this.CompConfig != null && this.CompConfig.ControlBuilderInterceptorTypeInternal != null)
				{
					this._controlBuilderInterceptor = (ControlBuilderInterceptor)Activator.CreateInstance(this.CompConfig.ControlBuilderInterceptorTypeInternal);
				}
				return this._controlBuilderInterceptor;
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00077428 File Offset: 0x00075628
		internal static Control ParseControl(string content, VirtualPath virtualPath, bool ignoreFilter)
		{
			if (content == null)
			{
				return null;
			}
			ITemplate template = TemplateParser.ParseTemplate(content, virtualPath, ignoreFilter);
			Control control = new Control();
			template.InstantiateIn(control);
			return control;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00077451 File Offset: 0x00075651
		public static ITemplate ParseTemplate(string content, string virtualPath, bool ignoreFilter)
		{
			return TemplateParser.ParseTemplate(content, VirtualPath.Create(virtualPath), ignoreFilter);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00077460 File Offset: 0x00075660
		private static ITemplate ParseTemplate(string content, VirtualPath virtualPath, bool ignoreFilter)
		{
			TemplateParser templateParser = new UserControlParser();
			return templateParser.ParseTemplateInternal(content, virtualPath, ignoreFilter);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0007747C File Offset: 0x0007567C
		private ITemplate ParseTemplateInternal(string content, VirtualPath virtualPath, bool ignoreFilter)
		{
			base.CurrentVirtualPath = virtualPath;
			this.CompilationMode = CompilationMode.Never;
			this._text = content;
			this.flags[33554432] = ignoreFilter;
			this.flags[67108864] = true;
			this.Parse();
			return this.RootBuilder;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000774CC File Offset: 0x000756CC
		internal virtual void PrepareParse()
		{
			if (this._circularReferenceChecker == null)
			{
				this._circularReferenceChecker = new CaseInsensitiveStringSet();
			}
			this._baseType = this.DefaultBaseType;
			this._mainDirectiveConfigSettings = TemplateParser.CreateEmptyAttributeBag();
			if (!this.FInDesigner)
			{
				this._compConfig = MTConfigUtil.GetCompilationConfig(base.CurrentVirtualPath);
				this._pagesConfig = MTConfigUtil.GetPagesConfig(base.CurrentVirtualPath);
			}
			this.ProcessConfigSettings();
			this._typeMapper = new MainTagNameToTypeMapper(this as BaseTemplateParser);
			this._typeMapper.RegisterTag("object", typeof(ObjectTag));
			this._sourceDependencies = new CaseInsensitiveStringSet();
			this._idListStack = new Stack();
			this._idList = new CaseInsensitiveStringSet();
			this._scriptList = new ArrayList();
			this.InitializeParseRecorders();
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00077590 File Offset: 0x00075790
		private void InitializeParseRecorders()
		{
			if (this.FInDesigner)
			{
				return;
			}
			this._parseRecorders = ParseRecorder.CreateRecorders(this);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000775A8 File Offset: 0x000757A8
		private void EnsureRootBuilderCreated()
		{
			if (this._rootBuilder != null)
			{
				return;
			}
			if (this.BaseType == this.DefaultBaseType)
			{
				this._rootBuilder = this.CreateDefaultFileLevelBuilder();
			}
			else
			{
				Type fileLevelControlBuilderType = this.GetFileLevelControlBuilderType();
				if (fileLevelControlBuilderType == null)
				{
					this._rootBuilder = this.CreateDefaultFileLevelBuilder();
				}
				else
				{
					this._rootBuilder = (RootBuilder)HttpRuntime.CreateNonPublicInstance(fileLevelControlBuilderType);
				}
			}
			this._rootBuilder.Line = 1;
			this._rootBuilder.Init(this, null, null, null, null, null);
			this._rootBuilder.SetTypeMapper(this.TypeMapper);
			this._rootBuilder.VirtualPath = base.CurrentVirtualPath;
			this._builderStack = new Stack();
			this._builderStack.Push(new BuilderStackEntry(this.RootBuilder, null, null, 0, null, 0));
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x00077673 File Offset: 0x00075873
		internal virtual Type DefaultFileLevelBuilderType
		{
			get
			{
				return typeof(RootBuilder);
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0007767F File Offset: 0x0007587F
		internal virtual RootBuilder CreateDefaultFileLevelBuilder()
		{
			return new RootBuilder();
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x00077688 File Offset: 0x00075888
		private Type GetFileLevelControlBuilderType()
		{
			FileLevelControlBuilderAttribute fileLevelControlBuilderAttribute = null;
			object[] customAttributes = this.BaseType.GetCustomAttributes(typeof(FileLevelControlBuilderAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				fileLevelControlBuilderAttribute = (FileLevelControlBuilderAttribute)customAttributes[0];
			}
			if (fileLevelControlBuilderAttribute == null)
			{
				return null;
			}
			Util.CheckAssignableType(this.DefaultFileLevelBuilderType, fileLevelControlBuilderAttribute.BuilderType);
			return fileLevelControlBuilderAttribute.BuilderType;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000776DC File Offset: 0x000758DC
		internal virtual void ProcessConfigSettings()
		{
			if (this._compConfig != null)
			{
				this.flags[4096] = this._compConfig.Explicit;
				this.flags[65536] = this._compConfig.Strict;
			}
			if (this.PagesConfig != null)
			{
				this._namespaceEntries = this.PagesConfig.Namespaces.NamespaceEntries;
				if (this._namespaceEntries != null)
				{
					this._namespaceEntries = (Hashtable)this._namespaceEntries.Clone();
				}
				if (!this.flags[33554432])
				{
					this._pageParserFilter = PageParserFilter.Create(this.PagesConfig, base.CurrentVirtualPath, this);
				}
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0007778D File Offset: 0x0007598D
		internal void Parse(ICollection referencedAssemblies, VirtualPath virtualPath)
		{
			this._referencedAssemblies = referencedAssemblies;
			base.CurrentVirtualPath = virtualPath;
			this.Parse();
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000777A4 File Offset: 0x000759A4
		internal void Parse()
		{
			Thread currentThread = Thread.CurrentThread;
			CultureInfo currentCulture = currentThread.CurrentCulture;
			currentThread.CurrentCulture = CultureInfo.InvariantCulture;
			try
			{
				try
				{
					this.PrepareParse();
					this.ParseInternal();
					this.HandlePostParse();
				}
				finally
				{
					currentThread.CurrentCulture = currentCulture;
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00077808 File Offset: 0x00075A08
		internal virtual void ParseInternal()
		{
			if (this._text != null)
			{
				this.ParseString(this._text, base.CurrentVirtualPath, Encoding.UTF8);
				return;
			}
			this.AddSourceDependency(base.CurrentVirtualPath);
			this.ParseFile(null, base.CurrentVirtualPath.VirtualPathString);
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00077848 File Offset: 0x00075A48
		internal TemplateParser()
		{
			this.ThrowOnFirstParseError = true;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x00077874 File Offset: 0x00075A74
		protected void ParseFile(string physicalPath, string virtualPath)
		{
			this.ParseFile(physicalPath, VirtualPath.Create(virtualPath));
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x00077884 File Offset: 0x00075A84
		internal void ParseFile(string physicalPath, VirtualPath virtualPath)
		{
			string o = (physicalPath != null) ? physicalPath : virtualPath.VirtualPathString;
			if (this._circularReferenceChecker.Contains(o))
			{
				this.ProcessError(SR.GetString("Circular_include"));
				return;
			}
			this._circularReferenceChecker.Add(o);
			try
			{
				if (physicalPath != null)
				{
					StreamReader streamReader;
					StreamReader reader = streamReader = Util.ReaderFromFile(physicalPath, base.CurrentVirtualPath);
					try
					{
						this.ParseReader(reader, virtualPath);
						return;
					}
					finally
					{
						if (streamReader != null)
						{
							((IDisposable)streamReader).Dispose();
						}
					}
				}
				using (Stream stream = virtualPath.OpenFile())
				{
					StreamReader reader = Util.ReaderFromStream(stream, base.CurrentVirtualPath);
					this.ParseReader(reader, virtualPath);
				}
			}
			finally
			{
				this._circularReferenceChecker.Remove(o);
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0007794C File Offset: 0x00075B4C
		private void ParseReader(StreamReader reader, VirtualPath virtualPath)
		{
			string text = reader.ReadToEnd();
			this._text = text;
			this.ParseString(text, virtualPath, reader.CurrentEncoding);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x00077975 File Offset: 0x00075B75
		private void AddLiteral(string literal)
		{
			if (this._literalBuilder == null)
			{
				this._literalBuilder = new StringBuilder();
			}
			this._literalBuilder.Append(literal);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00077997 File Offset: 0x00075B97
		private string GetLiteral()
		{
			if (this._literalBuilder == null)
			{
				return null;
			}
			return this._literalBuilder.ToString();
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000779AE File Offset: 0x00075BAE
		internal void UpdateTypeHashCode(string text)
		{
			this._typeHashCode.AddObject(text);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000779BC File Offset: 0x00075BBC
		internal void ParseString(string text, VirtualPath virtualPath, Encoding fileEncoding)
		{
			VirtualPath currentVirtualPath = base.CurrentVirtualPath;
			int lineNumber = this._lineNumber;
			base.CurrentVirtualPath = virtualPath;
			this._lineNumber = 1;
			this.flags[8] = true;
			try
			{
				this.ParseStringInternal(text, fileEncoding);
				if (this.HasParserErrors)
				{
					ParserError parserError = this.ParserErrors[0];
					Exception ex = parserError.Exception;
					if (ex == null)
					{
						ex = new HttpException(parserError.ErrorText);
					}
					HttpParseException ex2 = new HttpParseException(parserError.ErrorText, ex, parserError.VirtualPath, this.Text, parserError.Line);
					for (int i = 1; i < this.ParserErrors.Count; i++)
					{
						ex2.ParserErrors.Add(this.ParserErrors[i]);
					}
					throw ex2;
				}
				this.ThrowOnFirstParseError = true;
			}
			catch (Exception ex3)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_PRE_PROCESSING);
				PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_TOTAL);
				if (HttpException.GetErrorFormatter(ex3) == null)
				{
					throw new HttpParseException(ex3.Message, ex3, base.CurrentVirtualPath, text, this._lineNumber);
				}
				throw;
			}
			finally
			{
				base.CurrentVirtualPath = currentVirtualPath;
				this._lineNumber = lineNumber;
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00077AF0 File Offset: 0x00075CF0
		private void ParseStringInternal(string text, Encoding fileEncoding)
		{
			int num = 0;
			int num2 = text.LastIndexOf('>');
			Regex tagRegex = base.TagRegex;
			do
			{
				Match match;
				if ((match = BaseParser.textRegex.Match(text, num)).Success)
				{
					this.AddLiteral(match.ToString());
					this._lineNumber += Util.LineCount(text, num, match.Index + match.Length);
					num = match.Index + match.Length;
				}
				if (num == text.Length)
				{
					break;
				}
				bool flag = false;
				if (!this.flags[2] && (match = BaseParser.directiveRegex.Match(text, num)).Success)
				{
					this.ProcessLiteral();
					ParsedAttributeCollection parsedAttributeCollection;
					string text3;
					string text2 = this.ProcessAttributes(text, match, out parsedAttributeCollection, true, out text3);
					try
					{
						this.PreprocessDirective(text2, parsedAttributeCollection);
						this.ProcessDirective(text2, parsedAttributeCollection);
					}
					catch (Exception ex)
					{
						this.ProcessException(ex);
					}
					if (text2.Length == 0 && this._codeFileVirtualPath != null)
					{
						this.CreateModifiedMainDirectiveFileIfNeeded(text, match, parsedAttributeCollection, fileEncoding);
					}
					this.flags[8] = true;
				}
				else
				{
					if ((match = BaseParser.includeRegex.Match(text, num)).Success)
					{
						try
						{
							this.ProcessServerInclude(match);
							goto IL_2BB;
						}
						catch (Exception ex2)
						{
							this.ProcessException(ex2);
							goto IL_2BB;
						}
					}
					if (!(match = BaseParser.commentRegex.Match(text, num)).Success)
					{
						if (!this.flags[2] && (match = BaseParser.aspExprRegex.Match(text, num)).Success)
						{
							this.ProcessCodeBlock(match, CodeBlockType.Expression, text);
						}
						else if (!this.flags[2] && (match = BaseParser.aspEncodedExprRegex.Match(text, num)).Success)
						{
							this.ProcessCodeBlock(match, CodeBlockType.EncodedExpression, text);
						}
						else if (!this.flags[2] && (match = BaseParser.databindExprRegex.Match(text, num)).Success)
						{
							this.ProcessCodeBlock(match, CodeBlockType.DataBinding, text);
						}
						else if (!this.flags[2] && (match = BaseParser.aspCodeRegex.Match(text, num)).Success)
						{
							string text4 = match.Groups["code"].Value.Trim();
							if (text4.StartsWith("$", StringComparison.Ordinal))
							{
								this.ProcessError(SR.GetString("ExpressionBuilder_LiteralExpressionsNotAllowed", new object[]
								{
									match.ToString(),
									text4
								}));
							}
							else
							{
								this.ProcessCodeBlock(match, CodeBlockType.Code, text);
							}
						}
						else
						{
							if (!this.flags[2] && num2 > num && (match = tagRegex.Match(text, num)).Success)
							{
								try
								{
									if (!this.ProcessBeginTag(match, text))
									{
										flag = true;
									}
									goto IL_2BB;
								}
								catch (Exception ex3)
								{
									this.ProcessException(ex3);
									goto IL_2BB;
								}
							}
							if ((match = BaseParser.endtagRegex.Match(text, num)).Success && !this.ProcessEndTag(match))
							{
								flag = true;
							}
						}
					}
				}
				IL_2BB:
				if (match == null || !match.Success || flag)
				{
					if (!flag && !this.flags[2])
					{
						this.DetectSpecialServerTagError(text, num);
					}
					num++;
					this.AddLiteral("<");
				}
				else
				{
					this._lineNumber += Util.LineCount(text, num, match.Index + match.Length);
					num = match.Index + match.Length;
				}
			}
			while (num != text.Length);
			if (this.flags[2] && !this.IgnoreParseErrors)
			{
				this._lineNumber = this._scriptStartLineNumber;
				this.ProcessError(SR.GetString("Unexpected_eof_looking_for_tag", new object[]
				{
					"script"
				}));
				return;
			}
			this.ProcessLiteral();
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00077EA4 File Offset: 0x000760A4
		private void CreateModifiedMainDirectiveFileIfNeeded(string text, Match match, ParsedAttributeCollection mainDirective, Encoding fileEncoding)
		{
			TextWriter updatableDeploymentTargetWriter = BuildManager.GetUpdatableDeploymentTargetWriter(base.CurrentVirtualPath, fileEncoding);
			if (updatableDeploymentTargetWriter == null)
			{
				return;
			}
			using (updatableDeploymentTargetWriter)
			{
				updatableDeploymentTargetWriter.Write(text.Substring(0, match.Index));
				updatableDeploymentTargetWriter.Write("<%@ " + this.DefaultDirectiveName);
				foreach (object obj in ((IEnumerable)mainDirective))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text2 = (string)dictionaryEntry.Key;
					string value = (string)dictionaryEntry.Value;
					if (!StringUtil.EqualsIgnoreCase(text2, "codefile") && !StringUtil.EqualsIgnoreCase(text2, "codefilebaseclass"))
					{
						if (StringUtil.EqualsIgnoreCase(text2, "inherits"))
						{
							value = "__ASPNET_INHERITS";
						}
						updatableDeploymentTargetWriter.Write(" ");
						updatableDeploymentTargetWriter.Write(text2);
						updatableDeploymentTargetWriter.Write("=\"");
						updatableDeploymentTargetWriter.Write(value);
						updatableDeploymentTargetWriter.Write("\"");
					}
				}
				updatableDeploymentTargetWriter.Write(" %>");
				updatableDeploymentTargetWriter.Write(text.Substring(match.Index + match.Length));
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0007800C File Offset: 0x0007620C
		internal virtual void HandlePostParse()
		{
			if (!this.flags[2048])
			{
				this.ProcessMainDirective(this._mainDirectiveConfigSettings);
				this.flags[2048] = true;
			}
			if (this._pageParserFilter != null && !this._pageParserFilter.AllowBaseType(this.BaseType))
			{
				throw new HttpException(SR.GetString("Base_type_not_allowed", new object[]
				{
					this.BaseType.FullName
				}));
			}
			if (this.BuilderStack.Count > 1)
			{
				BuilderStackEntry builderStackEntry = (BuilderStackEntry)this._builderStack.Peek();
				string @string = SR.GetString("Unexpected_eof_looking_for_tag", new object[]
				{
					builderStackEntry._tagName
				});
				this.ProcessException(new HttpParseException(@string, null, builderStackEntry.VirtualPath, builderStackEntry._inputText, builderStackEntry.Line));
				return;
			}
			if (this._compilerType == null)
			{
				if (!this.FInDesigner)
				{
					this._compilerType = CompilationUtil.GetDefaultLanguageCompilerInfo(this._compConfig, base.CurrentVirtualPath);
				}
				else
				{
					this._compilerType = CompilationUtil.GetCodeDefaultLanguageCompilerInfo();
				}
			}
			CompilerParameters compilerParameters = this._compilerType.CompilerParameters;
			if (this.flags[8192])
			{
				compilerParameters.IncludeDebugInformation = this.flags[16384];
			}
			if (compilerParameters.IncludeDebugInformation)
			{
				HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Medium, "Debugging_not_supported_in_low_trust");
			}
			if (this._warningLevel >= 0)
			{
				compilerParameters.WarningLevel = this._warningLevel;
				compilerParameters.TreatWarningsAsErrors = (this._warningLevel > 0);
			}
			if (this._compilerOptions != null)
			{
				compilerParameters.CompilerOptions = this._compilerOptions;
			}
			if (this._pageParserFilter != null)
			{
				this._pageParserFilter.ParseComplete(this.RootBuilder);
			}
			this.ParseRecorders.ParseComplete(this.RootBuilder);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000781C4 File Offset: 0x000763C4
		private void ProcessLiteral()
		{
			string literal = this.GetLiteral();
			if (string.IsNullOrEmpty(literal))
			{
				this.flags[8] = false;
				return;
			}
			if (this.FApplicationFile)
			{
				int num = Util.FirstNonWhiteSpaceIndex(literal);
				if (num >= 0 && !this.IgnoreParseErrors)
				{
					this._lineNumber -= Util.LineCount(literal, num, literal.Length);
					this.ProcessError(SR.GetString("Invalid_app_file_content"));
				}
			}
			else
			{
				bool flag = false;
				if (this.flags[8])
				{
					this.flags[8] = false;
					if (Util.IsWhiteSpaceString(literal))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					if (!this.flags[2048])
					{
						this.ProcessMainDirective(this._mainDirectiveConfigSettings);
						this.flags[2048] = true;
					}
					ControlBuilder builder = ((BuilderStackEntry)this.BuilderStack.Peek())._builder;
					try
					{
						builder.AppendLiteralString(literal);
					}
					catch (Exception ex)
					{
						if (!this.IgnoreParseErrors)
						{
							int num2 = Util.FirstNonWhiteSpaceIndex(literal);
							if (num2 < 0)
							{
								num2 = 0;
							}
							this._lineNumber -= Util.LineCount(literal, num2, literal.Length);
							this.ProcessException(ex);
						}
					}
					this.UpdateTypeHashCode("string");
				}
			}
			this._literalBuilder = null;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0007831C File Offset: 0x0007651C
		private void ProcessServerScript()
		{
			string text = this.GetLiteral();
			if (string.IsNullOrEmpty(text))
			{
				if (!this.IgnoreParseErrors)
				{
					return;
				}
				text = string.Empty;
			}
			if (!this.flags[4] && !this.PageParserFilterProcessedCodeBlock(CodeConstructType.ScriptTag, text, this._currentScript.Line))
			{
				this.EnsureCodeAllowed();
				this._currentScript.Script = text;
				this._scriptList.Add(this._currentScript);
				this._currentScript = null;
			}
			this._literalBuilder = null;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0007839C File Offset: 0x0007659C
		internal virtual void CheckObjectTagScope(ref ObjectTagScope scope)
		{
			if (scope == ObjectTagScope.Default)
			{
				scope = ObjectTagScope.Page;
			}
			if (scope != ObjectTagScope.Page)
			{
				throw new HttpException(SR.GetString("App_session_only_valid_in_global_asax"));
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000783BC File Offset: 0x000765BC
		private void ProcessObjectTag(ObjectTagBuilder objectBuilder)
		{
			ObjectTagScope scope = objectBuilder.Scope;
			this.CheckObjectTagScope(ref scope);
			if (scope == ObjectTagScope.Page || scope == ObjectTagScope.AppInstance)
			{
				if (this._pageObjectList == null)
				{
					this._pageObjectList = new ArrayList();
				}
				this._pageObjectList.Add(objectBuilder);
				return;
			}
			if (scope == ObjectTagScope.Session)
			{
				if (this._sessionObjects == null)
				{
					this._sessionObjects = new HttpStaticObjectsCollection();
				}
				this._sessionObjects.Add(objectBuilder.ID, objectBuilder.ObjectType, objectBuilder.LateBound);
				return;
			}
			if (scope == ObjectTagScope.Application)
			{
				if (this._applicationObjects == null)
				{
					this._applicationObjects = new HttpStaticObjectsCollection();
				}
				this._applicationObjects.Add(objectBuilder.ID, objectBuilder.ObjectType, objectBuilder.LateBound);
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0007846A File Offset: 0x0007666A
		private void AppendSubBuilder(ControlBuilder builder, ControlBuilder subBuilder)
		{
			if (subBuilder is ObjectTagBuilder)
			{
				this.ProcessObjectTag((ObjectTagBuilder)subBuilder);
				return;
			}
			builder.AppendSubBuilder(subBuilder);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00078488 File Offset: 0x00076688
		private bool ProcessBeginTag(Match match, string inputText)
		{
			string value = match.Groups["tagname"].Value;
			ParsedAttributeCollection attribs;
			string text;
			this.ProcessAttributes(inputText, match, out attribs, false, out text);
			bool success = match.Groups["empty"].Success;
			if (StringUtil.EqualsIgnoreCase(value, "script") && this.flags[1])
			{
				this.ProcessScriptTag(match, inputText, attribs, success);
				return true;
			}
			if (!this.flags[2048])
			{
				this.ProcessMainDirective(this._mainDirectiveConfigSettings);
				this.flags[2048] = true;
			}
			ControlBuilder controlBuilder = null;
			ControlBuilder controlBuilder2 = null;
			Type type = null;
			string tagName;
			string filter = Util.ParsePropertyDeviceFilter(value, out tagName);
			if (this.BuilderStack.Count > 1)
			{
				controlBuilder = ((BuilderStackEntry)this._builderStack.Peek())._builder;
				if (controlBuilder is StringPropertyBuilder)
				{
					return false;
				}
				controlBuilder2 = controlBuilder.CreateChildBuilder(filter, tagName, attribs, this, controlBuilder, this._id, this._lineNumber, base.CurrentVirtualPath, ref type, false);
			}
			if (controlBuilder2 == null && this.flags[1])
			{
				controlBuilder2 = this.RootBuilder.CreateChildBuilder(filter, tagName, attribs, this, controlBuilder, this._id, this._lineNumber, base.CurrentVirtualPath, ref type, false);
			}
			if (controlBuilder2 == null && this._builderStack.Count > 1 && !success)
			{
				BuilderStackEntry builderStackEntry = (BuilderStackEntry)this._builderStack.Peek();
				if (StringUtil.EqualsIgnoreCase(value, builderStackEntry._tagName))
				{
					builderStackEntry._repeatCount++;
				}
			}
			if (controlBuilder2 == null)
			{
				if (!this.flags[1] || this.IgnoreParseErrors)
				{
					return false;
				}
				this.ProcessError(SR.GetString("Unknown_server_tag", new object[]
				{
					value
				}));
				return true;
			}
			else
			{
				if (this._pageParserFilter != null && !this._pageParserFilter.AllowControlInternal(type, controlBuilder2))
				{
					this.ProcessError(SR.GetString("Control_type_not_allowed", new object[]
					{
						type.FullName
					}));
					return true;
				}
				if (text != null)
				{
					this.ProcessError(SR.GetString("Duplicate_attr_in_tag", new object[]
					{
						text
					}));
				}
				this._id = controlBuilder2.ID;
				if (this._id != null)
				{
					if (!CodeGenerator.IsValidLanguageIndependentIdentifier(this._id))
					{
						this.ProcessError(SR.GetString("Invalid_identifier", new object[]
						{
							this._id
						}));
						return true;
					}
					if (this._idList.Contains(this._id))
					{
						this.ProcessError(SR.GetString("Id_already_used", new object[]
						{
							this._id
						}));
						return true;
					}
					this._idList.Add(this._id);
				}
				else if (this.flags[1])
				{
					PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)TypeDescriptor.GetAttributes(type)[typeof(PartialCachingAttribute)];
					if (!(controlBuilder2.Parser is PageThemeParser) && partialCachingAttribute != null)
					{
						this._id = "_ctrl_" + this._controlCount.ToString(NumberFormatInfo.InvariantInfo);
						controlBuilder2.ID = this._id;
						this._controlCount++;
						controlBuilder2.PreprocessAttribute(string.Empty, "id", this._id, false, 0, 0);
					}
				}
				this.ProcessLiteral();
				if (type != null)
				{
					this.UpdateTypeHashCode(type.FullName);
				}
				if (!success && controlBuilder2.HasBody())
				{
					if (controlBuilder2 is TemplateBuilder && ((TemplateBuilder)controlBuilder2).AllowMultipleInstances)
					{
						this._idListStack.Push(this._idList);
						this._idList = new CaseInsensitiveStringSet();
					}
					this._builderStack.Push(new BuilderStackEntry(controlBuilder2, value, base.CurrentVirtualPathString, this._lineNumber, inputText, match.Index + match.Length));
					this.ParseRecorders.RecordBeginTag(controlBuilder2, match);
				}
				else
				{
					controlBuilder = ((BuilderStackEntry)this._builderStack.Peek())._builder;
					this.AppendSubBuilder(controlBuilder, controlBuilder2);
					controlBuilder2.CloseControl();
					this.ParseRecorders.RecordEmptyTag(controlBuilder2, match);
				}
				return true;
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00078894 File Offset: 0x00076A94
		private void ProcessScriptTag(Match match, string text, IDictionary attribs, bool fSelfClosed)
		{
			this.ProcessLiteral();
			this.flags[8] = true;
			VirtualPath virtualPath = Util.GetAndRemoveVirtualPathAttribute(attribs, "src");
			if (virtualPath != null)
			{
				this.EnsureCodeAllowed();
				virtualPath = base.ResolveVirtualPath(virtualPath);
				HttpRuntime.CheckVirtualFilePermission(virtualPath.VirtualPathString);
				this.AddSourceDependency(virtualPath);
				this.ProcessLanguageAttribute((string)attribs["language"]);
				this._currentScript = new ScriptBlockData(1, 1, virtualPath.VirtualPathString);
				this._currentScript.Script = Util.StringFromVirtualPath(virtualPath);
				this._scriptList.Add(this._currentScript);
				this._currentScript = null;
				if (!fSelfClosed)
				{
					this.flags[2] = true;
					this._scriptStartLineNumber = this._lineNumber;
					this.flags[4] = true;
				}
				return;
			}
			this.ProcessLanguageAttribute((string)attribs["language"]);
			int num = match.Index + match.Length;
			int num2 = text.LastIndexOfAny(TemplateParser.s_newlineChars, num - 1);
			int column = num - num2;
			this._currentScript = new ScriptBlockData(this._lineNumber, column, base.CurrentVirtualPathString);
			if (fSelfClosed)
			{
				this.ProcessError(SR.GetString("Script_tag_without_src_must_have_content"));
			}
			this.flags[2] = true;
			this._scriptStartLineNumber = this._lineNumber;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000789E8 File Offset: 0x00076BE8
		private bool ProcessEndTag(Match match)
		{
			string value = match.Groups["tagname"].Value;
			if (!this.flags[2])
			{
				return this.MaybeTerminateControl(value, match);
			}
			if (!StringUtil.EqualsIgnoreCase(value, "script"))
			{
				return false;
			}
			this.ProcessServerScript();
			this.flags[2] = false;
			this.flags[4] = false;
			return true;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x00078A52 File Offset: 0x00076C52
		internal bool IsExpressionBuilderValue(string val)
		{
			return ControlBuilder.expressionBuilderRegex.Match(val, 0).Success;
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060024C7 RID: 9415
		internal abstract string DefaultDirectiveName { get; }

		// Token: 0x060024C8 RID: 9416 RVA: 0x00078A65 File Offset: 0x00076C65
		internal void PreprocessDirective(string directiveName, IDictionary directive)
		{
			if (this._pageParserFilter == null)
			{
				return;
			}
			if (directiveName.Length == 0)
			{
				directiveName = this.DefaultDirectiveName;
			}
			this._pageParserFilter.PreprocessDirective(directiveName, directive);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00078A90 File Offset: 0x00076C90
		internal virtual void ProcessDirective(string directiveName, IDictionary directive)
		{
			if (directiveName.Length == 0)
			{
				if (this.FInDesigner)
				{
					return;
				}
				if (this.flags[1024])
				{
					this.ProcessError(SR.GetString("Only_one_directive_allowed", new object[]
					{
						this.DefaultDirectiveName
					}));
					return;
				}
				if (this._mainDirectiveConfigSettings != null)
				{
					foreach (object obj in this._mainDirectiveConfigSettings)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!directive.Contains(dictionaryEntry.Key))
						{
							directive[dictionaryEntry.Key] = dictionaryEntry.Value;
						}
					}
				}
				this.ProcessMainDirective(directive);
				this.flags[1024] = true;
				this.flags[2048] = true;
				return;
			}
			else if (StringUtil.EqualsIgnoreCase(directiveName, "assembly"))
			{
				string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directive, "name");
				VirtualPath andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "src");
				Util.CheckUnknownDirectiveAttributes(directiveName, directive);
				if (andRemoveNonEmptyAttribute != null && andRemoveVirtualPathAttribute != null)
				{
					this.ProcessError(SR.GetString("Attributes_mutually_exclusive", new object[]
					{
						"Name",
						"Src"
					}));
				}
				if (andRemoveNonEmptyAttribute != null)
				{
					this.AddAssemblyDependency(andRemoveNonEmptyAttribute);
					return;
				}
				if (andRemoveVirtualPathAttribute != null)
				{
					this.ImportSourceFile(andRemoveVirtualPathAttribute);
					return;
				}
				this.ProcessError(SR.GetString("Missing_attr", new object[]
				{
					"name"
				}));
				return;
			}
			else
			{
				if (StringUtil.EqualsIgnoreCase(directiveName, "import"))
				{
					this.ProcessImportDirective(directiveName, directive);
					return;
				}
				if (!StringUtil.EqualsIgnoreCase(directiveName, "implements"))
				{
					if (!this.FInDesigner)
					{
						this.ProcessError(SR.GetString("Unknown_directive", new object[]
						{
							directiveName
						}));
					}
					return;
				}
				this.OnFoundDirectiveRequiringCompilation(directiveName);
				string andRemoveRequiredAttribute = Util.GetAndRemoveRequiredAttribute(directive, "interface");
				Util.CheckUnknownDirectiveAttributes(directiveName, directive);
				Type type = this.GetType(andRemoveRequiredAttribute);
				if (!type.IsInterface)
				{
					this.ProcessError(SR.GetString("Invalid_type_to_implement", new object[]
					{
						andRemoveRequiredAttribute
					}));
					return;
				}
				if (this._implementedInterfaces == null)
				{
					this._implementedInterfaces = new ArrayList();
				}
				this._implementedInterfaces.Add(type);
				return;
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00078CD0 File Offset: 0x00076ED0
		internal virtual void ProcessMainDirective(IDictionary mainDirective)
		{
			IDictionary parseData = new HybridDictionary();
			ParsedAttributeCollection parsedAttributeCollection = null;
			foreach (object obj in mainDirective)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				string text2 = Util.ParsePropertyDeviceFilter(text, out text);
				try
				{
					if (!this.ProcessMainDirectiveAttribute(text2, text, (string)dictionaryEntry.Value, parseData))
					{
						if (parsedAttributeCollection == null)
						{
							parsedAttributeCollection = TemplateParser.CreateEmptyAttributeBag();
						}
						parsedAttributeCollection.AddFilteredAttribute(text2, text, (string)dictionaryEntry.Value);
					}
				}
				catch (Exception ex)
				{
					this.ProcessException(ex);
				}
			}
			this.PostProcessMainDirectiveAttributes(parseData);
			this.RootBuilder.SetControlType(this.BaseType);
			if (parsedAttributeCollection == null)
			{
				return;
			}
			this.RootBuilder.ProcessImplicitResources(parsedAttributeCollection);
			foreach (object obj2 in parsedAttributeCollection.GetFilteredAttributeDictionaries())
			{
				FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)obj2;
				string filter = filteredAttributeDictionary.Filter;
				foreach (object obj3 in ((IEnumerable)filteredAttributeDictionary))
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj3;
					string attribName = (string)dictionaryEntry2.Key;
					this.ProcessUnknownMainDirectiveAttribute(filter, attribName, (string)dictionaryEntry2.Value);
				}
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00078E78 File Offset: 0x00077078
		internal virtual bool ProcessMainDirectiveAttribute(string deviceName, string name, string value, IDictionary parseData)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1483009432U)
			{
				if (num <= 617100660U)
				{
					if (num != 218424552U)
					{
						if (num != 349874234U)
						{
							if (num == 617100660U)
							{
								if (name == "strict")
								{
									this.flags[65536] = Util.GetBooleanAttribute(name, value);
									goto IL_348;
								}
							}
						}
						else if (name == "compileroptions")
						{
							this.OnFoundAttributeRequiringCompilation(name);
							string compilerOptions = value.Trim();
							CompilationUtil.CheckCompilerOptionsAllowed(compilerOptions, false, null, 0);
							this._compilerOptions = compilerOptions;
							goto IL_348;
						}
					}
					else if (name == "codebehind")
					{
						goto IL_348;
					}
				}
				else if (num != 865036930U)
				{
					if (num != 879704937U)
					{
						if (num == 1483009432U)
						{
							if (name == "debug")
							{
								this.flags[16384] = Util.GetBooleanAttribute(name, value);
								if (this.flags[16384] && !HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
								{
									throw new HttpException(SR.GetString("Insufficient_trust_for_attribute", new object[]
									{
										"debug"
									}));
								}
								this.flags[8192] = true;
								goto IL_348;
							}
						}
					}
					else if (name == "description")
					{
						goto IL_348;
					}
				}
				else if (name == "codefile")
				{
					this.OnFoundAttributeRequiringCompilation(name);
					try
					{
						this.ProcessCodeFile(VirtualPath.Create(Util.GetNonEmptyAttribute(name, value)));
						goto IL_348;
					}
					catch (Exception ex)
					{
						this.ProcessException(ex);
						goto IL_348;
					}
				}
			}
			else if (num <= 2563056745U)
			{
				if (num != 1760006473U)
				{
					if (num != 1998589948U)
					{
						if (num == 2563056745U)
						{
							if (name == "inherits")
							{
								parseData[name] = Util.GetNonEmptyAttribute(name, value);
								goto IL_348;
							}
						}
					}
					else if (name == "classname")
					{
						this._generatedClassName = Util.GetNonEmptyFullClassNameAttribute(name, value, ref this._generatedNamespace);
						goto IL_348;
					}
				}
				else if (name == "explicit")
				{
					this.flags[4096] = Util.GetBooleanAttribute(name, value);
					goto IL_348;
				}
			}
			else if (num <= 3119462523U)
			{
				if (num != 2913749231U)
				{
					if (num == 3119462523U)
					{
						if (name == "language")
						{
							this.ValidateBuiltInAttribute(deviceName, name, value);
							string nonEmptyAttribute = Util.GetNonEmptyAttribute(name, value);
							this.ProcessLanguageAttribute(nonEmptyAttribute);
							goto IL_348;
						}
					}
				}
				else if (name == "warninglevel")
				{
					this._warningLevel = Util.GetNonNegativeIntegerAttribute(name, value);
					goto IL_348;
				}
			}
			else if (num != 3393250228U)
			{
				if (num == 3543982537U)
				{
					if (name == "src")
					{
						this.OnFoundAttributeRequiringCompilation(name);
						parseData[name] = Util.GetNonEmptyAttribute(name, value);
						goto IL_348;
					}
				}
			}
			else if (name == "linepragmas")
			{
				this.flags[32768] = !Util.GetBooleanAttribute(name, value);
				goto IL_348;
			}
			return false;
			IL_348:
			this.ValidateBuiltInAttribute(deviceName, name, value);
			return true;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000791E8 File Offset: 0x000773E8
		internal void ValidateBuiltInAttribute(string deviceName, string name, string value)
		{
			if (this.IsExpressionBuilderValue(value))
			{
				this.ProcessError(SR.GetString("Illegal_Resource_Builder", new object[]
				{
					name
				}));
			}
			if (deviceName.Length > 0)
			{
				this.ProcessError(SR.GetString("Illegal_Device", new object[]
				{
					name
				}));
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0007923B File Offset: 0x0007743B
		internal virtual void ProcessUnknownMainDirectiveAttribute(string filter, string attribName, string value)
		{
			this.ProcessError(SR.GetString("Attr_not_supported_in_directive", new object[]
			{
				attribName,
				this.DefaultDirectiveName
			}));
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00079260 File Offset: 0x00077460
		internal virtual void PostProcessMainDirectiveAttributes(IDictionary parseData)
		{
			string text = (string)parseData["src"];
			Assembly assembly = null;
			if (text != null)
			{
				try
				{
					assembly = this.ImportSourceFile(VirtualPath.Create(text));
				}
				catch (Exception ex)
				{
					this.ProcessException(ex);
				}
			}
			string text2 = (string)parseData["codefilebaseclass"];
			if (text2 != null && this._codeFileVirtualPath == null)
			{
				throw new HttpException(SR.GetString("CodeFileBaseClass_Without_Codefile"));
			}
			string text3 = (string)parseData["inherits"];
			if (text3 != null)
			{
				try
				{
					this.ProcessInheritsAttribute(text3, text2, text, assembly);
					return;
				}
				catch (Exception ex2)
				{
					this.ProcessException(ex2);
					return;
				}
			}
			if (this._codeFileVirtualPath != null)
			{
				throw new HttpException(SR.GetString("Codefile_without_inherits"));
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00079338 File Offset: 0x00077538
		private void ProcessInheritsAttribute(string baseTypeName, string codeFileBaseTypeName, string src, Assembly assembly)
		{
			if (this._codeFileVirtualPath != null)
			{
				this._baseTypeName = Util.GetNonEmptyFullClassNameAttribute("inherits", baseTypeName, ref this._baseTypeNamespace);
				baseTypeName = codeFileBaseTypeName;
				if (baseTypeName == null)
				{
					return;
				}
			}
			Type type = null;
			if (assembly != null)
			{
				type = assembly.GetType(baseTypeName, false, true);
			}
			else
			{
				try
				{
					type = this.GetType(baseTypeName);
				}
				catch
				{
					if (this._generatedNamespace == null)
					{
						throw;
					}
					if (baseTypeName.IndexOf('.') >= 0)
					{
						throw;
					}
					try
					{
						string typeName = this._generatedNamespace + "." + baseTypeName;
						type = this.GetType(typeName);
					}
					catch
					{
					}
					if (type == null)
					{
						throw;
					}
				}
			}
			if (type == null)
			{
				this.ProcessError(SR.GetString("Non_existent_base_type", new object[]
				{
					baseTypeName,
					src
				}));
				return;
			}
			if (!this.DefaultBaseType.IsAssignableFrom(type))
			{
				this.ProcessError(SR.GetString("Invalid_type_to_inherit_from", new object[]
				{
					baseTypeName,
					this._baseType.FullName
				}));
				return;
			}
			if (this._pageParserFilter != null && !this._pageParserFilter.AllowBaseType(type))
			{
				throw new HttpException(SR.GetString("Base_type_not_allowed", new object[]
				{
					type.FullName
				}));
			}
			this._baseType = type;
			this.EnsureRootBuilderCreated();
			this.AddTypeDependency(this._baseType);
			this.flags[128] = true;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000794B4 File Offset: 0x000776B4
		private void ProcessImportDirective(string directiveName, IDictionary directive)
		{
			string andRemoveNonEmptyNoSpaceAttribute = Util.GetAndRemoveNonEmptyNoSpaceAttribute(directive, "namespace");
			if (andRemoveNonEmptyNoSpaceAttribute == null)
			{
				this.ProcessError(SR.GetString("Missing_attr", new object[]
				{
					"namespace"
				}));
			}
			else
			{
				this.AddImportEntry(andRemoveNonEmptyNoSpaceAttribute);
			}
			Util.CheckUnknownDirectiveAttributes(directiveName, directive);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00079500 File Offset: 0x00077700
		private void ProcessLanguageAttribute(string language)
		{
			if (language == null)
			{
				return;
			}
			if (this.FInDesigner)
			{
				return;
			}
			CompilerType compilerInfoFromLanguage = CompilationUtil.GetCompilerInfoFromLanguage(base.CurrentVirtualPath, language);
			if (this._compilerType != null && this._compilerType.CodeDomProviderType != compilerInfoFromLanguage.CodeDomProviderType)
			{
				this.ProcessError(SR.GetString("Mixed_lang_not_supported", new object[]
				{
					language
				}));
				return;
			}
			this._compilerType = compilerInfoFromLanguage;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0007956C File Offset: 0x0007776C
		private void ProcessCodeFile(VirtualPath codeFileVirtualPath)
		{
			this._codeFileVirtualPath = base.ResolveVirtualPath(codeFileVirtualPath);
			CompilerType compilerInfoFromVirtualPath = CompilationUtil.GetCompilerInfoFromVirtualPath(this._codeFileVirtualPath);
			if (this._compilerType != null && this._compilerType.CodeDomProviderType != compilerInfoFromVirtualPath.CodeDomProviderType)
			{
				this.ProcessError(SR.GetString("Inconsistent_CodeFile_Language"));
				return;
			}
			BuildManager.ValidateCodeFileVirtualPath(this._codeFileVirtualPath);
			Util.CheckVirtualFileExists(this._codeFileVirtualPath);
			this._compilerType = compilerInfoFromVirtualPath;
			this.AddSourceDependency(this._codeFileVirtualPath);
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000795EC File Offset: 0x000777EC
		private Assembly ImportSourceFile(VirtualPath virtualPath)
		{
			if (this.CompilationMode == CompilationMode.Never)
			{
				return null;
			}
			virtualPath = base.ResolveVirtualPath(virtualPath);
			if (this._pageParserFilter != null && !this._pageParserFilter.AllowVirtualReference(this.CompConfig, virtualPath))
			{
				this.ProcessError(SR.GetString("Reference_not_allowed", new object[]
				{
					virtualPath
				}));
			}
			this.AddSourceDependency(virtualPath);
			BuildResultCompiledAssembly buildResultCompiledAssembly = BuildManager.GetVPathBuildResult(virtualPath) as BuildResultCompiledAssembly;
			if (buildResultCompiledAssembly == null)
			{
				this.ProcessError(SR.GetString("Not_a_src_file", new object[]
				{
					virtualPath
				}));
			}
			Assembly resultAssembly = buildResultCompiledAssembly.ResultAssembly;
			this.AddAssemblyDependency(resultAssembly, true);
			return resultAssembly;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00079684 File Offset: 0x00077884
		private void DetectSpecialServerTagError(string text, int textPos)
		{
			if (this.IgnoreParseErrors)
			{
				return;
			}
			if (text.Length > textPos + 1 && text[textPos + 1] == '%')
			{
				this.ProcessError(SR.GetString("Malformed_server_block"));
				return;
			}
			Match match = BaseParser.gtRegex.Match(text, textPos);
			if (!match.Success)
			{
				return;
			}
			string text2 = text.Substring(textPos, match.Index - textPos + 2);
			match = BaseParser.runatServerRegex.Match(text2);
			if (!match.Success)
			{
				return;
			}
			Match match2 = BaseParser.ltRegex.Match(text2, 1);
			if (match2.Success && match2.Index < match.Index)
			{
				return;
			}
			string text3 = BaseParser.serverTagsRegex.Replace(text2, string.Empty);
			if (text3 != text2 && base.TagRegex.Match(text3).Success)
			{
				this.ProcessError(SR.GetString("Server_tags_cant_contain_percent_constructs"));
				return;
			}
			this.ProcessError(SR.GetString("Malformed_server_tag"));
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00079770 File Offset: 0x00077970
		internal void AddImportEntry(string ns)
		{
			if (this._namespaceEntries != null)
			{
				this._namespaceEntries = (Hashtable)this._namespaceEntries.Clone();
			}
			else
			{
				this._namespaceEntries = new Hashtable();
			}
			NamespaceEntry namespaceEntry = new NamespaceEntry();
			namespaceEntry.Namespace = ns;
			namespaceEntry.Line = this._lineNumber;
			namespaceEntry.VirtualPath = base.CurrentVirtualPathString;
			this._namespaceEntries[ns] = namespaceEntry;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000797DC File Offset: 0x000779DC
		internal Assembly LoadAssembly(string assemblyName, bool throwOnFail)
		{
			if (this._typeResolutionService != null)
			{
				AssemblyName name = new AssemblyName(assemblyName);
				return this._typeResolutionService.GetAssembly(name, throwOnFail);
			}
			return this._compConfig.LoadAssembly(assemblyName, throwOnFail);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00079813 File Offset: 0x00077A13
		internal Type GetType(string typeName, bool ignoreCase)
		{
			return this.GetType(typeName, ignoreCase, true);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00079820 File Offset: 0x00077A20
		internal Type GetType(string typeName, bool ignoreCase, bool throwOnError)
		{
			Assembly assembly = null;
			int num = Util.CommaIndexInTypeName(typeName);
			if (num > 0)
			{
				string text = typeName.Substring(num + 1).Trim();
				typeName = typeName.Substring(0, num).Trim();
				try
				{
					assembly = this.LoadAssembly(text, !this.FInDesigner);
				}
				catch
				{
					throw new HttpException(SR.GetString("Assembly_not_compiled", new object[]
					{
						text
					}));
				}
			}
			if (assembly != null)
			{
				return assembly.GetType(typeName, throwOnError, ignoreCase);
			}
			Type typeFromAssemblies = Util.GetTypeFromAssemblies(this._referencedAssemblies, typeName, ignoreCase);
			if (typeFromAssemblies != null)
			{
				return typeFromAssemblies;
			}
			typeFromAssemblies = Util.GetTypeFromAssemblies(this.AssemblyDependencies, typeName, ignoreCase);
			if (typeFromAssemblies != null)
			{
				return typeFromAssemblies;
			}
			if (throwOnError)
			{
				throw new HttpException(SR.GetString("Invalid_type", new object[]
				{
					typeName
				}));
			}
			return null;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000798FC File Offset: 0x00077AFC
		internal Type GetType(string typeName)
		{
			return this.GetType(typeName, false);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00079908 File Offset: 0x00077B08
		private void ProcessServerInclude(Match match)
		{
			if (this.flags[2])
			{
				throw new HttpException(SR.GetString("Include_not_allowed_in_server_script_tag"));
			}
			this.ProcessLiteral();
			string value = match.Groups["pathtype"].Value;
			string value2 = match.Groups["filename"].Value;
			if (value2.Length == 0)
			{
				this.ProcessError(SR.GetString("Empty_file_name"));
				return;
			}
			VirtualPath virtualPath = base.CurrentVirtualPath;
			string text = null;
			if (StringUtil.EqualsIgnoreCase(value, "file"))
			{
				if (UrlPath.IsAbsolutePhysicalPath(value2))
				{
					text = value2;
				}
				else
				{
					bool flag = true;
					try
					{
						virtualPath = base.ResolveVirtualPath(VirtualPath.Create(value2));
					}
					catch
					{
						flag = false;
					}
					if (flag)
					{
						HttpRuntime.CheckVirtualFilePermission(virtualPath.VirtualPathString);
						this.AddSourceDependency(virtualPath);
					}
					else
					{
						string directoryName = Path.GetDirectoryName(base.CurrentVirtualPath.MapPath());
						text = Path.GetFullPath(Path.Combine(directoryName, value2.Replace('/', '\\')));
					}
				}
			}
			else
			{
				if (!StringUtil.EqualsIgnoreCase(value, "virtual"))
				{
					this.ProcessError(SR.GetString("Only_file_virtual_supported_on_server_include"));
					return;
				}
				virtualPath = base.ResolveVirtualPath(VirtualPath.Create(value2));
				HttpRuntime.CheckVirtualFilePermission(virtualPath.VirtualPathString);
				this.AddSourceDependency(virtualPath);
			}
			if (text != null)
			{
				HttpRuntime.CheckFilePermission(text);
			}
			if (this._pageParserFilter != null && !this._pageParserFilter.AllowServerSideInclude(virtualPath.VirtualPathString))
			{
				this.ProcessError(SR.GetString("Include_not_allowed", new object[]
				{
					virtualPath
				}));
			}
			this.ParseFile(text, virtualPath);
			this.flags[8] = true;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00079AA0 File Offset: 0x00077CA0
		private void ProcessCodeBlock(Match match, CodeBlockType blockType, string text)
		{
			this.ProcessLiteral();
			Group group = match.Groups["code"];
			string text2 = group.Value;
			bool success = match.Groups["encode"].Success;
			text2 = text2.Replace("%\\>", "%>");
			int num = this._lineNumber;
			int num2 = -1;
			if (blockType != CodeBlockType.Code)
			{
				int num3 = -1;
				int num4 = 0;
				while (num4 < text2.Length && char.IsWhiteSpace(text2[num4]))
				{
					if (text2[num4] == '\r' || (text2[num4] == '\n' && (num4 == 0 || text2[num4 - 1] != '\r')))
					{
						num++;
						num3 = num4;
					}
					else if (text2[num4] == '\n')
					{
						num3 = num4;
					}
					num4++;
				}
				if (num3 >= 0)
				{
					text2 = text2.Substring(num3 + 1);
					num2 = 1;
				}
				num3 = -1;
				int num5 = text2.Length - 1;
				while (num5 >= 0 && char.IsWhiteSpace(text2[num5]))
				{
					if (text2[num5] == '\r' || text2[num5] == '\n')
					{
						num3 = num5;
					}
					num5--;
				}
				if (num3 >= 0)
				{
					text2 = text2.Substring(0, num3);
				}
				if (!this.IgnoreParseErrors && Util.IsWhiteSpaceString(text2))
				{
					this.ProcessError(SR.GetString("Empty_expression"));
					return;
				}
			}
			if (num2 < 0)
			{
				int num6 = text.LastIndexOfAny(TemplateParser.s_newlineChars, group.Index - 1);
				num2 = group.Index - num6;
			}
			ControlBuilder builder = ((BuilderStackEntry)this.BuilderStack.Peek())._builder;
			if (!this.PageParserFilterProcessedCodeBlock(TemplateParser.CodeConstructTypeFromCodeBlockType(blockType), text2, num))
			{
				this.EnsureCodeAllowed();
				ControlBuilder controlBuilder = new CodeBlockBuilder(blockType, text2, num, num2, base.CurrentVirtualPath, success);
				this.AppendSubBuilder(builder, controlBuilder);
				this.ParseRecorders.RecordCodeBlock(controlBuilder, match);
			}
			if (blockType == CodeBlockType.Code)
			{
				this.flags[8] = true;
			}
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00079C82 File Offset: 0x00077E82
		private static CodeConstructType CodeConstructTypeFromCodeBlockType(CodeBlockType blockType)
		{
			switch (blockType)
			{
			case CodeBlockType.Code:
				return CodeConstructType.CodeSnippet;
			case CodeBlockType.Expression:
				return CodeConstructType.ExpressionSnippet;
			case CodeBlockType.DataBinding:
				return CodeConstructType.DataBindingSnippet;
			case CodeBlockType.EncodedExpression:
				return CodeConstructType.EncodedExpressionSnippet;
			default:
				return CodeConstructType.CodeSnippet;
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00079CA8 File Offset: 0x00077EA8
		private bool PageParserFilterProcessedCodeBlock(CodeConstructType codeConstructType, string code, int lineNumber)
		{
			if (this._pageParserFilter == null || this.CompilationMode == CompilationMode.Never)
			{
				return false;
			}
			int lineNumber2 = this._lineNumber;
			this._lineNumber = lineNumber;
			bool result;
			try
			{
				result = this._pageParserFilter.ProcessCodeConstruct(codeConstructType, code);
			}
			finally
			{
				this._lineNumber = lineNumber2;
			}
			return result;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00079D00 File Offset: 0x00077F00
		internal bool PageParserFilterProcessedDataBindingAttribute(string controlId, string attributeName, string code)
		{
			return this._pageParserFilter != null && this.CompilationMode != CompilationMode.Never && this._pageParserFilter.ProcessDataBindingAttribute(controlId, attributeName, code);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00079D23 File Offset: 0x00077F23
		internal bool PageParserFilterProcessedEventHookupAttribute(string controlId, string eventName, string handlerName)
		{
			return this._pageParserFilter != null && this.CompilationMode != CompilationMode.Never && this._pageParserFilter.ProcessEventHookup(controlId, eventName, handlerName);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00079D48 File Offset: 0x00077F48
		internal void AddControl(Type type, IDictionary attributes)
		{
			ControlBuilder builder = ((BuilderStackEntry)this.BuilderStack.Peek())._builder;
			ControlBuilder subBuilder = ControlBuilder.CreateBuilderFromType(this, builder, type, null, null, attributes, this._lineNumber, base.CurrentVirtualPath.VirtualPathString);
			this.AppendSubBuilder(builder, subBuilder);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00079D90 File Offset: 0x00077F90
		private string ProcessAttributes(string text, Match match, out ParsedAttributeCollection attribs, bool fDirective, out string duplicateAttribute)
		{
			string text2 = string.Empty;
			attribs = TemplateParser.CreateEmptyAttributeBag();
			CaptureCollection captures = match.Groups["attrname"].Captures;
			CaptureCollection captures2 = match.Groups["attrval"].Captures;
			CaptureCollection captureCollection = null;
			if (fDirective)
			{
				captureCollection = match.Groups["equal"].Captures;
			}
			this.flags[1] = false;
			this._id = null;
			duplicateAttribute = null;
			for (int i = 0; i < captures.Count; i++)
			{
				string text3 = captures[i].ToString();
				if (fDirective)
				{
					text3 = text3.ToLower(CultureInfo.InvariantCulture);
				}
				Capture capture = captures2[i];
				string text4 = capture.ToString();
				string empty = string.Empty;
				string text5 = Util.ParsePropertyDeviceFilter(text3, out empty);
				text4 = HttpUtility.HtmlDecode(text4);
				bool flag = false;
				if (fDirective)
				{
					flag = (captureCollection[i].ToString().Length > 0);
				}
				if (StringUtil.EqualsIgnoreCase(empty, "id"))
				{
					this._id = text4;
				}
				else if (StringUtil.EqualsIgnoreCase(empty, "runat"))
				{
					this.ValidateBuiltInAttribute(text5, empty, text4);
					if (!StringUtil.EqualsIgnoreCase(text4, "server"))
					{
						this.ProcessError(SR.GetString("Runat_can_only_be_server"));
					}
					this.flags[1] = true;
					text3 = null;
				}
				else if (this.FInDesigner && StringUtil.EqualsIgnoreCase(empty, "ignoreParentFrozen"))
				{
					text3 = null;
				}
				if (text3 != null)
				{
					if (fDirective && !flag && i == 0)
					{
						text2 = text3;
						if (string.Compare(text2, this.DefaultDirectiveName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							text2 = string.Empty;
						}
					}
					else
					{
						try
						{
							if (fDirective && text2.Length > 0 && text5.Length > 0)
							{
								this.ProcessError(SR.GetString("Device_unsupported_in_directive", new object[]
								{
									text2
								}));
							}
							else
							{
								attribs.AddFilteredAttribute(text5, empty, text4);
								if (BuildManagerHost.InClientBuildManager)
								{
									int line = this._lineNumber + Util.LineCount(text, match.Index, capture.Index);
									int column = capture.Index - text.LastIndexOfAny(TemplateParser.s_newlineChars, capture.Index - 1);
									attribs.AddAttributeValuePositionInformation(empty, line, column);
								}
							}
						}
						catch (ArgumentException)
						{
							duplicateAttribute = text3;
						}
						catch (Exception ex)
						{
							this.ProcessException(ex);
						}
					}
				}
			}
			if (duplicateAttribute != null && fDirective)
			{
				this.ProcessError(SR.GetString("Duplicate_attr_in_directive", new object[]
				{
					duplicateAttribute
				}));
			}
			return text2;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0007A02C File Offset: 0x0007822C
		private static ParsedAttributeCollection CreateEmptyAttributeBag()
		{
			return new ParsedAttributeCollection();
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0007A034 File Offset: 0x00078234
		private bool MaybeTerminateControl(string tagName, Match match)
		{
			BuilderStackEntry builderStackEntry = (BuilderStackEntry)this.BuilderStack.Peek();
			ControlBuilder builder = builderStackEntry._builder;
			if (builderStackEntry._tagName == null || !StringUtil.EqualsIgnoreCase(builderStackEntry._tagName, tagName))
			{
				return false;
			}
			if (builderStackEntry._repeatCount > 0)
			{
				builderStackEntry._repeatCount--;
				return false;
			}
			this.ProcessLiteral();
			if (builder.NeedsTagInnerText())
			{
				try
				{
					builder.SetTagInnerText(builderStackEntry._inputText.Substring(builderStackEntry._textPos, match.Index - builderStackEntry._textPos));
				}
				catch (Exception ex)
				{
					if (!this.IgnoreParseErrors)
					{
						this._lineNumber = builder.Line;
						this.ProcessException(ex);
						return true;
					}
				}
			}
			if (builder is TemplateBuilder && ((TemplateBuilder)builder).AllowMultipleInstances)
			{
				this._idList = (StringSet)this._idListStack.Pop();
			}
			this._builderStack.Pop();
			this.AppendSubBuilder(((BuilderStackEntry)this._builderStack.Peek())._builder, builder);
			builder.CloseControl();
			this.ParseRecorders.RecordEndTag(builder, match);
			return true;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0007A15C File Offset: 0x0007835C
		internal Type MapStringToType(string typeName, IDictionary attribs)
		{
			return this.RootBuilder.GetChildControlType(typeName, attribs);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0007A16B File Offset: 0x0007836B
		internal void AddSourceDependency(VirtualPath fileName)
		{
			if (this._pageParserFilter != null)
			{
				this._pageParserFilter.OnDependencyAdded();
				this._pageParserFilter.OnDirectDependencyAdded();
			}
			this.AddSourceDependency2(fileName);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0007A192 File Offset: 0x00078392
		private void AddSourceDependency2(VirtualPath fileName)
		{
			if (this._sourceDependencies == null)
			{
				this._sourceDependencies = new CaseInsensitiveStringSet();
			}
			this._sourceDependencies.Add(fileName.VirtualPathString);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0007A1B8 File Offset: 0x000783B8
		internal void AddBuildResultDependency(BuildResult result)
		{
			if (this._pageParserFilter != null)
			{
				this._pageParserFilter.OnDirectDependencyAdded();
			}
			if (result.VirtualPathDependencies == null)
			{
				return;
			}
			foreach (object obj in result.VirtualPathDependencies)
			{
				string virtualPath = (string)obj;
				if (this._pageParserFilter != null)
				{
					this._pageParserFilter.OnDependencyAdded();
				}
				this.AddSourceDependency2(VirtualPath.Create(virtualPath));
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0007A248 File Offset: 0x00078448
		internal void AddTypeDependency(Type type)
		{
			this.AddBaseTypeDependencies(type);
			if (type.Namespace != null && BaseCodeDomTreeGenerator.IsAspNetNamespace(type.Namespace))
			{
				this.AddImportEntry(type.Namespace);
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0007A274 File Offset: 0x00078474
		private void AddBaseTypeDependencies(Type type)
		{
			Assembly assembly = type.Module.Assembly;
			if (assembly == typeof(string).Assembly || assembly == typeof(Page).Assembly || assembly == typeof(Uri).Assembly)
			{
				return;
			}
			this.AddAssemblyDependency(assembly);
			if (type.BaseType != null)
			{
				this.AddBaseTypeDependencies(type.BaseType);
			}
			Type[] interfaces = type.GetInterfaces();
			foreach (Type type2 in interfaces)
			{
				this.AddBaseTypeDependencies(type2);
			}
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0007A318 File Offset: 0x00078518
		internal Assembly AddAssemblyDependency(string assemblyName, bool addDependentAssemblies)
		{
			Assembly assembly = this.LoadAssembly(assemblyName, !this.FInDesigner);
			if (assembly != null)
			{
				this.AddAssemblyDependency(assembly, addDependentAssemblies);
			}
			return assembly;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0007A348 File Offset: 0x00078548
		internal Assembly AddAssemblyDependency(string assemblyName)
		{
			return this.AddAssemblyDependency(assemblyName, false);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0007A354 File Offset: 0x00078554
		internal void AddAssemblyDependency(Assembly assembly, bool addDependentAssemblies)
		{
			if (this._assemblyDependencies == null)
			{
				this._assemblyDependencies = new AssemblySet();
			}
			if (this._typeResolutionService != null)
			{
				this._typeResolutionService.ReferenceAssembly(assembly.GetName());
			}
			this._assemblyDependencies.Add(assembly);
			if (addDependentAssemblies)
			{
				AssemblySet referencedAssemblies = Util.GetReferencedAssemblies(assembly);
				this.AddAssemblyDependencies(referencedAssemblies);
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0007A3AA File Offset: 0x000785AA
		internal void AddAssemblyDependency(Assembly assembly)
		{
			this.AddAssemblyDependency(assembly, false);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0007A3B4 File Offset: 0x000785B4
		private void AddAssemblyDependencies(AssemblySet assemblyDependencies)
		{
			if (assemblyDependencies == null)
			{
				return;
			}
			foreach (object obj in ((IEnumerable)assemblyDependencies))
			{
				Assembly assembly = (Assembly)obj;
				this.AddAssemblyDependency(assembly);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x0007A40C File Offset: 0x0007860C
		ICollection IAssemblyDependencyParser.AssemblyDependencies
		{
			get
			{
				return this.AssemblyDependencies;
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0007A414 File Offset: 0x00078614
		internal IImplicitResourceProvider GetImplicitResourceProvider()
		{
			if (this.FInDesigner)
			{
				return null;
			}
			if (this.flags[262144])
			{
				return this._implicitResourceProvider;
			}
			this.flags[262144] = true;
			IResourceProvider localResourceProvider = ResourceExpressionBuilder.GetLocalResourceProvider(this._rootBuilder.VirtualPath);
			if (localResourceProvider == null)
			{
				return null;
			}
			this._implicitResourceProvider = (localResourceProvider as IImplicitResourceProvider);
			if (this._implicitResourceProvider == null)
			{
				this._implicitResourceProvider = new DefaultImplicitResourceProvider(localResourceProvider);
			}
			return this._implicitResourceProvider;
		}

		// Token: 0x04001D0C RID: 7436
		internal const string CodeFileBaseClassAttributeName = "codefilebaseclass";

		// Token: 0x04001D0D RID: 7437
		private CompilationSection _compConfig;

		// Token: 0x04001D0E RID: 7438
		private PagesSection _pagesConfig;

		// Token: 0x04001D0F RID: 7439
		private const int isServerTag = 1;

		// Token: 0x04001D10 RID: 7440
		private const int inScriptTag = 2;

		// Token: 0x04001D11 RID: 7441
		private const int ignoreScriptTag = 4;

		// Token: 0x04001D12 RID: 7442
		private const int ignoreNextSpaceString = 8;

		// Token: 0x04001D13 RID: 7443
		internal const int requiresCompilation = 16;

		// Token: 0x04001D14 RID: 7444
		private const int ignoreControlProperties = 32;

		// Token: 0x04001D15 RID: 7445
		internal const int aspCompatMode = 64;

		// Token: 0x04001D16 RID: 7446
		private const int hasCodeBehind = 128;

		// Token: 0x04001D17 RID: 7447
		private const int inDesigner = 256;

		// Token: 0x04001D18 RID: 7448
		private const int ignoreParseErrors = 512;

		// Token: 0x04001D19 RID: 7449
		private const int mainDirectiveSpecified = 1024;

		// Token: 0x04001D1A RID: 7450
		private const int mainDirectiveHandled = 2048;

		// Token: 0x04001D1B RID: 7451
		private const int useExplicit = 4096;

		// Token: 0x04001D1C RID: 7452
		private const int hasDebugAttribute = 8192;

		// Token: 0x04001D1D RID: 7453
		private const int debug = 16384;

		// Token: 0x04001D1E RID: 7454
		private const int noLinePragmas = 32768;

		// Token: 0x04001D1F RID: 7455
		private const int strict = 65536;

		// Token: 0x04001D20 RID: 7456
		internal const int noAutoEventWireup = 131072;

		// Token: 0x04001D21 RID: 7457
		private const int attemptedImplicitResources = 262144;

		// Token: 0x04001D22 RID: 7458
		internal const int buffer = 524288;

		// Token: 0x04001D23 RID: 7459
		internal const int requiresSessionState = 1048576;

		// Token: 0x04001D24 RID: 7460
		internal const int readOnlySessionState = 2097152;

		// Token: 0x04001D25 RID: 7461
		internal const int validateRequest = 4194304;

		// Token: 0x04001D26 RID: 7462
		internal const int asyncMode = 8388608;

		// Token: 0x04001D27 RID: 7463
		private const int throwOnFirstParseError = 16777216;

		// Token: 0x04001D28 RID: 7464
		private const int ignoreParserFilter = 33554432;

		// Token: 0x04001D29 RID: 7465
		internal const int calledFromParseControlFlag = 67108864;

		// Token: 0x04001D2A RID: 7466
		internal SimpleBitVector32 flags;

		// Token: 0x04001D2B RID: 7467
		private MainTagNameToTypeMapper _typeMapper;

		// Token: 0x04001D2C RID: 7468
		private Stack _builderStack;

		// Token: 0x04001D2D RID: 7469
		private string _id;

		// Token: 0x04001D2E RID: 7470
		private StringSet _idList;

		// Token: 0x04001D2F RID: 7471
		private Stack _idListStack;

		// Token: 0x04001D30 RID: 7472
		private ScriptBlockData _currentScript;

		// Token: 0x04001D31 RID: 7473
		private StringBuilder _literalBuilder;

		// Token: 0x04001D32 RID: 7474
		internal int _lineNumber;

		// Token: 0x04001D33 RID: 7475
		private int _scriptStartLineNumber;

		// Token: 0x04001D34 RID: 7476
		private string _text;

		// Token: 0x04001D35 RID: 7477
		private Type _baseType;

		// Token: 0x04001D36 RID: 7478
		private string _baseTypeNamespace;

		// Token: 0x04001D37 RID: 7479
		private string _baseTypeName;

		// Token: 0x04001D38 RID: 7480
		private ArrayList _implementedInterfaces;

		// Token: 0x04001D39 RID: 7481
		internal PageParserFilter _pageParserFilter;

		// Token: 0x04001D3A RID: 7482
		private IImplicitResourceProvider _implicitResourceProvider;

		// Token: 0x04001D3B RID: 7483
		private CompilationMode _compilationMode;

		// Token: 0x04001D3C RID: 7484
		private ParserErrorCollection _parserErrors;

		// Token: 0x04001D3D RID: 7485
		private IDesignerHost _designerHost;

		// Token: 0x04001D3E RID: 7486
		private ITypeResolutionService _typeResolutionService;

		// Token: 0x04001D3F RID: 7487
		private EventHandler _designTimeDataBindHandler;

		// Token: 0x04001D40 RID: 7488
		private StringSet _circularReferenceChecker;

		// Token: 0x04001D41 RID: 7489
		private ICollection _referencedAssemblies;

		// Token: 0x04001D42 RID: 7490
		private AssemblySet _assemblyDependencies;

		// Token: 0x04001D43 RID: 7491
		private StringSet _sourceDependencies;

		// Token: 0x04001D44 RID: 7492
		internal HttpStaticObjectsCollection _sessionObjects;

		// Token: 0x04001D45 RID: 7493
		internal HttpStaticObjectsCollection _applicationObjects;

		// Token: 0x04001D46 RID: 7494
		private RootBuilder _rootBuilder;

		// Token: 0x04001D47 RID: 7495
		internal IDictionary _mainDirectiveConfigSettings;

		// Token: 0x04001D48 RID: 7496
		private Hashtable _namespaceEntries;

		// Token: 0x04001D49 RID: 7497
		private CompilerType _compilerType;

		// Token: 0x04001D4A RID: 7498
		private ArrayList _scriptList;

		// Token: 0x04001D4B RID: 7499
		private HashCodeCombiner _typeHashCode = new HashCodeCombiner();

		// Token: 0x04001D4C RID: 7500
		private ArrayList _pageObjectList;

		// Token: 0x04001D4D RID: 7501
		private ParseRecorder _parseRecorders = ParseRecorder.Null;

		// Token: 0x04001D4E RID: 7502
		private int _warningLevel = -1;

		// Token: 0x04001D4F RID: 7503
		private string _compilerOptions;

		// Token: 0x04001D50 RID: 7504
		private VirtualPath _codeFileVirtualPath;

		// Token: 0x04001D51 RID: 7505
		private string _generatedClassName;

		// Token: 0x04001D52 RID: 7506
		private string _generatedNamespace;

		// Token: 0x04001D53 RID: 7507
		private ControlBuilderInterceptor _controlBuilderInterceptor;

		// Token: 0x04001D54 RID: 7508
		private int _controlCount;

		// Token: 0x04001D55 RID: 7509
		private static char[] s_newlineChars = new char[]
		{
			'\r',
			'\n'
		};
	}
}
