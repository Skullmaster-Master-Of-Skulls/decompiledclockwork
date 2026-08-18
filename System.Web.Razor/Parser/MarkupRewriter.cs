using System;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000038 RID: 56
	internal abstract class MarkupRewriter : ParserVisitor, ISyntaxTreeRewriter
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000767D File Offset: 0x0000587D
		protected MarkupRewriter(Action<SpanBuilder, SourceLocation, string> markupSpanFactory)
		{
			if (markupSpanFactory == null)
			{
				throw new ArgumentNullException("markupSpanFactory");
			}
			this._markupSpanFactory = markupSpanFactory;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000076A5 File Offset: 0x000058A5
		protected BlockBuilder Parent
		{
			get
			{
				if (this._blocks.Count <= 0)
				{
					return null;
				}
				return this._blocks.Peek();
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000076C2 File Offset: 0x000058C2
		public virtual Block Rewrite(Block input)
		{
			input.Accept(this);
			return this._blocks.Pop().Build();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000076DC File Offset: 0x000058DC
		public override void VisitBlock(Block block)
		{
			if (this.CanRewrite(block))
			{
				SyntaxTreeNode syntaxTreeNode = this.RewriteBlock(this._blocks.Peek(), block);
				if (syntaxTreeNode != null)
				{
					this._blocks.Peek().Children.Add(syntaxTreeNode);
					return;
				}
			}
			else
			{
				BlockBuilder blockBuilder = new BlockBuilder(block);
				blockBuilder.Children.Clear();
				this._blocks.Push(blockBuilder);
				base.VisitBlock(block);
				if (this._blocks.Count > 1)
				{
					this._blocks.Pop();
					this._blocks.Peek().Children.Add(blockBuilder.Build());
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000777C File Offset: 0x0000597C
		public override void VisitSpan(Span span)
		{
			if (this.CanRewrite(span))
			{
				SyntaxTreeNode syntaxTreeNode = this.RewriteSpan(this._blocks.Peek(), span);
				if (syntaxTreeNode != null)
				{
					this._blocks.Peek().Children.Add(syntaxTreeNode);
					return;
				}
			}
			else
			{
				this._blocks.Peek().Children.Add(span);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000077D5 File Offset: 0x000059D5
		protected virtual bool CanRewrite(Block block)
		{
			return false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000077D8 File Offset: 0x000059D8
		protected virtual bool CanRewrite(Span span)
		{
			return false;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000077DB File Offset: 0x000059DB
		protected virtual SyntaxTreeNode RewriteBlock(BlockBuilder parent, Block block)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000077E2 File Offset: 0x000059E2
		protected virtual SyntaxTreeNode RewriteSpan(BlockBuilder parent, Span span)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000077E9 File Offset: 0x000059E9
		protected void FillSpan(SpanBuilder builder, SourceLocation start, string content)
		{
			this._markupSpanFactory(builder, start, content);
		}

		// Token: 0x0400009A RID: 154
		private Stack<BlockBuilder> _blocks = new Stack<BlockBuilder>();

		// Token: 0x0400009B RID: 155
		private Action<SpanBuilder, SourceLocation, string> _markupSpanFactory;
	}
}
