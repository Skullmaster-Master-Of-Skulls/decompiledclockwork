using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x0200049A RID: 1178
	public class AuthenticationContextItem
	{
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x00026F33 File Offset: 0x00025133
		// (set) Token: 0x06002381 RID: 9089 RVA: 0x00026F3B File Offset: 0x0002513B
		public eAuthenticationContextItemType ContextItemType { get; set; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x00026F44 File Offset: 0x00025144
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x00026F4C File Offset: 0x0002514C
		public bool IsDisabled { get; set; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x00026F55 File Offset: 0x00025155
		// (set) Token: 0x06002385 RID: 9093 RVA: 0x00026F5D File Offset: 0x0002515D
		public int OrderId { get; set; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00026F66 File Offset: 0x00025166
		// (set) Token: 0x06002387 RID: 9095 RVA: 0x00026F6E File Offset: 0x0002516E
		public IDictionary<string, string> Args { get; set; }
	}
}
