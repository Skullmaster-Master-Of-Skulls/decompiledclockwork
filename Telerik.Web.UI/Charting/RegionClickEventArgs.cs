using System;

namespace Telerik.Charting
{
	// Token: 0x020016D3 RID: 5843
	public class RegionClickEventArgs : EventArgs
	{
		// Token: 0x17004536 RID: 17718
		// (get) Token: 0x0600E1B0 RID: 57776 RVA: 0x00323026 File Offset: 0x00321226
		// (set) Token: 0x0600E1B1 RID: 57777 RVA: 0x0032302E File Offset: 0x0032122E
		public IActiveRegion Element
		{
			get
			{
				return this.activeRegion;
			}
			set
			{
				this.activeRegion = value;
			}
		}

		// Token: 0x0600E1B2 RID: 57778 RVA: 0x00323037 File Offset: 0x00321237
		public RegionClickEventArgs(IActiveRegion element)
		{
			this.activeRegion = element;
		}

		// Token: 0x0400416D RID: 16749
		private IActiveRegion activeRegion;
	}
}
