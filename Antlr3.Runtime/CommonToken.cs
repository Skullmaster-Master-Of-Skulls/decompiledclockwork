using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Antlr.Runtime
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class CommonToken : IToken
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003FC1 File Offset: 0x000021C1
		public CommonToken()
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FD7 File Offset: 0x000021D7
		public CommonToken(int type)
		{
			this.type = type;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003FF4 File Offset: 0x000021F4
		public CommonToken(ICharStream input, int type, int channel, int start, int stop)
		{
			this.input = input;
			this.type = type;
			this.channel = channel;
			this.start = start;
			this.stop = stop;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000402F File Offset: 0x0000222F
		public CommonToken(int type, string text)
		{
			this.type = type;
			this.channel = 0;
			this.text = text;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000405C File Offset: 0x0000225C
		public CommonToken(IToken oldToken)
		{
			this.text = oldToken.Text;
			this.type = oldToken.Type;
			this.line = oldToken.Line;
			this.index = oldToken.TokenIndex;
			this.charPositionInLine = oldToken.CharPositionInLine;
			this.channel = oldToken.Channel;
			this.input = oldToken.InputStream;
			if (oldToken is CommonToken)
			{
				this.start = ((CommonToken)oldToken).start;
				this.stop = ((CommonToken)oldToken).stop;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000040FC File Offset: 0x000022FC
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00004169 File Offset: 0x00002369
		public string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				if (this.input == null)
				{
					return null;
				}
				if (this.start <= this.stop && this.stop < this.input.Count)
				{
					return this.input.Substring(this.start, this.stop - this.start + 1);
				}
				return "<EOF>";
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004172 File Offset: 0x00002372
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000417A File Offset: 0x0000237A
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004183 File Offset: 0x00002383
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000418B File Offset: 0x0000238B
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004194 File Offset: 0x00002394
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000419C File Offset: 0x0000239C
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000041A5 File Offset: 0x000023A5
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000041AD File Offset: 0x000023AD
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000041B6 File Offset: 0x000023B6
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000041BE File Offset: 0x000023BE
		public int StartIndex
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000041C7 File Offset: 0x000023C7
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000041CF File Offset: 0x000023CF
		public int StopIndex
		{
			get
			{
				return this.stop;
			}
			set
			{
				this.stop = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000041D8 File Offset: 0x000023D8
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000041E0 File Offset: 0x000023E0
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000041E9 File Offset: 0x000023E9
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000041F1 File Offset: 0x000023F1
		public ICharStream InputStream
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = value;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000041FC File Offset: 0x000023FC
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
				text2 = Regex.Replace(text2, "\n", "\\\\n");
				text2 = Regex.Replace(text2, "\r", "\\\\r");
				text2 = Regex.Replace(text2, "\t", "\\\\t");
			}
			else
			{
				text2 = "<no text>";
			}
			return string.Concat(new object[]
			{
				"[@",
				this.TokenIndex,
				",",
				this.start,
				":",
				this.stop,
				"='",
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

		// Token: 0x0600010D RID: 269 RVA: 0x0000432D File Offset: 0x0000252D
		[OnSerializing]
		internal void OnSerializing(StreamingContext context)
		{
			if (this.text == null)
			{
				this.text = this.Text;
			}
		}

		// Token: 0x0400002B RID: 43
		private int type;

		// Token: 0x0400002C RID: 44
		private int line;

		// Token: 0x0400002D RID: 45
		private int charPositionInLine = -1;

		// Token: 0x0400002E RID: 46
		private int channel;

		// Token: 0x0400002F RID: 47
		[NonSerialized]
		private ICharStream input;

		// Token: 0x04000030 RID: 48
		private string text;

		// Token: 0x04000031 RID: 49
		private int index = -1;

		// Token: 0x04000032 RID: 50
		private int start;

		// Token: 0x04000033 RID: 51
		private int stop;
	}
}
