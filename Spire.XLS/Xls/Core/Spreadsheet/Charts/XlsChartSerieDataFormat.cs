using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000185 RID: 389
	public class XlsChartSerieDataFormat : XlsObject, IChartSerieDataFormat, spr\u218E
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x000B4E3C File Offset: 0x000B3E3C
		internal XlsChartSerieDataFormat(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜏ();
			this.\u171D = new spr\u2436(base.ReservedHandle, this);
			if (!this.\u1719.ParentWorkbook.Loading)
			{
				this.ᜅ();
			}
			this.ᜆ();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000B4EB0 File Offset: 0x000B3EB0
		private void ᜆ()
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
			this.\u171F = new OColor(spr\u1D39.ᜂ);
			this.\u171F.AfterChange += this.ᜂ;
			this.\u171E = new OColor(spr\u1D39.ᜂ);
			this.\u171E.AfterChange += this.ᜁ;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000B4F3C File Offset: 0x000B3F3C
		internal void ᜏ()
		{
			int a_ = 12;
			for (;;)
			{
				this.\u1719 = (base.FindParent(typeof(XlsChart)) as XlsChart);
				Type[] arrTypes = new Type[]
				{
					typeof(ChartSerie),
					typeof(ChartFormat)
				};
				object obj = base.FindParent(arrTypes);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						num = 8;
						continue;
					case 2:
						if (obj != null)
						{
							num = 1;
							continue;
						}
						goto IL_17E;
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_167;
						default:
							if (false)
							{
							}
							if (!this.\u1719.TypeChanging)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 4:
						this.UpdateSerieFormat();
						goto IL_167;
					case 5:
						goto IL_15F;
					case 6:
						if (this.\u1718 != null)
						{
							num = 0;
							continue;
						}
						return;
					case 7:
						goto IL_172;
					case 8:
						if (this.\u1719 == null)
						{
							num = 5;
							continue;
						}
						this.\u1717 = (obj as XlsChartSerie);
						this.\u1718 = (obj as XlsChartFormat);
						this.\u1716 = (base.FindParent(typeof(XlsChartDataPoint)) as XlsChartDataPoint);
						num = 6;
						continue;
					}
					break;
					IL_167:
					num = 7;
				}
			}
			IL_15F:
			goto IL_17E;
			IL_172:
			return;
			IL_17E:
			throw new ArgumentNullException(RecordTableEnumerator.b("ቁ╃㑅ⵇ⑉㡋湍㽏け㹓㍕㭗⹙籛㵝şౡ੣॥ᱧ䩩๫୭偯ᑱ᭳͵ᙷṹ剻", a_));
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000B50DC File Offset: 0x000B40DC
		internal int ᜀ(IList<BiffRecordRaw> A_0, int A_1)
		{
			int a_ = 12;
			int num = 17;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
				{
					TBIFFRecord typeCode;
					if (typeCode <= TBIFFRecord.End)
					{
						num = 25;
						continue;
					}
					num = 6;
					continue;
				}
				case 1:
					goto IL_1B0;
				case 2:
					goto IL_1B0;
				case 3:
				{
					int num2;
					if (num2 <= 0)
					{
						num = 22;
						continue;
					}
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 0;
					continue;
				}
				case 4:
					goto IL_AD;
				case 5:
					goto IL_1B0;
				case 6:
				{
					if (true)
					{
					}
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.ChartSerFmt:
						this.\u1715 = (spr\u239E)biffRecordRaw;
						this.\u171C = true;
						num = 10;
						continue;
					case (TBIFFRecord)4190:
						goto IL_1B0;
					case TBIFFRecord.Chart3DDataFormat:
						this.ᜏ = (spr\u25C6)biffRecordRaw;
						this.\u171C = this.ᜀ(this.ᜏ);
						num = 1;
						continue;
					default:
						num = 18;
						continue;
					}
					break;
				}
				case 7:
				{
					if (A_1 > A_0.Count)
					{
						num = 20;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartDataFormat);
					this.ᜎ = (sprᲡ)biffRecordRaw;
					A_1++;
					biffRecordRaw = A_0[A_1++];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
					biffRecordRaw = A_0[A_1];
					int num2 = 1;
					num = 12;
					continue;
				}
				case 8:
					num = 23;
					continue;
				case 9:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartGelFrame)
					{
						num = 26;
						continue;
					}
					this.\u171D = new spr\u2436(base.ReservedHandle, this, (spr\u216D)biffRecordRaw);
					num = 11;
					continue;
				}
				case 10:
					goto IL_1B0;
				case 11:
					goto IL_1B0;
				case 12:
					goto IL_228;
				case 13:
					goto IL_1B0;
				case 14:
					goto IL_1B0;
				case 15:
					goto IL_1B0;
				case 16:
					if (A_1 >= 0)
					{
						num = 27;
						continue;
					}
					goto IL_2AA;
				case 18:
					num = 9;
					continue;
				case 19:
					goto IL_1B0;
				case 20:
					goto IL_18B;
				case 21:
					num = 24;
					continue;
				case 22:
					return A_1;
				case 23:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
					{
						int num2;
						num2++;
						num = 15;
						continue;
					}
					case TBIFFRecord.End:
					{
						int num2;
						num2--;
						num = 28;
						continue;
					}
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							if (false)
							{
							}
							num = 21;
							continue;
						}
						break;
					}
					break;
				}
				case 24:
					goto IL_1B0;
				case 25:
					num = 30;
					continue;
				case 26:
					num = 13;
					continue;
				case 27:
					num = 7;
					continue;
				case 28:
					goto IL_1B0;
				case 29:
					goto IL_228;
				case 30:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.ChartLineFormat:
						this.\u171A = new ChartBorder((spr\u2158)base.ReservedHandle, this, (spr\u22F3)biffRecordRaw);
						this.\u171C = true;
						num = 2;
						continue;
					case (TBIFFRecord)4104:
						goto IL_1B0;
					case TBIFFRecord.ChartMarkerFormat:
						this.\u1713 = (sprᣐ)biffRecordRaw;
						this.\u171C = true;
						num = 14;
						continue;
					case TBIFFRecord.ChartAreaFormat:
						this.\u171B = new ChartInterior((spr\u2158)base.ReservedHandle, this, (sprᨓ)biffRecordRaw);
						this.\u171C = true;
						goto IL_D6;
					case TBIFFRecord.ChartPieFormat:
						this.ᜐ = (spr\u2299)biffRecordRaw;
						this.\u171C = true;
						num = 31;
						continue;
					case TBIFFRecord.ChartAttachedLabel:
						this.\u1714 = (sprή)biffRecordRaw;
						num = 19;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				}
				case 31:
					goto IL_1B0;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 16;
				continue;
				IL_D6:
				num = 5;
				continue;
				IL_1B0:
				A_1++;
				biffRecordRaw = A_0[A_1];
				num = 29;
				continue;
				IL_228:
				num = 3;
			}
			IL_AD:
			throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇", a_));
			IL_18B:
			IL_2AA:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ቁ⭃㕅", a_), RecordTableEnumerator.b("ᑁ╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟๡ţᕥ᭧䩩ᡫ٭ᅯᱱ味䙵塷᭹ቻ᩽ꁿﺉﲍ낏ﲓ몙ﮝ캟얡킣캥", a_));
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000B5548 File Offset: 0x000B4548
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					bool flag;
					XlsChartDataLabels xlsChartDataLabels;
					bool flag2;
					XlsChartDataLabels xlsChartDataLabels2;
					switch (num)
					{
					case 0:
						if (flag)
						{
							num = 42;
							continue;
						}
						goto IL_57E;
					case 1:
						flag2 = xlsChartDataLabels.HasCategoryName;
						goto IL_304;
					case 2:
						num = 14;
						continue;
					case 3:
						xlsChartDataLabels2 = null;
						goto IL_2D9;
					case 4:
						if (this.IsInteriorSupported)
						{
							num = 28;
							continue;
						}
						goto IL_34B;
					case 5:
						goto IL_16B;
					case 6:
						records.ᜀ((BiffRecordRaw)this.ᜐ.Clone());
						num = 31;
						continue;
					case 7:
					{
						BiffRecordRaw a_2 = spr\u175E.ᜀ(TBIFFRecord.ChartLineFormat);
						records.ᜀ(a_2);
						num = 10;
						continue;
					}
					case 8:
						if (flag)
						{
							num = 37;
							continue;
						}
						goto IL_1B4;
					case 9:
						if (xlsChartDataLabels == null)
						{
							num = 2;
							continue;
						}
						num = 13;
						continue;
					case 10:
						goto IL_57E;
					case 11:
						flag2 = true;
						goto IL_304;
					case 12:
						if (this.ᜐ != null)
						{
							num = 6;
							continue;
						}
						goto IL_54E;
					case 13:
						if (!xlsChartDataLabels.HasSeriesName)
						{
							num = 27;
							continue;
						}
						num = 11;
						continue;
					case 14:
						flag2 = false;
						goto IL_304;
					case 15:
						goto IL_1B4;
					case 16:
						if (this.IsBorderSupported)
						{
							num = 7;
							continue;
						}
						goto IL_57E;
					case 17:
						goto IL_112;
					case 19:
						records.ᜀ((BiffRecordRaw)this.\u1713.Clone());
						num = 48;
						continue;
					case 20:
						goto IL_579;
					case 21:
						if (!this.\u1716.HasDataLabels)
						{
							num = 51;
							continue;
						}
						num = 33;
						continue;
					case 22:
						goto IL_27B;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_579;
						default:
							if (false)
							{
							}
							num = 36;
							continue;
						}
						break;
					case 24:
						if (this.ᜏ != null)
						{
							num = 49;
							continue;
						}
						goto IL_3BE;
					case 25:
						if (this.\u1716 != null)
						{
							num = 35;
							continue;
						}
						goto IL_42C;
					case 26:
						if (this.\u1714 != null)
						{
							num = 50;
							continue;
						}
						goto IL_614;
					case 27:
						num = 1;
						continue;
					case 28:
						this.\u171D.ᜀ(records);
						num = 47;
						continue;
					case 29:
						if (this.\u1713 != null)
						{
							num = 19;
							continue;
						}
						goto IL_5A6;
					case 30:
					{
						BiffRecordRaw a_3 = spr\u175E.ᜀ(TBIFFRecord.ChartAreaFormat);
						records.ᜀ(a_3);
						num = 39;
						continue;
					}
					case 31:
						goto IL_54E;
					case 32:
						if (this.\u171B != null)
						{
							num = 45;
							continue;
						}
						num = 8;
						continue;
					case 33:
						xlsChartDataLabels2 = (this.\u1716.DataLabels as XlsChartDataLabels);
						goto IL_2D9;
					case 34:
						goto IL_57E;
					case 35:
						num = 21;
						continue;
					case 36:
						if (this.\u1717.StartType != RecordTableEnumerator.b("搶娸娺䤼䬾⑀ㅂ", a_))
						{
							num = 5;
							continue;
						}
						goto IL_34B;
					case 37:
						num = 52;
						continue;
					case 38:
						if (this.\u171A != null)
						{
							num = 44;
							continue;
						}
						num = 0;
						continue;
					case 39:
						goto IL_1B4;
					case 40:
						if (this.\u1715 != null)
						{
							num = 20;
							continue;
						}
						goto IL_5EC;
					case 41:
						goto IL_5EC;
					case 42:
						num = 16;
						continue;
					case 43:
						goto IL_3BE;
					case 44:
						this.\u171A.ᜀ(records);
						num = 34;
						continue;
					case 45:
						this.\u171B.ᜀ(records);
						num = 15;
						continue;
					case 46:
						if (this.\u1717 != null)
						{
							num = 23;
							continue;
						}
						goto IL_16B;
					case 47:
						goto IL_34B;
					case 48:
						goto IL_5A6;
					case 49:
						records.ᜀ((BiffRecordRaw)this.ᜏ.Clone());
						num = 43;
						continue;
					case 50:
						records.ᜀ((BiffRecordRaw)this.\u1714.Clone());
						num = 22;
						continue;
					case 51:
						goto IL_42C;
					case 52:
						if (this.IsInteriorSupported)
						{
							num = 30;
							continue;
						}
						goto IL_1B4;
					}
					if (records == null)
					{
						num = 17;
						continue;
					}
					num = 25;
					continue;
					IL_16B:
					num = 4;
					continue;
					IL_1B4:
					num = 12;
					continue;
					IL_2D9:
					xlsChartDataLabels = xlsChartDataLabels2;
					num = 9;
					continue;
					IL_304:
					flag = flag2;
					records.ᜀ(this.ᜎ);
					records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
					num = 24;
					continue;
					IL_34B:
					num = 29;
					continue;
					IL_3BE:
					num = 38;
					continue;
					IL_42C:
					num = 3;
					continue;
					IL_54E:
					if (true)
					{
					}
					num = 40;
					continue;
					IL_579:
					records.ᜀ((BiffRecordRaw)this.\u1715.Clone());
					num = 41;
					continue;
					IL_57E:
					num = 32;
					continue;
					IL_5A6:
					num = 26;
					continue;
					IL_5EC:
					num = 46;
				}
				IL_112:
				throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
				IL_27B:
				IL_614:
				records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
				return;
			}
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000B5B7C File Offset: 0x000B4B7C
		public void SetDefaultValues()
		{
			XlsChart innerXlsChart;
			for (;;)
			{
				this.ᜄ();
				this.ᜎ.ᜁ((ushort)this.\u1717.Index);
				this.ᜎ.ᜀ((ushort)this.\u1717.Number);
				this.ᜏ = this.\u1717.\u170D();
				innerXlsChart = this.\u1717.InnerXlsChart;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u1717.ChartGroup > 0)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						this.LineProperties.Color = spr\u1D39.ᜀ(8388608);
						this.ᜐ = (spr\u2299)spr\u175E.ᜀ(TBIFFRecord.ChartPieFormat);
						this.MarkerFormat.ᜀ(ChartMarkerType.Diamond);
						this.\u1713.ᜀ(32);
						this.\u1713.ᜁ(32);
						this.\u1713.ᜀ(100);
						this.\u1713.ᜁ(true);
						this.\u171A.UseDefaultFormat = true;
						num = 4;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_126;
						default:
							if (false)
							{
							}
							if (innerXlsChart.IsChartStock)
							{
								num = 3;
								continue;
							}
							goto IL_AE;
						}
						break;
					case 3:
						goto IL_126;
					case 4:
						return;
					case 5:
						if (innerXlsChart.Series[innerXlsChart.Series.Count - 1] == this.\u1717)
						{
							num = 6;
							continue;
						}
						goto IL_AE;
					case 6:
						goto IL_167;
					}
					break;
					IL_AE:
					num = 0;
					continue;
					IL_126:
					if (true)
					{
					}
					num = 5;
				}
			}
			IL_167:
			this.LineProperties.Pattern = innerXlsChart.DefaultLinePattern;
			this.\u171A.UseDefaultFormat = false;
			this.ᜐ = (spr\u2299)spr\u175E.ᜀ(TBIFFRecord.ChartPieFormat);
			this.MarkerFormat.ᜀ(ChartMarkerType.DowJones);
			this.\u1713.ᜀ(60);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000B5D84 File Offset: 0x000B4D84
		private void ᜅ()
		{
			this.ᜏ = (spr\u25C6)spr\u175E.ᜀ(TBIFFRecord.Chart3DDataFormat);
			switch (this.\u1719.ChartType)
			{
			case ExcelChartType.CylinderClustered:
			case ExcelChartType.CylinderStacked:
			case ExcelChartType.Cylinder100PercentStacked:
			case ExcelChartType.CylinderBarClustered:
			case ExcelChartType.CylinderBarStacked:
			case ExcelChartType.CylinderBar100PercentStacked:
			case ExcelChartType.Cylinder3DClustered:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜏ.ᜀ(BaseFormatType.Circle);
					this.ᜏ.ᜀ(TopFormatType.Straight);
					return;
				}
				break;
			case ExcelChartType.ConeClustered:
			case ExcelChartType.ConeStacked:
			case ExcelChartType.ConeBarClustered:
			case ExcelChartType.ConeBarStacked:
			case ExcelChartType.Cone3DClustered:
				this.ᜏ.ᜀ(BaseFormatType.Circle);
				this.ᜏ.ᜀ(TopFormatType.Sharp);
				return;
			case ExcelChartType.Cone100PercentStacked:
			case ExcelChartType.ConeBar100PercentStacked:
				if (true)
				{
				}
				this.ᜏ.ᜀ(BaseFormatType.Circle);
				this.ᜏ.ᜀ(TopFormatType.Trunc);
				return;
			case ExcelChartType.PyramidClustered:
			case ExcelChartType.PyramidStacked:
			case ExcelChartType.PyramidBarClustered:
			case ExcelChartType.PyramidBarStacked:
			case ExcelChartType.Pyramid3DClustered:
				break;
			case ExcelChartType.Pyramid100PercentStacked:
			case ExcelChartType.PyramidBar100PercentStacked:
				this.ᜏ.ᜀ(BaseFormatType.Rectangle);
				this.ᜏ.ᜀ(TopFormatType.Trunc);
				return;
			default:
				return;
			}
			this.ᜏ.ᜀ(BaseFormatType.Rectangle);
			this.ᜏ.ᜀ(TopFormatType.Sharp);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000B5EBC File Offset: 0x000B4EBC
		private void ᜄ()
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
			this.ᜎ = null;
			this.ᜏ = null;
			this.\u171A = null;
			this.\u171B = null;
			this.ᜐ = null;
			this.\u1713 = null;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000B5F24 File Offset: 0x000B4F24
		public XlsChartSerieDataFormat Clone(object parent)
		{
			XlsChartSerieDataFormat xlsChartSerieDataFormat;
			for (;;)
			{
				IL_58:
				xlsChartSerieDataFormat = (XlsChartSerieDataFormat)base.MemberwiseClone();
				xlsChartSerieDataFormat.SetParent(parent);
				xlsChartSerieDataFormat.ᜏ();
				xlsChartSerieDataFormat.ᜎ = (sprᲡ)spr\u1CD3.ᜀ(this.ᜎ);
				xlsChartSerieDataFormat.ᜏ = (spr\u25C6)spr\u1CD3.ᜀ(this.ᜏ);
				for (;;)
				{
					IL_9D:
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9D;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 1:
							xlsChartSerieDataFormat.\u171D.ForeColorObject.ᜀ(this.\u171D.ForeColorObject, false);
							num = 15;
							continue;
						case 2:
							if (xlsChartSerieDataFormat.\u171D.ForeColorObject != this.\u171D.ForeColorObject)
							{
								num = 1;
								continue;
							}
							goto IL_163;
						case 3:
							if (xlsChartSerieDataFormat.IsSupportFill)
							{
								num = 0;
								continue;
							}
							return xlsChartSerieDataFormat;
						case 4:
							num = 3;
							continue;
						case 5:
							if (xlsChartSerieDataFormat.\u171D.BackColorObject != this.\u171D.BackColorObject)
							{
								num = 14;
								continue;
							}
							return xlsChartSerieDataFormat;
						case 6:
							if (this.\u171B != null)
							{
								num = 11;
								continue;
							}
							goto IL_294;
						case 7:
							if (this.\u171A != null)
							{
								num = 12;
								continue;
							}
							goto IL_13D;
						case 8:
							num = 17;
							continue;
						case 9:
							num = 10;
							continue;
						case 10:
							if (this.IsInteriorSupported)
							{
								num = 4;
								continue;
							}
							return xlsChartSerieDataFormat;
						case 11:
							xlsChartSerieDataFormat.\u171B = this.\u171B.Clone(xlsChartSerieDataFormat);
							num = 16;
							continue;
						case 12:
							xlsChartSerieDataFormat.\u171A = this.\u171A.Clone(xlsChartSerieDataFormat);
							num = 13;
							continue;
						case 13:
							goto IL_13D;
						case 14:
							xlsChartSerieDataFormat.\u171D.BackColorObject.ᜀ(this.\u171D.BackColorObject, false);
							num = 19;
							continue;
						case 15:
							if (true)
							{
							}
							goto IL_163;
						case 16:
							goto IL_294;
						case 17:
							if (!this.\u1719.IsParsed)
							{
								num = 9;
								continue;
							}
							return xlsChartSerieDataFormat;
						case 18:
							if (!this.\u1719.TypeChanging)
							{
								num = 8;
								continue;
							}
							return xlsChartSerieDataFormat;
						case 19:
							return xlsChartSerieDataFormat;
						}
						goto IL_58;
						IL_13D:
						num = 6;
						continue;
						IL_163:
						num = 5;
						continue;
						IL_294:
						xlsChartSerieDataFormat.ᜐ = (spr\u2299)spr\u1CD3.ᜀ(this.ᜐ);
						xlsChartSerieDataFormat.\u1713 = (sprᣐ)spr\u1CD3.ᜀ(this.\u1713);
						xlsChartSerieDataFormat.\u1714 = (sprή)spr\u1CD3.ᜀ(this.\u1714);
						xlsChartSerieDataFormat.\u1715 = (spr\u239E)spr\u1CD3.ᜀ(this.\u1715);
						xlsChartSerieDataFormat.\u171D = (spr\u2436)this.\u171D.Clone(xlsChartSerieDataFormat);
						num = 18;
					}
				}
			}
			return xlsChartSerieDataFormat;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000B6260 File Offset: 0x000B5260
		public void UpdateSerieIndex()
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
			this.ᜎ.ᜁ((ushort)this.\u1717.Index);
			this.ᜎ.ᜀ((ushort)this.\u1717.Number);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000B62CC File Offset: 0x000B52CC
		public void UpdateDataFormatInDataPoint()
		{
			int a_ = 3;
			if (this.ParentSerie != null)
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
					this.ᜎ.ᜁ((ushort)this.ParentSerie.Index);
					this.ᜎ.ᜀ((ushort)this.ParentSerie.Number);
					return;
				}
			}
			throw new ArgumentException(RecordTableEnumerator.b("椸娺似娾⽀㝂", a_));
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000B635C File Offset: 0x000B535C
		public void ChangeRadarDataFormat(ExcelChartType type)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.LineProperties.UseDefaultFormat = false;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 1:
					goto IL_7B;
				case 2:
					this.MarkerForegroundKnownColor = (ExcelColors)77;
					this.MarkerBackgroundKnownColor = (ExcelColors)77;
					this.LineProperties.UseDefaultFormat = true;
					this.\u171A.UseDefaultLineColor = true;
					this.IsAutoMarker = false;
					this.MarkerStyle = ChartMarkerType.None;
					num = 1;
					continue;
				case 3:
					if (type == ExcelChartType.RadarMarkers)
					{
						num = 0;
						continue;
					}
					return;
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					return;
				}
				if (type == ExcelChartType.Radar)
				{
					num = 2;
					continue;
				}
				IL_7B:
				num = 3;
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000B6440 File Offset: 0x000B5440
		public void ChangeScatterDataFormat(ExcelChartType type)
		{
			int num = 0;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 1:
					goto IL_12F;
				case 2:
					if (!flag)
					{
						num = 16;
						continue;
					}
					goto IL_14D;
				case 3:
					goto IL_17B;
				case 4:
					if (type == ExcelChartType.ScatterMarkers)
					{
						num = 1;
						continue;
					}
					goto IL_79;
				case 5:
					num = 2;
					continue;
				case 6:
					if (type == ExcelChartType.ScatterMarkers)
					{
						num = 5;
						continue;
					}
					return;
				case 7:
					goto IL_79;
				case 8:
					goto IL_99;
				case 9:
					goto IL_164;
				case 10:
					return;
				case 11:
					if (type == ExcelChartType.ScatterSmoothedLineMarkers)
					{
						num = 9;
						continue;
					}
					goto IL_99;
				case 12:
					this.\u171F.ᜀ((ExcelColors)77, !flag);
					num = 3;
					continue;
				case 13:
					if (flag)
					{
						goto IL_122;
					}
					goto IL_17B;
				case 14:
					num = 4;
					continue;
				case 15:
					num = 11;
					continue;
				case 16:
					this.\u171A.Pattern = ChartLinePatternType.None;
					if (true)
					{
					}
					num = 20;
					continue;
				case 17:
					if (type != ExcelChartType.ScatterSmoothedLine)
					{
						num = 15;
						continue;
					}
					goto IL_164;
				case 18:
					if (type != ExcelChartType.ScatterSmoothedLineMarkers)
					{
						num = 14;
						continue;
					}
					goto IL_12F;
				case 19:
					goto IL_74;
				case 20:
					goto IL_14D;
				}
				if (type == ExcelChartType.ScatterLineMarkers)
				{
					num = 19;
					continue;
				}
				flag = ((ChartSeries)this.\u1719.Series).IsSerieCreating;
				num = 13;
				continue;
				IL_79:
				num = 6;
				continue;
				IL_99:
				num = 18;
				continue;
				IL_122:
				num = 12;
				continue;
				IL_17B:
				this.MarkerSize = 5;
				this.MarkerStyle = ChartMarkerType.None;
				this.LineProperties.UseDefaultFormat = true;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_122;
				default:
					if (false)
					{
					}
					num = 17;
					continue;
				}
				IL_12F:
				this.MarkerStyle = ChartMarkerType.Diamond;
				this.IsAutoMarker = true;
				num = 7;
				continue;
				IL_14D:
				this.\u1713 = null;
				num = 10;
				continue;
				IL_164:
				this.IsSmoothedLine = true;
				num = 8;
			}
			IL_74:
			this.LineProperties.Pattern = ChartLinePatternType.None;
			this.\u171A.UseDefaultFormat = true;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000B66A8 File Offset: 0x000B56A8
		public void ChangeLineDataFormat(ExcelChartType type)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_12F;
				case 1:
					if (type == ExcelChartType.Line100PercentStacked)
					{
						num = 0;
						continue;
					}
					goto IL_7C;
				case 2:
					goto IL_7C;
				case 3:
					goto IL_B3;
				case 4:
					if (true)
					{
					}
					num = 7;
					continue;
				case 5:
					num = 13;
					continue;
				case 6:
					num = 9;
					continue;
				case 7:
					if (type == ExcelChartType.LineMarkers100PercentStacked)
					{
						num = 3;
						continue;
					}
					return;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13D;
					default:
						if (false)
						{
						}
						if (type != ExcelChartType.LineStacked)
						{
							num = 12;
							continue;
						}
						goto IL_12F;
					}
					break;
				case 10:
					return;
				case 11:
					if (type != ExcelChartType.LineMarkers)
					{
						num = 5;
						continue;
					}
					goto IL_B3;
				case 12:
					num = 1;
					continue;
				case 13:
					if (type != ExcelChartType.LineMarkersStacked)
					{
						num = 4;
						continue;
					}
					goto IL_B3;
				}
				if (type != ExcelChartType.Line)
				{
					num = 6;
					continue;
				}
				goto IL_12F;
				IL_7C:
				num = 11;
				continue;
				IL_B3:
				this.LineProperties.UseDefaultFormat = false;
				num = 10;
				continue;
				IL_13D:
				num = 2;
				continue;
				IL_12F:
				this.IsAutoMarker = false;
				this.MarkerStyle = ChartMarkerType.None;
				goto IL_13D;
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000B6804 File Offset: 0x000B5804
		internal void ᜃ(bool A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						int count;
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						XlsChartSeries xlsChartSeries;
						IChartSerieDataFormat dataFormat = xlsChartSeries[num2].DataPoints.DefaultDataPoint.DataFormat;
						num = 9;
						continue;
					}
					case 1:
						return;
					case 2:
						goto IL_CE;
					case 3:
						goto IL_82;
					case 4:
						goto IL_CE;
					case 6:
					{
						if (true)
						{
						}
						XlsChartSeries xlsChartSeries = this.\u1719.Series;
						num2 = 0;
						int count = xlsChartSeries.Count;
						goto IL_C0;
					}
					case 7:
						goto IL_82;
					case 8:
					{
						IChartSerieDataFormat dataFormat;
						dataFormat.BarTopType = this.BarTopType;
						num = 3;
						continue;
					}
					case 9:
					{
						if (A_0)
						{
							num = 8;
							continue;
						}
						IChartSerieDataFormat dataFormat;
						dataFormat.BarType = this.BarType;
						num = 7;
						continue;
					}
					}
					if (this.\u1717 != null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C0;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					IL_82:
					num2++;
					num = 4;
					continue;
					IL_C0:
					num = 2;
					continue;
					IL_CE:
					num = 0;
				}
				return;
			}
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000B695C File Offset: 0x000B595C
		public int UpdateLineColor()
		{
			int a_ = 4;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return -1;
				default:
				{
					if (false)
					{
					}
					ExcelChartType excelChartType = this.SerieType;
					string a = XlsChartFormat.ᜉ(excelChartType);
					int num = 9;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch (num)
						{
						case 0:
							flag = true;
							goto IL_CC;
						case 1:
							if (!(a == RecordTableEnumerator.b("瘹唻倽┿", a_)))
							{
								num = 2;
								continue;
							}
							goto IL_E8;
						case 2:
							num = 4;
							continue;
						case 3:
							num = 1;
							continue;
						case 4:
							flag = (a == RecordTableEnumerator.b("瘹唻倽┿", a_));
							goto IL_CC;
						case 5:
							if (true)
							{
							}
							num = 6;
							continue;
						case 6:
							if (excelChartType != ExcelChartType.Radar)
							{
								num = 3;
								continue;
							}
							goto IL_E8;
						case 7:
							goto IL_E6;
						case 8:
							if (!flag2)
							{
								num = 7;
								continue;
							}
							goto IL_127;
						case 9:
							if (excelChartType != ExcelChartType.RadarMarkers)
							{
								num = 5;
								continue;
							}
							goto IL_E8;
						}
						break;
						IL_CC:
						flag2 = flag;
						num = 8;
						continue;
						IL_E8:
						num = 0;
					}
					break;
				}
				}
			}
			return -1;
			IL_E6:
			return -1;
			IL_127:
			return XlsChartSerieDataFormat.UpdateColor(this.\u1717, this.\u1716);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000B6AA4 File Offset: 0x000B5AA4
		public static int UpdateColor(XlsChartSerie serie, XlsChartDataPoint dataPoint)
		{
			int num = 2;
			int num2;
			for (;;)
			{
				int index;
				switch (num)
				{
				case 0:
					index = serie.Index;
					goto IL_83;
				case 1:
					return 24;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C0;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					goto IL_99;
				case 4:
					if (num2 <= 30)
					{
						num = 3;
						continue;
					}
					goto IL_CD;
				case 5:
					index = dataPoint.Index;
					goto IL_83;
				case 6:
					if (!serie.GetCommonSerieFormat().IsVaryColor)
					{
						goto IL_C0;
					}
					num = 5;
					continue;
				case 7:
					num = 0;
					continue;
				}
				if (serie == null)
				{
					num = 1;
					continue;
				}
				num = 6;
				continue;
				IL_83:
				num2 = index;
				num = 4;
				continue;
				IL_C0:
				num = 7;
			}
			return 24;
			IL_99:
			return num2 + 24;
			IL_CD:
			num2 -= 30;
			return num2 % 55 + 7;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000B6B8C File Offset: 0x000B5B8C
		public void UpdateSerieFormat()
		{
			switch (0)
			{
			default:
			{
				int num = 44;
				for (;;)
				{
					bool flag;
					bool typeChanging;
					bool flag2;
					ExcelChartType destinationType;
					bool flag3;
					bool loading;
					switch (num)
					{
					case 0:
						goto IL_507;
					case 1:
						flag = false;
						goto IL_163;
					case 2:
						goto IL_390;
					case 3:
						if (!typeChanging)
						{
							num = 6;
							continue;
						}
						num = 1;
						continue;
					case 4:
						goto IL_224;
					case 5:
						goto IL_263;
					case 6:
						num = 18;
						continue;
					case 7:
						goto IL_10E;
					case 8:
						flag = true;
						goto IL_163;
					case 9:
						num = 13;
						continue;
					case 10:
						flag2 = true;
						goto IL_463;
					case 11:
						if (typeChanging)
						{
							num = 23;
							continue;
						}
						goto IL_263;
					case 12:
						this.\u171B = new ChartInterior((spr\u2158)base.ReservedHandle, this);
						num = 0;
						continue;
					case 13:
						if (!this.\u1719.ParentWorkbook.Loading)
						{
							num = 16;
							continue;
						}
						goto IL_224;
					case 14:
						flag2 = this.IsInteriorSupported;
						goto IL_463;
					case 15:
						num = 24;
						continue;
					case 16:
						this.\u1713 = (sprᣐ)spr\u175E.ᜀ(TBIFFRecord.ChartMarkerFormat);
						num = 4;
						continue;
					case 17:
						flag2 = false;
						goto IL_463;
					case 18:
						flag = this.IsBorderSupported;
						goto IL_163;
					case 19:
						if (this.\u171A == null)
						{
							num = 25;
							continue;
						}
						goto IL_390;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3F7;
						default:
							if (false)
							{
							}
							if (!typeChanging)
							{
								num = 47;
								continue;
							}
							num = 39;
							continue;
						}
						break;
					case 21:
						this.ᜐ = (spr\u2299)spr\u175E.ᜀ(TBIFFRecord.ChartPieFormat);
						num = 33;
						continue;
					case 22:
						num = 11;
						continue;
					case 23:
						num = 45;
						continue;
					case 24:
						if (!XlsChartSerieDataFormat.ᜀ(destinationType))
						{
							num = 7;
							continue;
						}
						num = 8;
						continue;
					case 25:
						num = 43;
						continue;
					case 26:
						num = 46;
						continue;
					case 27:
						if (this.\u1715 == null)
						{
							num = 36;
							continue;
						}
						goto IL_498;
					case 28:
						this.\u171A = new ChartBorder((spr\u2158)base.ReservedHandle, this);
						num = 2;
						continue;
					case 29:
						flag = false;
						goto IL_163;
					case 30:
						goto IL_343;
					case 31:
						if (true)
						{
						}
						goto IL_498;
					case 32:
						if (this.\u1713 == null)
						{
							num = 9;
							continue;
						}
						goto IL_224;
					case 33:
						goto IL_368;
					case 34:
						this.ᜏ = (spr\u25C6)spr\u175E.ᜀ(TBIFFRecord.Chart3DDataFormat);
						num = 30;
						continue;
					case 35:
						if (flag3)
						{
							num = 12;
							continue;
						}
						goto IL_509;
					case 36:
						this.\u1715 = (spr\u239E)spr\u175E.ᜀ(TBIFFRecord.ChartSerFmt);
						num = 31;
						continue;
					case 37:
						if (!loading)
						{
							num = 26;
							continue;
						}
						num = 29;
						continue;
					case 38:
						if (this.ᜐ == null)
						{
							num = 21;
							continue;
						}
						goto IL_368;
					case 39:
						flag2 = false;
						goto IL_463;
					case 40:
						if (this.\u171B == null)
						{
							num = 42;
							continue;
						}
						goto IL_509;
					case 41:
						if (!loading)
						{
							num = 22;
							continue;
						}
						num = 17;
						continue;
					case 42:
						num = 35;
						continue;
					case 43:
						if (flag3)
						{
							num = 28;
							continue;
						}
						goto IL_390;
					case 45:
						if (!XlsChartSerieDataFormat.ᜁ(destinationType))
						{
							num = 5;
							continue;
						}
						num = 10;
						continue;
					case 46:
						if (typeChanging)
						{
							num = 15;
							continue;
						}
						goto IL_10E;
					case 47:
						goto IL_3F7;
					}
					if (this.ᜏ == null)
					{
						num = 34;
						continue;
					}
					goto IL_343;
					IL_10E:
					num = 3;
					continue;
					IL_163:
					flag3 = flag;
					num = 19;
					continue;
					IL_224:
					num = 27;
					continue;
					IL_263:
					num = 20;
					continue;
					IL_343:
					num = 38;
					continue;
					IL_368:
					num = 32;
					continue;
					IL_390:
					num = 41;
					continue;
					IL_3F7:
					num = 14;
					continue;
					IL_463:
					flag3 = flag2;
					num = 40;
					continue;
					IL_498:
					typeChanging = this.\u1719.TypeChanging;
					loading = this.\u1719.ParentWorkbook.Loading;
					destinationType = this.\u1719.DestinationType;
					num = 37;
				}
				IL_507:
				IL_509:
				this.\u171C = true;
				return;
			}
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000B70AC File Offset: 0x000B60AC
		private bool ᜀ(spr\u25C6 A_0)
		{
			int a_ = 14;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 5;
					continue;
				case 2:
					goto IL_BB;
				case 3:
					if (A_0.ᜀ() == BaseFormatType.Rectangle)
					{
						num = 0;
						continue;
					}
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
						continue;
					}
					break;
				case 4:
					goto IL_3C;
				case 5:
					goto IL_58;
				}
				IL_31:
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				goto IL_31;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍", a_));
			IL_58:
			return A_0.ᜁ() != TopFormatType.Straight;
			IL_BB:
			return true;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000B717C File Offset: 0x000B617C
		public void ClearOnPropertyChange()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u1716.ClearDataFormats(this);
					num = 4;
					continue;
				case 2:
					num = 6;
					continue;
				case 3:
					if (this.\u1716 != null)
					{
						goto IL_E3;
					}
					goto IL_F3;
				case 4:
					goto IL_F3;
				case 5:
					if (this.\u1718 != null)
					{
						num = 7;
						continue;
					}
					num = 3;
					continue;
				case 6:
					if (this.\u1716.Index == 65535)
					{
						num = 0;
						continue;
					}
					goto IL_F3;
				case 7:
					goto IL_CE;
				case 8:
					return;
				}
				if (this.\u1719.Loading)
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
				IL_E3:
				num = 2;
				continue;
				IL_F3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E3;
				default:
					goto IL_109;
				}
			}
			return;
			IL_CE:
			XlsChartSeries xlsChartSeries = this.\u1719.Series;
			xlsChartSeries.ᜀ(this);
			return;
			IL_109:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000B72A0 File Offset: 0x000B62A0
		private bool ᜃ()
		{
			int a_ = 14;
			ExcelChartType excelChartType;
			for (;;)
			{
				excelChartType = this.SerieType;
				string a = XlsChartFormat.ᜉ(excelChartType);
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						if (!flag)
						{
							return false;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_105;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 1:
						num = 3;
						continue;
					case 2:
						if (!(a == RecordTableEnumerator.b("ࡃ⽅♇⽉", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_9E;
					case 3:
						flag2 = (a == RecordTableEnumerator.b("ᝃ╅⥇㹉㡋⭍≏", a_));
						goto IL_104;
					case 4:
						num = 9;
						continue;
					case 5:
						num = 7;
						continue;
					case 6:
						goto IL_9C;
					case 7:
						if (!(a == RecordTableEnumerator.b("ᙃ❅ⱇ⭉㹋", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_9E;
					case 8:
						flag2 = true;
						goto IL_104;
					case 9:
						if (excelChartType != ExcelChartType.Line3D)
						{
							num = 6;
							continue;
						}
						return false;
					}
					break;
					IL_9E:
					num = 8;
					continue;
					IL_105:
					num = 0;
					continue;
					IL_104:
					flag = flag2;
					goto IL_105;
				}
			}
			IL_9C:
			return excelChartType != ExcelChartType.RadarFilled;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000B73F0 File Offset: 0x000B63F0
		private static bool ᜁ(ExcelChartType A_0)
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
			return true;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000B742C File Offset: 0x000B642C
		private static bool ᜀ(ExcelChartType A_0)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			string a = XlsChartFormat.ᜉ(A_0);
			return !(a == RecordTableEnumerator.b("ᩈ㹊㽌⥎ぐげご", a_));
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000B7490 File Offset: 0x000B6490
		private void ᜂ()
		{
			ExcelColors excelColors;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_71:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜢ = null;
						num = 2;
						continue;
					case 1:
						if (!this.\u1719.ParentWorkbook.Loading)
						{
							num = 0;
							continue;
						}
						goto IL_CE;
					case 2:
						goto IL_96;
					case 3:
						this.MarkerFormat.ᜂ(excelColors == ExcelColors.Black);
						this.ᜠ = null;
						num = 1;
						continue;
					}
					goto IL_46;
				}
				IL_96:
				IL_CE:
				this.ClearOnPropertyChange();
				return;
			}
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_46:
			this.IsAutoMarker = false;
			excelColors = this.\u171F.ᜂ(this.\u1719.Workbook);
			this.MarkerFormat.ᜀ((ushort)excelColors);
			goto IL_71;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000B7574 File Offset: 0x000B6574
		private void ᜁ()
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
			this.IsAutoMarker = false;
			ExcelColors excelColors = this.\u171E.ᜂ(this.\u1719.Workbook);
			this.MarkerFormat.ᜁ((ushort)excelColors);
			this.MarkerFormat.ᜀ(excelColors == ExcelColors.Black);
			this.ClearOnPropertyChange();
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000B75F8 File Offset: 0x000B65F8
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x000B7640 File Offset: 0x000B6640
		public bool HasLineProperties
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
				return this.\u171A != null;
			}
			internal set
			{
				for (;;)
				{
					IL_00:
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (value)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 3:
							this.\u171A = new ChartBorder((spr\u2158)base.AppImplementation, this);
							num = 0;
							continue;
						}
						if (this.\u171A != null)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000B76EC File Offset: 0x000B66EC
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x000B7734 File Offset: 0x000B6734
		public bool HasShadow
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
				return this.\u1712 != null;
			}
			internal set
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
					if (value)
					{
						if (true)
						{
						}
						ChartShadow shadow = this.Shadow;
						return;
					}
					break;
				}
				this.\u1712 = null;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x000B7784 File Offset: 0x000B6784
		public Format3D Format3D
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								goto IL_76;
							case 1:
								this.ᜑ = new Format3D(base.AppImplementation, this);
								if (true)
								{
								}
								num = 0;
								continue;
							}
							if (this.ᜑ != null)
							{
								goto IL_78;
							}
							num = 1;
							break;
						}
					}
				}
				IL_76:
				IL_78:
				return this.ᜑ;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000B7810 File Offset: 0x000B6810
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x000B7858 File Offset: 0x000B6858
		public bool HasFormat3D
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
				return this.ᜑ != null;
			}
			internal set
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
					if (false)
					{
					}
					if (value)
					{
						Format3D format3D = this.Format3D;
						return;
					}
					break;
				}
				this.ᜑ = null;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000B78A8 File Offset: 0x000B68A8
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x000B78F0 File Offset: 0x000B68F0
		public bool HasInterior
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
				return this.\u171B != null;
			}
			internal set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							return;
						case 2:
							this.\u171B = new ChartInterior((spr\u2158)base.AppImplementation, this);
							num = 1;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 4:
							if (value)
							{
								num = 2;
								continue;
							}
							return;
						}
						if (true)
						{
						}
						if (this.\u171B != null)
						{
							return;
						}
						num = 3;
					}
				}
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000B79A0 File Offset: 0x000B69A0
		public ChartShadow Shadow
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								goto IL_76;
							case 2:
								this.\u1712 = new ChartShadow(base.AppImplementation, this);
								num = 0;
								continue;
							}
							if (true)
							{
							}
							if (this.\u1712 != null)
							{
								goto IL_78;
							}
							num = 2;
							break;
						}
					}
				}
				IL_76:
				IL_78:
				return this.\u1712;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x000B7A2C File Offset: 0x000B6A2C
		public ChartBorder LineProperties
		{
			get
			{
				int a_ = 2;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.HasLineProperties = true;
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_7F;
						}
						break;
					case 2:
						goto IL_E7;
					case 3:
						num = 4;
						continue;
					case 4:
						if (!this.IsBorderSupported)
						{
							num = 6;
							continue;
						}
						goto IL_87;
					case 6:
						goto IL_D4;
					case 7:
						if (true)
						{
						}
						if (this.\u1719.ParentWorkbook.Loading)
						{
							num = 0;
							continue;
						}
						this.UpdateSerieFormat();
						num = 2;
						continue;
					}
					if (!this.\u1719.TypeChanging)
					{
						num = 3;
						continue;
					}
					IL_87:
					num = 7;
				}
				IL_7F:
				if (false)
				{
				}
				goto IL_FD;
				IL_D4:
				throw new NotSupportedException(RecordTableEnumerator.b("氷刹唻䴽怿㉁㙃⥅㡇⽉㹋㩍⥏牑こ㥕⭗㑙筛⩝䁟ᅡᅣᙥᡧթṫᩭ偯᭱ᩳ噵౷ቹᕻൽꁿ慎ﺉ겋揄", a_));
				IL_E7:
				IL_FD:
				this.\u171C = true;
				return this.\u171A as ChartBorder;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x000B7B48 File Offset: 0x000B6B48
		public IChartInterior AreaProperties
		{
			get
			{
				int a_ = 16;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u1719.IsParsed)
						{
							num = 12;
							continue;
						}
						goto IL_94;
					case 1:
					{
						ExcelColors knownColor = (ExcelColors)XlsChartSerieDataFormat.UpdateColor(this.\u1717, this.\u1716);
						this.\u171B.ForegroundColorObject.SetKnownColor(knownColor);
						this.\u171B.UseDefaultFormat = true;
						num = 7;
						continue;
					}
					case 2:
						if (!this.IsInteriorSupported)
						{
							num = 6;
							continue;
						}
						goto IL_94;
					case 3:
						if (this.\u1719.IsParsed)
						{
							num = 9;
							continue;
						}
						goto IL_1B5;
					case 5:
						if (this.\u171B.UseDefaultFormat)
						{
							num = 1;
							continue;
						}
						goto IL_1B5;
					case 6:
						goto IL_10D;
					case 7:
						goto IL_14C;
					case 8:
						num = 3;
						continue;
					case 9:
						num = 5;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14E;
						default:
							if (false)
							{
							}
							if (!this.\u1719.TypeChanging)
							{
								num = 8;
								continue;
							}
							goto IL_1B5;
						}
						break;
					case 11:
						if (true)
						{
						}
						goto IL_14E;
					case 12:
						num = 2;
						continue;
					}
					if (!this.\u1719.TypeChanging)
					{
						num = 11;
						continue;
					}
					IL_94:
					this.UpdateSerieFormat();
					this.\u171C = true;
					num = 10;
					continue;
					IL_14E:
					num = 0;
				}
				IL_10D:
				throw new NotSupportedException(RecordTableEnumerator.b("݅㩇⽉ⵋṍ≏㵑⑓㍕⩗⹙㕛㭝፟䉡ݣݥ٧ѩͫᩭ偯ၱᅳ噵୷ཹ౻๽ꚅﺋ꺍晴ﶓ뢗蓮ﾝ튟횡蒣튥톧\udaa9즫肭", a_));
				IL_14C:
				IL_1B5:
				return this.\u171B;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x000B7D10 File Offset: 0x000B6D10
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x000B7D58 File Offset: 0x000B6D58
		public BaseFormatType BarType
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
				return this.Serie3DDataFormat.ᜀ();
			}
			set
			{
				int a_ = 15;
				if (true)
				{
				}
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E9;
					case 1:
					{
						bool loading;
						if (!loading)
						{
							num = 3;
							continue;
						}
						goto IL_D1;
					}
					case 2:
						goto IL_D1;
					case 3:
						this.ᜃ(false);
						num = 2;
						continue;
					case 4:
					{
						bool loading = this.\u1719.Loading;
						goto IL_B6;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B6;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 6:
					{
						bool loading;
						if (!loading)
						{
							num = 7;
							continue;
						}
						goto IL_116;
					}
					case 7:
						num = 9;
						continue;
					case 8:
						goto IL_114;
					case 9:
						if (Array.IndexOf<ExcelChartType>(XlsChartSerieDataFormat.\u170D, this.SerieType) == -1)
						{
							num = 8;
							continue;
						}
						goto IL_116;
					}
					if (value != this.BarType)
					{
						num = 4;
						continue;
					}
					break;
					IL_B6:
					num = 6;
					continue;
					IL_D1:
					this.\u171C = true;
					this.ClearOnPropertyChange();
					num = 0;
					continue;
					IL_116:
					this.Serie3DDataFormat.ᜀ(value);
					num = 1;
				}
				IL_E9:
				return;
				IL_114:
				throw new NotSupportedException(RecordTableEnumerator.b("݄♆㭈Ὂ㑌㽎㑐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ᩨṪᵬὮṰŲŴ坶ὸᑺོ彾ꦈ떔놞", a_));
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000B7EA8 File Offset: 0x000B6EA8
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x000B7EF0 File Offset: 0x000B6EF0
		public TopFormatType BarTopType
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
				return this.Serie3DDataFormat.ᜁ();
			}
			set
			{
				int a_ = 0;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (Array.IndexOf<ExcelChartType>(XlsChartSerieDataFormat.\u170D, this.SerieType) == -1)
						{
							num = 3;
							continue;
						}
						goto IL_111;
					case 1:
						goto IL_C9;
					case 2:
						goto IL_E1;
					case 3:
						goto IL_10C;
					case 4:
					{
						if (true)
						{
						}
						bool loading;
						if (!loading)
						{
							num = 8;
							continue;
						}
						goto IL_C9;
					}
					case 5:
					{
						bool loading;
						if (!loading)
						{
							num = 9;
							continue;
						}
						goto IL_111;
					}
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AE;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 7:
					{
						bool loading = this.\u1719.Loading;
						goto IL_AE;
					}
					case 8:
						this.ᜃ(true);
						num = 1;
						continue;
					case 9:
						num = 0;
						continue;
					}
					if (value != this.BarTopType)
					{
						num = 7;
						continue;
					}
					break;
					IL_AE:
					num = 5;
					continue;
					IL_C9:
					this.\u171C = true;
					this.ClearOnPropertyChange();
					num = 2;
					continue;
					IL_111:
					this.Serie3DDataFormat.ᜀ(value);
					num = 4;
				}
				IL_E1:
				return;
				IL_10C:
				throw new NotSupportedException(RecordTableEnumerator.b("琵夷䠹栻儽〿ᙁ㵃㙅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝፟ᝡᑣᙥݧᡩᡫ乭ᙯᵱٳ噵౷ቹᕻൽꁿ慎ﺉ겋揄뢕", a_));
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000B8040 File Offset: 0x000B7040
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x000B8094 File Offset: 0x000B7094
		public Color MarkerBackgroundColor
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
				return this.\u1719.Workbook.GetPaletteColor(this.MarkerForegroundKnownColor);
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
				this.MarkerBackgroundKnownColor = this.\u1719.Workbook.GetNearestColor(value);
				this.MarkerFormat.ᜁ(value.ToArgb() & 16777215);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x000B8100 File Offset: 0x000B7100
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x000B8154 File Offset: 0x000B7154
		public Color MarkerForegroundColor
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
				return this.\u1719.Workbook.GetPaletteColor(this.MarkerBackgroundKnownColor);
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
				this.MarkerForegroundKnownColor = this.\u1719.Workbook.GetNearestColor(value);
				this.MarkerFormat.ᜂ(value.ToArgb() & 16777215);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000B81C0 File Offset: 0x000B71C0
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x000B82AC File Offset: 0x000B72AC
		public ChartMarkerType MarkerStyle
		{
			get
			{
				int a_ = 0;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4B;
						default:
							goto IL_6B;
						}
						break;
					case 1:
						num = 5;
						continue;
					case 3:
						if (!this.\u1719.TypeChanging)
						{
							num = 1;
							continue;
						}
						goto IL_D2;
					case 4:
						num = 3;
						continue;
					case 5:
						if (!this.ᜃ())
						{
							num = 0;
							continue;
						}
						goto IL_D2;
					}
					goto IL_31;
					IL_4B:
					num = 4;
					continue;
					IL_31:
					if (!this.\u1719.Loading)
					{
						goto IL_4B;
					}
					goto IL_D2;
				}
				IL_6B:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("笵夷䠹圻嬽㈿ᅁぃ㽅⑇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟ᅡᅣᙥᡧթṫᩭ偯ᑱ᭳ѵ塷๹ᑻ᝽ꊁ꺍뚗", a_));
				IL_D2:
				return this.MarkerFormat.ᜂ();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.MarkerFormat.ᜀ(value);
						this.IsAutoMarker = false;
						num = 4;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.ClearOnPropertyChange();
							num = 0;
							continue;
						}
						break;
					case 4:
						if (!this.\u1719.TypeChanging)
						{
							num = 3;
							continue;
						}
						return;
					}
					IL_24:
					if (this.MarkerStyle != value)
					{
						num = 1;
						continue;
					}
					break;
					goto IL_24;
				}
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000B8360 File Offset: 0x000B7360
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x000B844C File Offset: 0x000B744C
		public ExcelColors MarkerForegroundKnownColor
		{
			get
			{
				int a_ = 1;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4B;
						default:
							goto IL_73;
						}
						break;
					case 1:
						num = 4;
						continue;
					case 2:
						if (!this.\u1719.TypeChanging)
						{
							num = 1;
							continue;
						}
						goto IL_D2;
					case 4:
						if (!this.ᜃ())
						{
							num = 0;
							continue;
						}
						goto IL_D2;
					case 5:
						num = 2;
						continue;
					}
					goto IL_31;
					IL_4B:
					num = 5;
					continue;
					IL_31:
					if (!this.\u1719.Loading)
					{
						goto IL_4B;
					}
					goto IL_D2;
				}
				IL_73:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("稶堸䤺嘼娾㍀Ղ⩄㕆ⱈⱊ㽌⁎⑐㵒ㅔᑖ㙘㝚㉜ⵞ⡠ൢŤɦᅨ䭪๬๮ὰᵲᩴͶ奸᥺᡼彾力歷꾎ﲒ랖膠삢춤욦\udba8\udfaa趬\udbae좰쎲킴馶", a_));
				IL_D2:
				return (ExcelColors)this.MarkerFormat.ᜋ();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.MarkerFormat.ᜂ(value == ExcelColors.Black);
						this.ClearOnPropertyChange();
						num = 2;
						continue;
					case 2:
						return;
					case 3:
						this.IsAutoMarker = false;
						this.MarkerFormat.ᜀ((ushort)value);
						num = 1;
						continue;
					}
					IL_20:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_20;
					default:
						if (false)
						{
						}
						if (this.MarkerForegroundKnownColor == value)
						{
							return;
						}
						num = 3;
						break;
					}
				}
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000B8500 File Offset: 0x000B7500
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x000B85E4 File Offset: 0x000B75E4
		public ExcelColors MarkerBackgroundKnownColor
		{
			get
			{
				int a_ = 7;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						goto IL_89;
					case 2:
						num = 5;
						continue;
					case 3:
						num = 0;
						continue;
					case 5:
						if (this.\u1719.TypeChanging)
						{
							goto IL_C9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					if (!this.\u1719.Loading)
					{
						num = 2;
						continue;
					}
					goto IL_C9;
					IL_6F:
					if (this.ᜃ())
					{
						goto IL_C9;
					}
					num = 1;
				}
				IL_89:
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("瀼帾㍀⡂⁄㕆ୈ⩊⹌⑎㙐⅒㩔≖㝘㽚ᙜㅞ๠ᑢ୤⑦٨ݪɬᵮ兰ၲᑴ᥶᝸ᑺॼ彾ꖄﲈﮊﶌ떔붜즠쪢횤螦쪨쎪첬\uddae얰鎲솴캶즸\udeba鎼", a_));
				IL_C9:
				return (ExcelColors)this.MarkerFormat.ᜊ();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.MarkerFormat.ᜀ(value == ExcelColors.Black);
						this.ClearOnPropertyChange();
						if (true)
						{
						}
						num = 3;
						continue;
					case 1:
						this.IsAutoMarker = false;
						this.MarkerFormat.ᜁ((ushort)value);
						num = 0;
						continue;
					case 3:
						return;
					}
					IL_20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_20;
					default:
						if (false)
						{
						}
						if (this.MarkerBackgroundKnownColor == value)
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000B8698 File Offset: 0x000B7698
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x000B877C File Offset: 0x000B777C
		public int MarkerSize
		{
			get
			{
				int a_ = 18;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_81;
					case 3:
						if (this.\u1719.TypeChanging)
						{
							goto IL_C9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 4:
						num = 5;
						continue;
					case 5:
						goto IL_67;
					}
					if (!this.\u1719.Loading)
					{
						num = 1;
						continue;
					}
					goto IL_C9;
					IL_67:
					if (this.ᜃ())
					{
						goto IL_C9;
					}
					num = 2;
				}
				IL_81:
				throw new NotSupportedException(RecordTableEnumerator.b("Շ⭉㹋╍㕏⁑ݓ㽕≗㽙籛㵝şౡ੣॥ᱧ䩩๫୭偯űųٵࡷᕹ๻੽ꁿꢇﺉ늑ﺕ聯뺝풟\udba1풣쎥蚧蒩", a_));
				IL_C9:
				return this.MarkerFormat.ᜉ() / 20;
			}
			set
			{
				int a_ = 3;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F6;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11F;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						if (true)
						{
						}
						if (value >= 2)
						{
							num = 4;
							continue;
						}
						goto IL_89;
					case 4:
						num = 9;
						continue;
					case 5:
						this.MarkerFormat.ᜀ(ChartMarkerType.Square);
						num = 8;
						continue;
					case 6:
						if (this.MarkerFormat.ᜇ())
						{
							goto IL_11F;
						}
						goto IL_C1;
					case 7:
						goto IL_D9;
					case 8:
						goto IL_C1;
					case 9:
						if (value > 72)
						{
							num = 0;
							continue;
						}
						this.MarkerFormat.ᜀ(value * 20);
						num = 6;
						continue;
					}
					if (value != this.MarkerSize)
					{
						num = 2;
						continue;
					}
					return;
					IL_C1:
					this.IsAutoMarker = false;
					this.ClearOnPropertyChange();
					num = 7;
					continue;
					IL_11F:
					num = 5;
				}
				IL_89:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("琸娺似吾⑀ㅂᙄ⹆㍈⹊", a_));
				IL_D9:
				return;
				IL_F6:
				goto IL_89;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000B88C4 File Offset: 0x000B78C4
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x000B89A8 File Offset: 0x000B79A8
		public bool IsAutoMarker
		{
			get
			{
				int a_ = 17;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						if (this.\u1719.TypeChanging)
						{
							goto IL_C9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 2:
						goto IL_89;
					case 4:
						num = 1;
						continue;
					case 5:
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (!this.\u1719.Loading)
					{
						num = 4;
						continue;
					}
					goto IL_C9;
					IL_6F:
					if (this.ᜃ())
					{
						goto IL_C9;
					}
					num = 2;
				}
				IL_89:
				throw new NotSupportedException(RecordTableEnumerator.b("ๆ㩈੊㡌㭎㹐Ṓ㑔╖㉘㹚⽜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲ٴɶॸ୺ቼൾꎂﮈꮊ歷떔漢膠힢\udca4힦첨薪", a_));
				IL_C9:
				return this.MarkerFormat.ᜇ();
			}
			set
			{
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EB;
						}
						if (false)
						{
						}
						this.MarkerFormat.ᜁ(value);
						num = 2;
						continue;
					case 1:
						if (!this.\u1719.TypeChanging)
						{
							num = 5;
							continue;
						}
						goto IL_FE;
					case 2:
						if (!value)
						{
							num = 4;
							continue;
						}
						goto IL_6C;
					case 3:
						goto IL_FC;
					case 4:
					{
						int num2 = XlsChartSerieDataFormat.UpdateColor(this.\u1717, this.\u1716);
						this.MarkerFormat.ᜁ((ushort)num2);
						this.MarkerFormat.ᜀ((ushort)num2);
						num = 7;
						continue;
					}
					case 5:
						goto IL_EB;
					case 7:
						goto IL_6C;
					}
					if (value != this.IsAutoMarker)
					{
						num = 0;
						continue;
					}
					break;
					IL_6C:
					num = 1;
					continue;
					IL_EB:
					this.ClearOnPropertyChange();
					num = 3;
				}
				IL_FC:
				IL_FE:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x000B8ABC File Offset: 0x000B7ABC
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x000B8B08 File Offset: 0x000B7B08
		public bool IsShowBackground
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
				return !this.MarkerFormat.ᜀ();
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
				this.MarkerFormat.ᜀ(!value);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000B8B54 File Offset: 0x000B7B54
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000B8BA0 File Offset: 0x000B7BA0
		public bool IsShowForeground
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
				return !this.MarkerFormat.ᜅ();
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
				this.MarkerFormat.ᜂ(!value);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000B8BEC File Offset: 0x000B7BEC
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000B8C34 File Offset: 0x000B7C34
		public int Percent
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
				return (int)this.PieFormat.ᜂ();
			}
			set
			{
				int a_ = 12;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string a = XlsChartFormat.ᜉ(this.SerieType);
						num = 1;
						continue;
					}
					case 1:
					{
						string a;
						if (a != RecordTableEnumerator.b("ቁⵃ⍅", a_))
						{
							goto IL_B4;
						}
						goto IL_C1;
					}
					case 2:
					{
						string a;
						if (a != RecordTableEnumerator.b("ف⭃㍅⽇≉≋㭍⑏", a_))
						{
							num = 3;
							continue;
						}
						goto IL_C1;
					}
					case 3:
						goto IL_85;
					case 5:
						num = 2;
						continue;
					}
					if (!this.\u1719.TypeChanging)
					{
						num = 0;
						continue;
					}
					goto IL_C1;
					IL_B4:
					num = 5;
					continue;
					IL_C1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						goto IL_DF;
					}
				}
				IL_85:
				throw new NotSupportedException(RecordTableEnumerator.b("ቁ⅃㑅⭇⽉≋㩍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ᕥᵧᩩᱫŭɯٱ味ၵ᝷ࡹ屻੽ꚅﲍ늑ﾙ늛", a_));
				IL_DF:
				if (false)
				{
				}
				this.PieFormat.ᜀ((ushort)value);
				this.ClearOnPropertyChange();
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000B8D44 File Offset: 0x000B7D44
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x000B8D8C File Offset: 0x000B7D8C
		public bool IsSmoothedLine
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
				return this.SerieFormat.ᜁ();
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
				this.SerieFormat.ᜂ(value);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000B8DD4 File Offset: 0x000B7DD4
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x000B8E1C File Offset: 0x000B7E1C
		public bool Is3DBubbles
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
				return this.SerieFormat.ᜀ();
			}
			set
			{
				int a_ = 16;
				int num = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						num = 5;
						continue;
					case 2:
					{
						ExcelChartType excelChartType = this.SerieType;
						num = 6;
						continue;
					}
					case 4:
						goto IL_A5;
					case 5:
					{
						ExcelChartType excelChartType;
						if (excelChartType == ExcelChartType.Bubble3D)
						{
							goto IL_CB;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					}
					case 6:
					{
						ExcelChartType excelChartType;
						if (excelChartType != ExcelChartType.Bubble)
						{
							num = 1;
							continue;
						}
						goto IL_CB;
					}
					}
					if (this.Is3DBubbles != value)
					{
						num = 2;
						continue;
					}
					return;
					IL_CB:
					this.SerieFormat.ᜀ(value);
					this.ClearOnPropertyChange();
					num = 0;
				}
				IL_A5:
				throw new NotSupportedException(RecordTableEnumerator.b("ཅ㭇祉ࡋ్╏け㙓㩕㵗⥙籛㵝şౡ੣॥ᱧ䩩๫୭偯űųٵࡷᕹ๻੽ꁿꢇﺉ늑ﺕ聯뺝풟\udba1풣쎥蚧", a_));
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000B8F14 File Offset: 0x000B7F14
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x000B8F5C File Offset: 0x000B7F5C
		public bool IsShadow
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
				return this.SerieFormat.ᜅ();
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
				this.SerieFormat.ᜁ(value);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000B8FA4 File Offset: 0x000B7FA4
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x000B8FEC File Offset: 0x000B7FEC
		public bool ShowActiveValue
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
				return this.AttachedLabel.ᜁ();
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
				this.AttachedLabel.ᜃ(value);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x000B9034 File Offset: 0x000B8034
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x000B907C File Offset: 0x000B807C
		public bool ShowPieInPercents
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
				return this.AttachedLabel.ᜆ();
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
				this.AttachedLabel.ᜀ(value);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x000B90C4 File Offset: 0x000B80C4
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x000B910C File Offset: 0x000B810C
		public bool ShowPieCategoryLabel
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
				return this.AttachedLabel.ᜃ();
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
				this.AttachedLabel.ᜅ(value);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x000B9154 File Offset: 0x000B8154
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x000B919C File Offset: 0x000B819C
		public bool SmoothLine
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
				return this.AttachedLabel.ᜀ();
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
				this.AttachedLabel.ᜁ(value);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000B91E4 File Offset: 0x000B81E4
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x000B922C File Offset: 0x000B822C
		public bool ShowCategoryLabel
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
				return this.AttachedLabel.ᜂ();
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
				this.AttachedLabel.ᜄ(value);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000B9274 File Offset: 0x000B8274
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x000B92BC File Offset: 0x000B82BC
		public bool ShowBubble
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
				return this.AttachedLabel.ᜅ();
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
				this.AttachedLabel.ᜂ(value);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000B9304 File Offset: 0x000B8304
		public IShapeFill Fill
		{
			get
			{
				int a_ = 1;
				int num = 4;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						goto IL_17F;
					case 1:
						num = 7;
						continue;
					case 2:
					{
						ExcelChartType excelChartType;
						if (excelChartType == ExcelChartType.Line3D)
						{
							num = 3;
							continue;
						}
						goto IL_B0;
					}
					case 3:
						goto IL_1FE;
					case 5:
						goto IL_192;
					case 6:
						if (!this.\u1719.IsParsed)
						{
							goto IL_22B;
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
							num = 14;
							continue;
						}
						break;
					case 7:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("搶娸娺䤼䬾⑀ㅂ", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_10B;
					}
					case 8:
					{
						ExcelChartType excelChartType;
						if (excelChartType != ExcelChartType.Radar)
						{
							num = 15;
							continue;
						}
						goto IL_B0;
					}
					case 9:
						flag = true;
						goto IL_1C9;
					case 10:
						num = 16;
						continue;
					case 11:
						num = 2;
						continue;
					case 12:
					{
						ExcelChartType excelChartType;
						flag = (excelChartType == ExcelChartType.RadarMarkers);
						goto IL_1C9;
					}
					case 13:
						num = 6;
						continue;
					case 14:
					{
						if (true)
						{
						}
						ExcelChartType excelChartType = this.SerieType;
						string a = XlsChartFormat.ᜉ(excelChartType);
						num = 18;
						continue;
					}
					case 15:
						num = 12;
						continue;
					case 16:
						if (flag2)
						{
							num = 0;
							continue;
						}
						this.UpdateSerieFormat();
						num = 5;
						continue;
					case 17:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("搶䰸䤺嬼帾≀♂", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_10B;
					}
					case 18:
					{
						string a;
						if (a == RecordTableEnumerator.b("笶倸唺堼", a_))
						{
							num = 11;
							continue;
						}
						goto IL_1FE;
					}
					}
					if (!this.\u1719.TypeChanging)
					{
						num = 13;
						continue;
					}
					goto IL_22B;
					IL_B0:
					num = 9;
					continue;
					IL_1C9:
					flag2 = flag;
					num = 17;
					continue;
					IL_1FE:
					num = 8;
				}
				IL_10B:
				throw new NotSupportedException(RecordTableEnumerator.b("挶儸刺丼Ἶㅀㅂ⩄㝆ⱈ㥊㥌㙎煐㩒♔㥖繘⽚絜ⱞᑠ።ᕤࡦ᭨Ὢ࡬୮兰ᩲ᭴坶൸፺ᑼ౾ꆀﮈﾊ권ﮎ", a_));
				IL_17F:
				goto IL_10B;
				IL_192:
				IL_22B:
				return this.\u171D;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000B9544 File Offset: 0x000B8544
		public bool IsSupportFill
		{
			get
			{
				int a_ = 14;
				bool flag2;
				for (;;)
				{
					IL_5D:
					ExcelChartType excelChartType = this.SerieType;
					string a = XlsChartFormat.ᜉ(excelChartType);
					int num = 7;
					for (;;)
					{
						bool flag;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C1;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								if (excelChartType != ExcelChartType.Radar)
								{
									num = 8;
									continue;
								}
								goto IL_10D;
							case 1:
								goto IL_C1;
							case 2:
								num = 6;
								continue;
							case 3:
								if (!(a == RecordTableEnumerator.b("ᝃ㍅㩇ⱉⵋⵍ㕏", a_)))
								{
									num = 9;
									continue;
								}
								return false;
							case 4:
								goto IL_10B;
							case 5:
								flag = true;
								goto IL_13B;
							case 6:
								if (excelChartType == ExcelChartType.Line3D)
								{
									num = 10;
									continue;
								}
								goto IL_10D;
							case 7:
								if (a == RecordTableEnumerator.b("ࡃ⽅♇⽉", a_))
								{
									num = 2;
									continue;
								}
								goto IL_99;
							case 8:
								num = 1;
								continue;
							case 9:
								if (true)
								{
								}
								num = 11;
								continue;
							case 10:
								goto IL_99;
							case 11:
								if (!(a == RecordTableEnumerator.b("ᝃ╅⥇㹉㡋⭍≏", a_)))
								{
									num = 4;
									continue;
								}
								return false;
							}
							goto IL_5D;
							IL_99:
							num = 0;
							continue;
							IL_10D:
							num = 5;
							continue;
						}
						IL_13B:
						flag2 = flag;
						num = 3;
						continue;
						IL_C1:
						flag = (excelChartType == ExcelChartType.RadarMarkers);
						goto IL_13B;
					}
				}
				IL_10B:
				return !flag2;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000B96C0 File Offset: 0x000B86C0
		public IChartFormat Options
		{
			get
			{
				int a_ = 8;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					if (this.\u1717 != null)
					{
						return this.\u1717.GetCommonSerieFormat();
					}
					break;
				}
				throw new NotSupportedException(RecordTableEnumerator.b("紽ℿⱁ⩃⥅㱇橉⭋⭍⑏牑❓㍕⩗㍙㥛ⵝ䁟ൡᑣብŧթɫᵭ幯", a_));
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x000B9730 File Offset: 0x000B8730
		public bool IsMarkerSupported
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
				return this.ᜃ();
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000B9774 File Offset: 0x000B8774
		public IChartInterior Interior
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
				return this.AreaProperties;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000B97B8 File Offset: 0x000B87B8
		public bool IsInteriorSupported
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
				return XlsChartSerieDataFormat.ᜁ(this.SerieType);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000B9800 File Offset: 0x000B8800
		public bool IsBorderSupported
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
				return XlsChartSerieDataFormat.ᜀ(this.SerieType);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000B9848 File Offset: 0x000B8848
		public XlsChartSerie ParentSerie
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
				return this.\u1717;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x000B988C File Offset: 0x000B888C
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x000B98D0 File Offset: 0x000B88D0
		internal sprᲡ DataFormat
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

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x000B9914 File Offset: 0x000B8914
		internal spr\u2299 PieFormat
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜐ = (spr\u2299)spr\u175E.ᜀ(TBIFFRecord.ChartPieFormat);
						num = 2;
						continue;
					case 2:
						goto IL_71;
					}
					IL_1C:
					if (this.ᜐ != null)
					{
						goto IL_7B;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_1C;
				}
				IL_71:
				if (true)
				{
				}
				IL_7B:
				this.\u171C = true;
				return this.ᜐ;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x000B99AC File Offset: 0x000B89AC
		internal sprᣐ MarkerFormat
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_08;
					case 1:
						goto IL_8C;
					case 2:
						this.\u1713 = (sprᣐ)spr\u175E.ᜀ(TBIFFRecord.ChartMarkerFormat);
						this.\u171C = true;
						this.\u1713.ᜁ(true);
						num = 1;
						continue;
					}
					IL_24:
					if (this.\u1713 != null)
					{
						break;
					}
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
						continue;
					}
					IL_08:
					if (true)
					{
					}
					goto IL_24;
				}
				IL_8C:
				return this.\u1713;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000B9A50 File Offset: 0x000B8A50
		internal spr\u25C6 Serie3DDataFormat
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜏ = (spr\u25C6)spr\u175E.ᜀ(TBIFFRecord.Chart3DDataFormat);
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6B;
						}
						break;
					}
					if (this.ᜏ != null)
					{
						goto IL_73;
					}
					num = 1;
				}
				IL_6B:
				if (false)
				{
				}
				IL_73:
				if (true)
				{
				}
				return this.ᜏ;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x000B9AE0 File Offset: 0x000B8AE0
		internal spr\u239E SerieFormat
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
							goto IL_73;
						}
						break;
					case 2:
						if (true)
						{
						}
						this.\u1715 = (spr\u239E)spr\u175E.ᜀ(TBIFFRecord.ChartSerFmt);
						num = 1;
						continue;
					}
					if (this.\u1715 != null)
					{
						goto IL_7B;
					}
					num = 2;
				}
				IL_73:
				if (false)
				{
				}
				IL_7B:
				this.\u171C = true;
				return this.\u1715;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000B9B78 File Offset: 0x000B8B78
		internal sprή AttachedLabel
		{
			get
			{
				int num = 1;
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
							goto IL_79;
						}
						break;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						this.UpdateSerieFormat();
						this.\u1714 = (sprή)spr\u175E.ᜀ(TBIFFRecord.ChartAttachedLabel);
						num = 0;
						continue;
					}
					if (this.\u1714 != null)
					{
						goto IL_81;
					}
					num = 2;
				}
				IL_79:
				if (false)
				{
				}
				IL_81:
				return this.\u1714;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x000B9C0C File Offset: 0x000B8C0C
		public bool HasBorder
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
				return this.\u171A != null;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000B9C54 File Offset: 0x000B8C54
		internal sprᣐ MarkerFormatOrNull
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
				return this.\u1713;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x000B9C98 File Offset: 0x000B8C98
		internal spr\u25C6 Serie3DdDataFormatOrNull
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
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000B9CDC File Offset: 0x000B8CDC
		internal spr\u239E SerieFormatOrNull
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
				return this.\u1715;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x000B9D20 File Offset: 0x000B8D20
		internal spr\u2299 PieFormatOrNull
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
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x000B9D64 File Offset: 0x000B8D64
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x000B9DAC File Offset: 0x000B8DAC
		public int SeriesNumber
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
				return (int)this.ᜎ.ᜄ();
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
				this.ᜎ.ᜀ((ushort)value);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x000B9DF4 File Offset: 0x000B8DF4
		public bool IsMarker
		{
			get
			{
				if (this.MarkerFormatOrNull == null)
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
						return true;
					}
				}
				if (true)
				{
				}
				return this.MarkerFormatOrNull.ᜂ() != ChartMarkerType.None;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x000B9E4C File Offset: 0x000B8E4C
		public bool HasBorderLine
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_92;
					case 1:
						goto IL_56;
					case 3:
						if (!this.\u171A.UseDefaultFormat)
						{
							num = 0;
							continue;
						}
						return true;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_56:
						if (true)
						{
						}
						num = 3;
						break;
					default:
						if (false)
						{
						}
						if (this.\u171A == null)
						{
							return true;
						}
						num = 1;
						break;
					}
				}
				IL_92:
				return this.\u171A.Pattern != ChartLinePatternType.None;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x000B9EF0 File Offset: 0x000B8EF0
		public bool IsSmoothed
		{
			get
			{
				if (this.SerieFormatOrNull == null)
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
						return false;
					}
				}
				if (true)
				{
				}
				return this.IsSmoothedLine;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000B9F40 File Offset: 0x000B8F40
		private ExcelChartType SerieType
		{
			get
			{
				if (true)
				{
				}
				if (this.\u1717 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						XlsChartSeries xlsChartSeries = this.\u1719.Series;
						return xlsChartSeries.GetTypeByOrder((int)this.ᜎ.ᜀ());
					}
					}
				}
				return this.\u1717.SerieType;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000B9FB0 File Offset: 0x000B8FB0
		public bool IsFormatted
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
				return this.\u171C;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x000B9FF4 File Offset: 0x000B8FF4
		internal XlsChart ParentXlsChart
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
				return this.\u1719;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000BA038 File Offset: 0x000B9038
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x000BA0A4 File Offset: 0x000B90A4
		public Color ForeGroundColor
		{
			get
			{
				if (this.AreaProperties != null)
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
						if (false)
						{
						}
						return (this.AreaProperties as XlsChartInterior).ForegroundColorObject.ᜁ(this.ParentXlsChart.Workbook);
					}
				}
				return Color.Empty;
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
				(this.AreaProperties as XlsChartInterior).ForegroundColorObject.ᜀ(value);
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000BA0F8 File Offset: 0x000B90F8
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x000BA160 File Offset: 0x000B9160
		public ExcelColors ForeGroundKnownColor
		{
			get
			{
				if (this.AreaProperties != null)
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
						if (true)
						{
						}
						return (this.AreaProperties as XlsChartInterior).ForegroundColorObject.ᜂ(this.ParentXlsChart.Workbook);
					}
				}
				return ExcelColors.Black;
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
				(this.AreaProperties as XlsChartInterior).ForegroundColorObject.SetKnownColor(value);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x000BA1B4 File Offset: 0x000B91B4
		public OColor MarkerBackColorObject
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
				return this.\u171E;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x000BA1F8 File Offset: 0x000B91F8
		public OColor MarkerForeColorObject
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
				return this.\u171F;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000BA23C File Offset: 0x000B923C
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x000BA280 File Offset: 0x000B9280
		internal GradientStops MarkerGradient
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x000BA2C4 File Offset: 0x000B92C4
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x000BA308 File Offset: 0x000B9308
		public double MarkerTransparencyValue
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x000BA34C File Offset: 0x000B934C
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x000BA390 File Offset: 0x000B9390
		internal Stream MarkerLineStream
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
				return this.ᜢ;
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
				this.ᜢ = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x000BA3D4 File Offset: 0x000B93D4
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x000BA418 File Offset: 0x000B9418
		internal Stream EffectListStream
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
				return this.ᜣ;
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
				this.ᜣ = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x000BA45C File Offset: 0x000B945C
		public OColor ForeGroundColorObject
		{
			get
			{
				if (this.AreaProperties != null)
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
						if (true)
						{
						}
						return (this.AreaProperties as XlsChartInterior).ForegroundColorObject;
					}
				}
				return null;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x000BA4B4 File Offset: 0x000B94B4
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x000BA510 File Offset: 0x000B9510
		public ExcelColors BackGroundKnownColor
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
				return (this.AreaProperties as XlsChartInterior).BackgroundColorObject.ᜂ(this.ParentXlsChart.Workbook);
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
				(this.AreaProperties as XlsChartInterior).BackgroundColorObject.SetKnownColor(value);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000BA564 File Offset: 0x000B9564
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x000BA5C0 File Offset: 0x000B95C0
		public Color BackGroundColor
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
				return (this.AreaProperties as XlsChartInterior).BackgroundColorObject.ᜁ(this.ParentXlsChart.Workbook);
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
				(this.AreaProperties as XlsChartInterior).BackgroundColorObject.ᜀ(value);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x000BA614 File Offset: 0x000B9614
		public OColor BackGroundColorObject
		{
			get
			{
				if (true)
				{
				}
				if (this.AreaProperties != null)
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
						return (this.AreaProperties as XlsChartInterior).BackgroundColorObject;
					}
				}
				return null;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x000BA66C File Offset: 0x000B966C
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x000BA6B4 File Offset: 0x000B96B4
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
				return this.AreaProperties.Pattern;
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
				this.AreaProperties.Pattern = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000BA6FC File Offset: 0x000B96FC
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x000BA744 File Offset: 0x000B9744
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
				return this.AreaProperties.UseDefaultFormat;
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
				this.AreaProperties.UseDefaultFormat = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x000BA78C File Offset: 0x000B978C
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x000BA7D8 File Offset: 0x000B97D8
		public bool Visible
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
				return this.AreaProperties.Pattern != ExcelPatternType.None;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_61;
					case 1:
						goto IL_A2;
					case 2:
						goto IL_2F;
					case 3:
						if (this.AreaProperties.Pattern == ExcelPatternType.None)
						{
							num = 1;
							continue;
						}
						return;
					}
					if (value)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.AreaProperties.Pattern = ExcelPatternType.None;
						num = 0;
						continue;
					}
					IL_2F:
					if (true)
					{
					}
					num = 3;
				}
				IL_61:
				return;
				IL_A2:
				this.AreaProperties.Pattern = ExcelPatternType.Solid;
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000BA88C File Offset: 0x000B988C
		// Note: this type is marked as 'beforefieldinit'.
		static XlsChartSerieDataFormat()
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
			XlsChartSerieDataFormat.\u170D = new ExcelChartType[]
			{
				ExcelChartType.Bar3DClustered,
				ExcelChartType.Bar3D100PercentStacked,
				ExcelChartType.Bar3DStacked,
				ExcelChartType.Column3D,
				ExcelChartType.Column3DClustered,
				ExcelChartType.Column3D100PercentStacked,
				ExcelChartType.Column3DStacked,
				ExcelChartType.ConeBarClustered,
				ExcelChartType.ConeBarStacked,
				ExcelChartType.ConeBar100PercentStacked,
				ExcelChartType.ConeClustered,
				ExcelChartType.Cone3DClustered,
				ExcelChartType.ConeStacked,
				ExcelChartType.Cone100PercentStacked,
				ExcelChartType.CylinderBarClustered,
				ExcelChartType.CylinderBarStacked,
				ExcelChartType.CylinderBar100PercentStacked,
				ExcelChartType.CylinderClustered,
				ExcelChartType.Cylinder3DClustered,
				ExcelChartType.CylinderStacked,
				ExcelChartType.Cylinder100PercentStacked,
				ExcelChartType.PyramidBarClustered,
				ExcelChartType.PyramidBarStacked,
				ExcelChartType.PyramidBar100PercentStacked,
				ExcelChartType.PyramidClustered,
				ExcelChartType.Pyramid3DClustered,
				ExcelChartType.PyramidStacked,
				ExcelChartType.PyramidBarStacked
			};
		}

		// Token: 0x04000E69 RID: 3689
		private const ushort ᜀ = 78;

		// Token: 0x04000E6A RID: 3690
		private const int ᜁ = 20;

		// Token: 0x04000E6B RID: 3691
		internal const int ᜂ = 24;

		// Token: 0x04000E6C RID: 3692
		private const string ᜃ = "Pie";

		// Token: 0x04000E6D RID: 3693
		private bool \u25D8\u0096\u0098\u0087;

		// Token: 0x04000E6E RID: 3694
		private const string ᜄ = "Doughnut";

		// Token: 0x04000E6F RID: 3695
		private bool[] \u2609\u009B\u00A1\u00A3;

		// Token: 0x04000E70 RID: 3696
		private const string ᜅ = "Surface";

		// Token: 0x04000E71 RID: 3697
		internal const string ᜆ = "Line";

		// Token: 0x04000E72 RID: 3698
		internal const string ᜇ = "Scatter";

		// Token: 0x04000E73 RID: 3699
		private const int ᜈ = 60;

		// Token: 0x04000E74 RID: 3700
		private const int ᜉ = 5;

		// Token: 0x04000E75 RID: 3701
		private const int ᜊ = 8388608;

		// Token: 0x04000E76 RID: 3702
		private int[] \u2609\u008F\u0091\u00A4;

		// Token: 0x04000E77 RID: 3703
		private const int ᜋ = 32;

		// Token: 0x04000E78 RID: 3704
		private const ExcelColors ᜌ = (ExcelColors)77;

		// Token: 0x04000E79 RID: 3705
		internal static readonly ExcelChartType[] \u170D;

		// Token: 0x04000E7A RID: 3706
		private sprᲡ ᜎ = (sprᲡ)spr\u175E.ᜀ(TBIFFRecord.ChartDataFormat);

		// Token: 0x04000E7B RID: 3707
		private spr\u25C6 ᜏ;

		// Token: 0x04000E7C RID: 3708
		private spr\u2299 ᜐ;

		// Token: 0x04000E7D RID: 3709
		private Format3D ᜑ;

		// Token: 0x04000E7E RID: 3710
		private ChartShadow \u1712;

		// Token: 0x04000E7F RID: 3711
		private float \u2593\u00A9\u00AE\u0099;

		// Token: 0x04000E80 RID: 3712
		private sprᣐ \u1713;

		// Token: 0x04000E81 RID: 3713
		private sprή \u1714;

		// Token: 0x04000E82 RID: 3714
		private spr\u239E \u1715;

		// Token: 0x04000E83 RID: 3715
		private XlsChartDataPoint \u1716;

		// Token: 0x04000E84 RID: 3716
		private XlsChartSerie \u1717;

		// Token: 0x04000E85 RID: 3717
		private XlsChartFormat \u1718;

		// Token: 0x04000E86 RID: 3718
		private XlsChart \u1719;

		// Token: 0x04000E87 RID: 3719
		private XlsChartBorder \u171A;

		// Token: 0x04000E88 RID: 3720
		private XlsChartInterior \u171B;

		// Token: 0x04000E89 RID: 3721
		private bool \u171C;

		// Token: 0x04000E8A RID: 3722
		private spr\u2436 \u171D;

		// Token: 0x04000E8B RID: 3723
		private OColor \u171E;

		// Token: 0x04000E8C RID: 3724
		private OColor \u171F;

		// Token: 0x04000E8D RID: 3725
		private GradientStops ᜠ;

		// Token: 0x04000E8E RID: 3726
		private int[] \u2593\u00AE\u0088\u00A5;

		// Token: 0x04000E8F RID: 3727
		private double ᜡ = 1.0;

		// Token: 0x04000E90 RID: 3728
		private Stream ᜢ;

		// Token: 0x04000E91 RID: 3729
		private Stream ᜣ;
	}
}
