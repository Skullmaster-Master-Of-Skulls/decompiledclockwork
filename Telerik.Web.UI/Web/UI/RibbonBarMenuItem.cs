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
	// Token: 0x02000F42 RID: 3906
	[ParseChildren(ChildrenAsProperties = true)]
	[ToolboxItem(false)]
	[XmlRoot("MenuItem")]
	public class RibbonBarMenuItem : WebControl, IRibbonBarSubComponent, IRibbonBarTextContainingItem, IRibbonBarImageContainingItem, IXmlSerializable, IRibbonBarCommandItem
	{
		// Token: 0x17002F1F RID: 12063
		// (get) Token: 0x060094DC RID: 38108 RVA: 0x00214CDC File Offset: 0x00212EDC
		public RibbonBarMenuItem ParentItem
		{
			get
			{
				RibbonBarMenuItemCollection ribbonBarMenuItemCollection = this.Container as RibbonBarMenuItemCollection;
				return (ribbonBarMenuItemCollection == null) ? null : (ribbonBarMenuItemCollection.Container as RibbonBarMenuItem);
			}
		}

		// Token: 0x17002F20 RID: 12064
		// (get) Token: 0x060094DD RID: 38109 RVA: 0x00214D08 File Offset: 0x00212F08
		// (set) Token: 0x060094DE RID: 38110 RVA: 0x00214D10 File Offset: 0x00212F10
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F21 RID: 12065
		// (get) Token: 0x060094DF RID: 38111 RVA: 0x00214D19 File Offset: 0x00212F19
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

		// Token: 0x17002F22 RID: 12066
		// (get) Token: 0x060094E0 RID: 38112 RVA: 0x00214D30 File Offset: 0x00212F30
		// (set) Token: 0x060094E1 RID: 38113 RVA: 0x00214D38 File Offset: 0x00212F38
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				if (!this.ParentWebControl.Controls.Contains(this))
				{
					this.ParentWebControl.Controls.Add(this);
				}
				this.Items.ParentWebControl = value;
			}
		}

		// Token: 0x17002F23 RID: 12067
		// (get) Token: 0x060094E2 RID: 38114 RVA: 0x00214D71 File Offset: 0x00212F71
		// (set) Token: 0x060094E3 RID: 38115 RVA: 0x00214D91 File Offset: 0x00212F91
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

		// Token: 0x17002F24 RID: 12068
		// (get) Token: 0x060094E4 RID: 38116 RVA: 0x00214DA4 File Offset: 0x00212FA4
		// (set) Token: 0x060094E5 RID: 38117 RVA: 0x00214DC4 File Offset: 0x00212FC4
		[DefaultValue("")]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17002F25 RID: 12069
		// (get) Token: 0x060094E6 RID: 38118 RVA: 0x00214DD7 File Offset: 0x00212FD7
		// (set) Token: 0x060094E7 RID: 38119 RVA: 0x00214DF7 File Offset: 0x00212FF7
		[DefaultValue("")]
		public string ImageAltText
		{
			get
			{
				return (string)(this.ViewState["ImageAltText"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageAltText"] = value;
			}
		}

		// Token: 0x17002F26 RID: 12070
		// (get) Token: 0x060094E8 RID: 38120 RVA: 0x00214E0A File Offset: 0x0021300A
		// (set) Token: 0x060094E9 RID: 38121 RVA: 0x00214E2A File Offset: 0x0021302A
		[DefaultValue("")]
		public string NavigateUrl
		{
			get
			{
				return (string)(this.ViewState["NavigateUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17002F27 RID: 12071
		// (get) Token: 0x060094EA RID: 38122 RVA: 0x00214E3D File Offset: 0x0021303D
		// (set) Token: 0x060094EB RID: 38123 RVA: 0x00214E5D File Offset: 0x0021305D
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

		// Token: 0x17002F28 RID: 12072
		// (get) Token: 0x060094EC RID: 38124 RVA: 0x00214E70 File Offset: 0x00213070
		// (set) Token: 0x060094ED RID: 38125 RVA: 0x00214E90 File Offset: 0x00213090
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

		// Token: 0x17002F29 RID: 12073
		// (get) Token: 0x060094EE RID: 38126 RVA: 0x00214EA3 File Offset: 0x002130A3
		// (set) Token: 0x060094EF RID: 38127 RVA: 0x00214EC3 File Offset: 0x002130C3
		public override string ToolTip
		{
			get
			{
				return (string)(this.ViewState["ToolTip"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17002F2A RID: 12074
		// (get) Token: 0x060094F0 RID: 38128 RVA: 0x00214ED6 File Offset: 0x002130D6
		// (set) Token: 0x060094F1 RID: 38129 RVA: 0x00214EF6 File Offset: 0x002130F6
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the command name associated with the MenuItem that is passed to the Command event.")]
		public string CommandName
		{
			get
			{
				return ((string)this.ViewState["CommandName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17002F2B RID: 12075
		// (get) Token: 0x060094F2 RID: 38130 RVA: 0x00214F09 File Offset: 0x00213109
		// (set) Token: 0x060094F3 RID: 38131 RVA: 0x00214F29 File Offset: 0x00213129
		[Description("Gets or sets an optional parameter passed to the Command event along with the associated CommandName.")]
		[Bindable(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		public string CommandArgument
		{
			get
			{
				return ((string)this.ViewState["CommandArgument"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x17002F2C RID: 12076
		// (get) Token: 0x060094F4 RID: 38132 RVA: 0x00214F3C File Offset: 0x0021313C
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

		// Token: 0x060094F5 RID: 38133 RVA: 0x00214F63 File Offset: 0x00213163
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x060094F6 RID: 38134 RVA: 0x00214F8C File Offset: 0x0021318C
		public override void RenderControl(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0)
			{
				new MenuButtonWrap
				{
					Container = this,
					Text = (string.IsNullOrEmpty(this.Text) ? "&nbsp;" : this.Text),
					ImageUrl = this.ImageUrl,
					DisabledImageUrl = this.DisabledImageUrl,
					ImageAltText = this.ImageAltText,
					Enabled = this.Enabled,
					NavigateUrl = this.NavigateUrl,
					CssClass = this.CssClass,
					AccessKey = this.AccessKey,
					ToolTip = this.ToolTip
				}.RenderControl(writer);
				return;
			}
			MenuWrap menuWrap = new MenuWrap();
			menuWrap.Container = this;
			menuWrap.Text = (string.IsNullOrEmpty(this.Text) ? "&nbsp;" : this.Text);
			menuWrap.ImageUrl = this.ImageUrl;
			menuWrap.DisabledImageUrl = this.DisabledImageUrl;
			menuWrap.ImageAltText = this.ImageAltText;
			menuWrap.Enabled = this.Enabled;
			menuWrap.CssClass = this.CssClass;
			menuWrap.AccessKey = this.AccessKey;
			menuWrap.Items.AddRange(this._items);
			menuWrap.RenderControl(writer);
		}

		// Token: 0x060094F7 RID: 38135 RVA: 0x002150CC File Offset: 0x002132CC
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

		// Token: 0x060094F8 RID: 38136 RVA: 0x00215130 File Offset: 0x00213330
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

		// Token: 0x060094F9 RID: 38137 RVA: 0x002151A0 File Offset: 0x002133A0
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x060094FA RID: 38138 RVA: 0x002151AC File Offset: 0x002133AC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060094FB RID: 38139 RVA: 0x002151B5 File Offset: 0x002133B5
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060094FC RID: 38140 RVA: 0x002151BE File Offset: 0x002133BE
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			if (!reader.IsEmptyElement)
			{
				this.ReadXmlForMenuItems(reader);
			}
		}

		// Token: 0x060094FD RID: 38141 RVA: 0x002151E0 File Offset: 0x002133E0
		protected virtual void ReadXmlForMenuItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "MenuItem")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "MenuItem")
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

		// Token: 0x060094FE RID: 38142 RVA: 0x00215276 File Offset: 0x00213476
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForMenuItems(writer);
		}

		// Token: 0x060094FF RID: 38143 RVA: 0x00215294 File Offset: 0x00213494
		protected virtual void WriteXmlForMenuItems(XmlWriter writer)
		{
			foreach (RibbonBarMenuItem ribbonBarMenuItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarMenuItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarMenuItem);
			}
		}

		// Token: 0x04002A9F RID: 10911
		protected const string NonBreakingSpace = "&nbsp;";

		// Token: 0x04002AA0 RID: 10912
		private WebControl _parentWebControl;

		// Token: 0x04002AA1 RID: 10913
		private RibbonBarMenuItemCollection _items;
	}
}
