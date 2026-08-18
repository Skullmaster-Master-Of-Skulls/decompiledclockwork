using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200002A RID: 42
	public class MarkupCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00006154 File Offset: 0x00004354
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			if (!context.Host.DesignTimeMode && string.IsNullOrEmpty(target.Content))
			{
				return;
			}
			if (context.Host.EnableInstrumentation)
			{
				context.AddContextCall(target, context.Host.GeneratedClassContext.BeginContextMethodName, true);
			}
			if (!string.IsNullOrEmpty(target.Content) && !context.Host.DesignTimeMode)
			{
				string generatedCode = context.BuildCodeString(delegate(CodeWriter cw)
				{
					if (!string.IsNullOrEmpty(context.TargetWriterName))
					{
						cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.WriteLiteralToMethodName);
						cw.WriteSnippet(context.TargetWriterName);
						cw.WriteParameterSeparator();
					}
					else
					{
						cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.WriteLiteralMethodName);
					}
					cw.WriteStringLiteral(target.Content);
					cw.WriteEndMethodInvoke();
					cw.WriteEndStatement();
				});
				context.AddStatement(generatedCode);
			}
			if (context.Host.EnableInstrumentation)
			{
				context.AddContextCall(target, context.Host.GeneratedClassContext.EndContextMethodName, true);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006262 File Offset: 0x00004462
		public override string ToString()
		{
			return "Markup";
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006269 File Offset: 0x00004469
		public override bool Equals(object obj)
		{
			return obj is MarkupCodeGenerator;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006274 File Offset: 0x00004474
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
