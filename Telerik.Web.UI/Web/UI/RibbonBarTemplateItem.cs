using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000F20 RID: 3872
	[PersistChildren(true)]
	[ParseChildren(typeof(WebControl), ChildrenAsProperties = false, DefaultProperty = "Template")]
	public class RibbonBarTemplateItem : RibbonBarItem, IRibbonBarSizableItem, INamingContainer
	{
		// Token: 0x17002EC0 RID: 11968
		// (get) Token: 0x060093D1 RID: 37841 RVA: 0x00212C89 File Offset: 0x00210E89
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002EC1 RID: 11969
		// (get) Token: 0x060093D2 RID: 37842 RVA: 0x00212C8D File Offset: 0x00210E8D
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.TemplatePanel;
			}
		}

		// Token: 0x17002EC2 RID: 11970
		// (get) Token: 0x060093D3 RID: 37843 RVA: 0x00212C90 File Offset: 0x00210E90
		// (set) Token: 0x060093D4 RID: 37844 RVA: 0x00212C98 File Offset: 0x00210E98
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[TemplateContainer(typeof(RibbonBarTemplateItem))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ITemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
				this.ClearTemplate();
				this.ApplyTemplate();
			}
		}

		// Token: 0x060093D5 RID: 37845 RVA: 0x00212CAD File Offset: 0x00210EAD
		private void ClearTemplate()
		{
			this.Controls.Clear();
		}

		// Token: 0x060093D6 RID: 37846 RVA: 0x00212CBA File Offset: 0x00210EBA
		private void ApplyTemplate()
		{
			if (this._template != null)
			{
				this._template.InstantiateIn(this);
			}
		}

		// Token: 0x060093D7 RID: 37847 RVA: 0x00212CD0 File Offset: 0x00210ED0
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarTemplateLiteRenderer(this);
			}
			return new RibbonBarTemplateClassicRenderer(this);
		}

		// Token: 0x060093D8 RID: 37848 RVA: 0x00212CED File Offset: 0x00210EED
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x17002EC3 RID: 11971
		// (get) Token: 0x060093D9 RID: 37849 RVA: 0x00212CFB File Offset: 0x00210EFB
		// (set) Token: 0x060093DA RID: 37850 RVA: 0x00212D1C File Offset: 0x00210F1C
		public RibbonBarItemSize Size
		{
			get
			{
				return (RibbonBarItemSize)(this.ViewState["Size"] ?? RibbonBarItemSize.Small);
			}
			set
			{
				this.ViewState["Size"] = value;
			}
		}

		// Token: 0x04002A62 RID: 10850
		private ITemplate _template;
	}
}
