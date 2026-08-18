using System;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200004E RID: 78
	public class VBLanguageCharacteristics : LanguageCharacteristics<VBTokenizer, VBSymbol, VBSymbolType>
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x000108AA File Offset: 0x0000EAAA
		private VBLanguageCharacteristics()
		{
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000108B2 File Offset: 0x0000EAB2
		public static VBLanguageCharacteristics Instance
		{
			get
			{
				return VBLanguageCharacteristics._instance;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000108B9 File Offset: 0x0000EAB9
		public override VBTokenizer CreateTokenizer(ITextDocument source)
		{
			return new VBTokenizer(source);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000108C1 File Offset: 0x0000EAC1
		public override string GetSample(VBSymbolType type)
		{
			return VBSymbol.GetSample(type);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000108CC File Offset: 0x0000EACC
		public override VBSymbolType FlipBracket(VBSymbolType bracket)
		{
			switch (bracket)
			{
			case VBSymbolType.LeftParenthesis:
				return VBSymbolType.RightParenthesis;
			case VBSymbolType.RightBrace:
				return VBSymbolType.LeftBrace;
			case VBSymbolType.LeftBrace:
				return VBSymbolType.RightBrace;
			case VBSymbolType.RightParenthesis:
				return VBSymbolType.LeftParenthesis;
			default:
				switch (bracket)
				{
				case VBSymbolType.RightBracket:
					return VBSymbolType.LeftBracket;
				case VBSymbolType.LeftBracket:
					return VBSymbolType.RightBracket;
				default:
					return VBSymbolType.Unknown;
				}
				break;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001091A File Offset: 0x0000EB1A
		public override VBSymbol CreateMarkerSymbol(SourceLocation location)
		{
			return new VBSymbol(location, string.Empty, VBSymbolType.Unknown);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010928 File Offset: 0x0000EB28
		public override VBSymbolType GetKnownSymbolType(KnownSymbolType type)
		{
			switch (type)
			{
			case KnownSymbolType.WhiteSpace:
				return VBSymbolType.WhiteSpace;
			case KnownSymbolType.NewLine:
				return VBSymbolType.NewLine;
			case KnownSymbolType.Identifier:
				return VBSymbolType.Identifier;
			case KnownSymbolType.Keyword:
				return VBSymbolType.Keyword;
			case KnownSymbolType.Transition:
				return VBSymbolType.Transition;
			case KnownSymbolType.CommentStart:
				return VBSymbolType.RazorCommentTransition;
			case KnownSymbolType.CommentStar:
				return VBSymbolType.RazorCommentStar;
			case KnownSymbolType.CommentBody:
				return VBSymbolType.RazorComment;
			}
			return VBSymbolType.Unknown;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00010978 File Offset: 0x0000EB78
		protected override VBSymbol CreateSymbol(SourceLocation location, string content, VBSymbolType type, IEnumerable<RazorError> errors)
		{
			return new VBSymbol(location, content, type, errors);
		}

		// Token: 0x040000FC RID: 252
		private static readonly VBLanguageCharacteristics _instance = new VBLanguageCharacteristics();
	}
}
