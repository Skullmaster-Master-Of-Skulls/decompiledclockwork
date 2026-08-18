using System;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000B3 RID: 179
	public sealed class Blend
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00025A38 File Offset: 0x00023C38
		public Blend()
		{
			this.factors = new float[1];
			this.positions = new float[1];
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00025A58 File Offset: 0x00023C58
		public Blend(int count)
		{
			this.factors = new float[count];
			this.positions = new float[count];
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00025A78 File Offset: 0x00023C78
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00025A80 File Offset: 0x00023C80
		public float[] Factors
		{
			get
			{
				return this.factors;
			}
			set
			{
				this.factors = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00025A89 File Offset: 0x00023C89
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00025A91 File Offset: 0x00023C91
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

		// Token: 0x0400095F RID: 2399
		private float[] factors;

		// Token: 0x04000960 RID: 2400
		private float[] positions;
	}
}
