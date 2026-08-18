using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200002B RID: 43
	public class ResolveUrlCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006364 File Offset: 0x00004564
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			if (string.IsNullOrEmpty(context.Host.GeneratedClassContext.ResolveUrlMethodName))
			{
				new MarkupCodeGenerator().GenerateCode(target, context);
				return;
			}
			if (!context.Host.DesignTimeMode && string.IsNullOrEmpty(target.Content))
			{
				return;
			}
			if (context.Host.EnableInstrumentation && context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
			{
				context.AddContextCall(target, context.Host.GeneratedClassContext.BeginContextMethodName, false);
			}
			if (!string.IsNullOrEmpty(target.Content) && !context.Host.DesignTimeMode)
			{
				string text = context.BuildCodeString(delegate(CodeWriter cw)
				{
					if (context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
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
					}
					cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.ResolveUrlMethodName);
					cw.WriteStringLiteral(target.Content);
					cw.WriteEndMethodInvoke();
					if (context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
					{
						cw.WriteEndMethodInvoke();
						cw.WriteEndStatement();
						return;
					}
					cw.WriteLineContinuation();
				});
				if (context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
				{
					context.AddStatement(text);
				}
				else
				{
					context.BufferStatementFragment(text);
				}
			}
			if (context.Host.EnableInstrumentation && context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
			{
				context.AddContextCall(target, context.Host.GeneratedClassContext.EndContextMethodName, false);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000064DE File Offset: 0x000046DE
		public override string ToString()
		{
			return "VirtualPath";
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000064E5 File Offset: 0x000046E5
		public override bool Equals(object obj)
		{
			return obj is ResolveUrlCodeGenerator;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000064F0 File Offset: 0x000046F0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
