using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000674 RID: 1652
	internal class PolicyVersionConverter : TypeConverter
	{
		// Token: 0x06003F5B RID: 16219 RVA: 0x000F074E File Offset: 0x000EE94E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000F076C File Offset: 0x000EE96C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x000F078C File Offset: 0x000EE98C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				PolicyVersion result;
				if (!(text == "Policy12"))
				{
					if (!(text == "Policy15"))
					{
						if (!(text == "Default"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassFactoryValue", new object[]
							{
								text,
								typeof(PolicyVersion).FullName
							})));
						}
						result = PolicyVersion.Default;
					}
					else
					{
						result = PolicyVersion.Policy15;
					}
				}
				else
				{
					result = PolicyVersion.Policy12;
				}
				return result;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000F0834 File Offset: 0x000EEA34
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is PolicyVersion)
			{
				PolicyVersion policyVersion = (PolicyVersion)value;
				string result;
				if (policyVersion == PolicyVersion.Default)
				{
					result = "Default";
				}
				else if (policyVersion == PolicyVersion.Policy12)
				{
					result = "Policy12";
				}
				else
				{
					if (policyVersion != PolicyVersion.Policy15)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassInstanceValue", new object[]
						{
							typeof(PolicyVersion).FullName
						})));
					}
					result = "Policy15";
				}
				return result;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
