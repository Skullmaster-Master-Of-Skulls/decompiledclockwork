using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200008E RID: 142
	public class RazorParser
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00017060 File Offset: 0x00015260
		public RazorParser(ParserBase codeParser, ParserBase markupParser)
		{
			if (codeParser == null)
			{
				throw new ArgumentNullException("codeParser");
			}
			if (markupParser == null)
			{
				throw new ArgumentNullException("markupParser");
			}
			this.MarkupParser = markupParser;
			this.CodeParser = codeParser;
			this.Optimizers = new List<ISyntaxTreeRewriter>
			{
				new WhiteSpaceRewriter(new Action<SpanBuilder, SourceLocation, string>(this.MarkupParser.BuildSpan)),
				new ConditionalAttributeCollapser(new Action<SpanBuilder, SourceLocation, string>(this.MarkupParser.BuildSpan))
			};
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x000170E4 File Offset: 0x000152E4
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x000170EC File Offset: 0x000152EC
		internal ParserBase CodeParser { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x000170F5 File Offset: 0x000152F5
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x000170FD File Offset: 0x000152FD
		internal ParserBase MarkupParser { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00017106 File Offset: 0x00015306
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0001710E File Offset: 0x0001530E
		internal IList<ISyntaxTreeRewriter> Optimizers { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00017117 File Offset: 0x00015317
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001711F File Offset: 0x0001531F
		public bool DesignTimeMode { get; set; }

		// Token: 0x0600060B RID: 1547 RVA: 0x00017128 File Offset: 0x00015328
		public virtual void Parse(TextReader input, ParserVisitor visitor)
		{
			ParserResults result = this.ParseCore(new SeekableTextReader(input));
			visitor.Visit(result);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00017149 File Offset: 0x00015349
		public virtual ParserResults Parse(TextReader input)
		{
			return this.ParseCore(new SeekableTextReader(input));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00017157 File Offset: 0x00015357
		public virtual ParserResults Parse(ITextDocument input)
		{
			return this.ParseCore(input);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00017160 File Offset: 0x00015360
		[Obsolete("Lookahead-based readers have been deprecated, use overrides which accept a TextReader or ITextDocument instead")]
		public virtual void Parse(LookaheadTextReader input, ParserVisitor visitor)
		{
			ParserResults result = this.ParseCore(new SeekableTextReader(input));
			visitor.Visit(result);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00017181 File Offset: 0x00015381
		[Obsolete("Lookahead-based readers have been deprecated, use overrides which accept a TextReader or ITextDocument instead")]
		public virtual ParserResults Parse(LookaheadTextReader input)
		{
			return this.ParseCore(new SeekableTextReader(input));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001718F File Offset: 0x0001538F
		public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback)
		{
			return this.CreateParseTask(input, new CallbackVisitor(spanCallback, errorCallback));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000171A0 File Offset: 0x000153A0
		public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback, SynchronizationContext context)
		{
			return this.CreateParseTask(input, new CallbackVisitor(spanCallback, errorCallback)
			{
				SynchronizationContext = context
			});
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000171C8 File Offset: 0x000153C8
		public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback, CancellationToken cancelToken)
		{
			return this.CreateParseTask(input, new CallbackVisitor(spanCallback, errorCallback)
			{
				CancelToken = new CancellationToken?(cancelToken)
			});
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000171F4 File Offset: 0x000153F4
		public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback, SynchronizationContext context, CancellationToken cancelToken)
		{
			return this.CreateParseTask(input, new CallbackVisitor(spanCallback, errorCallback)
			{
				SynchronizationContext = context,
				CancelToken = new CancellationToken?(cancelToken)
			});
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001726C File Offset: 0x0001546C
		public virtual Task CreateParseTask(TextReader input, ParserVisitor consumer)
		{
			return new Task(delegate()
			{
				try
				{
					this.Parse(input, consumer);
				}
				catch (OperationCanceledException)
				{
				}
			});
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000172A8 File Offset: 0x000154A8
		private ParserResults ParseCore(ITextDocument input)
		{
			ParserContext parserContext = new ParserContext(input, this.CodeParser, this.MarkupParser, this.MarkupParser)
			{
				DesignTimeMode = this.DesignTimeMode
			};
			this.MarkupParser.Context = parserContext;
			this.CodeParser.Context = parserContext;
			this.MarkupParser.ParseDocument();
			ParserResults parserResults = parserContext.CompleteParse();
			Block block = parserResults.Document;
			foreach (ISyntaxTreeRewriter syntaxTreeRewriter in this.Optimizers)
			{
				block = syntaxTreeRewriter.Rewrite(block);
			}
			Span span = null;
			foreach (Span span2 in block.Flatten())
			{
				span2.Previous = span;
				if (span != null)
				{
					span.Next = span2;
				}
				span = span2;
			}
			return new ParserResults(block, parserResults.ParserErrors);
		}
	}
}
