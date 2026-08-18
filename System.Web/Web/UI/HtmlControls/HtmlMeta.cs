using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004A7 RID: 1191
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlMeta : HtmlControl
	{
		// Token: 0x06003798 RID: 14232 RVA: 0x000EE3D2 File Offset: 0x000ED3D2
		public HtmlMeta() : base("meta")
		{
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000EE3E0 File Offset: 0x000ED3E0
		// (set) Token: 0x0600379A RID: 14234 RVA: 0x000EE408 File Offset: 0x000ED408
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string Content
		{
			get
			{
				string text = base.Attributes["content"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["content"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000EE420 File Offset: 0x000ED420
		// (set) Token: 0x0600379C RID: 14236 RVA: 0x000EE448 File Offset: 0x000ED448
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string HttpEquiv
		{
			get
			{
				string text = base.Attributes["http-equiv"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["http-equiv"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000EE460 File Offset: 0x000ED460
		// (set) Token: 0x0600379E RID: 14238 RVA: 0x000EE488 File Offset: 0x000ED488
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				string text = base.Attributes["name"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["name"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000EE4A0 File Offset: 0x000ED4A0
		// (set) Token: 0x060037A0 RID: 14240 RVA: 0x000EE4C8 File Offset: 0x000ED4C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string Scheme
		{
			get
			{
				string text = base.Attributes["scheme"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["scheme"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000EE4E0 File Offset: 0x000ED4E0
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.EnableLegacyRendering)
			{
				base.Render(writer);
				return;
			}
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write(" />");
		}
	}
}
