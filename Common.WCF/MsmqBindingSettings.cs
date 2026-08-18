using System;
using System.ServiceModel;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class MsmqBindingSettings : BindingSettings
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002315 File Offset: 0x00000515
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000231D File Offset: 0x0000051D
		public bool ExactlyOne { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002326 File Offset: 0x00000526
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000232E File Offset: 0x0000052E
		public TimeSpan TimeToLive { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002337 File Offset: 0x00000537
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000233F File Offset: 0x0000053F
		public DeadLetterQueue DeadLetterQueue { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002348 File Offset: 0x00000548
		public MsmqBindingSettings()
		{
			this.ExactlyOne = false;
			this.TimeToLive = TimeSpan.FromDays(1.0);
		}
	}
}
