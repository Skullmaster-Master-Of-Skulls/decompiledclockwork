using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Razor.Editor;
using System.Web.Razor.Generator;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x0200004B RID: 75
	public class SpanBuilder
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public SpanBuilder(Span original)
		{
			this.Kind = original.Kind;
			this._symbols = new List<ISymbol>(original.Symbols);
			this.EditHandler = original.EditHandler;
			this.CodeGenerator = original.CodeGenerator;
			this.Start = original.Start;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000DF26 File Offset: 0x0000C126
		public SpanBuilder()
		{
			this.Reset();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000DF4A File Offset: 0x0000C14A
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000DF52 File Offset: 0x0000C152
		public SourceLocation Start { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000DF5B File Offset: 0x0000C15B
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000DF63 File Offset: 0x0000C163
		public SpanKind Kind { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public ReadOnlyCollection<ISymbol> Symbols
		{
			get
			{
				return new ReadOnlyCollection<ISymbol>(this._symbols);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000DF79 File Offset: 0x0000C179
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000DF81 File Offset: 0x0000C181
		public SpanEditHandler EditHandler { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000DF8A File Offset: 0x0000C18A
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000DF92 File Offset: 0x0000C192
		public ISpanCodeGenerator CodeGenerator { get; set; }

		// Token: 0x06000364 RID: 868 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public void Reset()
		{
			this._symbols = new List<ISymbol>();
			this.EditHandler = SpanEditHandler.CreateDefault((string s) => Enumerable.Empty<ISymbol>());
			this.CodeGenerator = SpanCodeGenerator.Null;
			this.Start = SourceLocation.Zero;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000DFFA File Offset: 0x0000C1FA
		public Span Build()
		{
			return new Span(this);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000E002 File Offset: 0x0000C202
		public void ClearSymbols()
		{
			this._symbols.Clear();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000E010 File Offset: 0x0000C210
		public void Accept(ISymbol symbol)
		{
			if (symbol == null)
			{
				return;
			}
			if (this._symbols.Count == 0)
			{
				this.Start = symbol.Start;
				symbol.ChangeStart(SourceLocation.Zero);
				this._tracker.CurrentLocation = SourceLocation.Zero;
			}
			else
			{
				symbol.ChangeStart(this._tracker.CurrentLocation);
			}
			this._symbols.Add(symbol);
			this._tracker.UpdateLocation(symbol.Content);
		}

		// Token: 0x040000E8 RID: 232
		private IList<ISymbol> _symbols = new List<ISymbol>();

		// Token: 0x040000E9 RID: 233
		private SourceLocationTracker _tracker = new SourceLocationTracker();
	}
}
