using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records.Formula;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000019 RID: 25
	public class XlsRangesCollection : CollectionExtended<IXLSRange>, IEnumerable<IXLSRange>, IXLSRanges, ICombinedRange, spr\u1A8B
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00008AB4 File Offset: 0x00007AB4
		internal XlsRangesCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜃ();
			this.ᜂ = this.ᜁ.Workbook.MaxRowCount + 1;
			this.ᜃ = this.ᜁ.Workbook.MaxColumnCount + 1;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008B00 File Offset: 0x00007B00
		private void ᜃ()
		{
			int a_ = 5;
			this.ᜁ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.ᜁ == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("氺刼䴾⩀あⵄ≆ⱈ㽊", a_), RecordTableEnumerator.b("欺尼䴾⑀ⵂㅄ杆♈⥊❌⩎㉐❒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008B8C File Offset: 0x00007B8C
		public string RangeAddress
		{
			get
			{
				switch (0)
				{
				default:
				{
					StringBuilder stringBuilder;
					for (;;)
					{
						this.ᜁ();
						stringBuilder = new StringBuilder();
						int count = base.Count;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A3;
							case 1:
								if (count == 0)
								{
									if (true)
									{
									}
									num = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A3;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = base.InnerList[0];
									stringBuilder.Append(ixlsrange.RangeAddress);
									string value = this.ᜀ();
									int num2 = 1;
									int count2 = base.Count;
									num = 5;
									continue;
								}
								}
								break;
							case 2:
								goto IL_69;
							case 3:
								goto IL_122;
							case 4:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 3;
									continue;
								}
								string value;
								stringBuilder.Append(value);
								IXLSRange ixlsrange = base.InnerList[num2];
								stringBuilder.Append(ixlsrange.RangeAddress);
								num2++;
								num = 0;
								continue;
							}
							case 5:
								goto IL_101;
							}
							break;
							IL_101:
							num = 4;
							continue;
							IL_A3:
							goto IL_101;
						}
					}
					IL_69:
					return string.Empty;
					IL_122:
					return stringBuilder.ToString();
				}
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00008CC4 File Offset: 0x00007CC4
		public string RangeAddressLocal
		{
			get
			{
				switch (0)
				{
				default:
				{
					StringBuilder stringBuilder;
					for (;;)
					{
						this.ᜁ();
						stringBuilder = new StringBuilder();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_122;
							case 1:
								goto IL_F9;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 0;
									continue;
								}
								string value;
								stringBuilder.Append(value);
								IXLSRange ixlsrange = base.InnerList[num2];
								stringBuilder.Append(ixlsrange.RangeAddressLocal);
								num2++;
								num = 5;
								continue;
							}
							case 3:
								goto IL_57;
							case 4:
								if (count == 0)
								{
									num = 3;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_9B;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = base.InnerList[0];
									stringBuilder.Append(ixlsrange.RangeAddressLocal);
									string value = this.ᜀ();
									int num2 = 1;
									int count2 = base.Count;
									num = 1;
									continue;
								}
								}
								break;
							case 5:
								goto IL_9B;
							}
							break;
							IL_F9:
							if (true)
							{
							}
							num = 2;
							continue;
							IL_9B:
							goto IL_F9;
						}
					}
					IL_57:
					return string.Empty;
					IL_122:
					return stringBuilder.ToString();
				}
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008DFC File Offset: 0x00007DFC
		public string RangeGlobalAddress
		{
			get
			{
				switch (0)
				{
				default:
				{
					StringBuilder stringBuilder;
					for (;;)
					{
						this.ᜁ();
						stringBuilder = new StringBuilder();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_12C;
							case 1:
								goto IL_103;
							case 2:
								goto IL_9B;
							case 3:
							{
								if (true)
								{
								}
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 0;
									continue;
								}
								string argumentsSeparator;
								stringBuilder.Append(argumentsSeparator);
								IXLSRange ixlsrange = base.InnerList[num2];
								stringBuilder.Append(ixlsrange.RangeGlobalAddress);
								num2++;
								num = 2;
								continue;
							}
							case 4:
								if (count == 0)
								{
									num = 5;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_9B;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = base.InnerList[0];
									stringBuilder.Append(ixlsrange.RangeGlobalAddress);
									string argumentsSeparator = this.ᜁ.Workbook.ArgumentsSeparator;
									int num2 = 1;
									int count2 = base.Count;
									num = 1;
									continue;
								}
								}
								break;
							case 5:
								goto IL_57;
							}
							break;
							IL_103:
							num = 3;
							continue;
							IL_9B:
							goto IL_103;
						}
					}
					IL_57:
					return string.Empty;
					IL_12C:
					return stringBuilder.ToString();
				}
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008F40 File Offset: 0x00007F40
		public string RangeR1C1Address
		{
			get
			{
				switch (0)
				{
				default:
				{
					StringBuilder stringBuilder;
					for (;;)
					{
						this.ᜁ();
						stringBuilder = new StringBuilder();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_101;
							case 1:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								string value;
								stringBuilder.Append(value);
								IXLSRange ixlsrange = base.InnerList[num2];
								stringBuilder.Append(ixlsrange.RangeR1C1Address);
								num2++;
								num = 3;
								continue;
							}
							case 2:
								goto IL_57;
							case 3:
								goto IL_9B;
							case 4:
								if (count == 0)
								{
									num = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_9B;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = base.InnerList[0];
									stringBuilder.Append(ixlsrange.RangeR1C1Address);
									string value = this.ᜀ();
									int num2 = 1;
									int count2 = base.Count;
									if (true)
									{
									}
									num = 0;
									continue;
								}
								}
								break;
							case 5:
								goto IL_122;
							}
							break;
							IL_101:
							num = 1;
							continue;
							IL_9B:
							goto IL_101;
						}
					}
					IL_57:
					return string.Empty;
					IL_122:
					return stringBuilder.ToString();
				}
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009078 File Offset: 0x00008078
		public string RangeR1C1AddressLocal
		{
			get
			{
				switch (0)
				{
				default:
				{
					StringBuilder stringBuilder;
					for (;;)
					{
						this.ᜁ();
						stringBuilder = new StringBuilder();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_69;
							case 1:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								string value;
								stringBuilder.Append(value);
								IXLSRange ixlsrange = base.InnerList[num2];
								stringBuilder.Append(ixlsrange.RangeR1C1AddressLocal);
								num2++;
								num = 3;
								continue;
							}
							case 2:
								goto IL_122;
							case 3:
								goto IL_A3;
							case 4:
								if (count == 0)
								{
									if (true)
									{
									}
									num = 0;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A3;
								default:
								{
									if (false)
									{
									}
									IXLSRange ixlsrange = base.InnerList[0];
									stringBuilder.Append(ixlsrange.RangeR1C1AddressLocal);
									string value = this.ᜀ();
									int num2 = 1;
									int count2 = base.Count;
									num = 5;
									continue;
								}
								}
								break;
							case 5:
								goto IL_101;
							}
							break;
							IL_101:
							num = 1;
							continue;
							IL_A3:
							goto IL_101;
						}
					}
					IL_69:
					return string.Empty;
					IL_122:
					return stringBuilder.ToString();
				}
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000091B0 File Offset: 0x000081B0
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000092D4 File Offset: 0x000082D4
		public bool BooleanValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_59;
							case 1:
								goto IL_A1;
							case 2:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool booleanValue = ixlsrange.BooleanValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 5;
								continue;
							}
							case 3:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 6;
								continue;
							}
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A1;
								default:
									goto IL_102;
								}
								break;
							case 5:
								goto IL_A1;
							case 6:
							{
								IXLSRange ixlsrange;
								bool booleanValue;
								if (booleanValue != ixlsrange.BooleanValue)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 1;
								continue;
							}
							case 7:
							{
								bool booleanValue;
								return booleanValue;
							}
							}
							break;
							IL_A1:
							num = 3;
						}
					}
					IL_59:
					if (true)
					{
					}
					return false;
					IL_102:
					if (false)
					{
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_31;
						case 1:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.BooleanValue = value;
							num++;
							num2 = 0;
							continue;
						}
						case 2:
							goto IL_31;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_31;
							default:
								goto IL_91;
							}
							break;
						}
						break;
						IL_31:
						num2 = 1;
					}
				}
				IL_91:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00009378 File Offset: 0x00008378
		public IBorders Borders
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
				this.ᜁ();
				return this.Style.Borders;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000093C4 File Offset: 0x000083C4
		public CellRange[] Cells
		{
			get
			{
				switch (0)
				{
				default:
				{
					List<CellRange> list;
					for (;;)
					{
						this.ᜁ();
						list = new List<CellRange>();
						int num = 0;
						int count = base.Count;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_51;
							case 1:
								goto IL_4F;
							case 2:
								goto IL_83;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_4F;
								default:
								{
									if (false)
									{
									}
									if (num >= count)
									{
										num2 = 2;
										continue;
									}
									if (true)
									{
									}
									CellRange cellRange = (CellRange)base.InnerList[num];
									list.AddRange(cellRange.Cells);
									num++;
									num2 = 0;
									continue;
								}
								}
								break;
							}
							break;
							IL_51:
							num2 = 3;
							continue;
							IL_4F:
							goto IL_51;
						}
					}
					IL_83:
					return list.ToArray();
				}
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00009494 File Offset: 0x00008494
		public int Column
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
				this.ᜁ();
				return this.ᜃ;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000094DC File Offset: 0x000084DC
		public int ColumnGroupLevel
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int columnGroupLevel;
								IXLSRange ixlsrange;
								if (columnGroupLevel != ixlsrange.ColumnGroupLevel)
								{
									num = 3;
									continue;
								}
								int num2;
								num2++;
								if (true)
								{
								}
								num = 2;
								continue;
							}
							case 1:
							{
								int columnGroupLevel;
								return columnGroupLevel;
							}
							case 2:
								goto IL_A5;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A5;
								default:
									goto IL_106;
								}
								break;
							case 4:
							{
								if (count == 0)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								int columnGroupLevel = ixlsrange.ColumnGroupLevel;
								int num2 = 0;
								int count2 = base.Count;
								num = 6;
								continue;
							}
							case 5:
								return int.MinValue;
							case 6:
								goto IL_A5;
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							}
							break;
							IL_A5:
							num = 7;
						}
					}
					return int.MinValue;
					IL_106:
					if (false)
					{
					}
					return int.MinValue;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00009608 File Offset: 0x00008608
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000973C File Offset: 0x0000873C
		public double ColumnWidth
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (count == 0)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								double columnWidth = ixlsrange.ColumnWidth;
								int num2 = 0;
								int count2 = base.Count;
								num = 7;
								continue;
							}
							case 1:
							{
								double columnWidth;
								return columnWidth;
							}
							case 2:
								goto IL_59;
							case 3:
								goto IL_A9;
							case 4:
							{
								IXLSRange ixlsrange;
								double columnWidth;
								if (columnWidth != ixlsrange.ColumnWidth)
								{
									num = 6;
									continue;
								}
								int num2;
								num2++;
								num = 3;
								continue;
							}
							case 5:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 4;
								continue;
							}
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A9;
								default:
									goto IL_10A;
								}
								break;
							case 7:
								goto IL_A9;
							}
							break;
							IL_A9:
							num = 5;
						}
					}
					IL_59:
					if (true)
					{
					}
					return double.MinValue;
					IL_10A:
					if (false)
					{
					}
					return double.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_39;
							default:
								goto IL_91;
							}
							break;
						case 1:
							goto IL_39;
						case 2:
							if (true)
							{
							}
							goto IL_39;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.ColumnWidth = value;
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_39:
						num2 = 3;
					}
				}
				IL_91:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000097E0 File Offset: 0x000087E0
		int IXLSRange.Count
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					for (;;)
					{
						this.ᜁ();
						num = 0;
						int num2 = 0;
						int count = base.Count;
						int num3 = 2;
						for (;;)
						{
							switch (num3)
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
								{
									if (false)
									{
									}
									if (num2 >= count)
									{
										num3 = 1;
										continue;
									}
									IXLSRange ixlsrange = base.InnerList[num2];
									num += ixlsrange.Count;
									num2++;
									num3 = 3;
									continue;
								}
								}
								break;
							case 1:
								return num;
							case 2:
								goto IL_4B;
							case 3:
								goto IL_4D;
							}
							break;
							IL_4D:
							num3 = 0;
							continue;
							IL_4B:
							goto IL_4D;
						}
					}
					return num;
				}
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000098A0 File Offset: 0x000088A0
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000099D0 File Offset: 0x000089D0
		public DateTime DateTimeValue
		{
			get
			{
				switch (0)
				{
				default:
					if (true)
					{
					}
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
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
									goto IL_AA;
								default:
									goto IL_10B;
								}
								break;
							case 1:
							{
								if (count == 0)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								DateTime dateTimeValue = ixlsrange.DateTimeValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 5;
								continue;
							}
							case 2:
							{
								DateTime dateTimeValue;
								return dateTimeValue;
							}
							case 3:
							{
								IXLSRange ixlsrange;
								DateTime dateTimeValue;
								if (dateTimeValue != ixlsrange.DateTimeValue)
								{
									num = 0;
									continue;
								}
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 4:
								goto IL_61;
							case 5:
								goto IL_AA;
							case 6:
								goto IL_AA;
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 3;
								continue;
							}
							}
							break;
							IL_AA:
							num = 7;
						}
					}
					IL_61:
					return DateTime.MinValue;
					IL_10B:
					if (false)
					{
					}
					return DateTime.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_31;
						case 1:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_31;
							default:
								goto IL_91;
							}
							break;
						case 2:
							goto IL_31;
						case 3:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.DateTimeValue = value;
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_31:
						num2 = 3;
					}
				}
				IL_91:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00009A74 File Offset: 0x00008A74
		public string NumberText
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_11B:
							goto IL_D2;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 2;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
							{
								string numberText;
								IXLSRange ixlsrange;
								if (numberText != ixlsrange.NumberText)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 1;
								continue;
							}
							case 1:
								goto IL_99;
							case 2:
							{
								if (count == 0)
								{
									if (true)
									{
									}
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string numberText = ixlsrange.NumberText;
								int num2 = 0;
								int count2 = base.Count;
								num = 3;
								continue;
							}
							case 3:
								goto IL_11B;
							case 4:
								goto IL_CE;
							case 5:
								goto IL_87;
							case 6:
							{
								string numberText;
								return numberText;
							}
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							}
							goto IL_37;
						}
						IL_99:
						IL_D2:
						num = 7;
						goto IL_10;
					}
					IL_87:
					return null;
					IL_CE:
					return null;
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00009BA4 File Offset: 0x00008BA4
		public IXLSRange EndCell
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5D:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_3E;
				}
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						if (this.ᜅ < 1)
						{
							num = 2;
							continue;
						}
						goto IL_7C;
					case 1:
						if (this.ᜄ >= 1)
						{
							num = 3;
							continue;
						}
						goto IL_5F;
					case 2:
						goto IL_7A;
					case 3:
						goto IL_5D;
					}
					goto IL_3E;
				}
				IL_5F:
				return null;
				IL_7A:
				goto IL_5F;
				IL_7C:
				if (true)
				{
				}
				return this.Worksheet[this.ᜄ, this.ᜅ];
				IL_3E:
				this.ᜁ();
				num = 1;
				goto IL_28;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00009C4C File Offset: 0x00008C4C
		public IXLSRange EntireColumn
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
				this.ᜁ();
				return this.ᜀ(true);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00009C94 File Offset: 0x00008C94
		public IXLSRange EntireRow
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
				this.ᜁ();
				return this.ᜀ(false);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00009CDC File Offset: 0x00008CDC
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00009E08 File Offset: 0x00008E08
		public string ErrorValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					string errorValue;
					for (;;)
					{
						IL_37:
						int count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_118:
							goto IL_C7;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 0;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								errorValue = ixlsrange.ErrorValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 1:
								goto IL_118;
							case 2:
								goto IL_E4;
							case 3:
								goto IL_7F;
							case 4:
								goto IL_C3;
							case 5:
								goto IL_8E;
							case 6:
							{
								IXLSRange ixlsrange;
								if (errorValue != ixlsrange.ErrorValue)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 6;
								continue;
							}
							}
							goto IL_37;
						}
						IL_8E:
						IL_C7:
						num = 7;
						goto IL_10;
					}
					IL_7F:
					return null;
					IL_C3:
					return null;
					IL_E4:
					if (true)
					{
					}
					return errorValue;
				}
				}
			}
			set
			{
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_67:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.ErrorValue = value;
						num++;
						num2 = 0;
					}
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_46;
				}
				for (;;)
				{
					IL_30:
					switch (num2)
					{
					case 0:
						goto IL_5F;
					case 1:
						goto IL_67;
					case 2:
						return;
					case 3:
						goto IL_5F;
					}
					goto IL_46;
					IL_5F:
					num2 = 1;
				}
				return;
				IL_46:
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 3;
				goto IL_30;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009EAC File Offset: 0x00008EAC
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00009FD8 File Offset: 0x00008FD8
		public string Formula
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_118:
							goto IL_C7;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 1;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
							{
								string formula;
								IXLSRange ixlsrange;
								if (formula != ixlsrange.Formula)
								{
									num = 2;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 1:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string formula = ixlsrange.Formula;
								int num2 = 0;
								int count2 = base.Count;
								num = 7;
								continue;
							}
							case 2:
								goto IL_C3;
							case 3:
								goto IL_7F;
							case 4:
							{
								string formula;
								return formula;
							}
							case 5:
								goto IL_8E;
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									if (true)
									{
									}
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							case 7:
								goto IL_118;
							}
							goto IL_37;
						}
						IL_8E:
						IL_C7:
						num = 6;
						goto IL_10;
					}
					IL_7F:
					return null;
					IL_C3:
					return null;
				}
			}
			set
			{
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					if (num >= count)
					{
						num2 = 3;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.Formula = value;
						num++;
						num2 = 1;
					}
					break;
				default:
					if (false)
					{
					}
					goto IL_3E;
				}
				for (;;)
				{
					IL_28:
					switch (num2)
					{
					case 0:
						goto IL_5F;
					case 1:
						goto IL_57;
					case 2:
						goto IL_57;
					case 3:
						goto IL_6B;
					}
					goto IL_3E;
					IL_57:
					num2 = 0;
				}
				IL_6B:
				if (true)
				{
				}
				return;
				IL_3E:
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 2;
				goto IL_28;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000A07C File Offset: 0x0000907C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000A1A8 File Offset: 0x000091A8
		public string FormulaR1C1
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_118:
							goto IL_C7;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 7;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
							{
								string formulaR1C;
								return formulaR1C;
							}
							case 1:
							{
								string formulaR1C;
								IXLSRange ixlsrange;
								if (formulaR1C != ixlsrange.FormulaR1C1)
								{
									num = 6;
									continue;
								}
								int num2;
								num2++;
								num = 2;
								continue;
							}
							case 2:
								goto IL_8E;
							case 3:
								goto IL_7F;
							case 4:
								goto IL_118;
							case 5:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 6:
								goto IL_C3;
							case 7:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string formulaR1C = ixlsrange.FormulaR1C1;
								int num2 = 0;
								int count2 = base.Count;
								if (true)
								{
								}
								num = 4;
								continue;
							}
							}
							goto IL_37;
						}
						IL_8E:
						IL_C7:
						num = 5;
						goto IL_10;
					}
					IL_7F:
					return null;
					IL_C3:
					return null;
				}
			}
			set
			{
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_67:
					if (num >= count)
					{
						num2 = 0;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.FormulaR1C1 = value;
						num++;
						num2 = 1;
					}
					break;
				default:
					if (false)
					{
					}
					goto IL_46;
				}
				for (;;)
				{
					IL_28:
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_5F;
					case 2:
						goto IL_67;
					case 3:
						goto IL_5F;
					}
					goto IL_46;
					IL_5F:
					num2 = 2;
				}
				return;
				IL_46:
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 3;
				goto IL_28;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A24C File Offset: 0x0000924C
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000A378 File Offset: 0x00009378
		public string FormulaArray
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_118:
							goto IL_C7;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 6;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
								goto IL_7F;
							case 1:
							{
								string formulaArray;
								IXLSRange ixlsrange;
								if (formulaArray != ixlsrange.FormulaArray)
								{
									num = 3;
									continue;
								}
								int num2;
								num2++;
								num = 7;
								continue;
							}
							case 2:
								goto IL_118;
							case 3:
								goto IL_C3;
							case 4:
							{
								if (true)
								{
								}
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 5:
							{
								string formulaArray;
								return formulaArray;
							}
							case 6:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string formulaArray = ixlsrange.FormulaArray;
								int num2 = 0;
								int count2 = base.Count;
								num = 2;
								continue;
							}
							case 7:
								goto IL_8E;
							}
							goto IL_37;
						}
						IL_8E:
						IL_C7:
						num = 4;
						goto IL_10;
					}
					IL_7F:
					return null;
					IL_C3:
					return null;
				}
			}
			set
			{
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					if (num >= count)
					{
						num2 = 0;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.FormulaArray = value;
						num++;
						if (true)
						{
						}
						num2 = 3;
					}
					break;
				default:
					if (false)
					{
					}
					goto IL_3E;
				}
				for (;;)
				{
					IL_28:
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_5F;
					case 2:
						goto IL_57;
					case 3:
						goto IL_57;
					}
					goto IL_3E;
					IL_57:
					num2 = 1;
				}
				return;
				IL_3E:
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 2;
				goto IL_28;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000A41C File Offset: 0x0000941C
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000A54C File Offset: 0x0000954C
		public string FormulaArrayR1C1
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int count;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_11B:
							goto IL_D2;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 7;
							break;
						}
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
								goto IL_87;
							case 1:
								goto IL_11B;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 4;
								continue;
							}
							case 3:
								goto IL_CE;
							case 4:
							{
								IXLSRange ixlsrange;
								string formulaArrayR1C;
								if (formulaArrayR1C != ixlsrange.FormulaArrayR1C1)
								{
									num = 3;
									continue;
								}
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 5:
							{
								string formulaArrayR1C;
								return formulaArrayR1C;
							}
							case 6:
								goto IL_99;
							case 7:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string formulaArrayR1C = ixlsrange.FormulaArrayR1C1;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							}
							goto IL_37;
						}
						IL_99:
						IL_D2:
						num = 2;
						goto IL_10;
					}
					IL_87:
					return null;
					IL_CE:
					return null;
				}
			}
			set
			{
				if (true)
				{
				}
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_67:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.FormulaArrayR1C1 = value;
						num++;
						num2 = 1;
					}
					break;
				default:
					if (false)
					{
					}
					goto IL_46;
				}
				for (;;)
				{
					IL_30:
					switch (num2)
					{
					case 0:
						goto IL_67;
					case 1:
						goto IL_5F;
					case 2:
						return;
					case 3:
						goto IL_5F;
					}
					goto IL_46;
					IL_5F:
					num2 = 0;
				}
				return;
				IL_46:
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 3;
				goto IL_30;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000A5F0 File Offset: 0x000095F0
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000A718 File Offset: 0x00009718
		public bool IsFormulaHidden
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_C3;
							case 1:
							{
								bool isFormulaHidden;
								IXLSRange ixlsrange;
								if (isFormulaHidden != ixlsrange.IsFormulaHidden)
								{
									num = 6;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 0;
								continue;
							}
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 3:
								return false;
							case 4:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool isFormulaHidden = ixlsrange.IsFormulaHidden;
								int num2 = 0;
								int count2 = base.Count;
								num = 7;
								continue;
							}
							case 5:
							{
								bool isFormulaHidden;
								return isFormulaHidden;
							}
							case 6:
								goto IL_B7;
							case 7:
								goto IL_C3;
							}
							break;
							IL_C3:
							num = 2;
						}
					}
					return false;
					IL_B7:
					if (true)
					{
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					for (;;)
					{
						IL_27:
						if (true)
						{
						}
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return;
							case 1:
								goto IL_39;
							case 2:
								goto IL_39;
							case 3:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.IsFormulaHidden = value;
								num++;
								num2 = 1;
								continue;
							}
							}
							goto IL_18;
							IL_39:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_27;
							default:
								if (false)
								{
								}
								num2 = 3;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000A7C0 File Offset: 0x000097C0
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000A8F8 File Offset: 0x000098F8
		public DateTime FormulaDateTime
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_CC;
							case 1:
								goto IL_CC;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 7;
								continue;
							}
							case 3:
								goto IL_C4;
							case 4:
								goto IL_5C;
							case 5:
							{
								DateTime formulaDateTime;
								return formulaDateTime;
							}
							case 6:
							{
								if (count == 0)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								DateTime formulaDateTime = ixlsrange.FormulaDateTime;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 7:
							{
								IXLSRange ixlsrange;
								DateTime formulaDateTime;
								if (formulaDateTime != ixlsrange.FormulaDateTime)
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
								{
									if (false)
									{
									}
									int num2;
									num2++;
									break;
								}
								}
								num = 0;
								continue;
							}
							}
							break;
							IL_CC:
							num = 2;
						}
					}
					IL_5C:
					return DateTime.MinValue;
					IL_C4:
					return DateTime.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					for (;;)
					{
						IL_27:
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_31;
							case 1:
								return;
							case 2:
								goto IL_31;
							case 3:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.FormulaDateTime = value;
								num++;
								num2 = 2;
								continue;
							}
							}
							goto IL_18;
							IL_31:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_27;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num2 = 3;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000A9A0 File Offset: 0x000099A0
		public bool HasDataValidation
		{
			get
			{
				switch (0)
				{
				default:
				{
					bool flag;
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (true)
								{
								}
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								flag = ixlsrange.HasDataValidation;
								int num2 = 0;
								int count2 = base.Count;
								num = 8;
								continue;
							}
							case 1:
							{
								IXLSRange ixlsrange;
								if (flag != ixlsrange.HasDataValidation)
								{
									num = 2;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 2:
								flag = false;
								num = 6;
								continue;
							case 3:
								return false;
							case 4:
								goto IL_10D;
							case 5:
								IL_74:
								goto IL_B2;
							case 6:
								goto IL_10D;
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 8:
								goto IL_B2;
							}
							break;
							IL_B2:
							num = 7;
							continue;
							IL_10D:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_74;
							default:
								goto IL_123;
							}
						}
					}
					return false;
					IL_123:
					if (false)
					{
					}
					return flag;
				}
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000AAD8 File Offset: 0x00009AD8
		public bool HasBoolean
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								bool hasBoolean;
								return hasBoolean;
							}
							case 1:
								goto IL_C3;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 5;
								continue;
							}
							case 3:
								return false;
							case 4:
								return false;
							case 5:
							{
								bool hasBoolean;
								IXLSRange ixlsrange;
								if (hasBoolean != ixlsrange.HasBoolean)
								{
									num = 4;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 7;
								continue;
							}
							case 6:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasBoolean = ixlsrange.HasBoolean;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 7:
								if (true)
								{
								}
								goto IL_C3;
							}
							break;
							IL_C3:
							num = 2;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000AC00 File Offset: 0x00009C00
		public bool HasDateTime
		{
			get
			{
				if (true)
				{
				}
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (count == 0)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasDateTime = ixlsrange.HasDateTime;
								int num2 = 0;
								int count2 = base.Count;
								num = 4;
								continue;
							}
							case 1:
							{
								bool hasDateTime;
								return hasDateTime;
							}
							case 2:
								return false;
							case 3:
								goto IL_C3;
							case 4:
								goto IL_C3;
							case 5:
								return false;
							case 6:
							{
								IXLSRange ixlsrange;
								bool hasDateTime;
								if (hasDateTime != ixlsrange.HasDateTime)
								{
									num = 5;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 3;
								continue;
							}
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 6;
								continue;
							}
							}
							break;
							IL_C3:
							num = 7;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000AD28 File Offset: 0x00009D28
		public bool HasFormulaBoolValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return false;
							case 1:
								return false;
							case 2:
								goto IL_C3;
							case 3:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 4;
								continue;
							}
							case 4:
							{
								IXLSRange ixlsrange;
								bool hasFormulaBoolValue;
								if (hasFormulaBoolValue != ixlsrange.HasFormulaBoolValue)
								{
									num = 1;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
								{
									if (true)
									{
									}
									if (false)
									{
									}
									int num2;
									num2++;
									break;
								}
								}
								num = 2;
								continue;
							}
							case 5:
							{
								bool hasFormulaBoolValue;
								return hasFormulaBoolValue;
							}
							case 6:
								goto IL_C3;
							case 7:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormulaBoolValue = ixlsrange.HasFormulaBoolValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 6;
								continue;
							}
							}
							break;
							IL_C3:
							num = 3;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000AE50 File Offset: 0x00009E50
		public bool HasFormulaErrorValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								bool hasFormulaErrorValue;
								IXLSRange ixlsrange;
								if (hasFormulaErrorValue != ixlsrange.HasFormulaErrorValue)
								{
									num = 5;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 3;
								continue;
							}
							case 1:
								return false;
							case 2:
							{
								bool hasFormulaErrorValue;
								return hasFormulaErrorValue;
							}
							case 3:
								goto IL_BB;
							case 4:
								goto IL_BB;
							case 5:
								return false;
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							case 7:
							{
								if (count == 0)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormulaErrorValue = ixlsrange.HasFormulaErrorValue;
								int num2 = 0;
								int count2 = base.Count;
								if (true)
								{
								}
								num = 4;
								continue;
							}
							}
							break;
							IL_BB:
							num = 6;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000AF78 File Offset: 0x00009F78
		public bool HasFormulaDateTime
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						this.ᜁ();
						int count = base.Count;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 1:
							{
								IXLSRange ixlsrange;
								bool hasFormulaDateTime;
								if (hasFormulaDateTime != ixlsrange.HasFormulaDateTime)
								{
									num = 4;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 3;
								continue;
							}
							case 2:
								return false;
							case 3:
								goto IL_C3;
							case 4:
								return false;
							case 5:
								goto IL_C3;
							case 6:
							{
								if (count == 0)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormulaDateTime = ixlsrange.HasFormulaDateTime;
								int num2 = 0;
								int count2 = base.Count;
								num = 5;
								continue;
							}
							case 7:
							{
								bool hasFormulaDateTime;
								return hasFormulaDateTime;
							}
							}
							break;
							IL_C3:
							num = 0;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B0A0 File Offset: 0x0000A0A0
		public bool HasFormulaNumberValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								if (true)
								{
								}
								num = 7;
								continue;
							}
							case 1:
								goto IL_C3;
							case 2:
								goto IL_C3;
							case 3:
							{
								if (count == 0)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormulaNumberValue = ixlsrange.HasFormulaNumberValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 4:
								return false;
							case 5:
								return false;
							case 6:
							{
								bool hasFormulaNumberValue;
								return hasFormulaNumberValue;
							}
							case 7:
							{
								IXLSRange ixlsrange;
								bool hasFormulaNumberValue;
								if (hasFormulaNumberValue != ixlsrange.HasFormulaNumberValue)
								{
									num = 5;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 2;
								continue;
							}
							}
							break;
							IL_C3:
							num = 0;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000B1C8 File Offset: 0x0000A1C8
		public bool HasFormulaStringValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return false;
							case 1:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 2;
								continue;
							}
							case 2:
							{
								if (true)
								{
								}
								IXLSRange ixlsrange;
								bool hasFormulaStringValue;
								if (hasFormulaStringValue != ixlsrange.HasFormulaStringValue)
								{
									num = 6;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 5;
								continue;
							}
							case 3:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormulaStringValue = ixlsrange.HasFormulaStringValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 4;
								continue;
							}
							case 4:
								goto IL_C3;
							case 5:
								goto IL_C3;
							case 6:
								return false;
							case 7:
							{
								bool hasFormulaStringValue;
								return hasFormulaStringValue;
							}
							}
							break;
							IL_C3:
							num = 1;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B2F0 File Offset: 0x0000A2F0
		public bool HasFormula
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 5;
								continue;
							}
							case 1:
								goto IL_C3;
							case 2:
								return false;
							case 3:
							{
								bool hasFormula;
								return hasFormula;
							}
							case 4:
							{
								if (count == 0)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormula = ixlsrange.HasFormula;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 5:
							{
								if (true)
								{
								}
								IXLSRange ixlsrange;
								bool hasFormula;
								if (hasFormula != ixlsrange.HasFormula)
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
								{
									if (false)
									{
									}
									int num2;
									num2++;
									break;
								}
								}
								num = 6;
								continue;
							}
							case 6:
								goto IL_C3;
							case 7:
								return false;
							}
							break;
							IL_C3:
							num = 0;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000B418 File Offset: 0x0000A418
		public bool HasFormulaArray
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_C3;
							case 1:
								return false;
							case 2:
							{
								bool hasFormulaArray;
								return hasFormulaArray;
							}
							case 3:
							{
								bool hasFormulaArray;
								IXLSRange ixlsrange;
								if (hasFormulaArray != ixlsrange.HasFormulaArray)
								{
									num = 7;
									continue;
								}
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
									int num2;
									num2++;
									break;
								}
								}
								num = 4;
								continue;
							}
							case 4:
								if (true)
								{
								}
								goto IL_C3;
							case 5:
							{
								if (count == 0)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasFormulaArray = ixlsrange.HasFormulaArray;
								int num2 = 0;
								int count2 = base.Count;
								num = 0;
								continue;
							}
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 3;
								continue;
							}
							case 7:
								return false;
							}
							break;
							IL_C3:
							num = 6;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B540 File Offset: 0x0000A540
		public bool HasNumber
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								bool hasNumber;
								IXLSRange ixlsrange;
								if (hasNumber != ixlsrange.HasNumber)
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
								{
									if (false)
									{
									}
									if (true)
									{
									}
									int num2;
									num2++;
									break;
								}
								}
								num = 1;
								continue;
							}
							case 1:
								goto IL_C3;
							case 2:
								return false;
							case 3:
							{
								if (count == 0)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasNumber = ixlsrange.HasNumber;
								int num2 = 0;
								int count2 = base.Count;
								num = 5;
								continue;
							}
							case 4:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							case 5:
								goto IL_C3;
							case 6:
								return false;
							case 7:
							{
								bool hasNumber;
								return hasNumber;
							}
							}
							break;
							IL_C3:
							num = 4;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000B668 File Offset: 0x0000A668
		public bool HasRichText
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return false;
							case 1:
							{
								bool hasRichText;
								return hasRichText;
							}
							case 2:
							{
								if (count == 0)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasRichText = ixlsrange.HasRichText;
								int num2 = 0;
								int count2 = base.Count;
								num = 6;
								continue;
							}
							case 3:
							{
								bool hasRichText;
								IXLSRange ixlsrange;
								if (hasRichText != ixlsrange.HasRichText)
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
								{
									if (true)
									{
									}
									if (false)
									{
									}
									int num2;
									num2++;
									break;
								}
								}
								num = 5;
								continue;
							}
							case 4:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 3;
								continue;
							}
							case 5:
								goto IL_C3;
							case 6:
								goto IL_C3;
							case 7:
								return false;
							}
							break;
							IL_C3:
							num = 4;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B790 File Offset: 0x0000A790
		public bool HasString
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						if (true)
						{
						}
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_CB;
							case 1:
							{
								if (count == 0)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasString = ixlsrange.HasString;
								int num2 = 0;
								int count2 = base.Count;
								num = 2;
								continue;
							}
							case 2:
								goto IL_AE;
							case 3:
								goto IL_AE;
							case 4:
							{
								IXLSRange ixlsrange;
								bool hasString;
								if (hasString != ixlsrange.HasString)
								{
									num = 7;
									continue;
								}
								int num2;
								num2++;
								num = 3;
								continue;
							}
							case 5:
								return false;
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 4;
								continue;
							}
							case 7:
								return false;
							}
							break;
							IL_AE:
							num = 6;
						}
					}
					return false;
					IL_CB:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool hasString;
						return hasString;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000B8B8 File Offset: 0x0000A8B8
		public bool HasStyle
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return false;
							case 1:
								if (true)
								{
								}
								goto IL_AE;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 3;
								continue;
							}
							case 3:
							{
								IXLSRange ixlsrange;
								bool hasStyle;
								if (hasStyle != ixlsrange.HasStyle)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 1;
								continue;
							}
							case 4:
								return false;
							case 5:
								goto IL_CB;
							case 6:
								goto IL_AE;
							case 7:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasStyle = ixlsrange.HasStyle;
								int num2 = 0;
								int count2 = base.Count;
								num = 6;
								continue;
							}
							}
							break;
							IL_AE:
							num = 2;
						}
					}
					return false;
					IL_CB:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool hasStyle;
						return hasStyle;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000B9E0 File Offset: 0x0000A9E0
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000BB04 File Offset: 0x0000AB04
		public HorizontalAlignType HorizontalAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								HorizontalAlignType horizontalAlignment;
								IXLSRange ixlsrange;
								if (horizontalAlignment != ixlsrange.HorizontalAlignment)
								{
									num = 3;
									continue;
								}
								int num2;
								num2++;
								num = 7;
								continue;
							}
							case 1:
								goto IL_A3;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							case 3:
								return HorizontalAlignType.General;
							case 4:
							{
								if (count == 0)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								HorizontalAlignType horizontalAlignment = ixlsrange.HorizontalAlignment;
								int num2 = 0;
								int count2 = base.Count;
								if (true)
								{
								}
								num = 1;
								continue;
							}
							case 5:
								goto IL_C0;
							case 6:
								return HorizontalAlignType.General;
							case 7:
								goto IL_A3;
							}
							break;
							IL_A3:
							num = 2;
						}
					}
					return HorizontalAlignType.General;
					IL_C0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return HorizontalAlignType.General;
					default:
					{
						if (false)
						{
						}
						HorizontalAlignType horizontalAlignment;
						return horizontalAlignment;
					}
					}
					break;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_59:
						num2 = 3;
						break;
					default:
						if (false)
						{
						}
						num2 = 0;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_4D;
						case 1:
							goto IL_4D;
						case 2:
						{
							if (num >= count)
							{
								goto IL_59;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.HorizontalAlignment = value;
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						case 3:
							return;
						}
						break;
						IL_4D:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000BBAC File Offset: 0x0000ABAC
		public IHyperLinks Hyperlinks
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 8;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								XlsHyperLinksCollection xlsHyperLinksCollection;
								if (xlsHyperLinksCollection != null)
								{
									num = 1;
									continue;
								}
								goto IL_6C;
							}
							case 1:
							{
								XlsHyperLinksCollection xlsHyperLinksCollection;
								HyperLinksCollection hyperLinksCollection;
								hyperLinksCollection.AddRange(xlsHyperLinksCollection);
								num = 5;
								continue;
							}
							case 2:
								goto IL_E1;
							case 3:
								goto IL_E1;
							case 4:
							{
								HyperLinksCollection hyperLinksCollection;
								return hyperLinksCollection;
							}
							case 5:
								goto IL_6C;
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								XlsHyperLinksCollection xlsHyperLinksCollection = (XlsHyperLinksCollection)((XlsRange)ixlsrange).Hyperlinks;
								if (true)
								{
								}
								num = 0;
								continue;
							}
							case 7:
								goto IL_6A;
							case 8:
							{
								if (count == 0)
								{
									num = 7;
									continue;
								}
								HyperLinksCollection hyperLinksCollection = new HyperLinksCollection((spr\u2158)base.AppImplementation, this, true);
								int num2 = 0;
								int count2 = base.Count;
								num = 3;
								continue;
							}
							}
							break;
							for (;;)
							{
								IL_6C:
								int num2;
								num2++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_86;
								}
							}
							IL_86:
							if (false)
							{
							}
							num = 2;
							continue;
							IL_E1:
							num = 6;
						}
					}
					IL_6A:
					return null;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000BD00 File Offset: 0x0000AD00
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000BE30 File Offset: 0x0000AE30
		public int IndentLevel
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						if (true)
						{
						}
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_B2;
							case 1:
								goto IL_CF;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 4;
								continue;
							}
							case 3:
								return int.MinValue;
							case 4:
							{
								IXLSRange ixlsrange;
								int indentLevel;
								if (indentLevel != ixlsrange.IndentLevel)
								{
									num = 6;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 5:
								goto IL_B2;
							case 6:
								return int.MinValue;
							case 7:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								int indentLevel = ixlsrange.IndentLevel;
								int num2 = 0;
								int count2 = base.Count;
								num = 0;
								continue;
							}
							}
							break;
							IL_B2:
							num = 2;
						}
					}
					return int.MinValue;
					IL_CF:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return int.MinValue;
					default:
					{
						if (false)
						{
						}
						int indentLevel;
						return indentLevel;
					}
					}
					break;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_59:
						num2 = 2;
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								goto IL_59;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.IndentLevel = value;
							num++;
							num2 = 1;
							continue;
						}
						case 1:
							goto IL_4D;
						case 2:
							goto IL_61;
						case 3:
							goto IL_4D;
						}
						break;
						IL_4D:
						num2 = 0;
					}
				}
				IL_61:
				if (true)
				{
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000BED8 File Offset: 0x0000AED8
		public bool IsBlank
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A3;
							case 1:
								return false;
							case 2:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 6;
								continue;
							}
							case 3:
								goto IL_A3;
							case 4:
								goto IL_C8;
							case 5:
								return false;
							case 6:
							{
								IXLSRange ixlsrange;
								bool isBlank;
								if (isBlank != ixlsrange.IsBlank)
								{
									num = 1;
									continue;
								}
								int num2;
								num2++;
								num = 0;
								continue;
							}
							case 7:
							{
								if (count == 0)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool isBlank = ixlsrange.IsBlank;
								int num2 = 0;
								int count2 = base.Count;
								num = 3;
								continue;
							}
							}
							break;
							IL_A3:
							if (true)
							{
							}
							num = 2;
						}
					}
					return false;
					IL_C8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool isBlank;
						return isBlank;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000BFFC File Offset: 0x0000AFFC
		public bool HasError
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return false;
							case 1:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasError = ixlsrange.HasError;
								int num2 = 0;
								int count2 = base.Count;
								num = 4;
								continue;
							}
							case 2:
							{
								IXLSRange ixlsrange;
								bool hasError;
								if (hasError != ixlsrange.HasError)
								{
									num = 7;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 3:
								goto IL_CB;
							case 4:
								goto IL_AE;
							case 5:
								goto IL_AE;
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								if (true)
								{
								}
								num = 2;
								continue;
							}
							case 7:
								return false;
							}
							break;
							IL_AE:
							num = 6;
						}
					}
					return false;
					IL_CB:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool hasError;
						return hasError;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000C124 File Offset: 0x0000B124
		public bool IsGroupedByColumn
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (count == 0)
								{
									if (true)
									{
									}
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool isGroupedByColumn = ixlsrange.IsGroupedByColumn;
								int num2 = 0;
								int count2 = base.Count;
								num = 5;
								continue;
							}
							case 1:
							{
								IXLSRange ixlsrange;
								bool isGroupedByColumn;
								if (isGroupedByColumn != ixlsrange.IsGroupedByColumn)
								{
									num = 6;
									continue;
								}
								int num2;
								num2++;
								num = 7;
								continue;
							}
							case 2:
								goto IL_CB;
							case 3:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 4:
								return false;
							case 5:
								goto IL_AE;
							case 6:
								return false;
							case 7:
								goto IL_AE;
							}
							break;
							IL_AE:
							num = 3;
						}
					}
					return false;
					IL_CB:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool isGroupedByColumn;
						return isGroupedByColumn;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000C24C File Offset: 0x0000B24C
		public bool IsGroupedByRow
		{
			get
			{
				switch (0)
				{
				default:
					if (true)
					{
					}
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								bool isGroupedByRow;
								IXLSRange ixlsrange;
								if (isGroupedByRow != ixlsrange.IsGroupedByRow)
								{
									num = 2;
									continue;
								}
								int num2;
								num2++;
								num = 7;
								continue;
							}
							case 1:
							{
								if (count == 0)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool isGroupedByRow = ixlsrange.IsGroupedByRow;
								int num2 = 0;
								int count2 = base.Count;
								num = 4;
								continue;
							}
							case 2:
								return false;
							case 3:
								goto IL_C8;
							case 4:
								goto IL_AB;
							case 5:
								return false;
							case 6:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 0;
								continue;
							}
							case 7:
								goto IL_AB;
							}
							break;
							IL_AB:
							num = 6;
						}
					}
					return false;
					IL_C8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool isGroupedByRow;
						return isGroupedByRow;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000C370 File Offset: 0x0000B370
		public bool IsInitialized
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 2;
								continue;
							}
							case 1:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool isInitialized = ixlsrange.IsInitialized;
								int num2 = 0;
								int count2 = base.Count;
								num = 7;
								continue;
							}
							case 2:
							{
								IXLSRange ixlsrange;
								bool isInitialized;
								if (isInitialized != ixlsrange.IsInitialized)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 3:
								return false;
							case 4:
								return false;
							case 5:
								goto IL_A3;
							case 6:
								goto IL_C0;
							case 7:
								goto IL_A3;
							}
							break;
							IL_A3:
							num = 0;
						}
					}
					return false;
					IL_C0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						bool isInitialized;
						return isInitialized;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000C494 File Offset: 0x0000B494
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000C4D8 File Offset: 0x0000B4D8
		public int LastColumn
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
				return this.ᜅ;
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
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000C514 File Offset: 0x0000B514
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000C558 File Offset: 0x0000B558
		public int LastRow
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
				return this.ᜄ;
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
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000C594 File Offset: 0x0000B594
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000C6CC File Offset: 0x0000B6CC
		public double NumberValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 7;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 0:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 1;
								continue;
							}
							case 1:
							{
								IXLSRange ixlsrange;
								double numberValue;
								if (numberValue != ixlsrange.NumberValue)
								{
									num = 6;
									continue;
								}
								int num2;
								num2++;
								num = 3;
								continue;
							}
							case 2:
								goto IL_D3;
							case 3:
								goto IL_B6;
							case 4:
								goto IL_B6;
							case 5:
								goto IL_61;
							case 6:
								goto IL_AA;
							case 7:
							{
								if (count == 0)
								{
									num = 5;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								double numberValue = ixlsrange.NumberValue;
								int num2 = 0;
								int count2 = base.Count;
								num = 4;
								continue;
							}
							}
							break;
							IL_B6:
							num = 0;
						}
					}
					IL_61:
					return double.MinValue;
					IL_AA:
					return double.MinValue;
					IL_D3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
					{
						if (false)
						{
						}
						double numberValue;
						return numberValue;
					}
					}
					break;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_59:
						num2 = 2;
						break;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								goto IL_59;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.NumberValue = value;
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							goto IL_4D;
						case 2:
							goto IL_61;
						case 3:
							goto IL_4D;
						}
						break;
						IL_4D:
						num2 = 0;
					}
				}
				IL_61:
				if (true)
				{
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000C774 File Offset: 0x0000B774
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000C7C0 File Offset: 0x0000B7C0
		public string NumberFormat
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
				this.ᜁ();
				return sprṔ.ᜀ(base.InnerList);
			}
			set
			{
				if (true)
				{
				}
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_61:
						num2 = 1;
						break;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_55;
						case 1:
							return;
						case 2:
							goto IL_55;
						case 3:
						{
							if (num >= count)
							{
								goto IL_61;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.NumberFormat = value;
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_55:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000C868 File Offset: 0x0000B868
		public int Row
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
				this.ᜁ();
				return this.ᜂ;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000C8B0 File Offset: 0x0000B8B0
		public int RowGroupLevel
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 7;
								continue;
							}
							case 1:
								return int.MinValue;
							case 2:
							{
								if (count == 0)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								int rowGroupLevel = ixlsrange.RowGroupLevel;
								int num2 = 0;
								int count2 = base.Count;
								if (true)
								{
								}
								num = 5;
								continue;
							}
							case 3:
								goto IL_A7;
							case 4:
								return int.MinValue;
							case 5:
								goto IL_A7;
							case 6:
								goto IL_C4;
							case 7:
							{
								IXLSRange ixlsrange;
								int rowGroupLevel;
								if (rowGroupLevel != ixlsrange.RowGroupLevel)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 3;
								continue;
							}
							}
							break;
							IL_A7:
							num = 0;
						}
					}
					return int.MinValue;
					IL_C4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return int.MinValue;
					default:
					{
						if (false)
						{
						}
						int rowGroupLevel;
						return rowGroupLevel;
					}
					}
					break;
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000C9DC File Offset: 0x0000B9DC
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000CB14 File Offset: 0x0000BB14
		public double RowHeight
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_D5;
							case 1:
							{
								double rowHeight;
								IXLSRange ixlsrange;
								if (rowHeight != ixlsrange.RowHeight)
								{
									num = 5;
									continue;
								}
								int num2;
								num2++;
								num = 0;
								continue;
							}
							case 2:
							{
								double rowHeight;
								return rowHeight;
							}
							case 3:
							{
								if (count == 0)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								double rowHeight = ixlsrange.RowHeight;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_112;
							}
							case 4:
								goto IL_D5;
							case 5:
								goto IL_C9;
							case 6:
								goto IL_5C;
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_112;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							}
							}
							break;
							IL_D5:
							num = 7;
							continue;
							IL_112:
							num = 4;
						}
					}
					IL_5C:
					if (true)
					{
					}
					return double.MinValue;
					IL_C9:
					return double.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_31;
						case 2:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.RowHeight = value;
								num++;
								num2 = 1;
								continue;
							}
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
								num2 = 0;
								continue;
							}
							break;
						case 3:
							goto IL_31;
						}
						break;
						IL_31:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000CBBC File Offset: 0x0000BBBC
		public IXLSRange[] Rows
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
				this.ᜁ();
				return this.GetColumnRows(false);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000CC04 File Offset: 0x0000BC04
		public IXLSRange[] Columns
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
				this.ᜁ();
				return this.GetColumnRows(true);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000CC4C File Offset: 0x0000BC4C
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000CC94 File Offset: 0x0000BC94
		public IStyle Style
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
				this.ᜁ();
				return new StyleArrayWrapper(this);
			}
			set
			{
				int a_ = 13;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜁ();
					if (value != null)
					{
						if (true)
						{
						}
						this.CellStyleName = value.Name;
						return;
					}
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("၂ㅄ㹆╈⹊", a_));
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000CD04 File Offset: 0x0000BD04
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000CD50 File Offset: 0x0000BD50
		public string CellStyleName
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
				this.ᜁ();
				return sprṔ.ᜀ(base.List);
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					if (true)
					{
					}
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.CellStyleName = value;
								num++;
								num2 = 1;
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
								num2 = 3;
								continue;
							}
							break;
						case 1:
							goto IL_39;
						case 2:
							goto IL_39;
						case 3:
							return;
						}
						break;
						IL_39:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000CDF8 File Offset: 0x0000BDF8
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000CF28 File Offset: 0x0000BF28
		public string Text
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_D2;
							case 1:
								goto IL_CE;
							case 2:
							{
								string text;
								IXLSRange ixlsrange;
								if (text != ixlsrange.Text)
								{
									num = 1;
									continue;
								}
								int num2;
								num2++;
								num = 0;
								continue;
							}
							case 3:
								goto IL_5C;
							case 4:
							{
								string text;
								return text;
							}
							case 5:
								goto IL_D2;
							case 6:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string text = ixlsrange.Text;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_10F;
							}
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10F;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									num = 2;
									continue;
								}
								break;
							}
							}
							break;
							IL_D2:
							num = 7;
							continue;
							IL_10F:
							num = 5;
						}
					}
					IL_5C:
					return null;
					IL_CE:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					if (true)
					{
					}
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.Text = value;
								num++;
								num2 = 2;
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
								num2 = 0;
								continue;
							}
							break;
						case 2:
							goto IL_39;
						case 3:
							goto IL_39;
						}
						break;
						IL_39:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000CFD0 File Offset: 0x0000BFD0
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000D108 File Offset: 0x0000C108
		public TimeSpan TimeSpanValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								TimeSpan timeSpanValue;
								return timeSpanValue;
							}
							case 1:
								goto IL_5C;
							case 2:
								goto IL_D6;
							case 3:
							{
								if (count == 0)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								TimeSpan timeSpanValue = ixlsrange.TimeSpanValue;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_113;
							}
							case 4:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_113;
								default:
									if (false)
									{
									}
									if (true)
									{
									}
									num = 5;
									continue;
								}
								break;
							}
							case 5:
							{
								TimeSpan timeSpanValue;
								IXLSRange ixlsrange;
								if (timeSpanValue != ixlsrange.TimeSpanValue)
								{
									num = 7;
									continue;
								}
								int num2;
								num2++;
								num = 2;
								continue;
							}
							case 6:
								goto IL_D6;
							case 7:
								goto IL_CE;
							}
							break;
							IL_D6:
							num = 4;
							continue;
							IL_113:
							num = 6;
						}
					}
					IL_5C:
					return TimeSpan.MinValue;
					IL_CE:
					return TimeSpan.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.TimeSpanValue = value;
								num++;
								num2 = 2;
								continue;
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
								num2 = 0;
								continue;
							}
							break;
						case 2:
							goto IL_31;
						case 3:
							goto IL_31;
						}
						break;
						IL_31:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000D1B0 File Offset: 0x0000C1B0
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000D2E0 File Offset: 0x0000C2E0
		public string Value
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 7;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 0:
							{
								string value;
								IXLSRange ixlsrange;
								if (value != ixlsrange.Value)
								{
									num = 5;
									continue;
								}
								int num2;
								num2++;
								num = 3;
								continue;
							}
							case 1:
							{
								string value;
								return value;
							}
							case 2:
								goto IL_D2;
							case 3:
								goto IL_D2;
							case 4:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10F;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							}
							case 5:
								goto IL_CE;
							case 6:
								goto IL_64;
							case 7:
							{
								if (count == 0)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string value = ixlsrange.Value;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_10F;
							}
							}
							break;
							IL_D2:
							num = 4;
							continue;
							IL_10F:
							num = 2;
						}
					}
					IL_64:
					return null;
					IL_CE:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_39;
						case 1:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.Value = value;
								num++;
								num2 = 0;
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
								num2 = 3;
								continue;
							}
							break;
						case 2:
							if (true)
							{
							}
							goto IL_39;
						case 3:
							return;
						}
						break;
						IL_39:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000D388 File Offset: 0x0000C388
		public string EnvalutedValue
		{
			get
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_C8;
					case 3:
						if (((IWorksheet)base.Parent).FormulaEngine != null)
						{
							num = 1;
							continue;
						}
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
						if (!(base.Parent is IWorksheet))
						{
							goto IL_CA;
						}
						num = 0;
						break;
					}
				}
				IL_C8:
				string a_ = sprḅ.ᜀ(this.Column) + this.Row.ToString();
				return ((IWorksheet)base.Parent).FormulaEngine.ᜀ.\u17C4(a_);
				IL_CA:
				return null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000D460 File Offset: 0x0000C460
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000D590 File Offset: 0x0000C590
		public object Value2
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_5C;
							case 1:
							{
								object value;
								IXLSRange ixlsrange;
								if (!value.Equals(ixlsrange.Value2))
								{
									num = 2;
									continue;
								}
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 2:
								goto IL_CE;
							case 3:
								goto IL_D2;
							case 4:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								object value = ixlsrange.Value2;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_10F;
							}
							case 5:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 7;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10F;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							}
							case 6:
								goto IL_D2;
							case 7:
							{
								object value;
								return value;
							}
							}
							break;
							IL_D2:
							num = 5;
							continue;
							IL_10F:
							num = 3;
						}
					}
					IL_5C:
					if (true)
					{
					}
					return null;
					IL_CE:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.Value2 = value;
								num++;
								num2 = 2;
								continue;
							}
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
								num2 = 0;
								continue;
							}
							break;
						case 2:
							goto IL_31;
						case 3:
							goto IL_31;
						}
						break;
						IL_31:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000D638 File Offset: 0x0000C638
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000D760 File Offset: 0x0000C760
		public VerticalAlignType VerticalAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return VerticalAlignType.Top;
							case 1:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 6;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10A;
								default:
									if (false)
									{
									}
									num = 7;
									continue;
								}
								break;
							}
							case 2:
								goto IL_CD;
							case 3:
							{
								if (count == 0)
								{
									num = 4;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								VerticalAlignType verticalAlignment = ixlsrange.VerticalAlignment;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_10A;
							}
							case 4:
								return VerticalAlignType.Top;
							case 5:
								goto IL_CD;
							case 6:
							{
								VerticalAlignType verticalAlignment;
								return verticalAlignment;
							}
							case 7:
							{
								IXLSRange ixlsrange;
								VerticalAlignType verticalAlignment;
								if (verticalAlignment != ixlsrange.VerticalAlignment)
								{
									num = 0;
									continue;
								}
								int num2;
								num2++;
								num = 2;
								continue;
							}
							}
							break;
							IL_CD:
							num = 1;
							continue;
							IL_10A:
							num = 5;
						}
					}
					return VerticalAlignType.Top;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					if (true)
					{
					}
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_39;
						case 1:
							return;
						case 2:
							goto IL_39;
						case 3:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.VerticalAlignment = value;
								num++;
								num2 = 0;
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
								num2 = 1;
								continue;
							}
							break;
						}
						break;
						IL_39:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000D808 File Offset: 0x0000C808
		public IWorksheet Worksheet
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
				this.ᜁ();
				return this.ᜁ;
			}
		}

		// Token: 0x170000CD RID: 205
		public IXLSRange this[int row, int column]
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
				this.ᜁ();
				return this.Worksheet.AllocatedRange[row, column];
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
				this.ᜁ();
				this.Worksheet.AllocatedRange[row, column] = value;
			}
		}

		// Token: 0x170000CE RID: 206
		public IXLSRange this[int row, int column, int lastRow, int lastColumn]
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
				this.ᜁ();
				return this.Worksheet.AllocatedRange[row, column, lastRow, lastColumn];
			}
		}

		// Token: 0x170000CF RID: 207
		public IXLSRange this[string name]
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
				return this[name, false];
			}
		}

		// Token: 0x170000D0 RID: 208
		public IXLSRange this[string name, bool IsR1C1Notation]
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
				this.ᜁ();
				return this.Worksheet.AllocatedRange[name, IsR1C1Notation];
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000D9E8 File Offset: 0x0000C9E8
		public ConditionalFormats ConditionalFormats
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
				this.ᜁ();
				return base.AppImplementation.ᜀ(this);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000DA38 File Offset: 0x0000CA38
		public Validation DataValidation
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
				return null;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000DA74 File Offset: 0x0000CA74
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000DBA4 File Offset: 0x0000CBA4
		public string FormulaStringValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								string formulaStringValue;
								IXLSRange ixlsrange;
								if (formulaStringValue != ixlsrange.FormulaStringValue)
								{
									num = 6;
									continue;
								}
								int num2;
								num2++;
								num = 7;
								continue;
							}
							case 1:
								goto IL_5C;
							case 2:
							{
								if (count == 0)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string formulaStringValue = ixlsrange.FormulaStringValue;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_10F;
							}
							case 3:
							{
								string formulaStringValue;
								return formulaStringValue;
							}
							case 4:
								goto IL_CA;
							case 5:
							{
								if (true)
								{
								}
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 3;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_10F;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							}
							case 6:
								goto IL_C6;
							case 7:
								goto IL_CA;
							}
							break;
							IL_CA:
							num = 5;
							continue;
							IL_10F:
							num = 4;
						}
					}
					IL_5C:
					return null;
					IL_C6:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_31;
						case 1:
							if (num < count)
							{
								IXLSRange ixlsrange = base.InnerList[num];
								ixlsrange.FormulaStringValue = value;
								num++;
								num2 = 0;
								continue;
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
								num2 = 2;
								continue;
							}
							break;
						case 2:
							return;
						case 3:
							goto IL_31;
						}
						break;
						IL_31:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000DC4C File Offset: 0x0000CC4C
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000DD84 File Offset: 0x0000CD84
		public double FormulaNumberValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_C1;
							case 1:
								goto IL_CD;
							case 2:
							{
								double formulaNumberValue;
								return formulaNumberValue;
							}
							case 3:
							{
								double formulaNumberValue;
								IXLSRange ixlsrange;
								if (formulaNumberValue != ixlsrange.FormulaNumberValue)
								{
									num = 0;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 4:
								goto IL_5C;
							case 5:
								goto IL_CD;
							case 6:
							{
								if (count == 0)
								{
									num = 4;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = base.InnerList[0];
								double formulaNumberValue = ixlsrange.FormulaNumberValue;
								int num2 = 0;
								int count2 = base.Count;
								goto IL_112;
							}
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_112;
								default:
									if (false)
									{
									}
									num = 3;
									continue;
								}
								break;
							}
							}
							break;
							IL_CD:
							num = 7;
							continue;
							IL_112:
							num = 1;
						}
					}
					IL_5C:
					return double.MinValue;
					IL_C1:
					return double.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_67:
						int num;
						int count;
						if (num >= count)
						{
							num2 = 3;
						}
						else
						{
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.FormulaNumberValue = value;
							num++;
							num2 = 0;
						}
						break;
					}
					default:
					{
						if (false)
						{
						}
						this.ᜁ();
						int num = 0;
						int count = base.Count;
						num2 = 1;
						break;
					}
					}
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_5F;
						case 1:
							goto IL_5F;
						case 2:
							goto IL_67;
						case 3:
							return;
						}
						break;
						IL_5F:
						num2 = 2;
					}
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000DE2C File Offset: 0x0000CE2C
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000DF54 File Offset: 0x0000CF54
		public bool FormulaBoolValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int num;
						int count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_CD:
							num = 5;
							break;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 6;
							break;
						}
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_99;
							case 1:
								return false;
							case 2:
							{
								bool formulaBoolValue;
								return formulaBoolValue;
							}
							case 3:
							{
								bool formulaBoolValue;
								IXLSRange ixlsrange;
								if (formulaBoolValue != ixlsrange.FormulaBoolValue)
								{
									num = 4;
									continue;
								}
								int num2;
								num2++;
								num = 0;
								continue;
							}
							case 4:
								return false;
							case 5:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 3;
								continue;
							}
							case 6:
							{
								if (count == 0)
								{
									if (true)
									{
									}
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool formulaBoolValue = ixlsrange.FormulaBoolValue;
								int num2 = 1;
								int count2 = base.Count;
								num = 7;
								continue;
							}
							case 7:
								goto IL_116;
							}
							goto IL_37;
						}
						IL_116:
						IL_99:
						goto IL_CD;
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num;
					int count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5F:
						if (true)
						{
						}
						if (num >= count)
						{
							num2 = 0;
						}
						else
						{
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.FormulaBoolValue = value;
							num++;
							num2 = 3;
						}
						break;
					case 1:
						goto IL_38;
					default:
						goto IL_38;
					}
					for (;;)
					{
						IL_02:
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_57;
						case 2:
							goto IL_5F;
						case 3:
							goto IL_57;
						}
						goto IL_18;
						IL_57:
						num2 = 2;
					}
					IL_38:
					if (false)
					{
					}
					this.ᜁ();
					num = 0;
					count = base.Count;
					num2 = 1;
					goto IL_02;
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000DFFC File Offset: 0x0000CFFC
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000E12C File Offset: 0x0000D12C
		public string FormulaErrorValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int num;
						int count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_D2:
							num = 7;
							break;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 5;
							break;
						}
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_7F;
							case 1:
							{
								string formulaErrorValue;
								return formulaErrorValue;
							}
							case 2:
							{
								string formulaErrorValue;
								IXLSRange ixlsrange;
								if (formulaErrorValue != ixlsrange.FormulaErrorValue)
								{
									num = 4;
									continue;
								}
								if (true)
								{
								}
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 3:
								goto IL_11B;
							case 4:
								goto IL_CE;
							case 5:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								string formulaErrorValue = ixlsrange.FormulaErrorValue;
								int num2 = 1;
								int count2 = base.Count;
								num = 3;
								continue;
							}
							case 6:
								goto IL_99;
							case 7:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 1;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 2;
								continue;
							}
							}
							goto IL_37;
						}
						IL_11B:
						IL_99:
						goto IL_D2;
					}
					IL_7F:
					return null;
					IL_CE:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_67:
						int num;
						int count;
						if (num >= count)
						{
							num2 = 2;
						}
						else
						{
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.FormulaErrorValue = value;
							num++;
							num2 = 1;
						}
						break;
					}
					default:
					{
						if (false)
						{
						}
						this.ᜁ();
						int num = 0;
						int count = base.Count;
						num2 = 3;
						break;
					}
					}
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_67;
						case 1:
							goto IL_5F;
						case 2:
							return;
						case 3:
							goto IL_5F;
						}
						break;
						IL_5F:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000E1D4 File Offset: 0x0000D1D4
		public ICommentShape Comment
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
				this.ᜁ();
				return ((spr\u17FF)base.ReservedHandle).ᜁ(this);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000E228 File Offset: 0x0000D228
		public IRichTextString RichText
		{
			get
			{
				for (;;)
				{
					this.ᜁ();
					int num = 1;
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
								goto IL_3C;
							default:
								goto IL_81;
							}
							break;
						case 1:
							if (this.ᜆ == null)
							{
								goto IL_3C;
							}
							goto IL_89;
						case 2:
							this.ᜆ = new RTFStringArray((spr\u17FF)base.ReservedHandle, this.ᜁ, this);
							num = 0;
							continue;
						}
						break;
						IL_3C:
						num = 2;
					}
				}
				IL_81:
				if (false)
				{
				}
				IL_89:
				return this.ᜆ;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000E2C4 File Offset: 0x0000D2C4
		public bool HasMerged
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_37:
						int num;
						int count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_CA:
							num = 3;
							break;
						default:
							if (false)
							{
							}
							this.ᜁ();
							count = base.Count;
							num = 5;
							break;
						}
						for (;;)
						{
							switch (num)
							{
							case 0:
								return false;
							case 1:
								goto IL_113;
							case 2:
							{
								bool hasMerged;
								return hasMerged;
							}
							case 3:
							{
								int num2;
								int count2;
								if (num2 >= count2)
								{
									num = 2;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								num = 4;
								continue;
							}
							case 4:
							{
								bool hasMerged;
								IXLSRange ixlsrange;
								if (hasMerged != ixlsrange.HasMerged)
								{
									num = 7;
									continue;
								}
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 5:
							{
								if (count == 0)
								{
									num = 0;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								bool hasMerged = ixlsrange.HasMerged;
								int num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 6:
								goto IL_8E;
							case 7:
								return false;
							}
							goto IL_37;
						}
						IL_113:
						IL_8E:
						goto IL_CA;
					}
					return false;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000E3EC File Offset: 0x0000D3EC
		public IXLSRange MergeArea
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					XlsRangesCollection xlsRangesCollection;
					for (;;)
					{
						this.ᜁ();
						xlsRangesCollection = base.AppImplementation.ᜈ(this.Worksheet);
						int num = 0;
						int count = base.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_81;
							case 1:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								XlsRange xlsRange = (XlsRange)base.InnerList[num];
								xlsRangesCollection.Add(xlsRange.MergeArea);
								num++;
								num2 = 0;
								continue;
							}
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									goto IL_81;
								}
								break;
							case 3:
								return xlsRangesCollection;
							}
							break;
							IL_81:
							num2 = 1;
						}
					}
					return xlsRangesCollection;
				}
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000E4C4 File Offset: 0x0000D4C4
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000E510 File Offset: 0x0000D510
		public bool IsWrapText
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
				this.ᜁ();
				return sprṔ.ᜁ(this.Cells);
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
				this.ᜁ();
				sprṔ.ᜀ(this.Cells, value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000E560 File Offset: 0x0000D560
		public bool HasExternalFormula
		{
			get
			{
				for (;;)
				{
					int num = 0;
					int count = base.Count;
					if (true)
					{
					}
					int num2 = 4;
					for (;;)
					{
						IXLSRange ixlsrange;
						switch (num2)
						{
						case 0:
							goto IL_9E;
						case 1:
							return false;
						case 2:
							return true;
						case 3:
							goto IL_89;
						case 4:
							goto IL_9E;
						case 5:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							ixlsrange = base.InnerList[num];
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_89;
							default:
								if (false)
								{
								}
								num2 = 3;
								continue;
							}
							break;
						}
						break;
						IL_89:
						if (!ixlsrange.HasExternalFormula)
						{
							num2 = 1;
							continue;
						}
						num++;
						num2 = 0;
						continue;
						IL_9E:
						num2 = 5;
					}
				}
				return false;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000E628 File Offset: 0x0000D628
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000E750 File Offset: 0x0000D750
		public IgnoreErrorType IgnoreErrorOptions
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								IgnoreErrorType ignoreErrorType;
								return ignoreErrorType;
							}
							case 1:
							{
								IgnoreErrorType ignoreErrorType;
								if (ignoreErrorType == IgnoreErrorType.None)
								{
									if (true)
									{
									}
									num = 0;
									continue;
								}
								IList innerList;
								int num2;
								IXLSRange ixlsrange = (IXLSRange)innerList[num2];
								ignoreErrorType &= ixlsrange.IgnoreErrorOptions;
								num2++;
								num = 7;
								continue;
							}
							case 2:
							{
								int num2;
								int count2;
								if (num2 < count2)
								{
									num = 6;
									continue;
								}
								IgnoreErrorType ignoreErrorType;
								return ignoreErrorType;
							}
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_84;
								default:
									goto IL_7C;
								}
								break;
							case 4:
							{
								if (count == 0)
								{
									num = 3;
									continue;
								}
								IgnoreErrorType ignoreErrorType = IgnoreErrorType.All;
								IList innerList = base.InnerList;
								int num2 = 0;
								int count2 = base.Count;
								num = 5;
								continue;
							}
							case 5:
								goto IL_84;
							case 6:
								num = 1;
								continue;
							case 7:
								goto IL_84;
							}
							break;
							IL_84:
							num = 2;
						}
					}
					IL_7C:
					if (false)
					{
					}
					return IgnoreErrorType.None;
				}
			}
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						IList innerList = base.InnerList;
						int num = 0;
						int count = base.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
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
									goto IL_6E;
								}
								break;
							case 1:
							{
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								IXLSRange ixlsrange = (IXLSRange)innerList[num];
								ixlsrange.IgnoreErrorOptions = value;
								num++;
								num2 = 3;
								continue;
							}
							case 2:
								goto IL_84;
							case 3:
								goto IL_6E;
							}
							break;
							IL_6E:
							num2 = 1;
						}
					}
					IL_84:
					if (true)
					{
					}
					return;
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000E814 File Offset: 0x0000D814
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000E85C File Offset: 0x0000D85C
		public bool? IsStringsPreserved
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
				return this.ᜁ.ᜀ(this);
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
				this.ᜁ.ᜀ(this, value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000E8A4 File Offset: 0x0000D8A4
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000EA64 File Offset: 0x0000DA64
		public BuiltInStyles? BuiltInStyle
		{
			get
			{
				switch (0)
				{
				default:
				{
					BuiltInStyles? builtInStyles2;
					for (;;)
					{
						this.ᜁ();
						int count = base.Count;
						int num = 8;
						for (;;)
						{
							bool flag;
							int num2;
							switch (num)
							{
							case 0:
							{
								BuiltInStyles? result;
								return result;
							}
							case 1:
								goto IL_125;
							case 2:
							{
								BuiltInStyles? builtInStyles;
								BuiltInStyles? builtInStyle;
								if (builtInStyles.GetValueOrDefault() == builtInStyle.GetValueOrDefault())
								{
									num = 4;
									continue;
								}
								num = 5;
								continue;
							}
							case 3:
							{
								BuiltInStyles? builtInStyles;
								BuiltInStyles? builtInStyle;
								flag = (builtInStyles != null != (builtInStyle != null));
								goto IL_112;
							}
							case 4:
								if (true)
								{
								}
								num = 3;
								continue;
							case 5:
								flag = true;
								goto IL_112;
							case 6:
							{
								BuiltInStyles? result = null;
								num = 0;
								continue;
							}
							case 7:
								builtInStyles2 = null;
								num = 9;
								continue;
							case 8:
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
									if (count == 0)
									{
										num = 6;
										continue;
									}
									break;
								}
								IXLSRange ixlsrange = base.InnerList[0];
								builtInStyles2 = ixlsrange.BuiltInStyle;
								num2 = 0;
								int count2 = base.Count;
								num = 1;
								continue;
							}
							case 9:
								goto IL_9F;
							case 10:
								return builtInStyles2;
							case 11:
								goto IL_125;
							case 12:
							{
								int count2;
								if (num2 >= count2)
								{
									num = 10;
									continue;
								}
								IXLSRange ixlsrange = base.InnerList[num2];
								BuiltInStyles? builtInStyles = builtInStyles2;
								BuiltInStyles? builtInStyle = ixlsrange.BuiltInStyle;
								num = 2;
								continue;
							}
							}
							break;
							IL_112:
							if (flag)
							{
								num = 7;
								continue;
							}
							num2++;
							num = 11;
							continue;
							IL_125:
							num = 12;
						}
					}
					IL_9F:
					return builtInStyles2;
				}
				}
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_67:
						int num;
						int count;
						if (num >= count)
						{
							num2 = 3;
						}
						else
						{
							IXLSRange ixlsrange = base.InnerList[num];
							ixlsrange.BuiltInStyle = value;
							num++;
							num2 = 1;
						}
						break;
					}
					default:
					{
						if (false)
						{
						}
						this.ᜁ();
						int num = 0;
						int count = base.Count;
						num2 = 2;
						break;
					}
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_67;
						case 1:
							goto IL_5F;
						case 2:
							goto IL_5F;
						case 3:
							return;
						}
						break;
						IL_5F:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000EB0C File Offset: 0x0000DB0C
		public string RangeGlobalAddress2007
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
				return this.RangeGlobalAddress;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000EB50 File Offset: 0x0000DB50
		internal IXLSRange ᜅ()
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
			this.ᜁ();
			return null;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000EB94 File Offset: 0x0000DB94
		public IXLSRange Activate(bool scroll)
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
			this.ᜁ();
			return null;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000EBD8 File Offset: 0x0000DBD8
		internal new IXLSRange ᜀ(GroupByType A_0)
		{
			for (;;)
			{
				IL_18:
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						((XlsRange)ixlsrange).Group(A_0);
						num++;
						num2 = 3;
					}
					break;
				case 1:
					goto IL_38;
				default:
					goto IL_38;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						goto IL_57;
					case 1:
						goto IL_5F;
					case 2:
						goto IL_6B;
					case 3:
						goto IL_57;
					}
					goto IL_18;
					IL_57:
					num2 = 1;
				}
				IL_38:
				if (false)
				{
				}
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 0;
				goto IL_02;
			}
			IL_6B:
			if (true)
			{
			}
			return this;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000EC88 File Offset: 0x0000DC88
		protected internal IXLSRange Group(GroupByType groupBy, bool bCollapsed)
		{
			for (;;)
			{
				IL_18:
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						if (true)
						{
						}
						IXLSRange ixlsrange = base.InnerList[num];
						((XlsRange)ixlsrange).Group(groupBy, bCollapsed);
						num++;
						num2 = 0;
					}
					break;
				case 1:
					goto IL_38;
				default:
					goto IL_38;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						goto IL_57;
					case 1:
						goto IL_5F;
					case 2:
						return this;
					case 3:
						goto IL_57;
					}
					goto IL_18;
					IL_57:
					num2 = 1;
				}
				IL_38:
				if (false)
				{
				}
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 3;
				goto IL_02;
			}
			return this;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000ED38 File Offset: 0x0000DD38
		public void Merge()
		{
			for (;;)
			{
				IL_18:
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.Merge();
						num++;
						num2 = 3;
					}
					break;
				case 1:
					goto IL_38;
				default:
					goto IL_38;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						goto IL_57;
					case 1:
						goto IL_5F;
					case 2:
						return;
					case 3:
						if (true)
						{
						}
						goto IL_57;
					}
					goto IL_18;
					IL_57:
					num2 = 1;
				}
				IL_38:
				if (false)
				{
				}
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 0;
				goto IL_02;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000EDE0 File Offset: 0x0000DDE0
		public void Merge(bool clearCells)
		{
			for (;;)
			{
				IL_18:
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5F:
					if (num >= count)
					{
						if (true)
						{
						}
						num2 = 1;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.Merge(clearCells);
						num++;
						num2 = 2;
					}
					break;
				case 1:
					goto IL_38;
				default:
					goto IL_38;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						goto IL_57;
					case 1:
						return;
					case 2:
						goto IL_57;
					case 3:
						goto IL_5F;
					}
					goto IL_18;
					IL_57:
					num2 = 3;
				}
				IL_38:
				if (false)
				{
				}
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 0;
				goto IL_02;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000EE88 File Offset: 0x0000DE88
		protected internal IXLSRange Ungroup(GroupByType groupBy)
		{
			for (;;)
			{
				IL_18:
				int num;
				int count;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_67:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						((XlsRange)ixlsrange).Ungroup(groupBy);
						num++;
						num2 = 0;
					}
					break;
				case 1:
					goto IL_38;
				default:
					goto IL_38;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						goto IL_5F;
					case 1:
						goto IL_67;
					case 2:
						return this;
					case 3:
						goto IL_5F;
					}
					goto IL_18;
					IL_5F:
					num2 = 1;
				}
				IL_38:
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜁ();
				num = 0;
				count = base.Count;
				num2 = 3;
				goto IL_02;
			}
			return this;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000EF38 File Offset: 0x0000DF38
		public void UnMerge()
		{
			for (;;)
			{
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_67:
					int num;
					int count;
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.UnMerge();
						num++;
						num2 = 0;
					}
					break;
				}
				default:
				{
					if (false)
					{
					}
					this.ᜁ();
					int num = 0;
					int count = base.Count;
					num2 = 1;
					break;
				}
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_5F;
					case 1:
						goto IL_5F;
					case 2:
						return;
					case 3:
						goto IL_67;
					}
					break;
					IL_5F:
					num2 = 3;
				}
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000EFE0 File Offset: 0x0000DFE0
		public void FreezePanes()
		{
			for (;;)
			{
				IL_14:
				this.ᜁ();
				for (;;)
				{
					IL_1A:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							if (true)
							{
							}
							IXLSRange ixlsrange = base.InnerList[0];
							ixlsrange.FreezePanes();
							num = 0;
							continue;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1A;
							}
							if (false)
							{
							}
							if (base.Count == 1)
							{
								num = 1;
								continue;
							}
							return;
						}
						goto IL_14;
					}
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F070 File Offset: 0x0000E070
		void IXLSRange.ClearContents()
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_39;
					case 1:
						goto IL_31;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39;
						default:
							goto IL_63;
						}
						break;
					case 3:
						goto IL_31;
					}
					break;
					IL_31:
					num2 = 0;
					continue;
					IL_39:
					if (num >= count)
					{
						if (true)
						{
						}
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.ClearContents();
						num++;
						num2 = 3;
					}
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000F118 File Offset: 0x0000E118
		private new void ᜂ(bool A_0)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_39;
					case 1:
						goto IL_41;
					case 2:
						goto IL_39;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							goto IL_63;
						}
						break;
					}
					break;
					IL_39:
					num2 = 1;
					continue;
					IL_41:
					if (num >= count)
					{
						num2 = 3;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						((XlsRange)ixlsrange).Clear(A_0);
						num++;
						num2 = 0;
					}
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000F1C4 File Offset: 0x0000E1C4
		void IXLSRange.Clear(ExcelClearOptions option)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							goto IL_63;
						}
						break;
					case 1:
						goto IL_31;
					case 2:
						goto IL_31;
					case 3:
						goto IL_41;
					}
					break;
					IL_31:
					if (true)
					{
					}
					num2 = 3;
					continue;
					IL_41:
					if (num >= count)
					{
						num2 = 0;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.Clear(option);
						num++;
						num2 = 2;
					}
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000F26C File Offset: 0x0000E26C
		private new void ᜀ(MoveDirectionType A_0)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_39;
					case 1:
						if (true)
						{
						}
						goto IL_39;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							goto IL_63;
						}
						break;
					case 3:
						goto IL_41;
					}
					break;
					IL_39:
					num2 = 3;
					continue;
					IL_41:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						((XlsRange)ixlsrange).Clear(A_0);
						num++;
						num2 = 0;
					}
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F318 File Offset: 0x0000E318
		private new void ᜀ(MoveDirectionType A_0, CopyRangeOptions A_1)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39;
						default:
							goto IL_5B;
						}
						break;
					case 1:
						goto IL_31;
					case 2:
						goto IL_39;
					case 3:
						goto IL_31;
					}
					break;
					IL_31:
					num2 = 2;
					continue;
					IL_39:
					if (num >= count)
					{
						num2 = 0;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						((XlsRange)ixlsrange).ᜀ(A_0, A_1);
						num++;
						num2 = 1;
					}
				}
			}
			IL_5B:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F3C8 File Offset: 0x0000E3C8
		protected internal void MoveTo(IXLSRange destination)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_66:
					this.ᜁ();
					int num = 14;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							int count;
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							IXLSRange ixlsrange = base.InnerList[num2];
							int num4;
							int num3 = ixlsrange.Row + num4;
							int num6;
							int num5 = ixlsrange.Column + num6;
							num = 8;
							continue;
						}
						case 2:
						{
							int num3;
							if (num3 > 0)
							{
								num = 7;
								continue;
							}
							goto IL_110;
						}
						case 3:
						{
							if (true)
							{
							}
							int num5;
							if (num5 > 0)
							{
								num = 4;
								continue;
							}
							goto IL_110;
						}
						case 4:
						{
							IXLSRange ixlsrange;
							int num3;
							int num5;
							((XlsRange)ixlsrange).MoveTo(destination.Worksheet[num3, num5]);
							num = 12;
							continue;
						}
						case 5:
							num = 3;
							continue;
						case 6:
							goto IL_81;
						case 7:
							num = 13;
							continue;
						case 8:
						{
							int num3;
							if (num3 <= this.ᜁ.Workbook.MaxRowCount)
							{
								num = 10;
								continue;
							}
							goto IL_110;
						}
						case 9:
							goto IL_17C;
						case 10:
							num = 2;
							continue;
						case 11:
							goto IL_17C;
						case 12:
							goto IL_110;
						case 13:
						{
							int num5;
							if (num5 <= this.ᜁ.Workbook.MaxColumnCount)
							{
								num = 5;
								continue;
							}
							goto IL_110;
						}
						case 14:
						{
							if (destination == null)
							{
								num = 6;
								continue;
							}
							int num4 = destination.Row - this.Row;
							int num6 = destination.Column - this.Column;
							num2 = 0;
							int count = base.Count;
							num = 9;
							continue;
						}
						}
						break;
						IL_110:
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_66;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						IL_17C:
						num = 1;
					}
				}
				IL_81:
				throw new ArgumentNullException(RecordTableEnumerator.b("堻嬽㌿㙁ⵃ⡅⥇㹉╋⅍㹏", a_));
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F5EC File Offset: 0x0000E5EC
		protected internal IXLSRange CopyTo(IXLSRange destination)
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
			return this.ᜀ(destination, CopyRangeOptions.All);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000F630 File Offset: 0x0000E630
		internal new IXLSRange ᜀ(IXLSRange A_0, CopyRangeOptions A_1)
		{
			int a_ = 14;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_66:
					this.ᜁ();
					int num = 3;
					for (;;)
					{
						int num4;
						switch (num)
						{
						case 0:
							num = 11;
							continue;
						case 1:
							num = 5;
							continue;
						case 2:
							goto IL_81;
						case 3:
						{
							if (A_0 == null)
							{
								num = 2;
								continue;
							}
							int num2 = A_0.Row - this.Row;
							int num3 = A_0.Column - this.Column;
							num4 = 0;
							int count = base.Count;
							num = 9;
							continue;
						}
						case 4:
						{
							int num5;
							if (num5 <= this.ᜁ.Workbook.MaxRowCount)
							{
								num = 1;
								continue;
							}
							goto IL_11A;
						}
						case 5:
						{
							int num5;
							if (num5 > 0)
							{
								num = 7;
								continue;
							}
							goto IL_11A;
						}
						case 6:
							goto IL_11A;
						case 7:
							num = 13;
							continue;
						case 8:
						{
							if (true)
							{
							}
							int num5;
							IXLSRange ixlsrange;
							int num6;
							((XlsRange)ixlsrange).ᜁ(A_0.Worksheet[num5, num6], A_1);
							num = 6;
							continue;
						}
						case 9:
							goto IL_17E;
						case 10:
						{
							int count;
							if (num4 >= count)
							{
								num = 12;
								continue;
							}
							IXLSRange ixlsrange = base.InnerList[num4];
							int num2;
							int num5 = ixlsrange.Row + num2;
							int num3;
							int num6 = ixlsrange.Column + num3;
							num = 4;
							continue;
						}
						case 11:
						{
							int num6;
							if (num6 > 0)
							{
								num = 8;
								continue;
							}
							goto IL_11A;
						}
						case 12:
							return A_0;
						case 13:
						{
							int num6;
							if (num6 <= this.ᜁ.Workbook.MaxColumnCount)
							{
								num = 0;
								continue;
							}
							goto IL_11A;
						}
						case 14:
							goto IL_17E;
						}
						break;
						IL_11A:
						num4++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_66;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						IL_17E:
						num = 10;
					}
				}
				IL_81:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁃⍅㭇㹉╋⁍ㅏ♑㵓㥕㙗", a_));
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000F854 File Offset: 0x0000E854
		public IXLSRange Intersect(IXLSRange range)
		{
			switch (0)
			{
			default:
			{
				XlsRangesCollection xlsRangesCollection;
				for (;;)
				{
					this.ᜁ();
					xlsRangesCollection = base.AppImplementation.ᜈ(this.Worksheet);
					int num = 0;
					int count = xlsRangesCollection.Count;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_65;
						case 1:
							goto IL_80;
						case 2:
							if (xlsRangesCollection.Count <= 0)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_65;
							default:
								goto IL_E9;
							}
							break;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							xlsRangesCollection.Add(ixlsrange.Intersect(range));
							num++;
							num2 = 5;
							continue;
						}
						case 4:
							goto IL_82;
						case 5:
							goto IL_82;
						}
						break;
						IL_65:
						num2 = 2;
						continue;
						IL_82:
						num2 = 3;
					}
				}
				IL_80:
				return null;
				IL_E9:
				if (false)
				{
				}
				return xlsRangesCollection;
			}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000F954 File Offset: 0x0000E954
		public IXLSRange Merge(IXLSRange range)
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
			throw new NotImplementedException();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F994 File Offset: 0x0000E994
		public void AutoFitRows()
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_41;
					case 1:
						goto IL_39;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							goto IL_63;
						}
						break;
					case 3:
						goto IL_39;
					}
					break;
					IL_39:
					num2 = 0;
					continue;
					IL_41:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.AutoFitRows();
						num++;
						num2 = 1;
					}
				}
			}
			IL_63:
			if (false)
			{
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000FA3C File Offset: 0x0000EA3C
		public void AutoFitColumns()
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_39;
					case 1:
						goto IL_31;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39;
						default:
							goto IL_5B;
						}
						break;
					case 3:
						goto IL_31;
					}
					break;
					IL_31:
					num2 = 0;
					continue;
					IL_39:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						if (true)
						{
						}
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.AutoFitColumns();
						num++;
						num2 = 1;
					}
				}
			}
			IL_5B:
			if (false)
			{
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000FAE4 File Offset: 0x0000EAE4
		public ICommentShape AddComment()
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜁ();
				int num = 0;
				int count = base.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_41;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							goto IL_63;
						}
						break;
					case 2:
						goto IL_39;
					case 3:
						goto IL_39;
					}
					break;
					IL_39:
					num2 = 0;
					continue;
					IL_41:
					if (num >= count)
					{
						num2 = 1;
					}
					else
					{
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.AddComment();
						num++;
						num2 = 3;
					}
				}
			}
			IL_63:
			if (false)
			{
			}
			return this.Comment;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000FB94 File Offset: 0x0000EB94
		protected internal IXLSRange FindFirst(string findValue, FindType flags)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_127:
					num = 6;
					break;
				default:
					if (false)
					{
					}
					goto IL_84;
				}
				for (;;)
				{
					IL_3D:
					int num2;
					int count;
					IList innerList;
					switch (num)
					{
					case 0:
						goto IL_9F;
					case 1:
					{
						bool flag;
						if (!flag)
						{
							num = 12;
							continue;
						}
						goto IL_1AF;
					}
					case 2:
					{
						if (findValue == null)
						{
							num = 0;
							continue;
						}
						bool flag = (flags & FindType.Formula) == FindType.Formula;
						bool flag2 = (flags & FindType.Text) == FindType.Text;
						bool flag3 = (flags & FindType.FormulaStringValue) == FindType.FormulaStringValue;
						bool flag4 = (flags & FindType.Error) == FindType.Error;
						num = 1;
						continue;
					}
					case 3:
					{
						bool flag3;
						if (!flag3)
						{
							num = 10;
							continue;
						}
						goto IL_1AF;
					}
					case 4:
					{
						bool flag4;
						if (!flag4)
						{
							num = 9;
							continue;
						}
						goto IL_1AF;
					}
					case 5:
					{
						IXLSRange ixlsrange;
						if (ixlsrange != null)
						{
							num = 14;
							continue;
						}
						num2++;
						num = 7;
						continue;
					}
					case 6:
					{
						bool flag2;
						if (!flag2)
						{
							num = 15;
							continue;
						}
						goto IL_1AF;
					}
					case 7:
						goto IL_14C;
					case 8:
					{
						if (num2 >= count)
						{
							num = 11;
							continue;
						}
						IXLSRange ixlsrange = ((XlsRange)innerList[num2]).FindFirst(findValue, flags);
						num = 5;
						continue;
					}
					case 9:
						goto IL_18D;
					case 10:
						num = 4;
						continue;
					case 11:
						goto IL_16D;
					case 12:
						goto IL_127;
					case 13:
						goto IL_14C;
					case 14:
					{
						IXLSRange ixlsrange;
						return ixlsrange;
					}
					case 15:
						num = 3;
						continue;
					}
					goto IL_84;
					IL_14C:
					num = 8;
					continue;
					IL_1AF:
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 13;
				}
				IL_9F:
				return null;
				IL_16D:
				return null;
				IL_18D:
				throw new ArgumentException(RecordTableEnumerator.b("昵夷䠹崻匽┿㙁⅃㑅桇⍉㽋湍㹏㵑⁓癕⹗㭙せ㝝џ䱡", a_));
				IL_84:
				this.ᜁ();
				num = 2;
				goto IL_3D;
			}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000FD94 File Offset: 0x0000ED94
		protected internal IXLSRange FindFirst(double findValue, FindType flags)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				IXLSRange ixlsrange;
				for (;;)
				{
					this.ᜁ();
					bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
					bool flag2 = (flags & FindType.Number) == FindType.Number;
					int num = 1;
					for (;;)
					{
						int num2;
						int count;
						IList innerList;
						switch (num)
						{
						case 0:
							goto IL_E9;
						case 1:
							if (!flag)
							{
								num = 4;
								continue;
							}
							goto IL_124;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9E;
							default:
								if (false)
								{
								}
								goto IL_CC;
							}
							break;
						case 3:
							goto IL_106;
						case 4:
							num = 7;
							continue;
						case 5:
							goto IL_9E;
						case 6:
							return ixlsrange;
						case 7:
							if (!flag2)
							{
								num = 3;
								continue;
							}
							goto IL_124;
						case 8:
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							ixlsrange = ((XlsRange)innerList[num2]).FindFirst(findValue, flags);
							num = 5;
							continue;
						case 9:
							goto IL_CC;
						}
						break;
						IL_9E:
						if (true)
						{
						}
						if (ixlsrange != null)
						{
							num = 6;
							continue;
						}
						num2++;
						num = 9;
						continue;
						IL_CC:
						num = 8;
						continue;
						IL_124:
						innerList = base.InnerList;
						num2 = 0;
						count = innerList.Count;
						num = 2;
					}
				}
				return ixlsrange;
				IL_E9:
				return null;
				IL_106:
				throw new ArgumentException(RecordTableEnumerator.b("᥈⩊㽌⹎㱐㙒⅔㉖⭘筚㑜ⱞ䅠ൢ੤፦䥨ᵪ౬ͮᡰᝲ孴", a_));
			}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000FF04 File Offset: 0x0000EF04
		protected internal IXLSRange FindFirst(bool findValue)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_82:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_55;
				}
				break;
			}
			int num2;
			int count;
			IList innerList;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					IXLSRange ixlsrange;
					return ixlsrange;
				}
				case 1:
				{
					IXLSRange ixlsrange;
					if (ixlsrange != null)
					{
						num = 0;
						continue;
					}
					goto IL_7E;
				}
				case 2:
				{
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					IXLSRange ixlsrange = ((XlsRange)innerList[num2]).FindFirst(findValue);
					num = 1;
					continue;
				}
				case 3:
					goto IL_BC;
				case 4:
					goto IL_BC;
				case 5:
					goto IL_D8;
				}
				goto IL_55;
				IL_BC:
				num = 2;
			}
			IL_7E:
			num2++;
			goto IL_82;
			IL_D8:
			return null;
			IL_55:
			if (true)
			{
			}
			this.ᜁ();
			innerList = base.InnerList;
			num2 = 0;
			count = innerList.Count;
			num = 4;
			goto IL_36;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000FFEC File Offset: 0x0000EFEC
		protected internal IXLSRange FindFirst(DateTime findValue)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_82:
				num = 4;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_55;
				}
				break;
			}
			int num2;
			int count;
			IList innerList;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					IXLSRange ixlsrange = ((XlsRange)innerList[num2]).FindFirst(findValue);
					num = 1;
					continue;
				}
				case 1:
				{
					IXLSRange ixlsrange;
					if (ixlsrange != null)
					{
						num = 2;
						continue;
					}
					goto IL_7E;
				}
				case 2:
				{
					IXLSRange ixlsrange;
					return ixlsrange;
				}
				case 3:
					goto IL_D8;
				case 4:
					goto IL_BC;
				case 5:
					goto IL_BC;
				}
				goto IL_55;
				IL_BC:
				num = 0;
			}
			IL_7E:
			num2++;
			goto IL_82;
			IL_D8:
			return null;
			IL_55:
			this.ᜁ();
			innerList = base.InnerList;
			num2 = 0;
			count = innerList.Count;
			if (true)
			{
			}
			num = 5;
			goto IL_36;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000100D4 File Offset: 0x0000F0D4
		protected internal IXLSRange FindFirst(TimeSpan findValue)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7A:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_55;
				}
				break;
			}
			int num2;
			int count;
			IList innerList;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					IXLSRange ixlsrange = ((XlsRange)innerList[num2]).FindFirst(findValue);
					num = 4;
					continue;
				}
				case 1:
				{
					IXLSRange ixlsrange;
					return ixlsrange;
				}
				case 2:
					goto IL_B1;
				case 3:
					goto IL_B1;
				case 4:
				{
					IXLSRange ixlsrange;
					if (ixlsrange != null)
					{
						num = 1;
						continue;
					}
					goto IL_76;
				}
				case 5:
					goto IL_D5;
				}
				goto IL_55;
				IL_B1:
				num = 0;
			}
			IL_76:
			num2++;
			goto IL_7A;
			IL_D5:
			return null;
			IL_55:
			this.ᜁ();
			innerList = base.InnerList;
			num2 = 0;
			count = innerList.Count;
			num = 3;
			goto IL_36;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000101BC File Offset: 0x0000F1BC
		protected internal CellRange[] FindAll(DateTime findValue)
		{
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					if (true)
					{
					}
					this.ᜁ();
					list = new List<CellRange>();
					IList innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IXLSRange ixlsrange = (IXLSRange)innerList[num];
							CellRange[] array = ((XlsRange)ixlsrange).FindAll(findValue);
							num2 = 7;
							continue;
						}
						case 1:
							goto IL_136;
						case 2:
							goto IL_11A;
						case 3:
							goto IL_8C;
						case 4:
							goto IL_8A;
						case 5:
							if (list.Count == 0)
							{
								num2 = 4;
								continue;
							}
							goto IL_13B;
						case 6:
						{
							CellRange[] array;
							list.AddRange(array);
							num2 = 3;
							continue;
						}
						case 7:
						{
							CellRange[] array;
							if (array != null)
							{
								num2 = 6;
								continue;
							}
							goto IL_8C;
						}
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_136;
							default:
								if (false)
								{
								}
								goto IL_11A;
							}
							break;
						}
						break;
						IL_8C:
						num++;
						num2 = 8;
						continue;
						IL_11A:
						num2 = 0;
						continue;
						IL_136:
						num2 = 5;
					}
				}
				IL_8A:
				return null;
				IL_13B:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001030C File Offset: 0x0000F30C
		protected internal CellRange[] FindAll(TimeSpan findValue)
		{
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					this.ᜁ();
					list = new List<CellRange>();
					IList innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							CellRange[] array;
							if (array != null)
							{
								num2 = 7;
								continue;
							}
							goto IL_87;
						}
						case 1:
							goto IL_97;
						case 2:
							num2 = 5;
							continue;
						case 3:
							goto IL_87;
						case 4:
							goto IL_120;
						case 5:
							if (list.Count == 0)
							{
								num2 = 6;
								continue;
							}
							goto IL_144;
						case 6:
							goto IL_82;
						case 7:
						{
							if (true)
							{
							}
							CellRange[] array;
							list.AddRange(array);
							num2 = 3;
							continue;
						}
						case 8:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_97;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = (IXLSRange)innerList[num];
								CellRange[] array = ((XlsRange)ixlsrange).FindAll(findValue);
								num2 = 0;
								continue;
							}
							}
							break;
						}
						break;
						IL_87:
						num++;
						num2 = 1;
						continue;
						IL_120:
						num2 = 8;
						continue;
						IL_97:
						goto IL_120;
					}
				}
				IL_82:
				return null;
				IL_144:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010464 File Offset: 0x0000F464
		protected internal List<CellRange> FindAll(string findValue, FindType flags)
		{
			int a_ = 14;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_74:
					this.ᜁ();
					for (;;)
					{
						int num = 10;
						for (;;)
						{
							int num2;
							switch (num)
							{
							case 0:
								num = 7;
								continue;
							case 1:
							{
								List<CellRange> list;
								List<CellRange> list2;
								list.AddRange(list2);
								num = 16;
								continue;
							}
							case 2:
							{
								if (findValue == null)
								{
									num = 20;
									continue;
								}
								List<CellRange> list = new List<CellRange>();
								IList innerList = base.InnerList;
								num2 = 0;
								int count = innerList.Count;
								num = 6;
								continue;
							}
							case 3:
								goto IL_FD;
							case 4:
								num = 15;
								continue;
							case 5:
								num = 12;
								continue;
							case 6:
								goto IL_FD;
							case 7:
							{
								bool flag;
								if (!flag)
								{
									num = 5;
									continue;
								}
								goto IL_1A6;
							}
							case 8:
								goto IL_92;
							case 9:
							{
								bool flag2;
								if (!flag2)
								{
									num = 14;
									continue;
								}
								goto IL_1A6;
							}
							case 10:
							{
								if (findValue == null)
								{
									num = 8;
									continue;
								}
								bool flag3 = (flags & FindType.Formula) == FindType.Formula;
								bool flag = (flags & FindType.Text) == FindType.Text;
								bool flag4 = (flags & FindType.FormulaStringValue) == FindType.FormulaStringValue;
								bool flag2 = (flags & FindType.Error) == FindType.Error;
								num = 13;
								continue;
							}
							case 11:
							{
								int count;
								if (num2 >= count)
								{
									num = 4;
									continue;
								}
								IList innerList;
								IXLSRange ixlsrange = (IXLSRange)innerList[num2];
								List<CellRange> list2 = ((XlsRange)ixlsrange).FindAll(findValue, flags);
								num = 18;
								continue;
							}
							case 12:
							{
								bool flag4;
								if (!flag4)
								{
									num = 17;
									continue;
								}
								goto IL_1A6;
							}
							case 13:
							{
								bool flag3;
								if (!flag3)
								{
									num = 0;
									continue;
								}
								goto IL_1A6;
							}
							case 14:
								goto IL_249;
							case 15:
							{
								List<CellRange> list;
								if (list.Count == 0)
								{
									num = 19;
									continue;
								}
								return list;
							}
							case 16:
								goto IL_20C;
							case 17:
								if (true)
								{
								}
								num = 9;
								continue;
							case 18:
							{
								List<CellRange> list2;
								if (list2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_20C;
							}
							case 19:
								goto IL_20A;
							case 20:
								goto IL_1C4;
							}
							goto IL_74;
							IL_FD:
							num = 11;
							continue;
							IL_1A6:
							num = 2;
							continue;
							IL_20C:
							num2++;
							num = 3;
						}
						IL_249:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_263;
						}
					}
				}
				IL_92:
				return null;
				IL_1C4:
				return null;
				IL_20A:
				return null;
				IL_263:
				if (false)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("ᑃ❅㩇⭉⅋⭍⑏㝑♓癕ㅗ⥙籛そཟᙡ䑣ၥ१٩ի੭幯", a_));
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000106F4 File Offset: 0x0000F6F4
		protected internal CellRange[] FindAll(double findValue, FindType flags)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					this.ᜁ();
					bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
					bool flag2 = (flags & FindType.Number) == FindType.Number;
					int num = 5;
					for (;;)
					{
						int num2;
						int count;
						IList innerList;
						switch (num)
						{
						case 0:
							goto IL_8E;
						case 1:
							num = 8;
							continue;
						case 2:
							goto IL_118;
						case 3:
						{
							if (num2 >= count)
							{
								num = 10;
								continue;
							}
							IXLSRange ixlsrange = (IXLSRange)innerList[num2];
							CellRange[] array = ((XlsRange)ixlsrange).FindAll(findValue, flags);
							num = 11;
							continue;
						}
						case 4:
							goto IL_FB;
						case 5:
							if (!flag)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_173;
						case 6:
							goto IL_11A;
						case 7:
							if (list.Count == 0)
							{
								num = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_162;
							default:
								goto IL_1BC;
							}
							break;
						case 8:
							if (!flag2)
							{
								num = 2;
								continue;
							}
							goto IL_173;
						case 9:
							goto IL_8E;
						case 10:
							num = 7;
							continue;
						case 11:
						{
							CellRange[] array;
							if (array != null)
							{
								goto IL_162;
							}
							goto IL_11A;
						}
						case 12:
						{
							CellRange[] array;
							list.AddRange(array);
							num = 6;
							continue;
						}
						}
						break;
						IL_8E:
						num = 3;
						continue;
						IL_11A:
						num2++;
						num = 9;
						continue;
						IL_162:
						num = 12;
						continue;
						IL_173:
						list = new List<CellRange>();
						innerList = base.InnerList;
						num2 = 0;
						count = innerList.Count;
						num = 0;
					}
				}
				IL_FB:
				return null;
				IL_118:
				throw new ArgumentException(RecordTableEnumerator.b("ᝆ⡈㥊ⱌ≎㑐❒ご╖祘㉚⹜罞འౢᅤ䝦Ὠ੪Ŭٮᕰ嵲", a_));
				IL_1BC:
				if (false)
				{
				}
				return list.ToArray();
			}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000108CC File Offset: 0x0000F8CC
		protected internal CellRange[] FindAll(bool findValue)
		{
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					this.ᜁ();
					list = new List<CellRange>();
					IList innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 8;
							continue;
						case 1:
							goto IL_84;
						case 2:
						{
							CellRange[] array;
							if (array != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_84;
						}
						case 3:
						{
							CellRange[] array;
							list.AddRange(array);
							num2 = 1;
							continue;
						}
						case 4:
							goto IL_94;
						case 5:
							goto IL_82;
						case 6:
							goto IL_112;
						case 7:
							if (true)
							{
							}
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_94;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange = (IXLSRange)innerList[num];
								CellRange[] array = ((XlsRange)ixlsrange).FindAll(findValue);
								num2 = 2;
								continue;
							}
							}
							break;
						case 8:
							if (list.Count == 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_13E;
						}
						break;
						IL_84:
						num++;
						num2 = 4;
						continue;
						IL_112:
						num2 = 7;
						continue;
						IL_94:
						goto IL_112;
					}
				}
				IL_82:
				return null;
				IL_13E:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00010A20 File Offset: 0x0000FA20
		internal void ᜄ()
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

		// Token: 0x060001F7 RID: 503 RVA: 0x00010A60 File Offset: 0x0000FA60
		public void BorderAround()
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
			this.BorderAround(LineStyleType.Thin);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010AA4 File Offset: 0x0000FAA4
		public void BorderAround(LineStyleType borderLine)
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
			this.BorderAround(borderLine, ExcelColors.Black);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010AE8 File Offset: 0x0000FAE8
		public void BorderAround(LineStyleType borderLine, Color borderColor)
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
			ExcelColors nearestColor = this.ᜁ.ParentWorkbook.GetNearestColor(borderColor);
			this.BorderAround(borderLine, nearestColor);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00010B40 File Offset: 0x0000FB40
		public void BorderAround(LineStyleType borderLine, ExcelColors borderColor)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							goto IL_67;
						}
						break;
					case 1:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.BorderAround(borderLine, borderColor);
						num++;
						num2 = 2;
						continue;
					}
					case 2:
						goto IL_33;
					case 3:
						goto IL_33;
					}
					break;
					IL_33:
					num2 = 1;
				}
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00010BE4 File Offset: 0x0000FBE4
		public void BorderInside()
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
			this.BorderInside(LineStyleType.Thin);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00010C28 File Offset: 0x0000FC28
		public void BorderInside(LineStyleType borderLine)
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
			this.BorderInside(borderLine, ExcelColors.Black);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00010C6C File Offset: 0x0000FC6C
		public void BorderInside(LineStyleType borderLine, Color borderColor)
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
			ExcelColors nearestColor = this.ᜁ.Workbook.GetNearestColor(borderColor);
			this.BorderInside(borderLine, nearestColor);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00010CC4 File Offset: 0x0000FCC4
		public void BorderInside(LineStyleType borderLine, ExcelColors borderColor)
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
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						if (true)
						{
						}
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.BorderInside(borderLine, borderColor);
						num++;
						num2 = 2;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B;
						default:
							goto IL_5F;
						}
						break;
					case 2:
						goto IL_2B;
					case 3:
						goto IL_2B;
					}
					break;
					IL_2B:
					num2 = 0;
				}
			}
			IL_5F:
			if (false)
			{
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00010D68 File Offset: 0x0000FD68
		public void BorderNone()
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_33;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							goto IL_67;
						}
						break;
					case 2:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						IXLSRange ixlsrange = base.InnerList[num];
						ixlsrange.BorderNone();
						num++;
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_33;
					}
					break;
					IL_33:
					num2 = 2;
				}
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00010E08 File Offset: 0x0000FE08
		public void CollapseGroup(GroupByType groupBy)
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B;
						default:
							goto IL_67;
						}
						break;
					case 1:
						goto IL_2B;
					case 2:
						goto IL_2B;
					case 3:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						XlsRange xlsRange = (XlsRange)base.InnerList[num];
						xlsRange.CollapseGroup(groupBy);
						num++;
						num2 = 1;
						continue;
					}
					}
					break;
					IL_2B:
					if (true)
					{
					}
					num2 = 3;
				}
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00010EB0 File Offset: 0x0000FEB0
		public void ExpandGroup(GroupByType groupBy)
		{
			if (true)
			{
			}
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						IXLSRange ixlsrange = base.InnerList[num];
						(ixlsrange as XlsRange).ExpandGroup(groupBy);
						num++;
						num2 = 3;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							goto IL_67;
						}
						break;
					case 2:
						goto IL_33;
					case 3:
						goto IL_33;
					}
					break;
					IL_33:
					num2 = 0;
				}
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00010F58 File Offset: 0x0000FF58
		public void ExpandGroup(GroupByType groupBy, ExpandCollapseFlags flags)
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
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						IXLSRange ixlsrange = base.InnerList[num];
						(ixlsrange as XlsRange).ExpandGroup(groupBy, flags);
						num++;
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B;
						default:
							goto IL_5F;
						}
						break;
					case 3:
						goto IL_2B;
					}
					break;
					IL_2B:
					num2 = 1;
				}
			}
			IL_5F:
			if (false)
			{
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011000 File Offset: 0x00010000
		public string GetNewRangeLocation(Dictionary<string, string> names, out string sheetName)
		{
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					sheetName = this.ᜁ.Name;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 3;
								continue;
							}
							string value;
							stringBuilder.Append(value);
							IXLSRange ixlsrange = base.InnerList[num2];
							stringBuilder.Append(((ICombinedRange)ixlsrange).GetNewRangeLocation(names, out sheetName));
							num2++;
							num = 7;
							continue;
						}
						case 1:
						{
							int count;
							if (count == 0)
							{
								goto IL_108;
							}
							IXLSRange ixlsrange = base.InnerList[0];
							stringBuilder.Append(((ICombinedRange)ixlsrange).GetNewRangeLocation(names, out sheetName));
							string value = this.ᜀ();
							int num2 = 1;
							num = 5;
							continue;
						}
						case 2:
							goto IL_114;
						case 3:
							goto IL_EA;
						case 4:
						{
							if (names == null)
							{
								num = 6;
								continue;
							}
							stringBuilder = new StringBuilder();
							int count = base.Count;
							num = 1;
							continue;
						}
						case 5:
							goto IL_C5;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_108;
							default:
								goto IL_72;
							}
							break;
						case 7:
							goto IL_C5;
						}
						break;
						IL_C5:
						if (true)
						{
						}
						num = 0;
						continue;
						IL_108:
						num = 2;
					}
				}
				IL_72:
				if (false)
				{
				}
				return this.RangeAddress;
				IL_EA:
				return stringBuilder.ToString();
				IL_114:
				return string.Empty;
			}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00011170 File Offset: 0x00010170
		public IXLSRange Clone(object parent, Dictionary<string, string> hashNewNames, XlsWorkbook book)
		{
			switch (0)
			{
			default:
			{
				RangesCollection rangesCollection;
				for (;;)
				{
					if (true)
					{
					}
					IWorksheet clonedObject = ((IInternalWorksheet)this.ᜁ).GetClonedObject(hashNewNames, book);
					rangesCollection = new RangesCollection(base.ReservedHandle as spr\u2158, clonedObject);
					List<IXLSRange> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_6B;
						case 1:
							return rangesCollection;
						case 2:
							if (num < count)
							{
								object obj = ((ICombinedRange)innerList[num]).Clone(rangesCollection, hashNewNames, book);
								rangesCollection.Add((IXLSRange)obj);
								num++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
							}
							num2 = 1;
							continue;
						case 3:
							goto IL_6B;
						}
						break;
						IL_6B:
						num2 = 2;
					}
				}
				return rangesCollection;
			}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00011260 File Offset: 0x00010260
		public int CellsCount
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					for (;;)
					{
						num = 0;
						int num2 = 0;
						int count = base.Count;
						int num3 = 1;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_3D;
							case 1:
								goto IL_3D;
							case 2:
								return num;
							case 3:
								if (true)
								{
								}
								if (num2 < count)
								{
									ICombinedRange combinedRange = (ICombinedRange)base.InnerList[num2];
									num += combinedRange.CellsCount;
									num2++;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										num3 = 0;
										continue;
									}
								}
								num3 = 2;
								continue;
							}
							break;
							IL_3D:
							num3 = 3;
						}
					}
					return num;
				}
				}
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011320 File Offset: 0x00010320
		public void ClearConditionalFormats()
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int count = base.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_33;
					case 1:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						ICombinedRange combinedRange = (ICombinedRange)this[num];
						combinedRange.ClearConditionalFormats();
						num++;
						num2 = 3;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							goto IL_67;
						}
						break;
					case 3:
						goto IL_33;
					}
					break;
					IL_33:
					num2 = 1;
				}
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000113C0 File Offset: 0x000103C0
		public Rectangle[] GetRectangles()
		{
			switch (0)
			{
			default:
			{
				Rectangle[] array;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					for (;;)
					{
						int num = 0;
						int count = base.Count;
						int num2 = 0;
						int num3 = 2;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								return array;
							case 1:
							{
								int num4;
								if (num4 >= count)
								{
									num3 = 0;
									continue;
								}
								ICombinedRange combinedRange = (ICombinedRange)this[num4];
								Rectangle[] rectangles = combinedRange.GetRectangles();
								int num5;
								rectangles.CopyTo(array, num5);
								num5 += rectangles.Length;
								num4++;
								num3 = 7;
								continue;
							}
							case 2:
								goto IL_121;
							case 3:
							{
								if (num2 >= count)
								{
									num3 = 5;
									continue;
								}
								ICombinedRange combinedRange2 = (ICombinedRange)this[num2];
								num += combinedRange2.GetRectanglesCount();
								num2++;
								num3 = 4;
								continue;
							}
							case 4:
								goto IL_121;
							case 5:
							{
								array = new Rectangle[num];
								int num4 = 0;
								int num5 = 0;
								num3 = 6;
								continue;
							}
							case 6:
								if (true)
								{
								}
								goto IL_F5;
							case 7:
								goto IL_F5;
							}
							break;
							IL_F5:
							num3 = 1;
							continue;
							IL_121:
							num3 = 3;
						}
					}
					break;
				}
				return array;
			}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00011514 File Offset: 0x00010514
		public int GetRectanglesCount()
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					num = 0;
					int num2 = 0;
					int count = base.Count;
					int num3 = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							return num;
						case 1:
							goto IL_61;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return num;
							default:
								if (false)
								{
								}
								goto IL_61;
							}
							break;
						case 3:
						{
							if (num2 >= count)
							{
								num3 = 0;
								continue;
							}
							ICombinedRange combinedRange = (ICombinedRange)this[num2];
							num += combinedRange.GetRectanglesCount();
							num2++;
							num3 = 1;
							continue;
						}
						}
						break;
						IL_61:
						num3 = 3;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000115D0 File Offset: 0x000105D0
		public string WorksheetName
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
				return this.Worksheet.Name;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00011618 File Offset: 0x00010618
		protected internal new void Add(IXLSRange range)
		{
			int a_ = 0;
			for (;;)
			{
				this.ᜁ();
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5E;
					case 1:
						if (range.Worksheet != this.Worksheet)
						{
							num = 3;
							continue;
						}
						goto IL_B5;
					case 2:
						if (range != null)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 3:
						goto IL_9F;
					}
					break;
				}
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽", a_));
			IL_9F:
			throw new ArgumentException(RecordTableEnumerator.b("电夷吹ᬻ䨽怿ⵁ㑃⍅㩇⭉㡋⭍灏║㵓≕し穙⹛㽝๟աţᕥ䡧౩ṫŭᵯ剱ၳήṷᱹ᥻౽ꚅﾇﺋ晴", a_));
			IL_B5:
			this.ᜂ = Math.Min(this.ᜂ, range.Row);
			this.ᜃ = Math.Min(this.ᜃ, range.Column);
			this.ᜄ = Math.Max(this.ᜄ, range.LastRow);
			this.ᜅ = Math.Max(this.ᜅ, range.LastColumn);
			base.InnerList.Add(range);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00011744 File Offset: 0x00010744
		protected internal void AddRange(IXLSRange range)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						XlsRangesCollection xlsRangesCollection;
						xlsRangesCollection.Add(xlsRangesCollection[num2]);
						num2++;
						num = 3;
						continue;
					}
					case 1:
						return;
					case 2:
						if (range is XlsRangesCollection)
						{
							num = 4;
							continue;
						}
						goto IL_B8;
					case 3:
						goto IL_43;
					case 4:
					{
						XlsRangesCollection xlsRangesCollection = (XlsRangesCollection)range;
						int num2 = 0;
						int count = xlsRangesCollection.Count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 5:
						goto IL_43;
					}
					break;
					IL_43:
					num = 0;
				}
			}
			return;
			IL_B8:
			if (true)
			{
			}
			this.Add(range);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00011818 File Offset: 0x00010818
		public new void Remove(IXLSRange range)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜁ();
					List<IXLSRange> innerList = base.InnerList;
					int num = 0;
					int num2 = innerList.Count;
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							num3 = 8;
							continue;
						case 1:
						{
							IXLSRange ixlsrange;
							if (range.Worksheet == ixlsrange.Worksheet)
							{
								num3 = 0;
								continue;
							}
							goto IL_5C;
						}
						case 2:
						{
							if (num >= num2)
							{
								num3 = 5;
								continue;
							}
							IXLSRange ixlsrange = innerList[num];
							goto IL_97;
						}
						case 3:
							goto IL_BF;
						case 4:
							goto IL_5C;
						case 5:
							goto IL_DB;
						case 6:
							goto IL_BF;
						case 7:
							innerList.RemoveAt(num);
							num--;
							num2--;
							num3 = 4;
							continue;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_97;
							default:
							{
								if (false)
								{
								}
								IXLSRange ixlsrange;
								if (range.RangeAddressLocal == ixlsrange.RangeAddressLocal)
								{
									if (true)
									{
									}
									num3 = 7;
									continue;
								}
								goto IL_5C;
							}
							}
							break;
						}
						break;
						IL_5C:
						num++;
						num3 = 6;
						continue;
						IL_97:
						num3 = 1;
						continue;
						IL_BF:
						num3 = 2;
					}
				}
				IL_DB:
				base.InnerList.Remove(range);
				this.ᜂ();
				return;
			}
		}

		// Token: 0x170000E3 RID: 227
		public new IXLSRange this[int index]
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
				this.ᜁ();
				return base.InnerList[index];
			}
			set
			{
				this.ᜁ();
				if (value == null)
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
						throw new ArgumentNullException();
					}
				}
				base.InnerList[index] = value;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00011A18 File Offset: 0x00010A18
		private new void ᜂ()
		{
			for (;;)
			{
				this.ᜁ();
				this.ᜂ = this.ᜁ.Workbook.MaxRowCount + 1;
				this.ᜃ = this.ᜁ.Workbook.MaxColumnCount + 1;
				this.ᜄ = 0;
				this.ᜅ = 0;
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						if (true)
						{
						}
						IXLSRange ixlsrange = base.InnerList[num];
						this.ᜂ = Math.Min(this.ᜂ, ixlsrange.Row);
						this.ᜃ = Math.Min(this.ᜃ, ixlsrange.Column);
						this.ᜄ = Math.Max(this.ᜄ, ixlsrange.LastRow);
						this.ᜅ = Math.Max(this.ᜅ, ixlsrange.LastColumn);
						num++;
						num2 = 3;
						continue;
					}
					case 1:
						goto IL_6F;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							goto IL_6F;
						}
						break;
					}
					break;
					IL_6F:
					num2 = 0;
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00011B5C File Offset: 0x00010B5C
		private new SortedList<int, KeyValuePair<int, int>> ᜁ(bool A_0)
		{
			switch (0)
			{
			default:
			{
				SortedList<int, KeyValuePair<int, int>> sortedList;
				for (;;)
				{
					this.ᜁ();
					sortedList = new SortedList<int, KeyValuePair<int, int>>();
					int num = 0;
					int count = base.Count;
					int num2 = 12;
					for (;;)
					{
						int num3;
						int num4;
						int num5;
						int num6;
						int a_;
						int a_2;
						int num7;
						int num8;
						switch (num2)
						{
						case 0:
							num2 = 20;
							continue;
						case 1:
						{
							IXLSRange ixlsrange;
							num3 = ixlsrange.LastRow;
							goto IL_2E5;
						}
						case 2:
						{
							IXLSRange ixlsrange;
							num4 = ixlsrange.LastRow;
							goto IL_183;
						}
						case 3:
							num2 = 2;
							continue;
						case 4:
							if (num5 > num6)
							{
								num2 = 18;
								continue;
							}
							this.ᜀ(sortedList, num5, a_, a_2);
							num5++;
							goto IL_2D4;
						case 5:
							goto IL_1EC;
						case 6:
							if (!A_0)
							{
								num2 = 21;
								continue;
							}
							num2 = 14;
							continue;
						case 7:
						{
							IXLSRange ixlsrange;
							num4 = ixlsrange.LastColumn;
							goto IL_183;
						}
						case 8:
							return sortedList;
						case 9:
							goto IL_36B;
						case 10:
							num2 = 13;
							continue;
						case 11:
							if (!A_0)
							{
								num2 = 10;
								continue;
							}
							num2 = 26;
							continue;
						case 12:
							goto IL_36B;
						case 13:
						{
							IXLSRange ixlsrange;
							num7 = ixlsrange.Column;
							goto IL_324;
						}
						case 14:
						{
							IXLSRange ixlsrange;
							num8 = ixlsrange.Column;
							goto IL_346;
						}
						case 15:
						{
							IXLSRange ixlsrange;
							num8 = ixlsrange.Row;
							goto IL_346;
						}
						case 16:
						{
							IXLSRange ixlsrange;
							SortedList<int, KeyValuePair<int, int>> sortedList2 = ((RangesCollection)ixlsrange).ᜁ(A_0);
							IList<int> keys = sortedList2.Keys;
							IList<KeyValuePair<int, int>> values = sortedList2.Values;
							int num9 = 0;
							int count2 = sortedList2.Count;
							num2 = 5;
							continue;
						}
						case 17:
						{
							IXLSRange ixlsrange;
							if (ixlsrange is XlsRangesCollection)
							{
								num2 = 16;
								continue;
							}
							num2 = 6;
							continue;
						}
						case 18:
							goto IL_1A5;
						case 19:
						{
							if (num >= count)
							{
								num2 = 8;
								continue;
							}
							IXLSRange ixlsrange = base.InnerList[num];
							num2 = 17;
							continue;
						}
						case 20:
							goto IL_1A5;
						case 21:
							num2 = 15;
							continue;
						case 22:
							goto IL_F4;
						case 23:
						{
							int num9;
							int count2;
							if (num9 >= count2)
							{
								num2 = 0;
								continue;
							}
							IList<int> keys;
							IList<KeyValuePair<int, int>> values;
							this.ᜀ(sortedList, keys[num9], values[num9]);
							num9++;
							num2 = 24;
							continue;
						}
						case 24:
							goto IL_1EC;
						case 25:
							num2 = 27;
							continue;
						case 26:
						{
							IXLSRange ixlsrange;
							num7 = ixlsrange.Row;
							goto IL_324;
						}
						case 27:
						{
							IXLSRange ixlsrange;
							num3 = ixlsrange.LastColumn;
							goto IL_2E5;
						}
						case 28:
							if (!A_0)
							{
								num2 = 3;
								continue;
							}
							num2 = 7;
							continue;
						case 29:
							goto IL_F4;
						case 30:
							if (!A_0)
							{
								num2 = 25;
								continue;
							}
							num2 = 1;
							continue;
						}
						break;
						IL_F4:
						num2 = 4;
						continue;
						IL_183:
						num6 = num4;
						num2 = 11;
						continue;
						IL_1A5:
						num++;
						num2 = 9;
						continue;
						IL_1EC:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_2D4:
							num2 = 29;
							continue;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 23;
							continue;
						}
						IL_2E5:
						a_2 = num3;
						int num10;
						num5 = num10;
						num2 = 22;
						continue;
						IL_324:
						a_ = num7;
						num2 = 30;
						continue;
						IL_346:
						num10 = num8;
						num2 = 28;
						continue;
						IL_36B:
						num2 = 19;
					}
				}
				return sortedList;
			}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00011EF8 File Offset: 0x00010EF8
		private new void ᜀ(SortedList<int, KeyValuePair<int, int>> A_0, int A_1, KeyValuePair<int, int> A_2)
		{
			int a_ = 1;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜁ();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_59;
						case 1:
							if (A_0 == null)
							{
								num = 0;
								continue;
							}
							num = 2;
							continue;
						case 2:
							if (A_0.ContainsKey(A_1))
							{
								goto IL_C8;
							}
							goto IL_109;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C8;
							default:
							{
								if (false)
								{
								}
								KeyValuePair<int, int> keyValuePair = A_0[A_1];
								int key = A_2.Key;
								int value = A_2.Value;
								int key2 = Math.Min(keyValuePair.Key, key);
								int value2 = Math.Max(keyValuePair.Value, value);
								A_2 = new KeyValuePair<int, int>(key2, value2);
								num = 4;
								continue;
							}
							}
							break;
						case 4:
							goto IL_A9;
						}
						break;
						IL_C8:
						num = 3;
					}
				}
				IL_59:
				throw new ArgumentNullException(RecordTableEnumerator.b("嬶倸䠺䤼", a_));
				IL_A9:
				if (true)
				{
				}
				IL_109:
				A_0[A_1] = A_2;
				return;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00012018 File Offset: 0x00011018
		private new void ᜀ(SortedList<int, KeyValuePair<int, int>> A_0, int A_1, int A_2, int A_3)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				KeyValuePair<int, int> value;
				for (;;)
				{
					this.ᜁ();
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_68;
						case 1:
							goto IL_53;
						case 2:
							value = new KeyValuePair<int, int>(A_2, A_3);
							num = 0;
							continue;
						case 3:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6D;
							default:
								goto IL_100;
							}
							break;
						case 4:
						{
							if (!A_0.ContainsKey(A_1))
							{
								num = 2;
								continue;
							}
							KeyValuePair<int, int> keyValuePair = A_0[A_1];
							int key = Math.Min(keyValuePair.Key, A_2);
							int value2 = Math.Max(keyValuePair.Value, A_3);
							value = new KeyValuePair<int, int>(key, value2);
							num = 3;
							continue;
						}
						case 5:
							if (A_0 == null)
							{
								num = 1;
								continue;
							}
							goto IL_6D;
						}
						break;
						IL_6D:
						num = 4;
					}
				}
				IL_53:
				throw new ArgumentNullException(RecordTableEnumerator.b("⹁ⵃ㕅㱇", a_));
				IL_68:
				goto IL_108;
				IL_100:
				if (false)
				{
				}
				IL_108:
				A_0[A_1] = value;
				return;
			}
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00012138 File Offset: 0x00011138
		private new IXLSRange ᜀ(bool A_0)
		{
			switch (0)
			{
			default:
			{
				XlsRangesCollection xlsRangesCollection;
				for (;;)
				{
					for (;;)
					{
						this.ᜁ();
						xlsRangesCollection = base.AppImplementation.ᜈ(this.Worksheet);
						SortedList<int, KeyValuePair<int, int>> sortedList = this.ᜁ(A_0);
						int num = 18;
						for (;;)
						{
							int num3;
							int num2;
							int num4;
							int num5;
							int num6;
							int num7;
							IXLSRange ixlsrange;
							IXLSRange ixlsrange2;
							int num8;
							int num9;
							int count;
							switch (num)
							{
							case 0:
								num2 = num3;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							case 1:
								goto IL_265;
							case 2:
								num4 = this.ᜁ.AllocatedRange.Column;
								goto IL_222;
							case 3:
								if (!A_0)
								{
									num = 17;
									continue;
								}
								num = 6;
								continue;
							case 4:
								if (!A_0)
								{
									num = 8;
									continue;
								}
								num = 5;
								continue;
							case 5:
								ixlsrange = this.Worksheet.AllocatedRange[num5, num6, num7, num2];
								goto IL_1AE;
							case 6:
								num4 = this.ᜁ.AllocatedRange.Row;
								goto IL_222;
							case 7:
								goto IL_1FC;
							case 8:
								num = 25;
								continue;
							case 9:
								num = 23;
								continue;
							case 10:
								if (true)
								{
								}
								num = 24;
								continue;
							case 11:
								goto IL_2D5;
							case 12:
								ixlsrange2 = this.Worksheet.AllocatedRange[num5, num6, num7, num2];
								goto IL_2A9;
							case 13:
								if (!A_0)
								{
									num = 9;
									continue;
								}
								num = 19;
								continue;
							case 14:
								if (!A_0)
								{
									num = 10;
									continue;
								}
								num = 12;
								continue;
							case 15:
								if (num3 - num2 == 1)
								{
									num = 0;
									continue;
								}
								num = 4;
								continue;
							case 16:
								goto IL_1FC;
							case 17:
								num = 2;
								continue;
							case 18:
							{
								if (sortedList.Count == 0)
								{
									num = 22;
									continue;
								}
								IList<int> keys = sortedList.Keys;
								num6 = keys[0];
								num2 = num6;
								num = 3;
								continue;
							}
							case 19:
								num8 = this.ᜁ.AllocatedRange.LastRow;
								goto IL_309;
							case 20:
								if (xlsRangesCollection.Count == 1)
								{
									num = 11;
									continue;
								}
								return xlsRangesCollection;
							case 21:
							{
								if (num9 >= count)
								{
									num = 26;
									continue;
								}
								IList<int> keys;
								num3 = keys[num9];
								num = 15;
								continue;
							}
							case 22:
								goto IL_CA;
							case 23:
								num8 = this.ᜁ.AllocatedRange.LastColumn;
								goto IL_309;
							case 24:
								ixlsrange2 = this.Worksheet.AllocatedRange[num6, num5, num2, num7];
								goto IL_2A9;
							case 25:
								ixlsrange = this.Worksheet.AllocatedRange[num6, num5, num2, num7];
								goto IL_1AE;
							case 26:
								num = 14;
								continue;
							case 27:
								goto IL_265;
							}
							break;
							IL_1AE:
							IXLSRange range = ixlsrange;
							xlsRangesCollection.Add(range);
							num2 = (num6 = num3);
							num = 27;
							continue;
							IL_1FC:
							num = 21;
							continue;
							IL_222:
							num5 = num4;
							num = 13;
							continue;
							IL_265:
							num9++;
							num = 7;
							continue;
							IL_2A9:
							range = ixlsrange2;
							xlsRangesCollection.Add(range);
							num = 20;
							continue;
							IL_309:
							num7 = num8;
							num9 = 1;
							count = sortedList.Count;
							num = 16;
						}
					}
				}
				IL_CA:
				return null;
				IL_2D5:
				return xlsRangesCollection[0];
			}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000124F8 File Offset: 0x000114F8
		protected internal CellRange[] GetColumnRows(bool bIsColumn)
		{
			IXLSRange[] array;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜁ();
						SortedList<int, KeyValuePair<int, int>> sortedList = this.ᜁ(bIsColumn);
						IList<int> keys = sortedList.Keys;
						IList<KeyValuePair<int, int>> values = sortedList.Values;
						array = new IXLSRange[sortedList.Count];
						int num = 0;
						int count = sortedList.Count;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								if (true)
								{
								}
								int num3 = keys[num];
								KeyValuePair<int, int> keyValuePair = values[num];
								int key = keyValuePair.Key;
								int value = keyValuePair.Value;
								num2 = 2;
								continue;
							}
							case 1:
								goto IL_148;
							case 2:
							{
								int num3;
								int key;
								int value;
								array[num] = (bIsColumn ? ((CellRange)this.ᜁ.AllocatedRange[key, num3, value, num3]) : ((CellRange)this.ᜁ.AllocatedRange[num3, key, num3, value]));
								num++;
								num2 = 3;
								continue;
							}
							case 3:
								goto IL_127;
							case 4:
								goto IL_127;
							}
							break;
							IL_127:
							num2 = 0;
						}
					}
					break;
				}
				break;
			}
			IL_148:
			return (CellRange[])array;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00012658 File Offset: 0x00011658
		private new void ᜁ()
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
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00012694 File Offset: 0x00011694
		private new string ᜀ()
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
			return this.ᜁ.Workbook.ArgumentsSeparator;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000126E0 File Offset: 0x000116E0
		public Ptg[] GetNativePtg()
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				List<Ptg> list;
				for (;;)
				{
					int count = base.List.Count;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							if (num2 >= count)
							{
								if (true)
								{
								}
								num = 9;
								continue;
							}
							num = 11;
							continue;
						}
						case 1:
							goto IL_255;
						case 2:
							goto IL_A3;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_135;
							default:
								if (false)
								{
								}
								if (count == 0)
								{
									num = 12;
									continue;
								}
								num = 5;
								continue;
							}
							break;
						case 4:
						{
							Ptg item = FormulaUtil.ᜀ(FormulaToken.tParentheses, new object[]
							{
								RecordTableEnumerator.b("慈", a_)
							});
							list.Add(item);
							num = 6;
							continue;
						}
						case 5:
						{
							if (base.List[0] is XlsRangesCollection)
							{
								num = 7;
								continue;
							}
							list = new List<Ptg>();
							spr\u1A8B spr_u1A8B = (spr\u1A8B)base.List[0];
							Ptg item2 = FormulaUtil.ᜀ(FormulaToken.tCellRangeList, new object[]
							{
								RecordTableEnumerator.b("效", a_)
							});
							list.Add(spr_u1A8B.ᜀ()[0]);
							int num2 = 1;
							num = 2;
							continue;
						}
						case 6:
							goto IL_1AF;
						case 7:
							goto IL_10F;
						case 8:
							goto IL_A3;
						case 9:
							num = 10;
							continue;
						case 10:
							if (count > 1)
							{
								num = 4;
								continue;
							}
							goto IL_25A;
						case 11:
						{
							int num2;
							if (base.List[num2] is XlsRangesCollection)
							{
								num = 1;
								continue;
							}
							spr\u1A8B spr_u1A8B = (spr\u1A8B)base.List[num2];
							list.Add(spr_u1A8B.ᜀ()[0]);
							Ptg item2;
							list.Add(item2);
							num2++;
							num = 8;
							continue;
						}
						case 12:
							goto IL_9E;
						}
						break;
						IL_A3:
						num = 0;
					}
				}
				IL_9E:
				goto IL_135;
				IL_10F:
				throw new NotSupportedException(RecordTableEnumerator.b("ᭈ⩊⍌⡎㑐獒㙔㡖㕘㝚㡜㱞ᕠ੢੤०䥨੪Ṭ佮ၰ卲ၴ᭶ᱸᙺ᡼ᅾꎂꦈ力떔爵슠힢첤좦잨", a_));
				IL_135:
				return null;
				IL_1AF:
				goto IL_25A;
				IL_255:
				throw new NotSupportedException(RecordTableEnumerator.b("ᭈ⩊⍌⡎㑐獒㙔㡖㕘㝚㡜㱞ᕠ੢੤०䥨੪Ṭ佮ၰ卲ၴ᭶ᱸᙺ᡼ᅾꎂꦈ力떔爵슠힢첤좦잨", a_));
				IL_25A:
				return list.ToArray();
			}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00012950 File Offset: 0x00011950
		public new IEnumerator GetEnumerator()
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
			return this.Cells.GetEnumerator();
		}

		// Token: 0x04000051 RID: 81
		private new const string ᜀ = "Can't operate with ranges from different worksheet";

		// Token: 0x04000052 RID: 82
		private long[] \u2460\u0080\u00A7\u00AE;

		// Token: 0x04000053 RID: 83
		private byte[] \u2460\u0098\u009D\u0097;

		// Token: 0x04000054 RID: 84
		private new XlsWorksheet ᜁ;

		// Token: 0x04000055 RID: 85
		private new int ᜂ;

		// Token: 0x04000056 RID: 86
		private bool[] \u25D8\u00A2\u00A2\u00A6;

		// Token: 0x04000057 RID: 87
		private float \u2460\u008A\u0086\u0094;

		// Token: 0x04000058 RID: 88
		private string[] \u2593\u00A1\u0099\u009B;

		// Token: 0x04000059 RID: 89
		private int ᜃ;

		// Token: 0x0400005A RID: 90
		private int ᜄ;

		// Token: 0x0400005B RID: 91
		private int ᜅ;

		// Token: 0x0400005C RID: 92
		private RTFStringArray ᜆ;
	}
}
