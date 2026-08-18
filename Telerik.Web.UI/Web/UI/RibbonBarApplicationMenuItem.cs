using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E3B RID: 3643
	[XmlRoot("ApplicationMenuItem")]
	public class RibbonBarApplicationMenuItem : RibbonBarApplicationMenuItemBase
	{
		// Token: 0x17002BCE RID: 11214
		// (get) Token: 0x06008A9C RID: 35484 RVA: 0x001F9E34 File Offset: 0x001F8034
		// (set) Token: 0x06008A9D RID: 35485 RVA: 0x001F9E54 File Offset: 0x001F8054
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return (string)(this.ViewState["Description"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Description"] = value;
			}
		}

		// Token: 0x06008A9E RID: 35486 RVA: 0x001F9E67 File Offset: 0x001F8067
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarApplicationMenuItemLiteRenderer(this);
			}
			return new RibbonBarApplicationMenuItemClassicRenderer(this);
		}
	}
}
