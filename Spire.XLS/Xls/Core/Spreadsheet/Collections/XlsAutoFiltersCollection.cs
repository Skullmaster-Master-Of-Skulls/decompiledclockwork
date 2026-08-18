using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000021 RID: 33
	public class XlsAutoFiltersCollection : CollectionExtended<object>, IAutoFilters
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00015070 File Offset: 0x00014070
		internal XlsAutoFiltersCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ();
			base.Cleared += this.ᜀ;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000150A0 File Offset: 0x000140A0
		private new void ᜁ()
		{
			int a_ = 14;
			this.ᜃ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.ᜃ != null)
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
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("ᑃ❅㩇⽉≋㩍灏㵑㙓㱕㵗㥙⡛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻偽깿", a_), RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_));
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0001512C File Offset: 0x0001412C
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00015170 File Offset: 0x00014170
		public IXLSRange Range
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
			set
			{
				int a_ = 19;
				switch (0)
				{
				default:
				{
					int num = 20;
					for (;;)
					{
						int num2;
						int count;
						int row;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							if (num2 >= count)
							{
								num = 6;
								continue;
							}
							((XlsAutoFilter)this[num2]).Clear();
							num2++;
							num = 2;
							continue;
						case 1:
							goto IL_374;
						case 2:
							goto IL_22D;
						case 3:
						{
							INameRanges names;
							names.Remove(this.DefaultNamedRangeName);
							num = 9;
							continue;
						}
						case 4:
						{
							INameRanges names;
							INamedRange namedRange = names[this.DefaultNamedRangeName];
							namedRange.RefersToRange = this.ᜂ;
							num = 1;
							continue;
						}
						case 5:
							goto IL_302;
						case 6:
						{
							base.Clear();
							this.ᜂ = value;
							INameRanges names = this.Worksheet.Names;
							num = 8;
							continue;
						}
						case 7:
							goto IL_29E;
						case 8:
							if (value == null)
							{
								num = 3;
								continue;
							}
							num = 10;
							continue;
						case 9:
							goto IL_374;
						case 10:
						{
							INameRanges names;
							if (names.Contains(this.DefaultNamedRangeName))
							{
								num = 4;
								continue;
							}
							INamedRange namedRange2 = names.Add(this.DefaultNamedRangeName, this.ᜂ);
							namedRange2.Visible = false;
							((XlsName)namedRange2).IsBuiltIn = true;
							num = 12;
							continue;
						}
						case 11:
						{
							row = this.ᜂ.Row;
							num3 = value.Column;
							int lastColumn = value.LastColumn;
							num = 18;
							continue;
						}
						case 12:
							goto IL_374;
						case 13:
							goto IL_27D;
						case 14:
							num = 22;
							continue;
						case 15:
						{
							int lastColumn;
							if (num3 > lastColumn)
							{
								num = 7;
								continue;
							}
							IXLSRange mergeArea = this.ᜃ[row, num3].MergeArea;
							num = 19;
							continue;
						}
						case 16:
							num = 24;
							continue;
						case 17:
							if (value != null)
							{
								num = 11;
								continue;
							}
							return;
						case 18:
							goto IL_27D;
						case 19:
						{
							IXLSRange mergeArea;
							if (mergeArea == null)
							{
								num = 14;
								continue;
							}
							num = 21;
							continue;
						}
						case 21:
						{
							IXLSRange mergeArea;
							num4 = mergeArea.LastColumn;
							goto IL_18F;
						}
						case 22:
							num4 = num3;
							goto IL_18F;
						case 23:
							if (true)
							{
							}
							goto IL_22D;
						case 24:
							if (value.Worksheet != this.Worksheet)
							{
								num = 5;
								continue;
							}
							goto IL_307;
						}
						if (value != null)
						{
							num = 16;
							continue;
						}
						goto IL_307;
						IL_18F:
						int num5 = num4;
						base.InnerList.Add(new AutoFilter(this, num3, num5, row));
						XlsAutoFilter xlsAutoFilter = (XlsAutoFilter)base.InnerList[base.InnerList.Count - 1];
						xlsAutoFilter.Index = base.InnerList.Count - 1;
						xlsAutoFilter.ᜅ = num3;
						num3 = num5;
						num3++;
						num = 13;
						continue;
						IL_22D:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_374:
							base.InnerList.Clear();
							num = 17;
							continue;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						IL_27D:
						num = 15;
						continue;
						IL_307:
						num2 = 0;
						count = base.Count;
						num = 23;
					}
					IL_29E:
					return;
					IL_302:
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("཈≊⅌㭎㑐⅒畔╖㡘㕚㩜㩞በ䍢ࡤቦᩨὪ䵬൮ᑰ卲ٴᙶᑸṺ嵼ࡾﮎ", a_));
				}
				}
			}
		}

		// Token: 0x170000FB RID: 251
		public IAutoFilter this[int columnIndex]
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
				return (IAutoFilter)base.InnerList[columnIndex];
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0001556C File Offset: 0x0001456C
		public string AddressR1C1
		{
			get
			{
				int a_ = 12;
				INameRanges names = this.Worksheet.Names;
				if (!names.Contains(RecordTableEnumerator.b("ᵁɃ⽅⑇㹉⥋㱍ᑏ㍑⁓㝕㩗㭙⽛㭝", a_)))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					throw new ApplicationException(RecordTableEnumerator.b("сⵃ⩅㱇⽉㹋⭍㑏牑♓㝕㙗㵙㥛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻偽", a_));
				}
				IL_2D:
				INamedRange namedRange = names[RecordTableEnumerator.b("ᵁɃ⽅⑇㹉⥋㱍ᑏ㍑⁓㝕㩗㭙⽛㭝", a_)];
				return namedRange.ValueR1C1;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00015604 File Offset: 0x00014604
		public Worksheet Worksheet
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
				return (Worksheet)this.ᜃ;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001564C File Offset: 0x0001464C
		public bool HasFiltered
		{
			get
			{
				int num = 1;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						return false;
					case 2:
						return true;
					case 3:
						if (this[num2].IsFiltered)
						{
							num = 2;
							continue;
						}
						num2++;
						if (true)
						{
						}
						num = 7;
						continue;
					case 4:
						goto IL_7A;
					case 5:
						if (num2 < base.Count)
						{
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 6:
						return false;
					case 7:
						goto IL_7A;
					}
					goto IL_30;
					IL_38:
					num = 0;
					continue;
					IL_30:
					if (base.Count == 0)
					{
						goto IL_38;
					}
					num2 = 0;
					num = 4;
					continue;
					IL_7A:
					num = 5;
				}
				return false;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0001572C File Offset: 0x0001472C
		public string DefaultNamedRangeName
		{
			get
			{
				int a_ = 4;
				for (;;)
				{
					ExcelVersion version = this.Worksheet.Version;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_7B;
						case 2:
							switch (version)
							{
							case ExcelVersion.Version97to2003:
								goto IL_5C;
							case ExcelVersion.Version2007:
							case ExcelVersion.Version2010:
								goto IL_4D;
							default:
								num = 0;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_4D:
				return RecordTableEnumerator.b("改䐻刽⸿⽁橃᥅็⍉⁋㩍㕏⁑ၓ㝕ⱗ㭙㹛㽝፟ݡ", a_);
				IL_5C:
				if (true)
				{
				}
				return RecordTableEnumerator.b("改稻圽ⰿ㙁⅃㑅ే⭉㡋⽍㉏㍑❓㍕", a_);
				IL_7B:
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍㕏⩑㝓㍕㑗穙⩛㭝቟ᅡൣ॥٧", a_));
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000157F0 File Offset: 0x000147F0
		internal new void ᜀ(List<BiffRecordRaw> A_0)
		{
			int a_ = 0;
			sprᱠ a_2;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_168:
				BiffRecordRaw biffRecordRaw;
				a_2 = (sprᱠ)biffRecordRaw;
				num = 3;
				break;
			}
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num = 14;
					break;
				}
				break;
			}
			for (;;)
			{
				int num2;
				int a_3;
				int a_4;
				int num3;
				int count;
				switch (num)
				{
				case 0:
				{
					if (!this.ᜃ.Names.Contains(RecordTableEnumerator.b("椵縷匹倻䨽┿ぁC❅㱇⭉⹋⽍⍏㝑", a_)))
					{
						num = 12;
						continue;
					}
					INamedRange namedRange = this.ᜃ.Names[RecordTableEnumerator.b("椵縷匹倻䨽┿ぁC❅㱇⭉⹋⽍⍏㝑", a_)];
					this.ᜂ = namedRange.RefersToRange;
					num = 6;
					continue;
				}
				case 1:
					return;
				case 2:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.FilterMode:
						goto IL_19F;
					case TBIFFRecord.FnGroupCount:
						goto IL_22F;
					case TBIFFRecord.AutoFilterInfo:
						num2 = 0;
						num = 16;
						continue;
					case TBIFFRecord.AutoFilter:
						goto IL_168;
					default:
						num = 20;
						continue;
					}
					break;
				}
				case 3:
					if (base.InnerList.Count <= num2)
					{
						num = 17;
						continue;
					}
					goto IL_303;
				case 4:
					return;
				case 5:
					goto IL_303;
				case 6:
					if (this.ᜂ == null)
					{
						num = 18;
						continue;
					}
					a_3 = 0;
					a_4 = 0;
					num = 15;
					continue;
				case 7:
					goto IL_243;
				case 8:
					goto IL_1B6;
				case 9:
					goto IL_E7;
				case 10:
					goto IL_339;
				case 11:
					goto IL_243;
				case 12:
					return;
				case 13:
					num = 19;
					continue;
				case 15:
					if (this.ᜂ != null)
					{
						num = 25;
						continue;
					}
					goto IL_E7;
				case 16:
					if (true)
					{
					}
					goto IL_19F;
				case 17:
				{
					XlsAutoFilter item = new AutoFilter(this);
					base.InnerList.Add(item);
					num = 5;
					continue;
				}
				case 18:
					return;
				case 19:
					if (A_0.Count == 0)
					{
						num = 4;
						continue;
					}
					num2 = 0;
					num = 0;
					continue;
				case 20:
					num = 10;
					continue;
				case 21:
				{
					if (num3 >= count)
					{
						num = 1;
						continue;
					}
					BiffRecordRaw biffRecordRaw = A_0[num3];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 2;
					continue;
				}
				case 22:
				{
					int num4;
					int num5;
					if (num4 >= num5)
					{
						num = 9;
						continue;
					}
					XlsAutoFilter item2 = new AutoFilter(this);
					base.InnerList.Add(item2);
					num4++;
					num = 24;
					continue;
				}
				case 23:
					goto IL_19F;
				case 24:
					goto IL_1B6;
				case 25:
				{
					a_3 = this.ᜂ.Row;
					a_4 = this.ᜂ.Column;
					int num4 = 0;
					int num5 = this.ᜂ.LastColumn - this.ᜂ.Column + 1;
					num = 8;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 13;
					continue;
				}
				return;
				IL_E7:
				num3 = 0;
				count = A_0.Count;
				num = 7;
				continue;
				IL_19F:
				num3++;
				num = 11;
				continue;
				IL_1B6:
				num = 22;
				continue;
				IL_243:
				num = 21;
				continue;
				IL_303:
				((XlsAutoFilter)this[num2]).ᜀ(a_2, a_4, a_3);
				num2++;
				num = 23;
			}
			return;
			IL_22F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("挵嘷儹刻儽㜿ⱁ摃㑅ⵇ⥉⍋㱍㑏", a_));
			IL_339:
			goto IL_22F;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00015BB4 File Offset: 0x00014BB4
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 12;
			switch (0)
			{
			default:
				for (;;)
				{
					int count = base.Count;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_CA;
						case 1:
						{
							bool hasFiltered;
							if (hasFiltered)
							{
								num = 12;
								continue;
							}
							return;
						}
						case 2:
						{
							if (records == null)
							{
								num = 0;
								continue;
							}
							bool hasFiltered = this.HasFiltered;
							num = 7;
							continue;
						}
						case 3:
							return;
						case 4:
							goto IL_FB;
						case 5:
							if (count == 0)
							{
								num = 8;
								continue;
							}
							num = 2;
							continue;
						case 6:
							goto IL_81;
						case 7:
						{
							if (true)
							{
							}
							bool hasFiltered;
							if (hasFiltered)
							{
								num = 9;
								continue;
							}
							goto IL_FB;
						}
						case 8:
							return;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_81;
							default:
							{
								if (false)
								{
								}
								sprᾠ a_2 = (sprᾠ)spr\u175E.ᜀ(TBIFFRecord.FilterMode);
								records.ᜀ(a_2);
								num = 4;
								continue;
							}
							}
							break;
						case 10:
						{
							int num2;
							if (num2 >= count)
							{
								num = 3;
								continue;
							}
							XlsAutoFilter xlsAutoFilter = (XlsAutoFilter)this[num2];
							xlsAutoFilter.SerializeDataToList(records);
							num2++;
							num = 11;
							continue;
						}
						case 11:
							goto IL_148;
						case 12:
						{
							int num2 = 0;
							num = 6;
							continue;
						}
						}
						break;
						IL_FB:
						spr\u23C9 spr_u23C = (spr\u23C9)spr\u175E.ᜀ(TBIFFRecord.AutoFilterInfo);
						spr_u23C.ᜀ((ushort)count);
						records.ᜀ(spr_u23C);
						num = 1;
						continue;
						IL_148:
						num = 10;
						continue;
						IL_81:
						goto IL_148;
					}
				}
				return;
				IL_CA:
				throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃╅❇㡉⡋㵍", a_));
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00015D90 File Offset: 0x00014D90
		private new void ᜀ()
		{
			int a_ = 5;
			for (;;)
			{
				INameRanges names = this.Worksheet.Names;
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (names.Contains(RecordTableEnumerator.b("携笼嘾ⵀ㝂⁄㕆ൈ⩊㥌⹎㍐㉒♔㉖", a_)))
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_57;
						default:
							goto IL_95;
						}
						break;
					case 2:
						goto IL_57;
					}
					break;
					IL_57:
					names.Remove(RecordTableEnumerator.b("携笼嘾ⵀ㝂⁄㕆ൈ⩊㥌⹎㍐㉒♔㉖", a_));
					num = 1;
				}
			}
			IL_95:
			if (false)
			{
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00015E3C File Offset: 0x00014E3C
		public XlsAutoFiltersCollection Clone(XlsWorksheet parent)
		{
			int a_ = 14;
			int num = 2;
			XlsAutoFiltersCollection xlsAutoFiltersCollection;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜂ != null)
					{
						num = 4;
						continue;
					}
					goto IL_9E;
				case 1:
					goto IL_9E;
				case 3:
					goto IL_38;
				case 4:
					xlsAutoFiltersCollection.ᜂ = parent[this.ᜂ.RangeAddressLocal];
					num = 1;
					continue;
				}
				if (parent == null)
				{
					num = 3;
					continue;
				}
				IL_63:
				xlsAutoFiltersCollection = (XlsAutoFiltersCollection)base.Clone(parent);
				num = 0;
				continue;
				IL_9E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_63;
				default:
					goto IL_B4;
				}
			}
			IL_38:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_));
			IL_B4:
			if (false)
			{
			}
			return xlsAutoFiltersCollection;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00015F10 File Offset: 0x00014F10
		internal new void ᜀ(int A_0, int A_1, ExcelVersion A_2)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 5;
				int num2;
				int num3;
				for (;;)
				{
					int num4;
					int count;
					switch (num)
					{
					case 0:
						goto IL_1A7;
					case 1:
						goto IL_15D;
					case 2:
						goto IL_1A2;
					case 3:
						if (num2 > A_1)
						{
							num = 16;
							continue;
						}
						goto IL_122;
					case 4:
						if (A_2 == ExcelVersion.Version2010)
						{
							num = 17;
							continue;
						}
						num2 = this.Range.LastColumn;
						num3 = this.Range.LastRow;
						num = 6;
						continue;
					case 6:
						if (this.Range.Column <= A_1)
						{
							num = 8;
							continue;
						}
						goto IL_1A7;
					case 7:
						num = 4;
						continue;
					case 8:
						num = 15;
						continue;
					case 9:
						goto IL_122;
					case 10:
						goto IL_A4;
					case 11:
						if (num4 >= count)
						{
							if (true)
							{
							}
							num = 12;
							continue;
						}
						((XlsAutoFilter)this[num4]).Clear();
						num4++;
						num = 13;
						continue;
					case 12:
						goto IL_CD;
					case 13:
						goto IL_A4;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15D;
						default:
							if (false)
							{
							}
							if (num3 > A_0)
							{
								num = 1;
								continue;
							}
							goto IL_251;
						}
						break;
					case 15:
						if (this.Range.Row > A_0)
						{
							num = 0;
							continue;
						}
						num = 3;
						continue;
					case 16:
						num2 = A_1;
						num = 9;
						continue;
					case 17:
						goto IL_1E2;
					}
					if (A_2 != ExcelVersion.Version2007)
					{
						num = 7;
						continue;
					}
					goto IL_15F;
					IL_A4:
					num = 11;
					continue;
					IL_122:
					num = 14;
					continue;
					IL_15D:
					num3 = A_0;
					num = 2;
					continue;
					IL_1A7:
					num4 = 0;
					count = base.Count;
					num = 10;
				}
				IL_CD:
				base.Clear();
				return;
				IL_15F:
				INameRanges names = this.ᜃ.Names;
				INamedRange namedRange = names[RecordTableEnumerator.b("Ἷсⵃ⩅㱇⽉㹋੍ㅏ♑㕓㑕㥗⥙㥛", a_)];
				namedRange.Name = this.DefaultNamedRangeName;
				return;
				IL_1A2:
				goto IL_251;
				IL_1E2:
				goto IL_15F;
				IL_251:
				INameRanges names2 = this.ᜃ.Names;
				INamedRange namedRange2 = names2[RecordTableEnumerator.b("Ἷ㩁⡃⡅╇摉ፋࡍ㥏㹑⁓㍕⩗ṙ㵛⩝şaգᕥ൧", a_)];
				namedRange2.Name = this.DefaultNamedRangeName;
				this.Range = this.Worksheet[this.Range.Row, this.Range.Column, num3, num2];
				return;
			}
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000161C8 File Offset: 0x000151C8
		internal new void ᜂ()
		{
			INameRanges names = this.ᜃ.Names;
			INamedRange namedRange = names[this.DefaultNamedRangeName];
			if (namedRange == null)
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
					this.ᜂ = null;
					return;
				}
			}
			if (true)
			{
			}
			this.ᜂ = namedRange.RefersToRange;
		}

		// Token: 0x04000071 RID: 113
		internal new const string ᜀ = "_FilterDatabase";

		// Token: 0x04000072 RID: 114
		private string[] \u25D8\u008F\u0089\u0092;

		// Token: 0x04000073 RID: 115
		internal new const string ᜁ = "_xlnm._FilterDatabase";

		// Token: 0x04000074 RID: 116
		private new IXLSRange ᜂ;

		// Token: 0x04000075 RID: 117
		private long \u2609\u0086\u007F\u00A7;

		// Token: 0x04000076 RID: 118
		private float[] \u25D9\u0088\u00A8\u008A;

		// Token: 0x04000077 RID: 119
		private float[] \u25D8\u0091\u0084\u007F;

		// Token: 0x04000078 RID: 120
		private XlsWorksheet ᜃ;
	}
}
