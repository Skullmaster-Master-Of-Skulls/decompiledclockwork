using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;

namespace System.Web.Razor.Tokenizer.Symbols
{
	// Token: 0x0200007E RID: 126
	public class CSharpSymbol : SymbolBase<CSharpSymbolType>
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0001569B File Offset: 0x0001389B
		public CSharpSymbol(int offset, int line, int column, string content, CSharpSymbolType type) : this(new SourceLocation(offset, line, column), content, type, Enumerable.Empty<RazorError>())
		{
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000156B4 File Offset: 0x000138B4
		public CSharpSymbol(SourceLocation start, string content, CSharpSymbolType type) : this(start, content, type, Enumerable.Empty<RazorError>())
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000156C4 File Offset: 0x000138C4
		public CSharpSymbol(int offset, int line, int column, string content, CSharpSymbolType type, IEnumerable<RazorError> errors) : base(new SourceLocation(offset, line, column), content, type, errors)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000156DA File Offset: 0x000138DA
		public CSharpSymbol(SourceLocation start, string content, CSharpSymbolType type, IEnumerable<RazorError> errors) : base(start, content, type, errors)
		{
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000156E7 File Offset: 0x000138E7
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x000156EF File Offset: 0x000138EF
		public bool? EscapedIdentifier { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000156F8 File Offset: 0x000138F8
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00015700 File Offset: 0x00013900
		public CSharpKeyword? Keyword { get; set; }

		// Token: 0x06000574 RID: 1396 RVA: 0x0001570C File Offset: 0x0001390C
		public override bool Equals(object obj)
		{
			CSharpSymbol csharpSymbol = obj as CSharpSymbol;
			return base.Equals(obj) && csharpSymbol.Keyword == this.Keyword;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001575C File Offset: 0x0001395C
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Keyword.GetHashCode();
		}
	}
}
