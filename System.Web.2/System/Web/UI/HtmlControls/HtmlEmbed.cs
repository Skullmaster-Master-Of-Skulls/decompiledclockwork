using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200033B RID: 827
	public class HtmlEmbed : HtmlContainerControl
	{
		// Token: 0x0600263E RID: 9790 RVA: 0x0007DF10 File Offset: 0x0007C110
		public HtmlEmbed() : base("embed")
		{
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x0007DF20 File Offset: 0x0007C120
		// (set) Token: 0x06002640 RID: 9792 RVA: 0x0007DF48 File Offset: 0x0007C148
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string Src
		{
			get
			{
				string text = base.Attributes["src"];
				return text ?? string.Empty;
			}
			set
			{
				base.Attributes["src"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x0007DF60 File Offset: 0x0007C160
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
		}
	}
}
