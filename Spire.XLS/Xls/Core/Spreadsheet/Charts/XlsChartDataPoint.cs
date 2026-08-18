using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200018B RID: 395
	public class XlsChartDataPoint : XlsObject, IChartDataPoint
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x000BD064 File Offset: 0x000BC064
		internal XlsChartDataPoint(spr\u1DF5 A_0, object A_1, int A_2)
		{
			int a_ = 13;
			base..ctor(A_0, A_1);
			this.ᜁ = A_2;
			this.ᜂ = new ChartSerieDataFormat((spr\u2158)A_0, this);
			this.ᜂ.DataFormat.ᜂ((ushort)this.ᜁ);
			this.ᜃ = (XlsChart)base.FindParent(typeof(XlsChart));
			if (this.ᜃ == null)
			{
				throw new Exception(RecordTableEnumerator.b("ፂ⑄㕆ⱈ╊㥌潎㹐ㅒ㽔㉖㩘⽚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᕲᩴɶ᝸ὺ卼", a_));
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x000BD0EC File Offset: 0x000BC0EC
		public IChartDataLabels DataLabels
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
				this.ᜀ();
				return this.ᜀ;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x000BD134 File Offset: 0x000BC134
		public IChartSerieDataFormat DataFormat
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜂ = new ChartSerieDataFormat(base.ReservedHandle as spr\u2158, this);
							num = 1;
							continue;
						case 1:
							goto IL_7B;
						}
						if (this.ᜂ != null)
						{
							goto IL_7D;
						}
						num = 0;
						break;
					}
				}
				IL_7B:
				IL_7D:
				return this.ᜂ;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000BD1C4 File Offset: 0x000BC1C4
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x000BD208 File Offset: 0x000BC208
		protected internal XlsChartSerieDataFormat InnerDataFormat
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
				return this.ᜂ;
			}
			set
			{
				for (;;)
				{
					this.ᜂ = value;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (value != null)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								value.SetParent(this);
								value.ᜏ();
								if (true)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x000BD28C File Offset: 0x000BC28C
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x000BD2D0 File Offset: 0x000BC2D0
		public int Index
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x000BD314 File Offset: 0x000BC314
		protected internal XlsChartSerieDataFormat DataFormatOrNull
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
				return this.ᜂ;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x000BD358 File Offset: 0x000BC358
		public bool IsDefault
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
				return this.ᜁ == 65535;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x000BD3A0 File Offset: 0x000BC3A0
		public bool HasDataLabels
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
				return this.ᜀ != null;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000BD3E8 File Offset: 0x000BC3E8
		[CLSCompliant(false)]
		internal void ᜀ(RecordArrayList A_0)
		{
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
					{
						spr\u1B6D spr_u1B6D = this.ᜀ;
						spr_u1B6D.ᜀ(A_0);
						if (true)
						{
						}
						num = 2;
						continue;
					}
					case 2:
						return;
					}
					if (this.ᜀ == null)
					{
						return;
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000BD46C File Offset: 0x000BC46C
		[CLSCompliant(false)]
		internal void ᜁ(RecordArrayList A_0)
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜂ != null)
					{
						goto IL_93;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
					goto IL_46;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ㡊", a_));
			IL_93:
			this.ᜂ.UpdateDataFormatInDataPoint();
			this.ᜂ.SerializeDataToList(A_0);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000BD524 File Offset: 0x000BC524
		public void SetDataLabels(XlsChartTextArea textArea)
		{
			int a_ = 1;
			while (textArea != null)
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
					this.ᜀ();
					this.ᜀ.TextArea = textArea;
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䌶尸䌺䤼績㍀♂⑄", a_));
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000BD594 File Offset: 0x000BC594
		private void ᜀ()
		{
			int num = 1;
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
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.ᜀ = new ChartDataLabels((spr\u2158)base.ReservedHandle, this, this.Index);
						num = 0;
						continue;
					}
					if (this.ᜀ != null)
					{
						return;
					}
					num = 2;
					break;
				}
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000BD624 File Offset: 0x000BC624
		internal object ᜀ(object A_0, Dictionary<int, int> A_1, Dictionary<string, string> A_2)
		{
			XlsChartDataPoint xlsChartDataPoint;
			for (;;)
			{
				xlsChartDataPoint = new ChartDataPoint((spr\u2158)base.ReservedHandle, A_0, this.ᜁ);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
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
							if (this.ᜀ == null)
							{
								goto IL_95;
							}
							break;
						}
						num = 5;
						continue;
					case 1:
						return xlsChartDataPoint;
					case 2:
						xlsChartDataPoint.ᜂ = this.ᜂ.Clone(xlsChartDataPoint);
						num = 1;
						continue;
					case 3:
						goto IL_95;
					case 4:
						if (this.ᜂ != null)
						{
							num = 2;
							continue;
						}
						return xlsChartDataPoint;
					case 5:
						xlsChartDataPoint.ᜀ = (XlsChartDataLabels)this.ᜀ.ᜀ(xlsChartDataPoint, A_1, A_2);
						num = 3;
						continue;
					}
					break;
					IL_95:
					num = 4;
				}
			}
			return xlsChartDataPoint;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000BD718 File Offset: 0x000BC718
		public void UpdateSerieIndex()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜂ.UpdateSerieIndex();
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					if (this.ᜂ != null)
					{
						num = 0;
						continue;
					}
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜀ.UpdateSerieIndex();
						num = 5;
						continue;
					}
					break;
				case 5:
					goto IL_7D;
				}
				goto IL_28;
				IL_30:
				num = 4;
				continue;
				IL_28:
				if (this.ᜀ != null)
				{
					goto IL_30;
				}
				IL_7D:
				num = 2;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000BD7D8 File Offset: 0x000BC7D8
		public void ChangeChartStockHigh_Low_CloseType()
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
			this.DataFormat.MarkerStyle = ChartMarkerType.DowJones;
			this.ᜂ.IsAutoMarker = false;
			this.ᜂ.MarkerForegroundKnownColor = (ExcelColors)79;
			this.ᜂ.MarkerBackgroundKnownColor = (ExcelColors)79;
			this.ᜂ.LineProperties.Pattern = ChartLinePatternType.None;
			this.ᜂ.LineProperties.Weight = ChartLineWeightType.Hairline;
			this.ᜂ.LineProperties.KnownColor = (ExcelColors)77;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000BD880 File Offset: 0x000BC880
		public void ChangeChartStockVolume_High_Low_CloseType()
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
			this.DataFormat.MarkerStyle = ChartMarkerType.DowJones;
			this.ᜂ.IsAutoMarker = false;
			this.ᜂ.MarkerForegroundKnownColor = (ExcelColors)77;
			this.ᜂ.MarkerBackgroundKnownColor = (ExcelColors)77;
			ExcelChartType destinationType = this.ᜃ.DestinationType;
			this.ᜃ.DestinationType = ExcelChartType.Line;
			this.ᜂ.LineProperties.Pattern = ChartLinePatternType.None;
			this.ᜃ.DestinationType = destinationType;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000BD924 File Offset: 0x000BC924
		public void ChangeIntimateBuble(ExcelChartType typeToChange)
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
			this.DataFormat.LineProperties.Pattern = ChartLinePatternType.Solid;
			this.DataFormat.Is3DBubbles = (typeToChange != ExcelChartType.Bubble);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000BD988 File Offset: 0x000BC988
		public void CloneDataFormat(XlsChartSerieDataFormat serieFormat)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (!this.ᜂ.IsFormatted)
					{
						num = 6;
						continue;
					}
					goto IL_CB;
				case 2:
					if (this.ᜂ != null)
					{
						num = 3;
						continue;
					}
					goto IL_7D;
				case 3:
					num = 1;
					continue;
				case 5:
					goto IL_AD;
				case 6:
					goto IL_7D;
				}
				if (serieFormat == null)
				{
					num = 0;
					continue;
				}
				num = 2;
				continue;
				IL_7D:
				sprᲡ a_ = this.ᜂ.DataFormat;
				this.ᜂ = serieFormat.Clone(this);
				this.ᜂ.DataFormat = a_;
				num = 5;
			}
			return;
			IL_AD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				break;
			}
			IL_CB:
			if (true)
			{
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000BDA68 File Offset: 0x000BCA68
		public void ClearDataFormats(XlsChartSerieDataFormat format)
		{
			int num = 4;
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
				case 1:
					goto IL_32;
				default:
					goto IL_32;
				}
				IL_52:
				if (this.ᜂ != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_32:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.ᜂ.IsFormatted)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
				{
					sprᲡ a_ = this.ᜂ.DataFormat;
					this.ᜂ = format.Clone(this);
					this.ᜂ.DataFormat = a_;
					num = 0;
					continue;
				}
				case 3:
					num = 1;
					continue;
				}
				goto IL_52;
			}
		}

		// Token: 0x04000EA2 RID: 3746
		private int \u2460\u0082\u00AD\u0099;

		// Token: 0x04000EA3 RID: 3747
		private bool[] \u25D8\u00A0\u00A0\u0082;

		// Token: 0x04000EA4 RID: 3748
		private XlsChartDataLabels ᜀ;

		// Token: 0x04000EA5 RID: 3749
		private long[] \u25D9\u00AB\u008F\u0083;

		// Token: 0x04000EA6 RID: 3750
		private int ᜁ;

		// Token: 0x04000EA7 RID: 3751
		private XlsChartSerieDataFormat ᜂ;

		// Token: 0x04000EA8 RID: 3752
		private float[] \u2460\u00AC\u008F\u00A9;

		// Token: 0x04000EA9 RID: 3753
		private string \u2460\u0087\u00A4\u00B0;

		// Token: 0x04000EAA RID: 3754
		private XlsChart ᜃ;
	}
}
