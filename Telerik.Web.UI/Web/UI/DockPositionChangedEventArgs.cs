using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001046 RID: 4166
	public class DockPositionChangedEventArgs : EventArgs
	{
		// Token: 0x0600A3C4 RID: 41924 RVA: 0x00246BC8 File Offset: 0x00244DC8
		internal DockPositionChangedEventArgs(string dockZoneID, int index) : this(dockZoneID, index, false)
		{
		}

		// Token: 0x0600A3C5 RID: 41925 RVA: 0x00246BD3 File Offset: 0x00244DD3
		internal DockPositionChangedEventArgs(string dockZoneID, int index, bool isDragged)
		{
			this._dockZoneID = dockZoneID;
			this._index = index;
			this._isDragged = isDragged;
		}

		// Token: 0x170033AF RID: 13231
		// (get) Token: 0x0600A3C6 RID: 41926 RVA: 0x00246BF0 File Offset: 0x00244DF0
		public string DockZoneID
		{
			get
			{
				return this._dockZoneID;
			}
		}

		// Token: 0x170033B0 RID: 13232
		// (get) Token: 0x0600A3C7 RID: 41927 RVA: 0x00246BF8 File Offset: 0x00244DF8
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170033B1 RID: 13233
		// (get) Token: 0x0600A3C8 RID: 41928 RVA: 0x00246C00 File Offset: 0x00244E00
		public bool IsDragged
		{
			get
			{
				return this._isDragged;
			}
		}

		// Token: 0x04002D9C RID: 11676
		private readonly string _dockZoneID;

		// Token: 0x04002D9D RID: 11677
		private readonly int _index;

		// Token: 0x04002D9E RID: 11678
		private readonly bool _isDragged;
	}
}
