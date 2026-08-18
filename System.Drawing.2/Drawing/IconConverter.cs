using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;

namespace System.Drawing
{
	// Token: 0x0200001F RID: 31
	public class IconConverter : ExpandableObjectConverter
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000E9A2 File Offset: 0x0000CBA2
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(byte[]) || (!(sourceType == typeof(InstanceDescriptor)) && base.CanConvertFrom(context, sourceType));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(Image) || destinationType == typeof(Bitmap) || destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is byte[])
			{
				MemoryStream stream = new MemoryStream((byte[])value);
				return new Icon(stream);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(Image) || destinationType == typeof(Bitmap))
			{
				Icon icon = value as Icon;
				if (icon != null)
				{
					return icon.ToBitmap();
				}
			}
			if (destinationType == typeof(string))
			{
				if (value != null)
				{
					return value.ToString();
				}
				return SR.GetString("toStringNone");
			}
			else
			{
				if (!(destinationType == typeof(byte[])))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value == null)
				{
					return new byte[0];
				}
				MemoryStream memoryStream = null;
				try
				{
					memoryStream = new MemoryStream();
					Icon icon2 = value as Icon;
					if (icon2 != null)
					{
						icon2.Save(memoryStream);
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
				}
				if (memoryStream != null)
				{
					return memoryStream.ToArray();
				}
				return null;
			}
		}
	}
}
