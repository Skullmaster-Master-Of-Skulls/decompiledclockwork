using System;

namespace System.Configuration
{
	// Token: 0x02000094 RID: 148
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SubclassTypeValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x0001CC2E File Offset: 0x0001AE2E
		public SubclassTypeValidatorAttribute(Type baseClass)
		{
			this._baseClass = baseClass;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001CC3D File Offset: 0x0001AE3D
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new SubclassTypeValidator(this._baseClass);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001CC4A File Offset: 0x0001AE4A
		public Type BaseClass
		{
			get
			{
				return this._baseClass;
			}
		}

		// Token: 0x04000355 RID: 853
		private Type _baseClass;
	}
}
