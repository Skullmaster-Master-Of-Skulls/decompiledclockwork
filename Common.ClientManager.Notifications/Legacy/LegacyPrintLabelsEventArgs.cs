using System;

namespace TechnoPro.Common.ClientManager.Notifications.Legacy
{
	// Token: 0x0200000D RID: 13
	public class LegacyPrintLabelsEventArgs : EventArgs
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000030F7 File Offset: 0x000012F7
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000030FF File Offset: 0x000012FF
		public string LabelsString { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003108 File Offset: 0x00001308
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003110 File Offset: 0x00001310
		public bool ShowPrintPreview { get; set; }
	}
}
