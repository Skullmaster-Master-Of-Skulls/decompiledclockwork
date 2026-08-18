using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002C5 RID: 709
	public class LinkConverter : TypeConverter
	{
		// Token: 0x06002B43 RID: 11075 RVA: 0x000C24B8 File Offset: 0x000C06B8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000C24D6 File Offset: 0x000C06D6
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000C2508 File Offset: 0x000C0708
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
				return new LinkLabel.Link(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
			{
				text,
				"Start, Length"
			}));
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000C25E4 File Offset: 0x000C07E4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is LinkLabel.Link)
			{
				if (destinationType == typeof(string))
				{
					LinkLabel.Link link = (LinkLabel.Link)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[2];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, link.Start);
					array[num++] = converter.ConvertToString(context, culture, link.Length);
					return string.Join(separator, array);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					LinkLabel.Link link2 = (LinkLabel.Link)value;
					if (link2.LinkData == null)
					{
						MemberInfo constructor = typeof(LinkLabel.Link).GetConstructor(new Type[]
						{
							typeof(int),
							typeof(int)
						});
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[]
							{
								link2.Start,
								link2.Length
							}, true);
						}
					}
					else
					{
						MemberInfo constructor = typeof(LinkLabel.Link).GetConstructor(new Type[]
						{
							typeof(int),
							typeof(int),
							typeof(object)
						});
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[]
							{
								link2.Start,
								link2.Length,
								link2.LinkData
							}, true);
						}
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
