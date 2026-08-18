using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F1 RID: 2289
	internal class MsmqReceiveContextSettings : IReceiveContextSettings
	{
		// Token: 0x06005751 RID: 22353 RVA: 0x0014083D File Offset: 0x0013EA3D
		public MsmqReceiveContextSettings()
		{
			this.ValidityDuration = MsmqDefaults.ValidityDuration;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x00140850 File Offset: 0x0013EA50
		public MsmqReceiveContextSettings(IReceiveContextSettings toBeCloned)
		{
			this.Enabled = toBeCloned.Enabled;
			this.ValidityDuration = toBeCloned.ValidityDuration;
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06005753 RID: 22355 RVA: 0x00140870 File Offset: 0x0013EA70
		// (set) Token: 0x06005754 RID: 22356 RVA: 0x00140878 File Offset: 0x0013EA78
		public TimeSpan ValidityDuration { get; private set; }

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06005755 RID: 22357 RVA: 0x00140881 File Offset: 0x0013EA81
		// (set) Token: 0x06005756 RID: 22358 RVA: 0x00140889 File Offset: 0x0013EA89
		public bool Enabled { get; set; }

		// Token: 0x06005757 RID: 22359 RVA: 0x00140892 File Offset: 0x0013EA92
		internal void SetValidityDuration(TimeSpan validityDuration)
		{
			this.ValidityDuration = validityDuration;
		}
	}
}
