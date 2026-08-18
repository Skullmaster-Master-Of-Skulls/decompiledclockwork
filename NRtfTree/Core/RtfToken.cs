using System;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x02000015 RID: 21
	public class RtfToken
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006101 File Offset: 0x00004301
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006109 File Offset: 0x00004309
		public RtfTokenType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006112 File Offset: 0x00004312
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000611A File Offset: 0x0000431A
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006123 File Offset: 0x00004323
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000612B File Offset: 0x0000432B
		public bool HasParameter
		{
			get
			{
				return this.hasParam;
			}
			set
			{
				this.hasParam = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006134 File Offset: 0x00004334
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000613C File Offset: 0x0000433C
		public int Parameter
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006145 File Offset: 0x00004345
		public RtfToken()
		{
			this.type = RtfTokenType.None;
			this.key = "";
		}

		// Token: 0x04000069 RID: 105
		private RtfTokenType type;

		// Token: 0x0400006A RID: 106
		private string key;

		// Token: 0x0400006B RID: 107
		private bool hasParam;

		// Token: 0x0400006C RID: 108
		private int param;
	}
}
