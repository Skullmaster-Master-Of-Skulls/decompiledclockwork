using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ClockWorkServer
{
	// Token: 0x0200044D RID: 1101
	public class ServiceDiscoveryInfo
	{
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000255D0 File Offset: 0x000237D0
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x000255D8 File Offset: 0x000237D8
		public Uri EnpointAddress { get; set; }

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000255E1 File Offset: 0x000237E1
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x000255E9 File Offset: 0x000237E9
		public IList<Uri> Scopes { get; set; }
	}
}
