using System;
using System.Diagnostics;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000444 RID: 1092
	internal sealed class AppDomainInfo
	{
		// Token: 0x06002A80 RID: 10880 RVA: 0x000A42A8 File Offset: 0x000A24A8
		private AppDomainInfo(AppDomain appDomain)
		{
			this.instanceId = Guid.NewGuid();
			this.friendlyName = appDomain.FriendlyName;
			this.isDefaultAppDomain = appDomain.IsDefaultAppDomain();
			Process currentProcess = Process.GetCurrentProcess();
			this.processName = currentProcess.ProcessName;
			this.machineName = Environment.MachineName;
			this.processId = currentProcess.Id;
			this.id = appDomain.Id;
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x000A4313 File Offset: 0x000A2513
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000A431B File Offset: 0x000A251B
		public Guid InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x000A4323 File Offset: 0x000A2523
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000A432B File Offset: 0x000A252B
		public string Name
		{
			get
			{
				return this.friendlyName;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x000A4333 File Offset: 0x000A2533
		public bool IsDefaultAppDomain
		{
			get
			{
				return this.isDefaultAppDomain;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000A433B File Offset: 0x000A253B
		public int ProcessId
		{
			get
			{
				return this.processId;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000A4343 File Offset: 0x000A2543
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000A434C File Offset: 0x000A254C
		internal static AppDomainInfo Current
		{
			get
			{
				if (AppDomainInfo.singleton == null)
				{
					object obj = AppDomainInfo.syncRoot;
					lock (obj)
					{
						if (AppDomainInfo.singleton == null)
						{
							AppDomainInfo.singleton = new AppDomainInfo(AppDomain.CurrentDomain);
						}
					}
				}
				return AppDomainInfo.singleton;
			}
		}

		// Token: 0x040023EC RID: 9196
		private static object syncRoot = new object();

		// Token: 0x040023ED RID: 9197
		private static volatile AppDomainInfo singleton;

		// Token: 0x040023EE RID: 9198
		private Guid instanceId;

		// Token: 0x040023EF RID: 9199
		private string friendlyName;

		// Token: 0x040023F0 RID: 9200
		private bool isDefaultAppDomain;

		// Token: 0x040023F1 RID: 9201
		private string processName;

		// Token: 0x040023F2 RID: 9202
		private string machineName;

		// Token: 0x040023F3 RID: 9203
		private int processId;

		// Token: 0x040023F4 RID: 9204
		private int id;
	}
}
