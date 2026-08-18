using System;

namespace System.Configuration
{
	// Token: 0x020006E7 RID: 1767
	public class SettingsLoadedEventArgs : EventArgs
	{
		// Token: 0x060036A3 RID: 13987 RVA: 0x000E91BF File Offset: 0x000E81BF
		public SettingsLoadedEventArgs(SettingsProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x000E91CE File Offset: 0x000E81CE
		public SettingsProvider Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x0400319F RID: 12703
		private SettingsProvider _provider;
	}
}
