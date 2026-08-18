using System;
using System.IO;
using System.Text;
using WebGrease.Css.Ast;

namespace WebGrease.Css
{
	// Token: 0x0200019B RID: 411
	public class UnicodeDecoder
	{
		// Token: 0x06001521 RID: 5409 RVA: 0x0007A7CC File Offset: 0x000789CC
		private UnicodeDecoder(TextReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0007A7DC File Offset: 0x000789DC
		public static string Decode(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			string unicode;
			using (StringReader stringReader = new StringReader(text))
			{
				UnicodeDecoder unicodeDecoder = new UnicodeDecoder(stringReader);
				unicode = unicodeDecoder.GetUnicode();
			}
			return unicode;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0007A828 File Offset: 0x00078A28
		private static int HValue(char ch)
		{
			int result = 0;
			if ('0' <= ch && ch <= '9')
			{
				result = (int)(ch - '0');
			}
			else if ('a' <= ch && ch <= 'f')
			{
				result = (int)(ch - 'a' + '\n');
			}
			else if ('A' <= ch && ch <= 'F')
			{
				result = (int)(ch - 'A' + '\n');
			}
			return result;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0007A86F File Offset: 0x00078A6F
		private static bool IsH(char ch)
		{
			return ('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'f') || ('A' <= ch && ch <= 'F');
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0007A898 File Offset: 0x00078A98
		private static bool IsSpace(char ch)
		{
			switch (ch)
			{
			case '\t':
			case '\n':
			case '\f':
			case '\r':
				break;
			case '\v':
				return false;
			default:
				if (ch != ' ')
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0007A8CC File Offset: 0x00078ACC
		private string GetUnicode()
		{
			this.NextChar();
			StringBuilder stringBuilder = new StringBuilder();
			while (this._currentChar != '\0')
			{
				if (this._currentChar == '\\' && UnicodeDecoder.IsH(this.PeekChar()))
				{
					int num = this.GetUnicodeEncodingValue();
					if (num >= 55296 && num <= 56319)
					{
						this.NextChar();
						int num2 = num;
						if (this._currentChar != '\\' || !UnicodeDecoder.IsH(this.PeekChar()))
						{
							throw new AstException("High surrogate should be followed by the low surrogate.");
						}
						int unicodeEncodingValue = this.GetUnicodeEncodingValue();
						if (unicodeEncodingValue < 56320 || unicodeEncodingValue > 57343)
						{
							throw new AstException("Invalid low surrogate.");
						}
						num = 65536 + (num2 - 55296) * 1024 + (unicodeEncodingValue - 56320);
					}
					stringBuilder.Append(char.ConvertFromUtf32(num));
				}
				else
				{
					stringBuilder.Append(this._currentChar);
				}
				this.NextChar();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0007A9BC File Offset: 0x00078BBC
		private int GetUnicodeEncodingValue()
		{
			int num = 0;
			int num2 = 0;
			while (num2++ < 6 && UnicodeDecoder.IsH(this.PeekChar()))
			{
				this.NextChar();
				num = num * 16 + UnicodeDecoder.HValue(this._currentChar);
			}
			if (UnicodeDecoder.IsSpace(this.PeekChar()))
			{
				this.NextChar();
			}
			return num;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0007AA10 File Offset: 0x00078C10
		private void NextChar()
		{
			if (this._readAhead != null)
			{
				this._currentChar = this._readAhead[0];
				this._readAhead = ((this._readAhead.Length == 1) ? null : this._readAhead.Substring(1));
				return;
			}
			int num = this._reader.Read();
			if (num < 0)
			{
				this._currentChar = '\0';
				return;
			}
			this._currentChar = (char)num;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0007AA7C File Offset: 0x00078C7C
		private char PeekChar()
		{
			if (this._readAhead != null)
			{
				return this._readAhead[0];
			}
			int num = this._reader.Peek();
			if (num < 0)
			{
				return '\0';
			}
			return (char)num;
		}

		// Token: 0x04000B57 RID: 2903
		private readonly TextReader _reader;

		// Token: 0x04000B58 RID: 2904
		private char _currentChar;

		// Token: 0x04000B59 RID: 2905
		private string _readAhead;
	}
}
