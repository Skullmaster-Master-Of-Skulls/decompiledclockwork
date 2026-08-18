using System;
using System.IO;
using System.Text;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x0200000C RID: 12
	public class RtfLex
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00004E2D File Offset: 0x0000302D
		public RtfLex(TextReader rtfReader)
		{
			this.rtf = rtfReader;
			this.keysb = new StringBuilder();
			this.parsb = new StringBuilder();
			this.c = this.rtf.Read();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004E64 File Offset: 0x00003064
		public RtfToken NextToken()
		{
			RtfToken rtfToken = new RtfToken();
			while (this.c == 13 || this.c == 10 || this.c == 9 || this.c == 0)
			{
				this.c = this.rtf.Read();
			}
			if (this.c != -1)
			{
				int num = this.c;
				if (num != 92)
				{
					switch (num)
					{
					case 123:
						rtfToken.Type = RtfTokenType.GroupStart;
						this.c = this.rtf.Read();
						return rtfToken;
					case 125:
						rtfToken.Type = RtfTokenType.GroupEnd;
						this.c = this.rtf.Read();
						return rtfToken;
					}
					rtfToken.Type = RtfTokenType.Text;
					this.parseText(rtfToken);
				}
				else
				{
					this.parseKeyword(rtfToken);
				}
			}
			else
			{
				rtfToken.Type = RtfTokenType.Eof;
			}
			return rtfToken;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004F34 File Offset: 0x00003134
		private void parseKeyword(RtfToken token)
		{
			this.keysb.Length = 0;
			this.parsb.Length = 0;
			bool flag = false;
			this.c = this.rtf.Read();
			if (!char.IsLetter((char)this.c))
			{
				if (this.c == 92 || this.c == 123 || this.c == 125)
				{
					token.Type = RtfTokenType.Text;
					token.Key = ((char)this.c).ToString();
				}
				else
				{
					token.Type = RtfTokenType.Control;
					token.Key = ((char)this.c).ToString();
					if (token.Key == "'")
					{
						string text = "";
						text += (char)this.rtf.Read();
						text += (char)this.rtf.Read();
						token.HasParameter = true;
						token.Parameter = Convert.ToInt32(text, 16);
					}
				}
				this.c = this.rtf.Read();
				return;
			}
			while (char.IsLetter((char)this.c))
			{
				this.keysb.Append((char)this.c);
				this.c = this.rtf.Read();
			}
			token.Type = RtfTokenType.Keyword;
			token.Key = this.keysb.ToString();
			if (char.IsDigit((char)this.c) || this.c == 45)
			{
				token.HasParameter = true;
				if (this.c == 45)
				{
					flag = true;
					this.c = this.rtf.Read();
				}
				while (char.IsDigit((char)this.c))
				{
					this.parsb.Append((char)this.c);
					this.c = this.rtf.Read();
				}
				int num = Convert.ToInt32(this.parsb.ToString());
				if (flag)
				{
					num = -num;
				}
				token.Parameter = num;
			}
			if (this.c == 32)
			{
				this.c = this.rtf.Read();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000513C File Offset: 0x0000333C
		private void parseText(RtfToken token)
		{
			this.keysb.Length = 0;
			while (this.c != 92 && this.c != 125 && this.c != 123 && this.c != -1)
			{
				this.keysb.Append((char)this.c);
				this.c = this.rtf.Read();
				while (this.c == 13 || this.c == 10 || this.c == 9 || this.c == 0)
				{
					this.c = this.rtf.Read();
				}
			}
			token.Key = this.keysb.ToString();
		}

		// Token: 0x0400003A RID: 58
		private const int Eof = -1;

		// Token: 0x0400003B RID: 59
		private TextReader rtf;

		// Token: 0x0400003C RID: 60
		private StringBuilder keysb;

		// Token: 0x0400003D RID: 61
		private StringBuilder parsb;

		// Token: 0x0400003E RID: 62
		private int c;
	}
}
