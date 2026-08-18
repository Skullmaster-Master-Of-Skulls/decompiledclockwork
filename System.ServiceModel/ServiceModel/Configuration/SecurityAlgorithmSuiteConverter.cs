using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C3 RID: 1731
	internal class SecurityAlgorithmSuiteConverter : TypeConverter
	{
		// Token: 0x0600431E RID: 17182 RVA: 0x000FD6BB File Offset: 0x000FB8BB
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000FD6D9 File Offset: 0x000FB8D9
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000FD6F8 File Offset: 0x000FB8F8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1403417380U)
				{
					if (num <= 937905440U)
					{
						if (num <= 433860734U)
						{
							if (num != 131147335U)
							{
								if (num == 433860734U)
								{
									if (text == "Default")
									{
										return SecurityAlgorithmSuite.Default;
									}
								}
							}
							else if (text == "Basic256Sha256Rsa15")
							{
								return SecurityAlgorithmSuite.Basic256Sha256Rsa15;
							}
						}
						else if (num != 628094006U)
						{
							if (num == 937905440U)
							{
								if (text == "Basic192Sha256")
								{
									return SecurityAlgorithmSuite.Basic192Sha256;
								}
							}
						}
						else if (text == "Basic128Rsa15")
						{
							return SecurityAlgorithmSuite.Basic128Rsa15;
						}
					}
					else if (num <= 1241069385U)
					{
						if (num != 1189970636U)
						{
							if (num == 1241069385U)
							{
								if (text == "Basic128Sha256")
								{
									return SecurityAlgorithmSuite.Basic128Sha256;
								}
							}
						}
						else if (text == "Basic192Sha256Rsa15")
						{
							return SecurityAlgorithmSuite.Basic192Sha256Rsa15;
						}
					}
					else if (num != 1403237184U)
					{
						if (num == 1403417380U)
						{
							if (text == "TripleDesSha256")
							{
								return SecurityAlgorithmSuite.TripleDesSha256;
							}
						}
					}
					else if (text == "TripleDesSha256Rsa15")
					{
						return SecurityAlgorithmSuite.TripleDesSha256Rsa15;
					}
				}
				else if (num <= 2103522766U)
				{
					if (num <= 1945990914U)
					{
						if (num != 1816942825U)
						{
							if (num == 1945990914U)
							{
								if (text == "Basic256Rsa15")
								{
									return SecurityAlgorithmSuite.Basic256Rsa15;
								}
							}
						}
						else if (text == "TripleDesRsa15")
						{
							return SecurityAlgorithmSuite.TripleDesRsa15;
						}
					}
					else if (num != 2065838533U)
					{
						if (num == 2103522766U)
						{
							if (text == "Basic128")
							{
								return SecurityAlgorithmSuite.Basic128;
							}
						}
					}
					else if (text == "Basic256Sha256")
					{
						return SecurityAlgorithmSuite.Basic256Sha256;
					}
				}
				else if (num <= 3190594811U)
				{
					if (num != 2104655599U)
					{
						if (num == 3190594811U)
						{
							if (text == "Basic128Sha256Rsa15")
							{
								return SecurityAlgorithmSuite.Basic128Sha256Rsa15;
							}
						}
					}
					else if (text == "Basic192")
					{
						return SecurityAlgorithmSuite.Basic192;
					}
				}
				else if (num != 3440761322U)
				{
					if (num != 3529345877U)
					{
						if (num == 3652005491U)
						{
							if (text == "TripleDes")
							{
								return SecurityAlgorithmSuite.TripleDes;
							}
						}
					}
					else if (text == "Basic192Rsa15")
					{
						return SecurityAlgorithmSuite.Basic192Rsa15;
					}
				}
				else if (text == "Basic256")
				{
					return SecurityAlgorithmSuite.Basic256;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassFactoryValue", new object[]
				{
					text,
					typeof(SecurityAlgorithmSuite).FullName
				})));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000FDA80 File Offset: 0x000FBC80
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is SecurityAlgorithmSuite)
			{
				SecurityAlgorithmSuite securityAlgorithmSuite = (SecurityAlgorithmSuite)value;
				string result;
				if (securityAlgorithmSuite == SecurityAlgorithmSuite.Default)
				{
					result = "Default";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic256)
				{
					result = "Basic256";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic192)
				{
					result = "Basic192";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic128)
				{
					result = "Basic128";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.TripleDes)
				{
					result = "TripleDes";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic256Rsa15)
				{
					result = "Basic256Rsa15";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic192Rsa15)
				{
					result = "Basic192Rsa15";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic128Rsa15)
				{
					result = "Basic128Rsa15";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.TripleDesRsa15)
				{
					result = "TripleDesRsa15";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic256Sha256)
				{
					result = "Basic256Sha256";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic192Sha256)
				{
					result = "Basic192Sha256";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic128Sha256)
				{
					result = "Basic128Sha256";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.TripleDesSha256)
				{
					result = "TripleDesSha256";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic256Sha256Rsa15)
				{
					result = "Basic256Sha256Rsa15";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic192Sha256Rsa15)
				{
					result = "Basic192Sha256Rsa15";
				}
				else if (securityAlgorithmSuite == SecurityAlgorithmSuite.Basic128Sha256Rsa15)
				{
					result = "Basic128Sha256Rsa15";
				}
				else
				{
					if (securityAlgorithmSuite != SecurityAlgorithmSuite.TripleDesSha256Rsa15)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassInstanceValue", new object[]
						{
							typeof(SecurityAlgorithmSuite).FullName
						})));
					}
					result = "TripleDesSha256Rsa15";
				}
				return result;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
