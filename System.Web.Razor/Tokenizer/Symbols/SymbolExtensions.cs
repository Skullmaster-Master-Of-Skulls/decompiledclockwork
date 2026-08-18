using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Tokenizer.Symbols
{
	// Token: 0x0200006F RID: 111
	public static class SymbolExtensions
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x000129A7 File Offset: 0x00010BA7
		public static LocationTagged<string> GetContent(this SpanBuilder span)
		{
			return span.GetContent((IEnumerable<ISymbol> e) => e);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000129CC File Offset: 0x00010BCC
		public static LocationTagged<string> GetContent(this SpanBuilder span, Func<IEnumerable<ISymbol>, IEnumerable<ISymbol>> filter)
		{
			return filter(span.Symbols).GetContent(span.Start);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000129F0 File Offset: 0x00010BF0
		public static LocationTagged<string> GetContent(this IEnumerable<ISymbol> symbols, SourceLocation spanStart)
		{
			if (symbols.Any<ISymbol>())
			{
				return new LocationTagged<string>(string.Concat(from s in symbols
				select s.Content), spanStart + symbols.First<ISymbol>().Start);
			}
			return new LocationTagged<string>(string.Empty, spanStart);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00012A4F File Offset: 0x00010C4F
		public static LocationTagged<string> GetContent(this ISymbol symbol)
		{
			return new LocationTagged<string>(symbol.Content, symbol.Start);
		}
	}
}
