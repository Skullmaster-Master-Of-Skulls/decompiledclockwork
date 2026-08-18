using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x0200019C RID: 412
	public class ChartWallOrFloor : XlsChartWallOrFloor
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x000C60D8 File Offset: 0x000C50D8
		internal ChartWallOrFloor(spr\u2158 A_0, object A_1, bool A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x000C60F0 File Offset: 0x000C50F0
		internal ChartWallOrFloor(spr\u2158 A_0, object A_1, bool A_2, IList<BiffRecordRaw> A_3, ref int A_4) : base(A_0, A_1, A_2, A_3, ref A_4)
		{
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000C610C File Offset: 0x000C510C
		public new ChartBorder Border
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜀ = new ChartBorder((spr\u2158)base.ReservedHandle, this);
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (true)
					{
					}
					if (this.ᜀ != null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜀ;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000C619C File Offset: 0x000C519C
		public new ChartInterior Interior
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7B;
					case 2:
						this.ᜁ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜁ != null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜁ;
			}
		}

		// Token: 0x04000F02 RID: 3842
		private bool \u2460\u00A1\u009D\u009C;

		// Token: 0x04000F03 RID: 3843
		private float \u25D8\u00AB\u00A1\u0092;

		// Token: 0x04000F04 RID: 3844
		private bool \u25D9\u00A7\u008B\u00AB;

		// Token: 0x04000F05 RID: 3845
		private new ChartBorder ᜀ;

		// Token: 0x04000F06 RID: 3846
		private string \u25D9\u009A\u009B\u0086;

		// Token: 0x04000F07 RID: 3847
		private new ChartInterior ᜁ;
	}
}
