using System;
using System.Runtime.CompilerServices;

namespace Spire.DataExport.Common
{
	// Token: 0x0200016C RID: 364
	public class ColExport
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x0006121C File Offset: 0x0006021C
		public ColExport(RowExport RowExport)
		{
			this.ᜄ = RowExport;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00061254 File Offset: 0x00060254
		public string GetExportedValue(bool formatValue)
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
			return this.GetExportedValue(formatValue, true);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00061298 File Offset: 0x00060298
		internal bool OriginalDataIsNull
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
				return this.ᜄ.ColumnsExport.GetColumnIsNull(this.ᜃ, spr\u2059.ᜀ);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000612F0 File Offset: 0x000602F0
		public string GetExportedValue(bool formatValue, bool formatNullValue)
		{
			int num = 4;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_123;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_123;
					case 1:
						if (this.ᜄ.GetExportFieldData != null)
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						goto IL_18F;
					case 2:
						goto IL_125;
					case 3:
						if (formatValue)
						{
							num = 9;
							continue;
						}
						goto IL_125;
					case 5:
						num = 6;
						continue;
					case 6:
						if (!formatNullValue)
						{
							num = 0;
							continue;
						}
						this.ᜂ = this.ᜄ.ColumnsExport.ᜀ(this.ᜄ.FormatsExport.NullString);
						num = 2;
						continue;
					case 7:
						goto IL_125;
					case 8:
						goto IL_150;
					case 9:
						this.ᜂ = spr\u2059.ᜀ(this.ᜂ, this.ᜄ.ColumnsExport[this.ᜃ].Format, this.ᜄ.Culture, this.ᜄ.ColumnsExport[this.ᜃ].ColExportType, this.ᜄ.ColumnsExport.NormalFunc);
						num = 7;
						continue;
					}
					if (this.OriginalDataIsNull)
					{
						num = 5;
						break;
					}
					num = 3;
					break;
					IL_125:
					num = 1;
					break;
				}
			}
			IL_123:
			return null;
			IL_150:
			return this.ᜄ.GetExportFieldData(this);
			IL_18F:
			return this.ᜂ;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00061494 File Offset: 0x00060494
		public RowExport RowExport
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
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000614D8 File Offset: 0x000604D8
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0006151C File Offset: 0x0006051C
		public int ColumnIndex
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							this.ᜃ = value;
							num = 1;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_2D:
					num = 2;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (value != this.ᜃ)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00061598 File Offset: 0x00060598
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x000615DC File Offset: 0x000605DC
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
				return this.ᜀ;
			}
			set
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
							goto IL_2A;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜀ = value;
							this.ᜁ = null;
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					goto IL_1C;
					IL_2A:
					num = 1;
					continue;
					IL_1C:
					if (value != this.ᜀ)
					{
						goto IL_2A;
					}
					break;
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00061664 File Offset: 0x00060664
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x000616A8 File Offset: 0x000606A8
		public string Value
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
							goto IL_2A;
						default:
							if (false)
							{
							}
							this.ᜂ = value;
							if (true)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_71;
					}
					goto IL_1C;
					IL_2A:
					num = 0;
					continue;
					IL_1C:
					if (value != this.ᜂ)
					{
						goto IL_2A;
					}
					break;
				}
				IL_71:
				this.IsBinary = false;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00061730 File Offset: 0x00060730
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x00061774 File Offset: 0x00060774
		internal bool IsBinary
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000617B8 File Offset: 0x000607B8
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x000617FC File Offset: 0x000607FC
		internal object DataSource
		{
			[CompilerGenerated]
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00061840 File Offset: 0x00060840
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x000618F8 File Offset: 0x000608F8
		internal string XMLElementName
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.Name != null)
						{
							goto IL_96;
						}
						goto IL_A3;
					case 1:
						num = 0;
						continue;
					case 2:
						goto IL_81;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_96;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜁ = this.Name.Replace(' ', '_');
							num = 2;
							continue;
						}
						break;
					}
					if (this.ᜁ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_96:
					num = 4;
				}
				IL_81:
				IL_A3:
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0006193C File Offset: 0x0006093C
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00061980 File Offset: 0x00060980
		internal object OriginalValue
		{
			[CompilerGenerated]
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
				this.ᜇ = value;
			}
		}

		// Token: 0x04000735 RID: 1845
		private string \u25D9\u009D\u0094\u0083;

		// Token: 0x04000736 RID: 1846
		private string ᜀ = string.Empty;

		// Token: 0x04000737 RID: 1847
		private string ᜁ;

		// Token: 0x04000738 RID: 1848
		private string ᜂ = string.Empty;

		// Token: 0x04000739 RID: 1849
		private int ᜃ = -1;

		// Token: 0x0400073A RID: 1850
		private RowExport ᜄ;

		// Token: 0x0400073B RID: 1851
		private byte \u2460\u00A0\u00A0\u0080;

		// Token: 0x0400073C RID: 1852
		private string[] \u25D9\u00AD\u00A2\u007F;

		// Token: 0x0400073D RID: 1853
		[CompilerGenerated]
		private bool ᜅ;

		// Token: 0x0400073E RID: 1854
		[CompilerGenerated]
		private object ᜆ;

		// Token: 0x0400073F RID: 1855
		[CompilerGenerated]
		private object ᜇ;
	}
}
