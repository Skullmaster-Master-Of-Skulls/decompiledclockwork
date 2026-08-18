using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200019D RID: 413
	public class XlsChartWallOrFloor : XlsChartGridLine, IChartWallOrFloor, spr\u218E
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x000C622C File Offset: 0x000C522C
		internal XlsChartWallOrFloor(spr\u1DF5 A_0, object A_1, bool A_2)
		{
			int a_ = 9;
			this.ᜏ = -1;
			base..ctor(A_0, A_1, AxisLineIdentifierType.MajorGridLine);
			base.AxisLineType = AxisLineIdentifierType.WallsOrFloor;
			this.ᜉ = new ChartInterior((spr\u2158)A_0, this);
			ExcelVersion version = this.ᜄ.Version;
			if (version != ExcelVersion.Version2007)
			{
				bool flag = version == ExcelVersion.Version2010;
			}
			this.ᜉ.InitForFrameFormat(false, true, true, !A_2);
			this.ᜈ = A_2;
			this.ᜊ = (XlsChart)base.FindParent(typeof(XlsChart));
			this.\u170D = new spr\u2436(A_0, this);
			if (this.ᜊ == null)
			{
				throw new ApplicationException(RecordTableEnumerator.b("漾⁀ㅂ⁄⥆㵈歊≌ⵎ㭐㙒㙔⍖祘㡚㱜ㅞའౢᅤ䝦୨๪䵬८Ṱٲ᭴፶", a_));
			}
			this.SetToDefault();
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x000C62EC File Offset: 0x000C52EC
		internal XlsChartWallOrFloor(spr\u1DF5 A_0, object A_1, bool A_2, IList<BiffRecordRaw> A_3, ref int A_4)
		{
			int a_ = 14;
			this.ᜏ = -1;
			base..ctor(A_0, A_1, A_3, ref A_4);
			base.AxisLineType = AxisLineIdentifierType.WallsOrFloor;
			this.ᜈ = A_2;
			this.ᜊ = (XlsChart)base.FindParent(typeof(XlsChart));
			if (this.\u170D == null)
			{
				this.\u170D = new spr\u2436(A_0, this);
			}
			if (this.ᜊ == null)
			{
				throw new ApplicationException(RecordTableEnumerator.b("ᑃ❅㩇⽉≋㩍灏㵑㙓㱕㵗㥙⡛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻", a_));
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000C6378 File Offset: 0x000C5378
		internal override void Parse(IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 0;
			for (;;)
			{
				this.ᜉ = null;
				base.Parse(data, ref iPos);
				int num = 0;
				for (;;)
				{
					BiffRecordRaw biffRecordRaw;
					int num2;
					switch (num)
					{
					case 0:
						if (base.AxisLineType != AxisLineIdentifierType.WallsOrFloor)
						{
							num = 7;
							continue;
						}
						biffRecordRaw = data[iPos];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_192;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						goto IL_110;
					case 2:
						this.\u170D = new spr\u2436((spr\u2158)base.ReservedHandle, this, (spr\u216D)biffRecordRaw);
						iPos++;
						num = 14;
						continue;
					case 3:
						if (biffRecordRaw.TypeCode == TBIFFRecord.ChartAreaFormat)
						{
							num = 6;
							continue;
						}
						goto IL_20C;
					case 4:
						goto IL_110;
					case 5:
						goto IL_110;
					case 6:
						this.ᜉ = new ChartInterior((spr\u2158)base.ReservedHandle, this, data, ref iPos);
						num = 10;
						continue;
					case 7:
						goto IL_80;
					case 8:
						if (biffRecordRaw.TypeCode == TBIFFRecord.ChartGelFrame)
						{
							num = 2;
							continue;
						}
						goto IL_1FA;
					case 9:
						if (true)
						{
						}
						num = 4;
						continue;
					case 10:
						goto IL_20C;
					case 11:
						goto IL_180;
					case 12:
						goto IL_19D;
					case 13:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.Begin:
							num2++;
							num = 5;
							continue;
						case TBIFFRecord.End:
							num2--;
							num = 1;
							continue;
						default:
							num = 9;
							continue;
						}
						break;
					}
					case 14:
						goto IL_1FA;
					case 15:
						goto IL_180;
					case 16:
					{
						if (num2 <= 0)
						{
							goto IL_192;
						}
						biffRecordRaw = data[iPos];
						TBIFFRecord typeCode = biffRecordRaw.TypeCode;
						num = 13;
						continue;
					}
					}
					break;
					IL_110:
					iPos++;
					num = 15;
					continue;
					IL_180:
					num = 16;
					continue;
					IL_192:
					num = 12;
					continue;
					IL_1FA:
					num2 = 1;
					num = 11;
					continue;
					IL_20C:
					biffRecordRaw = data[iPos];
					num = 8;
				}
			}
			IL_80:
			throw new spr\u2313(RecordTableEnumerator.b("挵嘷儹刻儽㜿ⱁ摃❅ぇ⍉㽋湍㱏㭑㩓㍕硗⹙╛⹝՟", a_));
			IL_19D:
			iPos--;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000C65C8 File Offset: 0x000C55C8
		internal override void SerializeDataToList(RecordArrayList records)
		{
			for (;;)
			{
				base.SerializeDataToList(records);
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						while (this.ᜉ != null)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 2;
								goto IL_02;
							}
						}
						goto IL_79;
					case 1:
						goto IL_77;
					case 2:
						this.ᜉ.ᜀ(records);
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_77:
			IL_79:
			this.\u170D.ᜀ(records);
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x000C665C File Offset: 0x000C565C
		public override IChartInterior Interior
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6D;
						}
						break;
					case 2:
						this.ᜉ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
						num = 1;
						continue;
					}
					IL_1C:
					if (this.ᜉ == null)
					{
						num = 2;
						continue;
					}
					goto IL_7D;
					goto IL_1C;
				}
				IL_6D:
				if (true)
				{
				}
				if (false)
				{
				}
				IL_7D:
				return this.ᜉ;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000C66EC File Offset: 0x000C56EC
		public new ChartShadow Shadow
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_68;
						}
						break;
					case 2:
						this.ᜋ = new ChartShadow(base.AppImplementation, this);
						num = 1;
						continue;
					}
					IL_1C:
					if (this.ᜋ == null)
					{
						num = 2;
						continue;
					}
					goto IL_78;
					goto IL_1C;
				}
				IL_68:
				if (true)
				{
				}
				if (false)
				{
				}
				IL_78:
				return this.ᜋ;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x000C6778 File Offset: 0x000C5778
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x000C67C0 File Offset: 0x000C57C0
		public new bool HasShadow
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
				return this.ᜋ != null;
			}
			internal set
			{
				if (value)
				{
					for (;;)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2D;
						}
					}
					IL_2D:
					if (false)
					{
					}
					ChartShadow shadow = this.Shadow;
					return;
				}
				this.ᜋ = null;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000C6810 File Offset: 0x000C5810
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x000C6854 File Offset: 0x000C5854
		internal bool HasShapeProperties
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000C6898 File Offset: 0x000C5898
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000C68DC File Offset: 0x000C58DC
		internal int Thickness
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000C6920 File Offset: 0x000C5920
		public new Format3D Format3D
		{
			get
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_70;
						}
						break;
					case 2:
						this.ᜌ = new Format3D(base.AppImplementation, this);
						num = 1;
						continue;
					}
					IL_24:
					if (this.ᜌ == null)
					{
						num = 2;
						continue;
					}
					goto IL_78;
					goto IL_24;
				}
				IL_70:
				if (false)
				{
				}
				IL_78:
				return this.ᜌ;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x000C69AC File Offset: 0x000C59AC
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x000C69F4 File Offset: 0x000C59F4
		public new bool HasFormat3D
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
				return this.ᜌ != null;
			}
			internal set
			{
				if (value)
				{
					for (;;)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2D;
						}
					}
					IL_2D:
					if (false)
					{
					}
					Format3D format3D = this.Format3D;
					return;
				}
				this.ᜌ = null;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000C6A44 File Offset: 0x000C5A44
		public override IShapeFill Fill
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
				this.IsAutomaticFormat = false;
				return this.\u170D;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x000C6A8C File Offset: 0x000C5A8C
		public override bool HasInterior
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
				return this.ᜉ != null;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x000C6AD4 File Offset: 0x000C5AD4
		private bool IsWall
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
				return this.ᜈ;
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000C6B18 File Offset: 0x000C5B18
		public override void Delete()
		{
			if (this.ᜈ)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2A;
					}
				}
				IL_2A:
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜊ.Walls = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, this.ᜊ, true);
				return;
			}
			this.ᜊ.Floor = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, this.ᜊ, false);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000C6BA4 File Offset: 0x000C5BA4
		public void SetToDefault()
		{
			if (this.ᜈ)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_32;
					}
				}
				IL_32:
				if (false)
				{
				}
				this.ᜃ();
				this.ᜁ();
				return;
			}
			this.ᜂ();
			this.ᜀ();
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000C6C04 File Offset: 0x000C5C04
		private void ᜃ()
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
			base.Border.Weight = ChartLineWeightType.Narrow;
			base.Border.KnownColor = ExcelColors.Gray50Percent;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x000C6C58 File Offset: 0x000C5C58
		private void ᜂ()
		{
			if (this.ᜄ.Version != ExcelVersion.Version97to2003)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					base.Border.KnownColor = ExcelColors.Gray25Percent;
					return;
				}
			}
			if (true)
			{
			}
			base.Border.KnownColor = (ExcelColors)77;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000C6CC0 File Offset: 0x000C5CC0
		private void ᜁ()
		{
			if (this.ᜄ.Version != ExcelVersion.Version97to2003)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜉ.Pattern = ExcelPatternType.None;
					return;
				}
			}
			this.ᜉ.Pattern = ExcelPatternType.Solid;
			this.ᜉ.ForegroundColorObject.SetKnownColor(ExcelColors.Gray25Percent);
			this.ᜉ.BackgroundColorObject.SetKnownColor((ExcelColors)79);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000C6D48 File Offset: 0x000C5D48
		private void ᜀ()
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
			this.Interior.Pattern = ExcelPatternType.Solid;
			this.Interior.UseDefaultFormat = true;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000C6D9C File Offset: 0x000C5D9C
		public override object Clone(object parent)
		{
			XlsChartWallOrFloor xlsChartWallOrFloor;
			for (;;)
			{
				IL_26:
				xlsChartWallOrFloor = (XlsChartWallOrFloor)base.Clone(parent);
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						xlsChartWallOrFloor.ᜉ = this.ᜉ.Clone(xlsChartWallOrFloor);
						num = 1;
						continue;
					case 1:
						goto IL_67;
					case 2:
						if (this.ᜉ != null)
						{
							num = 0;
							continue;
						}
						goto IL_69;
					}
					goto IL_26;
				}
				IL_69:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_7F;
				}
				IL_67:
				goto IL_69;
			}
			IL_7F:
			if (false)
			{
			}
			return xlsChartWallOrFloor;
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000C6E30 File Offset: 0x000C5E30
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x000C6E78 File Offset: 0x000C5E78
		public Color ForeGroundColor
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
				return this.ᜉ.ForegroundColor;
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
				this.ᜉ.ForegroundColor = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000C6EC0 File Offset: 0x000C5EC0
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x000C6F08 File Offset: 0x000C5F08
		public ExcelColors ForeGroundKnownColor
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
				return this.ᜉ.ForegroundKnownColor;
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
				this.ᜉ.ForegroundKnownColor = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000C6F50 File Offset: 0x000C5F50
		public OColor ForeGroundColorObject
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
				return this.ᜉ.ForegroundColorObject;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000C6F98 File Offset: 0x000C5F98
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x000C6FE0 File Offset: 0x000C5FE0
		public Color BackGroundColor
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
				return this.ᜉ.BackgroundColor;
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
				this.ᜉ.BackgroundColor = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x000C7028 File Offset: 0x000C6028
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x000C7070 File Offset: 0x000C6070
		public ExcelColors BackGroundKnownColor
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
				return this.ᜉ.BackgroundKnownColor;
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
				this.ᜉ.BackgroundKnownColor = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x000C70B8 File Offset: 0x000C60B8
		public OColor BackGroundColorObject
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
				return this.ᜉ.BackgroundColorObject;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000C7100 File Offset: 0x000C6100
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x000C7148 File Offset: 0x000C6148
		public ExcelPatternType Pattern
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
				return this.Interior.Pattern;
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
				this.Interior.Pattern = value;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000C7190 File Offset: 0x000C6190
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x000C71D8 File Offset: 0x000C61D8
		public bool IsAutomaticFormat
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
				return this.Interior.UseDefaultFormat;
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
				this.Interior.UseDefaultFormat = value;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x000C7220 File Offset: 0x000C6220
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x000C726C File Offset: 0x000C626C
		public bool Visible
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
				return this.Interior.Pattern != ExcelPatternType.None;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_8A;
						case 1:
							goto IL_A2;
						case 2:
							goto IL_73;
						case 4:
							num = 0;
							continue;
						}
						if (true)
						{
						}
						if (value)
						{
							num = 4;
							continue;
						}
						this.Interior.Pattern = ExcelPatternType.None;
						num = 2;
						continue;
					}
					IL_8A:
					if (this.Interior.Pattern != ExcelPatternType.None)
					{
						break;
					}
					num = 1;
				}
				IL_73:
				return;
				IL_A2:
				this.Interior.Pattern = ExcelPatternType.Solid;
			}
		}

		// Token: 0x04000F08 RID: 3848
		internal const int ᜀ = 8421504;

		// Token: 0x04000F09 RID: 3849
		internal const ExcelColors ᜁ = ExcelColors.Gray50Percent;

		// Token: 0x04000F0A RID: 3850
		private const int ᜂ = 0;

		// Token: 0x04000F0B RID: 3851
		internal const int ᜃ = 12632256;

		// Token: 0x04000F0C RID: 3852
		internal new const ExcelColors ᜄ = (ExcelColors)79;

		// Token: 0x04000F0D RID: 3853
		internal const ExcelColors ᜅ = (ExcelColors)77;

		// Token: 0x04000F0E RID: 3854
		private const ExcelColors ᜆ = (ExcelColors)78;

		// Token: 0x04000F0F RID: 3855
		private spr\u216D ᜇ;

		// Token: 0x04000F10 RID: 3856
		private bool ᜈ;

		// Token: 0x04000F11 RID: 3857
		private byte[] \u25D8\u0097\u009F\u007F;

		// Token: 0x04000F12 RID: 3858
		private XlsChartInterior ᜉ;

		// Token: 0x04000F13 RID: 3859
		private XlsChart ᜊ;

		// Token: 0x04000F14 RID: 3860
		private ChartShadow ᜋ;

		// Token: 0x04000F15 RID: 3861
		private Format3D ᜌ;

		// Token: 0x04000F16 RID: 3862
		private spr\u2436 \u170D;

		// Token: 0x04000F17 RID: 3863
		private bool ᜎ;

		// Token: 0x04000F18 RID: 3864
		private int ᜏ;
	}
}
