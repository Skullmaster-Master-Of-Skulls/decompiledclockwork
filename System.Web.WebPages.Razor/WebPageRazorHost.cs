using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Razor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using System.Web.WebPages.Instrumentation;
using System.Web.WebPages.Razor.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages.Razor
{
	// Token: 0x0200000E RID: 14
	public class WebPageRazorHost : RazorEngineHost
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002DB8 File Offset: 0x00000FB8
		private WebPageRazorHost()
		{
			this.NamespaceImports.Add("System");
			this.NamespaceImports.Add("System.Collections.Generic");
			this.NamespaceImports.Add("System.IO");
			this.NamespaceImports.Add("System.Linq");
			this.NamespaceImports.Add("System.Net");
			this.NamespaceImports.Add("System.Web");
			this.NamespaceImports.Add("System.Web.Helpers");
			this.NamespaceImports.Add("System.Web.Security");
			this.NamespaceImports.Add("System.Web.UI");
			this.NamespaceImports.Add("System.Web.WebPages");
			this.NamespaceImports.Add("System.Web.WebPages.Html");
			this.RegisterSpecialFile("_AppStart", typeof(ApplicationStartPage));
			this.RegisterSpecialFile("_PageStart", typeof(StartPage));
			this.DefaultNamespace = "ASP";
			this.GeneratedClassContext = new GeneratedClassContext(GeneratedClassContext.DefaultExecuteMethodName, GeneratedClassContext.DefaultWriteMethodName, GeneratedClassContext.DefaultWriteLiteralMethodName, "WriteTo", "WriteLiteralTo", WebPageRazorHost.TemplateTypeName, "DefineSection", "BeginContext", "EndContext")
			{
				ResolveUrlMethodName = "Href"
			};
			this.DefaultPageBaseClass = WebPageRazorHost.PageBaseClass;
			this.DefaultDebugCompilation = true;
			this.EnableInstrumentation = false;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F2B File Offset: 0x0000112B
		public WebPageRazorHost(string virtualPath) : this(virtualPath, null)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F38 File Offset: 0x00001138
		public WebPageRazorHost(string virtualPath, string physicalPath) : this()
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"virtualPath"
				}), "virtualPath");
			}
			this.VirtualPath = virtualPath;
			this.PhysicalPath = physicalPath;
			this.DefaultClassName = this.GetClassName(this.VirtualPath);
			this.CodeLanguage = this.GetCodeLanguage();
			this.EnableInstrumentation = new InstrumentationService().IsAvailable;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002FB9 File Offset: 0x000011B9
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002FD5 File Offset: 0x000011D5
		public override RazorCodeLanguage CodeLanguage
		{
			get
			{
				if (this._codeLanguage == null)
				{
					this._codeLanguage = this.GetCodeLanguage();
				}
				return this._codeLanguage;
			}
			protected set
			{
				this._codeLanguage = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002FDE File Offset: 0x000011DE
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003004 File Offset: 0x00001204
		public override string DefaultBaseClass
		{
			get
			{
				if (base.DefaultBaseClass != null)
				{
					return base.DefaultBaseClass;
				}
				if (this.IsSpecialPage)
				{
					return this.SpecialPageBaseClass;
				}
				return this.DefaultPageBaseClass;
			}
			set
			{
				base.DefaultBaseClass = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000300D File Offset: 0x0000120D
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000302F File Offset: 0x0000122F
		public override string DefaultClassName
		{
			get
			{
				if (this._className == null)
				{
					this._className = this.GetClassName(this.VirtualPath);
				}
				return this._className;
			}
			set
			{
				this._className = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003038 File Offset: 0x00001238
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003040 File Offset: 0x00001240
		public bool DefaultDebugCompilation { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003049 File Offset: 0x00001249
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003051 File Offset: 0x00001251
		public string DefaultPageBaseClass { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000305A File Offset: 0x0000125A
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000307D File Offset: 0x0000127D
		internal string GlobalAsaxTypeName
		{
			get
			{
				string result;
				if ((result = this._globalAsaxTypeName) == null)
				{
					if (!HostingEnvironment.IsHosted)
					{
						return WebPageRazorHost.FallbackApplicationTypeName;
					}
					result = BuildManager.GetGlobalAsaxType().FullName;
				}
				return result;
			}
			set
			{
				this._globalAsaxTypeName = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003086 File Offset: 0x00001286
		public bool IsSpecialPage
		{
			get
			{
				this.CheckForSpecialPage();
				return this._isSpecialPage.Value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003099 File Offset: 0x00001299
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000030A7 File Offset: 0x000012A7
		public string PhysicalPath
		{
			get
			{
				this.MapPhysicalPath();
				return this._physicalPath;
			}
			set
			{
				this._physicalPath = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000030B0 File Offset: 0x000012B0
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000030B8 File Offset: 0x000012B8
		public override string InstrumentedSourceFilePath
		{
			get
			{
				return this.VirtualPath;
			}
			set
			{
				this.VirtualPath = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000030C1 File Offset: 0x000012C1
		private string SpecialPageBaseClass
		{
			get
			{
				this.CheckForSpecialPage();
				return this._specialFileBaseClass;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000030CF File Offset: 0x000012CF
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000030D7 File Offset: 0x000012D7
		public string VirtualPath { get; private set; }

		// Token: 0x0600007A RID: 122 RVA: 0x000030E0 File Offset: 0x000012E0
		public static void AddGlobalImport(string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"ns"
				}), "ns");
			}
			WebPageRazorHost._importedNamespaces.TryAdd(ns, null);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000312C File Offset: 0x0000132C
		private void CheckForSpecialPage()
		{
			if (this._isSpecialPage == null)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.VirtualPath);
				string specialFileBaseClass;
				if (this._specialFileBaseTypes.TryGetValue(fileNameWithoutExtension, out specialFileBaseClass))
				{
					this._isSpecialPage = new bool?(true);
					this._specialFileBaseClass = specialFileBaseClass;
					return;
				}
				this._isSpecialPage = new bool?(false);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003182 File Offset: 0x00001382
		public override ParserBase CreateMarkupParser()
		{
			return new HtmlMarkupParser();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000318C File Offset: 0x0000138C
		private static RazorCodeLanguage DetermineCodeLanguage(string fileName)
		{
			string text = Path.GetExtension(fileName);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (text[0] == '.')
			{
				text = text.Substring(1);
			}
			return WebPageRazorHost.GetLanguageByExtension(text);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031C8 File Offset: 0x000013C8
		protected virtual string GetClassName(string virtualPath)
		{
			return ParserHelpers.SanitizeClassName("_Page_" + virtualPath.TrimStart(new char[]
			{
				'~',
				'/'
			}));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031FC File Offset: 0x000013FC
		protected virtual RazorCodeLanguage GetCodeLanguage()
		{
			RazorCodeLanguage razorCodeLanguage = WebPageRazorHost.DetermineCodeLanguage(this.VirtualPath);
			if (razorCodeLanguage == null && !string.IsNullOrEmpty(this.PhysicalPath))
			{
				razorCodeLanguage = WebPageRazorHost.DetermineCodeLanguage(this.PhysicalPath);
			}
			if (razorCodeLanguage == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, RazorWebResources.BuildProvider_No_CodeLanguageService_For_Path, new object[]
				{
					this.VirtualPath
				}));
			}
			return razorCodeLanguage;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003264 File Offset: 0x00001464
		public static IEnumerable<string> GetGlobalImports()
		{
			return from pair in WebPageRazorHost._importedNamespaces.ToArray()
			select pair.Key;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003292 File Offset: 0x00001492
		private static RazorCodeLanguage GetLanguageByExtension(string extension)
		{
			return RazorCodeLanguage.GetLanguageByExtension(extension);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000329C File Offset: 0x0000149C
		private void MapPhysicalPath()
		{
			if (this._physicalPath == null && HostingEnvironment.IsHosted)
			{
				string text = HostingEnvironment.MapPath(this.VirtualPath);
				if (!string.IsNullOrEmpty(text) && File.Exists(text))
				{
					this._physicalPath = text;
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000032E4 File Offset: 0x000014E4
		public override void PostProcessGeneratedCode(CodeGeneratorContext context)
		{
			base.PostProcessGeneratedCode(context);
			context.Namespace.Imports.AddRange((from s in WebPageRazorHost.GetGlobalImports()
			select new CodeNamespaceImport(s)).ToArray<CodeNamespaceImport>());
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty
			{
				Name = "ApplicationInstance",
				Type = new CodeTypeReference(this.GlobalAsaxTypeName),
				HasGet = true,
				HasSet = false,
				Attributes = (MemberAttributes)12290
			};
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(new CodeTypeReference(this.GlobalAsaxTypeName), new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(null, "Context"), "ApplicationInstance"))));
			context.GeneratedClass.Members.Insert(0, codeMemberProperty);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033B9 File Offset: 0x000015B9
		protected void RegisterSpecialFile(string fileName, Type baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			this.RegisterSpecialFile(fileName, baseType.FullName);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033DC File Offset: 0x000015DC
		protected void RegisterSpecialFile(string fileName, string baseTypeName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"fileName"
				}), "fileName");
			}
			if (string.IsNullOrEmpty(baseTypeName))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[]
				{
					"baseTypeName"
				}), "baseTypeName");
			}
			this._specialFileBaseTypes[fileName] = baseTypeName;
		}

		// Token: 0x04000025 RID: 37
		internal const string PageClassNamePrefix = "_Page_";

		// Token: 0x04000026 RID: 38
		internal const string ApplicationInstancePropertyName = "ApplicationInstance";

		// Token: 0x04000027 RID: 39
		internal const string ContextPropertyName = "Context";

		// Token: 0x04000028 RID: 40
		internal const string DefineSectionMethodName = "DefineSection";

		// Token: 0x04000029 RID: 41
		internal const string WebDefaultNamespace = "ASP";

		// Token: 0x0400002A RID: 42
		internal const string WriteToMethodName = "WriteTo";

		// Token: 0x0400002B RID: 43
		internal const string WriteLiteralToMethodName = "WriteLiteralTo";

		// Token: 0x0400002C RID: 44
		internal const string BeginContextMethodName = "BeginContext";

		// Token: 0x0400002D RID: 45
		internal const string EndContextMethodName = "EndContext";

		// Token: 0x0400002E RID: 46
		internal const string ResolveUrlMethodName = "Href";

		// Token: 0x0400002F RID: 47
		private const string ApplicationStartFileName = "_AppStart";

		// Token: 0x04000030 RID: 48
		private const string PageStartFileName = "_PageStart";

		// Token: 0x04000031 RID: 49
		internal static readonly string FallbackApplicationTypeName = typeof(HttpApplication).FullName;

		// Token: 0x04000032 RID: 50
		internal static readonly string PageBaseClass = typeof(WebPage).FullName;

		// Token: 0x04000033 RID: 51
		internal static readonly string TemplateTypeName = typeof(HelperResult).FullName;

		// Token: 0x04000034 RID: 52
		private static ConcurrentDictionary<string, object> _importedNamespaces = new ConcurrentDictionary<string, object>();

		// Token: 0x04000035 RID: 53
		private readonly Dictionary<string, string> _specialFileBaseTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000036 RID: 54
		private string _className;

		// Token: 0x04000037 RID: 55
		private RazorCodeLanguage _codeLanguage;

		// Token: 0x04000038 RID: 56
		private string _globalAsaxTypeName;

		// Token: 0x04000039 RID: 57
		private bool? _isSpecialPage;

		// Token: 0x0400003A RID: 58
		private string _physicalPath;

		// Token: 0x0400003B RID: 59
		private string _specialFileBaseClass;
	}
}
