using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E5C RID: 3676
	[XmlRoot("NumericTextBox")]
	public class RibbonBarNumericTextBox : RibbonBarItem, IXmlSerializable
	{
		// Token: 0x17002C11 RID: 11281
		// (get) Token: 0x06008B6D RID: 35693 RVA: 0x001FB9F0 File Offset: 0x001F9BF0
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x17002C12 RID: 11282
		// (get) Token: 0x06008B6E RID: 35694 RVA: 0x001FB9F4 File Offset: 0x001F9BF4
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.NumericTextBox;
			}
		}

		// Token: 0x06008B6F RID: 35695 RVA: 0x001FB9F8 File Offset: 0x001F9BF8
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarNumericTextBoxLiteRenderer(this);
			}
			return new RibbonBarNumericTextBoxClassicRenderer(this);
		}

		// Token: 0x06008B70 RID: 35696 RVA: 0x001FBA15 File Offset: 0x001F9C15
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06008B71 RID: 35697 RVA: 0x001FBA23 File Offset: 0x001F9C23
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.Renderer.RenderContents(writer);
		}

		// Token: 0x17002C13 RID: 11283
		// (get) Token: 0x06008B72 RID: 35698 RVA: 0x001FBA34 File Offset: 0x001F9C34
		// (set) Token: 0x06008B73 RID: 35699 RVA: 0x001FBA70 File Offset: 0x001F9C70
		[Description("The value.")]
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(double?), "")]
		public virtual double? Value
		{
			get
			{
				if (string.IsNullOrEmpty(this.Text))
				{
					return null;
				}
				return new double?(double.Parse(this.Text, NumberFormatInfo.InvariantInfo));
			}
			set
			{
				if (value != null)
				{
					this.Text = value.Value.ToString(NumberFormatInfo.InvariantInfo);
					return;
				}
				this.Text = null;
			}
		}

		// Token: 0x17002C14 RID: 11284
		// (get) Token: 0x06008B74 RID: 35700 RVA: 0x001FBAA8 File Offset: 0x001F9CA8
		// (set) Token: 0x06008B75 RID: 35701 RVA: 0x001FBAD5 File Offset: 0x001F9CD5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002C15 RID: 11285
		// (get) Token: 0x06008B76 RID: 35702 RVA: 0x001FBAE8 File Offset: 0x001F9CE8
		// (set) Token: 0x06008B77 RID: 35703 RVA: 0x001FBB19 File Offset: 0x001F9D19
		public double Step
		{
			get
			{
				object obj = this.ViewState["Step"];
				if (obj != null)
				{
					return (double)obj;
				}
				return 1.0;
			}
			set
			{
				this.ViewState["Step"] = value;
			}
		}

		// Token: 0x17002C16 RID: 11286
		// (get) Token: 0x06008B78 RID: 35704 RVA: 0x001FBB31 File Offset: 0x001F9D31
		// (set) Token: 0x06008B79 RID: 35705 RVA: 0x001FBB51 File Offset: 0x001F9D51
		[ClientPersistedProperty]
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return (string)(this.ViewState["Prefix"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Prefix"] = value;
			}
		}

		// Token: 0x17002C17 RID: 11287
		// (get) Token: 0x06008B7A RID: 35706 RVA: 0x001FBB64 File Offset: 0x001F9D64
		// (set) Token: 0x06008B7B RID: 35707 RVA: 0x001FBB84 File Offset: 0x001F9D84
		[DefaultValue("")]
		[ClientPersistedProperty]
		public string Suffix
		{
			get
			{
				return (string)(this.ViewState["Suffix"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Suffix"] = value;
			}
		}

		// Token: 0x06008B7C RID: 35708 RVA: 0x001FBB97 File Offset: 0x001F9D97
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008B7D RID: 35709 RVA: 0x001FBBA3 File Offset: 0x001F9DA3
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008B7E RID: 35710 RVA: 0x001FBBAC File Offset: 0x001F9DAC
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008B7F RID: 35711 RVA: 0x001FBBB5 File Offset: 0x001F9DB5
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
		}

		// Token: 0x06008B80 RID: 35712 RVA: 0x001FBBC5 File Offset: 0x001F9DC5
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}
	}
}
