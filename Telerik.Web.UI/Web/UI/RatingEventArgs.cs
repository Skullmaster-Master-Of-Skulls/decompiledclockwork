using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F27 RID: 3879
	public class RatingEventArgs : EventArgs
	{
		// Token: 0x060093F6 RID: 37878 RVA: 0x00212E70 File Offset: 0x00211070
		public RatingEventArgs(RadRatingItem item)
		{
			this._item = item;
		}

		// Token: 0x17002ECE RID: 11982
		// (get) Token: 0x060093F7 RID: 37879 RVA: 0x00212E7F File Offset: 0x0021107F
		// (set) Token: 0x060093F8 RID: 37880 RVA: 0x00212E87 File Offset: 0x00211087
		public RadRatingItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x04002A6D RID: 10861
		private RadRatingItem _item;
	}
}
