using System;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000040 RID: 64
	public class HtmlLanguageCharacteristics : LanguageCharacteristics<HtmlTokenizer, HtmlSymbol, HtmlSymbolType>
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		private HtmlLanguageCharacteristics()
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BBFC File Offset: 0x00009DFC
		public static HtmlLanguageCharacteristics Instance
		{
			get
			{
				return HtmlLanguageCharacteristics._instance;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BC04 File Offset: 0x00009E04
		public override string GetSample(HtmlSymbolType type)
		{
			switch (type)
			{
			case HtmlSymbolType.Text:
				return RazorResources.HtmlSymbol_Text;
			case HtmlSymbolType.WhiteSpace:
				return RazorResources.HtmlSymbol_WhiteSpace;
			case HtmlSymbolType.NewLine:
				return RazorResources.HtmlSymbol_NewLine;
			case HtmlSymbolType.OpenAngle:
				return "<";
			case HtmlSymbolType.Bang:
				return "!";
			case HtmlSymbolType.Solidus:
				return "/";
			case HtmlSymbolType.QuestionMark:
				return "?";
			case HtmlSymbolType.DoubleHyphen:
				return "--";
			case HtmlSymbolType.LeftBracket:
				return "[";
			case HtmlSymbolType.CloseAngle:
				return ">";
			case HtmlSymbolType.RightBracket:
				return "]";
			case HtmlSymbolType.Equals:
				return "=";
			case HtmlSymbolType.DoubleQuote:
				return "\"";
			case HtmlSymbolType.SingleQuote:
				return "'";
			case HtmlSymbolType.Transition:
				return "@";
			case HtmlSymbolType.Colon:
				return ":";
			case HtmlSymbolType.RazorComment:
				return RazorResources.HtmlSymbol_RazorComment;
			case HtmlSymbolType.RazorCommentStar:
				return "*";
			case HtmlSymbolType.RazorCommentTransition:
				return "@";
			default:
				return RazorResources.Symbol_Unknown;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public override HtmlTokenizer CreateTokenizer(ITextDocument source)
		{
			return new HtmlTokenizer(source);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public override HtmlSymbolType FlipBracket(HtmlSymbolType bracket)
		{
			if (bracket == HtmlSymbolType.OpenAngle)
			{
				return HtmlSymbolType.CloseAngle;
			}
			switch (bracket)
			{
			case HtmlSymbolType.LeftBracket:
				return HtmlSymbolType.RightBracket;
			case HtmlSymbolType.CloseAngle:
				return HtmlSymbolType.OpenAngle;
			case HtmlSymbolType.RightBracket:
				return HtmlSymbolType.LeftBracket;
			default:
				return HtmlSymbolType.Unknown;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000BD1E File Offset: 0x00009F1E
		public override HtmlSymbol CreateMarkerSymbol(SourceLocation location)
		{
			return new HtmlSymbol(location, string.Empty, HtmlSymbolType.Unknown);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public override HtmlSymbolType GetKnownSymbolType(KnownSymbolType type)
		{
			switch (type)
			{
			case KnownSymbolType.WhiteSpace:
				return HtmlSymbolType.WhiteSpace;
			case KnownSymbolType.NewLine:
				return HtmlSymbolType.NewLine;
			case KnownSymbolType.Identifier:
				return HtmlSymbolType.Text;
			case KnownSymbolType.Keyword:
				return HtmlSymbolType.Text;
			case KnownSymbolType.Transition:
				return HtmlSymbolType.Transition;
			case KnownSymbolType.CommentStart:
				return HtmlSymbolType.RazorCommentTransition;
			case KnownSymbolType.CommentStar:
				return HtmlSymbolType.RazorCommentStar;
			case KnownSymbolType.CommentBody:
				return HtmlSymbolType.RazorComment;
			}
			return HtmlSymbolType.Unknown;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000BD7C File Offset: 0x00009F7C
		protected override HtmlSymbol CreateSymbol(SourceLocation location, string content, HtmlSymbolType type, IEnumerable<RazorError> errors)
		{
			return new HtmlSymbol(location, content, type, errors);
		}

		// Token: 0x040000B4 RID: 180
		private static readonly HtmlLanguageCharacteristics _instance = new HtmlLanguageCharacteristics();
	}
}
