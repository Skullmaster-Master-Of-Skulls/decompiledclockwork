using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000357 RID: 855
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlLink : HtmlControl
	{
		// Token: 0x06002756 RID: 10070 RVA: 0x0007FF19 File Offset: 0x0007E119
		public HtmlLink() : base("link")
		{
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0007FF28 File Offset: 0x0007E128
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x0007DED8 File Offset: 0x0007C0D8
		[WebCategory("Action")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public virtual string Href
		{
			get
			{
				string text = base.Attributes["href"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["href"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0007FF50 File Offset: 0x0007E150
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Href))
			{
				base.Attributes["href"] = base.ResolveClientUrl(this.Href);
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0007FF82 File Offset: 0x0007E182
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write(" />");
		}
	}
}
