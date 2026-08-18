using System;
using System.CodeDom;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200001E RID: 30
	public class AddImportCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004290 File Offset: 0x00002490
		public AddImportCodeGenerator(string ns, int namespaceKeywordLength)
		{
			this.Namespace = ns;
			this.NamespaceKeywordLength = namespaceKeywordLength;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000042A6 File Offset: 0x000024A6
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000042AE File Offset: 0x000024AE
		public string Namespace { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000042B7 File Offset: 0x000024B7
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000042BF File Offset: 0x000024BF
		public int NamespaceKeywordLength { get; set; }

		// Token: 0x060000DA RID: 218 RVA: 0x000042EC File Offset: 0x000024EC
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			string ns = this.Namespace;
			if (!string.IsNullOrEmpty(ns) && char.IsWhiteSpace(ns[0]))
			{
				ns = ns.Substring(1);
			}
			CodeNamespaceImport codeNamespaceImport = (from i in context.Namespace.Imports.OfType<CodeNamespaceImport>()
			where string.Equals(i.Namespace, ns.Trim(), StringComparison.Ordinal)
			select i).FirstOrDefault<CodeNamespaceImport>();
			if (codeNamespaceImport == null)
			{
				codeNamespaceImport = new CodeNamespaceImport(ns);
				context.Namespace.Imports.Add(codeNamespaceImport);
			}
			codeNamespaceImport.LinePragma = context.GenerateLinePragma(target);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004394 File Offset: 0x00002594
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Import:",
				this.Namespace,
				";KwdLen:",
				this.NamespaceKeywordLength
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000043D8 File Offset: 0x000025D8
		public override bool Equals(object obj)
		{
			AddImportCodeGenerator addImportCodeGenerator = obj as AddImportCodeGenerator;
			return addImportCodeGenerator != null && string.Equals(this.Namespace, addImportCodeGenerator.Namespace, StringComparison.Ordinal) && this.NamespaceKeywordLength == addImportCodeGenerator.NamespaceKeywordLength;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004413 File Offset: 0x00002613
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Namespace).Add(this.NamespaceKeywordLength).CombinedHash;
		}
	}
}
