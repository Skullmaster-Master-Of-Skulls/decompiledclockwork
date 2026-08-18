using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200004F RID: 79
	internal class WhiteSpaceRewriter : MarkupRewriter
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00010990 File Offset: 0x0000EB90
		public WhiteSpaceRewriter(Action<SpanBuilder, SourceLocation, string> markupSpanFactory) : base(markupSpanFactory)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010999 File Offset: 0x0000EB99
		protected override bool CanRewrite(Block block)
		{
			return block.Type == BlockType.Expression && base.Parent != null;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000109B4 File Offset: 0x0000EBB4
		protected override SyntaxTreeNode RewriteBlock(BlockBuilder parent, Block block)
		{
			BlockBuilder blockBuilder = new BlockBuilder(block);
			blockBuilder.Children.Clear();
			Span span = block.Children.FirstOrDefault<SyntaxTreeNode>() as Span;
			IEnumerable<SyntaxTreeNode> enumerable = block.Children;
			if (span.Content.All(new Func<char, bool>(char.IsWhiteSpace)))
			{
				SpanBuilder spanBuilder = new SpanBuilder(span);
				spanBuilder.ClearSymbols();
				base.FillSpan(spanBuilder, span.Start, span.Content);
				parent.Children.Add(spanBuilder.Build());
				enumerable = block.Children.Skip(1);
			}
			foreach (SyntaxTreeNode item in enumerable)
			{
				blockBuilder.Children.Add(item);
			}
			return blockBuilder.Build();
		}
	}
}
