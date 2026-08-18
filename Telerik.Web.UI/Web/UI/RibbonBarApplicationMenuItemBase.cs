using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020007C1 RID: 1985
	[ToolboxItem(false)]
	public abstract class RibbonBarApplicationMenuItemBase : WebControl, IRibbonBarCommandItem, IXmlSerializable
	{
		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x000DADD4 File Offset: 0x000D8FD4
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x000DADDC File Offset: 0x000D8FDC
		public RadRibbonBar RibbonBar { get; internal set; }

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x000DADE5 File Offset: 0x000D8FE5
		// (set) Token: 0x06004533 RID: 17715 RVA: 0x000DAE05 File Offset: 0x000D9005
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

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x000DAE18 File Offset: 0x000D9018
		// (set) Token: 0x06004535 RID: 17717 RVA: 0x000DAE38 File Offset: 0x000D9038
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

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x000DAE4B File Offset: 0x000D904B
		// (set) Token: 0x06004537 RID: 17719 RVA: 0x000DAE6B File Offset: 0x000D906B
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

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x000DAE7E File Offset: 0x000D907E
		// (set) Token: 0x06004539 RID: 17721 RVA: 0x000DAE9E File Offset: 0x000D909E
		[DefaultValue("")]
		[Description("Gets or sets the command name associated with the MenuItem that is passed to the Command event.")]
		[Category("Behavior")]
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

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x000DAEB1 File Offset: 0x000D90B1
		// (set) Token: 0x0600453B RID: 17723 RVA: 0x000DAED1 File Offset: 0x000D90D1
		[DefaultValue("")]
		[Description("Gets or sets an optional parameter passed to the Command event along with the associated CommandName.")]
		[Bindable(true)]
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

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x000DAEE4 File Offset: 0x000D90E4
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

		// Token: 0x0600453D RID: 17725 RVA: 0x000DAF00 File Offset: 0x000D9100
		protected virtual IRenderer CreateControlRenderer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000DAF08 File Offset: 0x000D9108
		internal void BaseAddAttributesToRender(HtmlTextWriter writer)
		{
			bool enabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Enabled = enabled;
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x000DAF31 File Offset: 0x000D9131
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000DAF3E File Offset: 0x000D913E
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x000DAF4C File Offset: 0x000D914C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x000DAF5A File Offset: 0x000D915A
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x000DAF66 File Offset: 0x000D9166
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000DAF6F File Offset: 0x000D916F
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000DAF78 File Offset: 0x000D9178
		public virtual void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000DAF88 File Offset: 0x000D9188
		protected virtual void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}

		// Token: 0x04001203 RID: 4611
		private IRenderer _renderer;
	}
}
