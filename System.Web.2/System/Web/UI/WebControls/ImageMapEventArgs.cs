using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000446 RID: 1094
	public class ImageMapEventArgs : EventArgs
	{
		// Token: 0x0600350B RID: 13579 RVA: 0x000AC685 File Offset: 0x000AA885
		public ImageMapEventArgs(string value)
		{
			this._postBackValue = value;
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000AC694 File Offset: 0x000AA894
		public string PostBackValue
		{
			get
			{
				return this._postBackValue;
			}
		}

		// Token: 0x040021B9 RID: 8633
		private string _postBackValue;
	}
}
