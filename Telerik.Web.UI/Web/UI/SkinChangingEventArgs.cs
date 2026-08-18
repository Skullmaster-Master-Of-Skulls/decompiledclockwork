using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AC2 RID: 6850
	public class SkinChangingEventArgs : EventArgs
	{
		// Token: 0x06010953 RID: 67923 RVA: 0x003B30AB File Offset: 0x003B12AB
		public SkinChangingEventArgs(string _skin)
		{
			this.skin = _skin;
		}

		// Token: 0x170050A1 RID: 20641
		// (get) Token: 0x06010954 RID: 67924 RVA: 0x003B30C5 File Offset: 0x003B12C5
		// (set) Token: 0x06010955 RID: 67925 RVA: 0x003B30CD File Offset: 0x003B12CD
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
			set
			{
				this.canceled = value;
			}
		}

		// Token: 0x170050A2 RID: 20642
		// (get) Token: 0x06010956 RID: 67926 RVA: 0x003B30D6 File Offset: 0x003B12D6
		// (set) Token: 0x06010957 RID: 67927 RVA: 0x003B30DE File Offset: 0x003B12DE
		public string Skin
		{
			get
			{
				return this.skin;
			}
			set
			{
				this.skin = value;
			}
		}

		// Token: 0x04004A17 RID: 18967
		private bool canceled;

		// Token: 0x04004A18 RID: 18968
		private string skin = "";
	}
}
