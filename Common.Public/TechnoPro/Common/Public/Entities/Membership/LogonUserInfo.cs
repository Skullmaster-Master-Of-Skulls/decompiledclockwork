using System;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002A6 RID: 678
	public class LogonUserInfo : BusinessBase<string>
	{
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00019FFB File Offset: 0x000181FB
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x0001A003 File Offset: 0x00018203
		public string Firstname { get; set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0001A00C File Offset: 0x0001820C
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x0001A014 File Offset: 0x00018214
		public string Lastname { get; set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0001A020 File Offset: 0x00018220
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Username
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}
	}
}
