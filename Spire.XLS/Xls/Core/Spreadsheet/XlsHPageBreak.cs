using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200005E RID: 94
	public class XlsHPageBreak : XlsObject, IHPageBreak
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0005C5B8 File Offset: 0x0005B5B8
		public PageBreakExtentType Extent
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

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0005C5FC File Offset: 0x0005B5FC
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0005C67C File Offset: 0x0005B67C
		internal IXLSRange Location
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
				return this.ᜃ.AllocatedRange[(int)(this.ᜂ.ᜃ() + 1), (int)(this.ᜂ.ᜀ() + 1), (int)(this.ᜂ.ᜃ() + 1), (int)(this.ᜂ.ᜁ() + 1)];
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
				this.ᜂ.ᜂ((ushort)(value.Column - 1));
				this.ᜂ.ᜁ((ushort)(value.LastColumn - 1));
				this.ᜂ.ᜀ((ushort)(value.Row - 1));
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0005C6F4 File Offset: 0x0005B6F4
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0005C738 File Offset: 0x0005B738
		public PageBreakType Type
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0005C77C File Offset: 0x0005B77C
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0005C7C4 File Offset: 0x0005B7C4
		public int StartColumn
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
				return (int)(this.ᜂ.ᜀ() + 1);
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
				this.ᜂ.ᜂ((ushort)(value - 1));
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0005C810 File Offset: 0x0005B810
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0005C858 File Offset: 0x0005B858
		public int EndColumn
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
				return (int)(this.ᜂ.ᜁ() + 1);
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
				this.ᜂ.ᜁ((ushort)(value - 1));
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0005C8A4 File Offset: 0x0005B8A4
		internal XlsHPageBreak(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0005C8D0 File Offset: 0x0005B8D0
		private XlsHPageBreak(spr\u1DF5 A_0, object A_1, sprἛ A_2) : this(A_0, A_1)
		{
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0005C8E8 File Offset: 0x0005B8E8
		internal XlsHPageBreak(spr\u1DF5 A_0, object A_1, spr\u2539.ᜀ A_2) : this(A_0, A_1)
		{
			this.ᜂ = A_2;
			this.ᜁ = PageBreakType.Manual;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0005C90C File Offset: 0x0005B90C
		internal XlsHPageBreak(spr\u1DF5 A_0, object A_1, IXLSRange A_2) : this(A_0, A_1)
		{
			this.ᜂ = new spr\u2539.ᜀ();
			this.ᜂ.ᜀ((ushort)(A_2.Row - 1));
			this.ᜂ.ᜂ((ushort)(A_2.Column - 1));
			this.ᜂ.ᜁ((ushort)(A_2.LastColumn - 1));
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0005C968 File Offset: 0x0005B968
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0005C9D0 File Offset: 0x0005B9D0
		internal spr\u2539.ᜀ HPageBreak
		{
			get
			{
				int a_ = 2;
				if (this.ᜂ == null)
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
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("瀷樹崻夽┿A㙃⍅⥇ⅉ", a_));
				}
				return this.ᜂ;
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
				this.ᜂ = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0005CA14 File Offset: 0x0005BA14
		public int Row
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
				return (int)this.ᜂ.ᜃ();
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0005CA5C File Offset: 0x0005BA5C
		private void ᜀ()
		{
			int a_ = 5;
			object obj = base.FindParent(typeof(XlsWorksheet));
			if (obj == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("欺尼䴾⑀ⵂㅄ杆♈⥊❌⩎㉐❒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
			}
			this.ᜃ = (XlsWorksheet)obj;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0005CAD8 File Offset: 0x0005BAD8
		public XlsHPageBreak Clone(object parent)
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
			XlsHPageBreak xlsHPageBreak = (XlsHPageBreak)base.MemberwiseClone();
			xlsHPageBreak.SetParent(parent);
			xlsHPageBreak.ᜀ();
			this.ᜂ = (spr\u2539.ᜀ)this.ᜂ.ᜂ();
			return xlsHPageBreak;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0005CB44 File Offset: 0x0005BB44
		internal void ᜀ(int A_0, int A_1, int A_2)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 2:
					if (true)
					{
					}
					for (;;)
					{
						this.ᜂ = new spr\u2539.ᜀ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_61;
						}
					}
					IL_61:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				if (this.ᜂ != null)
				{
					break;
				}
				num = 2;
			}
			IL_6F:
			this.ᜂ.ᜀ((ushort)(A_0 - 1));
			this.ᜂ.ᜂ((ushort)(A_1 - 1));
			this.ᜂ.ᜁ((ushort)(A_2 - 1));
		}

		// Token: 0x040001B1 RID: 433
		private PageBreakExtentType ᜀ = PageBreakExtentType.Partial;

		// Token: 0x040001B2 RID: 434
		private long[] \u2460\u0099\u00AF\u0089;

		// Token: 0x040001B3 RID: 435
		private string[] \u2609\u008B\u0088\u0084;

		// Token: 0x040001B4 RID: 436
		private PageBreakType ᜁ = PageBreakType.Manual;

		// Token: 0x040001B5 RID: 437
		private int[] \u25D9\u0087\u0097\u00A3;

		// Token: 0x040001B6 RID: 438
		private bool \u25D9\u0082\u0093\u00A3;

		// Token: 0x040001B7 RID: 439
		private spr\u2539.ᜀ ᜂ;

		// Token: 0x040001B8 RID: 440
		private XlsWorksheet ᜃ;
	}
}
