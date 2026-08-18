using System;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000B5 RID: 181
	public sealed class ColorBlend
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x00025A9A File Offset: 0x00023C9A
		public ColorBlend()
		{
			this.colors = new Color[1];
			this.positions = new float[1];
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00025ABA File Offset: 0x00023CBA
		public ColorBlend(int count)
		{
			this.colors = new Color[count];
			this.positions = new float[count];
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00025ADA File Offset: 0x00023CDA
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00025AE2 File Offset: 0x00023CE2
		public Color[] Colors
		{
			get
			{
				return this.colors;
			}
			set
			{
				this.colors = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00025AEB File Offset: 0x00023CEB
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00025AF3 File Offset: 0x00023CF3
		public float[] Positions
		{
			get
			{
				return this.positions;
			}
			set
			{
				this.positions = value;
			}
		}

		// Token: 0x04000967 RID: 2407
		private Color[] colors;

		// Token: 0x04000968 RID: 2408
		private float[] positions;
	}
}
