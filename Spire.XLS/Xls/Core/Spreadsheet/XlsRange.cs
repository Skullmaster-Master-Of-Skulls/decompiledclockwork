using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200012B RID: 299
	public class XlsRange : IEnumerable<IXLSRange>, sprṨ, ICombinedRange, spr\u23A5, spr\u1A8B, IDisposable
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0007F9E4 File Offset: 0x0007E9E4
		public string RangeAddress
		{
			get
			{
				int a_ = 4;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜆ();
				return this.\u171D.QuotedName + RecordTableEnumerator.b("ᬹ", a_) + this.RangeAddressLocal;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0007FA54 File Offset: 0x0007EA54
		public string RangeAddressLocal
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
				this.ᜆ();
				return sprṔ.ᜀ(this.FirstRow, this.FirstColumn, this.LastRow, this.LastColumn);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0007FAB4 File Offset: 0x0007EAB4
		public string RangeR1C1Address
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜆ();
				return this.\u171D.QuotedName + RecordTableEnumerator.b("᜵", a_) + this.RangeR1C1AddressLocal;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0007FB24 File Offset: 0x0007EB24
		public string RangeR1C1AddressLocal
		{
			get
			{
				int a_ = 19;
				string text;
				for (;;)
				{
					this.ᜆ();
					text = string.Format(RecordTableEnumerator.b("ᭈお経㉎ቐ⡒摔⩖", a_), this.Row, this.Column);
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
								return text;
							default:
								if (false)
								{
								}
								text = text + RecordTableEnumerator.b("獈", a_) + string.Format(RecordTableEnumerator.b("ᭈお経㉎ቐ⡒摔⩖", a_), this.LastRow, this.LastColumn);
								if (true)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 1:
							if (!this.IsSingleCell)
							{
								num = 0;
								continue;
							}
							return text;
						case 2:
							return text;
						}
						break;
					}
				}
				return text;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0007FC18 File Offset: 0x0007EC18
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0007FD58 File Offset: 0x0007ED58
		public bool BooleanValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_5E;
							case 1:
							{
								int num3;
								if (!this.\u171D.GetBoolean(num, num3))
								{
									num2 = 2;
									continue;
								}
								num3++;
								num2 = 5;
								continue;
							}
							case 2:
								return false;
							case 3:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 9;
									continue;
								}
								num2 = 1;
								continue;
							}
							case 4:
								if (true)
								{
								}
								if (num > lastRow)
								{
									num2 = 8;
									continue;
								}
								goto IL_FD;
							case 5:
								goto IL_5E;
							case 6:
								goto IL_BB;
							case 7:
								goto IL_BB;
							case 8:
								return true;
							case 9:
								num++;
								num2 = 6;
								continue;
							}
							break;
							IL_5E:
							num2 = 3;
							continue;
							IL_BB:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
							{
								IL_FD:
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 0;
								break;
							}
							default:
								if (false)
								{
								}
								num2 = 4;
								break;
							}
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					this.ᜉ();
					int num = 4;
					for (;;)
					{
						int num3;
						switch (num)
						{
						case 0:
							goto IL_138;
						case 1:
							goto IL_65;
						case 2:
							return;
						case 3:
							goto IL_C2;
						case 4:
						{
							if (this.IsSingleCell)
							{
								num = 5;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 0;
							continue;
						}
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_83;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						case 6:
							goto IL_138;
						case 7:
						{
							if (num3 > this.LastColumn)
							{
								num = 12;
								continue;
							}
							spr\u24F1 spr_u24F;
							int num2;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.BooleanValue = value;
							num3++;
							num = 8;
							continue;
						}
						case 8:
							goto IL_65;
						case 9:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 2;
								continue;
							}
							goto IL_83;
						}
						case 10:
							if (this.BooleanValue != value)
							{
								num = 11;
								continue;
							}
							goto IL_182;
						case 11:
							this.ᜀ(this.BooleanValue, value, this);
							num = 3;
							continue;
						case 12:
						{
							int num2;
							num2++;
							num = 6;
							continue;
						}
						}
						break;
						IL_65:
						num = 7;
						continue;
						IL_83:
						num3 = this.FirstColumn;
						if (true)
						{
						}
						num = 1;
						continue;
						IL_138:
						num = 9;
					}
				}
				IL_C2:
				IL_182:
				this.SetBoolean(value);
				this.SetChanged();
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0007FF00 File Offset: 0x0007EF00
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
				this.ᜆ();
				return this.Style.Borders;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0007FF4C File Offset: 0x0007EF4C
		public CellRange[] Cells
		{
			get
			{
				for (;;)
				{
					IL_40:
					this.ᜆ();
					int num = 5;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AA;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								this.InfillCells();
								num = 1;
								continue;
							case 1:
								goto IL_60;
							case 2:
								goto IL_AA;
							case 3:
								if (this.ᜡ == null)
								{
									num = 4;
									continue;
								}
								goto IL_C5;
							case 4:
								goto IL_78;
							case 5:
								if (this.ᜡ == null)
								{
									num = 6;
									continue;
								}
								goto IL_60;
							case 6:
								num = 2;
								continue;
							}
							goto IL_40;
						}
						IL_60:
						num = 3;
						continue;
						IL_AA:
						if (this.ᜢ)
						{
							goto IL_60;
						}
						num = 0;
					}
				}
				IL_78:
				if (true)
				{
				}
				throw new ArgumentNullException();
				IL_C5:
				return this.ᜡ.ToArray();
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0008002C File Offset: 0x0007F02C
		public int Column
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
				this.ᜆ();
				return this.FirstColumn;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00080074 File Offset: 0x0007F074
		public int ColumnGroupLevel
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int firstColumn = this.FirstColumn;
						int lastColumn = this.LastColumn;
						int num = 8;
						for (;;)
						{
							int num2;
							int num3;
							int num4;
							switch (num)
							{
							case 0:
							{
								spr\u216E spr_u216E = this.\u171D.ColumnInformation[firstColumn];
								num = 1;
								continue;
							}
							case 1:
							{
								spr\u216E spr_u216E;
								if (spr_u216E == null)
								{
									num = 3;
									continue;
								}
								num = 4;
								continue;
							}
							case 2:
								goto IL_14C;
							case 3:
								num = 12;
								continue;
							case 4:
							{
								spr\u216E spr_u216E;
								num2 = (int)spr_u216E.ᜊ();
								goto IL_173;
							}
							case 5:
								return -1;
							case 6:
								return num3;
							case 7:
								if (num4 > lastColumn)
								{
									if (true)
									{
									}
									num = 6;
									continue;
								}
								num = 9;
								continue;
							case 8:
							{
								if (firstColumn == lastColumn)
								{
									num = 0;
									continue;
								}
								int firstRow = this.FirstRow;
								num3 = this[firstRow, firstColumn].ColumnGroupLevel;
								num4 = firstColumn + 1;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_D4;
								default:
									if (false)
									{
									}
									num = 11;
									continue;
								}
								break;
							}
							case 9:
							{
								int firstRow;
								if (num3 != this[firstRow, num4].ColumnGroupLevel)
								{
									num = 5;
									continue;
								}
								goto IL_D4;
							}
							case 10:
								return num3;
							case 11:
								goto IL_14C;
							case 12:
								num2 = 0;
								goto IL_173;
							}
							break;
							IL_D4:
							num4++;
							num = 2;
							continue;
							IL_14C:
							num = 7;
							continue;
							IL_173:
							num3 = num2;
							num = 10;
						}
					}
					return -1;
				}
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00080218 File Offset: 0x0007F218
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0008036C File Offset: 0x0007F36C
		public double ColumnWidth
		{
			get
			{
				double num;
				for (;;)
				{
					if (true)
					{
					}
					this.ᜆ();
					num = double.MinValue;
					int num2 = 4;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							goto IL_C9;
						case 1:
							num = this.\u171D.ᜉ(this.m_iLeftColumn);
							num2 = 6;
							continue;
						case 2:
							if (num != this.\u171D.ᜉ(num3))
							{
								num2 = 3;
								continue;
							}
							num3++;
							num2 = 0;
							continue;
						case 3:
							num = double.MinValue;
							num2 = 8;
							continue;
						case 4:
							goto IL_50;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_50;
							default:
								if (false)
								{
								}
								if (num3 > this.m_iRightColumn)
								{
									num2 = 9;
									continue;
								}
								num2 = 2;
								continue;
							}
							break;
						case 6:
							return num;
						case 7:
							goto IL_C9;
						case 8:
							return num;
						case 9:
							return num;
						}
						break;
						IL_50:
						if (this.m_iLeftColumn == this.m_iRightColumn)
						{
							num2 = 1;
							continue;
						}
						num = this.\u171D.ᜉ(this.m_iLeftColumn);
						num3 = this.m_iLeftColumn + 1;
						num2 = 7;
						continue;
						IL_C9:
						num2 = 5;
					}
				}
				return num;
			}
			set
			{
				int a_ = 10;
				for (;;)
				{
					this.ᜆ();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							int lastColumn;
							if (num2 > lastColumn)
							{
								num = 5;
								continue;
							}
							this.\u171D.SetColumnWidth(num2, value);
							num2++;
							if (true)
							{
							}
							num = 6;
							continue;
						}
						case 1:
							if (value >= 0.0)
							{
								num = 7;
								continue;
							}
							goto IL_E0;
						case 2:
							goto IL_C4;
						case 3:
							goto IL_C2;
						case 4:
						{
							if (value > 255.0)
							{
								num = 3;
								continue;
							}
							int num2 = this.FirstColumn;
							int lastColumn = this.LastColumn;
							num = 2;
							continue;
						}
						case 5:
							goto IL_DE;
						case 6:
							goto IL_C4;
						case 7:
							num = 4;
							continue;
						}
						break;
						IL_C4:
						num = 0;
					}
				}
				IL_C2:
				goto IL_E0;
				IL_DE:
				return;
				IL_E0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("̿ⵁ⡃㍅╇⑉ᭋ❍㑏♑㱓", a_), RecordTableEnumerator.b("̿ⵁ⡃㍅╇⑉汋᥍㥏㙑⁓㹕硗⥙㑛ㅝᕟ๡c䙥੧ཀྵ䱫౭ᕯٱͳ፵ᵷᑹ屻乽ꁿꢇ뢉릋뮍뺏", a_));
				}
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00080498 File Offset: 0x0007F498
		public int Count
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
				this.ᜆ();
				return (this.LastColumn - this.FirstColumn + 1) * (this.LastRow - this.FirstRow + 1);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x000804F8 File Offset: 0x0007F4F8
		public bool HasDataValidation
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 2;
					bool result;
					for (;;)
					{
						int num2;
						IMigrantRange migrantRange;
						int num3;
						switch (num)
						{
						case 0:
							return result;
						case 1:
							if (num2 > this.m_iRightColumn)
							{
								num = 13;
								continue;
							}
							migrantRange.ResetRowColumn(num3, num2);
							num = 4;
							continue;
						case 3:
							goto IL_C7;
						case 4:
							if (!migrantRange.HasDataValidation)
							{
								num = 9;
								continue;
							}
							goto IL_F9;
						case 5:
							return result;
						case 6:
							if (true)
							{
							}
							goto IL_F9;
						case 7:
							goto IL_10E;
						case 8:
							result = (this.ᜇ() != null);
							num = 5;
							continue;
						case 9:
							result = false;
							num = 6;
							continue;
						case 10:
							goto IL_6B;
						case 11:
							goto IL_6B;
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C7;
							default:
								if (false)
								{
								}
								if (num3 > this.m_iBottomRow)
								{
									num = 0;
									continue;
								}
								num2 = this.m_iLeftColumn;
								num = 10;
								continue;
							}
							break;
						case 13:
							num3++;
							num = 7;
							continue;
						}
						if (this.IsSingleCell)
						{
							num = 8;
							continue;
						}
						result = true;
						migrantRange = new spr\u24F1(this.Application, this.\u171D);
						num3 = this.m_iTopRow;
						num = 3;
						continue;
						IL_6B:
						num = 1;
						continue;
						IL_F9:
						num2++;
						num = 11;
						continue;
						IL_10E:
						num = 12;
						continue;
						IL_C7:
						goto IL_10E;
					}
					return result;
				}
				}
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000806A4 File Offset: 0x0007F6A4
		public int ColumnCount
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
				this.ᜆ();
				return this.LastColumn - this.FirstColumn + 1;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000806F4 File Offset: 0x0007F6F4
		public int RowCount
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
				this.ᜆ();
				return this.LastRow - this.FirstRow + 1;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00080744 File Offset: 0x0007F744
		public bool HasConditionFormats
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 1;
					bool result;
					for (;;)
					{
						int num2;
						spr\u24F1 spr_u24F;
						int num3;
						switch (num)
						{
						case 0:
							if (num2 > this.m_iRightColumn)
							{
								num = 6;
								continue;
							}
							spr_u24F.ᜀ(num3, num2);
							if (true)
							{
							}
							num = 4;
							continue;
						case 2:
							return result;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BF;
							default:
								if (false)
								{
								}
								if (num3 > this.m_iBottomRow)
								{
									num = 2;
									continue;
								}
								num2 = this.m_iLeftColumn;
								num = 12;
								continue;
							}
							break;
						case 4:
							if (!spr_u24F.HasConditionFormats)
							{
								num = 5;
								continue;
							}
							goto IL_101;
						case 5:
							result = false;
							num = 13;
							continue;
						case 6:
							num3++;
							num = 10;
							continue;
						case 7:
							result = (this.\u171D.ConditionalFormats.Find(this.GetRectangles()) != null);
							num = 9;
							continue;
						case 8:
							goto IL_BF;
						case 9:
							return result;
						case 10:
							goto IL_116;
						case 11:
							goto IL_6B;
						case 12:
							goto IL_6B;
						case 13:
							goto IL_101;
						}
						if (this.IsSingleCell)
						{
							num = 7;
							continue;
						}
						result = true;
						spr_u24F = new spr\u24F1(this.Application, this.\u171D);
						num3 = this.m_iTopRow;
						num = 8;
						continue;
						IL_6B:
						num = 0;
						continue;
						IL_101:
						num2++;
						num = 11;
						continue;
						IL_116:
						num = 3;
						continue;
						IL_BF:
						goto IL_116;
					}
					return result;
				}
				}
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00080904 File Offset: 0x0007F904
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00080BEC File Offset: 0x0007FBEC
		public DateTime DateTimeValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					double number;
					for (;;)
					{
						this.ᜆ();
						number = this.\u171D.GetNumber(this.Row, this.Column);
						int num = 17;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								int lastColumn;
								if (num2 > lastColumn)
								{
									num = 20;
									continue;
								}
								int num3;
								double number2 = this.\u171D.GetNumber(num3, num2);
								num = 14;
								continue;
							}
							case 1:
								if (this.InnerNumberFormat.ᜀ(number) != CellFormatType.DateTime)
								{
									num = 15;
									continue;
								}
								num = 16;
								continue;
							case 2:
								goto IL_216;
							case 3:
								goto IL_DC;
							case 4:
							{
								double number2;
								if (number == number2)
								{
									num = 3;
									continue;
								}
								goto IL_B9;
							}
							case 5:
								goto IL_10A;
							case 6:
								num = 4;
								continue;
							case 7:
								num = 18;
								continue;
							case 8:
								goto IL_10A;
							case 9:
								goto IL_1C9;
							case 10:
								num = 1;
								continue;
							case 11:
								goto IL_191;
							case 12:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DC;
								default:
								{
									if (false)
									{
									}
									int num3;
									int lastRow;
									if (num3 > lastRow)
									{
										num = 9;
										continue;
									}
									int num2 = this.Column;
									int lastColumn = this.LastColumn;
									num = 8;
									continue;
								}
								}
								break;
							case 13:
								goto IL_182;
							case 14:
							{
								double number2;
								if (number2 != double.NaN)
								{
									num = 6;
									continue;
								}
								goto IL_B9;
							}
							case 15:
								goto IL_274;
							case 16:
								if (number != double.NaN)
								{
									num = 7;
									continue;
								}
								goto IL_244;
							case 17:
								if (number >= 0.0)
								{
									num = 10;
									continue;
								}
								goto IL_138;
							case 18:
							{
								if (this.InnerNumberFormat.ᜀ(number) != CellFormatType.DateTime)
								{
									num = 2;
									continue;
								}
								int num3 = this.Row;
								int lastRow = this.LastRow;
								num = 11;
								continue;
							}
							case 19:
							{
								double number2;
								if (this.InnerNumberFormat.ᜀ(number2) != CellFormatType.DateTime)
								{
									num = 13;
									continue;
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							case 20:
							{
								if (true)
								{
								}
								int num3;
								num3++;
								num = 21;
								continue;
							}
							case 21:
								goto IL_191;
							}
							break;
							IL_DC:
							num = 19;
							continue;
							IL_10A:
							num = 0;
							continue;
							IL_191:
							num = 12;
						}
					}
					IL_B9:
					return DateTime.MinValue;
					IL_138:
					string text = this.\u171D.GetText(this.Row, this.Column);
					return Convert.ToDateTime(text);
					IL_182:
					goto IL_B9;
					IL_1C9:
					return UtilityMethods.ᜀ(number);
					IL_216:
					IL_244:
					return DateTime.MinValue;
					IL_274:
					goto IL_138;
				}
				}
			}
			set
			{
				switch (0)
				{
				default:
				{
					for (;;)
					{
						this.ᜆ();
						int num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								num2++;
								goto IL_159;
							}
							case 1:
								goto IL_FB;
							case 2:
								goto IL_80;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_159;
								default:
									goto IL_75;
								}
								break;
							case 4:
								goto IL_80;
							case 5:
							{
								if (this.IsSingleCell)
								{
									num = 3;
									continue;
								}
								this.ᜉ();
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num2 = this.FirstRow;
								num = 1;
								continue;
							}
							case 6:
							{
								int num2;
								if (num2 > this.LastRow)
								{
									num = 9;
									continue;
								}
								int num3 = this.FirstColumn;
								num = 2;
								continue;
							}
							case 7:
							{
								int num3;
								if (num3 > this.LastColumn)
								{
									num = 0;
									continue;
								}
								int num2;
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								spr_u24F.DateTimeValue = value;
								num3++;
								num = 4;
								continue;
							}
							case 8:
								goto IL_FB;
							case 9:
								goto IL_11C;
							}
							break;
							IL_80:
							num = 7;
							continue;
							IL_FB:
							num = 6;
							continue;
							IL_159:
							num = 8;
						}
					}
					IL_75:
					if (false)
					{
					}
					this.FormatType = CellFormatType.DateTime;
					DateTime dateTimeValue = this.DateTimeValue;
					dateTimeValue != value;
					this.ᜀ(dateTimeValue, value, this);
					this.SetDateTime(value);
					this.SetChanged();
					return;
					IL_11C:
					if (true)
					{
					}
					return;
				}
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00080D80 File Offset: 0x0007FD80
		public string DisplayedText
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
				return this.NumberText;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00080DC4 File Offset: 0x0007FDC4
		public string NumberText
		{
			get
			{
				int a_ = 5;
				switch (0)
				{
				default:
				{
					string text;
					for (;;)
					{
						text = string.Empty;
						int num = 41;
						for (;;)
						{
							double number;
							string text2;
							sprᤅ sprᤅ;
							switch (num)
							{
							case 0:
								if (this.HasFormula)
								{
									num = 2;
									continue;
								}
								goto IL_366;
							case 1:
								if (double.IsNaN(this.FormulaNumberValue))
								{
									num = 35;
									continue;
								}
								goto IL_366;
							case 2:
								num = 1;
								continue;
							case 3:
								goto IL_2BC;
							case 4:
								if (number == 0.0)
								{
									num = 27;
									continue;
								}
								goto IL_121;
							case 5:
								return text;
							case 6:
								goto IL_533;
							case 7:
								text = this.FormulaBoolValue.ToString().ToUpper();
								num = 36;
								continue;
							case 8:
								text = this.FormulaErrorValue;
								num = 30;
								continue;
							case 9:
								num = 20;
								continue;
							case 10:
								num = 16;
								continue;
							case 11:
								if (this.InnerNumberFormat.ᜇ() == CellFormatType.DateTime)
								{
									num = 48;
									continue;
								}
								return text;
							case 12:
							{
								DateTime dateTime;
								CultureInfo provider;
								text2 = dateTime.ToString(RecordTableEnumerator.b("强", a_), provider);
								goto IL_5E3;
							}
							case 13:
								if (text == null)
								{
									num = 10;
									continue;
								}
								goto IL_4F2;
							case 14:
							{
								string formulaErrorValue;
								return formulaErrorValue;
							}
							case 15:
								if (this.ᜀ(this.InnerNumberFormat))
								{
									num = 22;
									continue;
								}
								return text;
							case 16:
								if (!double.IsNaN(number))
								{
									num = 24;
									continue;
								}
								goto IL_4F2;
							case 17:
								if (((XlsWorksheet)this.Worksheet).HasSheetCalculation)
								{
									num = 31;
									continue;
								}
								goto IL_402;
							case 18:
								if (this.HasFormulaStringValue)
								{
									num = 49;
									continue;
								}
								return text;
							case 19:
								if (!((XlsWorksheet)this.Worksheet).HasSheetCalculation)
								{
									num = 37;
									continue;
								}
								goto IL_366;
							case 20:
							{
								CellFormatType cellFormatType;
								if (cellFormatType == CellFormatType.General)
								{
									num = 50;
									continue;
								}
								goto IL_121;
							}
							case 21:
								if (!this.HasFormulaArray)
								{
									goto IL_402;
								}
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_31A;
								default:
									if (false)
									{
									}
									num = 3;
									continue;
								}
								break;
							case 22:
							{
								string name = Thread.CurrentThread.CurrentCulture.Name;
								CultureInfo provider = new CultureInfo(name);
								DateTime dateTime = DateTime.FromOADate(number);
								num = 39;
								continue;
							}
							case 23:
								goto IL_4F2;
							case 24:
								text = sprᤅ.ᜂ(number);
								num = 23;
								continue;
							case 25:
								if (double.IsNaN(number))
								{
									num = 44;
									continue;
								}
								goto IL_213;
							case 26:
								if (!this.\u171D.WindowTwo.ᜄ())
								{
									num = 29;
									continue;
								}
								goto IL_121;
							case 27:
								num = 26;
								continue;
							case 28:
								goto IL_402;
							case 29:
							{
								CellFormatType cellFormatType = sprᤅ.ᜀ(0.0);
								num = 51;
								continue;
							}
							case 30:
								goto IL_186;
							case 31:
								num = 47;
								continue;
							case 32:
								if (this.HasFormulaBoolValue)
								{
									num = 7;
									continue;
								}
								num = 45;
								continue;
							case 33:
							{
								DateTime dateTime;
								CultureInfo provider;
								text2 = dateTime.ToString(provider);
								goto IL_5E3;
							}
							case 34:
							{
								bool hasFormulaErrorValue;
								if (hasFormulaErrorValue)
								{
									num = 14;
									continue;
								}
								goto IL_213;
							}
							case 35:
								num = 19;
								continue;
							case 36:
								goto IL_361;
							case 37:
								num = 32;
								continue;
							case 38:
								goto IL_121;
							case 39:
								if (!sprᤅ.ᜁ(number))
								{
									num = 42;
									continue;
								}
								num = 33;
								continue;
							case 40:
								num = 21;
								continue;
							case 41:
								if (this.ContainsNumber)
								{
									num = 46;
									continue;
								}
								text = this.InnerNumberFormat.ᜁ(this.GetDisplayString());
								num = 43;
								continue;
							case 42:
								num = 12;
								continue;
							case 43:
								goto IL_2B7;
							case 44:
								num = 34;
								continue;
							case 45:
								goto IL_31A;
							case 46:
							{
								bool hasFormulaErrorValue = this.HasFormulaErrorValue;
								string formulaErrorValue = this.FormulaErrorValue;
								num = 0;
								continue;
							}
							case 47:
								if (!this.HasFormula)
								{
									num = 40;
									continue;
								}
								goto IL_2BC;
							case 48:
								num = 15;
								continue;
							case 49:
								text = this.FormulaStringValue;
								num = 6;
								continue;
							case 50:
								goto IL_1E1;
							case 51:
							{
								CellFormatType cellFormatType;
								if (cellFormatType != CellFormatType.Number)
								{
									num = 9;
									continue;
								}
								goto IL_1E1;
							}
							}
							break;
							IL_121:
							num = 13;
							continue;
							IL_1E1:
							text = this.GetDisplayString();
							num = 38;
							continue;
							IL_213:
							sprᤅ = this.InnerNumberFormat;
							text = null;
							num = 4;
							continue;
							IL_31A:
							if (this.HasFormulaErrorValue)
							{
								num = 8;
								continue;
							}
							num = 18;
							continue;
							IL_2BC:
							double.TryParse(this.EnvalutedValue, out number);
							num = 28;
							continue;
							IL_366:
							number = this.GetNumber();
							num = 17;
							continue;
							IL_402:
							num = 25;
							continue;
							IL_4F2:
							num = 11;
							continue;
							IL_5E3:
							text = text2;
							num = 5;
						}
					}
					IL_186:
					IL_2B7:
					IL_361:
					IL_533:
					return text;
				}
				}
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000813C4 File Offset: 0x000803C4
		private bool ᜀ(sprᤅ A_0)
		{
			int a_ = 9;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_72;
				case 1:
					if (Thread.CurrentThread.CurrentCulture.Name != RecordTableEnumerator.b("娾⽀湂၄ᑆ", a_))
					{
						num = 0;
						continue;
					}
					return false;
				case 2:
					goto IL_3C;
				case 3:
					num = 1;
					continue;
				case 4:
					if (Array.IndexOf<string>(this.ᜤ, A_0.ᜂ()) >= 0)
					{
						num = 3;
						continue;
					}
					return false;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 4;
				}
			}
			IL_3C:
			goto IL_9D;
			IL_72:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9D:
				throw new ArgumentNullException(RecordTableEnumerator.b("瘾⽀ⵂ⁄㕆݈㹊⁌ⵎ㑐⅒ፔ㡖⭘㙚㱜⭞", a_));
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x000814B4 File Offset: 0x000804B4
		public IXLSRange EndCell
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					this.ᜆ();
					if (this.IsSingleCell)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					goto IL_40;
				}
				if (false)
				{
				}
				return this;
				IL_40:
				return this.\u171D.InnerGetCell(this.LastColumn, this.LastRow);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00081518 File Offset: 0x00080518
		public IXLSRange EntireColumn
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
				this.ᜆ();
				int row = 1;
				int maxRowCount = this.m_book.MaxRowCount;
				return this[row, this.FirstColumn, maxRowCount, this.LastColumn];
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0008157C File Offset: 0x0008057C
		public IXLSRange EntireRow
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
				this.ᜆ();
				int column = 1;
				int maxColumnCount = this.m_book.MaxColumnCount;
				return this[this.FirstRow, column, this.FirstRow, maxColumnCount];
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000815E0 File Offset: 0x000805E0
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0008176C File Offset: 0x0008076C
		public string ErrorValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_47:
						this.ᜆ();
						string error = this.\u171D.GetError(this.Row, this.Column);
						for (;;)
						{
							IL_65:
							int num = 6;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_150;
								case 1:
								{
									int num2;
									num2++;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_65;
									default:
										if (false)
										{
										}
										num = 4;
										continue;
									}
									break;
								}
								case 2:
									goto IL_FB;
								case 3:
									goto IL_7C;
								case 4:
									goto IL_FB;
								case 5:
									return error;
								case 6:
								{
									if (error == null)
									{
										num = 8;
										continue;
									}
									int num2 = this.Row;
									int lastRow = this.LastRow;
									num = 2;
									continue;
								}
								case 7:
								{
									int num2;
									int num3;
									if (error != this.\u171D.GetError(num2, num3))
									{
										num = 0;
										continue;
									}
									num3++;
									num = 3;
									continue;
								}
								case 8:
									goto IL_7A;
								case 9:
									goto IL_7C;
								case 10:
								{
									int num2;
									int lastRow;
									if (num2 > lastRow)
									{
										num = 5;
										continue;
									}
									int num3 = this.Column;
									int lastColumn = this.LastColumn;
									num = 9;
									continue;
								}
								case 11:
								{
									int num3;
									int lastColumn;
									if (num3 > lastColumn)
									{
										num = 1;
										continue;
									}
									num = 7;
									continue;
								}
								}
								goto IL_47;
								IL_7C:
								num = 11;
								continue;
								IL_FB:
								num = 10;
							}
						}
					}
					IL_7A:
					return null;
					IL_150:
					if (true)
					{
					}
					return null;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							for (;;)
							{
								int num2;
								num2++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_112;
								}
							}
							IL_112:
							if (false)
							{
							}
							num = 3;
							continue;
						case 1:
							goto IL_4E;
						case 2:
							goto IL_53;
						case 3:
							goto IL_C9;
						case 4:
							goto IL_C9;
						case 5:
						{
							if (true)
							{
							}
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 0;
								continue;
							}
							int num2;
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.ErrorValue = value;
							num3++;
							num = 6;
							continue;
						}
						case 6:
							goto IL_53;
						case 7:
							return;
						case 8:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 7;
								continue;
							}
							int num3 = this.FirstColumn;
							num = 2;
							continue;
						}
						case 9:
						{
							if (this.IsSingleCell)
							{
								num = 1;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 4;
							continue;
						}
						}
						break;
						IL_53:
						num = 5;
						continue;
						IL_C9:
						num = 8;
					}
				}
				IL_4E:
				this.SetError(value);
				this.SetChanged();
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000818B8 File Offset: 0x000808B8
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00081B5C File Offset: 0x00080B5C
		public string Formula
		{
			get
			{
				int a_ = 5;
				switch (0)
				{
				default:
				{
					string text;
					for (;;)
					{
						this.ᜆ();
						text = null;
						int num = 9;
						for (;;)
						{
							string text2;
							int num2;
							switch (num)
							{
							case 0:
								text2 = this.\u171D.GetFormula(this.Row, this.Column, false);
								goto IL_1AA;
							case 1:
								return text;
							case 2:
								text = null;
								goto IL_152;
							case 3:
								text2 = string.Format(RecordTableEnumerator.b("䀺䘼䐾煀㹂㡄㩆", a_), this.FormulaArray);
								goto IL_1AA;
							case 4:
							{
								int lastRow;
								if (num2 > lastRow)
								{
									num = 1;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_152;
								default:
									if (false)
									{
									}
									num = 6;
									continue;
								}
								break;
							}
							case 5:
								num = 7;
								continue;
							case 6:
								goto IL_1BC;
							case 7:
								if (!this.HasFormulaArray)
								{
									num = 11;
									continue;
								}
								num = 3;
								continue;
							case 8:
								if (text != null)
								{
									num = 10;
									continue;
								}
								return text;
							case 9:
							{
								if (this.IsSingleCell)
								{
									num = 5;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								spr_u24F.ᜀ(this.Row, this.Column);
								text = spr_u24F.Formula;
								num = 8;
								continue;
							}
							case 10:
							{
								num2 = this.Row;
								int lastRow = this.LastRow;
								num = 14;
								continue;
							}
							case 11:
								num = 0;
								continue;
							case 12:
								goto IL_1BC;
							case 13:
								goto IL_16A;
							case 14:
								goto IL_16A;
							case 15:
								goto IL_118;
							case 16:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 19;
									continue;
								}
								if (true)
								{
								}
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								string formula = spr_u24F.Formula;
								num = 18;
								continue;
							}
							case 17:
								return text;
							case 18:
							{
								string formula;
								if (text != formula)
								{
									num = 2;
									continue;
								}
								int num3;
								num3++;
								num = 12;
								continue;
							}
							case 19:
								goto IL_118;
							}
							break;
							IL_118:
							num2++;
							num = 13;
							continue;
							IL_152:
							num = 15;
							continue;
							IL_16A:
							num = 4;
							continue;
							IL_1AA:
							text = text2;
							num = 17;
							continue;
							IL_1BC:
							num = 16;
						}
					}
					return text;
				}
				}
			}
			set
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_84;
					default:
					{
						if (false)
						{
						}
						this.ᜆ();
						this.ᜉ();
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_82;
							case 1:
								value = '=' + value;
								if (true)
								{
								}
								num = 0;
								continue;
							case 2:
								if (value[0] != '=')
								{
									num = 1;
									continue;
								}
								goto IL_84;
							}
							break;
						}
						break;
					}
					}
				}
				IL_82:
				IL_84:
				this.Value = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00081BF4 File Offset: 0x00080BF4
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00081C3C File Offset: 0x00080C3C
		public string FormulaArray
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
				this.ᜆ();
				return this.ᜀ(false);
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
				this.ᜆ();
				this.ᜀ(value, false);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00081C88 File Offset: 0x00080C88
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x00081E48 File Offset: 0x00080E48
		public string FormulaStringValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					string formulaStringValue;
					for (;;)
					{
						this.ᜆ();
						formulaStringValue = this.\u171D.GetFormulaStringValue(this.Row, this.Column);
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (formulaStringValue != null)
								{
									num = 8;
									continue;
								}
								return formulaStringValue;
							case 1:
								goto IL_FA;
							case 2:
								goto IL_8C;
							case 3:
								if (!this.IsSingleCell)
								{
									num = 1;
									continue;
								}
								return formulaStringValue;
							case 4:
							{
								int num2;
								int num3;
								if (formulaStringValue != this.\u171D.GetFormulaStringValue(num2, num3))
								{
									num = 10;
									continue;
								}
								if (true)
								{
								}
								num3++;
								num = 13;
								continue;
							}
							case 5:
								goto IL_141;
							case 6:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 9;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num = 2;
								continue;
							}
							case 7:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 12;
									continue;
								}
								num = 4;
								continue;
							}
							case 8:
							{
								int num2 = this.Row;
								int lastRow = this.LastRow;
								num = 11;
								continue;
							}
							case 9:
								goto IL_15D;
							case 10:
								goto IL_18C;
							case 11:
								goto IL_141;
							case 12:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FA;
								default:
								{
									if (false)
									{
									}
									int num2;
									num2++;
									num = 5;
									continue;
								}
								}
								break;
							case 13:
								goto IL_8C;
							}
							break;
							IL_8C:
							num = 7;
							continue;
							IL_FA:
							num = 0;
							continue;
							IL_141:
							num = 6;
						}
					}
					IL_15D:
					return formulaStringValue;
					IL_18C:
					return null;
				}
				}
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 7;
					for (;;)
					{
						int num3;
						switch (num)
						{
						case 0:
							goto IL_53;
						case 1:
						{
							if (true)
							{
							}
							int num2;
							if (num2 > this.LastColumn)
							{
								num = 3;
								continue;
							}
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num3, num2);
							spr_u24F.FormulaStringValue = value;
							num2++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_74;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						}
						case 2:
						{
							if (num3 > this.LastRow)
							{
								num = 6;
								continue;
							}
							int num2 = this.FirstColumn;
							num = 0;
							continue;
						}
						case 3:
							goto IL_74;
						case 4:
							goto IL_53;
						case 5:
							goto IL_DB;
						case 6:
							return;
						case 7:
						{
							if (this.IsSingleCell)
							{
								num = 9;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							num3 = this.FirstRow;
							num = 5;
							continue;
						}
						case 8:
							goto IL_DB;
						case 9:
							goto IL_4E;
						}
						break;
						IL_53:
						num = 1;
						continue;
						IL_74:
						num3++;
						num = 8;
						continue;
						IL_DB:
						num = 2;
					}
				}
				IL_4E:
				this.\u171D.CellRecords.SetStringValue(this.CellIndex, value);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00081F9C File Offset: 0x00080F9C
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x0008216C File Offset: 0x0008116C
		public double FormulaNumberValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					double formulaNumberValue;
					for (;;)
					{
						this.ᜆ();
						formulaNumberValue = this.\u171D.GetFormulaNumberValue(this.Row, this.Column);
						int num = 7;
						for (;;)
						{
							int num2;
							int num3;
							int lastColumn;
							double formulaNumberValue2;
							switch (num)
							{
							case 0:
								goto IL_8F;
							case 1:
							{
								int lastRow;
								if (num2 > lastRow)
								{
									num = 3;
									continue;
								}
								num3 = this.Column;
								lastColumn = this.LastColumn;
								num = 0;
								continue;
							}
							case 2:
								num2++;
								if (true)
								{
								}
								num = 6;
								continue;
							case 3:
								goto IL_16D;
							case 4:
								goto IL_19B;
							case 5:
								goto IL_8F;
							case 6:
								goto IL_151;
							case 7:
								if (!this.IsSingleCell)
								{
									num = 13;
									continue;
								}
								return formulaNumberValue;
							case 8:
								goto IL_151;
							case 9:
								if (formulaNumberValue != formulaNumberValue2)
								{
									num = 4;
									continue;
								}
								num3++;
								num = 5;
								continue;
							case 10:
								if (!double.IsNaN(formulaNumberValue))
								{
									num = 11;
									continue;
								}
								return formulaNumberValue;
							case 11:
							{
								num2 = this.Row;
								int lastRow = this.LastRow;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_9B;
								default:
									if (false)
									{
									}
									num = 8;
									continue;
								}
								break;
							}
							case 12:
								goto IL_9B;
							case 13:
								num = 10;
								continue;
							}
							break;
							IL_8F:
							num = 12;
							continue;
							IL_9B:
							if (num3 > lastColumn)
							{
								num = 2;
								continue;
							}
							formulaNumberValue2 = this.\u171D.GetFormulaNumberValue(num2, num3);
							num = 9;
							continue;
							IL_151:
							num = 1;
						}
					}
					IL_16D:
					return formulaNumberValue;
					IL_19B:
					return double.NaN;
				}
				}
			}
			set
			{
				int a_ = 8;
				switch (0)
				{
				default:
				{
					spr᱒ spr᱒;
					for (;;)
					{
						this.ᜆ();
						int num = 11;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_75;
							case 1:
								goto IL_132;
							case 2:
							{
								int num2;
								if (num2 > this.LastColumn)
								{
									num = 9;
									continue;
								}
								spr\u24F1 spr_u24F;
								int num3;
								spr_u24F.ᜀ(num3, num2);
								spr_u24F.FormulaNumberValue = value;
								num2++;
								num = 6;
								continue;
							}
							case 3:
								spr᱒ = (this.Record as spr᱒);
								num = 7;
								continue;
							case 4:
							{
								int num3;
								if (num3 <= this.LastRow)
								{
									int num2 = this.FirstColumn;
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
									num = 8;
									continue;
								}
								break;
							}
							case 5:
								goto IL_10C;
							case 6:
								goto IL_75;
							case 7:
								if (true)
								{
								}
								if (spr᱒ == null)
								{
									num = 5;
									continue;
								}
								goto IL_19B;
							case 8:
								return;
							case 9:
							{
								int num3;
								num3++;
								num = 10;
								continue;
							}
							case 10:
								goto IL_132;
							case 11:
							{
								if (this.IsSingleCell)
								{
									num = 3;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num3 = this.FirstRow;
								num = 1;
								continue;
							}
							}
							break;
							IL_75:
							num = 2;
							continue;
							IL_132:
							num = 4;
						}
					}
					IL_10C:
					throw new NotSupportedException(RecordTableEnumerator.b("氽ℿⱁ⍃⍅㭇橉ⵋ㱍㕏牑㩓㥕ⱗ穙㩛ㅝ቟ཡᅣ੥१䩩ṫ཭ṯᕱᅳյ噷", a_));
					IL_19B:
					spr᱒.ᜀ(value);
					this.Record = spr᱒;
					return;
				}
				}
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00082324 File Offset: 0x00081324
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x000824E4 File Offset: 0x000814E4
		public bool FormulaBoolValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					bool formulaBoolValue;
					for (;;)
					{
						this.ᜆ();
						formulaBoolValue = this.\u171D.GetFormulaBoolValue(this.Row, this.Column);
						int num = 9;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2 = this.Row;
								int lastRow = this.LastRow;
								num = 7;
								continue;
							}
							case 1:
								if (formulaBoolValue)
								{
									num = 0;
									continue;
								}
								return formulaBoolValue;
							case 2:
							{
								int num2;
								int num3;
								if (formulaBoolValue != this.\u171D.GetFormulaBoolValue(num2, num3))
								{
									num = 6;
									continue;
								}
								num3++;
								num = 10;
								continue;
							}
							case 3:
								goto IL_160;
							case 4:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 11;
									continue;
								}
								num = 2;
								continue;
							}
							case 5:
								goto IL_105;
							case 6:
								return false;
							case 7:
								goto IL_144;
							case 8:
								goto IL_144;
							case 9:
								if (!this.IsSingleCell)
								{
									if (true)
									{
									}
									num = 5;
									continue;
								}
								return formulaBoolValue;
							case 10:
								goto IL_97;
							case 11:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_105;
								default:
								{
									if (false)
									{
									}
									int num2;
									num2++;
									num = 8;
									continue;
								}
								}
								break;
							case 12:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 3;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num = 13;
								continue;
							}
							case 13:
								goto IL_97;
							}
							break;
							IL_97:
							num = 4;
							continue;
							IL_105:
							num = 1;
							continue;
							IL_144:
							num = 12;
						}
					}
					IL_160:
					return formulaBoolValue;
				}
				}
			}
			set
			{
				int a_ = 3;
				switch (0)
				{
				default:
				{
					spr᱒ spr᱒;
					for (;;)
					{
						this.ᜆ();
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (spr᱒ == null)
								{
									num = 5;
									continue;
								}
								goto IL_19B;
							case 1:
								if (true)
								{
								}
								goto IL_132;
							case 2:
							{
								int num2;
								if (num2 <= this.LastRow)
								{
									int num3 = this.FirstColumn;
									num = 11;
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
									num = 10;
									continue;
								}
								break;
							}
							case 3:
							{
								if (this.IsSingleCell)
								{
									num = 6;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num2 = this.FirstRow;
								num = 9;
								continue;
							}
							case 4:
							{
								int num2;
								num2++;
								num = 1;
								continue;
							}
							case 5:
								goto IL_10C;
							case 6:
								spr᱒ = (this.Record as spr᱒);
								num = 0;
								continue;
							case 7:
								goto IL_75;
							case 8:
							{
								int num3;
								if (num3 > this.LastColumn)
								{
									num = 4;
									continue;
								}
								int num2;
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								spr_u24F.FormulaBoolValue = value;
								num3++;
								num = 7;
								continue;
							}
							case 9:
								goto IL_132;
							case 10:
								return;
							case 11:
								goto IL_75;
							}
							break;
							IL_75:
							num = 8;
							continue;
							IL_132:
							num = 2;
						}
					}
					IL_10C:
					throw new NotSupportedException(RecordTableEnumerator.b("欸娺匼堾⑀あ敄♆㭈⹊浌ⅎ㹐❒畔ㅖ㙘⥚ぜ⩞ൠɢ䕤ᕦࡨժ੬੮ɰ嵲", a_));
					IL_19B:
					spr᱒.ᜂ(value);
					this.Record = spr᱒;
					return;
				}
				}
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0008269C File Offset: 0x0008169C
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0008285C File Offset: 0x0008185C
		public string FormulaErrorValue
		{
			get
			{
				if (true)
				{
				}
				switch (0)
				{
				default:
				{
					string formulaErrorValue;
					for (;;)
					{
						this.ᜆ();
						formulaErrorValue = this.\u171D.GetFormulaErrorValue(this.Row, this.Column);
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2 = this.Row;
								int lastRow = this.LastRow;
								num = 3;
								continue;
							}
							case 1:
								goto IL_141;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_102;
								default:
								{
									if (false)
									{
									}
									int num2;
									num2++;
									num = 1;
									continue;
								}
								}
								break;
							case 3:
								goto IL_141;
							case 4:
								goto IL_15D;
							case 5:
								goto IL_94;
							case 6:
								if (!this.IsSingleCell)
								{
									num = 10;
									continue;
								}
								return formulaErrorValue;
							case 7:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 2;
									continue;
								}
								num = 12;
								continue;
							}
							case 8:
								if (formulaErrorValue != null)
								{
									num = 0;
									continue;
								}
								return formulaErrorValue;
							case 9:
								goto IL_94;
							case 10:
								goto IL_102;
							case 11:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 4;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num = 9;
								continue;
							}
							case 12:
							{
								int num2;
								int num3;
								if (formulaErrorValue != this.\u171D.GetFormulaErrorValue(num2, num3))
								{
									num = 13;
									continue;
								}
								num3++;
								num = 5;
								continue;
							}
							case 13:
								goto IL_18C;
							}
							break;
							IL_94:
							num = 7;
							continue;
							IL_102:
							num = 8;
							continue;
							IL_141:
							num = 11;
						}
					}
					IL_15D:
					return formulaErrorValue;
					IL_18C:
					return null;
				}
				}
			}
			set
			{
				int a_ = 16;
				switch (0)
				{
				default:
				{
					int num2;
					spr᱒ spr᱒;
					for (;;)
					{
						this.ᜆ();
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								IL_1C0:
								if (num2 == -1)
								{
									num = 8;
									continue;
								}
								goto IL_19C;
							case 1:
							{
								if (this.IsSingleCell)
								{
									num = 12;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num3 = this.FirstRow;
								num = 11;
								continue;
							}
							case 2:
								return;
							case 3:
							{
								int num4;
								if (num4 > this.LastColumn)
								{
									num = 14;
									continue;
								}
								spr\u24F1 spr_u24F;
								int num3;
								spr_u24F.ᜀ(num3, num4);
								spr_u24F.FormulaErrorValue = value;
								num4++;
								num = 13;
								continue;
							}
							case 4:
								if (spr᱒ == null)
								{
									num = 5;
									continue;
								}
								num2 = this.ᜃ(value);
								num = 0;
								continue;
							case 5:
								goto IL_12A;
							case 6:
								goto IL_12C;
							case 7:
							{
								int num3;
								if (num3 > this.LastRow)
								{
									num = 2;
									continue;
								}
								int num4 = this.FirstColumn;
								num = 6;
								continue;
							}
							case 8:
								num2 = 0;
								num = 10;
								continue;
							case 9:
								goto IL_15D;
							case 10:
								goto IL_1F9;
							case 11:
								goto IL_15D;
							case 12:
								spr᱒ = (this.Record as spr᱒);
								num = 4;
								continue;
							case 13:
								goto IL_12C;
							case 14:
							{
								if (true)
								{
								}
								int num3;
								num3++;
								num = 9;
								continue;
							}
							}
							break;
							IL_12C:
							num = 3;
							continue;
							IL_15D:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1C0;
							default:
								if (false)
								{
								}
								num = 7;
								break;
							}
						}
					}
					IL_12A:
					throw new NotSupportedException(RecordTableEnumerator.b("ᑅ⥇⑉⭋⭍⍏牑㕓⑕㵗穙㉛ㅝᑟ䉡ɣ॥ᩧݩᥫɭᅯ剱ٳ᝵ᙷᵹ᥻ൽ깿", a_));
					IL_19C:
					spr᱒.ᜀ((byte)num2);
					this.Record = spr᱒;
					return;
					IL_1F9:
					goto IL_19C;
				}
				}
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00082A64 File Offset: 0x00081A64
		public object FormulaValue
		{
			get
			{
				int num = 10;
				object result;
				for (;;)
				{
					string formulaStringValue;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D5;
						default:
							goto IL_12B;
						}
						break;
					case 1:
						if (this.HasFormulaDateTime)
						{
							num = 12;
							continue;
						}
						if (true)
						{
						}
						num = 6;
						continue;
					case 2:
						return result;
					case 3:
						formulaStringValue = this.FormulaStringValue;
						num = 15;
						continue;
					case 4:
						result = this.FormulaBoolValue;
						num = 5;
						continue;
					case 5:
						goto IL_90;
					case 6:
						if (this.HasFormulaBoolValue)
						{
							num = 4;
							continue;
						}
						num = 13;
						continue;
					case 7:
						goto IL_BE;
					case 8:
						goto IL_74;
					case 9:
						result = this.FormulaErrorValue;
						num = 8;
						continue;
					case 11:
						result = formulaStringValue;
						num = 14;
						continue;
					case 12:
						result = this.FormulaDateTime;
						num = 2;
						continue;
					case 13:
						if (this.HasFormulaErrorValue)
						{
							num = 9;
							continue;
						}
						result = this.FormulaNumberValue;
						num = 7;
						continue;
					case 14:
						goto IL_A2;
					case 15:
						goto IL_D5;
					}
					if (this.HasFormula)
					{
						num = 3;
						continue;
					}
					result = null;
					num = 0;
					continue;
					IL_D5:
					if (formulaStringValue != null)
					{
						num = 11;
					}
					else
					{
						num = 1;
					}
				}
				IL_74:
				IL_90:
				IL_A2:
				IL_BE:
				return result;
				IL_12B:
				if (false)
				{
				}
				return result;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00082C18 File Offset: 0x00081C18
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00082E20 File Offset: 0x00081E20
		public bool IsFormulaHidden
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = 11;
						for (;;)
						{
							bool isFormulaHidden;
							int num2;
							switch (num)
							{
							case 0:
								num = 1;
								continue;
							case 1:
								if (isFormulaHidden)
								{
									spr\u24F1 spr_u24F;
									int num3;
									spr_u24F.ᜀ(num2, num3);
									isFormulaHidden = spr_u24F.IsFormulaHidden;
									num3++;
									num = 12;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_192;
								default:
									if (false)
									{
									}
									num = 10;
									continue;
								}
								break;
							case 2:
								num = 4;
								continue;
							case 3:
								if (num2 <= this.LastRow)
								{
									num = 2;
									continue;
								}
								return isFormulaHidden;
							case 4:
								goto IL_192;
							case 5:
							{
								int num3;
								if (num3 <= this.LastColumn)
								{
									num = 0;
									continue;
								}
								goto IL_84;
							}
							case 6:
								num2 = this.FirstRow;
								num = 14;
								continue;
							case 7:
								goto IL_10E;
							case 8:
								return isFormulaHidden;
							case 9:
								if (isFormulaHidden)
								{
									num = 6;
									continue;
								}
								return isFormulaHidden;
							case 10:
								goto IL_84;
							case 11:
							{
								if (this.IsSingleCell)
								{
									if (true)
									{
									}
									num = 13;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								isFormulaHidden = this.\u171D[this.FirstRow, this.FirstColumn].IsFormulaHidden;
								num = 9;
								continue;
							}
							case 12:
								goto IL_96;
							case 13:
								goto IL_7F;
							case 14:
								goto IL_10E;
							case 15:
								goto IL_96;
							}
							break;
							IL_84:
							num2++;
							num = 7;
							continue;
							IL_96:
							num = 5;
							continue;
							IL_10E:
							num = 3;
							continue;
							IL_192:
							if (!isFormulaHidden)
							{
								num = 8;
							}
							else
							{
								int num3 = this.FirstColumn;
								num = 15;
							}
						}
					}
					IL_7F:
					return this.Style.FormulaHidden;
				}
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E8;
						case 1:
							goto IL_6F;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_FC;
							default:
								goto IL_64;
							}
							break;
						case 3:
						{
							int num2;
							num2++;
							num = 0;
							continue;
						}
						case 4:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								goto IL_FC;
							}
							int num3 = this.FirstColumn;
							num = 1;
							continue;
						}
						case 5:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 3;
								continue;
							}
							int num2;
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.IsFormulaHidden = value;
							num3++;
							num = 9;
							continue;
						}
						case 6:
						{
							if (this.IsSingleCell)
							{
								num = 2;
								continue;
							}
							if (true)
							{
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 7;
							continue;
						}
						case 7:
							goto IL_E8;
						case 8:
							return;
						case 9:
							goto IL_6F;
						}
						break;
						IL_6F:
						num = 5;
						continue;
						IL_E8:
						num = 4;
						continue;
						IL_FC:
						num = 8;
					}
				}
				IL_64:
				if (false)
				{
				}
				this.Style.FormulaHidden = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00082F6C File Offset: 0x00081F6C
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x000831BC File Offset: 0x000821BC
		public DateTime FormulaDateTime
		{
			get
			{
				switch (0)
				{
				default:
				{
					double formulaNumberValue;
					for (;;)
					{
						this.ᜆ();
						formulaNumberValue = this.\u171D.GetFormulaNumberValue(this.Row, this.Column);
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_11B;
							case 1:
								goto IL_13B;
							case 2:
							{
								double formulaNumberValue2;
								if (this.InnerNumberFormat.ᜀ(formulaNumberValue2) != CellFormatType.DateTime)
								{
									num = 11;
									continue;
								}
								int num2;
								num2++;
								num = 13;
								continue;
							}
							case 3:
							{
								if (this.InnerNumberFormat.ᜀ(formulaNumberValue) != CellFormatType.DateTime)
								{
									num = 8;
									continue;
								}
								int num3 = this.Row;
								int lastRow = this.LastRow;
								num = 0;
								continue;
							}
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_11B;
								default:
									if (false)
									{
									}
									if (formulaNumberValue != double.NaN)
									{
										num = 5;
										continue;
									}
									goto IL_12F;
								}
								break;
							case 5:
								num = 3;
								continue;
							case 6:
								goto IL_1D6;
							case 7:
							{
								int num3;
								int lastRow;
								if (num3 > lastRow)
								{
									num = 17;
									continue;
								}
								int num2 = this.Column;
								int lastColumn = this.LastColumn;
								num = 6;
								continue;
							}
							case 8:
								goto IL_1D1;
							case 9:
								num = 2;
								continue;
							case 10:
							{
								double formulaNumberValue2;
								if (formulaNumberValue2 != double.NaN)
								{
									num = 12;
									continue;
								}
								goto IL_135;
							}
							case 11:
								goto IL_FF;
							case 12:
								num = 14;
								continue;
							case 13:
								goto IL_1D6;
							case 14:
							{
								double formulaNumberValue2;
								if (formulaNumberValue == formulaNumberValue2)
								{
									num = 9;
									continue;
								}
								goto IL_135;
							}
							case 15:
							{
								int num3;
								num3++;
								num = 1;
								continue;
							}
							case 16:
							{
								int num2;
								int lastColumn;
								if (num2 > lastColumn)
								{
									num = 15;
									continue;
								}
								int num3;
								double formulaNumberValue2 = this.\u171D.GetFormulaNumberValue(num3, num2);
								num = 10;
								continue;
							}
							case 17:
								goto IL_15A;
							}
							break;
							IL_13B:
							num = 7;
							continue;
							IL_11B:
							goto IL_13B;
							IL_1D6:
							num = 16;
						}
					}
					IL_FF:
					goto IL_135;
					IL_12F:
					return DateTime.MinValue;
					IL_135:
					return DateTime.MinValue;
					IL_15A:
					if (true)
					{
					}
					return UtilityMethods.ᜀ(formulaNumberValue);
					IL_1D1:
					goto IL_12F;
				}
				}
			}
			set
			{
				int a_ = 6;
				for (;;)
				{
					this.ᜆ();
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6E;
						case 1:
							goto IL_111;
						case 2:
							goto IL_6E;
						case 3:
						{
							int num2;
							num2++;
							num = 1;
							continue;
						}
						case 4:
							num = 6;
							continue;
						case 5:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 3;
								continue;
							}
							int num2;
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.FormulaDateTime = value;
							num3++;
							num = 0;
							continue;
						}
						case 6:
							goto IL_CD;
						case 7:
						{
							if (this.IsSingleCell)
							{
								num = 4;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 11;
							continue;
						}
						case 8:
							goto IL_E4;
						case 9:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 10;
								continue;
							}
							int num3 = this.FirstColumn;
							num = 2;
							continue;
						}
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CD;
							default:
								goto IL_149;
							}
							break;
						case 11:
							goto IL_111;
						}
						break;
						IL_6E:
						num = 5;
						continue;
						IL_CD:
						if (this.CellType != XlsRange.TCellType.Formula)
						{
							num = 8;
							continue;
						}
						goto IL_177;
						IL_111:
						num = 9;
					}
				}
				IL_E4:
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃㙅㩇╉㱋⭍≏♑ⵓ癕ㅗ⥙籛ㅝ๟๡ᵣ䙥๧թṫ乭ᙯᵱٳ᭵൷ᙹᵻ幽黎", a_));
				IL_149:
				if (false)
				{
				}
				return;
				IL_177:
				this.FormatType = CellFormatType.DateTime;
				this.\u171D.SetFormulaNumberValue(this.Row, this.Column, value.ToOADate());
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00083368 File Offset: 0x00082368
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x000835D0 File Offset: 0x000825D0
		public string FormulaR1C1
		{
			get
			{
				int a_ = 9;
				switch (0)
				{
				default:
				{
					string text;
					for (;;)
					{
						this.ᜆ();
						text = null;
						int num = 11;
						for (;;)
						{
							int num3;
							switch (num)
							{
							case 0:
								goto IL_156;
							case 1:
							{
								string formulaR1C;
								if (text != formulaR1C)
								{
									num = 15;
									continue;
								}
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 2:
								goto IL_113;
							case 3:
								goto IL_175;
							case 4:
								if (text != null)
								{
									num = 10;
									continue;
								}
								return text;
							case 5:
								goto IL_1B9;
							case 6:
								goto IL_1C8;
							case 7:
								goto IL_113;
							case 8:
								goto IL_156;
							case 9:
								num = 14;
								continue;
							case 10:
							{
								num3 = this.Row;
								int lastRow = this.LastRow;
								num = 0;
								continue;
							}
							case 11:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_113;
								default:
								{
									if (false)
									{
									}
									if (this.IsSingleCell)
									{
										num = 9;
										continue;
									}
									spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
									spr_u24F.ᜀ(this.Row, this.Column);
									text = spr_u24F.FormulaR1C1;
									num = 4;
									continue;
								}
								}
								break;
							case 12:
							{
								int num2;
								int lastColumn;
								if (num2 > lastColumn)
								{
									num = 2;
									continue;
								}
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num3, num2);
								string formulaR1C = spr_u24F.FormulaR1C1;
								num = 1;
								continue;
							}
							case 13:
							{
								int lastRow;
								if (num3 > lastRow)
								{
									num = 3;
									continue;
								}
								int num2 = this.Column;
								int lastColumn = this.LastColumn;
								num = 16;
								continue;
							}
							case 14:
								if (!this.HasFormulaArray)
								{
									num = 5;
									continue;
								}
								goto IL_F9;
							case 15:
								if (true)
								{
								}
								text = null;
								num = 7;
								continue;
							case 16:
								goto IL_1C8;
							}
							break;
							IL_113:
							num3++;
							num = 8;
							continue;
							IL_156:
							num = 13;
							continue;
							IL_1C8:
							num = 12;
						}
					}
					IL_F9:
					return string.Format(RecordTableEnumerator.b("䐾㩀㡂畄㩆㑈㙊", a_), this.FormulaArrayR1C1);
					IL_175:
					return text;
					IL_1B9:
					return this.\u171D.GetFormula(this.Row, this.Column, true);
				}
				}
			}
			set
			{
				int a_ = 5;
				int num = 10;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_146;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15A;
						default:
							if (false)
							{
							}
							goto IL_64;
						}
						break;
					case 3:
						goto IL_122;
					case 4:
						if (true)
						{
						}
						if (value[0] == '=')
						{
							num = 5;
							continue;
						}
						goto IL_146;
					case 5:
						goto IL_15A;
					case 6:
					{
						if (num2 > this.m_iBottomRow)
						{
							num = 0;
							continue;
						}
						int num3 = this.m_iLeftColumn;
						num = 9;
						continue;
					}
					case 7:
					{
						int num3;
						if (num3 > this.m_iRightColumn)
						{
							num = 12;
							continue;
						}
						this.\u171D.SetFormula(num2, num3, value, true);
						num3++;
						num = 2;
						continue;
					}
					case 8:
						goto IL_62;
					case 9:
						goto IL_64;
					case 11:
						goto IL_122;
					case 12:
						num2++;
						num = 3;
						continue;
					}
					if (value == null)
					{
						num = 8;
						continue;
					}
					num = 4;
					continue;
					IL_64:
					num = 7;
					continue;
					IL_122:
					num = 6;
					continue;
					IL_146:
					num2 = this.m_iTopRow;
					num = 11;
					continue;
					IL_15A:
					value = value.Substring(1);
					num = 1;
				}
				IL_62:
				throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼匾㑀♂", a_));
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00083750 File Offset: 0x00082750
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x00083798 File Offset: 0x00082798
		public string FormulaArrayR1C1
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
				this.ᜆ();
				return this.ᜀ(true);
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
				this.ᜆ();
				this.ᜀ(value, true);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000837E4 File Offset: 0x000827E4
		public bool HasFormula
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_FC;
							case 1:
								goto IL_5E;
							case 2:
								return false;
							case 3:
								return true;
							case 4:
								goto IL_BF;
							case 5:
								if (true)
								{
								}
								goto IL_5E;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FC;
								default:
									if (false)
									{
									}
									goto IL_BF;
								}
								break;
							case 7:
							{
								if (num > lastRow)
								{
									num2 = 3;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 1;
								continue;
							}
							case 8:
							{
								XlsWorksheet.TRangeValueType cellType;
								if (cellType != XlsWorksheet.TRangeValueType.Formula)
								{
									num2 = 2;
									continue;
								}
								int num3;
								num3++;
								num2 = 5;
								continue;
							}
							case 9:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 0;
									continue;
								}
								XlsWorksheet.TRangeValueType cellType = this.\u171D.GetCellType(num, num3, false);
								num2 = 8;
								continue;
							}
							}
							break;
							IL_5E:
							num2 = 9;
							continue;
							IL_BF:
							num2 = 7;
							continue;
							IL_FC:
							num++;
							num2 = 6;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00083928 File Offset: 0x00082928
		public bool HasFormulaArray
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 6;
									continue;
								}
								num2 = 5;
								continue;
							}
							case 1:
								return true;
							case 2:
								goto IL_5E;
							case 3:
								return false;
							case 4:
								goto IL_DA;
							case 5:
							{
								int num3;
								if (!this.\u171D.HasArrayFormulaRecord(num, num3))
								{
									num2 = 3;
									continue;
								}
								num3++;
								num2 = 8;
								continue;
							}
							case 6:
								num++;
								num2 = 9;
								continue;
							case 7:
							{
								if (num > lastRow)
								{
									goto IL_EA;
								}
								if (true)
								{
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 2;
								continue;
							}
							case 8:
								goto IL_5E;
							case 9:
								goto IL_DA;
							}
							break;
							IL_5E:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_EA:
								num2 = 1;
								continue;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							IL_DA:
							num2 = 7;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00083A6C File Offset: 0x00082A6C
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00083AC4 File Offset: 0x00082AC4
		public HorizontalAlignType HorizontalAlignment
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					if (this.IsSingleCell)
					{
						break;
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
				return this.Style.HorizontalAlignment;
				IL_32:
				if (false)
				{
				}
				if (true)
				{
				}
				return HorizontalAlignType.General;
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_FC;
							default:
								goto IL_64;
							}
							break;
						case 2:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								goto IL_FC;
							}
							int num3 = this.FirstColumn;
							num = 6;
							continue;
						}
						case 3:
							goto IL_E8;
						case 4:
							goto IL_E8;
						case 5:
						{
							if (this.IsSingleCell)
							{
								num = 1;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 4;
							continue;
						}
						case 6:
							goto IL_6F;
						case 7:
							goto IL_6F;
						case 8:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 9;
								continue;
							}
							int num2;
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.HorizontalAlignment = value;
							num3++;
							if (true)
							{
							}
							num = 7;
							continue;
						}
						case 9:
						{
							int num2;
							num2++;
							num = 3;
							continue;
						}
						}
						break;
						IL_6F:
						num = 8;
						continue;
						IL_E8:
						num = 2;
						continue;
						IL_FC:
						num = 0;
					}
				}
				IL_64:
				if (false)
				{
				}
				this.Style.HorizontalAlignment = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00083C10 File Offset: 0x00082C10
		public IHyperLinks Hyperlinks
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
				XlsHyperLinksCollection xlsHyperLinksCollection = (XlsHyperLinksCollection)this.\u171D.HyperLinks;
				return xlsHyperLinksCollection.GetRangeHyperlinks(this);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00083C64 File Offset: 0x00082C64
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x00083CC0 File Offset: 0x00082CC0
		public int IndentLevel
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					if (this.IsSingleCell)
					{
						break;
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
				return this.Style.IndentLevel;
				IL_32:
				if (false)
				{
				}
				if (true)
				{
				}
				return int.MinValue;
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6F;
						case 1:
							goto IL_6F;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_FC;
							default:
								goto IL_64;
							}
							break;
						case 3:
							if (true)
							{
							}
							goto IL_E8;
						case 4:
						{
							if (this.IsSingleCell)
							{
								num = 2;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 3;
							continue;
						}
						case 5:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								goto IL_FC;
							}
							int num3 = this.FirstColumn;
							num = 0;
							continue;
						}
						case 6:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 7;
								continue;
							}
							spr\u24F1 spr_u24F;
							int num2;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.IndentLevel = value;
							num3++;
							num = 1;
							continue;
						}
						case 7:
						{
							int num2;
							num2++;
							num = 8;
							continue;
						}
						case 8:
							goto IL_E8;
						case 9:
							return;
						}
						break;
						IL_6F:
						num = 6;
						continue;
						IL_E8:
						num = 5;
						continue;
						IL_FC:
						num = 9;
					}
				}
				IL_64:
				if (false)
				{
				}
				this.Style.IndentLevel = (int)((ushort)value);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00083E0C File Offset: 0x00082E0C
		public bool HasError
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num > lastRow)
								{
									num2 = 8;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 6;
								continue;
							}
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FC;
								default:
									if (false)
									{
									}
									goto IL_BF;
								}
								break;
							case 2:
								return false;
							case 3:
								goto IL_5E;
							case 4:
								goto IL_BF;
							case 5:
							{
								XlsWorksheet.TRangeValueType cellType;
								if (cellType != XlsWorksheet.TRangeValueType.Error)
								{
									num2 = 2;
									continue;
								}
								int num3;
								num3++;
								num2 = 3;
								continue;
							}
							case 6:
								goto IL_5E;
							case 7:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 9;
									continue;
								}
								XlsWorksheet.TRangeValueType cellType = this.\u171D.GetCellType(num, num3, false);
								if (true)
								{
								}
								num2 = 5;
								continue;
							}
							case 8:
								return true;
							case 9:
								goto IL_FC;
							}
							break;
							IL_5E:
							num2 = 7;
							continue;
							IL_BF:
							num2 = 0;
							continue;
							IL_FC:
							num++;
							num2 = 1;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00083F50 File Offset: 0x00082F50
		public bool IsGroupedByColumn
		{
			get
			{
				switch (0)
				{
				default:
				{
					spr\u216E spr_u216E;
					for (;;)
					{
						this.ᜆ();
						int firstColumn = this.FirstColumn;
						int lastColumn = this.LastColumn;
						int num = 3;
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
								return false;
							case 1:
							{
								int num2;
								if (num2 > lastColumn)
								{
									num = 2;
									continue;
								}
								num = 4;
								continue;
							}
							case 2:
								return true;
							case 3:
							{
								if (firstColumn == lastColumn)
								{
									num = 6;
									continue;
								}
								int firstRow = this.FirstRow;
								int num2 = firstColumn;
								num = 8;
								continue;
							}
							case 4:
							{
								int num2;
								int firstRow;
								while (!this[firstRow, num2].IsGroupedByColumn)
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
										num = 9;
										goto IL_10;
									}
								}
								num2++;
								num = 5;
								continue;
							}
							case 5:
								goto IL_CA;
							case 6:
								if (true)
								{
								}
								spr_u216E = this.\u171D.ColumnInformation[firstColumn];
								num = 7;
								continue;
							case 7:
								if (spr_u216E == null)
								{
									num = 0;
									continue;
								}
								goto IL_140;
							case 8:
								goto IL_CA;
							case 9:
								return false;
							}
							break;
							IL_CA:
							num = 1;
						}
					}
					return false;
					IL_140:
					return spr_u216E.ᜊ() != 0;
				}
				}
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x000840AC File Offset: 0x000830AC
		public bool IsGroupedByRow
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
					spr\u2502 spr_u;
					for (;;)
					{
						this.ᜆ();
						int firstRow = this.FirstRow;
						int lastRow = this.LastRow;
						int num = 2;
						for (;;)
						{
							IL_18:
							switch (num)
							{
							case 0:
							{
								int num2;
								int firstColumn;
								while (!this[num2, firstColumn].IsGroupedByRow)
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
										num = 5;
										goto IL_18;
									}
								}
								num2++;
								num = 6;
								continue;
							}
							case 1:
								if (spr_u == null)
								{
									num = 3;
									continue;
								}
								goto IL_13F;
							case 2:
							{
								if (firstRow == lastRow)
								{
									num = 4;
									continue;
								}
								int firstColumn = this.FirstColumn;
								int num2 = firstRow;
								num = 8;
								continue;
							}
							case 3:
								return false;
							case 4:
								spr_u = this.\u171D.ᜋ(firstRow);
								num = 1;
								continue;
							case 5:
								return false;
							case 6:
								goto IL_CA;
							case 7:
							{
								int num2;
								if (num2 > lastRow)
								{
									num = 9;
									continue;
								}
								num = 0;
								continue;
							}
							case 8:
								goto IL_CA;
							case 9:
								return true;
							}
							break;
							IL_CA:
							num = 7;
						}
					}
					return false;
					IL_13F:
					return spr_u.ᜀ() != 0;
				}
				}
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00084208 File Offset: 0x00083208
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x0008424C File Offset: 0x0008324C
		public int LastColumn
		{
			[DebuggerStepThrough]
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
				return this.m_iRightColumn;
			}
			set
			{
				int a_ = 19;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_C6;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C6;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 2:
						goto IL_BC;
					case 4:
						if (value != this.LastColumn)
						{
							num = 0;
							continue;
						}
						return;
					case 5:
						if (value > this.m_book.MaxColumnCount)
						{
							num = 2;
							continue;
						}
						num = 4;
						continue;
					case 6:
						return;
					}
					if (value >= 1)
					{
						num = 1;
						continue;
					}
					break;
					IL_C6:
					this.m_iRightColumn = value;
					this.OnLastColumnChanged();
					num = 6;
				}
				IL_7A:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Ո⩊㹌㭎ቐ㱒㥔≖㑘㕚", a_));
				IL_BC:
				if (true)
				{
				}
				goto IL_7A;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0008433C File Offset: 0x0008333C
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00084380 File Offset: 0x00083380
		public int LastRow
		{
			[DebuggerStepThrough]
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
				return this.m_iBottomRow;
			}
			set
			{
				int a_ = 1;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value > this.m_book.MaxRowCount)
						{
							num = 4;
							continue;
						}
						num = 2;
						continue;
					case 2:
						if (value != this.LastRow)
						{
							if (true)
							{
							}
							num = 5;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						goto IL_C4;
					case 5:
						goto IL_C6;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C6;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (value >= 1)
					{
						num = 6;
						continue;
					}
					break;
					IL_C6:
					this.m_iBottomRow = value;
					this.OnLastRowChanged();
					num = 3;
				}
				IL_82:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("笶堸䠺䤼派⹀㑂", a_));
				IL_C4:
				goto IL_82;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00084470 File Offset: 0x00083470
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0008460C File Offset: 0x0008360C
		public double NumberValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					double number;
					for (;;)
					{
						this.ᜆ();
						number = this.\u171D.GetNumber(this.Row, this.Column);
						int num = 9;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_85;
							case 1:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 5;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num = 0;
								continue;
							}
							case 2:
								goto IL_119;
							case 3:
								goto IL_167;
							case 4:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 7;
									continue;
								}
								num = 10;
								continue;
							}
							case 5:
								goto IL_135;
							case 6:
								return number;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_167;
								default:
								{
									if (false)
									{
									}
									int num2;
									num2++;
									num = 11;
									continue;
								}
								}
								break;
							case 8:
								goto IL_85;
							case 9:
							{
								if (number == double.NaN)
								{
									num = 6;
									continue;
								}
								int num2 = this.Row;
								int lastRow = this.LastRow;
								num = 2;
								continue;
							}
							case 10:
							{
								int num2;
								int num3;
								if (number != this.\u171D.GetNumber(num2, num3))
								{
									num = 3;
									continue;
								}
								num3++;
								num = 8;
								continue;
							}
							case 11:
								goto IL_119;
							}
							break;
							IL_85:
							num = 4;
							continue;
							IL_119:
							num = 1;
						}
					}
					return number;
					IL_135:
					if (true)
					{
					}
					return number;
					IL_167:
					return double.NaN;
				}
				}
			}
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						this.ᜆ();
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int num2;
								num2++;
								num = 4;
								continue;
							}
							case 1:
								goto IL_150;
							case 2:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 0;
									continue;
								}
								int num2;
								this[num2, num3].NumberValue = value;
								num3++;
								num = 1;
								continue;
							}
							case 3:
								num = 7;
								continue;
							case 4:
								goto IL_171;
							case 5:
								value = 0.0;
								num = 11;
								continue;
							case 6:
							{
								if (this.IsSingleCell)
								{
									num = 3;
									continue;
								}
								this.ᜉ();
								int num2 = this.FirstRow;
								int lastRow = this.LastRow;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1B9;
								default:
									if (false)
									{
									}
									num = 10;
									continue;
								}
								break;
							}
							case 7:
								if (BitConverter.DoubleToInt64Bits(value) == BitConverter.DoubleToInt64Bits(0.0))
								{
									num = 5;
									continue;
								}
								goto IL_19D;
							case 8:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 9;
									continue;
								}
								int num3 = this.FirstColumn;
								int lastColumn = this.LastColumn;
								num = 13;
								continue;
							}
							case 9:
								return;
							case 10:
								goto IL_171;
							case 11:
								goto IL_19D;
							case 12:
								goto IL_20A;
							case 13:
								goto IL_150;
							case 14:
								this.ᜀ(this.NumberValue, value, this);
								num = 12;
								continue;
							case 15:
								if (this.NumberValue != value)
								{
									goto IL_1B9;
								}
								goto IL_18F;
							}
							break;
							IL_150:
							num = 2;
							continue;
							IL_171:
							num = 8;
							continue;
							IL_19D:
							double numberValue = this.NumberValue;
							num = 15;
							continue;
							IL_1B9:
							num = 14;
						}
					}
					return;
					IL_18F:
					this.ᜁ(value);
					this.SetChanged();
					return;
					IL_20A:
					goto IL_18F;
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x00084828 File Offset: 0x00083828
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x00084888 File Offset: 0x00083888
		public string NumberFormat
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					if (!this.IsSingleCell)
					{
						goto IL_45;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
				}
				IL_24:
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ();
				IL_45:
				return sprṔ.ᜀ(this.CellsList);
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_DE;
						case 1:
							return;
						case 2:
						{
							if (this.IsSingleCell)
							{
								num = 7;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 4;
							continue;
						}
						case 3:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 1;
								continue;
							}
							int num3 = this.FirstColumn;
							num = 5;
							continue;
						}
						case 4:
							goto IL_DE;
						case 5:
							goto IL_53;
						case 6:
						{
							int num2;
							num2++;
							num = 0;
							continue;
						}
						case 7:
							goto IL_4E;
						case 8:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								goto IL_80;
							}
							spr\u24F1 spr_u24F;
							int num2;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.NumberFormat = value;
							num3++;
							num = 9;
							continue;
						}
						case 9:
							goto IL_53;
						}
						break;
						IL_53:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_80:
							if (true)
							{
							}
							num = 6;
							continue;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						IL_DE:
						num = 3;
					}
				}
				IL_4E:
				this.Style.NumberFormat = value;
				this.SetChanged();
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x000849DC File Offset: 0x000839DC
		public int Row
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
				this.ᜆ();
				return this.FirstRow;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00084A24 File Offset: 0x00083A24
		public int RowGroupLevel
		{
			get
			{
				switch (0)
				{
				default:
				{
					spr\u2502 spr_u;
					for (;;)
					{
						this.ᜆ();
						int firstRow = this.FirstRow;
						int lastRow = this.LastRow;
						int num = 7;
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
								spr_u = sprᜑ.ᜀ(this.\u171D, firstRow - 1, false);
								num = 5;
								continue;
							case 1:
								goto IL_C4;
							case 2:
							{
								int num2;
								if (num2 > lastRow)
								{
									num = 8;
									continue;
								}
								num = 6;
								continue;
							}
							case 3:
								return 0;
							case 4:
								goto IL_C4;
							case 5:
								if (spr_u == null)
								{
									num = 3;
									continue;
								}
								goto IL_158;
							case 6:
							{
								int num2;
								int rowGroupLevel;
								int firstColumn;
								while (rowGroupLevel != this[num2, firstColumn].RowGroupLevel)
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
										num = 9;
										goto IL_10;
									}
								}
								if (true)
								{
								}
								num2++;
								num = 4;
								continue;
							}
							case 7:
							{
								if (firstRow == lastRow)
								{
									num = 0;
									continue;
								}
								int firstColumn = this.FirstColumn;
								int rowGroupLevel = this[firstRow, firstColumn].RowGroupLevel;
								int num2 = firstRow + 1;
								num = 1;
								continue;
							}
							case 8:
							{
								int rowGroupLevel;
								return rowGroupLevel;
							}
							case 9:
								return -1;
							}
							break;
							IL_C4:
							num = 2;
						}
					}
					return -1;
					IL_158:
					return (int)spr_u.ᜀ();
				}
				}
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00084B94 File Offset: 0x00083B94
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x00084CEC File Offset: 0x00083CEC
		public double RowHeight
		{
			get
			{
				double num;
				for (;;)
				{
					this.ᜆ();
					num = double.MinValue;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							int num3;
							if (num3 > this.m_iBottomRow)
							{
								num2 = 8;
								continue;
							}
							goto IL_90;
						}
						case 1:
							goto IL_E0;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_90;
							default:
								if (false)
								{
								}
								num = this.\u171D.GetRowHeight(this.Row);
								num2 = 9;
								continue;
							}
							break;
						case 3:
							goto IL_E0;
						case 4:
						{
							if (this.m_iTopRow == this.m_iBottomRow)
							{
								num2 = 2;
								continue;
							}
							num = this.\u171D.GetRowHeight(this.m_iTopRow);
							int num3 = this.m_iTopRow + 1;
							num2 = 3;
							continue;
						}
						case 5:
							return num;
						case 6:
							num = double.MinValue;
							num2 = 5;
							continue;
						case 7:
						{
							int num3;
							if (num != this.\u171D.GetRowHeight(num3))
							{
								num2 = 6;
								continue;
							}
							num3++;
							num2 = 1;
							continue;
						}
						case 8:
							goto IL_FF;
						case 9:
							goto IL_DE;
						}
						break;
						IL_90:
						num2 = 7;
						continue;
						IL_E0:
						num2 = 0;
					}
				}
				IL_DE:
				return num;
				IL_FF:
				if (true)
				{
				}
				return num;
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
				this.ᜆ();
				this.SetRowHeight(value, true);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00084D38 File Offset: 0x00083D38
		public IXLSRange[] Rows
		{
			get
			{
				IXLSRange[] array;
				for (;;)
				{
					this.ᜆ();
					int num = 11;
					for (;;)
					{
						int num3;
						int num4;
						switch (num)
						{
						case 0:
						{
							int num2 = this.FirstRow;
							num = 2;
							continue;
						}
						case 1:
							if (this.LastColumn != 0)
							{
								num = 5;
								continue;
							}
							goto IL_14D;
						case 2:
							goto IL_83;
						case 3:
							goto IL_14D;
						case 4:
							if (this.LastRow != 0)
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
									num = 12;
									continue;
								}
							}
							num = 3;
							continue;
						case 5:
							num = 4;
							continue;
						case 6:
							if (num3 > 0)
							{
								num = 0;
								continue;
							}
							return array;
						case 7:
							num = 1;
							continue;
						case 8:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 9;
								continue;
							}
							array[num2 - this.FirstRow] = this.\u171D.AllocatedRange[num2, this.FirstColumn, num2, this.LastColumn];
							num2++;
							num = 10;
							continue;
						}
						case 9:
							return array;
						case 10:
							goto IL_83;
						case 11:
							if (this.FirstColumn != 0)
							{
								num = 7;
								continue;
							}
							goto IL_14D;
						case 12:
							num4 = this.LastRow - this.FirstRow + 1;
							goto IL_15B;
						case 13:
							num4 = 0;
							goto IL_15B;
						}
						break;
						IL_83:
						num = 8;
						continue;
						IL_14D:
						num = 13;
						continue;
						IL_15B:
						num3 = num4;
						array = new IXLSRange[num3];
						num = 6;
					}
				}
				return array;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00084EDC File Offset: 0x00083EDC
		public IXLSRange[] Columns
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					int num = 3;
					for (;;)
					{
						int num2;
						IXLSRange[] array;
						switch (num)
						{
						case 0:
							num = 7;
							continue;
						case 1:
							if (num2 > this.LastColumn)
							{
								num = 5;
								continue;
							}
							goto IL_55;
						case 2:
							goto IL_FE;
						case 3:
							if (this.FirstColumn != 0)
							{
								num = 0;
								continue;
							}
							goto IL_12A;
						case 4:
							goto IL_FE;
						case 5:
							return array;
						case 6:
							goto IL_FC;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_55;
							default:
								if (false)
								{
								}
								if (this.FirstColumn > this.m_book.MaxColumnCount)
								{
									num = 6;
									continue;
								}
								array = new IXLSRange[this.LastColumn - this.FirstColumn + 1];
								num2 = this.FirstColumn;
								num = 2;
								continue;
							}
							break;
						}
						break;
						IL_55:
						array[num2 - this.FirstColumn] = this.\u171D.AllocatedRange[this.FirstRow, num2, this.LastRow, num2];
						num2++;
						num = 4;
						continue;
						IL_FE:
						if (true)
						{
						}
						num = 1;
					}
				}
				IL_FC:
				IL_12A:
				return new IXLSRange[0];
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0008501C File Offset: 0x0008401C
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x000850DC File Offset: 0x000840DC
		public IStyle Style
		{
			get
			{
				if (true)
				{
				}
				for (;;)
				{
					this.ᜆ();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.IsSingleCell)
							{
								num = 2;
								continue;
							}
							goto IL_A6;
						case 1:
							goto IL_91;
						case 2:
							num = 1;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_91;
							default:
								if (false)
								{
								}
								this.CreateStyle();
								num = 4;
								continue;
							}
							break;
						case 4:
							goto IL_84;
						}
						break;
						IL_91:
						if (this.m_style != null)
						{
							goto IL_4E;
						}
						num = 3;
					}
				}
				IL_4E:
				return new CellStyle(this.m_style);
				IL_84:
				goto IL_4E;
				IL_A6:
				return new CellStyle(new StyleArrayWrapper(this));
			}
			set
			{
				switch (0)
				{
				default:
				{
					XlsRange.TCellType a_;
					for (;;)
					{
						IL_7B:
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_16C:
							num = 5;
							break;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜆ();
							num = 6;
							break;
						}
						ushort extendedFormatIndex;
						for (;;)
						{
							BiffRecordRaw biffRecordRaw;
							string text;
							switch (num)
							{
							case 0:
								if (value != null)
								{
									num = 17;
									continue;
								}
								num = 12;
								continue;
							case 1:
								this.SetChanged();
								a_ = this.CellType;
								num = 13;
								continue;
							case 2:
								goto IL_2FC;
							case 3:
							{
								int num2;
								num2++;
								num = 15;
								continue;
							}
							case 4:
								goto IL_133;
							case 5:
								goto IL_2CC;
							case 6:
							{
								if (this.IsSingleCell)
								{
									num = 1;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num2 = this.FirstRow;
								num = 16;
								continue;
							}
							case 7:
								if (biffRecordRaw == null)
								{
									num = 9;
									continue;
								}
								goto IL_31D;
							case 8:
								goto IL_2CC;
							case 9:
								goto IL_F9;
							case 10:
								num = 23;
								continue;
							case 11:
							{
								int num3;
								if (num3 > this.LastColumn)
								{
									num = 3;
									continue;
								}
								int num2;
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								spr_u24F.Style = value;
								num3++;
								num = 20;
								continue;
							}
							case 12:
								text = XlsStyle.DEF_DEFAULT_STYLES[0];
								goto IL_244;
							case 13:
								if (value is AddtionalFormatWrapper)
								{
									num = 14;
									continue;
								}
								num = 0;
								continue;
							case 14:
								goto IL_2A4;
							case 15:
								goto IL_21A;
							case 16:
								goto IL_21A;
							case 17:
								num = 22;
								continue;
							case 18:
								return;
							case 19:
							{
								int num2;
								if (num2 > this.LastRow)
								{
									num = 18;
									continue;
								}
								int num3 = this.FirstColumn;
								num = 4;
								continue;
							}
							case 20:
								goto IL_133;
							case 21:
								goto IL_117;
							case 22:
								text = value.Name;
								goto IL_244;
							case 23:
								if (biffRecordRaw.TypeCode != TBIFFRecord.Formula)
								{
									num = 2;
									continue;
								}
								goto IL_F9;
							case 24:
								if (biffRecordRaw != null)
								{
									num = 10;
									continue;
								}
								goto IL_2FC;
							}
							goto IL_7B;
							IL_F9:
							string value2 = this.Value;
							this.OnValueChanged(value2, value2);
							num = 21;
							continue;
							IL_133:
							num = 11;
							continue;
							IL_21A:
							num = 19;
							continue;
							IL_244:
							string name = text;
							XlsStyle xlsStyle = (XlsStyle)this.m_book.Styles[name];
							extendedFormatIndex = (ushort)xlsStyle.Wrapped.ᜠ();
							num = 8;
							continue;
							IL_2CC:
							this.ExtendedFormatIndex = extendedFormatIndex;
							biffRecordRaw = this.Record;
							num = 24;
							continue;
							IL_2FC:
							num = 7;
						}
						IL_2A4:
						extendedFormatIndex = (ushort)(value as AddtionalFormatWrapper).Wrapped.ᜠ();
						goto IL_16C;
					}
					IL_117:
					IL_31D:
					this.ᜀ(a_);
					return;
				}
				}
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00085410 File Offset: 0x00084410
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x00085470 File Offset: 0x00084470
		public string CellStyleName
		{
			get
			{
				this.ᜆ();
				if (!this.IsSingleCell)
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
						return sprṔ.ᜀ(this.CellsList);
					}
				}
				return this.ᜃ();
			}
			set
			{
				switch (0)
				{
				default:
				{
					XlsRange.TCellType a_;
					for (;;)
					{
						if (true)
						{
						}
						this.ᜆ();
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								return;
							case 1:
								goto IL_154;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_193;
								default:
								{
									if (false)
									{
									}
									int num2;
									num2++;
									num = 6;
									continue;
								}
								}
								break;
							case 3:
								if (value == null)
								{
									num = 4;
									continue;
								}
								goto IL_1A1;
							case 4:
							{
								string text = XlsStyle.DEF_DEFAULT_STYLES[0];
								num = 9;
								continue;
							}
							case 5:
							{
								int num2;
								if (num2 > this.LastRow)
								{
									num = 0;
									continue;
								}
								int num3 = this.FirstColumn;
								num = 10;
								continue;
							}
							case 6:
								goto IL_154;
							case 7:
							{
								if (this.IsSingleCell)
								{
									num = 12;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num2 = this.FirstRow;
								goto IL_193;
							}
							case 8:
								goto IL_7B;
							case 9:
								goto IL_D3;
							case 10:
								goto IL_7B;
							case 11:
							{
								int num3;
								if (num3 > this.LastColumn)
								{
									num = 2;
									continue;
								}
								int num2;
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								spr_u24F.CellStyleName = value;
								num3++;
								num = 8;
								continue;
							}
							case 12:
								a_ = this.CellType;
								num = 3;
								continue;
							}
							break;
							IL_7B:
							num = 11;
							continue;
							IL_154:
							num = 5;
							continue;
							IL_193:
							num = 1;
						}
					}
					IL_D3:
					IL_1A1:
					this.ᜀ(value);
					string value2 = this.Value;
					this.OnValueChanged(value2, value2);
					this.ᜀ(a_);
					this.SetChanged();
					return;
				}
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00085644 File Offset: 0x00084644
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x00085698 File Offset: 0x00084698
		public BuiltInStyles? BuiltInStyle
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
				string cellStyleName = this.CellStyleName;
				return new BuiltInStyles?((BuiltInStyles)Array.IndexOf<string>(XlsStyle.DEF_DEFAULT_STYLES, cellStyleName));
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
				string cellStyleName = XlsStyle.DEF_DEFAULT_STYLES[(int)value.Value];
				this.CellStyleName = cellStyleName;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x000856E8 File Offset: 0x000846E8
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x000858E0 File Offset: 0x000848E0
		public string Text
		{
			get
			{
				int a_ = 5;
				switch (0)
				{
				default:
				{
					string text;
					for (;;)
					{
						this.ᜆ();
						text = this.\u171D.GetText(this.Row, this.Column);
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (text == null)
								{
									num = 12;
									continue;
								}
								int num2 = this.Row;
								int lastRow = this.LastRow;
								num = 11;
								continue;
							}
							case 1:
							{
								int num2;
								num2++;
								num = 8;
								continue;
							}
							case 2:
								if (this.ExtendedFormat.\u1713())
								{
									num = 7;
									continue;
								}
								return text;
							case 3:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 1;
									continue;
								}
								num = 9;
								continue;
							}
							case 4:
								num = 2;
								continue;
							case 5:
								goto IL_194;
							case 6:
								goto IL_1E6;
							case 7:
								text = RecordTableEnumerator.b("᰺", a_) + text;
								num = 14;
								continue;
							case 8:
								goto IL_12A;
							case 9:
							{
								int num2;
								int num3;
								if (text != this.\u171D.GetText(num2, num3))
								{
									num = 6;
									continue;
								}
								if (true)
								{
								}
								num3++;
								num = 10;
								continue;
							}
							case 10:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_169;
								default:
									if (false)
									{
									}
									goto IL_194;
								}
								break;
							case 11:
								goto IL_12A;
							case 12:
								goto IL_8F;
							case 13:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 4;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num = 5;
								continue;
							}
							case 14:
								goto IL_169;
							}
							break;
							IL_12A:
							num = 13;
							continue;
							IL_194:
							num = 3;
						}
					}
					IL_8F:
					return null;
					IL_169:
					return text;
					IL_1E6:
					return null;
				}
				}
			}
			set
			{
				int a_ = 7;
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = 11;
						for (;;)
						{
							string numberFormat;
							switch (num)
							{
							case 0:
								this.ᜀ(this.Text, value, this);
								num = 24;
								continue;
							case 1:
							{
								int num2;
								if (num2 > this.LastColumn)
								{
									num = 16;
									continue;
								}
								spr\u24F1 spr_u24F;
								int num3;
								spr_u24F.ᜀ(num3, num2);
								spr_u24F.Text = value;
								num2++;
								num = 25;
								continue;
							}
							case 2:
							{
								if (this.IsSingleCell)
								{
									num = 4;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num3 = this.FirstRow;
								num = 23;
								continue;
							}
							case 3:
								num = 9;
								continue;
							case 4:
								num = 22;
								continue;
							case 5:
								goto IL_BF;
							case 6:
								goto IL_ED;
							case 7:
								if (numberFormat != RecordTableEnumerator.b("稼娾⽀♂㝄♆╈", a_))
								{
									num = 15;
									continue;
								}
								goto IL_ED;
							case 8:
								goto IL_246;
							case 9:
								if (value.Contains(RecordTableEnumerator.b("㜼", a_)))
								{
									num = 13;
									continue;
								}
								return;
							case 10:
								goto IL_246;
							case 11:
								if (value == null)
								{
									num = 5;
									continue;
								}
								this.ᜉ();
								num = 2;
								continue;
							case 12:
								goto IL_14E;
							case 13:
								this.IsWrapText = true;
								num = 27;
								continue;
							case 14:
								this.Value = value;
								num = 8;
								continue;
							case 15:
								IL_35C:
								this.NumberFormat = RecordTableEnumerator.b("紼", a_);
								num = 6;
								continue;
							case 16:
							{
								int num3;
								num3++;
								num = 12;
								continue;
							}
							case 17:
								if (this.Text != value)
								{
									num = 0;
									continue;
								}
								goto IL_38E;
							case 18:
								goto IL_30D;
							case 19:
								if (this.m_rtfString == null)
								{
									num = 28;
									continue;
								}
								goto IL_30D;
							case 20:
								goto IL_C4;
							case 21:
								goto IL_246;
							case 22:
								if (value.Length == 0)
								{
									num = 14;
									continue;
								}
								num = 17;
								continue;
							case 23:
								goto IL_14E;
							case 24:
								goto IL_38E;
							case 25:
								goto IL_C4;
							case 26:
							{
								int num3;
								if (num3 > this.LastRow)
								{
									num = 21;
									continue;
								}
								int num2 = this.FirstColumn;
								num = 20;
								continue;
							}
							case 27:
								return;
							case 28:
								this.CreateRichTextString();
								num = 18;
								continue;
							case 29:
								if (!this.IsWrapText)
								{
									if (true)
									{
									}
									num = 3;
									continue;
								}
								return;
							}
							break;
							IL_C4:
							num = 1;
							continue;
							IL_ED:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_35C;
							default:
								if (false)
								{
								}
								this.m_rtfString.ClearFormatting();
								this.m_rtfString.EndUpdate();
								this.SetChanged();
								num = 10;
								continue;
							}
							IL_14E:
							num = 26;
							continue;
							IL_246:
							num = 29;
							continue;
							IL_30D:
							this.m_rtfString.BeginUpdate();
							this.m_rtfString.Text = value;
							numberFormat = this.NumberFormat;
							num = 7;
							continue;
							IL_38E:
							value = this.ᜅ(value);
							num = 19;
						}
					}
					IL_BF:
					throw new ArgumentNullException(RecordTableEnumerator.b("椼娾㥀㝂", a_));
				}
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00085CD8 File Offset: 0x00084CD8
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x00085F28 File Offset: 0x00084F28
		public TimeSpan TimeSpanValue
		{
			get
			{
				switch (0)
				{
				default:
				{
					double number;
					for (;;)
					{
						this.ᜆ();
						number = this.\u171D.GetNumber(this.Row, this.Column);
						int num = 14;
						for (;;)
						{
							double number2;
							int num2;
							int num3;
							switch (num)
							{
							case 0:
								if (number2 != double.NaN)
								{
									num = 5;
									continue;
								}
								goto IL_119;
							case 1:
							{
								int lastRow;
								if (num2 > lastRow)
								{
									num = 13;
									continue;
								}
								num3 = this.Column;
								int lastColumn = this.LastColumn;
								num = 16;
								continue;
							}
							case 2:
								goto IL_11F;
							case 3:
								goto IL_1D1;
							case 4:
							{
								int lastColumn;
								if (num3 > lastColumn)
								{
									num = 11;
									continue;
								}
								goto IL_14B;
							}
							case 5:
								num = 9;
								continue;
							case 6:
								goto IL_E3;
							case 7:
							{
								if (this.InnerNumberFormat.ᜀ(number) != CellFormatType.DateTime)
								{
									num = 3;
									continue;
								}
								num2 = this.Row;
								int lastRow = this.LastRow;
								num = 17;
								continue;
							}
							case 8:
								if (this.InnerNumberFormat.ᜀ(number2) != CellFormatType.DateTime)
								{
									num = 6;
									continue;
								}
								num3++;
								num = 12;
								continue;
							case 9:
								if (number == number2)
								{
									num = 10;
									continue;
								}
								goto IL_119;
							case 10:
								num = 8;
								continue;
							case 11:
								num2++;
								num = 2;
								continue;
							case 12:
								goto IL_1D6;
							case 13:
								goto IL_146;
							case 14:
								if (number != double.NaN)
								{
									num = 15;
									continue;
								}
								goto IL_113;
							case 15:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_14B;
								default:
									if (false)
									{
									}
									num = 7;
									continue;
								}
								break;
							case 16:
								goto IL_1D6;
							case 17:
								goto IL_11F;
							}
							break;
							IL_11F:
							if (true)
							{
							}
							num = 1;
							continue;
							IL_14B:
							number2 = this.\u171D.GetNumber(num2, num3);
							num = 0;
							continue;
							IL_1D6:
							num = 4;
						}
					}
					IL_E3:
					goto IL_119;
					IL_113:
					return TimeSpan.MinValue;
					IL_119:
					return TimeSpan.MinValue;
					IL_146:
					return TimeSpan.FromDays(number);
					IL_1D1:
					goto IL_113;
				}
				}
			}
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						this.ᜆ();
						int num = 8;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_7B;
							case 1:
								return;
							case 2:
								goto IL_7B;
							case 3:
							{
								int num2;
								if (num2 > this.LastColumn)
								{
									num = 11;
									continue;
								}
								spr\u24F1 spr_u24F;
								int num3;
								spr_u24F.ᜀ(num3, num2);
								spr_u24F.TimeSpanValue = value;
								num2++;
								num = 0;
								continue;
							}
							case 4:
							{
								this.FormatType = CellFormatType.DateTime;
								TimeSpan timeSpanValue = this.TimeSpanValue;
								num = 9;
								continue;
							}
							case 5:
								goto IL_16A;
							case 6:
								goto IL_16A;
							case 7:
								goto IL_DC;
							case 8:
							{
								if (this.IsSingleCell)
								{
									num = 4;
									continue;
								}
								this.ᜉ();
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num3 = this.FirstRow;
								goto IL_1AF;
							}
							case 9:
							{
								TimeSpan timeSpanValue;
								if (timeSpanValue != value)
								{
									num = 10;
									continue;
								}
								goto IL_1BD;
							}
							case 10:
							{
								TimeSpan timeSpanValue;
								this.ᜀ(timeSpanValue, value, this);
								num = 7;
								continue;
							}
							case 11:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1AF;
								default:
								{
									if (false)
									{
									}
									int num3;
									num3++;
									num = 5;
									continue;
								}
								}
								break;
							case 12:
							{
								int num3;
								if (num3 > this.LastRow)
								{
									num = 1;
									continue;
								}
								int num2 = this.FirstColumn;
								num = 2;
								continue;
							}
							}
							break;
							IL_7B:
							num = 3;
							continue;
							IL_16A:
							num = 12;
							continue;
							IL_1AF:
							num = 6;
						}
					}
					IL_DC:
					IL_1BD:
					this.SetTimeSpan(value);
					this.SetChanged();
					return;
				}
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00086100 File Offset: 0x00085100
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x000862F4 File Offset: 0x000852F4
		public string Value
		{
			get
			{
				switch (0)
				{
				default:
				{
					string text;
					for (;;)
					{
						this.ᜆ();
						text = null;
						int num = 10;
						for (;;)
						{
							int num3;
							int lastRow;
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_16F;
								default:
									if (false)
									{
									}
									goto IL_76;
								}
								break;
							case 1:
							{
								string value;
								if (text != value)
								{
									num = 3;
									continue;
								}
								int num2;
								num2++;
								num = 0;
								continue;
							}
							case 2:
								goto IL_76;
							case 3:
								text = null;
								if (true)
								{
								}
								num = 11;
								continue;
							case 4:
								return text;
							case 5:
								goto IL_F5;
							case 6:
							{
								int num2;
								int lastColumn;
								if (num2 > lastColumn)
								{
									num = 5;
									continue;
								}
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num3, num2);
								string value = spr_u24F.Value;
								num = 1;
								continue;
							}
							case 7:
								goto IL_163;
							case 8:
								goto IL_163;
							case 9:
								goto IL_16F;
							case 10:
							{
								if (this.IsSingleCell)
								{
									num = 13;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								spr_u24F.ᜀ(this.Row, this.Column);
								text = spr_u24F.Value;
								num3 = this.Row;
								lastRow = this.LastRow;
								num = 8;
								continue;
							}
							case 11:
								goto IL_F5;
							case 12:
								return text;
							case 13:
								text = this.\u171D.ᜀ(this.Record as spr\u23A5, false);
								num = 12;
								continue;
							}
							break;
							IL_76:
							num = 6;
							continue;
							IL_F5:
							num3++;
							num = 7;
							continue;
							IL_163:
							num = 9;
							continue;
							IL_16F:
							if (num3 > lastRow)
							{
								num = 4;
							}
							else
							{
								int num2 = this.Column;
								int lastColumn = this.LastColumn;
								num = 2;
							}
						}
					}
					return text;
				}
				}
			}
			set
			{
				int a_ = 19;
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						this.ᜉ();
						int num = 19;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 0:
								num = 9;
								continue;
							case 1:
								return;
							case 2:
							{
								int num2;
								num2++;
								num = 15;
								continue;
							}
							case 3:
							{
								string value2 = this.Value;
								goto IL_1BC;
							}
							case 4:
							{
								string value2;
								this.OnValueChanged(value2, value);
								num = 10;
								continue;
							}
							case 5:
								goto IL_221;
							case 6:
								num = 11;
								continue;
							case 7:
								goto IL_153;
							case 8:
							{
								int num3;
								if (num3 > this.LastColumn)
								{
									num = 2;
									continue;
								}
								int num2;
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								spr_u24F.Value = value;
								num3++;
								num = 17;
								continue;
							}
							case 9:
								if (value.Contains(RecordTableEnumerator.b("䍈", a_)))
								{
									num = 16;
									continue;
								}
								return;
							case 10:
								goto IL_A6;
							case 11:
								if (value == null)
								{
									return;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1BC;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 12:
								goto IL_A6;
							case 13:
								if (!this.IsWrapText)
								{
									num = 6;
									continue;
								}
								return;
							case 14:
							{
								string value2;
								if (value != value2)
								{
									num = 4;
									continue;
								}
								goto IL_A6;
							}
							case 15:
								goto IL_153;
							case 16:
								this.IsWrapText = true;
								num = 1;
								continue;
							case 17:
								goto IL_221;
							case 18:
							{
								int num2;
								if (num2 > this.LastRow)
								{
									num = 12;
									continue;
								}
								int num3 = this.FirstColumn;
								num = 5;
								continue;
							}
							case 19:
							{
								if (this.IsSingleCell)
								{
									num = 3;
									continue;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num2 = this.FirstRow;
								num = 7;
								continue;
							}
							}
							break;
							IL_A6:
							num = 13;
							continue;
							IL_153:
							num = 18;
							continue;
							IL_1BC:
							num = 14;
							continue;
							IL_221:
							num = 8;
						}
					}
					return;
				}
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0008656C File Offset: 0x0008556C
		public string EnvalutedValue
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
							goto IL_C2;
						}
						break;
					case 2:
						if (((IWorksheet)this.Parent).FormulaEngine != null)
						{
							num = 1;
							continue;
						}
						goto IL_CA;
					case 3:
						num = 2;
						continue;
					}
					IL_2A:
					if (true)
					{
					}
					if (this.Parent is IWorksheet)
					{
						num = 3;
						continue;
					}
					goto IL_CA;
					goto IL_2A;
				}
				IL_C2:
				if (false)
				{
				}
				string a_ = sprḅ.ᜀ(this.Column) + this.Row.ToString();
				return ((IWorksheet)this.Parent).FormulaEngine.ᜀ.\u17C4(a_);
				IL_CA:
				return null;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00086644 File Offset: 0x00085644
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x000866CC File Offset: 0x000856CC
		public object Value2
		{
			get
			{
				object obj;
				for (;;)
				{
					for (;;)
					{
						this.ᜆ();
						obj = this.ᜊ();
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
							int num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (true)
									{
									}
									if (obj == null)
									{
										num = 1;
										continue;
									}
									return obj;
								case 1:
									obj = this.Value;
									num = 2;
									continue;
								case 2:
									return obj;
								}
								break;
							}
							break;
						}
						}
					}
				}
				return obj;
			}
			set
			{
				for (;;)
				{
					IL_4C:
					this.ᜆ();
					this.ᜉ();
					int num = 4;
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
								goto IL_75;
							case 1:
								goto IL_EE;
							case 2:
							{
								int num2;
								if (num2 > this.LastColumn)
								{
									num = 7;
									continue;
								}
								if (true)
								{
								}
								spr\u24F1 spr_u24F;
								int num3;
								spr_u24F.ᜀ(num3, num2);
								spr_u24F.Value2 = value;
								num2++;
								num = 5;
								continue;
							}
							case 3:
							{
								int num3;
								if (num3 > this.LastRow)
								{
									num = 6;
									continue;
								}
								int num2 = this.FirstColumn;
								num = 0;
								continue;
							}
							case 4:
							{
								if (this.IsSingleCell)
								{
									goto IL_68;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num3 = this.FirstRow;
								num = 1;
								continue;
							}
							case 5:
								goto IL_75;
							case 6:
								return;
							case 7:
							{
								int num3;
								num3++;
								num = 8;
								continue;
							}
							case 8:
								goto IL_EE;
							case 9:
								goto IL_70;
							}
							goto IL_4C;
							IL_75:
							num = 2;
							continue;
							IL_EE:
							num = 3;
							continue;
						}
						IL_68:
						num = 9;
					}
				}
				IL_70:
				this.ᜀ(value);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00086818 File Offset: 0x00085818
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0008685C File Offset: 0x0008585C
		internal bool IsNumReference
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
				return this.\u171E;
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
				this.\u171E = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000868A0 File Offset: 0x000858A0
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x000868E4 File Offset: 0x000858E4
		internal bool IsStringReference
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00086928 File Offset: 0x00085928
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x0008696C File Offset: 0x0008596C
		internal bool IsMultiReference
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
				return this.\u171F;
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
				this.\u171F = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000869B0 File Offset: 0x000859B0
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00086A08 File Offset: 0x00085A08
		public VerticalAlignType VerticalAlignment
		{
			get
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
					this.ᜆ();
					if (!this.IsSingleCell)
					{
						return VerticalAlignType.Bottom;
					}
					break;
				}
				return this.Style.VerticalAlignment;
			}
			set
			{
				for (;;)
				{
					IL_4C:
					this.ᜆ();
					int num = 7;
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
								return;
							case 1:
							{
								int num2;
								num2++;
								num = 9;
								continue;
							}
							case 2:
								goto IL_6A;
							case 3:
								goto IL_E0;
							case 4:
								goto IL_6F;
							case 5:
							{
								int num2;
								if (num2 > this.LastRow)
								{
									num = 0;
									continue;
								}
								int num3 = this.FirstColumn;
								num = 4;
								continue;
							}
							case 6:
							{
								int num3;
								if (num3 > this.LastColumn)
								{
									num = 1;
									continue;
								}
								int num2;
								spr\u24F1 spr_u24F;
								spr_u24F.ᜀ(num2, num3);
								spr_u24F.VerticalAlignment = value;
								num3++;
								num = 8;
								continue;
							}
							case 7:
							{
								if (this.IsSingleCell)
								{
									goto IL_62;
								}
								spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
								int num2 = this.FirstRow;
								num = 3;
								continue;
							}
							case 8:
								goto IL_6F;
							case 9:
								if (true)
								{
								}
								goto IL_E0;
							}
							goto IL_4C;
							IL_6F:
							num = 6;
							continue;
							IL_E0:
							num = 5;
							continue;
						}
						IL_62:
						num = 2;
					}
				}
				IL_6A:
				this.Style.VerticalAlignment = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00086B54 File Offset: 0x00085B54
		public IWorksheet Worksheet
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
				this.ᜆ();
				return this.\u171D;
			}
		}

		// Token: 0x170004C8 RID: 1224
		public IXLSRange this[int row, int column]
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
				this.ᜆ();
				this.CheckRange(row, column);
				return this.\u171D.InnerGetCell(column, row);
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
				this.ᜆ();
				this.CheckRange(row, column);
				this.\u171D.InnerSetCell(column, row, (XlsRange)value);
				this.SetChanged();
			}
		}

		// Token: 0x170004C9 RID: 1225
		public IXLSRange this[int row, int column, int lastRow, int lastColumn]
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_90:
					num = 3;
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
						if (column != lastColumn)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_D6;
					case 1:
						goto IL_8C;
					case 2:
						goto IL_D4;
					case 3:
						num = 0;
						continue;
					}
					goto IL_3E;
				}
				IL_8C:
				if (row == lastRow)
				{
					goto IL_90;
				}
				IL_9A:
				return this.AppImplementation.ᜀ(this.Parent, column, row, lastColumn, lastRow);
				IL_D4:
				goto IL_9A;
				IL_D6:
				return this[row, column];
				IL_3E:
				this.ᜆ();
				row = this.ᜁ(row, column, lastColumn);
				lastRow = this.ᜁ(lastRow, column, lastColumn);
				column = this.ᜀ(column, row, lastRow);
				lastColumn = this.ᜀ(lastColumn, row, lastRow);
				this.CheckRange(row, column);
				this.CheckRange(lastRow, lastColumn);
				num = 1;
				goto IL_28;
			}
		}

		// Token: 0x170004CA RID: 1226
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

		// Token: 0x170004CB RID: 1227
		public IXLSRange this[string name, bool IsR1C1Notation]
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_1B0:
						if (num != 2)
						{
							throw new ArgumentException();
						}
						num2 = 1;
						break;
					default:
						if (false)
						{
						}
						goto IL_77;
					}
					IXLSRange result;
					INamedRange namedRange;
					string text;
					for (;;)
					{
						IL_2C:
						switch (num2)
						{
						case 0:
							goto IL_BF;
						case 1:
						{
							int row;
							int column;
							int lastRow;
							int lastColumn;
							result = this[row, column, lastRow, lastColumn];
							num2 = 6;
							continue;
						}
						case 2:
							if (namedRange != null)
							{
								num2 = 10;
								continue;
							}
							name = name.ToUpper();
							num2 = 5;
							continue;
						case 3:
							if (namedRange != null)
							{
								num2 = 4;
								continue;
							}
							namedRange = this.m_book.Names[name];
							num2 = 2;
							continue;
						case 4:
							goto IL_124;
						case 5:
						{
							if (IsR1C1Notation)
							{
								num2 = 13;
								continue;
							}
							int row;
							int column;
							int lastRow;
							int lastColumn;
							num = sprṔ.ᜀ(name, this.Workbook, out row, out column, out lastRow, out lastColumn);
							num2 = 12;
							continue;
						}
						case 6:
							goto IL_140;
						case 7:
							goto IL_1DA;
						case 8:
							goto IL_1B0;
						case 9:
							if (this.\u171D.Name != text)
							{
								num2 = 15;
								continue;
							}
							goto IL_F4;
						case 10:
							goto IL_23F;
						case 11:
							if (text != null)
							{
								num2 = 14;
								continue;
							}
							goto IL_F4;
						case 12:
							if (num == 1)
							{
								num2 = 16;
								continue;
							}
							num2 = 8;
							continue;
						case 13:
							if (true)
							{
							}
							result = this.ᜁ(name);
							num2 = 7;
							continue;
						case 14:
							num2 = 9;
							continue;
						case 15:
							goto IL_20A;
						case 16:
						{
							int row;
							int column;
							result = this[row, column];
							num2 = 0;
							continue;
						}
						}
						goto IL_77;
						IL_F4:
						namedRange = this.\u171D.Names[name];
						num2 = 3;
					}
					IL_BF:
					return result;
					IL_124:
					return namedRange.RefersToRange;
					IL_140:
					IL_1DA:
					return result;
					IL_20A:
					return this.FindWorksheet(text).AllocatedRange[name];
					IL_23F:
					return namedRange.RefersToRange;
					IL_77:
					this.ᜆ();
					text = sprṔ.ᜀ(ref name);
					num2 = 11;
					goto IL_2C;
				}
				}
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00086FDC File Offset: 0x00085FDC
		public ConditionalFormats ConditionalFormats
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
				this.\u171D.\u171A();
				this.ᜆ();
				return this.AppImplementation.ᜀ(this);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00087034 File Offset: 0x00086034
		public Validation DataValidation
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_AA:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					goto IL_42;
				}
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (this.IsSingleCell)
						{
							num = 3;
							continue;
						}
						goto IL_B7;
					case 1:
						goto IL_A2;
					case 2:
					{
						XlsValidation a_ = this.ᜇ();
						this.m_dataValidation = this.AppImplementation.ᜀ(this, a_);
						num = 4;
						continue;
					}
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_98;
					}
					goto IL_42;
				}
				IL_6A:
				return (Validation)this.m_dataValidation;
				IL_98:
				goto IL_6A;
				IL_A2:
				if (this.m_dataValidation == null)
				{
					goto IL_AA;
				}
				goto IL_6A;
				IL_B7:
				return new Validation(this, this.AppImplementation.ᜀ(this));
				IL_42:
				this.ᜆ();
				num = 0;
				goto IL_28;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0008710C File Offset: 0x0008610C
		public bool HasFormulaBoolValue
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
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								num2 = 4;
								continue;
							case 1:
								goto IL_CC;
							case 2:
								goto IL_6E;
							case 3:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 8;
									continue;
								}
								XlsWorksheet.TRangeValueType cellType = this.\u171D.GetCellType(num, num3, true);
								num2 = 5;
								continue;
							}
							case 4:
							{
								XlsWorksheet.TRangeValueType cellType;
								if ((cellType & XlsWorksheet.TRangeValueType.Boolean) != XlsWorksheet.TRangeValueType.Boolean)
								{
									num2 = 11;
									continue;
								}
								int num3;
								num3++;
								num2 = 2;
								continue;
							}
							case 5:
							{
								XlsWorksheet.TRangeValueType cellType;
								if ((cellType & XlsWorksheet.TRangeValueType.Formula) == XlsWorksheet.TRangeValueType.Formula)
								{
									num2 = 0;
									continue;
								}
								return false;
							}
							case 6:
								goto IL_6E;
							case 7:
								goto IL_CC;
							case 8:
								goto IL_84;
							case 9:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_84;
								default:
									goto IL_161;
								}
								break;
							case 10:
							{
								if (num > lastRow)
								{
									num2 = 9;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 6;
								continue;
							}
							case 11:
								goto IL_149;
							}
							break;
							IL_6E:
							num2 = 3;
							continue;
							IL_84:
							num++;
							num2 = 7;
							continue;
							IL_CC:
							num2 = 10;
						}
					}
					return false;
					IL_149:
					return false;
					IL_161:
					if (false)
					{
					}
					return true;
				}
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00087284 File Offset: 0x00086284
		public bool HasFormulaErrorValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_149;
							case 1:
							{
								if (num > lastRow)
								{
									num2 = 11;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 3;
								continue;
							}
							case 2:
								num2 = 5;
								continue;
							case 3:
								goto IL_66;
							case 4:
								goto IL_66;
							case 5:
							{
								XlsWorksheet.TRangeValueType cellType;
								if ((cellType & XlsWorksheet.TRangeValueType.Error) != XlsWorksheet.TRangeValueType.Error)
								{
									num2 = 0;
									continue;
								}
								int num3;
								num3++;
								num2 = 4;
								continue;
							}
							case 6:
								goto IL_CC;
							case 7:
							{
								XlsWorksheet.TRangeValueType cellType;
								if ((cellType & XlsWorksheet.TRangeValueType.Formula) == XlsWorksheet.TRangeValueType.Formula)
								{
									num2 = 2;
									continue;
								}
								return false;
							}
							case 8:
								goto IL_84;
							case 9:
								goto IL_CC;
							case 10:
							{
								if (true)
								{
								}
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 8;
									continue;
								}
								XlsWorksheet.TRangeValueType cellType = this.\u171D.GetCellType(num, num3, true);
								num2 = 7;
								continue;
							}
							case 11:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_84;
								default:
									goto IL_161;
								}
								break;
							}
							break;
							IL_66:
							num2 = 10;
							continue;
							IL_84:
							num++;
							num2 = 9;
							continue;
							IL_CC:
							num2 = 1;
						}
					}
					return false;
					IL_149:
					return false;
					IL_161:
					if (false)
					{
					}
					return true;
				}
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x000873FC File Offset: 0x000863FC
		public bool HasFormulaDateTime
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 11;
						for (;;)
						{
							if (true)
							{
							}
							switch (num2)
							{
							case 0:
								goto IL_6E;
							case 1:
							{
								if (num > lastRow)
								{
									goto IL_F9;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 10;
								continue;
							}
							case 2:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 4;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F9;
								default:
								{
									if (false)
									{
									}
									double formulaNumberValue = this.\u171D.GetFormulaNumberValue(num, num3);
									num2 = 7;
									continue;
								}
								}
								break;
							}
							case 3:
							{
								double formulaNumberValue;
								CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(formulaNumberValue);
								num2 = 8;
								continue;
							}
							case 4:
								num++;
								num2 = 5;
								continue;
							case 5:
								goto IL_E9;
							case 6:
								goto IL_173;
							case 7:
							{
								double formulaNumberValue;
								if (!double.IsNaN(formulaNumberValue))
								{
									num2 = 3;
									continue;
								}
								return false;
							}
							case 8:
							{
								CellFormatType cellFormatType;
								if (cellFormatType != CellFormatType.DateTime)
								{
									num2 = 6;
									continue;
								}
								int num3;
								num3++;
								num2 = 0;
								continue;
							}
							case 9:
								return true;
							case 10:
								goto IL_6E;
							case 11:
								goto IL_E9;
							}
							break;
							IL_6E:
							num2 = 2;
							continue;
							IL_E9:
							num2 = 1;
							continue;
							IL_F9:
							num2 = 9;
						}
					}
					return true;
					IL_173:
					return false;
				}
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00087580 File Offset: 0x00086580
		public bool HasFormulaNumberValue
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num > lastRow)
								{
									goto IL_FC;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 6;
								continue;
							}
							case 1:
								return true;
							case 2:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 4;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FC;
								default:
								{
									if (false)
									{
									}
									double formulaNumberValue = this.\u171D.GetFormulaNumberValue(num, num3);
									num2 = 10;
									continue;
								}
								}
								break;
							}
							case 3:
								goto IL_EC;
							case 4:
								num++;
								num2 = 11;
								continue;
							case 5:
							{
								double formulaNumberValue;
								CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(formulaNumberValue);
								num2 = 9;
								continue;
							}
							case 6:
								goto IL_69;
							case 7:
								goto IL_176;
							case 8:
								goto IL_69;
							case 9:
							{
								CellFormatType cellFormatType;
								if (cellFormatType == CellFormatType.DateTime)
								{
									num2 = 7;
									continue;
								}
								int num3;
								num3++;
								num2 = 8;
								continue;
							}
							case 10:
							{
								if (true)
								{
								}
								double formulaNumberValue;
								if (!double.IsNaN(formulaNumberValue))
								{
									num2 = 5;
									continue;
								}
								return false;
							}
							case 11:
								goto IL_EC;
							}
							break;
							IL_69:
							num2 = 2;
							continue;
							IL_EC:
							num2 = 0;
							continue;
							IL_FC:
							num2 = 1;
						}
					}
					return true;
					IL_176:
					return false;
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00087708 File Offset: 0x00086708
		public bool HasFormulaStringValue
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
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 9;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 4;
									continue;
								}
								num2 = 6;
								continue;
							}
							case 1:
								goto IL_66;
							case 2:
								return false;
							case 3:
								goto IL_DD;
							case 4:
								num++;
								num2 = 3;
								continue;
							case 5:
								goto IL_66;
							case 6:
							{
								IL_BB:
								int num3;
								if (this.\u171D[num, num3].FormulaStringValue == null)
								{
									num2 = 2;
									continue;
								}
								num3++;
								num2 = 5;
								continue;
							}
							case 7:
								return true;
							case 8:
							{
								if (num > lastRow)
								{
									num2 = 7;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 1;
								continue;
							}
							case 9:
								goto IL_DD;
							}
							break;
							IL_66:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BB;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							IL_DD:
							num2 = 8;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00087850 File Offset: 0x00086850
		public bool IsBlank
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 9;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num > lastRow)
								{
									num2 = 3;
									continue;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 2;
								continue;
							}
							case 1:
							{
								IL_B0:
								int num3;
								if (this.\u171D.GetCellType(num, num3, false) != XlsWorksheet.TRangeValueType.Blank)
								{
									num2 = 5;
									continue;
								}
								num3++;
								num2 = 4;
								continue;
							}
							case 2:
								goto IL_5E;
							case 3:
								goto IL_EA;
							case 4:
								goto IL_5E;
							case 5:
								return false;
							case 6:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 7;
									continue;
								}
								num2 = 1;
								continue;
							}
							case 7:
								num++;
								num2 = 8;
								continue;
							case 8:
								goto IL_CE;
							case 9:
								goto IL_CE;
							}
							break;
							IL_5E:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B0;
							default:
								if (false)
								{
								}
								num2 = 6;
								continue;
							}
							IL_CE:
							num2 = 0;
						}
					}
					return false;
					IL_EA:
					if (true)
					{
					}
					return true;
				}
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00087990 File Offset: 0x00086990
		public bool HasPictures
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								try
								{
									num = 2;
									bool result;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_9E;
										case 1:
										{
											IEnumerator enumerator;
											if (!enumerator.MoveNext())
											{
												num = 8;
												continue;
											}
											IPictureShape pictureShape = (IPictureShape)enumerator.Current;
											num = 7;
											continue;
										}
										case 3:
										{
											IPictureShape pictureShape;
											ExcelPicture excelPicture = pictureShape as ExcelPicture;
											Rectangle rectangle = new Rectangle(excelPicture.LeftColumn, excelPicture.TopRow, excelPicture.RightColumn - excelPicture.LeftColumn + 1, excelPicture.BottomRow - excelPicture.TopRow + 1);
											num = 6;
											continue;
										}
										case 4:
											result = true;
											num = 0;
											continue;
										case 5:
											goto IL_163;
										case 6:
										{
											Rectangle rectangle;
											Rectangle rect;
											if (rectangle.IntersectsWith(rect))
											{
												num = 4;
												continue;
											}
											break;
										}
										case 7:
										{
											IPictureShape pictureShape;
											if (pictureShape is ExcelPicture)
											{
												num = 3;
												continue;
											}
											break;
										}
										case 8:
											num = 5;
											continue;
										}
										IL_A3:
										num = 1;
										continue;
										goto IL_A3;
									}
									IL_9E:
									return result;
									IL_163:
									return false;
								}
								finally
								{
									for (;;)
									{
										if (true)
										{
										}
										IEnumerator enumerator;
										IDisposable disposable = enumerator as IDisposable;
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 0:
												if (disposable != null)
												{
													num = 1;
													continue;
												}
												goto IL_1B5;
											case 1:
												disposable.Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_1B3;
											}
											break;
										}
									}
									IL_1B3:
									IL_1B5:;
								}
								goto IL_1B6;
							case 1:
								goto IL_1B6;
							case 2:
							{
								if (!this.\u171D.HasPictures)
								{
									num = 1;
									continue;
								}
								Rectangle rect = new Rectangle(this.Column, this.Row, this.LastColumn - this.Column + 1, this.LastRow - this.Row + 1);
								IEnumerator enumerator = this.\u171D.Pictures.GetEnumerator();
								num = 0;
								continue;
							}
							}
							break;
							IL_1B6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_1CC;
							}
						}
					}
					IL_1CC:
					if (false)
					{
					}
					return false;
				}
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00087BE8 File Offset: 0x00086BE8
		public bool HasBoolean
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 1;
						for (;;)
						{
							int num3;
							XlsWorksheet.TRangeValueType cellType;
							switch (num2)
							{
							case 0:
								return true;
							case 1:
								goto IL_B7;
							case 2:
								goto IL_5E;
							case 3:
								return false;
							case 4:
								goto IL_B7;
							case 5:
								goto IL_A4;
							case 6:
							{
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 9;
									continue;
								}
								cellType = this.\u171D.GetCellType(num, num3, false);
								num2 = 5;
								continue;
							}
							case 7:
								goto IL_5E;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A4;
								default:
								{
									if (false)
									{
									}
									if (num > lastRow)
									{
										num2 = 0;
										continue;
									}
									if (true)
									{
									}
									num3 = this.Column;
									int lastColumn = this.LastColumn;
									num2 = 2;
									continue;
								}
								}
								break;
							case 9:
								num++;
								num2 = 4;
								continue;
							}
							break;
							IL_5E:
							num2 = 6;
							continue;
							IL_A4:
							if (cellType != XlsWorksheet.TRangeValueType.Boolean)
							{
								num2 = 3;
								continue;
							}
							num3++;
							num2 = 7;
							continue;
							IL_B7:
							num2 = 8;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00087D2C File Offset: 0x00086D2C
		public bool HasDateTime
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return true;
							case 1:
								goto IL_DE;
							case 2:
							{
								double number;
								if (!double.IsNaN(number))
								{
									num2 = 7;
									continue;
								}
								return false;
							}
							case 3:
								goto IL_DE;
							case 4:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 10;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_EE;
								default:
								{
									if (false)
									{
									}
									double number = this.\u171D.GetNumber(num, num3);
									num2 = 2;
									continue;
								}
								}
								break;
							}
							case 5:
								goto IL_66;
							case 6:
								goto IL_170;
							case 7:
							{
								double number;
								CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(number);
								num2 = 11;
								continue;
							}
							case 8:
							{
								if (num > lastRow)
								{
									goto IL_EE;
								}
								int num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 5;
								continue;
							}
							case 9:
								goto IL_66;
							case 10:
								if (true)
								{
								}
								num++;
								num2 = 3;
								continue;
							case 11:
							{
								CellFormatType cellFormatType;
								if (cellFormatType != CellFormatType.DateTime)
								{
									num2 = 6;
									continue;
								}
								int num3;
								num3++;
								num2 = 9;
								continue;
							}
							}
							break;
							IL_66:
							num2 = 4;
							continue;
							IL_DE:
							num2 = 8;
							continue;
							IL_EE:
							num2 = 0;
						}
					}
					return true;
					IL_170:
					return false;
				}
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00087EAC File Offset: 0x00086EAC
		public bool HasNumber
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 11;
						for (;;)
						{
							CellFormatType cellFormatType;
							int num3;
							switch (num2)
							{
							case 0:
								goto IL_138;
							case 1:
								if (cellFormatType == CellFormatType.Unknown)
								{
									num2 = 7;
									continue;
								}
								goto IL_8F;
							case 2:
								return true;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_17A;
								default:
									if (false)
									{
									}
									goto IL_159;
								}
								break;
							case 4:
								goto IL_138;
							case 5:
								goto IL_17A;
							case 6:
								goto IL_8F;
							case 7:
								num2 = 16;
								continue;
							case 8:
							{
								if (num > lastRow)
								{
									num2 = 14;
									continue;
								}
								num3 = this.Column;
								int lastColumn = this.LastColumn;
								num2 = 0;
								continue;
							}
							case 9:
								if (cellFormatType != CellFormatType.DateTime)
								{
									num2 = 2;
									continue;
								}
								goto IL_7D;
							case 10:
							{
								double number;
								if (!double.IsNaN(number))
								{
									num2 = 12;
									continue;
								}
								goto IL_7D;
							}
							case 11:
								goto IL_159;
							case 12:
							{
								double number;
								cellFormatType = this.InnerNumberFormat.ᜀ(number);
								num2 = 1;
								continue;
							}
							case 13:
							{
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 15;
									continue;
								}
								double number = this.\u171D.GetNumber(num, num3);
								if (true)
								{
								}
								num2 = 10;
								continue;
							}
							case 14:
								return false;
							case 15:
								num++;
								num2 = 3;
								continue;
							case 16:
							{
								double number;
								if (number == 0.0)
								{
									num2 = 5;
									continue;
								}
								goto IL_8F;
							}
							}
							break;
							IL_7D:
							num3++;
							num2 = 4;
							continue;
							IL_8F:
							num2 = 9;
							continue;
							IL_138:
							num2 = 13;
							continue;
							IL_159:
							num2 = 8;
							continue;
							IL_17A:
							cellFormatType = this.InnerNumberFormat.ᜀ(1.0);
							num2 = 6;
						}
					}
					return true;
				}
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x000880C4 File Offset: 0x000870C4
		public bool HasString
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜆ();
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 1;
						for (;;)
						{
							int num3;
							XlsWorksheet.TRangeValueType cellType;
							switch (num2)
							{
							case 0:
								goto IL_5E;
							case 1:
								goto IL_C0;
							case 2:
							{
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 8;
									continue;
								}
								cellType = this.\u171D.GetCellType(num, num3, false);
								num2 = 7;
								continue;
							}
							case 3:
								goto IL_C0;
							case 4:
								return false;
							case 5:
								return true;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_AC;
								default:
								{
									if (false)
									{
									}
									if (num > lastRow)
									{
										num2 = 5;
										continue;
									}
									num3 = this.Column;
									int lastColumn = this.LastColumn;
									num2 = 9;
									continue;
								}
								}
								break;
							case 7:
								goto IL_AC;
							case 8:
								num++;
								num2 = 3;
								continue;
							case 9:
								goto IL_5E;
							}
							break;
							IL_5E:
							num2 = 2;
							continue;
							IL_AC:
							if (cellType != XlsWorksheet.TRangeValueType.String)
							{
								num2 = 4;
								continue;
							}
							if (true)
							{
							}
							num3++;
							num2 = 0;
							continue;
							IL_C0:
							num2 = 6;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0008820C File Offset: 0x0008720C
		public ICommentShape Comment
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.AddComment();
							num = 2;
							continue;
						case 1:
							goto IL_A1;
						case 2:
							goto IL_94;
						case 3:
							if (!this.IsSingleCell)
							{
								goto IL_CC;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A1;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 4:
							num = 1;
							continue;
						}
						break;
						IL_A1:
						if (this.\u171D.InnerComments[this.FirstRow, this.FirstColumn] != null)
						{
							goto IL_65;
						}
						num = 0;
					}
				}
				IL_65:
				return this.\u171D.InnerComments[this.FirstRow, this.FirstColumn];
				IL_94:
				goto IL_65;
				IL_CC:
				if (true)
				{
				}
				return ((spr\u17FF)this.Application).ᜁ(this);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00088300 File Offset: 0x00087300
		public bool HasComment
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
				return this.\u171D.InnerComments[this.FirstRow, this.FirstColumn] != null;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00088360 File Offset: 0x00087360
		public IRichTextString RichText
		{
			get
			{
				for (;;)
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜆ();
						num = 0;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.m_rtfString == null)
							{
								num = 2;
								continue;
							}
							goto IL_72;
						case 1:
							goto IL_70;
						case 2:
							if (true)
							{
							}
							this.CreateRichTextString();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_70:
				IL_72:
				return this.m_rtfString;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000883E8 File Offset: 0x000873E8
		public bool HasRichText
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_72;
						case 1:
							num = 0;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_72;
							default:
								if (false)
								{
								}
								if (this.IsSingleCell)
								{
									num = 1;
									continue;
								}
								return false;
							}
							break;
						case 3:
							goto IL_8D;
						}
						break;
						IL_72:
						if (true)
						{
						}
						if (!this.HasString)
						{
							return false;
						}
						num = 3;
					}
				}
				IL_8D:
				return this.RichText.IsFormatted;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00088488 File Offset: 0x00087488
		public bool HasMerged
		{
			get
			{
				switch (0)
				{
				default:
				{
					Rectangle a_;
					spr\u25A6.ᜀ ᜀ;
					spr\u25A6.ᜀ obj;
					for (;;)
					{
						if (true)
						{
						}
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_7B:
							num = 3;
							break;
						default:
							if (false)
							{
							}
							this.ᜆ();
							a_ = new Rectangle(this.FirstColumn - 1, this.FirstRow - 1, 0, 0);
							num = 0;
							break;
						}
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (this.IsSingleCell)
								{
									goto IL_7B;
								}
								Rectangle a_2 = new Rectangle(this.LastColumn - 1, this.LastRow - 1, 0, 0);
								ᜀ = this.\u171D.MergeCells.ᜂ(a_);
								obj = this.\u171D.MergeCells.ᜂ(a_2);
								num = 1;
								continue;
							}
							case 1:
								if (ᜀ != null)
								{
									num = 2;
									continue;
								}
								return false;
							case 2:
								goto IL_F0;
							case 3:
								goto IL_84;
							}
							break;
						}
					}
					IL_84:
					return this.\u171D.MergeCells.ᜂ(a_) != null;
					IL_F0:
					return ᜀ.Equals(obj);
				}
				}
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000885A0 File Offset: 0x000875A0
		public IXLSRange MergeArea
		{
			get
			{
				this.ᜆ();
				spr\u25A6.ᜀ ᜀ = this.ParentMergeRegion;
				if (ᜀ == null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_28;
						}
					}
					IL_28:
					if (false)
					{
					}
					return null;
				}
				if (true)
				{
				}
				return this.\u171D[ᜀ.ᜂ() + 1, ᜀ.ᜅ() + 1, ᜀ.ᜇ() + 1, ᜀ.ᜃ() + 1];
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0008861C File Offset: 0x0008761C
		public bool IsInitialized
		{
			get
			{
				this.ᜆ();
				if (this.IsBlank)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_26;
						}
					}
					IL_26:
					if (false)
					{
					}
					if (true)
					{
					}
					return this.HasStyle;
				}
				return true;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00088670 File Offset: 0x00087670
		public bool HasStyle
		{
			get
			{
				bool result;
				for (;;)
				{
					this.ᜆ();
					int num = 1;
					for (;;)
					{
						bool flag;
						int extendedFormatIndex;
						bool flag2;
						bool flag3;
						switch (num)
						{
						case 0:
							flag = this.m_style.IsInitialized;
							goto IL_C1;
						case 1:
							if (this.m_style != null)
							{
								num = 7;
								continue;
							}
							num = 2;
							continue;
						case 2:
							flag = false;
							goto IL_C1;
						case 3:
							flag2 = (extendedFormatIndex != this.m_book.DefaultXFIndex);
							goto IL_9B;
						case 4:
							if (!flag3)
							{
								num = 9;
								continue;
							}
							return true;
						case 5:
							num = 3;
							continue;
						case 6:
							if (extendedFormatIndex == 0)
							{
								num = 8;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9C;
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
						case 7:
							num = 0;
							continue;
						case 8:
							flag2 = false;
							goto IL_9B;
						case 9:
							return result;
						}
						break;
						IL_9C:
						num = 4;
						continue;
						IL_9B:
						result = flag2;
						goto IL_9C;
						IL_C1:
						flag3 = flag;
						extendedFormatIndex = (int)this.ExtendedFormatIndex;
						num = 6;
					}
				}
				return result;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0008878C File Offset: 0x0008778C
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x000887EC File Offset: 0x000877EC
		public bool IsWrapText
		{
			get
			{
				this.ᜆ();
				if (this.IsSingleCell)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_26;
						}
					}
					IL_26:
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ᜂ();
				}
				return sprṔ.ᜁ(this.CellsList);
			}
			set
			{
				for (;;)
				{
					this.ᜆ();
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_CD;
						case 1:
							goto IL_105;
						case 2:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 6;
								continue;
							}
							int num3 = this.FirstColumn;
							num = 7;
							continue;
						}
						case 3:
						{
							if (this.IsSingleCell)
							{
								num = 9;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 0;
							continue;
						}
						case 4:
							goto IL_57;
						case 5:
							goto IL_CD;
						case 6:
							goto IL_EC;
						case 7:
							goto IL_57;
						case 8:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 10;
								continue;
							}
							if (true)
							{
							}
							int num2;
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.IsWrapText = value;
							num3++;
							num = 4;
							continue;
						}
						case 9:
							this.Style.WrapText = value;
							num = 1;
							continue;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_57;
							default:
							{
								if (false)
								{
								}
								int num2;
								num2++;
								num = 5;
								continue;
							}
							}
							break;
						}
						break;
						IL_57:
						num = 8;
						continue;
						IL_CD:
						num = 2;
					}
				}
				IL_EC:
				IL_105:
				this.SetChanged();
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0008894C File Offset: 0x0008794C
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000889B0 File Offset: 0x000879B0
		public IgnoreErrorType IgnoreErrorOptions
		{
			get
			{
				if (true)
				{
				}
				spr\u2622 spr_u = this.\u171D.ErrorIndicators;
				Rectangle[] rectangles = this.GetRectangles();
				spr\u1F7E spr_u1F7E = spr_u.ᜀ(rectangles);
				if (spr_u1F7E == null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					if (false)
					{
					}
					return IgnoreErrorType.None;
				}
				return spr_u1F7E.ᜁ();
			}
			set
			{
				spr\u2622 spr_u = this.\u171D.ErrorIndicators;
				Rectangle rectangle = Rectangle.FromLTRB(this.Column - 1, this.Row - 1, this.LastColumn - 1, this.LastRow - 1);
				if (value == IgnoreErrorType.None)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_4D;
						}
					}
					IL_4D:
					if (true)
					{
					}
					if (false)
					{
					}
					spr_u.ᜁ(new Rectangle[]
					{
						rectangle
					});
					return;
				}
				spr\u1F7E a_ = new spr\u1F7E(rectangle, value);
				spr_u.ᜀ(a_);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00088A50 File Offset: 0x00087A50
		public bool HasExternalFormula
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						int num = this.Row;
						int lastRow = this.LastRow;
						int num2 = 7;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_58;
							case 1:
								goto IL_B0;
							case 2:
								num++;
								num2 = 1;
								continue;
							case 3:
							{
								int num3;
								int lastColumn;
								if (num3 > lastColumn)
								{
									num2 = 2;
									continue;
								}
								num2 = 5;
								continue;
							}
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return true;
								default:
								{
									if (false)
									{
									}
									if (num > lastRow)
									{
										num2 = 9;
										continue;
									}
									int num3 = this.Column;
									int lastColumn = this.LastColumn;
									num2 = 6;
									continue;
								}
								}
								break;
							case 5:
							{
								if (true)
								{
								}
								int num3;
								if (!this.\u171D.IsExternalFormula(num, num3))
								{
									num2 = 8;
									continue;
								}
								num3++;
								num2 = 0;
								continue;
							}
							case 6:
								goto IL_58;
							case 7:
								goto IL_B0;
							case 8:
								return false;
							case 9:
								return true;
							}
							break;
							IL_58:
							num2 = 3;
							continue;
							IL_B0:
							num2 = 4;
						}
					}
					return false;
				}
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00088B88 File Offset: 0x00087B88
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00088BD0 File Offset: 0x00087BD0
		public bool? IsStringsPreserved
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
				return this.\u171D.ᜀ(this);
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
				this.\u171D.ᜀ(this, value);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00088C18 File Offset: 0x00087C18
		internal spr\u1DF5 Application
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
				return this.\u171D.ReservedHandle;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00088C60 File Offset: 0x00087C60
		public object Parent
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
				return this.\u171D;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00088CA4 File Offset: 0x00087CA4
		private spr\u17FF AppImplementation
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
				return this.\u171D.AppImplementation;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00088CEC File Offset: 0x00087CEC
		public string RangeGlobalAddress
		{
			get
			{
				int a_ = 15;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u171D.QuotedName + RecordTableEnumerator.b("摄", a_) + this.RangeGlobalAddressWithoutSheetName;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00088D58 File Offset: 0x00087D58
		public string RangeGlobalAddressWithoutSheetName
		{
			get
			{
				int a_ = 13;
				string empty = string.Empty;
				string str = sprṔ.ᜁ(this.FirstColumn, this.FirstRow);
				if (this.IsSingleCell)
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
							continue;
						}
						break;
					}
					if (false)
					{
					}
					return empty + str;
				}
				string str2 = sprṔ.ᜁ(this.LastColumn, this.LastRow);
				return empty + str + RecordTableEnumerator.b("祂", a_) + str2;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00088DF0 File Offset: 0x00087DF0
		internal List<CellRange> CellsList
		{
			get
			{
				for (;;)
				{
					this.ᜆ();
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 6;
							continue;
						case 1:
							this.InfillCells();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5C;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 2:
							goto IL_44;
						case 3:
							if (this.ᜡ == null)
							{
								num = 4;
								continue;
							}
							goto IL_C5;
						case 4:
							goto IL_5C;
						case 5:
							if (this.ᜡ == null)
							{
								num = 0;
								continue;
							}
							goto IL_44;
						case 6:
							if (!this.ᜢ)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_44;
						}
						break;
						IL_44:
						num = 3;
					}
				}
				IL_5C:
				throw new ArgumentNullException();
				IL_C5:
				return this.ᜡ;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00088EC8 File Offset: 0x00087EC8
		protected internal bool IsSingleCell
		{
			get
			{
				if (this.m_iLeftColumn == this.m_iRightColumn)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_26;
						}
					}
					IL_26:
					if (true)
					{
					}
					if (false)
					{
					}
					return this.m_iTopRow == this.m_iBottomRow;
				}
				return false;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00088F24 File Offset: 0x00087F24
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x00088F68 File Offset: 0x00087F68
		protected internal int FirstRow
		{
			[DebuggerStepThrough]
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
				return this.m_iTopRow;
			}
			set
			{
				int a_ = 14;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (value > this.m_book.MaxRowCount)
						{
							num = 2;
							continue;
						}
						num = 4;
						continue;
					case 2:
						goto IL_BC;
					case 4:
						if (value != this.FirstRow)
						{
							num = 5;
							continue;
						}
						return;
					case 5:
						this.m_iTopRow = value;
						this.OnFirstRowChanged();
						num = 0;
						continue;
					case 6:
						num = 1;
						continue;
					}
					if (value < 1)
					{
						break;
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
						num = 6;
						break;
					}
				}
				IL_84:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Ƀ⽅㩇㥉㡋ᱍ㽏║", a_));
				IL_BC:
				if (true)
				{
				}
				goto IL_84;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00089058 File Offset: 0x00088058
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0008909C File Offset: 0x0008809C
		protected internal int FirstColumn
		{
			[DebuggerStepThrough]
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
				return this.m_iLeftColumn;
			}
			set
			{
				int a_ = 2;
				if (true)
				{
				}
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_C4;
					case 1:
						if (value > this.m_book.MaxColumnCount)
						{
							num = 0;
							continue;
						}
						num = 3;
						continue;
					case 2:
						num = 1;
						continue;
					case 3:
						if (value != this.FirstColumn)
						{
							num = 4;
							continue;
						}
						return;
					case 4:
						this.m_iLeftColumn = value;
						this.OnFirstColumnChanged();
						num = 5;
						continue;
					case 5:
						return;
					}
					if (value < 1)
					{
						break;
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
						num = 2;
						break;
					}
				}
				IL_8C:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("縷匹主䴽㐿၁⭃ㅅ", a_));
				IL_C4:
				goto IL_8C;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0008918C File Offset: 0x0008818C
		protected internal string InnerCellName
		{
			get
			{
				if (this.IsSingleCell)
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
							continue;
						}
						break;
					}
					if (false)
					{
					}
					return sprṔ.ᜂ(this.FirstColumn, this.FirstRow);
				}
				return null;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000891E4 File Offset: 0x000881E4
		protected internal long CellIndex
		{
			get
			{
				if (this.IsSingleCell)
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
							continue;
						}
						break;
					}
					if (false)
					{
					}
					return sprṔ.ᜀ(this.FirstColumn, this.FirstRow);
				}
				return -1L;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00089240 File Offset: 0x00088240
		internal XlsRange.TCellType CellType
		{
			get
			{
				BiffRecordRaw biffRecordRaw = this.Record;
				if (biffRecordRaw != null)
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
							continue;
						}
						break;
					}
					if (false)
					{
					}
					return (XlsRange.TCellType)this.Record.TypeCode;
				}
				return XlsRange.TCellType.Blank;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00089298 File Offset: 0x00088298
		[CLSCompliant(false)]
		protected internal ushort StyleXFIndex
		{
			get
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.m_style != null)
						{
							num = 1;
							continue;
						}
						num = 2;
						continue;
					case 1:
						goto IL_AA;
					case 2:
						goto IL_5E;
					case 3:
						num = 0;
						continue;
					case 4:
						goto IL_6E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5E:
						if (this.Record == null)
						{
							goto IL_C7;
						}
						num = 4;
						break;
					default:
						if (false)
						{
						}
						if (!this.IsSingleCell)
						{
							goto IL_C7;
						}
						num = 3;
						break;
					}
				}
				IL_6E:
				spr\u23A5 spr_u23A = (spr\u23A5)this.Record;
				return spr_u23A.ᜆ();
				IL_AA:
				if (true)
				{
				}
				return (ushort)this.m_style.Wrapped.ᜠ();
				IL_C7:
				return (ushort)this.m_book.DefaultXFIndex;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00089378 File Offset: 0x00088378
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x000893CC File Offset: 0x000883CC
		[CLSCompliant(false)]
		public ushort ExtendedFormatIndex
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
				return (ushort)this.\u171D.ᜅ(this.m_iTopRow, this.m_iLeftColumn);
			}
			set
			{
				int a_ = 7;
				if (!this.IsSingleCell)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_29;
						}
					}
					IL_29:
					if (true)
					{
					}
					if (false)
					{
					}
					throw new ArgumentException(RecordTableEnumerator.b("砼䜾㕀♂⭄⍆ⱈ⽊ୌ⁎⍐㹒㑔⍖ၘ㕚㥜㩞ᥠ䍢٤٦ݨ䭪ཬ੮兰ٲٴቶᵸ孺᭼ၾꎂ놐ﮖ떚", a_));
				}
				this.SetExtendedFormatIndex((int)value);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00089438 File Offset: 0x00088438
		internal sprᨾ.ᜀ RKSubRecord
		{
			get
			{
				int a_ = 15;
				if (this.CellType != XlsRange.TCellType.RK)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2E;
						}
					}
					IL_2E:
					if (false)
					{
					}
					if (true)
					{
					}
					throw new ArgumentException(RecordTableEnumerator.b("ل♆❈歊⍌⁎═獒㙔㡖㝘ⵚ㡜ⵞᕠ䍢ᝤ٦ݨ౪࡬佮հᱲ啴Ѷ౸᥺ོ᩾ꞈ", a_));
				}
				return new sprᨾ.ᜀ(this.StyleXFIndex, sprỔ.ᜀ(((sprỔ)this.Record).ᜀ()));
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000894C0 File Offset: 0x000884C0
		protected internal XlsWorkbook Workbook
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
				return this.m_book;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00089504 File Offset: 0x00088504
		private spr\u25A6.ᜀ ParentMergeRegion
		{
			get
			{
				switch (0)
				{
				default:
				{
					Rectangle a_;
					for (;;)
					{
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_6D:
							num = 3;
							break;
						default:
							if (false)
							{
							}
							a_ = new Rectangle(this.FirstColumn - 1, this.FirstRow - 1, 0, 0);
							num = 2;
							break;
						}
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								spr\u25A6.ᜀ ᜀ;
								spr\u25A6.ᜀ a_2;
								if (!spr\u25A6.ᜀ.ᜀ(ᜀ, a_2))
								{
									num = 1;
									continue;
								}
								return ᜀ;
							}
							case 1:
								goto IL_E2;
							case 2:
							{
								if (this.IsSingleCell)
								{
									goto IL_6D;
								}
								Rectangle a_3 = new Rectangle(this.LastColumn - 1, this.LastRow - 1, 0, 0);
								spr\u25A6.ᜀ ᜀ = this.\u171D.MergeCells.ᜂ(a_);
								spr\u25A6.ᜀ a_2 = this.\u171D.MergeCells.ᜂ(a_3);
								num = 0;
								continue;
							}
							case 3:
								goto IL_76;
							}
							break;
						}
					}
					IL_76:
					return this.\u171D.MergeCells.ᜂ(a_);
					IL_E2:
					if (true)
					{
					}
					return null;
				}
				}
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00089610 File Offset: 0x00088610
		protected internal XlsWorksheet InnerWorksheet
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
				return this.\u171D;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00089654 File Offset: 0x00088654
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x000896AC File Offset: 0x000886AC
		internal BiffRecordRaw Record
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
				return (BiffRecordRaw)this.\u171D.ᜃ(this.FirstRow, this.FirstColumn);
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
				this.\u171D.CellRecords.ᜀ(value, false);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000896FC File Offset: 0x000886FC
		internal Dictionary<spr\u225F, object> FormulaArrays
		{
			get
			{
				switch (0)
				{
				default:
				{
					Dictionary<spr\u225F, object> dictionary;
					for (;;)
					{
						dictionary = null;
						int num = 4;
						for (;;)
						{
							int num3;
							switch (num)
							{
							case 0:
								goto IL_168;
							case 1:
								goto IL_126;
							case 2:
								goto IL_8E;
							case 3:
								return dictionary;
							case 4:
							{
								if (this.IsSingleCell)
								{
									num = 13;
									continue;
								}
								dictionary = new Dictionary<spr\u225F, object>();
								Dictionary<long, object> dictionary2 = new Dictionary<long, object>();
								XlsCellRecordCollection cellRecords = this.\u171D.CellRecords;
								int num2 = this.FirstRow;
								int lastRow = this.LastRow;
								num = 1;
								continue;
							}
							case 5:
							{
								int num2;
								int lastRow;
								if (num2 > lastRow)
								{
									num = 12;
									continue;
								}
								num3 = this.FirstColumn;
								int lastColumn = this.LastColumn;
								num = 2;
								continue;
							}
							case 6:
								goto IL_126;
							case 7:
							{
								int num2;
								num2++;
								num = 6;
								continue;
							}
							case 8:
							{
								Dictionary<long, object> dictionary2;
								long key;
								if (!dictionary2.ContainsKey(key))
								{
									num = 11;
									continue;
								}
								goto IL_168;
							}
							case 9:
							{
								spr\u225F spr_u225F;
								long key = sprṔ.ᜀ(spr_u225F.ᜈ(), spr_u225F.ᜉ());
								num = 8;
								continue;
							}
							case 10:
							{
								spr\u225F spr_u225F;
								if (spr_u225F != null)
								{
									num = 9;
									continue;
								}
								goto IL_168;
							}
							case 11:
							{
								spr\u225F spr_u225F;
								dictionary[spr_u225F] = null;
								Dictionary<long, object> dictionary2;
								long key;
								dictionary2.Add(key, null);
								num = 0;
								continue;
							}
							case 12:
							{
								Dictionary<long, object> dictionary2;
								dictionary2.Clear();
								num = 15;
								continue;
							}
							case 13:
							{
								spr\u225F spr_u225F2 = this.\u171D.CellRecords.ᜁ(this.m_iTopRow, this.m_iLeftColumn);
								num = 18;
								continue;
							}
							case 14:
							{
								int lastColumn;
								if (num3 <= lastColumn)
								{
									if (true)
									{
									}
									XlsCellRecordCollection cellRecords;
									int num2;
									spr\u225F spr_u225F = cellRecords.ᜁ(num2, num3);
									num = 10;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_17A;
								default:
									if (false)
									{
									}
									num = 7;
									continue;
								}
								break;
							}
							case 15:
								return dictionary;
							case 16:
							{
								dictionary = new Dictionary<spr\u225F, object>();
								spr\u225F spr_u225F2;
								dictionary[spr_u225F2] = null;
								num = 3;
								continue;
							}
							case 17:
								goto IL_17A;
							case 18:
							{
								spr\u225F spr_u225F2;
								if (spr_u225F2 != null)
								{
									num = 16;
									continue;
								}
								return dictionary;
							}
							}
							break;
							IL_8E:
							num = 14;
							continue;
							IL_17A:
							goto IL_8E;
							IL_126:
							num = 5;
							continue;
							IL_168:
							num3++;
							num = 17;
						}
					}
					return dictionary;
				}
				}
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0008998C File Offset: 0x0008898C
		internal bool AreFormulaArraysNotSeparated
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
				return this.ᜀ(null);
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000899D0 File Offset: 0x000889D0
		internal bool ᜀ(ICollection<spr\u225F> A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4E;
					case 1:
						goto IL_22E;
					}
					if (A_0 == null)
					{
						num = 0;
					}
					else
					{
						int firstRow = this.FirstRow;
						int firstColumn = this.FirstColumn;
						int lastRow = this.LastRow;
						int lastColumn = this.LastColumn;
						IEnumerator<spr\u225F> enumerator = A_0.GetEnumerator();
						num = 1;
					}
				}
				IL_4E:
				IL_1E8:
				throw new ArgumentNullException(RecordTableEnumerator.b("堺刼匾݀ⱂ㝄⩆㱈❊ⱌ㱎", a_));
				IL_22E:
				try
				{
					num = 9;
					bool result;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_19E;
						case 1:
							num = 3;
							continue;
						case 2:
							num = 7;
							continue;
						case 3:
						{
							int lastColumn;
							spr\u225F spr_u225F;
							if (spr_u225F.ᜀ() + 1 > lastColumn)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 4:
							num = 0;
							continue;
						case 5:
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
								int lastRow;
								spr\u225F spr_u225F;
								if (spr_u225F.\u170D() + 1 <= lastRow)
								{
									num = 2;
									continue;
								}
								goto IL_9B;
							}
							}
							break;
						case 6:
							goto IL_9B;
						case 7:
						{
							int firstColumn;
							spr\u225F spr_u225F;
							if (spr_u225F.ᜈ() + 1 >= firstColumn)
							{
								num = 1;
								continue;
							}
							goto IL_9B;
						}
						case 8:
							goto IL_A7;
						case 10:
							num = 5;
							continue;
						case 11:
						{
							IEnumerator<spr\u225F> enumerator;
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							spr\u225F spr_u225F = enumerator.Current;
							num = 12;
							continue;
						}
						case 12:
						{
							int firstRow;
							spr\u225F spr_u225F;
							if (spr_u225F.ᜉ() + 1 >= firstRow)
							{
								num = 10;
								continue;
							}
							goto IL_9B;
						}
						}
						goto IL_99;
						IL_9B:
						result = false;
						num = 8;
						continue;
						IL_AC:
						num = 11;
						continue;
						IL_99:
						goto IL_AC;
					}
					IL_A7:
					return result;
					IL_19E:
					return true;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						IEnumerator<spr\u225F> enumerator;
						switch (num)
						{
						case 1:
							if (true)
							{
							}
							enumerator.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_1E5;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 1;
					}
					IL_1E5:;
				}
				goto IL_1E8;
			}
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00089C30 File Offset: 0x00088C30
		public int CellsCount
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.FirstColumn == 0)
						{
							num = 1;
							continue;
						}
						goto IL_72;
					case 1:
						goto IL_70;
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
							break;
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (this.FirstRow == 0)
					{
						break;
					}
					num = 3;
				}
				return 0;
				IL_70:
				return 0;
				IL_72:
				return (this.LastRow - this.FirstRow + 1) * (this.LastColumn - this.FirstColumn + 1);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00089CD8 File Offset: 0x00088CD8
		internal sprᤅ InnerNumberFormat
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
				int numberFormatIndex = this.m_book.GetExtFormat((int)this.ExtendedFormatIndex).NumberFormatIndex;
				return this.m_book.InnerFormats.ᜁ(numberFormatIndex);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00089D3C File Offset: 0x00088D3C
		public string RangeGlobalAddress2007
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
				return this.RangeGlobalAddress;
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00089D80 File Offset: 0x00088D80
		protected int CurrentStyleNumber(string pre)
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
			return this.m_book.CurrentStyleNumber(pre);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00089DC8 File Offset: 0x00088DC8
		protected void OnLastColumnChanged()
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

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00089E04 File Offset: 0x00088E04
		protected void OnFirstColumnChanged()
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

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00089E40 File Offset: 0x00088E40
		protected void OnLastRowChanged()
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
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00089E7C File Offset: 0x00088E7C
		protected void OnFirstRowChanged()
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

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00089EB8 File Offset: 0x00088EB8
		internal void ᜀ(XlsRange.TCellType A_0)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_EE;
				case 2:
					this.m_rtfString.Clear();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
				{
					string value;
					if (value.Length != 0)
					{
						num = 2;
						continue;
					}
					goto IL_F0;
				}
				case 4:
				{
					string value;
					if (value != null)
					{
						num = 0;
						continue;
					}
					goto IL_F0;
				}
				case 6:
				{
					string value = this.Value;
					goto IL_77;
				}
				case 7:
					if (this.CellType != XlsRange.TCellType.LabelSST)
					{
						num = 6;
						continue;
					}
					goto IL_F0;
				case 8:
					num = 7;
					continue;
				}
				if (A_0 == XlsRange.TCellType.LabelSST)
				{
					num = 8;
					continue;
				}
				break;
				IL_77:
				if (true)
				{
				}
				num = 4;
			}
			IL_EE:
			IL_F0:
			this.SetChanged();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00089FBC File Offset: 0x00088FBC
		protected void OnValueChanged(string old, string value)
		{
			int a_ = 14;
			switch (0)
			{
			default:
				for (;;)
				{
					this.SetChanged();
					int num = 12;
					for (;;)
					{
						sprᤅ sprᤅ;
						bool? flag;
						bool flag2;
						bool flag3;
						int num2;
						int num3;
						DateTime dateTime;
						bool flag4;
						bool? isStringsPreserved;
						CultureInfo provider;
						double number;
						switch (num)
						{
						case 0:
							goto IL_5C5;
						case 1:
							if (sprᤅ.ᜂ() != RecordTableEnumerator.b("̓⍅♇⽉㹋⽍㱏", a_))
							{
								num = 23;
								continue;
							}
							return;
						case 2:
							num = 37;
							continue;
						case 3:
							num = 14;
							continue;
						case 4:
							if (value == null)
							{
								num = 52;
								continue;
							}
							num = 36;
							continue;
						case 5:
							return;
						case 6:
							goto IL_39E;
						case 7:
							if (flag.GetValueOrDefault())
							{
								num = 54;
								continue;
							}
							num = 35;
							continue;
						case 8:
							num = 22;
							continue;
						case 9:
							goto IL_39E;
						case 10:
							goto IL_4DD;
						case 11:
							flag2 = false;
							num = 9;
							continue;
						case 12:
							if (old != value)
							{
								num = 50;
								continue;
							}
							goto IL_1F7;
						case 13:
							return;
						case 14:
							if (((XlsWorksheet)this.Parent).FormulaEngine != null)
							{
								num = 33;
								continue;
							}
							goto IL_4DD;
						case 15:
							if (!(this.Record is spr\u171D))
							{
								num = 26;
								continue;
							}
							return;
						case 16:
							if (flag2)
							{
								num = 42;
								continue;
							}
							goto IL_39E;
						case 17:
							flag3 = (flag != null);
							goto IL_4C7;
						case 18:
						{
							long ticks;
							if (ticks < XlsRange.\u171B)
							{
								num = 2;
								continue;
							}
							goto IL_3EF;
						}
						case 19:
							num2 = 0;
							goto IL_5A1;
						case 20:
							goto IL_1F7;
						case 21:
							if (num3 > 1)
							{
								num = 46;
								continue;
							}
							goto IL_295;
						case 22:
							flag4 = this.ᜀ(value, out dateTime);
							goto IL_1C9;
						case 23:
							goto IL_18C;
						case 24:
							goto IL_4D8;
						case 25:
							num = 55;
							continue;
						case 26:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2A1;
							default:
								goto IL_581;
							}
							break;
						case 27:
							if (value == old)
							{
								num = 5;
								continue;
							}
							isStringsPreserved = this.IsStringsPreserved;
							num = 39;
							continue;
						case 28:
						{
							bool flag5 = double.TryParse(value, (Array.IndexOf<string>(this.ᜥ, Thread.CurrentThread.CurrentCulture.Name) >= 0) ? NumberStyles.Float : NumberStyles.Any, provider, out number);
							num = 34;
							continue;
						}
						case 29:
							goto IL_64F;
						case 30:
							goto IL_628;
						case 31:
							if (flag2)
							{
								num = 29;
								continue;
							}
							num = 1;
							continue;
						case 32:
							if (value[1] != '&')
							{
								num = 45;
								continue;
							}
							goto IL_295;
						case 33:
							((XlsWorksheet)this.Parent).OnCaculateValueChanged(this.Row, this.Column, value);
							num = 10;
							continue;
						case 34:
						{
							bool flag5;
							if (!flag5)
							{
								num = 8;
								continue;
							}
							num = 53;
							continue;
						}
						case 35:
							flag3 = false;
							goto IL_4C7;
						case 36:
							num2 = value.Length;
							goto IL_5A1;
						case 37:
						{
							long ticks;
							if (ticks != 0L)
							{
								num = 11;
								continue;
							}
							goto IL_3EF;
						}
						case 38:
							if (num3 == 0)
							{
								num = 44;
								continue;
							}
							num = 27;
							continue;
						case 39:
							if (isStringsPreserved == null)
							{
								num = 48;
								continue;
							}
							goto IL_5C5;
						case 40:
							goto IL_3E0;
						case 41:
							num = 21;
							continue;
						case 42:
						{
							long ticks = dateTime.Ticks;
							num = 18;
							continue;
						}
						case 43:
						{
							bool flag5;
							if (!flag5)
							{
								num = 25;
								continue;
							}
							goto IL_628;
						}
						case 44:
							num = 15;
							continue;
						case 45:
							goto IL_566;
						case 46:
							num = 32;
							continue;
						case 47:
							if (this.Parent is XlsWorksheet)
							{
								num = 3;
								continue;
							}
							goto IL_4DD;
						case 48:
							isStringsPreserved = new bool?(this.\u171D.IsStringsPreserved);
							num = 0;
							continue;
						case 49:
							goto IL_2A1;
						case 50:
							this.ᜀ(old, value, this);
							num = 20;
							continue;
						case 51:
							if (value[0] == '=')
							{
								num = 41;
								continue;
							}
							goto IL_295;
						case 52:
							num = 19;
							continue;
						case 53:
							flag4 = false;
							goto IL_1C9;
						case 54:
							num = 17;
							continue;
						case 55:
							if (flag2)
							{
								num = 30;
								continue;
							}
							value = this.ᜅ(value);
							this.RichText.Text = value;
							num = 40;
							continue;
						}
						break;
						IL_1C9:
						flag2 = flag4;
						if (true)
						{
						}
						num = 16;
						continue;
						IL_1F7:
						num = 47;
						continue;
						IL_4C7:
						if (flag3)
						{
							num = 24;
							continue;
						}
						num = 51;
						continue;
						IL_295:
						num = 49;
						continue;
						IL_2A1:
						if (this.ᜂ(value))
						{
							num = 13;
							continue;
						}
						int a_2 = this.m_book.InnerExtFormats.ᜁ((int)this.ExtendedFormatIndex).ᝊ();
						sprᤅ = this.m_book.InnerFormats.ᜁ(a_2);
						dateTime = DateTime.FromOADate(0.0);
						provider = this.AppImplementation.\u171F();
						num = 28;
						continue;
						IL_39E:
						num = 43;
						continue;
						IL_3EF:
						number = dateTime.ToOADate();
						num = 6;
						continue;
						IL_4DD:
						num = 4;
						continue;
						IL_5A1:
						num3 = num2;
						num = 38;
						continue;
						IL_5C5:
						flag = isStringsPreserved;
						num = 7;
						continue;
						IL_628:
						this.SetNumber(number);
						num = 31;
					}
				}
				IL_18C:
				this.FormatType = CellFormatType.Number;
				return;
				IL_3E0:
				return;
				IL_4D8:
				this.Text = value;
				return;
				IL_566:
				this.SetFormula(value);
				return;
				IL_581:
				if (false)
				{
				}
				this.Record = this.ᜀ(TBIFFRecord.Blank);
				return;
				IL_64F:
				this.FormatType = CellFormatType.DateTime;
				return;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0008A684 File Offset: 0x00089684
		private string ᜅ(string A_0)
		{
			int num = 9;
			for (;;)
			{
				spr\u192F spr_u192F;
				switch (num)
				{
				case 0:
					goto IL_B8;
				case 1:
					if (this.m_book.Loading)
					{
						num = 0;
						continue;
					}
					num = 11;
					continue;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_DC;
				case 4:
					goto IL_70;
				case 5:
					this.Style.IsFirstSymbolApostrophe = true;
					A_0 = A_0.Substring(1);
					num = 4;
					continue;
				case 6:
					this.Style.IsFirstSymbolApostrophe = false;
					if (true)
					{
					}
					num = 3;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						if (spr_u192F.\u1713())
						{
							num = 6;
							continue;
						}
						return A_0;
					}
					break;
				case 8:
					num = 10;
					continue;
				case 10:
					if (A_0.Length != 0)
					{
						num = 2;
						continue;
					}
					return A_0;
				case 11:
					if (A_0[0] == '\'')
					{
						num = 5;
						continue;
					}
					goto IL_FE;
				}
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				return A_0;
				IL_FE:
				sprᢖ sprᢖ = this.m_book.InnerExtFormats;
				spr_u192F = sprᢖ.ᜁ((int)this.ExtendedFormatIndex);
				num = 7;
			}
			IL_70:
			return A_0;
			IL_B8:
			return A_0;
			IL_DC:
			return A_0;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0008A7F4 File Offset: 0x000897F4
		protected double ObjectToDouble(object value)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_74;
				case 3:
					goto IL_5E;
				}
				if (!(value is double))
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
					num = 3;
					continue;
				}
				IL_74:
				if (!(value is int))
				{
					goto IL_8D;
				}
				num = 0;
			}
			IL_5E:
			return (double)value;
			IL_84:
			return Convert.ToDouble((int)value);
			IL_8D:
			return double.Parse(value.ToString());
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0008A89C File Offset: 0x0008989C
		protected XlsRange ToggleGroup(GroupByType groupBy, bool isGroup, bool bCollapsed)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int num2;
					spr\u2502 spr_u;
					int num4;
					switch (num)
					{
					case 0:
					{
						num2 = this.FirstRow;
						int num3 = this.LastRow;
						XlsRange.ᜀ ᜀ = new XlsRange.ᜀ(this.ᜁ);
						num = 4;
						continue;
					}
					case 1:
						num = 11;
						continue;
					case 2:
						goto IL_1DC;
					case 4:
						goto IL_1DC;
					case 5:
						return this;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 28;
							continue;
						}
						break;
					case 7:
						goto IL_20A;
					case 8:
						goto IL_19F;
					case 9:
						if (spr_u.ᜀ() == 0)
						{
							num = 30;
							continue;
						}
						num = 14;
						continue;
					case 10:
						this.ᜌ();
						num = 17;
						continue;
					case 11:
						if (spr_u.ᜀ() > 0)
						{
							num = 18;
							continue;
						}
						goto IL_2F8;
					case 12:
						if (spr_u.ᜀ() < 7)
						{
							num = 25;
							continue;
						}
						goto IL_320;
					case 13:
						goto IL_1F5;
					case 14:
						if (isGroup)
						{
							num = 6;
							continue;
						}
						goto IL_19F;
					case 15:
					{
						int num3;
						if (num4 > num3)
						{
							num = 5;
							continue;
						}
						XlsRange.ᜀ ᜀ;
						spr_u = ᜀ(num4);
						num = 19;
						continue;
					}
					case 16:
						goto IL_2F8;
					case 17:
						goto IL_28F;
					case 18:
					{
						spr\u2502 spr_u2 = spr_u;
						spr_u2.ᜀ(spr_u2.ᜀ() - 1);
						num = 24;
						continue;
					}
					case 19:
						if (isGroup)
						{
							num = 21;
							continue;
						}
						goto IL_320;
					case 20:
						goto IL_19F;
					case 21:
						num = 12;
						continue;
					case 22:
					{
						if (groupBy == GroupByType.ByRows)
						{
							num = 0;
							continue;
						}
						num2 = this.FirstColumn;
						int num3 = this.LastColumn;
						XlsRange.ᜀ ᜀ = new XlsRange.ᜀ(this.ᜀ);
						num = 2;
						continue;
					}
					case 23:
						goto IL_20A;
					case 24:
						goto IL_2F8;
					case 25:
					{
						spr\u2502 spr_u3 = spr_u;
						spr_u3.ᜀ(spr_u3.ᜀ() + 1);
						num = 16;
						continue;
					}
					case 26:
						if (bCollapsed)
						{
							num = 13;
							continue;
						}
						goto IL_19F;
					case 27:
						if (!isGroup)
						{
							num = 1;
							continue;
						}
						goto IL_2F8;
					case 28:
						if (spr_u.ᜀ() != 1)
						{
							num = 29;
							continue;
						}
						goto IL_1F5;
					case 29:
						num = 26;
						continue;
					case 30:
						spr_u.ᜁ(false);
						num = 20;
						continue;
					}
					if (isGroup)
					{
						num = 10;
						continue;
					}
					goto IL_28F;
					IL_19F:
					num4++;
					num = 7;
					continue;
					IL_1DC:
					if (true)
					{
					}
					num4 = num2;
					num = 23;
					continue;
					IL_1F5:
					spr_u.ᜁ(bCollapsed);
					num = 8;
					continue;
					IL_20A:
					num = 15;
					continue;
					IL_28F:
					num = 22;
					continue;
					IL_2F8:
					num = 9;
					continue;
					IL_320:
					num = 27;
				}
				return this;
			}
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0008ABF4 File Offset: 0x00089BF4
		private spr\u2502 ᜁ(int A_0)
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
			return sprᜑ.ᜀ(this.\u171D, A_0 - 1, true);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0008AC40 File Offset: 0x00089C40
		private spr\u2502 ᜀ(int A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9D:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_3A;
			}
			spr\u216E spr_u216E;
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					if (spr_u216E == null)
					{
						num = 2;
						continue;
					}
					goto IL_A7;
				case 1:
					goto IL_A5;
				case 2:
					goto IL_5B;
				}
				goto IL_3A;
			}
			IL_5B:
			spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
			spr\u216E spr_u216E2 = spr_u216E;
			ushort a_;
			spr_u216E.ᜀ(a_ = (ushort)(A_0 - 1));
			spr_u216E2.ᜄ(a_);
			spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
			this.\u171D.ColumnInformation[A_0] = spr_u216E;
			goto IL_9D;
			IL_A5:
			IL_A7:
			if (true)
			{
			}
			return spr_u216E;
			IL_3A:
			spr_u216E = this.\u171D.ColumnInformation[A_0];
			num = 0;
			goto IL_28;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0008AD00 File Offset: 0x00089D00
		private void ᜌ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u171D.FirstRow == -1)
					{
						num = 13;
						continue;
					}
					goto IL_1CD;
				case 1:
					goto IL_80;
				case 2:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 5:
					goto IL_1CD;
				case 6:
					goto IL_18E;
				case 7:
					this.\u171D.LastRow = this.LastRow;
					num = 15;
					continue;
				case 8:
					goto IL_78;
				case 9:
					if (this.\u171D.FirstColumn == 2147483647)
					{
						num = 14;
						continue;
					}
					goto IL_78;
				case 10:
					if (this.\u171D.LastColumn == 2147483647)
					{
						num = 6;
						continue;
					}
					return;
				case 11:
					if (this.\u171D.LastRow < this.LastRow)
					{
						num = 7;
						continue;
					}
					goto IL_1F8;
				case 12:
					num = 9;
					continue;
				case 13:
					goto IL_D0;
				case 14:
					goto IL_16D;
				case 15:
					goto IL_1F8;
				case 16:
					if (true)
					{
					}
					if (this.\u171D.FirstColumn <= this.FirstColumn)
					{
						num = 12;
						continue;
					}
					goto IL_16D;
				case 17:
					num = 0;
					continue;
				}
				if (this.\u171D.FirstRow <= this.FirstRow)
				{
					num = 17;
					continue;
				}
				goto IL_D0;
				IL_78:
				num = 1;
				continue;
				IL_80:
				if (this.\u171D.LastColumn >= this.LastColumn)
				{
					num = 4;
					continue;
				}
				goto IL_18E;
				IL_D0:
				this.\u171D.FirstRow = this.FirstRow;
				num = 5;
				continue;
				IL_16D:
				this.\u171D.FirstColumn = this.FirstColumn;
				num = 8;
				continue;
				IL_18E:
				this.\u171D.LastColumn = this.LastColumn;
				num = 2;
				continue;
				IL_1CD:
				num = 11;
				continue;
				IL_1F8:
				num = 16;
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0008AF40 File Offset: 0x00089F40
		internal void ᜀ(XlsWorkbook A_0)
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
			this.m_book = A_0;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0008AF84 File Offset: 0x00089F84
		private spr\u2502 ᜀ(GroupByType A_0, IDictionary A_1, int A_2, bool A_3)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 1;
				spr\u2502 spr_u;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1D7;
					case 2:
						goto IL_1BE;
					case 3:
						goto IL_1D7;
					case 4:
					{
						if (A_2 > this.m_book.MaxRowCount)
						{
							num = 16;
							continue;
						}
						spr\u20BA spr_u20BA = (spr\u20BA)spr\u175E.ᜀ(TBIFFRecord.Row);
						spr_u20BA.ᜆ((ushort)(A_2 - 1));
						spr_u20BA.ᜃ((ushort)this.m_book.DefaultXFIndex);
						spr_u20BA.ᜄ((ushort)this.\u171D.DefaultPrintRowHeight);
						spr_u20BA.ᜆ(false);
						spr_u = spr_u20BA;
						num = 0;
						continue;
					}
					case 5:
						if (A_3)
						{
							num = 15;
							continue;
						}
						goto IL_BA;
					case 6:
						goto IL_12B;
					case 7:
						if (A_2 < 1)
						{
							num = 19;
							continue;
						}
						goto IL_BC;
					case 8:
						goto IL_1F0;
					case 9:
						num = 12;
						continue;
					case 10:
						spr_u = (spr\u2502)A_1[A_2];
						num = 2;
						continue;
					case 11:
					{
						if (A_2 > this.m_book.MaxColumnCount)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						spr\u216E spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
						spr\u216E spr_u216E2 = spr_u216E;
						ushort a_2;
						spr_u216E.ᜄ(a_2 = (ushort)(A_2 - 1));
						spr_u216E2.ᜀ(a_2);
						spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
						spr_u = spr_u216E;
						num = 3;
						continue;
					}
					case 12:
						if (A_3)
						{
							num = 6;
							continue;
						}
						goto IL_EC;
					case 13:
						num = 4;
						continue;
					case 14:
						goto IL_85;
					case 15:
						goto IL_191;
					case 16:
						num = 5;
						continue;
					case 17:
						if (A_1.Contains(A_2))
						{
							num = 10;
							continue;
						}
						num = 18;
						continue;
					case 18:
						if (A_0 == GroupByType.ByRows)
						{
							num = 13;
							continue;
						}
						num = 11;
						continue;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BC;
						default:
							goto IL_23F;
						}
						break;
					}
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 7;
					continue;
					IL_BC:
					spr_u = null;
					num = 17;
					continue;
					IL_1D7:
					A_1.Add(A_2, spr_u);
					num = 8;
				}
				IL_85:
				throw new ArgumentNullException(RecordTableEnumerator.b("圽⸿⑁⭃㑅╇⭉㡋❍㽏㱑", a_));
				IL_BA:
				return null;
				IL_EC:
				return null;
				IL_12B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽िⱁ⁃⍅ぇ", a_));
				IL_191:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽िⱁ⁃⍅ぇ", a_));
				IL_1BE:
				IL_1F0:
				return spr_u;
				IL_23F:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽िⱁ⁃⍅ぇ", a_));
			}
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0008B284 File Offset: 0x0008A284
		protected string GetDisplayString()
		{
			for (;;)
			{
				XlsRange.TCellType tcellType = this.CellType;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (tcellType <= XlsRange.TCellType.RString)
						{
							num = 17;
							continue;
						}
						num = 13;
						continue;
					case 1:
						num = 10;
						continue;
					case 2:
					{
						string formulaStringValue;
						if (formulaStringValue != null)
						{
							num = 15;
							continue;
						}
						goto IL_1F4;
					}
					case 3:
						num = 9;
						continue;
					case 4:
						if (tcellType != XlsRange.TCellType.RK)
						{
							num = 3;
							continue;
						}
						goto IL_128;
					case 5:
						switch (tcellType)
						{
						case XlsRange.TCellType.Label:
							goto IL_181;
						case XlsRange.TCellType.BoolErr:
							goto IL_D6;
						default:
							num = 14;
							continue;
						}
						break;
					case 6:
					{
						if (tcellType != XlsRange.TCellType.Formula)
						{
							num = 1;
							continue;
						}
						string formulaStringValue = this.FormulaStringValue;
						num = 2;
						continue;
					}
					case 7:
						num = 5;
						continue;
					case 8:
					{
						string formulaStringValue;
						if (formulaStringValue.Length != 0)
						{
							goto IL_8A;
						}
						goto IL_1F4;
					}
					case 9:
						goto IL_A5;
					case 10:
						if (tcellType != XlsRange.TCellType.RString)
						{
							num = 11;
							continue;
						}
						goto IL_181;
					case 11:
						num = 12;
						continue;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8A;
						default:
							goto IL_CB;
						}
						break;
					case 13:
						if (tcellType != XlsRange.TCellType.LabelSST)
						{
							num = 7;
							continue;
						}
						goto IL_181;
					case 14:
						num = 4;
						continue;
					case 15:
						num = 8;
						continue;
					case 16:
						goto IL_95;
					case 17:
						num = 6;
						continue;
					}
					break;
					IL_8A:
					num = 16;
				}
			}
			IL_95:
			return this.FormulaStringValue;
			IL_A5:
			goto IL_1F4;
			IL_CB:
			if (false)
			{
			}
			goto IL_1F4;
			IL_D6:
			return this.Value;
			IL_128:
			if (true)
			{
			}
			return this.ᜋ();
			IL_181:
			return this.\u171D.GetText(this.m_iTopRow, this.m_iLeftColumn);
			IL_1F4:
			return string.Empty;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0008B48C File Offset: 0x0008A48C
		private string ᜋ()
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					stringBuilder = new StringBuilder();
					string text = this.ᜁ();
					string[] array = text.Split(new char[]
					{
						';'
					});
					int num = 0;
					int num2 = array.Length;
					int num3 = 5;
					for (;;)
					{
						int num4;
						int num5;
						switch (num3)
						{
						case 0:
							goto IL_272;
						case 1:
							goto IL_194;
						case 2:
							goto IL_1CB;
						case 3:
						{
							if (true)
							{
							}
							string text2;
							char[] array2 = text2.ToCharArray();
							char[] array3 = array2;
							num4 = 0;
							num3 = 23;
							continue;
						}
						case 4:
						{
							string text2;
							if (!this.ᜄ(text2))
							{
								num3 = 10;
								continue;
							}
							num3 = 13;
							continue;
						}
						case 5:
							goto IL_1AB;
						case 6:
							if (num >= num2)
							{
								num3 = 2;
								continue;
							}
							num3 = 22;
							continue;
						case 7:
							goto IL_272;
						case 8:
						{
							string[] array4;
							if (num5 >= array4.Length)
							{
								num3 = 14;
								continue;
							}
							string text2 = array4[num5];
							num3 = 16;
							continue;
						}
						case 9:
							goto IL_138;
						case 10:
						{
							string text2;
							stringBuilder.Append(text2);
							num3 = 17;
							continue;
						}
						case 11:
						{
							char[] array3;
							if (num4 >= array3.Length)
							{
								num3 = 7;
								continue;
							}
							char c = array3[num4];
							num3 = 20;
							continue;
						}
						case 12:
							goto IL_1AB;
						case 13:
						{
							string text2;
							if (text2.Contains(RecordTableEnumerator.b(">", a_)))
							{
								num3 = 3;
								continue;
							}
							goto IL_272;
						}
						case 14:
							goto IL_25D;
						case 15:
						{
							string[] array5 = array[num - 1].Split(new char[]
							{
								'"'
							});
							string[] array4 = array5;
							num5 = 0;
							num3 = 9;
							continue;
						}
						case 16:
						{
							string text2;
							if (text2.Contains(RecordTableEnumerator.b("ᔾ", a_)))
							{
								num3 = 19;
								continue;
							}
							num3 = 4;
							continue;
						}
						case 17:
							goto IL_29E;
						case 18:
							stringBuilder.Append(RecordTableEnumerator.b("Ἶ", a_));
							stringBuilder.Append(RecordTableEnumerator.b("Ἶ", a_));
							num3 = 1;
							continue;
						case 19:
							stringBuilder.Append(RecordTableEnumerator.b("Ἶ", a_));
							num3 = 0;
							continue;
						case 20:
						{
							char c;
							if (c == '?')
							{
								num3 = 18;
								continue;
							}
							goto IL_194;
						}
						case 21:
							goto IL_C0;
						case 22:
							if (Array.IndexOf<char>(array[num].ToCharArray(), '@') >= 0)
							{
								num3 = 15;
								continue;
							}
							goto IL_25D;
						case 23:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_29E;
							default:
								if (false)
								{
								}
								goto IL_C0;
							}
							break;
						case 24:
							goto IL_138;
						}
						break;
						IL_C0:
						num3 = 11;
						continue;
						IL_138:
						num3 = 8;
						continue;
						IL_194:
						num4++;
						num3 = 21;
						continue;
						IL_1AB:
						num3 = 6;
						continue;
						IL_25D:
						num++;
						num3 = 12;
						continue;
						IL_272:
						num5++;
						num3 = 24;
						continue;
						IL_29E:
						goto IL_272;
					}
				}
				IL_1CB:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0008B808 File Offset: 0x0008A808
		private bool ᜄ(string A_0)
		{
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					result = false;
					char[] array = A_0.ToCharArray();
					char[] array2 = array;
					int num = 0;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return result;
						case 1:
						{
							char value;
							if (Array.IndexOf<char>(this.ᜣ, value) >= 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_4C;
						}
						case 2:
							result = true;
							num2 = 0;
							continue;
						case 3:
							return result;
						case 4:
							goto IL_9D;
						case 5:
							goto IL_9D;
						case 6:
							if (num < array2.Length)
							{
								char value = array2[num];
								if (true)
								{
								}
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4C;
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
						IL_4C:
						num++;
						num2 = 5;
						continue;
						IL_9D:
						num2 = 6;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0008B8FC File Offset: 0x0008A8FC
		protected DateTime GetDateTime()
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
			double number = this.GetNumber();
			return UtilityMethods.ᜀ(number);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0008B944 File Offset: 0x0008A944
		protected void SetDateTime(DateTime value)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_75:
				num = 0;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_42;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.m_rtfString != null)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					goto IL_68;
				}
				goto IL_42;
			}
			IL_68:
			this.m_rtfString.Clear();
			goto IL_75;
			IL_42:
			double number = UtilityMethods.ᜀ(value);
			this.SetNumber(number);
			num = 1;
			goto IL_30;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0008B9D0 File Offset: 0x0008A9D0
		protected void SetTimeSpan(TimeSpan time)
		{
			int a_ = 2;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9C:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_43;
			}
			for (;;)
			{
				IL_31:
				switch (num)
				{
				case 0:
					if (this.m_rtfString != null)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					goto IL_8F;
				}
				goto IL_43;
			}
			IL_8F:
			this.m_rtfString.Clear();
			goto IL_9C;
			IL_43:
			if (true)
			{
			}
			this.NumberFormat = RecordTableEnumerator.b("倷9儻匽稿ㅁ㝃", a_);
			this.SetNumber((double)time.Ticks / 864000000000.0);
			num = 0;
			goto IL_31;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0008BA84 File Offset: 0x0008AA84
		protected double GetNumber()
		{
			switch (0)
			{
			default:
			{
				double num;
				for (;;)
				{
					num = double.NaN;
					int num2 = 0;
					for (;;)
					{
						int num4;
						switch (num2)
						{
						case 0:
						{
							if (this.IsSingleCell)
							{
								num2 = 13;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							spr_u24F.ᜀ(this.Row, this.Column);
							num = spr_u24F.GetNumber();
							num2 = 9;
							continue;
						}
						case 1:
							if (this.CellType == XlsRange.TCellType.Number)
							{
								num2 = 19;
								continue;
							}
							num2 = 12;
							continue;
						case 2:
							num = double.NaN;
							num2 = 18;
							continue;
						case 3:
							return num;
						case 4:
						{
							double number;
							if (num != number)
							{
								num2 = 2;
								continue;
							}
							int num3;
							num3++;
							num2 = 16;
							continue;
						}
						case 5:
							goto IL_10C;
						case 6:
							goto IL_22C;
						case 7:
						{
							int num3;
							int lastColumn;
							if (num3 > lastColumn)
							{
								num2 = 6;
								continue;
							}
							spr\u24F1 spr_u24F;
							spr_u24F.ᜀ(num4, num3);
							double number = spr_u24F.GetNumber();
							num2 = 4;
							continue;
						}
						case 8:
							return num;
						case 9:
							if (!double.IsNaN(num))
							{
								num2 = 17;
								continue;
							}
							return num;
						case 10:
							goto IL_296;
						case 11:
							goto IL_18C;
						case 12:
							if (this.CellType == XlsRange.TCellType.Formula)
							{
								num2 = 23;
								continue;
							}
							return num;
						case 13:
							num2 = 10;
							continue;
						case 14:
						{
							if (true)
							{
							}
							sprỔ sprỔ = (sprỔ)this.Record;
							num = sprỔ.ᜀ();
							num2 = 3;
							continue;
						}
						case 15:
							return num;
						case 16:
							goto IL_10C;
						case 17:
						{
							num4 = this.Row;
							int lastRow = this.LastRow;
							num2 = 11;
							continue;
						}
						case 18:
							goto IL_22C;
						case 19:
						{
							spr\u19FF spr_u19FF = (spr\u19FF)this.Record;
							num = spr_u19FF.ᜅ();
							num2 = 20;
							continue;
						}
						case 20:
							return num;
						case 21:
						{
							int lastRow;
							if (num4 > lastRow)
							{
								num2 = 15;
								continue;
							}
							int num3 = this.Column;
							int lastColumn = this.LastColumn;
							num2 = 5;
							continue;
						}
						case 22:
							goto IL_18C;
						case 23:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_296;
							default:
							{
								if (false)
								{
								}
								spr᱒ spr᱒ = (spr᱒)this.Record;
								num = spr᱒.ᜌ();
								num2 = 8;
								continue;
							}
							}
							break;
						}
						break;
						IL_296:
						if (this.CellType == XlsRange.TCellType.RK)
						{
							num2 = 14;
							continue;
						}
						num2 = 1;
						continue;
						IL_10C:
						num2 = 7;
						continue;
						IL_18C:
						num2 = 21;
						continue;
						IL_22C:
						num4++;
						num2 = 22;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0008BDB4 File Offset: 0x0008ADB4
		protected void SetNumber(double value)
		{
			for (;;)
			{
				this.ᜉ();
				BiffRecordRaw a_ = this.ᜀ(value);
				this.Record = a_;
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.m_rtfString != null)
						{
							goto IL_41;
						}
						return;
					case 1:
						this.m_rtfString.Clear();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					break;
					IL_41:
					num = 1;
				}
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0008BE4C File Offset: 0x0008AE4C
		private void ᜁ(double A_0)
		{
			int a_ = 16;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜉ();
					BiffRecordRaw biffRecordRaw = this.ᜀ(A_0);
					spr\u23A5 spr_u23A = biffRecordRaw as spr\u23A5;
					int a_2 = (int)spr_u23A.ᜆ();
					spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ(a_2);
					int a_3 = spr_u192F.ᝊ();
					sprᤅ sprᤅ = this.m_book.InnerFormats.ᜁ(a_3);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 4;
							continue;
						case 1:
							if (sprᤅ.ᜂ() != RecordTableEnumerator.b("Ņⵇ⑉⥋㱍ㅏ㹑", a_))
							{
								num = 6;
								continue;
							}
							goto IL_CB;
						case 2:
							goto IL_CB;
						case 3:
							a_3 = this.m_book.InnerFormats.ᜉ(RecordTableEnumerator.b("癅晇穉籋", a_));
							spr_u192F = (spr_u192F.\u1758() as spr\u192F);
							spr_u192F.ᜀ(a_3);
							spr_u192F = this.m_book.InnerExtFormats.ᜁ(spr_u192F);
							spr_u23A.ᜀ((ushort)spr_u192F.ᜠ());
							num = 2;
							continue;
						case 4:
						{
							CellFormatType cellFormatType;
							if (cellFormatType != CellFormatType.General)
							{
								num = 3;
								continue;
							}
							goto IL_CB;
						}
						case 5:
							if (this.FormatType != CellFormatType.Number)
							{
								num = 0;
								continue;
							}
							goto IL_CB;
						case 6:
						{
							CellFormatType cellFormatType = sprᤅ.ᜀ(A_0);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1BD;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						case 7:
							this.m_rtfString.Clear();
							goto IL_1BD;
						case 8:
							if (true)
							{
							}
							if (this.m_rtfString != null)
							{
								num = 7;
								continue;
							}
							return;
						case 9:
							return;
						}
						break;
						IL_CB:
						this.Record = biffRecordRaw;
						num = 8;
						continue;
						IL_1BD:
						num = 9;
					}
				}
				return;
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0008C054 File Offset: 0x0008B054
		private BiffRecordRaw ᜀ(double A_0)
		{
			if (true)
			{
			}
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				biffRecordRaw = this.\u171D.ᜂ(this.m_iTopRow, this.m_iLeftColumn, A_0);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return biffRecordRaw;
					case 1:
					{
						spr\u19FF spr_u19FF = (spr\u19FF)this.ᜀ(TBIFFRecord.Number);
						spr_u19FF.ᜀ(A_0);
						biffRecordRaw = spr_u19FF;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					case 2:
						if (biffRecordRaw == null)
						{
							goto IL_40;
						}
						return biffRecordRaw;
					}
					break;
					IL_40:
					num = 1;
				}
			}
			return biffRecordRaw;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0008C0FC File Offset: 0x0008B0FC
		protected void SetBoolean(bool value)
		{
			for (;;)
			{
				spr\u249B spr_u249B = (spr\u249B)this.ᜀ(TBIFFRecord.BoolErr);
				spr_u249B.ᜀ(false);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.m_rtfString != null)
						{
							num = 3;
							continue;
						}
						return;
					case 2:
						spr_u249B.ᜀ(value ? 1 : 0);
						this.Record = spr_u249B;
						goto IL_91;
					case 3:
						if (true)
						{
						}
						this.m_rtfString.Clear();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_91;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
					IL_91:
					num = 1;
				}
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0008C1C0 File Offset: 0x0008B1C0
		protected void SetError(string errorString)
		{
			int a_ = 6;
			int num = 9;
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				switch (num)
				{
				case 0:
				{
					spr\u249B spr_u249B = (spr\u249B)this.ᜀ(TBIFFRecord.BoolErr);
					spr_u249B.ᜀ(true);
					spr_u249B.ᜀ((byte)num2);
					this.Record = spr_u249B;
					num = 8;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12F;
					default:
						goto IL_F5;
					}
					break;
				case 2:
					if (errorString.Length == 0)
					{
						num = 5;
						continue;
					}
					goto IL_12F;
				case 3:
					goto IL_DA;
				case 4:
					if (this.m_rtfString != null)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
					goto IL_BF;
				case 6:
					this.m_rtfString.Clear();
					num = 3;
					continue;
				case 7:
					if (num2 != -1)
					{
						num = 0;
						continue;
					}
					goto IL_59;
				case 8:
					num = 4;
					continue;
				}
				if (errorString == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				IL_12F:
				num2 = this.ᜃ(errorString);
				num = 7;
			}
			IL_59:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("爻儽㐿扁⅃㑅㩇╉㹋湍⍏♑♓㽕㙗㵙", a_));
			IL_BF:
			throw new ArgumentException(RecordTableEnumerator.b("夻䰽㈿ⵁ㙃ᕅ㱇㡉╋⁍㝏", a_), RecordTableEnumerator.b("夻䰽㈿ⵁ㙃ᕅ㱇㡉╋⁍㝏牑㝓㝕㙗穙㉛ㅝᑟ䉡٣ͥ䡧ཀྵūṭѯୱ", a_));
			IL_DA:
			return;
			IL_F5:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("夻䰽㈿ⵁ㙃ᕅ㱇㡉╋⁍㝏", a_));
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0008C354 File Offset: 0x0008B354
		private int ᜃ(string A_0)
		{
			int a_ = 14;
			int num = 6;
			int result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0[0] != '#')
					{
						num = 3;
						continue;
					}
					goto IL_4D;
				case 1:
					goto IL_4D;
				case 2:
					if (!FormulaUtil.ErrorNameToCode.TryGetValue(A_0, out result))
					{
						num = 5;
						continue;
					}
					goto IL_117;
				case 3:
					A_0 = '#' + A_0;
					num = 1;
					continue;
				case 4:
					if (A_0.Length == 0)
					{
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return -1;
					default:
						if (false)
						{
						}
						A_0 = A_0.ToUpper();
						num = 0;
						continue;
					}
					break;
				case 5:
					goto IL_6F;
				case 7:
					num = 4;
					continue;
				case 8:
					goto IL_E0;
				}
				if (A_0 != null)
				{
					num = 7;
					continue;
				}
				goto IL_E4;
				IL_4D:
				num = 2;
			}
			IL_6F:
			return -1;
			IL_E0:
			IL_E4:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇ཉ㹋㱍㽏⁑", a_));
			IL_117:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0008C484 File Offset: 0x0008B484
		protected internal void SetFormula(string value)
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
			this.SetFormula(value, null, false);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0008C4C8 File Offset: 0x0008B4C8
		protected internal void SetFormula(string value, Dictionary<string, string> hashWorksheetNames, bool bR1C1)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_8A;
					case 2:
						value = value.Substring(1, value.Length - 1);
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_37;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					goto IL_2C;
					IL_37:
					num = 2;
					continue;
					IL_2C:
					if (value[0] == '=')
					{
						goto IL_37;
					}
					break;
				}
				IL_8A:
				int a_ = this.Row - 1;
				int a_2 = this.Column - 1;
				FormulaUtil formulaUtil = this.m_book.FormulaUtil;
				Ptg[] array = formulaUtil.ᜀ(value, this.\u171D, hashWorksheetNames, a_, a_2, bR1C1);
				spr᱒ spr᱒ = (spr᱒)this.ᜀ(TBIFFRecord.Formula);
				spr᱒.ᜁ(array);
				spr᱒.ᜀ(true);
				spr᱒.ᜃ(true);
				this.Record = spr᱒;
				FormulaUtil.RaiseFormulaEvaluation(this, new EvaluateEventArgs(this, array));
				return;
			}
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0008C5CC File Offset: 0x0008B5CC
		[CLSCompliant(false)]
		internal void ᜁ(spr᱒ A_0)
		{
			int a_ = 7;
			if (A_0 == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆", a_));
			}
			this.Record = A_0;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0008C630 File Offset: 0x0008B630
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0008C6AC File Offset: 0x0008B6AC
		[CLSCompliant(false)]
		protected CellFormatType FormatType
		{
			get
			{
				if (!this.ContainsNumber)
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
						break;
					}
					return this.InnerNumberFormat.ᜀ(this.\u171D.ᜀ(this.Record as spr\u23A5, false));
				}
				if (true)
				{
				}
				return this.InnerNumberFormat.ᜀ(this.GetNumber());
			}
			set
			{
				int a_ = 11;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
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
							switch (value)
							{
							case CellFormatType.Text:
								goto IL_12A;
							case CellFormatType.Number:
								goto IL_DF;
							case CellFormatType.DateTime:
								goto IL_113;
							default:
								num = 2;
								continue;
							}
							break;
						}
						break;
					case 1:
						goto IL_DD;
					case 2:
						return;
					case 4:
						num = 6;
						continue;
					case 5:
						if (value != CellFormatType.DateTime)
						{
							num = 4;
							continue;
						}
						goto IL_53;
					case 6:
						if (this.NumberFormat == RecordTableEnumerator.b("ـ♂⭄≆㭈⩊⅌", a_))
						{
							num = 1;
							continue;
						}
						goto IL_53;
					case 7:
						num = 5;
						continue;
					}
					if (value != this.FormatType)
					{
						num = 7;
						continue;
					}
					return;
					IL_53:
					num = 0;
				}
				return;
				IL_DD:
				return;
				IL_DF:
				this.NumberFormat = RecordTableEnumerator.b("煀浂畄睆", a_);
				return;
				IL_113:
				this.NumberFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
				return;
				IL_12A:
				this.NumberFormat = RecordTableEnumerator.b("ŀ", a_);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0008C7F8 File Offset: 0x0008B7F8
		internal spr\u240D Format
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
				int numberFormatIndex = this.m_book.GetExtFormat((int)this.ExtendedFormatIndex).NumberFormatIndex;
				return this.m_book.InnerFormats.ᜁ(numberFormatIndex).ᜈ();
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0008C860 File Offset: 0x0008B860
		protected void SetChanged()
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
			this.\u171D.SetChanged();
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0008C8A8 File Offset: 0x0008B8A8
		protected void CheckRange(int row, int column)
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
					goto IL_8A;
				case 1:
					if (row <= this.m_book.MaxRowCount)
					{
						num = 5;
						continue;
					}
					goto IL_CE;
				case 3:
					goto IL_74;
				case 4:
					num = 1;
					continue;
				case 5:
					num = 7;
					continue;
				case 6:
					num = 3;
					continue;
				case 7:
					if (column >= 1)
					{
						num = 6;
						continue;
					}
					goto IL_CE;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					if (column <= this.m_book.MaxColumnCount)
					{
						return;
					}
					num = 0;
					break;
				default:
					if (false)
					{
					}
					if (row < 1)
					{
						goto IL_CE;
					}
					num = 4;
					break;
				}
			}
			IL_8A:
			IL_CE:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0008C98C File Offset: 0x0008B98C
		protected IWorksheet FindWorksheet(string sheetName)
		{
			int a_ = 10;
			IWorksheet worksheet = this.m_book.Worksheets[sheetName];
			if (worksheet == null)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇щⵋ⍍㕏", a_));
			}
			return worksheet;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0008C9FC File Offset: 0x0008B9FC
		public void ReparseFormulaString()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					if (this.CellType == XlsRange.TCellType.Formula)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					try
					{
						this.SetFormula(this.Formula);
						return;
					}
					catch (spr\u2313)
					{
						if (!this.m_book.Loading)
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
								throw;
							}
						}
						this.m_book.ᜀ(this);
						return;
					}
					goto IL_8B;
				}
				if (this.IsSingleCell)
				{
					num = 0;
					continue;
				}
				break;
				IL_8B:
				if (true)
				{
				}
				num = 1;
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0008CAD0 File Offset: 0x0008BAD0
		private void ᜁ(CopyRangeOptions A_0)
		{
			if (true)
			{
			}
			int num = this.LastRow + 1;
			int firstColumn = this.FirstColumn;
			int lastRow = this.\u171D.AllocatedRange.LastRow;
			int lastColumn = this.LastColumn;
			if (num > lastRow)
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
					break;
				}
				return;
			}
			IXLSRange a_ = this.\u171D.AllocatedRange[this.FirstRow, this.FirstColumn];
			IXLSRange a_2 = this.\u171D.AllocatedRange[num, firstColumn, lastRow, lastColumn];
			this.\u171D.ᜀ(a_, a_2, A_0, true);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0008CB80 File Offset: 0x0008BB80
		private void ᜀ(CopyRangeOptions A_0)
		{
			int firstRow = this.FirstRow;
			int num = this.LastColumn + 1;
			int lastRow = this.LastRow;
			int lastColumn = this.\u171D.AllocatedRange.LastColumn;
			if (num > lastColumn)
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
				return;
			}
			IXLSRange a_ = this.\u171D.AllocatedRange[this.FirstRow, this.FirstColumn];
			IXLSRange a_2 = this.\u171D.AllocatedRange[firstRow, num, lastRow, lastColumn];
			this.\u171D.ᜀ(a_, a_2, A_0, false);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0008CC30 File Offset: 0x0008BC30
		private string ᜀ(spr\u1C7C A_0)
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
			return spr\u223A.ᜀ(this.m_book.InnerSST.ᜂ(A_0.ᜁ()));
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0008CC88 File Offset: 0x0008BC88
		private string ᜀ(spr᱒ A_0)
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
			return this.ᜀ(A_0, false);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0008CCCC File Offset: 0x0008BCCC
		private string ᜀ(spr᱒ A_0, bool A_1)
		{
			int a_ = 4;
			try
			{
				switch (0)
				{
				default:
				{
					string result;
					for (;;)
					{
						FormulaUtil formulaUtil = this.m_book.FormulaUtil;
						spr\u225F spr_u225F = this.\u171D.CellRecords.ᜁ(A_0.\u1714() + 1, A_0.\u1713() + 1);
						int num = 3;
						for (;;)
						{
							string str;
							switch (num)
							{
							case 0:
								goto IL_F8;
							case 1:
								goto IL_119;
							case 2:
								goto IL_76;
							case 3:
								if (spr_u225F != null)
								{
									num = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_76;
								default:
									if (false)
									{
									}
									A_0.ᜀ(true);
									A_0.ᜃ(true);
									str = formulaUtil.ᜀ(A_0.ᜑ(), this.Row - 1, this.Column - 1, A_1, false);
									num = 4;
									continue;
								}
								break;
							case 4:
								goto IL_F8;
							}
							break;
							IL_76:
							str = formulaUtil.ᜀ(spr_u225F.ᜅ(), spr_u225F.ᜉ(), spr_u225F.ᜈ(), A_1, false);
							num = 0;
							continue;
							IL_F8:
							result = RecordTableEnumerator.b("ܹ", a_) + str;
							num = 1;
						}
					}
					IL_119:
					return result;
				}
				}
			}
			catch (spr\u2313)
			{
				if (!this.m_book.Loading)
				{
					throw;
				}
				this.m_book.ᜀ(this);
			}
			catch (Exception)
			{
				throw;
			}
			if (true)
			{
			}
			return null;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0008CE64 File Offset: 0x0008BE64
		public void SetRowHeight(double rowHeight, bool bIsBadFontHeight)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						if (this.LastRow == this.m_book.MaxRowCount)
						{
							num = 11;
							continue;
						}
						goto IL_135;
					case 2:
						if (rowHeight > 409.5)
						{
							num = 6;
							continue;
						}
						num2 = this.FirstRow;
						num3 = this.LastRow;
						num = 3;
						continue;
					case 3:
						if (this.LastRow - this.FirstRow > this.m_book.MaxRowCount - (this.LastRow - this.FirstRow))
						{
							num = 5;
							continue;
						}
						goto IL_135;
					case 4:
						goto IL_16E;
					case 5:
						num = 1;
						continue;
					case 6:
						goto IL_133;
					case 8:
						goto IL_7B;
					case 9:
						goto IL_7B;
					case 10:
						return;
					case 11:
						num2 = 1;
						num3 = this.FirstRow - 1;
						this.\u171D.IsZeroHeight = true;
						this.\u171D.IsVisible = true;
						num = 8;
						continue;
					case 12:
						if (true)
						{
						}
						if (num4 > num5)
						{
							num = 10;
							continue;
						}
						goto IL_E0;
					case 13:
						goto IL_16E;
					}
					if (rowHeight >= 0.0)
					{
						num = 0;
						continue;
					}
					break;
					IL_7B:
					num4 = num2;
					num5 = num3;
					num = 4;
					continue;
					IL_E0:
					this.\u171D.ᜀ(num4, rowHeight, bIsBadFontHeight, MeasureUnits.Point, true);
					num4++;
					num = 13;
					continue;
					IL_135:
					this.\u171D.IsVisible = false;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E0;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					IL_16E:
					num = 12;
				}
				IL_BE:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("主儽㜿ੁ⅃⽅⽇≉㡋", a_), RecordTableEnumerator.b("渻儽㜿扁ృ⍅ⅇⵉ⑋㩍灏⅑㱓㥕ⵗ㙙㡛繝ɟݡ䑣ѥ൧ṩ᭫୭ᕯᱱ味䙵塷᭹ቻ᩽ꁿ뚁뒃뾅ꚇ뾉ꊋ", a_));
				IL_133:
				goto IL_BE;
			}
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0008D094 File Offset: 0x0008C094
		protected void CreateRichTextString()
		{
			if (this.IsSingleCell)
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
					this.m_rtfString = new RangeRichTextString(this.Application, this.\u171D, this.m_iTopRow, this.m_iLeftColumn);
					return;
				}
			}
			this.m_rtfString = new RTFStringArray((spr\u17FF)this.Application, this.\u171D, this);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0008D11C File Offset: 0x0008C11C
		private object ᜊ()
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
						goto IL_7C;
					case 1:
						goto IL_40;
					case 2:
						goto IL_D5;
					case 3:
						if (this.HasDateTime)
						{
							num = 5;
							continue;
						}
						goto IL_107;
					case 5:
					{
						sprᤅ sprᤅ = this.InnerNumberFormat;
						num = 7;
						continue;
					}
					case 6:
						if (this.HasNumber)
						{
							num = 2;
							continue;
						}
						num = 3;
						continue;
					case 7:
					{
						sprᤅ sprᤅ;
						if (!sprᤅ.ᜁ(this.NumberValue))
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_D7;
					}
					}
					if (this.HasBoolean)
					{
						num = 1;
					}
					else
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
							num = 6;
							break;
						}
					}
				}
			}
			IL_40:
			return this.BooleanValue;
			IL_7C:
			return this.DateTimeValue;
			IL_D5:
			return this.NumberValue;
			IL_D7:
			return this.TimeSpanValue;
			IL_107:
			return null;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0008D234 File Offset: 0x0008C234
		private bool ᜂ(string A_0)
		{
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9F;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_62;
					case 1:
						goto IL_81;
					case 2:
						if (string.Compare(A_0, bool.FalseString, StringComparison.CurrentCultureIgnoreCase) == 0)
						{
							num = 4;
							continue;
						}
						num = 5;
						continue;
					case 3:
						if (true)
						{
						}
						break;
					case 4:
						goto IL_CC;
					case 5:
						if (FormulaUtil.ErrorNameToCode.ContainsKey(A_0))
						{
							num = 1;
							continue;
						}
						return false;
					}
					if (string.Compare(A_0, bool.TrueString, StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						num = 0;
					}
					else
					{
						num = 2;
					}
					break;
				}
			}
			IL_62:
			this.BooleanValue = true;
			return true;
			IL_81:
			IL_9F:
			this.ErrorValue = A_0;
			return true;
			IL_CC:
			this.BooleanValue = false;
			return true;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0008D310 File Offset: 0x0008C310
		protected internal void SetLabelSSTIndex(int index)
		{
			int a_ = 15;
			for (;;)
			{
				int num = 6;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (index >= this.m_book.InnerSST.Count)
						{
							num = 7;
							continue;
						}
						goto IL_118;
					case 2:
						num = 8;
						continue;
					case 3:
						this.Record = this.ᜀ(TBIFFRecord.Blank);
						num = 4;
						continue;
					case 4:
						goto IL_F7;
					case 5:
						if (index >= 0)
						{
							num = 0;
							continue;
						}
						goto IL_AB;
					case 7:
						goto IL_86;
					case 8:
						if (this.CellType != XlsRange.TCellType.Blank)
						{
							num = 3;
							continue;
						}
						return;
					}
					if (index == -1)
					{
						num = 2;
					}
					else
					{
						num = 5;
					}
				}
				IL_AB:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_C1;
				}
				IL_86:
				goto IL_AB;
			}
			return;
			IL_C1:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄ⥆ⵈ⹊㕌", a_));
			IL_F7:
			return;
			IL_118:
			spr\u1C7C spr_u1C7C = (spr\u1C7C)this.ᜀ(TBIFFRecord.LabelSST);
			spr_u1C7C.ᜀ(index);
			this.Record = spr_u1C7C;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0008D454 File Offset: 0x0008C454
		private void ᜉ()
		{
			int a_ = 9;
			ICollection<spr\u225F> keys;
			for (;;)
			{
				Dictionary<spr\u225F, object> dictionary = this.FormulaArrays;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B4;
					case 1:
						if (dictionary.Count == 0)
						{
							num = 0;
							continue;
						}
						keys = dictionary.Keys;
						num = 2;
						continue;
					case 2:
						if (true)
						{
						}
						if (!this.ᜀ(keys))
						{
							num = 5;
							continue;
						}
						goto IL_CA;
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
							num = 1;
							continue;
						}
						break;
					case 4:
						if (dictionary != null)
						{
							num = 3;
							continue;
						}
						return;
					case 5:
						goto IL_77;
					}
					break;
				}
			}
			IL_77:
			throw new sprṁ(RecordTableEnumerator.b("氾⑀㝂敄㕆⡈╊⩌⩎煐╒㑔㭖ⱘ㹚絜㥞`੢।ɦ൨䕪", a_));
			IL_B4:
			return;
			IL_CA:
			this.\u171D.ᜀ(keys, false);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0008D538 File Offset: 0x0008C538
		public void SetDataValidation(XlsValidation dv)
		{
			int a_ = 19;
			if (dv == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ㵊", a_));
				}
			}
			this.m_dataValidation = this.AppImplementation.ᜀ(this, dv);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0008D5A8 File Offset: 0x0008C5A8
		private void ᜈ()
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
			this.Record = this.ᜁ(TBIFFRecord.Blank);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0008D5F4 File Offset: 0x0008C5F4
		protected internal void AddComment(ICommentShape comment)
		{
			int a_ = 14;
			if (comment == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("❃⥅╇❉⥋⁍⑏", a_));
				}
			}
			XlsComment xlsComment = (XlsComment)this.AddComment();
			xlsComment.CopyFrom((XlsComment)comment, null);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0008D66C File Offset: 0x0008C66C
		protected internal void SetParent(XlsWorksheet parent)
		{
			int a_ = 10;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					goto IL_50;
				case 3:
					goto IL_5B;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5B:
					if (this.Parent != parent)
					{
						goto IL_94;
					}
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (parent == null)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
					break;
				}
			}
			IL_50:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("〿⍁㙃⍅♇㹉", a_));
			IL_94:
			this.\u171D = parent;
			this.m_book = parent.ParentWorkbook;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0008D720 File Offset: 0x0008C720
		public void UpdateNamedRange(int[] newIndexs)
		{
			int a_ = 0;
			for (;;)
			{
				spr᱒ spr᱒ = this.Record as spr᱒;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						Ptg[] a_2;
						spr᱒.ᜁ(a_2);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CC;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 1:
						goto IL_4F;
					case 2:
						goto IL_CC;
					case 3:
					{
						Ptg[] a_2;
						if (this.m_book.FormulaUtil.ᜁ(a_2, newIndexs))
						{
							num = 0;
							continue;
						}
						return;
					}
					case 4:
						goto IL_F4;
					case 5:
					{
						if (newIndexs == null)
						{
							num = 4;
							continue;
						}
						Ptg[] a_2 = spr᱒.ᜑ();
						num = 3;
						continue;
					}
					case 6:
						if (spr᱒ == null)
						{
							num = 1;
							continue;
						}
						num = 5;
						continue;
					}
					break;
				}
			}
			IL_4F:
			if (true)
			{
			}
			return;
			IL_CC:
			return;
			IL_F4:
			throw new ArgumentNullException(RecordTableEnumerator.b("圵䨷䠹爻嬽㜿ୁ⩃≅ⵇ㉉", a_));
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0008D824 File Offset: 0x0008C824
		private BiffRecordRaw ᜁ(TBIFFRecord A_0)
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
			BiffRecordRaw biffRecordRaw = this.ᜀ(A_0);
			this.\u171D.ᜀ(this.m_iLeftColumn, this.m_iTopRow, biffRecordRaw);
			return biffRecordRaw;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0008D880 File Offset: 0x0008C880
		private BiffRecordRaw ᜀ(TBIFFRecord A_0)
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
			return this.\u171D.ᜀ(A_0, this.m_iTopRow, this.m_iLeftColumn);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0008D8D4 File Offset: 0x0008C8D4
		public void UpdateRange(int startRow, int startColumn, int endRow, int endColumn)
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
			this.FirstRow = startRow;
			this.FirstColumn = startColumn;
			this.LastRow = endRow;
			this.LastColumn = endColumn;
			this.ResetCells();
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0008D934 File Offset: 0x0008C934
		protected internal bool ContainsNumber
		{
			get
			{
				for (;;)
				{
					XlsRange.TCellType tcellType = this.CellType;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (tcellType != XlsRange.TCellType.RK)
							{
								num = 1;
								continue;
							}
							goto IL_77;
						case 1:
							num = 6;
							continue;
						case 2:
							IL_3F:
							num = 5;
							continue;
						case 3:
							if (tcellType != XlsRange.TCellType.Formula)
							{
								num = 2;
								continue;
							}
							goto IL_41;
						case 4:
							num = 0;
							continue;
						case 5:
							if (true)
							{
							}
							if (tcellType != XlsRange.TCellType.Number)
							{
								num = 4;
								continue;
							}
							goto IL_77;
						case 6:
							return false;
						}
						break;
						IL_77:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3F;
						default:
							goto IL_97;
						}
					}
				}
				IL_41:
				return this.FormulaStringValue == null;
				IL_97:
				if (false)
				{
				}
				return true;
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0008DA04 File Offset: 0x0008CA04
		private bool ᜀ(string A_0, out DateTime A_1)
		{
			if (!this.m_book.DetectDateTimeInValue)
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
					A_1 = DateTime.MinValue;
					return false;
				}
			}
			return DateTime.TryParse(A_0, out A_1);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0008DA64 File Offset: 0x0008CA64
		private IXLSRange ᜁ(string A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 1;
				Rectangle a_2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_CF;
					case 2:
					{
						int num2;
						if (num2 > 2)
						{
							num = 0;
							continue;
						}
						a_2 = Rectangle.FromLTRB(1, 1, this.m_book.MaxColumnCount, this.m_book.MaxRowCount);
						string[] array;
						a_2 = this.ᜀ(array[0], a_2, true);
						num = 6;
						continue;
					}
					case 3:
						goto IL_F1;
					case 4:
					{
						if (A_0.Length == 0)
						{
							num = 3;
							continue;
						}
						string[] array = A_0.Split(new char[]
						{
							':'
						});
						int num2 = array.Length;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_174;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 5:
						goto IL_5C;
					case 6:
					{
						if (true)
						{
						}
						int num2;
						if (num2 == 2)
						{
							num = 7;
							continue;
						}
						goto IL_188;
					}
					case 7:
					{
						string[] array;
						a_2 = this.ᜀ(array[1], a_2, false);
						num = 8;
						continue;
					}
					case 8:
						goto IL_10E;
					}
					if (A_0 == null)
					{
						num = 5;
					}
					else
					{
						num = 4;
					}
				}
				IL_5C:
				throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀ᅂ⁄ⅆⱈ㥊⡌ⅎ㉐㙒", a_));
				IL_CF:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("丼䬾㍀ᅂ⁄ⅆⱈ㥊⡌ⅎ㉐㙒", a_));
				IL_F1:
				goto IL_174;
				IL_10E:
				goto IL_188;
				IL_174:
				throw new ArgumentException(RecordTableEnumerator.b("丼䬾㍀ᅂ⁄ⅆⱈ㥊⡌ⅎ㉐㙒畔穖祘⡚⥜ⵞࡠൢɤ䝦੨੪ͬŮṰݲ啴ᕶᱸ孺᡼ቾﲄꦆ", a_));
				IL_188:
				return this[a_2.Top, a_2.Left, a_2.Bottom, a_2.Right];
			}
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0008DC1C File Offset: 0x0008CC1C
		private Rectangle ᜀ(string A_0, Rectangle A_1, bool A_2)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 17;
				for (;;)
				{
					int num2;
					int num3;
					string text;
					int num5;
					string text2;
					int length;
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag)
						{
							num = 7;
							continue;
						}
						goto IL_C3;
					}
					case 1:
						goto IL_342;
					case 2:
						goto IL_BE;
					case 3:
						num = 6;
						continue;
					case 4:
						goto IL_289;
					case 5:
					{
						bool flag2;
						if (!flag2)
						{
							num = 3;
							continue;
						}
						num = 26;
						continue;
					}
					case 6:
						num2 = A_0.Length;
						goto IL_1EA;
					case 7:
						num = 30;
						continue;
					case 8:
					{
						bool flag2;
						if (!flag2)
						{
							num = 1;
							continue;
						}
						goto IL_15E;
					}
					case 9:
					{
						bool flag2;
						if (!flag2)
						{
							num = 11;
							continue;
						}
						num = 15;
						continue;
					}
					case 10:
						num = 20;
						continue;
					case 11:
						num = 22;
						continue;
					case 12:
						num = 8;
						continue;
					case 13:
						goto IL_37E;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_255;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							A_1.Y = num3;
							A_1.Height = 0;
							num = 29;
							continue;
						}
						break;
					case 15:
					{
						int num4;
						text = A_0.Substring(num4 + 1);
						goto IL_234;
					}
					case 16:
					{
						bool flag;
						if (!flag)
						{
							num = 12;
							continue;
						}
						goto IL_15E;
					}
					case 18:
					{
						bool flag;
						if (!flag)
						{
							num = 10;
							continue;
						}
						goto IL_255;
					}
					case 19:
						if (A_2)
						{
							num = 28;
							continue;
						}
						A_1.Width = num5 - A_1.X;
						num = 24;
						continue;
					case 20:
						text2 = null;
						goto IL_2C7;
					case 21:
						goto IL_C3;
					case 22:
						text = null;
						goto IL_234;
					case 23:
						text2 = A_0.Substring(1, length);
						goto IL_2C7;
					case 24:
						return A_1;
					case 25:
						num = 19;
						continue;
					case 26:
					{
						int num4;
						num2 = num4;
						goto IL_1EA;
					}
					case 27:
					{
						bool flag2;
						if (flag2)
						{
							num = 25;
							continue;
						}
						return A_1;
					}
					case 28:
						A_1.X = num5;
						A_1.Width = 0;
						num = 4;
						continue;
					case 29:
						goto IL_C3;
					case 30:
						if (A_2)
						{
							num = 14;
							continue;
						}
						A_1.Height = num3 - A_1.Y;
						num = 21;
						continue;
					case 31:
					{
						if (A_0.Length == 0)
						{
							num = 13;
							continue;
						}
						int num4 = A_0.IndexOf('C');
						bool flag = A_0[0] == 'R';
						bool flag2 = num4 != -1;
						num = 16;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num = 31;
					continue;
					IL_C3:
					num = 27;
					continue;
					IL_15E:
					num = 9;
					continue;
					IL_1EA:
					length = num2 - 1;
					num = 18;
					continue;
					IL_234:
					string a_2 = text;
					num = 5;
					continue;
					IL_255:
					num = 23;
					continue;
					IL_2C7:
					string a_3 = text2;
					num3 = this.ᜁ(a_3, true);
					num5 = this.ᜁ(a_2, false);
					num = 0;
				}
				IL_BE:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉ɋ⽍㵏㝑", a_));
				IL_289:
				return A_1;
				IL_342:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕅㱇㡉ṋ⭍㙏㝑♓㍕㙗㥙㥛", a_), RecordTableEnumerator.b("Յ⥇⑉歋㩍灏㹑㭓㕕㥗⹙㥛繝቟ൡ፣䙥ݧᡩ䱫൭Ὧṱų᭵ᙷ婹ཻ᭽ꒉ", a_));
				IL_37E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕅㱇㡉ɋ⽍㵏㝑瑓㽕⭗穙㥛㍝ၟᙡᵣ䡥", a_));
			}
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0008DFF0 File Offset: 0x0008CFF0
		private int ᜁ(string A_0, bool A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 14;
				int num2;
				for (;;)
				{
					int length;
					switch (num)
					{
					case 0:
						num2 += (A_1 ? this.Row : this.Column);
						num = 4;
						continue;
					case 1:
						num = 0;
						continue;
					case 2:
						num = 23;
						continue;
					case 3:
						if (!A_1)
						{
							num = 20;
							continue;
						}
						goto IL_B9;
					case 4:
						goto IL_267;
					case 5:
					{
						double num3;
						if (num3 >= -2147483648.0)
						{
							num = 2;
							continue;
						}
						goto IL_2B5;
					}
					case 6:
						num = 5;
						continue;
					case 7:
						num = 15;
						continue;
					case 8:
					{
						double num3;
						if (double.TryParse(A_0, NumberStyles.Integer, null, out num3))
						{
							num = 6;
							continue;
						}
						goto IL_2B5;
					}
					case 9:
						goto IL_179;
					case 10:
						num = 3;
						continue;
					case 11:
					{
						bool flag;
						if (flag)
						{
							num = 1;
							continue;
						}
						return num2;
					}
					case 12:
					{
						if (length == 0)
						{
							num = 9;
							continue;
						}
						bool flag = false;
						num = 18;
						continue;
					}
					case 13:
					{
						double num3;
						num2 = (int)num3;
						num = 11;
						continue;
					}
					case 15:
						if (A_0[length - 1] == ']')
						{
							num = 17;
							continue;
						}
						goto IL_129;
					case 16:
						goto IL_2A1;
					case 17:
					{
						A_0 = A_0.Substring(1, length - 2);
						bool flag = true;
						num = 21;
						continue;
					}
					case 18:
						if (A_0[0] == '[')
						{
							num = 7;
							continue;
						}
						goto IL_129;
					case 19:
						if (A_1)
						{
							goto IL_9D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_179;
						default:
							if (false)
							{
							}
							num = 22;
							continue;
						}
						break;
					case 20:
						num = 16;
						continue;
					case 21:
						goto IL_129;
					case 22:
						goto IL_257;
					case 23:
					{
						double num3;
						if (num3 <= 2147483647.0)
						{
							num = 13;
							continue;
						}
						goto IL_2B5;
					}
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					length = A_0.Length;
					num = 12;
					continue;
					IL_129:
					num = 8;
					continue;
					IL_179:
					num = 19;
				}
				IL_9D:
				return this.Row;
				IL_B9:
				if (true)
				{
				}
				return this.m_book.MaxRowCount;
				IL_257:
				return this.Column;
				IL_267:
				return num2;
				IL_2A1:
				return this.m_book.MaxColumnCount;
				IL_2B5:
				throw new ApplicationException(RecordTableEnumerator.b("戵倷弹᰻嬽㠿㉁㙃⍅㭇㥉╋⅍㹏牑ᝓ㝕㙗穙㉛ㅝᑟ䉡٣ͥ䡧ᩩ൫ᱭͯ᝱ၳ坵", a_));
			}
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0008E2C8 File Offset: 0x0008D2C8
		private string ᜀ(bool A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				string text;
				for (;;)
				{
					int num3;
					spr᱒ spr᱒;
					switch (num)
					{
					case 0:
						goto IL_17D;
					case 1:
						goto IL_221;
					case 2:
						goto IL_FC;
					case 3:
						goto IL_1F5;
					case 4:
					{
						string b;
						if (text != b)
						{
							num = 20;
							continue;
						}
						int num2;
						num2++;
						num = 16;
						continue;
					}
					case 5:
						if (text != null)
						{
							num = 17;
							continue;
						}
						return text;
					case 6:
						goto IL_29B;
					case 8:
					{
						int num2;
						int lastColumn;
						if (num2 > lastColumn)
						{
							num = 13;
							continue;
						}
						spr\u24F1 spr_u24F;
						spr_u24F.ᜀ(num3, num2);
						string b = spr_u24F.ᜀ(A_0);
						num = 4;
						continue;
					}
					case 9:
						goto IL_15D;
					case 10:
						if (!this.IsSingleCell)
						{
							num = 6;
							continue;
						}
						goto IL_D2;
					case 11:
						num = 10;
						continue;
					case 12:
						return text;
					case 13:
						goto IL_221;
					case 14:
						if (this.\u171D.ᜂ(spr᱒))
						{
							num = 23;
							continue;
						}
						return text;
					case 15:
						num = 14;
						continue;
					case 16:
						goto IL_FC;
					case 17:
					{
						num3 = this.Row;
						int lastRow = this.LastRow;
						num = 9;
						continue;
					}
					case 18:
						if (this.IsSingleCell)
						{
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F5;
						default:
						{
							if (false)
							{
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							spr_u24F.ᜀ(this.Row, this.Column);
							text = spr_u24F.ᜀ(A_0);
							num = 5;
							continue;
						}
						}
						break;
					case 19:
					{
						int lastRow;
						if (num3 > lastRow)
						{
							num = 0;
							continue;
						}
						int num2 = this.Column;
						int lastColumn = this.LastColumn;
						num = 2;
						continue;
					}
					case 20:
						text = null;
						num = 1;
						continue;
					case 21:
						if (spr᱒ != null)
						{
							num = 15;
							continue;
						}
						return text;
					case 22:
						goto IL_15D;
					case 23:
						text = this.ᜀ(spr᱒, A_0);
						num = 12;
						continue;
					}
					if (this.CellType != XlsRange.TCellType.Formula)
					{
						num = 11;
						continue;
					}
					IL_D2:
					text = null;
					num = 18;
					continue;
					IL_FC:
					num = 8;
					continue;
					IL_15D:
					num = 19;
					continue;
					IL_1F5:
					spr᱒ = (this.Record as spr᱒);
					num = 21;
					continue;
					IL_221:
					num3++;
					num = 22;
				}
				IL_17D:
				if (true)
				{
				}
				return text;
				IL_29B:
				return null;
			}
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0008E5B0 File Offset: 0x0008D5B0
		private void ᜀ(string A_0, bool A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 6;
				ParseFormulaOptions parseFormulaOptions;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_D5;
					case 1:
						if (num2 == 0)
						{
							num = 4;
							continue;
						}
						num = 5;
						continue;
					case 2:
						A_0 = A_0.Substring(2, num2 - 3);
						num2 -= 3;
						num = 3;
						continue;
					case 3:
						goto IL_D5;
					case 4:
						goto IL_D0;
					case 5:
						if (A_0.StartsWith(RecordTableEnumerator.b("䴵Է", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_119;
					case 7:
						if (true)
						{
						}
						if (A_1)
						{
							num = 14;
							continue;
						}
						goto IL_1F1;
					case 8:
						A_0 = A_0.Substring(1, num2 - 1);
						num = 0;
						continue;
					case 9:
						goto IL_71;
					case 10:
						if (A_0[num2 - 1] == '}')
						{
							num = 2;
							continue;
						}
						goto IL_119;
					case 11:
						if (A_0[0] == '=')
						{
							num = 8;
							continue;
						}
						goto IL_D5;
					case 12:
						goto IL_17B;
					case 13:
						num = 10;
						continue;
					case 14:
						parseFormulaOptions |= ParseFormulaOptions.UseR1C1;
						num = 12;
						continue;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num2 = A_0.Length;
					num = 1;
					continue;
					IL_D5:
					this.ᜉ();
					parseFormulaOptions = (ParseFormulaOptions.RootLevel | ParseFormulaOptions.InArray);
					num = 7;
					continue;
					IL_119:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B0;
					default:
						if (false)
						{
						}
						num = 11;
						break;
					}
				}
				IL_71:
				throw new ArgumentNullException(RecordTableEnumerator.b("瀵圷䠹儻䬽ⰿ⍁Ճ㑅㩇⭉㕋", a_));
				IL_D0:
				goto IL_1B0;
				IL_17B:
				goto IL_1F1;
				IL_1B0:
				throw new ArgumentException(RecordTableEnumerator.b("瀵圷䠹儻䬽ⰿ⍁Ճ㑅㩇⭉㕋湍㍏㍑㩓煕ⱗ穙㹛㭝䁟ݡॣᙥᱧ፩", a_));
				IL_1F1:
				Ptg[] a_2 = this.m_book.FormulaUtil.ᜁ(A_0, this.\u171D, null, 0, null, parseFormulaOptions, this.Row - 1, this.Column - 1);
				spr\u225F spr_u225F = (spr\u225F)spr\u175E.ᜀ(TBIFFRecord.Array);
				spr_u225F.ᜀ(a_2);
				spr_u225F.ᜁ(true);
				spr_u225F.ᜂ(this.FirstRow - 1);
				spr_u225F.ᜃ(this.FirstColumn - 1);
				spr_u225F.ᜀ(this.LastRow - 1);
				spr_u225F.ᜁ(this.LastColumn - 1);
				this.ᜀ(spr_u225F);
				return;
			}
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0008E838 File Offset: 0x0008D838
		[CLSCompliant(false)]
		internal void ᜀ(spr\u225F A_0)
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
			this.ᜀ(A_0, -1);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0008E87C File Offset: 0x0008D87C
		[CLSCompliant(false)]
		internal void ᜀ(spr\u225F A_0, int A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Ptg ptg = FormulaUtil.ᜀ(FormulaToken.tExp, new object[]
					{
						A_0.ᜉ(),
						A_0.ᜈ()
					});
					Ptg[] a_ = new Ptg[]
					{
						ptg
					};
					spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(TBIFFRecord.Formula);
					spr᱒.ᜁ(a_);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2;
							if (num2 > this.LastColumn)
							{
								num = 12;
								continue;
							}
							int num3;
							XlsRange xlsRange = (XlsRange)this.\u171D[num3, num2];
							spr᱒ = (spr᱒)spr᱒.ᜆ();
							this.ᜀ(spr᱒, xlsRange, A_1);
							xlsRange.ᜁ(spr᱒);
							num2++;
							num = 13;
							continue;
						}
						case 1:
							goto IL_24D;
						case 2:
							this.ᜀ(spr᱒, this, A_1);
							this.Record = spr᱒;
							num = 1;
							continue;
						case 3:
							goto IL_107;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B8;
							default:
								if (false)
								{
								}
								spr᱒.ᜁ((ushort)A_1);
								num = 3;
								continue;
							}
							break;
						case 5:
							if (A_1 != -1)
							{
								num = 4;
								continue;
							}
							goto IL_107;
						case 6:
						{
							int num3;
							if (num3 > this.LastRow)
							{
								num = 7;
								continue;
							}
							int num2 = this.FirstColumn;
							if (true)
							{
							}
							num = 9;
							continue;
						}
						case 7:
							goto IL_1E2;
						case 8:
							goto IL_1BD;
						case 9:
							goto IL_C2;
						case 10:
							goto IL_1BD;
						case 11:
						{
							if (this.IsSingleCell)
							{
								num = 2;
								continue;
							}
							XlsRange xlsRange = (XlsRange)this.\u171D[this.FirstRow, this.FirstColumn];
							this.ᜀ(spr᱒, xlsRange, A_1);
							xlsRange.Record = spr᱒;
							int num3 = this.FirstRow;
							num = 8;
							continue;
						}
						case 12:
						{
							int num3;
							num3++;
							num = 10;
							continue;
						}
						case 13:
							goto IL_1B8;
						}
						break;
						IL_C2:
						num = 0;
						continue;
						IL_1B8:
						goto IL_C2;
						IL_107:
						num = 11;
						continue;
						IL_1BD:
						num = 6;
					}
				}
				IL_1E2:
				IL_24D:
				this.\u171D.CellRecords.ᜀ(A_0);
				return;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0008EAEC File Offset: 0x0008DAEC
		private void ᜀ(spr\u23A5 A_0, XlsRange A_1, int A_2)
		{
			for (;;)
			{
				A_0.ᜃ(A_1.FirstRow - 1);
				A_0.ᜄ(A_1.FirstColumn - 1);
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
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (A_2 == -1)
							{
								num = 1;
								continue;
							}
							return;
						case 1:
							A_0.ᜀ(A_1.ExtendedFormatIndex);
							num = 2;
							continue;
						case 2:
							return;
						}
						break;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0008EB88 File Offset: 0x0008DB88
		private int ᜁ(int A_0, int A_1, int A_2)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						switch (A_0)
						{
						case -4:
							goto IL_6F;
						case -3:
							goto IL_9B;
						case -2:
							goto IL_82;
						case -1:
							return 1;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return A_0;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						break;
					case 2:
						goto IL_99;
					}
					break;
				}
			}
			IL_6F:
			return this.\u171D.CellRecords.GetMinimumRowIndex(A_1, A_2);
			IL_82:
			return this.m_book.MaxRowCount;
			IL_99:
			return A_0;
			IL_9B:
			return this.\u171D.CellRecords.GetMaximumRowIndex(A_1, A_2);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0008EC48 File Offset: 0x0008DC48
		private int ᜀ(int A_0, int A_1, int A_2)
		{
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (A_0)
						{
						case -4:
							goto IL_67;
						case -3:
							goto IL_9B;
						case -2:
							goto IL_7A;
						case -1:
							return 1;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return A_0;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_91;
					}
					break;
				}
			}
			IL_67:
			return this.\u171D.CellRecords.GetMinimumColumnIndex(A_1, A_2);
			IL_7A:
			return this.m_book.MaxColumnCount;
			IL_91:
			if (true)
			{
			}
			return A_0;
			IL_9B:
			return this.\u171D.CellRecords.GetMaximumColumnIndex(A_1, A_2);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0008ED08 File Offset: 0x0008DD08
		private XlsValidation ᜇ()
		{
			int a_ = 3;
			if (this.IsSingleCell)
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
					XlsDataValidationTable dvtable = this.\u171D.DVTable;
					long iCellIndex = sprṔ.ᜀ(this.m_iLeftColumn, this.m_iTopRow);
					return dvtable.FindDataValidation(iCellIndex);
				}
				}
			}
			if (true)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("稸娺匼ᠾ㕀捂♄♆╈❊浌㭎㥐㩒♔睖㑘㹚⥜㝞๠ݢ䕤Ŧ٨ᥪ䵬ᵮၰᵲቴቶ੸孺ॼ᝾ꖄ歷ﲎ랖ﶚ붜풠쾢톤캦\ud9a8잪좬辮튰횲\ud9b4\udbb6쪸", a_));
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0008ED90 File Offset: 0x0008DD90
		public void PartialClear()
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
			this.ᜡ = null;
			this.m_style = null;
			this.ᜢ = false;
			this.m_dataValidation = null;
			this.m_rtfString = null;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0008EDF0 File Offset: 0x0008DDF0
		protected void SetBorderToSingleCell(BordersLineType borderIndex, LineStyleType borderLine, ExcelColors borderColor)
		{
			int a_ = 7;
			if (this.IsSingleCell)
			{
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
					IBorder border = this.Borders[borderIndex];
					border.LineStyle = borderLine;
					border.KnownColor = borderColor;
					return;
				}
				}
			}
			throw new NotSupportedException(RecordTableEnumerator.b("渼䨾ㅀ㍂⩄㕆㵈㡊浌⁎㽐㽒ⱔ睖㽘㑚⽜罞በ੢୤fը๪䵬౮ᑰὲᥴ奶", a_));
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0008EE70 File Offset: 0x0008DE70
		private void ᜀ(GroupByType A_0, bool A_1, ExpandCollapseFlags A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				int a_;
				int a_2;
				int a_3;
				bool a_4;
				XlsRange.ᜀ a_5;
				for (;;)
				{
					switch (num)
					{
					case 1:
						a_ = this.Row;
						a_2 = this.LastRow;
						a_3 = this.m_book.MaxRowCount;
						a_4 = this.\u171D.PageSetup.IsSummaryRowBelow;
						a_5 = new XlsRange.ᜀ(this.ᜁ);
						num = 2;
						continue;
					case 2:
						goto IL_F8;
					case 3:
						goto IL_B1;
					}
					if (A_0 == GroupByType.ByRows)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FA;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							break;
						}
					}
					else
					{
						a_ = this.Column;
						a_2 = this.LastColumn;
						a_3 = this.m_book.MaxColumnCount;
						a_4 = this.\u171D.PageSetup.IsSummaryColumnRight;
						a_5 = new XlsRange.ᜀ(this.ᜀ);
						num = 3;
					}
				}
				IL_B1:
				IL_F8:
				IL_FA:
				this.ᜀ(A_1, a_, a_2, a_3, a_4, a_5, A_2);
				return;
			}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0008EF88 File Offset: 0x0008DF88
		private void ᜀ(bool A_0, int A_1, int A_2, int A_3, bool A_4, XlsRange.ᜀ A_5, ExpandCollapseFlags A_6)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					bool a_ = (A_6 & ExpandCollapseFlags.IncludeSubgroups) != ExpandCollapseFlags.Default;
					int num = 6;
					for (;;)
					{
						bool flag;
						int num2;
						int num3;
						int a_2;
						int a_3;
						switch (num)
						{
						case 0:
							if (flag)
							{
								num = 8;
								continue;
							}
							return;
						case 1:
							if (A_0)
							{
								num = 10;
								continue;
							}
							num = 0;
							continue;
						case 2:
							goto IL_151;
						case 3:
							num2 = A_2 + 1;
							goto IL_BB;
						case 4:
							num = 18;
							continue;
						case 5:
							if (num3 <= A_3)
							{
								num = 14;
								continue;
							}
							goto IL_151;
						case 6:
							if (!A_4)
							{
								num = 17;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_21A;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 7:
							num2 = A_1 - 1;
							goto IL_BB;
						case 8:
							this.ᜀ(A_1, A_2, A_5, a_, A_4);
							num = 15;
							continue;
						case 9:
							if (num3 > 0)
							{
								num = 13;
								continue;
							}
							goto IL_151;
						case 10:
							goto IL_1C6;
						case 11:
							if (!flag)
							{
								num = 4;
								continue;
							}
							goto IL_1AB;
						case 12:
							flag = true;
							this.ᜀ(A_0, a_2, a_3, A_3, A_4, A_5, ExpandCollapseFlags.ExpandParent);
							goto IL_21A;
						case 13:
						{
							spr\u2502 spr_u = A_5(num3);
							spr_u.ᜀ(A_0);
							num = 2;
							continue;
						}
						case 14:
							if (true)
							{
							}
							num = 9;
							continue;
						case 15:
							goto IL_11D;
						case 16:
							goto IL_1AB;
						case 17:
							num = 7;
							continue;
						case 18:
							if ((A_6 & ExpandCollapseFlags.ExpandParent) != ExpandCollapseFlags.Default)
							{
								num = 12;
								continue;
							}
							goto IL_1AB;
						}
						break;
						IL_BB:
						num3 = num2;
						num = 5;
						continue;
						IL_151:
						spr\u2502 spr_u2 = A_5(A_1);
						spr\u2502 spr_u3 = A_5(A_2);
						Math.Min(spr_u2.ᜀ(), spr_u3.ᜀ());
						a_2 = A_1;
						a_3 = A_2;
						flag = this.ᜀ(ref a_2, ref a_3, A_3, A_5);
						num = 11;
						continue;
						IL_1AB:
						num = 1;
						continue;
						IL_21A:
						num = 16;
					}
				}
				IL_11D:
				return;
				IL_1C6:
				this.ᜀ(A_1, A_2, A_5, true);
				return;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0008F1E0 File Offset: 0x0008E1E0
		private void ᜀ(int A_0, int A_1, XlsRange.ᜀ A_2, bool A_3)
		{
			for (;;)
			{
				IL_18:
				if (true)
				{
				}
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_48:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num2 = A_0;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_87;
					case 1:
						return;
					case 2:
					{
						if (num2 > A_1)
						{
							num = 1;
							continue;
						}
						spr\u2502 spr_u = A_2(num2);
						spr_u.ᜁ(A_3);
						num2++;
						num = 0;
						continue;
					}
					case 3:
						goto IL_46;
					}
					goto IL_18;
				}
				IL_46:
				IL_87:
				goto IL_48;
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0008F278 File Offset: 0x0008E278
		private void ᜀ(int A_0, int A_1, XlsRange.ᜀ A_2, bool A_3, bool A_4)
		{
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					spr\u2502 spr_u;
					int num2;
					int num3;
					int num4;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1DD;
						default:
							if (false)
							{
							}
							goto IL_FC;
						}
						break;
					case 1:
						goto IL_FC;
					case 2:
						if (true)
						{
						}
						spr_u.ᜀ(false);
						spr_u.ᜁ(false);
						num = 3;
						continue;
					case 3:
						goto IL_FC;
					case 5:
						return;
					case 6:
						if (A_4)
						{
							num = 12;
							continue;
						}
						num2 = 1;
						num = 16;
						continue;
					case 7:
						goto IL_1DD;
					case 8:
						if (num3 == num4)
						{
							num = 5;
							continue;
						}
						spr_u = A_2(num3);
						num = 7;
						continue;
					case 9:
						goto IL_14B;
					case 10:
						goto IL_14B;
					case 11:
						goto IL_74;
					case 12:
						this.ᜀ(ref A_0, ref A_1);
						num2 = -1;
						num = 15;
						continue;
					case 13:
					{
						spr\u2502 spr_u2;
						if (spr_u.ᜀ() >= spr_u2.ᜀ())
						{
							num = 2;
							continue;
						}
						num3 = this.ᜀ(num3 + num2, num2, int.MaxValue, A_2, (int)spr_u2.ᜀ());
						spr_u.ᜁ(false);
						num = 1;
						continue;
					}
					case 14:
					{
						spr\u2502 spr_u2 = A_2(num3 + num2);
						num = 13;
						continue;
					}
					case 15:
						goto IL_16C;
					case 16:
						goto IL_16C;
					}
					if (A_3)
					{
						num = 11;
						continue;
					}
					num = 6;
					continue;
					IL_FC:
					num3 += num2;
					num = 9;
					continue;
					IL_14B:
					num = 8;
					continue;
					IL_16C:
					num3 = A_0;
					num4 = A_1 + num2;
					num = 10;
					continue;
					IL_1DD:
					if (spr_u.ᜁ())
					{
						num = 14;
					}
					else
					{
						spr_u.ᜁ(false);
						num = 0;
					}
				}
				IL_74:
				this.ᜀ(A_0, A_1, A_2, false);
				return;
			}
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0008F490 File Offset: 0x0008E490
		private void ᜀ(ref int A_0, ref int A_1)
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
			int num = A_1;
			A_1 = A_0;
			A_0 = num;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0008F4D8 File Offset: 0x0008E4D8
		private bool ᜀ(ref int A_0, ref int A_1, int A_2, XlsRange.ᜀ A_3)
		{
			switch (0)
			{
			default:
			{
				int num6;
				for (;;)
				{
					spr\u2502 spr_u = A_3(A_0);
					int num = (int)spr_u.ᜀ();
					int num2 = 0;
					for (;;)
					{
						int num5;
						int num7;
						switch (num2)
						{
						case 0:
						{
							if (num <= 1)
							{
								num2 = 11;
								continue;
							}
							int num3 = this.ᜀ(A_0, -1, A_2, A_3);
							int num4 = this.ᜀ(A_1, 1, A_2, A_3);
							num2 = 10;
							continue;
						}
						case 1:
							num5 = 0;
							goto IL_15A;
						case 2:
							if (num6 == 0)
							{
								num2 = 9;
								continue;
							}
							goto IL_185;
						case 3:
						{
							int num3;
							num7 = (int)A_3(num3).ᜀ();
							goto IL_95;
						}
						case 4:
						{
							int num4;
							if (num4 <= 0)
							{
								goto IL_A7;
							}
							num2 = 7;
							continue;
						}
						case 5:
							num2 = 1;
							continue;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A7;
							default:
								if (false)
								{
								}
								num2 = 8;
								continue;
							}
							break;
						case 7:
						{
							int num4;
							num5 = (int)A_3(num4).ᜀ();
							goto IL_15A;
						}
						case 8:
							num7 = 0;
							goto IL_95;
						case 9:
							return true;
						case 10:
						{
							int num3;
							if (num3 <= 0)
							{
								if (true)
								{
								}
								num2 = 6;
								continue;
							}
							num2 = 3;
							continue;
						}
						case 11:
							return true;
						}
						break;
						IL_95:
						int val = num7;
						num2 = 4;
						continue;
						IL_A7:
						num2 = 5;
						continue;
						IL_15A:
						int val2 = num5;
						num6 = Math.Min(val, val2);
						num2 = 2;
					}
				}
				return true;
				IL_185:
				int num8 = this.ᜀ(A_0, -1, A_2, A_3, num6);
				int num9 = this.ᜀ(A_1, 1, A_2, A_3, num6);
				int a_ = (int)A_3(num8).ᜀ();
				A_0 = num8;
				A_1 = num9;
				return this.ᜀ(A_0, A_1, A_3, a_) != -1;
			}
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0008F6B8 File Offset: 0x0008E6B8
		private int ᜀ(int A_0, int A_1, int A_2, XlsRange.ᜀ A_3)
		{
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					for (;;)
					{
						int num = (int)A_3(A_0).ᜀ();
						result = -1;
						int num2 = A_0 + A_1;
						int num3 = 6;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_5A;
							case 1:
								result = num2;
								num3 = 5;
								continue;
							case 2:
								num3 = 8;
								continue;
							case 3:
								goto IL_DE;
							case 4:
								if (num2 > 0)
								{
									num3 = 2;
									continue;
								}
								return result;
							case 5:
								return result;
							case 6:
								goto IL_5A;
							case 7:
							{
								spr\u2502 spr_u;
								if ((int)spr_u.ᜀ() < num)
								{
									num3 = 1;
									continue;
								}
								num2 += A_1;
								num3 = 0;
								continue;
							}
							case 8:
							{
								if (num2 > A_2)
								{
									if (true)
									{
									}
									num3 = 3;
									continue;
								}
								spr\u2502 spr_u = A_3(num2);
								num3 = 7;
								continue;
							}
							}
							break;
							IL_5A:
							num3 = 4;
						}
					}
					IL_DE:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_F4;
					}
				}
				IL_F4:
				if (false)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0008F7D4 File Offset: 0x0008E7D4
		private int ᜀ(int A_0, int A_1, int A_2, XlsRange.ᜀ A_3, int A_4)
		{
			int num;
			for (;;)
			{
				num = A_0;
				int num2 = 2;
				for (;;)
				{
					spr\u2502 spr_u;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						if (num <= A_2)
						{
							num2 = 4;
							continue;
						}
						goto IL_BC;
					case 1:
						if (num > 0)
						{
							num2 = 5;
							continue;
						}
						goto IL_BC;
					case 2:
						goto IL_89;
					case 3:
						if ((int)spr_u.ᜀ() < A_4)
						{
							num2 = 6;
							continue;
						}
						goto IL_89;
					case 4:
						num2 = 3;
						continue;
					case 5:
						num2 = 0;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_60;
						}
						break;
					}
					break;
					IL_89:
					num += A_1;
					spr_u = A_3(num);
					num2 = 1;
				}
			}
			IL_60:
			if (false)
			{
			}
			IL_BC:
			return num - A_1;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0008F8A4 File Offset: 0x0008E8A4
		private int ᜀ(int A_0, int A_1, XlsRange.ᜀ A_2, int A_3)
		{
			int result;
			for (;;)
			{
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					result = -1;
					num = A_0;
					if (true)
					{
					}
					num2 = 7;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AC;
					case 1:
						num2 = 4;
						continue;
					case 2:
					{
						if (num > A_1)
						{
							num2 = 3;
							continue;
						}
						spr\u2502 spr_u = A_2(num);
						num2 = 5;
						continue;
					}
					case 3:
						return result;
					case 4:
					{
						spr\u2502 spr_u;
						if (!spr_u.ᜂ())
						{
							num2 = 8;
							continue;
						}
						goto IL_5E;
					}
					case 5:
					{
						spr\u2502 spr_u;
						if ((int)spr_u.ᜀ() == A_3)
						{
							num2 = 1;
							continue;
						}
						goto IL_5E;
					}
					case 6:
						return result;
					case 7:
						goto IL_AC;
					case 8:
						result = num;
						num2 = 6;
						continue;
					}
					break;
					IL_5E:
					num++;
					num2 = 0;
					continue;
					IL_AC:
					num2 = 2;
				}
			}
			return result;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0008F99C File Offset: 0x0008E99C
		internal IList<object> ᜀ(ref PivotDataType A_0)
		{
			switch (0)
			{
			default:
			{
				Dictionary<object, object> dictionary;
				for (;;)
				{
					bool flag = true;
					object value = new object();
					dictionary = new Dictionary<object, object>();
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					int row = this.Row;
					int column = this.Column;
					int lastRow = this.LastRow;
					int num = row;
					int num2 = 40;
					for (;;)
					{
						object obj;
						string text;
						double number;
						object obj2;
						switch (num2)
						{
						case 0:
							if (!dictionary.ContainsKey(obj))
							{
								num2 = 29;
								continue;
							}
							goto IL_765;
						case 1:
							goto IL_29F;
						case 2:
							num2 = 50;
							continue;
						case 3:
							A_0 |= PivotDataType.LongText;
							num2 = 22;
							continue;
						case 4:
							goto IL_562;
						case 5:
							num2 = 26;
							continue;
						case 6:
							if (text != null)
							{
								num2 = 64;
								continue;
							}
							goto IL_1A8;
						case 7:
						{
							if (num > lastRow)
							{
								num2 = 60;
								continue;
							}
							XlsWorksheet.TRangeValueType cellType = this.\u171D.GetCellType(num, column, false);
							obj = null;
							XlsWorksheet.TRangeValueType trangeValueType = cellType;
							goto IL_3AB;
						}
						case 8:
						{
							XlsWorksheet.TRangeValueType trangeValueType;
							if (trangeValueType != XlsWorksheet.TRangeValueType.String)
							{
								num2 = 5;
								continue;
							}
							text = this.\u171D.GetText(num, column);
							num2 = 6;
							continue;
						}
						case 9:
							text = obj.ToString().ToLower();
							num2 = 51;
							continue;
						case 10:
							goto IL_52E;
						case 11:
							goto IL_52E;
						case 12:
							if (true)
							{
							}
							goto IL_765;
						case 13:
							num2 = 54;
							continue;
						case 14:
							dictionary2.Add(text, value);
							dictionary.Add(obj, value);
							num2 = 18;
							continue;
						case 15:
							goto IL_1A8;
						case 16:
							if (text.Length <= 0)
							{
								num2 = 15;
								continue;
							}
							goto IL_562;
						case 17:
						{
							CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(number);
							num2 = 42;
							continue;
						}
						case 18:
							goto IL_765;
						case 19:
							dictionary.Add(obj, value);
							num2 = 12;
							continue;
						case 20:
							goto IL_765;
						case 21:
						{
							XlsWorksheet.TRangeValueType trangeValueType;
							if (trangeValueType != XlsWorksheet.TRangeValueType.Formula)
							{
								num2 = 36;
								continue;
							}
							bool flag2 = false;
							obj = this.ᜀ(ref A_0, num, column, ref flag2);
							num2 = 67;
							continue;
						}
						case 22:
							goto IL_5E6;
						case 23:
							goto IL_765;
						case 24:
							if (!dictionary.ContainsKey(obj))
							{
								num2 = 34;
								continue;
							}
							goto IL_765;
						case 25:
							obj2 = TimeSpan.FromDays(number);
							goto IL_820;
						case 26:
							goto IL_24B;
						case 27:
							A_0 = PivotDataType.Number;
							flag = false;
							num2 = 59;
							continue;
						case 28:
							goto IL_765;
						case 29:
							dictionary.Add(obj, value);
							num2 = 20;
							continue;
						case 30:
							if (!dictionary2.ContainsKey(text))
							{
								num2 = 61;
								continue;
							}
							goto IL_765;
						case 31:
							goto IL_16B;
						case 32:
							if (!double.IsNaN(number))
							{
								num2 = 17;
								continue;
							}
							obj = number;
							num2 = 48;
							continue;
						case 33:
							if (text.Length > 255)
							{
								num2 = 3;
								continue;
							}
							goto IL_5E6;
						case 34:
							dictionary2.Add(text, value);
							dictionary.Add(obj, value);
							num2 = 28;
							continue;
						case 35:
							goto IL_77C;
						case 36:
							num2 = 8;
							continue;
						case 37:
							if (!dictionary.ContainsKey(obj))
							{
								num2 = 19;
								continue;
							}
							goto IL_765;
						case 38:
							goto IL_6F2;
						case 39:
							goto IL_765;
						case 40:
							goto IL_319;
						case 41:
							dictionary2.Add(text, value);
							dictionary.Add(obj, value);
							num2 = 39;
							continue;
						case 42:
						{
							CellFormatType cellFormatType;
							if (cellFormatType == CellFormatType.Unknown)
							{
								num2 = 13;
								continue;
							}
							goto IL_6F2;
						}
						case 43:
						{
							XlsWorksheet.TRangeValueType trangeValueType;
							switch (trangeValueType)
							{
							case XlsWorksheet.TRangeValueType.Blank:
								text = string.Empty;
								A_0 |= PivotDataType.String;
								obj = text;
								num2 = 24;
								continue;
							case XlsWorksheet.TRangeValueType.Error:
							case XlsWorksheet.TRangeValueType.Error | XlsWorksheet.TRangeValueType.Boolean:
								goto IL_24B;
							case XlsWorksheet.TRangeValueType.Boolean:
								obj = this.\u171D.GetBoolean(num, column);
								A_0 |= PivotDataType.Boolean;
								num2 = 53;
								continue;
							case XlsWorksheet.TRangeValueType.Number:
								number = this.\u171D.GetNumber(num, column);
								num2 = 32;
								continue;
							default:
								num2 = 49;
								continue;
							}
							break;
						}
						case 44:
							obj2 = UtilityMethods.ᜀ(number);
							goto IL_820;
						case 45:
							goto IL_765;
						case 46:
							goto IL_319;
						case 47:
							if (number - Math.Floor(number) > 0.0)
							{
								num2 = 27;
								continue;
							}
							goto IL_16B;
						case 48:
							goto IL_16B;
						case 49:
							num2 = 21;
							continue;
						case 50:
						{
							CellFormatType cellFormatType;
							if (cellFormatType == CellFormatType.General)
							{
								num2 = 35;
								continue;
							}
							sprᤅ sprᤅ = this.InnerNumberFormat;
							A_0 |= PivotDataType.Date;
							num2 = 57;
							continue;
						}
						case 51:
							if (!dictionary2.ContainsKey(text))
							{
								num2 = 41;
								continue;
							}
							goto IL_765;
						case 52:
							dictionary.Add(obj, value);
							num2 = 45;
							continue;
						case 53:
							if (!dictionary.ContainsKey(obj))
							{
								num2 = 52;
								continue;
							}
							goto IL_765;
						case 54:
							if (number == 0.0)
							{
								num2 = 63;
								continue;
							}
							goto IL_6F2;
						case 55:
							if (text == string.Empty)
							{
								num2 = 4;
								continue;
							}
							A_0 |= PivotDataType.Blank;
							num2 = 10;
							continue;
						case 56:
							A_0 |= PivotDataType.Number;
							A_0 |= PivotDataType.Integer;
							num2 = 1;
							continue;
						case 57:
						{
							sprᤅ sprᤅ;
							if (!sprᤅ.ᜁ(number))
							{
								num2 = 58;
								continue;
							}
							num2 = 25;
							continue;
						}
						case 58:
							num2 = 44;
							continue;
						case 59:
							goto IL_16B;
						case 60:
							goto IL_33A;
						case 61:
							dictionary2.Add(text, value);
							dictionary.Add(obj, value);
							num2 = 23;
							continue;
						case 62:
							if (!dictionary2.ContainsKey(text))
							{
								num2 = 14;
								continue;
							}
							goto IL_765;
						case 63:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3AB;
							default:
							{
								if (false)
								{
								}
								CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(1.0);
								num2 = 38;
								continue;
							}
							}
							break;
						case 64:
							num2 = 16;
							continue;
						case 65:
						{
							CellFormatType cellFormatType;
							if (cellFormatType != CellFormatType.Number)
							{
								num2 = 2;
								continue;
							}
							goto IL_77C;
						}
						case 66:
							if (flag)
							{
								num2 = 56;
								continue;
							}
							goto IL_29F;
						case 67:
						{
							bool flag2;
							if (flag2)
							{
								num2 = 9;
								continue;
							}
							num2 = 0;
							continue;
						}
						}
						break;
						IL_16B:
						num2 = 37;
						continue;
						IL_1A8:
						num2 = 55;
						continue;
						IL_24B:
						obj = this.\u171D[num, column].NumberText;
						A_0 |= PivotDataType.String;
						text = obj.ToString().ToLower();
						num2 = 30;
						continue;
						IL_29F:
						num2 = 47;
						continue;
						IL_319:
						num2 = 7;
						continue;
						IL_3AB:
						num2 = 43;
						continue;
						IL_52E:
						obj = text;
						text = text.ToLower();
						num2 = 62;
						continue;
						IL_562:
						num2 = 33;
						continue;
						IL_5E6:
						A_0 |= PivotDataType.String;
						num2 = 11;
						continue;
						IL_6F2:
						num2 = 65;
						continue;
						IL_765:
						num++;
						num2 = 46;
						continue;
						IL_77C:
						obj = number;
						num2 = 66;
						continue;
						IL_820:
						obj = obj2;
						num2 = 31;
					}
				}
				IL_33A:
				object[] array = new object[dictionary.Count];
				dictionary.Keys.CopyTo(array, 0);
				return new List<object>(array);
			}
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00090228 File Offset: 0x0008F228
		private object ᜀ(ref PivotDataType A_0, int A_1, int A_2, ref bool A_3)
		{
			switch (0)
			{
			default:
			{
				object result;
				for (;;)
				{
					result = null;
					XlsRange.TCellType tcellType = (this.\u171D[A_1, A_2] as XlsRange).CellType;
					string formulaStringValue = this.\u171D.GetFormulaStringValue(A_1, A_2);
					XlsWorksheet.TRangeValueType cellType = this.\u171D.GetCellType(A_1, A_2, true);
					int num = 2;
					for (;;)
					{
						double formulaNumberValue;
						object obj;
						switch (num)
						{
						case 0:
							return result;
						case 1:
							if (tcellType != XlsRange.TCellType.LabelSST)
							{
								num = 32;
								continue;
							}
							goto IL_439;
						case 2:
							if ((cellType & XlsWorksheet.TRangeValueType.Boolean) == XlsWorksheet.TRangeValueType.Boolean)
							{
								num = 30;
								continue;
							}
							num = 37;
							continue;
						case 3:
							if (formulaNumberValue == 0.0)
							{
								num = 39;
								continue;
							}
							goto IL_25B;
						case 4:
						{
							sprᤅ sprᤅ;
							if (!sprᤅ.ᜁ(formulaNumberValue))
							{
								num = 25;
								continue;
							}
							num = 16;
							continue;
						}
						case 5:
							return result;
						case 6:
							return result;
						case 7:
							goto IL_1A4;
						case 8:
						{
							CellFormatType cellFormatType;
							if (cellFormatType == CellFormatType.General)
							{
								num = 9;
								continue;
							}
							sprᤅ sprᤅ = this.InnerNumberFormat;
							A_0 |= PivotDataType.Date;
							num = 4;
							continue;
						}
						case 9:
							goto IL_299;
						case 10:
							goto IL_439;
						case 11:
							num = 40;
							continue;
						case 12:
							result = string.Empty;
							A_0 |= PivotDataType.Blank;
							A_3 = true;
							num = 27;
							continue;
						case 13:
							num = 23;
							continue;
						case 14:
							if (!double.IsNaN(formulaNumberValue))
							{
								num = 36;
								continue;
							}
							result = formulaNumberValue;
							num = 0;
							continue;
						case 15:
							num = 1;
							continue;
						case 16:
							obj = TimeSpan.FromDays(formulaNumberValue);
							goto IL_36B;
						case 17:
							if (tcellType == XlsRange.TCellType.Label)
							{
								num = 10;
								continue;
							}
							num = 22;
							continue;
						case 18:
							A_0 |= ((formulaNumberValue <= 2147483647.0 && formulaNumberValue >= -2147483648.0 && Math.Round(formulaNumberValue) == formulaNumberValue) ? PivotDataType.Integer : PivotDataType.Float);
							if (true)
							{
							}
							num = 28;
							continue;
						case 19:
							goto IL_25B;
						case 20:
							num = 8;
							continue;
						case 21:
							if (tcellType != XlsRange.TCellType.Formula)
							{
								num = 15;
								continue;
							}
							goto IL_439;
						case 22:
							if (tcellType == XlsRange.TCellType.Blank)
							{
								num = 12;
								continue;
							}
							result = (this.\u171D[A_1, A_2] as XlsRange).GetDisplayString();
							A_0 |= PivotDataType.String;
							A_3 = true;
							num = 6;
							continue;
						case 23:
							if (tcellType != XlsRange.TCellType.RK)
							{
								num = 11;
								continue;
							}
							goto IL_1A4;
						case 24:
						{
							CellFormatType cellFormatType;
							if (cellFormatType == CellFormatType.Unknown)
							{
								num = 35;
								continue;
							}
							goto IL_25B;
						}
						case 25:
							num = 33;
							continue;
						case 26:
							num = 17;
							continue;
						case 27:
							return result;
						case 28:
							return result;
						case 29:
							if (tcellType != XlsRange.TCellType.RString)
							{
								num = 26;
								continue;
							}
							goto IL_439;
						case 30:
							goto IL_113;
						case 31:
						{
							CellFormatType cellFormatType;
							if (cellFormatType != CellFormatType.Number)
							{
								num = 20;
								continue;
							}
							goto IL_299;
						}
						case 32:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_113;
							default:
								if (false)
								{
								}
								num = 29;
								continue;
							}
							break;
						case 33:
							obj = UtilityMethods.ᜀ(formulaNumberValue);
							goto IL_36B;
						case 34:
							return result;
						case 35:
							num = 3;
							continue;
						case 36:
						{
							CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(formulaNumberValue);
							num = 24;
							continue;
						}
						case 37:
							if (formulaStringValue != null)
							{
								num = 13;
								continue;
							}
							goto IL_1A4;
						case 38:
							return result;
						case 39:
						{
							CellFormatType cellFormatType = this.InnerNumberFormat.ᜀ(1.0);
							num = 19;
							continue;
						}
						case 40:
							if (tcellType == XlsRange.TCellType.Number)
							{
								num = 7;
								continue;
							}
							num = 21;
							continue;
						}
						break;
						IL_113:
						result = this.\u171D.GetFormulaBoolValue(A_1, A_2);
						A_0 |= PivotDataType.Boolean;
						num = 5;
						continue;
						IL_1A4:
						formulaNumberValue = this.\u171D.GetFormulaNumberValue(A_1, A_2);
						num = 14;
						continue;
						IL_25B:
						num = 31;
						continue;
						IL_299:
						result = formulaNumberValue;
						A_0 |= PivotDataType.Number;
						num = 18;
						continue;
						IL_36B:
						result = obj;
						num = 38;
						continue;
						IL_439:
						result = this.\u171D.GetFormulaStringValue(A_1, A_2);
						A_0 |= PivotDataType.String;
						A_3 = true;
						num = 34;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00090705 File Offset: 0x0008F705
		private void ᜀ(ExcelColors A_0, int A_1, int A_2, int A_3, int A_4)
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
			this.ᜀ(A_0, A_1, A_2, A_3, A_4, ExcelColors.Black, ExcelPatternType.Solid);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00090744 File Offset: 0x0008F744
		private void ᜀ(ExcelColors A_0, int A_1, int A_2, int A_3, int A_4, ExcelColors A_5, ExcelPatternType A_6)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.Worksheet);
					int num = A_1;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_6D;
						case 1:
							goto IL_BD;
						case 2:
							num++;
							num2 = 0;
							continue;
						case 3:
							return;
						case 4:
							goto IL_6F;
						case 5:
						{
							if (num > A_2)
							{
								num2 = 3;
								continue;
							}
							if (true)
							{
							}
							int num3 = A_3;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6D;
							default:
								if (false)
								{
								}
								num2 = 7;
								continue;
							}
							break;
						}
						case 6:
						{
							int num3;
							if (num3 > A_4)
							{
								num2 = 2;
								continue;
							}
							spr_u24F.ᜀ(num, num3);
							IStyle style = spr_u24F.Style;
							style.FillPattern = A_6;
							style.KnownColor = A_0;
							style.PatternKnownColor = A_5;
							num3++;
							num2 = 4;
							continue;
						}
						case 7:
							goto IL_6F;
						}
						break;
						IL_6F:
						num2 = 6;
						continue;
						IL_BD:
						num2 = 5;
						continue;
						IL_6D:
						goto IL_BD;
					}
				}
				return;
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00090868 File Offset: 0x0008F868
		private void ᜃ(AutoFormatType A_0)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int firstRow;
				int lastRow;
				int firstColumn;
				int lastColumn;
				ExcelColors excelColors;
				ExcelColors excelColors2;
				for (;;)
				{
					firstRow = this.FirstRow;
					lastRow = this.LastRow;
					firstColumn = this.FirstColumn;
					lastColumn = this.LastColumn;
					excelColors = (ExcelColors)65;
					excelColors2 = ExcelColors.BlackCustom;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1CD;
						case 1:
							num = 0;
							continue;
						case 2:
							if (firstColumn != lastColumn)
							{
								num = 3;
								continue;
							}
							num = 6;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_75;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 4:
							goto IL_1EA;
						case 5:
							goto IL_75;
						case 6:
							goto IL_DF;
						}
						break;
						IL_75:
						switch (A_0)
						{
						case AutoFormatType.Classic_2:
							goto IL_11A;
						case AutoFormatType.Classic_3:
							goto IL_E2;
						case AutoFormatType.Accounting1:
						case AutoFormatType.Accounting2:
						case AutoFormatType.Accounting3:
						case AutoFormatType.Accounting4:
						case AutoFormatType.List3:
							goto IL_21C;
						case AutoFormatType.Colorful1:
							goto IL_1F2;
						case AutoFormatType.Colorful2:
							num = 2;
							break;
						case AutoFormatType.Colorful3:
							goto IL_C7;
						case AutoFormatType.List1:
							goto IL_1D2;
						case AutoFormatType.List2:
							goto IL_10E;
						case AutoFormatType.Effect3D1:
						case AutoFormatType.Effect3D2:
							goto IL_177;
						default:
							num = 1;
							break;
						}
					}
				}
				IL_C7:
				this.ᜀ(ExcelColors.Black, firstRow, lastRow, firstColumn, lastColumn);
				return;
				IL_DF:
				int num2 = lastColumn;
				goto IL_148;
				IL_E2:
				this.ᜀ(excelColors, lastRow, lastRow, firstColumn, lastColumn, excelColors2, ExcelPatternType.None);
				this.ᜀ(ExcelColors.Gray25Percent, firstRow + 1, lastRow - 1, firstColumn, lastColumn);
				this.ᜀ(ExcelColors.DarkBlue, firstRow, firstRow, firstColumn, lastColumn);
				return;
				IL_10E:
				this.ᜀ(false, excelColors, excelColors2);
				return;
				IL_11A:
				this.ᜀ(excelColors, firstRow + 1, lastRow, firstColumn + 1, lastColumn, excelColors2, ExcelPatternType.None);
				this.ᜀ(ExcelColors.Gray25Percent, firstRow + 1, lastRow, firstColumn, firstColumn);
				this.ᜀ(ExcelColors.Violet, firstRow, firstRow, firstColumn, lastColumn);
				return;
				IL_148:
				int a_ = num2;
				this.ᜀ(ExcelColors.Gray25Percent, firstRow + 1, lastRow, lastColumn, lastColumn);
				this.ᜀ(ExcelColors.YellowCustom, firstRow + 1, lastRow, firstColumn, a_, ExcelColors.WhiteCustom, ExcelPatternType.Percent70);
				this.ᜀ(ExcelColors.DarkRed, firstRow, firstRow, firstColumn, lastColumn);
				return;
				IL_177:
				this.ᜀ(ExcelColors.Gray25Percent, firstRow, lastRow, firstColumn, lastColumn);
				return;
				IL_1CD:
				goto IL_21C;
				IL_1D2:
				this.ᜀ(true, excelColors, excelColors2);
				return;
				IL_1EA:
				num2 = lastColumn - 1;
				goto IL_148;
				IL_1F2:
				this.ᜀ(ExcelColors.DarkBlue, firstRow + 1, lastRow, firstColumn, lastColumn);
				this.ᜀ(ExcelColors.Teal, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
				this.ᜀ(ExcelColors.Black, firstRow, firstRow, firstColumn, lastColumn);
				return;
				IL_21C:
				this.ᜀ(excelColors, firstRow, lastRow, firstColumn, lastColumn, excelColors2, ExcelPatternType.None);
				return;
			}
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00090AA0 File Offset: 0x0008FAA0
		private void ᜀ(bool A_0, ExcelColors A_1, ExcelColors A_2)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int firstRow;
				int firstColumn;
				int lastColumn;
				ExcelColors a_;
				for (;;)
				{
					firstRow = this.FirstRow;
					int lastRow = this.LastRow;
					firstColumn = this.FirstColumn;
					lastColumn = this.LastColumn;
					this.ᜀ(A_1, lastRow, lastRow, firstColumn, lastColumn, A_2, ExcelPatternType.None);
					int num = 12;
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						ExcelColors excelColors;
						int num5;
						switch (num)
						{
						case 0:
							if (A_0)
							{
								num = 6;
								continue;
							}
							goto IL_22A;
						case 1:
							if (num2 >= num3)
							{
								num = 5;
								continue;
							}
							num = 17;
							continue;
						case 2:
							num4 = 4;
							goto IL_16E;
						case 3:
							if (!A_0)
							{
								num = 16;
								continue;
							}
							num = 7;
							continue;
						case 4:
							goto IL_213;
						case 5:
							num = 0;
							continue;
						case 6:
							goto IL_1EC;
						case 7:
							num4 = 2;
							goto IL_16E;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_22A;
							default:
								if (false)
								{
								}
								num = 14;
								continue;
							}
							break;
						case 9:
							goto IL_213;
						case 10:
							this.ᜀ(a_, num2 + firstRow + 1, num2 + firstRow + 1, firstColumn, lastColumn);
							num = 9;
							continue;
						case 11:
							excelColors = ExcelColors.Gray25Percent;
							goto IL_1A5;
						case 12:
							if (!A_0)
							{
								num = 8;
								continue;
							}
							num = 11;
							continue;
						case 13:
							goto IL_14E;
						case 14:
							excelColors = ExcelColors.LightGreen;
							goto IL_1A5;
						case 15:
							goto IL_14E;
						case 16:
							num = 2;
							continue;
						case 17:
							if (num2 % num5 < num5 / 2)
							{
								num = 10;
								continue;
							}
							this.ᜀ(A_1, num2 + firstRow + 1, num2 + firstRow + 1, firstColumn, lastColumn, A_2, ExcelPatternType.None);
							num = 4;
							continue;
						}
						break;
						IL_14E:
						num = 1;
						continue;
						IL_16E:
						num5 = num4;
						num2 = 0;
						num3 = lastRow - firstRow - 1;
						num = 13;
						continue;
						IL_1A5:
						a_ = excelColors;
						num = 3;
						continue;
						IL_213:
						num2++;
						num = 15;
					}
				}
				IL_1EC:
				this.ᜀ(a_, firstRow, firstRow, firstColumn, lastColumn);
				return;
				IL_22A:
				this.ᜀ(ExcelColors.Green, firstRow, firstRow, firstColumn, lastColumn, ExcelColors.Teal, ExcelPatternType.Percent70);
				return;
			}
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00090CE8 File Offset: 0x0008FCE8
		private void ᜂ(AutoFormatType A_0)
		{
			switch (0)
			{
			default:
			{
				int firstRow;
				int lastRow;
				int firstColumn;
				int lastColumn;
				HorizontalAlignType a_;
				for (;;)
				{
					IL_0E:
					for (;;)
					{
						IL_5F:
						firstRow = this.FirstRow;
						lastRow = this.LastRow;
						firstColumn = this.FirstColumn;
						lastColumn = this.LastColumn;
						int num = 6;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							default:
								if (false)
								{
								}
								switch (num)
								{
								case 0:
									a_ = HorizontalAlignType.General;
									num = 1;
									continue;
								case 1:
									goto IL_11F;
								case 2:
									goto IL_A8;
								case 3:
									goto IL_95;
								case 4:
									this.ᜀ(HorizontalAlignType.Left, lastRow, lastRow, firstColumn, firstColumn);
									num = 2;
									continue;
								case 5:
									if (firstRow != lastRow)
									{
										num = 4;
										continue;
									}
									goto IL_A8;
								case 6:
									if (A_0 == AutoFormatType.None)
									{
										num = 3;
										continue;
									}
									a_ = HorizontalAlignType.Left;
									this.ᜀ(HorizontalAlignType.General, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
									this.ᜀ(HorizontalAlignType.General, firstRow, firstRow, firstColumn, firstColumn);
									num = 5;
									continue;
								case 7:
									if (Array.IndexOf<AutoFormatType>(XlsRange.\u1719, A_0) != -1)
									{
										num = 9;
										continue;
									}
									goto IL_17E;
								case 8:
									goto IL_111;
								case 9:
									if (true)
									{
									}
									a_ = HorizontalAlignType.Right;
									num = 8;
									continue;
								case 10:
									if (A_0 == AutoFormatType.List3)
									{
										num = 0;
										continue;
									}
									goto IL_11F;
								}
								goto IL_5F;
								IL_A8:
								num = 10;
								break;
								IL_11F:
								this.ᜀ(a_, firstRow + 1, lastRow - 1, firstColumn, firstColumn);
								a_ = HorizontalAlignType.Center;
								num = 7;
								break;
							}
						}
					}
				}
				IL_95:
				this.ᜀ(HorizontalAlignType.General, firstRow, lastRow, firstColumn, lastColumn);
				return;
				IL_111:
				IL_17E:
				this.ᜀ(a_, firstRow, firstRow, firstColumn + 1, lastColumn);
				return;
			}
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00090E8C File Offset: 0x0008FE8C
		private void ᜀ(HorizontalAlignType A_0, int A_1, int A_2, int A_3, int A_4)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.Worksheet);
					int num = A_1;
					int num2;
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_DE:
						spr_u24F.ᜀ(num, num2);
						IStyle style = spr_u24F.Style;
						style.HorizontalAlignment = A_0;
						style.VerticalAlignment = VerticalAlignType.Bottom;
						style.Rotation = 0;
						style.IndentLevel = 0;
						num2++;
						num3 = 1;
						break;
					}
					default:
						if (false)
						{
						}
						num3 = 2;
						break;
					}
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num > A_2)
							{
								num3 = 5;
								continue;
							}
							num2 = A_3;
							num3 = 3;
							continue;
						case 1:
							goto IL_89;
						case 2:
							goto IL_C0;
						case 3:
							goto IL_89;
						case 4:
							num++;
							num3 = 6;
							continue;
						case 5:
							return;
						case 6:
							goto IL_C0;
						case 7:
							if (num2 > A_4)
							{
								num3 = 4;
								continue;
							}
							goto IL_DE;
						}
						break;
						IL_89:
						num3 = 7;
						continue;
						IL_C0:
						num3 = 0;
					}
				}
				return;
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00090FB8 File Offset: 0x0008FFB8
		private void ᜁ(AutoFormatType A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_0E:
					int num = 0;
					for (;;)
					{
						int num3;
						int lastRow;
						switch (num)
						{
						case 1:
						{
							int num2;
							int lastColumn;
							if (num2 > lastColumn)
							{
								num = 3;
								continue;
							}
							((XlsWorksheet)this.Worksheet).AutoFitColumn(num2);
							num2++;
							num = 8;
							continue;
						}
						case 2:
						{
							if (true)
							{
							}
							int num2 = this.FirstColumn;
							int lastColumn = this.LastColumn;
							num = 7;
							continue;
						}
						case 3:
							return;
						case 4:
							return;
						case 5:
							goto IL_86;
						case 6:
							goto IL_86;
						case 7:
							goto IL_BD;
						case 8:
							goto IL_BD;
						case 9:
							if (num3 > lastRow)
							{
								num = 2;
								continue;
							}
							((XlsWorksheet)this.Worksheet).AutoFitRow(num3);
							num3++;
							num = 5;
							continue;
						}
						if (A_0 == AutoFormatType.None)
						{
							num = 4;
							continue;
						}
						num3 = this.FirstRow;
						lastRow = this.LastRow;
						num = 6;
						continue;
						IL_86:
						num = 9;
						continue;
						IL_BD:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							num = 1;
							break;
						}
					}
				}
				return;
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00091108 File Offset: 0x00090108
		private void ᜀ(AutoFormatType A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = A_0 == AutoFormatType.None;
					int num = 9;
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						spr\u24F1 spr_u24F;
						IStyle style;
						int lastRow;
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C4;
							default:
								if (false)
								{
								}
								num2 = 44;
								goto IL_10A;
							}
							break;
						case 2:
							goto IL_184;
						case 3:
							num = 15;
							continue;
						case 4:
							if (!flag)
							{
								num = 14;
								continue;
							}
							goto IL_83;
						case 5:
							goto IL_11C;
						case 6:
							num = 17;
							continue;
						case 7:
							num3++;
							num = 5;
							continue;
						case 8:
						{
							int lastColumn;
							if (num4 > lastColumn)
							{
								num = 7;
								continue;
							}
							spr_u24F.ᜀ(num3, num4);
							style = spr_u24F.Style;
							num = 4;
							continue;
						}
						case 9:
							if (!flag)
							{
								num = 6;
								continue;
							}
							goto IL_C4;
						case 10:
						{
							if (num3 > lastRow)
							{
								num = 0;
								continue;
							}
							num4 = this.Column;
							int lastColumn = this.LastColumn;
							num = 2;
							continue;
						}
						case 11:
							goto IL_83;
						case 12:
							return;
						case 13:
							goto IL_184;
						case 14:
							num = 16;
							continue;
						case 15:
							if (true)
							{
							}
							num2 = 43;
							goto IL_10A;
						case 16:
							if (num3 != this.Row + 1)
							{
								num = 3;
								continue;
							}
							num = 1;
							continue;
						case 17:
							if (Array.IndexOf<AutoFormatType>(XlsRange.\u171A, A_0) == -1)
							{
								num = 12;
								continue;
							}
							goto IL_C4;
						case 18:
							goto IL_11C;
						}
						break;
						IL_83:
						int numberFormatIndex;
						style.NumberFormatIndex = numberFormatIndex;
						num4++;
						num = 13;
						continue;
						IL_C4:
						spr_u24F = new spr\u24F1(this.Application, this.\u171D);
						numberFormatIndex = 0;
						num3 = this.Row + 1;
						lastRow = this.LastRow;
						num = 18;
						continue;
						IL_10A:
						numberFormatIndex = num2;
						num = 11;
						continue;
						IL_11C:
						num = 10;
						continue;
						IL_184:
						num = 8;
					}
				}
				return;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00091350 File Offset: 0x00090350
		private void ᜀ(AutoFormatType A_0, bool A_1, bool A_2)
		{
			int a_ = 14;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DD;
				case 1:
					goto IL_117;
				case 2:
					num = 1;
					continue;
				case 4:
					return;
				case 5:
					switch (A_0)
					{
					case AutoFormatType.Simple:
						goto IL_C9;
					case AutoFormatType.Classic1:
						goto IL_103;
					case AutoFormatType.Classic_2:
						goto IL_4E;
					case AutoFormatType.Classic_3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DD;
						default:
							goto IL_B1;
						}
						break;
					case AutoFormatType.Accounting1:
						goto IL_45;
					case AutoFormatType.Accounting2:
						goto IL_FA;
					case AutoFormatType.Accounting3:
						goto IL_91;
					case AutoFormatType.Accounting4:
						goto IL_C0;
					default:
						num = 2;
						continue;
					}
					break;
				case 6:
					num = 0;
					continue;
				}
				if (!A_1)
				{
					num = 6;
					continue;
				}
				IL_57:
				num = 5;
				continue;
				IL_DD:
				if (A_2)
				{
					goto IL_57;
				}
				num = 4;
			}
			IL_45:
			this.ᜃ(A_1, A_2);
			return;
			IL_4E:
			this.ᜅ(A_1, A_2);
			return;
			IL_91:
			this.ᜁ(A_1, A_2);
			return;
			IL_B1:
			if (false)
			{
			}
			this.ᜄ(A_1, A_2);
			return;
			IL_C0:
			this.ᜀ(A_1, A_2);
			return;
			IL_C9:
			this.ᜇ(A_1, A_2);
			return;
			IL_FA:
			this.ᜂ(A_1, A_2);
			return;
			IL_103:
			this.ᜆ(A_1, A_2);
			return;
			IL_117:
			if (true)
			{
			}
			throw new NotSupportedException(RecordTableEnumerator.b("ᅃ⡅⍇⑉⍋㥍㹏牑㕓⍕ⱗ㕙籛㡝ཟၡॣݥᱧ䩩ᡫ᝭o᝱婳", a_));
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00091494 File Offset: 0x00090494
		private void ᜇ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					bool flag;
					int lastRow;
					int num2;
					XlsFont xlsFont;
					int firstColumn;
					int firstRow;
					int lastColumn;
					switch (num)
					{
					case 0:
						if (!flag)
						{
							num = 8;
							continue;
						}
						num = 1;
						continue;
					case 1:
						num2 = lastRow;
						goto IL_CF;
					case 2:
						goto IL_AA;
					case 3:
						if (A_0)
						{
							num = 11;
							continue;
						}
						return;
					case 4:
						return;
					case 6:
						if (!A_1)
						{
							num = 4;
							continue;
						}
						goto IL_1A8;
					case 7:
						num = 6;
						continue;
					case 8:
						num = 10;
						continue;
					case 9:
						this.ᜀ(xlsFont, lastRow, lastRow, firstColumn, firstColumn);
						num = 2;
						continue;
					case 10:
						num2 = lastRow - 1;
						goto IL_CF;
					case 11:
						xlsFont = (XlsFont)this.m_book.InnerFonts[0];
						xlsFont = xlsFont.Clone(this.m_book.InnerFonts);
						this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
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
						break;
					case 12:
						if (!flag)
						{
							num = 9;
							continue;
						}
						return;
					}
					IL_5E:
					if (true)
					{
					}
					if (!A_0)
					{
						num = 7;
						continue;
					}
					goto IL_1A8;
					goto IL_5E;
					IL_CF:
					int a_ = num2;
					this.ᜀ(xlsFont, firstRow, a_, firstColumn, firstColumn);
					xlsFont = xlsFont.Clone(this.m_book.InnerFonts);
					xlsFont.IsBold = true;
					this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn);
					num = 12;
					continue;
					IL_1A8:
					firstRow = this.FirstRow;
					lastRow = this.LastRow;
					firstColumn = this.FirstColumn;
					lastColumn = this.LastColumn;
					flag = (firstRow == lastRow);
					num = 3;
				}
				IL_AA:
				return;
			}
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00091690 File Offset: 0x00090690
		private void ᜆ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					int firstColumn;
					int lastColumn;
					XlsFont xlsFont;
					int firstRow;
					int lastRow;
					int num2;
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_192;
					case 1:
						num = 2;
						continue;
					case 2:
						if (!A_1)
						{
							num = 4;
							continue;
						}
						goto IL_152;
					case 3:
						goto IL_FC;
					case 4:
						return;
					case 5:
						if (firstColumn != lastColumn)
						{
							num = 6;
							continue;
						}
						goto IL_FC;
					case 6:
						if (true)
						{
						}
						this.ᜀ(xlsFont, firstRow, firstRow, lastColumn, lastColumn);
						num = 3;
						continue;
					case 7:
						return;
					case 8:
						goto IL_DE;
					case 9:
						num2 = lastRow;
						goto IL_194;
					case 10:
						num2 = lastRow - 1;
						goto IL_194;
					case 11:
						if (flag)
						{
							num = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_192;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 12:
						this.ᜀ(xlsFont, lastRow, lastRow, firstColumn, firstColumn);
						num = 8;
						continue;
					case 14:
						num = 10;
						continue;
					case 15:
						if (!flag)
						{
							num = 12;
							continue;
						}
						goto IL_DE;
					case 16:
						if (A_0)
						{
							num = 0;
							continue;
						}
						return;
					}
					if (!A_0)
					{
						num = 1;
						continue;
					}
					goto IL_152;
					IL_DE:
					num = 5;
					continue;
					IL_FC:
					xlsFont = xlsFont.Clone(this.m_book.InnerFonts);
					xlsFont.IsBold = false;
					xlsFont.IsItalic = true;
					this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn - 1);
					num = 7;
					continue;
					IL_152:
					firstRow = this.FirstRow;
					lastRow = this.LastRow;
					firstColumn = this.FirstColumn;
					lastColumn = this.LastColumn;
					flag = (firstRow == lastRow);
					num = 16;
					continue;
					IL_192:
					xlsFont = (XlsFont)this.m_book.InnerFonts[0];
					xlsFont = xlsFont.Clone(this.m_book.InnerFonts);
					this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
					num = 11;
					continue;
					IL_194:
					int a_ = num2;
					this.ᜀ(xlsFont, firstRow, a_, firstColumn, firstColumn);
					xlsFont = xlsFont.Clone(this.m_book.InnerFonts);
					xlsFont.IsBold = true;
					this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn);
					num = 15;
				}
				return;
			}
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00091914 File Offset: 0x00090914
		private void ᜅ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 11;
				for (;;)
				{
					XlsFont xlsFont;
					int firstColumn;
					XlsFontsCollection innerFonts;
					int firstRow;
					int lastColumn;
					switch (num)
					{
					case 0:
						if (A_0)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
					{
						int lastRow;
						this.ᜀ(xlsFont, lastRow, lastRow, firstColumn, firstColumn);
						num = 6;
						continue;
					}
					case 2:
						num = 9;
						continue;
					case 3:
						goto IL_115;
					case 4:
					{
						xlsFont = (XlsFont)innerFonts[0];
						xlsFont = xlsFont.Clone(innerFonts);
						int lastRow;
						this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn, firstColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.IsBold = true;
						this.ᜀ(xlsFont, firstRow + 1, lastRow - 1, firstColumn, firstColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.KnownColor = ExcelColors.DarkBlue;
						num = 7;
						continue;
					}
					case 5:
						goto IL_137;
					case 6:
						goto IL_159;
					case 7:
					{
						int lastRow;
						if (firstRow != lastRow)
						{
							num = 1;
							continue;
						}
						goto IL_159;
					}
					case 8:
						return;
					case 9:
						if (!A_1)
						{
							num = 8;
							continue;
						}
						goto IL_1C8;
					case 10:
						if (firstColumn != lastColumn)
						{
							num = 3;
							continue;
						}
						return;
					}
					if (!A_0)
					{
						num = 2;
						continue;
					}
					goto IL_1C8;
					IL_115:
					this.ᜀ(xlsFont, firstRow, firstRow, lastColumn, lastColumn);
					num = 5;
					continue;
					IL_1C8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_115;
					default:
					{
						if (false)
						{
						}
						firstRow = this.FirstRow;
						int lastRow = this.LastRow;
						firstColumn = this.FirstColumn;
						lastColumn = this.LastColumn;
						innerFonts = this.m_book.InnerFonts;
						num = 0;
						continue;
					}
					}
					IL_159:
					xlsFont = xlsFont.Clone(innerFonts);
					xlsFont.IsBold = false;
					xlsFont.Size = 9.0;
					xlsFont.KnownColor = ExcelColors.White;
					this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn - 1);
					xlsFont = xlsFont.Clone(innerFonts);
					xlsFont.IsBold = true;
					num = 10;
				}
				IL_137:
				return;
			}
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00091B54 File Offset: 0x00090B54
		private void ᜄ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					XlsFontsCollection innerFonts;
					int firstRow;
					int lastRow;
					int firstColumn;
					int lastColumn;
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (!A_1)
						{
							num = 2;
							continue;
						}
						goto IL_4A;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_AD;
						}
						break;
					case 4:
					{
						XlsFont xlsFont = (XlsFont)innerFonts[0];
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.KnownColor = ExcelColors.DarkBlue;
						this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn, firstColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.IsBold = true;
						xlsFont.KnownColor = ExcelColors.Black;
						this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn, firstColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.KnownColor = ExcelColors.White;
						xlsFont.IsItalic = true;
						xlsFont.Size = 9.0;
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn);
						num = 0;
						continue;
					}
					case 5:
						if (A_0)
						{
							num = 4;
							continue;
						}
						return;
					case 6:
						num = 1;
						continue;
					}
					IL_3C:
					if (!A_0)
					{
						num = 6;
						continue;
					}
					goto IL_4A;
					goto IL_3C;
					IL_4A:
					firstRow = this.FirstRow;
					lastRow = this.LastRow;
					firstColumn = this.FirstColumn;
					lastColumn = this.LastColumn;
					innerFonts = this.m_book.InnerFonts;
					num = 5;
				}
				IL_AD:
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00091CEC File Offset: 0x00090CEC
		private void ᜃ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int firstRow;
					int firstColumn;
					int lastColumn;
					XlsFontsCollection innerFonts;
					XlsFont xlsFont;
					switch (num)
					{
					case 0:
						if (A_0)
						{
							num = 10;
							continue;
						}
						return;
					case 1:
						goto IL_F1;
					case 2:
					{
						int lastRow;
						if (firstRow != lastRow)
						{
							num = 11;
							continue;
						}
						goto IL_68;
					}
					case 3:
						num = 12;
						continue;
					case 4:
						if (firstColumn != lastColumn)
						{
							num = 7;
							continue;
						}
						goto IL_C1;
					case 5:
						return;
					case 6:
						goto IL_68;
					case 7:
						goto IL_116;
					case 9:
						if (true)
						{
						}
						goto IL_C1;
					case 10:
					{
						xlsFont = (XlsFont)innerFonts[0];
						xlsFont = xlsFont.Clone(innerFonts);
						int lastRow;
						this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn, firstColumn);
						this.ᜀ(xlsFont, firstRow + 1, lastRow - 1, firstColumn, firstColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.IsItalic = true;
						num = 2;
						continue;
					}
					case 11:
					{
						int lastRow;
						this.ᜀ(xlsFont, lastRow, lastRow, firstColumn, firstColumn);
						num = 6;
						continue;
					}
					case 12:
						if (!A_1)
						{
							num = 5;
							continue;
						}
						goto IL_1B5;
					}
					if (!A_0)
					{
						num = 3;
						continue;
					}
					goto IL_1B5;
					IL_68:
					xlsFont = xlsFont.Clone(innerFonts);
					xlsFont.IsBold = true;
					xlsFont.Size = 9.0;
					num = 4;
					continue;
					IL_C1:
					xlsFont = xlsFont.Clone(innerFonts);
					xlsFont.KnownColor = ExcelColors.Gray50Percent;
					this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn - 1);
					num = 1;
					continue;
					IL_116:
					this.ᜀ(xlsFont, firstRow, firstRow, lastColumn, lastColumn);
					num = 9;
					continue;
					IL_1B5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_116;
					default:
					{
						if (false)
						{
						}
						firstRow = this.FirstRow;
						int lastRow = this.LastRow;
						firstColumn = this.FirstColumn;
						lastColumn = this.LastColumn;
						innerFonts = this.m_book.InnerFonts;
						num = 0;
						break;
					}
					}
				}
				IL_F1:
				return;
			}
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00091F20 File Offset: 0x00090F20
		private void ᜂ(bool A_0, bool A_1)
		{
			int num = 4;
			for (;;)
			{
				XlsFontsCollection innerFonts;
				switch (num)
				{
				case 0:
					if (!A_1)
					{
						num = 1;
						continue;
					}
					goto IL_41;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						goto IL_7B;
					}
					break;
				case 2:
					return;
				case 3:
				{
					XlsFont xlsFont = (XlsFont)innerFonts[0];
					xlsFont = xlsFont.Clone(innerFonts);
					this.ᜀ(xlsFont, this.Row, this.LastRow, this.Column, this.LastColumn);
					num = 2;
					continue;
				}
				case 5:
					goto IL_8C;
				case 6:
					if (A_0)
					{
						num = 3;
						continue;
					}
					return;
				}
				if (!A_0)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				IL_41:
				innerFonts = this.m_book.InnerFonts;
				num = 6;
				continue;
				IL_8C:
				num = 0;
			}
			IL_7B:
			if (false)
			{
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00092018 File Offset: 0x00091018
		private void ᜁ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int firstColumn;
					XlsFont xlsFont;
					XlsFontsCollection innerFonts;
					int lastRow;
					switch (num)
					{
					case 0:
					{
						int lastColumn;
						if (firstColumn != lastColumn)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_18B;
					}
					case 1:
					{
						int lastColumn;
						int firstRow;
						this.ᜀ(xlsFont, firstRow, firstRow, lastColumn, lastColumn);
						num = 5;
						continue;
					}
					case 2:
						if (!A_1)
						{
							num = 10;
							continue;
						}
						goto IL_1D7;
					case 4:
						if (A_0)
						{
							num = 7;
							continue;
						}
						return;
					case 5:
						goto IL_18B;
					case 6:
						goto IL_147;
					case 7:
					{
						xlsFont = (XlsFont)innerFonts[0];
						xlsFont = xlsFont.Clone(innerFonts);
						int firstRow;
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn, firstColumn);
						int lastColumn;
						this.ᜀ(xlsFont, firstRow + 1, lastRow, firstColumn + 1, lastColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.IsItalic = true;
						this.ᜀ(xlsFont, firstRow + 1, lastRow - 1, firstColumn, firstColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.Size = 9.0;
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn - 1);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.IsBold = true;
						xlsFont.IsItalic = true;
						num = 0;
						continue;
					}
					case 8:
						num = 2;
						continue;
					case 9:
					{
						int firstRow;
						if (firstRow != lastRow)
						{
							num = 6;
							continue;
						}
						return;
					}
					case 10:
						return;
					case 11:
						goto IL_169;
					}
					if (!A_0)
					{
						num = 8;
						continue;
					}
					goto IL_1D7;
					IL_147:
					this.ᜀ(xlsFont, lastRow, lastRow, firstColumn, firstColumn);
					num = 11;
					continue;
					IL_1D7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_147;
					default:
					{
						if (false)
						{
						}
						int firstRow = this.FirstRow;
						lastRow = this.LastRow;
						firstColumn = this.FirstColumn;
						int lastColumn = this.LastColumn;
						innerFonts = this.m_book.InnerFonts;
						num = 4;
						continue;
					}
					}
					IL_18B:
					xlsFont = xlsFont.Clone(innerFonts);
					xlsFont.IsBold = true;
					xlsFont.IsItalic = false;
					xlsFont.Size = 10.0;
					num = 9;
				}
				IL_169:
				return;
			}
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00092264 File Offset: 0x00091264
		private void ᜀ(bool A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 11;
				for (;;)
				{
					int lastRow;
					XlsFontsCollection innerFonts;
					XlsFont xlsFont;
					int firstColumn;
					int lastColumn;
					switch (num)
					{
					case 0:
					{
						int firstRow;
						if (firstRow != lastRow)
						{
							num = 1;
							continue;
						}
						return;
					}
					case 1:
						goto IL_10A;
					case 2:
						goto IL_150;
					case 3:
						if (A_0)
						{
							num = 5;
							continue;
						}
						return;
					case 4:
						goto IL_12E;
					case 5:
					{
						if (true)
						{
						}
						xlsFont = (XlsFont)innerFonts[0];
						xlsFont = xlsFont.Clone(innerFonts);
						int firstRow;
						this.ᜀ(xlsFont, firstRow, lastRow, firstColumn, firstColumn);
						this.ᜀ(xlsFont, firstRow + 1, lastRow - 2, firstColumn + 1, lastColumn);
						xlsFont = xlsFont.Clone(innerFonts);
						xlsFont.Underline = FontUnderlineType.SingleAccounting;
						this.ᜀ(xlsFont, firstRow, firstRow, firstColumn + 1, lastColumn);
						num = 9;
						continue;
					}
					case 6:
						this.ᜀ(xlsFont, lastRow - 1, lastRow - 1, firstColumn + 1, lastColumn);
						num = 2;
						continue;
					case 7:
						num = 8;
						continue;
					case 8:
						if (!A_1)
						{
							num = 10;
							continue;
						}
						goto IL_182;
					case 9:
					{
						int firstRow;
						if (lastRow - firstRow > 1)
						{
							num = 6;
							continue;
						}
						goto IL_150;
					}
					case 10:
						return;
					}
					if (!A_0)
					{
						num = 7;
						continue;
					}
					goto IL_182;
					IL_10A:
					this.ᜀ(xlsFont, lastRow, lastRow, firstColumn + 1, lastColumn);
					num = 4;
					continue;
					IL_182:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10A;
					default:
					{
						if (false)
						{
						}
						int firstRow = this.FirstRow;
						lastRow = this.LastRow;
						firstColumn = this.FirstColumn;
						lastColumn = this.LastColumn;
						innerFonts = this.m_book.InnerFonts;
						num = 3;
						continue;
					}
					}
					IL_150:
					xlsFont = xlsFont.Clone(innerFonts);
					xlsFont.Underline = FontUnderlineType.DoubleAccounting;
					num = 0;
				}
				IL_12E:
				return;
			}
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0009245C File Offset: 0x0009145C
		private void ᜀ(IFont A_0, int A_1, int A_2, int A_3, int A_4)
		{
			int a_ = 5;
			switch (0)
			{
			default:
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
							if (false)
							{
							}
							break;
						}
						num = 7;
						continue;
					case 2:
						goto IL_130;
					case 3:
					{
						int num2;
						if (num2 > A_4)
						{
							num = 11;
							continue;
						}
						spr\u24F1 spr_u24F;
						int num3;
						spr_u24F.ᜀ(num3, num2);
						AddtionalFormatWrapper addtionalFormatWrapper = (AddtionalFormatWrapper)spr_u24F.Style;
						int fontIndex;
						addtionalFormatWrapper.FontIndex = fontIndex;
						num2++;
						num = 10;
						continue;
					}
					case 4:
						goto IL_17B;
					case 5:
						goto IL_6D;
					case 6:
						if (A_1 <= A_2)
						{
							num = 1;
							continue;
						}
						return;
					case 7:
					{
						if (true)
						{
						}
						if (A_3 > A_4)
						{
							num = 4;
							continue;
						}
						spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.Worksheet);
						spr_u24F.ᜀ(A_1, A_3);
						IFont font = spr_u24F.Style.Font;
						font.BeginUpdate();
						font.IsBold = A_0.IsBold;
						font.Color = A_0.Color;
						font.FontName = A_0.FontName;
						font.IsItalic = A_0.IsItalic;
						font.Size = A_0.Size;
						font.IsStrikethrough = A_0.IsStrikethrough;
						font.IsSubscript = A_0.IsSubscript;
						font.IsSuperscript = A_0.IsSuperscript;
						font.IsStrikethrough = A_0.IsStrikethrough;
						font.Underline = A_0.Underline;
						font.EndUpdate();
						int fontIndex = spr_u24F.m_style.FontIndex;
						int num3 = A_1;
						num = 12;
						continue;
					}
					case 8:
						return;
					case 9:
					{
						int num3;
						if (num3 > A_2)
						{
							num = 8;
							continue;
						}
						int num2 = A_3;
						num = 13;
						continue;
					}
					case 10:
						goto IL_6F;
					case 11:
					{
						int num3;
						num3++;
						num = 2;
						continue;
					}
					case 12:
						goto IL_130;
					case 13:
						goto IL_6F;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 6;
					continue;
					IL_6F:
					num = 3;
					continue;
					IL_130:
					num = 9;
				}
				IL_6D:
				throw new ArgumentNullException(RecordTableEnumerator.b("崺刼儾㕀", a_));
				IL_17B:
				return;
			}
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000926BC File Offset: 0x000916BC
		internal XlsRange(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 7;
			this.ᜣ = new char[]
			{
				'_',
				'?',
				'*'
			};
			this.ᜤ = new string[]
			{
				RecordTableEnumerator.b("昼ᬾ汀Ղ組睆祈ᙊ⥌⭎㕐㝒॔策՘筚ぜ㉞ౠ๢㥤䝦൨ཪㅬ䍮⵰卲౴๶xɺ", a_),
				RecordTableEnumerator.b("值ှ╀求㱄㹆え㉊", a_)
			};
			this.ᜥ = new string[]
			{
				RecordTableEnumerator.b("夼娾汀ɂᅄ", a_),
				RecordTableEnumerator.b("夼娾汀݂D", a_),
				RecordTableEnumerator.b("夼娾汀Bൄ", a_),
				RecordTableEnumerator.b("夼娾汀གౄ", a_),
				RecordTableEnumerator.b("夼娾汀ག၄", a_)
			};
			base..ctor();
			this.ᜁ(A_1);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0009278C File Offset: 0x0009178C
		internal XlsRange(spr\u1DF5 A_0, object A_1, sprἛ A_2) : this(A_0, A_1)
		{
			this.ᜀ(A_2);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000927A8 File Offset: 0x000917A8
		internal XlsRange(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, int A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000927C8 File Offset: 0x000917C8
		internal XlsRange(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, ref int A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000927E8 File Offset: 0x000917E8
		internal XlsRange(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, ref int A_3, bool A_4) : this(A_0, A_1)
		{
			this.SerializeDataToList(A_2, ref A_3, A_4);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00092808 File Offset: 0x00091808
		internal XlsRange(spr\u1DF5 A_0, object A_1, List<BiffRecordRaw> A_2, ref int A_3, bool A_4) : this(A_0, A_1)
		{
			this.SerializeDataToList(A_2, ref A_3, A_4);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00092828 File Offset: 0x00091828
		internal XlsRange(spr\u1DF5 A_0, object A_1, int A_2, int A_3, int A_4, int A_5)
		{
			int a_ = 2;
			this..ctor(A_0, A_1);
			if (A_2 > A_4)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帷匹主䴽㐿Ł⭃⩅桇╉㹋湍㱏㍑❓≕᭗㕙せ", a_));
			}
			if (A_3 > A_5)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帷匹主䴽㐿၁⭃ㅅ桇╉㹋湍㱏㍑❓≕੗㕙⭛", a_));
			}
			this.FirstColumn = A_2;
			this.FirstRow = A_3;
			this.LastColumn = A_4;
			this.LastRow = A_5;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0009289C File Offset: 0x0009189C
		internal XlsRange(spr\u1DF5 A_0, object A_1, int A_2, int A_3) : this(A_0, A_1)
		{
			this.FirstColumn = A_2;
			this.LastColumn = A_2;
			this.FirstRow = A_3;
			this.LastRow = A_3;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000928D0 File Offset: 0x000918D0
		internal XlsRange(spr\u1DF5 A_0, object A_1, BiffRecordRaw A_2, bool A_3) : this(A_0, A_1, new BiffRecordRaw[]
		{
			A_2
		}, 0)
		{
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000928F4 File Offset: 0x000918F4
		protected internal void InfillCells()
		{
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.FirstColumn > 0)
						{
							num = 8;
							continue;
						}
						goto IL_17B;
					case 1:
						this.ᜡ = new List<CellRange>();
						num = 14;
						continue;
					case 2:
						goto IL_17B;
					case 3:
					{
						int num2;
						num2++;
						num = 12;
						continue;
					}
					case 4:
					{
						int num2;
						int lastRow;
						if (num2 > lastRow)
						{
							num = 2;
							continue;
						}
						if (true)
						{
						}
						int num3 = this.FirstColumn;
						int lastColumn = this.LastColumn;
						num = 7;
						continue;
					}
					case 5:
					{
						int num3;
						int lastColumn;
						if (num3 > lastColumn)
						{
							num = 3;
							continue;
						}
						int num2;
						this.ᜡ.Add((CellRange)this.\u171D.InnerGetCell(num3, num2));
						num3++;
						num = 9;
						continue;
					}
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_157;
					case 8:
					{
						int num2 = this.FirstRow;
						int lastRow = this.LastRow;
						num = 11;
						continue;
					}
					case 9:
						goto IL_157;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CE;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 11:
						goto IL_1CE;
					case 12:
						goto IL_FB;
					case 13:
						return;
					case 14:
						if (this.FirstRow > 0)
						{
							num = 6;
							continue;
						}
						goto IL_17B;
					}
					if (!this.ᜢ)
					{
						num = 1;
						continue;
					}
					break;
					IL_FB:
					num = 4;
					continue;
					IL_1CE:
					goto IL_FB;
					IL_157:
					num = 5;
					continue;
					IL_17B:
					this.ᜢ = true;
					num = 13;
				}
				return;
			}
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00092AD4 File Offset: 0x00091AD4
		protected internal void ResetCells()
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
						if (false)
						{
						}
						this.ᜡ.Clear();
						break;
					}
					num = 2;
					continue;
				case 2:
					goto IL_6F;
				}
				if (true)
				{
				}
				if (this.ᜡ == null)
				{
					break;
				}
				num = 1;
			}
			IL_6F:
			this.ᜡ = null;
			this.ᜢ = false;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00092B60 File Offset: 0x00091B60
		public void Dispose()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					IL_85:
					if (this.m_rtfString != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					this.m_rtfString.Dispose();
					num = 5;
					continue;
				case 2:
					this.m_style = null;
					num = 3;
					continue;
				case 3:
					goto IL_61;
				case 5:
					return;
				}
				if (this.m_style != null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				IL_61:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00092C1C File Offset: 0x00091C1C
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
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00092C58 File Offset: 0x00091C58
		private void ᜁ(object A_0)
		{
			int a_ = 12;
			for (;;)
			{
				this.\u171D = (A_0 as XlsWorksheet);
				if (this.\u171D != null)
				{
					goto IL_61;
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
					goto IL_47;
				}
			}
			IL_47:
			if (false)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("ቁ╃㑅ⵇ⑉㡋湍㽏け㹓㍕㭗⹙籛㵝şౡ੣॥ᱧ䩩๫୭偯ᑱ᭳͵ᙷṹ剻", a_));
			IL_61:
			this.m_book = this.\u171D.ParentWorkbook;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00092CD8 File Offset: 0x00091CD8
		internal void ᜀ(sprἛ A_0)
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

		// Token: 0x06000E45 RID: 3653 RVA: 0x00092D18 File Offset: 0x00091D18
		internal void ᜀ(BiffRecordRaw[] A_0, ref int A_1)
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
			this.SerializeDataToList(A_0, ref A_1, false);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00092D5C File Offset: 0x00091D5C
		public void SerializeDataToList(IList data, ref int position, bool ignoreStyles)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				BiffRecordRaw biffRecordRaw;
				for (;;)
				{
					biffRecordRaw = (BiffRecordRaw)data[position];
					spr\u23A5 spr_u23A = (spr\u23A5)biffRecordRaw;
					this.FirstColumn = (this.LastColumn = spr_u23A.ᜅ() + 1);
					this.FirstRow = (this.LastRow = spr_u23A.ᜄ() + 1);
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (typeCode == TBIFFRecord.RK)
							{
								num = 3;
								continue;
							}
							goto IL_156;
						case 1:
							if (typeCode <= TBIFFRecord.RString)
							{
								num = 10;
								continue;
							}
							num = 7;
							continue;
						case 2:
							num = 9;
							continue;
						case 3:
							goto IL_1FB;
						case 4:
							if (typeCode != TBIFFRecord.Formula)
							{
								num = 11;
								continue;
							}
							goto IL_C8;
						case 5:
							if (typeCode != TBIFFRecord.RString)
							{
								num = 12;
								continue;
							}
							goto IL_117;
						case 6:
							num = 0;
							continue;
						case 7:
							if (typeCode != TBIFFRecord.LabelSST)
							{
								num = 2;
								continue;
							}
							return;
						case 8:
							goto IL_115;
						case 9:
							switch (typeCode)
							{
							case TBIFFRecord.Blank:
								goto IL_148;
							case (TBIFFRecord)514:
								goto IL_156;
							case TBIFFRecord.Number:
								goto IL_D7;
							case TBIFFRecord.Label:
								return;
							case TBIFFRecord.BoolErr:
								goto IL_1CA;
							}
							goto IL_197;
						case 10:
							num = 4;
							continue;
						case 11:
							num = 5;
							continue;
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_197;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num = 8;
								continue;
							}
							break;
						}
						break;
						IL_197:
						num = 6;
					}
				}
				IL_C8:
				this.ᜀ((spr᱒)biffRecordRaw, data, ref position);
				return;
				IL_D7:
				this.ᜀ((spr\u2230)biffRecordRaw);
				return;
				IL_115:
				goto IL_156;
				IL_117:
				this.ᜀ((spr\u19F6)biffRecordRaw);
				return;
				IL_148:
				this.ᜀ((spr\u171D)biffRecordRaw);
				return;
				IL_156:
				throw new ArgumentException(RecordTableEnumerator.b("ᵇ⑉❋⁍㽏║㩓癕㩗㍙㩛㡝䁟ၡţեݧᡩ࡫乭ѯୱѳ፵", a_));
				IL_1CA:
				XlsRange.ᜀ((spr\u249B)biffRecordRaw);
				return;
				IL_1FB:
				goto IL_D7;
			}
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00092F9C File Offset: 0x00091F9C
		internal string ᜀ(spr\u2230 A_0)
		{
			double a_;
			for (;;)
			{
				a_ = A_0.ᜀ();
				sprᤅ sprᤅ = this.InnerNumberFormat;
				CellFormatType cellFormatType = sprᤅ.ᜀ(a_);
				if (cellFormatType != CellFormatType.DateTime)
				{
					goto IL_5F;
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
					goto IL_44;
				}
			}
			IL_44:
			if (false)
			{
			}
			return this.DateTimeValue.ToString();
			IL_5F:
			return a_.ToString();
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00093010 File Offset: 0x00092010
		internal string ᜀ(spr\u171D A_0)
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
			return string.Empty;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00093050 File Offset: 0x00092050
		internal void ᜂ(spr᱒ A_0)
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
			throw new NotImplementedException();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00093090 File Offset: 0x00092090
		internal void ᜀ(spr᱒ A_0, IList A_1, ref int A_2)
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

		// Token: 0x06000E4B RID: 3659 RVA: 0x000930CC File Offset: 0x000920CC
		internal static string ᜀ(spr\u249B A_0)
		{
			int a_ = 1;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29;
					}
					if (false)
					{
					}
					num = 1;
					continue;
				case 1:
					if (FormulaUtil.ErrorCodeToName.ContainsKey((int)A_0.ᜄ()))
					{
						num = 2;
						continue;
					}
					goto IL_3E;
				case 2:
					goto IL_A9;
				}
				IL_29:
				if (!A_0.ᜂ())
				{
					goto IL_B3;
				}
				num = 0;
			}
			IL_3E:
			return RecordTableEnumerator.b("ᐶ眸ᐺ簼", a_);
			IL_A9:
			if (true)
			{
			}
			return FormulaUtil.ErrorCodeToName[(int)A_0.ᜄ()];
			IL_B3:
			return (A_0.ᜄ() == 1).ToString().ToUpper();
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000931A4 File Offset: 0x000921A4
		internal string ᜀ(spr\u19F6 A_0)
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
			return string.Empty;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000931E4 File Offset: 0x000921E4
		private void ᜀ(Ptg[] A_0, int A_1, int A_2, bool A_3)
		{
			int a_ = 16;
			while (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("㙅⥇㡉㽋⭍㑏ᑑ㭓⑕㕗⽙せ㽝", a_));
				}
			}
			XlsName.NameIndexChangedEventHandler a_2 = new XlsName.NameIndexChangedEventHandler(this.ᜀ);
			XlsRange.ᜀ(this.m_book, a_2, A_0, A_1, A_2, A_3);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00093260 File Offset: 0x00092260
		internal static void ᜀ(XlsWorkbook A_0, XlsName.NameIndexChangedEventHandler A_1, Ptg[] A_2, int A_3, int A_4, bool A_5)
		{
			int a_ = 17;
			try
			{
				switch (0)
				{
				default:
					for (;;)
					{
						Dictionary<long, object> a_2 = new Dictionary<long, object>();
						int num = 0;
						int num2 = A_2.Length;
						int num3 = 9;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (FormulaUtil.ᜀ(FormulaUtil.\u171D, A_2[num].TokenCode) != -1)
								{
									num3 = 4;
									continue;
								}
								goto IL_64;
							case 1:
								if (num >= num2)
								{
									num3 = 3;
									continue;
								}
								num3 = 7;
								continue;
							case 2:
							{
								spr\u1B76 spr_u1B = (spr\u1B76)A_2[num];
								spr_u1B.ᜂ();
								spr_u1B.ᜃ();
								XlsRange.ᜀ(A_0, spr_u1B, A_3, A_4, A_1, a_2, A_5);
								num3 = 10;
								continue;
							}
							case 3:
								num3 = 8;
								continue;
							case 4:
							{
								spr\u25A0 spr_u25A = (spr\u25A0)A_2[num];
								spr_u25A.ᜀ();
								XlsRange.ᜀ(A_0, spr_u25A, A_3, A_4, A_1, a_2, A_5);
								num3 = 5;
								continue;
							}
							case 5:
								goto IL_64;
							case 6:
								goto IL_D1;
							case 7:
								if (FormulaUtil.ᜀ(FormulaUtil.\u171C, A_2[num].TokenCode) != -1)
								{
									num3 = 2;
									continue;
								}
								num3 = 0;
								continue;
							case 8:
								goto IL_161;
							case 9:
								goto IL_D1;
							case 10:
								goto IL_64;
							}
							break;
							IL_64:
							num++;
							num3 = 6;
							continue;
							IL_D1:
							num3 = 1;
						}
					}
					IL_161:
					break;
				}
			}
			catch (Exception a_3)
			{
				if (!A_0.Loading)
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
						throw;
					}
				}
				throw new spr\u2313(RecordTableEnumerator.b("Ɇㅈ⡊⡌㽎═㩒㩔㥖祘㑚㹜㱞ᑠᅢdͦ䥨ᱪլ੮ὰ卲մᙶ୸ࡺᑼᅾꎂ", a_), a_3);
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00093440 File Offset: 0x00092440
		private static void ᜀ(XlsWorkbook A_0, spr\u1B76 A_1, int A_2, int A_3, XlsName.NameIndexChangedEventHandler A_4, Dictionary<long, object> A_5, bool A_6)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 20;
				long key;
				for (;;)
				{
					bool flag;
					bool flag2;
					int bookIndex;
					bool flag3;
					bool flag4;
					sprἉ sprἉ;
					int a_2;
					switch (num)
					{
					case 0:
						goto IL_36A;
					case 1:
						goto IL_205;
					case 2:
						if (A_5.ContainsKey(key))
						{
							num = 21;
							continue;
						}
						num = 25;
						continue;
					case 3:
						flag = false;
						goto IL_3C8;
					case 4:
						if (!flag2)
						{
							num = 38;
							continue;
						}
						goto IL_117;
					case 5:
						goto IL_36A;
					case 6:
						if (A_4 == null)
						{
							num = 1;
							continue;
						}
						bookIndex = A_0.GetBookIndex((int)A_1.ᜃ());
						num = 31;
						continue;
					case 7:
						num = 33;
						continue;
					case 8:
						num = 36;
						continue;
					case 9:
						if (flag2)
						{
							num = 35;
							continue;
						}
						goto IL_2CB;
					case 10:
						if (true)
						{
						}
						num = 17;
						continue;
					case 11:
						goto IL_37F;
					case 12:
						flag3 = true;
						goto IL_384;
					case 13:
						flag3 = A_0.IsLocalReference((int)A_1.ᜃ());
						goto IL_384;
					case 14:
						goto IL_347;
					case 15:
						num = 13;
						continue;
					case 16:
						flag = (A_3 == -1);
						goto IL_3C8;
					case 17:
						if (flag4)
						{
							num = 23;
							continue;
						}
						goto IL_2CB;
					case 18:
						if (flag4)
						{
							num = 19;
							continue;
						}
						return;
					case 19:
						goto IL_180;
					case 21:
						return;
					case 22:
						if (!flag2)
						{
							num = 7;
							continue;
						}
						return;
					case 23:
						goto IL_3C6;
					case 24:
						num = 37;
						continue;
					case 25:
						if (A_2 == -1)
						{
							num = 27;
							continue;
						}
						num = 3;
						continue;
					case 26:
						if (A_6)
						{
							num = 32;
							continue;
						}
						sprἉ.ᜀ(A_4);
						num = 0;
						continue;
					case 27:
						num = 16;
						continue;
					case 28:
						a_2 = bookIndex;
						goto IL_14A;
					case 29:
						goto IL_DA;
					case 30:
						a_2 = -1;
						goto IL_14A;
					case 31:
						if (A_1.ᜃ() >= 0)
						{
							num = 15;
							continue;
						}
						num = 12;
						continue;
					case 32:
						sprἉ.ᜁ(A_4);
						num = 5;
						continue;
					case 33:
						if (A_2 == bookIndex)
						{
							num = 8;
							continue;
						}
						goto IL_347;
					case 34:
						if ((int)A_1.ᜂ() != A_3)
						{
							num = 10;
							continue;
						}
						goto IL_428;
					case 35:
						num = 34;
						continue;
					case 36:
						if (A_2 != -1)
						{
							num = 24;
							continue;
						}
						goto IL_347;
					case 37:
						if ((int)A_1.ᜂ() != A_3)
						{
							num = 14;
							continue;
						}
						goto IL_180;
					case 38:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_117;
						default:
							if (false)
							{
							}
							num = 28;
							continue;
						}
						break;
					}
					if (A_1 == null)
					{
						num = 29;
						continue;
					}
					num = 6;
					continue;
					IL_117:
					num = 30;
					continue;
					IL_14A:
					key = XlsRange.ᜀ(a_2, (int)A_1.ᜂ());
					num = 2;
					continue;
					IL_180:
					XlsExternWorkbook xlsExternWorkbook = A_0.ExternWorkbooks[bookIndex];
					sprᭆ sprᭆ = xlsExternWorkbook.ExternNames;
					sprἉ = sprᭆ.ᜀ((int)(A_1.ᜂ() - 1));
					num = 26;
					continue;
					IL_2CB:
					num = 22;
					continue;
					IL_347:
					num = 18;
					continue;
					IL_36A:
					A_5.Add(key, null);
					num = 11;
					continue;
					IL_384:
					flag2 = flag3;
					num = 4;
					continue;
					IL_3C8:
					flag4 = flag;
					num = 9;
				}
				IL_DA:
				throw new ArgumentNullException(RecordTableEnumerator.b("⥆⡈♊⡌㝎", a_));
				IL_205:
				throw new ArgumentNullException(RecordTableEnumerator.b("⽆⡈╊⥌⍎㑐⅒", a_));
				IL_37F:
				return;
				IL_3C6:
				IL_428:
				((XlsName)A_0.Names[(int)(A_1.ᜂ() - 1)]).NameIndexChanged += A_4;
				A_5.Add(key, null);
				return;
			}
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000938A0 File Offset: 0x000928A0
		private static void ᜀ(XlsWorkbook A_0, spr\u25A0 A_1, int A_2, int A_3, XlsName.NameIndexChangedEventHandler A_4, Dictionary<long, object> A_5, bool A_6)
		{
			int a_ = 0;
			int num = 3;
			for (;;)
			{
				long key;
				XlsName xlsName;
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (A_5.ContainsKey(key))
					{
						num = 20;
						continue;
					}
					num = 18;
					continue;
				case 1:
					if (A_6)
					{
						num = 4;
						continue;
					}
					xlsName.NameIndexChanged -= A_4;
					num = 17;
					continue;
				case 2:
					num = 10;
					continue;
				case 4:
					if (true)
					{
					}
					xlsName.NameIndexChanged += A_4;
					num = 14;
					continue;
				case 5:
					goto IL_7B;
				case 6:
					goto IL_1F9;
				case 7:
					if ((int)A_1.ᜀ() != A_3)
					{
						num = 6;
						continue;
					}
					goto IL_C4;
				case 8:
					goto IL_1B9;
				case 9:
					num = 7;
					continue;
				case 10:
					flag = (A_3 == -1);
					goto IL_112;
				case 11:
					if (A_4 == null)
					{
						num = 8;
						continue;
					}
					key = XlsRange.ᜀ(-1, (int)A_1.ᜀ());
					num = 15;
					continue;
				case 12:
					goto IL_C4;
				case 13:
					if (flag2)
					{
						num = 12;
						continue;
					}
					return;
				case 14:
					goto IL_236;
				case 15:
					if (A_2 == -1)
					{
						num = 2;
						continue;
					}
					num = 19;
					continue;
				case 16:
					return;
				case 17:
					goto IL_236;
				case 18:
					if (A_2 == -1)
					{
						num = 9;
						continue;
					}
					goto IL_1F9;
				case 19:
					flag = false;
					goto IL_112;
				case 20:
					return;
				}
				if (A_1 == null)
				{
					num = 5;
					continue;
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
					num = 11;
					continue;
				}
				IL_C4:
				xlsName = (XlsName)A_0.Names[(int)(A_1.ᜀ() - 1)];
				num = 1;
				continue;
				IL_112:
				flag2 = flag;
				num = 0;
				continue;
				IL_1F9:
				num = 13;
				continue;
				IL_236:
				A_5.Add(key, null);
				num = 16;
			}
			IL_7B:
			throw new ArgumentNullException(RecordTableEnumerator.b("堵夷圹夻䘽", a_));
			IL_1B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("帵夷吹堻刽┿ぁ", a_));
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00093AFC File Offset: 0x00092AFC
		private static long ᜀ(int A_0, int A_1)
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
			return (long)((long)A_0 << 32 + A_1);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00093B44 File Offset: 0x00092B44
		private void ᜀ(object A_0, NameIndexChangedEventArgs A_1)
		{
			for (;;)
			{
				((spr\u1AE6)A_0).ᜁ(new XlsName.NameIndexChangedEventHandler(this.ᜀ));
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ((sprἉ)A_0, A_1);
						goto IL_80;
					case 1:
						goto IL_71;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_80;
						default:
							if (false)
							{
							}
							if (A_0 is XlsName)
							{
								num = 1;
								continue;
							}
							num = 4;
							continue;
						}
						break;
					case 3:
						goto IL_8B;
					case 4:
						if (A_0 is sprἉ)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
					IL_80:
					num = 3;
				}
			}
			IL_71:
			this.ᜀ((XlsName)A_0, A_1);
			return;
			IL_8B:
			if (true)
			{
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00093C14 File Offset: 0x00092C14
		private void ᜀ(XlsName A_0, NameIndexChangedEventArgs A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					spr\u1B76 spr_u1B;
					Ptg ptg;
					spr᱒ spr᱒;
					int num2;
					switch (num)
					{
					case 1:
						if (spr_u1B.ᜃ() != 65535)
						{
							num = 12;
							continue;
						}
						goto IL_D3;
					case 2:
						goto IL_1F4;
					case 3:
						if (this.m_book.IsLocalReference((int)spr_u1B.ᜃ()))
						{
							num = 15;
							continue;
						}
						goto IL_22F;
					case 4:
					{
						spr\u25A0 spr_u25A = ptg as spr\u25A0;
						spr᱒.ᜅ(true);
						num = 8;
						continue;
					}
					case 5:
						goto IL_198;
					case 6:
						return;
					case 7:
						goto IL_198;
					case 8:
					{
						spr\u25A0 spr_u25A;
						if (A_1.OldIndex == (int)(spr_u25A.ᜀ() - 1))
						{
							if (true)
							{
							}
							num = 11;
							continue;
						}
						goto IL_13E;
					}
					case 9:
						if (ptg is spr\u25A0)
						{
							num = 4;
							continue;
						}
						goto IL_13E;
					case 10:
						goto IL_22F;
					case 11:
					{
						spr\u25A0 spr_u25A;
						spr_u25A.ᜀ((ushort)(A_1.NewIndex + 1));
						num = 14;
						continue;
					}
					case 12:
						num = 3;
						continue;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F4;
						default:
							if (false)
							{
							}
							if (ptg is spr\u1B76)
							{
								num = 2;
								continue;
							}
							goto IL_22F;
						}
						break;
					case 14:
						goto IL_13E;
					case 15:
						num = 18;
						continue;
					case 16:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 6;
							continue;
						}
						Ptg[] array;
						ptg = array[num2];
						num = 13;
						continue;
					}
					case 17:
						goto IL_D3;
					case 18:
						if (A_1.OldIndex == (int)(spr_u1B.ᜂ() - 1))
						{
							num = 17;
							continue;
						}
						goto IL_22F;
					case 19:
					{
						spr᱒ = (spr᱒)this.Record;
						Ptg[] array = spr᱒.ᜑ();
						num2 = 0;
						int num3 = array.Length;
						num = 5;
						continue;
					}
					}
					if (this.CellType == XlsRange.TCellType.Formula)
					{
						num = 19;
						continue;
					}
					break;
					IL_D3:
					spr_u1B.ᜀ((ushort)(A_1.NewIndex + 1));
					num = 10;
					continue;
					IL_13E:
					num2++;
					num = 7;
					continue;
					IL_198:
					num = 16;
					continue;
					IL_1F4:
					spr_u1B = (ptg as spr\u1B76);
					spr᱒.ᜅ(true);
					num = 1;
					continue;
					IL_22F:
					num = 9;
				}
				return;
			}
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00093EAC File Offset: 0x00092EAC
		private void ᜀ(sprἉ A_0, NameIndexChangedEventArgs A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					spr\u1B76 spr_u1B;
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
						switch (num)
						{
						case 0:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 9;
								continue;
							}
							Ptg[] array;
							Ptg ptg = array[num2];
							num = 6;
							continue;
						}
						case 2:
							if (A_1.OldIndex == (int)(spr_u1B.ᜂ() - 1))
							{
								num = 10;
								continue;
							}
							goto IL_158;
						case 3:
							num = 2;
							continue;
						case 4:
							goto IL_113;
						case 5:
							if ((int)spr_u1B.ᜃ() == A_0.ᜂ())
							{
								num = 3;
								continue;
							}
							goto IL_158;
						case 6:
						{
							if (true)
							{
							}
							Ptg ptg;
							if (ptg is spr\u1B76)
							{
								num = 8;
								continue;
							}
							goto IL_158;
						}
						case 7:
							goto IL_158;
						case 8:
						{
							Ptg ptg;
							spr_u1B = (ptg as spr\u1B76);
							spr᱒ spr᱒;
							spr᱒.ᜅ(true);
							num = 5;
							continue;
						}
						case 9:
							return;
						case 10:
							goto IL_8A;
						case 11:
						{
							spr᱒ spr᱒ = (spr᱒)this.Record;
							Ptg[] array = spr᱒.ᜑ();
							num2 = 0;
							int num3 = array.Length;
							num = 12;
							continue;
						}
						case 12:
							goto IL_113;
						}
						if (this.CellType == XlsRange.TCellType.Formula)
						{
							num = 11;
							continue;
						}
						return;
						IL_113:
						num = 0;
						continue;
						IL_158:
						num2++;
						num = 4;
						continue;
					}
					}
					IL_8A:
					spr_u1B.ᜀ((ushort)(A_1.NewIndex + 1));
					num = 7;
				}
				return;
			}
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00094060 File Offset: 0x00093060
		public IXLSRange Activate()
		{
			for (;;)
			{
				this.ᜆ();
				if (this.IsSingleCell)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_34;
				}
			}
			this.\u171D.SetActiveCell(this);
			return this;
			IL_34:
			if (false)
			{
			}
			if (true)
			{
			}
			return null;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000940BC File Offset: 0x000930BC
		public virtual IXLSRange Activate(bool scroll)
		{
			for (;;)
			{
				this.Activate();
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (scroll)
						{
							num = 1;
							continue;
						}
						goto IL_79;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.\u171D.TopLeftCell = (this as CellRange);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_77;
					}
					break;
				}
			}
			IL_77:
			IL_79:
			this.\u171D.Activate();
			return this;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00094150 File Offset: 0x00093150
		protected internal IXLSRange Group(GroupByType groupBy, bool bCollapsed)
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
			this.ᜆ();
			return this.ToggleGroup(groupBy, true, bCollapsed);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0009419C File Offset: 0x0009319C
		protected internal IXLSRange Group(GroupByType groupBy)
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
			this.ᜆ();
			return this.Group(groupBy, false);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000941E8 File Offset: 0x000931E8
		public CellRange GroupByColumns(bool isCollapsed)
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
			this.ᜆ();
			return this.Group(GroupByType.ByColumns, isCollapsed) as CellRange;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00094238 File Offset: 0x00093238
		public CellRange GroupByRows(bool isCollapsed)
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
			this.ᜆ();
			return this.Group(GroupByType.ByRows, isCollapsed) as CellRange;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00094288 File Offset: 0x00093288
		public CellRange UngroupByColumns()
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
			return this.Ungroup(GroupByType.ByColumns) as CellRange;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000942D0 File Offset: 0x000932D0
		public CellRange UngroupByRows()
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
			return this.Ungroup(GroupByType.ByRows) as CellRange;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00094318 File Offset: 0x00093318
		public void Merge()
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
			this.Merge(false);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0009435C File Offset: 0x0009335C
		public void Merge(bool clearCells)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜆ();
					int num = 11;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							int row;
							int lastRow;
							if (row == lastRow)
							{
								num = 4;
								continue;
							}
							return;
						}
						case 2:
							goto IL_200;
						case 3:
						{
							int row;
							int lastColumn;
							this.\u171D.AutofitRow(row, num2, lastColumn, true);
							num = 8;
							continue;
						}
						case 4:
						{
							int column;
							num2 = column;
							int lastColumn = this.LastColumn;
							num = 17;
							continue;
						}
						case 5:
							return;
						case 6:
							num = 13;
							continue;
						case 7:
						{
							int column = this.Column;
							int row = this.Row;
							int lastRow = this.LastRow;
							int lastColumn2 = this.LastColumn;
							num = 1;
							continue;
						}
						case 8:
							goto IL_128;
						case 9:
							if (clearCells)
							{
								if (true)
								{
								}
								num = 16;
								continue;
							}
							goto IL_200;
						case 10:
						{
							int lastColumn;
							if (num2 > lastColumn)
							{
								num = 0;
								continue;
							}
							num = 15;
							continue;
						}
						case 11:
							if (this.IsSingleCell)
							{
								goto IL_76;
							}
							this.\u171D.MergeCells.ᜀ(this, MergeOperationType.Delete);
							num = 9;
							continue;
						case 12:
							goto IL_159;
						case 13:
						{
							int row;
							if (!this[row, num2].IsWrapText)
							{
								goto IL_128;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_76;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						}
						case 14:
							if (!this.m_book.IsLoaded)
							{
								num = 7;
								continue;
							}
							return;
						case 15:
						{
							int row;
							if (this[row, num2] != null)
							{
								num = 6;
								continue;
							}
							goto IL_128;
						}
						case 16:
							this.\u171D.ᜀ(this);
							num = 2;
							continue;
						case 17:
							goto IL_159;
						}
						break;
						IL_76:
						num = 5;
						continue;
						IL_128:
						num2++;
						num = 12;
						continue;
						IL_159:
						num = 10;
						continue;
						IL_200:
						num = 14;
					}
				}
				return;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00094594 File Offset: 0x00093594
		protected internal IXLSRange Ungroup(GroupByType groupBy)
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
			this.ᜆ();
			return this.ToggleGroup(groupBy, false, false);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000945E0 File Offset: 0x000935E0
		public void UnMerge()
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
			this.ᜆ();
			Rectangle a_ = Rectangle.FromLTRB(this.FirstColumn - 1, this.FirstRow - 1, this.LastColumn - 1, this.LastRow - 1);
			this.\u171D.MergeCells.ᜀ(a_);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00094658 File Offset: 0x00093658
		public void FreezePanes()
		{
			for (;;)
			{
				this.ᜆ();
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
							continue;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.\u171D.ᜂ(this);
							num = 1;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (this.IsSingleCell)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000946E0 File Offset: 0x000936E0
		public void ClearContents()
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
			this.ᜆ();
			this.Clear(false);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00094728 File Offset: 0x00093728
		protected internal void Clear(bool isClearFormat)
		{
			int a_ = 9;
			for (;;)
			{
				this.ᜆ();
				int num = 6;
				for (;;)
				{
					spr\u24F1 spr_u24F;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_BB;
					case 1:
						goto IL_108;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DF;
						default:
							if (false)
							{
							}
							goto IL_108;
						}
						break;
					case 3:
						return;
					case 4:
						if (isClearFormat)
						{
							num = 10;
							continue;
						}
						return;
					case 5:
					{
						int num2;
						if (num2 > this.LastColumn)
						{
							num = 9;
							continue;
						}
						spr_u24F.ᜀ(num3, num2);
						spr_u24F.ᜈ();
						num2++;
						num = 13;
						continue;
					}
					case 6:
						if (true)
						{
						}
						if (this.IsSingleCell)
						{
							num = 8;
							continue;
						}
						goto IL_DF;
					case 7:
						goto IL_74;
					case 8:
						this.ᜈ();
						num = 1;
						continue;
					case 9:
						num3++;
						num = 7;
						continue;
					case 10:
						this.CellStyleName = RecordTableEnumerator.b("焾⹀ㅂ⡄♆╈", a_);
						num = 3;
						continue;
					case 11:
					{
						if (num3 > this.LastRow)
						{
							num = 2;
							continue;
						}
						int num2 = this.FirstColumn;
						num = 0;
						continue;
					}
					case 12:
						goto IL_74;
					case 13:
						goto IL_BB;
					}
					break;
					IL_74:
					num = 11;
					continue;
					IL_BB:
					num = 5;
					continue;
					IL_DF:
					spr_u24F = new spr\u24F1(this.Application, this.\u171D);
					num3 = this.FirstRow;
					num = 12;
					continue;
					IL_108:
					num = 4;
				}
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000948DC File Offset: 0x000938DC
		public void ClearAll()
		{
			switch (0)
			{
			default:
			{
				XlsCommentsCollection innerComments;
				ICommentShape commentShape;
				for (;;)
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_8E:
						this.Clear(true);
						innerComments = this.\u171D.InnerComments;
						commentShape = innerComments[this.FirstRow, this.FirstColumn];
						num = 11;
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜆ();
						num = 1;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_EC;
						case 1:
						{
							if (this.IsSingleCell)
							{
								num = 7;
								continue;
							}
							spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
							int num2 = this.FirstRow;
							num = 3;
							continue;
						}
						case 2:
							goto IL_153;
						case 3:
							goto IL_EC;
						case 4:
							goto IL_9A;
						case 5:
						{
							int num3;
							if (num3 > this.LastColumn)
							{
								num = 9;
								continue;
							}
							spr\u24F1 spr_u24F;
							int num2;
							spr_u24F.ᜀ(num2, num3);
							spr_u24F.ClearAll();
							num3++;
							num = 8;
							continue;
						}
						case 6:
							goto IL_10D;
						case 7:
							goto IL_8E;
						case 8:
							goto IL_9A;
						case 9:
						{
							int num2;
							num2++;
							num = 0;
							continue;
						}
						case 10:
						{
							int num2;
							if (num2 > this.LastRow)
							{
								num = 6;
								continue;
							}
							int num3 = this.FirstColumn;
							num = 4;
							continue;
						}
						case 11:
							if (commentShape != null)
							{
								num = 2;
								continue;
							}
							return;
						}
						break;
						IL_9A:
						num = 5;
						continue;
						IL_EC:
						num = 10;
					}
				}
				IL_10D:
				return;
				IL_153:
				innerComments.Remove(commentShape);
				return;
			}
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00094A94 File Offset: 0x00093A94
		protected internal void Clear(MoveDirectionType direction)
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
			this.ᜆ();
			this.ᜀ(direction, CopyRangeOptions.None);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00094AE0 File Offset: 0x00093AE0
		internal void ᜀ(MoveDirectionType A_0, CopyRangeOptions A_1)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_62;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜆ();
			switch (A_0)
			{
			case MoveDirectionType.Left:
				IL_62:
				this.Clear(true);
				this.ᜀ(A_1);
				return;
			case MoveDirectionType.Up:
				this.Clear(true);
				this.ᜁ(A_1);
				return;
			case MoveDirectionType.None:
				this.Clear(true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00094B60 File Offset: 0x00093B60
		internal void ᜀ(ExcelClearOptions A_0, bool A_1)
		{
			int a_ = 2;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜆ();
					int num = 7;
					for (;;)
					{
						List<CellRange> list2;
						int num3;
						int count2;
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							List<CellRange> list;
							XlsRange xlsRange = list[num2];
							xlsRange.\u1718();
							num2++;
							num = 4;
							continue;
						}
						case 2:
							goto IL_159;
						case 3:
							num = 31;
							continue;
						case 4:
							goto IL_F9;
						case 5:
							this.CellStyleName = RecordTableEnumerator.b("瘷唹主匽ℿ⹁", a_);
							num = 19;
							continue;
						case 6:
							goto IL_2E2;
						case 7:
							if (A_0 == ExcelClearOptions.ClearFormat)
							{
								num = 15;
								continue;
							}
							if (true)
							{
							}
							num = 21;
							continue;
						case 8:
							list2 = this.CellsList;
							num3 = 0;
							count2 = list2.Count;
							num = 13;
							continue;
						case 9:
						{
							int num4;
							int count3;
							if (num4 >= count3)
							{
								num = 3;
								continue;
							}
							List<CellRange> list3;
							CellRange cellRange = list3[num4];
							num4++;
							num = 2;
							continue;
						}
						case 10:
							goto IL_2DD;
						case 11:
						{
							if (A_0 == ExcelClearOptions.ClearConditionalFormats)
							{
								num = 8;
								continue;
							}
							List<CellRange> list4 = this.CellsList;
							int num5 = 0;
							int count4 = list4.Count;
							num = 28;
							continue;
						}
						case 12:
							if (A_1)
							{
								num = 5;
								continue;
							}
							return;
						case 13:
							goto IL_32E;
						case 14:
						{
							List<CellRange> list5 = this.CellsList;
							int num6 = 0;
							int count5 = list5.Count;
							num = 16;
							continue;
						}
						case 15:
						{
							List<CellRange> list3 = this.CellsList;
							int num4 = 0;
							int count3 = list3.Count;
							num = 32;
							continue;
						}
						case 16:
							goto IL_2E2;
						case 17:
							if (A_0 == ExcelClearOptions.ClearComment)
							{
								num = 30;
								continue;
							}
							num = 11;
							continue;
						case 18:
							goto IL_F9;
						case 19:
							goto IL_26C;
						case 20:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_33A;
							default:
								goto IL_417;
							}
							break;
						case 21:
							if (A_0 == ExcelClearOptions.ClearContent)
							{
								num = 14;
								continue;
							}
							num = 17;
							continue;
						case 22:
						{
							int num5;
							int count4;
							if (num5 >= count4)
							{
								num = 27;
								continue;
							}
							List<CellRange> list4;
							XlsRange xlsRange2 = list4[num5];
							xlsRange2.Value = null;
							xlsRange2.\u1718();
							num5++;
							num = 29;
							continue;
						}
						case 23:
						{
							int num6;
							int count5;
							if (num6 >= count5)
							{
								num = 26;
								continue;
							}
							List<CellRange> list5;
							XlsRange xlsRange3 = list5[num6];
							xlsRange3.Value = null;
							num6++;
							num = 6;
							continue;
						}
						case 24:
							goto IL_32E;
						case 25:
							goto IL_33A;
						case 26:
							return;
						case 27:
							num = 12;
							continue;
						case 28:
							goto IL_308;
						case 29:
							goto IL_308;
						case 30:
						{
							List<CellRange> list = this.CellsList;
							int num2 = 0;
							int count = list.Count;
							num = 18;
							continue;
						}
						case 31:
							if (A_1)
							{
								num = 10;
								continue;
							}
							return;
						case 32:
							goto IL_159;
						}
						break;
						IL_33A:
						if (num3 >= count2)
						{
							num = 20;
							continue;
						}
						XlsRange xlsRange4 = list2[num3];
						xlsRange4.ClearConditionalFormats();
						num3++;
						num = 24;
						continue;
						IL_F9:
						num = 1;
						continue;
						IL_159:
						num = 9;
						continue;
						IL_2E2:
						num = 23;
						continue;
						IL_308:
						num = 22;
						continue;
						IL_32E:
						num = 25;
					}
				}
				return;
				IL_26C:
				return;
				IL_2DD:
				this.CellStyleName = RecordTableEnumerator.b("瘷唹主匽ℿ⹁", a_);
				return;
				IL_417:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00094F8C File Offset: 0x00093F8C
		public void Clear(ExcelClearOptions option)
		{
			for (;;)
			{
				switch (option)
				{
				case ExcelClearOptions.ClearFormat:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_39;
					}
					break;
				case ExcelClearOptions.ClearContent:
					goto IL_5B;
				case ExcelClearOptions.ClearComment:
					goto IL_1A;
				case ExcelClearOptions.ClearAll:
					goto IL_64;
				}
				break;
			}
			return;
			IL_1A:
			this.ᜀ(option, true);
			return;
			IL_39:
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ(option, true);
			return;
			IL_5B:
			this.ᜀ(option, true);
			return;
			IL_64:
			this.ᜀ(option, true);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00095008 File Offset: 0x00094008
		internal void \u1718()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsCommentsCollection innerComments = this.\u171D.InnerComments;
					ICommentShape commentShape = innerComments[this.FirstRow, this.FirstColumn];
					num = 2;
					continue;
				}
				case 1:
					return;
				case 2:
				{
					if (true)
					{
					}
					ICommentShape commentShape;
					if (commentShape != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 3:
					for (;;)
					{
						XlsCommentsCollection innerComments;
						ICommentShape commentShape;
						innerComments.Remove(commentShape);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					IL_60:
					if (false)
					{
					}
					num = 1;
					continue;
					goto IL_60;
				}
				if (!this.IsSingleCell)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000950C8 File Offset: 0x000940C8
		protected internal void MoveTo(IXLSRange destination)
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
			this.ᜆ();
			this.ᜀ(destination, CopyRangeOptions.All);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00095114 File Offset: 0x00094114
		protected internal void MoveTo(IXLSRange destination, bool bUpdateFormula)
		{
			for (;;)
			{
				for (;;)
				{
					if (true)
					{
					}
					this.ᜆ();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_69;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 2:
							if (!bUpdateFormula)
							{
								num = 1;
								continue;
							}
							num = 0;
							continue;
						case 3:
							goto IL_74;
						}
						break;
					}
				}
			}
			IL_69:
			CopyRangeOptions copyRangeOptions = CopyRangeOptions.UpdateFormulas;
			goto IL_77;
			IL_74:
			copyRangeOptions = CopyRangeOptions.None;
			IL_77:
			CopyRangeOptions a_ = copyRangeOptions;
			this.ᜀ(destination, a_);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000951A4 File Offset: 0x000941A4
		internal void ᜀ(IXLSRange A_0, CopyRangeOptions A_1)
		{
			this.ᜆ();
			if (this == A_0)
			{
				if (true)
				{
				}
			}
			else
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
					this.\u171D.ᜀ(A_0, this, A_1, false);
					return;
				}
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000951FC File Offset: 0x000941FC
		protected internal IXLSRange CopyTo(IXLSRange destination)
		{
			this.ᜆ();
			if (this != destination)
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
					return this.\u171D.ᜁ(destination, this, CopyRangeOptions.All);
				}
			}
			return destination;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00095254 File Offset: 0x00094254
		internal IXLSRange ᜁ(IXLSRange A_0, CopyRangeOptions A_1)
		{
			this.ᜆ();
			if (this != A_0)
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
					return this.\u171D.ᜁ(A_0, this, A_1);
				}
			}
			return A_0;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000952AC File Offset: 0x000942AC
		public IXLSRange Intersect(IXLSRange range)
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
			this.ᜆ();
			return this.\u171D.IntersectRanges(this, range);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000952FC File Offset: 0x000942FC
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
			this.ᜆ();
			return this.\u171D.MergeRanges(this, range);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0009534C File Offset: 0x0009434C
		public ICommentShape AddComment()
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
			return this.AddComment(true);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00095390 File Offset: 0x00094390
		public ICommentShape AddComment(bool bIsParseOptions)
		{
			if (true)
			{
			}
			XlsCommentsCollection innerComments;
			for (;;)
			{
				this.ᜆ();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						ICommentShape commentShape;
						if (commentShape != null)
						{
							num = 2;
							continue;
						}
						goto IL_4D;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
						{
							if (false)
							{
							}
							innerComments = this.\u171D.InnerComments;
							ICommentShape commentShape = innerComments[this.m_iTopRow, this.m_iLeftColumn];
							num = 0;
							continue;
						}
						}
						break;
					case 2:
					{
						ICommentShape commentShape;
						return commentShape;
					}
					case 3:
						goto IL_2E;
					}
					break;
					IL_2E:
					if (!this.IsSingleCell)
					{
						goto IL_B9;
					}
					num = 1;
				}
			}
			IL_4D:
			return innerComments.AddComment(this.m_iTopRow, this.m_iLeftColumn, bIsParseOptions);
			IL_B9:
			return null;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00095458 File Offset: 0x00094458
		public SizeF MeasureString(string measureString)
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
			this.ᜆ();
			return (this.Style.Font as FontWrapper).Wrapped.MeasureString(measureString);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000954B4 File Offset: 0x000944B4
		public void AutoFitRows()
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						this.ᜆ();
						int column = this.Column;
						int lastColumn = this.LastColumn;
						int num = this.FirstRow;
						int lastRow = this.LastRow;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (num <= lastRow)
								{
									this.\u171D.AutofitRow(num, column, lastColumn, true);
									num++;
									num2 = 1;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							case 1:
								goto IL_5C;
							case 2:
								goto IL_5C;
							case 3:
								return;
							}
							break;
							IL_5C:
							num2 = 0;
						}
					}
					break;
				}
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00095580 File Offset: 0x00094580
		public void AutoFitColumns()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_95:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_34;
			}
			int num2;
			int lastColumn;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					if (num2 > lastColumn)
					{
						num = 0;
						continue;
					}
					goto IL_7E;
				case 2:
					goto IL_60;
				case 3:
					goto IL_60;
				}
				goto IL_34;
				IL_60:
				num = 1;
			}
			return;
			IL_7E:
			int row;
			this.\u171D.AutofitColumn(num2, row, this.LastRow);
			num2++;
			goto IL_95;
			IL_34:
			this.ᜆ();
			row = this.Row;
			int lastRow = this.LastRow;
			num2 = this.FirstColumn;
			lastColumn = this.LastColumn;
			num = 2;
			goto IL_1E;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0009563C File Offset: 0x0009463C
		public void Replace(string oldValue, string newValue)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.Text = newValue;
						num = 4;
						continue;
					case 1:
						if (this.Text == oldValue)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						if (true)
						{
						}
						num = 1;
						continue;
					case 3:
						goto IL_2A;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_6B;
						}
						break;
					}
					break;
					IL_2A:
					if (!this.IsSingleCell)
					{
						return;
					}
					num = 2;
				}
			}
			IL_6B:
			if (false)
			{
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000956EC File Offset: 0x000946EC
		public void Replace(string oldValue, double newValue)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (this.Text == oldValue)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						this.NumberValue = newValue;
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_73;
						}
						break;
					case 4:
						goto IL_2A;
					}
					break;
					IL_2A:
					if (!this.IsSingleCell)
					{
						return;
					}
					if (true)
					{
					}
					num = 0;
				}
			}
			IL_73:
			if (false)
			{
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0009579C File Offset: 0x0009479C
		public void Replace(string oldValue, DateTime newValue)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.DateTimeValue = newValue;
						num = 4;
						continue;
					case 1:
						if (this.Text == oldValue)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						goto IL_2A;
					case 3:
						num = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_73;
						}
						break;
					}
					break;
					IL_2A:
					if (true)
					{
					}
					if (!this.IsSingleCell)
					{
						return;
					}
					num = 3;
				}
			}
			IL_73:
			if (false)
			{
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0009584C File Offset: 0x0009484C
		public void Replace(string oldValue, string[] newValues, bool isVertical)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.Text == oldValue)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 2:
						goto IL_2A;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_81;
						}
						break;
					case 4:
						this.\u171D.InsertArray(newValues, this.Row, this.Column, isVertical);
						num = 3;
						continue;
					}
					break;
					IL_2A:
					if (!this.IsSingleCell)
					{
						return;
					}
					num = 1;
				}
			}
			IL_81:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00095910 File Offset: 0x00094910
		public void Replace(string oldValue, int[] newValues, bool isVertical)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						this.\u171D.InsertArray(newValues, this.Row, this.Column, isVertical);
						num = 4;
						continue;
					case 2:
						goto IL_2A;
					case 3:
						if (this.Text == oldValue)
						{
							num = 1;
							continue;
						}
						return;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_81;
						}
						break;
					}
					break;
					IL_2A:
					if (!this.IsSingleCell)
					{
						return;
					}
					num = 0;
				}
			}
			IL_81:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000959D4 File Offset: 0x000949D4
		public void Replace(string oldValue, double[] newValues, bool isVertical)
		{
			for (;;)
			{
				this.ᜆ();
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (this.Text == oldValue)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						goto IL_32;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_32;
						default:
							goto IL_86;
						}
						break;
					case 4:
						this.\u171D.InsertArray(newValues, this.Row, this.Column, isVertical);
						num = 3;
						continue;
					}
					break;
					IL_32:
					if (!this.IsSingleCell)
					{
						return;
					}
					num = 0;
				}
			}
			IL_86:
			if (false)
			{
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00095A98 File Offset: 0x00094A98
		public void Replace(string oldValue, DataTable newValues, bool isFieldNamesShown)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_2A;
					case 2:
						if (this.Text == oldValue)
						{
							num = 3;
							continue;
						}
						goto IL_AC;
					case 3:
						this.\u171D.InsertDataTable(newValues, isFieldNamesShown, this.Row, this.Column);
						num = 4;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_7E;
						}
						break;
					}
					break;
					IL_2A:
					if (!this.IsSingleCell)
					{
						goto IL_AC;
					}
					num = 0;
				}
			}
			IL_7E:
			if (false)
			{
			}
			IL_AC:
			if (true)
			{
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00095B5C File Offset: 0x00094B5C
		public void Replace(string oldValue, DataColumn newValues, bool isFieldNamesShown)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_2A;
					case 1:
						this.\u171D.InsertDataColumn(newValues, isFieldNamesShown, this.Row, this.Column);
						num = 3;
						continue;
					case 2:
						if (this.Text == oldValue)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							goto IL_81;
						}
						break;
					case 4:
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
					IL_2A:
					if (!this.IsSingleCell)
					{
						return;
					}
					num = 4;
				}
			}
			IL_81:
			if (false)
			{
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00095C20 File Offset: 0x00094C20
		protected internal IXLSRange FindFirst(string findValue, FindType flags)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				IXLSRange[] array;
				for (;;)
				{
					this.ᜆ();
					int num = 14;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_282;
							default:
								if (false)
								{
								}
								num = 21;
								continue;
							}
							break;
						case 1:
							goto IL_282;
						case 2:
							if (this.Formula == findValue)
							{
								num = 9;
								continue;
							}
							goto IL_386;
						case 3:
							if (this.HasFormula)
							{
								num = 35;
								continue;
							}
							goto IL_386;
						case 4:
							return this;
						case 5:
						{
							bool flag;
							if (flag)
							{
								num = 17;
								continue;
							}
							goto IL_366;
						}
						case 6:
						{
							bool flag2;
							if (flag2)
							{
								num = 16;
								continue;
							}
							goto IL_344;
						}
						case 7:
							num = 28;
							continue;
						case 8:
							goto IL_166;
						case 9:
							return this;
						case 10:
							if (this.FormulaStringValue == findValue)
							{
								num = 4;
								continue;
							}
							goto IL_344;
						case 11:
							if (this.IsSingleCell)
							{
								num = 31;
								continue;
							}
							array = this.\u171D.Find(this, findValue, flags, true);
							num = 18;
							continue;
						case 12:
						{
							bool flag3;
							if (flag3)
							{
								num = 30;
								continue;
							}
							goto IL_438;
						}
						case 13:
							goto IL_2B2;
						case 14:
							if (findValue != null)
							{
								num = 15;
								continue;
							}
							goto IL_40C;
						case 15:
							num = 20;
							continue;
						case 16:
							num = 26;
							continue;
						case 17:
							num = 37;
							continue;
						case 18:
							if (array == null)
							{
								num = 22;
								continue;
							}
							goto IL_238;
						case 19:
						{
							bool flag4;
							if (flag4)
							{
								num = 33;
								continue;
							}
							goto IL_386;
						}
						case 20:
						{
							if (findValue.Length == 0)
							{
								num = 13;
								continue;
							}
							bool flag4 = (flags & FindType.Formula) == FindType.Formula;
							bool flag3 = (flags & FindType.Text) == FindType.Text;
							bool flag2 = (flags & FindType.FormulaStringValue) == FindType.FormulaStringValue;
							bool flag = (flags & FindType.Error) == FindType.Error;
							num = 23;
							continue;
						}
						case 21:
							if (this.ErrorValue == findValue)
							{
								num = 1;
								continue;
							}
							goto IL_366;
						case 22:
							goto IL_1F5;
						case 23:
						{
							bool flag4;
							if (!flag4)
							{
								num = 7;
								continue;
							}
							goto IL_2D7;
						}
						case 24:
							num = 29;
							continue;
						case 25:
							num = 10;
							continue;
						case 26:
							if (this.FormulaStringValue != null)
							{
								num = 25;
								continue;
							}
							goto IL_344;
						case 27:
						{
							bool flag2;
							if (!flag2)
							{
								num = 36;
								continue;
							}
							goto IL_2D7;
						}
						case 28:
						{
							bool flag3;
							if (!flag3)
							{
								num = 34;
								continue;
							}
							goto IL_2D7;
						}
						case 29:
							if (this.Text == findValue)
							{
								num = 32;
								continue;
							}
							goto IL_438;
						case 30:
							num = 38;
							continue;
						case 31:
							num = 5;
							continue;
						case 32:
							return this;
						case 33:
							num = 3;
							continue;
						case 34:
							num = 27;
							continue;
						case 35:
							num = 2;
							continue;
						case 36:
							num = 39;
							continue;
						case 37:
							if (this.HasError)
							{
								num = 0;
								continue;
							}
							goto IL_366;
						case 38:
							if (this.HasString)
							{
								num = 24;
								continue;
							}
							goto IL_438;
						case 39:
						{
							bool flag;
							if (!flag)
							{
								num = 8;
								continue;
							}
							goto IL_2D7;
						}
						}
						break;
						IL_2D7:
						num = 11;
						continue;
						IL_344:
						num = 12;
						continue;
						IL_366:
						num = 19;
						continue;
						IL_386:
						num = 6;
					}
				}
				return this;
				IL_166:
				throw new ArgumentException(RecordTableEnumerator.b("椸娺似帾ⱀ♂ㅄ≆㭈歊⑌㱎煐㵒㩔⍖祘ⵚ㱜㍞ࡠݢ䭤", a_));
				IL_1F5:
				return null;
				IL_238:
				return array[0];
				IL_282:
				if (true)
				{
				}
				return this;
				IL_2B2:
				IL_40C:
				return null;
				IL_438:
				return null;
			}
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00096068 File Offset: 0x00095068
		protected internal IXLSRange FindFirst(double findValue, FindType flags)
		{
			int a_ = 18;
			IXLSRange[] array;
			for (;;)
			{
				IL_61:
				this.ᜆ();
				bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
				bool flag2 = (flags & FindType.Number) == FindType.Number;
				for (;;)
				{
					IL_79:
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 7;
							continue;
						case 1:
							if (flag2)
							{
								num = 18;
								continue;
							}
							goto IL_BB;
						case 2:
							goto IL_141;
						case 3:
							return this;
						case 4:
							return this;
						case 5:
							if (flag)
							{
								num = 16;
								continue;
							}
							goto IL_21A;
						case 6:
							num = 1;
							continue;
						case 7:
							if (this.NumberValue == findValue)
							{
								num = 4;
								continue;
							}
							goto IL_BB;
						case 8:
							if (!flag)
							{
								num = 13;
								continue;
							}
							goto IL_DC;
						case 9:
							if (this.FormulaNumberValue == findValue)
							{
								num = 3;
								continue;
							}
							goto IL_21A;
						case 10:
							goto IL_1E4;
						case 11:
							if (!flag2)
							{
								num = 10;
								continue;
							}
							goto IL_DC;
						case 12:
							if (this.HasFormula)
							{
								num = 15;
								continue;
							}
							goto IL_21A;
						case 13:
							num = 11;
							continue;
						case 14:
							if (this.HasNumber)
							{
								num = 0;
								continue;
							}
							goto IL_BB;
						case 15:
							num = 9;
							continue;
						case 16:
							num = 12;
							continue;
						case 17:
							if (array == null)
							{
								num = 2;
								continue;
							}
							goto IL_159;
						case 18:
							num = 14;
							continue;
						case 19:
							if (this.IsSingleCell)
							{
								num = 6;
								continue;
							}
							array = this.\u171D.Find(this, findValue, flags, true);
							num = 17;
							continue;
						}
						goto IL_61;
						IL_BB:
						num = 5;
						continue;
						IL_DC:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
							if (false)
							{
							}
							num = 19;
							break;
						}
					}
				}
			}
			return this;
			IL_141:
			return null;
			IL_159:
			return array[0];
			IL_1E4:
			throw new ArgumentException(RecordTableEnumerator.b("ᡇ⭉㹋⽍㵏㝑⁓㍕⩗穙㕛ⵝ䁟ౡୣብ䡧ᱩ൫ɭ᥯ᙱ婳", a_));
			IL_21A:
			if (true)
			{
			}
			return null;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00096298 File Offset: 0x00095298
		protected internal IXLSRange FindFirst(bool findValue)
		{
			IXLSRange[] array;
			for (;;)
			{
				this.ᜆ();
				int num = 8;
				for (;;)
				{
					byte b;
					switch (num)
					{
					case 0:
						if (!findValue)
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					case 1:
						if (array == null)
						{
							num = 2;
							continue;
						}
						goto IL_137;
					case 2:
						goto IL_E2;
					case 3:
						b = 0;
						goto IL_B7;
					case 4:
						b = 1;
						goto IL_B7;
					case 5:
						num = 10;
						continue;
					case 6:
						if (true)
						{
						}
						num = 3;
						continue;
					case 7:
						if (this.HasBoolean)
						{
							num = 5;
							continue;
						}
						goto IL_135;
					case 8:
						if (this.IsSingleCell)
						{
							num = 9;
							continue;
						}
						num = 0;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_131;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 10:
						if (this.BooleanValue != findValue)
						{
							num = 11;
							continue;
						}
						return this;
					case 11:
						goto IL_B2;
					}
					break;
					IL_B7:
					byte findValue2 = b;
					array = this.\u171D.Find(this, findValue2, false, true);
					num = 1;
				}
			}
			IL_B2:
			goto IL_135;
			IL_E2:
			IL_131:
			return null;
			IL_135:
			return null;
			IL_137:
			return array[0];
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000963E0 File Offset: 0x000953E0
		protected internal IXLSRange FindFirst(DateTime findValue)
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜆ();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return this;
					case 1:
						if (this.DateTimeValue == findValue)
						{
							num = 0;
							continue;
						}
						goto IL_BF;
					case 2:
						if (!this.HasDateTime)
						{
							goto IL_BF;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_36;
					case 5:
						num = 2;
						continue;
					}
					break;
					IL_36:
					if (!this.IsSingleCell)
					{
						goto IL_AD;
					}
					num = 5;
				}
			}
			return this;
			IL_AD:
			double findValue2 = findValue.ToOADate();
			return this.FindFirst(findValue2, FindType.Number | FindType.FormulaValue);
			IL_BF:
			return null;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000964B0 File Offset: 0x000954B0
		protected internal IXLSRange FindFirst(TimeSpan findValue)
		{
			for (;;)
			{
				if (true)
				{
				}
				this.ᜆ();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return this;
					case 1:
						if (this.TimeSpanValue == findValue)
						{
							num = 0;
							continue;
						}
						goto IL_BF;
					case 2:
						num = 5;
						continue;
					case 3:
						goto IL_36;
					case 4:
						num = 1;
						continue;
					case 5:
						if (!this.HasDateTime)
						{
							goto IL_BF;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					}
					break;
					IL_36:
					if (!this.IsSingleCell)
					{
						goto IL_AD;
					}
					num = 2;
				}
			}
			return this;
			IL_AD:
			double totalDays = findValue.TotalDays;
			return this.FindFirst(totalDays, FindType.Number | FindType.FormulaValue);
			IL_BF:
			return null;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00096580 File Offset: 0x00095580
		protected internal List<CellRange> FindAll(string findValue, FindType flags)
		{
			int a_ = 16;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜆ();
					int num = 17;
					for (;;)
					{
						List<CellRange> list;
						switch (num)
						{
						case 0:
							num = 25;
							continue;
						case 1:
							if (this.HasFormula)
							{
								num = 30;
								continue;
							}
							goto IL_493;
						case 2:
							if (this.HasString)
							{
								num = 26;
								continue;
							}
							goto IL_224;
						case 3:
							num = 40;
							continue;
						case 4:
							goto IL_224;
						case 5:
							if (this.Text == findValue)
							{
								num = 14;
								continue;
							}
							goto IL_224;
						case 6:
							num = 1;
							continue;
						case 7:
							if (!(this.ErrorValue == findValue))
							{
								num = 29;
								continue;
							}
							goto IL_177;
						case 8:
						{
							bool flag;
							if (!flag)
							{
								num = 31;
								continue;
							}
							goto IL_43B;
						}
						case 9:
						{
							bool flag;
							if (flag)
							{
								num = 22;
								continue;
							}
							goto IL_224;
						}
						case 10:
						{
							bool flag2;
							if (!flag2)
							{
								num = 19;
								continue;
							}
							goto IL_43B;
						}
						case 11:
							goto IL_FC;
						case 12:
							goto IL_32B;
						case 13:
							goto IL_247;
						case 14:
							goto IL_177;
						case 15:
						{
							bool flag3;
							if (!flag3)
							{
								num = 12;
								continue;
							}
							goto IL_43B;
						}
						case 16:
						{
							object obj;
							if (obj != null)
							{
								num = 34;
								continue;
							}
							goto IL_224;
						}
						case 17:
							if (findValue != null)
							{
								num = 23;
								continue;
							}
							goto IL_463;
						case 18:
						{
							if (this.IsSingleCell)
							{
								num = 43;
								continue;
							}
							object obj = this.\u171D.Find(this, findValue, flags, false);
							num = 16;
							continue;
						}
						case 19:
							num = 15;
							continue;
						case 20:
						{
							bool flag4;
							if (flag4)
							{
								num = 6;
								continue;
							}
							goto IL_493;
						}
						case 21:
							goto IL_224;
						case 22:
							num = 2;
							continue;
						case 23:
							num = 42;
							continue;
						case 24:
							num = 8;
							continue;
						case 25:
							if (this.HasError)
							{
								num = 33;
								continue;
							}
							goto IL_3F0;
						case 26:
							num = 5;
							continue;
						case 27:
							goto IL_307;
						case 28:
							if (this.FormulaStringValue != null)
							{
								num = 3;
								continue;
							}
							goto IL_FC;
						case 29:
							goto IL_3F0;
						case 30:
							num = 35;
							continue;
						case 31:
							num = 10;
							continue;
						case 32:
							if (list.Count == 0)
							{
								goto IL_23B;
							}
							return list;
						case 33:
							num = 7;
							continue;
						case 34:
						{
							object obj;
							list.Add((CellRange)obj);
							if (true)
							{
							}
							num = 21;
							continue;
						}
						case 35:
							if (!(this.Formula == findValue))
							{
								num = 38;
								continue;
							}
							goto IL_177;
						case 36:
						{
							bool flag4;
							if (!flag4)
							{
								num = 24;
								continue;
							}
							goto IL_43B;
						}
						case 37:
						{
							bool flag2;
							if (flag2)
							{
								num = 39;
								continue;
							}
							goto IL_FC;
						}
						case 38:
							goto IL_493;
						case 39:
							num = 28;
							continue;
						case 40:
							if (!(this.FormulaStringValue == findValue))
							{
								num = 11;
								continue;
							}
							goto IL_177;
						case 41:
						{
							bool flag3;
							if (flag3)
							{
								num = 0;
								continue;
							}
							goto IL_3F0;
						}
						case 42:
						{
							if (findValue.Length == 0)
							{
								num = 27;
								continue;
							}
							list = new List<CellRange>();
							bool flag4 = (flags & FindType.Formula) == FindType.Formula;
							bool flag = (flags & FindType.Text) == FindType.Text;
							bool flag2 = (flags & FindType.FormulaStringValue) == FindType.FormulaStringValue;
							bool flag3 = (flags & FindType.Error) == FindType.Error;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_23B;
							default:
								if (false)
								{
								}
								num = 36;
								continue;
							}
							break;
						}
						case 43:
							num = 41;
							continue;
						}
						break;
						IL_FC:
						num = 9;
						continue;
						IL_177:
						list.Add((CellRange)this);
						num = 4;
						continue;
						IL_224:
						num = 32;
						continue;
						IL_23B:
						num = 13;
						continue;
						IL_3F0:
						num = 20;
						continue;
						IL_43B:
						num = 18;
						continue;
						IL_493:
						num = 37;
					}
				}
				IL_247:
				return null;
				IL_307:
				goto IL_463;
				IL_32B:
				throw new ArgumentException(RecordTableEnumerator.b("ᙅ⥇㡉ⵋ⍍㕏♑ㅓ⑕硗㍙⽛繝๟ൡၣ䙥ṧ୩kݭᑯ山", a_));
				IL_463:
				return null;
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00096A44 File Offset: 0x00095A44
		protected internal CellRange[] FindAll(double findValue, FindType flags)
		{
			int a_ = 2;
			for (;;)
			{
				this.ᜆ();
				bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
				bool flag2 = (flags & FindType.Number) == FindType.Number;
				int num = 14;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (this.HasNumber)
						{
							num = 4;
							continue;
						}
						goto IL_19B;
					case 2:
						if (this.HasFormula)
						{
							num = 15;
							continue;
						}
						goto IL_108;
					case 3:
						if (flag2)
						{
							num = 0;
							continue;
						}
						goto IL_19B;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17B;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					case 5:
						num = 8;
						continue;
					case 6:
						if (this.FormulaNumberValue != findValue)
						{
							num = 11;
							continue;
						}
						goto IL_146;
					case 7:
						num = 2;
						continue;
					case 8:
						if (!flag2)
						{
							num = 17;
							continue;
						}
						goto IL_17B;
					case 9:
						num = 3;
						continue;
					case 10:
						if (flag)
						{
							num = 7;
							continue;
						}
						goto IL_108;
					case 11:
						goto IL_144;
					case 12:
						goto IL_19B;
					case 13:
						if (this.IsSingleCell)
						{
							num = 9;
							continue;
						}
						goto IL_1E4;
					case 14:
						if (!flag)
						{
							num = 5;
							continue;
						}
						goto IL_17B;
					case 15:
						num = 6;
						continue;
					case 16:
						if (true)
						{
						}
						if (this.NumberValue != findValue)
						{
							num = 12;
							continue;
						}
						goto IL_146;
					case 17:
						goto IL_123;
					}
					break;
					IL_17B:
					num = 13;
					continue;
					IL_19B:
					num = 10;
				}
			}
			IL_108:
			return null;
			IL_123:
			throw new ArgumentException(RecordTableEnumerator.b("样嬹主弽ⴿ❁ぃ⍅㩇橉╋㵍灏㱑㭓≕硗ⱙ㵛㉝य़١䩣", a_));
			IL_144:
			goto IL_108;
			IL_146:
			return new CellRange[]
			{
				this as CellRange
			};
			IL_1E4:
			return this.\u171D.Find(this, findValue, flags, false);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00096C44 File Offset: 0x00095C44
		protected internal CellRange[] FindAll(bool findValue)
		{
			for (;;)
			{
				this.ᜆ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.IsSingleCell)
						{
							num = 5;
							continue;
						}
						num = 7;
						continue;
					case 1:
						if (!(this.HasBoolean & this.BooleanValue == findValue))
						{
							goto IL_85;
						}
						goto IL_59;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_85;
						default:
							goto IL_A6;
						}
						break;
					case 3:
						goto IL_53;
					case 4:
						num = 3;
						continue;
					case 5:
						num = 1;
						continue;
					case 6:
						goto IL_B9;
					case 7:
						if (!findValue)
						{
							num = 4;
							continue;
						}
						num = 6;
						continue;
					}
					break;
					IL_85:
					num = 2;
				}
			}
			IL_53:
			byte b = 0;
			goto IL_EE;
			IL_59:
			return new CellRange[]
			{
				this as CellRange
			};
			IL_A6:
			if (false)
			{
			}
			if (true)
			{
			}
			return null;
			IL_B9:
			b = 1;
			IL_EE:
			byte findValue2 = b;
			return this.\u171D.Find(this, findValue2, false, false);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00096D50 File Offset: 0x00095D50
		protected internal CellRange[] FindAll(DateTime findValue)
		{
			List<CellRange> list;
			for (;;)
			{
				this.ᜆ();
				list = new List<CellRange>();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A9;
					case 1:
						if (list.Count == 0)
						{
							num = 3;
							continue;
						}
						goto IL_119;
					case 2:
						list.Add(this as CellRange);
						num = 4;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A9;
						default:
							goto IL_C1;
						}
						break;
					case 4:
						goto IL_55;
					case 5:
						if (this.DateTimeValue == findValue)
						{
							num = 2;
							continue;
						}
						goto IL_55;
					case 6:
						if (this.HasDateTime)
						{
							num = 0;
							continue;
						}
						goto IL_55;
					case 7:
						if (this.IsSingleCell)
						{
							num = 8;
							continue;
						}
						goto IL_107;
					case 8:
						num = 6;
						continue;
					}
					break;
					IL_55:
					num = 1;
					continue;
					IL_A9:
					num = 5;
				}
			}
			IL_C1:
			if (false)
			{
			}
			if (true)
			{
			}
			return null;
			IL_107:
			double findValue2 = findValue.ToOADate();
			return this.FindAll(findValue2, FindType.Number | FindType.FormulaValue);
			IL_119:
			return list.ToArray();
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00096E7C File Offset: 0x00095E7C
		protected internal CellRange[] FindAll(TimeSpan findValue)
		{
			List<CellRange> list;
			for (;;)
			{
				this.ᜆ();
				list = new List<CellRange>();
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_B1;
					case 1:
						num = 7;
						continue;
					case 2:
						if (this.IsSingleCell)
						{
							num = 1;
							continue;
						}
						goto IL_107;
					case 3:
						if (this.TimeSpanValue == findValue)
						{
							num = 8;
							continue;
						}
						goto IL_5D;
					case 4:
						if (list.Count == 0)
						{
							num = 6;
							continue;
						}
						goto IL_119;
					case 5:
						goto IL_5D;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B1;
						}
						goto Block_4;
					case 7:
						if (this.HasDateTime)
						{
							num = 0;
							continue;
						}
						goto IL_5D;
					case 8:
						list.Add(this as CellRange);
						num = 5;
						continue;
					}
					break;
					IL_5D:
					num = 4;
					continue;
					IL_B1:
					num = 3;
				}
			}
			Block_4:
			if (false)
			{
			}
			return null;
			IL_107:
			double totalDays = findValue.TotalDays;
			return this.FindAll(totalDays, FindType.Number | FindType.FormulaValue);
			IL_119:
			return list.ToArray();
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00096FA8 File Offset: 0x00095FA8
		public void CopyToClipboard()
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
			spr\u214D spr_u214D = this.AppImplementation.ᜀ(this.\u171D);
			spr_u214D.ᜂ(this);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00096FFC File Offset: 0x00095FFC
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

		// Token: 0x06000E8A RID: 3722 RVA: 0x00097040 File Offset: 0x00096040
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

		// Token: 0x06000E8B RID: 3723 RVA: 0x00097084 File Offset: 0x00096084
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
			ExcelColors nearestColor = this.m_book.GetNearestColor(borderColor);
			this.BorderAround(borderLine, nearestColor);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000970D4 File Offset: 0x000960D4
		public void BorderAround(LineStyleType borderLine, ExcelColors borderColor)
		{
			switch (0)
			{
			default:
			{
				int num = 9;
				for (;;)
				{
					int num2;
					int lastColumn;
					spr\u24F1 spr_u24F;
					int row;
					int lastRow;
					int num3;
					switch (num)
					{
					case 0:
						if (num2 > lastColumn)
						{
							num = 4;
							continue;
						}
						spr_u24F.ᜀ(row, num2);
						spr_u24F.SetBorderToSingleCell(BordersLineType.EdgeTop, borderLine, borderColor);
						spr_u24F.ᜀ(lastRow, num2);
						spr_u24F.SetBorderToSingleCell(BordersLineType.EdgeBottom, borderLine, borderColor);
						num2++;
						num = 5;
						continue;
					case 1:
						goto IL_C8;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_137;
						default:
							goto IL_105;
						}
						break;
					case 3:
						if (true)
						{
						}
						goto IL_C8;
					case 4:
						num3 = row;
						num = 1;
						continue;
					case 5:
						goto IL_6C;
					case 6:
						goto IL_6C;
					case 7:
						if (num3 > lastRow)
						{
							num = 2;
							continue;
						}
						goto IL_137;
					case 8:
						goto IL_59;
					}
					if (this.IsSingleCell)
					{
						num = 8;
						continue;
					}
					int column = this.Column;
					lastColumn = this.LastColumn;
					row = this.Row;
					lastRow = this.LastRow;
					spr_u24F = new spr\u24F1(this.Application, this.\u171D);
					num2 = column;
					num = 6;
					continue;
					IL_6C:
					num = 0;
					continue;
					IL_C8:
					num = 7;
					continue;
					IL_137:
					spr_u24F.ᜀ(num3, column);
					spr_u24F.SetBorderToSingleCell(BordersLineType.EdgeLeft, borderLine, borderColor);
					spr_u24F.ᜀ(num3, lastColumn);
					spr_u24F.SetBorderToSingleCell(BordersLineType.EdgeRight, borderLine, borderColor);
					num3++;
					num = 3;
				}
				IL_59:
				this.SetBorderToSingleCell(BordersLineType.EdgeLeft, borderLine, borderColor);
				this.SetBorderToSingleCell(BordersLineType.EdgeRight, borderLine, borderColor);
				this.SetBorderToSingleCell(BordersLineType.EdgeTop, borderLine, borderColor);
				this.SetBorderToSingleCell(BordersLineType.EdgeBottom, borderLine, borderColor);
				return;
				IL_105:
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000972A0 File Offset: 0x000962A0
		public void BorderInside()
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
			this.BorderInside(LineStyleType.Thin);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000972E4 File Offset: 0x000962E4
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

		// Token: 0x06000E8F RID: 3727 RVA: 0x00097328 File Offset: 0x00096328
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
			ExcelColors nearestColor = this.m_book.GetNearestColor(borderColor);
			this.BorderInside(borderLine, nearestColor);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00097378 File Offset: 0x00096378
		public void BorderInside(LineStyleType borderLine, ExcelColors borderColor)
		{
			int a_ = 2;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_17:
					int num = 3;
					for (;;)
					{
						int num2;
						spr\u24F1 spr_u24F;
						int num3;
						int lastColumn;
						int row;
						int lastRow;
						switch (num)
						{
						case 0:
							if (num2 != this.LastColumn)
							{
								num = 12;
								continue;
							}
							goto IL_9D;
						case 1:
							goto IL_167;
						case 2:
							goto IL_89;
						case 4:
							return;
						case 5:
							spr_u24F.ᜀ(num3, num2);
							spr_u24F.SetBorderToSingleCell(BordersLineType.EdgeBottom, borderLine, borderColor);
							num = 2;
							continue;
						case 6:
							goto IL_84;
						case 7:
							goto IL_167;
						case 8:
							goto IL_145;
						case 9:
							goto IL_9D;
						case 10:
							if (num2 > lastColumn)
							{
								if (true)
								{
								}
								num = 4;
								continue;
							}
							num3 = row;
							num = 13;
							continue;
						case 11:
							if (num3 == this.LastRow)
							{
								goto IL_89;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						case 12:
							spr_u24F.ᜀ(num3, num2);
							spr_u24F.SetBorderToSingleCell(BordersLineType.EdgeRight, borderLine, borderColor);
							num = 9;
							continue;
						case 13:
							goto IL_145;
						case 14:
							num2++;
							num = 7;
							continue;
						case 15:
							if (num3 > lastRow)
							{
								num = 14;
								continue;
							}
							num = 0;
							continue;
						}
						if (this.IsSingleCell)
						{
							num = 6;
							continue;
						}
						int column = this.Column;
						lastColumn = this.LastColumn;
						row = this.Row;
						lastRow = this.LastRow;
						spr_u24F = new spr\u24F1(this.Application, this.\u171D);
						num2 = column;
						num = 1;
						continue;
						IL_89:
						num3++;
						num = 8;
						continue;
						IL_9D:
						num = 11;
						continue;
						IL_145:
						num = 15;
						continue;
						IL_167:
						num = 10;
					}
				}
				IL_84:
				throw new NotSupportedException(RecordTableEnumerator.b("氷刹唻䴽怿⽁⅃㉅⁇╉⡋湍㑏㵑ㅓ╕㙗絙⡛繝፟ᝡᑣᙥݧᡩᡫ乭ᙯᵱٳ噵୷፹ቻ᥽ꒃꂍ", a_));
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000975A4 File Offset: 0x000965A4
		public void BorderNone()
		{
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num3;
					int lastColumn;
					switch (num)
					{
					case 0:
					{
						int num2;
						int lastRow;
						if (num2 > lastRow)
						{
							num = 4;
							continue;
						}
						spr\u24F1 spr_u24F = new spr\u24F1(this.Application, this.\u171D);
						spr_u24F.ᜀ(num2, num3);
						spr_u24F.Borders.LineStyle = LineStyleType.None;
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F3;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					}
					case 1:
						goto IL_101;
					case 2:
						goto IL_73;
					case 3:
					{
						if (num3 > lastColumn)
						{
							num = 8;
							continue;
						}
						int num2 = this.FirstRow;
						int lastRow = this.LastRow;
						num = 2;
						continue;
					}
					case 4:
						num3++;
						num = 5;
						continue;
					case 5:
						goto IL_101;
					case 7:
						goto IL_6E;
					case 8:
						return;
					case 9:
						goto IL_73;
					}
					if (this.IsSingleCell)
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					num3 = this.FirstColumn;
					lastColumn = this.LastColumn;
					goto IL_F3;
					IL_73:
					num = 0;
					continue;
					IL_F3:
					num = 1;
					continue;
					IL_101:
					num = 3;
				}
				IL_6E:
				this.Borders.LineStyle = LineStyleType.None;
				return;
			}
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00097710 File Offset: 0x00096710
		public void SetAutoFormat(AutoFormatType format)
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
			this.SetAutoFormat(format, AutoFormatOptions.All);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00097754 File Offset: 0x00096754
		public void SetAutoFormat(AutoFormatType format, AutoFormatOptions options)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				bool a_2;
				bool a_3;
				for (;;)
				{
					IL_17:
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
							{
								if (false)
								{
								}
								bool flag;
								if (flag)
								{
									num = 3;
									continue;
								}
								goto IL_89;
							}
							}
							break;
						case 1:
							if (true)
							{
							}
							this.ᜀ(format);
							num = 2;
							continue;
						case 2:
							goto IL_163;
						case 3:
							this.ᜂ(format);
							num = 10;
							continue;
						case 4:
							return;
						case 6:
							goto IL_84;
						case 7:
						{
							bool flag2;
							if (flag2)
							{
								num = 12;
								continue;
							}
							goto IL_A3;
						}
						case 8:
							goto IL_A3;
						case 9:
							this.ᜁ(format);
							num = 13;
							continue;
						case 10:
							goto IL_89;
						case 11:
						{
							if (options == AutoFormatOptions.None)
							{
								num = 4;
								continue;
							}
							bool flag2 = (options & AutoFormatOptions.Patterns) == AutoFormatOptions.Patterns;
							bool flag = (options & AutoFormatOptions.Alignment) == AutoFormatOptions.Alignment;
							bool flag3 = (options & AutoFormatOptions.Width_Height) == AutoFormatOptions.Width_Height;
							bool flag4 = (options & AutoFormatOptions.Number) == AutoFormatOptions.Number;
							a_2 = ((options & AutoFormatOptions.Font) == AutoFormatOptions.Font);
							a_3 = ((options & AutoFormatOptions.Border) == AutoFormatOptions.Border);
							num = 7;
							continue;
						}
						case 12:
							this.ᜃ(format);
							num = 8;
							continue;
						case 13:
							goto IL_FF;
						case 14:
						{
							bool flag3;
							if (flag3)
							{
								num = 9;
								continue;
							}
							goto IL_FF;
						}
						case 15:
						{
							bool flag4;
							if (flag4)
							{
								num = 1;
								continue;
							}
							goto IL_1EC;
						}
						}
						if (this.IsSingleCell)
						{
							num = 6;
							continue;
						}
						num = 11;
						continue;
						IL_89:
						num = 14;
						continue;
						IL_A3:
						num = 0;
						continue;
						IL_FF:
						num = 15;
					}
				}
				IL_84:
				throw new NotSupportedException(RecordTableEnumerator.b("眵䴷丹医ḽ☿ⵁ㙃⭅⥇㹉汋⩍㽏㝑❓㡕罗⹙籛ⵝᕟቡୣᑥᱧ䩩իm偯űᵳᡵίᙹ᥻幽ꚇ", a_));
				IL_163:
				IL_1EC:
				this.ᜀ(format, a_2, a_3);
				return;
			}
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00097958 File Offset: 0x00096958
		private void ᜀ(object A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 17;
				for (;;)
				{
					bool? isStringsPreserved;
					bool? flag2;
					bool flag;
					switch (num)
					{
					case 0:
						isStringsPreserved = new bool?(this.\u171D.IsStringsPreserved);
						num = 5;
						continue;
					case 1:
						flag = (flag2 != null);
						goto IL_1C0;
					case 2:
						if (A_0 is TimeSpan)
						{
							num = 9;
							continue;
						}
						num = 13;
						continue;
					case 3:
						goto IL_142;
					case 4:
						num = 1;
						continue;
					case 5:
						goto IL_163;
					case 6:
						goto IL_232;
					case 7:
						if (!flag2.GetValueOrDefault())
						{
							num = 4;
							continue;
						}
						num = 8;
						continue;
					case 8:
						flag = false;
						goto IL_1C0;
					case 9:
						goto IL_D7;
					case 10:
						goto IL_1BB;
					case 11:
						num = 15;
						continue;
					case 12:
						if (A_0 is int)
						{
							num = 10;
							continue;
						}
						goto IL_18E;
					case 13:
						if (A_0 is double)
						{
							num = 3;
							continue;
						}
						num = 12;
						continue;
					case 14:
						isStringsPreserved = this.IsStringsPreserved;
						num = 16;
						continue;
					case 15:
						if (A_0 is DateTime)
						{
							num = 6;
							continue;
						}
						num = 2;
						continue;
					case 16:
						if (isStringsPreserved == null)
						{
							num = 0;
							continue;
						}
						goto IL_163;
					case 17:
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
						break;
					}
					if (true)
					{
					}
					if (A_0 != null)
					{
						num = 14;
						continue;
					}
					goto IL_246;
					IL_163:
					flag2 = isStringsPreserved;
					num = 7;
					continue;
					IL_1C0:
					if (!flag)
					{
						goto IL_147;
					}
					num = 11;
				}
				IL_D7:
				TimeSpan timeSpanValue = (TimeSpan)A_0;
				this.TimeSpanValue = timeSpanValue;
				return;
				IL_142:
				this.NumberValue = (double)A_0;
				return;
				IL_147:
				this.Value = A_0.ToString();
				return;
				IL_18E:
				this.Value = A_0.ToString();
				return;
				IL_1BB:
				this.NumberValue = (double)((int)A_0);
				return;
				IL_232:
				DateTime dateTimeValue = (DateTime)A_0;
				this.DateTimeValue = dateTimeValue;
				return;
				IL_246:
				this.Text = "";
				return;
			}
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00097BB8 File Offset: 0x00096BB8
		public void CollapseGroup(GroupByType groupBy)
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
			this.ᜀ(groupBy, true, ExpandCollapseFlags.Default);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00097BFC File Offset: 0x00096BFC
		public void ExpandGroup(GroupByType groupBy)
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
			this.ExpandGroup(groupBy, ExpandCollapseFlags.Default);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00097C40 File Offset: 0x00096C40
		public void ExpandGroup(GroupByType groupBy, ExpandCollapseFlags flags)
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
			this.ᜀ(groupBy, false, flags);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00097C84 File Offset: 0x00096C84
		public string GetNewRangeLocation(Dictionary<string, string> names, out string sheetName)
		{
			int a_ = 16;
			for (;;)
			{
				sheetName = this.\u171D.Name;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (!names.ContainsKey(sheetName))
						{
							num = 3;
							continue;
						}
						goto IL_94;
					case 2:
						if (names != null)
						{
							num = 0;
							continue;
						}
						goto IL_4D;
					case 3:
						goto IL_6E;
					}
					break;
				}
			}
			IL_4D:
			return this.RangeAddress;
			IL_6E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_94:
				sheetName = names[sheetName];
				return RecordTableEnumerator.b("慅", a_) + sheetName.Replace(RecordTableEnumerator.b("慅", a_), RecordTableEnumerator.b("慅潇", a_)) + RecordTableEnumerator.b("慅楇", a_) + this.RangeAddressLocal;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				goto IL_4D;
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00097D7C File Offset: 0x00096D7C
		public IXLSRange Clone(object parent, Dictionary<string, string> rangeNames, XlsWorkbook book)
		{
			IXLSRange result;
			for (;;)
			{
				IL_48:
				string text = this.\u171D.Name;
				int num = 2;
				for (;;)
				{
					XlsWorksheet xlsWorksheet;
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
							return result;
						case 1:
							goto IL_CA;
						case 2:
							if (rangeNames != null)
							{
								num = 3;
								continue;
							}
							goto IL_9D;
						case 3:
							num = 7;
							continue;
						case 4:
							text = rangeNames[text];
							if (true)
							{
							}
							num = 6;
							continue;
						case 5:
							return result;
						case 6:
							goto IL_9D;
						case 7:
							if (rangeNames.ContainsKey(text))
							{
								num = 4;
								continue;
							}
							goto IL_9D;
						case 8:
							if (xlsWorksheet != null)
							{
								num = 1;
								continue;
							}
							result = this.\u171D.Range[this.FirstRow, this.FirstColumn, this.LastRow, this.LastColumn];
							num = 0;
							continue;
						}
						goto IL_48;
						IL_9D:
						xlsWorksheet = (XlsWorksheet)book.Worksheets[text];
						result = null;
						num = 8;
						continue;
					}
					IL_CA:
					result = xlsWorksheet.Range[this.FirstRow, this.FirstColumn, this.LastRow, this.LastColumn];
					num = 5;
				}
			}
			return result;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00097ED8 File Offset: 0x00096ED8
		public void ClearConditionalFormats()
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
			this.\u171D.ConditionalFormats.Remove(this.GetRectangles());
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00097F2C File Offset: 0x00096F2C
		public Rectangle[] GetRectangles()
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
			return new Rectangle[]
			{
				Rectangle.FromLTRB(this.FirstColumn - 1, this.FirstRow - 1, this.LastColumn - 1, this.LastRow - 1)
			};
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00097FA0 File Offset: 0x00096FA0
		public int GetRectanglesCount()
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
			return 1;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00097FDC File Offset: 0x00096FDC
		public string WorksheetName
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
				return this.Worksheet.Name;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00098024 File Offset: 0x00097024
		internal static string ᜀ(int A_0, int A_1, bool A_2, bool A_3)
		{
			int a_ = 5;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7C;
				case 1:
					goto IL_EA;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (!A_3)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_EC;
					}
					break;
				case 3:
					goto IL_40;
				case 5:
					if (A_2)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				}
				if (A_1 < 1)
				{
					num = 3;
				}
				else
				{
					num = 5;
				}
			}
			IL_40:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("椺刼䠾慀⩂⭄⍆ⱈ㍊浌♎≐獒≔╖㙘㕚㩜煞䅠⩢ᅤ䝦੨੪ͬŮṰݲ啴ᕶᱸ孺ᅼ᩾ꖄ꾎ꂐ", a_));
			IL_7C:
			return sprṔ.ᜀ(A_0) + A_1;
			IL_EA:
			return string.Format(RecordTableEnumerator.b("椺䘼༾㱀B㹄癆㑈", a_), A_1, A_0);
			IL_EC:
			return string.Concat(new object[]
			{
				'$',
				sprṔ.ᜀ(A_0),
				'$',
				A_1
			});
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00098150 File Offset: 0x00097150
		public int ParseRangeString(string range, out int iFirstRow, out int iFirstColumn, out int iLastRow, out int iLastColumn)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num;
				Match match;
				for (;;)
				{
					iLastColumn = (iLastRow = (iFirstColumn = (iFirstRow = -1)));
					string[] array = range.Split(new char[]
					{
						':'
					});
					num = array.Length;
					Regex regex = FormulaUtil.FullRowRangeRegex;
					match = regex.Match(range);
					int num2 = 12;
					for (;;)
					{
						long num3;
						switch (num2)
						{
						case 0:
							goto IL_3CD;
						case 1:
							if (match.Success)
							{
								num2 = 13;
								continue;
							}
							goto IL_107;
						case 2:
							if (match.Index == 0)
							{
								num2 = 3;
								continue;
							}
							goto IL_2B1;
						case 3:
							num2 = 17;
							continue;
						case 4:
							if (num >= 1)
							{
								num2 = 8;
								continue;
							}
							goto IL_E3;
						case 5:
							if (true)
							{
							}
							num2 = 14;
							continue;
						case 6:
							goto IL_E3;
						case 7:
							if (num == 2)
							{
								num2 = 11;
								continue;
							}
							num2 = 21;
							continue;
						case 8:
							num3 = sprṔ.ᜁ(array[0]);
							iLastRow = (iFirstRow = sprṔ.ᜁ(num3));
							iLastColumn = (iFirstColumn = sprṔ.ᜀ(num3));
							num2 = 6;
							continue;
						case 9:
						{
							long num4;
							iLastRow = sprṔ.ᜁ(num4);
							iLastColumn = sprṔ.ᜀ(num4);
							num2 = 18;
							continue;
						}
						case 10:
							goto IL_30D;
						case 11:
						{
							long num4 = sprṔ.ᜁ(array[1]);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num2 = 16;
								continue;
							}
							break;
						}
						case 12:
							if (match.Success)
							{
								num2 = 20;
								continue;
							}
							goto IL_2B1;
						case 13:
							num2 = 19;
							continue;
						case 14:
							if (match.Length == range.Length)
							{
								num2 = 0;
								continue;
							}
							goto IL_107;
						case 15:
							goto IL_1EA;
						case 16:
						{
							long num4;
							if (num3 != num4)
							{
								num2 = 9;
								continue;
							}
							return num;
						}
						case 17:
							if (match.Length == range.Length)
							{
								num2 = 15;
								continue;
							}
							goto IL_2B1;
						case 18:
							goto IL_1BC;
						case 19:
							if (match.Index == 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_107;
						case 20:
							num2 = 2;
							continue;
						case 21:
							if (num > 2)
							{
								num2 = 10;
								continue;
							}
							return num;
						}
						break;
						IL_E3:
						num2 = 7;
						continue;
						IL_107:
						num3 = -1L;
						num2 = 4;
						continue;
						IL_2B1:
						regex = FormulaUtil.FullColumnRangeRegex;
						match = regex.Match(range);
						num2 = 1;
					}
				}
				IL_1BC:
				return num;
				IL_1EA:
				iFirstColumn = 1;
				iLastColumn = this.Workbook.MaxColumnCount;
				string value = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("ᑅ❇㵉絋", a_)].Value);
				string value2 = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("ᑅ❇㵉繋", a_)].Value);
				iFirstRow = Convert.ToInt32(value);
				iLastRow = Convert.ToInt32(value2);
				return num;
				IL_30D:
				throw new ArgumentException();
				IL_3CD:
				string a_2 = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("Յ❇♉㥋⍍㹏捑", a_)].Value);
				string a_3 = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("Յ❇♉㥋⍍㹏恑", a_)].Value);
				iFirstColumn = sprṔ.ᜀ(a_2);
				iLastColumn = sprṔ.ᜀ(a_3);
				iFirstRow = 1;
				iLastRow = this.Workbook.MaxRowCount;
				return num;
			}
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00098530 File Offset: 0x00097530
		protected internal void wrapStyle_OnNumberFormatChanged(object sender, EventArgs e)
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
			XlsRange.TCellType a_ = this.CellType;
			string value = this.Value;
			this.OnValueChanged(value, value);
			this.ᜀ(a_);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00098588 File Offset: 0x00097588
		private void ᜅ()
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
			int num = (int)this.ExtendedFormatIndex;
			spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ(num);
			num = spr_u192F.ᜯ();
			XlsStyle byXFIndex = this.m_book.InnerStyles.GetByXFIndex(num);
			this.ᜀ(byXFIndex, new EventHandler(this.wrapStyle_OnNumberFormatChanged));
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00098608 File Offset: 0x00097608
		private void ᜄ()
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
			this.ᜀ(this.m_style, new EventHandler(this.wrapStyle_OnNumberFormatChanged));
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0009865C File Offset: 0x0009765C
		private void ᜀ(AddtionalFormatWrapper A_0, EventHandler A_1)
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
			A_0.NumberFormatChanged += A_1;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000986A0 File Offset: 0x000976A0
		protected void CreateStyle()
		{
			int value;
			for (;;)
			{
				value = this.m_book.DefaultXFIndex;
				BiffRecordRaw biffRecordRaw = this.Record;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_82;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_80;
						case 1:
						{
							spr\u23A5 spr_u23A = (spr\u23A5)biffRecordRaw;
							value = (int)spr_u23A.ᜆ();
							num = 0;
							continue;
						}
						case 2:
							if (biffRecordRaw != null)
							{
								num = 1;
								continue;
							}
							goto IL_82;
						}
						break;
					}
					break;
				}
				}
			}
			IL_80:
			IL_82:
			this.CreateStyleWrapper(value);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00098738 File Offset: 0x00097738
		protected void CreateStyleWrapper(int value)
		{
			int a_ = 19;
			if (this.IsSingleCell)
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
					CellBaseStyle style = this.m_style;
					this.m_style = new spr\u21A0(this, value);
					return;
				}
				}
			}
			throw new ArgumentException(RecordTableEnumerator.b("Ὀ⩊⅌㩎㑐獒㙔㙖㝘筚㍜ぞᕠ䍢ݤɦ䥨౪ɬ᭮兰Ѳᵴቶ᝸孺ၼ੾권ﾒ璉랖漢쒠킢", a_));
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000987B0 File Offset: 0x000977B0
		public void SetExtendedFormatIndex(int index)
		{
			int a_ = 6;
			int num = 6;
			for (;;)
			{
				spr\u192F spr_u192F;
				switch (num)
				{
				case 0:
					if (index < 0)
					{
						num = 7;
						continue;
					}
					spr_u192F = this.m_book.InnerExtFormats.ᜁ(index);
					if (true)
					{
					}
					num = 5;
					continue;
				case 1:
					goto IL_103;
				case 2:
					if (this.m_style != null)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					spr_u192F = spr_u192F.ᜆ(this.m_book.InnerExtFormats.ᜁ((int)this.ExtendedFormatIndex));
					num = 1;
					continue;
				case 4:
					this.m_style.SetFormatIndex(index);
					num = 8;
					continue;
				case 5:
					if (spr_u192F.ᜑ().ᜎ() == sprỶ.TXFType.XF_CELL)
					{
						num = 3;
						continue;
					}
					spr_u192F = spr_u192F.ᜭ();
					num = 10;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_C8;
					}
					break;
				case 8:
					return;
				case 9:
					goto IL_55;
				case 10:
					goto IL_103;
				}
				if (!this.IsSingleCell)
				{
					num = 9;
					continue;
				}
				num = 0;
				continue;
				IL_103:
				index = spr_u192F.ᜠ();
				this.\u171D.CellRecords.SetCellStyle(this.Row, this.Column, index);
				num = 2;
			}
			IL_55:
			throw new ApplicationException(RecordTableEnumerator.b("樻弽ⰿ㝁⅃晅⭇⭉≋湍㹏㵑⁓癕㩗㽙籛㥝ཟᙡ䑣ᅥgཀྵɫ乭ᵯݱᡳɵᅷ੹ၻ᭽ꁿꪉﺋﺏ", a_));
			IL_C8:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻倽␿❁㱃", a_), RecordTableEnumerator.b("樻弽ⰿ㝁⅃晅⭇⭉≋⁍㽏♑瑓㑕㵗穙せ㭝፟ᅡ䑣ብg୩ɫ乭䁯", a_));
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0009897C File Offset: 0x0009797C
		private XlsStyle ᜀ(string A_0)
		{
			int num;
			XlsStyle xlsStyle;
			for (;;)
			{
				num = this.m_book.DefaultXFIndex;
				xlsStyle = null;
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 8;
						continue;
					case 1:
						goto IL_101;
					case 2:
						if (xlsStyle != null)
						{
							num2 = 14;
							continue;
						}
						goto IL_1EB;
					case 3:
						goto IL_18F;
					case 4:
						num2 = 13;
						continue;
					case 5:
						goto IL_71;
					case 6:
						goto IL_71;
					case 7:
						if (A_0.Length <= 0)
						{
							goto IL_16D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15D;
						default:
							if (false)
							{
							}
							num2 = 11;
							continue;
						}
						break;
					case 8:
						if (true)
						{
						}
						if (this.m_book.Version != ExcelVersion.Version2007)
						{
							num2 = 4;
							continue;
						}
						goto IL_8C;
					case 9:
						if (A_0 != null)
						{
							num2 = 10;
							continue;
						}
						goto IL_16D;
					case 10:
						num2 = 7;
						continue;
					case 11:
						num2 = 15;
						continue;
					case 12:
						goto IL_8C;
					case 13:
						if (this.m_book.Version == ExcelVersion.Version2010)
						{
							num2 = 12;
							continue;
						}
						goto IL_146;
					case 14:
						num = xlsStyle.Index;
						num2 = 1;
						continue;
					case 15:
						if (!this.m_book.InnerStyles.ᜁ(A_0))
						{
							num2 = 0;
							continue;
						}
						goto IL_146;
					}
					break;
					IL_71:
					num2 = 2;
					continue;
					IL_8C:
					Array.IndexOf<string>(XlsStyle.DEF_DEFAULT_STYLES, A_0);
					xlsStyle = this.m_book.InnerStyles.CreateBuiltInStyle(A_0);
					num2 = 5;
					continue;
					IL_15D:
					num2 = 6;
					continue;
					IL_146:
					xlsStyle = (XlsStyle)this.m_book.Styles[A_0];
					goto IL_15D;
					IL_16D:
					xlsStyle = (XlsStyle)this.m_book.Styles[num];
					num2 = 3;
				}
			}
			IL_101:
			IL_18F:
			IL_1EB:
			this.ExtendedFormatIndex = (ushort)num;
			return xlsStyle;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00098B80 File Offset: 0x00097B80
		private string ᜃ()
		{
			int a_ = 8;
			int num = 0;
			XlsStyle xlsStyle;
			for (;;)
			{
				int extendedFormatIndex;
				switch (num)
				{
				case 1:
					if (xlsStyle == null)
					{
						num = 5;
						continue;
					}
					goto IL_13A;
				case 2:
				{
					spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ(extendedFormatIndex);
					xlsStyle = this.m_book.InnerStyles.GetByXFIndex(spr_u192F.ᜯ());
					goto IL_73;
				}
				case 3:
					goto IL_45;
				case 4:
					if (xlsStyle == null)
					{
						num = 2;
						continue;
					}
					goto IL_13A;
				case 5:
					xlsStyle = (this.m_book.InnerStyles[RecordTableEnumerator.b("瀽⼿ぁ⥃❅⑇", a_)] as XlsStyle);
					this.ExtendedFormatIndex = (ushort)xlsStyle.Index;
					num = 6;
					continue;
				case 6:
					goto IL_138;
				}
				if (this.m_style != null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				extendedFormatIndex = (int)this.ExtendedFormatIndex;
				xlsStyle = this.m_book.InnerStyles.GetByXFIndex(extendedFormatIndex);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_73:
				num = 1;
			}
			IL_45:
			return this.m_style.Name;
			IL_138:
			IL_13A:
			return xlsStyle.Name;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00098CD0 File Offset: 0x00097CD0
		internal bool ᜁ(string A_0, out DateTime A_1)
		{
			int a_ = 0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3C;
				case 1:
					if (DateTime.TryParse(A_0, Thread.CurrentThread.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, out A_1))
					{
						num = 4;
						continue;
					}
					goto IL_EA;
				case 3:
					if (this.ExtendedFormat.ᝊ() == 14)
					{
						num = 5;
						continue;
					}
					goto IL_EA;
				case 4:
					return true;
				case 5:
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					string shortDatePattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
					A_1 = DateTime.Now;
					num = 3;
				}
			}
			for (;;)
			{
				IL_3C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_CE;
				}
			}
			IL_CE:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䠹砻弽㐿❁၃⽅╇⽉", a_));
			IL_EA:
			if (true)
			{
			}
			return false;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00098DD0 File Offset: 0x00097DD0
		private bool ᜂ()
		{
			if (true)
			{
			}
			if (this.m_style == null)
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
					return this.ExtendedFormat.\u1733();
				}
			}
			return this.m_style.WrapText;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00098E2C File Offset: 0x00097E2C
		private string ᜁ()
		{
			if (true)
			{
			}
			if (this.m_style == null)
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
					return this.ExtendedFormat.\u1715();
				}
			}
			return this.m_style.NumberFormat;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00098E88 File Offset: 0x00097E88
		private spr\u192F ExtendedFormat
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
				int extendedFormatIndex = (int)this.ExtendedFormatIndex;
				return this.m_book.InnerExtFormats.ᜁ(extendedFormatIndex);
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00098EDC File Offset: 0x00097EDC
		internal void \u171B()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u23A5 spr_u23A = (spr\u23A5)this.Record;
					this.m_style.SetFormatIndex((int)spr_u23A.ᜆ());
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					num = 2;
					continue;
				}
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					return;
				}
				if (this.m_style == null)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00098F6C File Offset: 0x00097F6C
		internal bool ᜀ(Dictionary<spr\u225F, object> A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int lastRow;
					XlsCellRecordCollection cellRecords;
					int num2;
					int num3;
					switch (num)
					{
					case 0:
					{
						if (true)
						{
						}
						spr\u225F spr_u225F;
						if (spr_u225F.\u170D() + 1 <= lastRow)
						{
							num = 21;
							continue;
						}
						return false;
					}
					case 1:
						num = 11;
						continue;
					case 2:
						goto IL_118;
					case 4:
					{
						spr\u225F spr_u225F;
						if (spr_u225F.ᜈ() + 1 >= this.FirstColumn)
						{
							num = 7;
							continue;
						}
						return false;
					}
					case 5:
					{
						spr\u225F spr_u225F;
						if (spr_u225F != null)
						{
							num = 1;
							continue;
						}
						goto IL_1F1;
					}
					case 6:
					{
						spr\u225F spr_u225F;
						int lastColumn;
						if (spr_u225F.ᜀ() + 1 > lastColumn)
						{
							num = 19;
							continue;
						}
						A_0.Add(spr_u225F, null);
						num = 16;
						continue;
					}
					case 7:
						num = 6;
						continue;
					case 8:
					{
						spr\u225F spr_u225F = cellRecords.ᜁ(num2, num3);
						num = 5;
						continue;
					}
					case 9:
						num2++;
						num = 25;
						continue;
					case 10:
						num = 0;
						continue;
					case 11:
					{
						spr\u225F spr_u225F;
						if (!A_0.ContainsKey(spr_u225F))
						{
							num = 14;
							continue;
						}
						goto IL_1F1;
					}
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B7;
						default:
							if (false)
							{
							}
							A_0 = new Dictionary<spr\u225F, object>();
							num = 17;
							continue;
						}
						break;
					case 13:
					{
						spr\u225F spr_u225F;
						if (spr_u225F.ᜉ() + 1 >= this.FirstRow)
						{
							num = 10;
							continue;
						}
						return false;
					}
					case 14:
						num = 13;
						continue;
					case 15:
					{
						int lastColumn;
						if (num3 <= lastColumn)
						{
							num = 8;
							continue;
						}
						goto IL_1F1;
					}
					case 16:
						goto IL_1F1;
					case 17:
						goto IL_13A;
					case 18:
					{
						int lastColumn;
						if (num3 > lastColumn)
						{
							num = 9;
							continue;
						}
						num3 = cellRecords.ᜀ(TBIFFRecord.Formula, num2, num3, lastColumn);
						num = 15;
						continue;
					}
					case 19:
						goto IL_2EF;
					case 20:
						goto IL_167;
					case 21:
						goto IL_1B7;
					case 22:
					{
						if (num2 > lastRow)
						{
							num = 23;
							continue;
						}
						num3 = this.FirstColumn;
						int lastColumn = this.LastColumn;
						num = 24;
						continue;
					}
					case 23:
						return true;
					case 24:
						goto IL_118;
					case 25:
						goto IL_167;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					goto IL_13A;
					IL_118:
					num = 18;
					continue;
					IL_13A:
					cellRecords = this.\u171D.CellRecords;
					num2 = this.FirstRow;
					lastRow = this.LastRow;
					num = 20;
					continue;
					IL_167:
					num = 22;
					continue;
					IL_1B7:
					num = 4;
					continue;
					IL_1F1:
					num3++;
					num = 2;
				}
				return false;
				IL_2EF:
				return false;
			}
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00099270 File Offset: 0x00098270
		public void Reparse()
		{
			XlsRange.TCellType tcellType = this.CellType;
			if (tcellType != XlsRange.TCellType.Formula)
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
			this.ᜂ((spr᱒)this.Record);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000992D4 File Offset: 0x000982D4
		TBIFFRecord spr\u23A5.TypeCode
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
				return (TBIFFRecord)this.CellType;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00099318 File Offset: 0x00098318
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x0009935C File Offset: 0x0009835C
		int spr\u23A5.Column
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
				return this.FirstColumn - 1;
			}
			set
			{
				int a_ = 5;
				if (this.IsSingleCell)
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
						this.FirstColumn = (this.LastColumn = value + 1);
						return;
					}
				}
				throw new ArgumentException(RecordTableEnumerator.b("洺尼匾㑀♂敄⑆⡈╊浌ⅎ㹐❒畔㕖㱘筚㩜ぞᕠ䍢ቤས౨ժ䵬ɮѰὲŴṶॸ᝺᡼彾ꦈ力", a_));
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x000993D0 File Offset: 0x000983D0
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x00099414 File Offset: 0x00098414
		int spr\u23A5.Row
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
				return this.FirstRow - 1;
			}
			set
			{
				int a_ = 18;
				if (this.IsSingleCell)
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
						this.FirstRow = (this.LastRow = value + 1);
						return;
					}
				}
				throw new ArgumentException(RecordTableEnumerator.b("ṇ⭉⁋㭍㕏牑㝓㝕㙗穙㉛ㅝᑟ䉡٣ͥ䡧ᥩ५ᩭ偯ձᱳ፵ᙷ婹ᅻ୽겋ﺑ뚕ﮙ劣얟톡", a_));
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00099488 File Offset: 0x00098488
		public Ptg[] GetNativePtg()
		{
			switch (0)
			{
			default:
			{
				Ptg ptg;
				for (;;)
				{
					IL_27:
					int num = this.m_book.AddSheetReference(this.\u171D.Name);
					for (;;)
					{
						IL_3E:
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (this.IsSingleCell)
								{
									num2 = 1;
									continue;
								}
								ptg = FormulaUtil.ᜀ(FormulaToken.tArea3d1, new object[]
								{
									num,
									this.FirstRow - 1,
									this.FirstColumn - 1,
									this.LastRow - 1,
									this.LastColumn - 1,
									0,
									0
								});
								num2 = 2;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_3E;
								default:
									if (false)
									{
									}
									if (true)
									{
									}
									ptg = FormulaUtil.ᜀ(FormulaToken.tRef3d1, new object[]
									{
										num,
										this.FirstRow - 1,
										this.FirstColumn - 1,
										0
									});
									num2 = 3;
									continue;
								}
								break;
							case 2:
								goto IL_ED;
							case 3:
								goto IL_14F;
							}
							goto IL_27;
						}
					}
				}
				IL_ED:
				IL_14F:
				return new Ptg[]
				{
					ptg
				};
			}
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000995F8 File Offset: 0x000985F8
		public IEnumerator<IXLSRange> GetEnumerator()
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
			return sprᝐ.ᜀ<IXLSRange, CellRange>(this.CellsList).GetEnumerator();
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00099644 File Offset: 0x00098644
		IEnumerator IEnumerable.GetEnumerator()
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
			return ((IEnumerable)this.CellsList).GetEnumerator();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0009968C File Offset: 0x0009868C
		internal void ᜀ(object A_0, object A_1, IXLSRange A_2)
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
			(this.Worksheet as XlsWorksheet).ᜀ(A_0, A_1, A_2);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000996DC File Offset: 0x000986DC
		// Note: this type is marked as 'beforefieldinit'.
		static XlsRange()
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
			XlsRange.\u1718 = new XlsRange.TCellType[]
			{
				XlsRange.TCellType.RK,
				XlsRange.TCellType.Number,
				XlsRange.TCellType.Formula
			};
			XlsRange.\u1719 = new AutoFormatType[]
			{
				AutoFormatType.Classic_2,
				AutoFormatType.Classic_3,
				AutoFormatType.Accounting1,
				AutoFormatType.Accounting2,
				AutoFormatType.Accounting3,
				AutoFormatType.Colorful2,
				AutoFormatType.Colorful3
			};
			XlsRange.\u171A = new AutoFormatType[]
			{
				AutoFormatType.Accounting1,
				AutoFormatType.Accounting2,
				AutoFormatType.Accounting3,
				AutoFormatType.Accounting4
			};
			XlsRange.\u171B = new DateTime(1900, 1, 1, 0, 0, 0, 0).Ticks;
			XlsRange.\u171C = new LineStyleType[]
			{
				LineStyleType.None,
				LineStyleType.Hair,
				LineStyleType.Thin
			};
		}

		// Token: 0x04000B92 RID: 2962
		internal const string ᜀ = "mm/dd/yyyy";

		// Token: 0x04000B93 RID: 2963
		internal const string ᜁ = "h:mm:ss";

		// Token: 0x04000B94 RID: 2964
		internal const string ᜂ = "0.00";

		// Token: 0x04000B95 RID: 2965
		internal const string ᜃ = "@";

		// Token: 0x04000B96 RID: 2966
		internal const string ᜄ = "General";

		// Token: 0x04000B97 RID: 2967
		internal const string ᜅ = "{{{0}}}";

		// Token: 0x04000B98 RID: 2968
		private const string ᜆ = "This method should be called for single cells only.";

		// Token: 0x04000B99 RID: 2969
		internal const string ᜇ = "Normal";

		// Token: 0x04000B9A RID: 2970
		internal const int ᜈ = 15;

		// Token: 0x04000B9B RID: 2971
		private const bool ᜉ = false;

		// Token: 0x04000B9C RID: 2972
		private const string ᜊ = " ";

		// Token: 0x04000B9D RID: 2973
		private const char ᜋ = '$';

		// Token: 0x04000B9E RID: 2974
		private const char ᜌ = 'C';

		// Token: 0x04000B9F RID: 2975
		private const char \u170D = 'R';

		// Token: 0x04000BA0 RID: 2976
		private const char ᜎ = '[';

		// Token: 0x04000BA1 RID: 2977
		private const char ᜏ = ']';

		// Token: 0x04000BA2 RID: 2978
		private const string ᜐ = "R{0}C{1}";

		// Token: 0x04000BA3 RID: 2979
		private const long ᜑ = 31241376000000000L;

		// Token: 0x04000BA4 RID: 2980
		private const int \u1712 = 61;

		// Token: 0x04000BA5 RID: 2981
		private const int \u1713 = 0;

		// Token: 0x04000BA6 RID: 2982
		private const int \u1714 = 43;

		// Token: 0x04000BA7 RID: 2983
		private const int \u1715 = 44;

		// Token: 0x04000BA8 RID: 2984
		private const int \u1716 = 32;

		// Token: 0x04000BA9 RID: 2985
		private const int \u1717 = -1;

		// Token: 0x04000BAA RID: 2986
		private static readonly XlsRange.TCellType[] \u1718;

		// Token: 0x04000BAB RID: 2987
		private static readonly AutoFormatType[] \u1719;

		// Token: 0x04000BAC RID: 2988
		private static readonly AutoFormatType[] \u171A;

		// Token: 0x04000BAD RID: 2989
		private static readonly long \u171B;

		// Token: 0x04000BAE RID: 2990
		private static readonly LineStyleType[] \u171C;

		// Token: 0x04000BAF RID: 2991
		private XlsWorksheet \u171D;

		// Token: 0x04000BB0 RID: 2992
		protected XlsWorkbook m_book;

		// Token: 0x04000BB1 RID: 2993
		protected int m_iLeftColumn;

		// Token: 0x04000BB2 RID: 2994
		protected int m_iRightColumn;

		// Token: 0x04000BB3 RID: 2995
		protected int m_iTopRow;

		// Token: 0x04000BB4 RID: 2996
		protected int m_iBottomRow;

		// Token: 0x04000BB5 RID: 2997
		private bool \u171E;

		// Token: 0x04000BB6 RID: 2998
		private bool \u171F;

		// Token: 0x04000BB7 RID: 2999
		private bool ᜠ;

		// Token: 0x04000BB8 RID: 3000
		private List<CellRange> ᜡ;

		// Token: 0x04000BB9 RID: 3001
		protected CellBaseStyle m_style;

		// Token: 0x04000BBA RID: 3002
		private bool ᜢ;

		// Token: 0x04000BBB RID: 3003
		protected XlsValidationWrapper m_dataValidation;

		// Token: 0x04000BBC RID: 3004
		protected IRTFWrapper m_rtfString;

		// Token: 0x04000BBD RID: 3005
		private char[] ᜣ;

		// Token: 0x04000BBE RID: 3006
		private string[] ᜤ;

		// Token: 0x04000BBF RID: 3007
		private string[] ᜥ;

		// Token: 0x020005FC RID: 1532
		internal enum TCellType
		{
			// Token: 0x04002C53 RID: 11347
			Number = 515,
			// Token: 0x04002C54 RID: 11348
			RK = 638,
			// Token: 0x04002C55 RID: 11349
			LabelSST = 253,
			// Token: 0x04002C56 RID: 11350
			Blank = 513,
			// Token: 0x04002C57 RID: 11351
			Formula = 6,
			// Token: 0x04002C58 RID: 11352
			BoolErr = 517,
			// Token: 0x04002C59 RID: 11353
			RString = 214,
			// Token: 0x04002C5A RID: 11354
			Label = 516
		}

		// Token: 0x020005FD RID: 1533
		// (Invoke) Token: 0x06005A13 RID: 23059
		private delegate spr\u2502 ᜀ(int A_0);

		// Token: 0x020005FE RID: 1534
		// (Invoke) Token: 0x06005A17 RID: 23063
		public delegate void CellValueChangedEventHandler(object sender, CellValueChangedEventArgs e);
	}
}
