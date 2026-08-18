using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000658 RID: 1624
	public class ScaleConverter : TypeConverter
	{
		// Token: 0x06003B9D RID: 15261 RVA: 0x000C21E0 File Offset: 0x000C03E0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = (string)value;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			return new Scale
			{
				Text = text
			};
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000C2210 File Offset: 0x000C0410
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Scale)
			{
				if (destinationType == typeof(string))
				{
					value.ToString();
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Scale scale = (Scale)value;
					string text = scale.Text;
					double num;
					ConstructorInfo constructor;
					if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
					{
						constructor = typeof(Scale).GetConstructor(new Type[]
						{
							typeof(double)
						});
						return new InstanceDescriptor(constructor, new object[]
						{
							num
						});
					}
					if (text.Length > 1 && text.EndsWith("%"))
					{
						string s = text.Substring(0, text.LastIndexOf("%"));
						if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
						{
							constructor = typeof(Scale).GetConstructor(new Type[]
							{
								typeof(double)
							});
							return new InstanceDescriptor(constructor, new object[]
							{
								num / 100.0
							});
						}
					}
					constructor = typeof(Scale).GetConstructor(new Type[]
					{
						typeof(string)
					});
					return new InstanceDescriptor(constructor, new object[]
					{
						text
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000C23A1 File Offset: 0x000C05A1
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000C23BF File Offset: 0x000C05BF
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}
	}
}
