using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001D6 RID: 470
	public class ChartDefaultFormatsCollection
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x000ECE34 File Offset: 0x000EBE34
		public ChartDefaultFormatsCollection()
		{
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000ECE48 File Offset: 0x000EBE48
		internal ChartDefaultFormatsCollection(spr\u1DF5 A_0, sprᾹ A_1, sprᾹ A_2)
		{
			this.ᜀ = new ChartFormatCollection((spr\u2158)A_0, A_1);
			this.ᜁ = new ChartFormatCollection((spr\u2158)A_0, A_2);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000ECE80 File Offset: 0x000EBE80
		internal void ᜀ(IList A_0, ref int A_1)
		{
			int a_ = 16;
			while (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				return;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋ", a_));
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000ECEE0 File Offset: 0x000EBEE0
		[CLSCompliant(false)]
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 2;
			while (records != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				return;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x000ECF40 File Offset: 0x000EBF40
		public XlsChartFormatCollection PrimaryFormats
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
				return this.ᜀ;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x000ECF84 File Offset: 0x000EBF84
		public XlsChartFormatCollection SecondaryFormats
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
				return this.ᜁ;
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000ECFC8 File Offset: 0x000EBFC8
		public void Remove(XlsChartFormat format)
		{
			int a_ = 11;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6A;
				case 1:
					if (this.ᜀ.Count == 1)
					{
						num = 6;
						continue;
					}
					goto IL_6C;
				case 2:
					num = 1;
					continue;
				case 3:
				{
					int drawingZOrder;
					if (this.ᜁ.ContainsIndex(drawingZOrder))
					{
						num = 7;
						continue;
					}
					return;
				}
				case 4:
				{
					int drawingZOrder;
					if (this.ᜀ.ContainsIndex(drawingZOrder))
					{
						num = 2;
						continue;
					}
					num = 3;
					continue;
				}
				case 6:
					goto IL_145;
				case 7:
					this.ᜁ.Remove(format);
					num = 8;
					continue;
				case 8:
					goto IL_D0;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				default:
					if (false)
					{
					}
					if (format == null)
					{
						num = 0;
					}
					else
					{
						int drawingZOrder = format.DrawingZOrder;
						num = 4;
					}
					break;
				}
			}
			IL_6A:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
			IL_6C:
			this.ᜀ.Remove(format);
			return;
			IL_D0:
			return;
			IL_145:
			this.ChangeCollections();
			this.ᜁ.Remove(format);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000ED120 File Offset: 0x000EC120
		internal void ᜀ(spr\u1DF5 A_0, object A_1, bool A_2)
		{
			while (!A_2)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				this.ᜁ = new ChartFormatCollection((spr\u2158)A_0, A_1);
				return;
			}
			if (true)
			{
			}
			this.ᜀ = new ChartFormatCollection((spr\u2158)A_0, A_1);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000ED188 File Offset: 0x000EC188
		public void ChangeCollections()
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
			XlsChartFormatCollection xlsChartFormatCollection = this.ᜀ;
			object parent = this.ᜀ.Parent;
			object parent2 = this.ᜁ.Parent;
			this.ᜀ = this.ᜁ;
			this.ᜀ.SetParent(parent);
			this.ᜀ.SetParents();
			this.ᜁ = xlsChartFormatCollection;
			this.ᜁ.SetParent(parent2);
			this.ᜁ.SetParents();
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000ED224 File Offset: 0x000EC224
		public XlsChartFormat AddFormat(XlsChartFormat formatToAdd, int order, int index, bool isPrimary)
		{
			XlsChartFormatCollection xlsChartFormatCollection;
			for (;;)
			{
				xlsChartFormatCollection = this.ᜀ(isPrimary);
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if (this.ᜁ.ContainsIndex(num2))
						{
							num = 7;
							continue;
						}
						goto IL_B2;
					case 1:
						goto IL_153;
					case 2:
						num = 9;
						continue;
					case 3:
						goto IL_B2;
					case 4:
						goto IL_B2;
					case 5:
						goto IL_139;
					case 6:
						this.ᜀ.UpdateFormatsOnAdding(num2);
						num = 4;
						continue;
					case 7:
						this.ᜁ.UpdateFormatsOnAdding(num2);
						num = 3;
						continue;
					case 8:
						goto IL_106;
					case 9:
						if (!this.ᜁ.ContainsIndex(order))
						{
							num = 8;
							continue;
						}
						goto IL_17E;
					case 10:
						goto IL_139;
					case 11:
						IL_144:
						if (num2 < order)
						{
							num = 1;
							continue;
						}
						num = 13;
						continue;
					case 12:
						if (!this.ᜀ.ContainsIndex(order))
						{
							num = 2;
							continue;
						}
						goto IL_17E;
					case 13:
						if (this.ᜀ.ContainsIndex(num2))
						{
							num = 6;
							continue;
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
					IL_B2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_144;
					default:
						if (false)
						{
						}
						num2--;
						num = 10;
						continue;
					}
					IL_139:
					num = 11;
					continue;
					IL_17E:
					num2 = 7;
					num = 5;
				}
			}
			IL_106:
			xlsChartFormatCollection.SetIndex(order, index);
			return formatToAdd;
			IL_153:
			xlsChartFormatCollection.SetIndex(order, index);
			return formatToAdd;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000ED3C8 File Offset: 0x000EC3C8
		public void RemoveFormat(int indexToRemove, int iOrder, bool isPrimary)
		{
			for (;;)
			{
				this.ᜀ(isPrimary);
				int num = 7;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						this.ᜀ.UpdateFormatsOnRemoving(num2);
						num = 3;
						continue;
					case 1:
						if (num2 >= 8)
						{
							num = 8;
							continue;
						}
						num = 10;
						continue;
					case 2:
						goto IL_167;
					case 3:
						goto IL_CF;
					case 4:
						goto IL_122;
					case 5:
						goto IL_167;
					case 6:
						goto IL_122;
					case 7:
						if (isPrimary)
						{
							num = 13;
							continue;
						}
						this.ᜁ.UpdateIndexesAfterRemove(indexToRemove);
						num = 5;
						continue;
					case 8:
						return;
					case 9:
						this.ᜁ.UpdateFormatsOnRemoving(num2);
						num = 11;
						continue;
					case 10:
						if (this.ᜀ.ContainsIndex(num2))
						{
							num = 0;
							continue;
						}
						num = 12;
						continue;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EC;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_CF;
						}
						break;
					case 12:
						if (this.ᜁ.ContainsIndex(num2))
						{
							num = 9;
							continue;
						}
						goto IL_CF;
					case 13:
						this.ᜀ.UpdateIndexesAfterRemove(indexToRemove);
						goto IL_EC;
					}
					break;
					IL_CF:
					num2++;
					num = 6;
					continue;
					IL_EC:
					num = 2;
					continue;
					IL_122:
					num = 1;
					continue;
					IL_167:
					num2 = iOrder + 1;
					num = 4;
				}
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000ED558 File Offset: 0x000EC558
		private XlsChartFormatCollection ᜀ(bool A_0)
		{
			if (!A_0)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_1B;
					}
				}
				IL_1B:
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
			return this.ᜀ;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000ED5A8 File Offset: 0x000EC5A8
		public ChartDefaultFormatsCollection CloneForPrimary(object parent)
		{
			ChartDefaultFormatsCollection chartDefaultFormatsCollection;
			for (;;)
			{
				chartDefaultFormatsCollection = new ChartDefaultFormatsCollection();
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_7B;
						}
						break;
					case 1:
						chartDefaultFormatsCollection.ᜀ = (XlsChartFormatCollection)this.ᜀ.Clone(parent);
						num = 0;
						continue;
					case 2:
						if (this.ᜀ != null)
						{
							num = 1;
							continue;
						}
						return chartDefaultFormatsCollection;
					}
					break;
				}
			}
			IL_7B:
			if (false)
			{
			}
			return chartDefaultFormatsCollection;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000ED63C File Offset: 0x000EC63C
		public void CloneForSecondary(ChartDefaultFormatsCollection result, object parent)
		{
			int a_ = 13;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					result.ᜁ = (XlsChartFormatCollection)this.ᜁ.Clone(parent);
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_99;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					if (this.ᜁ != null)
					{
						goto IL_99;
					}
					return;
				case 4:
					goto IL_5C;
				}
				if (true)
				{
				}
				if (result == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				IL_99:
				num = 1;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄㑆㱈❊㥌", a_));
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000ED700 File Offset: 0x000EC700
		public ExcelChartType DetectChartType(XlsChartSeries series)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 28;
				for (;;)
				{
					ExcelChartType result;
					ExcelChartType excelChartType;
					int count;
					int count2;
					switch (num)
					{
					case 0:
					{
						XlsChartFormat xlsChartFormat = this.SecondaryFormats[1];
						num = 22;
						continue;
					}
					case 1:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat.LineStyle == DropLineStyleType.HiLow)
						{
							num = 29;
							continue;
						}
						return result;
					}
					case 2:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat.IsChartChartLine)
						{
							num = 13;
							continue;
						}
						return result;
					}
					case 3:
						num = 2;
						continue;
					case 4:
						if (true)
						{
						}
						excelChartType = ExcelChartType.StockVolumeHighLowClose;
						goto IL_246;
					case 5:
						if (count > 1)
						{
							num = 7;
							continue;
						}
						num = 26;
						continue;
					case 6:
						return result;
					case 7:
						result = ExcelChartType.CombinationChart;
						num = 6;
						continue;
					case 8:
						if (this.SecondaryFormats.ContainsIndex(1))
						{
							num = 0;
							continue;
						}
						return result;
					case 9:
						return result;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16B;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 11:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat.FormatRecordType == TBIFFRecord.ChartLine)
						{
							num = 27;
							continue;
						}
						return result;
					}
					case 12:
						result = ExcelChartType.ColumnClustered;
						num = 19;
						continue;
					case 13:
						num = 11;
						continue;
					case 14:
						if (count2 <= 5)
						{
							num = 17;
							continue;
						}
						return result;
					case 15:
						return result;
					case 16:
					{
						XlsChartFormat xlsChartFormat;
						if (!xlsChartFormat.IsDropBar)
						{
							num = 24;
							continue;
						}
						num = 21;
						continue;
					}
					case 17:
						num = 8;
						continue;
					case 18:
						if (count == 0)
						{
							num = 12;
							continue;
						}
						num = 5;
						continue;
					case 19:
						return result;
					case 20:
						if (count2 >= 4)
						{
							num = 10;
							continue;
						}
						return result;
					case 21:
						excelChartType = ExcelChartType.StockVolumeOpenHighLowClose;
						goto IL_246;
					case 22:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat != null)
						{
							num = 3;
							continue;
						}
						return result;
					}
					case 23:
						result = this.ᜀ(series);
						num = 9;
						continue;
					case 24:
						num = 4;
						continue;
					case 25:
						goto IL_B6;
					case 26:
						if (this.ᜁ.Count == 0)
						{
							num = 23;
							continue;
						}
						num = 20;
						continue;
					case 27:
						num = 1;
						continue;
					case 29:
						num = 16;
						continue;
					}
					if (series == null)
					{
						num = 25;
						continue;
					}
					count = this.ᜀ.Count;
					count2 = series.Count;
					result = ExcelChartType.CombinationChart;
					IL_16B:
					num = 18;
					continue;
					IL_246:
					result = excelChartType;
					num = 15;
				}
				IL_B6:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤹夻䰽⤿❁㝃", a_));
			}
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000EDA48 File Offset: 0x000ECA48
		private ExcelChartType ᜀ(XlsChartSeries A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 19;
				ExcelChartType excelChartType;
				for (;;)
				{
					int count;
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 1:
					{
						int num2;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						XlsChartSerie xlsChartSerie = A_0[num2] as XlsChartSerie;
						num = 12;
						continue;
					}
					case 2:
						if (count >= 3)
						{
							num = 0;
							continue;
						}
						goto IL_36C;
					case 3:
						num = 16;
						continue;
					case 4:
						goto IL_118;
					case 5:
					{
						if (true)
						{
						}
						XlsChartSerie xlsChartSerie2;
						excelChartType = xlsChartSerie2.SerieType;
						num = 7;
						continue;
					}
					case 6:
						goto IL_3C7;
					case 7:
						goto IL_234;
					case 8:
						goto IL_1F1;
					case 9:
						goto IL_16F;
					case 10:
					{
						int num2 = 0;
						num = 23;
						continue;
					}
					case 11:
						flag = false;
						goto IL_37B;
					case 12:
					{
						XlsChartSerie xlsChartSerie;
						XlsChartSerie xlsChartSerie2;
						if (xlsChartSerie.ChartGroup == xlsChartSerie2.ChartGroup)
						{
							num = 32;
							continue;
						}
						goto IL_2EC;
					}
					case 13:
						num = 28;
						continue;
					case 14:
						if (excelChartType == (ExcelChartType)(-1))
						{
							num = 5;
							continue;
						}
						return excelChartType;
					case 15:
						goto IL_C6;
					case 16:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat.FormatRecordType == TBIFFRecord.ChartLine)
						{
							num = 13;
							continue;
						}
						goto IL_36C;
					}
					case 17:
						num = 29;
						continue;
					case 18:
						num = 26;
						continue;
					case 20:
						num = 24;
						continue;
					case 21:
						goto IL_2EC;
					case 22:
					{
						XlsChartSerie xlsChartSerie;
						string b;
						if (xlsChartSerie.ᜎ() != b)
						{
							num = 21;
							continue;
						}
						int num2;
						num2++;
						num = 8;
						continue;
					}
					case 23:
						goto IL_1F1;
					case 24:
					{
						XlsChartFormat xlsChartFormat;
						if (!xlsChartFormat.IsDropBar)
						{
							num = 31;
							continue;
						}
						return ExcelChartType.StockOpenHighLowClose;
					}
					case 25:
					{
						if (this.ᜁ.Count != 0)
						{
							num = 6;
							continue;
						}
						count = A_0.Count;
						XlsChartFormat xlsChartFormat = this.ᜀ[0];
						num = 2;
						continue;
					}
					case 26:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat.IsChartChartLine)
						{
							num = 17;
							continue;
						}
						goto IL_36C;
					}
					case 27:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_118;
						default:
							if (false)
							{
							}
							goto IL_16F;
						}
						break;
					case 28:
					{
						XlsChartFormat xlsChartFormat;
						if (xlsChartFormat.IsChartLineFormat)
						{
							num = 18;
							continue;
						}
						goto IL_36C;
					}
					case 29:
					{
						XlsChartFormat xlsChartFormat;
						flag = (xlsChartFormat.LineStyle == DropLineStyleType.HiLow);
						goto IL_37B;
					}
					case 30:
					{
						if (flag2)
						{
							num = 20;
							continue;
						}
						XlsChartSerie xlsChartSerie2 = A_0[0] as XlsChartSerie;
						string value = xlsChartSerie2.ᜇ();
						string b = xlsChartSerie2.ᜎ();
						excelChartType = (ExcelChartType)(-1);
						num = 33;
						continue;
					}
					case 31:
						return ExcelChartType.StockHighLowClose;
					case 32:
						num = 22;
						continue;
					case 33:
					{
						string value;
						if (Array.IndexOf<string>(ChartDefaultFormatsCollection.DEF_MABY_COMBINATION_TYPES_START, value) != -1)
						{
							num = 10;
							continue;
						}
						goto IL_16F;
					}
					}
					if (A_0 == null)
					{
						num = 15;
						continue;
					}
					num = 25;
					continue;
					IL_118:
					if (count <= 4)
					{
						num = 3;
						continue;
					}
					goto IL_36C;
					IL_16F:
					num = 14;
					continue;
					IL_1F1:
					num = 1;
					continue;
					IL_2EC:
					excelChartType = ExcelChartType.CombinationChart;
					num = 27;
					continue;
					IL_36C:
					num = 11;
					continue;
					IL_37B:
					flag2 = flag;
					num = 30;
				}
				IL_C6:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⅃㑅ⅇ⽉㽋", a_));
				IL_234:
				return excelChartType;
				IL_3C7:
				throw new ApplicationException(RecordTableEnumerator.b("ᝁ⩃ⵅ♇╉㭋⁍灏ㅑ㱓㝕⩗⹙籛⩝ᥟቡţ", a_));
			}
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000EDE54 File Offset: 0x000ECE54
		public void Clear()
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
			this.ᜀ.Clear();
			this.ᜁ.Clear();
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000EDEA8 File Offset: 0x000ECEA8
		internal XlsChartFormat ᜀ(ExcelChartType A_0, ExcelChartType A_1, spr\u1DF5 A_2, XlsChart A_3, XlsChartSerie A_4)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					if (true)
					{
					}
					XlsChartFormat xlsChartFormat;
					bool flag;
					List<XlsChartSerie> list;
					bool bCanReplace;
					XlsChartFormat xlsChartFormat2;
					XlsChartSeries xlsChartSeries;
					switch (num)
					{
					case 0:
						return xlsChartFormat;
					case 1:
						flag = false;
						goto IL_191;
					case 2:
						if (list.Count == 1)
						{
							num = 9;
							continue;
						}
						num = 1;
						continue;
					case 3:
						A_3.IsSecondaryAxes = true;
						this.ᜁ.Add(xlsChartFormat, bCanReplace);
						num = 0;
						continue;
					case 4:
						flag = (list[0] == A_4);
						goto IL_191;
					case 5:
						xlsChartFormat2 = new ChartFormat((spr\u2158)A_2, this.ᜀ);
						goto IL_AD;
					case 6:
						num = 5;
						continue;
					case 7:
					{
						bool flag2;
						if (!flag2)
						{
							num = 6;
							continue;
						}
						num = 12;
						continue;
					}
					case 8:
						goto IL_21A;
					case 9:
						num = 4;
						continue;
					case 10:
					{
						if (A_3 == null)
						{
							num = 8;
							continue;
						}
						bool flag2 = Array.IndexOf<ExcelChartType>(XlsChart.ᜭ, A_0) != -1;
						xlsChartSeries = A_3.Series;
						num = 7;
						continue;
					}
					case 11:
						A_3.ChangePrimaryAxis(false);
						num = 16;
						continue;
					case 12:
						xlsChartFormat2 = new ChartFormat((spr\u2158)A_2, this.ᜁ);
						goto IL_AD;
					case 13:
						goto IL_8C;
					case 14:
						return xlsChartFormat;
					case 15:
					{
						bool flag2;
						if (flag2)
						{
							num = 3;
							continue;
						}
						num = 17;
						continue;
					}
					case 16:
						goto IL_269;
					case 17:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜭ, A_1) != -1)
						{
							num = 11;
							continue;
						}
						goto IL_269;
					}
					if (A_2 == null)
					{
						num = 13;
						continue;
					}
					num = 10;
					continue;
					IL_AD:
					xlsChartFormat = xlsChartFormat2;
					xlsChartFormat.ᜂ(A_0, false);
					xlsChartFormat.DrawingZOrder = xlsChartSeries.FindOrderByType(A_0);
					XlsChartSeries xlsChartSeries2 = A_3.Series;
					list = xlsChartSeries2.ᜂ(xlsChartFormat.DrawingZOrder);
					num = 2;
					continue;
					IL_191:
					bCanReplace = flag;
					num = 15;
					continue;
					IL_269:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_230;
					default:
						if (false)
						{
						}
						this.ᜀ.Add(xlsChartFormat, bCanReplace);
						num = 14;
						break;
					}
				}
				IL_8C:
				throw new ArgumentNullException(RecordTableEnumerator.b("❅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙", a_));
				IL_21A:
				IL_230:
				throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍", a_));
			}
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000EE158 File Offset: 0x000ED158
		public void ChangeShallowAxis(bool bToPrimary, int iOrder, bool bAdd, int iNewOrder)
		{
			if (bToPrimary)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_1B;
					}
				}
				IL_1B:
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ(this.ᜁ, this.ᜀ, iOrder, iNewOrder, bAdd);
				return;
			}
			this.ᜀ(this.ᜀ, this.ᜁ, iOrder, iNewOrder, bAdd);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000EE1C8 File Offset: 0x000ED1C8
		private void ᜀ(XlsChartFormatCollection A_0, XlsChartFormatCollection A_1, int A_2, int A_3, bool A_4)
		{
			XlsChartFormat format = A_0.GetFormat(A_2, !A_4);
			XlsChartFormat xlsChartFormat = (XlsChartFormat)format.Clone(A_1);
			if (A_4)
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
						goto IL_3E;
					}
				}
				IL_3E:
				if (false)
				{
				}
				xlsChartFormat.DrawingZOrder = A_3;
				A_1.Add(xlsChartFormat, false);
				return;
			}
			A_1.AddFormat(xlsChartFormat);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000EE23C File Offset: 0x000ED23C
		// Note: this type is marked as 'beforefieldinit'.
		static ChartDefaultFormatsCollection()
		{
			int a_ = 11;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			ChartDefaultFormatsCollection.DEF_MABY_COMBINATION_TYPES = new ExcelChartType[]
			{
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterLine,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.Line,
				ExcelChartType.Line3D,
				ExcelChartType.LineMarkers,
				ExcelChartType.LineMarkersStacked,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.LineStacked,
				ExcelChartType.Line100PercentStacked,
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D,
				ExcelChartType.RadarMarkers,
				ExcelChartType.Radar
			};
			ChartDefaultFormatsCollection.DEF_MABY_COMBINATION_TYPES_START = new string[]
			{
				RecordTableEnumerator.b("ቀ⁂⑄㍆㵈⹊㽌", a_),
				RecordTableEnumerator.b("ീ⩂⭄≆", a_),
				RecordTableEnumerator.b("̀㙂❄╆╈⹊", a_),
				RecordTableEnumerator.b("ፀ≂⅄♆㭈", a_)
			};
		}

		// Token: 0x04001005 RID: 4101
		private bool \u2460\u00A6\u0091\u0094;

		// Token: 0x04001006 RID: 4102
		private int[] \u2460\u008E\u00A8\u0094;

		// Token: 0x04001007 RID: 4103
		private byte \u2460\u0088\u00AD\u009F;

		// Token: 0x04001008 RID: 4104
		private bool \u25D9\u00A6\u0092\u009D;

		// Token: 0x04001009 RID: 4105
		private string \u25D9\u0080\u00AE\u0095;

		// Token: 0x0400100A RID: 4106
		public static readonly ExcelChartType[] DEF_MABY_COMBINATION_TYPES;

		// Token: 0x0400100B RID: 4107
		public static readonly string[] DEF_MABY_COMBINATION_TYPES_START;

		// Token: 0x0400100C RID: 4108
		private bool \u2609\u0083\u00A4\u00A5;

		// Token: 0x0400100D RID: 4109
		private XlsChartFormatCollection ᜀ;

		// Token: 0x0400100E RID: 4110
		private XlsChartFormatCollection ᜁ;
	}
}
