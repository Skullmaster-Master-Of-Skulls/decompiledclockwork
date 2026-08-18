using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200001B RID: 27
	public class XlsHyperLinksCollection : CollectionExtended<HyperLink>, IHyperLinks, ICloneParent
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00012B5C File Offset: 0x00011B5C
		internal XlsHyperLinksCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
			this.CreateHyperlinkStyles();
			base.Removed += this.ᜀ;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00012BA8 File Offset: 0x00011BA8
		internal XlsHyperLinksCollection(spr\u1DF5 A_0, object A_1, bool A_2) : this(A_0, A_1)
		{
			this.ᜁ = A_2;
			if (!this.ᜁ)
			{
				this.ᜃ = new List<IHyperLink>();
			}
			this.ᜀ();
			this.CreateHyperlinkStyles();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00012BE8 File Offset: 0x00011BE8
		private new void ᜀ()
		{
			int a_ = 19;
			this.ᜀ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜀ == null)
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
						goto IL_56;
					}
				}
				IL_56:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌♎㽐㑒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ孴", a_));
			}
		}

		// Token: 0x170000E5 RID: 229
		public IHyperLink this[int index]
		{
			get
			{
				int a_ = 17;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						if (index > base.Count - 1)
						{
							num = 0;
							continue;
						}
						goto IL_A4;
					case 3:
						num = 1;
						continue;
					}
					if (index < 0)
					{
						break;
					}
					if (true)
					{
					}
					num = 3;
				}
				IL_3F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎", a_), RecordTableEnumerator.b("ᅆ⡈❊㡌⩎煐げ㑔㥖㝘㑚⥜罞͠٢䕤୦౨ᡪṬ佮հ᭲ᑴ᥶奸䭺嵼Ṿꖄﮈﮎ떔漢뾞슠첢키즦\udda8薪", a_));
				IL_86:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3F;
				default:
					if (false)
					{
					}
					goto IL_3F;
				}
				IL_A4:
				return base.List[index];
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00012D28 File Offset: 0x00011D28
		public new bool IsReadOnly
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

		// Token: 0x06000224 RID: 548 RVA: 0x00012D6C File Offset: 0x00011D6C
		public IHyperLink Add(IXLSRange range)
		{
			int a_ = 1;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 2:
					goto IL_5A;
				case 3:
					if (this.ᜁ)
					{
						num = 0;
						continue;
					}
					goto IL_98;
				}
				IL_33:
				if (range != null)
				{
					num = 3;
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
				goto IL_33;
			}
			IL_5A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶堸唺娼娾", a_));
			IL_7A:
			if (true)
			{
			}
			throw new spr\u23DE();
			IL_98:
			HyperLink hyperLink = base.AppImplementation.ᜀ(this, range) as HyperLink;
			this.Add(hyperLink);
			this.ᜀ(hyperLink);
			return hyperLink;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00012E34 File Offset: 0x00011E34
		public new void RemoveAt(int index)
		{
			int a_ = 9;
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_33;
				default:
					goto IL_33;
				}
				IL_6D:
				if (this.ᜁ)
				{
					num = 5;
					continue;
				}
				goto IL_D2;
				IL_33:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					goto IL_CA;
				case 3:
					if (index > base.Count - 1)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
				case 4:
					goto IL_6D;
				case 5:
					goto IL_7D;
				}
				if (index < 0)
				{
					goto IL_7F;
				}
				num = 0;
			}
			IL_7D:
			throw new spr\u23DE();
			IL_7F:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾⽀❂⁄㽆", a_), RecordTableEnumerator.b("椾⁀⽂い≆楈⡊ⱌⅎ㽐㱒⅔睖㭘㹚絜㍞Ѡၢᙤ䝦ᵨͪ౬Ů兰䍲啴ᙶ᝸ὺ嵼᡾力권ﮎ戀ﮔ랖滛햠趢", a_));
			IL_CA:
			goto IL_7F;
			IL_D2:
			base.RemoveAt(index);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00012F1C File Offset: 0x00011F1C
		protected internal int Add(IHyperLink link)
		{
			int a_ = 13;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 2:
					goto IL_40;
				case 3:
					if (this.ᜁ)
					{
						num = 6;
						continue;
					}
					goto IL_D8;
				case 4:
					goto IL_6A;
				case 5:
					if (this.ᜃ.Contains(link))
					{
						num = 4;
						continue;
					}
					this.ᜃ.Add(link);
					num = 0;
					continue;
				case 6:
					num = 5;
					continue;
				}
				if (link == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("⽂ⱄ⥆≈", a_));
			IL_6A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_B3:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				return -1;
			}
			IL_D8:
			base.Add(link as HyperLink);
			return base.Count - 1;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00013020 File Offset: 0x00012020
		protected internal int Parse(IList data, int iPos)
		{
			int a_ = 9;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (iPos >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_70;
				case 1:
					goto IL_4C;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return iPos;
					default:
						if (false)
						{
						}
						goto IL_B1;
					}
					break;
				case 3:
					if (true)
					{
					}
					goto IL_B1;
				case 4:
				{
					if (iPos > data.Count - 1)
					{
						num = 8;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
					num = 3;
					continue;
				}
				case 6:
				{
					BiffRecordRaw biffRecordRaw;
					if (biffRecordRaw.TypeCode != TBIFFRecord.HLink)
					{
						num = 7;
						continue;
					}
					HyperLink hyperLink = new HyperLink((spr\u2158)base.ReservedHandle, this, data, ref iPos);
					this.Add(hyperLink);
					this.ᜀ(hyperLink);
					biffRecordRaw = (BiffRecordRaw)data[iPos];
					num = 2;
					continue;
				}
				case 7:
					return iPos;
				case 8:
					goto IL_178;
				case 9:
					num = 4;
					continue;
				}
				if (data == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				IL_B1:
				num = 6;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬾⁀㝂⑄", a_));
			IL_70:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("伾⹀あ", a_), RecordTableEnumerator.b("椾⁀⽂い≆楈⡊ⱌⅎ㽐㱒⅔睖㭘㹚絜㍞Ѡၢᙤ䝦ᵨͪ౬Ů兰䍲啴ᙶ᝸ὺ嵼᡾力권ﮎ戀ﮔ랖ﶘ漢ﺞ膠삢쪤튦잨\udfaa", a_));
			IL_178:
			goto IL_70;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000131AC File Offset: 0x000121AC
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 1;
			int num = 4;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						goto IL_B8;
					}
					break;
				case 1:
					goto IL_B8;
				case 2:
				{
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					XlsHyperLink xlsHyperLink = base.List[num2];
					xlsHyperLink.ᜀ(A_0);
					num2++;
					num = 0;
					continue;
				}
				case 3:
					return;
				case 5:
					goto IL_3C;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num2 = 0;
				count = base.Count;
				num = 1;
				continue;
				IL_B8:
				num = 2;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00013290 File Offset: 0x00012290
		public void CreateHyperlinkStyles()
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
				{
					XlsStyle xlsStyle = this.ᜀ.InnerStyles.CreateBuiltInStyle(RecordTableEnumerator.b("爹䔻丽┿ぁ⡃⽅♇ⅉ", a_));
					IFont font = xlsStyle.Font;
					font.Underline = FontUnderlineType.Single;
					font.KnownColor = ExcelColors.BlueCustom;
					num = 1;
					continue;
				}
				}
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
					if (this.ᜀ.InnerStyles.ᜁ(RecordTableEnumerator.b("爹䔻丽┿ぁ⡃⽅♇ⅉ", a_)))
					{
						return;
					}
					break;
				}
				num = 2;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0001335C File Offset: 0x0001235C
		public XlsHyperLinksCollection GetRangeHyperlinks(IXLSRange range)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
						goto IL_189;
					case 1:
						num = 12;
						continue;
					case 2:
					{
						XlsHyperLinksCollection xlsHyperLinksCollection;
						return xlsHyperLinksCollection;
					}
					case 3:
						goto IL_7C;
					case 4:
						goto IL_189;
					case 5:
						goto IL_142;
					case 7:
					{
						if (this.ᜁ)
						{
							num = 16;
							continue;
						}
						XlsHyperLinksCollection xlsHyperLinksCollection = new HyperLinksCollection((spr\u2158)base.ReservedHandle, range, true);
						int row = range.Row;
						int column = range.Column;
						int lastRow = range.LastRow;
						int lastColumn = range.LastColumn;
						int num2 = row;
						num = 5;
						continue;
					}
					case 8:
					{
						bool flag;
						if (flag)
						{
							num = 1;
							continue;
						}
						goto IL_81;
					}
					case 9:
					{
						int lastRow;
						int num2;
						if (num2 > lastRow)
						{
							num = 2;
							continue;
						}
						int column;
						num3 = column;
						num = 4;
						continue;
					}
					case 10:
					{
						XlsHyperLinksCollection xlsHyperLinksCollection;
						List<HyperLink> list;
						xlsHyperLinksCollection.AddRange(list);
						num = 14;
						continue;
					}
					case 11:
					{
						int lastColumn;
						if (num3 > lastColumn)
						{
							num = 13;
							continue;
						}
						int num2;
						long key = sprṔ.ᜀ(num3, num2);
						List<HyperLink> list;
						bool flag = this.ᜂ.TryGetValue(key, out list);
						num = 8;
						continue;
					}
					case 12:
					{
						List<HyperLink> list;
						if (list.Count > 0)
						{
							num = 10;
							continue;
						}
						goto IL_81;
					}
					case 13:
					{
						if (true)
						{
						}
						int num2;
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_104;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						break;
					}
					case 14:
						goto IL_81;
					case 15:
						goto IL_142;
					case 16:
						goto IL_127;
					}
					if (range == null)
					{
						num = 3;
						continue;
					}
					goto IL_104;
					IL_81:
					num3++;
					num = 0;
					continue;
					IL_104:
					num = 7;
					continue;
					IL_142:
					num = 9;
					continue;
					IL_189:
					num = 11;
				}
				IL_7C:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠹崻倽✿❁", a_));
				IL_127:
				throw new NotSupportedException(RecordTableEnumerator.b("根夻弽␿潁⭃⡅⑇㍉汋ٍ⥏≑ㅓ⑕㑗㍙㉛㕝፟䉡ݣ॥ѧ٩५൭ѯ᭱᭳ᡵ୷婹ύώꢇ黎曆ﺍ﶑뢗ﮝ肟춡풣쎥\udaa7쮩\ud8ab잭\udfaf\udcb1骳", a_));
			}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000135A4 File Offset: 0x000125A4
		internal new void ᜀ(HyperLink A_0)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					int num3;
					int num5;
					int num4;
					List<HyperLink> list;
					int num6;
					switch (num)
					{
					case 0:
						goto IL_76;
					case 1:
						goto IL_71;
					case 2:
						return;
					case 3:
						goto IL_76;
					case 4:
						goto IL_E9;
					case 5:
						num2++;
						num = 6;
						continue;
					case 6:
						goto IL_132;
					case 7:
						if (true)
						{
						}
						break;
					case 8:
						if (num2 <= num3)
						{
							num4 = num5;
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E9;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 9:
					{
						list = new List<HyperLink>();
						long key;
						this.ᜂ[key] = list;
						num = 12;
						continue;
					}
					case 10:
					{
						if (num4 > num6)
						{
							num = 5;
							continue;
						}
						long key = sprṔ.ᜀ(num4, num2);
						num = 11;
						continue;
					}
					case 11:
					{
						long key;
						if (!this.ᜂ.TryGetValue(key, out list))
						{
							num = 9;
							continue;
						}
						goto IL_113;
					}
					case 12:
						goto IL_113;
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					int num7 = A_0.FirstRow + 1;
					num5 = A_0.FirstColumn + 1;
					num3 = A_0.LastRow + 1;
					num6 = A_0.LastColumn + 1;
					num2 = num7;
					num = 4;
					continue;
					IL_76:
					num = 10;
					continue;
					IL_113:
					list.Add(A_0);
					num4++;
					num = 0;
					continue;
					IL_132:
					num = 8;
					continue;
					IL_E9:
					goto IL_132;
				}
				IL_71:
				throw new ArgumentNullException(RecordTableEnumerator.b("⥄⹆❈⁊", a_));
			}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00013778 File Offset: 0x00012778
		protected internal void AddRange(IList<HyperLink> collection)
		{
			int a_ = 1;
			int num = 0;
			for (;;)
			{
				IEnumerator<HyperLink> enumerator;
				switch (num)
				{
				case 1:
					goto IL_3B;
				case 2:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								XlsHyperLink link = enumerator.Current;
								this.Add(link);
								num = 4;
								continue;
							}
							case 3:
								goto IL_A1;
							}
							IL_7F:
							num = 2;
							continue;
							goto IL_7F;
						}
						IL_A1:
						return;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_F9;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FB;
								default:
									if (false)
									{
									}
									enumerator.Dispose();
									num = 0;
									continue;
								}
								break;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 2;
						}
						IL_F9:
						IL_FB:;
					}
					goto IL_FC;
				}
				if (true)
				{
				}
				if (collection == null)
				{
					num = 1;
					continue;
				}
				IL_FC:
				enumerator = collection.GetEnumerator();
				num = 2;
			}
			IL_3B:
			throw new ArgumentNullException(RecordTableEnumerator.b("吶嘸场儼娾≀㝂ⱄ⡆❈", a_));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000138C8 File Offset: 0x000128C8
		protected internal IHyperLink GetHyperlinkByCellIndex(long lCellIndex)
		{
			List<HyperLink> list;
			for (;;)
			{
				bool flag = this.ᜂ.TryGetValue(lCellIndex, out list);
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_95;
				default:
				{
					if (false)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (list.Count > 0)
							{
								num = 1;
								continue;
							}
							goto IL_97;
						case 1:
							goto IL_95;
						case 2:
							if (flag)
							{
								num = 3;
								continue;
							}
							goto IL_97;
						case 3:
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			IL_95:
			return list[list.Count - 1];
			IL_97:
			return null;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00013970 File Offset: 0x00012970
		public override object Clone(object parent)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					Dictionary<long, List<HyperLink>>.Enumerator enumerator;
					XlsHyperLinksCollection xlsHyperLinksCollection;
					switch (num)
					{
					case 1:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									KeyValuePair<long, List<HyperLink>> keyValuePair = enumerator.Current;
									List<HyperLink> list = keyValuePair.Value;
									list = spr\u1CD3.ᜀ<HyperLink>(list, xlsHyperLinksCollection);
									xlsHyperLinksCollection.ᜂ.Add(keyValuePair.Key, list);
									num = 1;
									continue;
								}
								case 1:
									goto IL_D5;
								case 3:
									goto IL_F5;
								case 4:
									goto IL_101;
								}
								goto IL_81;
								IL_D5:
								num = 0;
								continue;
								IL_81:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_F5:
									num = 4;
									break;
								default:
									if (false)
									{
									}
									goto IL_D5;
								}
							}
							IL_101:
							return xlsHyperLinksCollection;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_111;
					case 2:
						goto IL_56;
					}
					if (true)
					{
					}
					if (parent == null)
					{
						num = 2;
						continue;
					}
					IL_111:
					xlsHyperLinksCollection = (XlsHyperLinksCollection)base.Clone(parent);
					xlsHyperLinksCollection.ᜁ = this.ᜁ;
					enumerator = this.ᜂ.GetEnumerator();
					num = 1;
				}
				IL_56:
				throw new ArgumentNullException(RecordTableEnumerator.b("〿⍁㙃⍅♇㹉", a_));
			}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00013AEC File Offset: 0x00012AEC
		internal new void ᜁ()
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
			this.ᜂ.Clear();
			this.ᜃ.Clear();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00013B40 File Offset: 0x00012B40
		private new void ᜀ(object A_0, CollectionChangeEventArgs<HyperLink> A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					HyperLink value = A_1.Value;
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						int num3;
						switch (num)
						{
						case 0:
						{
							int num2;
							int lastRow;
							if (num2 > lastRow)
							{
								num = 11;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B4;
							default:
							{
								if (false)
								{
								}
								int column;
								num3 = column;
								num = 3;
								continue;
							}
							}
							break;
						}
						case 1:
						{
							long key;
							List<HyperLink> list;
							if (this.ᜂ.TryGetValue(key, out list))
							{
								num = 7;
								continue;
							}
							goto IL_119;
						}
						case 2:
						{
							if (value == null)
							{
								num = 10;
								continue;
							}
							IXLSRange range = value.Range;
							int row = range.Row;
							int column = range.Column;
							int lastRow = range.LastRow;
							int lastColumn = range.LastColumn;
							int num2 = row;
							num = 4;
							continue;
						}
						case 3:
							goto IL_1B4;
						case 4:
							goto IL_130;
						case 5:
							goto IL_87;
						case 6:
						{
							int lastColumn;
							if (num3 > lastColumn)
							{
								num = 12;
								continue;
							}
							int num2;
							long key = sprṔ.ᜀ(num3, num2);
							num = 1;
							continue;
						}
						case 7:
						{
							List<HyperLink> list;
							list.Remove(value);
							num = 8;
							continue;
						}
						case 8:
							goto IL_119;
						case 9:
							goto IL_130;
						case 10:
							goto IL_82;
						case 11:
							return;
						case 12:
						{
							int num2;
							num2++;
							num = 9;
							continue;
						}
						}
						break;
						IL_87:
						num = 6;
						continue;
						IL_1B4:
						goto IL_87;
						IL_119:
						num3++;
						num = 5;
						continue;
						IL_130:
						num = 0;
					}
				}
				IL_82:
				throw new ArgumentNullException(RecordTableEnumerator.b("娵儷吹圻", a_));
			}
		}

		// Token: 0x0400005D RID: 93
		private new XlsWorkbook ᜀ;

		// Token: 0x0400005E RID: 94
		private new bool ᜁ;

		// Token: 0x0400005F RID: 95
		private long \u25D9\u00A2\u00A7\u0091;

		// Token: 0x04000060 RID: 96
		private new Dictionary<long, List<HyperLink>> ᜂ = new Dictionary<long, List<HyperLink>>();

		// Token: 0x04000061 RID: 97
		private string[] \u2460\u00A5\u0094\u00AB;

		// Token: 0x04000062 RID: 98
		private List<IHyperLink> ᜃ = new List<IHyperLink>();
	}
}
