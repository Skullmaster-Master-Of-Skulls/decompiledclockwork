using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Editor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000039 RID: 57
	internal class ConditionalAttributeCollapser : MarkupRewriter
	{
		// Token: 0x0600021D RID: 541 RVA: 0x000077F9 File Offset: 0x000059F9
		public ConditionalAttributeCollapser(Action<SpanBuilder, SourceLocation, string> markupSpanFactory) : base(markupSpanFactory)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007804 File Offset: 0x00005A04
		protected override bool CanRewrite(Block block)
		{
			AttributeBlockCodeGenerator attributeBlockCodeGenerator = block.CodeGenerator as AttributeBlockCodeGenerator;
			return attributeBlockCodeGenerator != null && block.Children.Any<SyntaxTreeNode>() && block.Children.All(new Func<SyntaxTreeNode, bool>(this.IsLiteralAttributeValue));
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007850 File Offset: 0x00005A50
		protected override SyntaxTreeNode RewriteBlock(BlockBuilder parent, Block block)
		{
			string content = string.Concat(from Span s in block.Children
			select s.Content);
			SpanBuilder spanBuilder = new SpanBuilder();
			spanBuilder.EditHandler = new SpanEditHandler(new Func<string, IEnumerable<ISymbol>>(HtmlTokenizer.Tokenize));
			base.FillSpan(spanBuilder, block.Children.Cast<Span>().First<Span>().Start, content);
			return spanBuilder.Build();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000078D0 File Offset: 0x00005AD0
		private bool IsLiteralAttributeValue(SyntaxTreeNode node)
		{
			if (node.IsBlock)
			{
				return false;
			}
			Span span = node as Span;
			LiteralAttributeCodeGenerator literalAttributeCodeGenerator = span.CodeGenerator as LiteralAttributeCodeGenerator;
			return span != null && ((literalAttributeCodeGenerator != null && literalAttributeCodeGenerator.ValueGenerator == null) || span.CodeGenerator == SpanCodeGenerator.Null || span.CodeGenerator is MarkupCodeGenerator);
		}
	}
}
