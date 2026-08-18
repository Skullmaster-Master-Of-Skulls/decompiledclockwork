using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200033D RID: 829
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlSource : HtmlControl
	{
		// Token: 0x06002646 RID: 9798 RVA: 0x0007DFAC File Offset: 0x0007C1AC
		public HtmlSource() : base("source")
		{
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x0007DFBC File Offset: 0x0007C1BC
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x0007DF48 File Offset: 0x0007C148
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

		// Token: 0x06002649 RID: 9801 RVA: 0x0007DFE4 File Offset: 0x0007C1E4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
