using System;
using System.Drawing;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A6D RID: 2669
	internal class Border
	{
		// Token: 0x060066F8 RID: 26360 RVA: 0x00181941 File Offset: 0x0017FB41
		public Border(Range range, BorderKind kind)
		{
			this.range = range;
			this.kind = kind;
		}

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x060066F9 RID: 26361 RVA: 0x00181957 File Offset: 0x0017FB57
		// (set) Token: 0x060066FA RID: 26362 RVA: 0x0018195F File Offset: 0x0017FB5F
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
				this.range.SetBorderColor(value, this.kind);
			}
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x060066FB RID: 26363 RVA: 0x0018197A File Offset: 0x0017FB7A
		// (set) Token: 0x060066FC RID: 26364 RVA: 0x00181982 File Offset: 0x0017FB82
		public BorderStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
				this.range.SetBorderStyle(value, this.kind);
			}
		}

		// Token: 0x040019B6 RID: 6582
		private readonly Range range;

		// Token: 0x040019B7 RID: 6583
		private readonly BorderKind kind;

		// Token: 0x040019B8 RID: 6584
		private Color color;

		// Token: 0x040019B9 RID: 6585
		private BorderStyle style;
	}
}
