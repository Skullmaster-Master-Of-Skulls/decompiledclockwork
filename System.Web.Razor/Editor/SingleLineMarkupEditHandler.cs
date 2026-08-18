using System;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Editor
{
	// Token: 0x0200001A RID: 26
	public class SingleLineMarkupEditHandler : SpanEditHandler
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public SingleLineMarkupEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer) : base(tokenizer)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003CAD File Offset: 0x00001EAD
		public SingleLineMarkupEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer, AcceptedCharacters accepted) : base(tokenizer, accepted)
		{
		}
	}
}
