using System;

namespace Antlr.Runtime
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class ClassicToken : IToken
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public ClassicToken(int type)
		{
			this.type = type;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public ClassicToken(IToken oldToken)
		{
			this.text = oldToken.Text;
			this.type = oldToken.Type;
			this.line = oldToken.Line;
			this.charPositionInLine = oldToken.CharPositionInLine;
			this.channel = oldToken.Channel;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E17 File Offset: 0x00002017
		public ClassicToken(int type, string text)
		{
			this.type = type;
			this.text = text;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003E2D File Offset: 0x0000202D
		public ClassicToken(int type, string text, int channel)
		{
			this.type = type;
			this.text = text;
			this.channel = channel;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003E4A File Offset: 0x0000204A
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003E52 File Offset: 0x00002052
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003E5B File Offset: 0x0000205B
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003E63 File Offset: 0x00002063
		public int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003E6C File Offset: 0x0000206C
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003E74 File Offset: 0x00002074
		public int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003E7D File Offset: 0x0000207D
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003E85 File Offset: 0x00002085
		public int CharPositionInLine
		{
			get
			{
				return this.charPositionInLine;
			}
			set
			{
				this.charPositionInLine = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003E8E File Offset: 0x0000208E
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003E96 File Offset: 0x00002096
		public int Channel
		{
			get
			{
				return this.channel;
			}
			set
			{
				this.channel = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003E9F File Offset: 0x0000209F
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003EA2 File Offset: 0x000020A2
		public int StartIndex
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003EA4 File Offset: 0x000020A4
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00003EA7 File Offset: 0x000020A7
		public int StopIndex
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003EA9 File Offset: 0x000020A9
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00003EB1 File Offset: 0x000020B1
		public int TokenIndex
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003EBA File Offset: 0x000020BA
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00003EBD File Offset: 0x000020BD
		public ICharStream InputStream
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003EC0 File Offset: 0x000020C0
		public override string ToString()
		{
			string text = "";
			if (this.channel > 0)
			{
				text = ",channel=" + this.channel;
			}
			string text2 = this.Text;
			if (text2 != null)
			{
				text2 = text2.Replace("\n", "\\\\n");
				text2 = text2.Replace("\r", "\\\\r");
				text2 = text2.Replace("\t", "\\\\t");
			}
			else
			{
				text2 = "<no text>";
			}
			return string.Concat(new object[]
			{
				"[@",
				this.TokenIndex,
				",'",
				text2,
				"',<",
				this.type,
				">",
				text,
				",",
				this.line,
				":",
				this.CharPositionInLine,
				"]"
			});
		}

		// Token: 0x04000025 RID: 37
		private string text;

		// Token: 0x04000026 RID: 38
		private int type;

		// Token: 0x04000027 RID: 39
		private int line;

		// Token: 0x04000028 RID: 40
		private int charPositionInLine;

		// Token: 0x04000029 RID: 41
		private int channel;

		// Token: 0x0400002A RID: 42
		private int index;
	}
}
