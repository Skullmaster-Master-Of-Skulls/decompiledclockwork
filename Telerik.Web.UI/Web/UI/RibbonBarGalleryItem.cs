using System;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007D4 RID: 2004
	[ToolboxItem(false)]
	[XmlRoot("GalleryItem")]
	public class RibbonBarGalleryItem : RibbonBarCollectionItemBase, IXmlSerializable
	{
		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x060045E8 RID: 17896 RVA: 0x000DBE1C File Offset: 0x000DA01C
		// (set) Token: 0x060045E9 RID: 17897 RVA: 0x000DBE3C File Offset: 0x000DA03C
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x000DBE4F File Offset: 0x000DA04F
		// (set) Token: 0x060045EB RID: 17899 RVA: 0x000DBE6F File Offset: 0x000DA06F
		[DefaultValue("")]
		public string CommandArgument
		{
			get
			{
				return (string)(this.ViewState["CommandArgument"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x000DBE82 File Offset: 0x000DA082
		// (set) Token: 0x060045ED RID: 17901 RVA: 0x000DBEA2 File Offset: 0x000DA0A2
		[Category("Appearance")]
		[Description("The URL of the image displayed for the item.")]
		[UrlProperty]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x000DBEB5 File Offset: 0x000DA0B5
		// (set) Token: 0x060045EF RID: 17903 RVA: 0x000DBEC0 File Offset: 0x000DA0C0
		[Description("Whether the item is selected or not.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				if (value && this.ParentWebControl != null)
				{
					RibbonBarGallery ribbonBarGallery = this.ParentWebControl as RibbonBarGallery;
					ribbonBarGallery.ClearSelection();
				}
				this._selected = value;
			}
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x000DBEF1 File Offset: 0x000DA0F1
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarGalleryItemLiteRenderer(this);
			}
			return new RibbonBarGalleryItemClassicRenderer(this);
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x000DBF0E File Offset: 0x000DA10E
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x000DBF1A File Offset: 0x000DA11A
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x000DBF23 File Offset: 0x000DA123
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x000DBF2C File Offset: 0x000DA12C
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x000DBF3C File Offset: 0x000DA13C
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}

		// Token: 0x04001215 RID: 4629
		private bool _selected;
	}
}
