using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D8 RID: 1752
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class StandardRuntimeEnumValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x060043CD RID: 17357 RVA: 0x00100341 File Offset: 0x000FE541
		public StandardRuntimeEnumValidatorAttribute(Type enumType)
		{
			this.EnumType = enumType;
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x00100350 File Offset: 0x000FE550
		// (set) Token: 0x060043CF RID: 17359 RVA: 0x00100358 File Offset: 0x000FE558
		public Type EnumType
		{
			get
			{
				return this.enumType;
			}
			set
			{
				this.enumType = value;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x00100361 File Offset: 0x000FE561
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new StandardRuntimeEnumValidator(this.enumType);
			}
		}

		// Token: 0x04002D26 RID: 11558
		private Type enumType;
	}
}
