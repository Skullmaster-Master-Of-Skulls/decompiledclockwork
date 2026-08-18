using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x0200005C RID: 92
	public class XlsChartShape : XlsShape, IChartShape
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x00059C6C File Offset: 0x00058C6C
		internal XlsChartShape(spr\u1DF5 A_0, object A_1, XlsChartShape A_2, Dictionary<string, string> A_3, Dictionary<int, int> A_4) : base(A_0, A_1, A_2)
		{
			this.ᜊ = A_2.ᜊ.Clone(A_3, this, A_4);
			this.m_bIsDisposed = A_2.m_bIsDisposed;
			this.ᜌ = A_2.ᜌ;
			this.\u170D = A_2.\u170D;
			this.ᜎ = A_2.ᜎ;
			this.ᜋ = A_2.ᜋ;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00059CD4 File Offset: 0x00058CD4
		internal XlsChartShape(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜊ = new spr\u23F7((spr\u2158)A_0, this);
			base.ShapeType = ExcelShapeType.Chart;
			base.BottomRow = 20;
			base.RightColumn = 10;
			this.m_bSupportOptions = false;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00059D1C File Offset: 0x00058D1C
		internal XlsChartShape(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.ShapeType = ExcelShapeType.Chart;
			this.m_bSupportOptions = false;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00059D44 File Offset: 0x00058D44
		internal XlsChart ChartObject
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
				return this.ᜊ;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00059D88 File Offset: 0x00058D88
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00059DCC File Offset: 0x00058DCC
		internal int OffsetX
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
				return this.ᜐ;
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
				this.ᜐ = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00059E10 File Offset: 0x00058E10
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00059E54 File Offset: 0x00058E54
		internal int OffsetY
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
				return this.ᜑ;
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
				this.ᜑ = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00059E98 File Offset: 0x00058E98
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00059EDC File Offset: 0x00058EDC
		internal int ExtentsX
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
				return this.\u1712;
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
				this.\u1712 = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00059F20 File Offset: 0x00058F20
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00059F64 File Offset: 0x00058F64
		internal int ExtentsY
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00059FA8 File Offset: 0x00058FA8
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00059FF0 File Offset: 0x00058FF0
		public new int Rotation
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
				return this.ᜊ.Rotation;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.Rotation = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0005A038 File Offset: 0x00059038
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0005A080 File Offset: 0x00059080
		public int Elevation
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
				return this.ᜊ.Elevation;
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
				this.ᜊ.Elevation = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0005A0C8 File Offset: 0x000590C8
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0005A110 File Offset: 0x00059110
		public int Perspective
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
				return this.ᜊ.Perspective;
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
				this.ᜊ.Perspective = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0005A158 File Offset: 0x00059158
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0005A1A0 File Offset: 0x000591A0
		public int HeightPercent
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.HeightPercent;
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
				this.ᜊ.HeightPercent = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0005A1E8 File Offset: 0x000591E8
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0005A230 File Offset: 0x00059230
		public int DepthPercent
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
				return this.ᜊ.DepthPercent;
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
				this.ᜊ.DepthPercent = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0005A278 File Offset: 0x00059278
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0005A2C0 File Offset: 0x000592C0
		public int GapDepth
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.GapDepth;
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
				this.ᜊ.GapDepth = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0005A308 File Offset: 0x00059308
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0005A350 File Offset: 0x00059350
		public bool RightAngleAxes
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
				return this.ᜊ.RightAngleAxes;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.RightAngleAxes = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0005A398 File Offset: 0x00059398
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x0005A3E0 File Offset: 0x000593E0
		public bool AutoScaling
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
				return this.ᜊ.AutoScaling;
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
				this.ᜊ.AutoScaling = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0005A428 File Offset: 0x00059428
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0005A470 File Offset: 0x00059470
		public bool WallsAndGridlines2D
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
				return this.ᜊ.WallsAndGridlines2D;
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
				this.ᜊ.WallsAndGridlines2D = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0005A4B8 File Offset: 0x000594B8
		protected internal IShapes Shapes
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
				return this.ᜊ.Shapes;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0005A500 File Offset: 0x00059500
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0005A548 File Offset: 0x00059548
		public ExcelChartType PivotChartType
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
				return this.ᜊ.PivotChartType;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.PivotChartType = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0005A590 File Offset: 0x00059590
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0005A5D8 File Offset: 0x000595D8
		public PivotTable PivotTable
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.PivotTable;
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
				this.ᜊ.PivotTable = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0005A620 File Offset: 0x00059620
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0005A668 File Offset: 0x00059668
		public bool DisplayEntireFieldButtons
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
				return this.ᜊ.DisplayEntireFieldButtons;
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
				this.ᜊ.DisplayEntireFieldButtons = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0005A6B0 File Offset: 0x000596B0
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0005A6F8 File Offset: 0x000596F8
		public bool DisplayValueFieldButtons
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
				return this.ᜊ.DisplayValueFieldButtons;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.DisplayValueFieldButtons = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0005A740 File Offset: 0x00059740
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0005A788 File Offset: 0x00059788
		public bool DisplayAxisFieldButtons
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
				return this.ᜊ.DisplayAxisFieldButtons;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.DisplayAxisFieldButtons = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0005A7D0 File Offset: 0x000597D0
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0005A818 File Offset: 0x00059818
		public bool DisplayLegendFieldButtons
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
				return this.ᜊ.DisplayLegendFieldButtons;
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
				this.ᜊ.DisplayLegendFieldButtons = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0005A860 File Offset: 0x00059860
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0005A8A8 File Offset: 0x000598A8
		public bool ShowReportFilterFieldButtons
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.ShowReportFilterFieldButtons;
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
				this.ᜊ.ShowReportFilterFieldButtons = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0005A8F0 File Offset: 0x000598F0
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0005A938 File Offset: 0x00059938
		public ExcelChartType ChartType
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
				return this.ᜊ.ChartType;
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
				this.ᜊ.ChartType = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0005A980 File Offset: 0x00059980
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0005A9C8 File Offset: 0x000599C8
		public IXLSRange DataRange
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
				return this.ᜊ.DataRange;
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
				this.ᜊ.DataRange = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0005AA10 File Offset: 0x00059A10
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0005AA58 File Offset: 0x00059A58
		public bool SeriesDataFromRange
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
				return this.ᜊ.SeriesDataFromRange;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.SeriesDataFromRange = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0005AAA0 File Offset: 0x00059AA0
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0005AAE8 File Offset: 0x00059AE8
		public string ChartTitle
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
				return this.ᜊ.ChartTitle;
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
				this.ᜊ.ChartTitle = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0005AB30 File Offset: 0x00059B30
		public IChartTextArea ChartTitleArea
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
				return this.ᜊ.ChartTitleArea;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0005AB78 File Offset: 0x00059B78
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x0005ABC0 File Offset: 0x00059BC0
		public string CategoryAxisTitle
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
				return this.ᜊ.CategoryAxisTitle;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.CategoryAxisTitle = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0005AC08 File Offset: 0x00059C08
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x0005AC50 File Offset: 0x00059C50
		public string ValueAxisTitle
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
				return this.ᜊ.ValueAxisTitle;
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
				this.ᜊ.ValueAxisTitle = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0005AC98 File Offset: 0x00059C98
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0005ACE0 File Offset: 0x00059CE0
		public string SecondaryCategoryAxisTitle
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
				return this.ᜊ.SecondaryCategoryAxisTitle;
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
				this.ᜊ.SecondaryCategoryAxisTitle = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0005AD28 File Offset: 0x00059D28
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0005AD70 File Offset: 0x00059D70
		public string SecondaryValueAxisTitle
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.SecondaryValueAxisTitle;
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
				this.ᜊ.SecondaryValueAxisTitle = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0005ADB8 File Offset: 0x00059DB8
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0005AE00 File Offset: 0x00059E00
		public string SeriesAxisTitle
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
				return this.ᜊ.SeriesAxisTitle;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.SeriesAxisTitle = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0005AE48 File Offset: 0x00059E48
		public IChartPageSetup PageSetup
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
				return this.ᜊ.PageSetup;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0005AE90 File Offset: 0x00059E90
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0005AED8 File Offset: 0x00059ED8
		public double XPos
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
				return this.ᜊ.XPos;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.XPos = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0005AF20 File Offset: 0x00059F20
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0005AF68 File Offset: 0x00059F68
		public double YPos
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
				return this.ᜊ.YPos;
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
				this.ᜊ.YPos = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0005AFB0 File Offset: 0x00059FB0
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0005AFF8 File Offset: 0x00059FF8
		double IChart.Width
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.Width;
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
				this.ᜊ.Width = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0005B040 File Offset: 0x0005A040
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0005B088 File Offset: 0x0005A088
		double IChart.Height
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
				return this.ᜊ.Height;
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
				this.ᜊ.Height = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0005B0D0 File Offset: 0x0005A0D0
		internal XlsChartSeries Series
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
				return this.ᜊ.Series;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0005B118 File Offset: 0x0005A118
		public IChartCategoryAxis PrimaryCategoryAxis
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
				return this.ᜊ.PrimaryCategoryAxis;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0005B160 File Offset: 0x0005A160
		public IChartValueAxis PrimaryValueAxis
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.PrimaryValueAxis;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0005B1A8 File Offset: 0x0005A1A8
		public IChartSeriesAxis PrimarySerieAxis
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.PrimarySerieAxis;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0005B1F0 File Offset: 0x0005A1F0
		public IChartCategoryAxis SecondaryCategoryAxis
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
				return this.ᜊ.SecondaryCategoryAxis;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0005B238 File Offset: 0x0005A238
		public IChartValueAxis SecondaryValueAxis
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.SecondaryValueAxis;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0005B280 File Offset: 0x0005A280
		public IChartFrameFormat ChartArea
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.ChartArea;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0005B2C8 File Offset: 0x0005A2C8
		public IChartFrameFormat PlotArea
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
				return this.ᜊ.PlotArea;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0005B310 File Offset: 0x0005A310
		public XlsChartFormatCollection PrimaryFormats
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
				return this.ᜊ.PrimaryFormats;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0005B358 File Offset: 0x0005A358
		public XlsChartFormatCollection SecondaryFormats
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
				return this.ᜊ.SecondaryFormats;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0005B3A0 File Offset: 0x0005A3A0
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0005B3E8 File Offset: 0x0005A3E8
		public bool IsRightToLeft
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
				return this.ᜊ.IsRightToLeft;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.IsRightToLeft = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0005B430 File Offset: 0x0005A430
		public IChartWallOrFloor Walls
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
				return this.ᜊ.Walls;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0005B478 File Offset: 0x0005A478
		public IChartWallOrFloor Floor
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.Floor;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0005B4C0 File Offset: 0x0005A4C0
		public IChartDataTable DataTable
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
				return this.ᜊ.DataTable;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0005B508 File Offset: 0x0005A508
		internal bool IsSelected
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
				return false;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0005B544 File Offset: 0x0005A544
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0005B58C File Offset: 0x0005A58C
		public bool HasDataTable
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
				return this.ᜊ.HasDataTable;
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
				this.ᜊ.HasDataTable = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0005B5D4 File Offset: 0x0005A5D4
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0005B61C File Offset: 0x0005A61C
		public bool HasLegend
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
				return this.ᜊ.HasLegend;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.HasLegend = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0005B664 File Offset: 0x0005A664
		public IChartLegend Legend
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
				return this.ᜊ.Legend;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0005B6AC File Offset: 0x0005A6AC
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x0005B6F4 File Offset: 0x0005A6F4
		public bool HasPlotArea
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
				return this.ᜊ.HasPlotArea;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ.HasPlotArea = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0005B73C File Offset: 0x0005A73C
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0005B784 File Offset: 0x0005A784
		public ChartPlotEmptyType DisplayBlanksAs
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
				return this.ᜊ.DisplayBlanksAs;
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
				this.ᜊ.DisplayBlanksAs = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0005B7CC File Offset: 0x0005A7CC
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0005B814 File Offset: 0x0005A814
		public bool PlotVisibleOnly
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
				return this.ᜊ.PlotVisibleOnly;
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
				this.ᜊ.PlotVisibleOnly = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0005B85C File Offset: 0x0005A85C
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0005B8A4 File Offset: 0x0005A8A4
		public bool SizeWithWindow
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
				return this.ᜊ.SizeWithWindow;
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
				this.ᜊ.SizeWithWindow = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0005B8EC File Offset: 0x0005A8EC
		public ITextBoxes TextBoxes
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.TextBoxes;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0005B934 File Offset: 0x0005A934
		public ICheckBoxes CheckBoxes
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
				return this.ᜊ.CheckBoxes;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0005B97C File Offset: 0x0005A97C
		internal IRadioButtons OptionButtons
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
				return this.ᜊ.RadioButtons;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0005B9C4 File Offset: 0x0005A9C4
		public IComboBoxes ComboBoxes
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
				return this.ᜊ.ComboBoxes;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0005BA0C File Offset: 0x0005AA0C
		public string CodeName
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.CodeName;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0005BA54 File Offset: 0x0005AA54
		public bool ProtectContents
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0005BA94 File Offset: 0x0005AA94
		public bool ProtectDrawingObjects
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0005BAD4 File Offset: 0x0005AAD4
		public bool ProtectScenarios
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0005BB14 File Offset: 0x0005AB14
		public SheetProtectionType Protection
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0005BB54 File Offset: 0x0005AB54
		public bool IsPasswordProtected
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0005BB94 File Offset: 0x0005AB94
		public void Protect(string password)
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
			throw new NotSupportedException();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0005BBD4 File Offset: 0x0005ABD4
		public void Protect(string password, SheetProtectionType options)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0005BC14 File Offset: 0x0005AC14
		public void Unprotect(string password)
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
			throw new NotSupportedException();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0005BC54 File Offset: 0x0005AC54
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			XlsChartShape xlsChartShape;
			for (;;)
			{
				IL_3A:
				xlsChartShape = new XlsChartShape(base.AppImplementation, parent, this, hashNewNames, dicFontIndexes);
				XlsWorksheetBase xlsWorksheetBase = XlsObject.FindParent(xlsChartShape.Parent, typeof(XlsWorksheetBase), true) as XlsWorksheetBase;
				int num = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return xlsChartShape;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							if (addToCollections)
							{
								num = 1;
								continue;
							}
							return xlsChartShape;
						case 1:
							if (true)
							{
							}
							xlsWorksheetBase.InnerShapes.AddShape(xlsChartShape);
							num = 2;
							continue;
						case 2:
							return xlsChartShape;
						}
						goto IL_3A;
					}
				}
			}
			return xlsChartShape;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0005BD00 File Offset: 0x0005AD00
		public override void UpdateFormula(int iCurIndex, int iSourceIndex, Rectangle sourceRect, int iDestIndex, Rectangle destRect)
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
			this.ᜊ.UpdateFormula(iCurIndex, iSourceIndex, sourceRect, iDestIndex, destRect);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0005BD50 File Offset: 0x0005AD50
		internal override void RegisterInSubCollection()
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
			this.m_shapes.WorksheetBase.InnerCharts.InnerAddChart(this);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0005BDA4 File Offset: 0x0005ADA4
		protected override void OnPrepareForSerialization()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
					num = 2;
					continue;
				case 2:
					goto IL_4B;
				}
				if (this.ᜏ != null)
				{
					break;
				}
				num = 1;
			}
			IL_4B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4B;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜏ.ᜉ(2);
				this.ᜏ.ᜈ(201);
				this.ᜏ.ᜆ(true);
				this.ᜏ.ᜇ(true);
				return;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0005BE60 File Offset: 0x0005AE60
		internal override void ParseClientData(spr᪙ clientData, ExcelParseOptions options)
		{
			for (;;)
			{
				IL_42:
				base.ParseClientData(clientData, options);
				int num = 1;
				BiffRecordRaw[] a_ = clientData.ᜀ();
				this.ᜊ = new spr\u23F7((spr\u2158)base.ReservedHandle, this, a_, ref num, options);
				int num2 = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							this.ᜊ.Parse();
							num2 = 1;
							continue;
						case 1:
							return;
						case 2:
							if ((options & ExcelParseOptions.DoNotParseCharts) == ExcelParseOptions.Default)
							{
								num2 = 0;
								continue;
							}
							return;
						}
						goto IL_42;
					}
				}
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0005BF10 File Offset: 0x0005AF10
		internal override void SerializeShape(spr\u21EB spgrContainer)
		{
			int a_ = 13;
			spr᪙ spr᪙;
			sprᮋ sprᮋ;
			sprὙ sprὙ;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 9;
					for (;;)
					{
						spr\u2223 spr_u2;
						spr\u23E7 spr_u23E;
						RecordArrayList recordArrayList;
						switch (num)
						{
						case 0:
							goto IL_2D6;
						case 1:
						{
							if (true)
							{
							}
							spr\u2003 spr_u = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
							spr_u2 = new spr\u2223();
							spr_u2.ᜀ(TObjType.otChart);
							spr_u2.ᜀ(true);
							sprទ a_2 = new sprទ();
							spr_u.ᜀ(spr_u2);
							spr_u.ᜀ(a_2);
							base.ᜀ(spr_u);
							num = 12;
							continue;
						}
						case 2:
							goto IL_2D6;
						case 3:
							if (spr_u23E.ᜀ().Length > 0)
							{
								num = 10;
								continue;
							}
							goto IL_324;
						case 4:
							goto IL_70;
						case 5:
							goto IL_149;
						case 6:
							spr_u2.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
							spr᪙.ᜀ(base.Obj);
							this.ᜊ.Width = spr\u17FF.ᜀ((double)((IShape)this).Width, MeasureUnits.Point);
							this.ᜊ.Height = spr\u17FF.ᜀ((double)((IShape)this).Height, MeasureUnits.Point);
							this.ᜊ.SerializeDataToList(recordArrayList);
							spr᪙.ᜀ(recordArrayList);
							num = 11;
							continue;
						case 7:
							if (base.Obj == null)
							{
								num = 1;
								continue;
							}
							spr_u2 = (base.Obj.ᜃ()[0] as spr\u2223);
							num = 13;
							continue;
						case 8:
							sprᮋ.ᜀ(3);
							sprᮋ.ᜇ(this.\u170D);
							sprᮋ.ᜂ(this.ᜎ);
							sprᮋ.ᜆ(this.ᜋ);
							sprᮋ.ᜅ(this.ᜌ);
							sprᮋ.ᜀ(0);
							sprᮋ.ᜃ(0);
							sprᮋ.ᜁ(0);
							sprᮋ.ᜄ(0);
							num = 2;
							continue;
						case 10:
							sprὙ.ᜀ(spr_u23E);
							num = 5;
							continue;
						case 11:
							if (base.ClientAnchor == null)
							{
								num = 8;
								continue;
							}
							sprᮋ = base.ClientAnchor;
							num = 0;
							continue;
						case 12:
							goto IL_100;
						case 13:
							goto IL_100;
						}
						if (spgrContainer == null)
						{
							num = 4;
							continue;
						}
						sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
						sprᮋ = (sprᮋ)spr\u231F.ᜀ(MsoRecords.msofbtClientAnchor);
						spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
						recordArrayList = new RecordArrayList();
						spr_u2 = null;
						num = 7;
						continue;
						IL_100:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						IL_2D6:
						sprὙ.ᜀ(this.ᜏ);
						spr_u23E = this.SerializeOptions(sprὙ);
						spr_u23E.ᜉ(3);
						spr_u23E.ᜈ(8);
						num = 3;
					}
					break;
				}
				}
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("あ㕄⁆㭈ࡊ≌ⅎ═㉒㱔㥖㱘⥚", a_));
			IL_149:
			IL_324:
			sprὙ.ᜀ(sprᮋ);
			sprὙ.ᜀ(spr᪙);
			spgrContainer.ᜀ(sprὙ);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0005C258 File Offset: 0x0005B258
		internal override void ParseClientAnchor(sprᮋ clientAnchor)
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
			base.ParseClientAnchor(clientAnchor);
			this.ᜌ = clientAnchor.ᜇ();
			this.ᜋ = clientAnchor.ᜉ();
			this.\u170D = clientAnchor.ᜃ();
			this.ᜎ = clientAnchor.ᜎ();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0005C2CC File Offset: 0x0005B2CC
		internal override spr\u23E7 SerializeOptions(spr\u1D3B parent)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3D;
				case 1:
					if (this.\u1712 == null)
					{
						num = 0;
						continue;
					}
					goto IL_FA;
				case 2:
					IL_08:
					break;
				case 3:
					num = 1;
					continue;
				}
				if (true)
				{
				}
				if (!this.m_bUpdateLineFill)
				{
					num = 3;
					continue;
				}
				IL_3D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				default:
					goto IL_53;
				}
			}
			IL_53:
			if (false)
			{
			}
			spr\u23E7 spr_u23E = base.SerializeOptions(parent);
			base.ᜄ(spr_u23E);
			base.ᜀ(spr_u23E, MsoOptions.ForeColor, 134217806U);
			base.ᜀ(spr_u23E, MsoOptions.BackColor, 134217805U);
			base.ᜅ(spr_u23E);
			base.ᜀ(spr_u23E, MsoOptions.LineColor, 134217805U);
			base.ᜀ(spr_u23E, MsoOptions.NoLineDrawDash, 524296U);
			base.ᜀ(spr_u23E, MsoOptions.ShadowObscured, 131072U);
			base.ᜇ(spr_u23E);
			return spr_u23E;
			IL_FA:
			return this.\u1712;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0005C3DC File Offset: 0x0005B3DC
		internal override spr\u23E7 CreateDefaultOptions()
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
			spr\u23E7 spr_u23E = base.CreateDefaultOptions();
			spr_u23E.ᜉ(3);
			spr_u23E.ᜈ(8);
			base.ᜁ(spr_u23E, MsoOptions.LockAgainstGrouping, 17039620U);
			return spr_u23E;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0005C43C File Offset: 0x0005B43C
		protected override void SetParents()
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
			base.SetParents();
			this.ᜏ = this.m_shapes.WorksheetBase;
			this.ᜏ.InnerCharts.InnerAddChart(this);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0005C4A0 File Offset: 0x0005B4A0
		public static implicit operator XlsWorksheetBase(XlsChartShape chartShape)
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
			return chartShape.ChartObject;
		}

		// Token: 0x0400019B RID: 411
		private new const int ᜀ = 201;

		// Token: 0x0400019C RID: 412
		private long[] \u25D9\u00A7\u0091\u00A9;

		// Token: 0x0400019D RID: 413
		private new const int ᜁ = 2;

		// Token: 0x0400019E RID: 414
		private new const int ᜂ = 3;

		// Token: 0x0400019F RID: 415
		private new const int ᜃ = 8;

		// Token: 0x040001A0 RID: 416
		private new const uint ᜄ = 17039620U;

		// Token: 0x040001A1 RID: 417
		private new const uint ᜅ = 134217805U;

		// Token: 0x040001A2 RID: 418
		private new const uint ᜆ = 524296U;

		// Token: 0x040001A3 RID: 419
		private new const uint ᜇ = 131072U;

		// Token: 0x040001A4 RID: 420
		private const uint ᜈ = 134217806U;

		// Token: 0x040001A5 RID: 421
		private const uint ᜉ = 134217805U;

		// Token: 0x040001A6 RID: 422
		private float[] \u25D8\u00AD\u00A2\u0081;

		// Token: 0x040001A7 RID: 423
		private XlsChart ᜊ;

		// Token: 0x040001A8 RID: 424
		private int ᜋ;

		// Token: 0x040001A9 RID: 425
		private int ᜌ;

		// Token: 0x040001AA RID: 426
		private int \u170D;

		// Token: 0x040001AB RID: 427
		private int ᜎ;

		// Token: 0x040001AC RID: 428
		private new XlsWorksheetBase ᜏ;

		// Token: 0x040001AD RID: 429
		private new int ᜐ;

		// Token: 0x040001AE RID: 430
		private int ᜑ;

		// Token: 0x040001AF RID: 431
		private new int \u1712;

		// Token: 0x040001B0 RID: 432
		private int \u1713;
	}
}
