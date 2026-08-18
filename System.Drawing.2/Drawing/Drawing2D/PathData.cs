using System;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000CC RID: 204
	public sealed class PathData
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00029227 File Offset: 0x00027427
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0002922F File Offset: 0x0002742F
		public PointF[] Points
		{
			get
			{
				return this.points;
			}
			set
			{
				this.points = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00029238 File Offset: 0x00027438
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00029240 File Offset: 0x00027440
		public byte[] Types
		{
			get
			{
				return this.types;
			}
			set
			{
				this.types = value;
			}
		}

		// Token: 0x040009F2 RID: 2546
		private PointF[] points;

		// Token: 0x040009F3 RID: 2547
		private byte[] types;
	}
}
