using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000904 RID: 2308
	[ToolboxBitmap(typeof(RadContentTemplateTile), "Telerik.Web.UI.TileList.png")]
	[Designer("Telerik.Web.Design.RadContentTemplateTileDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadContentTemplateTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	[TelerikToolboxCategory("Navigation")]
	public class RadContentTemplateTile : RadBaseTile, INamingContainer
	{
		// Token: 0x06005755 RID: 22357 RVA: 0x0010B1EB File Offset: 0x001093EB
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "tileType", this.TileType, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x0010B207 File Offset: 0x00109407
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x06005758 RID: 22360 RVA: 0x0010B218 File Offset: 0x00109418
		[Browsable(false)]
		public Panel ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._contentContainer;
			}
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x06005759 RID: 22361 RVA: 0x0010B226 File Offset: 0x00109426
		// (set) Token: 0x0600575A RID: 22362 RVA: 0x0010B22E File Offset: 0x0010942E
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[TemplateContainer(typeof(RadContentTemplateTile))]
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
				this.InstantiateContentTemplate();
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x0010B23D File Offset: 0x0010943D
		[ClientControlProperty]
		internal override string TileType
		{
			get
			{
				return "RadContentTemplateTile";
			}
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x0010B244 File Offset: 0x00109444
		protected override void CreateChildControls()
		{
			this.CreateContentContainer();
			base.CreateChildControls();
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x0010B254 File Offset: 0x00109454
		private void CreateContentContainer()
		{
			this._contentContainer = new Panel();
			this._contentContainer.Attributes["class"] = "rtileContent";
			this._contentContainer.ID = "C";
			this.Controls.Add(this._contentContainer);
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x0010B2A7 File Offset: 0x001094A7
		protected virtual void InstantiateContentTemplate()
		{
			if (!this._contentTemplateInstantiating && !this._contentTemplateInstantiated)
			{
				this._contentTemplateInstantiating = true;
				if (this._contentTemplate != null)
				{
					this._contentTemplate.InstantiateIn(this.ContentContainer);
					this._contentTemplateInstantiated = true;
				}
				this._contentTemplateInstantiating = false;
			}
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x0010B2E7 File Offset: 0x001094E7
		protected override void RenderTileBody(HtmlTextWriter writer)
		{
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x0010B2E9 File Offset: 0x001094E9
		protected override void RenderTileContent(HtmlTextWriter writer)
		{
		}

		// Token: 0x0400154D RID: 5453
		private Panel _contentContainer;

		// Token: 0x0400154E RID: 5454
		private bool _contentTemplateInstantiated;

		// Token: 0x0400154F RID: 5455
		private bool _contentTemplateInstantiating;

		// Token: 0x04001550 RID: 5456
		private ITemplate _contentTemplate;
	}
}
