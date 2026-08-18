using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000085 RID: 133
	public class ViewRendering
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x00013226 File Offset: 0x00011426
		public ViewRendering(string content, DesignerRegionCollection regions) : this(content, regions, true)
		{
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013231 File Offset: 0x00011431
		public ViewRendering(string content, DesignerRegionCollection regions, bool visible)
		{
			this._content = content;
			this._regions = regions;
			this._visible = visible;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001324E File Offset: 0x0001144E
		public string Content
		{
			get
			{
				if (this._content == null)
				{
					return string.Empty;
				}
				return this._content;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00013264 File Offset: 0x00011464
		public DesignerRegionCollection Regions
		{
			get
			{
				if (this._regions == null)
				{
					this._regions = new DesignerRegionCollection();
				}
				return this._regions;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001327F File Offset: 0x0001147F
		public bool Visible
		{
			get
			{
				return this._visible;
			}
		}

		// Token: 0x040001AE RID: 430
		private string _content;

		// Token: 0x040001AF RID: 431
		private DesignerRegionCollection _regions;

		// Token: 0x040001B0 RID: 432
		private bool _visible;
	}
}
