using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200011F RID: 287
	public sealed class AmbientProperties
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00018365 File Offset: 0x00016565
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0001836D File Offset: 0x0001656D
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00018376 File Offset: 0x00016576
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001837E File Offset: 0x0001657E
		public Cursor Cursor
		{
			get
			{
				return this.cursor;
			}
			set
			{
				this.cursor = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00018387 File Offset: 0x00016587
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001838F File Offset: 0x0001658F
		public Font Font
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

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00018398 File Offset: 0x00016598
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x000183A0 File Offset: 0x000165A0
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				this.foreColor = value;
			}
		}

		// Token: 0x040005CD RID: 1485
		private Color backColor;

		// Token: 0x040005CE RID: 1486
		private Color foreColor;

		// Token: 0x040005CF RID: 1487
		private Cursor cursor;

		// Token: 0x040005D0 RID: 1488
		private Font font;
	}
}
