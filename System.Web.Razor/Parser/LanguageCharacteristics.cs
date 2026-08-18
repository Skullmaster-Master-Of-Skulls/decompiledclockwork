using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200003E RID: 62
	public abstract class LanguageCharacteristics<TTokenizer, TSymbol, TSymbolType> where TTokenizer : Tokenizer<TSymbol, TSymbolType> where TSymbol : SymbolBase<TSymbolType>
	{
		// Token: 0x060002D0 RID: 720
		public abstract string GetSample(TSymbolType type);

		// Token: 0x060002D1 RID: 721
		public abstract TTokenizer CreateTokenizer(ITextDocument source);

		// Token: 0x060002D2 RID: 722
		public abstract TSymbolType FlipBracket(TSymbolType bracket);

		// Token: 0x060002D3 RID: 723
		public abstract TSymbol CreateMarkerSymbol(SourceLocation location);

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B444 File Offset: 0x00009644
		public virtual IEnumerable<TSymbol> TokenizeString(string content)
		{
			return this.TokenizeString(SourceLocation.Zero, content);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000B628 File Offset: 0x00009828
		public virtual IEnumerable<TSymbol> TokenizeString(SourceLocation start, string input)
		{
			using (SeekableTextReader reader = new SeekableTextReader(input))
			{
				TTokenizer tok = this.CreateTokenizer(reader);
				TSymbol sym;
				while ((sym = tok.NextSymbol()) != null)
				{
					sym.OffsetStart(start);
					yield return sym;
				}
			}
			yield break;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000B653 File Offset: 0x00009853
		public virtual bool IsWhiteSpace(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.WhiteSpace);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B65D File Offset: 0x0000985D
		public virtual bool IsNewLine(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.NewLine);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B667 File Offset: 0x00009867
		public virtual bool IsIdentifier(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.Identifier);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B671 File Offset: 0x00009871
		public virtual bool IsKeyword(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.Keyword);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B67B File Offset: 0x0000987B
		public virtual bool IsTransition(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.Transition);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B685 File Offset: 0x00009885
		public virtual bool IsCommentStart(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.CommentStart);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B68F File Offset: 0x0000988F
		public virtual bool IsCommentStar(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.CommentStar);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B699 File Offset: 0x00009899
		public virtual bool IsCommentBody(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.CommentBody);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B6A3 File Offset: 0x000098A3
		public virtual bool IsUnknown(TSymbol symbol)
		{
			return this.IsKnownSymbolType(symbol, KnownSymbolType.Unknown);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B6AD File Offset: 0x000098AD
		public virtual bool IsKnownSymbolType(TSymbol symbol, KnownSymbolType type)
		{
			return symbol != null && object.Equals(symbol.Type, this.GetKnownSymbolType(type));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B6DC File Offset: 0x000098DC
		public virtual Tuple<TSymbol, TSymbol> SplitSymbol(TSymbol symbol, int splitAt, TSymbolType leftType)
		{
			TSymbol item = this.CreateSymbol(symbol.Start, symbol.Content.Substring(0, splitAt), leftType, Enumerable.Empty<RazorError>());
			TSymbol item2 = default(TSymbol);
			if (splitAt < symbol.Content.Length)
			{
				item2 = this.CreateSymbol(SourceLocationTracker.CalculateNewLocation(symbol.Start, item.Content), symbol.Content.Substring(splitAt), symbol.Type, symbol.Errors);
			}
			return Tuple.Create<TSymbol, TSymbol>(item, item2);
		}

		// Token: 0x060002E1 RID: 737
		public abstract TSymbolType GetKnownSymbolType(KnownSymbolType type);

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B78E File Offset: 0x0000998E
		public virtual bool KnowsSymbolType(KnownSymbolType type)
		{
			return type == KnownSymbolType.Unknown || !object.Equals(this.GetKnownSymbolType(type), this.GetKnownSymbolType(KnownSymbolType.Unknown));
		}

		// Token: 0x060002E3 RID: 739
		protected abstract TSymbol CreateSymbol(SourceLocation location, string content, TSymbolType type, IEnumerable<RazorError> errors);
	}
}
