using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Utils;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200008D RID: 141
	public class ParserContext
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00016C34 File Offset: 0x00014E34
		public ParserContext(ITextDocument source, ParserBase codeParser, ParserBase markupParser, ParserBase activeParser)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (codeParser == null)
			{
				throw new ArgumentNullException("codeParser");
			}
			if (markupParser == null)
			{
				throw new ArgumentNullException("markupParser");
			}
			if (activeParser == null)
			{
				throw new ArgumentNullException("activeParser");
			}
			if (activeParser != codeParser && activeParser != markupParser)
			{
				throw new ArgumentException(RazorResources.ActiveParser_Must_Be_Code_Or_Markup_Parser, "activeParser");
			}
			this.Source = new TextDocumentReader(source);
			this.CodeParser = codeParser;
			this.MarkupParser = markupParser;
			this.ActiveParser = activeParser;
			this.Errors = new List<RazorError>();
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00016CD2 File Offset: 0x00014ED2
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00016CDA File Offset: 0x00014EDA
		public IList<RazorError> Errors { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00016CE3 File Offset: 0x00014EE3
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00016CEB File Offset: 0x00014EEB
		public TextDocumentReader Source { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00016CF4 File Offset: 0x00014EF4
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00016CFC File Offset: 0x00014EFC
		public ParserBase CodeParser { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00016D05 File Offset: 0x00014F05
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00016D0D File Offset: 0x00014F0D
		public ParserBase MarkupParser { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00016D16 File Offset: 0x00014F16
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00016D1E File Offset: 0x00014F1E
		public ParserBase ActiveParser { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00016D27 File Offset: 0x00014F27
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00016D2F File Offset: 0x00014F2F
		public bool DesignTimeMode { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00016D38 File Offset: 0x00014F38
		public BlockBuilder CurrentBlock
		{
			get
			{
				return this._blockStack.Peek();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00016D45 File Offset: 0x00014F45
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00016D4D File Offset: 0x00014F4D
		public Span LastSpan { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00016D56 File Offset: 0x00014F56
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00016D5E File Offset: 0x00014F5E
		public bool WhiteSpaceIsSignificantToAncestorBlock { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00016D67 File Offset: 0x00014F67
		public AcceptedCharacters LastAcceptedCharacters
		{
			get
			{
				if (this.LastSpan == null)
				{
					return AcceptedCharacters.None;
				}
				return this.LastSpan.EditHandler.AcceptedCharacters;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00016D83 File Offset: 0x00014F83
		internal Stack<BlockBuilder> BlockStack
		{
			get
			{
				return this._blockStack;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00016D8C File Offset: 0x00014F8C
		public char CurrentCharacter
		{
			get
			{
				if (this._terminated)
				{
					return '\0';
				}
				int num = this.Source.Peek();
				if (num == -1)
				{
					return '\0';
				}
				return (char)num;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00016DB7 File Offset: 0x00014FB7
		public bool EndOfFile
		{
			get
			{
				return this._terminated || this.Source.Peek() == -1;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00016DD1 File Offset: 0x00014FD1
		public void AddSpan(Span span)
		{
			this.EnusreNotTerminated();
			if (this._blockStack.Count == 0)
			{
				throw new InvalidOperationException(RazorResources.ParserContext_NoCurrentBlock);
			}
			this._blockStack.Peek().Children.Add(span);
			this.LastSpan = span;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016E10 File Offset: 0x00015010
		public IDisposable StartBlock(BlockType blockType)
		{
			this.EnusreNotTerminated();
			this._blockStack.Push(new BlockBuilder
			{
				Type = new BlockType?(blockType)
			});
			return new DisposableAction(new Action(this.EndBlock));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016E52 File Offset: 0x00015052
		public IDisposable StartBlock()
		{
			this.EnusreNotTerminated();
			this._blockStack.Push(new BlockBuilder());
			return new DisposableAction(new Action(this.EndBlock));
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00016E7C File Offset: 0x0001507C
		public void EndBlock()
		{
			this.EnusreNotTerminated();
			if (this._blockStack.Count == 0)
			{
				throw new InvalidOperationException(RazorResources.EndBlock_Called_Without_Matching_StartBlock);
			}
			if (this._blockStack.Count > 1)
			{
				BlockBuilder blockBuilder = this._blockStack.Pop();
				this._blockStack.Peek().Children.Add(blockBuilder.Build());
				return;
			}
			this._terminated = true;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00016F1C File Offset: 0x0001511C
		public bool IsWithin(BlockType type)
		{
			return this._blockStack.Any((BlockBuilder b) => b.Type == type);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00016F4D File Offset: 0x0001514D
		public void SwitchActiveParser()
		{
			this.EnusreNotTerminated();
			if (object.ReferenceEquals(this.ActiveParser, this.CodeParser))
			{
				this.ActiveParser = this.MarkupParser;
				return;
			}
			this.ActiveParser = this.CodeParser;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00016F81 File Offset: 0x00015181
		public void OnError(SourceLocation location, string message)
		{
			this.EnusreNotTerminated();
			this.Errors.Add(new RazorError(message, location));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00016F9B File Offset: 0x0001519B
		public void OnError(SourceLocation location, string message, params object[] args)
		{
			this.EnusreNotTerminated();
			this.OnError(location, string.Format(CultureInfo.CurrentCulture, message, args));
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00016FB8 File Offset: 0x000151B8
		public ParserResults CompleteParse()
		{
			if (this._blockStack.Count == 0)
			{
				throw new InvalidOperationException(RazorResources.ParserContext_CannotCompleteTree_NoRootBlock);
			}
			if (this._blockStack.Count != 1)
			{
				throw new InvalidOperationException(RazorResources.ParserContext_CannotCompleteTree_OutstandingBlocks);
			}
			return new ParserResults(this._blockStack.Pop().Build(), this.Errors);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00017014 File Offset: 0x00015214
		[Conditional("DEBUG")]
		internal void CaptureOwnerTask()
		{
			if (Task.CurrentId != null)
			{
				this._ownerTaskId = Task.CurrentId;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001703B File Offset: 0x0001523B
		[Conditional("DEBUG")]
		internal void AssertOnOwnerTask()
		{
			bool flag = this._ownerTaskId != null;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00017049 File Offset: 0x00015249
		[Conditional("DEBUG")]
		internal void AssertCurrent(char expected)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001704B File Offset: 0x0001524B
		private void EnusreNotTerminated()
		{
			if (this._terminated)
			{
				throw new InvalidOperationException(RazorResources.ParserContext_ParseComplete);
			}
		}

		// Token: 0x04000318 RID: 792
		private int? _ownerTaskId;

		// Token: 0x04000319 RID: 793
		private bool _terminated;

		// Token: 0x0400031A RID: 794
		private Stack<BlockBuilder> _blockStack = new Stack<BlockBuilder>();
	}
}
