using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200033C RID: 828
	public class HtmlIframe : HtmlContainerControl
	{
		// Token: 0x06002642 RID: 9794 RVA: 0x0007DF75 File Offset: 0x0007C175
		public HtmlIframe() : base("iframe")
		{
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x0007DF84 File Offset: 0x0007C184
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x0007DF48 File Offset: 0x0007C148
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

		// Token: 0x06002645 RID: 9797 RVA: 0x0007DF60 File Offset: 0x0007C160
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
		}
	}
}
