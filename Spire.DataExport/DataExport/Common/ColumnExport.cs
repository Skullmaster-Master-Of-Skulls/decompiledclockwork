using System;
using System.Collections;

namespace Spire.DataExport.Common
{
	// Token: 0x02000169 RID: 361
	public class ColumnExport
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0005FBD4 File Offset: 0x0005EBD4
		public ColumnExport(CollectionBase Collection)
		{
			this.ᜋ = 0;
			if (Collection is ColumnsExport)
			{
				this.ᜀ = (Collection as ColumnsExport);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0005FC44 File Offset: 0x0005EC44
		public string GetDefaultFormat()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_11E;
				case 1:
					if (this.ᜀ.OwnerFormatsExport != null)
					{
						num = 3;
						continue;
					}
					goto IL_120;
				case 3:
				{
					ColExportType colExportType = this.ᜂ;
					num = 4;
					continue;
				}
				case 4:
				{
					ColExportType colExportType;
					switch (colExportType)
					{
					case ColExportType.Integer:
					case ColExportType.Bigint:
						goto IL_102;
					case ColExportType.Float:
						goto IL_55;
					case ColExportType.Currency:
						goto IL_B2;
					case ColExportType.DateTime:
						goto IL_EB;
					case ColExportType.Time:
						goto IL_44;
					default:
						num = 6;
						continue;
					}
					break;
				}
				case 5:
					num = 1;
					continue;
				case 6:
					num = 0;
					continue;
				}
				if (!this.ᜌ)
				{
					goto IL_120;
				}
				num = 5;
			}
			IL_44:
			return this.ᜀ.OwnerFormatsExport.Time;
			IL_55:
			if (true)
			{
			}
			return this.ᜀ.OwnerFormatsExport.Float;
			IL_B2:
			return this.ᜀ.OwnerFormatsExport.Currency;
			IL_EB:
			return this.ᜀ.OwnerFormatsExport.DateTime;
			IL_102:
			return this.ᜀ.OwnerFormatsExport.Integer;
			IL_11E:
			return string.Empty;
			IL_120:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_11E;
			default:
				if (false)
				{
				}
				return string.Empty;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0005FD94 File Offset: 0x0005ED94
		public void SetDefaultFormat()
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
			this.ᜇ = this.GetDefaultFormat();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0005FDDC File Offset: 0x0005EDDC
		public ColumnsExport ColumnsExport
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
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0005FE20 File Offset: 0x0005EE20
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0005FE64 File Offset: 0x0005EE64
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
				return this.Number;
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
				this.Number = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0005FEA8 File Offset: 0x0005EEA8
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0005FEEC File Offset: 0x0005EEEC
		public int Number
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0005FF30 File Offset: 0x0005EF30
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0005FF74 File Offset: 0x0005EF74
		public string Name
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0005FFB8 File Offset: 0x0005EFB8
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0005FFFC File Offset: 0x0005EFFC
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
				return this.ᜄ;
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00060040 File Offset: 0x0005F040
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00060084 File Offset: 0x0005F084
		public int Width
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x000600C8 File Offset: 0x0005F0C8
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0006010C File Offset: 0x0005F10C
		public ColExportType ColExportType
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00060150 File Offset: 0x0005F150
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00060194 File Offset: 0x0005F194
		public ColumAlign ColAlign
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
				return this.ᜆ;
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
				this.ᜆ = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000601D8 File Offset: 0x0005F1D8
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0006021C File Offset: 0x0005F21C
		public string Format
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00060260 File Offset: 0x0005F260
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x000602A4 File Offset: 0x0005F2A4
		public string SQLType
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000602E8 File Offset: 0x0005F2E8
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0006032C File Offset: 0x0005F32C
		public int Length
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00060370 File Offset: 0x0005F370
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x000603B4 File Offset: 0x0005F3B4
		public long Size
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x000603F8 File Offset: 0x0005F3F8
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0006043C File Offset: 0x0005F43C
		public int Tag
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00060480 File Offset: 0x0005F480
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x000604C4 File Offset: 0x0005F4C4
		public bool AllowFormat
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00060508 File Offset: 0x0005F508
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0006054C File Offset: 0x0005F54C
		public bool IsNumeric
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u170D = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00060590 File Offset: 0x0005F590
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x000605D4 File Offset: 0x0005F5D4
		public bool IsString
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00060618 File Offset: 0x0005F618
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0006065C File Offset: 0x0005F65C
		public bool IsBlob
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x000606A0 File Offset: 0x0005F6A0
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x000606E4 File Offset: 0x0005F6E4
		public bool IsMemo
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00060728 File Offset: 0x0005F728
		public bool IsDefaultFormat
		{
			get
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
						goto IL_E5;
					case 2:
						if (this.ᜀ.OwnerFormatsExport != null)
						{
							num = 5;
							continue;
						}
						return false;
					case 3:
						return false;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E5;
						default:
						{
							if (false)
							{
							}
							ColExportType colExportType;
							switch (colExportType)
							{
							case ColExportType.Integer:
							case ColExportType.Bigint:
								goto IL_14F;
							case ColExportType.Float:
								goto IL_72;
							case ColExportType.Currency:
								goto IL_EA;
							case ColExportType.DateTime:
								goto IL_131;
							case ColExportType.Time:
								goto IL_4C;
							default:
								num = 0;
								continue;
							}
							break;
						}
						}
						break;
					case 5:
					{
						ColExportType colExportType = this.ᜂ;
						num = 4;
						continue;
					}
					case 6:
						num = 2;
						continue;
					}
					if (this.ᜌ)
					{
						num = 6;
						continue;
					}
					return false;
					IL_E5:
					num = 3;
				}
				IL_4C:
				return this.ᜇ == this.ᜀ.OwnerFormatsExport.Time;
				IL_72:
				return this.ᜇ == this.ᜀ.OwnerFormatsExport.Float;
				IL_EA:
				return this.ᜇ == this.ᜀ.OwnerFormatsExport.Currency;
				IL_131:
				return this.ᜇ == this.ᜀ.OwnerFormatsExport.DateTime;
				IL_14F:
				return this.ᜇ == this.ᜀ.OwnerFormatsExport.Integer;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000608B4 File Offset: 0x0005F8B4
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x000608F8 File Offset: 0x0005F8F8
		public bool IsExported
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0006093C File Offset: 0x0005F93C
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00060980 File Offset: 0x0005F980
		public bool NotTruncatable
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

		// Token: 0x0400071E RID: 1822
		private byte \u2460\u008F\u00AE\u0090;

		// Token: 0x0400071F RID: 1823
		private ColumnsExport ᜀ;

		// Token: 0x04000720 RID: 1824
		private int ᜁ = -1;

		// Token: 0x04000721 RID: 1825
		private ColExportType ᜂ = ColExportType.Unknown;

		// Token: 0x04000722 RID: 1826
		private string ᜃ = string.Empty;

		// Token: 0x04000723 RID: 1827
		private string ᜄ = string.Empty;

		// Token: 0x04000724 RID: 1828
		private int ᜅ;

		// Token: 0x04000725 RID: 1829
		private ColumAlign ᜆ;

		// Token: 0x04000726 RID: 1830
		private long \u25D9\u008F\u0082\u0089;

		// Token: 0x04000727 RID: 1831
		private byte[] \u2460\u00A9\u0094\u00A1;

		// Token: 0x04000728 RID: 1832
		private string ᜇ = string.Empty;

		// Token: 0x04000729 RID: 1833
		private string ᜈ = string.Empty;

		// Token: 0x0400072A RID: 1834
		private int ᜉ;

		// Token: 0x0400072B RID: 1835
		private long ᜊ;

		// Token: 0x0400072C RID: 1836
		private int ᜋ;

		// Token: 0x0400072D RID: 1837
		private bool ᜌ;

		// Token: 0x0400072E RID: 1838
		private bool \u170D;

		// Token: 0x0400072F RID: 1839
		private bool ᜎ;

		// Token: 0x04000730 RID: 1840
		private bool ᜏ;

		// Token: 0x04000731 RID: 1841
		private bool ᜐ;

		// Token: 0x04000732 RID: 1842
		private float[] \u2460\u008F\u0097\u00A6;

		// Token: 0x04000733 RID: 1843
		private bool ᜑ;

		// Token: 0x04000734 RID: 1844
		private bool \u1712;
	}
}
