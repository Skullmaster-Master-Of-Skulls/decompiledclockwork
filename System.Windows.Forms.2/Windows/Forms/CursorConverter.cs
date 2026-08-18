using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000178 RID: 376
	public class CursorConverter : TypeConverter
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x00043687 File Offset: 0x00041887
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(byte[]) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x000436B7 File Offset: 0x000418B7
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000436E8 File Offset: 0x000418E8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string b = ((string)value).Trim();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					if (string.Equals(propertyInfo.Name, b, StringComparison.OrdinalIgnoreCase))
					{
						object[] index = null;
						return propertyInfo.GetValue(null, index);
					}
				}
			}
			if (value is byte[])
			{
				MemoryStream stream = new MemoryStream((byte[])value);
				return new Cursor(stream);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00043764 File Offset: 0x00041964
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				PropertyInfo[] properties = this.GetProperties();
				int num = -1;
				for (int i = 0; i < properties.Length; i++)
				{
					PropertyInfo propertyInfo = properties[i];
					object[] index = null;
					Cursor cursor = (Cursor)propertyInfo.GetValue(null, index);
					if (cursor == (Cursor)value)
					{
						if (cursor == value)
						{
							return propertyInfo.Name;
						}
						num = i;
					}
				}
				if (num != -1)
				{
					return properties[num].Name;
				}
				throw new FormatException(SR.GetString("CursorCannotCovertToString"));
			}
			else
			{
				if (destinationType == typeof(InstanceDescriptor) && value is Cursor)
				{
					PropertyInfo[] properties2 = this.GetProperties();
					foreach (PropertyInfo propertyInfo2 in properties2)
					{
						if (propertyInfo2.GetValue(null, null) == value)
						{
							return new InstanceDescriptor(propertyInfo2, null);
						}
					}
				}
				if (!(destinationType == typeof(byte[])))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					Cursor cursor2 = (Cursor)value;
					cursor2.SavePicture(memoryStream);
					memoryStream.Close();
					return memoryStream.ToArray();
				}
				return new byte[0];
			}
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000438A9 File Offset: 0x00041AA9
		private PropertyInfo[] GetProperties()
		{
			return typeof(Cursors).GetProperties(BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000438BC File Offset: 0x00041ABC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					object[] index = null;
					arrayList.Add(propertyInfo.GetValue(null, index));
				}
				this.values = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
			}
			return this.values;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400095C RID: 2396
		private TypeConverter.StandardValuesCollection values;
	}
}
