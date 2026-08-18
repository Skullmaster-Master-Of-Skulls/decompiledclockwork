using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E1 RID: 225
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.AttachedTemplatePopup", "HtmlEditor.Popups.AttachedTemplatePopup")]
	public class AttachedTemplatePopup : AttachedPopup
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00011421 File Offset: 0x0000F621
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00011429 File Offset: 0x0000F629
		[DefaultValue("ajax__htmleditor_attachedpopup_default")]
		[Category("Appearance")]
		public string ContainerCSSClass
		{
			get
			{
				return this._containerCSSClass;
			}
			set
			{
				this._containerCSSClass = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00011432 File Offset: 0x0000F632
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001143A File Offset: 0x0000F63A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[Browsable(false)]
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00011443 File Offset: 0x0000F643
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

		// Token: 0x06000676 RID: 1654 RVA: 0x00011460 File Offset: 0x0000F660
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.CssPath.Length == 0)
			{
				base.CssPath = base.ResolveClientUrl(ToolkitResourceManager.GetStyleHref("HtmlEditor.Popups.AttachedTemplatePopup", this));
			}
			if (this._contentTemplate != null)
			{
				Control control = new Control();
				this._contentTemplate.InstantiateIn(control);
				this.Content.Add(control);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000114C0 File Offset: 0x0000F6C0
		protected override void CreateChildControls()
		{
			this._contentDiv = new HtmlGenericControl("div");
			this._contentDiv.Style[HtmlTextWriterStyle.Display] = "none";
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", this.ContainerCSSClass);
			this._contentDiv.Controls.Add(htmlGenericControl);
			for (int i = 0; i < this.Content.Count; i++)
			{
				htmlGenericControl.Controls.Add(this.Content[i]);
			}
			this.Controls.Add(this._contentDiv);
			base.CreateChildControls();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001156A File Offset: 0x0000F76A
		protected override void OnPreRender(EventArgs e)
		{
			this._contentDiv.Attributes.Add("id", this._contentDiv.ClientID);
			base.OnPreRender(e);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00011593 File Offset: 0x0000F793
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddElementProperty("contentDiv", this._contentDiv.ClientID);
		}

		// Token: 0x040002EF RID: 751
		private ITemplate _contentTemplate;

		// Token: 0x040002F0 RID: 752
		private HtmlGenericControl _contentDiv;

		// Token: 0x040002F1 RID: 753
		private Collection<Control> _content;

		// Token: 0x040002F2 RID: 754
		private string _containerCSSClass = "ajax__htmleditor_attachedpopup_default";
	}
}
