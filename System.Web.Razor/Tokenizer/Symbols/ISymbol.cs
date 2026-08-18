using System;
using System.Web.Razor.Text;

namespace System.Web.Razor.Tokenizer.Symbols
{
	// Token: 0x0200006D RID: 109
	public interface ISymbol
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004D0 RID: 1232
		SourceLocation Start { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004D1 RID: 1233
		string Content { get; }

		// Token: 0x060004D2 RID: 1234
		void OffsetStart(SourceLocation documentStart);

		// Token: 0x060004D3 RID: 1235
		void ChangeStart(SourceLocation newStart);
	}
}
