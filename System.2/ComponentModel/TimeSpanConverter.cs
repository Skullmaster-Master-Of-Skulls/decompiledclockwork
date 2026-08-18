using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005AF RID: 1455
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class TimeSpanConverter : TypeConverter
	{
		// Token: 0x06003634 RID: 13876 RVA: 0x000EC920 File Offset: 0x000EAB20
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000EC93E File Offset: 0x000EAB3E
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000EC95C File Offset: 0x000EAB5C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string input = ((string)value).Trim();
				try
				{
					return TimeSpan.Parse(input, culture);
				}
				catch (FormatException innerException)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						"TimeSpan"
					}), innerException);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000EC9D0 File Offset: 0x000EABD0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is TimeSpan)
			{
				MethodInfo method = typeof(TimeSpan).GetMethod("Parse", new Type[]
				{
					typeof(string)
				});
				if (method != null)
				{
					return new InstanceDescriptor(method, new object[]
					{
						value.ToString()
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
