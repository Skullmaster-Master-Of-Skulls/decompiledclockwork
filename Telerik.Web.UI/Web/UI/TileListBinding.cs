using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020008F9 RID: 2297
	public class TileListBinding : StateManager
	{
		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x00109865 File Offset: 0x00107A65
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CommonTileBinding CommonTileBinding
		{
			get
			{
				if (this._commonTileBinding == null)
				{
					this._commonTileBinding = new CommonTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._commonTileBinding).TrackViewState();
					}
				}
				return this._commonTileBinding;
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x060056B9 RID: 22201 RVA: 0x00109893 File Offset: 0x00107A93
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TextTileBinding TextTileBinding
		{
			get
			{
				if (this._textTileBinding == null)
				{
					this._textTileBinding = new TextTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._textTileBinding).TrackViewState();
					}
				}
				return this._textTileBinding;
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x001098C1 File Offset: 0x00107AC1
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageTileBinding ImageTileBinding
		{
			get
			{
				if (this._imageTileBinding == null)
				{
					this._imageTileBinding = new ImageTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._imageTileBinding).TrackViewState();
					}
				}
				return this._imageTileBinding;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x060056BB RID: 22203 RVA: 0x001098EF File Offset: 0x00107AEF
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageAndTextTileBinding ImageAndTextTileBinding
		{
			get
			{
				if (this._imageAndTextTileBinding == null)
				{
					this._imageAndTextTileBinding = new ImageAndTextTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._imageAndTextTileBinding).TrackViewState();
					}
				}
				return this._imageAndTextTileBinding;
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x060056BC RID: 22204 RVA: 0x0010991D File Offset: 0x00107B1D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public IconTileBinding IconTileBinding
		{
			get
			{
				if (this._iconTileBinding == null)
				{
					this._iconTileBinding = new IconTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._iconTileBinding).TrackViewState();
					}
				}
				return this._iconTileBinding;
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x060056BD RID: 22205 RVA: 0x0010994B File Offset: 0x00107B4B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ContentTemplateTileBinding ContentTemplateTileBinding
		{
			get
			{
				if (this._contentTemplateTileBinding == null)
				{
					this._contentTemplateTileBinding = new ContentTemplateTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._contentTemplateTileBinding).TrackViewState();
					}
				}
				return this._contentTemplateTileBinding;
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x060056BE RID: 22206 RVA: 0x00109979 File Offset: 0x00107B79
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LiveTileBinding LiveTileBinding
		{
			get
			{
				if (this._liveTileBinding == null)
				{
					this._liveTileBinding = new LiveTileBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._liveTileBinding).TrackViewState();
					}
				}
				return this._liveTileBinding;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x060056BF RID: 22207 RVA: 0x001099A7 File Offset: 0x00107BA7
		// (set) Token: 0x060056C0 RID: 22208 RVA: 0x001099AF File Offset: 0x00107BAF
		[TemplateContainer(typeof(RadBaseTile))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate TilePeekTemplate
		{
			get
			{
				return this._tilePeekTemplate;
			}
			set
			{
				this._tilePeekTemplate = value;
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x060056C1 RID: 22209 RVA: 0x001099B8 File Offset: 0x00107BB8
		// (set) Token: 0x060056C2 RID: 22210 RVA: 0x001099D8 File Offset: 0x00107BD8
		[Description("Gets or sets the HTML template, which will be used as TilePeekTemplate property value of the tile after it is bound to client datasource item.")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual string ClientTilePeekTemplate
		{
			get
			{
				return (base.ViewState["ClientTilePeekTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientTilePeekTemplate"] = value;
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x001099EB File Offset: 0x00107BEB
		// (set) Token: 0x060056C4 RID: 22212 RVA: 0x00109A0B File Offset: 0x00107C0B
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataClientTilePeekTemplateField
		{
			get
			{
				return (string)(base.ViewState["DataClientTilePeekTemplateField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataClientTilePeekTemplateField"] = value;
			}
		}

		// Token: 0x04001529 RID: 5417
		private CommonTileBinding _commonTileBinding;

		// Token: 0x0400152A RID: 5418
		private TextTileBinding _textTileBinding;

		// Token: 0x0400152B RID: 5419
		private ImageTileBinding _imageTileBinding;

		// Token: 0x0400152C RID: 5420
		private ImageAndTextTileBinding _imageAndTextTileBinding;

		// Token: 0x0400152D RID: 5421
		private IconTileBinding _iconTileBinding;

		// Token: 0x0400152E RID: 5422
		private ContentTemplateTileBinding _contentTemplateTileBinding;

		// Token: 0x0400152F RID: 5423
		private LiveTileBinding _liveTileBinding;

		// Token: 0x04001530 RID: 5424
		private ITemplate _tilePeekTemplate;
	}
}
