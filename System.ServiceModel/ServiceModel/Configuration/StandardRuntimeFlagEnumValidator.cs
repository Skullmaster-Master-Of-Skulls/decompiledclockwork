using System;
using System.ComponentModel;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D9 RID: 1753
	internal class StandardRuntimeFlagEnumValidator<TEnum> : ConfigurationValidatorBase where TEnum : struct
	{
		// Token: 0x060043D1 RID: 17361 RVA: 0x0010036E File Offset: 0x000FE56E
		public StandardRuntimeFlagEnumValidator()
		{
			StandardRuntimeFlagEnumValidatorAttribute.ValidateFlagEnumType(typeof(TEnum));
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x00100385 File Offset: 0x000FE585
		public override bool CanValidate(Type type)
		{
			return type == typeof(TEnum);
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x00100398 File Offset: 0x000FE598
		public override void Validate(object value)
		{
			if (!Enum.IsDefined(typeof(TEnum), value))
			{
				TEnum tenum;
				if (!Enum.TryParse<TEnum>(value.ToString(), true, out tenum))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(TEnum)));
				}
				int combinedValue = (int)((object)tenum);
				int[] array = (int[])Enum.GetValues(typeof(TEnum));
				if (!StandardRuntimeFlagEnumValidatorAttribute.IsCombinedValue(combinedValue, array, array.Length - 1))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(TEnum)));
				}
			}
		}
	}
}
