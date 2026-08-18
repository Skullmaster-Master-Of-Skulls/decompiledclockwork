using System;
using System.ComponentModel;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200001A RID: 26
	public class JSON
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004544 File Offset: 0x00002744
		private bool IsAtEnd
		{
			get
			{
				return this.SkipSpace() == '\0';
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000454F File Offset: 0x0000274F
		private char Current
		{
			get
			{
				if (this.m_currentIndex >= this.m_jsonText.Length)
				{
					return '\0';
				}
				return this.m_jsonText[this.m_currentIndex];
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004577 File Offset: 0x00002777
		private string Minified
		{
			get
			{
				return this.m_builder.ToString();
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00004584 File Offset: 0x00002784
		private JSON(string jsonText)
		{
			this.m_jsonText = jsonText;
			this.m_currentIndex = 0;
			this.m_builder = new StringBuilder();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000045A8 File Offset: 0x000027A8
		public static string Validate(string jsonText)
		{
			JSON json = new JSON(jsonText);
			if (!json.IsValidValue() || !json.IsAtEnd)
			{
				return null;
			}
			return json.Minified;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000045D4 File Offset: 0x000027D4
		private bool IsValidValue()
		{
			bool result = false;
			char c = this.SkipSpace();
			if (c <= '[')
			{
				if (c != '"')
				{
					switch (c)
					{
					case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						result = this.IsValidNumber();
						break;
					case '.':
					case '/':
						break;
					default:
						if (c == '[')
						{
							result = this.IsValidArray();
						}
						break;
					}
				}
				else
				{
					result = this.IsValidString();
				}
			}
			else if (c <= 'n')
			{
				if (c != 'f')
				{
					if (c == 'n')
					{
						result = this.IsFollowedBy("ull");
					}
				}
				else
				{
					result = this.IsFollowedBy("alse");
				}
			}
			else if (c != 't')
			{
				if (c == '{')
				{
					result = this.IsValidObject();
				}
			}
			else
			{
				result = this.IsFollowedBy("rue");
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000046A4 File Offset: 0x000028A4
		[Localizable(false)]
		private bool IsFollowedBy(string text)
		{
			for (int i = 0; i < text.Length; i++)
			{
				if (this.Peek(i + 1) != text[i])
				{
					return false;
				}
			}
			int num = text.Length + 1;
			this.m_builder.Append(this.m_jsonText, this.m_currentIndex, num);
			this.m_currentIndex += num;
			return true;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004708 File Offset: 0x00002908
		private bool IsValidNumber()
		{
			bool flag = false;
			int currentIndex = this.m_currentIndex;
			char c = this.Current;
			if (c == '-')
			{
				c = this.Next();
			}
			if ('0' <= c && c <= '9')
			{
				flag = true;
				if (c == '0')
				{
					if ('0' <= (c = this.Next()) && c <= '9')
					{
						flag = false;
					}
				}
				else
				{
					while ('0' <= (c = this.Next()) && c <= '9')
					{
					}
				}
				if (flag && c == '.')
				{
					c = this.Next();
					if ('0' <= c && c <= '9')
					{
						while ('0' <= (c = this.Next()))
						{
							if (c > '9')
							{
								break;
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				if ((flag && c == 'e') || c == 'E')
				{
					c = this.Next();
					if (c == '-' || c == '+')
					{
						c = this.Next();
					}
					if ('0' <= c && c <= '9')
					{
						while ('0' <= (c = this.Next()))
						{
							if (c > '9')
							{
								break;
							}
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			this.m_builder.Append(this.m_jsonText, currentIndex, this.m_currentIndex - currentIndex);
			return flag;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000047FC File Offset: 0x000029FC
		private bool IsValidString()
		{
			int currentIndex = this.m_currentIndex;
			char c = this.Next();
			while (c != '\0' && c != '"')
			{
				if (c == '\\')
				{
					c = this.Next();
					if (c != '"' && c != '/' && c != '\\' && c != 'b' && c != 'f' && c != 'n' && c != 'r' && c != 't')
					{
						if (c != 'u')
						{
							return false;
						}
						for (int i = 0; i < 4; i++)
						{
							c = this.Next();
							if (('0' > c || c > '9') && ('A' > c || c > 'F') && ('a' > c || c > 'f'))
							{
								return false;
							}
						}
					}
				}
				c = this.Next();
			}
			if (c != '"')
			{
				return false;
			}
			this.Next();
			this.m_builder.Append(this.m_jsonText, currentIndex, this.m_currentIndex - currentIndex);
			return true;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000048C4 File Offset: 0x00002AC4
		private bool IsValidArray()
		{
			this.Next();
			this.m_builder.Append('[');
			if (this.SkipSpace() != ']')
			{
				if (!this.IsValidValue())
				{
					return false;
				}
				while (this.SkipSpace() == ',')
				{
					this.m_builder.Append(',');
					this.Next();
					if (!this.IsValidValue())
					{
						return false;
					}
				}
			}
			if (this.SkipSpace() != ']')
			{
				return false;
			}
			this.Next();
			this.m_builder.Append(']');
			return true;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004948 File Offset: 0x00002B48
		private bool IsValidObject()
		{
			this.Next();
			this.m_builder.Append('{');
			if (this.SkipSpace() != '}')
			{
				if (!this.IsValidProperty())
				{
					return false;
				}
				while (this.SkipSpace() == ',')
				{
					this.Next();
					this.SkipSpace();
					this.m_builder.Append(',');
					if (!this.IsValidProperty())
					{
						return false;
					}
				}
			}
			if (this.SkipSpace() != '}')
			{
				return false;
			}
			this.Next();
			this.m_builder.Append('}');
			return true;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000049D0 File Offset: 0x00002BD0
		private bool IsValidProperty()
		{
			if (!this.IsValidString())
			{
				return false;
			}
			if (this.SkipSpace() != ':')
			{
				return false;
			}
			this.Next();
			this.m_builder.Append(':');
			return this.IsValidValue();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004A08 File Offset: 0x00002C08
		private char Peek(int offset = 0)
		{
			int num = this.m_currentIndex + offset;
			if (num >= this.m_jsonText.Length)
			{
				return '\0';
			}
			return this.m_jsonText[this.m_currentIndex + offset];
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004A44 File Offset: 0x00002C44
		private char Next()
		{
			if (++this.m_currentIndex >= this.m_jsonText.Length)
			{
				return '\0';
			}
			return this.m_jsonText[this.m_currentIndex];
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004A84 File Offset: 0x00002C84
		private char SkipSpace()
		{
			char c = this.Current;
			while (c == '\t' || c == '\n' || c == '\r' || c == ' ')
			{
				c = this.Next();
			}
			return c;
		}

		// Token: 0x0400004D RID: 77
		private string m_jsonText;

		// Token: 0x0400004E RID: 78
		private int m_currentIndex;

		// Token: 0x0400004F RID: 79
		private StringBuilder m_builder;
	}
}
