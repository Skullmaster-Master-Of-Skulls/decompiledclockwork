using System;
using TechnoPro.Common.Public.Entities.Database;

namespace TechnoPro.Common.Public.Entities.InstanceInfo
{
	// Token: 0x0200032C RID: 812
	[Serializable]
	public sealed class ServerInstanceInfo : InstanceInfo
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0001DE2A File Offset: 0x0001C02A
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0001DE32 File Offset: 0x0001C032
		public eClockWorkServerInstanceName ClockWorkServerInstanceName { get; set; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0001DE3B File Offset: 0x0001C03B
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x0001DE43 File Offset: 0x0001C043
		public string ProgramFilesFolder { get; set; }

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0001DE4C File Offset: 0x0001C04C
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x0001DE54 File Offset: 0x0001C054
		public string X509FindType { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x0001DE5D File Offset: 0x0001C05D
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x0001DE65 File Offset: 0x0001C065
		public string X509FindValue { get; set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0001DE70 File Offset: 0x0001C070
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x0001DE88 File Offset: 0x0001C088
		public DbConnectionInfo ClockWorkServerDbConnectionInfo
		{
			get
			{
				return base.DbConnectionInfo;
			}
			set
			{
				base.DbConnectionInfo = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0001DE93 File Offset: 0x0001C093
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x0001DE9B File Offset: 0x0001C09B
		public DbConnectionInfo ClockWorkTrackingDbConnectionInfo { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		public DbConnectionInfo ClockWorkFilesDbConnectionInfo { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0001DEB5 File Offset: 0x0001C0B5
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0001DEBD File Offset: 0x0001C0BD
		public string PatchUsername { get; set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0001DEC6 File Offset: 0x0001C0C6
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x0001DECE File Offset: 0x0001C0CE
		public string PatchPassword { get; set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		public bool ContainsPatchCredentials
		{
			get
			{
				return !string.IsNullOrEmpty(this.PatchUsername) && !string.IsNullOrEmpty(this.PatchPassword);
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0001DF08 File Offset: 0x0001C108
		public ServerInstanceInfo()
		{
			this.ClockWorkServerInstanceName = eClockWorkServerInstanceName.ClockWorkServer;
		}
	}
}
