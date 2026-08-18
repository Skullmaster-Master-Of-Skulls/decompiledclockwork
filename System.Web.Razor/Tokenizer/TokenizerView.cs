using System;
using System.Globalization;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000071 RID: 113
	public class TokenizerView<TTokenizer, TSymbol, TSymbolType> where TTokenizer : Tokenizer<TSymbol, TSymbolType> where TSymbol : SymbolBase<TSymbolType>
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x00012A62 File Offset: 0x00010C62
		public TokenizerView(TTokenizer tokenizer)
		{
			this.Tokenizer = tokenizer;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00012A71 File Offset: 0x00010C71
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00012A79 File Offset: 0x00010C79
		public TTokenizer Tokenizer { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012A82 File Offset: 0x00010C82
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00012A8A File Offset: 0x00010C8A
		public bool EndOfFile { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00012A93 File Offset: 0x00010C93
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00012A9B File Offset: 0x00010C9B
		public TSymbol Current { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00012AA4 File Offset: 0x00010CA4
		public ITextDocument Source
		{
			get
			{
				TTokenizer tokenizer = this.Tokenizer;
				return tokenizer.Source;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012AC8 File Offset: 0x00010CC8
		public bool Next()
		{
			TTokenizer tokenizer = this.Tokenizer;
			this.Current = tokenizer.NextSymbol();
			this.EndOfFile = (this.Current == null);
			return !this.EndOfFile;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00012B0C File Offset: 0x00010D0C
		public void PutBack(TSymbol symbol)
		{
			if (this.Source.Position != symbol.Start.AbsoluteIndex + symbol.Content.Length)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, RazorResources.TokenizerView_CannotPutBack, new object[]
				{
					symbol.Start.AbsoluteIndex + symbol.Content.Length,
					this.Source.Position
				}));
			}
			this.Source.Position -= symbol.Content.Length;
			this.Current = default(TSymbol);
			this.EndOfFile = (this.Source.Position >= this.Source.Length);
			TTokenizer tokenizer = this.Tokenizer;
			tokenizer.Reset();
		}
	}
}
