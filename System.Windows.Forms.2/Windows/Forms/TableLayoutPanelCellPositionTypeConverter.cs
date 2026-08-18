using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000392 RID: 914
	internal class TableLayoutPanelCellPositionTypeConverter : TypeConverter
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000C24B8 File Offset: 0x000C06B8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x00106240 File Offset: 0x00104440
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = ((string)value).Trim();
			if (text.Length == 0)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			char c = culture.TextInfo.ListSeparator[0];
			string[] array = text.Split(new char[]
			{
				c
			});
			int[] array2 = new int[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (int)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length == 2)
			{
				return new TableLayoutPanelCellPosition(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
			{
				text,
				"column, row"
			}));
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00106320 File Offset: 0x00104520
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is TableLayoutPanelCellPosition)
			{
				TableLayoutPanelCellPosition tableLayoutPanelCellPosition = (TableLayoutPanelCellPosition)value;
				return new InstanceDescriptor(typeof(TableLayoutPanelCellPosition).GetConstructor(new Type[]
				{
					typeof(int),
					typeof(int)
				}), new object[]
				{
					tableLayoutPanelCellPosition.Column,
					tableLayoutPanelCellPosition.Row
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x001063C8 File Offset: 0x001045C8
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new TableLayoutPanelCellPosition((int)propertyValues["Column"], (int)propertyValues["Row"]);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x001063F4 File Offset: 0x001045F4
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TableLayoutPanelCellPosition), attributes);
			return properties.Sort(new string[]
			{
				"Column",
				"Row"
			});
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
