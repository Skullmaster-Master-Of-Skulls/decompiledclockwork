using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authorization
{
	// Token: 0x02000494 RID: 1172
	public class AuthorizationContextItem
	{
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x00026E01 File Offset: 0x00025001
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x00026E09 File Offset: 0x00025009
		public eAuthorizationContextItemType ContextItemType { get; set; }

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x00026E12 File Offset: 0x00025012
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x00026E1A File Offset: 0x0002501A
		public string Title { get; set; }

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x00026E23 File Offset: 0x00025023
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x00026E2B File Offset: 0x0002502B
		public bool IsDisabled { get; set; }

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00026E34 File Offset: 0x00025034
		// (set) Token: 0x06002363 RID: 9059 RVA: 0x00026E3C File Offset: 0x0002503C
		public eLookupMethod LookupMethod { get; set; }

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x00026E45 File Offset: 0x00025045
		// (set) Token: 0x06002365 RID: 9061 RVA: 0x00026E4D File Offset: 0x0002504D
		public int LookupMethodCid { get; set; }

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x00026E56 File Offset: 0x00025056
		// (set) Token: 0x06002367 RID: 9063 RVA: 0x00026E5E File Offset: 0x0002505E
		public string UsernamePostfix { get; set; }

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x00026E67 File Offset: 0x00025067
		// (set) Token: 0x06002369 RID: 9065 RVA: 0x00026E6F File Offset: 0x0002506F
		public int OrderId { get; set; }
	}
}
