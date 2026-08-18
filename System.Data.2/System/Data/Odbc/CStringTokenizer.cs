using System;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002B4 RID: 692
	internal sealed class CStringTokenizer
	{
		// Token: 0x060029E9 RID: 10729 RVA: 0x00115864 File Offset: 0x00114C64
		internal CStringTokenizer(string text, char quote, char escape)
		{
			this._token = new StringBuilder();
			this._quote = quote;
			this._escape = escape;
			this._sqlstatement = text;
			if (text != null)
			{
				int num = text.IndexOf('\0');
				this._len = ((0 > num) ? text.Length : num);
				return;
			}
			this._len = 0;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x001158C0 File Offset: 0x00114CC0
		internal int CurrentPosition
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x001158D4 File Offset: 0x00114CD4
		internal string NextToken()
		{
			if (this._token.Length != 0)
			{
				this._idx += this._token.Length;
				this._token.Remove(0, this._token.Length);
			}
			while (this._idx < this._len && char.IsWhiteSpace(this._sqlstatement[this._idx]))
			{
				this._idx++;
			}
			if (this._idx == this._len)
			{
				return string.Empty;
			}
			int i = this._idx;
			bool flag = false;
			while (!flag && i < this._len)
			{
				if (this.IsValidNameChar(this._sqlstatement[i]))
				{
					while (i < this._len)
					{
						if (!this.IsValidNameChar(this._sqlstatement[i]))
						{
							break;
						}
						this._token.Append(this._sqlstatement[i]);
						i++;
					}
				}
				else
				{
					char c = this._sqlstatement[i];
					if (c == '[')
					{
						i = this.GetTokenFromBracket(i);
					}
					else
					{
						if (' ' == this._quote || c != this._quote)
						{
							if (!char.IsWhiteSpace(c))
							{
								if (c == ',')
								{
									if (i == this._idx)
									{
										this._token.Append(c);
									}
								}
								else
								{
									this._token.Append(c);
								}
							}
							break;
						}
						i = this.GetTokenFromQuote(i);
					}
				}
			}
			if (this._token.Length <= 0)
			{
				return string.Empty;
			}
			return this._token.ToString();
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00115A68 File Offset: 0x00114E68
		private int GetTokenFromBracket(int curidx)
		{
			while (curidx < this._len)
			{
				this._token.Append(this._sqlstatement[curidx]);
				curidx++;
				if (this._sqlstatement[curidx - 1] == ']')
				{
					break;
				}
			}
			return curidx;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00115AB0 File Offset: 0x00114EB0
		private int GetTokenFromQuote(int curidx)
		{
			int i;
			for (i = curidx; i < this._len; i++)
			{
				this._token.Append(this._sqlstatement[i]);
				if (this._sqlstatement[i] == this._quote && i > curidx && this._sqlstatement[i - 1] != this._escape && i + 1 < this._len && this._sqlstatement[i + 1] != this._quote)
				{
					return i + 1;
				}
			}
			return i;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00115B3C File Offset: 0x00114F3C
		private bool IsValidNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.' || ch == '$' || ch == '#' || ch == '@' || ch == '~' || ch == '`' || ch == '%' || ch == '^' || ch == '&' || ch == '|';
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00115B90 File Offset: 0x00114F90
		internal int FindTokenIndex(string tokenString)
		{
			string text;
			do
			{
				text = this.NextToken();
				if (this._idx == this._len || ADP.IsEmpty(text))
				{
					return -1;
				}
			}
			while (string.Compare(tokenString, text, StringComparison.OrdinalIgnoreCase) != 0);
			return this._idx;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x00115BCC File Offset: 0x00114FCC
		internal bool StartsWith(string tokenString)
		{
			int num = 0;
			while (num < this._len && char.IsWhiteSpace(this._sqlstatement[num]))
			{
				num++;
			}
			if (this._len - num < tokenString.Length)
			{
				return false;
			}
			if (string.Compare(this._sqlstatement, num, tokenString, 0, tokenString.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._idx = 0;
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x04001B07 RID: 6919
		private readonly StringBuilder _token;

		// Token: 0x04001B08 RID: 6920
		private readonly string _sqlstatement;

		// Token: 0x04001B09 RID: 6921
		private readonly char _quote;

		// Token: 0x04001B0A RID: 6922
		private readonly char _escape;

		// Token: 0x04001B0B RID: 6923
		private int _len;

		// Token: 0x04001B0C RID: 6924
		private int _idx;
	}
}
