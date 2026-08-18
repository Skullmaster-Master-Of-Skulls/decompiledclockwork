using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D2 RID: 466
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class StandardRuntimeEnumValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x00043F1F File Offset: 0x0004211F
		public StandardRuntimeEnumValidatorAttribute(Type enumType)
		{
			this.EnumType = enumType;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00043F2E File Offset: 0x0004212E
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00043F36 File Offset: 0x00042136
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00043F3F File Offset: 0x0004213F
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new StandardRuntimeEnumValidator(this.enumType);
			}
		}

		// Token: 0x04000D92 RID: 3474
		private Type enumType;
	}
}
