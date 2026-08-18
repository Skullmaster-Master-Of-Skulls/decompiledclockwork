using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000008 RID: 8
	public class IdentityMessage
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000024C8 File Offset: 0x000006C8
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000024D0 File Offset: 0x000006D0
		public virtual string Destination { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024D9 File Offset: 0x000006D9
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000024E1 File Offset: 0x000006E1
		public virtual string Subject { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024EA File Offset: 0x000006EA
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024F2 File Offset: 0x000006F2
		public virtual string Body { get; set; }
	}
}
