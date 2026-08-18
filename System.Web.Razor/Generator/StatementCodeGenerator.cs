using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000030 RID: 48
	public class StatementCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00006C98 File Offset: 0x00004E98
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			context.FlushBufferedStatement();
			string text = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteSnippet(target.Content);
			});
			int characterIndex = target.Start.CharacterIndex;
			int generatedCodeStart;
			text = CodeGeneratorPaddingHelper.PadStatement(context.Host, text, target, ref characterIndex, out generatedCodeStart);
			context.AddStatement(text, context.GenerateLinePragma(target, generatedCodeStart));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006D0B File Offset: 0x00004F0B
		public override string ToString()
		{
			return "Stmt";
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006D12 File Offset: 0x00004F12
		public override bool Equals(object obj)
		{
			return obj is StatementCodeGenerator;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006D1D File Offset: 0x00004F1D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
