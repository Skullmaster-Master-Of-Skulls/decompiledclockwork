using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A9 RID: 1449
	public class WebPartMovingEventArgs : WebPartCancelEventArgs
	{
		// Token: 0x0600495C RID: 18780 RVA: 0x000F3F92 File Offset: 0x000F2192
		public WebPartMovingEventArgs(WebPart webPart, WebPartZoneBase zone, int zoneIndex) : base(webPart)
		{
			this._zone = zone;
			this._zoneIndex = zoneIndex;
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x000F3FA9 File Offset: 0x000F21A9
		// (set) Token: 0x0600495E RID: 18782 RVA: 0x000F3FB1 File Offset: 0x000F21B1
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

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x000F3FBA File Offset: 0x000F21BA
		// (set) Token: 0x06004960 RID: 18784 RVA: 0x000F3FC2 File Offset: 0x000F21C2
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

		// Token: 0x0400279D RID: 10141
		private WebPartZoneBase _zone;

		// Token: 0x0400279E RID: 10142
		private int _zoneIndex;
	}
}
