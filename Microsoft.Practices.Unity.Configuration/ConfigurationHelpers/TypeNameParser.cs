using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000012 RID: 18
	internal class TypeNameParser : ParseBuilder
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003044 File Offset: 0x00001244
		public static TypeNameInfo Parse(string typeName)
		{
			InputStream arg = new InputStream(typeName);
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_TypeName),
				new Func<InputStream, ParseResult>(ParseBuilder.EOF)
			})(arg);
			if (!parseResult.Matched)
			{
				return null;
			}
			ParseResult parseResult2 = new SequenceResult(parseResult)[0];
			return (TypeNameInfo)parseResult2.ResultData;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030AC File Offset: 0x000012AC
		private static void InitializeTypeNameInfo(TypeNameParser.ParsedUnqualifiedName from, TypeNameInfo to)
		{
			to.Name = from.Rootname;
			to.Namespace = from.Namespace;
			to.IsGenericType = (from.GenericParameters != null);
			if (to.IsGenericType)
			{
				to.IsOpenGeneric = from.GenericParameters.IsOpenGeneric;
				if (to.IsOpenGeneric)
				{
					for (int i = 0; i < from.GenericParameters.Count; i++)
					{
						to.GenericParameters.Add(null);
					}
					return;
				}
				foreach (TypeNameInfo item in from.GenericParameters.Parameters)
				{
					to.GenericParameters.Add(item);
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031AC File Offset: 0x000013AC
		private static ParseResult Match_TypeName(InputStream input)
		{
			TypeNameInfo resultData = new TypeNameInfo();
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.WithAction(new Func<InputStream, ParseResult>(TypeNameParser.Match_UnqualifiedName), delegate(ParseResult r)
				{
					TypeNameParser.InitializeTypeNameInfo((TypeNameParser.ParsedUnqualifiedName)r.ResultData, resultData);
				}),
				ParseBuilder.ZeroOrOne(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
				{
					new Func<InputStream, ParseResult>(TypeNameParser.Match_Comma),
					ParseBuilder.WithAction(new Func<InputStream, ParseResult>(TypeNameParser.Match_AssemblyName), delegate(ParseResult r)
					{
						resultData.AssemblyName = r.MatchedString;
					})
				}))
			})(input);
			if (!parseResult.Matched)
			{
				return parseResult;
			}
			return new ParseResult(parseResult.MatchedString, resultData);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003260 File Offset: 0x00001460
		private static ParseResult Match_AssemblyName(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_SimpleName),
				ParseBuilder.ZeroOrMore(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
				{
					new Func<InputStream, ParseResult>(TypeNameParser.Match_Comma),
					new Func<InputStream, ParseResult>(TypeNameParser.Match_AssemblyNamePart)
				}))
			})(input);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003310 File Offset: 0x00001510
		private static ParseResult Match_UnqualifiedName(InputStream input)
		{
			TypeNameParser.ParsedUnqualifiedName resultData = new TypeNameParser.ParsedUnqualifiedName();
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.WithAction(ParseBuilder.ZeroOrOne(new Func<InputStream, ParseResult>(TypeNameParser.Match_Namespace)), delegate(ParseResult r)
				{
					resultData.Namespace = (string)r.ResultData;
				}),
				ParseBuilder.WithAction(new Func<InputStream, ParseResult>(TypeNameParser.Match_RootName), delegate(ParseResult r)
				{
					resultData.Rootname = r.MatchedString;
				}),
				ParseBuilder.WithAction(ParseBuilder.ZeroOrOne(new Func<InputStream, ParseResult>(TypeNameParser.Match_GenericParameters)), delegate(ParseResult r)
				{
					resultData.GenericParameters = (TypeNameParser.GenericParameters)r.ResultData;
				})
			})(input);
			if (parseResult.Matched)
			{
				return new ParseResult(parseResult.MatchedString, resultData);
			}
			return parseResult;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033F0 File Offset: 0x000015F0
		private static ParseResult Match_Namespace(InputStream input)
		{
			List<string> ids = new List<string>();
			ParseResult parseResult = ParseBuilder.OneOrMore(ParseBuilder.WithAction(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Id),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Dot)
			}), delegate(ParseResult r)
			{
				ids.Add(new SequenceResult(r)[0].MatchedString);
			}))(input);
			if (parseResult.Matched)
			{
				string resultData = string.Join(".", ids.ToArray());
				return new ParseResult(parseResult.MatchedString, resultData);
			}
			return parseResult;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003484 File Offset: 0x00001684
		private static ParseResult Match_RootName(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Id),
				ParseBuilder.ZeroOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_NestedName))
			})(input);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034C8 File Offset: 0x000016C8
		private static ParseResult Match_NestedName(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Plus),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Id)
			})(input);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003508 File Offset: 0x00001708
		private static ParseResult Match_GenericParameters(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_ClosedGeneric),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_OpenGeneric)
			})(input);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003548 File Offset: 0x00001748
		private static ParseResult Match_OpenGeneric(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_CLRSyntax),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_EmptyBrackets)
			})(input);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000035D8 File Offset: 0x000017D8
		private static ParseResult Match_CLRSyntax(InputStream input)
		{
			TypeNameParser.GenericParameters resultData = new TypeNameParser.GenericParameters();
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Backquote),
				ParseBuilder.WithAction(ParseBuilder.OneOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_Digit)), delegate(ParseResult r)
				{
					resultData.IsOpenGeneric = true;
					int num = int.Parse(r.MatchedString, CultureInfo.InvariantCulture);
					for (int i = 0; i < num; i++)
					{
						resultData.Parameters.Add(null);
					}
				})
			})(input);
			if (parseResult.Matched)
			{
				return new ParseResult(parseResult.MatchedString, resultData);
			}
			return parseResult;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000036A8 File Offset: 0x000018A8
		private static ParseResult Match_EmptyBrackets(InputStream input)
		{
			TypeNameParser.GenericParameters resultData = new TypeNameParser.GenericParameters();
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_LeftBracket),
				ParseBuilder.WithAction(ParseBuilder.ZeroOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_Comma)), delegate(ParseResult r)
				{
					int num = r.MatchedString.Length + 1;
					resultData.IsOpenGeneric = true;
					for (int i = 0; i < num; i++)
					{
						resultData.Parameters.Add(null);
					}
				}),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_RightBracket)
			})(input);
			if (parseResult.Matched)
			{
				return new ParseResult(parseResult.MatchedString, resultData);
			}
			return parseResult;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003738 File Offset: 0x00001938
		private static ParseResult Match_ClosedGeneric(InputStream input)
		{
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.ZeroOrOne(new Func<InputStream, ParseResult>(TypeNameParser.Match_CLRSyntax)),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_TypeParameters)
			})(input);
			if (parseResult.Matched)
			{
				TypeNameParser.GenericParameters genericParameters = new TypeNameParser.GenericParameters();
				genericParameters.IsOpenGeneric = false;
				genericParameters.Parameters.AddRange((List<TypeNameInfo>)new SequenceResult(parseResult)[1].ResultData);
				return new ParseResult(parseResult.MatchedString, genericParameters);
			}
			return parseResult;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000037C0 File Offset: 0x000019C0
		private static ParseResult Match_TypeParameters(InputStream input)
		{
			List<TypeNameInfo> list = new List<TypeNameInfo>();
			ParseResult parseResult = ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_LeftBracket),
				ParseBuilder.WithAction(new Func<InputStream, ParseResult>(TypeNameParser.Match_GenericTypeParameter), TypeNameParser.StoreTypeParameter(list)),
				ParseBuilder.ZeroOrMore(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
				{
					new Func<InputStream, ParseResult>(TypeNameParser.Match_Comma),
					ParseBuilder.WithAction(new Func<InputStream, ParseResult>(TypeNameParser.Match_GenericTypeParameter), TypeNameParser.StoreTypeParameter(list))
				})),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_RightBracket)
			})(input);
			if (parseResult.Matched)
			{
				return new ParseResult(parseResult.MatchedString, list);
			}
			return parseResult;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003894 File Offset: 0x00001A94
		private static Action<ParseResult> StoreTypeParameter(ICollection<TypeNameInfo> l)
		{
			return delegate(ParseResult r)
			{
				l.Add((TypeNameInfo)r.ResultData);
			};
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038FC File Offset: 0x00001AFC
		private static ParseResult Match_GenericTypeParameter(InputStream input)
		{
			Func<InputStream, ParseResult>[] array = new Func<InputStream, ParseResult>[2];
			array[0] = ParseBuilder.WithAction(new Func<InputStream, ParseResult>(TypeNameParser.Match_UnqualifiedName), delegate(ParseResult r)
			{
				TypeNameInfo typeNameInfo = new TypeNameInfo();
				TypeNameParser.InitializeTypeNameInfo((TypeNameParser.ParsedUnqualifiedName)r.ResultData, typeNameInfo);
				return new ParseResult(r.MatchedString, typeNameInfo);
			});
			array[1] = ParseBuilder.WithAction(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_LeftBracket),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_TypeName),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_RightBracket)
			}), (ParseResult r) => new SequenceResult(r)[1]);
			return ParseBuilder.FirstOf(array)(input);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000039AC File Offset: 0x00001BAC
		private static ParseResult Match_SimpleName(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_AssemblyNameStart),
				ParseBuilder.ZeroOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_AssemblyNameContinuation)),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003A00 File Offset: 0x00001C00
		private static ParseResult Match_AssemblyNamePart(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Culture),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Version),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_PublicKey),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_PublicKeyToken)
			})(input);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A5C File Offset: 0x00001C5C
		private static ParseResult Match_Culture(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match("Culture"),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Equals),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_LanguageTag)
			})(input);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private static ParseResult Match_LanguageTag(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_LangTagPart),
				ParseBuilder.ZeroOrOne(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
				{
					ParseBuilder.Match('-'),
					new Func<InputStream, ParseResult>(TypeNameParser.Match_LangTagPart)
				})),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B34 File Offset: 0x00001D34
		private static ParseResult Match_LangTagPart(InputStream input)
		{
			Func<InputStream, ParseResult> func = ParseBuilder.Match((char ch) => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(ch));
			Func<InputStream, ParseResult> func2 = ParseBuilder.ZeroOrOne(func);
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				func,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2,
				func2
			})(input);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private static ParseResult Match_Version(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match("Version"),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Equals),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_VersionNumber),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003C34 File Offset: 0x00001E34
		private static ParseResult Match_VersionNumber(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Int),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Dot),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Int),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Dot),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Int),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Dot),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Int)
			})(input);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003CC0 File Offset: 0x00001EC0
		private static ParseResult Match_PublicKey(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match("PublicKey"),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Equals),
				ParseBuilder.OneOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_HexDigit)),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003D30 File Offset: 0x00001F30
		private static ParseResult Match_PublicKeyToken(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match("PublicKeyToken"),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Equals),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_PublicKeyValue)
			})(input);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D8C File Offset: 0x00001F8C
		private static ParseResult Match_PublicKeyValue(InputStream input)
		{
			IEnumerable<Func<InputStream, ParseResult>> source = Enumerable.Repeat<Func<InputStream, ParseResult>>(new Func<InputStream, ParseResult>(TypeNameParser.Match_HexDigit), 16).Concat(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			});
			return ParseBuilder.Sequence(source.ToArray<Func<InputStream, ParseResult>>())(input);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003DDC File Offset: 0x00001FDC
		private static ParseResult Match_AssemblyNameStart(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Not(new Func<InputStream, ParseResult>(TypeNameParser.Match_Dot)),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_AssemblyNameChar)
			})(input);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003E1F File Offset: 0x0000201F
		private static ParseResult Match_AssemblyNameContinuation(InputStream input)
		{
			return TypeNameParser.Match_AssemblyNameChar(input);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003E38 File Offset: 0x00002038
		private static ParseResult Match_AssemblyNameChar(InputStream input)
		{
			Func<InputStream, ParseResult>[] array = new Func<InputStream, ParseResult>[2];
			array[0] = new Func<InputStream, ParseResult>(TypeNameParser.Match_QuotedChar);
			array[1] = ParseBuilder.Match((char ch) => !"^/\\:?\"<>|,[]".Contains(ch));
			return ParseBuilder.FirstOf(array)(input);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003E8C File Offset: 0x0000208C
		private static ParseResult Match_Id(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_IdStart),
				ParseBuilder.ZeroOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_IdContinuation))
			})(input);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003ED0 File Offset: 0x000020D0
		private static ParseResult Match_IdStart(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_IdNonAlpha),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_IdAlpha)
			})(input);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003F10 File Offset: 0x00002110
		private static ParseResult Match_IdContinuation(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_IdNonAlpha),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_IdAlphanumeric)
			})(input);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003F58 File Offset: 0x00002158
		private static ParseResult Match_IdAlpha(InputStream input)
		{
			Func<InputStream, ParseResult>[] array = new Func<InputStream, ParseResult>[2];
			array[0] = new Func<InputStream, ParseResult>(TypeNameParser.Match_QuotedChar);
			array[1] = ParseBuilder.Match((char ch) => char.IsLetter(ch));
			return ParseBuilder.FirstOf(array)(input);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003FB4 File Offset: 0x000021B4
		private static ParseResult Match_IdAlphanumeric(InputStream input)
		{
			Func<InputStream, ParseResult>[] array = new Func<InputStream, ParseResult>[2];
			array[0] = new Func<InputStream, ParseResult>(TypeNameParser.Match_QuotedChar);
			array[1] = ParseBuilder.Match((char ch) => char.IsLetterOrDigit(ch));
			return ParseBuilder.FirstOf(array)(input);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004030 File Offset: 0x00002230
		private static ParseResult Match_QuotedChar(InputStream input)
		{
			return ParseBuilder.WithAction(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Backslash),
				new Func<InputStream, ParseResult>(ParseBuilder.Any)
			}), delegate(ParseResult result)
			{
				string matchedString = new SequenceResult(result)[1].MatchedString;
				return new ParseResult(matchedString);
			})(input);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000409D File Offset: 0x0000229D
		private static ParseResult Match_IdNonAlpha(InputStream input)
		{
			return ParseBuilder.Match((char ch) => "_$@?".Contains(ch))(input);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000040CF File Offset: 0x000022CF
		private static ParseResult Match_Digit(InputStream input)
		{
			return ParseBuilder.Match((char ch) => char.IsDigit(ch))(input);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004106 File Offset: 0x00002306
		private static ParseResult Match_HexDigit(InputStream input)
		{
			return ParseBuilder.Match((char ch) => "0123456789ABCDEFabcdef".Contains(ch))(input);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004154 File Offset: 0x00002354
		private static ParseResult Match_Int(InputStream input)
		{
			return ParseBuilder.WithAction(ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Digit),
				ParseBuilder.ZeroOrMore(new Func<InputStream, ParseResult>(TypeNameParser.Match_Digit))
			}), (ParseResult r) => new ParseResult(r.MatchedString, int.Parse(r.MatchedString, CultureInfo.InvariantCulture)))(input);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000041BC File Offset: 0x000023BC
		private static ParseResult Match_LeftBracket(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match('['),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000041F8 File Offset: 0x000023F8
		private static ParseResult Match_RightBracket(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match(']'),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004231 File Offset: 0x00002431
		private static ParseResult Match_Dot(InputStream input)
		{
			return ParseBuilder.Match('.')(input);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004240 File Offset: 0x00002440
		private static ParseResult Match_Backquote(InputStream input)
		{
			return ParseBuilder.Match('`')(input);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000424F File Offset: 0x0000244F
		private static ParseResult Match_Plus(InputStream input)
		{
			return ParseBuilder.Match('+')(input);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004260 File Offset: 0x00002460
		private static ParseResult Match_Comma(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match(','),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004299 File Offset: 0x00002499
		private static ParseResult Match_Backslash(InputStream input)
		{
			return ParseBuilder.Match('\\')(input);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000042A8 File Offset: 0x000024A8
		private static ParseResult Match_Equals(InputStream input)
		{
			return ParseBuilder.Sequence(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match('='),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Spacing)
			})(input);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042E1 File Offset: 0x000024E1
		private static ParseResult Match_Spacing(InputStream input)
		{
			return ParseBuilder.ZeroOrOne(new Func<InputStream, ParseResult>(TypeNameParser.Match_Space))(input);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042FC File Offset: 0x000024FC
		private static ParseResult Match_Space(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match(' '),
				ParseBuilder.Match('\t'),
				new Func<InputStream, ParseResult>(TypeNameParser.Match_Eol)
			})(input);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004340 File Offset: 0x00002540
		private static ParseResult Match_Eol(InputStream input)
		{
			return ParseBuilder.FirstOf(new Func<InputStream, ParseResult>[]
			{
				ParseBuilder.Match("\r\n"),
				ParseBuilder.Match('\r'),
				ParseBuilder.Match('\n')
			})(input);
		}

		// Token: 0x02000013 RID: 19
		private class ParsedUnqualifiedName
		{
			// Token: 0x04000022 RID: 34
			public string Namespace;

			// Token: 0x04000023 RID: 35
			public string Rootname;

			// Token: 0x04000024 RID: 36
			public TypeNameParser.GenericParameters GenericParameters;
		}

		// Token: 0x02000014 RID: 20
		private class GenericParameters
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004391 File Offset: 0x00002591
			public int Count
			{
				get
				{
					return this.Parameters.Count;
				}
			}

			// Token: 0x04000025 RID: 37
			public readonly List<TypeNameInfo> Parameters = new List<TypeNameInfo>();

			// Token: 0x04000026 RID: 38
			public bool IsOpenGeneric;
		}
	}
}
