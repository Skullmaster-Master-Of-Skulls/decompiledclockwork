using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000579 RID: 1401
	public class WebPartAddingEventArgs : WebPartCancelEventArgs
	{
		// Token: 0x06004735 RID: 18229 RVA: 0x000EA7A2 File Offset: 0x000E89A2
		public WebPartAddingEventArgs(WebPart webPart, WebPartZoneBase zone, int zoneIndex) : base(webPart)
		{
			this._zone = zone;
			this._zoneIndex = zoneIndex;
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06004736 RID: 18230 RVA: 0x000EA7B9 File Offset: 0x000E89B9
		// (set) Token: 0x06004737 RID: 18231 RVA: 0x000EA7C1 File Offset: 0x000E89C1
		public WebPartZoneBase Zone
		{
			get
			{
				return this._zone;
			}
			set
			{
				this._zone = value;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x000EA7CA File Offset: 0x000E89CA
		// (set) Token: 0x06004739 RID: 18233 RVA: 0x000EA7D2 File Offset: 0x000E89D2
		public int ZoneIndex
		{
			get
			{
				return this._zoneIndex;
			}
			set
			{
				this._zoneIndex = value;
			}
		}

		// Token: 0x040026E4 RID: 9956
		private WebPartZoneBase _zone;

		// Token: 0x040026E5 RID: 9957
		private int _zoneIndex;
	}
}
