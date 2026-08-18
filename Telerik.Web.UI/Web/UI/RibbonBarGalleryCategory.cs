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
	// Token: 0x020007D3 RID: 2003
	[XmlRoot("GalleryCategory")]
	[ParseChildren(typeof(RibbonBarGalleryItem), ChildrenAsProperties = true, DefaultProperty = "Items")]
	[ToolboxItem(false)]
	public class RibbonBarGalleryCategory : RibbonBarCollectionItemBase, IXmlSerializable
	{
		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x000DBBFC File Offset: 0x000D9DFC
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x000DBC04 File Offset: 0x000D9E04
		public override WebControl ParentWebControl
		{
			get
			{
				return this.parentWebControl;
			}
			internal set
			{
				this.parentWebControl = value;
				if (!this.ParentWebControl.Controls.Contains(this))
				{
					this.ParentWebControl.Controls.Add(this);
				}
				this.Items.ParentWebControl = value;
			}
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000DBC3D File Offset: 0x000D9E3D
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarGalleryCategoryLiteRenderer(this);
			}
			return new RibbonBarGalleryCategoryClassicRenderer(this);
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x060045DD RID: 17885 RVA: 0x000DBC5A File Offset: 0x000D9E5A
		// (set) Token: 0x060045DE RID: 17886 RVA: 0x000DBC7A File Offset: 0x000D9E7A
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return (string)(this.ViewState["Title"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x060045DF RID: 17887 RVA: 0x000DBC8D File Offset: 0x000D9E8D
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public RibbonBarGalleryItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarGalleryItemCollection();
					this._items.Container = this;
				}
				return this._items;
			}
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000DBCB4 File Offset: 0x000D9EB4
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000DBCC0 File Offset: 0x000D9EC0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x000DBCC9 File Offset: 0x000D9EC9
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x000DBCD2 File Offset: 0x000D9ED2
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForCategories(reader);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000DBCEC File Offset: 0x000D9EEC
		protected void ReadXmlForCategories(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "GalleryCategory")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "GalleryCategory" && reader.Name != "GalleryItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarGalleryItem));
					RibbonBarGalleryItem item = (RibbonBarGalleryItem)xmlSerializer.Deserialize(reader);
					this.Items.Add(item);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000DBD97 File Offset: 0x000D9F97
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForListItems(writer);
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000DBDB4 File Offset: 0x000D9FB4
		protected virtual void WriteXmlForListItems(XmlWriter writer)
		{
			foreach (RibbonBarGalleryItem ribbonBarGalleryItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarGalleryItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarGalleryItem);
			}
		}

		// Token: 0x04001214 RID: 4628
		private RibbonBarGalleryItemCollection _items;
	}
}
