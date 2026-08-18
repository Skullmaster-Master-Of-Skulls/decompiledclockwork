using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x020000B0 RID: 176
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WmfPlaceableFileHeader
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002576F File Offset: 0x0002396F
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00025777 File Offset: 0x00023977
		public int Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00025780 File Offset: 0x00023980
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00025788 File Offset: 0x00023988
		public short Hmf
		{
			get
			{
				return this.hmf;
			}
			set
			{
				this.hmf = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00025791 File Offset: 0x00023991
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00025799 File Offset: 0x00023999
		public short BboxLeft
		{
			get
			{
				return this.bboxLeft;
			}
			set
			{
				this.bboxLeft = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000257A2 File Offset: 0x000239A2
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x000257AA File Offset: 0x000239AA
		public short BboxTop
		{
			get
			{
				return this.bboxTop;
			}
			set
			{
				this.bboxTop = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000257B3 File Offset: 0x000239B3
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x000257BB File Offset: 0x000239BB
		public short BboxRight
		{
			get
			{
				return this.bboxRight;
			}
			set
			{
				this.bboxRight = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000257C4 File Offset: 0x000239C4
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x000257CC File Offset: 0x000239CC
		public short BboxBottom
		{
			get
			{
				return this.bboxBottom;
			}
			set
			{
				this.bboxBottom = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000257D5 File Offset: 0x000239D5
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x000257DD File Offset: 0x000239DD
		public short Inch
		{
			get
			{
				return this.inch;
			}
			set
			{
				this.inch = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000257E6 File Offset: 0x000239E6
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x000257EE File Offset: 0x000239EE
		public int Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000257F7 File Offset: 0x000239F7
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x000257FF File Offset: 0x000239FF
		public short Checksum
		{
			get
			{
				return this.checksum;
			}
			set
			{
				this.checksum = value;
			}
		}

		// Token: 0x04000952 RID: 2386
		private int key = -1698247209;

		// Token: 0x04000953 RID: 2387
		private short hmf;

		// Token: 0x04000954 RID: 2388
		private short bboxLeft;

		// Token: 0x04000955 RID: 2389
		private short bboxTop;

		// Token: 0x04000956 RID: 2390
		private short bboxRight;

		// Token: 0x04000957 RID: 2391
		private short bboxBottom;

		// Token: 0x04000958 RID: 2392
		private short inch;

		// Token: 0x04000959 RID: 2393
		private int reserved;

		// Token: 0x0400095A RID: 2394
		private short checksum;
	}
}
