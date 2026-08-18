using System;

namespace TechnoPro.Common.ClientManager.Notifications.Legacy
{
	// Token: 0x0200000E RID: 14
	public class LegacyPrintControlEventArgs : EventArgs
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003119 File Offset: 0x00001319
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003121 File Offset: 0x00001321
		public object HeaderControl { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000312A File Offset: 0x0000132A
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003132 File Offset: 0x00001332
		public object Control { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000313B File Offset: 0x0000133B
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003143 File Offset: 0x00001343
		public bool ShowPrintPreview { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000314C File Offset: 0x0000134C
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003154 File Offset: 0x00001354
		public string Title { get; set; }
	}
}
