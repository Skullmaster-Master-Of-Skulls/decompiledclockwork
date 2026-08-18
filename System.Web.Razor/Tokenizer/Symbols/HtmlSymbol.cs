using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Tokenizer.Symbols
{
	// Token: 0x02000080 RID: 128
	public class HtmlSymbol : SymbolBase<HtmlSymbolType>
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00015784 File Offset: 0x00013984
		public HtmlSymbol(int offset, int line, int column, string content, HtmlSymbolType type) : this(new SourceLocation(offset, line, column), content, type, Enumerable.Empty<RazorError>())
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001579D File Offset: 0x0001399D
		public HtmlSymbol(SourceLocation start, string content, HtmlSymbolType type) : base(start, content, type, Enumerable.Empty<RazorError>())
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000157AD File Offset: 0x000139AD
		public HtmlSymbol(int offset, int line, int column, string content, HtmlSymbolType type, IEnumerable<RazorError> errors) : base(new SourceLocation(offset, line, column), content, type, errors)
		{
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000157C3 File Offset: 0x000139C3
		public HtmlSymbol(SourceLocation start, string content, HtmlSymbolType type, IEnumerable<RazorError> errors) : base(start, content, type, errors)
		{
		}
	}
}
