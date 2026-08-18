using System;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200003F RID: 63
	public class CSharpLanguageCharacteristics : LanguageCharacteristics<CSharpTokenizer, CSharpSymbol, CSharpSymbolType>
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000B7BE File Offset: 0x000099BE
		private CSharpLanguageCharacteristics()
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public static CSharpLanguageCharacteristics Instance
		{
			get
			{
				return CSharpLanguageCharacteristics._instance;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B7CD File Offset: 0x000099CD
		public override CSharpTokenizer CreateTokenizer(ITextDocument source)
		{
			return new CSharpTokenizer(source);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B7D5 File Offset: 0x000099D5
		protected override CSharpSymbol CreateSymbol(SourceLocation location, string content, CSharpSymbolType type, IEnumerable<RazorError> errors)
		{
			return new CSharpSymbol(location, content, type, errors);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B7E1 File Offset: 0x000099E1
		public override string GetSample(CSharpSymbolType type)
		{
			return CSharpLanguageCharacteristics.GetSymbolSample(type);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B7E9 File Offset: 0x000099E9
		public override CSharpSymbol CreateMarkerSymbol(SourceLocation location)
		{
			return new CSharpSymbol(location, string.Empty, CSharpSymbolType.Unknown);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public override CSharpSymbolType GetKnownSymbolType(KnownSymbolType type)
		{
			switch (type)
			{
			case KnownSymbolType.WhiteSpace:
				return CSharpSymbolType.WhiteSpace;
			case KnownSymbolType.NewLine:
				return CSharpSymbolType.NewLine;
			case KnownSymbolType.Identifier:
				return CSharpSymbolType.Identifier;
			case KnownSymbolType.Keyword:
				return CSharpSymbolType.Keyword;
			case KnownSymbolType.Transition:
				return CSharpSymbolType.Transition;
			case KnownSymbolType.CommentStart:
				return CSharpSymbolType.RazorCommentTransition;
			case KnownSymbolType.CommentStar:
				return CSharpSymbolType.RazorCommentStar;
			case KnownSymbolType.CommentBody:
				return CSharpSymbolType.RazorComment;
			}
			return CSharpSymbolType.Unknown;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B848 File Offset: 0x00009A48
		public override CSharpSymbolType FlipBracket(CSharpSymbolType bracket)
		{
			if (bracket <= CSharpSymbolType.LeftBrace)
			{
				switch (bracket)
				{
				case CSharpSymbolType.LeftParenthesis:
					return CSharpSymbolType.RightParenthesis;
				case CSharpSymbolType.RightParenthesis:
					return CSharpSymbolType.LeftParenthesis;
				default:
					switch (bracket)
					{
					case CSharpSymbolType.RightBracket:
						return CSharpSymbolType.LeftBracket;
					case CSharpSymbolType.LeftBracket:
						return CSharpSymbolType.RightBracket;
					case CSharpSymbolType.LeftBrace:
						return CSharpSymbolType.RightBrace;
					}
					break;
				}
			}
			else
			{
				if (bracket == CSharpSymbolType.RightBrace)
				{
					return CSharpSymbolType.LeftBrace;
				}
				if (bracket == CSharpSymbolType.LessThan)
				{
					return CSharpSymbolType.GreaterThan;
				}
				if (bracket == CSharpSymbolType.GreaterThan)
				{
					return CSharpSymbolType.LessThan;
				}
			}
			return CSharpSymbolType.Unknown;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		public static string GetKeyword(CSharpKeyword keyword)
		{
			return keyword.ToString().ToLowerInvariant();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public static string GetSymbolSample(CSharpSymbolType type)
		{
			string result;
			if (CSharpLanguageCharacteristics._symbolSamples.TryGetValue(type, out result))
			{
				return result;
			}
			switch (type)
			{
			case CSharpSymbolType.Identifier:
				return RazorResources.CSharpSymbol_Identifier;
			case CSharpSymbolType.Keyword:
				return RazorResources.CSharpSymbol_Keyword;
			case CSharpSymbolType.IntegerLiteral:
				return RazorResources.CSharpSymbol_IntegerLiteral;
			case CSharpSymbolType.NewLine:
				return RazorResources.CSharpSymbol_Newline;
			case CSharpSymbolType.WhiteSpace:
				return RazorResources.CSharpSymbol_Whitespace;
			case CSharpSymbolType.Comment:
				return RazorResources.CSharpSymbol_Comment;
			case CSharpSymbolType.RealLiteral:
				return RazorResources.CSharpSymbol_RealLiteral;
			case CSharpSymbolType.CharacterLiteral:
				return RazorResources.CSharpSymbol_CharacterLiteral;
			case CSharpSymbolType.StringLiteral:
				return RazorResources.CSharpSymbol_StringLiteral;
			default:
				return RazorResources.Symbol_Unknown;
			}
		}

		// Token: 0x040000B2 RID: 178
		private static readonly CSharpLanguageCharacteristics _instance = new CSharpLanguageCharacteristics();

		// Token: 0x040000B3 RID: 179
		private static Dictionary<CSharpSymbolType, string> _symbolSamples = new Dictionary<CSharpSymbolType, string>
		{
			{
				CSharpSymbolType.Arrow,
				"->"
			},
			{
				CSharpSymbolType.Minus,
				"-"
			},
			{
				CSharpSymbolType.Decrement,
				"--"
			},
			{
				CSharpSymbolType.MinusAssign,
				"-="
			},
			{
				CSharpSymbolType.NotEqual,
				"!="
			},
			{
				CSharpSymbolType.Not,
				"!"
			},
			{
				CSharpSymbolType.Modulo,
				"%"
			},
			{
				CSharpSymbolType.ModuloAssign,
				"%="
			},
			{
				CSharpSymbolType.AndAssign,
				"&="
			},
			{
				CSharpSymbolType.And,
				"&"
			},
			{
				CSharpSymbolType.DoubleAnd,
				"&&"
			},
			{
				CSharpSymbolType.LeftParenthesis,
				"("
			},
			{
				CSharpSymbolType.RightParenthesis,
				")"
			},
			{
				CSharpSymbolType.Star,
				"*"
			},
			{
				CSharpSymbolType.MultiplyAssign,
				"*="
			},
			{
				CSharpSymbolType.Comma,
				","
			},
			{
				CSharpSymbolType.Dot,
				"."
			},
			{
				CSharpSymbolType.Slash,
				"/"
			},
			{
				CSharpSymbolType.DivideAssign,
				"/="
			},
			{
				CSharpSymbolType.DoubleColon,
				"::"
			},
			{
				CSharpSymbolType.Colon,
				":"
			},
			{
				CSharpSymbolType.Semicolon,
				";"
			},
			{
				CSharpSymbolType.QuestionMark,
				"?"
			},
			{
				CSharpSymbolType.NullCoalesce,
				"??"
			},
			{
				CSharpSymbolType.RightBracket,
				"]"
			},
			{
				CSharpSymbolType.LeftBracket,
				"["
			},
			{
				CSharpSymbolType.XorAssign,
				"^="
			},
			{
				CSharpSymbolType.Xor,
				"^"
			},
			{
				CSharpSymbolType.LeftBrace,
				"{"
			},
			{
				CSharpSymbolType.OrAssign,
				"|="
			},
			{
				CSharpSymbolType.DoubleOr,
				"||"
			},
			{
				CSharpSymbolType.Or,
				"|"
			},
			{
				CSharpSymbolType.RightBrace,
				"}"
			},
			{
				CSharpSymbolType.Tilde,
				"~"
			},
			{
				CSharpSymbolType.Plus,
				"+"
			},
			{
				CSharpSymbolType.PlusAssign,
				"+="
			},
			{
				CSharpSymbolType.Increment,
				"++"
			},
			{
				CSharpSymbolType.LessThan,
				"<"
			},
			{
				CSharpSymbolType.LessThanEqual,
				"<="
			},
			{
				CSharpSymbolType.LeftShift,
				"<<"
			},
			{
				CSharpSymbolType.LeftShiftAssign,
				"<<="
			},
			{
				CSharpSymbolType.Assign,
				"="
			},
			{
				CSharpSymbolType.Equals,
				"=="
			},
			{
				CSharpSymbolType.GreaterThan,
				">"
			},
			{
				CSharpSymbolType.GreaterThanEqual,
				">="
			},
			{
				CSharpSymbolType.RightShift,
				">>"
			},
			{
				CSharpSymbolType.RightShiftAssign,
				">>>"
			},
			{
				CSharpSymbolType.Hash,
				"#"
			},
			{
				CSharpSymbolType.Transition,
				"@"
			}
		};
	}
}
