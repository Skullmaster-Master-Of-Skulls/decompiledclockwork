using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000340 RID: 832
	public class HtmlAudio : HtmlContainerControl
	{
		// Token: 0x06002654 RID: 9812 RVA: 0x0007E0D5 File Offset: 0x0007C2D5
		public HtmlAudio() : base("audio")
		{
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x0007E0E4 File Offset: 0x0007C2E4
		// (set) Token: 0x06002656 RID: 9814 RVA: 0x0007DF48 File Offset: 0x0007C148
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

		// Token: 0x06002657 RID: 9815 RVA: 0x0007DF60 File Offset: 0x0007C160
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
		}
	}
}
