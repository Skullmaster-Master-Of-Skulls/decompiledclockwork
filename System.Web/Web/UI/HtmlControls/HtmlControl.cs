using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020003FE RID: 1022
	[ToolboxItem(false)]
	[Designer("System.Web.UI.Design.HtmlIntrinsicControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HtmlControl : Control, IAttributeAccessor
	{
		// Token: 0x0600325C RID: 12892 RVA: 0x000DC871 File Offset: 0x000DB871
		protected HtmlControl() : this("span")
		{
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000DC87E File Offset: 0x000DB87E
		protected HtmlControl(string tag)
		{
			this._tagName = tag;
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000DC88D File Offset: 0x000DB88D
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

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000DC8AE File Offset: 0x000DB8AE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000DC8BB File Offset: 0x000DB8BB
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000DC8C4 File Offset: 0x000DB8C4
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x000DC8F2 File Offset: 0x000DB8F2
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000DC91E File Offset: 0x000DB91E
		protected override bool ViewStateIgnoresCase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000DC921 File Offset: 0x000DB921
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000DC929 File Offset: 0x000DB929
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000DC932 File Offset: 0x000DB932
		protected virtual void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.WriteAttribute("id", this.ClientID);
			}
			this.Attributes.Render(writer);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000DC959 File Offset: 0x000DB959
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag(this.TagName);
			this.RenderAttributes(writer);
			writer.Write('>');
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000DC976 File Offset: 0x000DB976
		string IAttributeAccessor.GetAttribute(string name)
		{
			return this.GetAttribute(name);
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000DC97F File Offset: 0x000DB97F
		protected virtual string GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000DC98D File Offset: 0x000DB98D
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.SetAttribute(name, value);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000DC997 File Offset: 0x000DB997
		protected virtual void SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000DC9A8 File Offset: 0x000DB9A8
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

		// Token: 0x0600326D RID: 12909 RVA: 0x000DCA20 File Offset: 0x000DBA20
		internal static string MapStringAttributeToString(string s)
		{
			if (s != null && s.Length == 0)
			{
				return null;
			}
			return s;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000DCA30 File Offset: 0x000DBA30
		internal static string MapIntegerAttributeToString(int n)
		{
			if (n == -1)
			{
				return null;
			}
			return n.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04002301 RID: 8961
		internal string _tagName;

		// Token: 0x04002302 RID: 8962
		private AttributeCollection _attributes;
	}
}
