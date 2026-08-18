using System;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x0200022F RID: 559
	public class PivotReportFilter
	{
		// Token: 0x06002226 RID: 8742 RVA: 0x00131DE4 File Offset: 0x00130DE4
		private PivotReportFilter(List<int> A_0, List<string> A_1, bool A_2)
		{
			this.ᜁ = A_1;
			this.ᜀ = A_2;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00131E08 File Offset: 0x00130E08
		public PivotReportFilter(List<string> filterFieldStrings, bool isMultipleSelect) : this(new List<int>(), filterFieldStrings, isMultipleSelect)
		{
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00131E24 File Offset: 0x00130E24
		public PivotReportFilter(string filterFieldString) : this(new List<string>
		{
			filterFieldString
		}, false)
		{
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00131E48 File Offset: 0x00130E48
		public PivotReportFilter() : this(new List<int>(), new List<string>(), false)
		{
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x00131E68 File Offset: 0x00130E68
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x00131EAC File Offset: 0x00130EAC
		public bool IsMultipleSelect
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00131EF0 File Offset: 0x00130EF0
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x00131F34 File Offset: 0x00130F34
		public List<string> FilterItemStrings
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
			set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						List<string>.Enumerator enumerator;
						switch (num)
						{
						case 1:
							goto IL_F4;
						case 2:
							if (true)
							{
							}
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
									{
										string item;
										this.ᜂ.Add(item);
										num = 5;
										continue;
									}
									case 2:
										goto IL_E4;
									case 3:
										num = 2;
										continue;
									case 4:
									{
										string item;
										if (!value.Contains(item))
										{
											num = 1;
											continue;
										}
										break;
									}
									case 6:
									{
										if (!enumerator.MoveNext())
										{
											num = 3;
											continue;
										}
										string item = enumerator.Current;
										num = 4;
										continue;
									}
									}
									IL_AB:
									num = 6;
									continue;
									goto IL_AB;
								}
								IL_E4:
								goto IL_118;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_F4;
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
							if (this.IsMultipleSelect)
							{
								num = 1;
								continue;
							}
							goto IL_12B;
						}
						IL_F4:
						enumerator = this.ᜁ.GetEnumerator();
						num = 2;
					}
				}
				IL_118:
				this.ᜂ.Clear();
				this.ᜁ = value;
				return;
				IL_12B:
				this.ᜁ.Clear();
				this.ᜁ.Add(value[0]);
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x0013209C File Offset: 0x0013109C
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x001320E0 File Offset: 0x001310E0
		internal List<string> RemovedStrings
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

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x00132124 File Offset: 0x00131124
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x00132168 File Offset: 0x00131168
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

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x001321AC File Offset: 0x001311AC
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x001321F0 File Offset: 0x001311F0
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

		// Token: 0x040011E0 RID: 4576
		private bool ᜀ;

		// Token: 0x040011E1 RID: 4577
		private List<string> ᜁ;

		// Token: 0x040011E2 RID: 4578
		private List<string> ᜂ;

		// Token: 0x040011E3 RID: 4579
		private long[] \u2593\u00AD\u00A6\u007F;

		// Token: 0x040011E4 RID: 4580
		private int ᜃ;

		// Token: 0x040011E5 RID: 4581
		private int ᜄ;
	}
}
