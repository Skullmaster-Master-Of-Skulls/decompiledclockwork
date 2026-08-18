using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006CF RID: 1743
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ServiceModelEnumValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x0600435C RID: 17244 RVA: 0x000FE91B File Offset: 0x000FCB1B
		public ServiceModelEnumValidatorAttribute(Type enumHelperType)
		{
			this.EnumHelperType = enumHelperType;
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000FE92A File Offset: 0x000FCB2A
		// (set) Token: 0x0600435E RID: 17246 RVA: 0x000FE932 File Offset: 0x000FCB32
		public Type EnumHelperType
		{
			get
			{
				return this.enumHelperType;
			}
			set
			{
				this.enumHelperType = value;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x000FE93B File Offset: 0x000FCB3B
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new ServiceModelEnumValidator(this.enumHelperType);
			}
		}

		// Token: 0x04002D16 RID: 11542
		private Type enumHelperType;
	}
}
