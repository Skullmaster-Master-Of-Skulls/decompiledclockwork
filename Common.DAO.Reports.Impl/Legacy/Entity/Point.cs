using System;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200001D RID: 29
	public class Point
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00028764 File Offset: 0x00026964
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0002876C File Offset: 0x0002696C
		public int X { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00028775 File Offset: 0x00026975
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0002877D File Offset: 0x0002697D
		public int Y { get; set; }

		// Token: 0x06000214 RID: 532 RVA: 0x0000834B File Offset: 0x0000654B
		public Point()
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00028786 File Offset: 0x00026986
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
