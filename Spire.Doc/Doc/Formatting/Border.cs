using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000471 RID: 1137
	public class Border : FormatBase
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x0039D180 File Offset: 0x0039C180
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x0039D1C4 File Offset: 0x0039C1C4
		internal string ColorShemeName
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

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x0039D208 File Offset: 0x0039C208
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x0039D24C File Offset: 0x0039C24C
		internal bool IsRead
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x0039D290 File Offset: 0x0039C290
		// (set) Token: 0x06003E9E RID: 16030 RVA: 0x0039D2D4 File Offset: 0x0039C2D4
		internal Border.BorderPositions BorderPosition
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x0039D318 File Offset: 0x0039C318
		// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x0039D360 File Offset: 0x0039C360
		public Color Color
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
				return (Color)base[1];
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
				base[1] = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x0039D3A8 File Offset: 0x0039C3A8
		// (set) Token: 0x06003EA2 RID: 16034 RVA: 0x0039D3F0 File Offset: 0x0039C3F0
		public float LineWidth
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
				return (float)base[3];
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						base[3] = value;
						int num = 9;
						for (;;)
						{
							switch (num)
							{
							case 0:
								this.BorderType = BorderStyle.Single;
								num = 1;
								continue;
							case 1:
								goto IL_5B;
							case 2:
								return;
							case 3:
								this.ᜀ();
								num = 2;
								continue;
							case 4:
								goto IL_5B;
							case 5:
								if (true)
								{
								}
								this.BorderType = BorderStyle.None;
								num = 4;
								continue;
							case 6:
								if (!this.IsRead)
								{
									num = 3;
									continue;
								}
								return;
							case 7:
								if (this.BorderType == BorderStyle.None)
								{
									num = 0;
									continue;
								}
								goto IL_5B;
							case 8:
								if (this.BorderType != BorderStyle.None)
								{
									num = 5;
									continue;
								}
								goto IL_5B;
							case 9:
								if (value == 0f)
								{
									num = 10;
									continue;
								}
								num = 7;
								continue;
							case 10:
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
							break;
							IL_5B:
							num = 6;
						}
					}
				}
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x0039D524 File Offset: 0x0039C524
		// (set) Token: 0x06003EA4 RID: 16036 RVA: 0x0039D56C File Offset: 0x0039C56C
		public BorderStyle BorderType
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
				return (BorderStyle)base[2];
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
				this.ᜀ(value);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x0039D5B0 File Offset: 0x0039C5B0
		// (set) Token: 0x06003EA6 RID: 16038 RVA: 0x0039D5F8 File Offset: 0x0039C5F8
		public float Space
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
				return (float)base[4];
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
				base[4] = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x0039D640 File Offset: 0x0039C640
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x0039D688 File Offset: 0x0039C688
		public bool Shadow
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
				return (bool)base[5];
			}
			set
			{
				if (true)
				{
				}
				for (;;)
				{
					IL_1C:
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_6F:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						base[5] = value;
						num = 0;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!this.IsRead)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							goto IL_67;
						}
						goto IL_1C;
					}
					IL_67:
					this.ᜀ();
					goto IL_6F;
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x0039D710 File Offset: 0x0039C710
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x0039D758 File Offset: 0x0039C758
		internal bool HasNoneStyle
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
				return (bool)base[6];
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
				base[6] = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x0039D7A0 File Offset: 0x0039C7A0
		internal bool IsBorderDefined
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.HasNoneStyle)
						{
							num = 2;
							continue;
						}
						return false;
					case 1:
						num = 0;
						continue;
					case 2:
						goto IL_7A;
					}
					if (this.BorderType != BorderStyle.None)
					{
						goto IL_7C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				return false;
				IL_7A:
				return base.HasKey(6);
				IL_7C:
				if (true)
				{
				}
				return true;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x0039D834 File Offset: 0x0039C834
		// (set) Token: 0x06003EAD RID: 16045 RVA: 0x0039D878 File Offset: 0x0039C878
		internal bool IsChanged
		{
			[CompilerGenerated]
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
				return this.ᜆ;
			}
			[CompilerGenerated]
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
				this.ᜆ = value;
			}
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x0039D8BC File Offset: 0x0039C8BC
		public Border(FormatBase parent, int baseKey) : base(parent, baseKey)
		{
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0039D8D4 File Offset: 0x0039C8D4
		private Border ᜁ()
		{
			Borders borders2;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						Borders borders = base.OwnerBase as Borders;
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									if (borders.CurrentRow != null)
									{
										num = 4;
										continue;
									}
									goto IL_11D;
								}
								break;
							case 2:
								num = 1;
								continue;
							case 3:
								goto IL_11B;
							case 4:
							{
								if (true)
								{
								}
								RowFormat rowFormat = borders.CurrentRow.RowFormat;
								borders2 = rowFormat.Borders;
								Border.BorderPositions borderPositions = this.BorderPosition;
								num = 5;
								continue;
							}
							case 5:
							{
								Border.BorderPositions borderPositions;
								switch (borderPositions)
								{
								case Border.BorderPositions.Left:
									goto IL_60;
								case Border.BorderPositions.Top:
									goto IL_108;
								case Border.BorderPositions.Right:
									goto IL_59;
								case Border.BorderPositions.Bottom:
									goto IL_B8;
								default:
									num = 0;
									continue;
								}
								break;
							}
							case 6:
								if (borders != null)
								{
									num = 2;
									continue;
								}
								goto IL_11D;
							}
							break;
						}
					}
					break;
				}
			}
			IL_59:
			return borders2.Right;
			IL_60:
			return borders2.Left;
			IL_B8:
			return borders2.Bottom;
			IL_108:
			return borders2.Top;
			IL_11B:
			IL_11D:
			return null;
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0039DA00 File Offset: 0x0039CA00
		internal void ᜀ(Border A_0)
		{
			for (;;)
			{
				IL_1C:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_C1:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					base[2] = A_0.BorderType;
					base[3] = A_0.LineWidth;
					base[1] = A_0.Color;
					base[5] = A_0.Shadow;
					num = 2;
					break;
				}
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
						goto IL_AD;
					case 2:
						if (A_0.BorderType != BorderStyle.Cleared)
						{
							num = 1;
							continue;
						}
						return;
					}
					goto IL_1C;
				}
				IL_AD:
				base[6] = A_0.HasNoneStyle;
				goto IL_C1;
			}
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x0039DADC File Offset: 0x0039CADC
		private void ᜀ()
		{
			switch (0)
			{
			default:
			{
				int num = 9;
				Table table;
				int rowIndex;
				for (;;)
				{
					Borders borders;
					int a_;
					switch (num)
					{
					case 0:
						if (borders.CurrentCell == null)
						{
							num = 6;
							continue;
						}
						a_ = borders.CurrentCell.CellFormat.CurCellIndex;
						num = 3;
						continue;
					case 1:
						goto IL_19C;
					case 2:
						if (base.OwnerBase is Borders)
						{
							num = 7;
							continue;
						}
						return;
					case 3:
						if (this.HasNoneStyle)
						{
							goto IL_17E;
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
							num = 10;
							continue;
						}
						break;
					case 4:
						goto IL_17E;
					case 5:
						if (table == null)
						{
							num = 11;
							continue;
						}
						rowIndex = borders.CurrentRow.GetRowIndex();
						num = 0;
						continue;
					case 6:
						goto IL_F0;
					case 7:
						borders = (base.OwnerBase as Borders);
						num = 8;
						continue;
					case 8:
						if (borders.CurrentRow == null)
						{
							num = 14;
							continue;
						}
						table = borders.CurrentRow.OwnerTable;
						num = 5;
						continue;
					case 10:
						num = 12;
						continue;
					case 11:
						return;
					case 12:
						if (this.BorderType != BorderStyle.None)
						{
							num = 4;
							continue;
						}
						return;
					case 13:
						num = 2;
						continue;
					case 14:
						return;
					}
					goto IL_5C;
					IL_67:
					num = 13;
					continue;
					IL_5C:
					if (base.OwnerBase != null)
					{
						goto IL_67;
					}
					return;
					IL_17E:
					if (true)
					{
					}
					this.ᜀ(table, borders, rowIndex, a_);
					num = 1;
				}
				IL_F0:
				this.ᜀ(table, rowIndex);
				return;
				IL_19C:
				return;
			}
			}
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0039DCC0 File Offset: 0x0039CCC0
		private void ᜀ(Table A_0, Borders A_1, int A_2, int A_3)
		{
			switch (0)
			{
			default:
			{
				int num = 23;
				TableCell tableCell2;
				TableCell tableCell3;
				TableCell tableCell4;
				for (;;)
				{
					Border.BorderPositions borderPositions;
					switch (num)
					{
					case 0:
						num = 15;
						continue;
					case 1:
						return;
					case 2:
					{
						TableCell tableCell = A_0[A_2 + 1, A_3];
						num = 16;
						continue;
					}
					case 3:
						if (tableCell2.CellFormat.Borders.Right.HasNoneStyle)
						{
							num = 19;
							continue;
						}
						return;
					case 4:
						if (tableCell3.CellFormat.Borders.Bottom.HasNoneStyle)
						{
							num = 9;
							continue;
						}
						return;
					case 5:
						num = 10;
						continue;
					case 6:
						if (tableCell4.CellFormat.Borders.Left.HasNoneStyle)
						{
							num = 13;
							continue;
						}
						return;
					case 7:
						tableCell4 = A_0[A_2, A_3 + 1];
						num = 6;
						continue;
					case 8:
						if (A_3 + 1 < A_0.Rows[A_2].Cells.Count)
						{
							num = 7;
							continue;
						}
						return;
					case 9:
						goto IL_125;
					case 10:
						if (A_3 - 1 < A_0.Rows[A_2].Cells.Count)
						{
							num = 18;
							continue;
						}
						return;
					case 11:
						switch (borderPositions)
						{
						case Border.BorderPositions.Left:
							num = 26;
							continue;
						case Border.BorderPositions.Top:
							num = 17;
							continue;
						case Border.BorderPositions.Right:
							num = 8;
							continue;
						case Border.BorderPositions.Bottom:
							goto IL_2BB;
						default:
							num = 12;
							continue;
						}
						break;
					case 12:
						return;
					case 13:
						goto IL_E2;
					case 14:
					{
						TableCell tableCell;
						tableCell.CellFormat.Borders.Top.ᜀ(this);
						num = 24;
						continue;
					}
					case 15:
						if (A_0.Rows[A_2 + 1].Cells.Count > A_3)
						{
							num = 2;
							continue;
						}
						return;
					case 16:
					{
						TableCell tableCell;
						if (tableCell.CellFormat.Borders.Top.HasNoneStyle)
						{
							num = 14;
							continue;
						}
						return;
					}
					case 17:
						if (A_2 > 0)
						{
							num = 25;
							continue;
						}
						return;
					case 18:
						tableCell2 = A_0[A_2, A_3 - 1];
						num = 3;
						continue;
					case 19:
						goto IL_38F;
					case 20:
						if (A_2 + 1 < A_0.Rows.Count)
						{
							num = 0;
							continue;
						}
						return;
					case 21:
						tableCell3 = A_0[A_2 - 1, A_3];
						num = 4;
						continue;
					case 22:
						if (A_0.Rows[A_2 - 1].Cells.Count > A_3)
						{
							num = 21;
							continue;
						}
						return;
					case 24:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2BB;
						default:
							goto IL_323;
						}
						break;
					case 25:
						num = 22;
						continue;
					case 26:
						if (A_3 > 0)
						{
							num = 5;
							continue;
						}
						return;
					}
					if (A_2 == -1)
					{
						num = 1;
						continue;
					}
					borderPositions = this.BorderPosition;
					num = 11;
					continue;
					IL_2BB:
					num = 20;
				}
				return;
				IL_E2:
				if (true)
				{
				}
				tableCell4.CellFormat.Borders.Left.ᜀ(this);
				return;
				IL_125:
				tableCell3.CellFormat.Borders.Bottom.ᜀ(this);
				return;
				IL_323:
				if (false)
				{
				}
				return;
				IL_38F:
				tableCell2.CellFormat.Borders.Right.ᜀ(this);
				return;
			}
			}
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x0039E0B8 File Offset: 0x0039D0B8
		private void ᜀ(Table A_0, int A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Border.BorderPositions borderPositions = this.BorderPosition;
					int num = 7;
					for (;;)
					{
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							goto IL_66;
						case 1:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 2:
									{
										if (!enumerator.MoveNext())
										{
											num = 4;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator.Current;
										tableCell.CellFormat.Borders.Top.BorderType = this.BorderType;
										tableCell.CellFormat.Borders.Top.Color = this.Color;
										tableCell.CellFormat.Borders.Top.LineWidth = this.LineWidth;
										num = 0;
										continue;
									}
									case 3:
										goto IL_2C2;
									case 4:
										num = 3;
										continue;
									}
									IL_290:
									num = 2;
									continue;
									goto IL_290;
								}
								IL_2C2:
								return;
							}
							finally
							{
								for (;;)
								{
									IL_2DC:
									IDisposable disposable;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_320:
										num = 0;
										break;
									default:
										if (false)
										{
										}
										disposable = (enumerator as IDisposable);
										num = 2;
										break;
									}
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_329;
										case 1:
											goto IL_317;
										case 2:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_32B;
										}
										goto IL_2DC;
									}
									IL_317:
									disposable.Dispose();
									goto IL_320;
								}
								IL_329:
								IL_32B:;
							}
							goto IL_32C;
						case 2:
						{
							TableRow tableRow = A_0.Rows[A_1];
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 3;
							continue;
						}
						case 3:
							goto IL_8E;
						case 4:
							goto IL_32C;
						case 5:
							if (A_1 != A_0.Rows.Count - 1)
							{
								num = 2;
								continue;
							}
							return;
						case 6:
							if (A_1 != 0)
							{
								num = 4;
								continue;
							}
							return;
						case 7:
							switch (borderPositions)
							{
							case Border.BorderPositions.Top:
								num = 6;
								continue;
							case Border.BorderPositions.Right:
								return;
							case Border.BorderPositions.Bottom:
								num = 5;
								continue;
							default:
								num = 0;
								continue;
							}
							break;
						}
						break;
						IL_32C:
						if (true)
						{
						}
						TableRow tableRow2 = A_0.Rows[A_1];
						enumerator = tableRow2.Cells.GetEnumerator();
						num = 1;
					}
				}
				IL_66:
				return;
				IL_8E:
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_17D;
						case 3:
							num = 0;
							continue;
						case 4:
						{
							IEnumerator enumerator2;
							if (!enumerator2.MoveNext())
							{
								num = 3;
								continue;
							}
							TableCell tableCell2 = (TableCell)enumerator2.Current;
							tableCell2.CellFormat.Borders.Bottom.BorderType = this.BorderType;
							tableCell2.CellFormat.Borders.Bottom.Color = this.Color;
							tableCell2.CellFormat.Borders.Bottom.LineWidth = this.LineWidth;
							num = 2;
							continue;
						}
						}
						IL_E5:
						num = 4;
						continue;
						goto IL_E5;
					}
					IL_17D:
					return;
				}
				finally
				{
					for (;;)
					{
						IEnumerator enumerator2;
						IDisposable disposable2 = enumerator2 as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1C8;
							case 1:
								disposable2.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_1CA;
							}
							break;
						}
					}
					IL_1C8:
					IL_1CA:;
				}
				return;
			}
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x0039E440 File Offset: 0x0039D440
		private void ᜀ(BorderStyle A_0)
		{
			int a_ = 2;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 27;
					for (;;)
					{
						Color color3;
						switch (num)
						{
						case 0:
							if (this.LineWidth != 0f)
							{
								num = 37;
								continue;
							}
							goto IL_1AF;
						case 1:
							goto IL_441;
						case 2:
							this.ᜀ();
							num = 25;
							continue;
						case 3:
							if ((BorderStyle)base[2] == BorderStyle.None)
							{
								num = 42;
								continue;
							}
							goto IL_441;
						case 4:
							if ((BorderStyle)base[2] != BorderStyle.None)
							{
								num = 48;
								continue;
							}
							goto IL_1DC;
						case 5:
							if (!(this.Color == Color.White))
							{
								num = 13;
								continue;
							}
							goto IL_204;
						case 6:
						{
							Border border;
							if (border != null)
							{
								num = 22;
								continue;
							}
							goto IL_332;
						}
						case 7:
							if (A_0 != BorderStyle.Cleared)
							{
								num = 45;
								continue;
							}
							goto IL_534;
						case 8:
							this.HasNoneStyle = true;
							num = 38;
							continue;
						case 9:
							if (!this.IsRead)
							{
								num = 2;
								continue;
							}
							return;
						case 10:
							if (this.LineWidth == 0f)
							{
								num = 29;
								continue;
							}
							goto IL_4D5;
						case 11:
							goto IL_441;
						case 12:
							if (A_0 == BorderStyle.Cleared)
							{
								num = 26;
								continue;
							}
							num = 3;
							continue;
						case 13:
						{
							Color color = this.Color;
							num = 17;
							continue;
						}
						case 14:
							if (A_0 != BorderStyle.Cleared)
							{
								num = 15;
								continue;
							}
							goto IL_441;
						case 15:
							num = 10;
							continue;
						case 16:
							this.Color = Color.Black;
							num = 44;
							continue;
						case 17:
						{
							Color color;
							if (color.Name.ToLower() == ClipboardData.b("๧౩੫࡭ᙯᑱ", a_))
							{
								num = 35;
								continue;
							}
							goto IL_534;
						}
						case 18:
							num = 5;
							continue;
						case 19:
							goto IL_1AF;
						case 20:
							if (this.LineWidth == 0f)
							{
								num = 23;
								continue;
							}
							goto IL_38D;
						case 21:
							goto IL_1DC;
						case 22:
						{
							Border border;
							Color color2 = border.Color;
							num = 43;
							continue;
						}
						case 23:
							this.LineWidth = 0.5f;
							num = 49;
							continue;
						case 24:
							if (A_0 == BorderStyle.None)
							{
								num = 8;
								continue;
							}
							this.HasNoneStyle = false;
							num = 11;
							continue;
						case 25:
							return;
						case 26:
							goto IL_507;
						case 28:
							if (!(this.Color == Color.Empty))
							{
								num = 18;
								continue;
							}
							goto IL_204;
						case 29:
							this.LineWidth = 0.5f;
							num = 39;
							continue;
						case 30:
							goto IL_244;
						case 31:
							num = 20;
							continue;
						case 32:
							if ((BorderStyle)base[2] == BorderStyle.Cleared)
							{
								num = 21;
								continue;
							}
							goto IL_534;
						case 33:
							if (color3.IsEmpty)
							{
								num = 46;
								continue;
							}
							goto IL_244;
						case 34:
							if (this.Color == Color.Empty)
							{
								num = 16;
								continue;
							}
							goto IL_138;
						case 35:
							goto IL_204;
						case 36:
							num = 12;
							continue;
						case 37:
							this.LineWidth = 0f;
							num = 19;
							continue;
						case 38:
							goto IL_441;
						case 39:
							goto IL_4D5;
						case 40:
							goto IL_332;
						case 41:
							goto IL_534;
						case 42:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								num = 14;
								continue;
							}
							break;
						case 43:
						{
							Color color2;
							if (color2.IsEmpty)
							{
								num = 40;
								continue;
							}
							goto IL_244;
						}
						case 44:
							goto IL_138;
						case 45:
							num = 47;
							continue;
						case 46:
						{
							Border border = this.ᜁ();
							num = 6;
							continue;
						}
						case 47:
							if (A_0 != BorderStyle.None)
							{
								num = 31;
								continue;
							}
							goto IL_534;
						case 48:
							num = 32;
							continue;
						case 49:
							goto IL_38D;
						}
						if (A_0 != BorderStyle.None)
						{
							num = 36;
							continue;
						}
						goto IL_507;
						IL_138:
						this.HasNoneStyle = false;
						num = 1;
						continue;
						IL_1AF:
						color3 = this.Color;
						num = 33;
						continue;
						IL_1DC:
						num = 7;
						continue;
						IL_204:
						this.Color = Color.Black;
						if (true)
						{
						}
						num = 41;
						continue;
						IL_244:
						num = 24;
						continue;
						IL_332:
						this.Color = Color.White;
						num = 30;
						continue;
						IL_38D:
						num = 28;
						continue;
						IL_441:
						num = 4;
						continue;
						IL_4D5:
						num = 34;
						continue;
						IL_507:
						num = 0;
						continue;
						IL_534:
						base[2] = A_0;
						num = 9;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0039E9E4 File Offset: 0x0039D9E4
		public void InitFormatting(Color color, float lineWidth, BorderStyle borderType, bool shadow)
		{
			base[1] = color;
			base[3] = lineWidth;
			base[2] = borderType;
			base[5] = shadow;
			if (borderType != BorderStyle.None)
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
					base[6] = false;
					return;
				}
			}
			base[6] = true;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0039EA74 File Offset: 0x0039DA74
		protected override object GetDefValue(int key)
		{
			int a_ = 19;
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27;
						default:
							goto IL_9D;
						}
						break;
					case 2:
						goto IL_27;
					}
					break;
					IL_27:
					switch (key)
					{
					case 1:
						goto IL_63;
					case 2:
						goto IL_B8;
					case 3:
						goto IL_58;
					case 4:
						goto IL_AD;
					case 5:
						goto IL_51;
					case 6:
						goto IL_6E;
					default:
						num = 0;
						break;
					}
				}
			}
			IL_51:
			return false;
			IL_58:
			return 0f;
			IL_63:
			return Color.Empty;
			IL_6E:
			return false;
			IL_9D:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentException(ClipboardData.b("ቸṺѼ彾Ꞇﮌ﶐朗랖漢쒠", a_));
			IL_AD:
			return 0f;
			IL_B8:
			return BorderStyle.None;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0039EB54 File Offset: 0x0039DB54
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 7;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.HasKey(5))
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						writer.WriteValue(ClipboardData.b("⑬ᱮ≰᭲ᑴ፶ᙸ౺", a_), this.Shadow);
						num = 9;
						continue;
					case 2:
						if (base.HasKey(3))
						{
							num = 7;
							continue;
						}
						goto IL_20D;
					case 3:
						goto IL_20D;
					case 4:
						writer.WriteValue(ClipboardData.b("㹬Ὦၰၲၴ", a_), this.Space);
						num = 13;
						continue;
					case 5:
						if (base.HasKey(2))
						{
							num = 10;
							continue;
						}
						goto IL_17A;
					case 6:
						if (base.HasKey(4))
						{
							num = 4;
							continue;
						}
						goto IL_12B;
					case 7:
						writer.WriteValue(ClipboardData.b("Ⅼٮὰᙲ≴Ṷᵸེᕼ", a_), this.LineWidth);
						num = 3;
						continue;
					case 8:
					{
						Color color = this.Color;
						num = 11;
						continue;
					}
					case 9:
						return;
					case 10:
						writer.WriteValue(ClipboardData.b("⽬nͰᝲၴն⵸ɺർ᩾", a_), this.BorderType);
						num = 16;
						continue;
					case 11:
					{
						Color color;
						if (!color.IsEmpty)
						{
							num = 14;
							continue;
						}
						goto IL_1EC;
					}
					case 12:
						goto IL_1EC;
					case 13:
						goto IL_12B;
					case 14:
						writer.WriteValue(ClipboardData.b("⹬nᵰᱲݴ", a_), this.Color);
						num = 12;
						continue;
					case 15:
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
							if (!base.HasKey(1))
							{
								goto IL_1EC;
							}
							break;
						}
						num = 8;
						continue;
					case 16:
						goto IL_17A;
					}
					break;
					IL_12B:
					num = 0;
					continue;
					IL_17A:
					num = 6;
					continue;
					IL_1EC:
					num = 2;
					continue;
					IL_20D:
					num = 5;
				}
			}
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0039ED98 File Offset: 0x0039DD98
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 17;
			for (;;)
			{
				if (true)
				{
				}
				base.ReadXmlAttributes(reader);
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
							goto IL_1E0;
						default:
							if (false)
							{
							}
							this.Color = reader.ReadColor(ClipboardData.b("㑶ᙸ᝺ቼൾ", a_));
							num = 13;
							continue;
						}
						break;
					case 1:
						goto IL_11B;
					case 2:
						return;
					case 3:
						if (reader.HasAttribute(ClipboardData.b("㕶ᙸॺ᥼᩾힂ﲄ", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_192;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("㭶ၸᕺ᡼⡾", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_1ED;
					case 5:
						goto IL_192;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("⑶ॸ᩺Ṽ᩾", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_11B;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("㑶ᙸ᝺ቼൾ", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_E7;
					case 8:
						this.Shadow = reader.ReadBoolean(ClipboardData.b("㹶੸⡺ᕼṾ", a_));
						goto IL_1E0;
					case 9:
						this.BorderType = (BorderStyle)reader.ReadEnum(ClipboardData.b("㕶ᙸॺ᥼᩾힂ﲄ", a_), typeof(BorderStyle));
						num = 5;
						continue;
					case 10:
						goto IL_1ED;
					case 11:
						this.Space = reader.ReadFloat(ClipboardData.b("⑶ॸ᩺Ṽ᩾", a_));
						num = 1;
						continue;
					case 12:
						if (reader.HasAttribute(ClipboardData.b("㹶੸⡺ᕼṾ", a_)))
						{
							num = 8;
							continue;
						}
						return;
					case 13:
						goto IL_E7;
					case 14:
						this.LineWidth = reader.ReadFloat(ClipboardData.b("㭶ၸᕺ᡼⡾", a_));
						num = 10;
						continue;
					}
					break;
					IL_E7:
					num = 4;
					continue;
					IL_11B:
					num = 12;
					continue;
					IL_192:
					num = 6;
					continue;
					IL_1E0:
					num = 2;
					continue;
					IL_1ED:
					num = 3;
				}
			}
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0039EFF4 File Offset: 0x0039DFF4
		protected override void InitXDLSHolder()
		{
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4C;
					case 1:
						base.XDLSHolder.SkipMe = true;
						num = 0;
						continue;
					}
					if (!base.IsDefault)
					{
						return;
					}
					num = 1;
				}
				IL_4C:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_6A;
				}
			}
			IL_6A:
			if (false)
			{
			}
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0039F074 File Offset: 0x0039E074
		protected override void OnChange(FormatBase format, int propertyKey)
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
			base.OnChange(format, propertyKey);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0039F0B8 File Offset: 0x0039E0B8
		internal override void ApplyBase(FormatBase baseFormat)
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
			base.ApplyBase(baseFormat);
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0039F0FC File Offset: 0x0039E0FC
		internal void ᜁ(Border A_0)
		{
			int num = 17;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Color != this.Color)
					{
						if (true)
						{
						}
						num = 13;
						continue;
					}
					goto IL_24C;
				case 1:
					return;
				case 2:
					A_0.Shadow = this.Shadow;
					num = 7;
					continue;
				case 3:
					if (A_0.HasNoneStyle != this.HasNoneStyle)
					{
						num = 23;
						continue;
					}
					goto IL_275;
				case 4:
					if (A_0.BorderType != this.BorderType)
					{
						num = 10;
						continue;
					}
					goto IL_18C;
				case 5:
					A_0.LineWidth = this.LineWidth;
					num = 20;
					continue;
				case 6:
					goto IL_275;
				case 7:
					goto IL_1C2;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0.IsRead = this.IsRead;
						break;
					}
					num = 16;
					continue;
				case 9:
					goto IL_24C;
				case 10:
					A_0.BorderType = this.BorderType;
					num = 11;
					continue;
				case 11:
					goto IL_18C;
				case 12:
					A_0.BorderPosition = this.BorderPosition;
					num = 14;
					continue;
				case 13:
					A_0.Color = this.Color;
					num = 9;
					continue;
				case 14:
					goto IL_F8;
				case 15:
					if (A_0.IsRead != this.IsRead)
					{
						num = 8;
						continue;
					}
					goto IL_128;
				case 16:
					goto IL_128;
				case 18:
					A_0.Space = this.Space;
					num = 1;
					continue;
				case 19:
					if (A_0.LineWidth != this.LineWidth)
					{
						num = 5;
						continue;
					}
					goto IL_1EE;
				case 20:
					goto IL_1EE;
				case 21:
					if (A_0.Shadow != this.Shadow)
					{
						num = 2;
						continue;
					}
					goto IL_1C2;
				case 22:
					if (A_0.Space != this.Space)
					{
						num = 18;
						continue;
					}
					return;
				case 23:
					A_0.HasNoneStyle = this.HasNoneStyle;
					num = 6;
					continue;
				}
				if (A_0.BorderPosition != this.BorderPosition)
				{
					num = 12;
					continue;
				}
				IL_F8:
				num = 4;
				continue;
				IL_128:
				num = 19;
				continue;
				IL_18C:
				num = 0;
				continue;
				IL_1C2:
				num = 22;
				continue;
				IL_1EE:
				num = 21;
				continue;
				IL_24C:
				num = 3;
				continue;
				IL_275:
				num = 15;
			}
		}

		// Token: 0x04002DAE RID: 11694
		internal new const int ᜀ = 1;

		// Token: 0x04002DAF RID: 11695
		internal const int ᜁ = 2;

		// Token: 0x04002DB0 RID: 11696
		internal new const int ᜂ = 3;

		// Token: 0x04002DB1 RID: 11697
		protected const int SpaceKey = 4;

		// Token: 0x04002DB2 RID: 11698
		protected const int ShadowKey = 5;

		// Token: 0x04002DB3 RID: 11699
		protected const int HasNoneStyleKey = 6;

		// Token: 0x04002DB4 RID: 11700
		private new Border.BorderPositions ᜃ;

		// Token: 0x04002DB5 RID: 11701
		private new bool ᜄ;

		// Token: 0x04002DB6 RID: 11702
		private string ᜅ;

		// Token: 0x04002DB7 RID: 11703
		[CompilerGenerated]
		private bool ᜆ;

		// Token: 0x02000472 RID: 1138
		internal enum BorderPositions
		{
			// Token: 0x04002DB9 RID: 11705
			Left,
			// Token: 0x04002DBA RID: 11706
			Top,
			// Token: 0x04002DBB RID: 11707
			Right,
			// Token: 0x04002DBC RID: 11708
			Bottom,
			// Token: 0x04002DBD RID: 11709
			Vertical = 5,
			// Token: 0x04002DBE RID: 11710
			Horizontal,
			// Token: 0x04002DBF RID: 11711
			DiagonalDown,
			// Token: 0x04002DC0 RID: 11712
			DiagonalUp
		}
	}
}
