using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000F36 RID: 3894
	[XmlRoot("ToggleButton")]
	public class RibbonBarToggleButton : RibbonBarButton, IXmlSerializable
	{
		// Token: 0x17002EF9 RID: 12025
		// (get) Token: 0x06009479 RID: 38009 RVA: 0x00214126 File Offset: 0x00212326
		// (set) Token: 0x0600947A RID: 38010 RVA: 0x0021412E File Offset: 0x0021232E
		[DefaultValue(false)]
		public bool Toggled { get; set; }

		// Token: 0x17002EFA RID: 12026
		// (get) Token: 0x0600947B RID: 38011 RVA: 0x00214137 File Offset: 0x00212337
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.ToggleButton;
			}
		}

		// Token: 0x17002EFB RID: 12027
		// (get) Token: 0x0600947C RID: 38012 RVA: 0x0021413A File Offset: 0x0021233A
		internal override string RibbonBarItemTypeCssClass
		{
			get
			{
				return "rrbToggleButton";
			}
		}

		// Token: 0x0600947D RID: 38013 RVA: 0x00214141 File Offset: 0x00212341
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarToggleButtonLiteRenderer(this);
			}
			return new RibbonBarToggleButtonClassicRenderer(this);
		}
	}
}
