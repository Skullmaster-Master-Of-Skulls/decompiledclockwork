using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AC3 RID: 6851
	public class SkinChangedEventArgs : EventArgs
	{
		// Token: 0x06010958 RID: 67928 RVA: 0x003B30E7 File Offset: 0x003B12E7
		public SkinChangedEventArgs(string _skin)
		{
			this.skin = _skin;
		}

		// Token: 0x170050A3 RID: 20643
		// (get) Token: 0x06010959 RID: 67929 RVA: 0x003B3101 File Offset: 0x003B1301
		public string Skin
		{
			get
			{
				return this.skin;
			}
		}

		// Token: 0x04004A19 RID: 18969
		private string skin = "";
	}
}
