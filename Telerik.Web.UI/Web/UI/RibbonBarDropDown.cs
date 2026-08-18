using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E59 RID: 3673
	[XmlRoot("DropDown")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarDropDown : RibbonBarDropDownItem, IXmlSerializable
	{
		// Token: 0x17002C01 RID: 11265
		// (get) Token: 0x06008B3D RID: 35645 RVA: 0x001FAF9C File Offset: 0x001F919C
		internal override string ItemCssClass
		{
			get
			{
				return "rrbDropDown";
			}
		}

		// Token: 0x17002C02 RID: 11266
		// (get) Token: 0x06008B3E RID: 35646 RVA: 0x001FAFA3 File Offset: 0x001F91A3
		internal override string InnerCssClass
		{
			get
			{
				return "rrbDDInner";
			}
		}

		// Token: 0x17002C03 RID: 11267
		// (get) Token: 0x06008B3F RID: 35647 RVA: 0x001FAFAA File Offset: 0x001F91AA
		internal override string InputCssClass
		{
			get
			{
				return "rrbDDFakeInput";
			}
		}

		// Token: 0x17002C04 RID: 11268
		// (get) Token: 0x06008B40 RID: 35648 RVA: 0x001FAFB1 File Offset: 0x001F91B1
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.DropDown;
			}
		}

		// Token: 0x17002C05 RID: 11269
		// (get) Token: 0x06008B41 RID: 35649 RVA: 0x001FAFB5 File Offset: 0x001F91B5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarListItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarListItemCollection();
					this._items.Container = this;
				}
				return this._items;
			}
		}

		// Token: 0x06008B42 RID: 35650 RVA: 0x001FAFDC File Offset: 0x001F91DC
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarDropDownLiteRenderer(this);
			}
			return new RibbonBarDropDownClassicRenderer(this);
		}

		// Token: 0x06008B43 RID: 35651 RVA: 0x001FAFFC File Offset: 0x001F91FC
		public IList<RibbonBarListItem> GetVisibleItems()
		{
			List<RibbonBarListItem> list = new List<RibbonBarListItem>();
			foreach (RibbonBarListItem ribbonBarListItem in this.Items)
			{
				if (ribbonBarListItem.Visible)
				{
					list.Add(ribbonBarListItem);
				}
			}
			return list;
		}

		// Token: 0x17002C06 RID: 11270
		// (get) Token: 0x06008B44 RID: 35652 RVA: 0x001FB060 File Offset: 0x001F9260
		// (set) Token: 0x06008B45 RID: 35653 RVA: 0x001FB09C File Offset: 0x001F929C
		[DefaultValue(-1)]
		[Browsable(false)]
		[Category("Behavior")]
		[Description("SelectedIndex")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						return i;
					}
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					if (this.Items.Count != 0)
					{
						throw new ArgumentOutOfRangeException("value", value, "The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered.");
					}
					value = -1;
				}
				if ((this.Items.Count != 0 && value < this.Items.Count) || value == -1)
				{
					this.ClearSelection();
					if (value >= 0)
					{
						this.Items[value].Selected = true;
					}
				}
			}
		}

		// Token: 0x06008B46 RID: 35654 RVA: 0x001FB110 File Offset: 0x001F9310
		public void ClearSelection()
		{
			foreach (RibbonBarListItem ribbonBarListItem in this.Items)
			{
				ribbonBarListItem.Selected = false;
			}
		}

		// Token: 0x06008B47 RID: 35655 RVA: 0x001FB164 File Offset: 0x001F9364
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008B48 RID: 35656 RVA: 0x001FB170 File Offset: 0x001F9370
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008B49 RID: 35657 RVA: 0x001FB179 File Offset: 0x001F9379
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008B4A RID: 35658 RVA: 0x001FB182 File Offset: 0x001F9382
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForListItems(reader);
		}

		// Token: 0x06008B4B RID: 35659 RVA: 0x001FB19C File Offset: 0x001F939C
		protected virtual void ReadXmlForListItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "DropDown")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "DropDown" && reader.Name != "ListItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarListItem));
					RibbonBarListItem item = (RibbonBarListItem)xmlSerializer.Deserialize(reader);
					this.Items.Add(item);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06008B4C RID: 35660 RVA: 0x001FB247 File Offset: 0x001F9447
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForListItems(writer);
		}

		// Token: 0x06008B4D RID: 35661 RVA: 0x001FB264 File Offset: 0x001F9464
		protected virtual void WriteXmlForListItems(XmlWriter writer)
		{
			foreach (RibbonBarListItem ribbonBarListItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarListItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarListItem);
			}
		}

		// Token: 0x0400270F RID: 9999
		private RibbonBarListItemCollection _items;
	}
}
