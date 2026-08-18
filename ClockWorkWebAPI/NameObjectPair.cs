using System;

namespace ClockWorkWebAPI
{
	// Token: 0x0200001D RID: 29
	public class NameObjectPair
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000D110 File Offset: 0x0000B310
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000D11C File Offset: 0x0000B31C
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000D134 File Offset: 0x0000B334
		public object Value
		{
			get
			{
				return this.o;
			}
			set
			{
				this.o = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000D140 File Offset: 0x0000B340
		public bool IsNull
		{
			get
			{
				return this.o == null;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000D15B File Offset: 0x0000B35B
		public NameObjectPair(string name, object val)
		{
			this.name = name;
			this.o = val;
		}

		// Token: 0x0400008A RID: 138
		private string name;

		// Token: 0x0400008B RID: 139
		private object o;
	}
}
