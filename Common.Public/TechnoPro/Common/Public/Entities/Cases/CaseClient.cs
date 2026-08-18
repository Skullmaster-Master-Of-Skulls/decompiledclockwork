using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Cases
{
	// Token: 0x02000468 RID: 1128
	public class CaseClient
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000264A7 File Offset: 0x000246A7
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x000264AF File Offset: 0x000246AF
		public PersonBase Client { get; set; }

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000264B8 File Offset: 0x000246B8
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x000264C0 File Offset: 0x000246C0
		public eCaseClientType ClientType { get; set; }
	}
}
