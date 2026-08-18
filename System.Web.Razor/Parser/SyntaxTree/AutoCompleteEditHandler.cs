using System;
using System.Collections.Generic;
using System.Web.Razor.Editor;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x02000006 RID: 6
	public class AutoCompleteEditHandler : SpanEditHandler
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002791 File Offset: 0x00000991
		public AutoCompleteEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer) : base(tokenizer)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000279A File Offset: 0x0000099A
		public AutoCompleteEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer, AcceptedCharacters accepted) : base(tokenizer, accepted)
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000027A4 File Offset: 0x000009A4
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000027AC File Offset: 0x000009AC
		public bool AutoCompleteAtEndOfSpan { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027B5 File Offset: 0x000009B5
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000027BD File Offset: 0x000009BD
		public string AutoCompleteString { get; set; }

		// Token: 0x0600003C RID: 60 RVA: 0x000027C6 File Offset: 0x000009C6
		protected override PartialParseResult CanAcceptChange(Span target, TextChange normalizedChange)
		{
			if (((this.AutoCompleteAtEndOfSpan && SpanEditHandler.IsAtEndOfSpan(target, normalizedChange)) || SpanEditHandler.IsAtEndOfFirstLine(target, normalizedChange)) && normalizedChange.IsInsert && ParserHelpers.IsNewLine(normalizedChange.NewText) && this.AutoCompleteString != null)
			{
				return PartialParseResult.Rejected | PartialParseResult.AutoCompleteBlock;
			}
			return PartialParseResult.Rejected;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002808 File Offset: 0x00000A08
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				",AutoComplete:[",
				this.AutoCompleteString ?? "<null>",
				"]",
				this.AutoCompleteAtEndOfSpan ? ";AtEnd" : ";AtEOL"
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002864 File Offset: 0x00000A64
		public override bool Equals(object obj)
		{
			AutoCompleteEditHandler autoCompleteEditHandler = obj as AutoCompleteEditHandler;
			return base.Equals(obj) && autoCompleteEditHandler != null && string.Equals(autoCompleteEditHandler.AutoCompleteString, this.AutoCompleteString, StringComparison.Ordinal) && this.AutoCompleteAtEndOfSpan == autoCompleteEditHandler.AutoCompleteAtEndOfSpan;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028A8 File Offset: 0x00000AA8
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(base.GetHashCode()).Add(this.AutoCompleteString).CombinedHash;
		}
	}
}
