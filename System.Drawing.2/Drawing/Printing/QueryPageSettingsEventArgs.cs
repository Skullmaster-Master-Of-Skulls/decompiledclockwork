using System;

namespace System.Drawing.Printing
{
	// Token: 0x0200006F RID: 111
	public class QueryPageSettingsEventArgs : PrintEventArgs
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x00020BA7 File Offset: 0x0001EDA7
		public QueryPageSettingsEventArgs(PageSettings pageSettings)
		{
			this.pageSettings = pageSettings;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00020BB6 File Offset: 0x0001EDB6
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00020BC5 File Offset: 0x0001EDC5
		public PageSettings PageSettings
		{
			get
			{
				this.PageSettingsChanged = true;
				return this.pageSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PageSettings();
				}
				this.pageSettings = value;
				this.PageSettingsChanged = true;
			}
		}

		// Token: 0x040006FF RID: 1791
		private PageSettings pageSettings;

		// Token: 0x04000700 RID: 1792
		internal bool PageSettingsChanged;
	}
}
