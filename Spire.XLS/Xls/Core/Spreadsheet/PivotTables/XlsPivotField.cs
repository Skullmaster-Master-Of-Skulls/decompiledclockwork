using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x02000159 RID: 345
	public class XlsPivotField : ICloneParent, IPivotField
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0009B8B0 File Offset: 0x0009A8B0
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0009B8F8 File Offset: 0x0009A8F8
		public AxisTypes Axis
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
				return this.ᜀ.ᜀ();
			}
			set
			{
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜆ.ᜀ(1);
						num = 13;
						continue;
					case 1:
						goto IL_1C1;
					case 2:
						if (this.ᜀ.ᜀ() == AxisTypes.Page)
						{
							num = 16;
							continue;
						}
						goto IL_1C1;
					case 3:
						if (value == AxisTypes.Page)
						{
							num = 0;
							continue;
						}
						goto IL_9B;
					case 4:
						goto IL_183;
					case 5:
						if (this.ᜀ.ᜀ() == AxisTypes.Row)
						{
							num = 15;
							continue;
						}
						goto IL_183;
					case 6:
						if (!this.ᜆ.Workbook.Loading)
						{
							num = 9;
							continue;
						}
						goto IL_183;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E0;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						if (this.ᜀ.ᜀ() == AxisTypes.Column)
						{
							num = 14;
							continue;
						}
						num = 5;
						continue;
					case 8:
						return;
					case 9:
						this.ᜆ.ᜀ(true);
						num = 2;
						continue;
					case 11:
						num = 6;
						continue;
					case 12:
						goto IL_183;
					case 13:
						goto IL_9B;
					case 14:
						this.ᜆ.ColumnItemsStream = null;
						num = 4;
						continue;
					case 15:
						this.ᜆ.RowItemsStream = null;
						num = 12;
						continue;
					case 16:
						goto IL_1E0;
					}
					if (this.ᜀ.ᜀ() != value)
					{
						num = 11;
						continue;
					}
					break;
					IL_9B:
					num = 7;
					continue;
					IL_183:
					this.ᜆ.ᜀ(this.ᜀ.ᜀ(), this);
					this.ᜀ.ᜀ(value);
					this.ᜆ.ᜀ(value, this, false);
					num = 8;
					continue;
					IL_1C1:
					num = 3;
					continue;
					IL_1E0:
					this.ᜆ.ᜀ(-1);
					num = 1;
				}
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0009BB1C File Offset: 0x0009AB1C
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0009BB64 File Offset: 0x0009AB64
		public string Name
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
				return this.ᜀ.ᜆ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0009BBAC File Offset: 0x0009ABAC
		public XlsPivotCacheField CacheField
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
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0009BBF0 File Offset: 0x0009ABF0
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0009BC34 File Offset: 0x0009AC34
		public bool DataField
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0009BC78 File Offset: 0x0009AC78
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0009BCC0 File Offset: 0x0009ACC0
		public int NumberFormatIndex
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
				return (int)this.ᜁ.ᜃ();
			}
			set
			{
				int a_ = 11;
				if (value >= 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_0D;
					}
					if (false)
					{
					}
					this.ᜁ.ᜂ((ushort)value);
					return;
				}
				IL_0D:
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝀≂⥄㉆ⱈ", a_));
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0009BD2C File Offset: 0x0009AD2C
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0009BD90 File Offset: 0x0009AD90
		public string NumberFormat
		{
			get
			{
				sprᤅ sprᤅ = this.ᜆ.Workbook.InnerFormats.ᜁ(this.NumberFormatIndex);
				if (sprᤅ != null)
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
						return sprᤅ.ᜂ();
					}
				}
				return null;
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
				this.NumberFormatIndex = this.ᜆ.Workbook.InnerFormats.ᜉ(value);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0009BDE8 File Offset: 0x0009ADE8
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x0009BE30 File Offset: 0x0009AE30
		public string SubtotalCaption
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
				return this.ᜁ.ᜑ();
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
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0009BE78 File Offset: 0x0009AE78
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x0009BEC0 File Offset: 0x0009AEC0
		public SubtotalTypes Subtotals
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
				return this.ᜀ.ᜄ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0009BF08 File Offset: 0x0009AF08
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x0009BF50 File Offset: 0x0009AF50
		public bool SubtotalTop
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
				return this.ᜁ.ᜊ();
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
				this.ᜁ.ᜃ(value);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0009BF98 File Offset: 0x0009AF98
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x0009BFE0 File Offset: 0x0009AFE0
		public bool IsAutoShow
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
				return this.ᜁ.ᜇ();
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
				this.ᜁ.ᜊ(value);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0009C028 File Offset: 0x0009B028
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0009C070 File Offset: 0x0009B070
		public bool CanDragToRow
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
				return this.ᜁ.ᜁ();
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
				this.ᜁ.ᜉ(value);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0009C0B8 File Offset: 0x0009B0B8
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0009C100 File Offset: 0x0009B100
		public bool CanDragToColumn
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
				return this.ᜁ.ᜀ();
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
				this.ᜁ.ᜇ(value);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0009C148 File Offset: 0x0009B148
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0009C190 File Offset: 0x0009B190
		public bool CanDragToPage
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
				return this.ᜁ.\u1712();
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
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0009C1D8 File Offset: 0x0009B1D8
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0009C220 File Offset: 0x0009B220
		public bool IsDragToHide
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
				return this.ᜁ.\u1713();
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
				this.ᜁ.ᜅ(value);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0009C268 File Offset: 0x0009B268
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0009C2AC File Offset: 0x0009B2AC
		public bool CanDragOff
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0009C2F0 File Offset: 0x0009B2F0
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0009C334 File Offset: 0x0009B334
		public bool ShowNewItemsInFilter
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0009C378 File Offset: 0x0009B378
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0009C3BC File Offset: 0x0009B3BC
		public bool ShowNewItemsOnRefresh
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0009C400 File Offset: 0x0009B400
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0009C444 File Offset: 0x0009B444
		public bool ShowBlankRow
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0009C488 File Offset: 0x0009B488
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0009C4CC File Offset: 0x0009B4CC
		public bool ShowPageBreak
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
				return this.\u170D;
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
				this.\u170D = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0009C510 File Offset: 0x0009B510
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0009C554 File Offset: 0x0009B554
		public int ItemsPerPage
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0009C598 File Offset: 0x0009B598
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0009C5DC File Offset: 0x0009B5DC
		public bool IsMeasureField
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0009C620 File Offset: 0x0009B620
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x0009C664 File Offset: 0x0009B664
		public bool IsMultiSelected
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
				return this.ᜐ;
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
				this.ᜐ = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0009C6A8 File Offset: 0x0009B6A8
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x0009C6F0 File Offset: 0x0009B6F0
		public bool IsShowAllItems
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
				return this.ᜁ.ᜎ();
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
				this.ᜁ.ᜋ(value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0009C738 File Offset: 0x0009B738
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0009C77C File Offset: 0x0009B77C
		public bool ShowOutline
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
				return this.ᜑ;
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
				this.ᜑ = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0009C7C0 File Offset: 0x0009B7C0
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0009C804 File Offset: 0x0009B804
		public bool ShowDropDown
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
				return this.\u1712;
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
				this.\u1712 = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0009C848 File Offset: 0x0009B848
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0009C88C File Offset: 0x0009B88C
		public bool ShowPropAsCaption
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0009C8D0 File Offset: 0x0009B8D0
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0009C914 File Offset: 0x0009B914
		public bool ShowItemPropAsCaption
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
				return this.\u1714;
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
				this.\u1714 = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0009C958 File Offset: 0x0009B958
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0009C99C File Offset: 0x0009B99C
		public bool ShowToolTip
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
				return this.\u1715;
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
				this.\u1715 = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0009C9E0 File Offset: 0x0009B9E0
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0009CA24 File Offset: 0x0009BA24
		public PivotFieldSortType? SortType
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0009CA68 File Offset: 0x0009BA68
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0009CAAC File Offset: 0x0009BAAC
		public bool IsAutoFiltersByRank
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
				return this.\u1717;
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
				this.\u1717 = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0009CAF0 File Offset: 0x0009BAF0
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x0009CB34 File Offset: 0x0009BB34
		public string Caption
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
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0009CB78 File Offset: 0x0009BB78
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0009CBBC File Offset: 0x0009BBBC
		internal int ItemIndex
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

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0009CC00 File Offset: 0x0009BC00
		internal Dictionary<int, spr\u1B6A> ItemOptions
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1719 = new Dictionary<int, spr\u1B6A>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6F;
					case 2:
						if (true)
						{
						}
						break;
					}
					goto IL_24;
					IL_36:
					num = 0;
					continue;
					IL_24:
					if (this.\u1719 == null)
					{
						goto IL_36;
					}
					break;
				}
				IL_6F:
				return this.\u1719;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0009CC84 File Offset: 0x0009BC84
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0009CCC8 File Offset: 0x0009BCC8
		public bool Compact
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

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0009CD0C File Offset: 0x0009BD0C
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0009CD50 File Offset: 0x0009BD50
		public bool CanDragToData
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0009CD94 File Offset: 0x0009BD94
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0009CDDC File Offset: 0x0009BDDC
		public string Formula
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
				return this.ᜃ.Formula;
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
				this.ᜃ.Formula = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0009CE24 File Offset: 0x0009BE24
		public bool IsFormulaField
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
				return this.ᜃ.IsFormulaField;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0009CE6C File Offset: 0x0009BE6C
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x0009CEB0 File Offset: 0x0009BEB0
		public Stream PreservedAutoSort
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
				return this.\u171A;
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
				this.\u171A = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0009CEF4 File Offset: 0x0009BEF4
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0009CF38 File Offset: 0x0009BF38
		internal bool IsAllDrilled
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
				return this.\u171B;
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
				this.\u171B = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0009CF7C File Offset: 0x0009BF7C
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0009CFC0 File Offset: 0x0009BFC0
		internal bool IsDataSourceSorted
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
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0009D004 File Offset: 0x0009C004
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0009D048 File Offset: 0x0009C048
		internal bool IsDefaultDrill
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
				this.\u171D = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0009D08C File Offset: 0x0009C08C
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0009D0D0 File Offset: 0x0009C0D0
		internal int FieldIndex
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u171F = value;
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0009D114 File Offset: 0x0009C114
		internal XlsPivotField(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 18;
			this.ᜀ = (sprṮ)spr\u175E.ᜀ(TBIFFRecord.PivotViewFields);
			this.ᜁ = (spr\u22BD)spr\u175E.ᜀ(TBIFFRecord.PivotViewFieldsEx);
			this.ᜂ = new List<spr\u1A6A>();
			this.ᜎ = 10;
			base..ctor();
			this.ᜅ = (XlsWorkbook)XlsObject.FindParent(A_1, typeof(XlsWorkbook));
			if (this.ᜅ == null)
			{
				throw new ArgumentException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_));
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0009D1A8 File Offset: 0x0009C1A8
		internal XlsPivotField(XlsPivotTable A_0)
		{
			int a_ = 3;
			this.ᜀ = (sprṮ)spr\u175E.ᜀ(TBIFFRecord.PivotViewFields);
			this.ᜁ = (spr\u22BD)spr\u175E.ᜀ(TBIFFRecord.PivotViewFieldsEx);
			this.ᜂ = new List<spr\u1A6A>();
			this.ᜎ = 10;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䴸娺弼匾⑀", a_));
			}
			this.ᜆ = A_0;
			this.Subtotals = SubtotalTypes.Default;
			this.IsDragToHide = true;
			this.CanDragOff = true;
			this.CanDragToColumn = true;
			this.CanDragToData = true;
			this.CanDragToPage = true;
			this.CanDragToRow = true;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0009D254 File Offset: 0x0009C254
		internal XlsPivotField(XlsWorkbook A_0)
		{
			int a_ = 19;
			this.ᜀ = (sprṮ)spr\u175E.ᜀ(TBIFFRecord.PivotViewFields);
			this.ᜁ = (spr\u22BD)spr\u175E.ᜀ(TBIFFRecord.PivotViewFieldsEx);
			this.ᜂ = new List<spr\u1A6A>();
			this.ᜎ = 10;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("⭈⑊≌⑎", a_));
			}
			this.ᜅ = A_0;
			this.Subtotals = SubtotalTypes.Default;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0009D2D4 File Offset: 0x0009C2D4
		internal XlsPivotField(XlsPivotCacheField A_0, XlsPivotTable A_1) : this(A_1)
		{
			this.ᜀ.ᜀ(A_0.Name);
			this.ᜀ.ᜁ((ushort)A_0.ItemCount);
			this.ᜃ = A_0;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0009D314 File Offset: 0x0009C314
		public int Parse(IList data, int iPos)
		{
			int a_ = 3;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (iPos > data.Count - 1)
					{
						goto IL_1F4;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.PivotViewFields);
					this.ᜀ = (sprṮ)biffRecordRaw;
					iPos++;
					int num2 = 0;
					int num3 = (int)this.ᜀ.ᜅ();
					num = 4;
					continue;
				}
				case 1:
					goto IL_1AD;
				case 2:
					if (iPos >= 0)
					{
						num = 8;
						continue;
					}
					goto IL_1B9;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F4;
					default:
						goto IL_79;
					}
					break;
				case 4:
					goto IL_84;
				case 6:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 10;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.PivotViewItem);
					this.ᜂ.Add((spr\u1A6A)biffRecordRaw);
					iPos++;
					num2++;
					num = 11;
					continue;
				}
				case 7:
				{
					BiffRecordRaw biffRecordRaw;
					this.ᜁ = (spr\u22BD)biffRecordRaw;
					iPos++;
					num = 1;
					continue;
				}
				case 8:
					num = 0;
					continue;
				case 9:
				{
					BiffRecordRaw biffRecordRaw;
					if (biffRecordRaw.TypeCode == TBIFFRecord.PivotViewFieldsEx)
					{
						num = 7;
						continue;
					}
					return iPos;
				}
				case 10:
				{
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
					num = 9;
					continue;
				}
				case 11:
					goto IL_84;
				case 12:
					goto IL_1FF;
				}
				if (true)
				{
				}
				if (data == null)
				{
					num = 3;
					continue;
				}
				num = 2;
				continue;
				IL_84:
				num = 6;
				continue;
				IL_1F4:
				num = 12;
			}
			IL_79:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("崸娺䤼帾", a_));
			IL_1AD:
			return iPos;
			IL_1B9:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤸吺丼", a_), RecordTableEnumerator.b("漸娺儼䨾⑀捂♄♆❈╊≌㭎煐ㅒご睖㕘㹚⹜ⱞ䅠ᝢ൤٦ݨ䭪嵬佮ၰᵲᅴ坶Ṹॺ᡼ṾꞆﶈ놐떚ﺜ풠춢톤覦", a_));
			IL_1FF:
			goto IL_1B9;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0009D524 File Offset: 0x0009C524
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 19;
			while (records == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
				}
			}
			records.ᜀ(this.ᜀ);
			records.AddList(this.ᜂ);
			records.ᜀ(this.ᜁ);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0009D5A8 File Offset: 0x0009C5A8
		internal void ᜀ(int A_0, spr\u1B6A A_1)
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
			this.ItemOptions.Add(A_0, A_1);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0009D5F0 File Offset: 0x0009C5F0
		public void AddItemOption(int index)
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
			this.ItemOptions.Add(index, null);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0009D638 File Offset: 0x0009C638
		public object Clone(object parent)
		{
			XlsPivotField xlsPivotField;
			for (;;)
			{
				IL_30:
				xlsPivotField = (XlsPivotField)base.MemberwiseClone();
				xlsPivotField.ᜆ = (XlsPivotTable)XlsObject.FindParent(parent, typeof(XlsPivotTable));
				XlsPivotCache cache = xlsPivotField.ᜆ.Cache;
				xlsPivotField.ᜀ = (sprṮ)spr\u1CD3.ᜀ(this.ᜀ);
				xlsPivotField.ᜁ = (spr\u22BD)spr\u1CD3.ᜀ(this.ᜁ);
				xlsPivotField.ᜂ = spr\u1CD3.ᜀ<spr\u1A6A>(this.ᜂ);
				int num = 1;
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
							return xlsPivotField;
						case 1:
							if (true)
							{
							}
							if (this.ᜃ != null)
							{
								num = 2;
								continue;
							}
							return xlsPivotField;
						case 2:
						{
							int index = this.ᜃ.Index;
							xlsPivotField.ᜃ = cache.CacheFields.ᜀ(index);
							goto IL_E6;
						}
						}
						goto IL_30;
					}
					IL_E6:
					num = 0;
				}
			}
			return xlsPivotField;
		}

		// Token: 0x04000D7C RID: 3452
		private sprṮ ᜀ;

		// Token: 0x04000D7D RID: 3453
		private long[] \u2593\u00AD\u008C\u008D;

		// Token: 0x04000D7E RID: 3454
		private spr\u22BD ᜁ;

		// Token: 0x04000D7F RID: 3455
		private List<spr\u1A6A> ᜂ;

		// Token: 0x04000D80 RID: 3456
		private XlsPivotCacheField ᜃ;

		// Token: 0x04000D81 RID: 3457
		private bool ᜄ;

		// Token: 0x04000D82 RID: 3458
		private XlsWorkbook ᜅ;

		// Token: 0x04000D83 RID: 3459
		private long \u2609\u009B\u0099\u00A4;

		// Token: 0x04000D84 RID: 3460
		private XlsPivotTable ᜆ;

		// Token: 0x04000D85 RID: 3461
		private bool ᜇ;

		// Token: 0x04000D86 RID: 3462
		private bool ᜈ;

		// Token: 0x04000D87 RID: 3463
		private bool ᜉ;

		// Token: 0x04000D88 RID: 3464
		private bool ᜊ;

		// Token: 0x04000D89 RID: 3465
		private bool ᜋ;

		// Token: 0x04000D8A RID: 3466
		private bool ᜌ;

		// Token: 0x04000D8B RID: 3467
		private bool \u170D;

		// Token: 0x04000D8C RID: 3468
		private int ᜎ;

		// Token: 0x04000D8D RID: 3469
		private bool ᜏ;

		// Token: 0x04000D8E RID: 3470
		private bool ᜐ;

		// Token: 0x04000D8F RID: 3471
		private bool ᜑ;

		// Token: 0x04000D90 RID: 3472
		private bool \u1712;

		// Token: 0x04000D91 RID: 3473
		private bool \u1713;

		// Token: 0x04000D92 RID: 3474
		private bool \u1714;

		// Token: 0x04000D93 RID: 3475
		private bool \u1715;

		// Token: 0x04000D94 RID: 3476
		private PivotFieldSortType? \u1716;

		// Token: 0x04000D95 RID: 3477
		private bool \u1717;

		// Token: 0x04000D96 RID: 3478
		private string \u1718;

		// Token: 0x04000D97 RID: 3479
		private Dictionary<int, spr\u1B6A> \u1719;

		// Token: 0x04000D98 RID: 3480
		private Stream \u171A;

		// Token: 0x04000D99 RID: 3481
		private bool \u171B;

		// Token: 0x04000D9A RID: 3482
		private bool \u171C;

		// Token: 0x04000D9B RID: 3483
		private bool \u171D;

		// Token: 0x04000D9C RID: 3484
		private int \u171E;

		// Token: 0x04000D9D RID: 3485
		private bool[] \u2460\u00A3\u0095\u00A8;

		// Token: 0x04000D9E RID: 3486
		private int \u171F;
	}
}
