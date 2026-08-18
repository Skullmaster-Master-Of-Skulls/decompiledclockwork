using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor.Editor;
using System.Web.Razor.Generator;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x02000098 RID: 152
	public class Span : SyntaxTreeNode
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00018901 File Offset: 0x00016B01
		public Span(SpanBuilder builder)
		{
			this.ReplaceWith(builder);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00018910 File Offset: 0x00016B10
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00018918 File Offset: 0x00016B18
		public SpanKind Kind { get; protected set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00018921 File Offset: 0x00016B21
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x00018929 File Offset: 0x00016B29
		public IEnumerable<ISymbol> Symbols { get; protected set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00018932 File Offset: 0x00016B32
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001893A File Offset: 0x00016B3A
		public Span Previous { get; protected internal set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00018943 File Offset: 0x00016B43
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001894B File Offset: 0x00016B4B
		public Span Next { get; protected internal set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00018954 File Offset: 0x00016B54
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001895C File Offset: 0x00016B5C
		public SpanEditHandler EditHandler { get; protected set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00018965 File Offset: 0x00016B65
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0001896D File Offset: 0x00016B6D
		public ISpanCodeGenerator CodeGenerator { get; protected set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00018976 File Offset: 0x00016B76
		public override bool IsBlock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00018979 File Offset: 0x00016B79
		public override int Length
		{
			get
			{
				return this.Content.Length;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00018986 File Offset: 0x00016B86
		public override SourceLocation Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001898E File Offset: 0x00016B8E
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00018996 File Offset: 0x00016B96
		public string Content { get; private set; }

		// Token: 0x060006D8 RID: 1752 RVA: 0x000189A0 File Offset: 0x00016BA0
		public void Change(Action<SpanBuilder> changes)
		{
			SpanBuilder spanBuilder = new SpanBuilder(this);
			changes(spanBuilder);
			this.ReplaceWith(spanBuilder);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000189D8 File Offset: 0x00016BD8
		public void ReplaceWith(SpanBuilder builder)
		{
			this.Kind = builder.Kind;
			this.Symbols = builder.Symbols;
			this.EditHandler = builder.EditHandler;
			this.CodeGenerator = (builder.CodeGenerator ?? SpanCodeGenerator.Null);
			this._start = builder.Start;
			builder.Reset();
			this.Content = this.Symbols.Aggregate(new StringBuilder(), (StringBuilder sb, ISymbol sym) => sb.Append(sym.Content), (StringBuilder sb) => sb.ToString());
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00018A80 File Offset: 0x00016C80
		public override void Accept(ParserVisitor visitor)
		{
			visitor.VisitSpan(this);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00018AB4 File Offset: 0x00016CB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Kind);
			stringBuilder.AppendFormat(" Span at {0}::{1} - [{2}]", this.Start, this.Length, this.Content);
			stringBuilder.Append(" Edit: <");
			stringBuilder.Append(this.EditHandler.ToString());
			stringBuilder.Append(">");
			stringBuilder.Append(" Gen: <");
			stringBuilder.Append(this.CodeGenerator.ToString());
			stringBuilder.Append("> {");
			stringBuilder.Append(string.Join(";", from sym in this.Symbols
			group sym by sym.GetType() into grp
			select grp.Key.Name + ":" + grp.Count<ISymbol>()));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00018BC4 File Offset: 0x00016DC4
		public void ChangeStart(SourceLocation newStart)
		{
			this._start = newStart;
			Span span = this;
			SourceLocationTracker sourceLocationTracker = new SourceLocationTracker(newStart);
			sourceLocationTracker.UpdateLocation(this.Content);
			while ((span = span.Next) != null)
			{
				span._start = sourceLocationTracker.CurrentLocation;
				sourceLocationTracker.UpdateLocation(span.Content);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00018C13 File Offset: 0x00016E13
		internal void SetStart(SourceLocation newStart)
		{
			this._start = newStart;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00018C1C File Offset: 0x00016E1C
		public override bool EquivalentTo(SyntaxTreeNode node)
		{
			Span span = node as Span;
			return span != null && this.Kind.Equals(span.Kind) && this.Start.Equals(span.Start) && this.EditHandler.Equals(span.EditHandler) && string.Equals(span.Content, this.Content, StringComparison.Ordinal);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00018C90 File Offset: 0x00016E90
		public override bool Equals(object obj)
		{
			Span span = obj as Span;
			return span != null && this.Kind.Equals(span.Kind) && this.EditHandler.Equals(span.EditHandler) && this.CodeGenerator.Equals(span.CodeGenerator) && this.Symbols.SequenceEqual(span.Symbols);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00018CFD File Offset: 0x00016EFD
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add((int)this.Kind).Add(this.Start).Add(this.Content).CombinedHash;
		}

		// Token: 0x0400033F RID: 831
		private SourceLocation _start;
	}
}
