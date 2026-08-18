using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000539 RID: 1337
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DateTimeOffsetConverter : TypeConverter
	{
		// Token: 0x06003272 RID: 12914 RVA: 0x000E1FCE File Offset: 0x000E01CE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000E1FEC File Offset: 0x000E01EC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000E200C File Offset: 0x000E020C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return DateTimeOffset.MinValue;
				}
				try
				{
					DateTimeFormatInfo dateTimeFormatInfo = null;
					if (culture != null)
					{
						dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
					}
					if (dateTimeFormatInfo != null)
					{
						return DateTimeOffset.Parse(text, dateTimeFormatInfo);
					}
					return DateTimeOffset.Parse(text, culture);
				}
				catch (FormatException innerException)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						"DateTimeOffset"
					}), innerException);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000E20C4 File Offset: 0x000E02C4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)) || !(value is DateTimeOffset))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					if (dateTimeOffset.Ticks == 0L)
					{
						ConstructorInfo constructor = typeof(DateTimeOffset).GetConstructor(new Type[]
						{
							typeof(long)
						});
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[]
							{
								dateTimeOffset.Ticks
							});
						}
					}
					ConstructorInfo constructor2 = typeof(DateTimeOffset).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(TimeSpan)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[]
						{
							dateTimeOffset.Year,
							dateTimeOffset.Month,
							dateTimeOffset.Day,
							dateTimeOffset.Hour,
							dateTimeOffset.Minute,
							dateTimeOffset.Second,
							dateTimeOffset.Millisecond,
							dateTimeOffset.Offset
						});
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTimeOffset left = (DateTimeOffset)value;
			if (left == DateTimeOffset.MinValue)
			{
				return string.Empty;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
			if (culture != CultureInfo.InvariantCulture)
			{
				string format;
				if (left.TimeOfDay.TotalSeconds == 0.0)
				{
					format = dateTimeFormatInfo.ShortDatePattern + " zzz";
				}
				else
				{
					format = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern + " zzz";
				}
				return left.ToString(format, CultureInfo.CurrentCulture);
			}
			if (left.TimeOfDay.TotalSeconds == 0.0)
			{
				return left.ToString("yyyy-MM-dd zzz", culture);
			}
			return left.ToString(culture);
		}
	}
}
