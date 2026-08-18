using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200033E RID: 830
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlTrack : HtmlControl
	{
		// Token: 0x0600264A RID: 9802 RVA: 0x0007E004 File Offset: 0x0007C204
		public HtmlTrack() : base("track")
		{
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0007E014 File Offset: 0x0007C214
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x0007DF48 File Offset: 0x0007C148
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

		// Token: 0x0600264D RID: 9805 RVA: 0x0007DFE4 File Offset: 0x0007C1E4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
