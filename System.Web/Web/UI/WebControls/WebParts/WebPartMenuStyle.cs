using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200073A RID: 1850
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartMenuStyle : TableStyle, ICustomTypeDescriptor
	{
		// Token: 0x060059CD RID: 22989 RVA: 0x0016B068 File Offset: 0x0016A068
		public WebPartMenuStyle() : this(null)
		{
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x0016B071 File Offset: 0x0016A071
		public WebPartMenuStyle(StateBag bag) : base(bag)
		{
			this.CellPadding = 1;
			this.CellSpacing = 0;
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x0016B088 File Offset: 0x0016A088
		// (set) Token: 0x060059D0 RID: 22992 RVA: 0x0016B0B2 File Offset: 0x0016A0B2
		[WebSysDescription("WebPartMenuStyle_ShadowColor")]
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebCategory("Appearance")]
		public Color ShadowColor
		{
			get
			{
				if (base.IsSet(2097152))
				{
					return (Color)base.ViewState["ShadowColor"];
				}
				return Color.Empty;
			}
			set
			{
				base.ViewState["ShadowColor"] = value;
				this.SetBit(2097152);
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x0016B0D5 File Offset: 0x0016A0D5
		// (set) Token: 0x060059D2 RID: 22994 RVA: 0x0016B0DD File Offset: 0x0016A0DD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override HorizontalAlign HorizontalAlign
		{
			get
			{
				return base.HorizontalAlign;
			}
			set
			{
			}
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x0016B0E0 File Offset: 0x0016A0E0
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			base.FillStyleAttributes(attributes, urlResolver);
			Color shadowColor = this.ShadowColor;
			if (!shadowColor.IsEmpty)
			{
				string str = ColorTranslator.ToHtml(shadowColor);
				string value = "progid:DXImageTransform.Microsoft.Shadow(color='" + str + "', Direction=135, Strength=3)";
				attributes.Add(HtmlTextWriterStyle.Filter, value);
			}
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x0016B128 File Offset: 0x0016A128
		public override void CopyFrom(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				base.CopyFrom(s);
				if (s is WebPartMenuStyle)
				{
					WebPartMenuStyle webPartMenuStyle = (WebPartMenuStyle)s;
					if (s.RegisteredCssClass.Length != 0)
					{
						if (webPartMenuStyle.IsSet(2097152))
						{
							base.ViewState.Remove("ShadowColor");
							base.ClearBit(2097152);
							return;
						}
					}
					else if (webPartMenuStyle.IsSet(2097152))
					{
						this.ShadowColor = webPartMenuStyle.ShadowColor;
					}
				}
			}
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x0016B1A8 File Offset: 0x0016A1A8
		public override void MergeWith(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				if (s is WebPartMenuStyle)
				{
					WebPartMenuStyle webPartMenuStyle = (WebPartMenuStyle)s;
					if (s.RegisteredCssClass.Length == 0 && webPartMenuStyle.IsSet(2097152) && !base.IsSet(2097152))
					{
						this.ShadowColor = webPartMenuStyle.ShadowColor;
					}
				}
			}
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x0016B219 File Offset: 0x0016A219
		public override void Reset()
		{
			if (base.IsSet(2097152))
			{
				base.ViewState.Remove("ShadowColor");
			}
			base.Reset();
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x0016B23E File Offset: 0x0016A23E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x0016B247 File Offset: 0x0016A247
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0016B250 File Offset: 0x0016A250
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0016B259 File Offset: 0x0016A259
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0016B262 File Offset: 0x0016A262
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0016B26B File Offset: 0x0016A26B
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x0016B274 File Offset: 0x0016A274
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x0016B27E File Offset: 0x0016A27E
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x0016B287 File Offset: 0x0016A287
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x0016B291 File Offset: 0x0016A291
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0016B29C File Offset: 0x0016A29C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.GetType(), attributes);
			PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
			PropertyDescriptor propertyDescriptor = properties["CellPadding"];
			PropertyDescriptor propertyDescriptor2 = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new DefaultValueAttribute(1)
			});
			PropertyDescriptor propertyDescriptor3 = properties["CellSpacing"];
			PropertyDescriptor propertyDescriptor4 = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor3, new Attribute[]
			{
				new DefaultValueAttribute(0)
			});
			PropertyDescriptor propertyDescriptor5 = properties["Font"];
			PropertyDescriptor propertyDescriptor6 = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor5, new Attribute[]
			{
				new BrowsableAttribute(false),
				new ThemeableAttribute(false),
				new EditorBrowsableAttribute(EditorBrowsableState.Never)
			});
			PropertyDescriptor propertyDescriptor7 = properties["ForeColor"];
			PropertyDescriptor propertyDescriptor8 = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor7, new Attribute[]
			{
				new BrowsableAttribute(false),
				new ThemeableAttribute(false),
				new EditorBrowsableAttribute(EditorBrowsableState.Never)
			});
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor9 = properties[i];
				if (propertyDescriptor9 == propertyDescriptor)
				{
					array[i] = propertyDescriptor2;
				}
				else if (propertyDescriptor9 == propertyDescriptor3)
				{
					array[i] = propertyDescriptor4;
				}
				else if (propertyDescriptor9 == propertyDescriptor5)
				{
					array[i] = propertyDescriptor6;
				}
				else if (propertyDescriptor9 == propertyDescriptor7)
				{
					array[i] = propertyDescriptor8;
				}
				else
				{
					array[i] = propertyDescriptor9;
				}
			}
			return new PropertyDescriptorCollection(array, true);
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0016B409 File Offset: 0x0016A409
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04003066 RID: 12390
		private const int PROP_SHADOWCOLOR = 2097152;
	}
}
