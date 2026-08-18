using System;
using System.Drawing;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000007 RID: 7
	public class RtfCharFormat
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003DD2 File Offset: 0x00001FD2
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003DDA File Offset: 0x00001FDA
		public bool Bold
		{
			get
			{
				return this.bold;
			}
			set
			{
				this.bold = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003DE3 File Offset: 0x00001FE3
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003DEB File Offset: 0x00001FEB
		public bool Italic
		{
			get
			{
				return this.italic;
			}
			set
			{
				this.italic = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003DF4 File Offset: 0x00001FF4
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003DFC File Offset: 0x00001FFC
		public bool Underline
		{
			get
			{
				return this.underline;
			}
			set
			{
				this.underline = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003E05 File Offset: 0x00002005
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003E0D File Offset: 0x0000200D
		public string Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003E16 File Offset: 0x00002016
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003E1E File Offset: 0x0000201E
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003E27 File Offset: 0x00002027
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003E2F File Offset: 0x0000202F
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x04000026 RID: 38
		private bool bold;

		// Token: 0x04000027 RID: 39
		private bool italic;

		// Token: 0x04000028 RID: 40
		private bool underline;

		// Token: 0x04000029 RID: 41
		private string font = "Arial";

		// Token: 0x0400002A RID: 42
		private int size = 10;

		// Token: 0x0400002B RID: 43
		private Color color = Color.Black;
	}
}
