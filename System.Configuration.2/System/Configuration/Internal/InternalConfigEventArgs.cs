using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000BA RID: 186
	public sealed class InternalConfigEventArgs : EventArgs
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public InternalConfigEventArgs(string configPath)
		{
			this._configPath = configPath;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001F9DF File Offset: 0x0001DBDF
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001F9E7 File Offset: 0x0001DBE7
		public string ConfigPath
		{
			get
			{
				return this._configPath;
			}
			set
			{
				this._configPath = value;
			}
		}

		// Token: 0x04000454 RID: 1108
		private string _configPath;
	}
}
