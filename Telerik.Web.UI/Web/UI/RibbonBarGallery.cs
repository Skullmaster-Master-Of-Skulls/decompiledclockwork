using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007D5 RID: 2005
	[ParseChildren(typeof(RibbonBarGalleryCategory), ChildrenAsProperties = true, DefaultProperty = "Categories")]
	[XmlRoot("Gallery")]
	public class RibbonBarGallery : RibbonBarItem, IXmlSerializable
	{
		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000DBF59 File Offset: 0x000DA159
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000DBF5D File Offset: 0x000DA15D
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.Gallery;
			}
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x000DBF61 File Offset: 0x000DA161
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarGalleryLiteRenderer(this);
			}
			return new RibbonBarGalleryClassicRenderer(this);
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x000DBF7E File Offset: 0x000DA17E
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x000DBF8C File Offset: 0x000DA18C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.Renderer.RenderContents(writer);
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x000DBF9A File Offset: 0x000DA19A
		// (set) Token: 0x060045FD RID: 17917 RVA: 0x000DBFBA File Offset: 0x000DA1BA
		[DefaultValue("")]
		public string CommandName
		{
			get
			{
				return (string)(this.ViewState["CommandName"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x000DBFCD File Offset: 0x000DA1CD
		// (set) Token: 0x060045FF RID: 17919 RVA: 0x000DBFEE File Offset: 0x000DA1EE
		[DefaultValue(5)]
		public int Columns
		{
			get
			{
				return (int)(this.ViewState["Columns"] ?? 5);
			}
			set
			{
				this.ViewState["Columns"] = value;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x000DC006 File Offset: 0x000DA206
		// (set) Token: 0x06004601 RID: 17921 RVA: 0x000DC027 File Offset: 0x000DA227
		[DefaultValue(5)]
		public int ExpandedColumns
		{
			get
			{
				return (int)(this.ViewState["ExpandedColumns"] ?? 5);
			}
			set
			{
				this.ViewState["ExpandedColumns"] = value;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x000DC03F File Offset: 0x000DA23F
		// (set) Token: 0x06004603 RID: 17923 RVA: 0x000DC064 File Offset: 0x000DA264
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit ExpandedHeight
		{
			get
			{
				return (Unit)(this.ViewState["ExpandedHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ExpandedHeight"] = value;
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x000DC099 File Offset: 0x000DA299
		// (set) Token: 0x06004605 RID: 17925 RVA: 0x000DC0BE File Offset: 0x000DA2BE
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit ItemHeight
		{
			get
			{
				return (Unit)(this.ViewState["ItemHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ItemHeight"] = value;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x000DC0F3 File Offset: 0x000DA2F3
		// (set) Token: 0x06004607 RID: 17927 RVA: 0x000DC118 File Offset: 0x000DA318
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public Unit ItemWidth
		{
			get
			{
				return (Unit)(this.ViewState["ItemWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ItemWidth"] = value;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000DC150 File Offset: 0x000DA350
		[Description("SelectedItem")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Category("Behavior")]
		[DefaultValue(-1)]
		public RibbonBarGalleryItem SelectedItem
		{
			get
			{
				for (int i = 0; i < this.Categories.Count; i++)
				{
					RibbonBarGalleryCategory ribbonBarGalleryCategory = this.Categories[i];
					for (int j = 0; j < ribbonBarGalleryCategory.Items.Count; j++)
					{
						RibbonBarGalleryItem ribbonBarGalleryItem = ribbonBarGalleryCategory.Items[j];
						if (ribbonBarGalleryItem.Selected)
						{
							return ribbonBarGalleryItem;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000DC1AE File Offset: 0x000DA3AE
		// (set) Token: 0x0600460A RID: 17930 RVA: 0x000DC1CF File Offset: 0x000DA3CF
		[DefaultValue(RibbonBarGalleryItemTextPosition.Bottom)]
		[Category("Appearance")]
		[Description("The position of the item text relative to its image.")]
		[ClientPersistedProperty]
		public RibbonBarGalleryItemTextPosition ItemTextPosition
		{
			get
			{
				return (RibbonBarGalleryItemTextPosition)(this.ViewState["ItemTextPosition"] ?? RibbonBarGalleryItemTextPosition.Bottom);
			}
			set
			{
				this.ViewState["ItemTextPosition"] = value;
			}
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x000DC1E7 File Offset: 0x000DA3E7
		public void ClearSelection()
		{
			if (this.SelectedItem != null)
			{
				this.SelectedItem.Selected = false;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x000DC1FD File Offset: 0x000DA3FD
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public RibbonBarGalleryCategoryCollection Categories
		{
			get
			{
				if (this._categories == null)
				{
					this._categories = new RibbonBarGalleryCategoryCollection();
					this._categories.Container = this;
					this._categories.ParentWebControl = this;
				}
				return this._categories;
			}
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x000DC230 File Offset: 0x000DA430
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x000DC23C File Offset: 0x000DA43C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x000DC245 File Offset: 0x000DA445
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x000DC24E File Offset: 0x000DA44E
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForCategories(reader);
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x000DC268 File Offset: 0x000DA468
		protected void ReadXmlForCategories(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Gallery")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "Gallery" && reader.Name != "GalleryCategory")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarGalleryCategory));
					RibbonBarGalleryCategory item = (RibbonBarGalleryCategory)xmlSerializer.Deserialize(reader);
					this.Categories.Add(item);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x000DC313 File Offset: 0x000DA513
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForListItems(writer);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x000DC330 File Offset: 0x000DA530
		protected virtual void WriteXmlForListItems(XmlWriter writer)
		{
			foreach (RibbonBarGalleryCategory ribbonBarGalleryCategory in this.Categories)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarGalleryCategory.GetType());
				xmlSerializer.Serialize(writer, ribbonBarGalleryCategory);
			}
		}

		// Token: 0x04001216 RID: 4630
		private RibbonBarGalleryCategoryCollection _categories;
	}
}
