using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004DF RID: 1247
	public class SubMenuStyle : Style, ICustomTypeDescriptor
	{
		// Token: 0x06003E40 RID: 15936 RVA: 0x000B75ED File Offset: 0x000B57ED
		public SubMenuStyle()
		{
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000B75F5 File Offset: 0x000B57F5
		public SubMenuStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x000B75FE File Offset: 0x000B57FE
		// (set) Token: 0x06003E43 RID: 15939 RVA: 0x000C8D64 File Offset: 0x000C6F64
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("SubMenuStyle_HorizontalPadding")]
		public Unit HorizontalPadding
		{
			get
			{
				if (base.IsSet(131072))
				{
					return (Unit)base.ViewState["HorizontalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["HorizontalPadding"] = value;
				this.SetBit(131072);
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000B7719 File Offset: 0x000B5919
		// (set) Token: 0x06003E45 RID: 15941 RVA: 0x000C8DBC File Offset: 0x000C6FBC
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("SubMenuStyle_VerticalPadding")]
		public Unit VerticalPadding
		{
			get
			{
				if (base.IsSet(65536))
				{
					return (Unit)base.ViewState["VerticalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["VerticalPadding"] = value;
				this.SetBit(65536);
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000C8E14 File Offset: 0x000C7014
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				SubMenuStyle subMenuStyle = s as SubMenuStyle;
				if (subMenuStyle != null && !subMenuStyle.IsEmpty)
				{
					if (s.RegisteredCssClass.Length != 0)
					{
						if (subMenuStyle.IsSet(65536))
						{
							base.ViewState.Remove("VerticalPadding");
							base.ClearBit(65536);
						}
						if (subMenuStyle.IsSet(131072))
						{
							base.ViewState.Remove("HorizontalPadding");
							base.ClearBit(131072);
							return;
						}
					}
					else
					{
						if (subMenuStyle.IsSet(65536))
						{
							this.VerticalPadding = subMenuStyle.VerticalPadding;
						}
						if (subMenuStyle.IsSet(131072))
						{
							this.HorizontalPadding = subMenuStyle.HorizontalPadding;
						}
					}
				}
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000C8ED8 File Offset: 0x000C70D8
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			StateBag viewState = base.ViewState;
			if (base.IsSet(8))
			{
				Color c = (Color)viewState["BackColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(c));
				}
			}
			if (base.IsSet(16))
			{
				Color c = (Color)viewState["BorderColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(c));
				}
			}
			BorderStyle borderStyle = base.BorderStyle;
			Unit borderWidth = base.BorderWidth;
			if (!borderWidth.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.BorderWidth, borderWidth.ToString(CultureInfo.InvariantCulture));
				if (borderStyle == BorderStyle.NotSet)
				{
					if (borderWidth.Value != 0.0)
					{
						attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
					}
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, Style.borderStyles[(int)borderStyle]);
				}
			}
			else if (borderStyle != BorderStyle.NotSet)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, Style.borderStyles[(int)borderStyle]);
			}
			if (base.IsSet(128))
			{
				Unit unit = (Unit)viewState["Height"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Height, unit.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (base.IsSet(256))
			{
				Unit unit = (Unit)viewState["Width"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Width, unit.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (!this.HorizontalPadding.IsEmpty || !this.VerticalPadding.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.Padding, string.Format(CultureInfo.InvariantCulture, "{0} {1} {0} {1}", new object[]
				{
					this.VerticalPadding.IsEmpty ? Unit.Pixel(0) : this.VerticalPadding,
					this.HorizontalPadding.IsEmpty ? Unit.Pixel(0) : this.HorizontalPadding
				}));
			}
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000C90C0 File Offset: 0x000C72C0
		public override void MergeWith(Style s)
		{
			if (s != null)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				SubMenuStyle subMenuStyle = s as SubMenuStyle;
				if (subMenuStyle != null && !subMenuStyle.IsEmpty && s.RegisteredCssClass.Length == 0)
				{
					if (subMenuStyle.IsSet(65536) && !base.IsSet(65536))
					{
						this.VerticalPadding = subMenuStyle.VerticalPadding;
					}
					if (subMenuStyle.IsSet(131072) && !base.IsSet(131072))
					{
						this.HorizontalPadding = subMenuStyle.HorizontalPadding;
					}
				}
			}
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x000C9158 File Offset: 0x000C7358
		public override void Reset()
		{
			if (base.IsSet(65536))
			{
				base.ViewState.Remove("VerticalPadding");
			}
			if (base.IsSet(131072))
			{
				base.ViewState.Remove("HorizontalPadding");
			}
			base.Reset();
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x0009ED22 File Offset: 0x0009CF22
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x0009ED2B File Offset: 0x0009CF2B
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x0009ED34 File Offset: 0x0009CF34
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x0009ED3D File Offset: 0x0009CF3D
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0009ED46 File Offset: 0x0009CF46
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x0009ED4F File Offset: 0x0009CF4F
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x0009ED58 File Offset: 0x0009CF58
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x0009ED62 File Offset: 0x0009CF62
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x0009ED6B File Offset: 0x0009CF6B
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x0009ED75 File Offset: 0x0009CF75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000C91A8 File Offset: 0x000C73A8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.GetType(), attributes);
			PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
			PropertyDescriptor propertyDescriptor = properties["Font"];
			PropertyDescriptor propertyDescriptor2 = properties["ForeColor"];
			Attribute[] attributes2 = new Attribute[]
			{
				new BrowsableAttribute(false),
				new EditorBrowsableAttribute(EditorBrowsableState.Never),
				new ThemeableAttribute(false)
			};
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor3 = properties[i];
				if (propertyDescriptor3 == propertyDescriptor || propertyDescriptor3 == propertyDescriptor2)
				{
					array[i] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor3, attributes2);
				}
				else
				{
					array[i] = propertyDescriptor3;
				}
			}
			return new PropertyDescriptorCollection(array, true);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00004335 File Offset: 0x00002535
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04002408 RID: 9224
		private const int PROP_VPADDING = 65536;

		// Token: 0x04002409 RID: 9225
		private const int PROP_HPADDING = 131072;
	}
}
