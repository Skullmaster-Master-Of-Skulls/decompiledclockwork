using System;
using System.ComponentModel;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D7 RID: 1751
	internal class StandardRuntimeEnumValidator : ConfigurationValidatorBase
	{
		// Token: 0x060043CA RID: 17354 RVA: 0x001002F9 File Offset: 0x000FE4F9
		public StandardRuntimeEnumValidator(Type enumType)
		{
			this.enumType = enumType;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x00100308 File Offset: 0x000FE508
		public override bool CanValidate(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x00100310 File Offset: 0x000FE510
		public override void Validate(object value)
		{
			if (!Enum.IsDefined(this.enumType, value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, this.enumType));
			}
		}

		// Token: 0x04002D25 RID: 11557
		private Type enumType;
	}
}
