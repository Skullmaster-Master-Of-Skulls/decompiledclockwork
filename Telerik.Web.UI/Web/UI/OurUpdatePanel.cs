using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FD5 RID: 4053
	internal class OurUpdatePanel : UpdatePanel
	{
		// Token: 0x06009D8A RID: 40330 RVA: 0x00232630 File Offset: 0x00230830
		public OurUpdatePanel(Control controlToAjaxify, RadAjaxControl ajaxControl)
		{
			this.controlToAjaxify = controlToAjaxify;
			this.AjaxControl = ajaxControl;
			this.ShouldUpdate = true;
		}

		// Token: 0x170031CA RID: 12746
		// (get) Token: 0x06009D8B RID: 40331 RVA: 0x00232654 File Offset: 0x00230854
		// (set) Token: 0x06009D8C RID: 40332 RVA: 0x0023265C File Offset: 0x0023085C
		public string CssClass { get; set; }

		// Token: 0x170031CB RID: 12747
		// (get) Token: 0x06009D8D RID: 40333 RVA: 0x00232665 File Offset: 0x00230865
		// (set) Token: 0x06009D8E RID: 40334 RVA: 0x0023266D File Offset: 0x0023086D
		public Unit Height { get; set; }

		// Token: 0x170031CC RID: 12748
		// (get) Token: 0x06009D8F RID: 40335 RVA: 0x00232676 File Offset: 0x00230876
		// (set) Token: 0x06009D90 RID: 40336 RVA: 0x0023267E File Offset: 0x0023087E
		public bool ShouldUpdate { get; set; }

		// Token: 0x06009D91 RID: 40337 RVA: 0x00232687 File Offset: 0x00230887
		public new void Update()
		{
			this.ShouldUpdate = true;
			base.Update();
		}

		// Token: 0x06009D92 RID: 40338 RVA: 0x00232698 File Offset: 0x00230898
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			ProxyScriptControl child = new ProxyScriptControl(this.controlToAjaxify);
			base.ContentTemplateContainer.Controls.Add(child);
			ControlRenderer child2 = new ControlRenderer(this.controlToAjaxify);
			base.ContentTemplateContainer.Controls.Add(child2);
		}

		// Token: 0x06009D93 RID: 40339 RVA: 0x002326E8 File Offset: 0x002308E8
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ShouldUpdate)
			{
				if (!this.Height.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "RadAjaxPanel" + (string.IsNullOrEmpty(this.CssClass) ? "" : (" " + this.CssClass)));
				base.Render(writer);
			}
		}

		// Token: 0x170031CD RID: 12749
		// (get) Token: 0x06009D94 RID: 40340 RVA: 0x00232766 File Offset: 0x00230966
		// (set) Token: 0x06009D95 RID: 40341 RVA: 0x0023276E File Offset: 0x0023096E
		[DefaultValue(ClientIDMode.AutoID)]
		[Description("This property is overridden in order to support controls which implement INamingContainer")]
		[NotifyParentProperty(true)]
		public override ClientIDMode ClientIDMode
		{
			get
			{
				return this.ClientIDModeValue;
			}
			set
			{
				if (this.ClientIDModeValue != value)
				{
					base.ClearEffectiveClientIDMode();
					base.ClearCachedClientID();
				}
				this.ClientIDModeValue = value;
			}
		}

		// Token: 0x04002C55 RID: 11349
		internal PreControlToAjaxify PreControlToAjaxify;

		// Token: 0x04002C56 RID: 11350
		internal RadAjaxControl AjaxControl;

		// Token: 0x04002C57 RID: 11351
		internal Control controlToAjaxify;

		// Token: 0x04002C58 RID: 11352
		private ClientIDMode ClientIDModeValue = ClientIDMode.AutoID;
	}
}
