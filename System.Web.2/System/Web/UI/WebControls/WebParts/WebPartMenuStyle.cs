using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A7 RID: 1447
	public sealed class WebPartMenuStyle : TableStyle, ICustomTypeDescriptor
	{
		// Token: 0x0600493F RID: 18751 RVA: 0x000F3BC4 File Offset: 0x000F1DC4
		public WebPartMenuStyle() : this(null)
		{
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x000F3BCD File Offset: 0x000F1DCD
		public WebPartMenuStyle(StateBag bag) : base(bag)
		{
			this.CellPadding = 1;
			this.CellSpacing = 0;
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x000F3BE4 File Offset: 0x000F1DE4
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x000F3C0E File Offset: 0x000F1E0E
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebSysDescription("WebPartMenuStyle_ShadowColor")]
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

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x000F3C31 File Offset: 0x000F1E31
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x00006164 File Offset: 0x00004364
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

		// Token: 0x06004945 RID: 18757 RVA: 0x000F3C3C File Offset: 0x000F1E3C
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

		// Token: 0x06004946 RID: 18758 RVA: 0x000F3C84 File Offset: 0x000F1E84
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

		// Token: 0x06004947 RID: 18759 RVA: 0x000F3D04 File Offset: 0x000F1F04
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

		// Token: 0x06004948 RID: 18760 RVA: 0x000F3D75 File Offset: 0x000F1F75
		public override void Reset()
		{
			if (base.IsSet(2097152))
			{
				base.ViewState.Remove("ShadowColor");
			}
			base.Reset();
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x0009ED22 File Offset: 0x0009CF22
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0009ED2B File Offset: 0x0009CF2B
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x0009ED34 File Offset: 0x0009CF34
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x0009ED3D File Offset: 0x0009CF3D
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x0009ED46 File Offset: 0x0009CF46
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x0009ED4F File Offset: 0x0009CF4F
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0009ED58 File Offset: 0x0009CF58
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0009ED62 File Offset: 0x0009CF62
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0009ED6B File Offset: 0x0009CF6B
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0009ED75 File Offset: 0x0009CF75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x000F3D9C File Offset: 0x000F1F9C
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

		// Token: 0x06004954 RID: 18772 RVA: 0x00004335 File Offset: 0x00002535
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0400279A RID: 10138
		private const int PROP_SHADOWCOLOR = 2097152;
	}
}
