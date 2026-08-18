using System;
using System.CodeDom;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000087 RID: 135
	public class CSharpRazorCodeGenerator : RazorCodeGenerator
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x00016444 File Offset: 0x00014644
		public CSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host) : base(className, rootNamespaceName, sourceFileName, host)
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00016458 File Offset: 0x00014658
		internal override Func<CodeWriter> CodeWriterFactory
		{
			get
			{
				return () => new CSharpCodeWriter();
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016477 File Offset: 0x00014677
		protected override void Initialize(CodeGeneratorContext context)
		{
			base.Initialize(context);
			context.GeneratedClass.Members.Insert(0, new CodeSnippetTypeMember("#line hidden"));
		}

		// Token: 0x040002FD RID: 765
		private const string HiddenLinePragma = "#line hidden";
	}
}
