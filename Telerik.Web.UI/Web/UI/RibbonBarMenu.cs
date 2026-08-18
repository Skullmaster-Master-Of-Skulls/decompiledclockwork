using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007CE RID: 1998
	[XmlRoot("Menu")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarMenu : RibbonBarMenuBaseItem, IXmlSerializable
	{
		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x000DB74D File Offset: 0x000D994D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarMenuItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarMenuItemCollection();
					this._items.Container = this;
				}
				return this._items;
			}
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x000DB774 File Offset: 0x000D9974
		public IList<RibbonBarMenuItem> GetVisibleItems()
		{
			List<RibbonBarMenuItem> list = new List<RibbonBarMenuItem>();
			foreach (RibbonBarMenuItem ribbonBarMenuItem in this.Items)
			{
				if (ribbonBarMenuItem.Visible)
				{
					list.Add(ribbonBarMenuItem);
				}
			}
			return list;
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x000DB7D8 File Offset: 0x000D99D8
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.Menu;
			}
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x000DB7DC File Offset: 0x000D99DC
		public RibbonBarMenuItem FindMenuItemByValue(string value)
		{
			foreach (RibbonBarMenuItem ribbonBarMenuItem in this.Items)
			{
				if (ribbonBarMenuItem.Value.Equals(value))
				{
					return ribbonBarMenuItem;
				}
				RibbonBarMenuItem ribbonBarMenuItem2 = ribbonBarMenuItem.FindMenuItemByValue(value);
				if (ribbonBarMenuItem2 != null)
				{
					return ribbonBarMenuItem2;
				}
			}
			return null;
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x060045B2 RID: 17842 RVA: 0x000DB84C File Offset: 0x000D9A4C
		// (set) Token: 0x060045B3 RID: 17843 RVA: 0x000DB854 File Offset: 0x000D9A54
		public override WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				if (!this._parentWebControl.Controls.Contains(this))
				{
					this._parentWebControl.Controls.Add(this);
				}
				this.Items.ParentWebControl = this;
			}
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x000DB88D File Offset: 0x000D9A8D
		internal override string RibbonBarItemTypeCssClass
		{
			get
			{
				return "rrbMenuButton";
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x000DB894 File Offset: 0x000D9A94
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarMenuLiteRenderer(this);
			}
			return new RibbonBarMenuClassicRenderer(this);
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x000DB8B1 File Offset: 0x000D9AB1
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x000DB8BD File Offset: 0x000D9ABD
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x000DB8C6 File Offset: 0x000D9AC6
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x000DB8CF File Offset: 0x000D9ACF
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForMenuItems(reader);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x000DB8E8 File Offset: 0x000D9AE8
		protected virtual void ReadXmlForMenuItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Menu")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "Menu" && reader.Name != "MenuItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarMenuItem));
					RibbonBarMenuItem item = (RibbonBarMenuItem)xmlSerializer.Deserialize(reader);
					this.Items.Add(item);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000DB993 File Offset: 0x000D9B93
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForMenuItems(writer);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x000DB9B0 File Offset: 0x000D9BB0
		protected virtual void WriteXmlForMenuItems(XmlWriter writer)
		{
			foreach (RibbonBarMenuItem ribbonBarMenuItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarMenuItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarMenuItem);
			}
		}

		// Token: 0x0400120D RID: 4621
		private RibbonBarMenuItemCollection _items;

		// Token: 0x0400120E RID: 4622
		private WebControl _parentWebControl;
	}
}
