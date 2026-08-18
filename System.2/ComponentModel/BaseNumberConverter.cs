using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000519 RID: 1305
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class BaseNumberConverter : TypeConverter
	{
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000DF9CA File Offset: 0x000DDBCA
		internal virtual bool AllowHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003177 RID: 12663
		internal abstract Type TargetType { get; }

		// Token: 0x06003178 RID: 12664
		internal abstract object FromString(string value, int radix);

		// Token: 0x06003179 RID: 12665
		internal abstract object FromString(string value, NumberFormatInfo formatInfo);

		// Token: 0x0600317A RID: 12666
		internal abstract object FromString(string value, CultureInfo culture);

		// Token: 0x0600317B RID: 12667 RVA: 0x000DF9CD File Offset: 0x000DDBCD
		internal virtual Exception FromStringError(string failedText, Exception innerException)
		{
			return new Exception(SR.GetString("ConvertInvalidPrimitive", new object[]
			{
				failedText,
				this.TargetType.Name
			}), innerException);
		}

		// Token: 0x0600317C RID: 12668
		internal abstract string ToString(object value, NumberFormatInfo formatInfo);

		// Token: 0x0600317D RID: 12669 RVA: 0x000DF9F7 File Offset: 0x000DDBF7
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000DFA18 File Offset: 0x000DDC18
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				try
				{
					if (this.AllowHex && text[0] == '#')
					{
						return this.FromString(text.Substring(1), 16);
					}
					if ((this.AllowHex && text.StartsWith("0x")) || text.StartsWith("0X") || text.StartsWith("&h") || text.StartsWith("&H"))
					{
						return this.FromString(text.Substring(2), 16);
					}
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					NumberFormatInfo formatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
					return this.FromString(text, formatInfo);
				}
				catch (Exception innerException)
				{
					throw this.FromStringError(text, innerException);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000DFB04 File Offset: 0x000DDD04
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && this.TargetType.IsInstanceOfType(value))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				NumberFormatInfo formatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
				return this.ToString(value, formatInfo);
			}
			if (destinationType.IsPrimitive)
			{
				return Convert.ChangeType(value, destinationType, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000DFB91 File Offset: 0x000DDD91
		public override bool CanConvertTo(ITypeDescriptorContext context, Type t)
		{
			return base.CanConvertTo(context, t) || t.IsPrimitive;
		}
	}
}
