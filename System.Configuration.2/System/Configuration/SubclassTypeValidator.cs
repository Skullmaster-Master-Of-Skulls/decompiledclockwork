using System;

namespace System.Configuration
{
	// Token: 0x02000093 RID: 147
	public sealed class SubclassTypeValidator : ConfigurationValidatorBase
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001CB8B File Offset: 0x0001AD8B
		public SubclassTypeValidator(Type baseClass)
		{
			if (baseClass == null)
			{
				throw new ArgumentNullException("baseClass");
			}
			this._base = baseClass;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001CBAE File Offset: 0x0001ADAE
		public override bool CanValidate(Type type)
		{
			return type == typeof(Type);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public override void Validate(object value)
		{
			if (value == null)
			{
				return;
			}
			if (!(value is Type))
			{
				ValidatorUtils.HelperParamValidation(value, typeof(Type));
			}
			if (!this._base.IsAssignableFrom((Type)value))
			{
				throw new ArgumentException(SR.GetString("Subclass_validator_error", new object[]
				{
					((Type)value).FullName,
					this._base.FullName
				}));
			}
		}

		// Token: 0x04000354 RID: 852
		private Type _base;
	}
}
