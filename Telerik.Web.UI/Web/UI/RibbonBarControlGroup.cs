using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E5B RID: 3675
	[XmlRoot("ControlGroup")]
	[ParseChildren(typeof(RibbonBarItem), ChildrenAsProperties = true, DefaultProperty = "Items")]
	public class RibbonBarControlGroup : RibbonBarItem, IXmlSerializable
	{
		// Token: 0x17002C0C RID: 11276
		// (get) Token: 0x06008B58 RID: 35672 RVA: 0x001FB41F File Offset: 0x001F961F
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public RibbonBarItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarItemCollection();
					this._items.Container = this;
					this._items.ParentWebControl = this;
				}
				return this._items;
			}
		}

		// Token: 0x17002C0D RID: 11277
		// (get) Token: 0x06008B59 RID: 35673 RVA: 0x001FB452 File Offset: 0x001F9652
		// (set) Token: 0x06008B5A RID: 35674 RVA: 0x001FB473 File Offset: 0x001F9673
		[Category("Appearance")]
		[DefaultValue(RibbonBarControlGroupOrientation.Auto)]
		[Description("The orientation in which the items will be placed inside the panel.")]
		public RibbonBarControlGroupOrientation Orientation
		{
			get
			{
				return (RibbonBarControlGroupOrientation)(this.ViewState["Orientation"] ?? RibbonBarControlGroupOrientation.Auto);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x001FB48B File Offset: 0x001F968B
		public List<RibbonBarItem> GetFunctionalItems()
		{
			return this.GetFunctionalItems(false);
		}

		// Token: 0x06008B5C RID: 35676 RVA: 0x001FB494 File Offset: 0x001F9694
		public List<RibbonBarItem> GetFunctionalItems(bool visibleOnly)
		{
			return RibbonBarGroup.GetFunctionalItems(visibleOnly, this.Items);
		}

		// Token: 0x06008B5D RID: 35677 RVA: 0x001FB4A2 File Offset: 0x001F96A2
		internal List<RibbonBarItem> GetSerializableItems(bool visibleOnly)
		{
			return RibbonBarGroup.GetSerializableItems(visibleOnly, this.Items);
		}

		// Token: 0x17002C0E RID: 11278
		// (get) Token: 0x06008B5E RID: 35678 RVA: 0x001FB4B0 File Offset: 0x001F96B0
		// (set) Token: 0x06008B5F RID: 35679 RVA: 0x001FB4B8 File Offset: 0x001F96B8
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

		// Token: 0x17002C0F RID: 11279
		// (get) Token: 0x06008B60 RID: 35680 RVA: 0x001FB4F1 File Offset: 0x001F96F1
		// (set) Token: 0x06008B61 RID: 35681 RVA: 0x001FB4FC File Offset: 0x001F96FC
		public override RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
			internal set
			{
				this._group = value;
				foreach (RibbonBarItem ribbonBarItem in this.Items)
				{
					ribbonBarItem.Group = value;
				}
			}
		}

		// Token: 0x17002C10 RID: 11280
		// (get) Token: 0x06008B62 RID: 35682 RVA: 0x001FB558 File Offset: 0x001F9758
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.ControlGroup;
			}
		}

		// Token: 0x06008B63 RID: 35683 RVA: 0x001FB55C File Offset: 0x001F975C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string text = "rrbControlGroup";
			switch (this.Orientation)
			{
			case RibbonBarControlGroupOrientation.Horizontal:
				text += " rrbHbox";
				break;
			case RibbonBarControlGroupOrientation.Vertical:
				text += " rrbVbox";
				break;
			case RibbonBarControlGroupOrientation.Auto:
				text += " rrbAbox";
				break;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06008B64 RID: 35684 RVA: 0x001FB5C3 File Offset: 0x001F97C3
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x06008B65 RID: 35685 RVA: 0x001FB5CB File Offset: 0x001F97CB
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008B66 RID: 35686 RVA: 0x001FB5D7 File Offset: 0x001F97D7
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008B67 RID: 35687 RVA: 0x001FB5E0 File Offset: 0x001F97E0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008B68 RID: 35688 RVA: 0x001FB5E9 File Offset: 0x001F97E9
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForButtons(reader);
		}

		// Token: 0x06008B69 RID: 35689 RVA: 0x001FB600 File Offset: 0x001F9800
		protected virtual void ReadXmlForButtons(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					string name;
					switch (name = reader.Name)
					{
					case "ToggleButton":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarToggleButton));
						RibbonBarToggleButton item = (RibbonBarToggleButton)xmlSerializer.Deserialize(reader);
						this.Items.Add(item);
						break;
					}
					case "Button":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarButton));
						RibbonBarButton item2 = (RibbonBarButton)xmlSerializer.Deserialize(reader);
						this.Items.Add(item2);
						break;
					}
					case "ToggleList":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarToggleList));
						RibbonBarToggleList item3 = (RibbonBarToggleList)xmlSerializer.Deserialize(reader);
						this.Items.Add(item3);
						break;
					}
					case "ButtonStrip":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarButtonStrip));
						RibbonBarButtonStrip item4 = (RibbonBarButtonStrip)xmlSerializer.Deserialize(reader);
						this.Items.Add(item4);
						break;
					}
					case "SplitButton":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarSplitButton));
						RibbonBarSplitButton item5 = (RibbonBarSplitButton)xmlSerializer.Deserialize(reader);
						this.Items.Add(item5);
						break;
					}
					case "Menu":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarMenu));
						RibbonBarMenu item6 = (RibbonBarMenu)xmlSerializer.Deserialize(reader);
						this.Items.Add(item6);
						break;
					}
					case "DropDown":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarDropDown));
						RibbonBarDropDown item7 = (RibbonBarDropDown)xmlSerializer.Deserialize(reader);
						this.Items.Add(item7);
						break;
					}
					case "ComboBox":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarComboBox));
						RibbonBarComboBox item8 = (RibbonBarComboBox)xmlSerializer.Deserialize(reader);
						this.Items.Add(item8);
						break;
					}
					case "NumericTextBox":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarNumericTextBox));
						RibbonBarNumericTextBox item9 = (RibbonBarNumericTextBox)xmlSerializer.Deserialize(reader);
						this.Items.Add(item9);
						break;
					}
					case "ColorPicker":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarColorPicker));
						RibbonBarColorPicker item10 = (RibbonBarColorPicker)xmlSerializer.Deserialize(reader);
						this.Items.Add(item10);
						break;
					}
					case "ControlGroup":
						using (XmlReader xmlReader = reader.ReadSubtree())
						{
							XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(RibbonBarControlGroup));
							RibbonBarControlGroup item11 = (RibbonBarControlGroup)xmlSerializer2.Deserialize(xmlReader);
							this.Items.Add(item11);
						}
						reader.MoveToContent();
						break;
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06008B6A RID: 35690 RVA: 0x001FB96C File Offset: 0x001F9B6C
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForButtons(writer);
		}

		// Token: 0x06008B6B RID: 35691 RVA: 0x001FB988 File Offset: 0x001F9B88
		protected virtual void WriteXmlForButtons(XmlWriter writer)
		{
			foreach (RibbonBarItem ribbonBarItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarItem);
			}
		}

		// Token: 0x04002710 RID: 10000
		private RibbonBarItemCollection _items;

		// Token: 0x04002711 RID: 10001
		private WebControl _parentWebControl;

		// Token: 0x04002712 RID: 10002
		private RibbonBarGroup _group;
	}
}
