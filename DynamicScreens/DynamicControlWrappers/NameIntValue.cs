using System;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200005F RID: 95
	public class NameIntValue
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00041496 File Offset: 0x00040496
		public NameIntValue(string name, int val)
		{
			this.name = name;
			this.val = val;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000414B0 File Offset: 0x000404B0
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x000414C8 File Offset: 0x000404C8
		public int Val
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000414D4 File Offset: 0x000404D4
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x000414EC File Offset: 0x000404EC
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

		// Token: 0x060004FC RID: 1276 RVA: 0x000414F8 File Offset: 0x000404F8
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0400037D RID: 893
		private int val;

		// Token: 0x0400037E RID: 894
		public string name;
	}
}
