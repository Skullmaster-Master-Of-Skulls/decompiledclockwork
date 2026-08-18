using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000152 RID: 338
	[NLogConfigurationItem]
	public class DatabaseCommandInfo
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x0001C195 File Offset: 0x0001A395
		public DatabaseCommandInfo()
		{
			this.Parameters = new List<DatabaseParameterInfo>();
			this.CommandType = CommandType.Text;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0001C1AF File Offset: 0x0001A3AF
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0001C1B7 File Offset: 0x0001A3B7
		[DefaultValue(CommandType.Text)]
		[RequiredParameter]
		public CommandType CommandType { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		public Layout ConnectionString { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0001C1D1 File Offset: 0x0001A3D1
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0001C1D9 File Offset: 0x0001A3D9
		[RequiredParameter]
		public Layout Text { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0001C1E2 File Offset: 0x0001A3E2
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0001C1EA File Offset: 0x0001A3EA
		public bool IgnoreFailures { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0001C1F3 File Offset: 0x0001A3F3
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0001C1FB File Offset: 0x0001A3FB
		[ArrayParameter(typeof(DatabaseParameterInfo), "parameter")]
		public IList<DatabaseParameterInfo> Parameters { get; private set; }
	}
}
