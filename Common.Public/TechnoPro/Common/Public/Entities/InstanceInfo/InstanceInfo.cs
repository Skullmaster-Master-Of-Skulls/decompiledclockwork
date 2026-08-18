using System;
using TechnoPro.Common.Public.Entities.Database;

namespace TechnoPro.Common.Public.Entities.InstanceInfo
{
	// Token: 0x0200032B RID: 811
	[Serializable]
	public class InstanceInfo : BusinessBase<string>
	{
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0001DD8D File Offset: 0x0001BF8D
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0001DD95 File Offset: 0x0001BF95
		public string Sitename { get; set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string InstanceName
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		public string AppPoolName { get; set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0001DDC9 File Offset: 0x0001BFC9
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0001DDD1 File Offset: 0x0001BFD1
		public string InstallationPath { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0001DDDA File Offset: 0x0001BFDA
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x0001DDE2 File Offset: 0x0001BFE2
		public string Version { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0001DDEB File Offset: 0x0001BFEB
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x0001DDF3 File Offset: 0x0001BFF3
		public DbConnectionInfo DbConnectionInfo { get; set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0001DDFC File Offset: 0x0001BFFC
		public string VirtualDirectory
		{
			get
			{
				return this.InstanceName;
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0001DE14 File Offset: 0x0001C014
		public InstanceInfo()
		{
			this.Sitename = "Default Web Site";
		}
	}
}
