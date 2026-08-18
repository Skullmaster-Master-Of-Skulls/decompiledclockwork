using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000018 RID: 24
	public class RazorCommentCodeGenerator : BlockCodeGenerator
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003B74 File Offset: 0x00001D74
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			if (!string.IsNullOrEmpty(context.CurrentBufferedStatement))
			{
				context.MarkEndOfGeneratedCode();
				context.BufferStatementFragment(context.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteLineContinuation();
				}));
			}
			context.FlushBufferedStatement();
		}
	}
}
