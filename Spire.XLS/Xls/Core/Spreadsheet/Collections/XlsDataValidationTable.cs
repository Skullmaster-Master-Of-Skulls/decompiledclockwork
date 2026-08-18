using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200020B RID: 523
	public class XlsDataValidationTable : CollectionExtended<XlsDataValidationCollection>
	{
		// Token: 0x06001EA6 RID: 7846 RVA: 0x00103310 File Offset: 0x00102310
		internal XlsDataValidationTable(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00103338 File Offset: 0x00102338
		internal XlsDataValidationTable(spr\u1DF5 A_0, object A_1, List<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00103358 File Offset: 0x00102358
		private new void ᜀ()
		{
			int a_ = 17;
			for (;;)
			{
				this.ᜀ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
				if (this.ᜀ == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("͆Ὀ⩊⅌潎⍐㙒㙔㡖⭘㽚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᕲᩴɶ᝸ὺ卼", a_));
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x001033D8 File Offset: 0x001023D8
		internal new void ᜀ(List<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 19;
			int num = 7;
			for (;;)
			{
				int count;
				spr\u22CB spr_u22CB;
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					if (A_1 >= count)
					{
						num = 9;
						continue;
					}
					goto IL_51;
				case 2:
					num = 6;
					continue;
				case 3:
					return;
				case 4:
				{
					if (spr_u22CB == null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					XlsDataValidationCollection xlsDataValidationCollection = new DataValidationCollection((spr\u2158)base.ReservedHandle, this, A_0, ref A_1);
					base.Add(xlsDataValidationCollection);
					this.ᜁ[spr_u22CB] = xlsDataValidationCollection;
					num = 1;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_166;
					default:
						if (false)
						{
						}
						if (A_1 >= 0)
						{
							num = 2;
							continue;
						}
						goto IL_7F;
					}
					break;
				case 6:
					if (A_1 > count)
					{
						num = 8;
						continue;
					}
					goto IL_51;
				case 8:
					goto IL_166;
				case 9:
					return;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				count = A_0.Count;
				num = 5;
				continue;
				IL_51:
				spr_u22CB = (A_0[A_1] as spr\u22CB);
				num = 4;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
			IL_7F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("♈ⵊ⭌㱎㑐❒", a_), RecordTableEnumerator.b("Ὀ⩊⅌㩎㑐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ը๪Ṭᱮ兰ݲᵴᙶ᝸孺䵼彾ꖄﮈﮎ떔漢뾞펠욢욤좦\udba8쾪\udeac膮\udcb2살\ud9b6춸閺", a_));
			IL_166:
			goto IL_7F;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00103550 File Offset: 0x00102550
		public new XlsDataValidationCollection Add(XlsDataValidationCollection dval)
		{
			spr\u22CB key;
			for (;;)
			{
				key = dval.Record;
				if (this.ᜁ.ContainsKey(key))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				goto IL_52;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.ᜁ[key];
			IL_52:
			this.ᜁ.Add(key, dval);
			base.Add(dval);
			return dval;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x001035C4 File Offset: 0x001025C4
		internal new XlsDataValidationCollection ᜀ(spr\u22CB A_0)
		{
			int num = 7;
			int num2;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 0:
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				case 1:
					goto IL_AD;
				case 2:
					if (base[num2].Worksheet.Index == this.Worksheet.Index)
					{
						num = 6;
						continue;
					}
					num2++;
					num = 4;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_46;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_93;
					}
					break;
				case 4:
					goto IL_93;
				case 5:
					goto IL_46;
				case 6:
					goto IL_84;
				}
				if (this.ᜁ.ContainsKey(A_0))
				{
					num = 5;
					continue;
				}
				num2 = 0;
				count = base.Count;
				num = 3;
				continue;
				IL_93:
				num = 0;
			}
			IL_46:
			return this.ᜁ[A_0];
			IL_84:
			XlsDataValidationCollection xlsDataValidationCollection = base[num2];
			this.ᜁ.Add(A_0, xlsDataValidationCollection);
			return xlsDataValidationCollection;
			IL_AD:
			xlsDataValidationCollection = new DataValidationCollection((spr\u2158)base.ReservedHandle, this, A_0);
			this.ᜁ.Add(A_0, xlsDataValidationCollection);
			base.Add(xlsDataValidationCollection);
			return xlsDataValidationCollection;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00103704 File Offset: 0x00102704
		public override object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				XlsDataValidationTable xlsDataValidationTable;
				for (;;)
				{
					xlsDataValidationTable = (XlsDataValidationTable)base.Clone(parent);
					List<XlsDataValidationCollection> innerList = xlsDataValidationTable.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_4D;
						case 1:
							goto IL_4F;
						case 2:
							return xlsDataValidationTable;
						case 3:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4D;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								XlsDataValidationCollection xlsDataValidationCollection = innerList[num];
								xlsDataValidationTable.ᜁ.Add(xlsDataValidationCollection.Record, xlsDataValidationCollection);
								num++;
								num2 = 1;
								continue;
							}
							}
							break;
						}
						break;
						IL_4F:
						num2 = 3;
						continue;
						IL_4D:
						goto IL_4F;
					}
				}
				return xlsDataValidationTable;
			}
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x001037D8 File Offset: 0x001027D8
		public XlsValidation FindDataValidation(long iCellIndex)
		{
			switch (0)
			{
			default:
			{
				XlsValidation xlsValidation;
				for (;;)
				{
					IL_2F:
					int num = 0;
					int count = base.Count;
					for (;;)
					{
						IL_38:
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return xlsValidation;
							case 1:
								if (xlsValidation != null)
								{
									num2 = 0;
									continue;
								}
								if (true)
								{
								}
								num++;
								num2 = 4;
								continue;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_38;
								default:
								{
									if (false)
									{
									}
									if (num >= count)
									{
										num2 = 5;
										continue;
									}
									XlsDataValidationCollection xlsDataValidationCollection = this[num];
									xlsValidation = xlsDataValidationCollection.FindByCellIndex(iCellIndex);
									num2 = 1;
									continue;
								}
								}
								break;
							case 3:
								goto IL_8D;
							case 4:
								goto IL_8D;
							case 5:
								goto IL_C5;
							}
							goto IL_2F;
							IL_8D:
							num2 = 2;
						}
					}
				}
				return xlsValidation;
				IL_C5:
				return null;
			}
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x001038B0 File Offset: 0x001028B0
		internal new XlsValidation ᜀ(int A_0, int A_1)
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
			long iCellIndex = sprṔ.ᜀ(A_1, A_0);
			return this.FindDataValidation(iCellIndex);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x001038FC File Offset: 0x001028FC
		public void UpdateNamedRangeIndexes(int[] arrNewIndex)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return;
				case 1:
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
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						XlsDataValidationCollection xlsDataValidationCollection = base.InnerList[num2];
						xlsDataValidationCollection.UpdateNamedRangeIndexes(arrNewIndex);
						num2++;
						num = 3;
						continue;
					}
					}
					break;
				case 3:
					goto IL_99;
				case 4:
					goto IL_99;
				case 5:
					goto IL_3C;
				}
				if (arrNewIndex == null)
				{
					num = 5;
					continue;
				}
				num2 = 0;
				count = base.Count;
				num = 4;
				continue;
				IL_99:
				num = 1;
			}
			IL_3C:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╃㑅㩇щ⥋㥍᥏㱑こ㍕⁗", a_));
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x001039E0 File Offset: 0x001029E0
		public void UpdateNamedRangeIndexes(IDictionary<int, int> dicNewIndex)
		{
			int a_ = 8;
			int num = 3;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_99;
				case 1:
					return;
				case 2:
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
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						XlsDataValidationCollection xlsDataValidationCollection = base.InnerList[num2];
						xlsDataValidationCollection.UpdateNamedRangeIndexes(dicNewIndex);
						num2++;
						num = 5;
						continue;
					}
					}
					break;
				case 4:
					goto IL_44;
				case 5:
					goto IL_99;
				}
				if (dicNewIndex == null)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				num2 = 0;
				count = base.Count;
				num = 0;
				continue;
				IL_99:
				num = 2;
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("娽⤿⅁੃⍅㽇͉≋⩍㕏⩑", a_));
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00103AC0 File Offset: 0x00102AC0
		public void Remove(Rectangle[] rectangles)
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2B;
					case 1:
						return;
					case 2:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this[num].Remove(rectangles);
							num++;
							num2 = 0;
							continue;
						}
						break;
					case 3:
						goto IL_2B;
					}
					break;
					IL_2B:
					num2 = 2;
				}
			}
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00103B5C File Offset: 0x00102B5C
		public void MarkUsedReferences(bool[] usedItems)
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					List<XlsDataValidationCollection> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_48;
							default:
							{
								if (false)
								{
								}
								XlsDataValidationCollection xlsDataValidationCollection = innerList[num];
								xlsDataValidationCollection.MarkUsedReferences(usedItems);
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 1:
							return;
						case 2:
							goto IL_48;
						case 3:
							goto IL_4A;
						}
						break;
						IL_4A:
						num2 = 0;
						continue;
						IL_48:
						goto IL_4A;
					}
				}
				return;
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00103C14 File Offset: 0x00102C14
		public void UpdateReferenceIndexes(int[] arrUpdatedIndexes)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<XlsDataValidationCollection> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_40;
							default:
							{
								if (false)
								{
								}
								XlsDataValidationCollection xlsDataValidationCollection = innerList[num];
								xlsDataValidationCollection.UpdateReferenceIndexes(arrUpdatedIndexes);
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						case 1:
							return;
						case 2:
							goto IL_4A;
						case 3:
							goto IL_40;
						}
						break;
						IL_4A:
						num2 = 0;
						continue;
						IL_40:
						if (true)
						{
						}
						goto IL_4A;
					}
				}
				return;
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00103CCC File Offset: 0x00102CCC
		internal new void ᜀ(XlsDataValidationTable A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6, bool A_7)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_0 == this)
						{
							num = 2;
							continue;
						}
						Rectangle rectangle = new Rectangle(A_2 - 1, A_1 - 1, A_6 - 1, A_5 - 1);
						Rectangle[] rectangles = new Rectangle[]
						{
							rectangle
						};
						Rectangle rectangle2 = new Rectangle(A_4 - 1, A_3 - 1, A_6 - 1, A_5 - 1);
						Rectangle[] rectangles2 = new Rectangle[]
						{
							rectangle2
						};
						A_0.Remove(rectangles2);
						Dictionary<spr\u22CB, XlsDataValidationCollection>.ValueCollection.Enumerator enumerator = this.ᜁ.Values.GetEnumerator();
						num = 3;
						continue;
					}
					case 1:
						goto IL_5E;
					case 2:
						goto IL_1A2;
					case 3:
						goto IL_229;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				IL_5E:
				throw new ArgumentNullException(RecordTableEnumerator.b("⍆ⱈ㡊㥌୎ぐ❒㑔Ŗ㡘㝚㑜㭞`ᝢ౤ࡦݨ", a_));
				IL_137:
				XlsDataValidationTable xlsDataValidationTable = new XlsDataValidationTable(base.AppImplementation, base.Parent);
				this.ᜀ(xlsDataValidationTable, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				xlsDataValidationTable.ᜀ(this, A_3, A_4, A_3, A_4, A_5, A_6, A_7);
				return;
				IL_1A2:
				goto IL_137;
				IL_229:
				try
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (A_7)
							{
								num = 2;
								continue;
							}
							break;
						case 2:
						{
							Rectangle[] rectangles;
							XlsDataValidationCollection xlsDataValidationCollection;
							xlsDataValidationCollection.Remove(rectangles);
							num = 4;
							continue;
						}
						case 3:
							goto IL_124;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
							{
								if (false)
								{
								}
								Dictionary<spr\u22CB, XlsDataValidationCollection>.ValueCollection.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 6;
									continue;
								}
								XlsDataValidationCollection xlsDataValidationCollection = enumerator.Current;
								A_0.ᜀ(xlsDataValidationCollection, A_1, A_2, A_3, A_4, A_5, A_6);
								num = 0;
								continue;
							}
							}
							break;
						case 6:
							num = 3;
							continue;
						}
						IL_93:
						num = 5;
						continue;
						goto IL_93;
					}
					IL_124:
					return;
				}
				finally
				{
					Dictionary<spr\u22CB, XlsDataValidationCollection>.ValueCollection.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_137;
			}
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00103F18 File Offset: 0x00102F18
		private new void ᜀ(XlsDataValidationCollection A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6)
		{
			for (;;)
			{
				spr\u22CB spr_u22CB = (spr\u22CB)A_0.Record.Clone();
				bool flag = false;
				int num = 5;
				for (;;)
				{
					XlsDataValidationCollection xlsDataValidationCollection;
					switch (num)
					{
					case 0:
						if (xlsDataValidationCollection.Count > 0)
						{
							goto IL_A2;
						}
						return;
					case 1:
						return;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_AF;
					case 4:
						xlsDataValidationCollection = new XlsDataValidationCollection(base.AppImplementation, this, spr_u22CB);
						flag = true;
						num = 3;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A2;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (!this.ᜁ.TryGetValue(spr_u22CB, out xlsDataValidationCollection))
							{
								num = 4;
								continue;
							}
							goto IL_AF;
						}
						break;
					case 6:
						if (flag)
						{
							num = 2;
							continue;
						}
						return;
					case 7:
						this.Add(xlsDataValidationCollection);
						num = 1;
						continue;
					}
					break;
					IL_A2:
					num = 7;
					continue;
					IL_AF:
					xlsDataValidationCollection.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
					num = 6;
				}
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00104034 File Offset: 0x00103034
		protected override void OnClearComplete()
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
			this.ᜁ.Clear();
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0010407C File Offset: 0x0010307C
		public XlsWorksheet Worksheet
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x001040C0 File Offset: 0x001030C0
		public XlsWorkbook Workbook
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
				return this.ᜀ.ParentWorkbook;
			}
		}

		// Token: 0x17000B52 RID: 2898
		public new XlsDataValidationCollection this[int index]
		{
			get
			{
				int a_ = 13;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_84;
					case 2:
						if (index > base.Count)
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
							goto IL_9C;
						}
						break;
					case 3:
						num = 2;
						continue;
					}
					IL_33:
					if (index >= 0)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
					goto IL_33;
				}
				IL_49:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩂⭄⍆ⱈ㍊", a_), RecordTableEnumerator.b("ᕂ⑄⭆㱈⹊浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠རdᑦᩨ䭪ᥬݮၰᵲ啴䝶奸ᑺོ彾ﶈﾌ꾎ﮒ練릘쾠힢认", a_));
				IL_84:
				goto IL_49;
				IL_9C:
				if (false)
				{
				}
				return base.List[index];
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x001041C4 File Offset: 0x001031C4
		public int ShapesCount
		{
			get
			{
				int num;
				for (;;)
				{
					num = 0;
					int num2 = 0;
					int count = base.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					default:
					{
						if (false)
						{
						}
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_53;
							case 1:
								goto IL_67;
							case 2:
								goto IL_53;
							case 3:
								if (num2 >= count)
								{
									num3 = 1;
									continue;
								}
								num += this[num2].ShapesCount;
								num2++;
								num3 = 2;
								continue;
							}
							break;
							IL_53:
							num3 = 3;
						}
						break;
					}
					}
				}
				IL_67:
				IL_89:
				if (true)
				{
				}
				return num;
			}
		}

		// Token: 0x040010C2 RID: 4290
		private bool \u2460\u00A9\u0087\u0099;

		// Token: 0x040010C3 RID: 4291
		private new XlsWorksheet ᜀ;

		// Token: 0x040010C4 RID: 4292
		private new Dictionary<spr\u22CB, XlsDataValidationCollection> ᜁ = new Dictionary<spr\u22CB, XlsDataValidationCollection>();
	}
}
