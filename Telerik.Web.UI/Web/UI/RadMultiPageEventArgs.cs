using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001ACE RID: 6862
	public class RadMultiPageEventArgs : EventArgs
	{
		// Token: 0x060109EC RID: 68076 RVA: 0x003B52D4 File Offset: 0x003B34D4
		public RadMultiPageEventArgs(RadPageView pageView)
		{
			this._pageView = pageView;
		}

		// Token: 0x170050CF RID: 20687
		// (get) Token: 0x060109ED RID: 68077 RVA: 0x003B52E3 File Offset: 0x003B34E3
		// (set) Token: 0x060109EE RID: 68078 RVA: 0x003B52EB File Offset: 0x003B34EB
		public RadPageView PageView
		{
			get
			{
				return this._pageView;
			}
			set
			{
				this._pageView = value;
			}
		}

		// Token: 0x04004A4C RID: 19020
		private RadPageView _pageView;
	}
}
