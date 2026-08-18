using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000153 RID: 339
	[NLogConfigurationItem]
	public class DatabaseParameterInfo
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0001C204 File Offset: 0x0001A404
		public DatabaseParameterInfo() : this(null, null)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0001C20E File Offset: 0x0001A40E
		public DatabaseParameterInfo(string parameterName, Layout parameterLayout)
		{
			this.Name = parameterName;
			this.Layout = parameterLayout;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0001C224 File Offset: 0x0001A424
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0001C22C File Offset: 0x0001A42C
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0001C235 File Offset: 0x0001A435
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x0001C23D File Offset: 0x0001A43D
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0001C246 File Offset: 0x0001A446
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x0001C24E File Offset: 0x0001A44E
		[DefaultValue(0)]
		public int Size { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001C257 File Offset: 0x0001A457
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x0001C25F File Offset: 0x0001A45F
		[DefaultValue(0)]
		public byte Precision { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0001C268 File Offset: 0x0001A468
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0001C270 File Offset: 0x0001A470
		[DefaultValue(0)]
		public byte Scale { get; set; }
	}
}
