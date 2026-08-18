using System;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000028 RID: 40
	public class ExpressionCodeGenerator : HybridCodeGenerator
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00005A10 File Offset: 0x00003C10
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			if (context.Host.EnableInstrumentation && context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
			{
				Span span = (from s in target.Children.OfType<Span>()
				where s.Kind == SpanKind.Code || s.Kind == SpanKind.Markup
				select s).FirstOrDefault<Span>();
				if (span != null)
				{
					context.AddContextCall(span, context.Host.GeneratedClassContext.BeginContextMethodName, false);
				}
			}
			string fragment = context.BuildCodeString(delegate(CodeWriter cw)
			{
				if (context.Host.DesignTimeMode)
				{
					context.EnsureExpressionHelperVariable();
					cw.WriteStartAssignment("__o");
					return;
				}
				if (context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
				{
					if (!string.IsNullOrEmpty(context.TargetWriterName))
					{
						cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.WriteToMethodName);
						cw.WriteSnippet(context.TargetWriterName);
						cw.WriteParameterSeparator();
						return;
					}
					cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.WriteMethodName);
				}
			});
			context.BufferStatementFragment(fragment);
			context.MarkStartOfGeneratedCode();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005B28 File Offset: 0x00003D28
		public override void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
			string fragment = context.BuildCodeString(delegate(CodeWriter cw)
			{
				if (context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
				{
					if (!context.Host.DesignTimeMode)
					{
						cw.WriteEndMethodInvoke();
					}
					cw.WriteEndStatement();
					return;
				}
				cw.WriteLineContinuation();
			});
			context.MarkEndOfGeneratedCode();
			context.BufferStatementFragment(fragment);
			context.FlushBufferedStatement();
			if (context.Host.EnableInstrumentation && context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
			{
				Span span = (from s in target.Children.OfType<Span>()
				where s.Kind == SpanKind.Code || s.Kind == SpanKind.Markup
				select s).FirstOrDefault<Span>();
				if (span != null)
				{
					context.AddContextCall(span, context.Host.GeneratedClassContext.EndContextMethodName, false);
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			Span sourceSpan = null;
			if (context.CreateCodeWriter().SupportsMidStatementLinePragmas || context.ExpressionRenderingMode == ExpressionRenderingMode.WriteToOutput)
			{
				sourceSpan = target;
			}
			context.BufferStatementFragment(target.Content, sourceSpan);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005C2B File Offset: 0x00003E2B
		public override string ToString()
		{
			return "Expr";
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005C32 File Offset: 0x00003E32
		public override bool Equals(object obj)
		{
			return obj is ExpressionCodeGenerator;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005C3D File Offset: 0x00003E3D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
