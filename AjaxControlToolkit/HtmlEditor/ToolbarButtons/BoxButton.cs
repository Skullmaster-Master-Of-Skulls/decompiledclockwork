using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F8 RID: 248
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.BoxButton", "HtmlEditor.ToolbarButtons.BoxButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class BoxButton : CommonButton
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00013504 File Offset: 0x00011704
		protected BoxButton() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001350E File Offset: 0x0001170E
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00013516 File Offset: 0x00011716
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[Browsable(false)]
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

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001351F File Offset: 0x0001171F
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

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001353C File Offset: 0x0001173C
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

		// Token: 0x060006F3 RID: 1779 RVA: 0x00013578 File Offset: 0x00011778
		protected override void CreateChildControls()
		{
			for (int i = 0; i < this.Content.Count; i++)
			{
				this.Controls.Add(this.Content[i]);
			}
			base.CreateChildControls();
		}

		// Token: 0x04000308 RID: 776
		private ITemplate _contentTemplate;

		// Token: 0x04000309 RID: 777
		private Collection<Control> _content;
	}
}
