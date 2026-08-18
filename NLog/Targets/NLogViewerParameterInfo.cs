using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000169 RID: 361
	[NLogConfigurationItem]
	public class NLogViewerParameterInfo
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0002101B File Offset: 0x0001F21B
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00021023 File Offset: 0x0001F223
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002102C File Offset: 0x0001F22C
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x00021034 File Offset: 0x0001F234
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
