using System;
using System.Text;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x02000168 RID: 360
	public class Hyphen
	{
		// Token: 0x06000DAF RID: 3503 RVA: 0x0004AA7B File Offset: 0x00049A7B
		internal Hyphen(string pre, string no, string post)
		{
			this.preBreak = pre;
			this.noBreak = no;
			this.postBreak = post;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0004AA98 File Offset: 0x00049A98
		internal Hyphen(string pre)
		{
			this.preBreak = pre;
			this.noBreak = null;
			this.postBreak = null;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0004AAB8 File Offset: 0x00049AB8
		public override string ToString()
		{
			if (this.noBreak == null && this.postBreak == null && this.preBreak != null && this.preBreak.Equals("-"))
			{
				return "-";
			}
			StringBuilder stringBuilder = new StringBuilder("{");
			stringBuilder.Append(this.preBreak);
			stringBuilder.Append("}{");
			stringBuilder.Append(this.postBreak);
			stringBuilder.Append("}{");
			stringBuilder.Append(this.noBreak);
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x04000A08 RID: 2568
		public string preBreak;

		// Token: 0x04000A09 RID: 2569
		public string noBreak;

		// Token: 0x04000A0A RID: 2570
		public string postBreak;
	}
}
