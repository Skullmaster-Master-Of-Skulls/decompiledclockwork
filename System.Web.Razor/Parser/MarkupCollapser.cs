using System;
using System.Linq;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000042 RID: 66
	internal class MarkupCollapser : MarkupRewriter
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000D910 File Offset: 0x0000BB10
		public MarkupCollapser(Action<SpanBuilder, SourceLocation, string> markupSpanFactory) : base(markupSpanFactory)
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D919 File Offset: 0x0000BB19
		protected override bool CanRewrite(Span span)
		{
			return span.Kind == SpanKind.Markup && span.CodeGenerator is MarkupCodeGenerator;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000D934 File Offset: 0x0000BB34
		protected override SyntaxTreeNode RewriteSpan(BlockBuilder parent, Span span)
		{
			Span span2 = parent.Children.LastOrDefault<SyntaxTreeNode>() as Span;
			if (span2 == null || !this.CanRewrite(span2))
			{
				return span;
			}
			parent.Children.Remove(span2);
			SpanBuilder spanBuilder = new SpanBuilder();
			base.FillSpan(spanBuilder, span2.Start, span2.Content + span.Content);
			return spanBuilder.Build();
		}
	}
}
