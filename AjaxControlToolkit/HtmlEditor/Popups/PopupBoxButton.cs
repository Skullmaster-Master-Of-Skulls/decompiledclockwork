using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E7 RID: 231
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.PopupBoxButton", "HtmlEditor.Popups.PopupBoxButton")]
	internal class PopupBoxButton : PopupCommonButton
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00012744 File Offset: 0x00010944
		public PopupBoxButton() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "ajax__htmleditor_popup_boxbutton";
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00012759 File Offset: 0x00010959
		public PopupBoxButton(HtmlTextWriterTag tag) : base(tag)
		{
			this.CssClass = "ajax__htmleditor_popup_boxbutton";
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001276D File Offset: 0x0001096D
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00012775 File Offset: 0x00010975
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate ContentTemplate
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

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001277E File Offset: 0x0001097E
		protected Collection<Control> Content
		{
			get
			{
				if (this._content == null)
				{
					this._content = new Collection<Control>();
				}
				return this._content;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001279C File Offset: 0x0001099C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this._contentTemplate != null)
			{
				Control control = new Control();
				this._contentTemplate.InstantiateIn(control);
				this.Content.Add(control);
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000127D8 File Offset: 0x000109D8
		protected override void CreateChildControls()
		{
			for (int i = 0; i < this.Content.Count; i++)
			{
				this.Controls.Add(this.Content[i]);
			}
			base.CreateChildControls();
		}

		// Token: 0x040002FB RID: 763
		private ITemplate _contentTemplate;

		// Token: 0x040002FC RID: 764
		private Collection<Control> _content;
	}
}
