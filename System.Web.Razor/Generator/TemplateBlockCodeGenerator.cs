using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000031 RID: 49
	public class TemplateBlockCodeGenerator : BlockCodeGenerator
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00006D90 File Offset: 0x00004F90
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			string fragment = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteStartLambdaExpression(new string[]
				{
					"item"
				});
				cw.WriteStartConstructor(context.Host.GeneratedClassContext.TemplateTypeName);
				cw.WriteStartLambdaDelegate(new string[]
				{
					"__razor_template_writer"
				});
			});
			context.MarkEndOfGeneratedCode();
			context.BufferStatementFragment(fragment);
			context.FlushBufferedStatement();
			this._oldTargetWriter = context.TargetWriterName;
			context.TargetWriterName = "__razor_template_writer";
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006E1C File Offset: 0x0000501C
		public override void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
			string fragment = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteEndLambdaDelegate();
				cw.WriteEndConstructor();
				cw.WriteEndLambdaExpression();
			});
			context.BufferStatementFragment(fragment);
			context.TargetWriterName = this._oldTargetWriter;
		}

		// Token: 0x04000085 RID: 133
		private const string TemplateWriterName = "__razor_template_writer";

		// Token: 0x04000086 RID: 134
		private const string ItemParameterName = "item";

		// Token: 0x04000087 RID: 135
		private string _oldTargetWriter;
	}
}
