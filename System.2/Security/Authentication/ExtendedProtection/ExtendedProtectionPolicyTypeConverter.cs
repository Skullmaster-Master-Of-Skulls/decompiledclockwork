using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x02000443 RID: 1091
	public class ExtendedProtectionPolicyTypeConverter : TypeConverter
	{
		// Token: 0x0600289A RID: 10394 RVA: 0x000BA665 File Offset: 0x000B8865
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000BA684 File Offset: 0x000B8884
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				ExtendedProtectionPolicy extendedProtectionPolicy = value as ExtendedProtectionPolicy;
				if (extendedProtectionPolicy != null)
				{
					Type[] types;
					object[] arguments;
					if (extendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.Never)
					{
						types = new Type[]
						{
							typeof(PolicyEnforcement)
						};
						arguments = new object[]
						{
							PolicyEnforcement.Never
						};
					}
					else
					{
						types = new Type[]
						{
							typeof(PolicyEnforcement),
							typeof(ProtectionScenario),
							typeof(ICollection)
						};
						object[] array = null;
						if (extendedProtectionPolicy.CustomServiceNames != null && extendedProtectionPolicy.CustomServiceNames.Count > 0)
						{
							array = new object[extendedProtectionPolicy.CustomServiceNames.Count];
							((ICollection)extendedProtectionPolicy.CustomServiceNames).CopyTo(array, 0);
						}
						arguments = new object[]
						{
							extendedProtectionPolicy.PolicyEnforcement,
							extendedProtectionPolicy.ProtectionScenario,
							array
						};
					}
					ConstructorInfo constructor = typeof(ExtendedProtectionPolicy).GetConstructor(types);
					return new InstanceDescriptor(constructor, arguments);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
