using System;

namespace NLog.Config
{
	// Token: 0x0200004F RID: 79
	public class LoggingConfigurationChangedEventArgs : EventArgs
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00006103 File Offset: 0x00004303
		internal LoggingConfigurationChangedEventArgs(LoggingConfiguration oldConfiguration, LoggingConfiguration newConfiguration)
		{
			this.OldConfiguration = oldConfiguration;
			this.NewConfiguration = newConfiguration;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006119 File Offset: 0x00004319
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006121 File Offset: 0x00004321
		public LoggingConfiguration OldConfiguration { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000612A File Offset: 0x0000432A
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00006132 File Offset: 0x00004332
		public LoggingConfiguration NewConfiguration { get; private set; }
	}
}
