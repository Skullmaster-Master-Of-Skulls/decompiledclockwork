using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x0200010F RID: 271
	public class XlsPivotTable : XlsObject, ICloneParent, IPivotTable
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x000797C4 File Offset: 0x000787C4
		internal XlsPivotTable(spr\u1DF5 A_0, object A_1)
		{
			spr\u23A9[] array = new spr\u23A9[2];
			this.ᜇ = array;
			this.ᜈ = new List<spr\u256A>();
			this.ᜉ = (spr\u2621)spr\u175E.ᜀ(TBIFFRecord.ViewExtendedInfo);
			this.ᜋ = new List<spr\u2492>();
			this.ᜌ = new List<BiffRecordRaw>();
			this.ᜑ = true;
			this.\u1714 = new List<XlsPivotField>();
			this.\u1715 = new List<XlsPivotField>();
			this.\u1716 = new List<XlsPivotField>();
			this.ᜬ = true;
			base..ctor(A_0, A_1);
			this.ᜂ();
			this.IsRowGrand = true;
			this.IsColumnGrand = true;
			this.\u1717 = new spr\u1A79(this, this.ᜉ, this.ᜅ);
			this.ᜡ = true;
			this.ᜢ = false;
			this.\u171F = false;
			this.\u171E = true;
			this.FirstDataRow = 2;
			this.FirstDataCol = 1;
			this.FirstHeaderRow = 1;
			this.RowsPerPage = 1;
			this.ColumnsPerPage = 1;
			this.ShowDataFieldInRow = false;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000798E0 File Offset: 0x000788E0
		internal XlsPivotTable(spr\u1DF5 A_0, object A_1, int A_2, IXLSRange A_3) : this(A_0, A_1)
		{
			this.CacheIndex = A_2;
			this.ᜎ = new PivotTableFields(this);
			this.ᜐ = new PivotDataFields(A_0, this);
			this.ᜏ = A_3;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00079920 File Offset: 0x00078920
		private void ᜂ()
		{
			int a_ = 11;
			this.\u1713 = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.\u1713 != null)
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
					this.\u170D = this.\u1713.ParentWorkbook;
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ɀ≂⭄⥆♈㽊浌⥎㡐㵒ㅔ睖⥘㩚⽜㩞འᝢ䕤ၦ٨ᥪ٬ᱮᥰᙲၴͶ坸", a_));
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000799B0 File Offset: 0x000789B0
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x000799F8 File Offset: 0x000789F8
		public int CacheIndex
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
				return (int)this.ᜅ.ᜎ();
			}
			set
			{
				int a_ = 0;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_91;
					case 2:
						num = 3;
						continue;
					case 3:
						if (value > 65535)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_93;
					}
					if (value < 0)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				IL_5D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("电夷夹吻嬽िⱁ⁃⍅ぇ", a_));
				IL_91:
				goto IL_5D;
				IL_93:
				this.ᜅ.ᜅ((ushort)value);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00079AA8 File Offset: 0x00078AA8
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00079AF0 File Offset: 0x00078AF0
		public bool DisplayErrorString
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
				return this.ᜉ.\u171C();
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
				this.ᜉ.ᜂ(value);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00079B38 File Offset: 0x00078B38
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x00079B80 File Offset: 0x00078B80
		public bool DisplayNullString
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
				return this.ᜉ.ᜇ();
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
				this.ᜉ.ᜇ(value);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00079BC8 File Offset: 0x00078BC8
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00079C10 File Offset: 0x00078C10
		public bool IsColumnGrand
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
				return this.ᜅ.ᜡ();
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
				this.ᜅ.ᜈ(value);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00079C58 File Offset: 0x00078C58
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00079CA0 File Offset: 0x00078CA0
		public bool EnableDrilldown
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
				return this.ᜉ.ᜀ();
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
				this.ᜉ.ᜈ(value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00079CE8 File Offset: 0x00078CE8
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00079D30 File Offset: 0x00078D30
		public bool EnableFieldDialog
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
				return this.ᜉ.ᜑ();
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
				this.ᜉ.ᜊ(value);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00079D78 File Offset: 0x00078D78
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00079DC0 File Offset: 0x00078DC0
		public bool EnableWizard
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
				return this.ᜉ.ᜁ();
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
				this.ᜉ.ᜀ(value);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00079E08 File Offset: 0x00078E08
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00079E50 File Offset: 0x00078E50
		public string ErrorString
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
				return this.ᜉ.ᜐ();
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
				this.ᜉ.ᜂ(value);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00079E98 File Offset: 0x00078E98
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x00079EE0 File Offset: 0x00078EE0
		public bool ManualUpdate
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
				return this.ᜉ.ᜅ();
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
				this.ᜉ.ᜁ(value);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00079F28 File Offset: 0x00078F28
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00079F70 File Offset: 0x00078F70
		public bool MergeLabels
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
				return this.ᜉ.ᜉ();
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
				this.ᜉ.ᜅ(value);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00079FB8 File Offset: 0x00078FB8
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0007A000 File Offset: 0x00079000
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
				return this.ᜅ.\u1713();
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0007A048 File Offset: 0x00079048
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0007A090 File Offset: 0x00079090
		public string NullString
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
				return this.ᜉ.\u171D();
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
				this.ᜉ.ᜁ(value);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0007A0D8 File Offset: 0x000790D8
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0007A128 File Offset: 0x00079128
		public PagesOrderType PageFieldOrder
		{
			get
			{
				if (this.ᜉ.\u1716())
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
						return PagesOrderType.OverThenDown;
					}
				}
				return PagesOrderType.DownThenOver;
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
				this.ᜉ.ᜆ(PagesOrderType.OverThenDown == value);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0007A174 File Offset: 0x00079174
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0007A1BC File Offset: 0x000791BC
		public string PageFieldStyle
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
				return this.ᜉ.ᜏ();
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
				this.ᜉ.ᜅ(value);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0007A204 File Offset: 0x00079204
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0007A24C File Offset: 0x0007924C
		public int PageFieldWrapCount
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
				return this.\u1717.ᜡ();
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
				this.\u1717.ᜀ((int)((ushort)value));
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0007A294 File Offset: 0x00079294
		internal List<IPivotField> PivotPageFields
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_48;
					case 2:
						goto IL_65;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_48:
						if (true)
						{
						}
						this.\u1718 = new List<IPivotField>();
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (this.\u1718 != null)
						{
							goto IL_67;
						}
						num = 1;
						break;
					}
				}
				IL_65:
				IL_67:
				return this.\u1718;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0007A318 File Offset: 0x00079318
		internal List<IPivotField> PivotRowFields
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_65;
					case 2:
						goto IL_50;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_50:
						this.ᜧ = new List<IPivotField>();
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (this.ᜧ != null)
						{
							goto IL_67;
						}
						num = 2;
						break;
					}
				}
				IL_65:
				IL_67:
				return this.ᜧ;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0007A39C File Offset: 0x0007939C
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0007A3E4 File Offset: 0x000793E4
		public bool IsRowGrand
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
				return this.ᜅ.ᜏ();
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
				this.ᜅ.ᜁ(value);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0007A42C File Offset: 0x0007942C
		public XlsPivotCache Cache
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
				return this.\u170D.PivotCaches[this.CacheIndex];
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0007A480 File Offset: 0x00079480
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0007A4C8 File Offset: 0x000794C8
		public CellRange Location
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
				return (CellRange)this.ᜏ;
			}
			set
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
						goto IL_66;
					case 1:
						goto IL_55;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_55:
						this.ᜀ(true);
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (this.Workbook.Loading)
						{
							goto IL_68;
						}
						num = 1;
						break;
					}
				}
				IL_66:
				IL_68:
				this.ᜏ = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0007A550 File Offset: 0x00079550
		internal PivotTableFields InternalFields
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_50;
					case 2:
						goto IL_66;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_50:
						this.ᜎ = new PivotTableFields(this);
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (this.ᜎ != null)
						{
							goto IL_68;
						}
						num = 1;
						break;
					}
				}
				IL_66:
				IL_68:
				return this.ᜎ;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0007A5D8 File Offset: 0x000795D8
		public PivotTableFields PivotFields
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
				return this.ᜎ;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0007A61C File Offset: 0x0007961C
		public PivotDataFields DataFields
		{
			get
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_50;
					case 1:
						goto IL_6C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_50:
						this.ᜐ = new PivotDataFields(this.Application, this);
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (this.ᜐ != null)
						{
							goto IL_6E;
						}
						num = 0;
						break;
					}
				}
				IL_6C:
				IL_6E:
				return this.ᜐ;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0007A6A8 File Offset: 0x000796A8
		PivotDataFields IPivotTable.DataFields
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
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0007A6EC File Offset: 0x000796EC
		public XlsWorkbook Workbook
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
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0007A730 File Offset: 0x00079730
		public XlsWorksheet Worksheet
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
				return this.\u1713;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0007A774 File Offset: 0x00079774
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0007A7BC File Offset: 0x000797BC
		public bool ShowDrillIndicators
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
				return this.\u1717.ᜠ();
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
				this.\u1717.ᜎ(value);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0007A804 File Offset: 0x00079804
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0007A84C File Offset: 0x0007984C
		public bool DisplayFieldCaptions
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
				return this.\u1717.ᜄ();
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
				this.\u1717.ᜀ(value);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0007A894 File Offset: 0x00079894
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0007A8D8 File Offset: 0x000798D8
		public bool RepeatItemsOnEachPrintedPage
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

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0007A91C File Offset: 0x0007991C
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0007A960 File Offset: 0x00079960
		public PivotBuiltInStyles? BuiltInStyle
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0007A9A4 File Offset: 0x000799A4
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0007A9E8 File Offset: 0x000799E8
		public bool ShowRowGrand
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
				return this.IsColumnGrand;
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
				this.IsColumnGrand = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0007AA2C File Offset: 0x00079A2C
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0007AA70 File Offset: 0x00079A70
		public bool ShowColumnGrand
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
				return this.IsRowGrand;
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
				this.IsRowGrand = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0007AAB4 File Offset: 0x00079AB4
		public IPivotTableOptions Options
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
				return this.\u1717;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0007AAF8 File Offset: 0x00079AF8
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0007AB3C File Offset: 0x00079B3C
		public int FirstDataCol
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
				return this.\u1719;
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
				this.\u1719 = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0007AB80 File Offset: 0x00079B80
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0007ABC4 File Offset: 0x00079BC4
		public int FirstDataRow
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

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0007AC08 File Offset: 0x00079C08
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0007AC4C File Offset: 0x00079C4C
		public int FirstHeaderRow
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

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0007AC90 File Offset: 0x00079C90
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0007ACD4 File Offset: 0x00079CD4
		public int ColumnsPerPage
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u171C = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0007AD18 File Offset: 0x00079D18
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0007AD5C File Offset: 0x00079D5C
		public int RowsPerPage
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u171D = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0007ADA0 File Offset: 0x00079DA0
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0007ADE4 File Offset: 0x00079DE4
		public bool ShowColHeaderStyle
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0007AE28 File Offset: 0x00079E28
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0007AE6C File Offset: 0x00079E6C
		public bool ShowColStripes
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

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0007AEB0 File Offset: 0x00079EB0
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0007AEF4 File Offset: 0x00079EF4
		public bool ShowLastCol
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0007AF38 File Offset: 0x00079F38
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0007AF7C File Offset: 0x00079F7C
		public bool ShowRowHeaderStyle
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0007AFC0 File Offset: 0x00079FC0
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0007B004 File Offset: 0x0007A004
		public bool ShowRowStripes
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
				return this.ᜢ;
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
				this.ᜢ = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0007B048 File Offset: 0x0007A048
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0007B08C File Offset: 0x0007A08C
		internal Stream ColumnItemsStream
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
				return this.ᜣ;
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
				this.ᜣ = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0007B0D0 File Offset: 0x0007A0D0
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0007B114 File Offset: 0x0007A114
		internal Stream RowItemsStream
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
				return this.ᜤ;
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
				this.ᜤ = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0007B158 File Offset: 0x0007A158
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0007B19C File Offset: 0x0007A19C
		public bool ShowDataFieldInRow
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
				return this.ᜥ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜀ(true);
						num = 4;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							if (!this.\u170D.Loading)
							{
								num = 1;
								continue;
							}
							goto IL_3B;
						}
						break;
					case 4:
						goto IL_3B;
					case 5:
						goto IL_6F;
					}
					if (this.ᜥ != value)
					{
						num = 5;
						continue;
					}
					break;
					IL_3B:
					if (true)
					{
					}
					this.ᜥ = value;
					num = 0;
					continue;
					IL_6F:
					num = 2;
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0007B258 File Offset: 0x0007A258
		internal spr\u1DF5 Application
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
				return this.\u170D.AppImplementation;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0007B2A0 File Offset: 0x0007A2A0
		internal Dictionary<string, Stream> PreservedElements
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5A:
					this.ᜦ = new Dictionary<string, Stream>();
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						goto IL_5A;
					case 2:
						goto IL_6F;
					}
					if (this.ᜦ != null)
					{
						break;
					}
					num = 1;
				}
				IL_6F:
				return this.ᜦ;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0007B324 File Offset: 0x0007A324
		public IPivotCalculatedFields CalculatedFields
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
				return this.ᜌ();
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0007B368 File Offset: 0x0007A368
		public IPivotFields PageFields
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
				return this.ᜁ(AxisTypes.Page);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0007B3AC File Offset: 0x0007A3AC
		public IPivotFields RowFields
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
				return this.ᜁ(AxisTypes.Row);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0007B3F0 File Offset: 0x0007A3F0
		public IPivotFields ColumnFields
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
				return this.ᜁ(AxisTypes.Column);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0007B434 File Offset: 0x0007A434
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0007B478 File Offset: 0x0007A478
		public bool IsChanged
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
				return this.ᜩ;
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
				this.ᜩ = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0007B4BC File Offset: 0x0007A4BC
		internal List<int> ColFieldsOrder
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5A:
					this.ᜪ = new List<int>();
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 2:
						goto IL_5A;
					}
					if (true)
					{
					}
					if (this.ᜪ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6F:
				return this.ᜪ;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0007B540 File Offset: 0x0007A540
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0007B584 File Offset: 0x0007A584
		public CollectionExtended<PivotReportFilter> ReportFilters
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
				return this.ᜫ;
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
				this.ᜫ = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0007B5C8 File Offset: 0x0007A5C8
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0007B6F4 File Offset: 0x0007A6F4
		public bool AllSubTotalTop
		{
			get
			{
				IEnumerator<PivotField> enumerator = this.PivotFields.GetEnumerator();
				bool result;
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A8;
						case 1:
							goto IL_9E;
						case 2:
							this.ᜬ = false;
							result = this.ᜬ;
							num = 1;
							continue;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							XlsPivotField xlsPivotField = enumerator.Current;
							num = 5;
							continue;
						}
						case 5:
						{
							XlsPivotField xlsPivotField;
							if (!xlsPivotField.SubtotalTop)
							{
								num = 2;
								continue;
							}
							break;
						}
						case 6:
							num = 0;
							continue;
						}
						IL_4D:
						num = 3;
						continue;
						goto IL_4D;
					}
					IL_9E:
					goto IL_103;
					IL_A8:
					goto IL_18;
				}
				finally
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_F0:
						enumerator.Dispose();
						num = 1;
						break;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F0;
						case 1:
							goto IL_100;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 0;
					}
					IL_100:;
				}
				goto IL_103;
				IL_18:
				return this.ᜬ;
				IL_103:
				if (true)
				{
				}
				return result;
			}
			set
			{
				IEnumerator<PivotField> enumerator = this.PivotFields.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							XlsPivotField xlsPivotField = enumerator.Current;
							xlsPivotField.SubtotalTop = value;
							num = 4;
							continue;
						}
						case 1:
							num = 2;
							continue;
						case 2:
							goto IL_78;
						}
						IL_56:
						num = 0;
						continue;
						goto IL_56;
					}
					IL_78:;
				}
				finally
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_BD:
						if (true)
						{
						}
						enumerator.Dispose();
						num = 2;
						break;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_BD;
						case 2:
							goto IL_D5;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 1;
					}
					IL_D5:;
				}
				this.ᜬ = value;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0007B7F0 File Offset: 0x0007A7F0
		public int Parse(IList data, int iPos)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 24;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
						int num2 = 0;
						num = 14;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_373;
						default:
							goto IL_326;
						}
						break;
					case 2:
						return iPos;
					case 3:
					{
						BiffRecordRaw biffRecordRaw;
						if (Array.IndexOf<TBIFFRecord>(XlsPivotTable.ᜄ, biffRecordRaw.TypeCode) == -1)
						{
							num = 2;
							continue;
						}
						this.ᜌ.Add(biffRecordRaw);
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						num = 8;
						continue;
					}
					case 4:
					{
						BiffRecordRaw biffRecordRaw;
						biffRecordRaw.CheckTypeCode(TBIFFRecord.ViewExtendedInfo);
						this.ᜉ = (spr\u2621)biffRecordRaw;
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						num = 16;
						continue;
					}
					case 5:
						goto IL_13A;
					case 6:
					{
						BiffRecordRaw biffRecordRaw;
						if (biffRecordRaw.TypeCode != TBIFFRecord.RowColumnFieldId)
						{
							num = 26;
							continue;
						}
						int num2;
						this.ᜇ[num2] = (spr\u23A9)biffRecordRaw;
						num2++;
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						num = 21;
						continue;
					}
					case 7:
						num = 15;
						continue;
					case 8:
						goto IL_2B2;
					case 9:
						goto IL_18F;
					case 10:
						goto IL_13A;
					case 11:
					{
						BiffRecordRaw biffRecordRaw;
						this.ᜊ = (spr\u19DB)biffRecordRaw;
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						num = 9;
						continue;
					}
					case 12:
					{
						int num3;
						int num4;
						if (num3 >= num4)
						{
							num = 0;
							continue;
						}
						XlsPivotField xlsPivotField = new XlsPivotField(base.ReservedHandle, this);
						iPos = xlsPivotField.Parse(data, iPos);
						this.ᜆ.Add(xlsPivotField);
						num3++;
						num = 18;
						continue;
					}
					case 13:
						if (iPos >= 0)
						{
							num = 7;
							continue;
						}
						goto IL_118;
					case 14:
						goto IL_1EB;
					case 15:
					{
						if (iPos > data.Count - 1)
						{
							num = 17;
							continue;
						}
						this.ᜁ();
						BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
						biffRecordRaw.CheckTypeCode(TBIFFRecord.PivotViewDefinition);
						this.ᜅ = (sprᣆ)biffRecordRaw;
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						int num3 = 0;
						int num4 = (int)this.ᜅ.\u1717();
						num = 20;
						continue;
					}
					case 16:
						goto IL_2B2;
					case 17:
						goto IL_18D;
					case 18:
						goto IL_373;
					case 19:
					{
						BiffRecordRaw biffRecordRaw;
						if (biffRecordRaw.TypeCode != TBIFFRecord.LineItemArray)
						{
							num = 4;
							continue;
						}
						spr\u256A spr_u256A = (spr\u256A)biffRecordRaw;
						int[] array;
						int num5;
						spr_u256A.ᜀ(array[num5++]);
						this.ᜈ.Add(spr_u256A);
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						num = 10;
						continue;
					}
					case 20:
						if (true)
						{
						}
						goto IL_451;
					case 21:
						goto IL_1EB;
					case 22:
						goto IL_18F;
					case 23:
					{
						BiffRecordRaw biffRecordRaw;
						if (biffRecordRaw.TypeCode == TBIFFRecord.PageItem)
						{
							num = 11;
							continue;
						}
						goto IL_18F;
					}
					case 25:
					{
						BiffRecordRaw biffRecordRaw;
						if (biffRecordRaw.TypeCode != TBIFFRecord.DataItem)
						{
							num = 27;
							continue;
						}
						this.ᜋ.Add((spr\u2492)biffRecordRaw);
						iPos++;
						biffRecordRaw = (BiffRecordRaw)data[iPos];
						num = 22;
						continue;
					}
					case 26:
					{
						int[] array = new int[]
						{
							(int)this.ᜅ.\u170D(),
							(int)this.ᜅ.\u1712()
						};
						int num5 = 0;
						num = 23;
						continue;
					}
					case 27:
						num = 5;
						continue;
					}
					if (data == null)
					{
						num = 1;
						continue;
					}
					num = 13;
					continue;
					IL_13A:
					num = 19;
					continue;
					IL_18F:
					num = 25;
					continue;
					IL_1EB:
					num = 6;
					continue;
					IL_2B2:
					num = 3;
					continue;
					IL_451:
					num = 12;
					continue;
					IL_373:
					goto IL_451;
				}
				IL_118:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍂⩄㑆", a_), RecordTableEnumerator.b("ᕂ⑄⭆㱈⹊浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠རdᑦᩨ䭪ᥬݮၰᵲ啴䝶奸᩺፼᭾ꆀﾊﶎ놐ﶔ뮚列ﺞ햠슢薤쮦첨얪쪬\udbae\ud9b0鶲", a_));
				IL_18D:
				goto IL_118;
				IL_326:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("❂⑄㍆⡈", a_));
			}
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0007BCA4 File Offset: 0x0007ACA4
		public void Serialize(RecordArrayList records)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num4;
					int count;
					switch (num)
					{
					case 0:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 9;
							continue;
						}
						spr\u23A9 spr_u23A = this.ᜇ[num2];
						num = 13;
						continue;
					}
					case 2:
					{
						if (num4 >= count)
						{
							num = 4;
							continue;
						}
						XlsPivotField xlsPivotField = this.ᜆ[num4];
						xlsPivotField.SerializeDataToList(records);
						num4++;
						num = 14;
						continue;
					}
					case 3:
						goto IL_80;
					case 4:
					{
						if (true)
						{
						}
						int num2 = 0;
						int num3 = this.ᜇ.Length;
						num = 7;
						continue;
					}
					case 5:
					{
						spr\u23A9 spr_u23A;
						records.ᜀ(spr_u23A);
						int num2;
						num2++;
						num = 3;
						continue;
					}
					case 6:
						records.ᜀ(this.ᜊ);
						num = 8;
						continue;
					case 7:
						goto IL_80;
					case 8:
						goto IL_176;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_104;
						default:
							if (false)
							{
							}
							goto IL_106;
						}
						break;
					case 10:
						if (this.ᜊ != null)
						{
							num = 6;
							continue;
						}
						goto IL_1E1;
					case 11:
						goto IL_71;
					case 12:
						goto IL_104;
					case 13:
					{
						spr\u23A9 spr_u23A;
						if (spr_u23A != null)
						{
							num = 5;
							continue;
						}
						goto IL_106;
					}
					case 14:
						goto IL_178;
					}
					if (records == null)
					{
						num = 11;
						continue;
					}
					records.ᜀ(this.ᜅ);
					num4 = 0;
					count = this.ᜆ.Count;
					num = 12;
					continue;
					IL_80:
					num = 0;
					continue;
					IL_106:
					num = 10;
					continue;
					IL_178:
					num = 2;
					continue;
					IL_104:
					goto IL_178;
				}
				IL_71:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑❓", a_));
				IL_176:
				IL_1E1:
				records.AddList(this.ᜋ);
				records.AddList(this.ᜈ);
				records.ᜀ(this.ᜉ);
				records.AddList(this.ᜌ);
				return;
			}
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0007BEC4 File Offset: 0x0007AEC4
		private void ᜁ()
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
			this.ᜆ.Clear();
			this.ᜈ.Clear();
			this.ᜋ.Clear();
			this.ᜌ.Clear();
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0007BF2C File Offset: 0x0007AF2C
		internal void ᜀ(AxisTypes A_0, XlsPivotField A_1)
		{
			for (;;)
			{
				List<XlsPivotField> list = this.ᜀ(A_0);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 != AxisTypes.Row)
						{
							num = 9;
							continue;
						}
						this.PivotRowFields.Remove(A_1);
						num = 7;
						continue;
					case 1:
						if (list != null)
						{
							num = 3;
							continue;
						}
						return;
					case 2:
						goto IL_74;
					case 3:
						list.Remove(A_1);
						num = 6;
						continue;
					case 4:
						num = 5;
						continue;
					case 5:
						goto IL_74;
					case 6:
						return;
					case 7:
						goto IL_74;
					case 8:
						if (A_0 != AxisTypes.Page)
						{
							num = 4;
							continue;
						}
						goto IL_F1;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F1;
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
					IL_74:
					num = 1;
					continue;
					IL_F1:
					this.PivotPageFields.Remove(A_1);
					num = 2;
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0007C048 File Offset: 0x0007B048
		internal void ᜀ(AxisTypes A_0, XlsPivotField A_1, bool A_2)
		{
			int a_ = 19;
			for (;;)
			{
				List<XlsPivotField> list = this.ᜀ(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_71;
					case 1:
						goto IL_45;
					case 2:
						if (this.ᜎ.Count < this.Cache.CacheFields.Count)
						{
							num = 5;
							continue;
						}
						goto IL_71;
					case 3:
					{
						string name = RecordTableEnumerator.b("ᩈ㹊⁌潎㹐㕒畔睖", a_) + A_1.Name;
						this.DataFields.Add(A_1, name, SubtotalTypes.Sum);
						num = 4;
						continue;
					}
					case 4:
						return;
					case 5:
						this.ᜎ.Add(A_1 as PivotField);
						A_1.Axis = A_0;
						num = 7;
						continue;
					case 6:
						if (A_2)
						{
							num = 3;
							continue;
						}
						return;
					case 7:
						if (true)
						{
						}
						goto IL_71;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_45;
						default:
							if (false)
							{
							}
							list.Add(A_1);
							num = 0;
							continue;
						}
						break;
					}
					break;
					IL_45:
					if (list != null)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
					IL_71:
					num = 6;
				}
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0007C19C File Offset: 0x0007B19C
		internal List<XlsPivotField> ᜀ(AxisTypes A_0)
		{
			List<XlsPivotField> result;
			for (;;)
			{
				for (;;)
				{
					result = null;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_8D;
						case 1:
							switch (A_0)
							{
							case AxisTypes.Row:
								result = this.\u1714;
								num = 5;
								continue;
							case AxisTypes.Column:
								result = this.\u1715;
								if (true)
								{
								}
								num = 4;
								continue;
							case (AxisTypes)3:
								return result;
							case AxisTypes.Page:
								result = this.\u1716;
								num = 2;
								continue;
							default:
								num = 3;
								continue;
							}
							break;
						case 2:
							return result;
						case 3:
							num = 0;
							continue;
						case 4:
							goto IL_6F;
						case 5:
							goto IL_80;
						}
						break;
					}
				}
				IL_8D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_A3;
				}
			}
			IL_6F:
			IL_80:
			return result;
			IL_A3:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0007C26C File Offset: 0x0007B26C
		internal PivotTableFields ᜁ(AxisTypes A_0)
		{
			if (true)
			{
			}
			PivotTableFields pivotTableFields;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return pivotTableFields;
			}
			if (false)
			{
			}
			pivotTableFields = new PivotTableFields(this.Workbook.AppImplementation, this);
			IEnumerator<PivotField> enumerator = this.ᜎ.GetEnumerator();
			try
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						XlsPivotField xlsPivotField;
						pivotTableFields.Add(xlsPivotField as PivotField);
						num = 2;
						continue;
					}
					case 1:
						num = 6;
						continue;
					case 4:
					{
						XlsPivotField xlsPivotField;
						if (xlsPivotField.Axis == A_0)
						{
							num = 0;
							continue;
						}
						break;
					}
					case 5:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						XlsPivotField xlsPivotField = enumerator.Current;
						num = 4;
						continue;
					}
					case 6:
						goto IL_D6;
					}
					IL_9E:
					num = 5;
					continue;
					goto IL_9E;
				}
				IL_D6:;
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						enumerator.Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_10F;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 0;
				}
				IL_10F:;
			}
			return pivotTableFields;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0007C39C File Offset: 0x0007B39C
		internal byte ᜃ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u170D.Version == ExcelVersion.Version2010)
					{
						num = 2;
						continue;
					}
					return 2;
				case 1:
					return 3;
				case 2:
					goto IL_6A;
				}
				if (true)
				{
				}
				if (this.\u170D.Version == ExcelVersion.Version2007)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return 3;
			IL_6A:
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
			return 4;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0007C434 File Offset: 0x0007B434
		internal void ᜀ(int A_0)
		{
			int row;
			int lastRow;
			int column;
			int lastColumn;
			int num;
			int num2;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					if (true)
					{
					}
					for (;;)
					{
						row = this.ᜏ.Row;
						lastRow = this.ᜏ.LastRow;
						column = this.ᜏ.Column;
						lastColumn = this.ᜏ.LastColumn;
						num = this.ᜀ();
						num2 = num + A_0;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (num != 0)
								{
									num3 = 1;
									continue;
								}
								goto IL_AB;
							case 1:
								num++;
								num3 = 3;
								continue;
							case 2:
								goto IL_A9;
							case 3:
								goto IL_AB;
							case 4:
								if (num2 != 0)
								{
									num3 = 5;
									continue;
								}
								goto IL_F9;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num2++;
									num3 = 2;
									continue;
								}
								break;
							}
							break;
							IL_AB:
							num3 = 4;
						}
					}
					break;
				}
			}
			IL_A9:
			IL_F9:
			this.ᜏ = this.ᜏ[row - num + num2, column, lastRow - num + num2, lastColumn];
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0007C55C File Offset: 0x0007B55C
		private int ᜀ()
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 0;
				int count = this.PivotFields.Count;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_AA;
					case 1:
						goto IL_AA;
					case 2:
						if (num2 >= count)
						{
							num3 = 4;
							continue;
						}
						num3 = 5;
						continue;
					case 3:
						if (true)
						{
						}
						num++;
						num3 = 6;
						continue;
					case 4:
						return num;
					case 5:
						IL_89:
						if (this.PivotFields[num2].Axis == AxisTypes.Page)
						{
							num3 = 3;
							continue;
						}
						goto IL_3E;
					case 6:
						goto IL_3E;
					}
					break;
					IL_3E:
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					default:
						if (false)
						{
						}
						num3 = 1;
						continue;
					}
					IL_AA:
					num3 = 2;
				}
			}
			return num;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0007C63C File Offset: 0x0007B63C
		internal void ᜀ(bool A_0)
		{
			for (;;)
			{
				this.ᜩ = true;
				this.Cache.IsRefreshOnLoad = true;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					int num = 1;
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
							if (A_0)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							this.ᜄ();
							this.ColumnItemsStream = null;
							this.RowItemsStream = null;
							this.PreservedElements.Clear();
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0007C6E0 File Offset: 0x0007B6E0
		internal void ᜄ()
		{
			int num = 1;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_47;
				case 2:
					if (num2 != 0)
					{
						goto IL_47;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					goto IL_7C;
				case 4:
					goto IL_45;
				case 5:
					num2 = 1;
					num = 0;
					continue;
				}
				if (this.ᜏ != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_45:
				IXLSRange ixlsrange = this.ᜏ;
				this.ᜏ = this.\u1713[ixlsrange.Row, ixlsrange.Column];
				num2 = ixlsrange.Row - (1 + this.PageFields.Count);
				num = 2;
				continue;
				IL_47:
				ixlsrange = this.\u1713[num2, ixlsrange.Column, ixlsrange.LastRow, ixlsrange.LastColumn + 1];
				((XlsRange)ixlsrange).Clear(true);
				num = 3;
			}
			IL_7C:
			if (true)
			{
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0007C7FC File Offset: 0x0007B7FC
		internal spr\u205E ᜌ()
		{
			spr\u205E spr_u205E;
			for (;;)
			{
				spr_u205E = new spr\u205E(this);
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= this.PivotFields.Count)
						{
							num2 = 2;
							continue;
						}
						num2 = 6;
						continue;
					case 1:
						goto IL_B3;
					case 2:
						return spr_u205E;
					case 3:
						if (true)
						{
						}
						spr_u205E.Add(this.PivotFields[num]);
						num2 = 4;
						continue;
					case 4:
						goto IL_37;
					case 5:
						goto IL_B3;
					case 6:
						IL_93:
						if (this.PivotFields[num].IsFormulaField)
						{
							num2 = 3;
							continue;
						}
						goto IL_37;
					}
					break;
					IL_37:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (false)
						{
						}
						num2 = 5;
						continue;
					}
					IL_B3:
					num2 = 0;
				}
			}
			return spr_u205E;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0007C8F0 File Offset: 0x0007B8F0
		public void Clear()
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
				this.ᜄ();
				IEnumerator<PivotField> enumerator = this.PivotFields.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A9;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							XlsPivotField xlsPivotField = enumerator.Current;
							xlsPivotField.Axis = AxisTypes.None;
							xlsPivotField.DataField = false;
							num = 2;
							continue;
						}
						case 4:
							num = 0;
							continue;
						}
						IL_87:
						num = 1;
						continue;
						goto IL_87;
					}
					IL_A9:;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_E2;
						case 2:
							enumerator.Dispose();
							num = 1;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 2;
					}
					IL_E2:;
				}
				break;
			}
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0007C9F4 File Offset: 0x0007B9F4
		public object Clone(object parent)
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
			return this.Clone(parent, this.CacheIndex, null);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0007CA40 File Offset: 0x0007BA40
		public object Clone(object parent, int cacheIndex, Dictionary<string, string> hashWorksheetNames)
		{
			XlsPivotTable xlsPivotTable;
			for (;;)
			{
				xlsPivotTable = (XlsPivotTable)base.MemberwiseClone();
				xlsPivotTable.SetParent(xlsPivotTable);
				xlsPivotTable.ᜂ();
				xlsPivotTable.ᜅ = (sprᣆ)spr\u1CD3.ᜀ(this.ᜅ);
				xlsPivotTable.ᜉ = (spr\u2621)spr\u1CD3.ᜀ(this.ᜉ);
				xlsPivotTable.ᜊ = (spr\u19DB)spr\u1CD3.ᜀ(this.ᜊ);
				xlsPivotTable.ᜆ = spr\u1CD3.ᜀ<XlsPivotField>(this.ᜆ, xlsPivotTable);
				xlsPivotTable.ᜈ = spr\u1CD3.ᜀ<spr\u256A>(this.ᜈ);
				xlsPivotTable.ᜋ = spr\u1CD3.ᜀ<spr\u2492>(this.ᜋ);
				xlsPivotTable.ᜌ = spr\u1CD3.ᜀ(this.ᜌ);
				XlsPivotTable xlsPivotTable2 = xlsPivotTable;
				spr\u23A9[] array = new spr\u23A9[2];
				xlsPivotTable2.ᜇ = array;
				xlsPivotTable.ᜇ[0] = (spr\u23A9)spr\u1CD3.ᜀ(this.ᜇ[0]);
				xlsPivotTable.ᜇ[1] = (spr\u23A9)spr\u1CD3.ᜀ(this.ᜇ[1]);
				xlsPivotTable.CacheIndex = cacheIndex;
				xlsPivotTable.ᜎ = (PivotTableFields)spr\u1CD3.ᜀ(this.ᜎ, xlsPivotTable);
				xlsPivotTable.ᜐ = (PivotDataFields)spr\u1CD3.ᜀ(this.ᜐ, xlsPivotTable);
				object parent2 = xlsPivotTable.FindParent(typeof(XlsWorksheet));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return xlsPivotTable;
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
							return xlsPivotTable;
						case 1:
							if (this.ᜏ != null)
							{
								num = 2;
								continue;
							}
							return xlsPivotTable;
						case 2:
							if (true)
							{
							}
							xlsPivotTable.ᜏ = ((ICombinedRange)this.ᜏ).Clone(parent2, hashWorksheetNames, xlsPivotTable.\u170D);
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			return xlsPivotTable;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0007CC04 File Offset: 0x0007BC04
		internal XlsPivotTable ᜀ(PivotTablesCollection A_0, Dictionary<string, string> A_1)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int cacheIndex;
				for (;;)
				{
					XlsWorksheet parentWorksheet = A_0.ParentWorksheet;
					XlsWorkbook parentWorkbook = parentWorksheet.ParentWorkbook;
					cacheIndex = this.CacheIndex;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_127;
						case 1:
							if (true)
							{
							}
							if (parentWorkbook == this.Workbook)
							{
								goto IL_129;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_95;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 2:
							goto IL_E3;
						case 3:
						{
							if (parentWorkbook.Version != this.Workbook.Version)
							{
								num = 0;
								continue;
							}
							XlsPivotCache xlsPivotCache = this.Workbook.PivotCaches[this.CacheIndex];
							XlsPivotCachesCollection pivotCaches = ((IWorkbook)parentWorkbook).PivotCaches;
							xlsPivotCache = (XlsPivotCache)xlsPivotCache.Clone(pivotCaches, A_1);
							pivotCaches.Add(xlsPivotCache);
							cacheIndex = pivotCaches.Count - 1;
							num = 2;
							continue;
						}
						case 4:
							goto IL_95;
						}
						break;
						IL_95:
						num = 3;
					}
				}
				IL_E3:
				goto IL_129;
				IL_127:
				throw new InvalidOperationException(RecordTableEnumerator.b("ل♆❈╊≌㭎煐げ㩔❖⁘筚ⵜ㙞ᝠౢᅤ䝦ᵨ੪ཬͮᑰr啴ᕶᱸེ੼᩾ꖄ力ﺐﲒﺔ릘즠莢솤캦쾨춪좬\uddae풰\uddb2솴鞶쾸\udeba쾼첾ꣀ곂ꯄ듆", a_));
				IL_129:
				return (XlsPivotTable)this.Clone(A_0, cacheIndex, A_1);
			}
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0007CD4C File Offset: 0x0007BD4C
		// Note: this type is marked as 'beforefieldinit'.
		static XlsPivotTable()
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
			XlsPivotTable.ᜄ = new TBIFFRecord[]
			{
				TBIFFRecord.QsiSXTag,
				(TBIFFRecord)2064,
				TBIFFRecord.PivotViewAdditionalInfo,
				(TBIFFRecord)244,
				(TBIFFRecord)245,
				TBIFFRecord.PivotFormat,
				TBIFFRecord.RuleData,
				TBIFFRecord.RuleFilter,
				TBIFFRecord.SelectionInfo,
				TBIFFRecord.DBQueryExt
			};
		}

		// Token: 0x04000A15 RID: 2581
		internal const string ᜀ = "Sum of ";

		// Token: 0x04000A16 RID: 2582
		internal const TBIFFRecord ᜁ = TBIFFRecord.PivotViewDefinition;

		// Token: 0x04000A17 RID: 2583
		private const byte ᜂ = 3;

		// Token: 0x04000A18 RID: 2584
		private const byte ᜃ = 4;

		// Token: 0x04000A19 RID: 2585
		private static readonly TBIFFRecord[] ᜄ;

		// Token: 0x04000A1A RID: 2586
		private sprᣆ ᜅ = (sprᣆ)spr\u175E.ᜀ(TBIFFRecord.PivotViewDefinition);

		// Token: 0x04000A1B RID: 2587
		private List<XlsPivotField> ᜆ = new List<XlsPivotField>();

		// Token: 0x04000A1C RID: 2588
		private spr\u23A9[] ᜇ;

		// Token: 0x04000A1D RID: 2589
		private bool \u2593\u0082\u0093\u00AF;

		// Token: 0x04000A1E RID: 2590
		private List<spr\u256A> ᜈ;

		// Token: 0x04000A1F RID: 2591
		private spr\u2621 ᜉ;

		// Token: 0x04000A20 RID: 2592
		private spr\u19DB ᜊ;

		// Token: 0x04000A21 RID: 2593
		private List<spr\u2492> ᜋ;

		// Token: 0x04000A22 RID: 2594
		private List<BiffRecordRaw> ᜌ;

		// Token: 0x04000A23 RID: 2595
		private XlsWorkbook \u170D;

		// Token: 0x04000A24 RID: 2596
		private PivotTableFields ᜎ;

		// Token: 0x04000A25 RID: 2597
		private IXLSRange ᜏ;

		// Token: 0x04000A26 RID: 2598
		private PivotDataFields ᜐ;

		// Token: 0x04000A27 RID: 2599
		private bool ᜑ;

		// Token: 0x04000A28 RID: 2600
		private bool \u2609\u00AF\u00A5\u009F;

		// Token: 0x04000A29 RID: 2601
		private PivotBuiltInStyles? \u1712;

		// Token: 0x04000A2A RID: 2602
		private XlsWorksheet \u1713;

		// Token: 0x04000A2B RID: 2603
		private List<XlsPivotField> \u1714;

		// Token: 0x04000A2C RID: 2604
		private List<XlsPivotField> \u1715;

		// Token: 0x04000A2D RID: 2605
		private byte \u2593\u00A9\u0093\u009F;

		// Token: 0x04000A2E RID: 2606
		private List<XlsPivotField> \u1716;

		// Token: 0x04000A2F RID: 2607
		private spr\u1A79 \u1717;

		// Token: 0x04000A30 RID: 2608
		private List<IPivotField> \u1718;

		// Token: 0x04000A31 RID: 2609
		private int \u1719;

		// Token: 0x04000A32 RID: 2610
		private int \u171A;

		// Token: 0x04000A33 RID: 2611
		private int \u171B;

		// Token: 0x04000A34 RID: 2612
		private int \u171C;

		// Token: 0x04000A35 RID: 2613
		private int \u171D;

		// Token: 0x04000A36 RID: 2614
		private bool \u171E;

		// Token: 0x04000A37 RID: 2615
		private bool \u171F;

		// Token: 0x04000A38 RID: 2616
		private bool ᜠ;

		// Token: 0x04000A39 RID: 2617
		private bool ᜡ;

		// Token: 0x04000A3A RID: 2618
		private int[] \u2609\u0084\u0097\u0088;

		// Token: 0x04000A3B RID: 2619
		private bool ᜢ;

		// Token: 0x04000A3C RID: 2620
		private Stream ᜣ;

		// Token: 0x04000A3D RID: 2621
		private Stream ᜤ;

		// Token: 0x04000A3E RID: 2622
		private bool ᜥ;

		// Token: 0x04000A3F RID: 2623
		private Dictionary<string, Stream> ᜦ;

		// Token: 0x04000A40 RID: 2624
		private List<IPivotField> ᜧ;

		// Token: 0x04000A41 RID: 2625
		private spr\u205E ᜨ;

		// Token: 0x04000A42 RID: 2626
		private bool ᜩ;

		// Token: 0x04000A43 RID: 2627
		private List<int> ᜪ;

		// Token: 0x04000A44 RID: 2628
		private CollectionExtended<PivotReportFilter> ᜫ;

		// Token: 0x04000A45 RID: 2629
		private bool ᜬ;
	}
}
