using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004A6 RID: 1190
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlLink : HtmlControl
	{
		// Token: 0x06003793 RID: 14227 RVA: 0x000EE332 File Offset: 0x000ED332
		public HtmlLink() : base("link")
		{
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000EE340 File Offset: 0x000ED340
		// (set) Token: 0x06003795 RID: 14229 RVA: 0x000EE368 File Offset: 0x000ED368
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Action")]
		[DefaultValue("")]
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

		// Token: 0x06003796 RID: 14230 RVA: 0x000EE380 File Offset: 0x000ED380
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Href))
			{
				base.Attributes["href"] = base.ResolveClientUrl(this.Href);
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000EE3B2 File Offset: 0x000ED3B2
		protected internal override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write(" />");
		}
	}
}
