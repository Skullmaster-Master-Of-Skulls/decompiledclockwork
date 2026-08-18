using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000358 RID: 856
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlMeta : HtmlControl
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x0007FFA2 File Offset: 0x0007E1A2
		public HtmlMeta() : base("meta")
		{
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0007FFB0 File Offset: 0x0007E1B0
		// (set) Token: 0x0600275D RID: 10077 RVA: 0x0007FFD8 File Offset: 0x0007E1D8
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0007FFF0 File Offset: 0x0007E1F0
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x00080018 File Offset: 0x0007E218
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x00080030 File Offset: 0x0007E230
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x0007E2A4 File Offset: 0x0007C4A4
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x00080058 File Offset: 0x0007E258
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x00080080 File Offset: 0x0007E280
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x06002764 RID: 10084 RVA: 0x00080098 File Offset: 0x0007E298
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
