using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200010D RID: 269
	public class XlsAutoFilter : IAutoFilter, ICloneParent
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x00076224 File Offset: 0x00075224
		internal XlsAutoFilter(XlsAutoFiltersCollection A_0)
		{
			int a_ = 15;
			this.ᜇ = new Dictionary<IXLSRange, double>();
			this.ᜈ = new List<KeyValuePair<IXLSRange, double>>();
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㕄♆㭈⹊⍌㭎", a_));
			}
			this.ᜀ(A_0);
			this.ᜂ = (sprᱠ)spr\u175E.ᜀ(TBIFFRecord.AutoFilter);
			this.ᜄ = A_0;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00076294 File Offset: 0x00075294
		internal XlsAutoFilter(XlsAutoFiltersCollection A_0, int A_1, int A_2, int A_3) : this(A_0)
		{
			this.ᜃ = this.WorksheetShapes.ᜈ();
			this.ᜃ.LeftColumn = A_1;
			this.ᜃ.RightColumn = A_2 + 1;
			this.ᜃ.TopRow = A_3;
			this.ᜃ.BottomRow = A_3 + 1;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000762F0 File Offset: 0x000752F0
		internal XlsAutoFilter(XlsAutoFiltersCollection A_0, sprᱠ A_1, int A_2, int A_3)
		{
			this.ᜇ = new Dictionary<IXLSRange, double>();
			this.ᜈ = new List<KeyValuePair<IXLSRange, double>>();
			base..ctor();
			this.ᜀ(A_0);
			this.ᜀ(A_1, A_2, A_3);
			this.ᜄ = A_0;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00076334 File Offset: 0x00075334
		private void ᜀ(XlsAutoFiltersCollection A_0)
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
			this.ᜀ = new AutoFilterCondition(A_0);
			this.ᜁ = new AutoFilterCondition(A_0);
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00076388 File Offset: 0x00075388
		public IAutoFilterCondition FirstCondition
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000763CC File Offset: 0x000753CC
		public IAutoFilterCondition SecondCondition
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00076410 File Offset: 0x00075410
		public bool IsFiltered
		{
			get
			{
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.FirstCondition.DataType == FilterDataType.NotUsed)
						{
							num = 7;
							continue;
						}
						return true;
					case 1:
						if (this.SecondCondition.DataType == FilterDataType.NotUsed)
						{
							num = 4;
							continue;
						}
						return true;
					case 2:
						goto IL_9F;
					case 3:
						if (this.Top10Items <= 0)
						{
							goto IL_D5;
						}
						return true;
					case 4:
						goto IL_105;
					case 5:
						num = 9;
						continue;
					case 6:
						goto IL_130;
					case 7:
						num = 1;
						continue;
					case 8:
						if (!this.IsSimple1)
						{
							num = 5;
							continue;
						}
						return true;
					case 9:
						if (this.IsSimple2)
						{
							num = 6;
							continue;
						}
						num = 0;
						continue;
					case 10:
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_D5:
						num = 2;
						continue;
					default:
						if (false)
						{
						}
						if (this.IsTop10Items)
						{
							num = 10;
							continue;
						}
						break;
					}
					IL_9F:
					num = 8;
				}
				return true;
				IL_105:
				if (true)
				{
				}
				return false;
				IL_130:
				return true;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00076554 File Offset: 0x00075554
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0007659C File Offset: 0x0007559C
		public bool IsAnd
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
				return this.ᜂ.ᜇ();
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
				this.ᜂ.ᜅ(value);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000765E4 File Offset: 0x000755E4
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00076630 File Offset: 0x00075630
		public bool IsOR
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
				return !this.ᜂ.ᜇ();
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
				this.ᜂ.ᜅ(!value);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0007667C File Offset: 0x0007567C
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x000766C4 File Offset: 0x000756C4
		public bool IsTop10Percent
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
				return this.ᜂ.ᜄ();
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
				this.ᜂ.ᜁ(value);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0007670C File Offset: 0x0007570C
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00076754 File Offset: 0x00075754
		public bool IsSimple1
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
				return this.ᜂ.ᜊ();
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
				this.ᜂ.ᜂ(value);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0007679C File Offset: 0x0007579C
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000767E4 File Offset: 0x000757E4
		public bool IsSimple2
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
				return this.ᜂ.ᜉ();
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
				this.ᜂ.ᜄ(value);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0007682C File Offset: 0x0007582C
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00076874 File Offset: 0x00075874
		public bool ShowTopItem
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
				return this.ᜂ.\u170D();
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
				this.ᜂ.ᜃ(value);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000768BC File Offset: 0x000758BC
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00076904 File Offset: 0x00075904
		public bool IsTop10Items
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
				return this.ᜂ.ᜅ();
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
				this.ᜂ.ᜀ(value);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0007694C File Offset: 0x0007594C
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00076994 File Offset: 0x00075994
		public int Top10Items
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
				return this.ᜂ.ᜀ();
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
				this.ᜂ.ᜀ(value);
				this.ᜀ();
				this.ᜁ();
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000769E8 File Offset: 0x000759E8
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
				return this.ᜄ.Worksheet;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00076A30 File Offset: 0x00075A30
		internal spr\u1D9B WorksheetShapes
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
				return (spr\u1D9B)this.Worksheet.Shapes;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00076A7C File Offset: 0x00075A7C
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00076AC4 File Offset: 0x00075AC4
		public int Index
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
				return (int)(this.ᜂ.ᜈ() + 1);
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
				this.ᜂ.ᜀ((ushort)value);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00076B0C File Offset: 0x00075B0C
		public bool HasFirstCondition
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6B;
					case 2:
						if (this.ᜀ.DataType != FilterDataType.NotUsed)
						{
							num = 1;
							continue;
						}
						goto IL_6D;
					case 3:
						IL_3A:
						num = 2;
						continue;
					}
					if (!this.IsTop10Items)
					{
						num = 3;
						continue;
					}
					IL_6D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3A;
					default:
						goto IL_83;
					}
				}
				IL_6B:
				return this.ᜀ.DataType != FilterDataType.MatchAllBlanks;
				IL_83:
				if (false)
				{
				}
				if (true)
				{
				}
				return false;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00076BAC File Offset: 0x00075BAC
		public bool HasSecondCondition
		{
			get
			{
				while (this.IsTop10Items)
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
						return false;
					}
				}
				return this.ᜁ.DataType != FilterDataType.NotUsed;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00076C04 File Offset: 0x00075C04
		public bool IsBlanks
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
				return this.ᜂ.ᜋ();
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00076C4C File Offset: 0x00075C4C
		public bool IsNonBlanks
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
				return this.ᜂ.ᜆ();
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00076C94 File Offset: 0x00075C94
		public void Clear()
		{
			int num = 2;
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
						this.ᜃ.Remove();
						this.ᜃ = null;
						goto IL_5C;
					case 1:
						return;
					}
					if (this.ᜃ != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				IL_5C:
				if (true)
				{
				}
				num = 1;
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00076D1C File Offset: 0x00075D1C
		public object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				XlsAutoFilter xlsAutoFilter;
				for (;;)
				{
					XlsAutoFiltersCollection a_ = (XlsAutoFiltersCollection)XlsObject.FindParent(parent, typeof(XlsAutoFiltersCollection));
					xlsAutoFilter = new AutoFilter(a_);
					int num = 5;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
						{
							XlsFormControlShape xlsFormControlShape;
							if (xlsFormControlShape.LeftColumn == this.ᜃ.LeftColumn)
							{
								num = 11;
								continue;
							}
							goto IL_283;
						}
						case 1:
							num = 20;
							continue;
						case 2:
							return xlsAutoFilter;
						case 3:
							goto IL_1B2;
						case 4:
						{
							XlsFormControlShape xlsFormControlShape;
							if (xlsFormControlShape.RightColumn == this.ᜃ.RightColumn)
							{
								num = 24;
								continue;
							}
							goto IL_283;
						}
						case 5:
							if (this.ᜀ != null)
							{
								num = 26;
								continue;
							}
							goto IL_1B2;
						case 6:
							num = 22;
							continue;
						case 7:
							goto IL_18F;
						case 8:
							goto IL_33A;
						case 9:
						{
							spr\u1D9B spr_u1D9B = xlsAutoFilter.WorksheetShapes;
							bool flag = false;
							num2 = 0;
							int count = spr_u1D9B.Count;
							num = 7;
							continue;
						}
						case 10:
							if (this.ᜃ != null)
							{
								goto IL_351;
							}
							return xlsAutoFilter;
						case 11:
							num = 4;
							continue;
						case 12:
						{
							bool flag = true;
							XlsFormControlShape xlsFormControlShape;
							xlsAutoFilter.ᜃ = xlsFormControlShape;
							num = 28;
							continue;
						}
						case 13:
							if (this.ᜁ != null)
							{
								num = 27;
								continue;
							}
							goto IL_167;
						case 14:
						{
							spr\u1D9B spr_u1D9B;
							xlsAutoFilter.ᜃ = (XlsFormControlShape)this.ᜃ.Clone(spr_u1D9B, null, null, true);
							num = 2;
							continue;
						}
						case 15:
							goto IL_20B;
						case 16:
							num = 0;
							continue;
						case 17:
						{
							bool flag;
							if (!flag)
							{
								num = 14;
								continue;
							}
							return xlsAutoFilter;
						}
						case 18:
							xlsAutoFilter.ᜂ = (sprᱠ)this.ᜂ.ᜂ();
							num = 8;
							continue;
						case 19:
							goto IL_167;
						case 20:
						{
							XlsFormControlShape xlsFormControlShape;
							if (xlsFormControlShape.BottomRow == this.ᜃ.BottomRow)
							{
								num = 16;
								continue;
							}
							goto IL_283;
						}
						case 21:
							goto IL_18F;
						case 22:
						{
							XlsFormControlShape xlsFormControlShape;
							if (xlsFormControlShape.TopRow == this.ᜃ.TopRow)
							{
								num = 1;
								continue;
							}
							goto IL_283;
						}
						case 23:
						{
							int count;
							if (num2 >= count)
							{
								num = 15;
								continue;
							}
							spr\u1D9B spr_u1D9B;
							XlsFormControlShape xlsFormControlShape = spr_u1D9B[num2] as XlsFormControlShape;
							num = 30;
							continue;
						}
						case 24:
							num = 25;
							continue;
						case 25:
						{
							XlsFormControlShape xlsFormControlShape;
							if (xlsFormControlShape.ShapeType != this.ᜃ.ShapeType)
							{
								goto IL_283;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_351;
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
							break;
						}
						case 26:
							xlsAutoFilter.ᜀ = this.ᜀ.Clone(xlsAutoFilter);
							num = 3;
							continue;
						case 27:
							xlsAutoFilter.ᜁ = this.ᜁ.Clone(xlsAutoFilter);
							num = 19;
							continue;
						case 28:
							goto IL_20B;
						case 29:
							if (this.ᜂ != null)
							{
								num = 18;
								continue;
							}
							goto IL_33A;
						case 30:
						{
							XlsFormControlShape xlsFormControlShape;
							if (xlsFormControlShape != null)
							{
								num = 6;
								continue;
							}
							goto IL_283;
						}
						}
						break;
						IL_167:
						num = 29;
						continue;
						IL_18F:
						num = 23;
						continue;
						IL_1B2:
						num = 13;
						continue;
						IL_20B:
						num = 17;
						continue;
						IL_283:
						num2++;
						num = 21;
						continue;
						IL_33A:
						num = 10;
						continue;
						IL_351:
						num = 9;
					}
				}
				return xlsAutoFilter;
			}
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00077110 File Offset: 0x00076110
		internal void ᜀ()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				for (;;)
				{
					IXLSRange range = this.Worksheet.AutoFilters.Range;
					IXLSRange ixlsrange = this.Worksheet.Range[range.Row + 1, this.ᜅ, range.LastRow, this.ᜅ];
					IEnumerator enumerator = ixlsrange.GetEnumerator();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_140;
						case 1:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 1:
										goto IL_F2;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											num = 4;
											continue;
										}
										IXLSRange ixlsrange2 = (IXLSRange)enumerator.Current;
										this.ᜇ.Add(ixlsrange2, ixlsrange2.NumberValue);
										num = 0;
										continue;
									}
									case 4:
										num = 1;
										continue;
									}
									IL_CC:
									num = 3;
									continue;
									goto IL_CC;
								}
								IL_F2:
								goto IL_1D9;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_13D;
										case 1:
											disposable.Dispose();
											num = 0;
											continue;
										case 2:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_13F;
										}
										break;
									}
								}
								IL_13D:
								IL_13F:;
							}
							goto Block_2;
						case 2:
							goto IL_203;
						}
						break;
						IL_1D9:
						Dictionary<IXLSRange, double>.Enumerator enumerator2 = this.ᜇ.GetEnumerator();
						num = 0;
						continue;
						Block_2:
						try
						{
							IL_140:
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator2.MoveNext())
									{
										num = 2;
										continue;
									}
									KeyValuePair<IXLSRange, double> item = enumerator2.Current;
									this.ᜈ.Add(item);
									num = 3;
									continue;
								}
								case 1:
									goto IL_1C9;
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
										num = 1;
										continue;
									}
									break;
								}
								IL_187:
								num = 0;
								continue;
								goto IL_187;
							}
							IL_1C9:
							goto IL_1F7;
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_1D9;
						IL_1F7:
						num = 2;
					}
				}
				IL_203:
				List<KeyValuePair<IXLSRange, double>> list = this.ᜈ;
				if (XlsAutoFilter.ᜉ == null)
				{
					XlsAutoFilter.ᜉ = new Comparison<KeyValuePair<IXLSRange, double>>(XlsAutoFilter.ᜀ);
				}
				list.Sort(XlsAutoFilter.ᜉ);
				return;
			}
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00077374 File Offset: 0x00076374
		internal void ᜁ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = this.ᜈ.Count;
					int num = 4;
					for (;;)
					{
						int num2;
						int num4;
						KeyValuePair<IXLSRange, double> keyValuePair;
						switch (num)
						{
						case 0:
							goto IL_29A;
						case 1:
							((XlsWorksheet)this.ᜈ[num2].Key.Worksheet).ᜂ(this.ᜈ[num2].Key.Row, false);
							num = 5;
							continue;
						case 2:
							goto IL_29A;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_295;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								XlsCellRecordCollection cellRecords;
								if (!cellRecords.Table.ᜄ().ᜁ(this.ᜈ[num2].Key.Row - 1).ᜅ())
								{
									num = 1;
									continue;
								}
								goto IL_146;
							}
							}
							break;
						case 4:
						{
							if (this.IsTop10Items)
							{
								num = 6;
								continue;
							}
							int num3 = this.Top10Items - 1;
							this.ᜆ = this.ᜈ[num3].Value;
							num4 = num3 + 1;
							int num5 = count;
							num = 0;
							continue;
						}
						case 5:
							goto IL_146;
						case 6:
						{
							int num3 = count - this.Top10Items;
							this.ᜆ = this.ᜈ[num3].Value;
							num4 = 0;
							int num5 = num3 + 1;
							num = 2;
							continue;
						}
						case 7:
							goto IL_21B;
						case 8:
							if (keyValuePair.Value != this.ᜆ)
							{
								num = 12;
								continue;
							}
							goto IL_146;
						case 9:
							goto IL_295;
						case 10:
						{
							int num5;
							if (num2 >= num5)
							{
								num = 14;
								continue;
							}
							XlsCellRecordCollection cellRecords = this.Worksheet.CellRecords;
							num = 13;
							continue;
						}
						case 11:
							goto IL_21B;
						case 12:
							num = 3;
							continue;
						case 13:
						{
							XlsCellRecordCollection cellRecords;
							if (cellRecords.Table.ᜄ().ᜁ(this.ᜈ[num2].Key.Row - 1) != null)
							{
								num = 9;
								continue;
							}
							goto IL_146;
						}
						case 14:
							return;
						}
						break;
						IL_146:
						num2++;
						num = 7;
						continue;
						IL_21B:
						num = 10;
						continue;
						IL_295:
						keyValuePair = this.ᜈ[num2];
						num = 8;
						continue;
						IL_29A:
						num2 = num4;
						num = 11;
					}
				}
				return;
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00077630 File Offset: 0x00076630
		internal void ᜀ(FilterConditionType A_0, FilterDataType A_1, object A_2, int A_3)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					FilterConditionType conditionOperator = this.ᜄ[A_3].SecondCondition.ConditionOperator;
					this.ᜄ.Worksheet.\u1713();
					int num = 73;
					for (;;)
					{
						int num2;
						KeyValuePair<IXLSRange, double> keyValuePair2;
						int num3;
						int num4;
						int num5;
						int num6;
						int num7;
						int num8;
						KeyValuePair<IXLSRange, double> keyValuePair9;
						int num9;
						KeyValuePair<IXLSRange, double> keyValuePair21;
						KeyValuePair<IXLSRange, double> keyValuePair24;
						switch (num)
						{
						case 0:
							goto IL_1BE4;
						case 1:
							goto IL_8F7;
						case 2:
						{
							KeyValuePair<IXLSRange, double> keyValuePair = this.ᜈ[num2];
							num = 64;
							continue;
						}
						case 3:
							num = 81;
							continue;
						case 4:
						{
							if (((XlsRange)keyValuePair2.Key).DisplayedText == "")
							{
								num = 132;
								continue;
							}
							keyValuePair2 = this.ᜈ[num3];
							XlsWorksheet xlsWorksheet = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num3];
							xlsWorksheet.ᜂ(keyValuePair2.Key.Row, true);
							num = 127;
							continue;
						}
						case 5:
							((XlsWorksheet)this.ᜈ[num4].Key.Worksheet).ᜂ(this.ᜈ[num4].Key.Row, false);
							num = 19;
							continue;
						case 6:
						{
							KeyValuePair<IXLSRange, double> keyValuePair3 = this.ᜈ[num5];
							num = 72;
							continue;
						}
						case 7:
						{
							KeyValuePair<IXLSRange, double> keyValuePair4 = this.ᜈ[num4];
							num = 10;
							continue;
						}
						case 8:
							goto IL_10EF;
						case 9:
							keyValuePair2 = this.ᜈ[num6];
							num = 93;
							continue;
						case 10:
						{
							KeyValuePair<IXLSRange, double> keyValuePair4;
							if (keyValuePair4.Key.HasFormula)
							{
								num = 172;
								continue;
							}
							KeyValuePair<IXLSRange, double> keyValuePair5 = this.ᜈ[num4];
							num = 146;
							continue;
						}
						case 11:
							goto IL_1DD5;
						case 12:
							goto IL_1AF7;
						case 13:
						{
							KeyValuePair<IXLSRange, double> keyValuePair6;
							if (Convert.ToDouble(((XlsRange)keyValuePair6.Key).EnvalutedValue) >= Convert.ToDouble(A_2))
							{
								num = 134;
								continue;
							}
							goto IL_EE5;
						}
						case 14:
							goto IL_CF4;
						case 15:
							if (keyValuePair2.Key.Value == "")
							{
								num = 184;
								continue;
							}
							goto IL_1DD5;
						case 16:
							keyValuePair2 = this.ᜈ[num3];
							num = 40;
							continue;
						case 17:
							goto IL_1C39;
						case 18:
							goto IL_909;
						case 19:
							if (true)
							{
							}
							goto IL_777;
						case 20:
							if (num7 >= this.ᜈ.Count)
							{
								num = 128;
								continue;
							}
							num = 56;
							continue;
						case 21:
							((XlsWorksheet)this.ᜈ[num5].Key.Worksheet).ᜂ(this.ᜈ[num5].Key.Row, true);
							num = 137;
							continue;
						case 22:
							if (num2 >= this.ᜈ.Count)
							{
								num = 3;
								continue;
							}
							num = 168;
							continue;
						case 23:
							if (keyValuePair2.Value < Convert.ToDouble(A_2))
							{
								num = 61;
								continue;
							}
							goto IL_13F9;
						case 24:
							keyValuePair2 = this.ᜈ[num8];
							num = 187;
							continue;
						case 25:
						{
							KeyValuePair<IXLSRange, double> keyValuePair7 = this.ᜈ[num6];
							num = 51;
							continue;
						}
						case 26:
							goto IL_19DE;
						case 27:
						{
							KeyValuePair<IXLSRange, double> keyValuePair8;
							if (keyValuePair8.Key.Value == "")
							{
								num = 107;
								continue;
							}
							goto IL_EE5;
						}
						case 28:
						{
							if (keyValuePair9.Key.HasFormula)
							{
								num = 192;
								continue;
							}
							KeyValuePair<IXLSRange, double> keyValuePair10 = this.ᜈ[num4];
							num = 59;
							continue;
						}
						case 29:
							goto IL_AF5;
						case 30:
							((XlsWorksheet)this.ᜈ[num4].Key.Worksheet).ᜂ(this.ᜈ[num4].Key.Row, true);
							num = 55;
							continue;
						case 31:
							goto IL_FF4;
						case 32:
							goto IL_1399;
						case 33:
							if (num5 >= this.ᜈ.Count)
							{
								num = 169;
								continue;
							}
							num = 75;
							continue;
						case 34:
							keyValuePair2 = this.ᜈ[num3];
							num = 4;
							continue;
						case 35:
							((XlsWorksheet)this.ᜈ[num2].Key.Worksheet).ᜂ(this.ᜈ[num2].Key.Row, true);
							num = 148;
							continue;
						case 36:
							num = 129;
							continue;
						case 37:
						{
							KeyValuePair<IXLSRange, double> keyValuePair11;
							if (keyValuePair11.Value < Convert.ToDouble(A_2))
							{
								num = 113;
								continue;
							}
							goto IL_1DD5;
						}
						case 38:
							goto IL_1794;
						case 39:
							if (num6 >= this.ᜈ.Count)
							{
								num = 74;
								continue;
							}
							num = 154;
							continue;
						case 40:
							if (keyValuePair2.Key.HasFormula)
							{
								num = 136;
								continue;
							}
							keyValuePair2 = this.ᜈ[num3];
							num = 44;
							continue;
						case 41:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 181;
								continue;
							}
							goto IL_15A7;
						case 42:
							goto IL_10EF;
						case 43:
							if (keyValuePair2.Key.Value != string.Empty)
							{
								num = 142;
								continue;
							}
							goto IL_10EF;
						case 44:
							if (keyValuePair2.Key.Value == Convert.ToString(A_2))
							{
								num = 143;
								continue;
							}
							goto IL_1794;
						case 45:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 96;
								continue;
							}
							goto IL_E50;
						case 46:
						{
							keyValuePair2 = this.ᜈ[num8];
							XlsWorksheet xlsWorksheet2 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num8];
							xlsWorksheet2.ᜂ(keyValuePair2.Key.Row, false);
							num = 12;
							continue;
						}
						case 47:
							goto IL_671;
						case 48:
							goto IL_AF5;
						case 49:
							goto IL_777;
						case 50:
							goto IL_1DD5;
						case 51:
						{
							KeyValuePair<IXLSRange, double> keyValuePair7;
							if (Convert.ToDouble(((XlsRange)keyValuePair7.Key).EnvalutedValue) < Convert.ToDouble(A_2))
							{
								num = 114;
								continue;
							}
							goto IL_1DD5;
						}
						case 52:
							if (keyValuePair2.Key.Value == "")
							{
								num = 174;
								continue;
							}
							goto IL_1AF7;
						case 53:
							num = 195;
							continue;
						case 54:
							goto IL_1794;
						case 55:
							goto IL_777;
						case 56:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 53;
								continue;
							}
							goto IL_CF9;
						case 57:
							goto IL_1AF7;
						case 58:
							goto IL_1C45;
						case 59:
						{
							KeyValuePair<IXLSRange, double> keyValuePair10;
							if (keyValuePair10.Value <= Convert.ToDouble(A_2))
							{
								num = 124;
								continue;
							}
							goto IL_78C;
						}
						case 60:
							keyValuePair2 = this.ᜈ[num2];
							num = 83;
							continue;
						case 61:
							keyValuePair2 = this.ᜈ[num6];
							num = 15;
							continue;
						case 62:
						{
							KeyValuePair<IXLSRange, double> keyValuePair12;
							if (Convert.ToDouble(((XlsRange)keyValuePair12.Key).EnvalutedValue) <= Convert.ToDouble(A_2))
							{
								num = 30;
								continue;
							}
							goto IL_777;
						}
						case 63:
							if (keyValuePair2.Key.Value == string.Empty)
							{
								num = 171;
								continue;
							}
							goto IL_10EF;
						case 64:
						{
							KeyValuePair<IXLSRange, double> keyValuePair;
							if (Convert.ToDouble(((XlsRange)keyValuePair.Key).EnvalutedValue) < Convert.ToDouble(A_2))
							{
								num = 76;
								continue;
							}
							goto IL_EE5;
						}
						case 65:
							goto IL_487;
						case 66:
							goto IL_19DE;
						case 67:
							num = 89;
							continue;
						case 68:
							num = 105;
							continue;
						case 69:
						{
							KeyValuePair<IXLSRange, double> keyValuePair13;
							if (keyValuePair13.Key.Value == string.Empty)
							{
								num = 158;
								continue;
							}
							goto IL_777;
						}
						case 70:
							goto IL_EE5;
						case 71:
							if (keyValuePair2.Key.Value != string.Empty)
							{
								num = 80;
								continue;
							}
							goto IL_19DE;
						case 72:
						{
							KeyValuePair<IXLSRange, double> keyValuePair3;
							if (Convert.ToDouble(((XlsRange)keyValuePair3.Key).EnvalutedValue) <= Convert.ToDouble(A_2))
							{
								num = 115;
								continue;
							}
							goto IL_8F7;
						}
						case 73:
							switch (A_0)
							{
							case FilterConditionType.Less:
								num6 = 0;
								num = 31;
								continue;
							case FilterConditionType.Equal:
								num = 185;
								continue;
							case FilterConditionType.LessOrEqual:
								num4 = 0;
								num = 160;
								continue;
							case FilterConditionType.Greater:
								num5 = 0;
								num = 159;
								continue;
							case FilterConditionType.NotEqual:
								num = 157;
								continue;
							case FilterConditionType.GreaterOrEqual:
								num2 = 0;
								num = 65;
								continue;
							default:
								num = 166;
								continue;
							}
							break;
						case 74:
							num = 150;
							continue;
						case 75:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 196;
								continue;
							}
							goto IL_404;
						case 76:
							((XlsWorksheet)this.ᜈ[num2].Key.Worksheet).ᜂ(this.ᜈ[num2].Key.Row, false);
							num = 70;
							continue;
						case 77:
						{
							keyValuePair2 = this.ᜈ[num3];
							XlsWorksheet xlsWorksheet3 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num3];
							xlsWorksheet3.ᜂ(keyValuePair2.Key.Row, true);
							num = 38;
							continue;
						}
						case 78:
							goto IL_BAC;
						case 79:
							if (keyValuePair2.Key.HasFormula)
							{
								num = 9;
								continue;
							}
							keyValuePair2 = this.ᜈ[num6];
							num = 23;
							continue;
						case 80:
						{
							keyValuePair2 = this.ᜈ[num7];
							XlsWorksheet xlsWorksheet4 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num7];
							xlsWorksheet4.ᜂ(keyValuePair2.Key.Row, true);
							num = 66;
							continue;
						}
						case 81:
							goto IL_C08;
						case 82:
						{
							KeyValuePair<IXLSRange, double> keyValuePair14;
							if (keyValuePair14.Key.HasFormula)
							{
								num = 25;
								continue;
							}
							KeyValuePair<IXLSRange, double> keyValuePair11 = this.ᜈ[num6];
							num = 37;
							continue;
						}
						case 83:
						{
							if (keyValuePair2.Key.HasFormula)
							{
								num = 86;
								continue;
							}
							KeyValuePair<IXLSRange, double> keyValuePair15 = this.ᜈ[num2];
							num = 186;
							continue;
						}
						case 84:
							goto IL_1AF7;
						case 85:
						{
							KeyValuePair<IXLSRange, double> keyValuePair16 = this.ᜈ[num5];
							num = 116;
							continue;
						}
						case 86:
						{
							KeyValuePair<IXLSRange, double> keyValuePair6 = this.ᜈ[num2];
							num = 13;
							continue;
						}
						case 87:
						{
							keyValuePair2 = this.ᜈ[num6];
							XlsWorksheet xlsWorksheet5 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num6];
							xlsWorksheet5.ᜂ(keyValuePair2.Key.Row, false);
							num = 111;
							continue;
						}
						case 88:
							keyValuePair2 = this.ᜈ[num8];
							num = 170;
							continue;
						case 89:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 60;
								continue;
							}
							goto IL_C1E;
						case 90:
							goto IL_1837;
						case 91:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 98;
								continue;
							}
							goto IL_1D46;
						case 92:
							goto IL_1C63;
						case 93:
							if (Convert.ToDouble(((XlsRange)keyValuePair2.Key).EnvalutedValue) >= Convert.ToDouble(A_2))
							{
								num = 87;
								continue;
							}
							goto IL_1DD5;
						case 94:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 68;
								continue;
							}
							goto IL_18ED;
						case 95:
							goto IL_487;
						case 96:
							keyValuePair2 = this.ᜈ[num9];
							num = 63;
							continue;
						case 97:
							goto IL_C19;
						case 98:
							num = 100;
							continue;
						case 99:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 109;
								continue;
							}
							goto IL_E50;
						case 100:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 88;
								continue;
							}
							goto IL_1D46;
						case 101:
							if (num9 >= this.ᜈ.Count)
							{
								num = 36;
								continue;
							}
							num = 99;
							continue;
						case 102:
							if (keyValuePair2.Key.Value == string.Empty)
							{
								num = 108;
								continue;
							}
							goto IL_19DE;
						case 103:
							num7 = 0;
							num = 48;
							continue;
						case 104:
							goto IL_F98;
						case 105:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 16;
								continue;
							}
							goto IL_18ED;
						case 106:
							goto IL_8F7;
						case 107:
							goto IL_1279;
						case 108:
						{
							keyValuePair2 = this.ᜈ[num7];
							XlsWorksheet xlsWorksheet6 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num7];
							xlsWorksheet6.ᜂ(keyValuePair2.Key.Row, false);
							num = 26;
							continue;
						}
						case 109:
							num = 45;
							continue;
						case 110:
							goto IL_144B;
						case 111:
							goto IL_1DD5;
						case 112:
							goto IL_777;
						case 113:
						{
							XlsWorksheet xlsWorksheet7 = (XlsWorksheet)this.ᜈ[num6].Key.Worksheet;
							keyValuePair2 = this.ᜈ[num6];
							xlsWorksheet7.ᜂ(keyValuePair2.Key.Row, true);
							num = 11;
							continue;
						}
						case 114:
							((XlsWorksheet)this.ᜈ[num6].Key.Worksheet).ᜂ(this.ᜈ[num6].Key.Row, true);
							num = 50;
							continue;
						case 115:
							((XlsWorksheet)this.ᜈ[num5].Key.Worksheet).ᜂ(this.ᜈ[num5].Key.Row, false);
							num = 106;
							continue;
						case 116:
						{
							KeyValuePair<IXLSRange, double> keyValuePair16;
							if (keyValuePair16.Key.HasFormula)
							{
								num = 163;
								continue;
							}
							KeyValuePair<IXLSRange, double> keyValuePair17 = this.ᜈ[num5];
							num = 140;
							continue;
						}
						case 117:
							if (A_1 == FilterDataType.String)
							{
								num = 153;
								continue;
							}
							goto IL_1399;
						case 118:
							goto IL_1794;
						case 119:
							goto IL_14D9;
						case 120:
							if (num4 >= this.ᜈ.Count)
							{
								num = 177;
								continue;
							}
							num = 41;
							continue;
						case 121:
							goto IL_FF4;
						case 122:
							keyValuePair2 = this.ᜈ[num7];
							num = 71;
							continue;
						case 123:
							goto IL_1C39;
						case 124:
						{
							KeyValuePair<IXLSRange, double> keyValuePair13 = this.ᜈ[num4];
							num = 69;
							continue;
						}
						case 125:
							goto IL_1AF7;
						case 126:
							if (Convert.ToDouble(((XlsRange)keyValuePair2.Key).EnvalutedValue) == Convert.ToDouble(A_2))
							{
								num = 46;
								continue;
							}
							goto IL_1AF7;
						case 127:
							goto IL_1794;
						case 128:
							num = 0;
							continue;
						case 129:
							goto IL_7E5;
						case 130:
						{
							KeyValuePair<IXLSRange, double> keyValuePair18;
							if (keyValuePair18.Value > Convert.ToDouble(A_2))
							{
								num = 165;
								continue;
							}
							goto IL_BAC;
						}
						case 131:
							goto IL_EE5;
						case 132:
							goto IL_A4B;
						case 133:
							if (Convert.ToDouble(((XlsRange)keyValuePair2.Key).DisplayedText) == Convert.ToDouble(A_2))
							{
								num = 77;
								continue;
							}
							goto IL_1794;
						case 134:
							((XlsWorksheet)this.ᜈ[num2].Key.Worksheet).ᜂ(this.ᜈ[num2].Key.Row, true);
							num = 131;
							continue;
						case 135:
						{
							keyValuePair2 = this.ᜈ[num8];
							XlsWorksheet xlsWorksheet8 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num8];
							xlsWorksheet8.ᜂ(keyValuePair2.Key.Row, true);
							num = 125;
							continue;
						}
						case 136:
							keyValuePair2 = this.ᜈ[num3];
							num = 133;
							continue;
						case 137:
							goto IL_8F7;
						case 138:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 7;
								continue;
							}
							goto IL_15A7;
						case 139:
							keyValuePair2 = this.ᜈ[num8];
							num = 52;
							continue;
						case 140:
						{
							KeyValuePair<IXLSRange, double> keyValuePair17;
							if (keyValuePair17.Value > Convert.ToDouble(A_2))
							{
								num = 175;
								continue;
							}
							goto IL_8F7;
						}
						case 141:
						{
							KeyValuePair<IXLSRange, double> keyValuePair8 = this.ᜈ[num2];
							num = 27;
							continue;
						}
						case 142:
						{
							keyValuePair2 = this.ᜈ[num9];
							XlsWorksheet xlsWorksheet9 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num9];
							xlsWorksheet9.ᜂ(keyValuePair2.Key.Row, false);
							num = 42;
							continue;
						}
						case 143:
						{
							keyValuePair2 = this.ᜈ[num3];
							XlsWorksheet xlsWorksheet10 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num3];
							xlsWorksheet10.ᜂ(keyValuePair2.Key.Row, true);
							num = 54;
							continue;
						}
						case 144:
							num = 97;
							continue;
						case 145:
							((XlsWorksheet)this.ᜈ[num4].Key.Worksheet).ᜂ(this.ᜈ[num4].Key.Row, true);
							num = 112;
							continue;
						case 146:
						{
							KeyValuePair<IXLSRange, double> keyValuePair5;
							if (keyValuePair5.Value <= Convert.ToDouble(A_2))
							{
								num = 145;
								continue;
							}
							goto IL_777;
						}
						case 147:
							if (keyValuePair2.Value != Convert.ToDouble(A_2))
							{
								num = 135;
								continue;
							}
							goto IL_1AF7;
						case 148:
							goto IL_EE5;
						case 149:
							goto IL_144B;
						case 150:
							goto IL_EE0;
						case 151:
							goto IL_8F7;
						case 152:
						{
							KeyValuePair<IXLSRange, double> keyValuePair14 = this.ᜈ[num6];
							num = 82;
							continue;
						}
						case 153:
							this.IsSimple1 = true;
							num = 32;
							continue;
						case 154:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 176;
								continue;
							}
							goto IL_1075;
						case 155:
						{
							KeyValuePair<IXLSRange, double> keyValuePair19;
							if (keyValuePair19.Key.Value == "")
							{
								num = 78;
								continue;
							}
							goto IL_8F7;
						}
						case 156:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 85;
								continue;
							}
							goto IL_404;
						case 157:
							if (A_1 == FilterDataType.MatchAllNonBlanks)
							{
								num = 103;
								continue;
							}
							num8 = 0;
							num = 17;
							continue;
						case 158:
							goto IL_78C;
						case 159:
							goto IL_909;
						case 160:
							goto IL_1837;
						case 161:
							goto IL_14D9;
						case 162:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 152;
								continue;
							}
							goto IL_1075;
						case 163:
						{
							KeyValuePair<IXLSRange, double> keyValuePair20 = this.ᜈ[num5];
							num = 180;
							continue;
						}
						case 164:
							if (keyValuePair2.Value != Convert.ToDouble(A_2))
							{
								num = 139;
								continue;
							}
							goto IL_1E3E;
						case 165:
						{
							KeyValuePair<IXLSRange, double> keyValuePair19 = this.ᜈ[num5];
							num = 155;
							continue;
						}
						case 166:
							num = 47;
							continue;
						case 167:
							if (keyValuePair21.Key.HasFormula)
							{
								num = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1C45;
							default:
							{
								if (false)
								{
								}
								KeyValuePair<IXLSRange, double> keyValuePair18 = this.ᜈ[num5];
								num = 130;
								continue;
							}
							}
							break;
						case 168:
							if (conditionOperator != (FilterConditionType)0)
							{
								num = 67;
								continue;
							}
							goto IL_C1E;
						case 169:
							num = 14;
							continue;
						case 170:
							if (keyValuePair2.Key.HasFormula)
							{
								num = 24;
								continue;
							}
							keyValuePair2 = this.ᜈ[num8];
							num = 147;
							continue;
						case 171:
						{
							keyValuePair2 = this.ᜈ[num9];
							XlsWorksheet xlsWorksheet11 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num9];
							xlsWorksheet11.ᜂ(keyValuePair2.Key.Row, true);
							num = 8;
							continue;
						}
						case 172:
						{
							KeyValuePair<IXLSRange, double> keyValuePair12 = this.ᜈ[num4];
							num = 62;
							continue;
						}
						case 173:
						{
							KeyValuePair<IXLSRange, double> keyValuePair22;
							if (keyValuePair22.Value >= Convert.ToDouble(A_2))
							{
								num = 141;
								continue;
							}
							goto IL_1279;
						}
						case 174:
							goto IL_1E3E;
						case 175:
							((XlsWorksheet)this.ᜈ[num5].Key.Worksheet).ᜂ(this.ᜈ[num5].Key.Row, true);
							num = 1;
							continue;
						case 176:
							num = 162;
							continue;
						case 177:
							num = 104;
							continue;
						case 178:
						{
							KeyValuePair<IXLSRange, double> keyValuePair23;
							if (Convert.ToDouble(((XlsRange)keyValuePair23.Key).EnvalutedValue) > Convert.ToDouble(A_2))
							{
								num = 5;
								continue;
							}
							goto IL_777;
						}
						case 179:
						{
							keyValuePair2 = this.ᜈ[num8];
							XlsWorksheet xlsWorksheet12 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
							keyValuePair2 = this.ᜈ[num8];
							xlsWorksheet12.ᜂ(keyValuePair2.Key.Row, true);
							num = 84;
							continue;
						}
						case 180:
						{
							KeyValuePair<IXLSRange, double> keyValuePair20;
							if (Convert.ToDouble(((XlsRange)keyValuePair20.Key).EnvalutedValue) > Convert.ToDouble(A_2))
							{
								num = 21;
								continue;
							}
							goto IL_8F7;
						}
						case 181:
							num = 138;
							continue;
						case 182:
							if (!(((XlsRange)keyValuePair2.Key).DisplayedText != Convert.ToString(A_2)))
							{
								num = 34;
								continue;
							}
							goto IL_A4B;
						case 183:
							if (keyValuePair2.Key.HasFormula)
							{
								num = 191;
								continue;
							}
							keyValuePair2 = this.ᜈ[num8];
							num = 164;
							continue;
						case 184:
							goto IL_13F9;
						case 185:
							if (A_1 == FilterDataType.MatchAllBlanks)
							{
								num = 189;
								continue;
							}
							num = 117;
							continue;
						case 186:
						{
							KeyValuePair<IXLSRange, double> keyValuePair15;
							if (keyValuePair15.Value >= Convert.ToDouble(A_2))
							{
								num = 35;
								continue;
							}
							goto IL_EE5;
						}
						case 187:
							if (Convert.ToDouble(((XlsRange)keyValuePair2.Key).EnvalutedValue) != Convert.ToDouble(A_2))
							{
								num = 179;
								continue;
							}
							goto IL_1AF7;
						case 188:
							goto IL_1DD5;
						case 189:
							this.IsSimple1 = true;
							num9 = 0;
							num = 161;
							continue;
						case 190:
						{
							if (keyValuePair24.Key.HasFormula)
							{
								num = 2;
								continue;
							}
							KeyValuePair<IXLSRange, double> keyValuePair22 = this.ᜈ[num2];
							num = 173;
							continue;
						}
						case 191:
							keyValuePair2 = this.ᜈ[num8];
							num = 126;
							continue;
						case 192:
						{
							KeyValuePair<IXLSRange, double> keyValuePair23 = this.ᜈ[num4];
							num = 178;
							continue;
						}
						case 193:
							if (num3 >= this.ᜈ.Count)
							{
								num = 144;
								continue;
							}
							num = 94;
							continue;
						case 194:
							goto IL_EE5;
						case 195:
							if (!this.ᜄ[A_3].IsAnd)
							{
								num = 122;
								continue;
							}
							goto IL_CF9;
						case 196:
							num = 156;
							continue;
						}
						break;
						IL_404:
						keyValuePair21 = this.ᜈ[num5];
						num = 167;
						continue;
						IL_487:
						num = 22;
						continue;
						IL_777:
						num4++;
						num = 90;
						continue;
						IL_78C:
						((XlsWorksheet)this.ᜈ[num4].Key.Worksheet).ᜂ(this.ᜈ[num4].Key.Row, false);
						num = 49;
						continue;
						IL_8F7:
						num5++;
						num = 18;
						continue;
						IL_909:
						num = 33;
						continue;
						IL_A4B:
						keyValuePair2 = this.ᜈ[num3];
						XlsWorksheet xlsWorksheet13 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
						keyValuePair2 = this.ᜈ[num3];
						xlsWorksheet13.ᜂ(keyValuePair2.Key.Row, false);
						num = 118;
						continue;
						IL_AF5:
						num = 20;
						continue;
						IL_BAC:
						((XlsWorksheet)this.ᜈ[num5].Key.Worksheet).ᜂ(this.ᜈ[num5].Key.Row, false);
						num = 151;
						continue;
						IL_C1E:
						keyValuePair24 = this.ᜈ[num2];
						num = 190;
						continue;
						IL_CF9:
						keyValuePair2 = this.ᜈ[num7];
						num = 102;
						continue;
						IL_1C45:
						if (num8 >= this.ᜈ.Count)
						{
							num = 92;
							continue;
						}
						num = 91;
						continue;
						IL_E50:
						keyValuePair2 = this.ᜈ[num9];
						num = 43;
						continue;
						IL_EE5:
						num2++;
						num = 95;
						continue;
						IL_FF4:
						num = 39;
						continue;
						IL_1075:
						keyValuePair2 = this.ᜈ[num6];
						num = 79;
						continue;
						IL_10EF:
						num9++;
						num = 119;
						continue;
						IL_1279:
						((XlsWorksheet)this.ᜈ[num2].Key.Worksheet).ᜂ(this.ᜈ[num2].Key.Row, false);
						num = 194;
						continue;
						IL_1399:
						num3 = 0;
						num = 110;
						continue;
						IL_13F9:
						keyValuePair2 = this.ᜈ[num6];
						XlsWorksheet xlsWorksheet14 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
						keyValuePair2 = this.ᜈ[num6];
						xlsWorksheet14.ᜂ(keyValuePair2.Key.Row, false);
						num = 188;
						continue;
						IL_144B:
						num = 193;
						continue;
						IL_14D9:
						num = 101;
						continue;
						IL_15A7:
						keyValuePair9 = this.ᜈ[num4];
						num = 28;
						continue;
						IL_1794:
						num3++;
						num = 149;
						continue;
						IL_1837:
						num = 120;
						continue;
						IL_18ED:
						keyValuePair2 = this.ᜈ[num3];
						num = 182;
						continue;
						IL_19DE:
						num7++;
						num = 29;
						continue;
						IL_1AF7:
						num8++;
						num = 123;
						continue;
						IL_1C39:
						num = 58;
						continue;
						IL_1D46:
						keyValuePair2 = this.ᜈ[num8];
						num = 183;
						continue;
						IL_1DD5:
						num6++;
						num = 121;
						continue;
						IL_1E3E:
						keyValuePair2 = this.ᜈ[num8];
						XlsWorksheet xlsWorksheet15 = (XlsWorksheet)keyValuePair2.Key.Worksheet;
						keyValuePair2 = this.ᜈ[num8];
						xlsWorksheet15.ᜂ(keyValuePair2.Key.Row, false);
						num = 57;
					}
				}
				IL_671:
				IL_7E5:
				IL_C08:
				IL_C19:
				IL_CF4:
				IL_EE0:
				IL_F98:
				IL_1BE4:
				IL_1C63:
				this.ᜄ.Worksheet.ᜑ();
				return;
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0007951C File Offset: 0x0007851C
		internal void ᜀ(sprᱠ A_0, int A_1, int A_2)
		{
			switch (0)
			{
			default:
			{
				XlsFormControlShape xlsFormControlShape;
				for (;;)
				{
					this.ᜂ = (sprᱠ)A_0.ᜂ();
					this.ᜀ.ᜁ(A_0.ᜁ());
					this.ᜁ.ᜁ(A_0.ᜏ());
					spr\u1D9B spr_u1D9B = this.WorksheetShapes;
					int num = 0;
					int count = spr_u1D9B.Count;
					int num2 = 9;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_11E;
						case 1:
							if (xlsFormControlShape.Top == A_2)
							{
								num2 = 6;
								continue;
							}
							goto IL_8D;
						case 2:
							if (xlsFormControlShape.LeftColumn == A_1)
							{
								num2 = 3;
								continue;
							}
							goto IL_8D;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8B;
							default:
								if (false)
								{
								}
								num2 = 1;
								continue;
							}
							break;
						case 4:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IShape shape = spr_u1D9B[num];
							num2 = 5;
							continue;
						}
						case 5:
						{
							if (true)
							{
							}
							IShape shape;
							if (shape is XlsFormControlShape)
							{
								num2 = 7;
								continue;
							}
							goto IL_8D;
						}
						case 6:
							goto IL_C1;
						case 7:
						{
							IShape shape;
							xlsFormControlShape = (shape as XlsFormControlShape);
							num2 = 2;
							continue;
						}
						case 8:
							goto IL_102;
						case 9:
							goto IL_8B;
						}
						break;
						IL_8D:
						num++;
						num2 = 8;
						continue;
						IL_102:
						num2 = 4;
						continue;
						IL_8B:
						goto IL_102;
					}
				}
				IL_C1:
				this.ᜃ = xlsFormControlShape;
				return;
				IL_11E:
				this.ᜃ = this.WorksheetShapes.ᜈ();
				this.ᜃ.LeftColumn = A_1;
				this.ᜃ.TopRow = A_2;
				return;
			}
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000796CC File Offset: 0x000786CC
		public void SerializeDataToList(RecordArrayList records)
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
			this.ᜀ.ᜀ(this.ᜂ.ᜁ());
			this.ᜁ.ᜀ(this.ᜂ.ᜏ());
			records.ᜀ(this.ᜂ);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00079740 File Offset: 0x00078740
		[CompilerGenerated]
		private static int ᜀ(KeyValuePair<IXLSRange, double> A_0, KeyValuePair<IXLSRange, double> A_1)
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
			return A_0.Value.CompareTo(A_1.Value);
		}

		// Token: 0x04000A06 RID: 2566
		private XlsAutoFilterCondition ᜀ;

		// Token: 0x04000A07 RID: 2567
		private XlsAutoFilterCondition ᜁ;

		// Token: 0x04000A08 RID: 2568
		private sprᱠ ᜂ;

		// Token: 0x04000A09 RID: 2569
		private bool[] \u2593\u0096\u00A2\u009B;

		// Token: 0x04000A0A RID: 2570
		private XlsFormControlShape ᜃ;

		// Token: 0x04000A0B RID: 2571
		private byte \u25D9\u0092\u00AB\u007F;

		// Token: 0x04000A0C RID: 2572
		private long[] \u2593\u0087\u00AF\u00A8;

		// Token: 0x04000A0D RID: 2573
		private string[] \u2609\u00A2\u008C\u008C;

		// Token: 0x04000A0E RID: 2574
		private XlsAutoFiltersCollection ᜄ;

		// Token: 0x04000A0F RID: 2575
		internal int ᜅ;

		// Token: 0x04000A10 RID: 2576
		internal double ᜆ;

		// Token: 0x04000A11 RID: 2577
		private Dictionary<IXLSRange, double> ᜇ;

		// Token: 0x04000A12 RID: 2578
		private string \u2460\u00A9\u009E\u008C;

		// Token: 0x04000A13 RID: 2579
		private List<KeyValuePair<IXLSRange, double>> ᜈ;

		// Token: 0x04000A14 RID: 2580
		[CompilerGenerated]
		private static Comparison<KeyValuePair<IXLSRange, double>> ᜉ;
	}
}
