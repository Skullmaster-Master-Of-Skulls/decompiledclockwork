using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000640 RID: 1600
	internal class MessageVersionConverter : TypeConverter
	{
		// Token: 0x06003D92 RID: 15762 RVA: 0x000EB155 File Offset: 0x000E9355
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000EB173 File Offset: 0x000E9373
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000EB194 File Offset: 0x000E9394
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2190444292U)
				{
					if (num <= 810547195U)
					{
						if (num != 433860734U)
						{
							if (num == 810547195U)
							{
								if (text == "None")
								{
									return MessageVersion.None;
								}
							}
						}
						else if (text == "Default")
						{
							return MessageVersion.Default;
						}
					}
					else if (num != 1442462253U)
					{
						if (num == 2190444292U)
						{
							if (text == "Soap12WSAddressingAugust2004")
							{
								return MessageVersion.Soap12WSAddressingAugust2004;
							}
						}
					}
					else if (text == "Soap11WSAddressingAugust2004")
					{
						return MessageVersion.Soap11WSAddressingAugust2004;
					}
				}
				else if (num <= 2402661685U)
				{
					if (num != 2352328828U)
					{
						if (num == 2402661685U)
						{
							if (text == "Soap12")
							{
								return MessageVersion.Soap12;
							}
						}
					}
					else if (text == "Soap11")
					{
						return MessageVersion.Soap11;
					}
				}
				else if (num != 3593027163U)
				{
					if (num == 4217126754U)
					{
						if (text == "Soap12WSAddressing10")
						{
							return MessageVersion.Soap12WSAddressing10;
						}
					}
				}
				else if (text == "Soap11WSAddressing10")
				{
					return MessageVersion.Soap11WSAddressing10;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassFactoryValue", new object[]
				{
					text,
					typeof(MessageVersion).FullName
				})));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000EB338 File Offset: 0x000E9538
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is MessageVersion)
			{
				MessageVersion messageVersion = (MessageVersion)value;
				string result;
				if (messageVersion == MessageVersion.Default)
				{
					result = "Default";
				}
				else if (messageVersion == MessageVersion.Soap11WSAddressing10)
				{
					result = "Soap11WSAddressing10";
				}
				else if (messageVersion == MessageVersion.Soap12WSAddressing10)
				{
					result = "Soap12WSAddressing10";
				}
				else if (messageVersion == MessageVersion.Soap11WSAddressingAugust2004)
				{
					result = "Soap11WSAddressingAugust2004";
				}
				else if (messageVersion == MessageVersion.Soap12WSAddressingAugust2004)
				{
					result = "Soap12WSAddressingAugust2004";
				}
				else if (messageVersion == MessageVersion.Soap11)
				{
					result = "Soap11";
				}
				else if (messageVersion == MessageVersion.Soap12)
				{
					result = "Soap12";
				}
				else
				{
					if (messageVersion != MessageVersion.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassInstanceValue", new object[]
						{
							typeof(MessageVersion).FullName
						})));
					}
					result = "None";
				}
				return result;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
