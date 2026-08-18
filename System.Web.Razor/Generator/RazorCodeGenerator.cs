using System;
using System.CodeDom;
using System.Linq;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000055 RID: 85
	public abstract class RazorCodeGenerator : ParserVisitor
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0001132C File Offset: 0x0000F52C
		protected RazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
		{
			if (string.IsNullOrEmpty(className))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "className");
			}
			if (rootNamespaceName == null)
			{
				throw new ArgumentNullException("rootNamespaceName");
			}
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			this.ClassName = className;
			this.RootNamespaceName = rootNamespaceName;
			this.SourceFileName = sourceFileName;
			this.GenerateLinePragmas = !string.IsNullOrEmpty(this.SourceFileName);
			this.Host = host;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000113A8 File Offset: 0x0000F5A8
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public string ClassName { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000113B9 File Offset: 0x0000F5B9
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x000113C1 File Offset: 0x0000F5C1
		public string RootNamespaceName { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000113CA File Offset: 0x0000F5CA
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000113D2 File Offset: 0x0000F5D2
		public string SourceFileName { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000113DB File Offset: 0x0000F5DB
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000113E3 File Offset: 0x0000F5E3
		public RazorEngineHost Host { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000113EC File Offset: 0x0000F5EC
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x000113F4 File Offset: 0x0000F5F4
		public bool GenerateLinePragmas { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000113FD File Offset: 0x0000F5FD
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00011405 File Offset: 0x0000F605
		public bool DesignTimeMode { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001140E File Offset: 0x0000F60E
		public CodeGeneratorContext Context
		{
			get
			{
				this.EnsureContextInitialized();
				return this._context;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001141C File Offset: 0x0000F61C
		internal virtual Func<CodeWriter> CodeWriterFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001141F File Offset: 0x0000F61F
		public override void VisitStartBlock(Block block)
		{
			block.CodeGenerator.GenerateStartBlockCode(block, this.Context);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011433 File Offset: 0x0000F633
		public override void VisitEndBlock(Block block)
		{
			block.CodeGenerator.GenerateEndBlockCode(block, this.Context);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00011447 File Offset: 0x0000F647
		public override void VisitSpan(Span span)
		{
			span.CodeGenerator.GenerateCode(span, this.Context);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001145B File Offset: 0x0000F65B
		public override void OnComplete()
		{
			this.Context.FlushBufferedStatement();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011468 File Offset: 0x0000F668
		private void EnsureContextInitialized()
		{
			if (this._context == null)
			{
				this._context = CodeGeneratorContext.Create(this.Host, this.CodeWriterFactory, this.ClassName, this.RootNamespaceName, this.SourceFileName, this.GenerateLinePragmas);
				this.Initialize(this._context);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000114C0 File Offset: 0x0000F6C0
		protected virtual void Initialize(CodeGeneratorContext context)
		{
			context.Namespace.Imports.AddRange((from s in this.Host.NamespaceImports
			select new CodeNamespaceImport(s)).ToArray<CodeNamespaceImport>());
			if (!string.IsNullOrEmpty(this.Host.DefaultBaseClass))
			{
				context.GeneratedClass.BaseTypes.Add(new CodeTypeReference(this.Host.DefaultBaseClass));
			}
			context.GeneratedClass.Members.Add(new CodeConstructor
			{
				Attributes = MemberAttributes.Public
			});
		}

		// Token: 0x04000116 RID: 278
		private CodeGeneratorContext _context;
	}
}
