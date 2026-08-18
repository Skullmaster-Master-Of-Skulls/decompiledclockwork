using System;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000010 RID: 16
	public class RtfParFormat
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005C57 File Offset: 0x00003E57
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005C5F File Offset: 0x00003E5F
		public TextAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005C68 File Offset: 0x00003E68
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005C70 File Offset: 0x00003E70
		public float LeftIndentation
		{
			get
			{
				return this.leftIndentation;
			}
			set
			{
				this.leftIndentation = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005C79 File Offset: 0x00003E79
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005C81 File Offset: 0x00003E81
		public float RightIndentation
		{
			get
			{
				return this.rightIndentation;
			}
			set
			{
				this.rightIndentation = value;
			}
		}

		// Token: 0x0400004A RID: 74
		private TextAlignment alignment;

		// Token: 0x0400004B RID: 75
		private float leftIndentation;

		// Token: 0x0400004C RID: 76
		private float rightIndentation;
	}
}
