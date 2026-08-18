using System;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x0200020B RID: 523
	internal sealed class CStringTokenizer
	{
		// Token: 0x06001CDB RID: 7387 RVA: 0x0026AEC8 File Offset: 0x0026A2C8
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

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0026AF28 File Offset: 0x0026A328
		internal int CurrentPosition
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0026AF48 File Offset: 0x0026A348
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
								char c2 = c;
								if (c2 == ',')
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

		// Token: 0x06001CDE RID: 7390 RVA: 0x0026B0E8 File Offset: 0x0026A4E8
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

		// Token: 0x06001CDF RID: 7391 RVA: 0x0026B138 File Offset: 0x0026A538
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

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0026B1C8 File Offset: 0x0026A5C8
		private bool IsValidNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.' || ch == '$' || ch == '#' || ch == '@' || ch == '~' || ch == '`' || ch == '%' || ch == '^' || ch == '&' || ch == '|';
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0026B228 File Offset: 0x0026A628
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

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0026B268 File Offset: 0x0026A668
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

		// Token: 0x04001099 RID: 4249
		private readonly StringBuilder _token;

		// Token: 0x0400109A RID: 4250
		private readonly string _sqlstatement;

		// Token: 0x0400109B RID: 4251
		private readonly char _quote;

		// Token: 0x0400109C RID: 4252
		private readonly char _escape;

		// Token: 0x0400109D RID: 4253
		private int _len;

		// Token: 0x0400109E RID: 4254
		private int _idx;
	}
}
