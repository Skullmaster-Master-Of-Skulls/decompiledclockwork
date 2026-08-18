using System;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007C2 RID: 1986
	[XmlRoot("ApplicationSplitMenuItem")]
	public class RibbonBarApplicationSplitMenuItem : RibbonBarApplicationMenuItemBase
	{
		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x000DAFA5 File Offset: 0x000D91A5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarApplicationMenuItemBaseCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarApplicationMenuItemBaseCollection(base.RibbonBar);
				}
				return this._items;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x000DAFC6 File Offset: 0x000D91C6
		// (set) Token: 0x0600454A RID: 17738 RVA: 0x000DAFE6 File Offset: 0x000D91E6
		[DefaultValue("")]
		public string Header
		{
			get
			{
				return (string)(this.ViewState["Header"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Header"] = value;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x000DAFF9 File Offset: 0x000D91F9
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x000DB019 File Offset: 0x000D9219
		public string ExpandAccessKey
		{
			get
			{
				return (string)(this.ViewState["ExpandAccessKey"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ExpandAccessKey"] = value;
			}
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x000DB02C File Offset: 0x000D922C
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarApplicationSplitMenuItemLiteRenderer(this);
			}
			return new RibbonBarApplicationSplitMenuItemClassicRenderer(this);
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x000DB04C File Offset: 0x000D924C
		public override void DataBind()
		{
			base.DataBind();
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Items[i].RibbonBar = base.RibbonBar;
			}
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000DB08C File Offset: 0x000D928C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.DataBind();
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000DB09B File Offset: 0x000D929B
		public override void ReadXml(XmlReader reader)
		{
			base.ReadXml(reader);
			this.ReadXmlForMenuItems(reader);
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x000DB0AC File Offset: 0x000D92AC
		protected void ReadXmlForMenuItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ApplicationSplitMenuItem")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "ApplicationSplitMenuItem" && reader.Name != "ApplicationMenuItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarApplicationMenuItem));
					RibbonBarApplicationMenuItem item = (RibbonBarApplicationMenuItem)xmlSerializer.Deserialize(reader);
					this.Items.Add(item);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000DB157 File Offset: 0x000D9357
		protected override void WriteXml(XmlWriter writer)
		{
			base.WriteXml(writer);
			this.WriteXmlForMenuItems(writer);
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000DB168 File Offset: 0x000D9368
		protected virtual void WriteXmlForMenuItems(XmlWriter writer)
		{
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in this.Items)
			{
				RibbonBarApplicationMenuItem ribbonBarApplicationMenuItem = (RibbonBarApplicationMenuItem)ribbonBarApplicationMenuItemBase;
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarApplicationMenuItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarApplicationMenuItem);
			}
		}

		// Token: 0x04001205 RID: 4613
		private RibbonBarApplicationMenuItemBaseCollection _items;
	}
}
