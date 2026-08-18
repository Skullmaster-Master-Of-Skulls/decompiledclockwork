using System;
using System.Diagnostics.Tracing;

namespace System.ServiceModel
{
	// Token: 0x020000A2 RID: 162
	[EventData(Name = "ServiceKPI")]
	internal struct ServiceKPI
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0001068D File Offset: 0x0000E88D
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00010695 File Offset: 0x0000E895
		[EventField(Tags = (EventFieldTags)134217728)]
		public string ServiceId { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001069E File Offset: 0x0000E89E
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000106A6 File Offset: 0x0000E8A6
		public string HostType { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000106AF File Offset: 0x0000E8AF
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000106B7 File Offset: 0x0000E8B7
		public string EndpointsV2 { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000106C0 File Offset: 0x0000E8C0
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x000106C8 File Offset: 0x0000E8C8
		public string Version { get; set; }
	}
}
