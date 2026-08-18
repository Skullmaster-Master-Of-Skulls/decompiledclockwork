using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E3A RID: 3642
	[ToolboxItem(false)]
	[XmlRoot("ApplicationMenu")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarApplicationMenu : WebControl, IXmlSerializable, INamingContainer
	{
		// Token: 0x17002BC4 RID: 11204
		// (get) Token: 0x06008A77 RID: 35447 RVA: 0x001F9784 File Offset: 0x001F7984
		// (set) Token: 0x06008A78 RID: 35448 RVA: 0x001F978C File Offset: 0x001F798C
		public RadRibbonBar RibbonBar { get; internal set; }

		// Token: 0x06008A79 RID: 35449 RVA: 0x001F9795 File Offset: 0x001F7995
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06008A7A RID: 35450 RVA: 0x001F97A4 File Offset: 0x001F79A4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				base.Dispose();
				if (this._footerPane != null)
				{
					this._footerPane.Dispose();
				}
				if (this._auxiliaryPane != null)
				{
					this._auxiliaryPane.Dispose();
				}
			}
		}

		// Token: 0x17002BC5 RID: 11205
		// (get) Token: 0x06008A7B RID: 35451 RVA: 0x001F97D5 File Offset: 0x001F79D5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarApplicationMenuItemBaseCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarApplicationMenuItemBaseCollection(this.RibbonBar);
				}
				return this._items;
			}
		}

		// Token: 0x17002BC6 RID: 11206
		// (get) Token: 0x06008A7C RID: 35452 RVA: 0x001F97F6 File Offset: 0x001F79F6
		// (set) Token: 0x06008A7D RID: 35453 RVA: 0x001F982B File Offset: 0x001F7A2B
		[Category("Appearance")]
		[Description("Specifies the rendering mode of the control")]
		[NotifyParentProperty(true)]
		[DefaultValue(RenderMode.Classic)]
		public virtual RenderMode RenderMode
		{
			get
			{
				if (this.RibbonBar != null)
				{
					return this.RibbonBar.ResolvedRenderMode;
				}
				return (RenderMode)(this.ViewState["RenderMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderMode"] = value;
			}
		}

		// Token: 0x06008A7E RID: 35454 RVA: 0x001F99D8 File Offset: 0x001F7BD8
		internal IEnumerable<RibbonBarApplicationMenuItemBase> GetVisibleItems()
		{
			foreach (RibbonBarApplicationMenuItemBase item in this.Items)
			{
				if (item.Visible)
				{
					yield return item;
				}
			}
			yield break;
		}

		// Token: 0x17002BC7 RID: 11207
		// (get) Token: 0x06008A7F RID: 35455 RVA: 0x001F99F5 File Offset: 0x001F7BF5
		[NotifyParentProperty(true)]
		[Description("Footer Pane")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public RibbonBarApplicationMenuFooterPane FooterPane
		{
			get
			{
				if (this._footerPane == null)
				{
					this._footerPane = new RibbonBarApplicationMenuFooterPane();
				}
				return this._footerPane;
			}
		}

		// Token: 0x17002BC8 RID: 11208
		// (get) Token: 0x06008A80 RID: 35456 RVA: 0x001F9A10 File Offset: 0x001F7C10
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[Description("Auxiliary Pane")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public RibbonBarApplicationMenuAuxiliaryPane AuxiliaryPane
		{
			get
			{
				if (this._auxiliaryPane == null)
				{
					this._auxiliaryPane = new RibbonBarApplicationMenuAuxiliaryPane();
				}
				return this._auxiliaryPane;
			}
		}

		// Token: 0x17002BC9 RID: 11209
		// (get) Token: 0x06008A81 RID: 35457 RVA: 0x001F9A2B File Offset: 0x001F7C2B
		// (set) Token: 0x06008A82 RID: 35458 RVA: 0x001F9A4B File Offset: 0x001F7C4B
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

		// Token: 0x17002BCA RID: 11210
		// (get) Token: 0x06008A83 RID: 35459 RVA: 0x001F9A5E File Offset: 0x001F7C5E
		// (set) Token: 0x06008A84 RID: 35460 RVA: 0x001F9A66 File Offset: 0x001F7C66
		internal bool AllowRender
		{
			get
			{
				return this._allowRender;
			}
			set
			{
				this._allowRender = value;
			}
		}

		// Token: 0x17002BCB RID: 11211
		// (get) Token: 0x06008A85 RID: 35461 RVA: 0x001F9A6F File Offset: 0x001F7C6F
		// (set) Token: 0x06008A86 RID: 35462 RVA: 0x001F9A77 File Offset: 0x001F7C77
		internal string SkinToRender
		{
			get
			{
				return this._skinToRender;
			}
			set
			{
				this._skinToRender = value;
			}
		}

		// Token: 0x17002BCC RID: 11212
		// (get) Token: 0x06008A87 RID: 35463 RVA: 0x001F9A80 File Offset: 0x001F7C80
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

		// Token: 0x06008A88 RID: 35464 RVA: 0x001F9A9C File Offset: 0x001F7C9C
		protected virtual IRenderer CreateControlRenderer()
		{
			if (this.RenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarApplicationMenuLiteRenderer(this);
			}
			return new RibbonBarApplicationMenuClassicRenderer(this);
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x001F9AB4 File Offset: 0x001F7CB4
		internal void BaseAddAttributesToRender(HtmlTextWriter writer)
		{
			bool enabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Enabled = enabled;
		}

		// Token: 0x17002BCD RID: 11213
		// (get) Token: 0x06008A8A RID: 35466 RVA: 0x001F9ADD File Offset: 0x001F7CDD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x06008A8B RID: 35467 RVA: 0x001F9AEA File Offset: 0x001F7CEA
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06008A8C RID: 35468 RVA: 0x001F9AF8 File Offset: 0x001F7CF8
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06008A8D RID: 35469 RVA: 0x001F9B06 File Offset: 0x001F7D06
		public override void RenderControl(HtmlTextWriter writer)
		{
			if (this.AllowRender)
			{
				base.RenderControl(writer);
			}
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x001F9B17 File Offset: 0x001F7D17
		protected string GetFooterPaneCssClass()
		{
			return ((RibbonBarApplicationMenuRenderBase)this.Renderer).GetFooterPaneCssClass();
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x001F9B29 File Offset: 0x001F7D29
		protected string GetAuxiliaryPaneContentCssClass()
		{
			return ((RibbonBarApplicationMenuRenderBase)this.Renderer).GetAuxiliaryPaneContentCssClass();
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x001F9B3B File Offset: 0x001F7D3B
		protected override void CreateChildControls()
		{
			this.CreateFooterContent();
			this.CreateAuxiliaryContent();
		}

		// Token: 0x06008A91 RID: 35473 RVA: 0x001F9B4C File Offset: 0x001F7D4C
		private void CreateFooterContent()
		{
			if (this.FooterPane.ContentTemplate != null || this.FooterPane.ContentWrapper.Controls.Count > 0)
			{
				this.FooterPane.ContentWrapper.CssClass = this.GetFooterPaneCssClass();
				this.Controls.Add(this.FooterPane.ContentWrapper);
				if (this.FooterPane.ContentTemplate != null)
				{
					this.ApplyTemplate(this.FooterPane.ContentWrapper, this.FooterPane.ContentTemplate);
				}
			}
		}

		// Token: 0x06008A92 RID: 35474 RVA: 0x001F9BD4 File Offset: 0x001F7DD4
		private void CreateAuxiliaryContent()
		{
			this.AuxiliaryPane.ContentWrapper.CssClass = this.GetAuxiliaryPaneContentCssClass();
			this.Controls.Add(this.AuxiliaryPane.ContentWrapper);
			if (this.AuxiliaryPane.ContentTemplate != null)
			{
				this.ApplyTemplate(this.AuxiliaryPane.ContentWrapper, this.AuxiliaryPane.ContentTemplate);
			}
		}

		// Token: 0x06008A93 RID: 35475 RVA: 0x001F9C38 File Offset: 0x001F7E38
		private void ApplyTemplate(WebControl control, ITemplate template)
		{
			int i = control.Controls.Count;
			if (template != null)
			{
				template.InstantiateIn(control);
			}
			while (i > 0)
			{
				control.Controls.Add(control.Controls[0]);
				i--;
			}
		}

		// Token: 0x06008A94 RID: 35476 RVA: 0x001F9C7C File Offset: 0x001F7E7C
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008A95 RID: 35477 RVA: 0x001F9C88 File Offset: 0x001F7E88
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008A96 RID: 35478 RVA: 0x001F9C91 File Offset: 0x001F7E91
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008A97 RID: 35479 RVA: 0x001F9C9A File Offset: 0x001F7E9A
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForMenuItems(reader);
		}

		// Token: 0x06008A98 RID: 35480 RVA: 0x001F9CB4 File Offset: 0x001F7EB4
		protected virtual void ReadXmlForMenuItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ApplicationMenu")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "ApplicationMenu" && reader.Name != "ApplicationMenuItem" && reader.Name != "ApplicationSplitMenuItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					if (reader.Name == "ApplicationMenuItem")
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarApplicationMenuItem));
						RibbonBarApplicationMenuItem item = (RibbonBarApplicationMenuItem)xmlSerializer.Deserialize(reader);
						this.Items.Add(item);
					}
					else
					{
						XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(RibbonBarApplicationSplitMenuItem));
						RibbonBarApplicationSplitMenuItem item2 = (RibbonBarApplicationSplitMenuItem)xmlSerializer2.Deserialize(reader);
						this.Items.Add(item2);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06008A99 RID: 35481 RVA: 0x001F9DAE File Offset: 0x001F7FAE
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForMenuItems(writer);
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x001F9DCC File Offset: 0x001F7FCC
		protected virtual void WriteXmlForMenuItems(XmlWriter writer)
		{
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarApplicationMenuItemBase.GetType());
				xmlSerializer.Serialize(writer, ribbonBarApplicationMenuItemBase);
			}
		}

		// Token: 0x040026B8 RID: 9912
		private IRenderer _renderer;

		// Token: 0x040026B9 RID: 9913
		private RibbonBarApplicationMenuItemBaseCollection _items;

		// Token: 0x040026BA RID: 9914
		private RibbonBarApplicationMenuFooterPane _footerPane;

		// Token: 0x040026BB RID: 9915
		private RibbonBarApplicationMenuAuxiliaryPane _auxiliaryPane;

		// Token: 0x040026BC RID: 9916
		private bool _allowRender;

		// Token: 0x040026BD RID: 9917
		private string _skinToRender;
	}
}
