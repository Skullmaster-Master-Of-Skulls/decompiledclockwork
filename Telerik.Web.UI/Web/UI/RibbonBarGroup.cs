using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000F46 RID: 3910
	[ToolboxItem(false)]
	[XmlRoot("Group")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarGroup : WebControl, IRibbonBarSubComponent, IXmlSerializable
	{
		// Token: 0x17002F30 RID: 12080
		// (get) Token: 0x0600950B RID: 38155 RVA: 0x00215424 File Offset: 0x00213624
		// (set) Token: 0x0600950C RID: 38156 RVA: 0x00215444 File Offset: 0x00213644
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

		// Token: 0x17002F31 RID: 12081
		// (get) Token: 0x0600950D RID: 38157 RVA: 0x00215457 File Offset: 0x00213657
		// (set) Token: 0x0600950E RID: 38158 RVA: 0x00215477 File Offset: 0x00213677
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return (string)(this.ViewState["Value"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17002F32 RID: 12082
		// (get) Token: 0x0600950F RID: 38159 RVA: 0x0021548A File Offset: 0x0021368A
		// (set) Token: 0x06009510 RID: 38160 RVA: 0x002154AB File Offset: 0x002136AB
		[DefaultValue(false)]
		public bool EnableLauncher
		{
			get
			{
				return (bool)(this.ViewState["EnableLauncher"] ?? false);
			}
			set
			{
				this.ViewState["EnableLauncher"] = value;
			}
		}

		// Token: 0x17002F33 RID: 12083
		// (get) Token: 0x06009511 RID: 38161 RVA: 0x002154C3 File Offset: 0x002136C3
		// (set) Token: 0x06009512 RID: 38162 RVA: 0x002154CB File Offset: 0x002136CB
		[UrlProperty]
		public string CollapsedImageUrl { get; set; }

		// Token: 0x17002F34 RID: 12084
		// (get) Token: 0x06009513 RID: 38163 RVA: 0x002154D4 File Offset: 0x002136D4
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17002F35 RID: 12085
		// (get) Token: 0x06009514 RID: 38164 RVA: 0x00215507 File Offset: 0x00213707
		// (set) Token: 0x06009515 RID: 38165 RVA: 0x0021550F File Offset: 0x0021370F
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F36 RID: 12086
		// (get) Token: 0x06009516 RID: 38166 RVA: 0x00215518 File Offset: 0x00213718
		public RadRibbonBar RibbonBar
		{
			get
			{
				if (this.Container == null)
				{
					return null;
				}
				return this.Container.RibbonBar;
			}
		}

		// Token: 0x17002F37 RID: 12087
		// (get) Token: 0x06009517 RID: 38167 RVA: 0x00215530 File Offset: 0x00213730
		public RibbonBarTab Tab
		{
			get
			{
				RibbonBarGroupCollection ribbonBarGroupCollection = this.Container as RibbonBarGroupCollection;
				return ribbonBarGroupCollection.Container as RibbonBarTab;
			}
		}

		// Token: 0x06009518 RID: 38168 RVA: 0x00215556 File Offset: 0x00213756
		public List<RibbonBarItem> GetFunctionalItems()
		{
			return this.GetFunctionalItems(false);
		}

		// Token: 0x06009519 RID: 38169 RVA: 0x0021555F File Offset: 0x0021375F
		public List<RibbonBarItem> GetFunctionalItems(bool visibleOnly)
		{
			return RibbonBarGroup.GetFunctionalItems(visibleOnly, this.Items);
		}

		// Token: 0x0600951A RID: 38170 RVA: 0x00215570 File Offset: 0x00213770
		internal static List<RibbonBarItem> GetFunctionalItems(bool visibleOnly, RibbonBarItemCollection itemsCollection)
		{
			List<RibbonBarItem> list = new List<RibbonBarItem>();
			foreach (RibbonBarItem ribbonBarItem in itemsCollection)
			{
				if (!visibleOnly || ribbonBarItem.Visible)
				{
					RibbonBarControlGroup ribbonBarControlGroup = ribbonBarItem as RibbonBarControlGroup;
					if (ribbonBarControlGroup != null)
					{
						list.AddRange(ribbonBarControlGroup.GetFunctionalItems(visibleOnly));
					}
					else
					{
						RibbonBarButtonStrip ribbonBarButtonStrip = ribbonBarItem as RibbonBarButtonStrip;
						if (ribbonBarButtonStrip != null)
						{
							list.AddRange(visibleOnly ? ribbonBarButtonStrip.GetVisibleButtons().ToArray() : ribbonBarButtonStrip.Buttons.ToArray());
						}
						else
						{
							RibbonBarToggleList ribbonBarToggleList = ribbonBarItem as RibbonBarToggleList;
							if (ribbonBarToggleList != null)
							{
								list.AddRange(visibleOnly ? ribbonBarToggleList.GetVisibleButtons().ToArray() : ribbonBarToggleList.ToggleButtons.ToArray());
							}
							else if (!(ribbonBarItem is RibbonBarTemplateItem))
							{
								list.Add(ribbonBarItem);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600951B RID: 38171 RVA: 0x00215660 File Offset: 0x00213860
		public List<RibbonBarItem> GetVisibleFunctionalItems()
		{
			return this.GetFunctionalItems(true);
		}

		// Token: 0x0600951C RID: 38172 RVA: 0x0021566C File Offset: 0x0021386C
		public List<RibbonBarToggleList> GetToggleLists()
		{
			List<RibbonBarToggleList> list = new List<RibbonBarToggleList>();
			foreach (RibbonBarItem ribbonBarItem in this.Items)
			{
				RibbonBarToggleList ribbonBarToggleList = ribbonBarItem as RibbonBarToggleList;
				if (ribbonBarToggleList != null)
				{
					list.Add(ribbonBarToggleList);
				}
			}
			return list;
		}

		// Token: 0x0600951D RID: 38173 RVA: 0x002156D0 File Offset: 0x002138D0
		public RibbonBarButton FindButtonByValue(string value)
		{
			foreach (RibbonBarItem ribbonBarItem in this.GetFunctionalItems())
			{
				switch (ribbonBarItem.ItemType)
				{
				case RibbonBarItemType.Button:
				{
					RibbonBarButton ribbonBarButton = (RibbonBarButton)ribbonBarItem;
					if (ribbonBarButton.Value.Equals(value))
					{
						return ribbonBarButton;
					}
					break;
				}
				case RibbonBarItemType.SplitButton:
				{
					RibbonBarSplitButton ribbonBarSplitButton = (RibbonBarSplitButton)ribbonBarItem;
					RibbonBarButton ribbonBarButton2 = ribbonBarSplitButton.FindButtonByValue(value);
					if (ribbonBarButton2 != null)
					{
						return ribbonBarButton2;
					}
					break;
				}
				}
			}
			return null;
		}

		// Token: 0x0600951E RID: 38174 RVA: 0x0021576C File Offset: 0x0021396C
		public RibbonBarToggleButton FindToggleButtonByValue(string value)
		{
			foreach (RibbonBarItem ribbonBarItem in this.GetFunctionalItems())
			{
				RibbonBarItemType itemType = ribbonBarItem.ItemType;
				if (itemType == RibbonBarItemType.ToggleButton)
				{
					RibbonBarToggleButton ribbonBarToggleButton = (RibbonBarToggleButton)ribbonBarItem;
					if (ribbonBarToggleButton.Value.Equals(value))
					{
						return ribbonBarToggleButton;
					}
				}
			}
			return null;
		}

		// Token: 0x0600951F RID: 38175 RVA: 0x002157E4 File Offset: 0x002139E4
		public RibbonBarMenuItem FindMenuItemByValue(string value)
		{
			foreach (RibbonBarItem ribbonBarItem in this.GetFunctionalItems())
			{
				RibbonBarItemType itemType = ribbonBarItem.ItemType;
				if (itemType == RibbonBarItemType.Menu)
				{
					RibbonBarMenu ribbonBarMenu = (RibbonBarMenu)ribbonBarItem;
					RibbonBarMenuItem ribbonBarMenuItem = ribbonBarMenu.FindMenuItemByValue(value);
					if (ribbonBarMenuItem != null)
					{
						return ribbonBarMenuItem;
					}
				}
			}
			return null;
		}

		// Token: 0x17002F38 RID: 12088
		// (get) Token: 0x06009520 RID: 38176 RVA: 0x00215858 File Offset: 0x00213A58
		protected IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateControlRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x06009521 RID: 38177 RVA: 0x00215874 File Offset: 0x00213A74
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06009522 RID: 38178 RVA: 0x00215882 File Offset: 0x00213A82
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			((RibbonBarGroupRenderer)this.Renderer).RenderBeginTag(writer);
		}

		// Token: 0x06009523 RID: 38179 RVA: 0x00215895 File Offset: 0x00213A95
		protected virtual IRenderer CreateControlRenderer()
		{
			if (this.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarGroupLiteRenderer(this);
			}
			return new RibbonBarGroupClassicRenderer(this);
		}

		// Token: 0x06009524 RID: 38180 RVA: 0x002158B2 File Offset: 0x00213AB2
		internal void BaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x17002F39 RID: 12089
		// (get) Token: 0x06009525 RID: 38181 RVA: 0x002158BB File Offset: 0x00213ABB
		// (set) Token: 0x06009526 RID: 38182 RVA: 0x002158C4 File Offset: 0x00213AC4
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				if (this._parentWebControl != null && !this._parentWebControl.Controls.Contains(this))
				{
					this._parentWebControl.Controls.Add(this);
				}
				this.Items.ParentWebControl = this;
			}
		}

		// Token: 0x06009527 RID: 38183 RVA: 0x00215910 File Offset: 0x00213B10
		internal static List<RibbonBarItem> GetSerializableItems(bool visibleOnly, RibbonBarItemCollection itemsCollection)
		{
			List<RibbonBarItem> list = new List<RibbonBarItem>();
			foreach (RibbonBarItem ribbonBarItem in itemsCollection)
			{
				if (!visibleOnly || ribbonBarItem.Visible)
				{
					RibbonBarControlGroup ribbonBarControlGroup = ribbonBarItem as RibbonBarControlGroup;
					if (ribbonBarControlGroup != null)
					{
						list.AddRange(ribbonBarControlGroup.GetSerializableItems(visibleOnly));
					}
					else
					{
						RibbonBarButtonStrip ribbonBarButtonStrip = ribbonBarItem as RibbonBarButtonStrip;
						if (ribbonBarButtonStrip != null)
						{
							list.AddRange(ribbonBarButtonStrip.Buttons.ToArray());
						}
						else
						{
							RibbonBarToggleList ribbonBarToggleList = ribbonBarItem as RibbonBarToggleList;
							if (ribbonBarToggleList != null)
							{
								list.AddRange(ribbonBarToggleList.GetVisibleButtons().ToArray());
							}
							else
							{
								list.Add(ribbonBarItem);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06009528 RID: 38184 RVA: 0x002159C8 File Offset: 0x00213BC8
		internal List<RibbonBarItem> GetSerializableItems(bool visibleOnly)
		{
			return RibbonBarGroup.GetSerializableItems(visibleOnly, this.Items);
		}

		// Token: 0x06009529 RID: 38185 RVA: 0x002159D6 File Offset: 0x00213BD6
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600952A RID: 38186 RVA: 0x002159E2 File Offset: 0x00213BE2
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600952B RID: 38187 RVA: 0x002159EB File Offset: 0x00213BEB
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x0600952C RID: 38188 RVA: 0x002159F4 File Offset: 0x00213BF4
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForButtons(reader);
		}

		// Token: 0x0600952D RID: 38189 RVA: 0x00215A0C File Offset: 0x00213C0C
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
					case "Gallery":
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarGallery));
						RibbonBarGallery item11 = (RibbonBarGallery)xmlSerializer.Deserialize(reader);
						this.Items.Add(item11);
						break;
					}
					case "ControlGroup":
						using (XmlReader xmlReader = reader.ReadSubtree())
						{
							XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(RibbonBarControlGroup));
							RibbonBarControlGroup item12 = (RibbonBarControlGroup)xmlSerializer2.Deserialize(xmlReader);
							this.Items.Add(item12);
						}
						reader.MoveToContent();
						break;
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x0600952E RID: 38190 RVA: 0x00215DB8 File Offset: 0x00213FB8
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForButtons(writer);
		}

		// Token: 0x0600952F RID: 38191 RVA: 0x00215DD4 File Offset: 0x00213FD4
		protected virtual void WriteXmlForButtons(XmlWriter writer)
		{
			foreach (RibbonBarItem ribbonBarItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarItem);
			}
		}

		// Token: 0x04002AAD RID: 10925
		private RibbonBarItemCollection _items;

		// Token: 0x04002AAE RID: 10926
		private IRenderer _renderer;

		// Token: 0x04002AAF RID: 10927
		internal RadRibbonBar _ribbonBar;

		// Token: 0x04002AB0 RID: 10928
		private WebControl _parentWebControl;
	}
}
