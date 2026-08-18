using System;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x020001A0 RID: 416
	public class ChartWrappedFrameFormat : XlsChartWrappedFrameFormat
	{
		// Token: 0x060014FF RID: 5375 RVA: 0x000C78FC File Offset: 0x000C68FC
		internal ChartWrappedFrameFormat(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x000C7914 File Offset: 0x000C6914
		public new ChartBorder Border
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ = new ChartBorder((spr\u2158)base.ReservedHandle, this);
						num = 1;
						continue;
					case 1:
						goto IL_7B;
					}
					IL_1C:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						if (false)
						{
						}
						if (this.ᜀ != null)
						{
							goto IL_7D;
						}
						num = 0;
						break;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜀ;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x000C79A4 File Offset: 0x000C69A4
		public new ChartInterior Interior
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_7B;
					case 2:
						this.ᜁ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
						num = 1;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						if (false)
						{
						}
						if (this.ᜁ != null)
						{
							goto IL_7D;
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

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x000C7A34 File Offset: 0x000C6A34
		public new Workbook Workbook
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.m_chart.InnerWorkbook.InnerWorkBook;
			}
		}

		// Token: 0x04000F1B RID: 3867
		private byte \u25D8\u00A7\u0082\u0091;

		// Token: 0x04000F1C RID: 3868
		private new ChartBorder ᜀ;

		// Token: 0x04000F1D RID: 3869
		private ChartInterior ᜁ;
	}
}
