using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x02000713 RID: 1811
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MachineKeyValidationConverter : ConfigurationConverterBase
	{
		// Token: 0x0600571C RID: 22300 RVA: 0x00130460 File Offset: 0x0012E660
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (!(value is MachineKeyValidation))
			{
				throw new ArgumentException(SR.GetString("Config_Invalid_enum_value", new object[]
				{
					"SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512"
				}));
			}
			return MachineKeyValidationConverter.ConvertFromEnum((MachineKeyValidation)value);
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x00130493 File Offset: 0x0012E693
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return MachineKeyValidationConverter.ConvertToEnum((string)data);
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x001304A8 File Offset: 0x0012E6A8
		internal static string ConvertFromEnum(MachineKeyValidation enumValue)
		{
			switch (enumValue)
			{
			case MachineKeyValidation.MD5:
				return "MD5";
			case MachineKeyValidation.SHA1:
				return "SHA1";
			case MachineKeyValidation.TripleDES:
				return "3DES";
			case MachineKeyValidation.AES:
				return "AES";
			case MachineKeyValidation.HMACSHA256:
				return "HMACSHA256";
			case MachineKeyValidation.HMACSHA384:
				return "HMACSHA384";
			case MachineKeyValidation.HMACSHA512:
				return "HMACSHA512";
			default:
				throw new ArgumentException(SR.GetString("Wrong_validation_enum"));
			}
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x00130514 File Offset: 0x0012E714
		internal static MachineKeyValidation ConvertToEnum(string strValue)
		{
			if (strValue == null)
			{
				return MachineKeyValidation.SHA1;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(strValue);
			if (num <= 1416239282U)
			{
				if (num != 415037492U)
				{
					if (num != 957249328U)
					{
						if (num == 1416239282U)
						{
							if (strValue == "3DES")
							{
								return MachineKeyValidation.TripleDES;
							}
						}
					}
					else if (strValue == "HMACSHA512")
					{
						return MachineKeyValidation.HMACSHA512;
					}
				}
				else if (strValue == "SHA1")
				{
					return MachineKeyValidation.SHA1;
				}
			}
			else if (num <= 2012598173U)
			{
				if (num != 1935726387U)
				{
					if (num == 2012598173U)
					{
						if (strValue == "HMACSHA384")
						{
							return MachineKeyValidation.HMACSHA384;
						}
					}
				}
				else if (strValue == "MD5")
				{
					return MachineKeyValidation.MD5;
				}
			}
			else if (num != 2018892245U)
			{
				if (num == 2893537640U)
				{
					if (strValue == "AES")
					{
						return MachineKeyValidation.AES;
					}
				}
			}
			else if (strValue == "HMACSHA256")
			{
				return MachineKeyValidation.HMACSHA256;
			}
			if (strValue.StartsWith("alg:", StringComparison.Ordinal))
			{
				return MachineKeyValidation.Custom;
			}
			throw new ArgumentException(SR.GetString("Wrong_validation_enum"));
		}
	}
}
