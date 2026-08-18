using System;

namespace System.Configuration
{
	// Token: 0x02000027 RID: 39
	public sealed class ConfigurationElementProperty
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000F262 File Offset: 0x0000D462
		public ConfigurationElementProperty(ConfigurationValidatorBase validator)
		{
			if (validator == null)
			{
				throw new ArgumentNullException("validator");
			}
			this._validator = validator;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000F27F File Offset: 0x0000D47F
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this._validator;
			}
		}

		// Token: 0x040001C2 RID: 450
		private ConfigurationValidatorBase _validator;
	}
}
