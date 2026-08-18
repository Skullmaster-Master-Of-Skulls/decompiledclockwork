using System;

namespace AutoComboBox.MyControls.ExtendedRichTextBox
{
	// Token: 0x02000102 RID: 258
	public class ScrollBarInformation
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x0004E2D4 File Offset: 0x0004D2D4
		public ScrollBarInformation()
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0004E304 File Offset: 0x0004D304
		public ScrollBarInformation(int min, int max, int page, int pos, int trackpos)
		{
			this.nMin = min;
			this.nMax = max;
			this.nPage = page;
			this.nPos = pos;
			this.nTrackPos = trackpos;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0004E364 File Offset: 0x0004D364
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0004E37C File Offset: 0x0004D37C
		public int Minimum
		{
			get
			{
				return this.nMin;
			}
			set
			{
				this.nMin = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0004E388 File Offset: 0x0004D388
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0004E3A0 File Offset: 0x0004D3A0
		public int Maximum
		{
			get
			{
				return this.nMax;
			}
			set
			{
				this.nMax = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0004E3AC File Offset: 0x0004D3AC
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x0004E3C4 File Offset: 0x0004D3C4
		public int Page
		{
			get
			{
				return this.nPage;
			}
			set
			{
				this.nPage = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0004E3D0 File Offset: 0x0004D3D0
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0004E3E8 File Offset: 0x0004D3E8
		public int Position
		{
			get
			{
				return this.nPos;
			}
			set
			{
				this.nPos = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0004E3F4 File Offset: 0x0004D3F4
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0004E40C File Offset: 0x0004D40C
		public int TrackPosition
		{
			get
			{
				return this.nTrackPos;
			}
			set
			{
				this.nTrackPos = value;
			}
		}

		// Token: 0x0400076C RID: 1900
		private int nMin = 0;

		// Token: 0x0400076D RID: 1901
		private int nMax = 0;

		// Token: 0x0400076E RID: 1902
		private int nPage = 0;

		// Token: 0x0400076F RID: 1903
		private int nPos = 0;

		// Token: 0x04000770 RID: 1904
		private int nTrackPos = 0;
	}
}
