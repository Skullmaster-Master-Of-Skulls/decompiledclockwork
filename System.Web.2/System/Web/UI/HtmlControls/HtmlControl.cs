using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000345 RID: 837
	[Designer("System.Web.UI.Design.HtmlIntrinsicControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	public abstract class HtmlControl : Control, IAttributeAccessor
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x0007E872 File Offset: 0x0007CA72
		protected HtmlControl() : this("span")
		{
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0007E87F File Offset: 0x0007CA7F
		protected HtmlControl(string tag)
		{
			this._tagName = tag;
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x0007E88E File Offset: 0x0007CA8E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new AttributeCollection(this.ViewState);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x0007E8AF File Offset: 0x0007CAAF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x0007E8BC File Offset: 0x0007CABC
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0007E8C4 File Offset: 0x0007CAC4
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x0007E8F2 File Offset: 0x0007CAF2
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		public bool Disabled
		{
			get
			{
				string text = this.Attributes["disabled"];
				return text != null && text.Equals("disabled");
			}
			set
			{
				if (value)
				{
					this.Attributes["disabled"] = "disabled";
					return;
				}
				this.Attributes["disabled"] = null;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool ViewStateIgnoresCase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0007E91E File Offset: 0x0007CB1E
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0007E927 File Offset: 0x0007CB27
		protected virtual void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.WriteAttribute("id", this.ClientID);
			}
			this.Attributes.Render(writer);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0007E94E File Offset: 0x0007CB4E
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write('>');
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0007E96B File Offset: 0x0007CB6B
		string IAttributeAccessor.GetAttribute(string name)
		{
			return this.GetAttribute(name);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0007E974 File Offset: 0x0007CB74
		protected virtual string GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x0007E982 File Offset: 0x0007CB82
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.SetAttribute(name, value);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0007E98C File Offset: 0x0007CB8C
		protected virtual void SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0007E99C File Offset: 0x0007CB9C
		internal void PreProcessRelativeReferenceAttribute(HtmlTextWriter writer, string attribName)
		{
			string text = this.Attributes[attribName];
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			try
			{
				text = base.ResolveClientUrl(text);
			}
			catch (Exception ex)
			{
				throw new HttpException(SR.GetString("Property_Had_Malformed_Url", new object[]
				{
					attribName,
					ex.Message
				}));
			}
			writer.WriteAttribute(attribName, text);
			this.Attributes.Remove(attribName);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0007EA14 File Offset: 0x0007CC14
		internal static string MapStringAttributeToString(string s)
		{
			if (s != null && s.Length == 0)
			{
				return null;
			}
			return s;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0007EA24 File Offset: 0x0007CC24
		internal static string MapIntegerAttributeToString(int n)
		{
			if (n == -1)
			{
				return null;
			}
			return n.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04001DBB RID: 7611
		internal string _tagName;

		// Token: 0x04001DBC RID: 7612
		private AttributeCollection _attributes;
	}
}
