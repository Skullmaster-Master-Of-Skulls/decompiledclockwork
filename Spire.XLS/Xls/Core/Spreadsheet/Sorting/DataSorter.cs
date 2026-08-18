using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Sorting
{
	// Token: 0x02000177 RID: 375
	public class DataSorter : IDataSort
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000AE6F8 File Offset: 0x000AD6F8
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x000AE73C File Offset: 0x000AD73C
		private ISortedRule CustomAlgorithm
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x000AE780 File Offset: 0x000AD780
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x000AE7C4 File Offset: 0x000AD7C4
		public bool IsCaseSensitive
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
				this.ᜀ = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x000AE808 File Offset: 0x000AD808
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x000AE84C File Offset: 0x000AD84C
		public bool IsIncludeTitle
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

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000AE890 File Offset: 0x000AD890
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x000AE8D4 File Offset: 0x000AD8D4
		public SortOrientationType Orientation
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x000AE918 File Offset: 0x000AD918
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x000AE95C File Offset: 0x000AD95C
		public SortColumns SortColumns
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x000AE9A0 File Offset: 0x000AD9A0
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x000AE9E4 File Offset: 0x000AD9E4
		internal CellRange SortRange
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
				return this.ᜄ;
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000AEA28 File Offset: 0x000ADA28
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x000AEA6C File Offset: 0x000ADA6C
		internal SortedWayType SortedWay
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000AEAB0 File Offset: 0x000ADAB0
		internal DataSorter(IWorkbook A_0)
		{
			this.ᜆ = A_0;
			this.ᜃ = new SortColumns((A_0 as XlsWorkbook).AppImplementation, A_0);
			this.IsCaseSensitive = false;
			this.IsIncludeTitle = true;
			this.Orientation = SortOrientationType.TopToBottom;
			this.SortedWay = SortedWayType.InsertionSort;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000AEB00 File Offset: 0x000ADB00
		public void Sort(CellRange range)
		{
			int a_ = 7;
			int[] array;
			OrderBy[] array2;
			Color[] array3;
			for (;;)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.SortRange = range;
						int num = 0;
						for (;;)
						{
							IEnumerator<SortColumn> enumerator;
							int num2;
							switch (num)
							{
							case 0:
								if (this.SortRange == null)
								{
									num = 1;
									continue;
								}
								goto IL_1A6;
							case 1:
								goto IL_5A;
							case 2:
								try
								{
									num = 1;
									for (;;)
									{
										ISortColumn sortColumn;
										switch (num)
										{
										case 0:
											goto IL_CA;
										case 2:
											if (sortColumn.ComparsionType == SortComparsionType.FontColor)
											{
												num = 3;
												continue;
											}
											goto IL_CA;
										case 3:
											goto IL_110;
										case 4:
											num = 2;
											continue;
										case 5:
											num = 7;
											continue;
										case 6:
											if (sortColumn.ComparsionType != SortComparsionType.BackgroundColor)
											{
												num = 4;
												continue;
											}
											goto IL_110;
										case 7:
											goto IL_161;
										case 9:
											if (!enumerator.MoveNext())
											{
												num = 5;
												continue;
											}
											sortColumn = enumerator.Current;
											num = 6;
											continue;
										}
										goto IL_A1;
										IL_CA:
										array[num2] = sortColumn.Key;
										array2[num2] = sortColumn.Order;
										num2++;
										num = 8;
										continue;
										IL_ED:
										num = 9;
										continue;
										IL_A1:
										goto IL_ED;
										IL_110:
										array3[num2] = sortColumn.Color;
										num = 0;
									}
									IL_161:
									goto IL_219;
								}
								finally
								{
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											enumerator.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_1A3;
										}
										if (enumerator == null)
										{
											break;
										}
										num = 0;
									}
									IL_1A3:;
								}
								goto IL_1A6;
							}
							break;
							IL_1A6:
							array = new int[this.ᜃ.Count];
							array2 = new OrderBy[array.Length];
							array3 = new Color[array.Length];
							num2 = 0;
							enumerator = this.ᜃ.GetEnumerator();
							num = 2;
						}
					}
					IL_5A:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_1FF;
					}
					break;
				}
			}
			IL_1FF:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("渼倾㍀㝂敄ᕆ⡈╊⩌⩎", a_));
			IL_219:
			this.SortBy(array, array2, array3);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x000AED40 File Offset: 0x000ADD40
		public void SortBy(int[] iColumns, OrderBy[] orderBy, Color[] colors)
		{
			switch (0)
			{
			default:
			{
				spr\u2374 spr_u;
				for (;;)
				{
					Type[] array = new Type[iColumns.Length];
					object[][] array2 = null;
					if (true)
					{
					}
					int num = 16;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_3D0;
						case 1:
							goto IL_13F;
						case 2:
							if (this.ᜃ[0].ComparsionType == SortComparsionType.Values)
							{
								num = 25;
								continue;
							}
							spr_u = new spr\u177B(array2, array, orderBy, colors);
							spr_u.ᜄ(0, spr_u.ᜁ().Length - 1, 1);
							num2 = 1;
							num = 27;
							continue;
						case 3:
							spr_u = new spr\u1C36(spr_u.ᜁ(), array, orderBy, colors);
							spr_u.ᜄ(0, spr_u.ᜁ().Length - 1, 1);
							num = 7;
							continue;
						case 4:
							for (;;)
							{
								SortedWayType sortedWayType;
								switch (sortedWayType)
								{
								case SortedWayType.QuickSort:
									goto IL_24A;
								case SortedWayType.HeapSort:
									goto IL_D9;
								case SortedWayType.InsertionSort:
									goto IL_359;
								default:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										goto IL_2D5;
									}
									break;
								}
							}
							IL_D9:
							spr_u = new sprṪ(array2, array, orderBy, colors);
							spr_u.ᜄ(0, spr_u.ᜁ().Length, 1);
							num = 22;
							continue;
							IL_24A:
							spr_u = new spr\u25D5(array2, array, orderBy, colors);
							spr_u.ᜄ(0, array2.Length - 1, 1);
							num = 18;
							continue;
							IL_2D5:
							if (false)
							{
							}
							num = 17;
							continue;
							IL_359:
							spr_u = new sprℍ(array2, array, orderBy, colors);
							spr_u.ᜄ(0, array2.Length - 1, 1);
							num = 13;
							continue;
						case 5:
							if (this.ᜂ == SortOrientationType.TopToBottom)
							{
								num = 28;
								continue;
							}
							goto IL_47D;
						case 6:
							goto IL_1EF;
						case 7:
							goto IL_3A8;
						case 8:
							if (num2 >= this.ᜃ.Count)
							{
								num = 15;
								continue;
							}
							num = 23;
							continue;
						case 9:
							if (this.ᜂ == SortOrientationType.TopToBottom)
							{
								num = 12;
								continue;
							}
							goto IL_217;
						case 10:
							spr_u.ᜄ(0, spr_u.ᜁ().Length - 1, num2 + 1);
							num = 6;
							continue;
						case 11:
							spr_u = new spr\u1C36(spr_u.ᜁ(), array, orderBy, colors);
							spr_u.ᜄ(0, spr_u.ᜁ().Length - 1, 1);
							num = 0;
							continue;
						case 12:
							goto IL_3F3;
						case 13:
							if (this.ᜂ == SortOrientationType.TopToBottom)
							{
								num = 26;
								continue;
							}
							goto IL_1AA;
						case 14:
							goto IL_3CB;
						case 15:
							num = 5;
							continue;
						case 16:
							if (this.Orientation == SortOrientationType.TopToBottom)
							{
								num = 20;
								continue;
							}
							array2 = this.ᜁ(this.SortRange, array, iColumns);
							num = 1;
							continue;
						case 17:
							return;
						case 18:
							if (array.Length > 1)
							{
								num = 11;
								continue;
							}
							goto IL_3D0;
						case 19:
							goto IL_13F;
						case 20:
							array2 = this.ᜀ(this.SortRange, array, iColumns);
							num = 19;
							continue;
						case 21:
							goto IL_2FF;
						case 22:
							if (array.Length > 1)
							{
								num = 3;
								continue;
							}
							goto IL_3A8;
						case 23:
							if (this.ᜃ[num2].ComparsionType != SortComparsionType.Values)
							{
								num = 10;
								continue;
							}
							goto IL_1EF;
						case 24:
							if (this.ᜂ == SortOrientationType.TopToBottom)
							{
								num = 14;
								continue;
							}
							goto IL_C6;
						case 25:
						{
							SortOrientationType sortOrientationType = this.ᜂ;
							SortedWayType sortedWayType = this.SortedWay;
							num = 4;
							continue;
						}
						case 26:
							goto IL_393;
						case 27:
							goto IL_2FF;
						case 28:
							goto IL_1A8;
						}
						break;
						IL_13F:
						num = 2;
						continue;
						IL_1EF:
						num2++;
						num = 21;
						continue;
						IL_2FF:
						num = 8;
						continue;
						IL_3A8:
						num = 24;
						continue;
						IL_3D0:
						num = 9;
					}
				}
				IL_C6:
				this.ᜁ(this.SortRange, spr_u.ᜁ());
				return;
				IL_1A8:
				this.ᜀ(this.SortRange, spr_u.ᜁ());
				return;
				IL_1AA:
				this.ᜁ(this.SortRange, spr_u.ᜁ());
				return;
				IL_217:
				this.ᜁ(this.SortRange, spr_u.ᜁ());
				return;
				IL_393:
				this.ᜀ(this.SortRange, spr_u.ᜁ());
				return;
				IL_3CB:
				this.ᜀ(this.SortRange, spr_u.ᜁ());
				return;
				IL_3F3:
				this.ᜀ(this.SortRange, spr_u.ᜁ());
				return;
				IL_47D:
				this.ᜁ(this.SortRange, spr_u.ᜁ());
				return;
			}
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000AF1DC File Offset: 0x000AE1DC
		internal object[][] ᜀ(IXLSRange A_0, Type[] A_1, int[] A_2)
		{
			switch (0)
			{
			default:
			{
				object[][] array;
				for (;;)
				{
					for (;;)
					{
						int num = A_0.Row;
						int num2 = 13;
						for (;;)
						{
							int num3;
							int num5;
							int[] array2;
							int num9;
							int lastRow;
							int num10;
							switch (num2)
							{
							case 0:
								return array;
							case 1:
								goto IL_2C7;
							case 2:
								goto IL_2C7;
							case 3:
								goto IL_13E;
							case 4:
							{
								if (num3 >= A_2.Length)
								{
									num2 = 5;
									continue;
								}
								int num4 = A_2[num3];
								int num6;
								A_1[num5] = this.ᜀ(A_0[num, num4 + 1], out num6);
								array2[num5++] = num6;
								num3++;
								num2 = 7;
								continue;
							}
							case 5:
							{
								if (true)
								{
								}
								SortColumns sortColumns = this.ᜃ;
								int num7 = num;
								int num8 = 0;
								num2 = 17;
								continue;
							}
							case 6:
							{
								int num7;
								num7++;
								num9++;
								int num8;
								num8++;
								num2 = 16;
								continue;
							}
							case 7:
								goto IL_27F;
							case 8:
							{
								int num7;
								if (num7 > lastRow)
								{
									num2 = 0;
									continue;
								}
								num5 = 1;
								array[num9] = new object[A_2.Length + 1];
								int num8;
								array[num9][0] = num8;
								num10 = 0;
								num2 = 3;
								continue;
							}
							case 9:
								goto IL_2C7;
							case 10:
								goto IL_27F;
							case 11:
								goto IL_13E;
							case 12:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num2 = 2;
									continue;
								}
								break;
							case 13:
								if (this.IsIncludeTitle)
								{
									num2 = 15;
									continue;
								}
								goto IL_166;
							case 14:
							{
								SortComparsionType comparsionType;
								switch (comparsionType)
								{
								case SortComparsionType.Values:
								{
									int num7;
									int num11;
									array[num9][num5] = this.ᜀ(A_0[num7, num11 + 1], array2[num5 - 1]);
									num2 = 1;
									continue;
								}
								case SortComparsionType.BackgroundColor:
								{
									int num7;
									int num11;
									array[num9][num5] = A_0[num7, num11 + 1].Style.Color;
									num2 = 9;
									continue;
								}
								case SortComparsionType.FontColor:
								{
									int num7;
									int num11;
									array[num9][num5] = A_0[num7, num11 + 1].Style.Font.Color;
									num2 = 20;
									continue;
								}
								default:
									num2 = 12;
									continue;
								}
								break;
							}
							case 15:
								num = A_0.Row + 1;
								num2 = 19;
								continue;
							case 16:
								goto IL_1E2;
							case 17:
								goto IL_1E2;
							case 18:
							{
								if (num10 >= A_2.Length)
								{
									num2 = 6;
									continue;
								}
								int num11 = A_2[num10];
								SortColumns sortColumns;
								SortComparsionType comparsionType = this.ᜃ[sortColumns.ᜀ(num11)].ComparsionType;
								num2 = 14;
								continue;
							}
							case 19:
								goto IL_166;
							case 20:
								goto IL_2C7;
							}
							break;
							IL_13E:
							num2 = 18;
							continue;
							IL_166:
							lastRow = A_0.LastRow;
							int column = A_0.Column;
							int lastColumn = A_0.LastColumn;
							int num12 = lastRow - num + 1;
							array = new object[num12][];
							num9 = 0;
							num5 = 0;
							array2 = new int[A_1.Length];
							num3 = 0;
							num2 = 10;
							continue;
							IL_1E2:
							num2 = 8;
							continue;
							IL_27F:
							num2 = 4;
							continue;
							IL_2C7:
							num5++;
							num10++;
							num2 = 11;
						}
					}
				}
				return array;
			}
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000AF558 File Offset: 0x000AE558
		internal object[][] ᜁ(IXLSRange A_0, Type[] A_1, int[] A_2)
		{
			switch (0)
			{
			default:
			{
				int num2;
				int num4;
				int num5;
				int[] array;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_2B4:
					int num;
					if (num >= A_2.Length)
					{
						num2 = 17;
					}
					else
					{
						int num3 = A_2[num];
						int num6;
						A_1[num4] = this.ᜀ(A_0[num3 + 1, num5], out num6);
						array[num4++] = num6;
						num++;
						num2 = 3;
					}
					break;
				}
				default:
					if (false)
					{
					}
					goto IL_87;
				}
				object[][] array2;
				for (;;)
				{
					IL_2C:
					int num7;
					int num8;
					int num9;
					switch (num2)
					{
					case 0:
						goto IL_21E;
					case 1:
						num2 = 15;
						continue;
					case 2:
						goto IL_179;
					case 3:
						goto IL_2A8;
					case 4:
						goto IL_2E4;
					case 5:
					{
						if (true)
						{
						}
						if (num7 >= num8)
						{
							num2 = 7;
							continue;
						}
						array2[num7] = new object[num9 + 1];
						int num10;
						int num11;
						array2[num7][num10] = num11++;
						SortComparsionType comparsionType = this.ᜃ[num10].ComparsionType;
						num2 = 16;
						continue;
					}
					case 6:
						return array2;
					case 7:
					{
						int num10;
						num10++;
						num2 = 13;
						continue;
					}
					case 8:
						goto IL_2B4;
					case 9:
					{
						int num10;
						if (num10 >= num9)
						{
							num2 = 6;
							continue;
						}
						int num12 = num5;
						int num13 = A_2[num10];
						num7 = 0;
						num2 = 2;
						continue;
					}
					case 10:
						goto IL_2A8;
					case 11:
						goto IL_1A6;
					case 12:
						goto IL_179;
					case 13:
						goto IL_21E;
					case 14:
						goto IL_2E4;
					case 15:
						goto IL_2E4;
					case 16:
					{
						SortComparsionType comparsionType;
						switch (comparsionType)
						{
						case SortComparsionType.Values:
						{
							int num10;
							int num12;
							int num13;
							array2[num7][num10 + 1] = this.ᜀ(A_0[num13 + 1, num12++], array[num10]);
							num2 = 4;
							continue;
						}
						case SortComparsionType.BackgroundColor:
						{
							int num10;
							int num12;
							int num13;
							array2[num7][num10 + 1] = A_0[num13 + 1, num12++].Style.Color;
							num2 = 14;
							continue;
						}
						case SortComparsionType.FontColor:
						{
							int num10;
							int num12;
							int num13;
							array2[num7][num10 + 1] = A_0[num13 + 1, num12++].Style.Font.Color;
							num2 = 20;
							continue;
						}
						default:
							num2 = 1;
							continue;
						}
						break;
					}
					case 17:
					{
						int num11 = 0;
						int num13 = 0;
						int num10 = 0;
						num2 = 0;
						continue;
					}
					case 18:
						num5++;
						num2 = 11;
						continue;
					case 19:
						if (this.IsIncludeTitle)
						{
							num2 = 18;
							continue;
						}
						goto IL_1A6;
					case 20:
						goto IL_2E4;
					}
					goto IL_87;
					IL_179:
					num2 = 5;
					continue;
					IL_1A6:
					int lastColumn = A_0.LastColumn;
					num8 = lastColumn - num5 + 1;
					num9 = A_2.Length;
					array2 = new object[num8][];
					num4 = 0;
					array = new int[A_1.Length];
					int num = 0;
					num2 = 10;
					continue;
					IL_21E:
					num2 = 9;
					continue;
					IL_2A8:
					num2 = 8;
					continue;
					IL_2E4:
					num4++;
					num7++;
					num2 = 12;
				}
				return array2;
				IL_87:
				int row = A_0.Row;
				int lastRow = A_0.LastRow;
				num5 = A_0.Column;
				num2 = 19;
				goto IL_2C;
			}
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000AF8D0 File Offset: 0x000AE8D0
		internal object ᜀ(IXLSRange A_0, int A_1)
		{
			for (;;)
			{
				IL_14:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_80;
					case 1:
						switch (A_1)
						{
						case 1:
							goto IL_5B;
						case 2:
							goto IL_AA;
						case 3:
							goto IL_54;
						case 4:
							goto IL_48;
						case 5:
							goto IL_9E;
						case 6:
							goto IL_67;
						default:
							num = 2;
							continue;
						}
						break;
					case 2:
						num = 0;
						continue;
					}
					goto IL_14;
				}
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_96;
				}
			}
			IL_48:
			return A_0.NumberValue;
			IL_54:
			return A_0.FormulaStringValue;
			IL_5B:
			return A_0.FormulaDateTime;
			IL_67:
			return A_0.NumberText;
			IL_96:
			if (false)
			{
			}
			if (true)
			{
			}
			return A_0.Value;
			IL_9E:
			return A_0.DateTimeValue;
			IL_AA:
			return A_0.FormulaNumberValue;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000AF9A4 File Offset: 0x000AE9A4
		internal void ᜀ(IXLSRange A_0, object[][] A_1)
		{
			for (;;)
			{
				int a_ = 0;
				int num = 2;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_109;
						default:
							if (false)
							{
							}
							if (num2 >= A_1.Length)
							{
								num = 6;
								continue;
							}
							num = 8;
							continue;
						}
						break;
					case 1:
						goto IL_105;
					case 2:
						if (true)
						{
						}
						if ((int)A_1[0][0] != -1)
						{
							num = 10;
							continue;
						}
						goto IL_9C;
					case 3:
						if ((int)A_1[num2][0] != -1)
						{
							num = 4;
							continue;
						}
						goto IL_105;
					case 4:
						this.ᜁ(A_0, A_1, num2);
						num = 1;
						continue;
					case 5:
						goto IL_B5;
					case 6:
						return;
					case 7:
						num = 3;
						continue;
					case 8:
						if (A_1[num2] != null)
						{
							num = 7;
							continue;
						}
						goto IL_105;
					case 9:
						goto IL_9C;
					case 10:
						this.ᜁ(A_0, A_1, a_);
						num = 9;
						continue;
					case 11:
						goto IL_B5;
					}
					break;
					IL_9C:
					num2 = 1;
					num = 11;
					continue;
					IL_B5:
					num = 0;
					continue;
					IL_109:
					num = 5;
					continue;
					IL_105:
					num2++;
					goto IL_109;
				}
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000AFAE8 File Offset: 0x000AEAE8
		internal void ᜁ(IXLSRange A_0, object[][] A_1)
		{
			for (;;)
			{
				int a_ = 0;
				int num = 1;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_C5;
					case 1:
						if ((int)A_1[0][0] != -1)
						{
							num = 6;
							continue;
						}
						goto IL_E3;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							if (false)
							{
							}
							this.ᜀ(A_0, A_1, num2);
							num = 9;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						goto IL_E3;
					case 5:
						goto IL_C5;
					case 6:
						this.ᜀ(A_0, A_1, a_);
						num = 4;
						continue;
					case 7:
						if ((int)A_1[num2][0] != -1)
						{
							num = 3;
							continue;
						}
						goto IL_54;
					case 8:
						if (num2 >= A_1.Length)
						{
							goto IL_D6;
						}
						num = 7;
						continue;
					case 9:
						goto IL_54;
					}
					break;
					IL_54:
					num2++;
					num = 0;
					continue;
					IL_C5:
					num = 8;
					continue;
					IL_D6:
					num = 2;
					continue;
					IL_E3:
					num2 = 0;
					num = 5;
				}
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000AFC00 File Offset: 0x000AEC00
		internal void ᜀ(IXLSRange A_0, object[][] A_1, int A_2)
		{
			switch (0)
			{
			default:
			{
				int row;
				int lastRow;
				int num;
				XlsWorksheet xlsWorksheet;
				int num4;
				int num5;
				XlsRange xlsRange;
				for (;;)
				{
					row = A_0.Row;
					lastRow = A_0.LastRow;
					num = A_0.Column;
					int num2 = 0;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							if (this.IsIncludeTitle)
							{
								num2 = 5;
								continue;
							}
							goto IL_162;
						case 1:
							goto IL_9C;
						case 2:
							goto IL_162;
						case 3:
							if (num3 != -1)
							{
								num2 = 6;
								continue;
							}
							goto IL_1CD;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B5;
							default:
								if (false)
								{
								}
								num2 = 3;
								continue;
							}
							break;
						case 5:
							num++;
							num2 = 2;
							continue;
						case 6:
							(xlsWorksheet[row, num + num3, lastRow, num + num3] as XlsRange).ᜁ(xlsWorksheet[row, num + num4, lastRow, num + num4], CopyRangeOptions.All);
							A_1[num4][0] = -1;
							num4 = num3;
							num2 = 7;
							continue;
						case 7:
							if (num3 == A_2)
							{
								if (true)
								{
								}
								num2 = 8;
								continue;
							}
							goto IL_9C;
						case 8:
							goto IL_160;
						case 9:
							goto IL_B5;
						}
						break;
						IL_9C:
						num3 = (int)A_1[num4][0];
						num2 = 9;
						continue;
						IL_B5:
						if (num3 != A_2)
						{
							num2 = 4;
							continue;
						}
						goto IL_1CD;
						IL_162:
						int lastColumn = A_0.LastColumn;
						xlsWorksheet = (A_0.Worksheet as XlsWorksheet);
						num5 = lastColumn + 1;
						xlsWorksheet.InsertColumn(num5);
						xlsRange = (xlsWorksheet[row, num5, lastRow, num5] as XlsRange);
						(xlsWorksheet[row, num + A_2, lastRow, num + A_2] as XlsRange).ᜁ(xlsRange, CopyRangeOptions.All);
						num3 = 0;
						num4 = A_2;
						num2 = 1;
					}
				}
				IL_160:
				IL_1CD:
				xlsRange.CopyTo(xlsWorksheet[row, num + num4, lastRow, num + num4]);
				xlsWorksheet.DeleteColumn(num5);
				A_1[num4][0] = -1;
				return;
			}
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x000AFE08 File Offset: 0x000AEE08
		internal void ᜁ(IXLSRange A_0, object[][] A_1, int A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = A_0.Row;
					int num2 = 12;
					for (;;)
					{
						int num4;
						int num3;
						XlsWorksheet xlsWorksheet;
						int column;
						int lastColumn;
						switch (num2)
						{
						case 0:
							num3 = (int)A_1[num4][0];
							num2 = 3;
							continue;
						case 1:
							num = A_0.Row + 1;
							num2 = 7;
							continue;
						case 2:
							A_1[num4][0] = -1;
							num2 = 8;
							continue;
						case 3:
							if (num3 != A_2)
							{
								num2 = 5;
								continue;
							}
							goto IL_1E9;
						case 4:
							goto IL_1E9;
						case 5:
							num2 = 9;
							continue;
						case 6:
							if (A_1[num4] != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_1E9;
						case 7:
							goto IL_117;
						case 8:
							return;
						case 9:
							if (num3 != -1)
							{
								num2 = 10;
								continue;
							}
							goto IL_1E9;
						case 10:
							(xlsWorksheet[num3 + num, column, num3 + num, lastColumn] as XlsRange).ᜁ(xlsWorksheet[num4 + num, column, num4 + num, lastColumn], CopyRangeOptions.All);
							A_1[num4][0] = -1;
							num4 = num3;
							num2 = 14;
							continue;
						case 11:
							if (A_1[num4] != null)
							{
								num2 = 2;
								continue;
							}
							return;
						case 12:
							if (this.IsIncludeTitle)
							{
								num2 = 1;
								continue;
							}
							goto IL_117;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								goto IL_261;
							}
							break;
						case 14:
							if (num3 == A_2)
							{
								num2 = 4;
								continue;
							}
							goto IL_261;
						}
						break;
						IL_117:
						int lastRow = A_0.LastRow;
						column = A_0.Column;
						lastColumn = A_0.LastColumn;
						xlsWorksheet = (A_0.Worksheet as XlsWorksheet);
						int num5 = A_0.LastRow + 1;
						XlsRange xlsRange = xlsWorksheet[num5, column, num5, lastColumn] as XlsRange;
						xlsWorksheet.InsertRow(num5);
						(xlsWorksheet[num + A_2, column, num + A_2, lastColumn] as XlsRange).ᜁ(xlsRange, CopyRangeOptions.All);
						num4 = A_2;
						num3 = num4;
						num2 = 13;
						continue;
						IL_1E9:
						xlsRange.ᜁ(xlsWorksheet[num4 + num, column, num4 + num, lastColumn], CopyRangeOptions.All);
						xlsWorksheet.DeleteRow(xlsRange.Row);
						num2 = 11;
						continue;
						IL_261:
						num2 = 6;
					}
				}
				return;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000B009C File Offset: 0x000AF09C
		internal Type ᜀ(IXLSRange A_0, out int A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.HasNumber)
					{
						num = 12;
						continue;
					}
					num = 1;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_19D;
					default:
						if (false)
						{
						}
						if (A_0.HasString)
						{
							num = 9;
							continue;
						}
						num = 13;
						continue;
					}
					break;
				case 3:
					if (A_0.HasFormulaDateTime)
					{
						num = 6;
						continue;
					}
					num = 7;
					continue;
				case 4:
					goto IL_107;
				case 5:
					num = 3;
					continue;
				case 6:
					goto IL_17F;
				case 7:
					if (A_0.HasFormulaStringValue)
					{
						num = 11;
						continue;
					}
					num = 10;
					continue;
				case 8:
					goto IL_15C;
				case 9:
					goto IL_94;
				case 10:
					if (A_0.HasFormulaNumberValue)
					{
						if (true)
						{
						}
						num = 8;
						continue;
					}
					goto IL_99;
				case 11:
					goto IL_E1;
				case 12:
					goto IL_B7;
				case 13:
					if (A_0.HasDateTime)
					{
						num = 4;
						continue;
					}
					goto IL_1AB;
				}
				if (A_0.HasFormula)
				{
					num = 5;
					continue;
				}
				IL_99:
				num = 0;
			}
			IL_94:
			A_1 = 6;
			return typeof(string);
			IL_B7:
			A_1 = 4;
			return typeof(double);
			IL_E1:
			goto IL_19D;
			IL_107:
			A_1 = 5;
			return typeof(DateTime);
			IL_15C:
			A_1 = 2;
			return typeof(double);
			IL_17F:
			A_1 = 1;
			return typeof(DateTime);
			IL_19D:
			A_1 = 3;
			return typeof(string);
			IL_1AB:
			A_1 = 6;
			return typeof(string);
		}

		// Token: 0x04000E3C RID: 3644
		private bool \u2609\u0083\u0098\u0080;

		// Token: 0x04000E3D RID: 3645
		private bool ᜀ;

		// Token: 0x04000E3E RID: 3646
		private bool ᜁ;

		// Token: 0x04000E3F RID: 3647
		private SortOrientationType ᜂ;

		// Token: 0x04000E40 RID: 3648
		private SortColumns ᜃ;

		// Token: 0x04000E41 RID: 3649
		private CellRange ᜄ;

		// Token: 0x04000E42 RID: 3650
		private long \u25D9\u00A0\u00AD\u00AD;

		// Token: 0x04000E43 RID: 3651
		private SortedWayType ᜅ;

		// Token: 0x04000E44 RID: 3652
		private string \u25D9\u00AF\u0085\u0096;

		// Token: 0x04000E45 RID: 3653
		private IWorkbook ᜆ;

		// Token: 0x04000E46 RID: 3654
		private ISortedRule ᜇ;
	}
}
