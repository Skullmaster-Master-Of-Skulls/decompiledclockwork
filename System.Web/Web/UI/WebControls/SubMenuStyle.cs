using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200061D RID: 1565
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SubMenuStyle : Style, ICustomTypeDescriptor
	{
		// Token: 0x06004DA9 RID: 19881 RVA: 0x0013B1F2 File Offset: 0x0013A1F2
		public SubMenuStyle()
		{
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x0013B1FA File Offset: 0x0013A1FA
		public SubMenuStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06004DAB RID: 19883 RVA: 0x0013B203 File Offset: 0x0013A203
		// (set) Token: 0x06004DAC RID: 19884 RVA: 0x0013B230 File Offset: 0x0013A230
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[WebSysDescription("SubMenuStyle_HorizontalPadding")]
		[WebCategory("Layout")]
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

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x0013B285 File Offset: 0x0013A285
		// (set) Token: 0x06004DAE RID: 19886 RVA: 0x0013B2B0 File Offset: 0x0013A2B0
		[WebSysDescription("SubMenuStyle_VerticalPadding")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
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

		// Token: 0x06004DAF RID: 19887 RVA: 0x0013B308 File Offset: 0x0013A308
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

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0013B3CC File Offset: 0x0013A3CC
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

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0013B5BC File Offset: 0x0013A5BC
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

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0013B654 File Offset: 0x0013A654
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

		// Token: 0x06004DB3 RID: 19891 RVA: 0x0013B6A1 File Offset: 0x0013A6A1
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x0013B6AA File Offset: 0x0013A6AA
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0013B6B3 File Offset: 0x0013A6B3
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x0013B6BC File Offset: 0x0013A6BC
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x0013B6C5 File Offset: 0x0013A6C5
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x0013B6CE File Offset: 0x0013A6CE
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x0013B6D7 File Offset: 0x0013A6D7
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x0013B6E1 File Offset: 0x0013A6E1
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x0013B6EA File Offset: 0x0013A6EA
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x0013B6F4 File Offset: 0x0013A6F4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x0013B700 File Offset: 0x0013A700
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

		// Token: 0x06004DBE RID: 19902 RVA: 0x0013B7B3 File Offset: 0x0013A7B3
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04002C6E RID: 11374
		private const int PROP_VPADDING = 65536;

		// Token: 0x04002C6F RID: 11375
		private const int PROP_HPADDING = 131072;
	}
}
