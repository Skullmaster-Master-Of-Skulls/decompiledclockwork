using System;
using System.CodeDom;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000032 RID: 50
	public class TypeMemberCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00006E84 File Offset: 0x00005084
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			string code = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteSnippet(target.Content);
			});
			int generatedCodeStart;
			string text = CodeGeneratorPaddingHelper.Pad(context.Host, code, target, out generatedCodeStart);
			context.GeneratedClass.Members.Add(new CodeSnippetTypeMember(text)
			{
				LinePragma = context.GenerateLinePragma(target, generatedCodeStart)
			});
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006EF7 File Offset: 0x000050F7
		public override string ToString()
		{
			return "TypeMember";
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006EFE File Offset: 0x000050FE
		public override bool Equals(object obj)
		{
			return obj is TypeMemberCodeGenerator;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006F09 File Offset: 0x00005109
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
