using System;
using System.Collections;

namespace log4net.Repository
{
	// Token: 0x020000D4 RID: 212
	public class ConfigurationChangedEventArgs : EventArgs
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x00014775 File Offset: 0x00012975
		public ConfigurationChangedEventArgs(ICollection configurationMessages)
		{
			this.configurationMessages = configurationMessages;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00014784 File Offset: 0x00012984
		public ICollection ConfigurationMessages
		{
			get
			{
				return this.configurationMessages;
			}
		}

		// Token: 0x04000289 RID: 649
		private readonly ICollection configurationMessages;
	}
}
