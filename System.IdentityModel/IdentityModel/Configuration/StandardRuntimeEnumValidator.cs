using System;
using System.ComponentModel;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D1 RID: 465
	internal class StandardRuntimeEnumValidator : ConfigurationValidatorBase
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x00043ED7 File Offset: 0x000420D7
		public StandardRuntimeEnumValidator(Type enumType)
		{
			this.enumType = enumType;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00043EE6 File Offset: 0x000420E6
		public override bool CanValidate(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00043EEE File Offset: 0x000420EE
		public override void Validate(object value)
		{
			if (!Enum.IsDefined(this.enumType, value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, this.enumType));
			}
		}

		// Token: 0x04000D91 RID: 3473
		private Type enumType;
	}
}
