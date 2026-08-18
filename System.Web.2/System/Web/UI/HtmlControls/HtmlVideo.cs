using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200033F RID: 831
	public class HtmlVideo : HtmlContainerControl
	{
		// Token: 0x0600264E RID: 9806 RVA: 0x0007E03C File Offset: 0x0007C23C
		public HtmlVideo() : base("video")
		{
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x0007E04C File Offset: 0x0007C24C
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x0007E074 File Offset: 0x0007C274
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string Poster
		{
			get
			{
				string text = base.Attributes["poster"];
				return text ?? string.Empty;
			}
			set
			{
				base.Attributes["poster"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x0007E08C File Offset: 0x0007C28C
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x0007DF48 File Offset: 0x0007C148
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

		// Token: 0x06002653 RID: 9811 RVA: 0x0007E0B4 File Offset: 0x0007C2B4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.PreProcessRelativeReferenceAttribute(writer, "poster");
			base.RenderAttributes(writer);
		}
	}
}
