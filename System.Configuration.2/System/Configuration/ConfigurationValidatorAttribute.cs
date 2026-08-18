using System;

namespace System.Configuration
{
	// Token: 0x0200003D RID: 61
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigurationValidatorAttribute : Attribute
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0001207B File Offset: 0x0001027B
		protected ConfigurationValidatorAttribute()
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00012084 File Offset: 0x00010284
		public ConfigurationValidatorAttribute(Type validator)
		{
			if (validator == null)
			{
				throw new ArgumentNullException("validator");
			}
			if (!typeof(ConfigurationValidatorBase).IsAssignableFrom(validator))
			{
				throw new ArgumentException(SR.GetString("Validator_Attribute_param_not_validator", new object[]
				{
					"ConfigurationValidatorBase"
				}));
			}
			this._validator = validator;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000120E2 File Offset: 0x000102E2
		public virtual ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return (ConfigurationValidatorBase)TypeUtil.CreateInstanceRestricted(this._declaringType, this._validator);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000120FA File Offset: 0x000102FA
		internal void SetDeclaringType(Type declaringType)
		{
			if (declaringType == null)
			{
				return;
			}
			if (this._declaringType == null)
			{
				this._declaringType = declaringType;
				return;
			}
			this._declaringType != declaringType;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00012129 File Offset: 0x00010329
		public Type ValidatorType
		{
			get
			{
				return this._validator;
			}
		}

		// Token: 0x0400021B RID: 539
		internal Type _declaringType;

		// Token: 0x0400021C RID: 540
		private readonly Type _validator;
	}
}
