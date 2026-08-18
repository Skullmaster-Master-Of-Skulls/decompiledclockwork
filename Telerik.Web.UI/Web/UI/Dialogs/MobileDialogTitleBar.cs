using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x02000271 RID: 625
	[RequiredScript(typeof(jQueryPlugins))]
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.MobileDialogTitleBar", "Telerik.Web.UI.Dialogs.MobileDialogTitleBar.js")]
	public class MobileDialogTitleBar : RadWebControl, IScriptControl
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0004CD24 File Offset: 0x0004AF24
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0004CD3B File Offset: 0x0004AF3B
		[ClientPropertyName("title")]
		[ClientControlProperty]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return (string)this.ViewState["Title"];
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0004CD4E File Offset: 0x0004AF4E
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0004CD65 File Offset: 0x0004AF65
		[ClientControlEvent]
		[ClientPropertyName("cancel")]
		public string OnClientCancel
		{
			get
			{
				return (string)this.ViewState["OnClientCancel"];
			}
			set
			{
				this.ViewState["OnClientCancel"] = value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0004CD78 File Offset: 0x0004AF78
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x0004CD8F File Offset: 0x0004AF8F
		[ClientControlEvent]
		[ClientPropertyName("ok")]
		public string OnClientOk
		{
			get
			{
				return (string)this.ViewState["OnClientOk"];
			}
			set
			{
				this.ViewState["OnClientOk"] = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0004CDA2 File Offset: 0x0004AFA2
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0004CDC2 File Offset: 0x0004AFC2
		[DefaultValue("re")]
		public string CssClassPrefix
		{
			get
			{
				return ((string)this.ViewState["CssClassPrefix"]) ?? "re";
			}
			set
			{
				this.ViewState["CssClassPrefix"] = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0004CDD5 File Offset: 0x0004AFD5
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x0004CDF5 File Offset: 0x0004AFF5
		[DefaultValue("MobileDialogTitleBar")]
		public override string CssClass
		{
			get
			{
				return ((string)this.ViewState["CssClass"]) ?? "MobileDialogTitleBar";
			}
			set
			{
				this.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0004CE08 File Offset: 0x0004B008
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x0004CE28 File Offset: 0x0004B028
		[DefaultValue("Button")]
		public string ButtonCssClass
		{
			get
			{
				return ((string)this.ViewState["ButtonCssClass"]) ?? "Button";
			}
			set
			{
				this.ViewState["ButtonCssClass"] = value;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0004CE3B File Offset: 0x0004B03B
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x0004CE5B File Offset: 0x0004B05B
		[DefaultValue("Icon")]
		public string IconCssClass
		{
			get
			{
				return ((string)this.ViewState["IconCssClass"]) ?? "Icon";
			}
			set
			{
				this.ViewState["IconCssClass"] = value;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0004CE6E File Offset: 0x0004B06E
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0004CE8E File Offset: 0x0004B08E
		[DefaultValue("OK")]
		public string OkButtonText
		{
			get
			{
				return ((string)this.ViewState["OkButtonText"]) ?? "OK";
			}
			set
			{
				this.ViewState["OkButtonText"] = value;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0004CEA1 File Offset: 0x0004B0A1
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x0004CEC1 File Offset: 0x0004B0C1
		[DefaultValue("Cancel")]
		public string CancelButtonText
		{
			get
			{
				return ((string)this.ViewState["CancelButtonText"]) ?? "Cancel";
			}
			set
			{
				this.ViewState["CancelButtonText"] = value;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0004CED4 File Offset: 0x0004B0D4
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "Cancel", this.CancelButtonText);
			this.RenderTitle(writer);
			this.RenderButton(writer, "OK", this.OkButtonText);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0004CF01 File Offset: 0x0004B101
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute("class", string.Format("{0} t-hbox", this.PrefixCss(this.CssClass)));
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0004CF2C File Offset: 0x0004B12C
		public virtual void RenderButton(HtmlTextWriter writer, string buttonName, string title)
		{
			writer.AddAttribute("role", "button");
			writer.AddAttribute("title", title);
			writer.AddAttribute("class", string.Format("{0} {0}{1}", this.PrefixCss(this.ButtonCssClass), buttonName));
			writer.AddAttribute("unselectable", "on");
			writer.RenderBeginTag("span");
			string arg = this.PrefixCss(this.IconCssClass);
			writer.AddAttribute("class", string.Format("{0} {0}{1}", arg, buttonName));
			writer.AddAttribute("unselectable", "on");
			writer.RenderBeginTag("span");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0004CFDD File Offset: 0x0004B1DD
		public virtual void RenderTitle(HtmlTextWriter writer)
		{
			writer.AddAttribute("class", "t-flex");
			writer.RenderBeginTag("h6");
			writer.Write(this.Title);
			writer.RenderEndTag();
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0004D00C File Offset: 0x0004B20C
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0004D00F File Offset: 0x0004B20F
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0004D012 File Offset: 0x0004B212
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0004D018 File Offset: 0x0004B218
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_cssClass", this.PrefixCss(this.CssClass));
			descriptor.AddProperty("_buttonCssClass", this.PrefixCss(this.ButtonCssClass));
			descriptor.AddProperty("_iconCssClass", this.PrefixCss(this.IconCssClass));
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0004D071 File Offset: 0x0004B271
		public override void RenderClientStateField(HtmlTextWriter writer)
		{
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004D073 File Offset: 0x0004B273
		private string PrefixCss(string cssClass)
		{
			return this.CssClassPrefix + cssClass;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0004D081 File Offset: 0x0004B281
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "title", this.Title, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0004D0A1 File Offset: 0x0004B2A1
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "cancel", this.OnClientCancel);
			RadWebControl.DescribeEvent(descriptor, "ok", this.OnClientOk);
			base.DescribeClientEvents(descriptor);
		}
	}
}
