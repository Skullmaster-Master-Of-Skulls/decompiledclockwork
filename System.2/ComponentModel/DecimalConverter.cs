using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200053A RID: 1338
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000E2360 File Offset: 0x000E0560
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000E2363 File Offset: 0x000E0563
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000E236F File Offset: 0x000E056F
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000E2390 File Offset: 0x000E0590
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is decimal))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			object[] arguments = new object[]
			{
				decimal.GetBits((decimal)value)
			};
			MemberInfo constructor = typeof(decimal).GetConstructor(new Type[]
			{
				typeof(int[])
			});
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, arguments);
			}
			return null;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000E2423 File Offset: 0x000E0623
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000E2435 File Offset: 0x000E0635
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000E2448 File Offset: 0x000E0648
		internal override object FromString(string value, CultureInfo culture)
		{
			return decimal.Parse(value, culture);
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000E2458 File Offset: 0x000E0658
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}
	}
}
