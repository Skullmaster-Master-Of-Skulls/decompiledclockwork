using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000082 RID: 130
	public class ViewEventArgs : EventArgs
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x000131F1 File Offset: 0x000113F1
		public ViewEventArgs(ViewEvent eventType, DesignerRegion region, EventArgs eventArgs)
		{
			this._eventType = eventType;
			this._region = region;
			this._eventArgs = eventArgs;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001320E File Offset: 0x0001140E
		public EventArgs EventArgs
		{
			get
			{
				return this._eventArgs;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00013216 File Offset: 0x00011416
		public ViewEvent EventType
		{
			get
			{
				return this._eventType;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001321E File Offset: 0x0001141E
		public DesignerRegion Region
		{
			get
			{
				return this._region;
			}
		}

		// Token: 0x040001A7 RID: 423
		private DesignerRegion _region;

		// Token: 0x040001A8 RID: 424
		private EventArgs _eventArgs;

		// Token: 0x040001A9 RID: 425
		private ViewEvent _eventType;
	}
}
