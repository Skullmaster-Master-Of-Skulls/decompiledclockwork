using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200033A RID: 826
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlArea : HtmlControl
	{
		// Token: 0x0600263A RID: 9786 RVA: 0x0007DEA1 File Offset: 0x0007C0A1
		public HtmlArea() : base("area")
		{
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x0007DEB0 File Offset: 0x0007C0B0
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x0007DED8 File Offset: 0x0007C0D8
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string Href
		{
			get
			{
				string text = base.Attributes["href"];
				return text ?? string.Empty;
			}
			set
			{
				base.Attributes["href"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0007DEF0 File Offset: 0x0007C0F0
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "href");
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
