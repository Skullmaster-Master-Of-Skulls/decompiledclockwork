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
	// Token: 0x02000F4B RID: 3915
	[ParseChildren(typeof(RibbonBarGroup), ChildrenAsProperties = true, DefaultProperty = "Groups")]
	[XmlRoot("Tab")]
	[ToolboxItem(false)]
	public class RibbonBarTab : WebControl, IRibbonBarSubComponent, IXmlSerializable
	{
		// Token: 0x17002F46 RID: 12102
		// (get) Token: 0x0600955C RID: 38236 RVA: 0x002162EC File Offset: 0x002144EC
		// (set) Token: 0x0600955D RID: 38237 RVA: 0x0021630C File Offset: 0x0021450C
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

		// Token: 0x17002F47 RID: 12103
		// (get) Token: 0x0600955E RID: 38238 RVA: 0x0021631F File Offset: 0x0021451F
		// (set) Token: 0x0600955F RID: 38239 RVA: 0x0021633F File Offset: 0x0021453F
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

		// Token: 0x17002F48 RID: 12104
		// (get) Token: 0x06009560 RID: 38240 RVA: 0x00216352 File Offset: 0x00214552
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public RibbonBarGroupCollection Groups
		{
			get
			{
				if (this._groups == null)
				{
					this._groups = new RibbonBarGroupCollection();
					this._groups.Container = this;
				}
				return this._groups;
			}
		}

		// Token: 0x17002F49 RID: 12105
		// (get) Token: 0x06009561 RID: 38241 RVA: 0x00216379 File Offset: 0x00214579
		// (set) Token: 0x06009562 RID: 38242 RVA: 0x00216381 File Offset: 0x00214581
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F4A RID: 12106
		// (get) Token: 0x06009563 RID: 38243 RVA: 0x0021638A File Offset: 0x0021458A
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

		// Token: 0x06009564 RID: 38244 RVA: 0x002163A4 File Offset: 0x002145A4
		public List<RibbonBarGroup> GetVisibleGroups()
		{
			List<RibbonBarGroup> list = new List<RibbonBarGroup>();
			foreach (RibbonBarGroup ribbonBarGroup in this.Groups)
			{
				if (ribbonBarGroup.Visible)
				{
					list.Add(ribbonBarGroup);
				}
			}
			return list;
		}

		// Token: 0x06009565 RID: 38245 RVA: 0x00216408 File Offset: 0x00214608
		public RibbonBarGroup FindGroupByValue(string value)
		{
			foreach (RibbonBarGroup ribbonBarGroup in this.Groups)
			{
				if (ribbonBarGroup.Value.Equals(value))
				{
					return ribbonBarGroup;
				}
			}
			return null;
		}

		// Token: 0x06009566 RID: 38246 RVA: 0x0021646C File Offset: 0x0021466C
		public RibbonBarButton FindButtonByValue(string value)
		{
			foreach (RibbonBarGroup ribbonBarGroup in this.Groups)
			{
				RibbonBarButton ribbonBarButton = ribbonBarGroup.FindButtonByValue(value);
				if (ribbonBarButton != null)
				{
					return ribbonBarButton;
				}
			}
			return null;
		}

		// Token: 0x06009567 RID: 38247 RVA: 0x002164CC File Offset: 0x002146CC
		public RibbonBarToggleButton FindToggleButtonByValue(string value)
		{
			foreach (RibbonBarGroup ribbonBarGroup in this.Groups)
			{
				RibbonBarToggleButton ribbonBarToggleButton = ribbonBarGroup.FindToggleButtonByValue(value);
				if (ribbonBarToggleButton != null)
				{
					return ribbonBarToggleButton;
				}
			}
			return null;
		}

		// Token: 0x06009568 RID: 38248 RVA: 0x0021652C File Offset: 0x0021472C
		public RibbonBarMenuItem FindMenuItemByValue(string value)
		{
			foreach (RibbonBarGroup ribbonBarGroup in this.Groups)
			{
				RibbonBarMenuItem ribbonBarMenuItem = ribbonBarGroup.FindMenuItemByValue(value);
				if (ribbonBarMenuItem != null)
				{
					return ribbonBarMenuItem;
				}
			}
			return null;
		}

		// Token: 0x17002F4B RID: 12107
		// (get) Token: 0x06009569 RID: 38249 RVA: 0x0021658C File Offset: 0x0021478C
		// (set) Token: 0x0600956A RID: 38250 RVA: 0x00216594 File Offset: 0x00214794
		[Obsolete("This property is obsolete since Q3 2011 BETA. Contextual tabs are now added in contextual tab groups defined in RadRibbonBar.", false)]
		public string ContextualTabID
		{
			get
			{
				return this._contextualTabId;
			}
			set
			{
				this._contextualTabId = value;
			}
		}

		// Token: 0x17002F4C RID: 12108
		// (get) Token: 0x0600956B RID: 38251 RVA: 0x0021659D File Offset: 0x0021479D
		// (set) Token: 0x0600956C RID: 38252 RVA: 0x002165A5 File Offset: 0x002147A5
		public RibbonBarContextualTabGroup ContextualTabGroup { get; internal set; }

		// Token: 0x17002F4D RID: 12109
		// (get) Token: 0x0600956D RID: 38253 RVA: 0x002165AE File Offset: 0x002147AE
		// (set) Token: 0x0600956E RID: 38254 RVA: 0x002165B8 File Offset: 0x002147B8
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
				this.Groups.ParentWebControl = this._parentWebControl;
			}
		}

		// Token: 0x17002F4E RID: 12110
		// (get) Token: 0x0600956F RID: 38255 RVA: 0x00216609 File Offset: 0x00214809
		// (set) Token: 0x06009570 RID: 38256 RVA: 0x00216611 File Offset: 0x00214811
		[DefaultValue(false)]
		internal bool Selected { get; set; }

		// Token: 0x17002F4F RID: 12111
		// (get) Token: 0x06009571 RID: 38257 RVA: 0x0021661A File Offset: 0x0021481A
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

		// Token: 0x06009572 RID: 38258 RVA: 0x00216636 File Offset: 0x00214836
		protected virtual IRenderer CreateControlRenderer()
		{
			if (this.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarTabLiteRenderer(this);
			}
			return new RibbonBarTabClassicRenderer(this);
		}

		// Token: 0x06009573 RID: 38259 RVA: 0x00216654 File Offset: 0x00214854
		internal void BaseAddAttributesToRender(HtmlTextWriter writer)
		{
			bool enabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Enabled = enabled;
		}

		// Token: 0x17002F50 RID: 12112
		// (get) Token: 0x06009574 RID: 38260 RVA: 0x0021667D File Offset: 0x0021487D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x06009575 RID: 38261 RVA: 0x0021668A File Offset: 0x0021488A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06009576 RID: 38262 RVA: 0x00216698 File Offset: 0x00214898
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06009577 RID: 38263 RVA: 0x002166A6 File Offset: 0x002148A6
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06009578 RID: 38264 RVA: 0x002166B2 File Offset: 0x002148B2
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06009579 RID: 38265 RVA: 0x002166BB File Offset: 0x002148BB
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x0600957A RID: 38266 RVA: 0x002166C4 File Offset: 0x002148C4
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForGroups(reader);
		}

		// Token: 0x0600957B RID: 38267 RVA: 0x002166DC File Offset: 0x002148DC
		protected virtual void ReadXmlForGroups(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarGroup));
						RibbonBarGroup group = (RibbonBarGroup)xmlSerializer.Deserialize(xmlReader);
						this.Groups.Add(group);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x0600957C RID: 38268 RVA: 0x0021675C File Offset: 0x0021495C
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForGroups(writer);
		}

		// Token: 0x0600957D RID: 38269 RVA: 0x00216778 File Offset: 0x00214978
		protected virtual void WriteXmlForGroups(XmlWriter writer)
		{
			foreach (RibbonBarGroup o in this.Groups)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarGroup));
				xmlSerializer.Serialize(writer, o);
			}
		}

		// Token: 0x04002ABC RID: 10940
		private RibbonBarGroupCollection _groups;

		// Token: 0x04002ABD RID: 10941
		private string _contextualTabId;

		// Token: 0x04002ABE RID: 10942
		internal RadRibbonBar _ribbonBar;

		// Token: 0x04002ABF RID: 10943
		private IRenderer _renderer;

		// Token: 0x04002AC0 RID: 10944
		private WebControl _parentWebControl;
	}
}
