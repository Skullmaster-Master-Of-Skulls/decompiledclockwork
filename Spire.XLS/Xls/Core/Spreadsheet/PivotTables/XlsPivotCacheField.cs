using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x0200022A RID: 554
	public class XlsPivotCacheField
	{
		// Token: 0x060021B7 RID: 8631 RVA: 0x0012EFF8 File Offset: 0x0012DFF8
		internal XlsPivotCacheField()
		{
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0012F054 File Offset: 0x0012E054
		internal XlsPivotCacheField(sprἛ A_0)
		{
			this.ᜀ(A_0);
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x0012F0B8 File Offset: 0x0012E0B8
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x0012F0FC File Offset: 0x0012E0FC
		public string Formula
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜈ = value;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0012F140 File Offset: 0x0012E140
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x0012F184 File Offset: 0x0012E184
		public bool IsDataBaseField
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0012F1C8 File Offset: 0x0012E1C8
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x0012F210 File Offset: 0x0012E210
		public bool IsInIndexList
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
				return this.ᜁ.ᜆ();
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
				this.ᜁ.ᜄ(value);
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0012F258 File Offset: 0x0012E258
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x0012F2A0 File Offset: 0x0012E2A0
		public bool IsDouble
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
				return this.ᜁ.ᜃ();
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

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0012F2E8 File Offset: 0x0012E2E8
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x0012F330 File Offset: 0x0012E330
		public bool IsDoubleInt
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
				return this.ᜁ.ᜉ();
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
				this.ᜁ.ᜈ(value);
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0012F378 File Offset: 0x0012E378
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x0012F3C0 File Offset: 0x0012E3C0
		public bool IsString
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
				return this.ᜁ.ᜄ();
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

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0012F408 File Offset: 0x0012E408
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x0012F450 File Offset: 0x0012E450
		public bool IsUnknown
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
				return this.ᜁ.ᜌ();
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
				this.ᜁ.ᜇ(value);
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0012F498 File Offset: 0x0012E498
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x0012F4E0 File Offset: 0x0012E4E0
		public bool IsLongIndex
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜁ.ᜂ(value);
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0012F528 File Offset: 0x0012E528
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x0012F570 File Offset: 0x0012E570
		public bool IsUnknown2
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
				this.ᜁ.ᜆ(value);
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0012F5B8 File Offset: 0x0012E5B8
		public bool IsDate
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
				return this.ᜁ.ᜂ();
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0012F600 File Offset: 0x0012E600
		public int ItemCount
		{
			get
			{
				if (this.ᜐ != null)
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
						return this.ᜐ.Count;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0012F654 File Offset: 0x0012E654
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x0012F698 File Offset: 0x0012E698
		internal IXLSRange ItemRange
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜑ = value;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x0012F6DC File Offset: 0x0012E6DC
		internal IList<object> Items
		{
			get
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
				{
					if (false)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜐ = new List<object>();
							num = 2;
							continue;
						case 2:
							goto IL_6F;
						}
						if (this.ᜐ != null)
						{
							break;
						}
						num = 0;
					}
					break;
				}
				}
				IL_6F:
				return this.ᜐ;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0012F760 File Offset: 0x0012E760
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x0012F7A8 File Offset: 0x0012E7A8
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
				return this.ᜁ.ᜏ();
			}
			set
			{
				int a_ = 3;
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
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_5A;
						case 1:
							goto IL_88;
						case 2:
							if (value.Length == 0)
							{
								num = 1;
								continue;
							}
							goto IL_9E;
						}
						if (value == null)
						{
							num = 0;
						}
						else
						{
							num = 2;
						}
					}
					IL_5A:
					throw new ArgumentNullException(RecordTableEnumerator.b("伸娺儼䨾⑀", a_));
					IL_9E:
					if (true)
					{
					}
					this.ᜁ.ᜀ(value);
					return;
				}
				}
				IL_88:
				throw new ArgumentException(RecordTableEnumerator.b("伸娺儼䨾⑀捂桄杆㩈㽊㽌♎㽐㑒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ", a_));
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0012F868 File Offset: 0x0012E868
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0012F8AC File Offset: 0x0012E8AC
		public int Index
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

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0012F8F0 File Offset: 0x0012E8F0
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x0012F934 File Offset: 0x0012E934
		public PivotDataType DataType
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
				return this.ᜆ;
			}
			internal set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0012F978 File Offset: 0x0012E978
		public bool IsFormulaField
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
				return this.Formula != null;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0012F9C0 File Offset: 0x0012E9C0
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0012FA04 File Offset: 0x0012EA04
		internal spr\u1920 FieldGroup
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜉ = value;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0012FA48 File Offset: 0x0012EA48
		internal spr\u1920 InternalFieldGroup
		{
			get
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
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_70;
						case 2:
							this.ᜉ = new spr\u1920(this);
							num = 1;
							continue;
						}
						if (this.ᜉ != null)
						{
							break;
						}
						num = 2;
					}
					break;
				}
				}
				IL_70:
				return this.ᜉ;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x0012FAD0 File Offset: 0x0012EAD0
		// (set) Token: 0x060021DB RID: 8667 RVA: 0x0012FB14 File Offset: 0x0012EB14
		public string Caption
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x0012FB58 File Offset: 0x0012EB58
		// (set) Token: 0x060021DD RID: 8669 RVA: 0x0012FB9C File Offset: 0x0012EB9C
		public int NumFormatIndex
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

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x0012FBE0 File Offset: 0x0012EBE0
		internal spr\u23FD CalculatedItems
		{
			get
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
				{
					if (false)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_6F;
						case 2:
							this.ᜄ = new spr\u23FD();
							num = 1;
							continue;
						}
						if (this.ᜄ != null)
						{
							break;
						}
						num = 2;
					}
					break;
				}
				}
				IL_6F:
				return this.ᜄ;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0012FC64 File Offset: 0x0012EC64
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x0012FCA8 File Offset: 0x0012ECA8
		internal int ParentFeildGroupIndex
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜌ = value;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0012FCEC File Offset: 0x0012ECEC
		public bool IsFieldGroup
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
				return this.ᜉ != null;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0012FD34 File Offset: 0x0012ED34
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x0012FD78 File Offset: 0x0012ED78
		internal int Hierarchy
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

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0012FDBC File Offset: 0x0012EDBC
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x0012FE00 File Offset: 0x0012EE00
		internal int Level
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

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0012FE44 File Offset: 0x0012EE44
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x0012FE88 File Offset: 0x0012EE88
		internal bool? IsParsed
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0012FECC File Offset: 0x0012EECC
		public object GetValue(int index)
		{
			int a_ = 10;
			for (;;)
			{
				IL_09:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						if (index >= this.ItemCount)
						{
							num = 0;
							continue;
						}
						goto IL_CA;
					case 3:
						goto IL_6D;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							if (index >= 0)
							{
								num = 3;
								continue;
							}
							goto IL_4B;
						}
						break;
					case 5:
						num = 4;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜐ == null)
					{
						num = 5;
						continue;
					}
					IL_6D:
					num = 1;
				}
			}
			IL_4B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_), RecordTableEnumerator.b("ᘿ⍁⡃㍅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝౟ݡᝣᕥ䡧ṩѫ཭ṯ剱䑳噵᥷ᑹ᡻幽ﲇﺋ꺍晴뢗ﮝ춟톡蒣얥잧\udfa9슫\udaad麯", a_));
			IL_86:
			goto IL_4B;
			IL_CA:
			return this.ᜐ[index];
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0012FFB0 File Offset: 0x0012EFB0
		internal void ᜀ(IWorksheet A_0, int A_1, int A_2, int A_3)
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
			this.ᜐ = new List<object>();
			this.ᜂ.ᜀ(spr\u2503.SQLDataType.SQL_UNKNOWN_TYPE);
			this.ᜐ = (this.ᜑ as XlsRange).ᜀ(ref this.ᜆ);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00130020 File Offset: 0x0012F020
		internal int ᜁ(object A_0)
		{
			for (;;)
			{
				this.ᜐ.Add(A_0);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜆ |= PivotDataType.Boolean;
						num = 24;
						continue;
					case 1:
						goto IL_1D0;
					case 2:
						if (A_0 is bool)
						{
							num = 21;
							continue;
						}
						num = 25;
						continue;
					case 3:
						if (A_0 is double)
						{
							num = 26;
							continue;
						}
						num = 9;
						continue;
					case 4:
					{
						string text;
						if (text.Length > 255)
						{
							num = 16;
							continue;
						}
						goto IL_B5;
					}
					case 5:
						goto IL_CE;
					case 6:
					{
						string text;
						if (text == string.Empty)
						{
							num = 1;
							continue;
						}
						this.ᜆ |= PivotDataType.Blank;
						num = 11;
						continue;
					}
					case 7:
						goto IL_10C;
					case 8:
					{
						PivotDataType pivotDataType = this.ᜆ;
						double num2;
						if (num2 > 2147483647.0 || num2 < -2147483648.0)
						{
							goto IL_28B;
						}
						if (Math.Round(num2) != num2)
						{
							goto IL_28B;
						}
						PivotDataType pivotDataType2 = PivotDataType.Integer;
						IL_2F0:
						this.ᜆ = (pivotDataType | pivotDataType2);
						num = 22;
						continue;
						IL_28B:
						pivotDataType2 = PivotDataType.Float;
						goto IL_2F0;
					}
					case 9:
						if (A_0 is TimeSpan)
						{
							num = 13;
							continue;
						}
						num = 18;
						continue;
					case 10:
						goto IL_327;
					case 11:
						goto IL_342;
					case 12:
						goto IL_215;
					case 13:
						this.ᜆ |= PivotDataType.Date;
						num = 7;
						continue;
					case 14:
						if (A_0 is string)
						{
							num = 17;
							continue;
						}
						num = 2;
						continue;
					case 15:
					{
						string text;
						if (text.Length <= 0)
						{
							num = 20;
							continue;
						}
						goto IL_1D0;
					}
					case 16:
						this.ᜆ |= PivotDataType.LongText;
						num = 27;
						continue;
					case 17:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_301;
						default:
						{
							if (false)
							{
							}
							string text = (string)A_0;
							num = 15;
							continue;
						}
						}
						break;
					case 18:
						if (A_0 is DateTime)
						{
							num = 19;
							continue;
						}
						num = 14;
						continue;
					case 19:
						this.ᜆ |= PivotDataType.Date;
						num = 12;
						continue;
					case 20:
						num = 6;
						continue;
					case 21:
						this.ᜆ |= PivotDataType.Boolean;
						num = 29;
						continue;
					case 22:
						goto IL_301;
					case 23:
						if (A_0 == null)
						{
							num = 28;
							continue;
						}
						goto IL_39A;
					case 24:
						goto IL_384;
					case 25:
						if (A_0 is ushort)
						{
							num = 0;
							continue;
						}
						num = 23;
						continue;
					case 26:
					{
						this.ᜆ |= PivotDataType.Number;
						double num2 = (double)A_0;
						num = 8;
						continue;
					}
					case 27:
						goto IL_B5;
					case 28:
						this.ᜆ |= PivotDataType.Blank;
						num = 10;
						continue;
					case 29:
						goto IL_17F;
					}
					break;
					IL_B5:
					this.ᜆ |= PivotDataType.String;
					num = 5;
					continue;
					IL_1D0:
					num = 4;
				}
			}
			IL_CE:
			IL_10C:
			IL_17F:
			IL_215:
			goto IL_3A0;
			IL_301:
			if (true)
			{
			}
			IL_327:
			IL_342:
			IL_384:
			goto IL_3A0;
			IL_39A:
			throw new NotSupportedException();
			IL_3A0:
			sprឩ sprឩ = this.ᜁ;
			ushort a_;
			this.ᜁ.ᜀ(a_ = (ushort)this.ᜐ.Count);
			sprឩ.ᜁ(a_);
			return this.ItemCount - 1;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x001303FC File Offset: 0x0012F3FC
		internal void ᜀ(sprἛ A_0)
		{
			int a_ = 11;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
					goto IL_B3;
				case 2:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 5;
						continue;
					}
					spr\u1929 item = (spr\u1929)A_0.ᜃ();
					this.ᜃ.Add(item);
					num2++;
					num = 0;
					continue;
				}
				case 4:
				{
					if (A_0.ᜉ() != TBIFFRecord.PivotField)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					this.ᜃ.Clear();
					this.ᜁ = (sprឩ)A_0.ᜃ();
					this.ᜂ = (spr\u2503)A_0.ᜃ();
					int num2 = 0;
					int num3 = (int)this.ᜁ.ᜊ();
					num = 1;
					continue;
				}
				case 5:
					return;
				case 6:
					goto IL_FC;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_99;
					}
					break;
				}
				IL_39:
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 4;
				continue;
				goto IL_39;
				IL_B3:
				num = 2;
			}
			IL_99:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
			IL_FC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᑀⵂ⁄㽆㥈⹊⹌㭎㑐㝒畔╖㱘㡚㉜ⵞՠ", a_));
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00130560 File Offset: 0x0012F560
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 13;
			if (records == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌㱎", a_));
				}
			}
			records.ᜀ(this.ᜁ);
			records.ᜀ(this.ᜂ);
			records.AddList(this.ᜃ);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x001305E4 File Offset: 0x0012F5E4
		private BiffRecordRaw ᜀ(object A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12E;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						switch (num)
						{
						case 1:
							goto IL_16F;
						case 2:
							if (A_0 is ushort)
							{
								num = 9;
								continue;
							}
							goto IL_192;
						case 3:
							goto IL_12E;
						case 4:
							goto IL_D9;
						case 5:
							if (A_0 is double)
							{
								num = 1;
								continue;
							}
							num = 7;
							continue;
						case 6:
							if (A_0 is bool)
							{
								num = 4;
								continue;
							}
							num = 5;
							continue;
						case 7:
							if (A_0 is string)
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						case 8:
							goto IL_78;
						case 9:
							goto IL_101;
						}
						if (A_0 == null)
						{
							num = 8;
						}
						else
						{
							num = 6;
						}
						break;
					}
				}
				IL_78:
				return spr\u175E.ᜀ(TBIFFRecord.PivotEmpty);
				IL_D9:
				spr\u1B5F spr_u1B5F = (spr\u1B5F)spr\u175E.ᜀ(TBIFFRecord.PivotBoolean);
				spr_u1B5F.ᜀ((bool)A_0);
				return spr_u1B5F;
				IL_101:
				return (spr\u20A8)spr\u175E.ᜀ(TBIFFRecord.PivotError);
				IL_12E:
				spr\u260F spr_u260F = (spr\u260F)spr\u175E.ᜀ(TBIFFRecord.PivotString);
				spr_u260F.ᜀ((string)A_0);
				return spr_u260F;
				IL_16F:
				spr\u1AF2 spr_u1AF = (spr\u1AF2)spr\u175E.ᜀ(TBIFFRecord.PivotDouble);
				spr_u1AF.ᜀ((double)A_0);
				return spr_u1AF;
				IL_192:
				throw new NotSupportedException();
			}
			}
		}

		// Token: 0x040011BF RID: 4543
		internal const int ᜀ = 255;

		// Token: 0x040011C0 RID: 4544
		private sprឩ ᜁ = (sprឩ)spr\u175E.ᜀ(TBIFFRecord.PivotField);

		// Token: 0x040011C1 RID: 4545
		private spr\u2503 ᜂ = (spr\u2503)spr\u175E.ᜀ(TBIFFRecord.SQLDataTypeId);

		// Token: 0x040011C2 RID: 4546
		private List<spr\u1929> ᜃ = new List<spr\u1929>();

		// Token: 0x040011C3 RID: 4547
		private spr\u23FD ᜄ;

		// Token: 0x040011C4 RID: 4548
		private int ᜅ;

		// Token: 0x040011C5 RID: 4549
		private PivotDataType ᜆ;

		// Token: 0x040011C6 RID: 4550
		private bool ᜇ;

		// Token: 0x040011C7 RID: 4551
		private string ᜈ;

		// Token: 0x040011C8 RID: 4552
		private spr\u1920 ᜉ;

		// Token: 0x040011C9 RID: 4553
		private string ᜊ;

		// Token: 0x040011CA RID: 4554
		private int ᜋ;

		// Token: 0x040011CB RID: 4555
		private int ᜌ = -1;

		// Token: 0x040011CC RID: 4556
		private int \u170D;

		// Token: 0x040011CD RID: 4557
		private int ᜎ;

		// Token: 0x040011CE RID: 4558
		private bool? ᜏ;

		// Token: 0x040011CF RID: 4559
		private float \u2593\u0096\u009D\u009A;

		// Token: 0x040011D0 RID: 4560
		private IList<object> ᜐ = new List<object>();

		// Token: 0x040011D1 RID: 4561
		private IXLSRange ᜑ;
	}
}
