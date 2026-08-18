using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000538 RID: 1336
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DateTimeConverter : TypeConverter
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x000E1C69 File Offset: 0x000DFE69
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000E1C87 File Offset: 0x000DFE87
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000E1CA8 File Offset: 0x000DFEA8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return DateTime.MinValue;
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
						return DateTime.Parse(text, dateTimeFormatInfo);
					}
					return DateTime.Parse(text, culture);
				}
				catch (FormatException innerException)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						"DateTime"
					}), innerException);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000E1D60 File Offset: 0x000DFF60
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)) || !(value is DateTime))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					if (dateTime.Ticks == 0L)
					{
						ConstructorInfo constructor = typeof(DateTime).GetConstructor(new Type[]
						{
							typeof(long)
						});
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[]
							{
								dateTime.Ticks
							});
						}
					}
					ConstructorInfo constructor2 = typeof(DateTime).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[]
						{
							dateTime.Year,
							dateTime.Month,
							dateTime.Day,
							dateTime.Hour,
							dateTime.Minute,
							dateTime.Second,
							dateTime.Millisecond
						});
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTime d = (DateTime)value;
			if (d == DateTime.MinValue)
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
				if (d.TimeOfDay.TotalSeconds == 0.0)
				{
					format = dateTimeFormatInfo.ShortDatePattern;
				}
				else
				{
					format = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern;
				}
				return d.ToString(format, CultureInfo.CurrentCulture);
			}
			if (d.TimeOfDay.TotalSeconds == 0.0)
			{
				return d.ToString("yyyy-MM-dd", culture);
			}
			return d.ToString(culture);
		}
	}
}
