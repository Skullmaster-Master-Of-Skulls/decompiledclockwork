using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F12 RID: 3858
	public class RadTabStripReorderedEventArgs : RadTabStripEventArgs
	{
		// Token: 0x06009246 RID: 37446 RVA: 0x0020F27F File Offset: 0x0020D47F
		public RadTabStripReorderedEventArgs(RadTab tab, int offset) : base(tab)
		{
			this._offset = offset;
		}

		// Token: 0x17002E39 RID: 11833
		// (get) Token: 0x06009247 RID: 37447 RVA: 0x0020F28F File Offset: 0x0020D48F
		// (set) Token: 0x06009248 RID: 37448 RVA: 0x0020F297 File Offset: 0x0020D497
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x04002A39 RID: 10809
		private int _offset;
	}
}
