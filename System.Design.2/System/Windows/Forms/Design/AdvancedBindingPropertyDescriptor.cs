using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000291 RID: 657
	internal class AdvancedBindingPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0008BF58 File Offset: 0x0008A158
		internal AdvancedBindingPropertyDescriptor() : base(SR.GetString("AdvancedBindingPropertyDescName"), null)
		{
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0008BF6B File Offset: 0x0008A16B
		public override Type ComponentType
		{
			get
			{
				return typeof(ControlBindingsCollection);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0008BF77 File Offset: 0x0008A177
		public override AttributeCollection Attributes
		{
			get
			{
				return new AttributeCollection(new Attribute[]
				{
					new SRDescriptionAttribute("AdvancedBindingPropertyDescriptorDesc"),
					NotifyParentPropertyAttribute.Yes,
					new MergablePropertyAttribute(false)
				});
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0008BFA2 File Offset: 0x0008A1A2
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0008BFAE File Offset: 0x0008A1AE
		public override TypeConverter Converter
		{
			get
			{
				if (AdvancedBindingPropertyDescriptor.advancedBindingTypeConverter == null)
				{
					AdvancedBindingPropertyDescriptor.advancedBindingTypeConverter = new AdvancedBindingPropertyDescriptor.AdvancedBindingTypeConverter();
				}
				return AdvancedBindingPropertyDescriptor.advancedBindingTypeConverter;
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0008BFC6 File Offset: 0x0008A1C6
		public override object GetEditor(Type type)
		{
			if (type == typeof(UITypeEditor))
			{
				return AdvancedBindingPropertyDescriptor.advancedBindingEditor;
			}
			return base.GetEditor(type);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0008BFE7 File Offset: 0x0008A1E7
		protected override void FillAttributes(IList attributeList)
		{
			attributeList.Add(RefreshPropertiesAttribute.All);
			base.FillAttributes(attributeList);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0001283F File Offset: 0x00010A3F
		public override object GetValue(object component)
		{
			return component;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00003937 File Offset: 0x00001B37
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00003937 File Offset: 0x00001B37
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x04001556 RID: 5462
		internal static AdvancedBindingEditor advancedBindingEditor = new AdvancedBindingEditor();

		// Token: 0x04001557 RID: 5463
		internal static AdvancedBindingPropertyDescriptor.AdvancedBindingTypeConverter advancedBindingTypeConverter = new AdvancedBindingPropertyDescriptor.AdvancedBindingTypeConverter();

		// Token: 0x0200051D RID: 1309
		internal class AdvancedBindingTypeConverter : TypeConverter
		{
			// Token: 0x06003000 RID: 12288 RVA: 0x00009BF5 File Offset: 0x00007DF5
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
