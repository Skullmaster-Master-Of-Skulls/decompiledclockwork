using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000555 RID: 1365
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class GuidConverter : TypeConverter
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x000E3F9C File Offset: 0x000E219C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000E3FBA File Offset: 0x000E21BA
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000E3FD8 File Offset: 0x000E21D8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string g = ((string)value).Trim();
				return new Guid(g);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000E4010 File Offset: 0x000E2210
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Guid)
			{
				ConstructorInfo constructor = typeof(Guid).GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						value.ToString()
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
