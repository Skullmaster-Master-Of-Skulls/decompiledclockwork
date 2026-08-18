using System;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001AB RID: 427
	public class XlsChartFormatCollection : CollectionExtended<XlsChartFormat>
	{
		// Token: 0x060016EF RID: 5871 RVA: 0x000DD9EC File Offset: 0x000DC9EC
		internal XlsChartFormatCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜃ = new int[8];
			for (int i = 0; i < 8; i++)
			{
				this.ᜃ[i] = -1;
			}
			this.SetParents();
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000DDA2C File Offset: 0x000DCA2C
		protected internal void SetParents()
		{
			int a_ = 18;
			this.ᜄ = (sprᾹ)base.FindParent(typeof(sprᾹ));
			if (this.ᜄ == null)
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
					throw new ApplicationException(RecordTableEnumerator.b("ᡇ⭉㹋⭍㹏♑瑓㥕㩗す㥛㵝ᑟ䉡ݣݥ٧ѩͫᩭ偯ၱᅳ噵ṷᕹॻၽ겁", a_));
				}
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x000DDAAC File Offset: 0x000DCAAC
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 7;
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (num2 >= base.Count)
					{
						goto IL_C3;
					}
					if (true)
					{
					}
					base.List[num2].SerializeDataToList(records);
					num2++;
					num = 4;
					continue;
				case 1:
					goto IL_46;
				case 3:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C3;
					default:
						if (false)
						{
						}
						goto IL_AF;
					}
					break;
				case 5:
					goto IL_AF;
				}
				if (records == null)
				{
					num = 1;
					continue;
				}
				num2 = 0;
				num = 5;
				continue;
				IL_AF:
				num = 0;
				continue;
				IL_C3:
				num = 3;
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆㩈", a_));
		}

		// Token: 0x1700085F RID: 2143
		public new XlsChartFormat this[int index]
		{
			get
			{
				int a_ = 11;
				if (this.ᜃ[index] == -1)
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
						throw new ArgumentException(RecordTableEnumerator.b("ࡀⵂ⅄≆ㅈ歊≌㩎═獒㩔ㅖ祘㥚㉜⩞འݢᙤ䥦", a_));
					}
				}
				return base.List[this.ᜃ[index]];
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x000DDC04 File Offset: 0x000DCC04
		public bool IsPrimary
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
				return this.ᜄ.ᜁ();
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000DDC4C File Offset: 0x000DCC4C
		public bool NeedSecondaryAxis
		{
			get
			{
				int num = 0;
				XlsChartFormat xlsChartFormat;
				for (;;)
				{
					TBIFFRecord tbiffrecord;
					switch (num)
					{
					case 1:
						if (!xlsChartFormat.Is3D)
						{
							num = 2;
							continue;
						}
						if (true)
						{
						}
						num = 12;
						continue;
					case 2:
						num = 4;
						continue;
					case 3:
						goto IL_D3;
					case 4:
						if (Array.IndexOf<TBIFFRecord>(XlsChartFormatCollection.ᜂ, tbiffrecord) == -1)
						{
							num = 7;
							continue;
						}
						num = 10;
						continue;
					case 5:
						if (base.Count < 1)
						{
							num = 3;
							continue;
						}
						goto IL_127;
					case 6:
						num = 5;
						continue;
					case 7:
						num = 8;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_127;
						default:
							if (false)
							{
							}
							if (tbiffrecord == TBIFFRecord.ChartBar)
							{
								num = 9;
								continue;
							}
							num = 11;
							continue;
						}
						break;
					case 9:
						num = 13;
						continue;
					case 10:
						goto IL_AE;
					case 11:
						goto IL_62;
					case 12:
						goto IL_FB;
					case 13:
						goto IL_E0;
					}
					if (this.IsPrimary)
					{
						num = 6;
						continue;
					}
					return false;
					IL_127:
					xlsChartFormat = base.List[0];
					tbiffrecord = xlsChartFormat.FormatRecordType;
					num = 1;
				}
				IL_62:
				return false;
				IL_AE:
				return true;
				IL_D3:
				return false;
				IL_E0:
				return xlsChartFormat.IsHorizontalBar;
				IL_FB:
				return false;
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000DDDC0 File Offset: 0x000DCDC0
		public new XlsChartFormat Add(XlsChartFormat format)
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
			return this.Add(format, false);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000DDE04 File Offset: 0x000DCE04
		public XlsChartFormat Add(XlsChartFormat format, bool bCanReplace)
		{
			int a_ = 7;
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					return format;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						if (false)
						{
						}
						if (num2 >= 0)
						{
							num = 0;
							continue;
						}
						goto IL_4F;
					}
					break;
				case 3:
					if (!bCanReplace)
					{
						num = 6;
						continue;
					}
					base[num2] = format;
					num = 7;
					continue;
				case 4:
					goto IL_4D;
				case 6:
					goto IL_4F;
				case 7:
					return format;
				}
				goto IL_39;
				IL_45:
				num = 4;
				continue;
				IL_39:
				if (format == null)
				{
					goto IL_45;
				}
				int drawingZOrder = format.DrawingZOrder;
				num2 = this.ᜃ[drawingZOrder];
				num = 2;
				continue;
				IL_4F:
				base.Add(format);
				num2 = base.Count - 1;
				format = this.ᜄ.ᜆ().AddFormat(format, drawingZOrder, num2, this.IsPrimary);
				if (true)
				{
				}
				num = 1;
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬼倾㍀⹂⑄㍆", a_));
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000DDF40 File Offset: 0x000DCF40
		public XlsChartFormat FindOrAdd(XlsChartFormat formatToAdd)
		{
			switch (0)
			{
			default:
			{
				XlsChartFormat xlsChartFormat;
				for (;;)
				{
					xlsChartFormat = null;
					int num = 0;
					int count = base.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return xlsChartFormat;
						case 1:
							goto IL_F3;
						case 2:
							IL_80:
							xlsChartFormat = this.Add(formatToAdd, false);
							if (true)
							{
							}
							num2 = 0;
							continue;
						case 3:
							goto IL_62;
						case 4:
						{
							if (num >= count)
							{
								num2 = 5;
								continue;
							}
							XlsChartFormat xlsChartFormat2 = base.InnerList[num];
							num2 = 7;
							continue;
						}
						case 5:
							goto IL_62;
						case 6:
						{
							XlsChartFormat xlsChartFormat2;
							xlsChartFormat = xlsChartFormat2;
							num2 = 3;
							continue;
						}
						case 7:
						{
							XlsChartFormat xlsChartFormat2;
							if (formatToAdd == xlsChartFormat2)
							{
								num2 = 6;
								continue;
							}
							num++;
							num2 = 8;
							continue;
						}
						case 8:
							goto IL_F3;
						case 9:
							if (xlsChartFormat == null)
							{
								num2 = 2;
								continue;
							}
							return xlsChartFormat;
						}
						break;
						IL_62:
						num2 = 9;
						continue;
						IL_F3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_80;
						default:
							if (false)
							{
							}
							num2 = 4;
							break;
						}
					}
				}
				return xlsChartFormat;
			}
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000DE084 File Offset: 0x000DD084
		public bool ContainsIndex(int index)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_77:
				num = 0;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_38:
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					num = 3;
					continue;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
					goto IL_73;
				}
				if (index >= 8)
				{
					return false;
				}
				num = 1;
			}
			IL_73:
			if (index >= 0)
			{
				goto IL_77;
			}
			return false;
			IL_7F:
			return this.ᜃ[index] != -1;
			IL_20:
			if (false)
			{
			}
			num = 2;
			goto IL_38;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000DE114 File Offset: 0x000DD114
		public new void Remove(XlsChartFormat toRemove)
		{
			int a_ = 12;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_71;
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 0;
				int drawingZOrder;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_C4;
					case 2:
					{
						XlsChartSeries xlsChartSeries;
						if (xlsChartSeries.ᜅ(drawingZOrder) != 0)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_DA;
					}
					case 3:
						goto IL_71;
					}
					if (toRemove == null)
					{
						num = 3;
					}
					else
					{
						drawingZOrder = toRemove.DrawingZOrder;
						XlsChart ᜅ = this.ᜄ.ᜅ;
						XlsChartSeries xlsChartSeries = ᜅ.Series;
						num = 2;
					}
				}
				IL_C4:
				throw new ArgumentException(RecordTableEnumerator.b("၁⅃⭅❇㱉⥋湍㙏㵑♓㭕㥗⹙籛㡝şୡࡣͥ౧䑩", a_));
				IL_DA:
				int num2 = this.ᜃ[drawingZOrder];
				base.RemoveAt(num2);
				this.ᜃ[drawingZOrder] = -1;
				this.ᜄ.ᜆ().RemoveFormat(num2, drawingZOrder, this.IsPrimary);
				return;
			}
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙁⭃ᑅⵇ❉⍋㡍㕏", a_));
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000DE22C File Offset: 0x000DD22C
		public void UpdateIndexesAfterRemove(int removeIndex)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						this.ᜃ[num] = this.ᜃ[num] - 1;
						num2 = 1;
						continue;
					case 1:
						goto IL_42;
					case 2:
						goto IL_8C;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (num >= 8)
							{
								num2 = 6;
								continue;
							}
							num2 = 5;
							continue;
						}
						break;
					case 4:
						goto IL_8C;
					case 5:
						if (this.ᜃ[num] > removeIndex)
						{
							num2 = 0;
							continue;
						}
						goto IL_42;
					case 6:
						return;
					}
					break;
					IL_42:
					num++;
					num2 = 2;
					continue;
					IL_8C:
					num2 = 3;
				}
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000DE300 File Offset: 0x000DD300
		public void UpdateSeriesByChartGroup(int newIndex, int OldIndex)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_4F:
					XlsChart ᜅ = this.ᜄ.ᜅ;
					XlsChartSeries xlsChartSeries = ᜅ.Series;
					int num = 0;
					int count = xlsChartSeries.Count;
					if (true)
					{
					}
					int num2 = 5;
					for (;;)
					{
						XlsChartSerie xlsChartSerie;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							switch (num2)
							{
							case 0:
								goto IL_E0;
							case 1:
								goto IL_7E;
							case 2:
								if (num >= count)
								{
									num2 = 4;
									continue;
								}
								xlsChartSerie = (XlsChartSerie)xlsChartSeries[num];
								num2 = 3;
								continue;
							case 3:
								if (xlsChartSerie.ChartGroup == OldIndex)
								{
									num2 = 0;
									continue;
								}
								goto IL_7E;
							case 4:
								return;
							case 5:
								goto IL_E2;
							case 6:
								goto IL_E2;
							}
							goto IL_4F;
							IL_7E:
							num++;
							num2 = 6;
							continue;
							IL_E2:
							num2 = 2;
							continue;
						}
						IL_E0:
						xlsChartSerie.ChartGroup = newIndex;
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000DE410 File Offset: 0x000DD410
		public new void Clear()
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				for (;;)
				{
					IL_1E:
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_58;
					case 1:
						goto IL_58;
					case 2:
						if (num2 >= 8)
						{
							num = 3;
							continue;
						}
						this.ᜃ[num2] = -1;
						num2++;
						num = 1;
						continue;
					case 3:
						return;
					}
					goto IL_46;
					IL_58:
					num = 2;
				}
				return;
			default:
				if (false)
				{
				}
				break;
			}
			IL_46:
			base.Clear();
			num2 = 0;
			num = 0;
			goto IL_1E;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x000DE4A4 File Offset: 0x000DD4A4
		public override object Clone(object parent)
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
			XlsChartFormatCollection xlsChartFormatCollection = (ChartFormatCollection)base.Clone(parent);
			xlsChartFormatCollection.ᜃ = spr\u1CD3.ᜀ(this.ᜃ);
			return xlsChartFormatCollection;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000DE500 File Offset: 0x000DD500
		public void SetIndex(int index, int Value)
		{
			int a_ = 15;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_72;
				case 1:
					num = 6;
					continue;
				case 2:
					if (true)
					{
					}
					num = 4;
					continue;
				case 3:
					if (index < 0)
					{
						goto IL_CA;
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
					if (Value >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_CA;
				case 6:
					if (Value >= base.List.Count)
					{
						num = 0;
						continue;
					}
					goto IL_DE;
				case 7:
					num = 3;
					continue;
				}
				IL_39:
				if (index < 8)
				{
					num = 7;
					continue;
				}
				break;
				goto IL_39;
			}
			IL_72:
			IL_CA:
			throw new ArgumentException(RecordTableEnumerator.b("ౄ⥆ⵈ⹊㕌潎㡐⁒畔㡖ⱘ⽚絜ぞݠ䍢ݤࡦᱨժ६ᱮ", a_));
			IL_DE:
			this.ᜃ[index] = Value;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000DE5F4 File Offset: 0x000DD5F4
		public void UpdateFormatsOnAdding(int index)
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
			XlsChartFormat xlsChartFormat = this[index];
			xlsChartFormat.DrawingZOrder = index + 1;
			this.UpdateSeriesByChartGroup(index + 1, index);
			this.ᜃ[index + 1] = this.ᜃ[index];
			this.ᜃ[index] = -1;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000DE668 File Offset: 0x000DD668
		public void UpdateFormatsOnRemoving(int index)
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
			XlsChartFormat xlsChartFormat = this[index];
			xlsChartFormat.DrawingZOrder = index - 1;
			this.UpdateSeriesByChartGroup(index - 1, index);
			this.ᜃ[index - 1] = this.ᜃ[index];
			this.ᜃ[index] = -1;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000DE6DC File Offset: 0x000DD6DC
		protected internal XlsChartFormat GetFormat(int iOrder, bool bDelete)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num;
				int num2;
				int num3;
				int num4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_152:
					this.ᜃ[iOrder] = -1;
					base.RemoveAt(num);
					num2 = 0;
					num3 = this.ᜃ.Length;
					num4 = 4;
					break;
				default:
					if (false)
					{
					}
					goto IL_68;
				}
				for (;;)
				{
					IL_35:
					switch (num4)
					{
					case 0:
						goto IL_10E;
					case 1:
						goto IL_10C;
					case 2:
					{
						int num5 = this.ᜃ[num2] = num5 - 1;
						if (true)
						{
						}
						num4 = 3;
						continue;
					}
					case 3:
						goto IL_140;
					case 4:
						goto IL_10E;
					case 5:
					{
						if (num2 >= num3)
						{
							num4 = 9;
							continue;
						}
						int num5 = this.ᜃ[num2];
						num4 = 10;
						continue;
					}
					case 6:
					{
						if (num == -1)
						{
							num4 = 7;
							continue;
						}
						XlsChartFormat result = base.List[num];
						num4 = 8;
						continue;
					}
					case 7:
						goto IL_87;
					case 8:
					{
						if (bDelete)
						{
							num4 = 1;
							continue;
						}
						XlsChartFormat result;
						return result;
					}
					case 9:
					{
						XlsChartFormat result;
						return result;
					}
					case 10:
					{
						int num5;
						if (num5 > num)
						{
							num4 = 2;
							continue;
						}
						goto IL_140;
					}
					}
					goto IL_68;
					IL_10E:
					num4 = 5;
					continue;
					IL_140:
					num2++;
					num4 = 0;
				}
				IL_87:
				throw new ArgumentException(RecordTableEnumerator.b("╂⩄㕆⑈⩊㥌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢ͤࡦᱨժ६䅮", a_));
				IL_10C:
				goto IL_152;
				IL_68:
				num = this.ᜃ[iOrder];
				num4 = 6;
				goto IL_35;
			}
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000DE868 File Offset: 0x000DD868
		protected internal void AddFormat(XlsChartFormat format)
		{
			int a_ = 17;
			while (!(format == null))
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
					if (true)
					{
					}
					int drawingZOrder = format.DrawingZOrder;
					base.Add(format);
					int num = base.Count - 1;
					this.ᜃ[drawingZOrder] = num;
					return;
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈㥊⁌⹎═", a_));
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000DE8EC File Offset: 0x000DD8EC
		// Note: this type is marked as 'beforefieldinit'.
		static XlsChartFormatCollection()
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
			XlsChartFormatCollection.ᜂ = new TBIFFRecord[]
			{
				TBIFFRecord.ChartPie,
				TBIFFRecord.ChartRadar,
				TBIFFRecord.ChartRadarArea,
				TBIFFRecord.ChartBoppop
			};
		}

		// Token: 0x04000F87 RID: 3975
		private int \u25D8\u00A6\u009B\u00A7;

		// Token: 0x04000F88 RID: 3976
		private new const int ᜀ = -1;

		// Token: 0x04000F89 RID: 3977
		private float \u2460\u009B\u00A9\u00A0;

		// Token: 0x04000F8A RID: 3978
		internal new const int ᜁ = 8;

		// Token: 0x04000F8B RID: 3979
		private new static readonly TBIFFRecord[] ᜂ;

		// Token: 0x04000F8C RID: 3980
		private bool[] \u25D8ª\u0080\u0081;

		// Token: 0x04000F8D RID: 3981
		private int[] ᜃ;

		// Token: 0x04000F8E RID: 3982
		private float \u25D9\u00B0\u0099\u0090;

		// Token: 0x04000F8F RID: 3983
		private sprᾹ ᜄ;
	}
}
