using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F11 RID: 3857
	public class RadTabStripEventArgs : EventArgs
	{
		// Token: 0x06009243 RID: 37443 RVA: 0x0020F25F File Offset: 0x0020D45F
		public RadTabStripEventArgs(RadTab tab)
		{
			this._tab = tab;
		}

		// Token: 0x17002E38 RID: 11832
		// (get) Token: 0x06009244 RID: 37444 RVA: 0x0020F26E File Offset: 0x0020D46E
		// (set) Token: 0x06009245 RID: 37445 RVA: 0x0020F276 File Offset: 0x0020D476
		public RadTab Tab
		{
			get
			{
				return this._tab;
			}
			set
			{
				this._tab = value;
			}
		}

		// Token: 0x04002A38 RID: 10808
		private RadTab _tab;
	}
}
