using System;
using System.Collections;

namespace OracleInternal.Network
{
	// Token: 0x0200015A RID: 346
	internal sealed class NVTokens
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00092718 File Offset: 0x00090918
		internal int Token
		{
			get
			{
				if (this.m_tkType == null)
				{
					throw new NetworkException(303);
				}
				if (this.m_tkPos < this.m_numTokens)
				{
					return (int)this.m_tkType[this.m_tkPos];
				}
				throw new NetworkException(351);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00092768 File Offset: 0x00090968
		internal string Literal
		{
			get
			{
				if (this.m_tkValue == null)
				{
					throw new NetworkException(303);
				}
				if (this.m_tkPos < this.m_numTokens)
				{
					return (string)this.m_tkValue[this.m_tkPos];
				}
				throw new NetworkException(351);
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000927C0 File Offset: 0x000909C0
		internal NVTokens()
		{
			this.m_tkType = null;
			this.m_tkValue = null;
			this.m_numTokens = 0;
			this.m_tkPos = 0;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000927E4 File Offset: 0x000909E4
		private static bool IsWhiteSpace(char it)
		{
			return it == ' ' || it == '\t' || it == '\n' || it == '\r';
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00092800 File Offset: 0x00090A00
		private static string TrimWhiteSpace(string it)
		{
			int length = it.Length;
			int i = 0;
			int num = length;
			while (i < length)
			{
				if (!NVTokens.IsWhiteSpace(it[i]))
				{
					break;
				}
				i++;
			}
			while (i < num && NVTokens.IsWhiteSpace(it[num - 1]))
			{
				num--;
			}
			return it.Substring(i, num - i);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00092854 File Offset: 0x00090A54
		internal bool ParseTokens(string nvString)
		{
			this.m_numTokens = 0;
			this.m_tkPos = 0;
			this.m_tkType = ArrayList.Synchronized(new ArrayList(25));
			this.m_tkValue = ArrayList.Synchronized(new ArrayList(25));
			int length = nvString.Length;
			int i = 0;
			while (i < length)
			{
				while (i < length && NVTokens.IsWhiteSpace(nvString[i]))
				{
					i++;
				}
				if (i < length)
				{
					char c = nvString[i];
					switch (c)
					{
					case '(':
						this.AddToken(1, '(');
						i++;
						continue;
					case ')':
						this.AddToken(2, ')');
						i++;
						continue;
					case '*':
					case '+':
						break;
					case ',':
						this.AddToken(3, ',');
						i++;
						continue;
					default:
						if (c == '=')
						{
							this.AddToken(4, '=');
							i++;
							continue;
						}
						break;
					}
					int num = i;
					int num2 = -1;
					bool flag = false;
					char c2 = '"';
					if (nvString[i] == '\'' || nvString[i] == '"')
					{
						flag = true;
						c2 = nvString[i];
						i++;
						num = i;
					}
					while (i < length)
					{
						if (nvString[i] == '\\')
						{
							i += 2;
						}
						else
						{
							if (flag)
							{
								if (nvString[i] == c2)
								{
									num2 = i;
									i++;
									break;
								}
							}
							else
							{
								if (nvString[i] == '\'' || nvString[i] == '"')
								{
									throw new NetworkException(303);
								}
								if (nvString[i] == '(' || nvString[i] == ')' || nvString[i] == ',' || nvString[i] == '=')
								{
									num2 = i;
									break;
								}
							}
							i++;
						}
					}
					if (num2 == -1)
					{
						num2 = i;
					}
					this.AddToken(8, NVTokens.TrimWhiteSpace(nvString.Substring(num, num2 - num)));
				}
			}
			this.AddToken(9, '%');
			return true;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00092A20 File Offset: 0x00090C20
		internal string PopLiteral()
		{
			if (this.m_tkValue == null)
			{
				throw new NetworkException(303);
			}
			if (this.m_tkPos < this.m_numTokens)
			{
				return (string)this.m_tkValue[this.m_tkPos++];
			}
			throw new NetworkException(351);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00092A80 File Offset: 0x00090C80
		internal void EatToken()
		{
			if (this.m_tkPos < this.m_numTokens)
			{
				this.m_tkPos++;
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00092AA0 File Offset: 0x00090CA0
		private void AddToken(int tk, char tk_char)
		{
			this.AddToken(tk, Convert.ToString(tk_char));
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00092AB0 File Offset: 0x00090CB0
		private void AddToken(int tk, string tk_val)
		{
			this.m_tkType.Add(tk);
			this.m_tkValue.Add(tk_val);
			this.m_numTokens++;
		}

		// Token: 0x04000F2F RID: 3887
		internal const int TKN_NONE = 0;

		// Token: 0x04000F30 RID: 3888
		internal const int TKN_LPAREN = 1;

		// Token: 0x04000F31 RID: 3889
		internal const int TKN_RPAREN = 2;

		// Token: 0x04000F32 RID: 3890
		internal const int TKN_COMMA = 3;

		// Token: 0x04000F33 RID: 3891
		internal const int TKN_EQUAL = 4;

		// Token: 0x04000F34 RID: 3892
		internal const int TKN_LITERAL = 8;

		// Token: 0x04000F35 RID: 3893
		internal const int TKN_EOS = 9;

		// Token: 0x04000F36 RID: 3894
		private const char TKN_LPAREN_VALUE = '(';

		// Token: 0x04000F37 RID: 3895
		private const char TKN_RPAREN_VALUE = ')';

		// Token: 0x04000F38 RID: 3896
		private const char TKN_COMMA_VALUE = ',';

		// Token: 0x04000F39 RID: 3897
		private const char TKN_EQUAL_VALUE = '=';

		// Token: 0x04000F3A RID: 3898
		private const char TKN_BKSLASH_VALUE = '\\';

		// Token: 0x04000F3B RID: 3899
		private const char TKN_DQUOTE_VALUE = '"';

		// Token: 0x04000F3C RID: 3900
		private const char TKN_SQUOTE_VALUE = '\'';

		// Token: 0x04000F3D RID: 3901
		private const char TKN_EOS_VALUE = '%';

		// Token: 0x04000F3E RID: 3902
		private const char TKN_SPC_VALUE = ' ';

		// Token: 0x04000F3F RID: 3903
		private const char TKN_TAB_VALUE = '\t';

		// Token: 0x04000F40 RID: 3904
		private const char TKN_LF_VALUE = '\n';

		// Token: 0x04000F41 RID: 3905
		private const char TKN_CR_VALUE = '\r';

		// Token: 0x04000F42 RID: 3906
		private ArrayList m_tkType;

		// Token: 0x04000F43 RID: 3907
		private ArrayList m_tkValue;

		// Token: 0x04000F44 RID: 3908
		private int m_numTokens;

		// Token: 0x04000F45 RID: 3909
		private int m_tkPos;
	}
}
