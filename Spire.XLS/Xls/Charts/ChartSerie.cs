using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x020001A6 RID: 422
	public class ChartSerie : XlsChartSerie
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x000CC8B0 File Offset: 0x000CB8B0
		internal ChartSerie(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x000CC8C8 File Offset: 0x000CB8C8
		internal ChartSerie(spr\u2158 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1, A_2, ref A_3)
		{
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x000CC8E0 File Offset: 0x000CB8E0
		public new ChartDataPointsCollection DataPoints
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							break;
						}
						this.m_dataPoints = new ChartDataPointsCollection((spr\u2158)base.ReservedHandle, this);
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.m_dataPoints != null)
					{
						break;
					}
					num = 2;
				}
				IL_7B:
				return (ChartDataPointsCollection)this.m_dataPoints;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x000CC978 File Offset: 0x000CB978
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x000CC9C0 File Offset: 0x000CB9C0
		public new CellRange CategoryLabels
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (CellRange)base.CategoryLabels;
			}
			set
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
				base.CategoryLabels = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x000CCA04 File Offset: 0x000CBA04
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x000CCA4C File Offset: 0x000CBA4C
		public new CellRange Bubbles
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
				return (CellRange)base.Bubbles;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				base.Bubbles = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000CCA90 File Offset: 0x000CBA90
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000CCAD8 File Offset: 0x000CBAD8
		public new CellRange Values
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (CellRange)base.Values;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				base.Values = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000CCB1C File Offset: 0x000CBB1C
		public new ChartSerieDataFormat Format
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartSerieDataFormat)base.Format;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000CCB64 File Offset: 0x000CBB64
		public ChartSerieDataFormat DataFormat
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.Format;
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000CCBA8 File Offset: 0x000CBBA8
		public new CellRange GetSerieNameRange()
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
			return (CellRange)base.GetSerieNameRange();
		}
	}
}
