using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200000D RID: 13
	[ToolboxBitmap(typeof(Accessor), "Accordion.bmp")]
	[ToolboxData("<{0}:AccordionPane runat=\"server\"></{0}:AccordionPane>")]
	public class AccordionPane : WebControl
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000039BC File Offset: 0x00001BBC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AccordionContentPanel HeaderContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._header;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000039CA File Offset: 0x00001BCA
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000039DD File Offset: 0x00001BDD
		[Browsable(true)]
		[Category("Appearance")]
		[Description("CSS class for Accordion Pane Header")]
		public string HeaderCssClass
		{
			get
			{
				this.EnsureChildControls();
				return this._header.CssClass;
			}
			set
			{
				this.EnsureChildControls();
				this._header.CssClass = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000039F1 File Offset: 0x00001BF1
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000039F9 File Offset: 0x00001BF9
		[Browsable(false)]
		[Description("Accordion Pane Header")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateInstance(TemplateInstance.Single)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(AccordionContentPanel))]
		public virtual ITemplate Header
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003A02 File Offset: 0x00001C02
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AccordionContentPanel ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._content;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003A10 File Offset: 0x00001C10
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003A23 File Offset: 0x00001C23
		[Description("CSS class for Accordion Pane Content")]
		[Browsable(true)]
		[Category("Appearance")]
		public string ContentCssClass
		{
			get
			{
				this.EnsureChildControls();
				return this._content.CssClass;
			}
			set
			{
				this.EnsureChildControls();
				this._content.CssClass = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003A37 File Offset: 0x00001C37
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003A3F File Offset: 0x00001C3F
		[TemplateInstance(TemplateInstance.Single)]
		[TemplateContainer(typeof(AccordionContentPanel))]
		[Description("Accordion Pane Content")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate Content
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003A48 File Offset: 0x00001C48
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A58 File Offset: 0x00001C58
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this._header = new AccordionContentPanel(null, -1, AccordionItemType.Header);
			this.Controls.Add(this._header);
			this._content = new AccordionContentPanel(null, -1, AccordionItemType.Content);
			this.Controls.Add(this._content);
			this._content.Collapsed = true;
			if (this._headerTemplate != null)
			{
				this._headerTemplate.InstantiateIn(this._header);
			}
			if (this._contentTemplate != null)
			{
				this._contentTemplate.InstantiateIn(this._content);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003AEC File Offset: 0x00001CEC
		public override Control FindControl(string id)
		{
			this.EnsureChildControls();
			Control result;
			if ((result = base.FindControl(id)) == null)
			{
				result = (this._header.FindControl(id) ?? this._content.FindControl(id));
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B1B File Offset: 0x00001D1B
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B1D File Offset: 0x00001D1D
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x0400002B RID: 43
		private AccordionContentPanel _header;

		// Token: 0x0400002C RID: 44
		private ITemplate _headerTemplate;

		// Token: 0x0400002D RID: 45
		private AccordionContentPanel _content;

		// Token: 0x0400002E RID: 46
		private ITemplate _contentTemplate;
	}
}
