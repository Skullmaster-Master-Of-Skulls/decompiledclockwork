using System;
using System.Text;
using NLog.Internal;

namespace NLog.Conditions
{
	// Token: 0x0200003C RID: 60
	internal sealed class ConditionTokenizer
	{
		// Token: 0x0600010D RID: 269 RVA: 0x000044C3 File Offset: 0x000026C3
		public ConditionTokenizer(SimpleStringReader stringReader)
		{
			this.stringReader = stringReader;
			this.TokenType = ConditionTokenType.BeginningOfInput;
			this.GetNextToken();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000044DF File Offset: 0x000026DF
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000044E7 File Offset: 0x000026E7
		public int TokenPosition { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000044F0 File Offset: 0x000026F0
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000044F8 File Offset: 0x000026F8
		public ConditionTokenType TokenType { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004501 File Offset: 0x00002701
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004509 File Offset: 0x00002709
		public string TokenValue { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004514 File Offset: 0x00002714
		public string StringTokenValue
		{
			get
			{
				string tokenValue = this.TokenValue;
				return tokenValue.Substring(1, tokenValue.Length - 2).Replace("''", "'");
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004548 File Offset: 0x00002748
		public void Expect(ConditionTokenType tokenType)
		{
			if (this.TokenType != tokenType)
			{
				throw new ConditionParseException(string.Concat(new object[]
				{
					"Expected token of type: ",
					tokenType,
					", got ",
					this.TokenType,
					" (",
					this.TokenValue,
					")."
				}));
			}
			this.GetNextToken();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000045B8 File Offset: 0x000027B8
		public string EatKeyword()
		{
			if (this.TokenType != ConditionTokenType.Keyword)
			{
				throw new ConditionParseException("Identifier expected");
			}
			string tokenValue = this.TokenValue;
			this.GetNextToken();
			return tokenValue;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000045E7 File Offset: 0x000027E7
		public bool IsKeyword(string keyword)
		{
			return this.TokenType == ConditionTokenType.Keyword && this.TokenValue.Equals(keyword, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004606 File Offset: 0x00002806
		public bool IsEOF()
		{
			return this.TokenType == ConditionTokenType.EndOfInput;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004613 File Offset: 0x00002813
		public bool IsNumber()
		{
			return this.TokenType == ConditionTokenType.Number;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000461E File Offset: 0x0000281E
		public bool IsToken(ConditionTokenType tokenType)
		{
			return this.TokenType == tokenType;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000462C File Offset: 0x0000282C
		public void GetNextToken()
		{
			if (this.TokenType == ConditionTokenType.EndOfInput)
			{
				throw new ConditionParseException("Cannot read past end of stream.");
			}
			this.SkipWhitespace();
			this.TokenPosition = this.TokenPosition;
			int num = this.PeekChar();
			if (num == -1)
			{
				this.TokenType = ConditionTokenType.EndOfInput;
				return;
			}
			char c = (char)num;
			if (char.IsDigit(c))
			{
				this.ParseNumber(c);
				return;
			}
			if (c == '\'')
			{
				this.ParseSingleQuotedString(c);
				return;
			}
			if (c == '_' || char.IsLetter(c))
			{
				this.ParseKeyword(c);
				return;
			}
			if (c == '}' || c == ':')
			{
				this.TokenType = ConditionTokenType.EndOfInput;
				return;
			}
			this.TokenValue = c.ToString();
			bool flag = this.TryGetComparisonToken(c);
			if (flag)
			{
				return;
			}
			flag = this.TryGetLogicalToken(c);
			if (flag)
			{
				return;
			}
			if (c < ' ' || c >= '\u0080')
			{
				throw new ConditionParseException("Invalid token: " + c);
			}
			ConditionTokenType conditionTokenType = ConditionTokenizer.charIndexToTokenType[(int)c];
			if (conditionTokenType != ConditionTokenType.Invalid)
			{
				this.TokenType = conditionTokenType;
				this.TokenValue = new string(c, 1);
				this.ReadChar();
				return;
			}
			throw new ConditionParseException("Invalid punctuation: " + c);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004740 File Offset: 0x00002940
		private bool TryGetComparisonToken(char ch)
		{
			if (ch == '<')
			{
				this.ReadChar();
				int num = this.PeekChar();
				if (num == 62)
				{
					this.TokenType = ConditionTokenType.NotEqual;
					this.TokenValue = "<>";
					this.ReadChar();
					return true;
				}
				if (num == 61)
				{
					this.TokenType = ConditionTokenType.LessThanOrEqualTo;
					this.TokenValue = "<=";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.LessThan;
				this.TokenValue = "<";
				return true;
			}
			else
			{
				if (ch != '>')
				{
					return false;
				}
				this.ReadChar();
				int num2 = this.PeekChar();
				if (num2 == 61)
				{
					this.TokenType = ConditionTokenType.GreaterThanOrEqualTo;
					this.TokenValue = ">=";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.GreaterThan;
				this.TokenValue = ">";
				return true;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004800 File Offset: 0x00002A00
		private bool TryGetLogicalToken(char ch)
		{
			if (ch == '!')
			{
				this.ReadChar();
				int num = this.PeekChar();
				if (num == 61)
				{
					this.TokenType = ConditionTokenType.NotEqual;
					this.TokenValue = "!=";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.Not;
				this.TokenValue = "!";
				return true;
			}
			else if (ch == '&')
			{
				this.ReadChar();
				int num2 = this.PeekChar();
				if (num2 == 38)
				{
					this.TokenType = ConditionTokenType.And;
					this.TokenValue = "&&";
					this.ReadChar();
					return true;
				}
				throw new ConditionParseException("Expected '&&' but got '&'");
			}
			else if (ch == '|')
			{
				this.ReadChar();
				int num3 = this.PeekChar();
				if (num3 == 124)
				{
					this.TokenType = ConditionTokenType.Or;
					this.TokenValue = "||";
					this.ReadChar();
					return true;
				}
				throw new ConditionParseException("Expected '||' but got '|'");
			}
			else
			{
				if (ch != '=')
				{
					return false;
				}
				this.ReadChar();
				int num4 = this.PeekChar();
				if (num4 == 61)
				{
					this.TokenType = ConditionTokenType.EqualTo;
					this.TokenValue = "==";
					this.ReadChar();
					return true;
				}
				this.TokenType = ConditionTokenType.EqualTo;
				this.TokenValue = "=";
				return true;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004920 File Offset: 0x00002B20
		private static ConditionTokenType[] BuildCharIndexToTokenType()
		{
			ConditionTokenizer.CharToTokenType[] array = new ConditionTokenizer.CharToTokenType[]
			{
				new ConditionTokenizer.CharToTokenType('(', ConditionTokenType.LeftParen),
				new ConditionTokenizer.CharToTokenType(')', ConditionTokenType.RightParen),
				new ConditionTokenizer.CharToTokenType('.', ConditionTokenType.Dot),
				new ConditionTokenizer.CharToTokenType(',', ConditionTokenType.Comma),
				new ConditionTokenizer.CharToTokenType('!', ConditionTokenType.Not),
				new ConditionTokenizer.CharToTokenType('-', ConditionTokenType.Minus)
			};
			ConditionTokenType[] array2 = new ConditionTokenType[128];
			for (int i = 0; i < 128; i++)
			{
				array2[i] = ConditionTokenType.Invalid;
			}
			foreach (ConditionTokenizer.CharToTokenType charToTokenType in array)
			{
				array2[(int)charToTokenType.Character] = charToTokenType.TokenType;
			}
			return array2;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004A14 File Offset: 0x00002C14
		private void ParseSingleQuotedString(char ch)
		{
			this.TokenType = ConditionTokenType.String;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				ch = (char)num;
				stringBuilder.Append((char)this.ReadChar());
				if (ch == '\'')
				{
					if (this.PeekChar() != 39)
					{
						break;
					}
					stringBuilder.Append('\'');
					this.ReadChar();
				}
			}
			if (num == -1)
			{
				throw new ConditionParseException("String literal is missing a closing quote character.");
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004A98 File Offset: 0x00002C98
		private void ParseKeyword(char ch)
		{
			this.TokenType = ConditionTokenType.Keyword;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1 && ((ushort)num == 95 || (ushort)num == 45 || char.IsLetterOrDigit((char)num)))
			{
				stringBuilder.Append((char)this.ReadChar());
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B00 File Offset: 0x00002D00
		private void ParseNumber(char ch)
		{
			this.TokenType = ConditionTokenType.Number;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				ch = (char)num;
				if (!char.IsDigit(ch) && ch != '.')
				{
					break;
				}
				stringBuilder.Append((char)this.ReadChar());
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B64 File Offset: 0x00002D64
		private void SkipWhitespace()
		{
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				if (!char.IsWhiteSpace((char)num))
				{
					return;
				}
				this.ReadChar();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004B8F File Offset: 0x00002D8F
		private int PeekChar()
		{
			return this.stringReader.Peek();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004B9C File Offset: 0x00002D9C
		private int ReadChar()
		{
			return this.stringReader.Read();
		}

		// Token: 0x0400004A RID: 74
		private static readonly ConditionTokenType[] charIndexToTokenType = ConditionTokenizer.BuildCharIndexToTokenType();

		// Token: 0x0400004B RID: 75
		private readonly SimpleStringReader stringReader;

		// Token: 0x0200003D RID: 61
		private struct CharToTokenType
		{
			// Token: 0x06000126 RID: 294 RVA: 0x00004BB5 File Offset: 0x00002DB5
			public CharToTokenType(char character, ConditionTokenType tokenType)
			{
				this.Character = character;
				this.TokenType = tokenType;
			}

			// Token: 0x0400004F RID: 79
			public readonly char Character;

			// Token: 0x04000050 RID: 80
			public readonly ConditionTokenType TokenType;
		}
	}
}
