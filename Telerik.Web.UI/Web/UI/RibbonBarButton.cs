using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007CB RID: 1995
	[XmlRoot("Button")]
	public class RibbonBarButton : RibbonBarClickableItem, IXmlSerializable, IRibbonBarCommandItem
	{
		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x000DB5BC File Offset: 0x000D97BC
		// (set) Token: 0x06004593 RID: 17811 RVA: 0x000DB5DC File Offset: 0x000D97DC
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

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x000DB5EF File Offset: 0x000D97EF
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.Button;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06004595 RID: 17813 RVA: 0x000DB5F2 File Offset: 0x000D97F2
		// (set) Token: 0x06004596 RID: 17814 RVA: 0x000DB612 File Offset: 0x000D9812
		[DefaultValue("")]
		[Description("Gets or sets the command name associated with the Button that is passed to the Command event.")]
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

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x000DB625 File Offset: 0x000D9825
		// (set) Token: 0x06004598 RID: 17816 RVA: 0x000DB645 File Offset: 0x000D9845
		[Description("Gets or sets an optional parameter passed to the Command event along with the associated CommandName.")]
		[Category("Behavior")]
		[Bindable(true)]
		[DefaultValue("")]
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

		// Token: 0x06004599 RID: 17817 RVA: 0x000DB658 File Offset: 0x000D9858
		public RibbonBarButton()
		{
			this.ShouldRenderButtonStripClasses = false;
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x000DB667 File Offset: 0x000D9867
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarButtonLiteRenderer(this);
			}
			return new RibbonBarButtonClassicRenderer(this);
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x000DB684 File Offset: 0x000D9884
		// (set) Token: 0x0600459C RID: 17820 RVA: 0x000DB68C File Offset: 0x000D988C
		internal bool ShouldRenderButtonStripClasses { get; set; }

		// Token: 0x0600459D RID: 17821 RVA: 0x000DB695 File Offset: 0x000D9895
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x000DB6A1 File Offset: 0x000D98A1
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x000DB6AA File Offset: 0x000D98AA
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000DB6B3 File Offset: 0x000D98B3
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x000DB6C3 File Offset: 0x000D98C3
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}
	}
}
