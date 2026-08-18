using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;

namespace System.Web.Razor
{
	// Token: 0x02000058 RID: 88
	public class RazorEngineHost
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00011640 File Offset: 0x0000F840
		protected RazorEngineHost()
		{
			this.GeneratedClassContext = GeneratedClassContext.Default;
			this.NamespaceImports = new HashSet<string>();
			this.DesignTimeMode = false;
			this.DefaultNamespace = "Razor";
			this.DefaultClassName = "__CompiledTemplate";
			this.EnableInstrumentation = false;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001169B File Offset: 0x0000F89B
		public RazorEngineHost(RazorCodeLanguage codeLanguage) : this(codeLanguage, () => new HtmlMarkupParser())
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000116C1 File Offset: 0x0000F8C1
		public RazorEngineHost(RazorCodeLanguage codeLanguage, Func<ParserBase> markupParserFactory) : this()
		{
			if (codeLanguage == null)
			{
				throw new ArgumentNullException("codeLanguage");
			}
			if (markupParserFactory == null)
			{
				throw new ArgumentNullException("markupParserFactory");
			}
			this.CodeLanguage = codeLanguage;
			this._markupParserFactory = markupParserFactory;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000116F3 File Offset: 0x0000F8F3
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x000116FB File Offset: 0x0000F8FB
		public virtual GeneratedClassContext GeneratedClassContext { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00011704 File Offset: 0x0000F904
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0001170C File Offset: 0x0000F90C
		public virtual ISet<string> NamespaceImports { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00011715 File Offset: 0x0000F915
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001171D File Offset: 0x0000F91D
		public virtual string DefaultBaseClass { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00011726 File Offset: 0x0000F926
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0001172E File Offset: 0x0000F92E
		public virtual bool DesignTimeMode { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00011737 File Offset: 0x0000F937
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0001173F File Offset: 0x0000F93F
		public virtual string DefaultClassName { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00011748 File Offset: 0x0000F948
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00011750 File Offset: 0x0000F950
		public virtual string DefaultNamespace { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00011759 File Offset: 0x0000F959
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00011761 File Offset: 0x0000F961
		public virtual bool StaticHelpers { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001176A File Offset: 0x0000F96A
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00011772 File Offset: 0x0000F972
		public virtual RazorCodeLanguage CodeLanguage { get; protected set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001177B File Offset: 0x0000F97B
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0001178D File Offset: 0x0000F98D
		public virtual bool EnableInstrumentation
		{
			get
			{
				return !this.DesignTimeMode && this._instrumentationActive;
			}
			set
			{
				this._instrumentationActive = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00011796 File Offset: 0x0000F996
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001179E File Offset: 0x0000F99E
		public virtual bool IsIndentingWithTabs { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000117A7 File Offset: 0x0000F9A7
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000117AF File Offset: 0x0000F9AF
		public virtual int TabSize
		{
			get
			{
				return this._tabSize;
			}
			set
			{
				this._tabSize = Math.Max(value, 1);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000117BE File Offset: 0x0000F9BE
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000117C6 File Offset: 0x0000F9C6
		public virtual string InstrumentedSourceFilePath { get; set; }

		// Token: 0x0600042F RID: 1071 RVA: 0x000117CF File Offset: 0x0000F9CF
		public virtual ParserBase CreateMarkupParser()
		{
			if (this._markupParserFactory != null)
			{
				return this._markupParserFactory();
			}
			return null;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000117E6 File Offset: 0x0000F9E6
		public virtual ParserBase DecorateCodeParser(ParserBase incomingCodeParser)
		{
			if (incomingCodeParser == null)
			{
				throw new ArgumentNullException("incomingCodeParser");
			}
			return incomingCodeParser;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000117F7 File Offset: 0x0000F9F7
		public virtual ParserBase DecorateMarkupParser(ParserBase incomingMarkupParser)
		{
			if (incomingMarkupParser == null)
			{
				throw new ArgumentNullException("incomingMarkupParser");
			}
			return incomingMarkupParser;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011808 File Offset: 0x0000FA08
		public virtual RazorCodeGenerator DecorateCodeGenerator(RazorCodeGenerator incomingCodeGenerator)
		{
			if (incomingCodeGenerator == null)
			{
				throw new ArgumentNullException("incomingCodeGenerator");
			}
			return incomingCodeGenerator;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011819 File Offset: 0x0000FA19
		public virtual void PostProcessGeneratedCode(CodeGeneratorContext context)
		{
			this.PostProcessGeneratedCode(context.CompileUnit, context.Namespace, context.GeneratedClass, context.TargetMethod);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011839 File Offset: 0x0000FA39
		[Obsolete("This method is obsolete, use the override which takes a CodeGeneratorContext instead")]
		public virtual void PostProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeNamespace generatedNamespace, CodeTypeDeclaration generatedClass, CodeMemberMethod executeMethod)
		{
			if (codeCompileUnit == null)
			{
				throw new ArgumentNullException("codeCompileUnit");
			}
			if (generatedNamespace == null)
			{
				throw new ArgumentNullException("generatedNamespace");
			}
			if (generatedClass == null)
			{
				throw new ArgumentNullException("generatedClass");
			}
			if (executeMethod == null)
			{
				throw new ArgumentNullException("executeMethod");
			}
		}

		// Token: 0x04000123 RID: 291
		internal const string InternalDefaultClassName = "__CompiledTemplate";

		// Token: 0x04000124 RID: 292
		internal const string InternalDefaultNamespace = "Razor";

		// Token: 0x04000125 RID: 293
		private bool _instrumentationActive;

		// Token: 0x04000126 RID: 294
		private Func<ParserBase> _markupParserFactory;

		// Token: 0x04000127 RID: 295
		private int _tabSize = 4;
	}
}
