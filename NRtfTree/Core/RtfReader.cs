using System;
using System.IO;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x02000011 RID: 17
	public class RtfReader
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00005C92 File Offset: 0x00003E92
		public RtfReader(SarParser reader)
		{
			this.reader = reader;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public int LoadRtfFile(string path)
		{
			int result = 0;
			this.rtf = new StreamReader(path);
			this.lex = new RtfLex(this.rtf);
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public int LoadRtfText(string text)
		{
			int result = 0;
			this.rtf = new StringReader(text);
			this.lex = new RtfLex(this.rtf);
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005D04 File Offset: 0x00003F04
		public int Parse()
		{
			int result = 0;
			this.reader.StartRtfDocument();
			this.tok = this.lex.NextToken();
			while (this.tok.Type != RtfTokenType.Eof)
			{
				switch (this.tok.Type)
				{
				case RtfTokenType.Keyword:
					this.reader.RtfKeyword(this.tok.Key, this.tok.HasParameter, this.tok.Parameter);
					break;
				case RtfTokenType.Control:
					this.reader.RtfControl(this.tok.Key, this.tok.HasParameter, this.tok.Parameter);
					break;
				case RtfTokenType.Text:
					this.reader.RtfText(this.tok.Key);
					break;
				case RtfTokenType.Eof:
					goto IL_E5;
				case RtfTokenType.GroupStart:
					this.reader.StartRtfGroup();
					break;
				case RtfTokenType.GroupEnd:
					this.reader.EndRtfGroup();
					break;
				default:
					goto IL_E5;
				}
				IL_E7:
				this.tok = this.lex.NextToken();
				continue;
				IL_E5:
				result = -1;
				goto IL_E7;
			}
			this.reader.EndRtfDocument();
			this.rtf.Close();
			return result;
		}

		// Token: 0x0400004D RID: 77
		private TextReader rtf;

		// Token: 0x0400004E RID: 78
		private RtfLex lex;

		// Token: 0x0400004F RID: 79
		private RtfToken tok;

		// Token: 0x04000050 RID: 80
		private SarParser reader;
	}
}
