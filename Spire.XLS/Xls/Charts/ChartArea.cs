using System;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x02000188 RID: 392
	public class ChartArea : XlsChartFrameFormat
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x000BB58C File Offset: 0x000BA58C
		internal ChartArea(spr\u2158 A_0, object A_1) : base(A_0, A_1, true, false, true)
		{
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x000BB5A4 File Offset: 0x000BA5A4
		public new ChartBorder Border
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
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_50;
					}
					if (this.m_border == null)
					{
						num = 2;
						continue;
					}
					break;
					IL_50:
					this.m_border = new ChartBorder((spr\u2158)base.ReservedHandle, this);
					num = 0;
				}
				IL_7B:
				return (ChartBorder)this.m_border;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x000BB63C File Offset: 0x000BA63C
		public new ChartInterior Interior
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7B;
					case 1:
						goto IL_50;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					}
					if (this.ᜂ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_50:
					this.ᜂ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
					num = 0;
				}
				IL_7B:
				return (ChartInterior)this.ᜂ;
			}
		}
	}
}
