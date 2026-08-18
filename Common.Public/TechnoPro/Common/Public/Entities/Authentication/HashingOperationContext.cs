using System;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x0200048E RID: 1166
	public class HashingOperationContext : OperationContext
	{
		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00026C3E File Offset: 0x00024E3E
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x00026C46 File Offset: 0x00024E46
		public string HashingKey { get; set; }

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x00026C4F File Offset: 0x00024E4F
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x00026C57 File Offset: 0x00024E57
		public int TokenLifetimeInMinutes { get; set; }
	}
}
