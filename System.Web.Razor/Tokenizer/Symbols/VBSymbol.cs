using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;

namespace System.Web.Razor.Tokenizer.Symbols
{
	// Token: 0x0200007B RID: 123
	public class VBSymbol : SymbolBase<VBSymbolType>
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x000153A6 File Offset: 0x000135A6
		public VBSymbol(int offset, int line, int column, string content, VBSymbolType type) : this(new SourceLocation(offset, line, column), content, type, Enumerable.Empty<RazorError>())
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000153BF File Offset: 0x000135BF
		public VBSymbol(SourceLocation start, string content, VBSymbolType type) : this(start, content, type, Enumerable.Empty<RazorError>())
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000153CF File Offset: 0x000135CF
		public VBSymbol(int offset, int line, int column, string content, VBSymbolType type, IEnumerable<RazorError> errors) : base(new SourceLocation(offset, line, column), content, type, errors)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000153E5 File Offset: 0x000135E5
		public VBSymbol(SourceLocation start, string content, VBSymbolType type, IEnumerable<RazorError> errors) : base(start, content, type, errors)
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000153F2 File Offset: 0x000135F2
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x000153FA File Offset: 0x000135FA
		public VBKeyword? Keyword { get; set; }

		// Token: 0x06000568 RID: 1384 RVA: 0x00015404 File Offset: 0x00013604
		public override bool Equals(object obj)
		{
			VBSymbol vbsymbol = obj as VBSymbol;
			return base.Equals(obj) && vbsymbol.Keyword == this.Keyword;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00015454 File Offset: 0x00013654
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Keyword.GetHashCode();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001547C File Offset: 0x0001367C
		public static string GetSample(VBSymbolType type)
		{
			string result;
			if (!VBSymbol._symbolSamples.TryGetValue(type, out result))
			{
				switch (type)
				{
				case VBSymbolType.WhiteSpace:
					return RazorResources.VBSymbol_WhiteSpace;
				case VBSymbolType.NewLine:
					return RazorResources.VBSymbol_NewLine;
				case VBSymbolType.LineContinuation:
					break;
				case VBSymbolType.Comment:
					return RazorResources.VBSymbol_Comment;
				case VBSymbolType.Identifier:
					return RazorResources.VBSymbol_Identifier;
				case VBSymbolType.Keyword:
					return RazorResources.VBSymbol_Keyword;
				case VBSymbolType.IntegerLiteral:
					return RazorResources.VBSymbol_IntegerLiteral;
				case VBSymbolType.FloatingPointLiteral:
					return RazorResources.VBSymbol_FloatingPointLiteral;
				case VBSymbolType.StringLiteral:
					return RazorResources.VBSymbol_StringLiteral;
				case VBSymbolType.CharacterLiteral:
					return RazorResources.VBSymbol_CharacterLiteral;
				case VBSymbolType.DateLiteral:
					return RazorResources.VBSymbol_DateLiteral;
				default:
					if (type == VBSymbolType.RazorComment)
					{
						return RazorResources.VBSymbol_RazorComment;
					}
					break;
				}
				return RazorResources.Symbol_Unknown;
			}
			return result;
		}

		// Token: 0x04000223 RID: 547
		private static Dictionary<VBSymbolType, string> _symbolSamples = new Dictionary<VBSymbolType, string>
		{
			{
				VBSymbolType.LineContinuation,
				"_"
			},
			{
				VBSymbolType.LeftParenthesis,
				"("
			},
			{
				VBSymbolType.RightParenthesis,
				")"
			},
			{
				VBSymbolType.LeftBracket,
				"["
			},
			{
				VBSymbolType.RightBracket,
				"]"
			},
			{
				VBSymbolType.LeftBrace,
				"{"
			},
			{
				VBSymbolType.RightBrace,
				"}"
			},
			{
				VBSymbolType.Bang,
				"!"
			},
			{
				VBSymbolType.Hash,
				"#"
			},
			{
				VBSymbolType.Comma,
				","
			},
			{
				VBSymbolType.Dot,
				"."
			},
			{
				VBSymbolType.Colon,
				":"
			},
			{
				VBSymbolType.QuestionMark,
				"?"
			},
			{
				VBSymbolType.Concatenation,
				"&"
			},
			{
				VBSymbolType.Multiply,
				"*"
			},
			{
				VBSymbolType.Add,
				"+"
			},
			{
				VBSymbolType.Subtract,
				"-"
			},
			{
				VBSymbolType.Divide,
				"/"
			},
			{
				VBSymbolType.IntegerDivide,
				"\\"
			},
			{
				VBSymbolType.Exponentiation,
				"^"
			},
			{
				VBSymbolType.Equal,
				"="
			},
			{
				VBSymbolType.LessThan,
				"<"
			},
			{
				VBSymbolType.GreaterThan,
				">"
			},
			{
				VBSymbolType.Dollar,
				"$"
			},
			{
				VBSymbolType.Transition,
				"@"
			},
			{
				VBSymbolType.RazorCommentTransition,
				"@"
			},
			{
				VBSymbolType.RazorCommentStar,
				"*"
			}
		};
	}
}
