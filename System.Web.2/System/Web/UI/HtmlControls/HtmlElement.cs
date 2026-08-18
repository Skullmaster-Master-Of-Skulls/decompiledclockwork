using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000339 RID: 825
	public class HtmlElement : HtmlContainerControl
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x0007DE3C File Offset: 0x0007C03C
		public HtmlElement() : base("html")
		{
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x0007DE4C File Offset: 0x0007C04C
		// (set) Token: 0x06002638 RID: 9784 RVA: 0x0007DE74 File Offset: 0x0007C074
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string Manifest
		{
			get
			{
				string text = base.Attributes["manifest"];
				return text ?? string.Empty;
			}
			set
			{
				base.Attributes["manifest"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0007DE8C File Offset: 0x0007C08C
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "manifest");
			base.RenderAttributes(writer);
		}
	}
}
